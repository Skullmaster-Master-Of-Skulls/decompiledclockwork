using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Diagnostics;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000257 RID: 599
	internal class RegistryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600114F RID: 4431 RVA: 0x0003F5D8 File Offset: 0x0003D7D8
		private static RegistryHandle GetHKCR()
		{
			RegistryHandle registryHandle = null;
			int num = SafeNativeMethods.RegOpenKeyEx(RegistryHandle.HKEY_LOCAL_MACHINE, "Software\\Classes", 0, 131097, out registryHandle);
			if (num != 0)
			{
				Utility.CloseInvalidOutSafeHandle(registryHandle);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			if (registryHandle == null || registryHandle.IsInvalid)
			{
				Utility.CloseInvalidOutSafeHandle(registryHandle);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(6));
			}
			return registryHandle;
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0003F63C File Offset: 0x0003D83C
		private static RegistryHandle Get64bitHKCR()
		{
			RegistryHandle registryHandle = null;
			int num = SafeNativeMethods.RegOpenKeyEx(RegistryHandle.HKEY_LOCAL_MACHINE, "Software\\Classes", 0, 131353, out registryHandle);
			if (num != 0)
			{
				Utility.CloseInvalidOutSafeHandle(registryHandle);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			if (registryHandle == null || registryHandle.IsInvalid)
			{
				Utility.CloseInvalidOutSafeHandle(registryHandle);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(6));
			}
			return registryHandle;
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0003F6A0 File Offset: 0x0003D8A0
		private static RegistryHandle Get32bitHKCR()
		{
			RegistryHandle registryHandle = null;
			int num = SafeNativeMethods.RegOpenKeyEx(RegistryHandle.HKEY_LOCAL_MACHINE, "Software\\Classes", 0, 131609, out registryHandle);
			if (num != 0)
			{
				Utility.CloseInvalidOutSafeHandle(registryHandle);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			if (registryHandle == null || registryHandle.IsInvalid)
			{
				Utility.CloseInvalidOutSafeHandle(registryHandle);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(6));
			}
			return registryHandle;
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0003F704 File Offset: 0x0003D904
		private static RegistryHandle GetCorrectBitnessHive(bool is64bit)
		{
			if (is64bit && IntPtr.Size == 8)
			{
				return RegistryHandle.GetHKCR();
			}
			if (is64bit && IntPtr.Size == 4)
			{
				return RegistryHandle.Get64bitHKCR();
			}
			if (!is64bit && IntPtr.Size == 8)
			{
				return RegistryHandle.Get32bitHKCR();
			}
			if (!is64bit && IntPtr.Size == 4)
			{
				return RegistryHandle.GetHKCR();
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(50));
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0003F766 File Offset: 0x0003D966
		public static RegistryHandle GetBitnessHKCR(bool is64bit)
		{
			return RegistryHandle.GetCorrectBitnessHive(is64bit);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0003F770 File Offset: 0x0003D970
		public static RegistryHandle GetCorrectBitnessHKLMSubkey(bool is64bit, string key)
		{
			if (is64bit && IntPtr.Size == 8)
			{
				return RegistryHandle.GetHKLMSubkey(key);
			}
			if (is64bit && IntPtr.Size == 4)
			{
				return RegistryHandle.Get64bitHKLMSubkey(key);
			}
			if (!is64bit && IntPtr.Size == 8)
			{
				return RegistryHandle.Get32bitHKLMSubkey(key);
			}
			if (!is64bit && IntPtr.Size == 4)
			{
				return RegistryHandle.GetHKLMSubkey(key);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(50));
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0003F7D8 File Offset: 0x0003D9D8
		private static RegistryHandle GetHKLMSubkey(string key)
		{
			RegistryHandle registryHandle = null;
			int num = SafeNativeMethods.RegOpenKeyEx(RegistryHandle.HKEY_LOCAL_MACHINE, key, 0, 131097, out registryHandle);
			if (num != 0)
			{
				Utility.CloseInvalidOutSafeHandle(registryHandle);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			if (registryHandle == null || registryHandle.IsInvalid)
			{
				Utility.CloseInvalidOutSafeHandle(registryHandle);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(6));
			}
			return registryHandle;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0003F838 File Offset: 0x0003DA38
		private static RegistryHandle Get64bitHKLMSubkey(string key)
		{
			RegistryHandle registryHandle = null;
			int num = SafeNativeMethods.RegOpenKeyEx(RegistryHandle.HKEY_LOCAL_MACHINE, key, 0, 131353, out registryHandle);
			if (num != 0)
			{
				Utility.CloseInvalidOutSafeHandle(registryHandle);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			if (registryHandle == null || registryHandle.IsInvalid)
			{
				Utility.CloseInvalidOutSafeHandle(registryHandle);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(6));
			}
			return registryHandle;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0003F898 File Offset: 0x0003DA98
		private static RegistryHandle Get32bitHKLMSubkey(string key)
		{
			RegistryHandle registryHandle = null;
			int num = SafeNativeMethods.RegOpenKeyEx(RegistryHandle.HKEY_LOCAL_MACHINE, key, 0, 131609, out registryHandle);
			if (num != 0)
			{
				Utility.CloseInvalidOutSafeHandle(registryHandle);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
			if (registryHandle == null || registryHandle.IsInvalid)
			{
				Utility.CloseInvalidOutSafeHandle(registryHandle);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(6));
			}
			return registryHandle;
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0003F8F8 File Offset: 0x0003DAF8
		internal static RegistryHandle GetNativeHKLMSubkey(string subKey, bool writeable)
		{
			RegistryHandle registryHandle = null;
			int num = 131353;
			if (writeable)
			{
				num |= 131078;
			}
			if (SafeNativeMethods.RegOpenKeyEx(RegistryHandle.HKEY_LOCAL_MACHINE, subKey, 0, num, out registryHandle) != 0 || registryHandle == null || registryHandle.IsInvalid)
			{
				Utility.CloseInvalidOutSafeHandle(registryHandle);
				return null;
			}
			return registryHandle;
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0003F93F File Offset: 0x0003DB3F
		public RegistryHandle(IntPtr hKey, bool ownHandle) : base(ownHandle)
		{
			this.handle = hKey;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0003F94F File Offset: 0x0003DB4F
		public RegistryHandle() : base(true)
		{
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0003F958 File Offset: 0x0003DB58
		public bool DeleteKey(string key)
		{
			return SafeNativeMethods.RegDeleteKey(this, key) == 0;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0003F974 File Offset: 0x0003DB74
		public void SetValue(string valName, string value)
		{
			int num = SafeNativeMethods.RegSetValueEx(this, valName, 0, 1, value, value.Length * 2 + 2);
			if (num != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num));
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0003F9AC File Offset: 0x0003DBAC
		public RegistryHandle OpenSubKey(string subkey)
		{
			RegistryHandle registryHandle = null;
			if (SafeNativeMethods.RegOpenKeyEx(this, subkey, 0, 131097, out registryHandle) != 0 || registryHandle == null || registryHandle.IsInvalid)
			{
				Utility.CloseInvalidOutSafeHandle(registryHandle);
				return null;
			}
			return registryHandle;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0003F9E4 File Offset: 0x0003DBE4
		public string GetStringValue(string valName)
		{
			int num = 0;
			int num2 = 0;
			if (SafeNativeMethods.RegQueryValueEx(this, valName, null, ref num, null, ref num2) == 0 && num == 1)
			{
				byte[] array = new byte[num2];
				int num3 = SafeNativeMethods.RegQueryValueEx(this, valName, null, ref num, array, ref num2);
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				return unicodeEncoding.GetString(array);
			}
			return null;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0003FA30 File Offset: 0x0003DC30
		public StringCollection GetSubKeyNames()
		{
			int num = 0;
			StringCollection stringCollection = new StringCollection();
			int num3;
			do
			{
				int num2 = 0;
				num3 = SafeNativeMethods.RegEnumKey(this, num, null, ref num2);
				if (num3 == 234)
				{
					StringBuilder stringBuilder = new StringBuilder(num2 + 1);
					num3 = SafeNativeMethods.RegEnumKey(this, num, stringBuilder, ref num2);
					if (num3 == 0)
					{
						stringCollection.Add(stringBuilder.ToString());
					}
				}
				num++;
			}
			while (num3 == 0);
			return stringCollection;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0003FA8C File Offset: 0x0003DC8C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		internal unsafe object GetValue(string valName)
		{
			object result = null;
			int num = 0;
			int num2 = 0;
			if (SafeNativeMethods.RegQueryValueEx(this, valName, null, ref num, null, ref num2) == 0)
			{
				byte[] array = new byte[num2];
				if (SafeNativeMethods.RegQueryValueEx(this, valName, null, ref num, array, ref num2) == 0)
				{
					UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
					string @string = unicodeEncoding.GetString(array);
					switch (num)
					{
					case 1:
					case 2:
						return @string.Trim(new char[1]);
					case 3:
						return array;
					case 4:
					{
						byte[] array2;
						byte* value;
						if ((array2 = array) == null || array2.Length == 0)
						{
							value = null;
						}
						else
						{
							value = &array2[0];
						}
						result = Marshal.ReadInt32((IntPtr)((void*)value));
						array2 = null;
						return result;
					}
					case 7:
						return @string.Split(new char[1], StringSplitOptions.RemoveEmptyEntries);
					case 11:
					{
						byte[] array2;
						byte* value2;
						if ((array2 = array) == null || array2.Length == 0)
						{
							value2 = null;
						}
						else
						{
							value2 = &array2[0];
						}
						result = Marshal.ReadInt64((IntPtr)((void*)value2));
						array2 = null;
						return result;
					}
					}
					result = array;
				}
			}
			return result;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0003FBB2 File Offset: 0x0003DDB2
		protected override bool ReleaseHandle()
		{
			return SafeNativeMethods.RegCloseKey(this.handle) == 0;
		}

		// Token: 0x04001964 RID: 6500
		internal static readonly RegistryHandle HKEY_CLASSES_ROOT = new RegistryHandle(new IntPtr(int.MinValue), false);

		// Token: 0x04001965 RID: 6501
		internal static readonly RegistryHandle HKEY_CURRENT_USER = new RegistryHandle(new IntPtr(-2147483647), false);

		// Token: 0x04001966 RID: 6502
		internal static readonly RegistryHandle HKEY_LOCAL_MACHINE = new RegistryHandle(new IntPtr(-2147483646), false);

		// Token: 0x04001967 RID: 6503
		internal static readonly RegistryHandle HKEY_USERS = new RegistryHandle(new IntPtr(-2147483645), false);

		// Token: 0x04001968 RID: 6504
		internal static readonly RegistryHandle HKEY_PERFORMANCE_DATA = new RegistryHandle(new IntPtr(-2147483644), false);

		// Token: 0x04001969 RID: 6505
		internal static readonly RegistryHandle HKEY_CURRENT_CONFIG = new RegistryHandle(new IntPtr(-2147483643), false);

		// Token: 0x0400196A RID: 6506
		internal static readonly RegistryHandle HKEY_DYN_DATA = new RegistryHandle(new IntPtr(-2147483642), false);
	}
}
