using System;
using System.ServiceModel;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x020005FD RID: 1533
	[MessageContract]
	public class UploadLargeFileResp
	{
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x0000E436 File Offset: 0x0000C636
		// (set) Token: 0x06001F5B RID: 8027 RVA: 0x0000E43E File Offset: 0x0000C63E
		[MessageBodyMember(Order = 1)]
		public BasicFileInfoDTO FileInfo { get; set; }
	}
}
