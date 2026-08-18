using System;
using System.Net.Sockets;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x0200009E RID: 158
	internal class NetworkSenderFactory : INetworkSenderFactory
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		public NetworkSender Create(string url, int maxQueueSize)
		{
			if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
			{
				return new HttpNetworkSender(url);
			}
			if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
			{
				return new HttpNetworkSender(url);
			}
			if (url.StartsWith("tcp://", StringComparison.OrdinalIgnoreCase))
			{
				return new TcpNetworkSender(url, AddressFamily.Unspecified)
				{
					MaxQueueSize = maxQueueSize
				};
			}
			if (url.StartsWith("tcp4://", StringComparison.OrdinalIgnoreCase))
			{
				return new TcpNetworkSender(url, AddressFamily.InterNetwork)
				{
					MaxQueueSize = maxQueueSize
				};
			}
			if (url.StartsWith("tcp6://", StringComparison.OrdinalIgnoreCase))
			{
				return new TcpNetworkSender(url, AddressFamily.InterNetworkV6)
				{
					MaxQueueSize = maxQueueSize
				};
			}
			if (url.StartsWith("udp://", StringComparison.OrdinalIgnoreCase))
			{
				return new UdpNetworkSender(url, AddressFamily.Unspecified);
			}
			if (url.StartsWith("udp4://", StringComparison.OrdinalIgnoreCase))
			{
				return new UdpNetworkSender(url, AddressFamily.InterNetwork);
			}
			if (url.StartsWith("udp6://", StringComparison.OrdinalIgnoreCase))
			{
				return new UdpNetworkSender(url, AddressFamily.InterNetworkV6);
			}
			throw new ArgumentException("Unrecognized network address", "url");
		}

		// Token: 0x04000103 RID: 259
		public static readonly INetworkSenderFactory Default = new NetworkSenderFactory();
	}
}
