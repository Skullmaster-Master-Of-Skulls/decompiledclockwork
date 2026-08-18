using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C08 RID: 3080
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaJobVolunteerWorkingHoursByVolunteerAndJobResp
	{
		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x060040C6 RID: 16582 RVA: 0x0001FC30 File Offset: 0x0001DE30
		// (set) Token: 0x060040C7 RID: 16583 RVA: 0x0001FC38 File Offset: 0x0001DE38
		[DataMember]
		public IList<MediaJobVolunteerWorkingHoursInfoDTO> MediaJobVolunteerWorkingHoursList { get; set; }
	}
}
