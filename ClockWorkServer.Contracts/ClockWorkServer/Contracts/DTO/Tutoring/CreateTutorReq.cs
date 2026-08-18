using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001A3 RID: 419
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateTutorReq : BaseMessageReq
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x000045B4 File Offset: 0x000027B4
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x000045BC File Offset: 0x000027BC
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x000045C5 File Offset: 0x000027C5
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x000045CD File Offset: 0x000027CD
		[DataMember]
		public string MiddleName { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x000045D6 File Offset: 0x000027D6
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x000045DE File Offset: 0x000027DE
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x000045E7 File Offset: 0x000027E7
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x000045EF File Offset: 0x000027EF
		[DataMember]
		public string StudentNumber { get; set; }
	}
}
