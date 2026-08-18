using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000481 RID: 1153
	public sealed class X509Store : IDisposable
	{
		// Token: 0x06002AA1 RID: 10913 RVA: 0x000C244F File Offset: 0x000C064F
		public X509Store() : this("MY", StoreLocation.CurrentUser)
		{
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000C245D File Offset: 0x000C065D
		public X509Store(string storeName) : this(storeName, StoreLocation.CurrentUser)
		{
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000C2467 File Offset: 0x000C0667
		public X509Store(StoreName storeName) : this(storeName, StoreLocation.CurrentUser)
		{
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000C2471 File Offset: 0x000C0671
		public X509Store(StoreLocation storeLocation) : this("MY", storeLocation)
		{
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000C2480 File Offset: 0x000C0680
		public X509Store(StoreName storeName, StoreLocation storeLocation)
		{
			this.m_safeCertStoreHandle = SafeCertStoreHandle.InvalidHandle;
			base..ctor();
			if (storeLocation != StoreLocation.CurrentUser && storeLocation != StoreLocation.LocalMachine)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[]
				{
					"storeLocation"
				}));
			}
			switch (storeName)
			{
			case StoreName.AddressBook:
				this.m_storeName = "AddressBook";
				break;
			case StoreName.AuthRoot:
				this.m_storeName = "AuthRoot";
				break;
			case StoreName.CertificateAuthority:
				this.m_storeName = "CA";
				break;
			case StoreName.Disallowed:
				this.m_storeName = "Disallowed";
				break;
			case StoreName.My:
				this.m_storeName = "My";
				break;
			case StoreName.Root:
				this.m_storeName = "Root";
				break;
			case StoreName.TrustedPeople:
				this.m_storeName = "TrustedPeople";
				break;
			case StoreName.TrustedPublisher:
				this.m_storeName = "TrustedPublisher";
				break;
			default:
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[]
				{
					"storeName"
				}));
			}
			this.m_location = storeLocation;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000C2594 File Offset: 0x000C0794
		public X509Store(string storeName, StoreLocation storeLocation)
		{
			this.m_safeCertStoreHandle = SafeCertStoreHandle.InvalidHandle;
			base..ctor();
			if (storeLocation != StoreLocation.CurrentUser && storeLocation != StoreLocation.LocalMachine)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[]
				{
					"storeLocation"
				}));
			}
			this.m_storeName = storeName;
			this.m_location = storeLocation;
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000C25F0 File Offset: 0x000C07F0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public X509Store(IntPtr storeHandle)
		{
			this.m_safeCertStoreHandle = SafeCertStoreHandle.InvalidHandle;
			base..ctor();
			if (storeHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("storeHandle");
			}
			this.m_safeCertStoreHandle = CAPISafe.CertDuplicateStore(storeHandle);
			if (this.m_safeCertStoreHandle == null || this.m_safeCertStoreHandle.IsInvalid)
			{
				throw new CryptographicException(SR.GetString("Cryptography_InvalidStoreHandle"), "storeHandle");
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x000C265C File Offset: 0x000C085C
		public IntPtr StoreHandle
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (this.m_safeCertStoreHandle == null || this.m_safeCertStoreHandle.IsInvalid || this.m_safeCertStoreHandle.IsClosed)
				{
					throw new CryptographicException(SR.GetString("Cryptography_X509_StoreNotOpen"));
				}
				return this.m_safeCertStoreHandle.DangerousGetHandle();
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002AA9 RID: 10921 RVA: 0x000C269B File Offset: 0x000C089B
		public StoreLocation Location
		{
			get
			{
				return this.m_location;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002AAA RID: 10922 RVA: 0x000C26A3 File Offset: 0x000C08A3
		public string Name
		{
			get
			{
				return this.m_storeName;
			}
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000C26AC File Offset: 0x000C08AC
		public void Open(OpenFlags flags)
		{
			if (this.m_location != StoreLocation.CurrentUser && this.m_location != StoreLocation.LocalMachine)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Arg_EnumIllegalVal"), new object[]
				{
					"m_location"
				}));
			}
			uint dwFlags = X509Utils.MapX509StoreFlags(this.m_location, flags);
			if (!this.m_safeCertStoreHandle.IsInvalid)
			{
				this.m_safeCertStoreHandle.Dispose();
			}
			this.m_safeCertStoreHandle = CAPI.CertOpenStore(new IntPtr(10L), 65537U, IntPtr.Zero, dwFlags, this.m_storeName);
			if (this.m_safeCertStoreHandle == null || this.m_safeCertStoreHandle.IsInvalid)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			CAPISafe.CertControlStore(this.m_safeCertStoreHandle, 0U, 4U, IntPtr.Zero);
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000C276F File Offset: 0x000C096F
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x000C2777 File Offset: 0x000C0977
		public void Close()
		{
			if (this.m_safeCertStoreHandle != null && !this.m_safeCertStoreHandle.IsClosed)
			{
				this.m_safeCertStoreHandle.Dispose();
			}
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000C279C File Offset: 0x000C099C
		public void Add(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (this.m_safeCertStoreHandle == null || this.m_safeCertStoreHandle.IsInvalid || this.m_safeCertStoreHandle.IsClosed)
			{
				throw new CryptographicException(SR.GetString("Cryptography_X509_StoreNotOpen"));
			}
			if (!CAPI.CertAddCertificateContextToStore(this.m_safeCertStoreHandle, certificate.CertContext, 5U, SafeCertContextHandle.InvalidHandle))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000C2810 File Offset: 0x000C0A10
		public void AddRange(X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			int num = 0;
			try
			{
				foreach (X509Certificate2 certificate in certificates)
				{
					this.Add(certificate);
					num++;
				}
			}
			catch
			{
				for (int i = 0; i < num; i++)
				{
					this.Remove(certificates[i]);
				}
				throw;
			}
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000C2880 File Offset: 0x000C0A80
		public void Remove(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			X509Store.RemoveCertificateFromStore(this.m_safeCertStoreHandle, certificate.CertContext);
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000C28A4 File Offset: 0x000C0AA4
		public void RemoveRange(X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			int num = 0;
			try
			{
				foreach (X509Certificate2 certificate in certificates)
				{
					this.Remove(certificate);
					num++;
				}
			}
			catch
			{
				for (int i = 0; i < num; i++)
				{
					this.Add(certificates[i]);
				}
				throw;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x000C2914 File Offset: 0x000C0B14
		public X509Certificate2Collection Certificates
		{
			get
			{
				if (this.m_safeCertStoreHandle.IsInvalid || this.m_safeCertStoreHandle.IsClosed)
				{
					return new X509Certificate2Collection();
				}
				return X509Utils.GetCertificates(this.m_safeCertStoreHandle);
			}
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000C2944 File Offset: 0x000C0B44
		private static void RemoveCertificateFromStore(SafeCertStoreHandle safeCertStoreHandle, SafeCertContextHandle safeCertContext)
		{
			if (safeCertContext == null || safeCertContext.IsInvalid)
			{
				return;
			}
			if (safeCertStoreHandle == null || safeCertStoreHandle.IsInvalid || safeCertStoreHandle.IsClosed)
			{
				throw new CryptographicException(SR.GetString("Cryptography_X509_StoreNotOpen"));
			}
			SafeCertContextHandle safeCertContextHandle = CAPI.CertFindCertificateInStore(safeCertStoreHandle, 65537U, 0U, 851968U, safeCertContext.DangerousGetHandle(), SafeCertContextHandle.InvalidHandle);
			if (safeCertContextHandle == null || safeCertContextHandle.IsInvalid)
			{
				return;
			}
			GC.SuppressFinalize(safeCertContextHandle);
			if (!CAPI.CertDeleteCertificateFromStore(safeCertContextHandle))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x0400265C RID: 9820
		private string m_storeName;

		// Token: 0x0400265D RID: 9821
		private StoreLocation m_location;

		// Token: 0x0400265E RID: 9822
		private SafeCertStoreHandle m_safeCertStoreHandle;
	}
}
