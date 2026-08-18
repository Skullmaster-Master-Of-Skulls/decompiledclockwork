using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005C7 RID: 1479
	public static class OldUserSettingAdapter
	{
		// Token: 0x06002F87 RID: 12167 RVA: 0x00036A88 File Offset: 0x00034C88
		public static int BoolToInt(this bool trueFalse)
		{
			return trueFalse ? 1 : 0;
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x00036AA4 File Offset: 0x00034CA4
		public static bool IntToBool(this int oneZero)
		{
			return oneZero == 1;
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x00036ABC File Offset: 0x00034CBC
		public static List<int> GetSettingValue_ConcatenatedIntList(this List<OldUserSetting> allUserSettings, eSettingCode settingCode, string overrideDefaultValue)
		{
			List<int> list = new List<int>();
			List<OldUserSetting> list2 = allUserSettings.GetUserSettings(settingCode);
			bool flag = list2 == null || list2.Count < 1;
			if (flag)
			{
				string defaultValueString = settingCode.GetDefaultValueString();
				list2 = new List<OldUserSetting>
				{
					new OldUserSetting
					{
						SettingCode = settingCode,
						StringVal = defaultValueString
					}
				};
			}
			foreach (OldUserSetting oldUserSetting in list2)
			{
				string text = oldUserSetting.StringVal ?? "";
				bool flag2 = text.Length > 0;
				if (flag2)
				{
					string[] array = text.Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries);
					foreach (string s in array)
					{
						int item;
						bool flag3 = int.TryParse(s, out item) && !list.Contains(item);
						if (flag3)
						{
							list.Add(item);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x00036BE8 File Offset: 0x00034DE8
		public static List<OldUserSetting> GetUserSettings(this List<OldUserSetting> allUserSettings, eSettingCode settingCode)
		{
			bool flag = allUserSettings == null;
			List<OldUserSetting> result;
			if (flag)
			{
				result = new List<OldUserSetting>();
			}
			else
			{
				List<OldUserSetting> list = (from s in allUserSettings
				where s.SettingCode == settingCode
				select s).ToList<OldUserSetting>();
				bool flag2 = list.Count < 1;
				if (flag2)
				{
					result = list;
				}
				else
				{
					List<OldUserSetting> list2 = (from g in list
					where g.SettingType == eOldUserSettingType.PersonSetting
					select g).ToList<OldUserSetting>();
					bool flag3 = list2.Count > 0;
					if (flag3)
					{
						result = list2;
					}
					else
					{
						list2 = (from g in list
						where g.SettingType == eOldUserSettingType.GroupSetting
						select g).ToList<OldUserSetting>();
						bool flag4 = list2.Count > 0;
						if (flag4)
						{
							result = list2;
						}
						else
						{
							result = list;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x00036CCC File Offset: 0x00034ECC
		public static string GetSettingValue_String(this List<OldUserSetting> allUserSettings, eSettingCode settingCode, bool concatenateValues, string overrideDefaultValue)
		{
			string result;
			if (concatenateValues)
			{
				List<OldUserSetting> userSettings = allUserSettings.GetUserSettings(settingCode);
				bool flag = userSettings.Count < 1;
				if (flag)
				{
					result = (overrideDefaultValue ?? settingCode.GetDefaultValueString());
				}
				else
				{
					List<string> list = new List<string>();
					foreach (OldUserSetting oldUserSetting in userSettings)
					{
						string text = oldUserSetting.StringVal ?? "";
						bool flag2 = text.Length > 0;
						if (flag2)
						{
							string[] array = text.Split(new char[]
							{
								','
							}, StringSplitOptions.RemoveEmptyEntries);
							foreach (string text2 in array)
							{
								string text3 = text2.Trim();
								bool flag3 = text3.Length > 0 && !list.Contains(text3);
								if (flag3)
								{
									list.Add(text3);
								}
							}
						}
					}
					result = string.Join(",", list.ToArray());
				}
			}
			else
			{
				OldUserSetting userSetting = allUserSettings.GetUserSetting(settingCode);
				bool flag4 = userSetting == null;
				if (flag4)
				{
					result = (overrideDefaultValue ?? settingCode.GetDefaultValueString());
				}
				else
				{
					result = (userSetting.StringVal ?? (overrideDefaultValue ?? settingCode.GetDefaultValueString()));
				}
			}
			return result;
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x00036E34 File Offset: 0x00035034
		public static int GetSettingValue_Int(this List<OldUserSetting> allUserSettings, eSettingCode settingCode)
		{
			OldUserSetting userSetting = allUserSettings.GetUserSetting(settingCode);
			bool flag = userSetting == null;
			int result;
			if (flag)
			{
				result = settingCode.GetDefaultValueInt();
			}
			else
			{
				bool flag2 = userSetting.IntVal == 0 && string.IsNullOrEmpty(userSetting.StringVal);
				if (flag2)
				{
					int result2;
					bool flag3 = int.TryParse(userSetting.StringVal, out result2);
					if (flag3)
					{
						return result2;
					}
				}
				result = userSetting.IntVal;
			}
			return result;
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x00036E9C File Offset: 0x0003509C
		public static bool GetSettingValue_Bool(this List<OldUserSetting> allUserSettings, eSettingCode settingCode)
		{
			return allUserSettings.GetSettingValue_Bool(settingCode, null);
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x00036EC0 File Offset: 0x000350C0
		public static bool GetSettingValue_Bool(this List<OldUserSetting> allUserSettings, eSettingCode settingCode, bool? defaultValue)
		{
			OldUserSetting userSetting = allUserSettings.GetUserSetting(settingCode);
			bool flag = userSetting == null;
			int num;
			if (flag)
			{
				num = ((defaultValue != null) ? (defaultValue.Value ? 1 : 0) : settingCode.GetDefaultValueInt());
			}
			else
			{
				num = userSetting.IntVal;
			}
			return num == 1;
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x00036F10 File Offset: 0x00035110
		public static bool? GetSettingValue_BoolWithNullAsDefaultValue(this List<OldUserSetting> allUserSettings, eSettingCode settingCode, bool? defaultValue)
		{
			OldUserSetting userSetting = allUserSettings.GetUserSetting(settingCode);
			bool flag = userSetting == null;
			bool? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int intVal = userSetting.IntVal;
				result = new bool?(intVal == 1);
			}
			return result;
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x00036F54 File Offset: 0x00035154
		public static int GetDefaultValueInt(this eSettingCode settingCode)
		{
			OldUserSettingAttribute attribute = settingCode.GetAttribute<OldUserSettingAttribute>();
			return (attribute != null) ? attribute.DefaultValueInt : 0;
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x00036F80 File Offset: 0x00035180
		public static string GetDefaultValueString(this eSettingCode settingCode)
		{
			OldUserSettingAttribute attribute = settingCode.GetAttribute<OldUserSettingAttribute>();
			return (attribute == null) ? "" : (attribute.DefaultValueString ?? "");
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x00036FB8 File Offset: 0x000351B8
		public static OldUserSetting GetUserSetting(this List<OldUserSetting> allUserSettings, eSettingCode settingCode)
		{
			bool flag = allUserSettings == null || allUserSettings.Count < 1;
			OldUserSetting result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<OldUserSetting> list = (from s in allUserSettings
				where s.SettingCode == settingCode
				select s).ToList<OldUserSetting>();
				bool flag2 = list.Count < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					OldUserSetting oldUserSetting = list.FirstOrDefault((OldUserSetting g) => g.SettingType == eOldUserSettingType.PersonSetting);
					bool flag3 = oldUserSetting != null;
					if (flag3)
					{
						result = oldUserSetting;
					}
					else
					{
						OldUserSetting oldUserSetting2 = list.FirstOrDefault((OldUserSetting g) => g.SettingType == eOldUserSettingType.GroupSetting);
						bool flag4 = oldUserSetting2 != null;
						if (flag4)
						{
							result = oldUserSetting2;
						}
						else
						{
							result = list[0];
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x00037094 File Offset: 0x00035294
		public static string GetOldUserSettingsDefinitionHtml()
		{
			eOldUserSettingGroup[] array = (eOldUserSettingGroup[])Enum.GetValues(typeof(eOldUserSettingGroup));
			eSettingCode[] source = (eSettingCode[])Enum.GetValues(typeof(eSettingCode));
			List<eSettingCode> list = source.ToList<eSettingCode>();
			List<OldUserSettingWithAttribute> source2 = list.ConvertAll<OldUserSettingWithAttribute>((eSettingCode g) => new OldUserSettingWithAttribute(g));
			List<OldUserSettingGroupWithEnums> list2 = new List<OldUserSettingGroupWithEnums>();
			eOldUserSettingGroup[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				eOldUserSettingGroup groupEnum = array2[i];
				OldUserSettingGroupAttribute attribute = WebSettingGroupWithEnums.GetAttribute<OldUserSettingGroupAttribute>(groupEnum);
				bool flag = groupEnum != eOldUserSettingGroup.Unknown && attribute != null && !attribute.IsHidden;
				if (flag)
				{
					list2.Add(new OldUserSettingGroupWithEnums(groupEnum, attribute, (from g in source2
					where g.SettingAttribute != null && !g.SettingAttribute.IsHidden && g.SettingAttribute.Group == groupEnum
					select g).ToList<OldUserSettingWithAttribute>()));
				}
			}
			list2.Sort((OldUserSettingGroupWithEnums g1, OldUserSettingGroupWithEnums g2) => (g1.GroupAttribute.DisplayName ?? "").CompareTo(g2.GroupAttribute.DisplayName ?? ""));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (OldUserSettingGroupWithEnums oldUserSettingGroupWithEnums in list2)
			{
				bool flag2 = oldUserSettingGroupWithEnums.Settings.Count > 0;
				if (flag2)
				{
					string arg = oldUserSettingGroupWithEnums.GroupAttribute.DisplayName ?? "";
					stringBuilder.AppendFormat("<h1>{0}</h1>", arg);
					string text = (oldUserSettingGroupWithEnums.GroupAttribute.Description ?? "").Trim();
					bool flag3 = text.Length > 0;
					if (flag3)
					{
						stringBuilder.AppendFormat("<p><i>{0}</i></p><br />", text);
					}
					foreach (OldUserSettingWithAttribute oldUserSettingWithAttribute in oldUserSettingGroupWithEnums.Settings)
					{
						string arg2 = (oldUserSettingWithAttribute.SettingAttribute.Title ?? "").Trim() + " [" + ((int)oldUserSettingWithAttribute.Setting).ToString() + "]";
						string text2 = oldUserSettingWithAttribute.SettingAttribute.SubGroup ?? "";
						bool flag4 = text2.Length > 0;
						if (flag4)
						{
							bool flag5 = text2.StartsWith("_");
							if (flag5)
							{
								text2 = text2.Substring(1);
							}
							text2 += ": ";
						}
						stringBuilder.AppendFormat("<h3>{0}{1}</h3>", text2, arg2);
						text = (oldUserSettingWithAttribute.SettingAttribute.Description ?? "").Trim();
						bool flag6 = text.Length > 0;
						if (flag6)
						{
							stringBuilder.AppendFormat("<p><i>{0}</i></p><br />", text.Replace("<", "[").Replace(">", "]"));
						}
						else
						{
							stringBuilder.AppendLine("<br />");
						}
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
