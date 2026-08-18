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
	// Token: 0x0200012A RID: 298
	internal static class LocalDBAPI
	{
		// Token: 0x060011E6 RID: 4582 RVA: 0x00089680 File Offset: 0x00088A80
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

		// Token: 0x060011E7 RID: 4583 RVA: 0x000896D4 File Offset: 0x00088AD4
		internal static void ReleaseDLLHandles()
		{
			LocalDBAPI.s_userInstanceDLLHandle = IntPtr.Zero;
			LocalDBAPI.s_localDBFormatMessage = null;
			LocalDBAPI.s_localDBCreateInstance = null;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x000896F8 File Offset: 0x00088AF8
		private static IntPtr UserInstanceDLLHandle
		{
			get
			{
				if (LocalDBAPI.s_userInstanceDLLHandle == IntPtr.Zero)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(LocalDBAPI.s_dllLock, ref flag);
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
						if (flag)
						{
							Monitor.Exit(LocalDBAPI.s_dllLock);
						}
					}
				}
				return LocalDBAPI.s_userInstanceDLLHandle;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x000897BC File Offset: 0x00088BBC
		private static LocalDBAPI.LocalDBCreateInstanceDelegate LocalDBCreateInstance
		{
			get
			{
				if (LocalDBAPI.s_localDBCreateInstance == null)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(LocalDBAPI.s_dllLock, ref flag);
						if (LocalDBAPI.s_localDBCreateInstance == null)
						{
							IntPtr procAddress = SafeNativeMethods.GetProcAddress(LocalDBAPI.UserInstanceDLLHandle, "LocalDBCreateInstance");
							if (procAddress == IntPtr.Zero)
							{
								int lastWin32Error = Marshal.GetLastWin32Error();
								Bid.Trace("<sc.LocalDBAPI.LocalDBCreateInstance> GetProcAddress for LocalDBCreateInstance error 0x{%X}", lastWin32Error);
								throw LocalDBAPI.CreateLocalDBException(Res.GetString("LocalDB_MethodNotFound"), null, 0, 0);
							}
							LocalDBAPI.s_localDBCreateInstance = (LocalDBAPI.LocalDBCreateInstanceDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(LocalDBAPI.LocalDBCreateInstanceDelegate));
						}
					}
					finally
					{
						if (flag)
						{
							Monitor.Exit(LocalDBAPI.s_dllLock);
						}
					}
				}
				return LocalDBAPI.s_localDBCreateInstance;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0008987C File Offset: 0x00088C7C
		private static LocalDBAPI.LocalDBFormatMessageDelegate LocalDBFormatMessage
		{
			get
			{
				if (LocalDBAPI.s_localDBFormatMessage == null)
				{
					bool flag = false;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						Monitor.Enter(LocalDBAPI.s_dllLock, ref flag);
						if (LocalDBAPI.s_localDBFormatMessage == null)
						{
							IntPtr procAddress = SafeNativeMethods.GetProcAddress(LocalDBAPI.UserInstanceDLLHandle, "LocalDBFormatMessage");
							if (procAddress == IntPtr.Zero)
							{
								int lastWin32Error = Marshal.GetLastWin32Error();
								Bid.Trace("<sc.LocalDBAPI.LocalDBFormatMessage> GetProcAddress for LocalDBFormatMessage error 0x{%X}", lastWin32Error);
								throw LocalDBAPI.CreateLocalDBException(Res.GetString("LocalDB_MethodNotFound"), null, 0, 0);
							}
							LocalDBAPI.s_localDBFormatMessage = (LocalDBAPI.LocalDBFormatMessageDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(LocalDBAPI.LocalDBFormatMessageDelegate));
						}
					}
					finally
					{
						if (flag)
						{
							Monitor.Exit(LocalDBAPI.s_dllLock);
						}
					}
				}
				return LocalDBAPI.s_localDBFormatMessage;
			}
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0008993C File Offset: 0x00088D3C
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

		// Token: 0x060011EC RID: 4588 RVA: 0x00089A34 File Offset: 0x00088E34
		private static SqlException CreateLocalDBException(string errorMessage, string instance = null, int localDbError = 0, int sniError = 0)
		{
			SqlErrorCollection sqlErrorCollection = new SqlErrorCollection();
			int infoNumber = (localDbError == 0) ? sniError : localDbError;
			if (sniError != 0)
			{
				string snierrorMessage = SQL.GetSNIErrorMessage(sniError);
				errorMessage = string.Format(null, "{0} (error: {1} - {2})", new object[]
				{
					errorMessage,
					sniError,
					snierrorMessage
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

		// Token: 0x060011ED RID: 4589 RVA: 0x00089ABC File Offset: 0x00088EBC
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

		// Token: 0x060011EE RID: 4590 RVA: 0x00089B2C File Offset: 0x00088F2C
		internal static void AssertLocalDBPermissions()
		{
			LocalDBAPI._partialTrustAllowed = true;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00089B40 File Offset: 0x00088F40
		internal static void CreateLocalDBInstance(string instance)
		{
			LocalDBAPI.DemandLocalDBPermissions();
			if (LocalDBAPI.s_configurableInstances == null)
			{
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Monitor.Enter(LocalDBAPI.s_configLock, ref flag);
					if (LocalDBAPI.s_configurableInstances == null)
					{
						Dictionary<string, LocalDBAPI.InstanceInfo> dictionary = new Dictionary<string, LocalDBAPI.InstanceInfo>(StringComparer.OrdinalIgnoreCase);
						object section = PrivilegedConfigurationManager.GetSection("system.data.localdb");
						if (section != null)
						{
							LocalDBConfigurationSection localDBConfigurationSection = section as LocalDBConfigurationSection;
							if (localDBConfigurationSection == null)
							{
								throw LocalDBAPI.CreateLocalDBException(Res.GetString("LocalDB_BadConfigSectionType"), null, 0, 0);
							}
							using (IEnumerator enumerator = localDBConfigurationSection.LocalDbInstances.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									LocalDBInstanceElement localDBInstanceElement = (LocalDBInstanceElement)obj;
									dictionary.Add(localDBInstanceElement.Name.Trim(), new LocalDBAPI.InstanceInfo(localDBInstanceElement.Version.Trim()));
								}
								goto IL_D1;
							}
						}
						Bid.Trace("<sc.LocalDBAPI.CreateLocalDBInstance> No system.data.localdb section found in configuration");
						IL_D1:
						LocalDBAPI.s_configurableInstances = dictionary;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(LocalDBAPI.s_configLock);
					}
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

		// Token: 0x0400060F RID: 1551
		private const string const_localDbPrefix = "(localdb)\\";

		// Token: 0x04000610 RID: 1552
		private const string const_partialTrustFlagKey = "ALLOW_LOCALDB_IN_PARTIAL_TRUST";

		// Token: 0x04000611 RID: 1553
		private static PermissionSet _fullTrust = null;

		// Token: 0x04000612 RID: 1554
		private static bool _partialTrustFlagChecked = false;

		// Token: 0x04000613 RID: 1555
		private static bool _partialTrustAllowed = false;

		// Token: 0x04000614 RID: 1556
		private static IntPtr s_userInstanceDLLHandle = IntPtr.Zero;

		// Token: 0x04000615 RID: 1557
		private static object s_dllLock = new object();

		// Token: 0x04000616 RID: 1558
		private static LocalDBAPI.LocalDBCreateInstanceDelegate s_localDBCreateInstance = null;

		// Token: 0x04000617 RID: 1559
		private static LocalDBAPI.LocalDBFormatMessageDelegate s_localDBFormatMessage = null;

		// Token: 0x04000618 RID: 1560
		private const uint const_LOCALDB_TRUNCATE_ERR_MESSAGE = 1U;

		// Token: 0x04000619 RID: 1561
		private const int const_ErrorMessageBufferSize = 1024;

		// Token: 0x0400061A RID: 1562
		private static object s_configLock = new object();

		// Token: 0x0400061B RID: 1563
		private static Dictionary<string, LocalDBAPI.InstanceInfo> s_configurableInstances = null;

		// Token: 0x0200035E RID: 862
		// (Invoke) Token: 0x06003434 RID: 13364
		[SuppressUnmanagedCodeSecurity]
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int LocalDBCreateInstanceDelegate([MarshalAs(UnmanagedType.LPWStr)] string version, [MarshalAs(UnmanagedType.LPWStr)] string instance, uint flags);

		// Token: 0x0200035F RID: 863
		// (Invoke) Token: 0x06003438 RID: 13368
		[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		private delegate int LocalDBFormatMessageDelegate(int hrLocalDB, uint dwFlags, uint dwLanguageId, StringBuilder buffer, ref uint buflen);

		// Token: 0x02000360 RID: 864
		private class InstanceInfo
		{
			// Token: 0x0600343B RID: 13371 RVA: 0x00140424 File Offset: 0x0013F824
			internal InstanceInfo(string version)
			{
				this.version = version;
				this.created = false;
			}

			// Token: 0x04001F05 RID: 7941
			internal readonly string version;

			// Token: 0x04001F06 RID: 7942
			internal bool created;
		}
	}
}
