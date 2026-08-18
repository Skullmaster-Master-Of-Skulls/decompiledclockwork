using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000417 RID: 1047
	[DataContract(Namespace = "http://tpro.ca")]
	public class LectureNoteDescriptionDTO
	{
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x0000A94F File Offset: 0x00008B4F
		// (set) Token: 0x060016C6 RID: 5830 RVA: 0x0000A957 File Offset: 0x00008B57
		[DataMember]
		public int NotetakerDocumentId { get; set; }

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x0000A960 File Offset: 0x00008B60
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x0000A968 File Offset: 0x00008B68
		[DataMember]
		public DateTime LectureDate { get; set; }

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0000A971 File Offset: 0x00008B71
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x0000A979 File Offset: 0x00008B79
		[DataMember]
		public DateTime DateUploaded { get; set; }

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0000A982 File Offset: 0x00008B82
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x0000A98A File Offset: 0x00008B8A
		[DataMember]
		public NotetakerBaseDTO NotetakerBaseInfo { get; set; }

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0000A993 File Offset: 0x00008B93
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x0000A99B File Offset: 0x00008B9B
		[DataMember]
		public LookupCourseBaseDTO CourseBaseInfo { get; set; }

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x0000A9AC File Offset: 0x00008BAC
		[DataMember]
		public string Comment { get; set; }

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0000A9B5 File Offset: 0x00008BB5
		// (set) Token: 0x060016D2 RID: 5842 RVA: 0x0000A9BD File Offset: 0x00008BBD
		[DataMember]
		public string Filename { get; set; }

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0000A9C6 File Offset: 0x00008BC6
		// (set) Token: 0x060016D4 RID: 5844 RVA: 0x0000A9CE File Offset: 0x00008BCE
		[DataMember]
		public DateTime? MarkedForDeletionDate { get; set; }

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0000A9D7 File Offset: 0x00008BD7
		// (set) Token: 0x060016D6 RID: 5846 RVA: 0x0000A9DF File Offset: 0x00008BDF
		[DataMember]
		public int FileSizeInBytes { get; set; }
	}
}
