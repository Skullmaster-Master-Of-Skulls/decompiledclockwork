using System;
using EncryptionClassLibrary;
using Microsoft.Win32;

namespace AutoComboBox
{
	// Token: 0x020000EF RID: 239
	public class Utility
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x0004A620 File Offset: 0x00049620
		public static RegistryKey GetRegistryKey(RegistryKey StartKey, string[] RegKeyBreakdown, bool CreateKeyIfNotPresent, bool openWritable)
		{
			RegistryKey registryKey;
			for (;;)
			{
				registryKey = StartKey;
				int i = 0;
				while (i < RegKeyBreakdown.Length)
				{
					string text = RegKeyBreakdown[i];
					RegistryKey registryKey2 = registryKey.OpenSubKey(text, openWritable);
					if (registryKey2 != null)
					{
						registryKey = registryKey2;
						i++;
					}
					else
					{
						if (CreateKeyIfNotPresent)
						{
							registryKey2 = registryKey.CreateSubKey(text);
							registryKey = null;
							break;
						}
						goto IL_44;
					}
				}
				if (registryKey != null)
				{
					goto Block_3;
				}
			}
			IL_44:
			return null;
			Block_3:
			return registryKey;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0004A6A4 File Offset: 0x000496A4
		public static object GetRegistryValue(RegistryKey regKey, string valueName, bool isEncrypted)
		{
			if (regKey != null)
			{
				try
				{
					object value = regKey.GetValue(valueName);
					if (value != null && isEncrypted)
					{
						string text = (string)value;
						if (text.Length > 0)
						{
							return DPAPIencryption.UnProtectData(text, DPAPIencryption.GetEntropy());
						}
					}
					return value;
				}
				catch (Exception result)
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0004A724 File Offset: 0x00049724
		public static string GetRegistryValueString(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName, bool isEncrypted)
		{
			RegistryKey registryKey = Utility.GetRegistryKey(StartKey, RegKeyBreakdown, false, false);
			object registryValue = Utility.GetRegistryValue(registryKey, valueName, isEncrypted);
			string result;
			if (registryValue == null)
			{
				result = "";
			}
			else
			{
				result = registryValue.ToString().Trim();
			}
			return result;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0004A768 File Offset: 0x00049768
		public static string GetRegistryValueStringCurrentUser(string valueName, bool isEncrypted)
		{
			RegistryKey currentUser = Registry.CurrentUser;
			string[] regKeyBreakdown = Utility.registryBreakdown;
			return Utility.GetRegistryValueString(currentUser, regKeyBreakdown, valueName, isEncrypted);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0004A790 File Offset: 0x00049790
		public static object GetRegistryValue(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName, bool isEncrypted)
		{
			RegistryKey registryKey = Utility.GetRegistryKey(StartKey, RegKeyBreakdown, false, false);
			return Utility.GetRegistryValue(registryKey, valueName, isEncrypted);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0004A7B4 File Offset: 0x000497B4
		public static object SetRegistryValue(RegistryKey regKey, string valueName, object valueObject, bool isEncrypted)
		{
			if (regKey != null)
			{
				try
				{
					if (isEncrypted)
					{
						string text = (string)valueObject;
						text = DPAPIencryption.ProtectData(text, DPAPIencryption.GetEntropy());
						regKey.SetValue(valueName, text);
					}
					else
					{
						regKey.SetValue(valueName, valueObject);
					}
					return valueObject;
				}
				catch (Exception result)
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0004A820 File Offset: 0x00049820
		public static object SetRegistryValue(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName, object valueObject, bool isEncrypted)
		{
			RegistryKey registryKey = Utility.GetRegistryKey(StartKey, RegKeyBreakdown, true, true);
			return Utility.SetRegistryValue(registryKey, valueName, valueObject, isEncrypted);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0004A848 File Offset: 0x00049848
		public static object SetRegistryValueCurrentUser(string valueName, object valueObject, bool isEncrypted)
		{
			return Utility.SetRegistryValue(Registry.CurrentUser, Utility.registryBreakdown, valueName, valueObject, isEncrypted);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0004A86C File Offset: 0x0004986C
		public static void DeleteRegistryValue(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName)
		{
			RegistryKey registryKey = Utility.GetRegistryKey(StartKey, RegKeyBreakdown, false, true);
			if (registryKey != null)
			{
				registryKey.DeleteValue(valueName, false);
			}
		}

		// Token: 0x040006DC RID: 1756
		public static string[] registryBreakdown = new string[]
		{
			"Software",
			"TechnoPro",
			"ClockWork"
		};
	}
}
