using System;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x0200011D RID: 285
	[Serializable]
	public class UserPermissionGroupAttribute : Attribute
	{
		// Token: 0x060006BD RID: 1725 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public UserPermissionGroupAttribute()
		{
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0000F94F File Offset: 0x0000DB4F
		public UserPermissionGroupAttribute(string name, int iconIndex, string description)
		{
			this.Name = name;
			this.IconIndex = iconIndex;
			this.Description = description;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0000F971 File Offset: 0x0000DB71
		public UserPermissionGroupAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0000F983 File Offset: 0x0000DB83
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0000F98B File Offset: 0x0000DB8B
		public string Name { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0000F994 File Offset: 0x0000DB94
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0000F99C File Offset: 0x0000DB9C
		public int IconIndex { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0000F9A5 File Offset: 0x0000DBA5
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x0000F9AD File Offset: 0x0000DBAD
		public string Description { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0000F9B6 File Offset: 0x0000DBB6
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0000F9BE File Offset: 0x0000DBBE
		public bool IsScreenViewModifyCreatePermissions { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0000F9C7 File Offset: 0x0000DBC7
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0000F9CF File Offset: 0x0000DBCF
		public bool IsHidden { get; set; }
	}
}
