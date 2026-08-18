using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009FC RID: 2556
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateReaderReq : BaseMessageReq
	{
		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x00019D59 File Offset: 0x00017F59
		// (set) Token: 0x06003520 RID: 13600 RVA: 0x00019D61 File Offset: 0x00017F61
		[DataMember]
		public ProctorDTO Proctor { get; set; }
	}
}
