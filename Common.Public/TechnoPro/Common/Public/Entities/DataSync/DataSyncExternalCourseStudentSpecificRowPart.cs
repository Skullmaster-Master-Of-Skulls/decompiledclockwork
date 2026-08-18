using System;

namespace TechnoPro.Common.Public.Entities.DataSync
{
	// Token: 0x020003D3 RID: 979
	public class DataSyncExternalCourseStudentSpecificRowPart
	{
		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x00021C63 File Offset: 0x0001FE63
		// (set) Token: 0x06001E24 RID: 7716 RVA: 0x00021C6B File Offset: 0x0001FE6B
		public string GradeLetter { get; set; }

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x00021C74 File Offset: 0x0001FE74
		// (set) Token: 0x06001E26 RID: 7718 RVA: 0x00021C7C File Offset: 0x0001FE7C
		public string InProgressGradeLetter { get; set; }

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x00021C85 File Offset: 0x0001FE85
		// (set) Token: 0x06001E28 RID: 7720 RVA: 0x00021C8D File Offset: 0x0001FE8D
		public decimal Grade { get; set; }

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x00021C96 File Offset: 0x0001FE96
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x00021C9E File Offset: 0x0001FE9E
		public decimal InProgressGrade { get; set; }

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x00021CA7 File Offset: 0x0001FEA7
		// (set) Token: 0x06001E2C RID: 7724 RVA: 0x00021CAF File Offset: 0x0001FEAF
		public double TuitionCost { get; set; }

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x00021CB8 File Offset: 0x0001FEB8
		// (set) Token: 0x06001E2E RID: 7726 RVA: 0x00021CC0 File Offset: 0x0001FEC0
		public DateTime? RegistrationDate { get; set; }

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x00021CC9 File Offset: 0x0001FEC9
		// (set) Token: 0x06001E30 RID: 7728 RVA: 0x00021CD1 File Offset: 0x0001FED1
		public string RegistrationNote { get; set; }
	}
}
