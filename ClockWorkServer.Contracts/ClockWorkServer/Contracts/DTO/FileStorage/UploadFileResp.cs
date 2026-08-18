using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x02000604 RID: 1540
	[DataContract(Namespace = "http://tpro.ca")]
	public class UploadFileResp
	{
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x0000E4FA File Offset: 0x0000C6FA
		// (set) Token: 0x06001F78 RID: 8056 RVA: 0x0000E502 File Offset: 0x0000C702
		[DataMember]
		public BasicFileInfoDTO FileInfo { get; set; }
	}
}
