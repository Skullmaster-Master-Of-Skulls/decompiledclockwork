using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000990 RID: 2448
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppTypesResp
	{
		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x060031B7 RID: 12727 RVA: 0x000182BF File Offset: 0x000164BF
		// (set) Token: 0x060031B8 RID: 12728 RVA: 0x000182C7 File Offset: 0x000164C7
		[DataMember]
		public List<AppTypeDTO> AppTypes { get; set; }
	}
}
