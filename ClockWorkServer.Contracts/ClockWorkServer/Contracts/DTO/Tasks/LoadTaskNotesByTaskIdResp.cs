using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001EA RID: 490
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTaskNotesByTaskIdResp
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00005286 File Offset: 0x00003486
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x0000528E File Offset: 0x0000348E
		[DataMember]
		public List<TaskNoteDTO> TaskNotes { get; set; }
	}
}
