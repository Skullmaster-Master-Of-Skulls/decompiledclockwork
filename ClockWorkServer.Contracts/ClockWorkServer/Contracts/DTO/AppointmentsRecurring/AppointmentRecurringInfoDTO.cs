using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000AC0 RID: 2752
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentRecurringInfoDTO
	{
		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x06003A5B RID: 14939 RVA: 0x0001C4F2 File Offset: 0x0001A6F2
		// (set) Token: 0x06003A5C RID: 14940 RVA: 0x0001C4FA File Offset: 0x0001A6FA
		[DataMember]
		public List<RecurringAppointmentDTO> Appointments { get; set; }

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x06003A5D RID: 14941 RVA: 0x0001C503 File Offset: 0x0001A703
		// (set) Token: 0x06003A5E RID: 14942 RVA: 0x0001C50B File Offset: 0x0001A70B
		[DataMember]
		public int MasterGroupCode { get; set; }
	}
}
