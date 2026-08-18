using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x0200011D RID: 285
	[DataContract(Namespace = "http://tpro.ca")]
	public class VetsStudentCardInfoDTO
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00003239 File Offset: 0x00001439
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x00003241 File Offset: 0x00001441
		[DataMember]
		public VetsStudentCardInfoItemDTO[] CurrentAndFutureItems { get; set; }
	}
}
