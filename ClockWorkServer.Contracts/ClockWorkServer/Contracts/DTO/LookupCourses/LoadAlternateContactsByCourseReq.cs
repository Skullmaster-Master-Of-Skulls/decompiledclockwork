using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000797 RID: 1943
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAlternateContactsByCourseReq : BaseMessageReq
	{
		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x00012CEB File Offset: 0x00010EEB
		// (set) Token: 0x060027F3 RID: 10227 RVA: 0x00012CF3 File Offset: 0x00010EF3
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
