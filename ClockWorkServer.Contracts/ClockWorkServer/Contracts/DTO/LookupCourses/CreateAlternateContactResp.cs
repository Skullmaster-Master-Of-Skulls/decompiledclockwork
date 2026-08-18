using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x02000794 RID: 1940
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateAlternateContactResp
	{
		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x00012CB8 File Offset: 0x00010EB8
		// (set) Token: 0x060027EA RID: 10218 RVA: 0x00012CC0 File Offset: 0x00010EC0
		[DataMember]
		public int AlternateContactId { get; set; }
	}
}
