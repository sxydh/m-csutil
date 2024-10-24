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
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
            finally
            {
                listener?.Stop();
            }
        }
    }
}