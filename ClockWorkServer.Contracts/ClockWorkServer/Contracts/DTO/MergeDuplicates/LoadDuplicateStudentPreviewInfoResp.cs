using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates
{
	// Token: 0x02000466 RID: 1126
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDuplicateStudentPreviewInfoResp
	{
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x0000B136 File Offset: 0x00009336
		// (set) Token: 0x06001803 RID: 6147 RVA: 0x0000B13E File Offset: 0x0000933E
		[DataMember]
		public DuplicateStudentSetDTO DuplicateStudentSet { get; set; }
	}
}
