using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000802 RID: 2050
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupSubjectByIdResp
	{
		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x00013DA3 File Offset: 0x00011FA3
		// (set) Token: 0x060029CF RID: 10703 RVA: 0x00013DAB File Offset: 0x00011FAB
		[DataMember]
		public LookupSubjectDTO Subject { get; set; }
	}
}
