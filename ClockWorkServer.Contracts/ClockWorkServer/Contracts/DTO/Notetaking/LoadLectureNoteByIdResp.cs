using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000438 RID: 1080
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLectureNoteByIdResp
	{
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x0000ACB2 File Offset: 0x00008EB2
		// (set) Token: 0x0600174D RID: 5965 RVA: 0x0000ACBA File Offset: 0x00008EBA
		[DataMember]
		public LectureNoteDTO LectureNote { get; set; }
	}
}
