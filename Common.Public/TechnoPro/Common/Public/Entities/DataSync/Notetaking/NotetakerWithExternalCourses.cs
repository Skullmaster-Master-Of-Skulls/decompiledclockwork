using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Public.Entities.DataSync.Notetaking
{
	// Token: 0x020003DB RID: 987
	public class NotetakerWithExternalCourses
	{
		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x00021FE2 File Offset: 0x000201E2
		// (set) Token: 0x06001E7D RID: 7805 RVA: 0x00021FEA File Offset: 0x000201EA
		public SPProvider Notetaker { get; set; }

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x00021FF3 File Offset: 0x000201F3
		// (set) Token: 0x06001E7F RID: 7807 RVA: 0x00021FFB File Offset: 0x000201FB
		public IList<DataSyncExternalCourse> ExternalCourses { get; set; }
	}
}
