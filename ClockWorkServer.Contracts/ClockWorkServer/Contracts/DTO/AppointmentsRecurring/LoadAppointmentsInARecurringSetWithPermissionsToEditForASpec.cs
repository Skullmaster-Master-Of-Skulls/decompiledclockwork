using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000AB5 RID: 2741
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserReq : BaseMessageReq
	{
		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x06003A24 RID: 14884 RVA: 0x0001C37C File Offset: 0x0001A57C
		// (set) Token: 0x06003A25 RID: 14885 RVA: 0x0001C384 File Offset: 0x0001A584
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x06003A26 RID: 14886 RVA: 0x0001C38D File Offset: 0x0001A58D
		// (set) Token: 0x06003A27 RID: 14887 RVA: 0x0001C395 File Offset: 0x0001A595
		[DataMember]
		public int PersonId { get; set; }
	}
}
