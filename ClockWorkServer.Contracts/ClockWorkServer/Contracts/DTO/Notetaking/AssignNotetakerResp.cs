using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000451 RID: 1105
	[DataContract(Namespace = "http://tpro.ca")]
	public class AssignNotetakerResp
	{
		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x0000AEF4 File Offset: 0x000090F4
		// (set) Token: 0x060017AA RID: 6058 RVA: 0x0000AEFC File Offset: 0x000090FC
		[DataMember]
		public bool WasThisTheFirstStudentAssignedToThisNotetakerAndCourse { get; set; }
	}
}
