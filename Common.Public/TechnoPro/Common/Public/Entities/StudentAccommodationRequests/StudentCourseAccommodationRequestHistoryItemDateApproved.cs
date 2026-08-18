using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests
{
	// Token: 0x020001A5 RID: 421
	public class StudentCourseAccommodationRequestHistoryItemDateApproved
	{
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00013C33 File Offset: 0x00011E33
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x00013C3B File Offset: 0x00011E3B
		public int StudentCourseAccommodationRequestId { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00013C44 File Offset: 0x00011E44
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x00013C4C File Offset: 0x00011E4C
		public DateTime DateApproved { get; set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00013C55 File Offset: 0x00011E55
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x00013C5D File Offset: 0x00011E5D
		public PersonBase WhoApproved { get; set; }
	}
}
