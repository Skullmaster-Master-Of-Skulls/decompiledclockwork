using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates
{
	// Token: 0x02000463 RID: 1123
	[DataContract(Namespace = "http://tpro.ca")]
	public class MergeDuplicateStudentsReq : BaseMessageReq
	{
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0000B114 File Offset: 0x00009314
		// (set) Token: 0x060017FC RID: 6140 RVA: 0x0000B11C File Offset: 0x0000931C
		[DataMember]
		public DuplicateStudentSetDTO DuplicateStudentSet { get; set; }
	}
}
