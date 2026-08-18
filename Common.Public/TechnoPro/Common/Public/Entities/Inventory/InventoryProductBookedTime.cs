using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Inventory
{
	// Token: 0x02000314 RID: 788
	public class InventoryProductBookedTime : BusinessBase<int>
	{
		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x0001D63E File Offset: 0x0001B83E
		// (set) Token: 0x06001891 RID: 6289 RVA: 0x0001D646 File Offset: 0x0001B846
		public Guid ProductUniqueId { get; set; }

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x0001D64F File Offset: 0x0001B84F
		// (set) Token: 0x06001893 RID: 6291 RVA: 0x0001D657 File Offset: 0x0001B857
		public DateTime StartDate { get; set; }

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x0001D660 File Offset: 0x0001B860
		// (set) Token: 0x06001895 RID: 6293 RVA: 0x0001D668 File Offset: 0x0001B868
		public DateTime EndDate { get; set; }

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x0001D671 File Offset: 0x0001B871
		// (set) Token: 0x06001897 RID: 6295 RVA: 0x0001D679 File Offset: 0x0001B879
		public PersonBase To { get; set; }

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x0001D682 File Offset: 0x0001B882
		// (set) Token: 0x06001899 RID: 6297 RVA: 0x0001D68A File Offset: 0x0001B88A
		public PersonBase From { get; set; }

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x0001D693 File Offset: 0x0001B893
		// (set) Token: 0x0600189B RID: 6299 RVA: 0x0001D69B File Offset: 0x0001B89B
		public InventoryProductBookingType BookingType { get; set; }
	}
}
