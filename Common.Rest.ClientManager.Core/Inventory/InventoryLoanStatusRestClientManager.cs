using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Inventory
{
	// Token: 0x02000045 RID: 69
	public class InventoryLoanStatusRestClientManager : BearerTokenRestProxy<IInventoryLoanStatusClientManager>, IInventoryLoanStatusClientManager, IWebService
	{
		// Token: 0x0600027E RID: 638 RVA: 0x00007A87 File Offset: 0x00005C87
		public InventoryLoanStatusRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00007A91 File Offset: 0x00005C91
		public InventoryLoanStatusRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00007A9C File Offset: 0x00005C9C
		public int CreateLoanStatus(InventoryLoanStatusDTO loanStatus)
		{
			return base.Post<InventoryLoanStatusDTO, int>(loanStatus, "inventoryloanstatus");
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00007AAA File Offset: 0x00005CAA
		public void UpdateLoanStatus(InventoryLoanStatusDTO loanStatus)
		{
			base.Put<InventoryLoanStatusDTO>(loanStatus, "inventoryloanstatus");
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007AB8 File Offset: 0x00005CB8
		public InventoryLoanStatusDTO GetLoanStatusById(int lStatusId)
		{
			return base.Get<InventoryLoanStatusDTO>(string.Format("inventoryloanstatus/loanstatusid/{0}", lStatusId), true);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007AD4 File Offset: 0x00005CD4
		public InventoryLoanStatusDTO GetLoanStatusByName(string loanStatusName)
		{
			return this.GetLoanStatusList().FirstOrDefault((InventoryLoanStatusDTO ls) => ls.Name.Equals(loanStatusName, StringComparison.InvariantCultureIgnoreCase));
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007B05 File Offset: 0x00005D05
		public IList<InventoryLoanStatusDTO> GetLoanStatusList()
		{
			return base.GetMany<InventoryLoanStatusDTO>("inventoryloanstatus", true);
		}
	}
}
