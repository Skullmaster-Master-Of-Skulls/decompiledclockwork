using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200043B RID: 1083
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadEquivalentCoursesReq : BaseReportMessageReq
	{
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0000ACE5 File Offset: 0x00008EE5
		// (set) Token: 0x06001756 RID: 5974 RVA: 0x0000ACED File Offset: 0x00008EED
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
