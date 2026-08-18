using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000446 RID: 1094
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateLectureNoteResp
	{
		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x0000ADE4 File Offset: 0x00008FE4
		// (set) Token: 0x0600177F RID: 6015 RVA: 0x0000ADEC File Offset: 0x00008FEC
		[DataMember]
		public int NotetakerDocumentId { get; set; }
	}
}
