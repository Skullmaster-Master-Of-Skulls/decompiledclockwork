using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B0F RID: 2831
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadHolidaysReq : BaseMessageReq
	{
		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x06003BC5 RID: 15301 RVA: 0x0001D0EF File Offset: 0x0001B2EF
		// (set) Token: 0x06003BC6 RID: 15302 RVA: 0x0001D0F7 File Offset: 0x0001B2F7
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x0001D100 File Offset: 0x0001B300
		// (set) Token: 0x06003BC8 RID: 15304 RVA: 0x0001D108 File Offset: 0x0001B308
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
