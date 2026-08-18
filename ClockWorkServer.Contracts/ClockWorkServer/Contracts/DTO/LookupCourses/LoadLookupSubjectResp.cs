using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200080A RID: 2058
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupSubjectResp
	{
		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x060029E8 RID: 10728 RVA: 0x00013E3C File Offset: 0x0001203C
		// (set) Token: 0x060029E9 RID: 10729 RVA: 0x00013E44 File Offset: 0x00012044
		[DataMember]
		public LookupSubjectDTO Subject { get; set; }
	}
}
