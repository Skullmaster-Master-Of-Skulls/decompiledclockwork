using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.OldSettingsAndPermissions
{
	// Token: 0x020001C0 RID: 448
	public class SnapshotPermission
	{
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0001452B File Offset: 0x0001272B
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x00014533 File Offset: 0x00012733
		public int PermissionId { get; set; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0001453C File Offset: 0x0001273C
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x00014544 File Offset: 0x00012744
		public int PersonId { get; set; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0001454D File Offset: 0x0001274D
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x00014555 File Offset: 0x00012755
		public int PermissionCode { get; set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0001455E File Offset: 0x0001275E
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x00014566 File Offset: 0x00012766
		public int? PermissionValue { get; set; }
	}
}
