using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000808 RID: 2056
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupSubjectBySubjectDescriptionResp
	{
		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x060029E0 RID: 10720 RVA: 0x00013E09 File Offset: 0x00012009
		// (set) Token: 0x060029E1 RID: 10721 RVA: 0x00013E11 File Offset: 0x00012011
		[DataMember]
		public LookupSubjectDTO Subject { get; set; }
	}
}
