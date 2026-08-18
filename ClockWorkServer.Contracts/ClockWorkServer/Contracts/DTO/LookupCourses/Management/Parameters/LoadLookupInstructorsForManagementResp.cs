using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management.Parameters
{
	// Token: 0x02000817 RID: 2071
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLookupInstructorsForManagementResp
	{
		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06002A3A RID: 10810 RVA: 0x000140EA File Offset: 0x000122EA
		// (set) Token: 0x06002A3B RID: 10811 RVA: 0x000140F2 File Offset: 0x000122F2
		[DataMember]
		public LookInstructorForManagementListDTO InstructorList { get; set; }
	}
}
