using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000AB1 RID: 2737
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCurrentRecurringAppointmentsSetResp
	{
		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x06003A14 RID: 14868 RVA: 0x0001C316 File Offset: 0x0001A516
		// (set) Token: 0x06003A15 RID: 14869 RVA: 0x0001C31E File Offset: 0x0001A51E
		[DataMember]
		public AppointmentRecurringInfoDTO RecurringSet { get; set; }
	}
}
