using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000974 RID: 2420
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadShowTimeAsTypeByIdResp
	{
		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x000180D2 File Offset: 0x000162D2
		// (set) Token: 0x06003162 RID: 12642 RVA: 0x000180DA File Offset: 0x000162DA
		[DataMember]
		public AppShowTimeAsTypeDTO ShowTimeAsType { get; set; }
	}
}
