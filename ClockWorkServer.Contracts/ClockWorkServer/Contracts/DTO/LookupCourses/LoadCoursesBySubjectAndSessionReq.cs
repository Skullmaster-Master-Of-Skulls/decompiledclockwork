using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007AE RID: 1966
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCoursesBySubjectAndSessionReq : BaseMessageReq
	{
		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x000132F5 File Offset: 0x000114F5
		// (set) Token: 0x0600286C RID: 10348 RVA: 0x000132FD File Offset: 0x000114FD
		[DataMember]
		public SessionDTO Session { get; set; }

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600286D RID: 10349 RVA: 0x00013306 File Offset: 0x00011506
		// (set) Token: 0x0600286E RID: 10350 RVA: 0x0001330E File Offset: 0x0001150E
		[DataMember]
		public int SubjectId { get; set; }
	}
}
