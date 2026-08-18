using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates
{
	// Token: 0x02000467 RID: 1127
	[DataContract(Namespace = "http://tpro.ca")]
	public class PotentialDuplicateStudentSetDTO
	{
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0000B147 File Offset: 0x00009347
		// (set) Token: 0x06001806 RID: 6150 RVA: 0x0000B14F File Offset: 0x0000934F
		[DataMember]
		public PersonBaseDTO Student1 { get; set; }

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0000B158 File Offset: 0x00009358
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x0000B160 File Offset: 0x00009360
		[DataMember]
		public PersonBaseDTO Student2 { get; set; }

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x0000B169 File Offset: 0x00009369
		// (set) Token: 0x0600180A RID: 6154 RVA: 0x0000B171 File Offset: 0x00009371
		[DataMember]
		public int EditDistance { get; set; }
	}
}
