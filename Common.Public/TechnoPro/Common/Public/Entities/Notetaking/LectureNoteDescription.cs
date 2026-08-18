using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.Notetaking
{
	// Token: 0x02000282 RID: 642
	public class LectureNoteDescription : BusinessBase<int>
	{
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x000196DC File Offset: 0x000178DC
		// (set) Token: 0x0600136B RID: 4971 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int NotetakerDocumentId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x000196F4 File Offset: 0x000178F4
		// (set) Token: 0x0600136D RID: 4973 RVA: 0x000196FC File Offset: 0x000178FC
		public DateTime LectureDate { get; set; }

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x00019705 File Offset: 0x00017905
		// (set) Token: 0x0600136F RID: 4975 RVA: 0x0001970D File Offset: 0x0001790D
		public DateTime DateUploaded { get; set; }

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x00019716 File Offset: 0x00017916
		// (set) Token: 0x06001371 RID: 4977 RVA: 0x0001971E File Offset: 0x0001791E
		public NotetakerBase NotetakerBaseInfo { get; set; }

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x00019727 File Offset: 0x00017927
		// (set) Token: 0x06001373 RID: 4979 RVA: 0x0001972F File Offset: 0x0001792F
		public LookupCourseBase CourseBaseInfo { get; set; }

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x00019738 File Offset: 0x00017938
		// (set) Token: 0x06001375 RID: 4981 RVA: 0x00019740 File Offset: 0x00017940
		public string Comment { get; set; }

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x00019749 File Offset: 0x00017949
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x00019751 File Offset: 0x00017951
		public string Filename { get; set; }

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x0001975A File Offset: 0x0001795A
		// (set) Token: 0x06001379 RID: 4985 RVA: 0x00019762 File Offset: 0x00017962
		public int FileSizeInBytes { get; set; }

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x0001976B File Offset: 0x0001796B
		// (set) Token: 0x0600137B RID: 4987 RVA: 0x00019773 File Offset: 0x00017973
		public DateTime? MarkedForDeletionDate { get; set; }
	}
}
