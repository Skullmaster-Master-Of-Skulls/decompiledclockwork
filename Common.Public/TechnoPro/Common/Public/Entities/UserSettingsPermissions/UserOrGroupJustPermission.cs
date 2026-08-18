using System;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x02000116 RID: 278
	public class UserOrGroupJustPermission : ICloneable<UserOrGroupJustPermission>, ICloneable
	{
		// Token: 0x06000689 RID: 1673 RVA: 0x0000D55A File Offset: 0x0000B75A
		public UserOrGroupJustPermission()
		{
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0000F632 File Offset: 0x0000D832
		public UserOrGroupJustPermission(UserOrGroupJustPermission item)
		{
			this.Id = item.Id;
			this.Permission = item.Permission;
			this.IsAllowed = item.IsAllowed;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0000F663 File Offset: 0x0000D863
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0000F66B File Offset: 0x0000D86B
		public int Id { get; set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0000F674 File Offset: 0x0000D874
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0000F67C File Offset: 0x0000D87C
		public UserPermissionEnum Permission { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0000F685 File Offset: 0x0000D885
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x0000F68D File Offset: 0x0000D88D
		public bool IsAllowed { get; set; }

		// Token: 0x06000691 RID: 1681 RVA: 0x0000F698 File Offset: 0x0000D898
		public UserOrGroupJustPermission Clone()
		{
			return new UserOrGroupJustPermission(this);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0000F6B0 File Offset: 0x0000D8B0
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
