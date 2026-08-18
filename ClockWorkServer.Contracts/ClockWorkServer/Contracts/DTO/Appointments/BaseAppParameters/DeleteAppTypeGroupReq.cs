using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200098C RID: 2444
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAppTypeGroupReq : BaseMessageReq
	{
		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x060031AB RID: 12715 RVA: 0x0001827B File Offset: 0x0001647B
		// (set) Token: 0x060031AC RID: 12716 RVA: 0x00018283 File Offset: 0x00016483
		[DataMember]
		public int AppointmentTypeGroupId { get; set; }
	}
}
