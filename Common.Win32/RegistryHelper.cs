using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using EncryptionClassLibrary;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace TechnoPro.Common.Win32
{
	// Token: 0x0200000C RID: 12
	public class RegistryHelper
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000023EF File Offset: 0x000005EF
		public RegistryHelper()
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003859 File Offset: 0x00001A59
		public RegistryHelper(params string[] subKeyPath)
		{
			this._startSubKeyPath = subKeyPath;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003868 File Offset: 0x00001A68
		public RegistryKey StartLocalMachineRegistryKey
		{
			get
			{
				RegistryKey registryKey = Registry.LocalMachine;
				foreach (string subkey in this._startSubKeyPath ?? RegistryHelper.TechnoproSubKeyPath)
				{
					if (registryKey != null)
					{
						registryKey = registryKey.CreateSubKey(subkey);
					}
				}
				return registryKey;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000038AC File Offset: 0x00001AAC
		public RegistryKey StartCurrentUserRegistryKey
		{
			get
			{
				RegistryKey registryKey = Registry.CurrentUser;
				foreach (string subkey in this._startSubKeyPath ?? RegistryHelper.TechnoproSubKeyPath)
				{
					if (registryKey != null)
					{
						registryKey = registryKey.CreateSubKey(subkey);
					}
				}
				return registryKey;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000038F0 File Offset: 0x00001AF0
		public void GetConnectionStringAndDatabasePassword(eRegWow64Options wow64Options, string regName, out string connectionString, out string databasePassword)
		{
			string text = regName + "_cs";
			string text2 = regName + "_k";
			string s = this.ReadLocalMachineRegistry<string>(wow64Options, new string[]
			{
				"ClockWork",
				"mc",
				text
			});
			string s2 = this.ReadLocalMachineRegistry<string>(wow64Options, new string[]
			{
				"ClockWork",
				"mc",
				text2
			});
			byte[] encData = Convert.FromBase64String(s);
			byte[] encData2 = Convert.FromBase64String(s2);
			string text3 = DPAPIEncryptionV2.ByteArrayToString(DPAPIEncryptionV2.UnProtectData(encData, ProtectionScope.LocalMachine));
			string text4 = DPAPIEncryptionV2.ByteArrayToString(DPAPIEncryptionV2.UnProtectData(encData2, ProtectionScope.LocalMachine));
			if (string.IsNullOrEmpty(text3))
			{
				databasePassword = "";
				connectionString = "";
				throw new Exception("Missing connection string from local machine registry");
			}
			if (string.IsNullOrEmpty(text4))
			{
				databasePassword = "";
				connectionString = "";
				throw new Exception("Missing database password from local machine registry");
			}
			connectionString = text3;
			databasePassword = text4;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000039D0 File Offset: 0x00001BD0
		public void DeleteLocalMachineRegistry(params string[] keypath)
		{
			RegistryKey startLocalMachineRegistryKey = this.StartLocalMachineRegistryKey;
			this.DeleteRegistry(startLocalMachineRegistryKey, keypath);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000039EC File Offset: 0x00001BEC
		public void DeleteCurrentUserRegistry(params string[] keypath)
		{
			RegistryKey startCurrentUserRegistryKey = this.StartCurrentUserRegistryKey;
			this.DeleteRegistry(startCurrentUserRegistryKey, keypath);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003A08 File Offset: 0x00001C08
		public void DeleteLocalMachineRegistry(eRegWow64Options wow64Options, params string[] keypath)
		{
			RegistryKey startLocalMachineRegistryKey = this.GetStartLocalMachineRegistryKey(wow64Options, true);
			this.DeleteRegistry(startLocalMachineRegistryKey, keypath);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003A28 File Offset: 0x00001C28
		public void DeleteCurrentUserRegistry(eRegWow64Options wow64Options, params string[] keypath)
		{
			RegistryKey startCurrentUserRegistryKey = this.GetStartCurrentUserRegistryKey(wow64Options, true);
			this.DeleteRegistry(startCurrentUserRegistryKey, keypath);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003A48 File Offset: 0x00001C48
		public T ReadLocalMachineRegistry<T>(params string[] keypath)
		{
			RegistryKey startLocalMachineRegistryKey = this.StartLocalMachineRegistryKey;
			return this.ReadRegistry<T>(startLocalMachineRegistryKey, keypath);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003A64 File Offset: 0x00001C64
		public T ReadLocalMachineRegistry<T>(eRegWow64Options wow64Options, params string[] keypath)
		{
			RegistryKey startLocalMachineRegistryKey = this.GetStartLocalMachineRegistryKey(wow64Options, false);
			return this.ReadRegistry<T>(startLocalMachineRegistryKey, keypath);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003A84 File Offset: 0x00001C84
		public void WriteLocalMachineRegistry<T>(T value, params string[] keypath)
		{
			RegistryKey startLocalMachineRegistryKey = this.StartLocalMachineRegistryKey;
			this.WriteRegistry<T>(startLocalMachineRegistryKey, value, keypath);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003AA4 File Offset: 0x00001CA4
		public void WriteLocalMachineRegistry<T>(eRegWow64Options wow64Options, T value, params string[] keypath)
		{
			RegistryKey startLocalMachineRegistryKey = this.GetStartLocalMachineRegistryKey(wow64Options, true);
			this.WriteRegistry<T>(startLocalMachineRegistryKey, value, keypath);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003AC4 File Offset: 0x00001CC4
		public T ReadCurrentUserRegistry<T>(params string[] keypath)
		{
			RegistryKey startCurrentUserRegistryKey = this.StartCurrentUserRegistryKey;
			return this.ReadRegistry<T>(startCurrentUserRegistryKey, keypath);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public T ReadCurrentUserRegistry<T>(eRegWow64Options wow64Options, params string[] keypath)
		{
			RegistryKey startCurrentUserRegistryKey = this.GetStartCurrentUserRegistryKey(wow64Options, false);
			return this.ReadRegistry<T>(startCurrentUserRegistryKey, keypath);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003B00 File Offset: 0x00001D00
		public void WriteCurrentUserRegistry<T>(T value, params string[] keypath)
		{
			RegistryKey startCurrentUserRegistryKey = this.StartCurrentUserRegistryKey;
			this.WriteRegistry<T>(startCurrentUserRegistryKey, value, keypath);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003B20 File Offset: 0x00001D20
		public void WriteCurrentUserRegistry<T>(eRegWow64Options wow64Options, T value, params string[] keypath)
		{
			RegistryKey startCurrentUserRegistryKey = this.GetStartCurrentUserRegistryKey(wow64Options, true);
			this.WriteRegistry<T>(startCurrentUserRegistryKey, value, keypath);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003B40 File Offset: 0x00001D40
		public string[] GetLocalMachineSubKeyNames(params string[] keypath)
		{
			RegistryKey startLocalMachineRegistryKey = this.StartLocalMachineRegistryKey;
			return this.GetSubKeyNames(startLocalMachineRegistryKey, keypath);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003B5C File Offset: 0x00001D5C
		public string[] GetLocalMachineSubKeyValueNames(params string[] keypath)
		{
			RegistryKey startLocalMachineRegistryKey = this.StartLocalMachineRegistryKey;
			return this.GetSubKeyValueNames(startLocalMachineRegistryKey, keypath);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003B78 File Offset: 0x00001D78
		public string[] GetCurrentUserSubKeyNames(params string[] keypath)
		{
			RegistryKey startCurrentUserRegistryKey = this.StartCurrentUserRegistryKey;
			return this.GetSubKeyNames(startCurrentUserRegistryKey, keypath);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003B94 File Offset: 0x00001D94
		public string[] GetCurrentUserSubKeyValueNames(params string[] keypath)
		{
			RegistryKey startCurrentUserRegistryKey = this.StartCurrentUserRegistryKey;
			return this.GetSubKeyValueNames(startCurrentUserRegistryKey, keypath);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public string[] GetLocalMachineSubKeyNames(eRegWow64Options wow64Options, params string[] keypath)
		{
			RegistryKey startLocalMachineRegistryKey = this.GetStartLocalMachineRegistryKey(wow64Options, false);
			return this.GetSubKeyNames(startLocalMachineRegistryKey, keypath);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public string[] GetLocalMachineSubKeyValueNames(eRegWow64Options wow64Options, params string[] keypath)
		{
			RegistryKey startLocalMachineRegistryKey = this.GetStartLocalMachineRegistryKey(wow64Options, false);
			return this.GetSubKeyValueNames(startLocalMachineRegistryKey, keypath);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public string[] GetCurrentUserSubKeyNames(eRegWow64Options wow64Options, params string[] keypath)
		{
			RegistryKey startCurrentUserRegistryKey = this.GetStartCurrentUserRegistryKey(wow64Options, false);
			return this.GetSubKeyNames(startCurrentUserRegistryKey, keypath);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003C10 File Offset: 0x00001E10
		public string[] GetCurrentUserSubKeyValueNames(eRegWow64Options wow64Options, params string[] keypath)
		{
			RegistryKey startCurrentUserRegistryKey = this.GetStartCurrentUserRegistryKey(wow64Options, false);
			return this.GetSubKeyValueNames(startCurrentUserRegistryKey, keypath);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003C30 File Offset: 0x00001E30
		public RegistryKey GetStartLocalMachineRegistryKey(eRegWow64Options wow64Options, bool pWritetable = false)
		{
			RegistryKey registryKey = Registry.LocalMachine;
			foreach (string pSubKeyName in this._startSubKeyPath ?? RegistryHelper.TechnoproSubKeyPath)
			{
				if (registryKey != null)
				{
					registryKey = RegistryHelper.OpenSubKey(registryKey, pSubKeyName, pWritetable, wow64Options);
				}
			}
			return registryKey;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003C74 File Offset: 0x00001E74
		public RegistryKey GetStartCurrentUserRegistryKey(eRegWow64Options wow64Options, bool pWritetable = false)
		{
			RegistryKey registryKey = Registry.CurrentUser;
			foreach (string pSubKeyName in this._startSubKeyPath ?? RegistryHelper.TechnoproSubKeyPath)
			{
				if (registryKey != null)
				{
					registryKey = RegistryHelper.OpenSubKey(registryKey, pSubKeyName, pWritetable, wow64Options);
				}
			}
			return registryKey;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003CB8 File Offset: 0x00001EB8
		private T ReadRegistry<T>(RegistryKey regKey, params string[] keypath)
		{
			for (int i = 0; i < keypath.Length - 1; i++)
			{
				regKey = regKey.OpenSubKey(keypath[i]);
				if (regKey == null)
				{
					return default(T);
				}
			}
			if (!regKey.GetValueNames().Contains(keypath.Last<string>()))
			{
				return default(T);
			}
			return (T)((object)regKey.GetValue(keypath.Last<string>()));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003D1C File Offset: 0x00001F1C
		private void WriteRegistry<T>(RegistryKey regKey, T value, params string[] keypath)
		{
			for (int i = 0; i < keypath.Length - 1; i++)
			{
				regKey = regKey.CreateSubKey(keypath[i]);
			}
			regKey.SetValue(keypath.Last<string>(), value);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003D58 File Offset: 0x00001F58
		private string[] GetSubKeyNames(RegistryKey regKey, params string[] keypath)
		{
			foreach (string name in keypath)
			{
				regKey = regKey.OpenSubKey(name);
				if (regKey == null)
				{
					return null;
				}
			}
			return regKey.GetSubKeyNames();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003D90 File Offset: 0x00001F90
		private string[] GetSubKeyValueNames(RegistryKey regKey, params string[] keypath)
		{
			foreach (string name in keypath)
			{
				regKey = regKey.OpenSubKey(name);
				if (regKey == null)
				{
					return null;
				}
			}
			return regKey.GetValueNames();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003DC8 File Offset: 0x00001FC8
		private void DeleteRegistry(RegistryKey regKey, params string[] keypath)
		{
			for (int i = 0; i < keypath.Length - 1; i++)
			{
				regKey = regKey.CreateSubKey(keypath[i]);
			}
			regKey.DeleteValue(keypath.Last<string>());
		}

		// Token: 0x0600005A RID: 90
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegOpenKeyEx(IntPtr hKey, string subKey, uint options, int sam, out IntPtr phkResult);

		// Token: 0x0600005B RID: 91 RVA: 0x00003DFC File Offset: 0x00001FFC
		private static RegistryKey OpenSubKey(RegistryKey pParentKey, string pSubKeyName, bool pWriteable, eRegWow64Options pOptions)
		{
			if (pParentKey == null || RegistryHelper.GetRegistryKeyHandle(pParentKey).Equals(IntPtr.Zero))
			{
				throw new Exception("OpenSubKey: Parent key is not open");
			}
			eRegistryRights eRegistryRights = eRegistryRights.ReadKey;
			if (pWriteable)
			{
				eRegistryRights = eRegistryRights.WriteKey;
			}
			IntPtr hKey;
			if (RegistryHelper.RegOpenKeyEx(RegistryHelper.GetRegistryKeyHandle(pParentKey), pSubKeyName, 0U, (int)(eRegistryRights | (eRegistryRights)pOptions), out hKey) != 0)
			{
				Win32Exception innerException = new Win32Exception();
				throw new Exception("OpenSubKey: Exception encountered opening key", innerException);
			}
			return RegistryHelper.PointerToRegistryKey(hKey, pWriteable, false);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003E6E File Offset: 0x0000206E
		private static IntPtr GetRegistryKeyHandle(RegistryKey pRegisteryKey)
		{
			SafeHandle safeHandle = (SafeHandle)Type.GetType("Microsoft.Win32.RegistryKey").GetField("hkey", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(pRegisteryKey);
			safeHandle.DangerousGetHandle();
			return safeHandle.DangerousGetHandle();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003EA0 File Offset: 0x000020A0
		private static RegistryKey PointerToRegistryKey(IntPtr hKey, bool pWritable, bool pOwnsHandle)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
			Type type = typeof(SafeHandleZeroOrMinusOneIsInvalid).Assembly.GetType("Microsoft.Win32.SafeHandles.SafeRegistryHandle");
			Type[] types = new Type[]
			{
				typeof(IntPtr),
				typeof(bool)
			};
			return RegistryKey.FromHandle((SafeRegistryHandle)type.GetConstructor(bindingAttr, null, types, null).Invoke(new object[]
			{
				hKey,
				pOwnsHandle
			}));
		}

		// Token: 0x04000021 RID: 33
		public static string[] TechnoproSubKeyPath = new string[]
		{
			"Software",
			"TechnoPro"
		};

		// Token: 0x04000022 RID: 34
		private readonly string[] _startSubKeyPath;
	}
}
