using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BFD RID: 3069
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaJobVolunteerInfoByVolunteerReq : BaseMessageReq
	{
		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x0600409D RID: 16541 RVA: 0x0001FB31 File Offset: 0x0001DD31
		// (set) Token: 0x0600409E RID: 16542 RVA: 0x0001FB39 File Offset: 0x0001DD39
		[DataMember]
		public int VolunteerId { get; set; }
	}
}
