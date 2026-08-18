using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.OldSettingsAndPermissions
{
	// Token: 0x020001C1 RID: 449
	public class SnapshotPermissionGroup
	{
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0001456F File Offset: 0x0001276F
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x00014577 File Offset: 0x00012777
		public int PermissionGroupId { get; set; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00014580 File Offset: 0x00012780
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x00014588 File Offset: 0x00012788
		public int GroupId { get; set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00014591 File Offset: 0x00012791
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x00014599 File Offset: 0x00012799
		public int PermissionCode { get; set; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x000145A2 File Offset: 0x000127A2
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x000145AA File Offset: 0x000127AA
		public int? PermissionValue { get; set; }
	}
}
