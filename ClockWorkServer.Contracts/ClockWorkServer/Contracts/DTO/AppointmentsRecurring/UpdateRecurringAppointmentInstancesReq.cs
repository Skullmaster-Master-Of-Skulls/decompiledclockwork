using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000AB3 RID: 2739
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRecurringAppointmentInstancesReq : BaseMessageReq
	{
		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x06003A1A RID: 14874 RVA: 0x0001C338 File Offset: 0x0001A538
		// (set) Token: 0x06003A1B RID: 14875 RVA: 0x0001C340 File Offset: 0x0001A540
		[DataMember]
		public BaseExtendedAppointmentDTO MasterAppointment { get; set; }

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x06003A1C RID: 14876 RVA: 0x0001C349 File Offset: 0x0001A549
		// (set) Token: 0x06003A1D RID: 14877 RVA: 0x0001C351 File Offset: 0x0001A551
		[DataMember]
		public IList<RecurringInstanceDTO> AppointmentsInRecurringSet { get; set; }

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x06003A1E RID: 14878 RVA: 0x0001C35A File Offset: 0x0001A55A
		// (set) Token: 0x06003A1F RID: 14879 RVA: 0x0001C362 File Offset: 0x0001A562
		[DataMember]
		public RecurringInstanceSetModifyBehaviourDTO ModifyBehaviour { get; set; }
	}
}
