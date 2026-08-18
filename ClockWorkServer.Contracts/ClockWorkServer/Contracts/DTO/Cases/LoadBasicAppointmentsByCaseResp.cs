using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cases
{
	// Token: 0x020008A5 RID: 2213
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadBasicAppointmentsByCaseResp
	{
		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x00015397 File Offset: 0x00013597
		// (set) Token: 0x06002CD8 RID: 11480 RVA: 0x0001539F File Offset: 0x0001359F
		[DataMember]
		public IList<BaseBasicAppointmentDTO> Appointments { get; set; }
	}
}
