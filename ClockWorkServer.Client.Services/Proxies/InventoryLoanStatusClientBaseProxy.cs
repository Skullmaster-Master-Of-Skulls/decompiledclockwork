using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000B8 RID: 184
	internal class InventoryLoanStatusClientBaseProxy : ClientBase<IInventoryLoanStatus>, IInventoryLoanStatus, IService
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x0001370C File Offset: 0x0001190C
		public InventoryLoanStatusClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00013717 File Offset: 0x00011917
		public InventoryLoanStatusClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00013724 File Offset: 0x00011924
		public CreateLoanStatusResp CreateLoanStatus(CreateLoanStatusReq request)
		{
			return base.Channel.CreateLoanStatus(request);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00013744 File Offset: 0x00011944
		public UpdateLoanStatusResp UpdateLoanStatus(UpdateLoanStatusReq request)
		{
			return base.Channel.UpdateLoanStatus(request);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00013764 File Offset: 0x00011964
		public GetLoanStatusByIdResp GetLoanStatusById(GetLoanStatusByIdReq request)
		{
			return base.Channel.GetLoanStatusById(request);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00013784 File Offset: 0x00011984
		public GetLoanStatusListResp GetLoanStatusList(GetLoanStatusListReq request)
		{
			return base.Channel.GetLoanStatusList(request);
		}
	}
}
