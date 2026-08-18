using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews
{
	// Token: 0x02000A42 RID: 2626
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClassTestDefinitionsManagementContextDTO
	{
		// Token: 0x17001388 RID: 5000
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x0001A3DB File Offset: 0x000185DB
		// (set) Token: 0x0600362A RID: 13866 RVA: 0x0001A3E3 File Offset: 0x000185E3
		[DataMember]
		public int ReportId { get; set; }
	}
}
