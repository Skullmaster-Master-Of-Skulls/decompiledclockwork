using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000956 RID: 2390
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllIconInfosResp
	{
		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x060030D9 RID: 12505 RVA: 0x00017D4D File Offset: 0x00015F4D
		// (set) Token: 0x060030DA RID: 12506 RVA: 0x00017D55 File Offset: 0x00015F55
		[DataMember]
		public IList<IconInfoDTO> IconInfos { get; set; }
	}
}
