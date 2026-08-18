using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files.FileUpload;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TempFiles
{
	// Token: 0x020001DC RID: 476
	[DataContract(Namespace = "http://tpro.ca")]
	public class DownloadTempFileReq : BaseMessageReq
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00004F23 File Offset: 0x00003123
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x00004F2B File Offset: 0x0000312B
		[DataMember]
		public TempFileContextDTO Context { get; set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00004F34 File Offset: 0x00003134
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x00004F3C File Offset: 0x0000313C
		[DataMember]
		public int TempFileId { get; set; }
	}
}
