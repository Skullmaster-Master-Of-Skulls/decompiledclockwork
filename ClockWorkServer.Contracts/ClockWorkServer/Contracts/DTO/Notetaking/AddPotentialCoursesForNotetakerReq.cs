using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200043D RID: 1085
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddPotentialCoursesForNotetakerReq : BaseReportMessageReq
	{
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x0000AD07 File Offset: 0x00008F07
		// (set) Token: 0x0600175C RID: 5980 RVA: 0x0000AD0F File Offset: 0x00008F0F
		[DataMember]
		public int ServiceProviderId { get; set; }

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x0000AD18 File Offset: 0x00008F18
		// (set) Token: 0x0600175E RID: 5982 RVA: 0x0000AD20 File Offset: 0x00008F20
		[DataMember]
		public IList<DataSyncExternalCourseDTO> ExternalCourses { get; set; }
	}
}
