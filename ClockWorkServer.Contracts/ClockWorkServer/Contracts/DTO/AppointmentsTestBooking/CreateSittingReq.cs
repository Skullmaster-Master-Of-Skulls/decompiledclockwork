using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A1D RID: 2589
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateSittingReq : BaseMessageReq
	{
		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06003592 RID: 13714 RVA: 0x0001A012 File Offset: 0x00018212
		// (set) Token: 0x06003593 RID: 13715 RVA: 0x0001A01A File Offset: 0x0001821A
		[DataMember]
		public SittingDTO Sitting { get; set; }
	}
}
