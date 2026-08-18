using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BFA RID: 3066
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaVolunteerByVolunteerAndJobResp
	{
		// Token: 0x170017D4 RID: 6100
		// (get) Token: 0x06004094 RID: 16532 RVA: 0x0001FAFE File Offset: 0x0001DCFE
		// (set) Token: 0x06004095 RID: 16533 RVA: 0x0001FB06 File Offset: 0x0001DD06
		[DataMember]
		public MediaJobVolunteerInfoDTO MediaJobVolunteer { get; set; }
	}
}
