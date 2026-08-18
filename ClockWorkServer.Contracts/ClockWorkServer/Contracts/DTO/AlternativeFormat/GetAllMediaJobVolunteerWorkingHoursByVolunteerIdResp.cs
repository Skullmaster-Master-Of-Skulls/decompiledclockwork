using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C0A RID: 3082
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAllMediaJobVolunteerWorkingHoursByVolunteerIdResp
	{
		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x060040CC RID: 16588 RVA: 0x0001FC52 File Offset: 0x0001DE52
		// (set) Token: 0x060040CD RID: 16589 RVA: 0x0001FC5A File Offset: 0x0001DE5A
		[DataMember]
		public IList<MediaJobVolunteerWorkingHoursInfoDTO> MediaJobVolunteerWorkingHoursList { get; set; }
	}
}
