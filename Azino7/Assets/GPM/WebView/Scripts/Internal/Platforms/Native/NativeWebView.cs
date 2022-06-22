namespace Gpm.WebView.Internal
{
    using System;
    using System.Collections.Generic;
    using Gpm.Common.ThirdParty.LitJson;
    using Gpm.Communicator;
    using UnityEngine;

    public class NativeWebView : IWebView
    {
        protected static class ApiScheme
        {
            public const string SHOW_URL_DEPRECATED = "gpmwebview://showUrlDeprecated";
            public const string SHOW_HTML_FILE_DEPRECATED = "gpmwebview://showHtmlFileDeprecated";
            public const string SHOW_HTML_STRING_DEPRECATED = "gpmwebview://showHtmlStringDeprecated";
            public const string SHOW_URL = "gpmwebview://showUrl";
            public const string SHOW_HTML_FILE = "gpmwebview://showHtmlFile";
            public const string SHOW_HTML_STRING = "gpmwebview://showHtmlString";
            public const string SHOW_SAFE_BROWSING = "gpmwebview://showSafeBrowsing";
            public const string CLOSE = "gpmwebview://close";
            public const string IS_ACTIVE = "gpmwebview://isActive";
            public const string EXECUTE_JAVASCRIPT = "gpmwebview://executeJavaScript";
            public const string SET_FILE_DOWNLOAD_PATH = "gpmwebview://setFileDownloadPath";
            public const string CAN_GO_BACK = "gpmwebview://canGoBack";
            public const string CAN_GO_FORWARD = "gpmwebview://canGoForward";
            public const string GO_BACK = "gpmwebview://goBack";
            public const string GO_FORWARD = "gpmwebview://goForward";
            public const string SET_POSITION = "gpmwebview://setPosition";
            public const string SET_SIZE = "gpmwebview://setSize";
            public const string SET_MARGINS = "gpmwebview://setMargins";
            public const string GET_X = "gpmwebview://getX";
            public const string GET_Y = "gpmwebview://getY";
            public const string GET_WIDTH = "gpmwebview://getWidth";
            public const string GET_HEIGHT = "gpmwebview://getHeight";
        }

        protected static class CallbackScheme
        {
            public const string SCHEME_EVENT_CALLBACK = "gpmwebview://schemeEvent";
            public const string CLOSE_CALLBACK = "gpmwebview://closeCallback";
            public const string CALLBACK = "gpmwebview://callback";
            public const string PAGE_LOAD_CALLBACK = "gpmwebview://pageLoadCallback";

            public const string WEBVIEW_CALLBACK = "gpmwebview://webViewCallback";
        }

        private const string DOMAIN = "GPM_WEBVIEW";
        private const string DEFAULT_NAVIGATION_BAR_COLOR = "#4B96E6";
        private const string DEFAULT_NAVIGATION_TEXT_COLOR = "#FFFFFF";

        protected string CLASS_NAME = string.Empty;

        public bool CanGoBack
        {
            get
            {
                NativeMessage message = new NativeMessage()
                {
                    scheme = ApiScheme.CAN_GO_BACK
                };

                var resultMessage = CallSync(JsonMapper.ToJson(message), string.Empty);

                return Convert.ToBoolean(resultMessage.data);
            }
        }

        public bool CanGoForward
        {
            get
            {
                NativeMessage message = new NativeMessage()
                {
                    scheme = ApiScheme.CAN_GO_FORWARD
                };

                var resultMessage = CallSync(JsonMapper.ToJson(message), string.Empty);

                return Convert.ToBoolean(resultMessage.data);
            }
        }

        public NativeWebView()
        {
            Initialize();
        }

        virtual protected void Initialize()
        {
            GpmCommunicatorVO.Configuration configuration = new GpmCommunicatorVO.Configuration()
            {
                className = CLASS_NAME
            };

            GpmCommunicator.InitializeClass(configuration);
            GpmCommunicator.AddReceiver(DOMAIN, OnAsyncEvent);
        }

        [System.Obsolete("This method is deprecated.")]
        public void ShowUrl(
            string url,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent,
            GpmWebViewCallback.GpmWebViewPageLoadDelegate pageLoadCallback)
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.SHOW_URL_DEPRECATED,
                callback = NativeCallbackHandler.RegisterCallback(openCallback)
            };

            NativeRequest.ShowWebViewDeprecated showWebView = MakeShowWebView(url, configuration, closeCallback, schemeList, schemeEvent, pageLoadCallback);

            nativeMessage.data = JsonMapper.ToJson(showWebView);
            CallAsync(JsonMapper.ToJson(nativeMessage), null);
        }

        [System.Obsolete("This method is deprecated.")]
        public void ShowHtmlFile(
            string filePath,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent,
            GpmWebViewCallback.GpmWebViewPageLoadDelegate pageLoadCallback)
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.SHOW_HTML_FILE_DEPRECATED,
                callback = NativeCallbackHandler.RegisterCallback(openCallback)
            };

            NativeRequest.ShowWebViewDeprecated showWebView = MakeShowWebView(filePath, configuration, closeCallback, schemeList, schemeEvent, pageLoadCallback);

            nativeMessage.data = JsonMapper.ToJson(showWebView);

            CallAsync(JsonMapper.ToJson(nativeMessage), null);
        }

        [System.Obsolete("This method is deprecated.")]
        public void ShowHtmlString(
            string htmlString,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent,
            GpmWebViewCallback.GpmWebViewPageLoadDelegate pageLoadCallback)
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.SHOW_HTML_STRING_DEPRECATED,
                callback = NativeCallbackHandler.RegisterCallback(openCallback)
            };

            NativeRequest.ShowWebViewDeprecated showWebView = MakeShowWebView(htmlString, configuration, closeCallback, schemeList, schemeEvent, pageLoadCallback);

            nativeMessage.data = JsonMapper.ToJson(showWebView);

            CallAsync(JsonMapper.ToJson(nativeMessage), null);
        }

        public void ShowUrl(
            string url,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewDelegate callback,
            List<string> schemeList)
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.SHOW_URL,
                callback = NativeCallbackHandler.RegisterCallback(callback)
            };

            NativeRequest.ShowWebView showWebView = MakeShowWebView(url, configuration, schemeList);

            nativeMessage.data = JsonMapper.ToJson(showWebView);
            CallAsync(JsonMapper.ToJson(nativeMessage), null);
        }

        public void ShowHtmlFile(
            string filePath,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewDelegate callback,
            List<string> schemeList)
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.SHOW_HTML_FILE,
                callback = NativeCallbackHandler.RegisterCallback(callback)
            };

            NativeRequest.ShowWebView showWebView = MakeShowWebView(filePath, configuration, schemeList);

            nativeMessage.data = JsonMapper.ToJson(showWebView);

            CallAsync(JsonMapper.ToJson(nativeMessage), null);
        }

        public void ShowHtmlString(
            string htmlString,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewDelegate callback,
            List<string> schemeList)
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.SHOW_HTML_STRING,
                callback = NativeCallbackHandler.RegisterCallback(callback)
            };

            NativeRequest.ShowWebView showWebView = MakeShowWebView(htmlString, configuration, schemeList);

            nativeMessage.data = JsonMapper.ToJson(showWebView);

            CallAsync(JsonMapper.ToJson(nativeMessage), null);
        }

        public void ShowSafeBrowsing(
            string url,
            GpmWebViewRequest.ConfigurationSafeBrowsing configuration = null,
            GpmWebViewCallback.GpmWebViewDelegate callback = null)
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.SHOW_SAFE_BROWSING,
                callback = NativeCallbackHandler.RegisterCallback(callback)
            };

            NativeRequest.ShowSafeBrowsing showSafeBrowsing = new NativeRequest.ShowSafeBrowsing
            {
                url = url,
                configuration = new NativeRequest.ConfigurationSafeBrowsing()
                {
                    navigationBarColor = (configuration == null) ? DEFAULT_NAVIGATION_BAR_COLOR : configuration.navigationBarColor,
                    navigationTextColor = (configuration == null) ? DEFAULT_NAVIGATION_TEXT_COLOR : configuration.navigationTextColor
                }
            };

            nativeMessage.data = JsonMapper.ToJson(showSafeBrowsing);

            CallAsync(JsonMapper.ToJson(nativeMessage), null);
        }

        public void Close()
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.CLOSE
            };

            string jsonData = JsonMapper.ToJson(nativeMessage);

            CallAsync(jsonData, null);
        }

        public bool IsActive()
        {
            NativeMessage message = new NativeMessage()
            {
                scheme = ApiScheme.IS_ACTIVE
            };

            var resultMessage = CallSync(JsonMapper.ToJson(message), string.Empty);

            return Convert.ToBoolean(resultMessage.data);
        }

        public void ExecuteJavaScript(string script)
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.EXECUTE_JAVASCRIPT
            };

            nativeMessage.data = JsonMapper.ToJson(new NativeRequest.ExecuteJavaScript
            {
                script = script
            });

            string jsonData = JsonMapper.ToJson(nativeMessage);

            CallAsync(jsonData, null);
        }

        public void SetFileDownloadPath(string path)
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.SET_FILE_DOWNLOAD_PATH
            };

            string jsonData = JsonMapper.ToJson(nativeMessage);

            CallAsync(jsonData, null);
        }

        [System.Obsolete("This method is deprecated.")]
        private NativeRequest.ShowWebViewDeprecated MakeShowWebView(
            string data,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent,
            GpmWebViewCallback.GpmWebViewPageLoadDelegate pageLoadCallback)
        {
            NativeRequest.ShowWebViewDeprecated showWebView = new NativeRequest.ShowWebViewDeprecated
            {
                data = data,
                closeCallback = NativeCallbackHandler.RegisterCallback(closeCallback),
                schemeList = schemeList,
                schemeEvent = NativeCallbackHandler.RegisterCallback(schemeEvent),
                pageLoadCallback = NativeCallbackHandler.RegisterCallback(pageLoadCallback),
                configuration = new NativeRequest.Configuration()
                {
                    style = configuration.style,
                    isClearCookie = configuration.isClearCookie,
                    isClearCache = configuration.isClearCache,
                    isNavigationBarVisible = configuration.isNavigationBarVisible,
                    navigationBarColor = configuration.navigationBarColor,
                    title = configuration.title,
                    isBackButtonVisible = configuration.isBackButtonVisible,
                    isForwardButtonVisible = configuration.isForwardButtonVisible,
                    supportMultipleWindows = configuration.supportMultipleWindows,
                    userAgentString = configuration.userAgentString,

                    hasPosition = configuration.position.hasValue,
                    positionX = configuration.position.x,
                    positionY = configuration.position.y,
                    hasSize = configuration.size.hasValue,
                    sizeWidth = configuration.size.width,
                    sizeHeight = configuration.size.height,
                    hasMargins = configuration.margins.hasValue,
                    marginsLeft = configuration.margins.left,
                    marginsTop = configuration.margins.top,
                    marginsRight = configuration.margins.right,
                    marginsBottom = configuration.margins.bottom,

                    contentMode = configuration.contentMode,
                    isMaskViewVisible = configuration.isMaskViewVisible,
                    isAutoRotation = configuration.isAutoRotation
                }
            };

            return showWebView;
        }

        private NativeRequest.ShowWebView MakeShowWebView(
            string data,
            GpmWebViewRequest.Configuration configuration,
            List<string> schemeList)
        {
            NativeRequest.ShowWebView showWebView = new NativeRequest.ShowWebView
            {
                data = data,
                schemeList = schemeList,
                configuration = new NativeRequest.Configuration()
                {
                    style = configuration.style,
                    isClearCookie = configuration.isClearCookie,
                    isClearCache = configuration.isClearCache,
                    isNavigationBarVisible = configuration.isNavigationBarVisible,
                    navigationBarColor = configuration.navigationBarColor,
                    title = configuration.title,
                    isBackButtonVisible = configuration.isBackButtonVisible,
                    isForwardButtonVisible = configuration.isForwardButtonVisible,
                    supportMultipleWindows = configuration.supportMultipleWindows,
                    userAgentString = configuration.userAgentString,

                    hasPosition = configuration.position.hasValue,
                    positionX = configuration.position.x,
                    positionY = configuration.position.y,
                    hasSize = configuration.size.hasValue,
                    sizeWidth = configuration.size.width,
                    sizeHeight = configuration.size.height,
                    hasMargins = configuration.margins.hasValue,
                    marginsLeft = configuration.margins.left,
                    marginsTop = configuration.margins.top,
                    marginsRight = configuration.margins.right,
                    marginsBottom = configuration.margins.bottom,

                    contentMode = configuration.contentMode,
                    isMaskViewVisible = configuration.isMaskViewVisible,
                    isAutoRotation = configuration.isAutoRotation
                }
            };

            return showWebView;
        }

        private void CallAsync(string data, string extra)
        {
            GpmCommunicatorVO.Message message = new GpmCommunicatorVO.Message()
            {
                domain = DOMAIN,
                data = data,
                extra = extra
            };

            GpmCommunicator.CallAsync(message);
        }

        private GpmCommunicatorVO.Message CallSync(string data, string extra)
        {
            GpmCommunicatorVO.Message message = new GpmCommunicatorVO.Message()
            {
                domain = DOMAIN,
                data = data,
                extra = extra
            };

            return GpmCommunicator.CallSync(message);
        }

        private void OnAsyncEvent(GpmCommunicatorVO.Message message)
        {
            Debug.Log("OnAsyncEvent : " + message.data);
            NativeMessage nativeMessage = JsonMapper.ToObject<NativeMessage>(message.data);

            if (nativeMessage == null)
            {
                return;
            }

            if (nativeMessage.scheme == CallbackScheme.WEBVIEW_CALLBACK)
            {
                OnWebViewCallback(nativeMessage);
            }
            else
            {
                OnAsyncEventDeprecated(nativeMessage);
            }
        }

        private void OnAsyncEventDeprecated(NativeMessage nativeMessage)
        {
            switch (nativeMessage.scheme)
            {
                case CallbackScheme.SCHEME_EVENT_CALLBACK:
                    {
                        OnSchemeEventCallback(nativeMessage);
                    }
                    break;
                case CallbackScheme.CLOSE_CALLBACK:
                    {
                        OnCloseCallback(nativeMessage);
                    }
                    break;
                case CallbackScheme.CALLBACK:
                    {
                        OnCallback(nativeMessage);
                    }
                    break;
                case CallbackScheme.PAGE_LOAD_CALLBACK:
                    {
                        OnPageLoadCallback(nativeMessage);
                    }
                    break;
            }
        }

        [System.Obsolete("This method is deprecated.")]
        private void OnCloseCallback(NativeMessage nativeMessage)
        {
            var callback = NativeCallbackHandler.GetCallback<GpmWebViewCallback.GpmWebViewErrorDelegate>(nativeMessage.callback);

            if (callback != null)
            {
                GpmWebViewError error = null;

                if (string.IsNullOrEmpty(nativeMessage.error) == false)
                {
                    error = JsonMapper.ToObject<GpmWebViewError>(nativeMessage.error);
                }

                NativeCallbackHandler.UnregisterCallback(nativeMessage.callback);
                NativeCallbackHandler.UnregisterCallback(int.Parse(nativeMessage.extra));

                callback(error);
            }
        }

        [System.Obsolete("This method is deprecated.")]
        private void OnCallback(NativeMessage nativeMessage)
        {
            var callback = NativeCallbackHandler.GetCallback<GpmWebViewCallback.GpmWebViewErrorDelegate>(nativeMessage.callback);

            if (callback != null)
            {
                GpmWebViewError error = null;

                if (string.IsNullOrEmpty(nativeMessage.error) == false)
                {
                    error = JsonMapper.ToObject<GpmWebViewError>(nativeMessage.error);
                }

                NativeCallbackHandler.UnregisterCallback(nativeMessage.callback);

                callback(error);
            }
        }

        [System.Obsolete("This method is deprecated.")]
        private void OnSchemeEventCallback(NativeMessage nativeMessage)
        {
            var callback = NativeCallbackHandler.GetCallback<GpmWebViewCallback.GpmWebViewDelegate<string>>(nativeMessage.callback);

            if (callback != null)
            {
                GpmWebViewError error = null;

                if (string.IsNullOrEmpty(nativeMessage.error) == false)
                {
                    error = JsonMapper.ToObject<GpmWebViewError>(nativeMessage.error);
                }

                callback(nativeMessage.data, error);
            }
        }

        [System.Obsolete("This method is deprecated.")]
        private void OnPageLoadCallback(NativeMessage nativeMessage)
        {
            var callback = NativeCallbackHandler.GetCallback<GpmWebViewCallback.GpmWebViewPageLoadDelegate>(nativeMessage.callback);

            if (callback != null)
            {
                callback(nativeMessage.data);
            }
        }

        private void OnWebViewCallback(NativeMessage nativeMessage)
        {
            var callback = NativeCallbackHandler.GetCallback<GpmWebViewCallback.GpmWebViewDelegate>(nativeMessage.callback);

            if (callback != null)
            {
                GpmWebViewError error = null;

                if (string.IsNullOrEmpty(nativeMessage.error) == false)
                {
                    error = JsonMapper.ToObject<GpmWebViewError>(nativeMessage.error);
                }

                GpmWebViewCallback.CallbackType callbackType = (GpmWebViewCallback.CallbackType)nativeMessage.callbackType;
                if (callbackType == GpmWebViewCallback.CallbackType.Close)
                {
                    NativeCallbackHandler.UnregisterCallback(nativeMessage.callback);
                }
                callback(callbackType, nativeMessage.data, error);
            }
        }


        public void GoBack()
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.GO_BACK
            };

            string jsonData = JsonMapper.ToJson(nativeMessage);

            CallAsync(jsonData, null);
        }

        public void GoForward()
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.GO_FORWARD
            };

            string jsonData = JsonMapper.ToJson(nativeMessage);

            CallAsync(jsonData, null);
        }

        public void SetPosition(int x, int y)
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.SET_POSITION
            };

            nativeMessage.data = JsonMapper.ToJson(new NativeRequest.Position
            {
                x = x,
                y = y
            });

            string jsonData = JsonMapper.ToJson(nativeMessage);

            CallAsync(jsonData, null);
        }

        public void SetSize(int width, int height)
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.SET_SIZE
            };

            nativeMessage.data = JsonMapper.ToJson(new NativeRequest.Size
            {
                width = width,
                height = height
            });

            string jsonData = JsonMapper.ToJson(nativeMessage);

            CallAsync(jsonData, null);
        }

        public void SetMargins(int left, int top, int right, int bottom)
        {
            NativeMessage nativeMessage = new NativeMessage
            {
                scheme = ApiScheme.SET_MARGINS
            };

            nativeMessage.data = JsonMapper.ToJson(new NativeRequest.Margins
            {
                left = left,
                top = top,
                right = right,
                bottom = bottom
            });

            string jsonData = JsonMapper.ToJson(nativeMessage);

            CallAsync(jsonData, null);
        }

        public int GetX()
        {
            NativeMessage message = new NativeMessage()
            {
                scheme = ApiScheme.GET_X
            };

            var resultMessage = CallSync(JsonMapper.ToJson(message), string.Empty);

            return Convert.ToInt32(resultMessage.data);
        }

        public int GetY()
        {
            NativeMessage message = new NativeMessage()
            {
                scheme = ApiScheme.GET_Y
            };

            var resultMessage = CallSync(JsonMapper.ToJson(message), string.Empty);

            return Convert.ToInt32(resultMessage.data);
        }

        public int GetWidth()
        {
            NativeMessage message = new NativeMessage()
            {
                scheme = ApiScheme.GET_WIDTH
            };

            var resultMessage = CallSync(JsonMapper.ToJson(message), string.Empty);

            return Convert.ToInt32(resultMessage.data);
        }

        public int GetHeight()
        {
            NativeMessage message = new NativeMessage()
            {
                scheme = ApiScheme.GET_HEIGHT
            };

            var resultMessage = CallSync(JsonMapper.ToJson(message), string.Empty);

            return Convert.ToInt32(resultMessage.data);
        }
    }
}
