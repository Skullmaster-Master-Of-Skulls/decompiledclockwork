using System;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x0200011A RID: 282
	[Serializable]
	public class UserPermissionAttribute : Attribute
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public UserPermissionAttribute()
		{
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000F8E0 File Offset: 0x0000DAE0
		public UserPermissionAttribute(string name, UserPermissionGroup group)
		{
			this.Name = name;
			this.Group = group;
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0000F8FA File Offset: 0x0000DAFA
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0000F902 File Offset: 0x0000DB02
		public string Name { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0000F90B File Offset: 0x0000DB0B
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0000F913 File Offset: 0x0000DB13
		public UserPermissionGroup Group { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0000F91C File Offset: 0x0000DB1C
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0000F924 File Offset: 0x0000DB24
		public bool IsHidden { get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0000F92D File Offset: 0x0000DB2D
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x0000F935 File Offset: 0x0000DB35
		public PermissionSemantic PermissionSemantic { get; set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0000F93E File Offset: 0x0000DB3E
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x0000F946 File Offset: 0x0000DB46
		public string Description { get; set; }
	}
}
