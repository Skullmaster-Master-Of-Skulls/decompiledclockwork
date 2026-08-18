using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000981 RID: 2433
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllAppTypesResp
	{
		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x000181AF File Offset: 0x000163AF
		// (set) Token: 0x06003189 RID: 12681 RVA: 0x000181B7 File Offset: 0x000163B7
		[DataMember]
		public IList<AppTypeDTO> AllAppTypes { get; set; }
	}
}
