using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C09 RID: 3081
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAllMediaJobVolunteerWorkingHoursByVolunteerIdReq : BaseMessageReq
	{
		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x060040C9 RID: 16585 RVA: 0x0001FC41 File Offset: 0x0001DE41
		// (set) Token: 0x060040CA RID: 16586 RVA: 0x0001FC49 File Offset: 0x0001DE49
		[DataMember]
		public int VolunteerId { get; set; }
	}
}
