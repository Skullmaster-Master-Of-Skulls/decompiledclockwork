using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Inventory
{
	// Token: 0x02000055 RID: 85
	public class InventoryLoanStatusClientManager : IInventoryLoanStatusClientManager, IWebService
	{
		// Token: 0x060002EE RID: 750 RVA: 0x0000CF3C File Offset: 0x0000B13C
		public int CreateLoanStatus(InventoryLoanStatusDTO loanStatus)
		{
			CreateLoanStatusReq createLoanStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateLoanStatusReq>();
			createLoanStatusReq.LoanStatus = loanStatus;
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			int loanStatusId = ClientServiceFactory.GetClientInstance<IInventoryLoanStatus>().CreateLoanStatus(createLoanStatusReq).LoanStatusId;
			bool flag = loanStatusId > 0;
			if (flag)
			{
				clientCache.Remove("cInventoryLoanStatusList");
			}
			return loanStatusId;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000CF90 File Offset: 0x0000B190
		public void UpdateLoanStatus(InventoryLoanStatusDTO loanStatus)
		{
			UpdateLoanStatusReq updateLoanStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateLoanStatusReq>();
			updateLoanStatusReq.LoanStatus = loanStatus;
			ClientServiceFactory.GetClientInstance<IInventoryLoanStatus>().UpdateLoanStatus(updateLoanStatusReq);
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			clientCache.Remove("cInventoryLoanStatusList");
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000CFD0 File Offset: 0x0000B1D0
		public InventoryLoanStatusDTO GetLoanStatusById(int lStatusId)
		{
			GetLoanStatusByIdReq getLoanStatusByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetLoanStatusByIdReq>();
			getLoanStatusByIdReq.LoanStatusId = lStatusId;
			return ClientServiceFactory.GetClientInstance<IInventoryLoanStatus>().GetLoanStatusById(getLoanStatusByIdReq).LoanStatus;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000D008 File Offset: 0x0000B208
		public InventoryLoanStatusDTO GetLoanStatusByName(string loanStatusName)
		{
			IList<InventoryLoanStatusDTO> loanStatusList = this.GetLoanStatusList();
			return loanStatusList.FirstOrDefault((InventoryLoanStatusDTO ls) => ls.Name.Equals(loanStatusName, StringComparison.InvariantCultureIgnoreCase));
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000D040 File Offset: 0x0000B240
		public IList<InventoryLoanStatusDTO> GetLoanStatusList()
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			IList<InventoryLoanStatusDTO> loanStatusList = clientCache.LoanStatusList;
			bool flag = loanStatusList != null;
			IList<InventoryLoanStatusDTO> result;
			if (flag)
			{
				result = loanStatusList;
			}
			else
			{
				GetLoanStatusListReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetLoanStatusListReq>();
				result = (clientCache.LoanStatusList = ClientServiceFactory.GetClientInstance<IInventoryLoanStatus>().GetLoanStatusList(request).LoanStatusList);
			}
			return result;
		}
	}
}
