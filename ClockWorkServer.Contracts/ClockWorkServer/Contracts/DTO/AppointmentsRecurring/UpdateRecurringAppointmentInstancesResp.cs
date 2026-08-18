using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000AB4 RID: 2740
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRecurringAppointmentInstancesResp
	{
		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x06003A21 RID: 14881 RVA: 0x0001C36B File Offset: 0x0001A56B
		// (set) Token: 0x06003A22 RID: 14882 RVA: 0x0001C373 File Offset: 0x0001A573
		[DataMember]
		public IList<RecurringInstanceDTO> AppointmentsInRecurringSetWithNewAppointmentIds { get; set; }
	}
}
