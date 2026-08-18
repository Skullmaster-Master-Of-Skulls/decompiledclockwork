using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest
{
	// Token: 0x02000B45 RID: 2885
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentBookingResDTO
	{
		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x06003CEB RID: 15595 RVA: 0x0001D8E7 File Offset: 0x0001BAE7
		// (set) Token: 0x06003CEC RID: 15596 RVA: 0x0001D8EF File Offset: 0x0001BAEF
		[DataMember]
		public bool PassedChecks { get; set; }

		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06003CED RID: 15597 RVA: 0x0001D8F8 File Offset: 0x0001BAF8
		// (set) Token: 0x06003CEE RID: 15598 RVA: 0x0001D900 File Offset: 0x0001BB00
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x06003CEF RID: 15599 RVA: 0x0001D909 File Offset: 0x0001BB09
		// (set) Token: 0x06003CF0 RID: 15600 RVA: 0x0001D911 File Offset: 0x0001BB11
		[DataMember]
		public string PublicMessage { get; set; }

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x06003CF1 RID: 15601 RVA: 0x0001D91A File Offset: 0x0001BB1A
		// (set) Token: 0x06003CF2 RID: 15602 RVA: 0x0001D922 File Offset: 0x0001BB22
		[DataMember]
		public string PrivateMessage { get; set; }
	}
}
