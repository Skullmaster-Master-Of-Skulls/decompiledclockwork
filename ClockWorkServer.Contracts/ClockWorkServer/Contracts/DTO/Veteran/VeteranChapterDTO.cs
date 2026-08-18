using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Veteran
{
	// Token: 0x02000121 RID: 289
	[DataContract(Namespace = "http://tpro.ca")]
	public class VeteranChapterDTO
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x000033D1 File Offset: 0x000015D1
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x000033D9 File Offset: 0x000015D9
		[DataMember]
		public string ChapterTitle { get; set; }
	}
}
