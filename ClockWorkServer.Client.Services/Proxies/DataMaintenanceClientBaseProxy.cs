using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Data;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200007F RID: 127
	internal class DataMaintenanceClientBaseProxy : ClientBase<IDataMaintenance>, IDataMaintenance, IService
	{
		// Token: 0x06000557 RID: 1367 RVA: 0x0000EE98 File Offset: 0x0000D098
		public DataMaintenanceClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000EEA3 File Offset: 0x0000D0A3
		public DataMaintenanceClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000EEB0 File Offset: 0x0000D0B0
		public LoadAssignmentsForStaffDropListResp LoadAssignmentsForStaffDropList(LoadAssignmentsForStaffDropListReq Request)
		{
			return base.Channel.LoadAssignmentsForStaffDropList(Request);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		public ReassignStaffDropListResp ReassignStaffDropList(ReassignStaffDropListReq Request)
		{
			return base.Channel.ReassignStaffDropList(Request);
		}
	}
}
