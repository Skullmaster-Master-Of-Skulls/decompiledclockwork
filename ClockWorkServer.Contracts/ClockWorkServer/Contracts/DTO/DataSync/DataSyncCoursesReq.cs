using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x020006FD RID: 1789
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncCoursesReq : BaseReportMessageReq
	{
		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x000109F6 File Offset: 0x0000EBF6
		// (set) Token: 0x0600246E RID: 9326 RVA: 0x000109FE File Offset: 0x0000EBFE
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x00010A07 File Offset: 0x0000EC07
		// (set) Token: 0x06002470 RID: 9328 RVA: 0x00010A0F File Offset: 0x0000EC0F
		[DataMember]
		public List<DataSyncExternalCourseDTO> ExternalCourses { get; set; }
	}
}
