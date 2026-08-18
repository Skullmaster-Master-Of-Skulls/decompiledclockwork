using System;
using System.Net;

namespace Renci.SshNet.Abstractions
{
	// Token: 0x02000116 RID: 278
	internal static class DnsAbstraction
	{
		// Token: 0x06000C0F RID: 3087 RVA: 0x000271B9 File Offset: 0x000253B9
		public static IPAddress[] GetHostAddresses(string hostNameOrAddress)
		{
			return Dns.GetHostAddresses(hostNameOrAddress);
		}
	}
}
