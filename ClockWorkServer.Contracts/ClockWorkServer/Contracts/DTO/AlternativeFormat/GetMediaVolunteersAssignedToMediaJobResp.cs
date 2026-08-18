using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BFC RID: 3068
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetMediaVolunteersAssignedToMediaJobResp
	{
		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x0600409A RID: 16538 RVA: 0x0001FB20 File Offset: 0x0001DD20
		// (set) Token: 0x0600409B RID: 16539 RVA: 0x0001FB28 File Offset: 0x0001DD28
		[DataMember]
		public IList<MediaJobVolunteerInfoDTO> MediaJobVolunteerList { get; set; }
	}
}
