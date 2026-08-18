using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B35 RID: 2869
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentsWithSpecialPermissionsResp
	{
		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06003C5F RID: 15455 RVA: 0x0001D4C9 File Offset: 0x0001B6C9
		// (set) Token: 0x06003C60 RID: 15456 RVA: 0x0001D4D1 File Offset: 0x0001B6D1
		[DataMember]
		public IList<AppointmentDTO> Appointments { get; set; }

		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x06003C61 RID: 15457 RVA: 0x0001D4DA File Offset: 0x0001B6DA
		// (set) Token: 0x06003C62 RID: 15458 RVA: 0x0001D4E2 File Offset: 0x0001B6E2
		[DataMember]
		public IDictionary<int, IList<eAppointmentPermissionRestriction>> PermissionRestrictions { get; set; }
	}
}
