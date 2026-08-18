using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x0200011A RID: 282
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetChaptersResp
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x000031A0 File Offset: 0x000013A0
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x000031A8 File Offset: 0x000013A8
		[DataMember]
		public IList<VetsChapterDTO> Chapters { get; set; }
	}
}
