using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x020005F6 RID: 1526
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetFileResp
	{
		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0000E349 File Offset: 0x0000C549
		// (set) Token: 0x06001F3F RID: 7999 RVA: 0x0000E351 File Offset: 0x0000C551
		[DataMember]
		public BinaryFileDTO File { get; set; }
	}
}
