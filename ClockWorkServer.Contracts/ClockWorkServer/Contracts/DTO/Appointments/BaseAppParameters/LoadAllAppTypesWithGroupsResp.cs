using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x020009A1 RID: 2465
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllAppTypesWithGroupsResp
	{
		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x0001838B File Offset: 0x0001658B
		// (set) Token: 0x060031E1 RID: 12769 RVA: 0x00018393 File Offset: 0x00016593
		[DataMember]
		public IList<AppTypeGroupWithAppTypesDTO> AppTypeGroupsWithAppTypes { get; set; }
	}
}
