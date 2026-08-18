using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007BF RID: 1983
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAcademicTermResp
	{
		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x060028A6 RID: 10406 RVA: 0x0001345A File Offset: 0x0001165A
		// (set) Token: 0x060028A7 RID: 10407 RVA: 0x00013462 File Offset: 0x00011662
		[DataMember]
		public AcademicTermDTO AcademicTerm { get; set; }
	}
}
