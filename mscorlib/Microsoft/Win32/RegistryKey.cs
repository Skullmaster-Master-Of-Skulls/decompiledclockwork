using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x02000474 RID: 1140
	[ComVisible(true)]
	public sealed class RegistryKey : MarshalByRefObject, IDisposable
	{
		// Token: 0x06002D56 RID: 11606 RVA: 0x000974BA File Offset: 0x000964BA
		private RegistryKey(SafeRegistryHandle hkey, bool writable) : this(hkey, writable, false, false, false)
		{
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000974C8 File Offset: 0x000964C8
		private RegistryKey(SafeRegistryHandle hkey, bool writable, bool systemkey, bool remoteKey, bool isPerfData)
		{
			this.hkey = hkey;
			this.keyName = "";
			this.remoteKey = remoteKey;
			if (systemkey)
			{
				this.state |= 2;
			}
			if (writable)
			{
				this.state |= 4;
			}
			if (isPerfData)
			{
				this.state |= 8;
			}
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x00097529 File Offset: 0x00096529
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x00097534 File Offset: 0x00096534
		private void Dispose(bool disposing)
		{
			if (this.hkey != null)
			{
				bool flag = this.IsPerfDataKey();
				if (this.IsSystemKey())
				{
					if (!flag)
					{
						return;
					}
				}
				try
				{
					this.hkey.Dispose();
				}
				catch (IOException)
				{
				}
				if (flag)
				{
					this.hkey = new SafeRegistryHandle(RegistryKey.HKEY_PERFORMANCE_DATA, !RegistryKey.IsWin9x());
					return;
				}
				this.hkey = null;
			}
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000975A0 File Offset: 0x000965A0
		public void Flush()
		{
			if (this.hkey != null && this.IsDirty())
			{
				Win32Native.RegFlushKey(this.hkey);
			}
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000975BE File Offset: 0x000965BE
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000975C7 File Offset: 0x000965C7
		public RegistryKey CreateSubKey(string subkey)
		{
			return this.CreateSubKey(subkey, this.checkMode);
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000975D6 File Offset: 0x000965D6
		[ComVisible(false)]
		public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck)
		{
			return this.CreateSubKey(subkey, permissionCheck, null);
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000975E4 File Offset: 0x000965E4
		[ComVisible(false)]
		public unsafe RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistrySecurity registrySecurity)
		{
			RegistryKey.ValidateKeyName(subkey);
			RegistryKey.ValidateKeyMode(permissionCheck);
			this.EnsureWriteable();
			subkey = RegistryKey.FixupName(subkey);
			if (!this.remoteKey)
			{
				RegistryKey registryKey = this.InternalOpenSubKey(subkey, permissionCheck != RegistryKeyPermissionCheck.ReadSubTree);
				if (registryKey != null)
				{
					this.CheckSubKeyWritePermission(subkey);
					this.CheckSubTreePermission(subkey, permissionCheck);
					registryKey.checkMode = permissionCheck;
					return registryKey;
				}
			}
			this.CheckSubKeyCreatePermission(subkey);
			Win32Native.SECURITY_ATTRIBUTES security_ATTRIBUTES = null;
			if (registrySecurity != null)
			{
				security_ATTRIBUTES = new Win32Native.SECURITY_ATTRIBUTES();
				security_ATTRIBUTES.nLength = Marshal.SizeOf(security_ATTRIBUTES);
				byte[] securityDescriptorBinaryForm = registrySecurity.GetSecurityDescriptorBinaryForm();
				byte* ptr = stackalloc byte[1 * securityDescriptorBinaryForm.Length];
				Buffer.memcpy(securityDescriptorBinaryForm, 0, ptr, 0, securityDescriptorBinaryForm.Length);
				security_ATTRIBUTES.pSecurityDescriptor = ptr;
			}
			int num = 0;
			SafeRegistryHandle safeRegistryHandle = null;
			int num2 = Win32Native.RegCreateKeyEx(this.hkey, subkey, 0, null, 0, RegistryKey.GetRegistryKeyAccess(permissionCheck != RegistryKeyPermissionCheck.ReadSubTree), security_ATTRIBUTES, out safeRegistryHandle, out num);
			if (num2 == 0 && !safeRegistryHandle.IsInvalid)
			{
				RegistryKey registryKey2 = new RegistryKey(safeRegistryHandle, permissionCheck != RegistryKeyPermissionCheck.ReadSubTree, false, this.remoteKey, false);
				this.CheckSubTreePermission(subkey, permissionCheck);
				registryKey2.checkMode = permissionCheck;
				if (subkey.Length == 0)
				{
					registryKey2.keyName = this.keyName;
				}
				else
				{
					registryKey2.keyName = this.keyName + "\\" + subkey;
				}
				return registryKey2;
			}
			if (num2 != 0)
			{
				this.Win32Error(num2, this.keyName + "\\" + subkey);
			}
			return null;
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x0009772A File Offset: 0x0009672A
		public void DeleteSubKey(string subkey)
		{
			this.DeleteSubKey(subkey, true);
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x00097734 File Offset: 0x00096734
		public void DeleteSubKey(string subkey, bool throwOnMissingSubKey)
		{
			RegistryKey.ValidateKeyName(subkey);
			this.EnsureWriteable();
			subkey = RegistryKey.FixupName(subkey);
			this.CheckSubKeyWritePermission(subkey);
			RegistryKey registryKey = this.InternalOpenSubKey(subkey, false);
			if (registryKey != null)
			{
				try
				{
					if (registryKey.InternalSubKeyCount() > 0)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_RegRemoveSubKey);
					}
				}
				finally
				{
					registryKey.Close();
				}
				int num = Win32Native.RegDeleteKey(this.hkey, subkey);
				if (num != 0)
				{
					if (num != 2)
					{
						this.Win32Error(num, null);
						return;
					}
					if (throwOnMissingSubKey)
					{
						ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyAbsent);
						return;
					}
				}
			}
			else if (throwOnMissingSubKey)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyAbsent);
			}
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000977C4 File Offset: 0x000967C4
		public void DeleteSubKeyTree(string subkey)
		{
			RegistryKey.ValidateKeyName(subkey);
			if (subkey.Length == 0 && this.IsSystemKey())
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegKeyDelHive);
			}
			this.EnsureWriteable();
			subkey = RegistryKey.FixupName(subkey);
			this.CheckSubTreeWritePermission(subkey);
			RegistryKey registryKey = this.InternalOpenSubKey(subkey, true);
			if (registryKey != null)
			{
				try
				{
					if (registryKey.InternalSubKeyCount() > 0)
					{
						string[] array = registryKey.InternalGetSubKeyNames();
						for (int i = 0; i < array.Length; i++)
						{
							registryKey.DeleteSubKeyTreeInternal(array[i]);
						}
					}
				}
				finally
				{
					registryKey.Close();
				}
				int num = Win32Native.RegDeleteKey(this.hkey, subkey);
				if (num != 0)
				{
					this.Win32Error(num, null);
					return;
				}
			}
			else
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyAbsent);
			}
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x00097870 File Offset: 0x00096870
		private void DeleteSubKeyTreeInternal(string subkey)
		{
			RegistryKey registryKey = this.InternalOpenSubKey(subkey, true);
			if (registryKey != null)
			{
				try
				{
					if (registryKey.InternalSubKeyCount() > 0)
					{
						string[] array = registryKey.InternalGetSubKeyNames();
						for (int i = 0; i < array.Length; i++)
						{
							registryKey.DeleteSubKeyTreeInternal(array[i]);
						}
					}
				}
				finally
				{
					registryKey.Close();
				}
				int num = Win32Native.RegDeleteKey(this.hkey, subkey);
				if (num != 0)
				{
					this.Win32Error(num, null);
					return;
				}
			}
			else
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyAbsent);
			}
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000978EC File Offset: 0x000968EC
		public void DeleteValue(string name)
		{
			this.DeleteValue(name, true);
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000978F8 File Offset: 0x000968F8
		public void DeleteValue(string name, bool throwOnMissingValue)
		{
			if (name == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.name);
			}
			this.EnsureWriteable();
			this.CheckValueWritePermission(name);
			int num = Win32Native.RegDeleteValue(this.hkey, name);
			if ((num == 2 || num == 206) && throwOnMissingValue)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSubKeyValueAbsent);
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x00097940 File Offset: 0x00096940
		internal static RegistryKey GetBaseKey(IntPtr hKey)
		{
			int num = (int)hKey & 268435455;
			bool flag = hKey == RegistryKey.HKEY_PERFORMANCE_DATA;
			SafeRegistryHandle safeRegistryHandle = new SafeRegistryHandle(hKey, flag && !RegistryKey.IsWin9x());
			return new RegistryKey(safeRegistryHandle, true, true, false, flag)
			{
				checkMode = RegistryKeyPermissionCheck.Default,
				keyName = RegistryKey.hkeyNames[num]
			};
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x0009799C File Offset: 0x0009699C
		public static RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			int num = (int)(hKey & (RegistryHive)268435455);
			if (num < 0 || num >= RegistryKey.hkeyNames.Length || ((long)hKey & (long)((ulong)-16)) != (long)((ulong)-2147483648))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RegKeyOutOfRange"));
			}
			RegistryKey.CheckUnmanagedCodePermission();
			SafeRegistryHandle safeRegistryHandle = null;
			int num2 = Win32Native.RegConnectRegistry(machineName, new SafeRegistryHandle(new IntPtr((int)hKey), false), out safeRegistryHandle);
			if (num2 == 1114)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DllInitFailure"));
			}
			if (num2 != 0)
			{
				RegistryKey.Win32ErrorStatic(num2, null);
			}
			if (safeRegistryHandle.IsInvalid)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RegKeyNoRemoteConnect", new object[]
				{
					machineName
				}));
			}
			return new RegistryKey(safeRegistryHandle, true, false, true, (IntPtr)((long)hKey) == RegistryKey.HKEY_PERFORMANCE_DATA)
			{
				checkMode = RegistryKeyPermissionCheck.Default,
				keyName = RegistryKey.hkeyNames[num]
			};
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x00097A80 File Offset: 0x00096A80
		public RegistryKey OpenSubKey(string name, bool writable)
		{
			RegistryKey.ValidateKeyName(name);
			this.EnsureNotDisposed();
			name = RegistryKey.FixupName(name);
			this.CheckOpenSubKeyPermission(name, writable);
			SafeRegistryHandle safeRegistryHandle = null;
			int num = Win32Native.RegOpenKeyEx(this.hkey, name, 0, RegistryKey.GetRegistryKeyAccess(writable), out safeRegistryHandle);
			if (num == 0 && !safeRegistryHandle.IsInvalid)
			{
				return new RegistryKey(safeRegistryHandle, writable, false, this.remoteKey, false)
				{
					checkMode = this.GetSubKeyPermissonCheck(writable),
					keyName = this.keyName + "\\" + name
				};
			}
			if (num == 5 || num == 1346)
			{
				ThrowHelper.ThrowSecurityException(ExceptionResource.Security_RegistryPermission);
			}
			return null;
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x00097B16 File Offset: 0x00096B16
		[ComVisible(false)]
		public RegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck)
		{
			RegistryKey.ValidateKeyMode(permissionCheck);
			return this.InternalOpenSubKey(name, permissionCheck, RegistryKey.GetRegistryKeyAccess(permissionCheck));
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x00097B2C File Offset: 0x00096B2C
		[ComVisible(false)]
		public RegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck, RegistryRights rights)
		{
			return this.InternalOpenSubKey(name, permissionCheck, (int)rights);
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x00097B38 File Offset: 0x00096B38
		private RegistryKey InternalOpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck, int rights)
		{
			RegistryKey.ValidateKeyName(name);
			RegistryKey.ValidateKeyMode(permissionCheck);
			RegistryKey.ValidateKeyRights(rights);
			this.EnsureNotDisposed();
			name = RegistryKey.FixupName(name);
			this.CheckOpenSubKeyPermission(name, permissionCheck);
			SafeRegistryHandle safeRegistryHandle = null;
			int num = Win32Native.RegOpenKeyEx(this.hkey, name, 0, rights, out safeRegistryHandle);
			if (num == 0 && !safeRegistryHandle.IsInvalid)
			{
				return new RegistryKey(safeRegistryHandle, permissionCheck == RegistryKeyPermissionCheck.ReadWriteSubTree, false, this.remoteKey, false)
				{
					keyName = this.keyName + "\\" + name,
					checkMode = permissionCheck
				};
			}
			if (num == 5 || num == 1346)
			{
				ThrowHelper.ThrowSecurityException(ExceptionResource.Security_RegistryPermission);
			}
			return null;
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x00097BD4 File Offset: 0x00096BD4
		internal RegistryKey InternalOpenSubKey(string name, bool writable)
		{
			RegistryKey.ValidateKeyName(name);
			this.EnsureNotDisposed();
			int registryKeyAccess = RegistryKey.GetRegistryKeyAccess(writable);
			SafeRegistryHandle safeRegistryHandle = null;
			if (Win32Native.RegOpenKeyEx(this.hkey, name, 0, registryKeyAccess, out safeRegistryHandle) == 0 && !safeRegistryHandle.IsInvalid)
			{
				return new RegistryKey(safeRegistryHandle, writable, false, this.remoteKey, false)
				{
					keyName = this.keyName + "\\" + name
				};
			}
			return null;
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x00097C3C File Offset: 0x00096C3C
		public RegistryKey OpenSubKey(string name)
		{
			return this.OpenSubKey(name, false);
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002D6D RID: 11629 RVA: 0x00097C46 File Offset: 0x00096C46
		public int SubKeyCount
		{
			get
			{
				this.CheckKeyReadPermission();
				return this.InternalSubKeyCount();
			}
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x00097C54 File Offset: 0x00096C54
		internal int InternalSubKeyCount()
		{
			this.EnsureNotDisposed();
			int result = 0;
			int num = 0;
			int num2 = Win32Native.RegQueryInfoKey(this.hkey, null, null, Win32Native.NULL, ref result, null, null, ref num, null, null, null, null);
			if (num2 != 0)
			{
				this.Win32Error(num2, null);
			}
			return result;
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x00097C94 File Offset: 0x00096C94
		public string[] GetSubKeyNames()
		{
			this.CheckKeyReadPermission();
			return this.InternalGetSubKeyNames();
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x00097CA4 File Offset: 0x00096CA4
		internal string[] InternalGetSubKeyNames()
		{
			this.EnsureNotDisposed();
			int num = this.InternalSubKeyCount();
			string[] array = new string[num];
			if (num > 0)
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				for (int i = 0; i < num; i++)
				{
					int capacity = stringBuilder.Capacity;
					int num2 = Win32Native.RegEnumKeyEx(this.hkey, i, stringBuilder, out capacity, null, null, null, null);
					if (num2 != 0)
					{
						this.Win32Error(num2, null);
					}
					array[i] = stringBuilder.ToString();
				}
			}
			return array;
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x00097D19 File Offset: 0x00096D19
		public int ValueCount
		{
			get
			{
				this.CheckKeyReadPermission();
				return this.InternalValueCount();
			}
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x00097D28 File Offset: 0x00096D28
		internal int InternalValueCount()
		{
			this.EnsureNotDisposed();
			int result = 0;
			int num = 0;
			int num2 = Win32Native.RegQueryInfoKey(this.hkey, null, null, Win32Native.NULL, ref num, null, null, ref result, null, null, null, null);
			if (num2 != 0)
			{
				this.Win32Error(num2, null);
			}
			return result;
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x00097D68 File Offset: 0x00096D68
		public string[] GetValueNames()
		{
			this.CheckKeyReadPermission();
			this.EnsureNotDisposed();
			int num = this.InternalValueCount();
			string[] array = new string[num];
			if (num > 0)
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				for (int i = 0; i < num; i++)
				{
					int capacity = stringBuilder.Capacity;
					int num2 = Win32Native.RegEnumValue(this.hkey, i, stringBuilder, ref capacity, Win32Native.NULL, null, null, null);
					if (num2 == 234 && !this.IsPerfDataKey() && this.remoteKey)
					{
						int[] array2 = new int[1];
						byte[] lpData = new byte[5];
						array2[0] = 5;
						num2 = Win32Native.RegEnumValueA(this.hkey, i, stringBuilder, ref capacity, Win32Native.NULL, null, lpData, array2);
						if (num2 == 234)
						{
							array2[0] = 0;
							num2 = Win32Native.RegEnumValueA(this.hkey, i, stringBuilder, ref capacity, Win32Native.NULL, null, null, array2);
						}
					}
					if (num2 != 0 && (!this.IsPerfDataKey() || num2 != 234))
					{
						this.Win32Error(num2, null);
					}
					array[i] = stringBuilder.ToString();
				}
			}
			return array;
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x00097E74 File Offset: 0x00096E74
		public object GetValue(string name)
		{
			this.CheckValueReadPermission(name);
			return this.InternalGetValue(name, null, false, true);
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x00097E87 File Offset: 0x00096E87
		public object GetValue(string name, object defaultValue)
		{
			this.CheckValueReadPermission(name);
			return this.InternalGetValue(name, defaultValue, false, true);
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x00097E9C File Offset: 0x00096E9C
		[ComVisible(false)]
		public object GetValue(string name, object defaultValue, RegistryValueOptions options)
		{
			if (options < RegistryValueOptions.None || options > RegistryValueOptions.DoNotExpandEnvironmentNames)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)options
				}), "options");
			}
			bool doNotExpand = options == RegistryValueOptions.DoNotExpandEnvironmentNames;
			this.CheckValueReadPermission(name);
			return this.InternalGetValue(name, defaultValue, doNotExpand, true);
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x00097EF0 File Offset: 0x00096EF0
		internal object InternalGetValue(string name, object defaultValue, bool doNotExpand, bool checkSecurity)
		{
			if (checkSecurity)
			{
				this.EnsureNotDisposed();
			}
			object obj = defaultValue;
			int num = 0;
			int num2 = 0;
			int num3 = Win32Native.RegQueryValueEx(this.hkey, name, null, ref num, null, ref num2);
			if (num3 != 0)
			{
				if (this.IsPerfDataKey())
				{
					int num4 = 65000;
					int num5 = num4;
					byte[] array = new byte[num4];
					int num6;
					while (234 == (num6 = Win32Native.RegQueryValueEx(this.hkey, name, null, ref num, array, ref num5)))
					{
						num4 *= 2;
						num5 = num4;
						array = new byte[num4];
					}
					if (num6 != 0)
					{
						this.Win32Error(num6, name);
					}
					return array;
				}
				if (num3 != 234)
				{
					return obj;
				}
			}
			switch (num)
			{
			case 1:
				if (RegistryKey._SystemDefaultCharSize != 1)
				{
					StringBuilder stringBuilder = new StringBuilder(num2 / 2);
					num3 = Win32Native.RegQueryValueEx(this.hkey, name, null, ref num, stringBuilder, ref num2);
					obj = stringBuilder.ToString();
				}
				else
				{
					byte[] array2 = new byte[num2];
					num3 = Win32Native.RegQueryValueEx(this.hkey, name, null, ref num, array2, ref num2);
					obj = Encoding.Default.GetString(array2, 0, array2.Length - 1);
				}
				break;
			case 2:
				if (RegistryKey._SystemDefaultCharSize != 1)
				{
					StringBuilder stringBuilder2 = new StringBuilder(num2 / 2);
					num3 = Win32Native.RegQueryValueEx(this.hkey, name, null, ref num, stringBuilder2, ref num2);
					if (doNotExpand)
					{
						obj = stringBuilder2.ToString();
					}
					else
					{
						obj = Environment.ExpandEnvironmentVariables(stringBuilder2.ToString());
					}
				}
				else
				{
					byte[] array3 = new byte[num2];
					num3 = Win32Native.RegQueryValueEx(this.hkey, name, null, ref num, array3, ref num2);
					string @string = Encoding.Default.GetString(array3, 0, array3.Length - 1);
					if (doNotExpand)
					{
						obj = @string;
					}
					else
					{
						obj = Environment.ExpandEnvironmentVariables(@string);
					}
				}
				break;
			case 3:
			case 5:
			{
				byte[] array4 = new byte[num2];
				num3 = Win32Native.RegQueryValueEx(this.hkey, name, null, ref num, array4, ref num2);
				obj = array4;
				break;
			}
			case 4:
			{
				int num7 = 0;
				num3 = Win32Native.RegQueryValueEx(this.hkey, name, null, ref num, ref num7, ref num2);
				obj = num7;
				break;
			}
			case 7:
			{
				bool flag = RegistryKey._SystemDefaultCharSize != 1;
				IList list = new ArrayList();
				if (flag)
				{
					char[] array5 = new char[num2 / 2];
					num3 = Win32Native.RegQueryValueEx(this.hkey, name, null, ref num, array5, ref num2);
					int num8 = 0;
					int num9 = array5.Length;
					while (num3 == 0)
					{
						if (num8 >= num9)
						{
							break;
						}
						int num10 = num8;
						while (num10 < num9 && array5[num10] != '\0')
						{
							num10++;
						}
						if (num10 < num9)
						{
							if (num10 - num8 > 0)
							{
								list.Add(new string(array5, num8, num10 - num8));
							}
							else if (num10 != num9 - 1)
							{
								list.Add(string.Empty);
							}
						}
						else
						{
							list.Add(new string(array5, num8, num9 - num8));
						}
						num8 = num10 + 1;
					}
				}
				else
				{
					byte[] array6 = new byte[num2];
					num3 = Win32Native.RegQueryValueEx(this.hkey, name, null, ref num, array6, ref num2);
					int num11 = 0;
					int num12 = array6.Length;
					while (num3 == 0 && num11 < num12)
					{
						int num13 = num11;
						while (num13 < num12 && array6[num13] != 0)
						{
							num13++;
						}
						if (num13 < num12)
						{
							if (num13 - num11 > 0)
							{
								list.Add(Encoding.Default.GetString(array6, num11, num13 - num11));
							}
							else if (num13 != num12 - 1)
							{
								list.Add(string.Empty);
							}
						}
						else
						{
							list.Add(Encoding.Default.GetString(array6, num11, num12 - num11));
						}
						num11 = num13 + 1;
					}
				}
				obj = new string[list.Count];
				list.CopyTo((Array)obj, 0);
				break;
			}
			case 11:
			{
				long num14 = 0L;
				num3 = Win32Native.RegQueryValueEx(this.hkey, name, null, ref num, ref num14, ref num2);
				obj = num14;
				break;
			}
			}
			return obj;
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000982C4 File Offset: 0x000972C4
		[ComVisible(false)]
		public RegistryValueKind GetValueKind(string name)
		{
			this.CheckValueReadPermission(name);
			this.EnsureNotDisposed();
			int num = 0;
			int num2 = 0;
			int num3 = Win32Native.RegQueryValueEx(this.hkey, name, null, ref num, null, ref num2);
			if (num3 != 0)
			{
				this.Win32Error(num3, null);
			}
			if (!Enum.IsDefined(typeof(RegistryValueKind), num))
			{
				return RegistryValueKind.Unknown;
			}
			return (RegistryValueKind)num;
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x0009831A File Offset: 0x0009731A
		private bool IsDirty()
		{
			return (this.state & 1) != 0;
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x0009832A File Offset: 0x0009732A
		private bool IsSystemKey()
		{
			return (this.state & 2) != 0;
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x0009833A File Offset: 0x0009733A
		private bool IsWritable()
		{
			return (this.state & 4) != 0;
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x0009834A File Offset: 0x0009734A
		private bool IsPerfDataKey()
		{
			return (this.state & 8) != 0;
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x0009835A File Offset: 0x0009735A
		private static bool IsWin9x()
		{
			return (Environment.OSInfo & Environment.OSName.Win9x) != Environment.OSName.Invalid;
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x0009836A File Offset: 0x0009736A
		public string Name
		{
			get
			{
				this.EnsureNotDisposed();
				return this.keyName;
			}
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x00098378 File Offset: 0x00097378
		private void SetDirty()
		{
			this.state |= 1;
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x00098388 File Offset: 0x00097388
		public void SetValue(string name, object value)
		{
			this.SetValue(name, value, RegistryValueKind.Unknown);
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x00098394 File Offset: 0x00097394
		[ComVisible(false)]
		public unsafe void SetValue(string name, object value, RegistryValueKind valueKind)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (name != null && name.Length > 255)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RegKeyStrLenBug"));
			}
			if (!Enum.IsDefined(typeof(RegistryValueKind), valueKind))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RegBadKeyKind"), "valueKind");
			}
			this.EnsureWriteable();
			if (!this.remoteKey && this.ContainsRegistryValue(name))
			{
				this.CheckValueWritePermission(name);
			}
			else
			{
				this.CheckValueCreatePermission(name);
			}
			if (valueKind == RegistryValueKind.Unknown)
			{
				valueKind = this.CalculateValueKind(value);
			}
			int num = 0;
			try
			{
				switch (valueKind)
				{
				case RegistryValueKind.String:
				case RegistryValueKind.ExpandString:
				{
					string text = value.ToString();
					if (RegistryKey._SystemDefaultCharSize == 1)
					{
						byte[] bytes = Encoding.Default.GetBytes(text);
						byte[] array = new byte[bytes.Length + 1];
						Array.Copy(bytes, 0, array, 0, bytes.Length);
						num = Win32Native.RegSetValueEx(this.hkey, name, 0, valueKind, array, array.Length);
						goto IL_374;
					}
					num = Win32Native.RegSetValueEx(this.hkey, name, 0, valueKind, text, text.Length * 2 + 2);
					goto IL_374;
				}
				case RegistryValueKind.Binary:
					break;
				case RegistryValueKind.DWord:
				{
					int num2 = Convert.ToInt32(value, CultureInfo.InvariantCulture);
					num = Win32Native.RegSetValueEx(this.hkey, name, 0, RegistryValueKind.DWord, ref num2, 4);
					goto IL_374;
				}
				case (RegistryValueKind)5:
				case (RegistryValueKind)6:
				case (RegistryValueKind)8:
				case (RegistryValueKind)9:
				case (RegistryValueKind)10:
					goto IL_374;
				case RegistryValueKind.MultiString:
				{
					string[] array2 = (string[])((string[])value).Clone();
					bool flag = RegistryKey._SystemDefaultCharSize != 1;
					int num3 = 0;
					if (flag)
					{
						for (int i = 0; i < array2.Length; i++)
						{
							if (array2[i] == null)
							{
								ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetStrArrNull);
							}
							num3 += (array2[i].Length + 1) * 2;
						}
						num3 += 2;
					}
					else
					{
						for (int j = 0; j < array2.Length; j++)
						{
							if (array2[j] == null)
							{
								ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetStrArrNull);
							}
							num3 += Encoding.Default.GetByteCount(array2[j]) + 1;
						}
						num3++;
					}
					byte[] array3 = new byte[num3];
					try
					{
						fixed (byte* ptr = array3)
						{
							IntPtr intPtr = new IntPtr((void*)ptr);
							for (int k = 0; k < array2.Length; k++)
							{
								if (flag)
								{
									string.InternalCopy(array2[k], intPtr, array2[k].Length * 2);
									intPtr = new IntPtr((long)intPtr + (long)(array2[k].Length * 2));
									*(short*)intPtr.ToPointer() = 0;
									intPtr = new IntPtr((long)intPtr + 2L);
								}
								else
								{
									byte[] bytes2 = Encoding.Default.GetBytes(array2[k]);
									Buffer.memcpy(bytes2, 0, (byte*)intPtr.ToPointer(), 0, bytes2.Length);
									intPtr = new IntPtr((long)intPtr + (long)bytes2.Length);
									*(byte*)intPtr.ToPointer() = 0;
									intPtr = new IntPtr((long)intPtr + 1L);
								}
							}
							if (flag)
							{
								*(short*)intPtr.ToPointer() = 0;
								intPtr = new IntPtr((long)intPtr + 2L);
							}
							else
							{
								*(byte*)intPtr.ToPointer() = 0;
								intPtr = new IntPtr((long)intPtr + 1L);
							}
							num = Win32Native.RegSetValueEx(this.hkey, name, 0, RegistryValueKind.MultiString, array3, num3);
							goto IL_374;
						}
					}
					finally
					{
						byte* ptr = null;
					}
					break;
				}
				case RegistryValueKind.QWord:
				{
					long num4 = Convert.ToInt64(value, CultureInfo.InvariantCulture);
					num = Win32Native.RegSetValueEx(this.hkey, name, 0, RegistryValueKind.QWord, ref num4, 8);
					goto IL_374;
				}
				default:
					goto IL_374;
				}
				byte[] array4 = (byte[])value;
				num = Win32Native.RegSetValueEx(this.hkey, name, 0, RegistryValueKind.Binary, array4, array4.Length);
				IL_374:;
			}
			catch (OverflowException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetMismatchedKind);
			}
			catch (InvalidOperationException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetMismatchedKind);
			}
			catch (FormatException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetMismatchedKind);
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegSetMismatchedKind);
			}
			if (num == 0)
			{
				this.SetDirty();
				return;
			}
			this.Win32Error(num, null);
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000987D0 File Offset: 0x000977D0
		private RegistryValueKind CalculateValueKind(object value)
		{
			if (value is int)
			{
				return RegistryValueKind.DWord;
			}
			if (!(value is Array))
			{
				return RegistryValueKind.String;
			}
			if (value is byte[])
			{
				return RegistryValueKind.Binary;
			}
			if (value is string[])
			{
				return RegistryValueKind.MultiString;
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_RegSetBadArrType", new object[]
			{
				value.GetType().Name
			}));
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x0009882A File Offset: 0x0009782A
		public override string ToString()
		{
			this.EnsureNotDisposed();
			return this.keyName;
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x00098838 File Offset: 0x00097838
		public RegistrySecurity GetAccessControl()
		{
			return this.GetAccessControl(AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x00098842 File Offset: 0x00097842
		public RegistrySecurity GetAccessControl(AccessControlSections includeSections)
		{
			this.EnsureNotDisposed();
			return new RegistrySecurity(this.hkey, this.keyName, includeSections);
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x0009885C File Offset: 0x0009785C
		public void SetAccessControl(RegistrySecurity registrySecurity)
		{
			this.EnsureWriteable();
			if (registrySecurity == null)
			{
				throw new ArgumentNullException("registrySecurity");
			}
			registrySecurity.Persist(this.hkey, this.keyName);
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x00098884 File Offset: 0x00097884
		internal void Win32Error(int errorCode, string str)
		{
			switch (errorCode)
			{
			case 2:
				throw new IOException(Environment.GetResourceString("Arg_RegKeyNotFound"), errorCode);
			case 3:
			case 4:
				break;
			case 5:
				if (str != null)
				{
					throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_RegistryKeyGeneric_Key", new object[]
					{
						str
					}));
				}
				throw new UnauthorizedAccessException();
			case 6:
				this.hkey.SetHandleAsInvalid();
				this.hkey = null;
				break;
			default:
				if (errorCode == 234)
				{
					if (this.remoteKey)
					{
						return;
					}
				}
				break;
			}
			throw new IOException(Win32Native.GetMessage(errorCode), errorCode);
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x00098918 File Offset: 0x00097918
		internal static void Win32ErrorStatic(int errorCode, string str)
		{
			if (errorCode != 5)
			{
				throw new IOException(Win32Native.GetMessage(errorCode), errorCode);
			}
			if (str != null)
			{
				throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_RegistryKeyGeneric_Key", new object[]
				{
					str
				}));
			}
			throw new UnauthorizedAccessException();
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x0009895C File Offset: 0x0009795C
		internal static string FixupName(string name)
		{
			if (name.IndexOf('\\') == -1)
			{
				return name;
			}
			StringBuilder stringBuilder = new StringBuilder(name);
			RegistryKey.FixupPath(stringBuilder);
			int num = stringBuilder.Length - 1;
			if (stringBuilder[num] == '\\')
			{
				stringBuilder.Length = num;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x000989A4 File Offset: 0x000979A4
		private static void FixupPath(StringBuilder path)
		{
			int length = path.Length;
			bool flag = false;
			char maxValue = char.MaxValue;
			for (int i = 1; i < length - 1; i++)
			{
				if (path[i] == '\\')
				{
					i++;
					while (i < length && path[i] == '\\')
					{
						path[i] = maxValue;
						i++;
						flag = true;
					}
				}
			}
			if (flag)
			{
				int i = 0;
				int num = 0;
				while (i < length)
				{
					if (path[i] == maxValue)
					{
						i++;
					}
					else
					{
						path[num] = path[i];
						i++;
						num++;
					}
				}
				path.Length += num - i;
			}
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x00098A44 File Offset: 0x00097A44
		private void CheckOpenSubKeyPermission(string subkeyName, bool subKeyWritable)
		{
			if (this.checkMode == RegistryKeyPermissionCheck.Default)
			{
				this.CheckSubKeyReadPermission(subkeyName);
			}
			if (subKeyWritable && this.checkMode == RegistryKeyPermissionCheck.ReadSubTree)
			{
				this.CheckSubTreeReadWritePermission(subkeyName);
			}
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x00098A68 File Offset: 0x00097A68
		private void CheckOpenSubKeyPermission(string subkeyName, RegistryKeyPermissionCheck subKeyCheck)
		{
			if (subKeyCheck == RegistryKeyPermissionCheck.Default && this.checkMode == RegistryKeyPermissionCheck.Default)
			{
				this.CheckSubKeyReadPermission(subkeyName);
			}
			this.CheckSubTreePermission(subkeyName, subKeyCheck);
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x00098A84 File Offset: 0x00097A84
		private void CheckSubTreePermission(string subkeyName, RegistryKeyPermissionCheck subKeyCheck)
		{
			if (subKeyCheck == RegistryKeyPermissionCheck.ReadSubTree)
			{
				if (this.checkMode == RegistryKeyPermissionCheck.Default)
				{
					this.CheckSubTreeReadPermission(subkeyName);
					return;
				}
			}
			else if (subKeyCheck == RegistryKeyPermissionCheck.ReadWriteSubTree && this.checkMode != RegistryKeyPermissionCheck.ReadWriteSubTree)
			{
				this.CheckSubTreeReadWritePermission(subkeyName);
			}
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x00098AAE File Offset: 0x00097AAE
		private void CheckSubKeyWritePermission(string subkeyName)
		{
			if (this.remoteKey)
			{
				RegistryKey.CheckUnmanagedCodePermission();
				return;
			}
			if (this.checkMode == RegistryKeyPermissionCheck.Default)
			{
				new RegistryPermission(RegistryPermissionAccess.Write, this.keyName + "\\" + subkeyName + "\\.").Demand();
			}
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x00098AE7 File Offset: 0x00097AE7
		private void CheckSubKeyReadPermission(string subkeyName)
		{
			if (this.remoteKey)
			{
				RegistryKey.CheckUnmanagedCodePermission();
				return;
			}
			new RegistryPermission(RegistryPermissionAccess.Read, this.keyName + "\\" + subkeyName + "\\.").Demand();
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x00098B18 File Offset: 0x00097B18
		private void CheckSubKeyCreatePermission(string subkeyName)
		{
			if (this.remoteKey)
			{
				RegistryKey.CheckUnmanagedCodePermission();
				return;
			}
			if (this.checkMode == RegistryKeyPermissionCheck.Default)
			{
				new RegistryPermission(RegistryPermissionAccess.Create, this.keyName + "\\" + subkeyName + "\\.").Demand();
			}
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x00098B51 File Offset: 0x00097B51
		private void CheckSubTreeReadPermission(string subkeyName)
		{
			if (this.remoteKey)
			{
				RegistryKey.CheckUnmanagedCodePermission();
				return;
			}
			if (this.checkMode == RegistryKeyPermissionCheck.Default)
			{
				new RegistryPermission(RegistryPermissionAccess.Read, this.keyName + "\\" + subkeyName + "\\").Demand();
			}
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x00098B8A File Offset: 0x00097B8A
		private void CheckSubTreeWritePermission(string subkeyName)
		{
			if (this.remoteKey)
			{
				RegistryKey.CheckUnmanagedCodePermission();
				return;
			}
			if (this.checkMode == RegistryKeyPermissionCheck.Default)
			{
				new RegistryPermission(RegistryPermissionAccess.Write, this.keyName + "\\" + subkeyName + "\\").Demand();
			}
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x00098BC3 File Offset: 0x00097BC3
		private void CheckSubTreeReadWritePermission(string subkeyName)
		{
			if (this.remoteKey)
			{
				RegistryKey.CheckUnmanagedCodePermission();
				return;
			}
			new RegistryPermission(RegistryPermissionAccess.Read | RegistryPermissionAccess.Write, this.keyName + "\\" + subkeyName).Demand();
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x00098BEF File Offset: 0x00097BEF
		private static void CheckUnmanagedCodePermission()
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x00098BFC File Offset: 0x00097BFC
		private void CheckValueWritePermission(string valueName)
		{
			if (this.remoteKey)
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
				return;
			}
			if (this.checkMode == RegistryKeyPermissionCheck.Default)
			{
				new RegistryPermission(RegistryPermissionAccess.Write, this.keyName + "\\" + valueName).Demand();
			}
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x00098C36 File Offset: 0x00097C36
		private void CheckValueCreatePermission(string valueName)
		{
			if (this.remoteKey)
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
				return;
			}
			if (this.checkMode == RegistryKeyPermissionCheck.Default)
			{
				new RegistryPermission(RegistryPermissionAccess.Create, this.keyName + "\\" + valueName).Demand();
			}
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x00098C70 File Offset: 0x00097C70
		private void CheckValueReadPermission(string valueName)
		{
			if (this.checkMode == RegistryKeyPermissionCheck.Default)
			{
				new RegistryPermission(RegistryPermissionAccess.Read, this.keyName + "\\" + valueName).Demand();
			}
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x00098C96 File Offset: 0x00097C96
		private void CheckKeyReadPermission()
		{
			if (this.checkMode == RegistryKeyPermissionCheck.Default)
			{
				new RegistryPermission(RegistryPermissionAccess.Read, this.keyName + "\\.").Demand();
			}
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x00098CBC File Offset: 0x00097CBC
		private bool ContainsRegistryValue(string name)
		{
			int num = 0;
			int num2 = 0;
			int num3 = Win32Native.RegQueryValueEx(this.hkey, name, null, ref num, null, ref num2);
			return num3 == 0;
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x00098CE4 File Offset: 0x00097CE4
		private void EnsureNotDisposed()
		{
			if (this.hkey == null)
			{
				ThrowHelper.ThrowObjectDisposedException(this.keyName, ExceptionResource.ObjectDisposed_RegKeyClosed);
			}
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x00098CFB File Offset: 0x00097CFB
		private void EnsureWriteable()
		{
			this.EnsureNotDisposed();
			if (!this.IsWritable())
			{
				ThrowHelper.ThrowUnauthorizedAccessException(ExceptionResource.UnauthorizedAccess_RegistryNoWrite);
			}
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x00098D14 File Offset: 0x00097D14
		private static int GetRegistryKeyAccess(bool isWritable)
		{
			int result;
			if (!isWritable)
			{
				result = 131097;
			}
			else
			{
				result = 131103;
			}
			return result;
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x00098D34 File Offset: 0x00097D34
		private static int GetRegistryKeyAccess(RegistryKeyPermissionCheck mode)
		{
			int result = 0;
			switch (mode)
			{
			case RegistryKeyPermissionCheck.Default:
			case RegistryKeyPermissionCheck.ReadSubTree:
				result = 131097;
				break;
			case RegistryKeyPermissionCheck.ReadWriteSubTree:
				result = 131103;
				break;
			}
			return result;
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x00098D68 File Offset: 0x00097D68
		private RegistryKeyPermissionCheck GetSubKeyPermissonCheck(bool subkeyWritable)
		{
			if (this.checkMode == RegistryKeyPermissionCheck.Default)
			{
				return this.checkMode;
			}
			if (subkeyWritable)
			{
				return RegistryKeyPermissionCheck.ReadWriteSubTree;
			}
			return RegistryKeyPermissionCheck.ReadSubTree;
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x00098D80 File Offset: 0x00097D80
		private static void ValidateKeyName(string name)
		{
			if (name == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.name);
			}
			int num = name.IndexOf("\\", StringComparison.OrdinalIgnoreCase);
			int num2 = 0;
			while (num != -1)
			{
				if (num - num2 > 255)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegKeyStrLenBug);
				}
				num2 = num + 1;
				num = name.IndexOf("\\", num2, StringComparison.OrdinalIgnoreCase);
			}
			if (name.Length - num2 > 255)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RegKeyStrLenBug);
			}
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x00098DE5 File Offset: 0x00097DE5
		private static void ValidateKeyMode(RegistryKeyPermissionCheck mode)
		{
			if (mode < RegistryKeyPermissionCheck.Default || mode > RegistryKeyPermissionCheck.ReadWriteSubTree)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidRegistryKeyPermissionCheck, ExceptionArgument.mode);
			}
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x00098DF7 File Offset: 0x00097DF7
		private static void ValidateKeyRights(int rights)
		{
			if ((rights & -983104) != 0)
			{
				ThrowHelper.ThrowSecurityException(ExceptionResource.Security_RegistryPermission);
			}
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x00098E0C File Offset: 0x00097E0C
		// Note: this type is marked as 'beforefieldinit'.
		static RegistryKey()
		{
			int num = 3;
			sbyte[] array = new sbyte[4];
			array[0] = 65;
			array[1] = 65;
			RegistryKey._SystemDefaultCharSize = num - Win32Native.lstrlen(array);
		}

		// Token: 0x04001774 RID: 6004
		private const int STATE_DIRTY = 1;

		// Token: 0x04001775 RID: 6005
		private const int STATE_SYSTEMKEY = 2;

		// Token: 0x04001776 RID: 6006
		private const int STATE_WRITEACCESS = 4;

		// Token: 0x04001777 RID: 6007
		private const int STATE_PERF_DATA = 8;

		// Token: 0x04001778 RID: 6008
		private const int MaxKeyLength = 255;

		// Token: 0x04001779 RID: 6009
		private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x0400177A RID: 6010
		private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x0400177B RID: 6011
		private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;

		// Token: 0x0400177C RID: 6012
		internal static readonly IntPtr HKEY_CLASSES_ROOT = new IntPtr(int.MinValue);

		// Token: 0x0400177D RID: 6013
		internal static readonly IntPtr HKEY_CURRENT_USER = new IntPtr(-2147483647);

		// Token: 0x0400177E RID: 6014
		internal static readonly IntPtr HKEY_LOCAL_MACHINE = new IntPtr(-2147483646);

		// Token: 0x0400177F RID: 6015
		internal static readonly IntPtr HKEY_USERS = new IntPtr(-2147483645);

		// Token: 0x04001780 RID: 6016
		internal static readonly IntPtr HKEY_PERFORMANCE_DATA = new IntPtr(-2147483644);

		// Token: 0x04001781 RID: 6017
		internal static readonly IntPtr HKEY_CURRENT_CONFIG = new IntPtr(-2147483643);

		// Token: 0x04001782 RID: 6018
		internal static readonly IntPtr HKEY_DYN_DATA = new IntPtr(-2147483642);

		// Token: 0x04001783 RID: 6019
		private static readonly string[] hkeyNames = new string[]
		{
			"HKEY_CLASSES_ROOT",
			"HKEY_CURRENT_USER",
			"HKEY_LOCAL_MACHINE",
			"HKEY_USERS",
			"HKEY_PERFORMANCE_DATA",
			"HKEY_CURRENT_CONFIG",
			"HKEY_DYN_DATA"
		};

		// Token: 0x04001784 RID: 6020
		private SafeRegistryHandle hkey;

		// Token: 0x04001785 RID: 6021
		private int state;

		// Token: 0x04001786 RID: 6022
		private string keyName;

		// Token: 0x04001787 RID: 6023
		private bool remoteKey;

		// Token: 0x04001788 RID: 6024
		private RegistryKeyPermissionCheck checkMode;

		// Token: 0x04001789 RID: 6025
		private static readonly int _SystemDefaultCharSize;
	}
}
