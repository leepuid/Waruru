using System.Collections;
using UnityEngine;
using System.IO;

public class CaptureShare : MonoBehaviour
{

    public void ClickShare()
    {
        StartCoroutine(TakeScreenShot());
    }

    private IEnumerator TakeScreenShot()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        Destroy(ss);

        new NativeShare().AddFile(filePath).SetSubject("Share test").SetText("Test").Share();
    }
}