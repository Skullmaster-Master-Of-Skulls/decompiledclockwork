using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.Core.Inventory;
using TechnoPro.Common.Core.Mappers.Inventory;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000052 RID: 82
	public class InventoryLoanStatusServiceManager : IInventoryLoanStatus, IService
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0000EE58 File Offset: 0x0000D058
		public CreateLoanStatusResp CreateLoanStatus(CreateLoanStatusReq request)
		{
			IInventoryLoanStatusManager inventoryLoanStatusManager = new InventoryLoanStatusManager(request.GetOperationContext());
			return new CreateLoanStatusResp
			{
				LoanStatusId = inventoryLoanStatusManager.CreateLoanStatus(request.LoanStatus.ToDomainObject())
			};
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000EE94 File Offset: 0x0000D094
		public UpdateLoanStatusResp UpdateLoanStatus(UpdateLoanStatusReq request)
		{
			IInventoryLoanStatusManager inventoryLoanStatusManager = new InventoryLoanStatusManager(request.GetOperationContext());
			inventoryLoanStatusManager.UpdateLoanStatus(request.LoanStatus.ToDomainObject());
			return new UpdateLoanStatusResp();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000EECC File Offset: 0x0000D0CC
		public GetLoanStatusByIdResp GetLoanStatusById(GetLoanStatusByIdReq request)
		{
			IInventoryLoanStatusManager inventoryLoanStatusManager = new InventoryLoanStatusManager(request.GetOperationContext());
			return new GetLoanStatusByIdResp
			{
				LoanStatus = inventoryLoanStatusManager.GetLoanStatusById(request.LoanStatusId).ToDTO()
			};
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000EF08 File Offset: 0x0000D108
		public GetLoanStatusListResp GetLoanStatusList(GetLoanStatusListReq request)
		{
			IInventoryLoanStatusManager inventoryLoanStatusManager = new InventoryLoanStatusManager(request.GetOperationContext());
			return new GetLoanStatusListResp
			{
				LoanStatusList = inventoryLoanStatusManager.GetLoanStatusList().ToDTO()
			};
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000EF40 File Offset: 0x0000D140
		public int CheckConnectivity()
		{
			return 1;
		}
	}
}
