using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A62 RID: 2658
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdatePrivateNoteReq : BaseMessageReq
	{
		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x060037C1 RID: 14273 RVA: 0x0001B183 File Offset: 0x00019383
		// (set) Token: 0x060037C2 RID: 14274 RVA: 0x0001B18B File Offset: 0x0001938B
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x060037C3 RID: 14275 RVA: 0x0001B194 File Offset: 0x00019394
		// (set) Token: 0x060037C4 RID: 14276 RVA: 0x0001B19C File Offset: 0x0001939C
		[DataMember]
		public string PrivateNote { get; set; }
	}
}
