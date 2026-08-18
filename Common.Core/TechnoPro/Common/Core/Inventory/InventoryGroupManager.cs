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
	// Token: 0x020000E4 RID: 228
	public class InventoryGroupManager : IInventoryGroupManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00039DC9 File Offset: 0x00037FC9
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x00039DD1 File Offset: 0x00037FD1
		public IInventoryGroupDAO GroupDAO { get; set; }

		// Token: 0x060008B6 RID: 2230 RVA: 0x00039DDA File Offset: 0x00037FDA
		public InventoryGroupManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.GroupDAO = new InventoryGroupDAO(opContext);
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00039DF9 File Offset: 0x00037FF9
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x00039E01 File Offset: 0x00038001
		public OperationContext OpContext { get; set; }

		// Token: 0x060008B9 RID: 2233 RVA: 0x00039E0C File Offset: 0x0003800C
		public int CreateProductGroup(InventoryGroup pGroup)
		{
			return this.GroupDAO.CreateProductGroup(pGroup);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00039E2A File Offset: 0x0003802A
		public void UpdateProductGroup(InventoryGroup pGroup)
		{
			this.GroupDAO.UpdateProductGroup(pGroup);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00039E3C File Offset: 0x0003803C
		public bool DeleteEmptyProductGroup(int pGroupId)
		{
			return this.GroupDAO.DeleteEmptyProductGroup(pGroupId);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00039E5C File Offset: 0x0003805C
		public InventoryGroup GetGroupById(int pGroupId)
		{
			return this.GroupDAO.GetGroupById(pGroupId);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00039E7C File Offset: 0x0003807C
		public IList<InventoryGroup> GetGroups()
		{
			return this.GroupDAO.GetGroups();
		}
	}
}
