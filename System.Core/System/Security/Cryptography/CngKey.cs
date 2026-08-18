using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020000E9 RID: 233
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CngKey : IDisposable
	{
		// Token: 0x06000724 RID: 1828 RVA: 0x000174C8 File Offset: 0x000156C8
		[SecurityCritical]
		private CngKey(SafeNCryptProviderHandle kspHandle, SafeNCryptKeyHandle keyHandle)
		{
			this.m_keyHandle = keyHandle;
			this.m_kspHandle = kspHandle;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x000174E0 File Offset: 0x000156E0
		public CngAlgorithmGroup AlgorithmGroup
		{
			[SecuritySafeCritical]
			get
			{
				string propertyAsString = NCryptNative.GetPropertyAsString(this.m_keyHandle, "Algorithm Group", CngPropertyOptions.None);
				if (propertyAsString == null)
				{
					return null;
				}
				return new CngAlgorithmGroup(propertyAsString);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001750C File Offset: 0x0001570C
		public CngAlgorithm Algorithm
		{
			[SecuritySafeCritical]
			get
			{
				string propertyAsString = NCryptNative.GetPropertyAsString(this.m_keyHandle, "Algorithm Name", CngPropertyOptions.None);
				return new CngAlgorithm(propertyAsString);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00017534 File Offset: 0x00015734
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x00017554 File Offset: 0x00015754
		public CngExportPolicies ExportPolicy
		{
			[SecuritySafeCritical]
			get
			{
				return (CngExportPolicies)NCryptNative.GetPropertyAsDWord(this.m_keyHandle, "Export Policy", CngPropertyOptions.None);
			}
			internal set
			{
				CngProperty property = new CngProperty("Export Policy", BitConverter.GetBytes((int)value), CngPropertyOptions.Persist);
				this.SetProperty(property);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001757F File Offset: 0x0001577F
		public SafeNCryptKeyHandle Handle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			get
			{
				return this.m_keyHandle.Duplicate();
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001758C File Offset: 0x0001578C
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x000175E0 File Offset: 0x000157E0
		public bool IsEphemeral
		{
			[SecuritySafeCritical]
			get
			{
				byte[] array = null;
				bool flag;
				try
				{
					array = NCryptNative.GetProperty(this.m_keyHandle, "CLR IsEphemeral", CngPropertyOptions.CustomProperty, out flag);
				}
				catch (CryptographicException)
				{
					return false;
				}
				return flag && array != null && array.Length == 1 && array[0] == 1;
			}
			[SecurityCritical]
			private set
			{
				NCryptNative.SetProperty(this.m_keyHandle, "CLR IsEphemeral", new byte[]
				{
					value ? 1 : 0
				}, CngPropertyOptions.CustomProperty);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00017608 File Offset: 0x00015808
		public bool IsMachineKey
		{
			[SecuritySafeCritical]
			get
			{
				int propertyAsDWord = NCryptNative.GetPropertyAsDWord(this.m_keyHandle, "Key Type", CngPropertyOptions.None);
				return (propertyAsDWord & 32) == 32;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001762F File Offset: 0x0001582F
		public string KeyName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.IsEphemeral)
				{
					return null;
				}
				return NCryptNative.GetPropertyAsString(this.m_keyHandle, "Name", CngPropertyOptions.None);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001764C File Offset: 0x0001584C
		public int KeySize
		{
			[SecuritySafeCritical]
			get
			{
				int result = 0;
				if (NCryptNative.GetPropertyAsInt(this.m_keyHandle, "PublicKeyLength", CngPropertyOptions.None, ref result) == NCryptNative.ErrorCode.Success)
				{
					return result;
				}
				return NCryptNative.GetPropertyAsDWord(this.m_keyHandle, "Length", CngPropertyOptions.None);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00017688 File Offset: 0x00015888
		public CngKeyUsages KeyUsage
		{
			[SecuritySafeCritical]
			get
			{
				return (CngKeyUsages)NCryptNative.GetPropertyAsDWord(this.m_keyHandle, "Key Usage", CngPropertyOptions.None);
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x000176A8 File Offset: 0x000158A8
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x000176BB File Offset: 0x000158BB
		public IntPtr ParentWindowHandle
		{
			[SecuritySafeCritical]
			get
			{
				return NCryptNative.GetPropertyAsIntPtr(this.m_keyHandle, "HWND Handle", CngPropertyOptions.None);
			}
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			set
			{
				NCryptNative.SetProperty<IntPtr>(this.m_keyHandle, "HWND Handle", value, CngPropertyOptions.None);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x000176D0 File Offset: 0x000158D0
		public CngProvider Provider
		{
			[SecuritySafeCritical]
			get
			{
				string propertyAsString = NCryptNative.GetPropertyAsString(this.m_kspHandle, "Name", CngPropertyOptions.None);
				if (propertyAsString == null)
				{
					return null;
				}
				return new CngProvider(propertyAsString);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x000176FA File Offset: 0x000158FA
		public SafeNCryptProviderHandle ProviderHandle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			get
			{
				return this.m_kspHandle.Duplicate();
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00017707 File Offset: 0x00015907
		public string UniqueName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.IsEphemeral)
				{
					return null;
				}
				return NCryptNative.GetPropertyAsString(this.m_keyHandle, "Unique Name", CngPropertyOptions.None);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00017724 File Offset: 0x00015924
		public CngUIPolicy UIPolicy
		{
			[SecuritySafeCritical]
			get
			{
				NCryptNative.NCRYPT_UI_POLICY propertyAsStruct = NCryptNative.GetPropertyAsStruct<NCryptNative.NCRYPT_UI_POLICY>(this.m_keyHandle, "UI Policy", CngPropertyOptions.None);
				string propertyAsString = NCryptNative.GetPropertyAsString(this.m_keyHandle, "Use Context", CngPropertyOptions.None);
				return new CngUIPolicy(propertyAsStruct.dwFlags, propertyAsStruct.pszFriendlyName, propertyAsStruct.pszDescription, propertyAsString, propertyAsStruct.pszCreationTitle);
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00017774 File Offset: 0x00015974
		[SecuritySafeCritical]
		internal KeyContainerPermission BuildKeyContainerPermission(KeyContainerPermissionFlags flags)
		{
			KeyContainerPermission keyContainerPermission = null;
			if (!this.IsEphemeral)
			{
				string text = null;
				string providerName = null;
				try
				{
					text = this.KeyName;
					providerName = NCryptNative.GetPropertyAsString(this.m_kspHandle, "Name", CngPropertyOptions.None);
				}
				catch (CryptographicException)
				{
				}
				if (text != null)
				{
					KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(text, flags);
					keyContainerPermissionAccessEntry.ProviderName = providerName;
					keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
					keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
				}
				else
				{
					keyContainerPermission = new KeyContainerPermission(flags);
				}
			}
			return keyContainerPermission;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x000177F0 File Offset: 0x000159F0
		public static CngKey Create(CngAlgorithm algorithm)
		{
			return CngKey.Create(algorithm, null);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000177F9 File Offset: 0x000159F9
		public static CngKey Create(CngAlgorithm algorithm, string keyName)
		{
			return CngKey.Create(algorithm, keyName, null);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00017804 File Offset: 0x00015A04
		[SecuritySafeCritical]
		public static CngKey Create(CngAlgorithm algorithm, string keyName, CngKeyCreationParameters creationParameters)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (creationParameters == null)
			{
				creationParameters = new CngKeyCreationParameters();
			}
			if (!NCryptNative.NCryptSupported)
			{
				throw new PlatformNotSupportedException(SR.GetString("Cryptography_PlatformNotSupported"));
			}
			if (keyName != null)
			{
				KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(keyName, KeyContainerPermissionFlags.Create);
				keyContainerPermissionAccessEntry.ProviderName = creationParameters.Provider.Provider;
				new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags)
				{
					AccessEntries = 
					{
						keyContainerPermissionAccessEntry
					}
				}.Demand();
			}
			SafeNCryptProviderHandle safeNCryptProviderHandle = NCryptNative.OpenStorageProvider(creationParameters.Provider.Provider);
			SafeNCryptKeyHandle safeNCryptKeyHandle = NCryptNative.CreatePersistedKey(safeNCryptProviderHandle, algorithm.Algorithm, keyName, creationParameters.KeyCreationOptions);
			CngKey.SetKeyProperties(safeNCryptKeyHandle, creationParameters);
			NCryptNative.FinalizeKey(safeNCryptKeyHandle);
			CngKey cngKey = new CngKey(safeNCryptProviderHandle, safeNCryptKeyHandle);
			if (keyName == null)
			{
				cngKey.IsEphemeral = true;
			}
			return cngKey;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x000178C4 File Offset: 0x00015AC4
		[SecuritySafeCritical]
		public void Delete()
		{
			KeyContainerPermission keyContainerPermission = this.BuildKeyContainerPermission(KeyContainerPermissionFlags.Delete);
			if (keyContainerPermission != null)
			{
				keyContainerPermission.Demand();
			}
			NCryptNative.DeleteKey(this.m_keyHandle);
			this.Dispose();
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x000178F3 File Offset: 0x00015AF3
		[SecuritySafeCritical]
		public void Dispose()
		{
			if (this.m_kspHandle != null)
			{
				this.m_kspHandle.Dispose();
			}
			if (this.m_keyHandle != null)
			{
				this.m_keyHandle.Dispose();
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001791B File Offset: 0x00015B1B
		public static bool Exists(string keyName)
		{
			return CngKey.Exists(keyName, CngProvider.MicrosoftSoftwareKeyStorageProvider);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00017928 File Offset: 0x00015B28
		public static bool Exists(string keyName, CngProvider provider)
		{
			return CngKey.Exists(keyName, provider, CngKeyOpenOptions.None);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00017934 File Offset: 0x00015B34
		[SecuritySafeCritical]
		public static bool Exists(string keyName, CngProvider provider, CngKeyOpenOptions options)
		{
			if (keyName == null)
			{
				throw new ArgumentNullException("keyName");
			}
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!NCryptNative.NCryptSupported)
			{
				throw new PlatformNotSupportedException(SR.GetString("Cryptography_PlatformNotSupported"));
			}
			bool result;
			using (SafeNCryptProviderHandle safeNCryptProviderHandle = NCryptNative.OpenStorageProvider(provider.Provider))
			{
				SafeNCryptKeyHandle safeNCryptKeyHandle = null;
				try
				{
					NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptOpenKey(safeNCryptProviderHandle, out safeNCryptKeyHandle, keyName, 0, options);
					bool flag = errorCode == NCryptNative.ErrorCode.KeyDoesNotExist || errorCode == NCryptNative.ErrorCode.NotFound;
					if (errorCode != NCryptNative.ErrorCode.Success && !flag)
					{
						throw new CryptographicException((int)errorCode);
					}
					result = (errorCode == NCryptNative.ErrorCode.Success);
				}
				finally
				{
					if (safeNCryptKeyHandle != null)
					{
						safeNCryptKeyHandle.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000179F0 File Offset: 0x00015BF0
		public static CngKey Import(byte[] keyBlob, CngKeyBlobFormat format)
		{
			return CngKey.Import(keyBlob, format, CngProvider.MicrosoftSoftwareKeyStorageProvider);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000179FE File Offset: 0x00015BFE
		internal static CngKey Import(byte[] keyBlob, string curveName, CngKeyBlobFormat format)
		{
			return CngKey.Import(keyBlob, curveName, format, CngProvider.MicrosoftSoftwareKeyStorageProvider);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00017A0D File Offset: 0x00015C0D
		public static CngKey Import(byte[] keyBlob, CngKeyBlobFormat format, CngProvider provider)
		{
			return CngKey.Import(keyBlob, null, format, provider);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00017A18 File Offset: 0x00015C18
		[SecuritySafeCritical]
		internal static CngKey Import(byte[] keyBlob, string curveName, CngKeyBlobFormat format, CngProvider provider)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!NCryptNative.NCryptSupported)
			{
				throw new PlatformNotSupportedException(SR.GetString("Cryptography_PlatformNotSupported"));
			}
			if (!(format == CngKeyBlobFormat.EccPublicBlob) && !(format == CngKeyBlobFormat.EccFullPublicBlob) && !(format == CngKeyBlobFormat.GenericPublicBlob))
			{
				new KeyContainerPermission(KeyContainerPermissionFlags.Import).Demand();
			}
			SafeNCryptProviderHandle safeNCryptProviderHandle = NCryptNative.OpenStorageProvider(provider.Provider);
			SafeNCryptKeyHandle keyHandle;
			if (curveName == null)
			{
				keyHandle = NCryptNative.ImportKey(safeNCryptProviderHandle, keyBlob, format.Format);
			}
			else
			{
				keyHandle = ECCng.ImportKeyBlob(format.Format, keyBlob, curveName, safeNCryptProviderHandle);
			}
			return new CngKey(safeNCryptProviderHandle, keyHandle)
			{
				IsEphemeral = (format != CngKeyBlobFormat.OpaqueTransportBlob)
			};
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00017AF4 File Offset: 0x00015CF4
		[SecuritySafeCritical]
		public byte[] Export(CngKeyBlobFormat format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			KeyContainerPermission keyContainerPermission = this.BuildKeyContainerPermission(KeyContainerPermissionFlags.Export);
			if (keyContainerPermission != null)
			{
				keyContainerPermission.Demand();
			}
			return NCryptNative.ExportKey(this.m_keyHandle, format.Format);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00017B38 File Offset: 0x00015D38
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public CngProperty GetProperty(string name, CngPropertyOptions options)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			bool flag;
			byte[] property = NCryptNative.GetProperty(this.m_keyHandle, name, options, out flag);
			if (!flag)
			{
				throw new CryptographicException(-2146893807);
			}
			return new CngProperty(name, property, options);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00017B7C File Offset: 0x00015D7C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public bool HasProperty(string name, CngPropertyOptions options)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			bool result;
			NCryptNative.GetProperty(this.m_keyHandle, name, options, out result);
			return result;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00017BA8 File Offset: 0x00015DA8
		public static CngKey Open(string keyName)
		{
			return CngKey.Open(keyName, CngProvider.MicrosoftSoftwareKeyStorageProvider);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00017BB5 File Offset: 0x00015DB5
		public static CngKey Open(string keyName, CngProvider provider)
		{
			return CngKey.Open(keyName, provider, CngKeyOpenOptions.None);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00017BC0 File Offset: 0x00015DC0
		[SecuritySafeCritical]
		public static CngKey Open(string keyName, CngProvider provider, CngKeyOpenOptions openOptions)
		{
			if (keyName == null)
			{
				throw new ArgumentNullException("keyName");
			}
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!NCryptNative.NCryptSupported)
			{
				throw new PlatformNotSupportedException(SR.GetString("Cryptography_PlatformNotSupported"));
			}
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(keyName, KeyContainerPermissionFlags.Open);
			keyContainerPermissionAccessEntry.ProviderName = provider.Provider;
			new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags)
			{
				AccessEntries = 
				{
					keyContainerPermissionAccessEntry
				}
			}.Demand();
			SafeNCryptProviderHandle safeNCryptProviderHandle = NCryptNative.OpenStorageProvider(provider.Provider);
			SafeNCryptKeyHandle keyHandle = NCryptNative.OpenKey(safeNCryptProviderHandle, keyName, openOptions);
			return new CngKey(safeNCryptProviderHandle, keyHandle);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00017C50 File Offset: 0x00015E50
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static CngKey Open(SafeNCryptKeyHandle keyHandle, CngKeyHandleOpenOptions keyHandleOpenOptions)
		{
			if (keyHandle == null)
			{
				throw new ArgumentNullException("keyHandle");
			}
			if (keyHandle.IsClosed || keyHandle.IsInvalid)
			{
				throw new ArgumentException(SR.GetString("Cryptography_OpenInvalidHandle"), "keyHandle");
			}
			SafeNCryptKeyHandle keyHandle2 = keyHandle.Duplicate();
			SafeNCryptProviderHandle safeNCryptProviderHandle = new SafeNCryptProviderHandle();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				IntPtr propertyAsIntPtr = NCryptNative.GetPropertyAsIntPtr(keyHandle, "Provider Handle", CngPropertyOptions.None);
				safeNCryptProviderHandle.SetHandleValue(propertyAsIntPtr);
			}
			CngKey cngKey = null;
			bool flag = false;
			try
			{
				cngKey = new CngKey(safeNCryptProviderHandle, keyHandle2);
				bool flag2 = (keyHandleOpenOptions & CngKeyHandleOpenOptions.EphemeralKey) == CngKeyHandleOpenOptions.EphemeralKey;
				if (!cngKey.IsEphemeral && flag2)
				{
					cngKey.IsEphemeral = true;
				}
				else if (cngKey.IsEphemeral && !flag2)
				{
					throw new ArgumentException(SR.GetString("Cryptography_OpenEphemeralKeyHandleWithoutEphemeralFlag"), "keyHandleOpenOptions");
				}
				flag = true;
			}
			finally
			{
				if (!flag && cngKey != null)
				{
					cngKey.Dispose();
				}
			}
			return cngKey;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00017D38 File Offset: 0x00015F38
		[SecurityCritical]
		private static void SetKeyProperties(SafeNCryptKeyHandle keyHandle, CngKeyCreationParameters creationParameters)
		{
			if (creationParameters.ExportPolicy != null)
			{
				NCryptNative.SetProperty(keyHandle, "Export Policy", (int)creationParameters.ExportPolicy.Value, CngPropertyOptions.Persist);
			}
			if (creationParameters.KeyUsage != null)
			{
				NCryptNative.SetProperty(keyHandle, "Key Usage", (int)creationParameters.KeyUsage.Value, CngPropertyOptions.Persist);
			}
			if (creationParameters.ParentWindowHandle != IntPtr.Zero)
			{
				NCryptNative.SetProperty<IntPtr>(keyHandle, "HWND Handle", creationParameters.ParentWindowHandle, CngPropertyOptions.None);
			}
			if (creationParameters.UIPolicy != null)
			{
				NCryptNative.SetProperty<NCryptNative.NCRYPT_UI_POLICY>(keyHandle, "UI Policy", new NCryptNative.NCRYPT_UI_POLICY
				{
					dwVersion = 1,
					dwFlags = creationParameters.UIPolicy.ProtectionLevel,
					pszCreationTitle = creationParameters.UIPolicy.CreationTitle,
					pszFriendlyName = creationParameters.UIPolicy.FriendlyName,
					pszDescription = creationParameters.UIPolicy.Description
				}, CngPropertyOptions.Persist);
				if (creationParameters.UIPolicy.UseContext != null)
				{
					NCryptNative.SetProperty(keyHandle, "Use Context", creationParameters.UIPolicy.UseContext, CngPropertyOptions.Persist);
				}
			}
			foreach (CngProperty cngProperty in creationParameters.ParametersNoDemand)
			{
				NCryptNative.SetProperty(keyHandle, cngProperty.Name, cngProperty.Value, cngProperty.Options);
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00017EB8 File Offset: 0x000160B8
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public void SetProperty(CngProperty property)
		{
			NCryptNative.SetProperty(this.m_keyHandle, property.Name, property.Value, property.Options);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00017EDA File Offset: 0x000160DA
		internal bool IsECNamedCurve()
		{
			return CngKey.IsECNamedCurve(this.Algorithm.Algorithm);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00017EEC File Offset: 0x000160EC
		internal static bool IsECNamedCurve(string algorithm)
		{
			return algorithm == CngAlgorithm.ECDiffieHellman.Algorithm || algorithm == CngAlgorithm.ECDsa.Algorithm;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00017F12 File Offset: 0x00016112
		[SecuritySafeCritical]
		internal string GetCurveName()
		{
			if (this.IsECNamedCurve())
			{
				return NCryptNative.GetPropertyAsString(this.m_keyHandle, "ECCCurveName", CngPropertyOptions.None);
			}
			return this.GetECSpecificCurveName();
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00017F34 File Offset: 0x00016134
		private string GetECSpecificCurveName()
		{
			string algorithm = this.Algorithm.Algorithm;
			if (algorithm == CngAlgorithm.ECDiffieHellmanP256.Algorithm || algorithm == CngAlgorithm.ECDsaP256.Algorithm)
			{
				return "nistP256";
			}
			if (algorithm == CngAlgorithm.ECDiffieHellmanP384.Algorithm || algorithm == CngAlgorithm.ECDsaP384.Algorithm)
			{
				return "nistP384";
			}
			if (algorithm == CngAlgorithm.ECDiffieHellmanP521.Algorithm || algorithm == CngAlgorithm.ECDsaP521.Algorithm)
			{
				return "nistP521";
			}
			throw new PlatformNotSupportedException(SR.GetString("Cryptography_CurveNotSupported", new object[]
			{
				algorithm
			}));
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00017FE4 File Offset: 0x000161E4
		internal static CngProperty GetPropertyFromNamedCurve(ECCurve curve)
		{
			string text = curve.Oid.FriendlyName ?? "";
			byte[] array = new byte[(text.Length + 1) * 2];
			Encoding.Unicode.GetBytes(text, 0, text.Length, array, 0);
			return new CngProperty("ECCCurveName", array, CngPropertyOptions.None);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00018038 File Offset: 0x00016238
		internal static CngAlgorithm EcdsaCurveNameToAlgorithm(string name)
		{
			if (name == "nistP256" || name == "ECDSA_P256")
			{
				return CngAlgorithm.ECDsaP256;
			}
			if (name == "nistP384" || name == "ECDSA_P384")
			{
				return CngAlgorithm.ECDsaP384;
			}
			if (!(name == "nistP521") && !(name == "ECDSA_P521"))
			{
				return CngAlgorithm.ECDsa;
			}
			return CngAlgorithm.ECDsaP521;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000180AC File Offset: 0x000162AC
		internal static CngAlgorithm EcdhCurveNameToAlgorithm(string name)
		{
			if (name == "nistP256" || name == "ECDH_P256")
			{
				return CngAlgorithm.ECDiffieHellmanP256;
			}
			if (name == "nistP384" || name == "ECDH_P384")
			{
				return CngAlgorithm.ECDiffieHellmanP384;
			}
			if (!(name == "nistP521") && !(name == "ECDH_P521"))
			{
				return CngAlgorithm.ECDiffieHellman;
			}
			return CngAlgorithm.ECDiffieHellmanP521;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00018120 File Offset: 0x00016320
		internal static CngKey Create(ECCurve curve, Func<string, CngAlgorithm> algorithmResolver)
		{
			curve.Validate();
			CngKeyCreationParameters cngKeyCreationParameters = new CngKeyCreationParameters
			{
				ExportPolicy = new CngExportPolicies?(CngExportPolicies.AllowPlaintextExport)
			};
			CngAlgorithm cngAlgorithm;
			if (curve.IsNamed)
			{
				cngAlgorithm = algorithmResolver(curve.Oid.FriendlyName);
				if (CngKey.IsECNamedCurve(cngAlgorithm.Algorithm))
				{
					cngKeyCreationParameters.Parameters.Add(CngKey.GetPropertyFromNamedCurve(curve));
				}
				else if (!(cngAlgorithm == CngAlgorithm.ECDsaP256) && !(cngAlgorithm == CngAlgorithm.ECDiffieHellmanP256) && !(cngAlgorithm == CngAlgorithm.ECDsaP384) && !(cngAlgorithm == CngAlgorithm.ECDiffieHellmanP384) && !(cngAlgorithm == CngAlgorithm.ECDsaP521) && !(cngAlgorithm == CngAlgorithm.ECDiffieHellmanP521))
				{
					throw new ArgumentException(SR.GetString("Cryptography_InvalidKeySize"));
				}
			}
			else
			{
				if (!curve.IsPrime)
				{
					throw new PlatformNotSupportedException(SR.GetString("Cryptography_CurveNotSupported", new object[]
					{
						curve.CurveType.ToString()
					}));
				}
				byte[] primeCurveParameterBlob = ECCng.GetPrimeCurveParameterBlob(ref curve);
				CngProperty item = new CngProperty("ECCParameters", primeCurveParameterBlob, CngPropertyOptions.None);
				cngKeyCreationParameters.Parameters.Add(item);
				cngAlgorithm = algorithmResolver(null);
			}
			CngKey result;
			try
			{
				result = CngKey.Create(cngAlgorithm, null, cngKeyCreationParameters);
			}
			catch (CryptographicException ex)
			{
				Interop.NCrypt.ErrorCode hresult = (Interop.NCrypt.ErrorCode)ex.HResult;
				if (hresult == Interop.NCrypt.ErrorCode.NTE_INVALID_PARAMETER || hresult == Interop.NCrypt.ErrorCode.NTE_NOT_SUPPORTED)
				{
					string text = curve.IsNamed ? curve.Oid.FriendlyName : curve.CurveType.ToString();
					throw new PlatformNotSupportedException(SR.GetString("Cryptography_CurveNotSupported", new object[]
					{
						text
					}), ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x04000611 RID: 1553
		private SafeNCryptKeyHandle m_keyHandle;

		// Token: 0x04000612 RID: 1554
		private SafeNCryptProviderHandle m_kspHandle;
	}
}
