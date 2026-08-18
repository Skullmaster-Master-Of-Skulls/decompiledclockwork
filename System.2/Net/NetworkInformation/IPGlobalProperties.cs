using System;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002A1 RID: 673
	[__DynamicallyInvokable]
	public abstract class IPGlobalProperties
	{
		// Token: 0x06001919 RID: 6425 RVA: 0x0007DFCD File Offset: 0x0007C1CD
		[__DynamicallyInvokable]
		public static IPGlobalProperties GetIPGlobalProperties()
		{
			new NetworkInformationPermission(NetworkInformationAccess.Read).Demand();
			return new SystemIPGlobalProperties();
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0007DFDF File Offset: 0x0007C1DF
		internal static IPGlobalProperties InternalGetIPGlobalProperties()
		{
			return new SystemIPGlobalProperties();
		}

		// Token: 0x0600191B RID: 6427
		[__DynamicallyInvokable]
		public abstract IPEndPoint[] GetActiveUdpListeners();

		// Token: 0x0600191C RID: 6428
		[__DynamicallyInvokable]
		public abstract IPEndPoint[] GetActiveTcpListeners();

		// Token: 0x0600191D RID: 6429
		[__DynamicallyInvokable]
		public abstract TcpConnectionInformation[] GetActiveTcpConnections();

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600191E RID: 6430
		[__DynamicallyInvokable]
		public abstract string DhcpScopeName { [__DynamicallyInvokable] get; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600191F RID: 6431
		[__DynamicallyInvokable]
		public abstract string DomainName { [__DynamicallyInvokable] get; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001920 RID: 6432
		[__DynamicallyInvokable]
		public abstract string HostName { [__DynamicallyInvokable] get; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001921 RID: 6433
		[__DynamicallyInvokable]
		public abstract bool IsWinsProxy { [__DynamicallyInvokable] get; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001922 RID: 6434
		[__DynamicallyInvokable]
		public abstract NetBiosNodeType NodeType { [__DynamicallyInvokable] get; }

		// Token: 0x06001923 RID: 6435
		[__DynamicallyInvokable]
		public abstract TcpStatistics GetTcpIPv4Statistics();

		// Token: 0x06001924 RID: 6436
		[__DynamicallyInvokable]
		public abstract TcpStatistics GetTcpIPv6Statistics();

		// Token: 0x06001925 RID: 6437
		[__DynamicallyInvokable]
		public abstract UdpStatistics GetUdpIPv4Statistics();

		// Token: 0x06001926 RID: 6438
		[__DynamicallyInvokable]
		public abstract UdpStatistics GetUdpIPv6Statistics();

		// Token: 0x06001927 RID: 6439
		[__DynamicallyInvokable]
		public abstract IcmpV4Statistics GetIcmpV4Statistics();

		// Token: 0x06001928 RID: 6440
		[__DynamicallyInvokable]
		public abstract IcmpV6Statistics GetIcmpV6Statistics();

		// Token: 0x06001929 RID: 6441
		[__DynamicallyInvokable]
		public abstract IPGlobalStatistics GetIPv4GlobalStatistics();

		// Token: 0x0600192A RID: 6442
		[__DynamicallyInvokable]
		public abstract IPGlobalStatistics GetIPv6GlobalStatistics();

		// Token: 0x0600192B RID: 6443 RVA: 0x0007DFE6 File Offset: 0x0007C1E6
		[__DynamicallyInvokable]
		public virtual UnicastIPAddressInformationCollection GetUnicastAddresses()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x0007DFED File Offset: 0x0007C1ED
		[__DynamicallyInvokable]
		public virtual IAsyncResult BeginGetUnicastAddresses(AsyncCallback callback, object state)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0007DFF4 File Offset: 0x0007C1F4
		[__DynamicallyInvokable]
		public virtual UnicastIPAddressInformationCollection EndGetUnicastAddresses(IAsyncResult asyncResult)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0007DFFB File Offset: 0x0007C1FB
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<UnicastIPAddressInformationCollection> GetUnicastAddressesAsync()
		{
			return Task<UnicastIPAddressInformationCollection>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetUnicastAddresses), new Func<IAsyncResult, UnicastIPAddressInformationCollection>(this.EndGetUnicastAddresses), null);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x0007E022 File Offset: 0x0007C222
		[__DynamicallyInvokable]
		protected IPGlobalProperties()
		{
		}
	}
}
