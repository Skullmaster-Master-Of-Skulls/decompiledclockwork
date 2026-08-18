using System;
using System.Linq;
using TechnoPro.Common.Public.Entities.SettingsPermissionsGeneral;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x0200012C RID: 300
	[Serializable]
	public class OldUserSettingAttribute : Attribute
	{
		// Token: 0x06000713 RID: 1811 RVA: 0x0000FED2 File Offset: 0x0000E0D2
		public OldUserSettingAttribute()
		{
			this.Title = "";
			this.Description = "";
			this.DefaultValueInt = -1;
			this.DefaultValueString = "";
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0000FF08 File Offset: 0x0000E108
		public OldUserSettingAttribute(string title, eOldUserSettingInputType inputType, eOldUserSettingGroup group)
		{
			this.Title = title;
			this.Description = "";
			this.InputType = inputType;
			this.Group = group;
			this.DefaultValueInt = -1;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0000FF3E File Offset: 0x0000E13E
		public OldUserSettingAttribute(string title, string description, eOldUserSettingInputType inputType, eOldUserSettingGroup group)
		{
			this.Title = title;
			this.Description = description;
			this.InputType = inputType;
			this.Group = group;
			this.DefaultValueInt = -1;
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0000FF71 File Offset: 0x0000E171
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0000FF79 File Offset: 0x0000E179
		public eOldUserSettingGroup Group { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0000FF82 File Offset: 0x0000E182
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0000FF8A File Offset: 0x0000E18A
		public string Title { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0000FF93 File Offset: 0x0000E193
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0000FF9B File Offset: 0x0000E19B
		public string Description { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0000FFA4 File Offset: 0x0000E1A4
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0000FFAC File Offset: 0x0000E1AC
		public bool IsHidden { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0000FFB5 File Offset: 0x0000E1B5
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0000FFBD File Offset: 0x0000E1BD
		public eOldUserSettingInputType InputType { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0000FFC6 File Offset: 0x0000E1C6
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0000FFCE File Offset: 0x0000E1CE
		public eSettingLevel SettingLevel { get; set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0000FFD7 File Offset: 0x0000E1D7
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0000FFDF File Offset: 0x0000E1DF
		public int DefaultValueInt { get; set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x0000FFF0 File Offset: 0x0000E1F0
		public string DefaultValueString { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0000FFF9 File Offset: 0x0000E1F9
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x00010001 File Offset: 0x0000E201
		public string SubGroup { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001000A File Offset: 0x0000E20A
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x00010012 File Offset: 0x0000E212
		public string Example { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001001B File Offset: 0x0000E21B
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x00010023 File Offset: 0x0000E223
		public bool AllowOrderingForListControls { get; set; }

		// Token: 0x0600072C RID: 1836 RVA: 0x0001002C File Offset: 0x0000E22C
		public static OldUserSettingAttribute GetAttribute(eSettingCode enumItem)
		{
			return OldUserSettingAttribute.GetAttribute<OldUserSettingAttribute>(enumItem);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001004C File Offset: 0x0000E24C
		public static T GetAttribute<T>(Enum enumeration) where T : Attribute
		{
			T t = enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault<T>();
			T result;
			if ((result = t) == null)
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000100A0 File Offset: 0x0000E2A0
		public eOldUserSettingStorageLocation GetStorageLocation()
		{
			OldUserSettingInputTypeAttribute attribute = OldUserSettingInputTypeAttribute.GetAttribute(this.InputType);
			return (attribute != null) ? attribute.StorageLocation : eOldUserSettingStorageLocation.Unknown;
		}
	}
}
