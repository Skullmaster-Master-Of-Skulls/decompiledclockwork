using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BF6 RID: 3062
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaVolunteerByIdResp
	{
		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x0001FAA9 File Offset: 0x0001DCA9
		// (set) Token: 0x06004087 RID: 16519 RVA: 0x0001FAB1 File Offset: 0x0001DCB1
		[DataMember]
		public MediaJobVolunteerInfoDTO MediaJobVolunteer { get; set; }
	}
}
