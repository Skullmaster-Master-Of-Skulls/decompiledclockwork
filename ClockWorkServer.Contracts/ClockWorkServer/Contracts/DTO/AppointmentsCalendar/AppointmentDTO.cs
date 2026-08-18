using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cases;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar
{
	// Token: 0x02000AFC RID: 2812
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentDTO : BaseExtendedAppointmentDTO
	{
		// Token: 0x06003B62 RID: 15202 RVA: 0x0001CE31 File Offset: 0x0001B031
		public AppointmentDTO()
		{
			this.Icons = new List<AppointmentIconDTO>();
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x06003B63 RID: 15203 RVA: 0x0001CE47 File Offset: 0x0001B047
		// (set) Token: 0x06003B64 RID: 15204 RVA: 0x0001CE4F File Offset: 0x0001B04F
		[DataMember]
		public IList<AppointmentIconDTO> Icons { get; set; }

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x06003B65 RID: 15205 RVA: 0x0001CE58 File Offset: 0x0001B058
		// (set) Token: 0x06003B66 RID: 15206 RVA: 0x0001CE60 File Offset: 0x0001B060
		[DataMember]
		public CaseBaseDTO CaseInfo { get; set; }

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x06003B67 RID: 15207 RVA: 0x0001CE69 File Offset: 0x0001B069
		// (set) Token: 0x06003B68 RID: 15208 RVA: 0x0001CE71 File Offset: 0x0001B071
		[DataMember]
		public BasicAppointmentTestExamInfoDTO TestExamInfo { get; set; }

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x06003B69 RID: 15209 RVA: 0x0001CE7A File Offset: 0x0001B07A
		// (set) Token: 0x06003B6A RID: 15210 RVA: 0x0001CE82 File Offset: 0x0001B082
		[DataMember]
		public AppointmentWorkshopInfoDTO WorkshopInfo { get; set; }

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x0001CE8B File Offset: 0x0001B08B
		// (set) Token: 0x06003B6C RID: 15212 RVA: 0x0001CE93 File Offset: 0x0001B093
		public object Tag { get; set; }
	}
}
