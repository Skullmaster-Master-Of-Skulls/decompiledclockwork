using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files.FileUpload;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TempFiles
{
	// Token: 0x020001DE RID: 478
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteTempFilesReq : BaseMessageReq
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00004F56 File Offset: 0x00003156
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x00004F5E File Offset: 0x0000315E
		[DataMember]
		public TempFileContextDTO Context { get; set; }
	}
}
