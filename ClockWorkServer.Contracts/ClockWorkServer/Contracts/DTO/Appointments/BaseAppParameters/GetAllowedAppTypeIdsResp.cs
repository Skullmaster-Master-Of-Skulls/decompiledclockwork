using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000993 RID: 2451
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAllowedAppTypeIdsResp
	{
		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x060031BE RID: 12734 RVA: 0x000182E1 File Offset: 0x000164E1
		// (set) Token: 0x060031BF RID: 12735 RVA: 0x000182E9 File Offset: 0x000164E9
		[DataMember]
		public IList<int> AppTypeIds { get; set; }
	}
}
