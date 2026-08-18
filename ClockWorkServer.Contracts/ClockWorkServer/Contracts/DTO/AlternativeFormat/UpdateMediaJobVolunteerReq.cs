using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BF1 RID: 3057
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateMediaJobVolunteerReq : BaseMessageReq
	{
		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x0600407B RID: 16507 RVA: 0x0001FA76 File Offset: 0x0001DC76
		// (set) Token: 0x0600407C RID: 16508 RVA: 0x0001FA7E File Offset: 0x0001DC7E
		[DataMember]
		public AlternateFormatVolunteerDTO Volunteer { get; set; }
	}
}
