using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class ResourceManager
{
    public bool Loaded { get; set; }
    private Dictionary<string, UnityEngine.Object> resources = new();

    // 리소스 해제를 위한 Dictionary
    private Dictionary<string, AsyncOperationHandle> resourcesHandle = new();
    private Dictionary<string, IList<IResourceLocation>> resourcesLabelHandle = new();


    #region 객체 생성/파괴
    public GameObject Instantiate(string key, Transform parent = null, bool instantiateInWorld = false, bool pooling = false)
    {
        // 리소스에서 GameObject 타입으로 키를 통해 객체를 가져옵니다.
        GameObject go = GetResource<GameObject>(key);
        if (go == null)
        {
            // 객체를 찾지 못하면 오류 메시지를 출력하고 null을 반환합니다.
            Debug.LogError($"[ResourceManager] Instantiate({key}): Failed to load prefab.");
            return null;
        }

        // 풀링이 활성화된 경우 게임 매니저 풀에서 객체를 반환합니다.
        if (pooling) return Main.Pool.Pop(go);

        // 새로운 객체를 인스턴스화하여 반환합니다.
        return UnityEngine.Object.Instantiate(go, parent, instantiateInWorld);
    }


    public void Destroy(GameObject obj)
    {
        // 객체가 null인 경우 아무 작업도 하지 않습니다.
        if (obj == null) return;

        // 풀에 객체를 푸시할 수 있는 경우, 해당 객체를 풀에 반환합니다.
        if (Main.Pool.Push(obj)) return;

        // 풀에 반환하지 못한 경우 객체를 삭제합니다.
        UnityEngine.Object.Destroy(obj);
    }

    // resources(딕셔너리)에서 값을 반환
    public T GetResource<T>(string key) where T : UnityEngine.Object
    {
        // 키를 통해 딕셔너리에서 리소스를 가져옵니다.
        if (!resources.TryGetValue(key, out UnityEngine.Object resource)) return null;
        return resource as T;
    }
    #endregion

    // 메모리 해제
    public void ReleaseAsset(string key)
    {
        // 키를 통해 저장된 리소스 핸들을 찾습니다.
        if (resourcesHandle.TryGetValue(key, out AsyncOperationHandle operationHandle) == false) return;
        // Addressables 시스템에서 리소스를 해제합니다.
        Addressables.Release(operationHandle);
        // 딕셔너리에서 리소스와 핸들을 제거합니다.
        resources.Remove(key);
        resourcesHandle.Remove(key);
    }

    // 라벨 메모리 해제
    public void ReleaseAllAsset(string key)
    {
        // 키를 통해 저장된 리소스 라벨 핸들을 찾습니다.
        if (resourcesLabelHandle.TryGetValue(key, out IList<IResourceLocation> operationHandle) == false) return;
        // 각 리소스의 PrimaryKey를 통해 리소스를 해제합니다.
        foreach (IResourceLocation handle in operationHandle)
        {
            ReleaseAsset(handle.PrimaryKey);
        }
        // 라벨 핸들을 딕셔너리에서 제거합니다.
        resourcesLabelHandle.Remove(key);
    }

    // 단일 로드
    public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    {
        // 리소스가 이미 로드된 경우 콜백을 실행합니다.
        if (resources.TryGetValue(key, out UnityEngine.Object resource))
        {
            callback?.Invoke(resource as T);
            return;
        }

        string loadKey = key;

        // 스프라이트 리소스 로드 키 설정
        if (key.Contains(".sprite"))
            loadKey = $"{key}[{key.Replace(".sprite", "")}]";

        // 리소스 비동기 로드 시작
        if (key.Contains(".sprite"))
        {
            AsyncOperationHandle<Sprite> asyncOperation = Addressables.LoadAssetAsync<Sprite>(loadKey);
            asyncOperation.Completed += obj => {
                resources.Add(key, obj.Result);
                resourcesHandle.Add(key, obj);
                callback?.Invoke(obj.Result as T);
            };
        }
        else if (key.Contains(".multiSprite"))
        {
            AsyncOperationHandle<IList<Sprite>> handle = Addressables.LoadAssetAsync<IList<Sprite>>(loadKey);
            HandleCallback<Sprite>(key, handle, objs => callback?.Invoke(objs as T));
        }
        else
        {
            var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
            asyncOperation.Completed += obj => {
                resources.Add(key, obj.Result);
                resourcesHandle.Add(key, obj);
                callback?.Invoke(obj.Result as T);
            };
        }
    }

    // 단일 로드 && 인스턴스화
    public void InstantiateAssetAsync(string key, Transform parent = null, bool instantiateInWorld = false)
    {
        // 리소스가 이미 로드된 경우 인스턴스화합니다.
        if (resources.TryGetValue(key, out UnityEngine.Object resource))
        {
            Instantiate(key, parent, instantiateInWorld);
            return;
        }

        // Addressables 시스템에서 비동기로 GameObject를 인스턴스화합니다.
        AsyncOperationHandle<GameObject> asyncOperation = Addressables.InstantiateAsync(key, parent, instantiateInWorld);
        asyncOperation.Completed += (AsyncOperationHandle<GameObject> obj) => {
            resources.Add(key, obj.Result);
            resourcesHandle.Add(key, obj);
        };
    }

    // 라벨 로드
    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
    {
        // 라벨에 해당하는 리소스 위치들을 비동기로 로드합니다.
        Addressables.LoadResourceLocationsAsync(label, typeof(T)).Completed += (obj) => {
            int loadCount = 0;
            int totalCount = obj.Result.Count;
            resourcesLabelHandle.TryAdd(label, obj.Result);
            foreach (IResourceLocation location in obj.Result)
            {
                // 각 위치에 해당하는 리소스를 비동기로 로드합니다.
                LoadAsync<T>(location.PrimaryKey, obj => {
                    loadCount++;
                    callback?.Invoke(location.PrimaryKey, loadCount, totalCount);
                });
            }
        };

        Loaded = true;
    }

    // 라벨 로드 && 인스턴스화
    public void InstantialteAllAsync(string label, Transform parent = null, bool instantiateInWorld = false)
    {
        // 라벨에 해당하는 리소스 위치들을 비동기로 로드합니다.
        AsyncOperationHandle<IList<IResourceLocation>> operation = Addressables.LoadResourceLocationsAsync(label, typeof(GameObject));
        operation.Completed += (AsyncOperationHandle<IList<IResourceLocation>> obj) => {
            resourcesLabelHandle.Add(label, obj.Result);
            foreach (IResourceLocation location in obj.Result)
            {
                // 각 위치에 해당하는 리소스를 비동기로 인스턴스화합니다.
                InstantiateAssetAsync(location.PrimaryKey, parent, instantiateInWorld);
            }
        };
    }

    // 콜백 처리 함수
    private void HandleCallback<T>(string key, AsyncOperationHandle<IList<T>> handle, Action<IList<T>> callback) where T : UnityEngine.Object
    {
        handle.Completed += operationHandle => {
            IList<T> resultList = operationHandle.Result;
            // 리스트의 각 아이템을 resources에 추가합니다.
            for (int i = 0; i < resultList.Count; i++)
            {
                resources.Add(resultList[i].name, resultList[i]);
            }
            callback?.Invoke(resultList);
        };
    }
}
