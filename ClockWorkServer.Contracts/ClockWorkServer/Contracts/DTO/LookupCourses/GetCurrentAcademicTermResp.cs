using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007B9 RID: 1977
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCurrentAcademicTermResp
	{
		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x000133F4 File Offset: 0x000115F4
		// (set) Token: 0x06002895 RID: 10389 RVA: 0x000133FC File Offset: 0x000115FC
		[DataMember]
		public AcademicTermDTO AcademicTerm { get; set; }
	}
}
