using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x02000120 RID: 288
	[Serializable]
	public class UserPermissionIsAllowedSet : BusinessBase<int>
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0000FA28 File Offset: 0x0000DC28
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x0000E258 File Offset: 0x0000C458
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

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0000FA40 File Offset: 0x0000DC40
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x0000FA48 File Offset: 0x0000DC48
		public IList<UserPermissionIsAllowed> GeneralPermissionsAllowed { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0000FA51 File Offset: 0x0000DC51
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x0000FA59 File Offset: 0x0000DC59
		public IList<int> ScreenNumsAllowedViewScreen { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0000FA62 File Offset: 0x0000DC62
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x0000FA6A File Offset: 0x0000DC6A
		public IList<int> ScreenNumsAllowedModifyScreen { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0000FA73 File Offset: 0x0000DC73
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x0000FA7B File Offset: 0x0000DC7B
		public IList<int> ScreenNumsAllowedCreateScreen { get; set; }
	}
}
