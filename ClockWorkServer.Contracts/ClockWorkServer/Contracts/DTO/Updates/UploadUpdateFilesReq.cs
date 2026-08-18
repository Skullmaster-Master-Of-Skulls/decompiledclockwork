using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Updates
{
	// Token: 0x02000172 RID: 370
	[DataContract(Namespace = "http://tpro.ca")]
	public class UploadUpdateFilesReq : BaseMessageReq
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00003FCD File Offset: 0x000021CD
		// (set) Token: 0x060008E9 RID: 2281 RVA: 0x00003FD5 File Offset: 0x000021D5
		[DataMember]
		public IList<FileSystemStructureDTO> Updates { get; set; }
	}
}
