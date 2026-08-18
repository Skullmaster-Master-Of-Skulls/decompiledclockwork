using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000796 RID: 1942
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAlternateContactByIdResp
	{
		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x00012CDA File Offset: 0x00010EDA
		// (set) Token: 0x060027F0 RID: 10224 RVA: 0x00012CE2 File Offset: 0x00010EE2
		[DataMember]
		public AlternateContactDTO AltContact { get; set; }
	}
}
