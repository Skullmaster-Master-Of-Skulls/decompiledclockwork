using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.TestBookings
{
	// Token: 0x020004F2 RID: 1266
	public class TestBookingsStatusAttribute : Attribute
	{
		// Token: 0x0600262B RID: 9771 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public TestBookingsStatusAttribute()
		{
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x00028D7D File Offset: 0x00026F7D
		public TestBookingsStatusAttribute(string title)
		{
			this.Title = title;
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x00028D8F File Offset: 0x00026F8F
		// (set) Token: 0x0600262E RID: 9774 RVA: 0x00028D97 File Offset: 0x00026F97
		public string Title { get; set; }
	}
}
