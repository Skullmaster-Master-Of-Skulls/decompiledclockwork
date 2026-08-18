using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200099A RID: 2458
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppTypeWithExtendedInfoIdByIdReq : BaseMessageReq
	{
		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x060031CD RID: 12749 RVA: 0x00018325 File Offset: 0x00016525
		// (set) Token: 0x060031CE RID: 12750 RVA: 0x0001832D File Offset: 0x0001652D
		[DataMember]
		public int AppTypeId { get; set; }
	}
}
