using System;
using System.ServiceModel;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x020005FB RID: 1531
	[MessageContract]
	public class BasicFileInfoMessageDTO : BaseMessageContractReq
	{
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		// (set) Token: 0x06001F50 RID: 8016 RVA: 0x0000E3C0 File Offset: 0x0000C5C0
		[MessageHeader(MustUnderstand = true)]
		public FileIdentifierMessageDTO FileIdentifier { get; set; }

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0000E3C9 File Offset: 0x0000C5C9
		// (set) Token: 0x06001F52 RID: 8018 RVA: 0x0000E3D1 File Offset: 0x0000C5D1
		[MessageHeader(MustUnderstand = true)]
		public string FileName { get; set; }

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x0000E3DA File Offset: 0x0000C5DA
		// (set) Token: 0x06001F54 RID: 8020 RVA: 0x0000E3E2 File Offset: 0x0000C5E2
		[MessageHeader(MustUnderstand = true)]
		public long Length { get; set; }
	}
}
