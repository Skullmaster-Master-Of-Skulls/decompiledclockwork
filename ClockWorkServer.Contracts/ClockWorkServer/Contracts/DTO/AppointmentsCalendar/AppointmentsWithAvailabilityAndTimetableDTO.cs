using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar
{
	// Token: 0x02000AFA RID: 2810
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentsWithAvailabilityAndTimetableDTO
	{
		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06003B52 RID: 15186 RVA: 0x0001CDBA File Offset: 0x0001AFBA
		// (set) Token: 0x06003B53 RID: 15187 RVA: 0x0001CDC2 File Offset: 0x0001AFC2
		[DataMember]
		public IList<AppointmentDTO> Appointments { get; set; }

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x06003B54 RID: 15188 RVA: 0x0001CDCB File Offset: 0x0001AFCB
		// (set) Token: 0x06003B55 RID: 15189 RVA: 0x0001CDD3 File Offset: 0x0001AFD3
		[DataMember]
		public IList<AvailabilityScheduleItemsForContextDTO> AvailabilitySchedules { get; set; }

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x06003B56 RID: 15190 RVA: 0x0001CDDC File Offset: 0x0001AFDC
		// (set) Token: 0x06003B57 RID: 15191 RVA: 0x0001CDE4 File Offset: 0x0001AFE4
		[DataMember]
		public IList<HolidayDTO> Holidays { get; set; }

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x06003B58 RID: 15192 RVA: 0x0001CDED File Offset: 0x0001AFED
		// (set) Token: 0x06003B59 RID: 15193 RVA: 0x0001CDF5 File Offset: 0x0001AFF5
		[DataMember]
		public IDictionary<int, IList<AppointmentTimetableItemDTO>> TimetableItems { get; set; }
	}
}
