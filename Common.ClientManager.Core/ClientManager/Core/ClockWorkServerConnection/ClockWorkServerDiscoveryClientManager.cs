using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.ClientManager.ICore.ClockWorkServerConnection;
using TechnoPro.Common.Public.Entities.ClockWorkServer;
using TechnoPro.Common.WCF.Discovery;

namespace TechnoPro.Common.ClientManager.Core.ClockWorkServerConnection
{
	// Token: 0x02000077 RID: 119
	public class ClockWorkServerDiscoveryClientManager : IClockWorkServerDiscoveryClientManager
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x000143D4 File Offset: 0x000125D4
		public IList<Uri> GetAvailableClockWorkServerList(Uri discoveryScopeUri = null, int discoveryDurationInSeconds = 5)
		{
			IList<EndpointAddress> source = DiscoveryHelper.DiscoverAddresses<IClockWorkServerDiscovery>(discoveryDurationInSeconds, discoveryScopeUri ?? new Uri("net.tcp://clockworks.ca"));
			return (from a in source
			select a.Uri).ToList<Uri>();
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00014428 File Offset: 0x00012628
		public object FindAvailableClockWorkServerListAsync(Uri discoveryScopeUri = null, int discoveryDurationInSeconds = 10)
		{
			FindCriteria findCriteria = new FindCriteria(typeof(IClockWorkServerDiscovery))
			{
				Duration = TimeSpan.FromSeconds((double)discoveryDurationInSeconds)
			};
			findCriteria.Scopes.Add(discoveryScopeUri ?? new Uri("net.tcp://clockworks.ca"));
			DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());
			discoveryClient.FindProgressChanged += delegate(object sender, FindProgressChangedEventArgs args)
			{
				EventHandler<ServiceDiscoveryInfo> findProgressChanged = this.FindProgressChanged;
				bool flag = findProgressChanged != null;
				if (flag)
				{
					findProgressChanged(sender, new ServiceDiscoveryInfo
					{
						EnpointAddress = args.EndpointDiscoveryMetadata.Address.Uri,
						Scopes = args.EndpointDiscoveryMetadata.Scopes
					});
				}
			};
			discoveryClient.FindCompleted += delegate(object sender, FindCompletedEventArgs args)
			{
				EventHandler<IList<ServiceDiscoveryInfo>> findCompleted = this.FindCompleted;
				bool flag = findCompleted != null;
				if (flag)
				{
					findCompleted(sender, (from e in args.Result.Endpoints
					select new ServiceDiscoveryInfo
					{
						EnpointAddress = e.Address.Uri,
						Scopes = e.Scopes
					}).ToList<ServiceDiscoveryInfo>());
				}
			};
			Guid guid = Guid.NewGuid();
			discoveryClient.FindAsync(findCriteria, guid);
			return guid;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000144C0 File Offset: 0x000126C0
		public void CancelFindAvailableClockWorkServerListAsync(object userState)
		{
			DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());
			discoveryClient.CancelAsync(userState);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000463 RID: 1123 RVA: 0x000144E4 File Offset: 0x000126E4
		// (remove) Token: 0x06000464 RID: 1124 RVA: 0x0001451C File Offset: 0x0001271C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ServiceDiscoveryInfo> FindProgressChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000465 RID: 1125 RVA: 0x00014554 File Offset: 0x00012754
		// (remove) Token: 0x06000466 RID: 1126 RVA: 0x0001458C File Offset: 0x0001278C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<IList<ServiceDiscoveryInfo>> FindCompleted;
	}
}
