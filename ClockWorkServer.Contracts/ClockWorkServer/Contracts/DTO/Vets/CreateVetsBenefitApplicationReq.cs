using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x02000111 RID: 273
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateVetsBenefitApplicationReq : BaseMessageReq
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0000307F File Offset: 0x0000127F
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x00003087 File Offset: 0x00001287
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00003090 File Offset: 0x00001290
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x00003098 File Offset: 0x00001298
		[DataMember]
		public int SemesterId { get; set; }
	}
}
