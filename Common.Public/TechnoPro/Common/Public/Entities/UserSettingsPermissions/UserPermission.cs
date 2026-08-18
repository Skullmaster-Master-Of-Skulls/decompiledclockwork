using System;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x02000118 RID: 280
	[Serializable]
	public class UserPermission : ICloneable<UserPermission>, ICloneable
	{
		// Token: 0x060006A3 RID: 1699 RVA: 0x0000D55A File Offset: 0x0000B75A
		public UserPermission()
		{
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0000F804 File Offset: 0x0000DA04
		public UserPermission(UserPermission item)
		{
			this.PersonOrGroupId = item.PersonOrGroupId;
			this.PermissionType = item.PermissionType;
			this.Permission = item.Permission;
			this.PermissionValue = item.PermissionValue;
			this.OrderNum = item.OrderNum;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0000F85A File Offset: 0x0000DA5A
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0000F862 File Offset: 0x0000DA62
		public int PersonOrGroupId { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0000F86B File Offset: 0x0000DA6B
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0000F873 File Offset: 0x0000DA73
		public eUserPermissionType PermissionType { get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0000F87C File Offset: 0x0000DA7C
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0000F884 File Offset: 0x0000DA84
		public UserPermissionEnum Permission { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0000F88D File Offset: 0x0000DA8D
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x0000F895 File Offset: 0x0000DA95
		public int PermissionValue { get; set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0000F89E File Offset: 0x0000DA9E
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x0000F8A6 File Offset: 0x0000DAA6
		public int OrderNum { get; set; }

		// Token: 0x060006AF RID: 1711 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
		public UserPermission Clone()
		{
			return new UserPermission(this);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000F8C8 File Offset: 0x0000DAC8
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
