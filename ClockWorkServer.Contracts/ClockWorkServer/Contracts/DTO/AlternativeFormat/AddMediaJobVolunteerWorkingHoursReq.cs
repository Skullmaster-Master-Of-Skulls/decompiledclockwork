using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C0B RID: 3083
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddMediaJobVolunteerWorkingHoursReq : BaseMessageReq
	{
		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x060040CF RID: 16591 RVA: 0x0001FC63 File Offset: 0x0001DE63
		// (set) Token: 0x060040D0 RID: 16592 RVA: 0x0001FC6B File Offset: 0x0001DE6B
		[DataMember]
		public MediaJobVolunteerWorkingHoursInfoDTO MediaJobVolunteerWorkingHours { get; set; }
	}
}
