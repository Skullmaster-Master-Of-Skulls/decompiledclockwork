using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BFF RID: 3071
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateMediaJobVolunteerReq : BaseMessageReq
	{
		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x060040A3 RID: 16547 RVA: 0x0001FB53 File Offset: 0x0001DD53
		// (set) Token: 0x060040A4 RID: 16548 RVA: 0x0001FB5B File Offset: 0x0001DD5B
		[DataMember]
		public MediaJobVolunteerInfoDTO MediaJobVolunteer { get; set; }
	}
}
