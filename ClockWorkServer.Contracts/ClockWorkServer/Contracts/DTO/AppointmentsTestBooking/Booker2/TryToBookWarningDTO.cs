using System;
using System.Runtime.Serialization;
using NewBooker.Entities.AutoTestBooking.Booker2;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2
{
	// Token: 0x02000A92 RID: 2706
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToBookWarningDTO
	{
		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x060038E9 RID: 14569 RVA: 0x0001BA0D File Offset: 0x00019C0D
		// (set) Token: 0x060038EA RID: 14570 RVA: 0x0001BA15 File Offset: 0x00019C15
		[DataMember]
		public eTryToBookWarningType Type { get; set; }
	}
}
