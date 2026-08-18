using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates
{
	// Token: 0x02000462 RID: 1122
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindPotentialDuplicateStudentsResp
	{
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0000B103 File Offset: 0x00009303
		// (set) Token: 0x060017F9 RID: 6137 RVA: 0x0000B10B File Offset: 0x0000930B
		[DataMember]
		public IList<PotentialDuplicateStudentSetDTO> PotentialDuplicateSets { get; set; }
	}
}
