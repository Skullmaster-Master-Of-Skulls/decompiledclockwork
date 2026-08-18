using System;

namespace TechnoPro.Common.Public.Entities.DataMigration
{
	// Token: 0x020003FC RID: 1020
	public class MigrationExternalCourse : IMigrationDataItems
	{
		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x00023320 File Offset: 0x00021520
		// (set) Token: 0x06001F47 RID: 8007 RVA: 0x00023328 File Offset: 0x00021528
		public DateTime StartDate { get; set; }

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x00023331 File Offset: 0x00021531
		// (set) Token: 0x06001F49 RID: 8009 RVA: 0x00023339 File Offset: 0x00021539
		public DateTime EndDate { get; set; }

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x00023342 File Offset: 0x00021542
		// (set) Token: 0x06001F4B RID: 8011 RVA: 0x0002334A File Offset: 0x0002154A
		public string Duration { get; set; }

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x00023353 File Offset: 0x00021553
		// (set) Token: 0x06001F4D RID: 8013 RVA: 0x0002335B File Offset: 0x0002155B
		public string Term { get; set; }

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x00023364 File Offset: 0x00021564
		// (set) Token: 0x06001F4F RID: 8015 RVA: 0x0002336C File Offset: 0x0002156C
		public string Subject { get; set; }

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x00023375 File Offset: 0x00021575
		// (set) Token: 0x06001F51 RID: 8017 RVA: 0x0002337D File Offset: 0x0002157D
		public string CourseCode { get; set; }

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x00023386 File Offset: 0x00021586
		// (set) Token: 0x06001F53 RID: 8019 RVA: 0x0002338E File Offset: 0x0002158E
		public string Section { get; set; }

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06001F54 RID: 8020 RVA: 0x00023397 File Offset: 0x00021597
		// (set) Token: 0x06001F55 RID: 8021 RVA: 0x0002339F File Offset: 0x0002159F
		public string TimeOfDay { get; set; }

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x000233A8 File Offset: 0x000215A8
		// (set) Token: 0x06001F57 RID: 8023 RVA: 0x000233B0 File Offset: 0x000215B0
		public string Campus { get; set; }

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x000233B9 File Offset: 0x000215B9
		// (set) Token: 0x06001F59 RID: 8025 RVA: 0x000233C1 File Offset: 0x000215C1
		public string Department { get; set; }

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x000233CA File Offset: 0x000215CA
		// (set) Token: 0x06001F5B RID: 8027 RVA: 0x000233D2 File Offset: 0x000215D2
		public string Location { get; set; }
	}
}
