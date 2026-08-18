using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000418 RID: 1048
	[DataContract(Namespace = "http://tpro.ca")]
	public class LectureNoteDTO
	{
		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x0000A9E8 File Offset: 0x00008BE8
		// (set) Token: 0x060016D9 RID: 5849 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		[DataMember]
		public int NotetakerDocumentId { get; set; }

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x0000A9F9 File Offset: 0x00008BF9
		// (set) Token: 0x060016DB RID: 5851 RVA: 0x0000AA01 File Offset: 0x00008C01
		[DataMember]
		public LectureNoteDescriptionDTO LectureNoteDescription { get; set; }

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x0000AA0A File Offset: 0x00008C0A
		// (set) Token: 0x060016DD RID: 5853 RVA: 0x0000AA12 File Offset: 0x00008C12
		[DataMember]
		public BinaryFileDTO LectureNoteDocument { get; set; }
	}
}
