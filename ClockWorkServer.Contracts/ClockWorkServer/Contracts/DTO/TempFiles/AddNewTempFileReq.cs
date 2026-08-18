using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files.FileUpload;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TempFiles
{
	// Token: 0x020001DA RID: 474
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddNewTempFileReq : BaseMessageReq
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00004EF0 File Offset: 0x000030F0
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x00004EF8 File Offset: 0x000030F8
		[DataMember]
		public TempFileContextDTO Context { get; set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00004F01 File Offset: 0x00003101
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x00004F09 File Offset: 0x00003109
		[DataMember]
		public BinaryFileDTO FileToUpload { get; set; }
	}
}
