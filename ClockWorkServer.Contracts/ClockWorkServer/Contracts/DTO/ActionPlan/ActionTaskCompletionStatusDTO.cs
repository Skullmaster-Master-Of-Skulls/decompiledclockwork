using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ActionPlan
{
	// Token: 0x02000C93 RID: 3219
	[DataContract(Namespace = "http://tpro.ca")]
	public class ActionTaskCompletionStatusDTO
	{
		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x0600430E RID: 17166 RVA: 0x00024434 File Offset: 0x00022634
		// (set) Token: 0x0600430F RID: 17167 RVA: 0x0002443C File Offset: 0x0002263C
		[DataMember]
		public int CompletedId { get; set; }

		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x06004310 RID: 17168 RVA: 0x00024445 File Offset: 0x00022645
		// (set) Token: 0x06004311 RID: 17169 RVA: 0x0002444D File Offset: 0x0002264D
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x06004312 RID: 17170 RVA: 0x00024456 File Offset: 0x00022656
		// (set) Token: 0x06004313 RID: 17171 RVA: 0x0002445E File Offset: 0x0002265E
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x06004314 RID: 17172 RVA: 0x00024467 File Offset: 0x00022667
		// (set) Token: 0x06004315 RID: 17173 RVA: 0x0002446F File Offset: 0x0002266F
		[DataMember]
		public bool MeansComplete { get; set; }

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x00024478 File Offset: 0x00022678
		// (set) Token: 0x06004317 RID: 17175 RVA: 0x00024480 File Offset: 0x00022680
		[DataMember]
		public int? ColourArgB { get; set; }

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x06004318 RID: 17176 RVA: 0x00024489 File Offset: 0x00022689
		// (set) Token: 0x06004319 RID: 17177 RVA: 0x00024491 File Offset: 0x00022691
		[DataMember]
		public int ImageIndex { get; set; }

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x0600431A RID: 17178 RVA: 0x0002449A File Offset: 0x0002269A
		// (set) Token: 0x0600431B RID: 17179 RVA: 0x000244A2 File Offset: 0x000226A2
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x000244AB File Offset: 0x000226AB
		// (set) Token: 0x0600431D RID: 17181 RVA: 0x000244B3 File Offset: 0x000226B3
		[DataMember]
		public bool IsDefault { get; set; }
	}
}
