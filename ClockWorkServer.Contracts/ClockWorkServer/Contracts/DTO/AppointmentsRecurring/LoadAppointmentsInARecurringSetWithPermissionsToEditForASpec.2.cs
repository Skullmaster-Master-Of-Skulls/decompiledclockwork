using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000AB6 RID: 2742
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserResp
	{
		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x06003A29 RID: 14889 RVA: 0x0001C39E File Offset: 0x0001A59E
		// (set) Token: 0x06003A2A RID: 14890 RVA: 0x0001C3A6 File Offset: 0x0001A5A6
		[DataMember]
		public IDictionary<int, bool> EditPermissions { get; set; }
	}
}
