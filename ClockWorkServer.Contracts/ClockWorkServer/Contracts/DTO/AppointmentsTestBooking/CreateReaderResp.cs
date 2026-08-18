using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009FD RID: 2557
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateReaderResp
	{
		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06003522 RID: 13602 RVA: 0x00019D6A File Offset: 0x00017F6A
		// (set) Token: 0x06003523 RID: 13603 RVA: 0x00019D72 File Offset: 0x00017F72
		[DataMember]
		public int PersonId { get; set; }
	}
}
