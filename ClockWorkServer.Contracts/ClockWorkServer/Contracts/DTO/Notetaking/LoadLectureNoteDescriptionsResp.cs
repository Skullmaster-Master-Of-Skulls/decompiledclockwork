using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200041C RID: 1052
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLectureNoteDescriptionsResp
	{
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		// (set) Token: 0x060016FD RID: 5885 RVA: 0x0000AB00 File Offset: 0x00008D00
		[DataMember]
		public IList<LectureNoteDescriptionDTO> LectureNoteDescriptions { get; set; }
	}
}
