using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x020005F7 RID: 1527
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveFileReq : BaseMessageReq
	{
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x0000E35A File Offset: 0x0000C55A
		// (set) Token: 0x06001F42 RID: 8002 RVA: 0x0000E362 File Offset: 0x0000C562
		[DataMember]
		public FileStructureDTO File { get; set; }
	}
}
