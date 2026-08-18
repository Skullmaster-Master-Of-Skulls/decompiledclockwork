using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x02000602 RID: 1538
	[DataContract(Namespace = "http://tpro.ca")]
	public class DownloadFileResp
	{
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
		// (set) Token: 0x06001F72 RID: 8050 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		[DataMember]
		public InMemoryFileDTO File { get; set; }
	}
}
