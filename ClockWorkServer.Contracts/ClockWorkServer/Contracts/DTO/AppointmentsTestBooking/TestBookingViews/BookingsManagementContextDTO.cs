using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews
{
	// Token: 0x02000A41 RID: 2625
	[DataContract(Namespace = "http://tpro.ca")]
	public class BookingsManagementContextDTO
	{
		// Token: 0x17001386 RID: 4998
		// (get) Token: 0x06003624 RID: 13860 RVA: 0x0001A3B9 File Offset: 0x000185B9
		// (set) Token: 0x06003625 RID: 13861 RVA: 0x0001A3C1 File Offset: 0x000185C1
		[DataMember]
		public bool LoadExtendedInfo { get; set; }

		// Token: 0x17001387 RID: 4999
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x0001A3CA File Offset: 0x000185CA
		// (set) Token: 0x06003627 RID: 13863 RVA: 0x0001A3D2 File Offset: 0x000185D2
		[DataMember]
		public int ReportId { get; set; }
	}
}
