using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.Inventory;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Inventory
{
	// Token: 0x020000E9 RID: 233
	public class InventoryProductStatusManager : IInventoryProductStatusManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0003AACB File Offset: 0x00038CCB
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0003AAD3 File Offset: 0x00038CD3
		public IInventoryProductStatusDAO ProductStatusDAO { get; set; }

		// Token: 0x0600090B RID: 2315 RVA: 0x0003AADC File Offset: 0x00038CDC
		public InventoryProductStatusManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.ProductStatusDAO = new InventoryProductStatusDAO(opContext);
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0003AAFB File Offset: 0x00038CFB
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x0003AB03 File Offset: 0x00038D03
		public OperationContext OpContext { get; set; }

		// Token: 0x0600090E RID: 2318 RVA: 0x0003AB0C File Offset: 0x00038D0C
		public int CreateProductStatus(InventoryProductStatus productStatus)
		{
			return this.ProductStatusDAO.CreateProductStatus(productStatus);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0003AB2A File Offset: 0x00038D2A
		public void UpdateProductStatus(InventoryProductStatus productStatus)
		{
			this.ProductStatusDAO.UpdateProductStatus(productStatus);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0003AB3C File Offset: 0x00038D3C
		public InventoryProductStatus GetProductStatusById(int pStatusId)
		{
			return this.ProductStatusDAO.GetProductStatusById(pStatusId);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0003AB5C File Offset: 0x00038D5C
		public IList<InventoryProductStatus> GetProductStatusList()
		{
			return this.ProductStatusDAO.GetProductStatusList();
		}
	}
}
