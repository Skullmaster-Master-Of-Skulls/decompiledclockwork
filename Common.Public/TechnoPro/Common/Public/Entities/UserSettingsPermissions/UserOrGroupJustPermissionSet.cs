using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x02000117 RID: 279
	public class UserOrGroupJustPermissionSet : ICloneable<UserOrGroupJustPermissionSet>, ICloneable
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x0000D55A File Offset: 0x0000B75A
		public UserOrGroupJustPermissionSet()
		{
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0000F6C8 File Offset: 0x0000D8C8
		public UserOrGroupJustPermissionSet(UserOrGroupJustPermissionSet item)
		{
			this.PermissionType = item.PermissionType;
			this.PersonOrGroupId = item.PersonOrGroupId;
			IList<UserOrGroupJustPermission> generalPermissions;
			if (item.GeneralPermissions != null)
			{
				generalPermissions = (from g in item.GeneralPermissions
				select g.Clone()).ToList<UserOrGroupJustPermission>();
			}
			else
			{
				generalPermissions = null;
			}
			this.GeneralPermissions = generalPermissions;
			this.ScreenNumsAllowedViewScreen = new List<int>(item.ScreenNumsAllowedViewScreen);
			this.ScreenNumsAllowedModifyScreen = new List<int>(item.ScreenNumsAllowedModifyScreen);
			this.ScreenNumsAllowedCreateScreen = new List<int>(item.ScreenNumsAllowedCreateScreen);
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0000F76E File Offset: 0x0000D96E
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x0000F776 File Offset: 0x0000D976
		public eUserPermissionType PermissionType { get; set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0000F77F File Offset: 0x0000D97F
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x0000F787 File Offset: 0x0000D987
		public int PersonOrGroupId { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0000F790 File Offset: 0x0000D990
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x0000F798 File Offset: 0x0000D998
		public IList<UserOrGroupJustPermission> GeneralPermissions { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0000F7A1 File Offset: 0x0000D9A1
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0000F7A9 File Offset: 0x0000D9A9
		public IList<int> ScreenNumsAllowedViewScreen { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0000F7B2 File Offset: 0x0000D9B2
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x0000F7BA File Offset: 0x0000D9BA
		public IList<int> ScreenNumsAllowedModifyScreen { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0000F7C3 File Offset: 0x0000D9C3
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0000F7CB File Offset: 0x0000D9CB
		public IList<int> ScreenNumsAllowedCreateScreen { get; set; }

		// Token: 0x060006A1 RID: 1697 RVA: 0x0000F7D4 File Offset: 0x0000D9D4
		public UserOrGroupJustPermissionSet Clone()
		{
			return new UserOrGroupJustPermissionSet(this);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0000F7EC File Offset: 0x0000D9EC
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
