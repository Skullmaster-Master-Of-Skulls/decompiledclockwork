using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentLog
{
	// Token: 0x02000B3D RID: 2877
	[DataContract(Namespace = "http://tpro.ca")]
	public class LogAppModificationsPreChangeCommittedReq : BaseMsmqMessageReq
	{
		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x06003C83 RID: 15491 RVA: 0x0001D5B7 File Offset: 0x0001B7B7
		// (set) Token: 0x06003C84 RID: 15492 RVA: 0x0001D5BF File Offset: 0x0001B7BF
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
