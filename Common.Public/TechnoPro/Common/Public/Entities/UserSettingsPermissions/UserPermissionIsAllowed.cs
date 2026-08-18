using System;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x0200011F RID: 287
	[Serializable]
	public class UserPermissionIsAllowed : BusinessBase<UserPermissionEnum>
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0000F9D8 File Offset: 0x0000DBD8
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		public virtual UserPermissionEnum Permission
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0000F9FB File Offset: 0x0000DBFB
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x0000FA03 File Offset: 0x0000DC03
		public bool IsAllowed { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0000FA0C File Offset: 0x0000DC0C
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x0000FA14 File Offset: 0x0000DC14
		public eUserPermissionType PermissionType { get; set; }
	}
}
