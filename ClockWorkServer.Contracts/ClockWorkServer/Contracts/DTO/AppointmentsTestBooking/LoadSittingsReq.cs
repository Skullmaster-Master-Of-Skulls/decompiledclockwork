using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009E8 RID: 2536
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSittingsReq : BaseMessageReq
	{
		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x060034DB RID: 13531 RVA: 0x00019BC1 File Offset: 0x00017DC1
		// (set) Token: 0x060034DC RID: 13532 RVA: 0x00019BC9 File Offset: 0x00017DC9
		[DataMember]
		public DateTime Day { get; set; }
	}
}
