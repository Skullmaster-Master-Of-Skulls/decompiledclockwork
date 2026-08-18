using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal.NotesUpload
{
	// Token: 0x02000207 RID: 519
	public class ServiceProviderCourseWithUploadInfo
	{
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0001703D File Offset: 0x0001523D
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x00017045 File Offset: 0x00015245
		public LookupCourseBase Course { get; set; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0001704E File Offset: 0x0001524E
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x00017056 File Offset: 0x00015256
		public DateTime? LastNoteUploadedDate { get; set; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0001705F File Offset: 0x0001525F
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x00017067 File Offset: 0x00015267
		public DateTime? LastEmailReminderForNoNotesUploadSentDate { get; set; }
	}
}
