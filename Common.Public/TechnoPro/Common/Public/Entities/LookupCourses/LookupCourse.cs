using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.LookupCourses
{
	// Token: 0x020002EC RID: 748
	[Serializable]
	public class LookupCourse : LookupCourseBase
	{
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x0001BD96 File Offset: 0x00019F96
		// (set) Token: 0x0600167F RID: 5759 RVA: 0x0001BD9E File Offset: 0x00019F9E
		public List<LookupInstructor> Instructors { get; set; }

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x0001BDA7 File Offset: 0x00019FA7
		// (set) Token: 0x06001681 RID: 5761 RVA: 0x0001BDAF File Offset: 0x00019FAF
		public List<LookupTimetableItem> TimetableItems { get; set; }

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x0001BDB8 File Offset: 0x00019FB8
		// (set) Token: 0x06001683 RID: 5763 RVA: 0x0001BDC0 File Offset: 0x00019FC0
		public List<AlternateContact> AlternateContacts { get; set; }

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0001BDC9 File Offset: 0x00019FC9
		// (set) Token: 0x06001685 RID: 5765 RVA: 0x0001BDD1 File Offset: 0x00019FD1
		public bool IsExemptFromDataSync { get; set; }

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0001BDDA File Offset: 0x00019FDA
		// (set) Token: 0x06001687 RID: 5767 RVA: 0x0001BDE2 File Offset: 0x00019FE2
		public string ExternalCourseId { get; set; }

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x0001BDEB File Offset: 0x00019FEB
		// (set) Token: 0x06001689 RID: 5769 RVA: 0x0001BDF3 File Offset: 0x00019FF3
		public int BatchDataSyncLogId { get; set; }
	}
}
