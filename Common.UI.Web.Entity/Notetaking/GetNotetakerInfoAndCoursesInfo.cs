using System;

namespace TechnoPro.Common.UI.Web.Entity.Notetaking
{
	// Token: 0x0200002B RID: 43
	public class GetNotetakerInfoAndCoursesInfo
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00002E40 File Offset: 0x00001040
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00002E48 File Offset: 0x00001048
		public eGetNotetakerInfoAndCoursesSource Source { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00002E51 File Offset: 0x00001051
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00002E59 File Offset: 0x00001059
		public string StudentNumber { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00002E62 File Offset: 0x00001062
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00002E6A File Offset: 0x0000106A
		public string Username { get; set; }
	}
}
