using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000AB9 RID: 2745
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRecurringAppointmentAttendeesReq : BaseMessageReq
	{
		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06003A34 RID: 14900 RVA: 0x0001C3E2 File Offset: 0x0001A5E2
		// (set) Token: 0x06003A35 RID: 14901 RVA: 0x0001C3EA File Offset: 0x0001A5EA
		[DataMember]
		public int GroupCode { get; set; }

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x0001C3F3 File Offset: 0x0001A5F3
		// (set) Token: 0x06003A37 RID: 14903 RVA: 0x0001C3FB File Offset: 0x0001A5FB
		[DataMember]
		public int AppIdAlreadyUpdated { get; set; }

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06003A38 RID: 14904 RVA: 0x0001C404 File Offset: 0x0001A604
		// (set) Token: 0x06003A39 RID: 14905 RVA: 0x0001C40C File Offset: 0x0001A60C
		[DataMember]
		public IList<AttendeeDTO> AttendeesAdded { get; set; }

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06003A3A RID: 14906 RVA: 0x0001C415 File Offset: 0x0001A615
		// (set) Token: 0x06003A3B RID: 14907 RVA: 0x0001C41D File Offset: 0x0001A61D
		[DataMember]
		public IList<AttendeeDTO> AttendeesModified { get; set; }

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x06003A3C RID: 14908 RVA: 0x0001C426 File Offset: 0x0001A626
		// (set) Token: 0x06003A3D RID: 14909 RVA: 0x0001C42E File Offset: 0x0001A62E
		[DataMember]
		public IList<int> AttendeePersonIdsRemoved { get; set; }
	}
}
