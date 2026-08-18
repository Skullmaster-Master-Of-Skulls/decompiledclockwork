using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Inventory;
using TechnoPro.Common.ClientManager.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Inventory
{
	// Token: 0x02000046 RID: 70
	public class InventoryLocationRestClientManager : BearerTokenRestProxy<IInventoryLocationClientManager>, IInventoryLocationClientManager, IWebService
	{
		// Token: 0x06000285 RID: 645 RVA: 0x00007B13 File Offset: 0x00005D13
		public InventoryLocationRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00007B1D File Offset: 0x00005D1D
		public InventoryLocationRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007B28 File Offset: 0x00005D28
		public int CreateLocation(InventoryLocationDTO location)
		{
			return base.Post<InventoryLocationDTO, int>(location, "inventorylocation");
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00007B36 File Offset: 0x00005D36
		public InventoryLocationDTO GetLocationById(int locationId)
		{
			return base.Get<InventoryLocationDTO>(string.Format("inventorylocation/locationid/{0}", locationId), true);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00007B4F File Offset: 0x00005D4F
		public IList<InventoryLocationDTO> GetAllLocations()
		{
			return base.GetMany<InventoryLocationDTO>("inventorylocation", true);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00007B5D File Offset: 0x00005D5D
		public IList<InventoryLocationDTO> GetLocations(string includingText)
		{
			return base.GetMany<InventoryLocationDTO>(string.Format("inventorylocation/matching?searchingtext={0}", includingText), true);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00007B71 File Offset: 0x00005D71
		public bool LocationInUse(int locationId)
		{
			return base.Get<bool>(string.Format("inventorylocation/isinuse/locationid/{0}", locationId), true);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007B8A File Offset: 0x00005D8A
		public bool DeleteLocation(int locationId)
		{
			base.Delete(string.Format("inventorylocation/locationid/{0}", locationId));
			return true;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00007BA3 File Offset: 0x00005DA3
		public void UpdateLocation(InventoryLocationDTO location)
		{
			base.Put<InventoryLocationDTO>(location, "inventorylocation");
		}
	}
}
