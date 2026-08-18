using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000975 RID: 2421
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadShowTimeAsTypeByAppCodeReq : BaseMessageReq
	{
		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x000180E3 File Offset: 0x000162E3
		// (set) Token: 0x06003165 RID: 12645 RVA: 0x000180EB File Offset: 0x000162EB
		[DataMember]
		public int AppCode { get; set; }
	}
}
