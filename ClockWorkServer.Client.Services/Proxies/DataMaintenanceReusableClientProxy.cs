using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Data;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200007E RID: 126
	public class DataMaintenanceReusableClientProxy : WCFTokenBasedReusableClientProxy<IDataMaintenance>, IDataMaintenance, IService
	{
		// Token: 0x06000553 RID: 1363 RVA: 0x0000EE0E File Offset: 0x0000D00E
		public DataMaintenanceReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000EE19 File Offset: 0x0000D019
		public DataMaintenanceReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000EE28 File Offset: 0x0000D028
		public LoadAssignmentsForStaffDropListResp LoadAssignmentsForStaffDropList(LoadAssignmentsForStaffDropListReq Request)
		{
			return this.WrapServiceMethod<LoadAssignmentsForStaffDropListResp>(() => this.Proxy.LoadAssignmentsForStaffDropList(Request));
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000EE60 File Offset: 0x0000D060
		public ReassignStaffDropListResp ReassignStaffDropList(ReassignStaffDropListReq Request)
		{
			return this.WrapServiceMethod<ReassignStaffDropListResp>(() => this.Proxy.ReassignStaffDropList(Request));
		}
	}
}
