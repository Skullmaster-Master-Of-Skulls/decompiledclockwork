using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000416 RID: 1046
	[DataContract(Namespace = "http://tpro.ca")]
	public class DownloadedLectureNoteDTO : LectureNoteDTO
	{
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x0000A935 File Offset: 0x00008B35
		// (set) Token: 0x060016C3 RID: 5827 RVA: 0x0000A93D File Offset: 0x00008B3D
		[DataMember]
		public DateTime LastDateDownloaded { get; set; }
	}
}
