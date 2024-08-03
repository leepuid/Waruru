using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPGSManager : MonoBehaviour
{
    public void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            string id = PlayGamesPlatform.Instance.GetUserId();
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            Debug.Log($"Successfully Logged In. ID: {id}, Name: {name}\n");
        }
        else
        {
            Debug.Log($"Log In Failed. \n");
        }
    }
}
