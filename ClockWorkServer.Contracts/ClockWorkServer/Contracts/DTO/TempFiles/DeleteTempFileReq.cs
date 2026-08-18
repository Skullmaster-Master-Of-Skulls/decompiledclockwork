using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files.FileUpload;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TempFiles
{
	// Token: 0x020001DF RID: 479
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteTempFileReq : BaseMessageReq
	{
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00004F67 File Offset: 0x00003167
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x00004F6F File Offset: 0x0000316F
		[DataMember]
		public TempFileContextDTO Context { get; set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00004F78 File Offset: 0x00003178
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x00004F80 File Offset: 0x00003180
		[DataMember]
		public int TempFileId { get; set; }
	}
}
