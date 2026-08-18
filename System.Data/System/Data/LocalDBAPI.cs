using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Data
{
	// Token: 0x02000338 RID: 824
	internal static class LocalDBAPI
	{
		// Token: 0x06002B18 RID: 11032 RVA: 0x002C3168 File Offset: 0x002C2568
		internal static string GetLocalDbInstanceNameFromServerName(string serverName)
		{
			if (serverName == null)
			{
				return null;
			}
			serverName = serverName.TrimStart(new char[0]);
			if (!serverName.StartsWith("(localdb)\\", StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			string text = serverName.Substring("(localdb)\\".Length).Trim();
			if (text.Length == 0)
			{
				return null;
			}
			return text;
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x002C31C8 File Offset: 0x002C25C8
		internal static void ReleaseDLLHandles()
		{
			LocalDBAPI.s_userInstanceDLLHandle = IntPtr.Zero;
			LocalDBAPI.s_localDBFormatMessage = null;
			LocalDBAPI.s_localDBCreateInstance = null;
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002B1A RID: 11034 RVA: 0x002C31F8 File Offset: 0x002C25F8
		private static IntPtr UserInstanceDLLHandle
		{
			get
			{
				if (LocalDBAPI.s_userInstanceDLLHandle == IntPtr.Zero)
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(LocalDBAPI.s_dllLock);
						if (LocalDBAPI.s_userInstanceDLLHandle == IntPtr.Zero)
						{
							SNINativeMethodWrapper.SNIQueryInfo(SNINativeMethodWrapper.QTypes.SNI_QUERY_LOCALDB_HMODULE, ref LocalDBAPI.s_userInstanceDLLHandle);
							if (!(LocalDBAPI.s_userInstanceDLLHandle != IntPtr.Zero))
							{
								SNINativeMethodWrapper.SNI_Error sni_Error = new SNINativeMethodWrapper.SNI_Error();
								SNINativeMethodWrapper.SNIGetLastError(sni_Error);
								throw LocalDBAPI.CreateLocalDBException(Res.GetString("LocalDB_FailedGetDLLHandle"), null, 0, (int)sni_Error.sniError);
							}
							Bid.Trace("<sc.LocalDBAPI.UserInstanceDLLHandle> LocalDB - handle obtained");
						}
					}
					finally
					{
						Monitor.Exit(LocalDBAPI.s_dllLock);
					}
				}
				return LocalDBAPI.s_userInstanceDLLHandle;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x002C32B8 File Offset: 0x002C26B8
		private static LocalDBAPI.LocalDBCreateInstanceDelegate LocalDBCreateInstance
		{
			get
			{
				if (LocalDBAPI.s_localDBCreateInstance == null)
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(LocalDBAPI.s_dllLock);
						if (LocalDBAPI.s_localDBCreateInstance == null)
						{
							IntPtr procAddress = SafeNativeMethods.GetProcAddress(LocalDBAPI.UserInstanceDLLHandle, "LocalDBCreateInstance");
							if (procAddress == IntPtr.Zero)
							{
								int lastWin32Error = Marshal.GetLastWin32Error();
								Bid.Trace("<sc.LocalDBAPI.LocalDBCreateInstance> GetProcAddress for LocalDBCreateInstance error 0x{%X}", lastWin32Error);
								throw LocalDBAPI.CreateLocalDBException(Res.GetString("LocalDB_MethodNotFound"));
							}
							LocalDBAPI.s_localDBCreateInstance = (LocalDBAPI.LocalDBCreateInstanceDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(LocalDBAPI.LocalDBCreateInstanceDelegate));
						}
					}
					finally
					{
						Monitor.Exit(LocalDBAPI.s_dllLock);
					}
				}
				return LocalDBAPI.s_localDBCreateInstance;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06002B1C RID: 11036 RVA: 0x002C3378 File Offset: 0x002C2778
		private static LocalDBAPI.LocalDBFormatMessageDelegate LocalDBFormatMessage
		{
			get
			{
				if (LocalDBAPI.s_localDBFormatMessage == null)
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(LocalDBAPI.s_dllLock);
						if (LocalDBAPI.s_localDBFormatMessage == null)
						{
							IntPtr procAddress = SafeNativeMethods.GetProcAddress(LocalDBAPI.UserInstanceDLLHandle, "LocalDBFormatMessage");
							if (procAddress == IntPtr.Zero)
							{
								int lastWin32Error = Marshal.GetLastWin32Error();
								Bid.Trace("<sc.LocalDBAPI.LocalDBFormatMessage> GetProcAddress for LocalDBFormatMessage error 0x{%X}", lastWin32Error);
								throw LocalDBAPI.CreateLocalDBException(Res.GetString("LocalDB_MethodNotFound"));
							}
							LocalDBAPI.s_localDBFormatMessage = (LocalDBAPI.LocalDBFormatMessageDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(LocalDBAPI.LocalDBFormatMessageDelegate));
						}
					}
					finally
					{
						Monitor.Exit(LocalDBAPI.s_dllLock);
					}
				}
				return LocalDBAPI.s_localDBFormatMessage;
			}
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x002C3438 File Offset: 0x002C2838
		internal static string GetLocalDBMessage(int hrCode)
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				uint capacity = (uint)stringBuilder.Capacity;
				int num = LocalDBAPI.LocalDBFormatMessage(hrCode, 1U, (uint)CultureInfo.CurrentCulture.LCID, stringBuilder, ref capacity);
				if (num >= 0)
				{
					result = stringBuilder.ToString();
				}
				else
				{
					stringBuilder = new StringBuilder(1024);
					capacity = (uint)stringBuilder.Capacity;
					num = LocalDBAPI.LocalDBFormatMessage(hrCode, 1U, 0U, stringBuilder, ref capacity);
					if (num >= 0)
					{
						result = stringBuilder.ToString();
					}
					else
					{
						result = string.Format(CultureInfo.CurrentCulture, "{0} (0x{1:X}).", new object[]
						{
							Res.GetString("LocalDB_UnobtainableMessage"),
							num
						});
					}
				}
			}
			catch (SqlException ex)
			{
				result = string.Format(CultureInfo.CurrentCulture, "{0} ({1}).", new object[]
				{
					Res.GetString("LocalDB_UnobtainableMessage"),
					ex.Message
				});
			}
			return result;
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x002C3538 File Offset: 0x002C2938
		private static SqlException CreateLocalDBException(string errorMessage)
		{
			return LocalDBAPI.CreateLocalDBException(errorMessage, null, 0, 0);
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x002C3558 File Offset: 0x002C2958
		private static SqlException CreateLocalDBException(string errorMessage, string instance, int localDbError, int sniError)
		{
			SqlErrorCollection sqlErrorCollection = new SqlErrorCollection();
			int infoNumber = (localDbError == 0) ? sniError : localDbError;
			if (sniError != 0)
			{
				string name = string.Format(null, "SNI_ERROR_{0}", new object[]
				{
					sniError
				});
				errorMessage = string.Format(null, "{0} (error: {1} - {2})", new object[]
				{
					errorMessage,
					sniError,
					Res.GetString(name)
				});
			}
			sqlErrorCollection.Add(new SqlError(infoNumber, 0, 20, instance, errorMessage, null, 0));
			if (localDbError != 0)
			{
				sqlErrorCollection.Add(new SqlError(infoNumber, 0, 20, instance, LocalDBAPI.GetLocalDBMessage(localDbError), null, 0));
			}
			SqlException ex = SqlException.CreateException(sqlErrorCollection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x002C3608 File Offset: 0x002C2A08
		internal static void DemandLocalDBPermissions()
		{
			if (!LocalDBAPI._partialTrustAllowed)
			{
				if (!LocalDBAPI._partialTrustFlagChecked)
				{
					object data = AppDomain.CurrentDomain.GetData("ALLOW_LOCALDB_IN_PARTIAL_TRUST");
					if (data != null && data is bool)
					{
						LocalDBAPI._partialTrustAllowed = (bool)data;
					}
					LocalDBAPI._partialTrustFlagChecked = true;
					if (LocalDBAPI._partialTrustAllowed)
					{
						return;
					}
				}
				if (LocalDBAPI._fullTrust == null)
				{
					LocalDBAPI._fullTrust = new NamedPermissionSet("FullTrust");
				}
				LocalDBAPI._fullTrust.Demand();
			}
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x002C3678 File Offset: 0x002C2A78
		internal static void AssertLocalDBPermissions()
		{
			LocalDBAPI._partialTrustAllowed = true;
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x002C3698 File Offset: 0x002C2A98
		internal static void CreateLocalDBInstance(string instance)
		{
			LocalDBAPI.DemandLocalDBPermissions();
			if (LocalDBAPI.s_configurableInstances == null)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(LocalDBAPI.s_configLock);
					if (LocalDBAPI.s_configurableInstances == null)
					{
						Dictionary<string, LocalDBAPI.InstanceInfo> dictionary = new Dictionary<string, LocalDBAPI.InstanceInfo>(StringComparer.OrdinalIgnoreCase);
						object section = PrivilegedConfigurationManager.GetSection("system.data.localdb");
						if (section != null)
						{
							LocalDBConfigurationSection localDBConfigurationSection = section as LocalDBConfigurationSection;
							if (localDBConfigurationSection == null)
							{
								throw LocalDBAPI.CreateLocalDBException(Res.GetString("LocalDB_BadConfigSectionType"));
							}
							using (IEnumerator enumerator = localDBConfigurationSection.LocalDbInstances.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									LocalDBInstanceElement localDBInstanceElement = (LocalDBInstanceElement)obj;
									dictionary.Add(localDBInstanceElement.Name.Trim(), new LocalDBAPI.InstanceInfo(localDBInstanceElement.Version.Trim()));
								}
								goto IL_C9;
							}
						}
						Bid.Trace("<sc.LocalDBAPI.CreateLocalDBInstance> No system.data.localdb section found in configuration");
						IL_C9:
						LocalDBAPI.s_configurableInstances = dictionary;
					}
				}
				finally
				{
					Monitor.Exit(LocalDBAPI.s_configLock);
				}
			}
			LocalDBAPI.InstanceInfo instanceInfo = null;
			if (!LocalDBAPI.s_configurableInstances.TryGetValue(instance, out instanceInfo))
			{
				return;
			}
			if (instanceInfo.created)
			{
				return;
			}
			if (instanceInfo.version.Contains("\0"))
			{
				throw LocalDBAPI.CreateLocalDBException(Res.GetString("LocalDB_InvalidVersion"), instance, 0, 0);
			}
			int num = LocalDBAPI.LocalDBCreateInstance(instanceInfo.version, instance, 0U);
			Bid.Trace("<sc.LocalDBAPI.CreateLocalDBInstance> Starting creation of instance %ls version %ls", instance, instanceInfo.version);
			if (num < 0)
			{
				throw LocalDBAPI.CreateLocalDBException(Res.GetString("LocalDB_CreateFailed"), instance, num, 0);
			}
			Bid.Trace("<sc.LocalDBAPI.CreateLocalDBInstance> Finished creation of instance %ls", instance);
			instanceInfo.created = true;
		}

		// Token: 0x04001C4F RID: 7247
		private const string const_localDbPrefix = "(localdb)\\";

		// Token: 0x04001C50 RID: 7248
		private const string const_partialTrustFlagKey = "ALLOW_LOCALDB_IN_PARTIAL_TRUST";

		// Token: 0x04001C51 RID: 7249
		private const uint const_LOCALDB_TRUNCATE_ERR_MESSAGE = 1U;

		// Token: 0x04001C52 RID: 7250
		private const int const_ErrorMessageBufferSize = 1024;

		// Token: 0x04001C53 RID: 7251
		private static PermissionSet _fullTrust = null;

		// Token: 0x04001C54 RID: 7252
		private static bool _partialTrustFlagChecked = false;

		// Token: 0x04001C55 RID: 7253
		private static bool _partialTrustAllowed = false;

		// Token: 0x04001C56 RID: 7254
		private static IntPtr s_userInstanceDLLHandle = IntPtr.Zero;

		// Token: 0x04001C57 RID: 7255
		private static object s_dllLock = new object();

		// Token: 0x04001C58 RID: 7256
		private static LocalDBAPI.LocalDBCreateInstanceDelegate s_localDBCreateInstance = null;

		// Token: 0x04001C59 RID: 7257
		private static LocalDBAPI.LocalDBFormatMessageDelegate s_localDBFormatMessage = null;

		// Token: 0x04001C5A RID: 7258
		private static object s_configLock = new object();

		// Token: 0x04001C5B RID: 7259
		private static Dictionary<string, LocalDBAPI.InstanceInfo> s_configurableInstances = null;

		// Token: 0x02000339 RID: 825
		// (Invoke) Token: 0x06002B25 RID: 11045
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		[SuppressUnmanagedCodeSecurity]
		private delegate int LocalDBCreateInstanceDelegate([MarshalAs(UnmanagedType.LPWStr)] string version, [MarshalAs(UnmanagedType.LPWStr)] string instance, uint flags);

		// Token: 0x0200033A RID: 826
		// (Invoke) Token: 0x06002B29 RID: 11049
		[SuppressUnmanagedCodeSecurity]
		[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private delegate int LocalDBFormatMessageDelegate(int hrLocalDB, uint dwFlags, uint dwLanguageId, StringBuilder buffer, ref uint buflen);

		// Token: 0x0200033B RID: 827
		private class InstanceInfo
		{
			// Token: 0x06002B2C RID: 11052 RVA: 0x002C3898 File Offset: 0x002C2C98
			internal InstanceInfo(string version)
			{
				this.version = version;
				this.created = false;
			}

			// Token: 0x04001C5C RID: 7260
			internal readonly string version;

			// Token: 0x04001C5D RID: 7261
			internal bool created;
		}
	}
}
