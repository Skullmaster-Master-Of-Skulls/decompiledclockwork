using System;
using EncryptionClassLibrary;
using Microsoft.Win32;

namespace ImportExportClassLibrary
{
	// Token: 0x0200001C RID: 28
	internal class RegistryFunctions
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x0000480C File Offset: 0x0000380C
		public static RegistryKey GetRegistryKey(RegistryKey StartKey, string[] RegKeyBreakdown, bool CreateKeyIfNotPresent, bool openWritable)
		{
			for (;;)
			{
				RegistryKey registryKey = StartKey;
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
						goto IL_2F;
					}
				}
				if (registryKey != null)
				{
					return registryKey;
				}
			}
			IL_2F:
			return null;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004860 File Offset: 0x00003860
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

		// Token: 0x060000C8 RID: 200 RVA: 0x000048BC File Offset: 0x000038BC
		public static string GetRegistryValueString(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName, bool isEncrypted)
		{
			RegistryKey registryKey = RegistryFunctions.GetRegistryKey(StartKey, RegKeyBreakdown, false, false);
			object registryValue = RegistryFunctions.GetRegistryValue(registryKey, valueName, isEncrypted);
			if (registryValue == null)
			{
				return "";
			}
			return registryValue.ToString().Trim();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000048F0 File Offset: 0x000038F0
		public static string GetRegistryValueStringCurrentUser(string valueName, bool isEncrypted)
		{
			RegistryKey currentUser = Registry.CurrentUser;
			string[] regKeyBreakdown = RegistryFunctions.registryBreakdown;
			return RegistryFunctions.GetRegistryValueString(currentUser, regKeyBreakdown, valueName, isEncrypted);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004914 File Offset: 0x00003914
		public static object GetRegistryValue(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName, bool isEncrypted)
		{
			RegistryKey registryKey = RegistryFunctions.GetRegistryKey(StartKey, RegKeyBreakdown, false, false);
			return RegistryFunctions.GetRegistryValue(registryKey, valueName, isEncrypted);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004934 File Offset: 0x00003934
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

		// Token: 0x060000CC RID: 204 RVA: 0x00004988 File Offset: 0x00003988
		public static object SetRegistryValue(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName, object valueObject, bool isEncrypted)
		{
			RegistryKey registryKey = RegistryFunctions.GetRegistryKey(StartKey, RegKeyBreakdown, true, true);
			return RegistryFunctions.SetRegistryValue(registryKey, valueName, valueObject, isEncrypted);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000049AB File Offset: 0x000039AB
		public static object SetRegistryValueCurrentUser(string valueName, object valueObject, bool isEncrypted)
		{
			return RegistryFunctions.SetRegistryValue(Registry.CurrentUser, RegistryFunctions.registryBreakdown, valueName, valueObject, isEncrypted);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000049C0 File Offset: 0x000039C0
		public static void DeleteRegistryValue(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName)
		{
			RegistryKey registryKey = RegistryFunctions.GetRegistryKey(StartKey, RegKeyBreakdown, false, true);
			if (registryKey != null)
			{
				registryKey.DeleteValue(valueName, false);
			}
		}

		// Token: 0x04000028 RID: 40
		public static string[] registryBreakdown = new string[]
		{
			"Software",
			"TechnoPro",
			"ClockWork"
		};
	}
}
