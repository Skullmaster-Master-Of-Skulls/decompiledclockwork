using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005BF RID: 1471
	internal static class Iis7Helper
	{
		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x0600397A RID: 14714 RVA: 0x000DE5FA File Offset: 0x000DC7FA
		internal static int IisVersion
		{
			get
			{
				return Iis7Helper.iisVersion;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x0600397B RID: 14715 RVA: 0x000DE601 File Offset: 0x000DC801
		internal static bool IsIis7
		{
			get
			{
				return Iis7Helper.isIis7;
			}
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x000DE608 File Offset: 0x000DC808
		[SecuritySafeCritical]
		private static bool GetIsIis7()
		{
			Iis7Helper.iisVersion = -1;
			object obj = Iis7Helper.UnsafeGetMajorVersionFromRegistry();
			if (obj != null && obj.GetType().Equals(typeof(int)))
			{
				Iis7Helper.iisVersion = (int)obj;
			}
			return Iis7Helper.iisVersion >= 7;
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x000DE654 File Offset: 0x000DC854
		[SecurityCritical]
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\InetSTP")]
		private static object UnsafeGetMajorVersionFromRegistry()
		{
			object result;
			using (RegistryKey localMachine = Registry.LocalMachine)
			{
				using (RegistryKey registryKey = localMachine.OpenSubKey("Software\\Microsoft\\InetSTP"))
				{
					result = ((registryKey != null) ? registryKey.GetValue("MajorVersion") : null);
				}
			}
			return result;
		}

		// Token: 0x040029E8 RID: 10728
		private static int iisVersion;

		// Token: 0x040029E9 RID: 10729
		private static bool isIis7 = Iis7Helper.GetIsIis7();

		// Token: 0x040029EA RID: 10730
		private const string subKey = "Software\\Microsoft\\InetSTP";
	}
}
