using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000995 RID: 2453
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllowedAppTypesResp
	{
		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000182F2 File Offset: 0x000164F2
		// (set) Token: 0x060031C3 RID: 12739 RVA: 0x000182FA File Offset: 0x000164FA
		[DataMember]
		public IList<AppTypeDTO> AppTypes { get; set; }
	}
}
