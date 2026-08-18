using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000987 RID: 2439
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppTypeGroupByIdResp
	{
		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x00018237 File Offset: 0x00016437
		// (set) Token: 0x0600319F RID: 12703 RVA: 0x0001823F File Offset: 0x0001643F
		[DataMember]
		public AppTypeGroupDTO AppTypeGroup { get; set; }
	}
}
