using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200095F RID: 2399
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAttendeeReq : BaseMessageReq
	{
		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x00017E3B File Offset: 0x0001603B
		// (set) Token: 0x060030FF RID: 12543 RVA: 0x00017E43 File Offset: 0x00016043
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x00017E4C File Offset: 0x0001604C
		// (set) Token: 0x06003101 RID: 12545 RVA: 0x00017E54 File Offset: 0x00016054
		[DataMember]
		public int PersonId { get; set; }
	}
}
