using System;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x02000128 RID: 296
	[Serializable]
	public class OldUserSettingGroupAttribute : Attribute
	{
		// Token: 0x060006FE RID: 1790 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public OldUserSettingGroupAttribute()
		{
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0000FD42 File Offset: 0x0000DF42
		public OldUserSettingGroupAttribute(string displayName)
		{
			this.DisplayName = displayName;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0000FD54 File Offset: 0x0000DF54
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0000FD5C File Offset: 0x0000DF5C
		public string DisplayName { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0000FD65 File Offset: 0x0000DF65
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0000FD6D File Offset: 0x0000DF6D
		public string Description { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0000FD76 File Offset: 0x0000DF76
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0000FD7E File Offset: 0x0000DF7E
		public bool IsHidden { get; set; }

		// Token: 0x06000706 RID: 1798 RVA: 0x0000FD88 File Offset: 0x0000DF88
		public static OldUserSettingGroupAttribute GetAttribute(eOldUserSettingGroup enumItem)
		{
			return OldUserSettingGroupAttribute.GetAttribute<OldUserSettingGroupAttribute>(enumItem);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0000FDA5 File Offset: 0x0000DFA5
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x0000FDAD File Offset: 0x0000DFAD
		public int OrderNum { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0000FDB8 File Offset: 0x0000DFB8
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x0000FDD0 File Offset: 0x0000DFD0
		public string IconName
		{
			get
			{
				return this.iconName;
			}
			set
			{
				this.iconName = value;
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0000FDDC File Offset: 0x0000DFDC
		public static T GetAttribute<T>(Enum enumeration) where T : Attribute
		{
			T t = enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault<T>();
			bool flag = t == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				result = t;
			}
			return result;
		}

		// Token: 0x0400038B RID: 907
		protected string iconName;
	}
}
