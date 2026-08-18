using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000701 RID: 1793
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncLookupCoursesReq : BaseReportMessageReq
	{
		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x00010A5C File Offset: 0x0000EC5C
		// (set) Token: 0x0600247E RID: 9342 RVA: 0x00010A64 File Offset: 0x0000EC64
		[DataMember]
		public new int WhoAmI { get; set; }

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x00010A6D File Offset: 0x0000EC6D
		// (set) Token: 0x06002480 RID: 9344 RVA: 0x00010A75 File Offset: 0x0000EC75
		[DataMember]
		public IList<DataSyncExternalCourseDTO> AllExternalCourses { get; set; }
	}
}
