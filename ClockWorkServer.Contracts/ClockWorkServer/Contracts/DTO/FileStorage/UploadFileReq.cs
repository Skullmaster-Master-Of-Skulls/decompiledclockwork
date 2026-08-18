using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x02000603 RID: 1539
	[DataContract(Namespace = "http://tpro.ca")]
	public class UploadFileReq : BaseMessageReq
	{
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x0000E4E9 File Offset: 0x0000C6E9
		// (set) Token: 0x06001F75 RID: 8053 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		[DataMember]
		public InMemoryFileDTO File { get; set; }
	}
}
