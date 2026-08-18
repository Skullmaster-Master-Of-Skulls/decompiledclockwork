using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BF5 RID: 3061
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaVolunteerByIdReq : BaseMessageReq
	{
		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x06004083 RID: 16515 RVA: 0x0001FA98 File Offset: 0x0001DC98
		// (set) Token: 0x06004084 RID: 16516 RVA: 0x0001FAA0 File Offset: 0x0001DCA0
		[DataMember]
		public int JobVolunteerId { get; set; }
	}
}
