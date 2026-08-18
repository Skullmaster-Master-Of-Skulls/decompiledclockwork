using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x02000121 RID: 289
	public class UserPermissionSet : BusinessBase<int>
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0000FA84 File Offset: 0x0000DC84
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PersonId
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

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0000FA9C File Offset: 0x0000DC9C
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
		public IList<UserPermission> PersonPermissions { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0000FAAD File Offset: 0x0000DCAD
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x0000FAB5 File Offset: 0x0000DCB5
		public IList<UserPermission> GroupPermissions { get; set; }
	}
}
