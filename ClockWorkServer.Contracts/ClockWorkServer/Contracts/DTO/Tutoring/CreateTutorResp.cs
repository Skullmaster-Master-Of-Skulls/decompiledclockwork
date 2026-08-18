using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001A4 RID: 420
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateTutorResp
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x000045F8 File Offset: 0x000027F8
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x00004600 File Offset: 0x00002800
		[DataMember]
		public int PersonId { get; set; }
	}
}
