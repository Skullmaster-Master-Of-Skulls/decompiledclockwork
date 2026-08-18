using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000972 RID: 2418
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllShowTimeAsTypesResp
	{
		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x000180B0 File Offset: 0x000162B0
		// (set) Token: 0x0600315C RID: 12636 RVA: 0x000180B8 File Offset: 0x000162B8
		[DataMember]
		public IList<AppShowTimeAsTypeDTO> ShowTimeAsTypes { get; set; }
	}
}
