using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BF8 RID: 3064
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaVolunteerByPersonIdResp
	{
		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x0600408C RID: 16524 RVA: 0x0001FACB File Offset: 0x0001DCCB
		// (set) Token: 0x0600408D RID: 16525 RVA: 0x0001FAD3 File Offset: 0x0001DCD3
		[DataMember]
		public AlternateFormatVolunteerDTO MediaVolunteer { get; set; }
	}
}
