using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A1E RID: 2590
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateSittingResp
	{
		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06003595 RID: 13717 RVA: 0x0001A023 File Offset: 0x00018223
		// (set) Token: 0x06003596 RID: 13718 RVA: 0x0001A02B File Offset: 0x0001822B
		[DataMember]
		public int SittingId { get; set; }
	}
}
