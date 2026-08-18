using System;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests
{
	// Token: 0x0200019D RID: 413
	public class LoaExternalAccessLogItem
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00013936 File Offset: 0x00011B36
		// (set) Token: 0x06000A91 RID: 2705 RVA: 0x0001393E File Offset: 0x00011B3E
		public int StaffPersonId { get; set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00013947 File Offset: 0x00011B47
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x0001394F File Offset: 0x00011B4F
		public int StudentPersonId { get; set; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00013958 File Offset: 0x00011B58
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x00013960 File Offset: 0x00011B60
		public int LuCourseId { get; set; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x00013969 File Offset: 0x00011B69
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x00013971 File Offset: 0x00011B71
		public DateTime DateAccessed { get; set; }
	}
}
