using System;
using System.ServiceModel;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x020005FA RID: 1530
	[MessageContract]
	public class DownloadLargeFileMessageReq : BaseMessageContractReq
	{
		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x0000E39E File Offset: 0x0000C59E
		// (set) Token: 0x06001F4D RID: 8013 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		[MessageHeader(MustUnderstand = true)]
		public FileIdentifierMessageDTO FileIdentifier { get; set; }
	}
}
