using System;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Channels;
using Microsoft.Win32;

namespace System.ServiceModel.Security
{
	// Token: 0x02000355 RID: 853
	internal static class SecurityUtilsEx
	{
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x00074330 File Offset: 0x00072530
		internal static bool RequiresFipsCompliance
		{
			[SecuritySafeCritical]
			get
			{
				if (SecurityUtilsEx.fipsAlgorithmPolicy == -1)
				{
					if (OSEnvironmentHelper.IsVistaOrGreater)
					{
						bool flag2;
						bool flag = UnsafeNativeMethods.BCryptGetFipsAlgorithmMode(out flag2) == 0;
						if (flag && flag2)
						{
							SecurityUtilsEx.fipsAlgorithmPolicy = 1;
						}
						else
						{
							SecurityUtilsEx.fipsAlgorithmPolicy = 0;
						}
					}
					else
					{
						SecurityUtilsEx.fipsAlgorithmPolicy = SecurityUtilsEx.GetFipsAlgorithmPolicyKeyFromRegistry();
						if (SecurityUtilsEx.fipsAlgorithmPolicy != 1)
						{
							SecurityUtilsEx.fipsAlgorithmPolicy = 0;
						}
					}
				}
				return SecurityUtilsEx.fipsAlgorithmPolicy == 1;
			}
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0007438C File Offset: 0x0007258C
		[SecurityCritical]
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Lsa")]
		private static int GetFipsAlgorithmPolicyKeyFromRegistry()
		{
			int result = -1;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Lsa", false))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("FIPSAlgorithmPolicy");
					if (value != null)
					{
						result = (int)value;
					}
				}
			}
			return result;
		}

		// Token: 0x04001ED7 RID: 7895
		private static int fipsAlgorithmPolicy = -1;

		// Token: 0x04001ED8 RID: 7896
		private const string fipsPolicyRegistryKey = "System\\CurrentControlSet\\Control\\Lsa";
	}
}
