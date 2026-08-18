using System;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.Adapters;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x02000115 RID: 277
	public class LookupUserSetting : BusinessBase<UserLookupSetting>
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0000F410 File Offset: 0x0000D610
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0000F428 File Offset: 0x0000D628
		public UserLookupSetting Setting
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
				this.SettingDataAttribute = value.GetSettingAttribute();
				this.GroupDataAtt = this.SettingDataAttribute.Group.GetGroupAttribute();
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0000F457 File Offset: 0x0000D657
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0000F45F File Offset: 0x0000D65F
		public UserSettingDataAttribute SettingDataAttribute { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0000F468 File Offset: 0x0000D668
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x0000F470 File Offset: 0x0000D670
		private UserSettingGroupDataAttribute GroupDataAtt { get; set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0000F47C File Offset: 0x0000D67C
		public UserSettingGroup Group
		{
			get
			{
				return this.SettingDataAttribute.Group;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0000F49C File Offset: 0x0000D69C
		public string Name
		{
			get
			{
				return this.SettingDataAttribute.Name;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0000F4BC File Offset: 0x0000D6BC
		public string SubGroup
		{
			get
			{
				return this.SettingDataAttribute.SubGroup ?? string.Empty;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0000F4E4 File Offset: 0x0000D6E4
		public string GroupName
		{
			get
			{
				return (this.GroupDataAtt != null) ? this.GroupDataAtt.Name : Enum.GetName(typeof(Group), this.SettingDataAttribute.Group);
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0000F52C File Offset: 0x0000D72C
		public string Description
		{
			get
			{
				return this.SettingDataAttribute.Description;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0000F54C File Offset: 0x0000D74C
		public Type SystemType
		{
			get
			{
				return this.SettingDataAttribute.SystemType;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0000F56C File Offset: 0x0000D76C
		public SettingSemantic SemanticType
		{
			get
			{
				return this.SettingDataAttribute.SemanticType;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0000F58C File Offset: 0x0000D78C
		public bool HasDefaultValue
		{
			get
			{
				return this.SettingDataAttribute.DefaultValue != null;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0000F5AC File Offset: 0x0000D7AC
		public bool IsHidden
		{
			get
			{
				return this.SettingDataAttribute.IsHidden;
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000F5C9 File Offset: 0x0000D7C9
		public LookupUserSetting(UserLookupSetting setting)
		{
			this.Setting = setting;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0000F5DC File Offset: 0x0000D7DC
		public T GetDefaultValue<T>()
		{
			return (this.SettingDataAttribute.DefaultValue != null && typeof(T).IsInstanceOfType(this.SettingDataAttribute.DefaultValue)) ? ((T)((object)this.SettingDataAttribute.DefaultValue)) : default(T);
		}
	}
}
