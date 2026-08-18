using System;

namespace TechnoPro.Common.UI.Web.Entity.DynamicForms.Accommodations
{
	// Token: 0x02000039 RID: 57
	public class AccommodationsExpiryDate
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000340A File Offset: 0x0000160A
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00003412 File Offset: 0x00001612
		public eAccommodationsExpiryDateStatus Status { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000341B File Offset: 0x0000161B
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00003423 File Offset: 0x00001623
		public DateTime? ExpiryDate { get; set; }
	}
}
