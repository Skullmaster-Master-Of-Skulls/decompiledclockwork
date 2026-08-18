using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001B0 RID: 432
	[DataContract(Namespace = "http://tpro.ca")]
	[Serializable]
	public class TPMailAttachmentDTO
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x000046CD File Offset: 0x000028CD
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x000046D5 File Offset: 0x000028D5
		[DataMember]
		public string FileNameForDisplay { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x000046DE File Offset: 0x000028DE
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x000046E6 File Offset: 0x000028E6
		[DataMember]
		public byte[] FileBytes { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x000046EF File Offset: 0x000028EF
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x000046F7 File Offset: 0x000028F7
		[DataMember]
		public int FileIdForSavedAttachment { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00004700 File Offset: 0x00002900
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x00004708 File Offset: 0x00002908
		[DataMember]
		public int FileAttachmentId { get; set; }
	}
}
