using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001E7 RID: 487
	[DataContract(Namespace = "http://tpro.ca")]
	public class TaskNoteDTO
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x000051ED File Offset: 0x000033ED
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x000051F5 File Offset: 0x000033F5
		[DataMember]
		public int TaskNoteId { get; set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x000051FE File Offset: 0x000033FE
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x00005206 File Offset: 0x00003406
		[DataMember]
		public PersonBaseDTO WhoEntered { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0000520F File Offset: 0x0000340F
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x00005217 File Offset: 0x00003417
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00005220 File Offset: 0x00003420
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x00005228 File Offset: 0x00003428
		[DataMember]
		public PersonBaseDTO WhoLastModified { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00005231 File Offset: 0x00003431
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x00005239 File Offset: 0x00003439
		[DataMember]
		public DateTime DateLastModified { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00005242 File Offset: 0x00003442
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x0000524A File Offset: 0x0000344A
		[DataMember]
		public string Notes { get; set; }
	}
}
