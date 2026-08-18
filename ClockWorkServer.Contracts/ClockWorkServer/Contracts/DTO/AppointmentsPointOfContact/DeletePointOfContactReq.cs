using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact
{
	// Token: 0x02000920 RID: 2336
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeletePointOfContactReq : BaseMessageReq
	{
		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x00016857 File Offset: 0x00014A57
		// (set) Token: 0x06002F54 RID: 12116 RVA: 0x0001685F File Offset: 0x00014A5F
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
