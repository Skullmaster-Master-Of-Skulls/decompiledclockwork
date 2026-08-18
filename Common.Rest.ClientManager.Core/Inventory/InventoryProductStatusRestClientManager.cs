using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Inventory
{
	// Token: 0x02000048 RID: 72
	public class InventoryProductStatusRestClientManager : BearerTokenRestProxy<IInventoryProductStatusClientManager>, IInventoryProductStatusClientManager, IWebService
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x00007EF1 File Offset: 0x000060F1
		public InventoryProductStatusRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00007EFB File Offset: 0x000060FB
		public InventoryProductStatusRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00007F06 File Offset: 0x00006106
		public int CreateProductStatus(InventoryProductStatusDTO productStatus)
		{
			return base.Post<InventoryProductStatusDTO, int>(productStatus, "inventoryprodutstatus");
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00007F14 File Offset: 0x00006114
		public void UpdateProductStatus(InventoryProductStatusDTO productStatus)
		{
			base.Put<InventoryProductStatusDTO>(productStatus, "inventoryprodutstatus");
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007F22 File Offset: 0x00006122
		public InventoryProductStatusDTO GetProductStatusById(int pStatusId)
		{
			return base.Get<InventoryProductStatusDTO>(string.Format("inventoryprodutstatus/productstatusid/{0}", pStatusId), true);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00007F3B File Offset: 0x0000613B
		public IList<InventoryProductStatusDTO> GetProductStatusList()
		{
			return base.GetMany<InventoryProductStatusDTO>("inventoryprodutstatus", true);
		}
	}
}
