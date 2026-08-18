using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000976 RID: 2422
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadShowTimeAsTypeByAppCodeResp
	{
		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x000180F4 File Offset: 0x000162F4
		// (set) Token: 0x06003168 RID: 12648 RVA: 0x000180FC File Offset: 0x000162FC
		[DataMember]
		public AppShowTimeAsTypeDTO ShowTimeAsType { get; set; }
	}
}
