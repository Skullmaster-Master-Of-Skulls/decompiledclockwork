using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000806 RID: 2054
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupSubjectBySubjectCodeResp
	{
		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x060029DA RID: 10714 RVA: 0x00013DE7 File Offset: 0x00011FE7
		// (set) Token: 0x060029DB RID: 10715 RVA: 0x00013DEF File Offset: 0x00011FEF
		[DataMember]
		public LookupSubjectDTO Subject { get; set; }
	}
}
