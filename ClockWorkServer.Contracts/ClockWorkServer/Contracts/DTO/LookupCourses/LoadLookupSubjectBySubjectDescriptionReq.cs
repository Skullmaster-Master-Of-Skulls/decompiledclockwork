using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000807 RID: 2055
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupSubjectBySubjectDescriptionReq : BaseMessageReq
	{
		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x00013DF8 File Offset: 0x00011FF8
		// (set) Token: 0x060029DE RID: 10718 RVA: 0x00013E00 File Offset: 0x00012000
		[DataMember]
		public string SubjectDescription { get; set; }
	}
}
