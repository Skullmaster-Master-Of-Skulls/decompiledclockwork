using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200098E RID: 2446
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateAppTypeGroupResp
	{
		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x0001829D File Offset: 0x0001649D
		// (set) Token: 0x060031B2 RID: 12722 RVA: 0x000182A5 File Offset: 0x000164A5
		[DataMember]
		public int AppointmentTypeGroupId { get; set; }
	}
}
