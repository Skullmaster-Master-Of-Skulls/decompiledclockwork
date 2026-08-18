using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000423 RID: 1059
	[DataContract(Namespace = "http://tpro.ca")]
	public class RemoveNotesDeletionMarksReq : BaseReportMessageReq
	{
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x0000AB2B File Offset: 0x00008D2B
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x0000AB33 File Offset: 0x00008D33
		[DataMember]
		public int[] NotetakerDocumentIds { get; set; }
	}
}
