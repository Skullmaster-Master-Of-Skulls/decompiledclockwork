using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob
{
	// Token: 0x02000892 RID: 2194
	[DataContract(Namespace = "http://tpro.ca")]
	public class DailyJobTaskDTO
	{
		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x00014FC4 File Offset: 0x000131C4
		// (set) Token: 0x06002C5E RID: 11358 RVA: 0x00014FCC File Offset: 0x000131CC
		[DataMember]
		public int WindowsTaskJobId { get; set; }

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x00014FD5 File Offset: 0x000131D5
		// (set) Token: 0x06002C60 RID: 11360 RVA: 0x00014FDD File Offset: 0x000131DD
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06002C61 RID: 11361 RVA: 0x00014FE6 File Offset: 0x000131E6
		// (set) Token: 0x06002C62 RID: 11362 RVA: 0x00014FEE File Offset: 0x000131EE
		[DataMember]
		public string Arguments { get; set; }

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06002C63 RID: 11363 RVA: 0x00014FF7 File Offset: 0x000131F7
		// (set) Token: 0x06002C64 RID: 11364 RVA: 0x00014FFF File Offset: 0x000131FF
		[DataMember]
		public DateTime? LastRunStartDate { get; set; }

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06002C65 RID: 11365 RVA: 0x00015008 File Offset: 0x00013208
		// (set) Token: 0x06002C66 RID: 11366 RVA: 0x00015010 File Offset: 0x00013210
		[DataMember]
		public DateTime? LastRunEndDate { get; set; }

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06002C67 RID: 11367 RVA: 0x00015019 File Offset: 0x00013219
		// (set) Token: 0x06002C68 RID: 11368 RVA: 0x00015021 File Offset: 0x00013221
		[DataMember]
		public string LastRunResult { get; set; }

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06002C69 RID: 11369 RVA: 0x0001502A File Offset: 0x0001322A
		// (set) Token: 0x06002C6A RID: 11370 RVA: 0x00015032 File Offset: 0x00013232
		[DataMember]
		public int GroupId { get; set; }

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06002C6B RID: 11371 RVA: 0x0001503B File Offset: 0x0001323B
		// (set) Token: 0x06002C6C RID: 11372 RVA: 0x00015043 File Offset: 0x00013243
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x0001504C File Offset: 0x0001324C
		// (set) Token: 0x06002C6E RID: 11374 RVA: 0x00015054 File Offset: 0x00013254
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x0001505D File Offset: 0x0001325D
		// (set) Token: 0x06002C70 RID: 11376 RVA: 0x00015065 File Offset: 0x00013265
		[DataMember]
		public ReportBaseDTO ReportBase { get; set; }
	}
}
