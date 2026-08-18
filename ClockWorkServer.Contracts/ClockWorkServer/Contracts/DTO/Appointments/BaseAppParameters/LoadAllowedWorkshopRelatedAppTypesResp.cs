using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000999 RID: 2457
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllowedWorkshopRelatedAppTypesResp
	{
		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x060031CA RID: 12746 RVA: 0x00018314 File Offset: 0x00016514
		// (set) Token: 0x060031CB RID: 12747 RVA: 0x0001831C File Offset: 0x0001651C
		[DataMember]
		public IList<AppTypeDTO> AppTypes { get; set; }
	}
}
