using System.Collections;

namespace websocket
{
    public interface IWebSocket
    {
        public IEnumerator Connect();
        public void Close();
    }
}