using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009FB RID: 2555
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateProctorResp
	{
		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x0600351C RID: 13596 RVA: 0x00019D48 File Offset: 0x00017F48
		// (set) Token: 0x0600351D RID: 13597 RVA: 0x00019D50 File Offset: 0x00017F50
		[DataMember]
		public int PersonId { get; set; }
	}
}
