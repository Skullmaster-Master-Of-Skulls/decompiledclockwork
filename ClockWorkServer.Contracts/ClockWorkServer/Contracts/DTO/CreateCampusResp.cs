using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000F1 RID: 241
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCampusResp
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0000296A File Offset: 0x00000B6A
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x00002972 File Offset: 0x00000B72
		[DataMember]
		public int CampusId { get; set; }
	}
}
