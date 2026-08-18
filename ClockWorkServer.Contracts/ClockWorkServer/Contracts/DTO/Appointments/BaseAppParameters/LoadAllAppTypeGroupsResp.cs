using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000989 RID: 2441
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllAppTypeGroupsResp
	{
		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x060031A2 RID: 12706 RVA: 0x00018248 File Offset: 0x00016448
		// (set) Token: 0x060031A3 RID: 12707 RVA: 0x00018250 File Offset: 0x00016450
		[DataMember]
		public IList<AppTypeGroupWithAppTypesDTO> AppTypeGroupsWithAppTypes { get; set; }
	}
}
