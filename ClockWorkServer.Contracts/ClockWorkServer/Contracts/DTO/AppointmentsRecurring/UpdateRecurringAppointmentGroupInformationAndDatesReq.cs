using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000AB2 RID: 2738
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRecurringAppointmentGroupInformationAndDatesReq : BaseMessageReq
	{
		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x06003A17 RID: 14871 RVA: 0x0001C327 File Offset: 0x0001A527
		// (set) Token: 0x06003A18 RID: 14872 RVA: 0x0001C32F File Offset: 0x0001A52F
		[DataMember]
		public AppointmentRecurringInfoDTO RecurringSet { get; set; }
	}
}
