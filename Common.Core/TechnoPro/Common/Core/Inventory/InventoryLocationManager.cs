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
	// Token: 0x020000E7 RID: 231
	public class InventoryLocationManager : IInventoryLocationManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0003A339 File Offset: 0x00038539
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x0003A341 File Offset: 0x00038541
		public IInventoryLocationDAO LocationDAO { get; set; }

		// Token: 0x060008E3 RID: 2275 RVA: 0x0003A34A File Offset: 0x0003854A
		public InventoryLocationManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.LocationDAO = new InventoryLocationDAO(opContext);
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0003A369 File Offset: 0x00038569
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x0003A371 File Offset: 0x00038571
		public OperationContext OpContext { get; set; }

		// Token: 0x060008E6 RID: 2278 RVA: 0x0003A37C File Offset: 0x0003857C
		public int CreateLocation(InventoryLocation location)
		{
			return this.LocationDAO.CreateLocation(location);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0003A39C File Offset: 0x0003859C
		public InventoryLocation GetLocationById(int locationId)
		{
			return this.LocationDAO.GetLocationById(locationId);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0003A3BC File Offset: 0x000385BC
		public IList<InventoryLocation> GetAllLocations()
		{
			return this.LocationDAO.GetAllLocations();
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0003A3DC File Offset: 0x000385DC
		public IList<InventoryLocation> GetLocations(string includingText)
		{
			return this.LocationDAO.GetLocations(includingText);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0003A3FC File Offset: 0x000385FC
		public bool LocationInUse(int locationId)
		{
			return this.LocationDAO.LocationInUse(locationId);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0003A41C File Offset: 0x0003861C
		public bool DeleteLocation(int locationId)
		{
			return this.LocationDAO.DeleteLocation(locationId);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0003A43A File Offset: 0x0003863A
		public void UpdateLocation(InventoryLocation location)
		{
			this.LocationDAO.UpdateLocation(location);
		}
	}
}
