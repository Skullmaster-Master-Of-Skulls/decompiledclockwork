using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x020005FF RID: 1535
	[DataContract(Namespace = "http://tpro.ca")]
	public class DownloadFileReq : BaseMessageReq
	{
		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x0000E47A File Offset: 0x0000C67A
		// (set) Token: 0x06001F65 RID: 8037 RVA: 0x0000E482 File Offset: 0x0000C682
		[DataMember]
		public FileIdentifierDTO FileIdentifier { get; set; }
	}
}
