using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BFE RID: 3070
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaJobVolunteerInfoByVolunteerResp
	{
		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x060040A0 RID: 16544 RVA: 0x0001FB42 File Offset: 0x0001DD42
		// (set) Token: 0x060040A1 RID: 16545 RVA: 0x0001FB4A File Offset: 0x0001DD4A
		[DataMember]
		public IList<MediaJobVolunteerInfoDTO> MediaJobVolunteerList { get; set; }
	}
}
