using System;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings
{
	// Token: 0x0200012A RID: 298
	public class OldUserSettingInputTypeAttribute : Attribute
	{
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0000FE36 File Offset: 0x0000E036
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x0000FE3E File Offset: 0x0000E03E
		public eOldUserSettingStorageLocation StorageLocation { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0000FE47 File Offset: 0x0000E047
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0000FE4F File Offset: 0x0000E04F
		public string WinFormsEditControlClass { get; set; }

		// Token: 0x06000711 RID: 1809 RVA: 0x0000FE58 File Offset: 0x0000E058
		public static OldUserSettingInputTypeAttribute GetAttribute(eOldUserSettingInputType enumItem)
		{
			return OldUserSettingInputTypeAttribute.GetAttribute<OldUserSettingInputTypeAttribute>(enumItem);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0000FE78 File Offset: 0x0000E078
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
	}
}
