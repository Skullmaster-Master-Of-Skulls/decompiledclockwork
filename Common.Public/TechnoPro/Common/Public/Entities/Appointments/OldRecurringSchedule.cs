using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004BC RID: 1212
	[Obsolete("Use Holiday instead")]
	public class OldRecurringSchedule
	{
		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x060024A1 RID: 9377 RVA: 0x00027B37 File Offset: 0x00025D37
		// (set) Token: 0x060024A2 RID: 9378 RVA: 0x00027B3F File Offset: 0x00025D3F
		public DateTime? ActiveStartDate { get; set; }

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x00027B48 File Offset: 0x00025D48
		// (set) Token: 0x060024A4 RID: 9380 RVA: 0x00027B50 File Offset: 0x00025D50
		public DateTime? ActiveEndDate { get; set; }

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x060024A5 RID: 9381 RVA: 0x00027B59 File Offset: 0x00025D59
		// (set) Token: 0x060024A6 RID: 9382 RVA: 0x00027B61 File Offset: 0x00025D61
		public DateTime StartDateTime { get; set; }

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x060024A7 RID: 9383 RVA: 0x00027B6A File Offset: 0x00025D6A
		// (set) Token: 0x060024A8 RID: 9384 RVA: 0x00027B72 File Offset: 0x00025D72
		public DateTime EndDateTime { get; set; }

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x00027B7B File Offset: 0x00025D7B
		// (set) Token: 0x060024AA RID: 9386 RVA: 0x00027B83 File Offset: 0x00025D83
		public int EveryTypeCode { get; set; }

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x00027B8C File Offset: 0x00025D8C
		// (set) Token: 0x060024AC RID: 9388 RVA: 0x00027B94 File Offset: 0x00025D94
		public int MultiplyBy { get; set; }

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x00027B9D File Offset: 0x00025D9D
		// (set) Token: 0x060024AE RID: 9390 RVA: 0x00027BA5 File Offset: 0x00025DA5
		public string Description { get; set; }
	}
}
