using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000467 RID: 1127
	internal static class PrivateKeyEnforcer
	{
		// Token: 0x060029F5 RID: 10741 RVA: 0x000BEFD8 File Offset: 0x000BD1D8
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void VerifyNotPfx(byte[] rawData, string settingName, ref int setting)
		{
			PrivateKeyEnforcer.Impl.VerifyNotPfx(rawData, settingName, ref setting);
		}

		// Token: 0x02000876 RID: 2166
		private static class Impl
		{
			// Token: 0x06004568 RID: 17768 RVA: 0x00121B8C File Offset: 0x0011FD8C
			[SecuritySafeCritical]
			[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
			internal static void VerifyNotPfx(byte[] rawData, string settingName, ref int setting)
			{
				PrivateKeyEnforcer.Impl.PrivateKeySetting privateKeySetting = (PrivateKeyEnforcer.Impl.PrivateKeySetting)setting;
				if (privateKeySetting == PrivateKeyEnforcer.Impl.PrivateKeySetting.Uninitialized)
				{
					privateKeySetting = (PrivateKeyEnforcer.Impl.ReadPrivateKeySetting(settingName) ? PrivateKeyEnforcer.Impl.PrivateKeySetting.Enabled : PrivateKeyEnforcer.Impl.PrivateKeySetting.Disabled);
					setting = (int)privateKeySetting;
				}
				if (privateKeySetting == PrivateKeyEnforcer.Impl.PrivateKeySetting.Enabled)
				{
					X509ContentType certContentType = X509Certificate2.GetCertContentType(rawData);
					if (certContentType == X509ContentType.Pfx)
					{
						throw new CryptographicException(SR.GetString("Cryptography_X509_PfxBlobsNotAllowed"));
					}
				}
			}

			// Token: 0x06004569 RID: 17769 RVA: 0x00121BD0 File Offset: 0x0011FDD0
			[SecuritySafeCritical]
			[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
			private static bool ReadPrivateKeySetting(string settingName)
			{
				bool flag = false;
				string environmentVariable = Environment.GetEnvironmentVariable("COMPlus_" + settingName);
				if (environmentVariable != null && bool.TryParse(environmentVariable, out flag))
				{
					return flag;
				}
				if (PrivateKeyEnforcer.Impl.TryReadSettingFromRegistry(settingName, Registry.CurrentUser, ref flag))
				{
					return flag;
				}
				return !PrivateKeyEnforcer.Impl.TryReadSettingFromRegistry(settingName, Registry.LocalMachine, ref flag) || flag;
			}

			// Token: 0x0600456A RID: 17770 RVA: 0x00121C24 File Offset: 0x0011FE24
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
			private static bool TryReadSettingFromRegistry(string regValueName, RegistryKey regKey, ref bool value)
			{
				try
				{
					using (RegistryKey registryKey = regKey.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework", false))
					{
						if (registryKey != null)
						{
							object value2 = registryKey.GetValue(regValueName);
							if (value2 != null)
							{
								value = Convert.ToBoolean(value2, CultureInfo.InvariantCulture);
								return true;
							}
						}
					}
				}
				catch
				{
				}
				return false;
			}

			// Token: 0x02000932 RID: 2354
			private enum PrivateKeySetting
			{
				// Token: 0x04003DE3 RID: 15843
				Uninitialized,
				// Token: 0x04003DE4 RID: 15844
				Enabled,
				// Token: 0x04003DE5 RID: 15845
				Disabled
			}
		}
	}
}
