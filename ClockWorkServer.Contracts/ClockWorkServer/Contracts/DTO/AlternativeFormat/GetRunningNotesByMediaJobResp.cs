using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BA9 RID: 2985
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetRunningNotesByMediaJobResp
	{
		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x06003F31 RID: 16177 RVA: 0x0001F1E5 File Offset: 0x0001D3E5
		// (set) Token: 0x06003F32 RID: 16178 RVA: 0x0001F1ED File Offset: 0x0001D3ED
		[DataMember]
		public IList<MediaJobRunningNoteDTO> RunningNoteList { get; set; }
	}
}
