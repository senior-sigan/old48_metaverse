using System;
using System.Collections;
using UnityEngine;
using websocket;

public class Networking : MonoBehaviour
{
    private IWebSocket ws = new WebSocket(new Uri("ws://localhost:8000"));

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return ws.Connect();

        while (true)
        {
            yield return new WaitForFixedUpdate();
        }
    }
}