using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000432 RID: 1074
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadNotetakerBaseByNotetakeeAndCourseResp
	{
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x0000AC2A File Offset: 0x00008E2A
		// (set) Token: 0x06001737 RID: 5943 RVA: 0x0000AC32 File Offset: 0x00008E32
		[DataMember]
		public NotetakerBaseDTO NotetakerBase { get; set; }
	}
}
