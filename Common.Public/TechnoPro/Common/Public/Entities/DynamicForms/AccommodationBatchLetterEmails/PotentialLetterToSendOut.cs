using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.AccommodationBatchLetterEmails
{
	// Token: 0x020003BE RID: 958
	public class PotentialLetterToSendOut
	{
		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x000211DF File Offset: 0x0001F3DF
		// (set) Token: 0x06001D35 RID: 7477 RVA: 0x000211E7 File Offset: 0x0001F3E7
		public int PersonId { get; set; }

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x000211F0 File Offset: 0x0001F3F0
		// (set) Token: 0x06001D37 RID: 7479 RVA: 0x000211F8 File Offset: 0x0001F3F8
		public int LuCourseId { get; set; }

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06001D38 RID: 7480 RVA: 0x00021201 File Offset: 0x0001F401
		// (set) Token: 0x06001D39 RID: 7481 RVA: 0x00021209 File Offset: 0x0001F409
		public DateTime? AccommodationsExpiryDate { get; set; }

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x00021212 File Offset: 0x0001F412
		// (set) Token: 0x06001D3B RID: 7483 RVA: 0x0002121A File Offset: 0x0001F41A
		public DateTime? DateLetterLastSent { get; set; }

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x00021223 File Offset: 0x0001F423
		// (set) Token: 0x06001D3D RID: 7485 RVA: 0x0002122B File Offset: 0x0001F42B
		public DateTime? MaxDateAccommodationsWereModified { get; set; }
	}
}
