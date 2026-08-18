using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000997 RID: 2455
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllowedTestExamRelatedAppTypesResp
	{
		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x00018303 File Offset: 0x00016503
		// (set) Token: 0x060031C7 RID: 12743 RVA: 0x0001830B File Offset: 0x0001650B
		[DataMember]
		public IList<AppTypeDTO> AppTypes { get; set; }
	}
}
