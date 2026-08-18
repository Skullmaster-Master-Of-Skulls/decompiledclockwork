using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.Reports
{
	// Token: 0x020001BA RID: 442
	public class SnapshotSearchGroupInfo
	{
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0001420C File Offset: 0x0001240C
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x00014214 File Offset: 0x00012414
		public int SearchGroupInfoId { get; set; }

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0001421D File Offset: 0x0001241D
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x00014225 File Offset: 0x00012425
		public string GroupTitle { get; set; }

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0001422E File Offset: 0x0001242E
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x00014236 File Offset: 0x00012436
		public string GroupDescription { get; set; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0001423F File Offset: 0x0001243F
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x00014247 File Offset: 0x00012447
		public int IconIndex { get; set; }

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x00014250 File Offset: 0x00012450
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x00014258 File Offset: 0x00012458
		public int OrderNum { get; set; }

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00014261 File Offset: 0x00012461
		// (set) Token: 0x06000BB3 RID: 2995 RVA: 0x00014269 File Offset: 0x00012469
		public int ParentSearchGroupInfoId { get; set; }
	}
}
