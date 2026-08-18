using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000425 RID: 1061
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddNotesDeletionMarksReq : BaseReportMessageReq
	{
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x0000AB3C File Offset: 0x00008D3C
		// (set) Token: 0x0600170E RID: 5902 RVA: 0x0000AB44 File Offset: 0x00008D44
		[DataMember]
		public DateTime DateOfDeletion { get; set; }

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x0000AB4D File Offset: 0x00008D4D
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x0000AB55 File Offset: 0x00008D55
		[DataMember]
		public int[] NotetakerDocumentIds { get; set; }
	}
}
