using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Inventory
{
	// Token: 0x02000043 RID: 67
	public class InventoryGroupRestClientManager : BearerTokenRestProxy<IInventoryGroupClientManager>, IInventoryGroupClientManager, IWebService
	{
		// Token: 0x06000260 RID: 608 RVA: 0x000077A9 File Offset: 0x000059A9
		public InventoryGroupRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000077B3 File Offset: 0x000059B3
		public InventoryGroupRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000077BE File Offset: 0x000059BE
		public int CreateProductGroup(InventoryGroupDTO pGroup)
		{
			return base.Post<InventoryGroupDTO, int>(pGroup, "inventorygroup");
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000077CC File Offset: 0x000059CC
		public void UpdateProductGroup(InventoryGroupDTO pGroup)
		{
			base.Put<InventoryGroupDTO>(pGroup, "inventorygroup");
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000077DA File Offset: 0x000059DA
		public bool DeleteEmptyProductGroup(int pGroupId)
		{
			base.Delete(string.Format("inventorygroup/groupid/{0}", pGroupId));
			return true;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000077F3 File Offset: 0x000059F3
		public InventoryGroupDTO GetGroupById(int pGroupId)
		{
			return base.Get<InventoryGroupDTO>(string.Format("inventorygroup/groupid/{0}", pGroupId), true);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000780C File Offset: 0x00005A0C
		public IList<InventoryGroupDTO> GetGroups()
		{
			return base.GetMany<InventoryGroupDTO>("inventorygroup", true);
		}
	}
}
