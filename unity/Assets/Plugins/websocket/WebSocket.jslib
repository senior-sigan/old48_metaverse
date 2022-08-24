const LibraryWebSocket = {
  $webSocketInstances: [],
  SocketCreate: function (url) {
    const urlStr = UTF8ToString(url);
    const socket = {
      ws: new WebSocket(urlStr),
      buffer: new Uint8Array(0),
      error: null,
      messages: [],
    };
    // socket.ws.binaryType = "arraybuffer";
    socket.ws.onmessage = (ev) => {
      console.log("[INFO] OnMessage ", ev.data);
    };
    socket.ws.onclose = (ev) => {
      console.log("[INFO] Socket close");
    };
    socket.ws.onopen = (ev) => {
      console.log("[INFO] Socket open");
    };

    const newLen = webSocketInstances.push(socket);
    return newLen - 1;
  },
  /**
   * -1 ERROR      Socket doesn't exist
   * 0  CONNECTING Socket has been created. The connection is not yet open.
   * 1  OPEN       The connection is open and ready to communicate.
   * 2  CLOSING    The connection is in the process of closing.
   * 3  CLOSED     The connection is closed or couldn't be opened.
   *
   * @param {number} socketInstance
   * @returns {number}
   */
  SocketState: function (socketInstance) {
    const socket = webSocketInstances[socketInstance];
    if (socket) {
      return socket.ws.readyState;
    }
    return -1;
  },
  SocketClose: function (socketInstance) {
    const socket = webSocketInstances[socketInstance];
    if (socket) socket.ws.close();
  },
};

autoAddDeps(LibraryWebSocket, "$webSocketInstances");
mergeInto(LibraryManager.library, LibraryWebSocket);
