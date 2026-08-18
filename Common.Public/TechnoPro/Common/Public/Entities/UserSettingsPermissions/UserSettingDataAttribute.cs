using System;
using System.Reflection;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions
{
	// Token: 0x02000124 RID: 292
	public class UserSettingDataAttribute : UserSettingGroupDataAttribute
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x0000FBE8 File Offset: 0x0000DDE8
		public string SubGroup
		{
			get
			{
				return this.subGroup;
			}
			set
			{
				this.subGroup = value;
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000FBF2 File Offset: 0x0000DDF2
		public UserSettingDataAttribute(string name, UserSettingGroup group, SettingSemantic semanticType) : base(name)
		{
			this.group = group;
			this.semanticType = semanticType;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000FC0B File Offset: 0x0000DE0B
		public UserSettingDataAttribute(string name, string description, UserSettingGroup group, SettingSemantic semanticType) : base(name)
		{
			this.description = description;
			this.group = group;
			this.semanticType = semanticType;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		public UserSettingDataAttribute(string name, string subGroup, string description, UserSettingGroup group, SettingSemantic semanticType) : base(name)
		{
			this.subGroup = subGroup;
			this.description = description;
			this.group = group;
			this.semanticType = semanticType;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0000FC58 File Offset: 0x0000DE58
		public UserSettingGroup Group
		{
			get
			{
				return this.group;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0000FC70 File Offset: 0x0000DE70
		public Type SystemType
		{
			get
			{
				Type type = this.semanticType.GetType();
				FieldInfo field = type.GetField(this.semanticType.ToString());
				SemanticTypeAttribute[] array = field.GetCustomAttributes(typeof(SemanticTypeAttribute), false) as SemanticTypeAttribute[];
				bool flag = array != null && array.Length != 0;
				Type result;
				if (flag)
				{
					result = array[0].SystemType;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0000FCE4 File Offset: 0x0000DEE4
		public SettingSemantic SemanticType
		{
			get
			{
				return this.semanticType;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0000FCFC File Offset: 0x0000DEFC
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x0000FD14 File Offset: 0x0000DF14
		public object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0000FD20 File Offset: 0x0000DF20
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x0000FD38 File Offset: 0x0000DF38
		public bool IsHidden
		{
			get
			{
				return this.hidden;
			}
			set
			{
				this.hidden = value;
			}
		}

		// Token: 0x0400035F RID: 863
		protected UserSettingGroup group;

		// Token: 0x04000360 RID: 864
		protected SettingSemantic semanticType;

		// Token: 0x04000361 RID: 865
		protected object defaultValue;

		// Token: 0x04000362 RID: 866
		protected bool hidden;

		// Token: 0x04000363 RID: 867
		protected string subGroup;
	}
}
