using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WebViewHandler))]
public class StartView : View
{
    
    private WebViewHandler _webViewHandler;
    public override void Initialize()
    {
        _webViewHandler = GetComponent<WebViewHandler>();
    }

    public override void Show(object parameter = null)
    {
        base.Show(parameter);

        StartCoroutine(CheckInternetConnection((isConnected) => {
            if (isConnected)
                ConnectedSuccess();
            else
                ConnectionFailed();
        }));
    }

    private void ConnectedSuccess()
    {
#if UNITY_EDITOR
        Debug.Log("Open URL");
        ViewManager.Show<MenuView>();
#else
          _webViewHandler.ShowUrlFullScreen("https://www.google.com",()=> 
        {
            ViewManager.Show<MenuView>();
        });
#endif

    }

    private void ConnectionFailed()
    {
        ViewManager.Show<MenuView>();
    }

    private IEnumerator CheckInternetConnection(Action<bool> action)
    {
        WWW www = new WWW("https://www.google.com");
        yield return www;
        if (www.error != null)
        {
            Debug.Log("CheckInternetConnection - fail!");
            action(false);
        }
        else
        {
            Debug.Log("CheckInternetConnection - success!");
            action(true);
        }
    }
  
}
