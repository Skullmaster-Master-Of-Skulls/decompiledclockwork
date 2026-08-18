using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000466 RID: 1126
	[Serializable]
	public class X509Certificate2 : X509Certificate
	{
		// Token: 0x060029BF RID: 10687 RVA: 0x000BD63E File Offset: 0x000BB83E
		public X509Certificate2()
		{
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000BD651 File Offset: 0x000BB851
		public X509Certificate2(byte[] rawData) : base(rawData)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000BD676 File Offset: 0x000BB876
		public X509Certificate2(byte[] rawData, string password) : base(rawData, password)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000BD69C File Offset: 0x000BB89C
		public X509Certificate2(byte[] rawData, SecureString password) : base(rawData, password)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x000BD6C2 File Offset: 0x000BB8C2
		public X509Certificate2(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags) : base(rawData, password, keyStorageFlags)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x000BD6E9 File Offset: 0x000BB8E9
		public X509Certificate2(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags) : base(rawData, password, keyStorageFlags)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x000BD710 File Offset: 0x000BB910
		public X509Certificate2(string fileName) : base(fileName)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000BD735 File Offset: 0x000BB935
		public X509Certificate2(string fileName, string password) : base(fileName, password)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000BD75B File Offset: 0x000BB95B
		public X509Certificate2(string fileName, SecureString password) : base(fileName, password)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000BD781 File Offset: 0x000BB981
		public X509Certificate2(string fileName, string password, X509KeyStorageFlags keyStorageFlags) : base(fileName, password, keyStorageFlags)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x000BD7A8 File Offset: 0x000BB9A8
		public X509Certificate2(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags) : base(fileName, password, keyStorageFlags)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000BD7CF File Offset: 0x000BB9CF
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public X509Certificate2(IntPtr handle) : base(handle)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x000BD7F4 File Offset: 0x000BB9F4
		public X509Certificate2(X509Certificate certificate) : base(certificate)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000BD819 File Offset: 0x000BBA19
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected X509Certificate2(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x000BD83F File Offset: 0x000BBA3F
		public override string ToString()
		{
			return base.ToString(true);
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x000BD848 File Offset: 0x000BBA48
		public override string ToString(bool verbose)
		{
			if (!verbose || this.m_safeCertContext.IsInvalid)
			{
				return this.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			string newLine = Environment.NewLine;
			string value = newLine + newLine;
			string value2 = newLine + "  ";
			stringBuilder.Append("[Version]");
			stringBuilder.Append(value2);
			stringBuilder.Append("V" + this.Version.ToString());
			stringBuilder.Append(value);
			stringBuilder.Append("[Subject]");
			stringBuilder.Append(value2);
			stringBuilder.Append(this.SubjectName.Name);
			string nameInfo = this.GetNameInfo(X509NameType.SimpleName, false);
			if (nameInfo.Length > 0)
			{
				stringBuilder.Append(value2);
				stringBuilder.Append("Simple Name: ");
				stringBuilder.Append(nameInfo);
			}
			string nameInfo2 = this.GetNameInfo(X509NameType.EmailName, false);
			if (nameInfo2.Length > 0)
			{
				stringBuilder.Append(value2);
				stringBuilder.Append("Email Name: ");
				stringBuilder.Append(nameInfo2);
			}
			string nameInfo3 = this.GetNameInfo(X509NameType.UpnName, false);
			if (nameInfo3.Length > 0)
			{
				stringBuilder.Append(value2);
				stringBuilder.Append("UPN Name: ");
				stringBuilder.Append(nameInfo3);
			}
			string nameInfo4 = this.GetNameInfo(X509NameType.DnsName, false);
			if (nameInfo4.Length > 0)
			{
				stringBuilder.Append(value2);
				stringBuilder.Append("DNS Name: ");
				stringBuilder.Append(nameInfo4);
			}
			stringBuilder.Append(value);
			stringBuilder.Append("[Issuer]");
			stringBuilder.Append(value2);
			stringBuilder.Append(this.IssuerName.Name);
			nameInfo = this.GetNameInfo(X509NameType.SimpleName, true);
			if (nameInfo.Length > 0)
			{
				stringBuilder.Append(value2);
				stringBuilder.Append("Simple Name: ");
				stringBuilder.Append(nameInfo);
			}
			nameInfo2 = this.GetNameInfo(X509NameType.EmailName, true);
			if (nameInfo2.Length > 0)
			{
				stringBuilder.Append(value2);
				stringBuilder.Append("Email Name: ");
				stringBuilder.Append(nameInfo2);
			}
			nameInfo3 = this.GetNameInfo(X509NameType.UpnName, true);
			if (nameInfo3.Length > 0)
			{
				stringBuilder.Append(value2);
				stringBuilder.Append("UPN Name: ");
				stringBuilder.Append(nameInfo3);
			}
			nameInfo4 = this.GetNameInfo(X509NameType.DnsName, true);
			if (nameInfo4.Length > 0)
			{
				stringBuilder.Append(value2);
				stringBuilder.Append("DNS Name: ");
				stringBuilder.Append(nameInfo4);
			}
			stringBuilder.Append(value);
			stringBuilder.Append("[Serial Number]");
			stringBuilder.Append(value2);
			stringBuilder.Append(this.SerialNumber);
			stringBuilder.Append(value);
			stringBuilder.Append("[Not Before]");
			stringBuilder.Append(value2);
			stringBuilder.Append(X509Certificate.FormatDate(this.NotBefore));
			stringBuilder.Append(value);
			stringBuilder.Append("[Not After]");
			stringBuilder.Append(value2);
			stringBuilder.Append(X509Certificate.FormatDate(this.NotAfter));
			stringBuilder.Append(value);
			stringBuilder.Append("[Thumbprint]");
			stringBuilder.Append(value2);
			stringBuilder.Append(this.Thumbprint);
			stringBuilder.Append(value);
			stringBuilder.Append("[Signature Algorithm]");
			stringBuilder.Append(value2);
			stringBuilder.Append(this.SignatureAlgorithm.FriendlyName + "(" + this.SignatureAlgorithm.Value + ")");
			stringBuilder.Append(value);
			stringBuilder.Append("[Public Key]");
			try
			{
				PublicKey publicKey = this.PublicKey;
				string value3 = publicKey.Oid.FriendlyName;
				stringBuilder.Append(value2);
				stringBuilder.Append("Algorithm: ");
				stringBuilder.Append(value3);
				try
				{
					value3 = publicKey.Key.KeySize.ToString();
					stringBuilder.Append(value2);
					stringBuilder.Append("Length: ");
					stringBuilder.Append(value3);
				}
				catch (NotSupportedException)
				{
				}
				value3 = publicKey.EncodedKeyValue.Format(true);
				stringBuilder.Append(value2);
				stringBuilder.Append("Key Blob: ");
				stringBuilder.Append(value3);
				value3 = publicKey.EncodedParameters.Format(true);
				stringBuilder.Append(value2);
				stringBuilder.Append("Parameters: ");
				stringBuilder.Append(value3);
			}
			catch (CryptographicException)
			{
			}
			this.AppendPrivateKeyInfo(stringBuilder);
			X509ExtensionCollection extensions = this.Extensions;
			if (extensions.Count > 0)
			{
				stringBuilder.Append(value);
				stringBuilder.Append("[Extensions]");
				foreach (X509Extension x509Extension in extensions)
				{
					try
					{
						string text = x509Extension.Oid.FriendlyName;
						stringBuilder.Append(newLine);
						stringBuilder.Append("* " + text);
						stringBuilder.Append("(" + x509Extension.Oid.Value + "):");
						text = x509Extension.Format(true);
						stringBuilder.Append(value2);
						stringBuilder.Append(text);
					}
					catch (CryptographicException)
					{
					}
				}
			}
			stringBuilder.Append(newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060029CF RID: 10703 RVA: 0x000BDD68 File Offset: 0x000BBF68
		// (set) Token: 0x060029D0 RID: 10704 RVA: 0x000BDDB0 File Offset: 0x000BBFB0
		public bool Archived
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				uint num = 0U;
				return CAPISafe.CertGetCertificateContextProperty(this.m_safeCertContext, 19U, SafeLocalAllocHandle.InvalidHandle, ref num);
			}
			set
			{
				SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
				if (value)
				{
					safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPIBase.CRYPTOAPI_BLOB))));
				}
				if (!CAPI.CertSetCertificateContextProperty(this.m_safeCertContext, 19U, 0U, safeLocalAllocHandle))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				safeLocalAllocHandle.Dispose();
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x000BDE04 File Offset: 0x000BC004
		public X509ExtensionCollection Extensions
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_extensions == null)
				{
					this.m_extensions = new X509ExtensionCollection(this.m_safeCertContext);
				}
				return this.m_extensions;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x000BDE54 File Offset: 0x000BC054
		// (set) Token: 0x060029D3 RID: 10707 RVA: 0x000BDEDC File Offset: 0x000BC0DC
		public string FriendlyName
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
				uint num = 0U;
				if (!CAPISafe.CertGetCertificateContextProperty(this.m_safeCertContext, 11U, safeLocalAllocHandle, ref num))
				{
					return string.Empty;
				}
				safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
				if (!CAPISafe.CertGetCertificateContextProperty(this.m_safeCertContext, 11U, safeLocalAllocHandle, ref num))
				{
					return string.Empty;
				}
				string result = Marshal.PtrToStringUni(safeLocalAllocHandle.DangerousGetHandle());
				safeLocalAllocHandle.Dispose();
				return result;
			}
			set
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (value == null)
				{
					value = string.Empty;
				}
				X509Certificate2.SetFriendlyNameExtendedProperty(this.m_safeCertContext, value);
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x000BDF18 File Offset: 0x000BC118
		public unsafe X500DistinguishedName IssuerName
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_issuerName == null)
				{
					CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)((void*)this.m_safeCertContext.DangerousGetHandle());
					CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
					this.m_issuerName = new X500DistinguishedName(cert_INFO.Issuer);
				}
				return this.m_issuerName;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060029D5 RID: 10709 RVA: 0x000BDF98 File Offset: 0x000BC198
		public unsafe DateTime NotAfter
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_notAfter == DateTime.MinValue)
				{
					CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)((void*)this.m_safeCertContext.DangerousGetHandle());
					CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
					long fileTime = (long)((ulong)cert_INFO.NotAfter.dwHighDateTime << 32 | (ulong)cert_INFO.NotAfter.dwLowDateTime);
					this.m_notAfter = DateTime.FromFileTime(fileTime);
				}
				return this.m_notAfter;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060029D6 RID: 10710 RVA: 0x000BE03C File Offset: 0x000BC23C
		public unsafe DateTime NotBefore
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_notBefore == DateTime.MinValue)
				{
					CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)((void*)this.m_safeCertContext.DangerousGetHandle());
					CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
					long fileTime = (long)((ulong)cert_INFO.NotBefore.dwHighDateTime << 32 | (ulong)cert_INFO.NotBefore.dwLowDateTime);
					this.m_notBefore = DateTime.FromFileTime(fileTime);
				}
				return this.m_notBefore;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x000BE0E0 File Offset: 0x000BC2E0
		public bool HasPrivateKey
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				uint num = 0U;
				bool flag;
				using (SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle)
				{
					flag = CAPISafe.CertGetCertificateContextProperty(this.m_safeCertContext, 5U, invalidHandle, ref num);
					if (!flag)
					{
						flag = CAPISafe.CertGetCertificateContextProperty(this.m_safeCertContext, 2U, invalidHandle, ref num);
					}
				}
				return flag;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060029D8 RID: 10712 RVA: 0x000BE158 File Offset: 0x000BC358
		// (set) Token: 0x060029D9 RID: 10713 RVA: 0x000BE1F4 File Offset: 0x000BC3F4
		public AsymmetricAlgorithm PrivateKey
		{
			get
			{
				if (!this.HasPrivateKey)
				{
					return null;
				}
				if (this.m_privateKey == null)
				{
					CspParameters cspParameters = new CspParameters();
					if (!X509Certificate2.GetPrivateKeyInfo(this.m_safeCertContext, ref cspParameters))
					{
						return null;
					}
					cspParameters.Flags |= CspProviderFlags.UseExistingKey;
					uint algorithmId = this.PublicKey.AlgorithmId;
					if (algorithmId != 8704U)
					{
						if (algorithmId != 9216U && algorithmId != 41984U)
						{
							throw new NotSupportedException(SR.GetString("NotSupported_KeyAlgorithm"));
						}
						this.m_privateKey = new RSACryptoServiceProvider(cspParameters);
					}
					else
					{
						this.m_privateKey = new DSACryptoServiceProvider(cspParameters);
					}
				}
				return this.m_privateKey;
			}
			set
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				ICspAsymmetricAlgorithm cspAsymmetricAlgorithm = value as ICspAsymmetricAlgorithm;
				if (value != null && cspAsymmetricAlgorithm == null)
				{
					throw new NotSupportedException(SR.GetString("NotSupported_InvalidKeyImpl"));
				}
				if (cspAsymmetricAlgorithm != null)
				{
					if (cspAsymmetricAlgorithm.CspKeyContainerInfo == null)
					{
						throw new ArgumentException("CspKeyContainerInfo");
					}
					if (X509Certificate2.s_publicKeyOffset == 0)
					{
						X509Certificate2.s_publicKeyOffset = Marshal.SizeOf(typeof(CAPIBase.BLOBHEADER));
					}
					ICspAsymmetricAlgorithm cspAsymmetricAlgorithm2 = this.PublicKey.Key as ICspAsymmetricAlgorithm;
					byte[] array = cspAsymmetricAlgorithm2.ExportCspBlob(false);
					byte[] array2 = cspAsymmetricAlgorithm.ExportCspBlob(false);
					if (array == null || array2 == null || array.Length != array2.Length || array.Length <= X509Certificate2.s_publicKeyOffset)
					{
						throw new CryptographicUnexpectedOperationException(SR.GetString("Cryptography_X509_KeyMismatch"));
					}
					for (int i = X509Certificate2.s_publicKeyOffset; i < array.Length; i++)
					{
						if (array[i] != array2[i])
						{
							throw new CryptographicUnexpectedOperationException(SR.GetString("Cryptography_X509_KeyMismatch"));
						}
					}
				}
				X509Certificate2.SetPrivateKeyProperty(this.m_safeCertContext, cspAsymmetricAlgorithm);
				this.m_privateKey = value;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x060029DA RID: 10714 RVA: 0x000BE300 File Offset: 0x000BC500
		public PublicKey PublicKey
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_publicKey == null)
				{
					string keyAlgorithm = this.GetKeyAlgorithm();
					byte[] keyAlgorithmParameters = this.GetKeyAlgorithmParameters();
					byte[] publicKey = this.GetPublicKey();
					Oid oid = new Oid(keyAlgorithm, OidGroup.PublicKeyAlgorithm, true);
					this.m_publicKey = new PublicKey(oid, new AsnEncodedData(oid, keyAlgorithmParameters), new AsnEncodedData(oid, publicKey));
				}
				return this.m_publicKey;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x000BE375 File Offset: 0x000BC575
		public byte[] RawData
		{
			get
			{
				return this.GetRawCertData();
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x000BE37D File Offset: 0x000BC57D
		public string SerialNumber
		{
			get
			{
				return this.GetSerialNumberString();
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x000BE388 File Offset: 0x000BC588
		public unsafe X500DistinguishedName SubjectName
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_subjectName == null)
				{
					CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)((void*)this.m_safeCertContext.DangerousGetHandle());
					CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
					this.m_subjectName = new X500DistinguishedName(cert_INFO.Subject);
				}
				return this.m_subjectName;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060029DE RID: 10718 RVA: 0x000BE408 File Offset: 0x000BC608
		public Oid SignatureAlgorithm
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_signatureAlgorithm == null)
				{
					this.m_signatureAlgorithm = X509Certificate2.GetSignatureAlgorithm(this.m_safeCertContext);
				}
				return this.m_signatureAlgorithm;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x000BE456 File Offset: 0x000BC656
		public string Thumbprint
		{
			get
			{
				return this.GetCertHashString();
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060029E0 RID: 10720 RVA: 0x000BE460 File Offset: 0x000BC660
		public int Version
		{
			get
			{
				if (this.m_safeCertContext.IsInvalid)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
				}
				if (this.m_version == 0)
				{
					this.m_version = (int)X509Certificate2.GetVersion(this.m_safeCertContext);
				}
				return this.m_version;
			}
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000BE4B0 File Offset: 0x000BC6B0
		public unsafe string GetNameInfo(X509NameType nameType, bool forIssuer)
		{
			uint dwFlags = forIssuer ? 1U : 0U;
			uint num = X509Utils.MapNameType(nameType);
			if (num == 1U)
			{
				return CAPI.GetCertNameInfo(this.m_safeCertContext, dwFlags, num);
			}
			if (num == 4U)
			{
				return CAPI.GetCertNameInfo(this.m_safeCertContext, dwFlags, num);
			}
			string text = string.Empty;
			CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)((void*)this.m_safeCertContext.DangerousGetHandle());
			CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
			IntPtr[] array = new IntPtr[]
			{
				CAPISafe.CertFindExtension(forIssuer ? "2.5.29.8" : "2.5.29.7", cert_INFO.cExtension, cert_INFO.rgExtension),
				CAPISafe.CertFindExtension(forIssuer ? "2.5.29.18" : "2.5.29.17", cert_INFO.cExtension, cert_INFO.rgExtension)
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != IntPtr.Zero)
				{
					CAPIBase.CERT_EXTENSION cert_EXTENSION = (CAPIBase.CERT_EXTENSION)Marshal.PtrToStructure(array[i], typeof(CAPIBase.CERT_EXTENSION));
					byte[] array2 = new byte[cert_EXTENSION.Value.cbData];
					Marshal.Copy(cert_EXTENSION.Value.pbData, array2, 0, array2.Length);
					uint num2 = 0U;
					SafeLocalAllocHandle safeLocalAllocHandle = null;
					SafeLocalAllocHandle safeLocalAllocHandle2 = X509Utils.StringToAnsiPtr(cert_EXTENSION.pszObjId);
					bool flag = CAPI.DecodeObject(safeLocalAllocHandle2.DangerousGetHandle(), array2, out safeLocalAllocHandle, out num2);
					safeLocalAllocHandle2.Dispose();
					if (flag)
					{
						CAPIBase.CERT_ALT_NAME_INFO cert_ALT_NAME_INFO = (CAPIBase.CERT_ALT_NAME_INFO)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CERT_ALT_NAME_INFO));
						int num3 = 0;
						while ((long)num3 < (long)((ulong)cert_ALT_NAME_INFO.cAltEntry))
						{
							IntPtr ptr = new IntPtr((long)cert_ALT_NAME_INFO.rgAltEntry + (long)(num3 * Marshal.SizeOf(typeof(CAPIBase.CERT_ALT_NAME_ENTRY))));
							CAPIBase.CERT_ALT_NAME_ENTRY cert_ALT_NAME_ENTRY = (CAPIBase.CERT_ALT_NAME_ENTRY)Marshal.PtrToStructure(ptr, typeof(CAPIBase.CERT_ALT_NAME_ENTRY));
							switch (num)
							{
							case 6U:
								if (cert_ALT_NAME_ENTRY.dwAltNameChoice == 3U)
								{
									text = Marshal.PtrToStringUni(cert_ALT_NAME_ENTRY.Value.pwszDNSName);
								}
								break;
							case 7U:
								if (cert_ALT_NAME_ENTRY.dwAltNameChoice == 7U)
								{
									text = Marshal.PtrToStringUni(cert_ALT_NAME_ENTRY.Value.pwszURL);
								}
								break;
							case 8U:
								if (cert_ALT_NAME_ENTRY.dwAltNameChoice == 1U)
								{
									CAPIBase.CERT_OTHER_NAME cert_OTHER_NAME = (CAPIBase.CERT_OTHER_NAME)Marshal.PtrToStructure(cert_ALT_NAME_ENTRY.Value.pOtherName, typeof(CAPIBase.CERT_OTHER_NAME));
									if (cert_OTHER_NAME.pszObjId == "1.3.6.1.4.1.311.20.2.3")
									{
										uint num4 = 0U;
										SafeLocalAllocHandle safeLocalAllocHandle3 = null;
										flag = CAPI.DecodeObject(new IntPtr(24L), X509Utils.PtrToByte(cert_OTHER_NAME.Value.pbData, cert_OTHER_NAME.Value.cbData), out safeLocalAllocHandle3, out num4);
										if (flag)
										{
											CAPIBase.CERT_NAME_VALUE cert_NAME_VALUE = (CAPIBase.CERT_NAME_VALUE)Marshal.PtrToStructure(safeLocalAllocHandle3.DangerousGetHandle(), typeof(CAPIBase.CERT_NAME_VALUE));
											if (X509Utils.IsCertRdnCharString(cert_NAME_VALUE.dwValueType))
											{
												text = Marshal.PtrToStringUni(cert_NAME_VALUE.Value.pbData);
											}
											safeLocalAllocHandle3.Dispose();
										}
									}
								}
								break;
							}
							num3++;
						}
						safeLocalAllocHandle.Dispose();
					}
				}
			}
			if (nameType == X509NameType.DnsName && (text == null || text.Length == 0))
			{
				text = CAPI.GetCertNameInfo(this.m_safeCertContext, dwFlags, 3U);
			}
			return text;
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000BE7DF File Offset: 0x000BC9DF
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void Import(byte[] rawData)
		{
			this.Reset();
			base.Import(rawData);
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000BE7FF File Offset: 0x000BC9FF
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			base.Import(rawData, password, keyStorageFlags);
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000BE821 File Offset: 0x000BCA21
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void Import(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			base.Import(rawData, password, keyStorageFlags);
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000BE843 File Offset: 0x000BCA43
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void Import(string fileName)
		{
			this.Reset();
			base.Import(fileName);
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x000BE863 File Offset: 0x000BCA63
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			base.Import(fileName, password, keyStorageFlags);
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x000BE885 File Offset: 0x000BCA85
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void Import(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			this.Reset();
			base.Import(fileName, password, keyStorageFlags);
			this.m_safeCertContext = CAPI.CertDuplicateCertificateContext(base.Handle);
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000BE8A8 File Offset: 0x000BCAA8
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public override void Reset()
		{
			this.m_version = 0;
			this.m_notBefore = DateTime.MinValue;
			this.m_notAfter = DateTime.MinValue;
			this.m_privateKey = null;
			this.m_publicKey = null;
			this.m_extensions = null;
			this.m_signatureAlgorithm = null;
			this.m_subjectName = null;
			this.m_issuerName = null;
			if (!this.m_safeCertContext.IsInvalid)
			{
				this.m_safeCertContext.Dispose();
				this.m_safeCertContext = SafeCertContextHandle.InvalidHandle;
			}
			base.Reset();
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x000BE928 File Offset: 0x000BCB28
		public bool Verify()
		{
			if (this.m_safeCertContext.IsInvalid)
			{
				throw new CryptographicException(SR.GetString("Cryptography_InvalidHandle"), "m_safeCertContext");
			}
			int num = X509Utils.VerifyCertificate(this.CertContext, null, null, X509RevocationMode.Online, X509RevocationFlag.ExcludeRoot, DateTime.Now, new TimeSpan(0, 0, 0), null, new IntPtr(1L), IntPtr.Zero);
			return num == 0;
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000BE988 File Offset: 0x000BCB88
		public static X509ContentType GetCertContentType(byte[] rawData)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Arg_EmptyOrNullArray"), "rawData");
			}
			uint contentType = X509Certificate2.QueryCertBlobType(rawData);
			return X509Utils.MapContentType(contentType);
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000BE9C0 File Offset: 0x000BCBC0
		public static X509ContentType GetCertContentType(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			string fullPath = Path.GetFullPath(fileName);
			new FileIOPermission(FileIOPermissionAccess.Read, fullPath).Demand();
			uint contentType = X509Certificate2.QueryCertFileType(fileName);
			return X509Utils.MapContentType(contentType);
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060029EC RID: 10732 RVA: 0x000BE9FB File Offset: 0x000BCBFB
		internal new SafeCertContextHandle CertContext
		{
			get
			{
				return this.m_safeCertContext;
			}
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000BEA04 File Offset: 0x000BCC04
		internal static bool GetPrivateKeyInfo(SafeCertContextHandle safeCertContext, ref CspParameters parameters)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			uint num = 0U;
			if (!CAPISafe.CertGetCertificateContextProperty(safeCertContext, 2U, safeLocalAllocHandle, ref num))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error == -2146885628)
				{
					return false;
				}
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			else
			{
				safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num)));
				if (CAPISafe.CertGetCertificateContextProperty(safeCertContext, 2U, safeLocalAllocHandle, ref num))
				{
					CAPIBase.CRYPT_KEY_PROV_INFO crypt_KEY_PROV_INFO = (CAPIBase.CRYPT_KEY_PROV_INFO)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CRYPT_KEY_PROV_INFO));
					parameters.ProviderName = crypt_KEY_PROV_INFO.pwszProvName;
					parameters.KeyContainerName = crypt_KEY_PROV_INFO.pwszContainerName;
					parameters.ProviderType = (int)crypt_KEY_PROV_INFO.dwProvType;
					parameters.KeyNumber = (int)crypt_KEY_PROV_INFO.dwKeySpec;
					parameters.Flags = (((crypt_KEY_PROV_INFO.dwFlags & 32U) == 32U) ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
					safeLocalAllocHandle.Dispose();
					return true;
				}
				int lastWin32Error2 = Marshal.GetLastWin32Error();
				if (lastWin32Error2 == -2146885628)
				{
					return false;
				}
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000BEAE8 File Offset: 0x000BCCE8
		private void AppendPrivateKeyInfo(StringBuilder sb)
		{
			if (!this.HasPrivateKey)
			{
				return;
			}
			CspKeyContainerInfo cspKeyContainerInfo = null;
			try
			{
				CspParameters parameters = new CspParameters();
				if (X509Certificate2.GetPrivateKeyInfo(this.m_safeCertContext, ref parameters))
				{
					cspKeyContainerInfo = new CspKeyContainerInfo(parameters);
				}
			}
			catch (SecurityException)
			{
			}
			catch (CryptographicException)
			{
			}
			sb.Append(Environment.NewLine + Environment.NewLine + "[Private Key]");
			if (cspKeyContainerInfo == null)
			{
				return;
			}
			sb.Append(Environment.NewLine + "  Key Store: ");
			sb.Append(cspKeyContainerInfo.MachineKeyStore ? "Machine" : "User");
			sb.Append(Environment.NewLine + "  Provider Name: ");
			sb.Append(cspKeyContainerInfo.ProviderName);
			sb.Append(Environment.NewLine + "  Provider type: ");
			sb.Append(cspKeyContainerInfo.ProviderType);
			sb.Append(Environment.NewLine + "  Key Spec: ");
			sb.Append(cspKeyContainerInfo.KeyNumber);
			sb.Append(Environment.NewLine + "  Key Container Name: ");
			sb.Append(cspKeyContainerInfo.KeyContainerName);
			try
			{
				string uniqueKeyContainerName = cspKeyContainerInfo.UniqueKeyContainerName;
				sb.Append(Environment.NewLine + "  Unique Key Container Name: ");
				sb.Append(uniqueKeyContainerName);
			}
			catch (CryptographicException)
			{
			}
			catch (NotSupportedException)
			{
			}
			try
			{
				bool value = cspKeyContainerInfo.HardwareDevice;
				sb.Append(Environment.NewLine + "  Hardware Device: ");
				sb.Append(value);
			}
			catch (CryptographicException)
			{
			}
			try
			{
				bool value = cspKeyContainerInfo.Removable;
				sb.Append(Environment.NewLine + "  Removable: ");
				sb.Append(value);
			}
			catch (CryptographicException)
			{
			}
			try
			{
				bool value = cspKeyContainerInfo.Protected;
				sb.Append(Environment.NewLine + "  Protected: ");
				sb.Append(value);
			}
			catch (CryptographicException)
			{
			}
			catch (NotSupportedException)
			{
			}
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000BED1C File Offset: 0x000BCF1C
		private unsafe static Oid GetSignatureAlgorithm(SafeCertContextHandle safeCertContextHandle)
		{
			CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)((void*)safeCertContextHandle.DangerousGetHandle());
			CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
			return new Oid(cert_INFO.SignatureAlgorithm.pszObjId, OidGroup.SignatureAlgorithm, false);
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000BED68 File Offset: 0x000BCF68
		private unsafe static uint GetVersion(SafeCertContextHandle safeCertContextHandle)
		{
			CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)((void*)safeCertContextHandle.DangerousGetHandle());
			CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
			return cert_INFO.dwVersion + 1U;
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000BEDAC File Offset: 0x000BCFAC
		private unsafe static uint QueryCertBlobType(byte[] rawData)
		{
			uint result = 0U;
			if (!CAPI.CryptQueryObject(2U, rawData, 16382U, 14U, 0U, IntPtr.Zero, new IntPtr((void*)(&result)), IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000BEDFC File Offset: 0x000BCFFC
		private unsafe static uint QueryCertFileType(string fileName)
		{
			uint result = 0U;
			if (!CAPI.CryptQueryObject(1U, fileName, 16382U, 14U, 0U, IntPtr.Zero, new IntPtr((void*)(&result)), IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000BEE4C File Offset: 0x000BD04C
		private unsafe static void SetFriendlyNameExtendedProperty(SafeCertContextHandle safeCertContextHandle, string name)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = X509Utils.StringToUniPtr(name);
			using (safeLocalAllocHandle)
			{
				CAPIBase.CRYPTOAPI_BLOB cryptoapi_BLOB = default(CAPIBase.CRYPTOAPI_BLOB);
				cryptoapi_BLOB.cbData = (uint)(2 * (name.Length + 1));
				cryptoapi_BLOB.pbData = safeLocalAllocHandle.DangerousGetHandle();
				if (!CAPI.CertSetCertificateContextProperty(safeCertContextHandle, 11U, 0U, new IntPtr((void*)(&cryptoapi_BLOB))))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000BEEC4 File Offset: 0x000BD0C4
		private static void SetPrivateKeyProperty(SafeCertContextHandle safeCertContextHandle, ICspAsymmetricAlgorithm asymmetricAlgorithm)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			if (asymmetricAlgorithm != null)
			{
				CAPIBase.CRYPT_KEY_PROV_INFO crypt_KEY_PROV_INFO = default(CAPIBase.CRYPT_KEY_PROV_INFO);
				crypt_KEY_PROV_INFO.pwszContainerName = asymmetricAlgorithm.CspKeyContainerInfo.KeyContainerName;
				crypt_KEY_PROV_INFO.pwszProvName = asymmetricAlgorithm.CspKeyContainerInfo.ProviderName;
				crypt_KEY_PROV_INFO.dwProvType = (uint)asymmetricAlgorithm.CspKeyContainerInfo.ProviderType;
				crypt_KEY_PROV_INFO.dwFlags = (asymmetricAlgorithm.CspKeyContainerInfo.MachineKeyStore ? 32U : 0U);
				crypt_KEY_PROV_INFO.cProvParam = 0U;
				crypt_KEY_PROV_INFO.rgProvParam = IntPtr.Zero;
				crypt_KEY_PROV_INFO.dwKeySpec = (uint)asymmetricAlgorithm.CspKeyContainerInfo.KeyNumber;
				safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPIBase.CRYPT_KEY_PROV_INFO))));
				Marshal.StructureToPtr(crypt_KEY_PROV_INFO, safeLocalAllocHandle.DangerousGetHandle(), false);
			}
			try
			{
				if (!CAPI.CertSetCertificateContextProperty(safeCertContextHandle, 2U, 0U, safeLocalAllocHandle))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				if (!safeLocalAllocHandle.IsInvalid)
				{
					Marshal.DestroyStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CRYPT_KEY_PROV_INFO));
					safeLocalAllocHandle.Dispose();
				}
			}
		}

		// Token: 0x040025C2 RID: 9666
		private int m_version;

		// Token: 0x040025C3 RID: 9667
		private DateTime m_notBefore;

		// Token: 0x040025C4 RID: 9668
		private DateTime m_notAfter;

		// Token: 0x040025C5 RID: 9669
		private AsymmetricAlgorithm m_privateKey;

		// Token: 0x040025C6 RID: 9670
		private PublicKey m_publicKey;

		// Token: 0x040025C7 RID: 9671
		private X509ExtensionCollection m_extensions;

		// Token: 0x040025C8 RID: 9672
		private Oid m_signatureAlgorithm;

		// Token: 0x040025C9 RID: 9673
		private X500DistinguishedName m_subjectName;

		// Token: 0x040025CA RID: 9674
		private X500DistinguishedName m_issuerName;

		// Token: 0x040025CB RID: 9675
		private SafeCertContextHandle m_safeCertContext = SafeCertContextHandle.InvalidHandle;

		// Token: 0x040025CC RID: 9676
		private static int s_publicKeyOffset;

		// Token: 0x040025CD RID: 9677
		internal const X509KeyStorageFlags KeyStorageFlags47 = X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet;

		// Token: 0x040025CE RID: 9678
		internal new const X509KeyStorageFlags KeyStorageFlagsAll = X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet;
	}
}
