using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x02000601 RID: 1537
	[DataContract(Namespace = "http://tpro.ca")]
	public class InMemoryFileDTO : BasicFileInfoDTO
	{
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x0000E4BE File Offset: 0x0000C6BE
		// (set) Token: 0x06001F6F RID: 8047 RVA: 0x0000E4C6 File Offset: 0x0000C6C6
		[DataMember]
		public byte[] FileData { get; set; }
	}
}
