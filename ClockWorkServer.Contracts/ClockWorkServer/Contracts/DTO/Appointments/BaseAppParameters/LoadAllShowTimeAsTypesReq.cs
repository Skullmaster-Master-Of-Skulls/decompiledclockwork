using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000971 RID: 2417
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllShowTimeAsTypesReq : BaseMessageReq
	{
		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x0001809F File Offset: 0x0001629F
		// (set) Token: 0x06003159 RID: 12633 RVA: 0x000180A7 File Offset: 0x000162A7
		[DataMember]
		public int AppCode { get; set; }
	}
}
