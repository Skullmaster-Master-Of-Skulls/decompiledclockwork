using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200078E RID: 1934
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAlternateContactByEmployeeIdResp
	{
		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x060027D3 RID: 10195 RVA: 0x00012C30 File Offset: 0x00010E30
		// (set) Token: 0x060027D4 RID: 10196 RVA: 0x00012C38 File Offset: 0x00010E38
		[DataMember]
		public AlternateContactDTO AlternateContact { get; set; }
	}
}
