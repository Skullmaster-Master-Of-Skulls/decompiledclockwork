using System;
using Microsoft.Win32;

namespace TechnoPro.Common.Win32
{
	// Token: 0x0200000B RID: 11
	public static class WindowsRegistry
	{
		// Token: 0x0600002B RID: 43 RVA: 0x0000359C File Offset: 0x0000179C
		public static RegistryKey GetRegistryKey(RegistryKey StartKey, string[] registryBreakdown, bool CreateKeyIfNotPresent, bool openWritable)
		{
			for (;;)
			{
				RegistryKey registryKey = StartKey;
				int i = 0;
				while (i < registryBreakdown.Length)
				{
					string text = registryBreakdown[i];
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

		// Token: 0x0600002C RID: 44 RVA: 0x000035E8 File Offset: 0x000017E8
		public static string[] GetClockWorkRegistryBreakdown()
		{
			return WindowsRegistry.registryBreakdown;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000035EF File Offset: 0x000017EF
		public static RegistryKey GetRegistryKey(RegistryKey StartKey, bool CreateKeyIfNotPresent, bool openWritable)
		{
			return WindowsRegistry.GetRegistryKey(StartKey, WindowsRegistry.registryBreakdown, CreateKeyIfNotPresent, openWritable);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003600 File Offset: 0x00001800
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
							return DPAPIEncryption.UnProtectData(text, DPAPIEncryption.GetEntropy());
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

		// Token: 0x0600002F RID: 47 RVA: 0x0000365C File Offset: 0x0000185C
		public static string GetRegistryValueString(RegistryKey StartKey, string[] registryBreakdown, string valueName, bool isEncrypted)
		{
			object registryValue = WindowsRegistry.GetRegistryValue(WindowsRegistry.GetRegistryKey(StartKey, registryBreakdown, false, false), valueName, isEncrypted);
			if (registryValue == null)
			{
				return "";
			}
			return registryValue.ToString().Trim();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003690 File Offset: 0x00001890
		public static string GetRegistryValueString(RegistryKey StartKey, string valueName, bool isEncrypted)
		{
			object registryValue = WindowsRegistry.GetRegistryValue(StartKey, valueName, isEncrypted);
			if (registryValue == null)
			{
				return "";
			}
			return registryValue.ToString().Trim();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000036BC File Offset: 0x000018BC
		public static object SetRegistryValue(RegistryKey regKey, string valueName, object valueObject, bool isEncrypted)
		{
			if (regKey != null)
			{
				try
				{
					if (isEncrypted)
					{
						string text = (string)valueObject;
						text = DPAPIEncryption.ProtectData(text, DPAPIEncryption.GetEntropy());
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

		// Token: 0x06000032 RID: 50 RVA: 0x00003710 File Offset: 0x00001910
		public static object SetRegistryValueCurrentUser(string valueName, object valueObject, bool isEncrypted)
		{
			return WindowsRegistry.SetRegistryValue(WindowsRegistry.GetRegistryKey(Registry.CurrentUser, true, true), valueName, valueObject, isEncrypted);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003726 File Offset: 0x00001926
		public static object SetRegistryValueBoolCurrentUser(string valueName, bool valueObject, bool isEncrypted)
		{
			return WindowsRegistry.SetRegistryValue(WindowsRegistry.GetRegistryKey(Registry.CurrentUser, true, true), valueName, valueObject ? "1" : "0", isEncrypted);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000374C File Offset: 0x0000194C
		public static bool GetRegistryValueBoolCurrentUser(string valueName, bool isEncrypted)
		{
			string registryValueString = WindowsRegistry.GetRegistryValueString(WindowsRegistry.GetRegistryKey(Registry.CurrentUser, true, true), valueName, isEncrypted);
			return registryValueString != null && registryValueString.ToString().Trim() == "1";
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003788 File Offset: 0x00001988
		public static int GetRegistryValueIntCurrentUser(string valueName, int defaultReturnValue = 0)
		{
			string registryValueStringCurrentUser = WindowsRegistry.GetRegistryValueStringCurrentUser(valueName, false);
			if (string.IsNullOrEmpty(registryValueStringCurrentUser))
			{
				return defaultReturnValue;
			}
			int result;
			if (!int.TryParse(registryValueStringCurrentUser, out result))
			{
				return defaultReturnValue;
			}
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000037B4 File Offset: 0x000019B4
		public static string GetRegistryValueStringCurrentUser(string valueName, bool isEncrypted)
		{
			return WindowsRegistry.GetRegistryValueString(WindowsRegistry.GetRegistryKey(Registry.CurrentUser, WindowsRegistry.registryBreakdown, false, false), valueName, isEncrypted);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000037D0 File Offset: 0x000019D0
		public static void DeleteRegistryValue(RegistryKey StartKey, string[] RegKeyBreakdown, string valueName)
		{
			RegistryKey registryKey = WindowsRegistry.GetRegistryKey(StartKey, true, true);
			if (registryKey != null)
			{
				registryKey.DeleteValue(valueName, false);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000037F4 File Offset: 0x000019F4
		public static void DeleteRegistryKeyCurrentUser(string valueName)
		{
			try
			{
				RegistryKey registryKey = WindowsRegistry.GetRegistryKey(Registry.CurrentUser, WindowsRegistry.registryBreakdown, true, true);
				if (registryKey != null)
				{
					registryKey.DeleteValue(valueName);
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000020 RID: 32
		private static string[] registryBreakdown = new string[]
		{
			"Software",
			"TechnoPro",
			"ClockWork"
		};
	}
}
