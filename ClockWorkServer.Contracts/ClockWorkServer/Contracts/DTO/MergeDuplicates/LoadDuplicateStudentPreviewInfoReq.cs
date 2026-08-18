using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates
{
	// Token: 0x02000465 RID: 1125
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDuplicateStudentPreviewInfoReq : BaseMessageReq
	{
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x0000B125 File Offset: 0x00009325
		// (set) Token: 0x06001800 RID: 6144 RVA: 0x0000B12D File Offset: 0x0000932D
		[DataMember]
		public DuplicateStudentSetDTO DuplicateStudentSet { get; set; }
	}
}
