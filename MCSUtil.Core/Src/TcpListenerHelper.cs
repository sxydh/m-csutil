using System.Net;
using System.Net.Sockets;

namespace MCSUtil.Core
{
    public static class TcpListenerHelper
    {
        public static bool IsPortInUse(int port)
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                return false;
            }
            catch (SocketException)
            {
                return true;
            }
            finally
            {
                listener?.Stop();
            }
        }
    }
}