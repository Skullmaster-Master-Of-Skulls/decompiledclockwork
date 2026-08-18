using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200011F RID: 287
	internal static class CertificateExtensionsCommon
	{
		// Token: 0x0600093D RID: 2365 RVA: 0x00020184 File Offset: 0x0001E384
		[SecurityCritical]
		internal static X509Certificate2 CopyWithPersistedCngKey(X509Certificate2 publicCert, CngKey cngKey)
		{
			if (string.IsNullOrEmpty(cngKey.KeyName))
			{
				return null;
			}
			X509Certificate2 x509Certificate = new X509Certificate2(publicCert.RawData);
			CngProvider provider = cngKey.Provider;
			string keyName = cngKey.KeyName;
			bool isMachineKey = cngKey.IsMachineKey;
			int dwKeySpec = CertificateExtensionsCommon.GuessKeySpec(provider, keyName, isMachineKey, cngKey.AlgorithmGroup);
			X509Native.CRYPT_KEY_PROV_INFO crypt_KEY_PROV_INFO = default(X509Native.CRYPT_KEY_PROV_INFO);
			crypt_KEY_PROV_INFO.pwszContainerName = cngKey.KeyName;
			crypt_KEY_PROV_INFO.pwszProvName = cngKey.Provider.Provider;
			crypt_KEY_PROV_INFO.dwFlags = (isMachineKey ? 32 : 0);
			crypt_KEY_PROV_INFO.dwKeySpec = dwKeySpec;
			using (SafeCertContextHandle certificateContext = X509Native.GetCertificateContext(x509Certificate))
			{
				if (!X509Native.SetCertificateKeyProvInfo(certificateContext, ref crypt_KEY_PROV_INFO))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					x509Certificate.Dispose();
					throw new CryptographicException(lastWin32Error);
				}
			}
			return x509Certificate;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00020258 File Offset: 0x0001E458
		[SecurityCritical]
		internal static X509Certificate2 CopyWithPersistedCapiKey(X509Certificate2 publicCert, CspKeyContainerInfo keyContainerInfo)
		{
			if (string.IsNullOrEmpty(keyContainerInfo.KeyContainerName))
			{
				return null;
			}
			X509Certificate2 x509Certificate = new X509Certificate2(publicCert.RawData);
			X509Native.CRYPT_KEY_PROV_INFO crypt_KEY_PROV_INFO = default(X509Native.CRYPT_KEY_PROV_INFO);
			crypt_KEY_PROV_INFO.pwszContainerName = keyContainerInfo.KeyContainerName;
			crypt_KEY_PROV_INFO.pwszProvName = keyContainerInfo.ProviderName;
			crypt_KEY_PROV_INFO.dwProvType = keyContainerInfo.ProviderType;
			crypt_KEY_PROV_INFO.dwKeySpec = (int)keyContainerInfo.KeyNumber;
			crypt_KEY_PROV_INFO.dwFlags = (keyContainerInfo.MachineKeyStore ? 32 : 0);
			using (SafeCertContextHandle certificateContext = X509Native.GetCertificateContext(x509Certificate))
			{
				if (!X509Native.SetCertificateKeyProvInfo(certificateContext, ref crypt_KEY_PROV_INFO))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					x509Certificate.Dispose();
					throw new CryptographicException(lastWin32Error);
				}
			}
			return x509Certificate;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00020314 File Offset: 0x0001E514
		[SecurityCritical]
		internal static X509Certificate2 CopyWithEphemeralCngKey(X509Certificate2 publicCert, CngKey cngKey)
		{
			X509Certificate2 x509Certificate = new X509Certificate2(publicCert.RawData);
			SafeNCryptKeyHandle handle = cngKey.Handle;
			using (SafeCertContextHandle certificateContext = X509Native.GetCertificateContext(x509Certificate))
			{
				if (!X509Native.SetCertificateNCryptKeyHandle(certificateContext, handle))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					x509Certificate.Dispose();
					throw new CryptographicException(lastWin32Error);
				}
			}
			handle.SetHandleAsInvalid();
			return x509Certificate;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0002037C File Offset: 0x0001E57C
		private static int GuessKeySpec(CngProvider provider, string keyName, bool machineKey, CngAlgorithmGroup algorithmGroup)
		{
			if (provider == CngProvider.MicrosoftSoftwareKeyStorageProvider || provider == CngProvider.MicrosoftSmartCardKeyStorageProvider)
			{
				return 0;
			}
			int result;
			try
			{
				CngKeyOpenOptions openOptions = machineKey ? CngKeyOpenOptions.MachineKey : CngKeyOpenOptions.None;
				using (CngKey.Open(keyName, provider, openOptions))
				{
					result = 0;
				}
			}
			catch (CryptographicException)
			{
				CspParameters cspParameters = new CspParameters
				{
					ProviderName = provider.Provider,
					KeyContainerName = keyName,
					Flags = CspProviderFlags.UseExistingKey,
					KeyNumber = 2
				};
				if (machineKey)
				{
					cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
				}
				int num;
				if (!CertificateExtensionsCommon.TryGuessKeySpec(cspParameters, algorithmGroup, out num))
				{
					throw;
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00020430 File Offset: 0x0001E630
		private static bool TryGuessKeySpec(CspParameters cspParameters, CngAlgorithmGroup algorithmGroup, out int keySpec)
		{
			if (algorithmGroup == CngAlgorithmGroup.Rsa)
			{
				return CertificateExtensionsCommon.TryGuessRsaKeySpec(cspParameters, out keySpec);
			}
			if (algorithmGroup == CngAlgorithmGroup.Dsa)
			{
				return CertificateExtensionsCommon.TryGuessDsaKeySpec(cspParameters, out keySpec);
			}
			keySpec = 0;
			return false;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00020460 File Offset: 0x0001E660
		private static bool TryGuessRsaKeySpec(CspParameters cspParameters, out int keySpec)
		{
			int[] array = new int[]
			{
				1,
				24,
				12,
				2
			};
			foreach (int providerType in array)
			{
				cspParameters.ProviderType = providerType;
				try
				{
					using (new RSACryptoServiceProvider(cspParameters))
					{
						keySpec = cspParameters.KeyNumber;
						return true;
					}
				}
				catch (CryptographicException)
				{
				}
			}
			keySpec = 0;
			return false;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x000204E4 File Offset: 0x0001E6E4
		private static bool TryGuessDsaKeySpec(CspParameters cspParameters, out int keySpec)
		{
			int[] array = new int[]
			{
				13,
				3
			};
			foreach (int providerType in array)
			{
				cspParameters.ProviderType = providerType;
				try
				{
					using (new DSACryptoServiceProvider(cspParameters))
					{
						keySpec = cspParameters.KeyNumber;
						return true;
					}
				}
				catch (CryptographicException)
				{
				}
			}
			keySpec = 0;
			return false;
		}
	}
}
