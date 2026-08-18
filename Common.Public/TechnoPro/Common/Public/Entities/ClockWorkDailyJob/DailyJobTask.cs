using System;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Public.Entities.ClockWorkDailyJob
{
	// Token: 0x0200045E RID: 1118
	public class DailyJobTask : BusinessBase<int>
	{
		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x000260E4 File Offset: 0x000242E4
		// (set) Token: 0x06002201 RID: 8705 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int WindowsTaskJobId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x000260FC File Offset: 0x000242FC
		// (set) Token: 0x06002203 RID: 8707 RVA: 0x00026104 File Offset: 0x00024304
		public bool IsActive { get; set; }

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x0002610D File Offset: 0x0002430D
		// (set) Token: 0x06002205 RID: 8709 RVA: 0x00026115 File Offset: 0x00024315
		public string Arguments { get; set; }

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x0002611E File Offset: 0x0002431E
		// (set) Token: 0x06002207 RID: 8711 RVA: 0x00026126 File Offset: 0x00024326
		public DateTime? LastRunStartDate { get; set; }

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x0002612F File Offset: 0x0002432F
		// (set) Token: 0x06002209 RID: 8713 RVA: 0x00026137 File Offset: 0x00024337
		public DateTime? LastRunEndDate { get; set; }

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x00026140 File Offset: 0x00024340
		// (set) Token: 0x0600220B RID: 8715 RVA: 0x00026148 File Offset: 0x00024348
		public string LastRunResult { get; set; }

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x00026151 File Offset: 0x00024351
		// (set) Token: 0x0600220D RID: 8717 RVA: 0x00026159 File Offset: 0x00024359
		public int GroupId { get; set; }

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x00026162 File Offset: 0x00024362
		// (set) Token: 0x0600220F RID: 8719 RVA: 0x0002616A File Offset: 0x0002436A
		public string Description { get; set; }

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x00026173 File Offset: 0x00024373
		// (set) Token: 0x06002211 RID: 8721 RVA: 0x0002617B File Offset: 0x0002437B
		public int OrderNum { get; set; }

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x00026184 File Offset: 0x00024384
		// (set) Token: 0x06002213 RID: 8723 RVA: 0x0002618C File Offset: 0x0002438C
		public ReportBase ReportBase { get; set; }
	}
}
