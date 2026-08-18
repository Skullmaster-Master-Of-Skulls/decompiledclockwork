using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.PeopleAndGroups
{
	// Token: 0x020001BC RID: 444
	public class SnapshotGroup
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00014382 File Offset: 0x00012582
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x0001438A File Offset: 0x0001258A
		public int GroupId { get; set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x00014393 File Offset: 0x00012593
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x0001439B File Offset: 0x0001259B
		public string Description { get; set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x000143A4 File Offset: 0x000125A4
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x000143AC File Offset: 0x000125AC
		public bool IsPrimary { get; set; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x000143B5 File Offset: 0x000125B5
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x000143BD File Offset: 0x000125BD
		public bool ViewAppsVisible { get; set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x000143C6 File Offset: 0x000125C6
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x000143CE File Offset: 0x000125CE
		public string FullDescription { get; set; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x000143D7 File Offset: 0x000125D7
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x000143DF File Offset: 0x000125DF
		public int OrderNum { get; set; }
	}
}
