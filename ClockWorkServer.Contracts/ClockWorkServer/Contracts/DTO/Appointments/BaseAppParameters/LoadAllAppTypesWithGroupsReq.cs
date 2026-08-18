using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x020009A0 RID: 2464
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllAppTypesWithGroupsReq : BaseMessageReq
	{
		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x060031DD RID: 12765 RVA: 0x0001837A File Offset: 0x0001657A
		// (set) Token: 0x060031DE RID: 12766 RVA: 0x00018382 File Offset: 0x00016582
		[DataMember]
		public bool IgnoreCache { get; set; }
	}
}
