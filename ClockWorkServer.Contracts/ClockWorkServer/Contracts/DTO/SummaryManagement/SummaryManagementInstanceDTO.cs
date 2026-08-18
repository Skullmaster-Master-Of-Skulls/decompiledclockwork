using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.SummaryManagement;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.SummaryManagement
{
	// Token: 0x02000223 RID: 547
	[DataContract(Namespace = "http://tpro.ca")]
	public class SummaryManagementInstanceDTO
	{
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00005A3A File Offset: 0x00003C3A
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00005A42 File Offset: 0x00003C42
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00005A4B File Offset: 0x00003C4B
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x00005A53 File Offset: 0x00003C53
		[DataMember]
		public int ReportId { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00005A5C File Offset: 0x00003C5C
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x00005A64 File Offset: 0x00003C64
		[DataMember]
		public byte[] ButtonImage { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00005A6D File Offset: 0x00003C6D
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x00005A75 File Offset: 0x00003C75
		[DataMember]
		public IList<int> ScreenNumsToTriggerUpdateWhenChanged { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00005A7E File Offset: 0x00003C7E
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x00005A86 File Offset: 0x00003C86
		[DataMember]
		public int[] Screens { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00005A8F File Offset: 0x00003C8F
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x00005A97 File Offset: 0x00003C97
		[DataMember]
		public int EmailCidOnPerDateFormToUpdateWhenEmailSent { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00005AA0 File Offset: 0x00003CA0
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00005AA8 File Offset: 0x00003CA8
		[DataMember]
		public eSummaryManagementType SummaryManagementType { get; set; }
	}
}
