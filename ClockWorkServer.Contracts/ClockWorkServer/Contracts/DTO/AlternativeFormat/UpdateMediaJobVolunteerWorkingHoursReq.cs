using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C0D RID: 3085
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateMediaJobVolunteerWorkingHoursReq : BaseMessageReq
	{
		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x060040D5 RID: 16597 RVA: 0x0001FC85 File Offset: 0x0001DE85
		// (set) Token: 0x060040D6 RID: 16598 RVA: 0x0001FC8D File Offset: 0x0001DE8D
		[DataMember]
		public MediaJobVolunteerWorkingHoursInfoDTO MediaJobVolunteerWorkingHours { get; set; }
	}
}
