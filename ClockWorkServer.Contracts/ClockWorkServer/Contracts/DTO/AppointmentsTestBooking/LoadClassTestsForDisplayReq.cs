using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A14 RID: 2580
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestsForDisplayReq : BaseMessageReq
	{
		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x00019F68 File Offset: 0x00018168
		// (set) Token: 0x06003576 RID: 13686 RVA: 0x00019F70 File Offset: 0x00018170
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x00019F79 File Offset: 0x00018179
		// (set) Token: 0x06003578 RID: 13688 RVA: 0x00019F81 File Offset: 0x00018181
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
