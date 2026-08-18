using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.DynamicControls
{
	// Token: 0x020001C9 RID: 457
	public class SnapshotLookupGroups
	{
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00014A6A File Offset: 0x00012C6A
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00014A72 File Offset: 0x00012C72
		public int LookupGroupId { get; set; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00014A7B File Offset: 0x00012C7B
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x00014A83 File Offset: 0x00012C83
		public string Description { get; set; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00014A8C File Offset: 0x00012C8C
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x00014A94 File Offset: 0x00012C94
		public int SortBy { get; set; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00014A9D File Offset: 0x00012C9D
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x00014AA5 File Offset: 0x00012CA5
		public int ChildList { get; set; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00014AAE File Offset: 0x00012CAE
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x00014AB6 File Offset: 0x00012CB6
		public bool IsVisible { get; set; }
	}
}
