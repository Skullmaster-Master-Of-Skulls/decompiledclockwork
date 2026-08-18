using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007DB RID: 2011
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupDurationTermSubjectDTO
	{
		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x0001367A File Offset: 0x0001187A
		// (set) Token: 0x06002903 RID: 10499 RVA: 0x00013682 File Offset: 0x00011882
		[DataMember]
		public string Duration { get; set; }

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06002904 RID: 10500 RVA: 0x0001368B File Offset: 0x0001188B
		// (set) Token: 0x06002905 RID: 10501 RVA: 0x00013693 File Offset: 0x00011893
		[DataMember]
		public string Term { get; set; }

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06002906 RID: 10502 RVA: 0x0001369C File Offset: 0x0001189C
		// (set) Token: 0x06002907 RID: 10503 RVA: 0x000136A4 File Offset: 0x000118A4
		[DataMember]
		public int SubjectId { get; set; }

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06002908 RID: 10504 RVA: 0x000136AD File Offset: 0x000118AD
		// (set) Token: 0x06002909 RID: 10505 RVA: 0x000136B5 File Offset: 0x000118B5
		[DataMember]
		public string SubjectTitle { get; set; }
	}
}
