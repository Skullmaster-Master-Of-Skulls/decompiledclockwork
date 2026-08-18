using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BF3 RID: 3059
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteMediaJobVolunteerReq : BaseMessageReq
	{
		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x0600407F RID: 16511 RVA: 0x0001FA87 File Offset: 0x0001DC87
		// (set) Token: 0x06004080 RID: 16512 RVA: 0x0001FA8F File Offset: 0x0001DC8F
		[DataMember]
		public int VolunteerId { get; set; }
	}
}
