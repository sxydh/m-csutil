using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace MCSUtil.Core
{
    public static class IPHelper
    {
        public static List<string> GetAliveIPList()
        {
            return GetAliveIPList(GetSubnetIPList());
        }

        public static List<string> GetAliveIPList(List<string> ipList)
        {
            if (ipList == null || ipList.Count == 0)
            {
                return new List<string>();
            }

            var aliveIPList = new ConcurrentBag<string>();
            using (var countdownEvent = new CountdownEvent(ipList.Count))
            {
                foreach (var ip in ipList)
                {
                    var ping = new Ping();
                    ping.PingCompleted += (sender, e) =>
                    {
                        if (e.Reply.Status == IPStatus.Success)
                        {
                            aliveIPList.Add((string)e.UserState);
                        }

                        ping.Dispose();
                        // ReSharper disable once AccessToDisposedClosure
                        countdownEvent.Signal();
                    };
                    ping.SendAsync(ip, 5000, ip);
                }

                countdownEvent.Wait();
            }

            return aliveIPList.ToList();
        }

        public static List<string> GetSubnetIPList()
        {
            /* 获取子网掩码和网关 */
            string subnetMask = null;
            string gateway = null;
            foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (subnetMask != null && gateway != null)
                {
                    break;
                }

                if (networkInterface.Name != "WLAN")
                {
                    continue;
                }

                if (networkInterface.OperationalStatus != OperationalStatus.Up)
                {
                    continue;
                }

                // 获取子网掩码
                if (subnetMask == null)
                {
                    var unicastIPAddressInformationCollection = networkInterface.GetIPProperties().UnicastAddresses;
                    foreach (var unicastIPAddressInformation in unicastIPAddressInformationCollection)
                    {
                        if (unicastIPAddressInformation.Address.AddressFamily != AddressFamily.InterNetwork)
                        {
                            continue;
                        }

                        subnetMask = unicastIPAddressInformation.IPv4Mask.ToString();
                        break;
                    }
                }

                // 获取网关
                if (gateway == null)
                {
                    var gatewayIPAddressInformationCollection = networkInterface.GetIPProperties().GatewayAddresses;
                    foreach (var gatewayIPAddressInformation in gatewayIPAddressInformationCollection)
                    {
                        if (gatewayIPAddressInformation.Address.AddressFamily != AddressFamily.InterNetwork)
                        {
                            continue;
                        }

                        gateway = gatewayIPAddressInformation.Address.ToString();
                        break;
                    }
                }
            }

            if (subnetMask == null || gateway == null)
            {
                return new List<string>();
            }

            /* 计算子网列表 */
            var subnetMaskArray = subnetMask.Split('.');
            var gatewayArray = gateway.Split('.');
            var subnetArray = new string[4];
            for (var i = 0; i < 4; i++)
            {
                subnetArray[i] = (int.Parse(subnetMaskArray[i]) & int.Parse(gatewayArray[i])).ToString();
            }

            var subnetIPList = new List<string>();
            for (var i = 1; i < 255; i++)
            {
                subnetIPList.Add(subnetArray[0] + "." + subnetArray[1] + "." + subnetArray[2] + "." + i);
            }

            return subnetIPList;
        }
    }
}