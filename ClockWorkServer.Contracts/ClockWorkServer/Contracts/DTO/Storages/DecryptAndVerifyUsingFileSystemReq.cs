using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Storages
{
	// Token: 0x02000260 RID: 608
	[DataContract(Namespace = "http://tpro.ca")]
	public class DecryptAndVerifyUsingFileSystemReq : BaseMessageReq
	{
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x000068A8 File Offset: 0x00004AA8
		// (set) Token: 0x06000DEB RID: 3563 RVA: 0x000068B0 File Offset: 0x00004AB0
		[DataMember]
		public string EncryptedFileName { get; set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x000068B9 File Offset: 0x00004AB9
		// (set) Token: 0x06000DED RID: 3565 RVA: 0x000068C1 File Offset: 0x00004AC1
		[DataMember]
		public string OutputDecryptedFileName { get; set; }
	}
}
