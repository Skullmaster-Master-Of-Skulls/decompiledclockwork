using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x02000605 RID: 1541
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteFileReq : BaseMessageReq
	{
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x0000E50B File Offset: 0x0000C70B
		// (set) Token: 0x06001F7B RID: 8059 RVA: 0x0000E513 File Offset: 0x0000C713
		[DataMember]
		public FileIdentifierDTO FileIdentifier { get; set; }
	}
}
