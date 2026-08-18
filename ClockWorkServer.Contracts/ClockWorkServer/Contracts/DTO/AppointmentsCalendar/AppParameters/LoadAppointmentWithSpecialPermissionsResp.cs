using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B33 RID: 2867
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentWithSpecialPermissionsResp
	{
		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x0001D452 File Offset: 0x0001B652
		// (set) Token: 0x06003C50 RID: 15440 RVA: 0x0001D45A File Offset: 0x0001B65A
		[DataMember]
		public AppointmentDTO Appointment { get; set; }

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x0001D463 File Offset: 0x0001B663
		// (set) Token: 0x06003C52 RID: 15442 RVA: 0x0001D46B File Offset: 0x0001B66B
		[DataMember]
		public IList<eAppointmentPermissionRestriction> PermissionRestrictions { get; set; }
	}
}
