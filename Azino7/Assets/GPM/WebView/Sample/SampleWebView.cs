using System.Collections;
using System.Collections.Generic;
using Gpm.WebView;
using UnityEngine;

public class SampleWebView : MonoBehaviour
{
    public string sampleUrl = "https://www.google.com";
    public List<string> customSchemeList = new List<string> { "CUSTOM_SCHEME" };

    public void OpenWithFullScreen()
    {
        GpmWebView.ShowUrl(sampleUrl,
            GetConfigurationFullScreen("FULL SCREEN", "#4B96E6", true, true),
            OnWebViewCallback,
            customSchemeList);
    }

    public void OpenWithFullScreenCustom()
    {
        GpmWebView.ShowUrl(sampleUrl,
            GetConfigurationFullScreen("FULL SCREEN CUSTOM", "#125de6", false, false),
            OnWebViewCallback,
            customSchemeList);
    }

    private GpmWebViewRequest.Configuration GetConfigurationFullScreen(string title, string navigationBarColor, bool showNavigationBar, bool supportMultipleWindow)
    {
        return new GpmWebViewRequest.Configuration()
        {
            style = GpmWebViewStyle.FULLSCREEN,
            isClearCache = true,
            isClearCookie = true,
            title = title,
            navigationBarColor = navigationBarColor,

            isNavigationBarVisible = showNavigationBar,
            isBackButtonVisible = showNavigationBar,
            isForwardButtonVisible = showNavigationBar,

            supportMultipleWindows = supportMultipleWindow,

            contentMode = GpmWebViewContentMode.RECOMMENDED,
        };
    }

    public void OpenWithPopup()
    {
        GpmWebView.ShowUrl(sampleUrl,
            GetConfigurationPopup(true, true, true),
            OnWebViewCallback,
            customSchemeList);
    }

    public void OpenWithPopupConfigurationFrame()
    {
        GpmWebViewRequest.Configuration configuration = GetConfigurationPopup(true, true, false);
        configuration.position = new GpmWebViewRequest.Position
        {
            hasValue = true,
            x = (int)(Screen.width * 0.1f),
            y = (int)(Screen.height * 0.1f)
        };
        configuration.size = new GpmWebViewRequest.Size
        {
            hasValue = true,
            width = (int)(Screen.width * 0.8f),
            height = (int)(Screen.height * 0.8f)
        };

        GpmWebView.ShowUrl(sampleUrl, configuration, OnWebViewCallback, customSchemeList);
    }

    public void OpenWithPopupConfigurationMargins()
    {
        GpmWebViewRequest.Configuration configuration = GetConfigurationPopup(true, false, false);
        configuration.margins = new GpmWebViewRequest.Margins
        {
            hasValue = true,
            left = (int)(Screen.width * 0.1f),
            top = (int)(Screen.height * 0.1f),
            right = (int)(Screen.width * 0.1f),
            bottom = (int)(Screen.height * 0.1f)
        };

        GpmWebView.ShowUrl(sampleUrl, configuration, OnWebViewCallback, customSchemeList);
    }

    public void OpenWithPopupChangeFrame()
    {
        GpmWebView.ShowUrl(sampleUrl,
            GetConfigurationPopup(false, true, true),
            OnWebViewCallback,
            customSchemeList);

        StartCoroutine(CoCheckWebView(() =>
        {
            GpmWebView.SetPosition((int)(Screen.width * 0.1f), (int)(Screen.height * 0.1f));
            GpmWebView.SetSize((int)(Screen.width * 0.8f), (int)(Screen.height * 0.8f));
        }));
    }

    public void OpenWithPopupChangeMargins()
    {
        GpmWebView.ShowUrl(sampleUrl,
            GetConfigurationPopup(false, true, true),
            OnWebViewCallback,
            customSchemeList);

        StartCoroutine(CoCheckWebView(() =>
        {
            GpmWebView.SetMargins((int)(Screen.width * 0.1f), (int)(Screen.height * 0.1f), (int)(Screen.width * 0.1f), (int)(Screen.height * 0.1f));
        }));
    }

    private GpmWebViewRequest.Configuration GetConfigurationPopup(bool showNavigationBar, bool supportMultipleWindow, bool maskViewVisible)
    {
        return new GpmWebViewRequest.Configuration()
        {
            style = GpmWebViewStyle.POPUP,
            isClearCache = true,
            isClearCookie = true,
            title = "POPUP SAMPLE",

            isNavigationBarVisible = showNavigationBar,
            isBackButtonVisible = showNavigationBar,
            isForwardButtonVisible = showNavigationBar,

            supportMultipleWindows = supportMultipleWindow,

            contentMode = GpmWebViewContentMode.RECOMMENDED,
            isMaskViewVisible = maskViewVisible
        };
    }

    public void OpenWithSafeBrowsing()
    {
        GpmWebViewSafeBrowsing.ShowSafeBrowsing(sampleUrl,
            new GpmWebViewRequest.ConfigurationSafeBrowsing()
            {
                navigationBarColor = "#4B96E6",
                navigationTextColor = "#FFFFFF"
            },
            OnWebViewCallback);
    }

    private IEnumerator CoCheckWebView(System.Action onUpdate)
    {
        while (true)
        {
            if (GpmWebView.IsActive() == true)
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        if (onUpdate != null)
        {
            onUpdate();
        }
    }

    private void OnWebViewCallback(GpmWebViewCallback.CallbackType callbackType, string data, GpmWebViewError error)
    {
        Debug.Log("OnWebViewCallback: " + callbackType);
        switch (callbackType)
        {
            case GpmWebViewCallback.CallbackType.Open:
                if (error != null)
                {
                    Debug.LogFormat("Fail to open WebView. Error:{0}", error);
                }
                break;
            case GpmWebViewCallback.CallbackType.Close:
                if (error != null)
                {
                    Debug.LogFormat("Fail to close WebView. Error:{0}", error);
                }
                break;
            case GpmWebViewCallback.CallbackType.PageLoad:
                if (string.IsNullOrEmpty(data) == false)
                {
                    Debug.LogFormat("Loaded Page:{0}", data);
                }
                break;
            case GpmWebViewCallback.CallbackType.MultiWindowOpen:
                break;
            case GpmWebViewCallback.CallbackType.MultiWindowClose:
                break;
            case GpmWebViewCallback.CallbackType.Scheme:
                Debug.LogFormat("Scheme:{0}", data);
                break;
        }
    }
}
