using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;

namespace websocket
{
    public class WebSocket : IWebSocket
    {
        private readonly Uri serverUrl;

        public WebSocket(Uri url)
        {
            serverUrl = url;

            var protocol = serverUrl.Scheme;
            if (!protocol.Equals("ws") && !protocol.Equals("wss"))
            {
                throw new ArgumentException("Unsupported protocol: " + protocol);
            }
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        int nativeRef;

        [DllImport("__Internal")]
        private static extern int SocketCreate(string url);

        [DllImport("__Internal")]
        private static extern int SocketState(int socketInstance);

        [DllImport("__Internal")]
        private static extern void SocketClose(int socketInstance);

        public IEnumerator Connect()
        {
            nativeRef = SocketCreate(serverUrl.ToString());

            while (SocketState(nativeRef) == 0)
                yield return 0;
        }

        public void Close()
        {
            SocketClose(nativeRef);
        }
#else
        public IEnumerator Connect()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
#endif
    }
}