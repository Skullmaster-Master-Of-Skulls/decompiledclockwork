using System;
using System.Runtime.Serialization;
using NewBooker.Entities.AutoTestBooking.Booker2;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2
{
	// Token: 0x02000A8E RID: 2702
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToBookFailureDTO
	{
		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x060038B3 RID: 14515 RVA: 0x0001B842 File Offset: 0x00019A42
		// (set) Token: 0x060038B4 RID: 14516 RVA: 0x0001B84A File Offset: 0x00019A4A
		[DataMember]
		public eTryToBookFailureType Type { get; set; }
	}
}
