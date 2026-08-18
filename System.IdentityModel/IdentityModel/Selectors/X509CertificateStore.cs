using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001B2 RID: 434
	internal class X509CertificateStore
	{
		// Token: 0x06000E23 RID: 3619 RVA: 0x0004072C File Offset: 0x0003E92C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public X509CertificateStore(StoreName storeName, StoreLocation storeLocation)
		{
			switch (storeName)
			{
			case StoreName.AddressBook:
				this.storeName = "AddressBook";
				break;
			case StoreName.AuthRoot:
				this.storeName = "AuthRoot";
				break;
			case StoreName.CertificateAuthority:
				this.storeName = "CA";
				break;
			case StoreName.Disallowed:
				this.storeName = "Disallowed";
				break;
			case StoreName.My:
				this.storeName = "My";
				break;
			case StoreName.Root:
				this.storeName = "Root";
				break;
			case StoreName.TrustedPeople:
				this.storeName = "TrustedPeople";
				break;
			case StoreName.TrustedPublisher:
				this.storeName = "TrustedPublisher";
				break;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("storeName", (int)storeName, typeof(StoreName)));
			}
			if (storeLocation != StoreLocation.CurrentUser && storeLocation != StoreLocation.LocalMachine)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("storeLocation", SR.GetString("X509CertStoreLocationNotValid")));
			}
			this.storeLocation = storeLocation;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0004082A File Offset: 0x0003EA2A
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public void Close()
		{
			((IDisposable)this.certStoreHandle).Dispose();
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00040838 File Offset: 0x0003EA38
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public void Open(OpenFlags openFlags)
		{
			uint dwFlags = this.MapX509StoreFlags(this.storeLocation, openFlags);
			SafeCertStoreHandle safeCertStoreHandle = CAPI.CertOpenStore(new IntPtr(10L), 65537U, IntPtr.Zero, dwFlags, this.storeName);
			if (safeCertStoreHandle == null || safeCertStoreHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(lastWin32Error));
			}
			this.certStoreHandle = safeCertStoreHandle;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0004089C File Offset: 0x0003EA9C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public X509Certificate2Collection Find(X509FindType findType, object findValue, bool validOnly)
		{
			SafeHGlobalHandle safeHGlobalHandle = SafeHGlobalHandle.InvalidHandle;
			SafeCertContextHandle safeCertContextHandle = SafeCertContextHandle.InvalidHandle;
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			SafeHGlobalHandle safeHGlobalHandle2 = SafeHGlobalHandle.InvalidHandle;
			try
			{
				uint dwFindType;
				switch (findType)
				{
				case X509FindType.FindByThumbprint:
				{
					byte[] array = findValue as byte[];
					if (array == null)
					{
						string text = findValue as string;
						if (text == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("X509FindValueMismatchMulti", new object[]
							{
								findType,
								typeof(string),
								typeof(byte[]),
								findValue.GetType()
							})));
						}
						array = SecurityUtils.DecodeHexString(text);
					}
					CAPI.CRYPTOAPI_BLOB cryptoapi_BLOB = default(CAPI.CRYPTOAPI_BLOB);
					safeHGlobalHandle2 = SafeHGlobalHandle.AllocHGlobal(array);
					cryptoapi_BLOB.pbData = safeHGlobalHandle2.DangerousGetHandle();
					cryptoapi_BLOB.cbData = (uint)array.Length;
					dwFindType = 65536U;
					safeHGlobalHandle = SafeHGlobalHandle.AllocHGlobal(CAPI.CRYPTOAPI_BLOB.Size);
					Marshal.StructureToPtr(cryptoapi_BLOB, safeHGlobalHandle.DangerousGetHandle(), false);
					break;
				}
				case X509FindType.FindBySubjectName:
				{
					string text = findValue as string;
					if (text == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("X509FindValueMismatch", new object[]
						{
							findType,
							typeof(string),
							findValue.GetType()
						})));
					}
					dwFindType = 524295U;
					safeHGlobalHandle = SafeHGlobalHandle.AllocHGlobal(text);
					break;
				}
				case X509FindType.FindBySubjectDistinguishedName:
					if (!(findValue is string))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("X509FindValueMismatch", new object[]
						{
							findType,
							typeof(string),
							findValue.GetType()
						})));
					}
					dwFindType = 0U;
					break;
				case X509FindType.FindByIssuerName:
				{
					string text = findValue as string;
					if (text == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("X509FindValueMismatch", new object[]
						{
							findType,
							typeof(string),
							findValue.GetType()
						})));
					}
					dwFindType = 524292U;
					safeHGlobalHandle = SafeHGlobalHandle.AllocHGlobal(text);
					break;
				}
				case X509FindType.FindByIssuerDistinguishedName:
					if (!(findValue is string))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("X509FindValueMismatch", new object[]
						{
							findType,
							typeof(string),
							findValue.GetType()
						})));
					}
					dwFindType = 0U;
					break;
				case X509FindType.FindBySerialNumber:
				{
					byte[] array = findValue as byte[];
					if (array == null)
					{
						string text = findValue as string;
						if (text == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("X509FindValueMismatchMulti", new object[]
							{
								findType,
								typeof(string),
								typeof(byte[]),
								findValue.GetType()
							})));
						}
						array = SecurityUtils.DecodeHexString(text);
						int num = array.Length;
						int i = 0;
						int num2 = num - 1;
						while (i < array.Length / 2)
						{
							byte b = array[i];
							array[i] = array[num2];
							array[num2] = b;
							i++;
							num2--;
						}
					}
					findValue = array;
					dwFindType = 0U;
					break;
				}
				default:
					if (findType != X509FindType.FindBySubjectKeyIdentifier)
					{
						X509Store x509Store = new X509Store(this.certStoreHandle.DangerousGetHandle());
						try
						{
							return x509Store.Certificates.Find(findType, findValue, validOnly);
						}
						finally
						{
							x509Store.Close();
						}
					}
					else
					{
						byte[] array = findValue as byte[];
						if (array == null)
						{
							string text = findValue as string;
							if (text == null)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("X509FindValueMismatchMulti", new object[]
								{
									findType,
									typeof(string),
									typeof(byte[]),
									findValue.GetType()
								})));
							}
							array = SecurityUtils.DecodeHexString(text);
						}
						findValue = array;
						dwFindType = 0U;
					}
					break;
				}
				safeCertContextHandle = CAPI.CertFindCertificateInStore(this.certStoreHandle, 65537U, 0U, dwFindType, safeHGlobalHandle, safeCertContextHandle);
				while (safeCertContextHandle != null && !safeCertContextHandle.IsInvalid)
				{
					X509Certificate2 certificate;
					if (this.TryGetMatchingX509Certificate(safeCertContextHandle.DangerousGetHandle(), findType, dwFindType, findValue, validOnly, out certificate))
					{
						x509Certificate2Collection.Add(certificate);
					}
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						GC.SuppressFinalize(safeCertContextHandle);
						safeCertContextHandle = CAPI.CertFindCertificateInStore(this.certStoreHandle, 65537U, 0U, dwFindType, safeHGlobalHandle, safeCertContextHandle);
					}
				}
			}
			finally
			{
				if (safeCertContextHandle != null)
				{
					safeCertContextHandle.Close();
				}
				safeHGlobalHandle.Close();
				safeHGlobalHandle2.Close();
			}
			return x509Certificate2Collection;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00040D30 File Offset: 0x0003EF30
		private bool TryGetMatchingX509Certificate(IntPtr certContext, X509FindType findType, uint dwFindType, object findValue, bool validOnly, out X509Certificate2 cert)
		{
			cert = new X509Certificate2(certContext);
			if (dwFindType == 0U)
			{
				switch (findType)
				{
				case X509FindType.FindBySubjectDistinguishedName:
					if (string.Compare((string)findValue, cert.SubjectName.Name, StringComparison.OrdinalIgnoreCase) != 0)
					{
						cert.Reset();
						cert = null;
						return false;
					}
					break;
				case X509FindType.FindByIssuerName:
					break;
				case X509FindType.FindByIssuerDistinguishedName:
					if (string.Compare((string)findValue, cert.IssuerName.Name, StringComparison.OrdinalIgnoreCase) != 0)
					{
						cert.Reset();
						cert = null;
						return false;
					}
					break;
				case X509FindType.FindBySerialNumber:
					if (!this.BinaryMatches((byte[])findValue, cert.GetSerialNumber()))
					{
						cert.Reset();
						cert = null;
						return false;
					}
					break;
				default:
					if (findType == X509FindType.FindBySubjectKeyIdentifier)
					{
						X509SubjectKeyIdentifierExtension x509SubjectKeyIdentifierExtension = cert.Extensions["2.5.29.14"] as X509SubjectKeyIdentifierExtension;
						if (x509SubjectKeyIdentifierExtension == null || !this.BinaryMatches((byte[])findValue, x509SubjectKeyIdentifierExtension.RawData))
						{
							cert.Reset();
							cert = null;
							return false;
						}
					}
					break;
				}
			}
			if (validOnly && !new X509Chain(false)
			{
				ChainPolicy = 
				{
					RevocationMode = X509RevocationMode.NoCheck,
					RevocationFlag = X509RevocationFlag.ExcludeRoot
				}
			}.Build(cert))
			{
				cert.Reset();
				cert = null;
				return false;
			}
			return cert != null;
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00040E70 File Offset: 0x0003F070
		private bool BinaryMatches(byte[] src, byte[] dst)
		{
			if (src.Length != dst.Length)
			{
				return false;
			}
			for (int i = 0; i < src.Length; i++)
			{
				if (src[i] != dst[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00040EA0 File Offset: 0x0003F0A0
		private uint MapX509StoreFlags(StoreLocation storeLocation, OpenFlags flags)
		{
			uint num = 0U;
			uint num2 = (uint)(flags & (OpenFlags.ReadWrite | OpenFlags.MaxAllowed));
			if (num2 != 0U)
			{
				if (num2 == 2U)
				{
					num |= 4096U;
				}
			}
			else
			{
				num |= 32768U;
			}
			if ((flags & OpenFlags.OpenExistingOnly) == OpenFlags.OpenExistingOnly)
			{
				num |= 16384U;
			}
			if ((flags & OpenFlags.IncludeArchived) == OpenFlags.IncludeArchived)
			{
				num |= 512U;
			}
			if (storeLocation == StoreLocation.LocalMachine)
			{
				num |= 131072U;
			}
			else if (storeLocation == StoreLocation.CurrentUser)
			{
				num |= 65536U;
			}
			return num;
		}

		// Token: 0x04000CF0 RID: 3312
		private SafeCertStoreHandle certStoreHandle = SafeCertStoreHandle.InvalidHandle;

		// Token: 0x04000CF1 RID: 3313
		private string storeName;

		// Token: 0x04000CF2 RID: 3314
		private StoreLocation storeLocation;
	}
}
