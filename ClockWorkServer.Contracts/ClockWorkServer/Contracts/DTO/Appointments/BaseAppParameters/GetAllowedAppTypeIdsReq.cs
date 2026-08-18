using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000992 RID: 2450
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAllowedAppTypeIdsReq : BaseMessageReq
	{
		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x060031BB RID: 12731 RVA: 0x000182D0 File Offset: 0x000164D0
		// (set) Token: 0x060031BC RID: 12732 RVA: 0x000182D8 File Offset: 0x000164D8
		[DataMember]
		public int PersonId { get; set; }
	}
}
