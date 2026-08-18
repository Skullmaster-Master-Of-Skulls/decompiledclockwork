using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000085 RID: 133
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SignedCms
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x00019EE2 File Offset: 0x000180E2
		public SignedCms() : this(SubjectIdentifierType.IssuerAndSerialNumber, new ContentInfo(Oid.FromOidValue("1.2.840.113549.1.7.1", OidGroup.ExtensionOrAttribute), new byte[0]), false)
		{
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00019F02 File Offset: 0x00018102
		public SignedCms(SubjectIdentifierType signerIdentifierType) : this(signerIdentifierType, new ContentInfo(Oid.FromOidValue("1.2.840.113549.1.7.1", OidGroup.ExtensionOrAttribute), new byte[0]), false)
		{
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00019F22 File Offset: 0x00018122
		public SignedCms(ContentInfo contentInfo) : this(SubjectIdentifierType.IssuerAndSerialNumber, contentInfo, false)
		{
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00019F2D File Offset: 0x0001812D
		public SignedCms(SubjectIdentifierType signerIdentifierType, ContentInfo contentInfo) : this(signerIdentifierType, contentInfo, false)
		{
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00019F38 File Offset: 0x00018138
		public SignedCms(ContentInfo contentInfo, bool detached) : this(SubjectIdentifierType.IssuerAndSerialNumber, contentInfo, detached)
		{
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00019F44 File Offset: 0x00018144
		[SecuritySafeCritical]
		public SignedCms(SubjectIdentifierType signerIdentifierType, ContentInfo contentInfo, bool detached)
		{
			if (contentInfo == null)
			{
				throw new ArgumentNullException("contentInfo");
			}
			if (contentInfo.Content == null)
			{
				throw new ArgumentNullException("contentInfo.Content");
			}
			if (signerIdentifierType != SubjectIdentifierType.SubjectKeyIdentifier && signerIdentifierType != SubjectIdentifierType.IssuerAndSerialNumber && signerIdentifierType != SubjectIdentifierType.NoSignature)
			{
				signerIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
			}
			this.m_safeCryptMsgHandle = SafeCryptMsgHandle.InvalidHandle;
			this.m_signerIdentifierType = signerIdentifierType;
			this.m_version = 0;
			this.m_contentInfo = contentInfo;
			this.m_detached = detached;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00019FAE File Offset: 0x000181AE
		public int Version
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
				{
					return this.m_version;
				}
				return (int)PkcsUtils.GetVersion(this.m_safeCryptMsgHandle);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00019FD7 File Offset: 0x000181D7
		public ContentInfo ContentInfo
		{
			get
			{
				return this.m_contentInfo;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00019FDF File Offset: 0x000181DF
		public bool Detached
		{
			get
			{
				return this.m_detached;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00019FE7 File Offset: 0x000181E7
		public X509Certificate2Collection Certificates
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
				{
					return new X509Certificate2Collection();
				}
				return PkcsUtils.GetCertificates(this.m_safeCryptMsgHandle);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0001A00F File Offset: 0x0001820F
		public SignerInfoCollection SignerInfos
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
				{
					return new SignerInfoCollection();
				}
				return new SignerInfoCollection(this);
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001A032 File Offset: 0x00018232
		[SecuritySafeCritical]
		public byte[] Encode()
		{
			if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
			{
				throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_MessageNotSigned"));
			}
			return PkcsUtils.GetMessage(this.m_safeCryptMsgHandle);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001A064 File Offset: 0x00018264
		[SecuritySafeCritical]
		public void Decode(byte[] encodedMessage)
		{
			if (encodedMessage == null)
			{
				throw new ArgumentNullException("encodedMessage");
			}
			if (this.m_safeCryptMsgHandle != null && !this.m_safeCryptMsgHandle.IsInvalid)
			{
				this.m_safeCryptMsgHandle.Dispose();
			}
			this.m_safeCryptMsgHandle = SignedCms.OpenToDecode(encodedMessage, this.ContentInfo, this.Detached);
			if (!this.Detached)
			{
				Oid contentType = PkcsUtils.GetContentType(this.m_safeCryptMsgHandle);
				byte[] content = PkcsUtils.GetContent(this.m_safeCryptMsgHandle);
				this.m_contentInfo = new ContentInfo(contentType, content);
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001A0E4 File Offset: 0x000182E4
		public void ComputeSignature()
		{
			this.ComputeSignature(new CmsSigner(this.m_signerIdentifierType), true);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001A0F8 File Offset: 0x000182F8
		public void ComputeSignature(CmsSigner signer)
		{
			this.ComputeSignature(signer, true);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001A102 File Offset: 0x00018302
		[SecuritySafeCritical]
		private static int SafeGetLastWin32Error()
		{
			return Marshal.GetLastWin32Error();
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001A10C File Offset: 0x0001830C
		[SecuritySafeCritical]
		public void ComputeSignature(CmsSigner signer, bool silent)
		{
			if (signer == null)
			{
				throw new ArgumentNullException("signer");
			}
			if (this.ContentInfo.Content.Length == 0)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Sign_Empty_Content"));
			}
			if (SubjectIdentifierType.NoSignature == signer.SignerIdentifierType)
			{
				if (this.m_safeCryptMsgHandle != null && !this.m_safeCryptMsgHandle.IsInvalid)
				{
					throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Sign_No_Signature_First_Signer"));
				}
				this.Sign(signer, silent);
				return;
			}
			else
			{
				if (signer.Certificate == null)
				{
					if (silent)
					{
						throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_RecipientCertificateNotFound"));
					}
					signer.Certificate = PkcsUtils.SelectSignerCertificate();
				}
				if (!signer.Certificate.HasPrivateKey)
				{
					throw new CryptographicException(-2146893811);
				}
				CspParameters parameters = new CspParameters();
				if (X509Utils.GetPrivateKeyInfo(X509Utils.GetCertContext(signer.Certificate), ref parameters))
				{
					KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
					KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Open | KeyContainerPermissionFlags.Sign);
					keyContainerPermission.AccessEntries.Add(accessEntry);
					keyContainerPermission.Demand();
				}
				if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
				{
					this.Sign(signer, silent);
					return;
				}
				this.CoSign(signer, silent);
				return;
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001A220 File Offset: 0x00018420
		[SecuritySafeCritical]
		public unsafe void RemoveSignature(int index)
		{
			if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
			{
				throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_MessageNotSigned"));
			}
			uint num = 0U;
			uint num2 = (uint)Marshal.SizeOf(typeof(uint));
			if (!CAPI.CAPISafe.CryptMsgGetParam(this.m_safeCryptMsgHandle, 5U, 0U, new IntPtr((void*)(&num)), new IntPtr((void*)(&num2))))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			if (index < 0 || index >= (int)num)
			{
				throw new ArgumentOutOfRangeException("index", SecurityResources.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (!CAPI.CryptMsgControl(this.m_safeCryptMsgHandle, 0U, 7U, new IntPtr((void*)(&index))))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001A2CD File Offset: 0x000184CD
		[SecuritySafeCritical]
		public void RemoveSignature(SignerInfo signerInfo)
		{
			if (signerInfo == null)
			{
				throw new ArgumentNullException("signerInfo");
			}
			this.RemoveSignature(PkcsUtils.GetSignerIndex(this.m_safeCryptMsgHandle, signerInfo, 0));
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001A2F0 File Offset: 0x000184F0
		public void CheckSignature(bool verifySignatureOnly)
		{
			this.CheckSignature(new X509Certificate2Collection(), verifySignatureOnly);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001A300 File Offset: 0x00018500
		[SecuritySafeCritical]
		public void CheckSignature(X509Certificate2Collection extraStore, bool verifySignatureOnly)
		{
			if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
			{
				throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_MessageNotSigned"));
			}
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			SignedCms.CheckSignatures(this.SignerInfos, extraStore, verifySignatureOnly);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001A34D File Offset: 0x0001854D
		[SecuritySafeCritical]
		public void CheckHash()
		{
			if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
			{
				throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_MessageNotSigned"));
			}
			SignedCms.CheckHashes(this.SignerInfos);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001A37F File Offset: 0x0001857F
		[SecurityCritical]
		internal SafeCryptMsgHandle GetCryptMsgHandle()
		{
			return this.m_safeCryptMsgHandle;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001A388 File Offset: 0x00018588
		[SecuritySafeCritical]
		internal void ReopenToDecode()
		{
			byte[] message = PkcsUtils.GetMessage(this.m_safeCryptMsgHandle);
			if (this.m_safeCryptMsgHandle != null && !this.m_safeCryptMsgHandle.IsInvalid)
			{
				this.m_safeCryptMsgHandle.Dispose();
			}
			this.m_safeCryptMsgHandle = SignedCms.OpenToDecode(message, this.ContentInfo, this.Detached);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001A3DC File Offset: 0x000185DC
		[SecuritySafeCritical]
		private unsafe void Sign(CmsSigner signer, bool silent)
		{
			CAPI.CMSG_SIGNED_ENCODE_INFO cmsg_SIGNED_ENCODE_INFO = new CAPI.CMSG_SIGNED_ENCODE_INFO(Marshal.SizeOf(typeof(CAPI.CMSG_SIGNED_ENCODE_INFO)));
			SafeCryptProvHandle safeCryptProvHandle;
			CAPI.CMSG_SIGNER_ENCODE_INFO cmsg_SIGNER_ENCODE_INFO = PkcsUtils.CreateSignerEncodeInfo(signer, silent, out safeCryptProvHandle);
			byte[] encodedMessage = null;
			SafeCryptMsgHandle safeCryptMsgHandle;
			try
			{
				SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CMSG_SIGNER_ENCODE_INFO))));
				try
				{
					Marshal.StructureToPtr(cmsg_SIGNER_ENCODE_INFO, safeLocalAllocHandle.DangerousGetHandle(), false);
					X509Certificate2Collection x509Certificate2Collection = PkcsUtils.CreateBagOfCertificates(signer);
					SafeLocalAllocHandle safeLocalAllocHandle2 = PkcsUtils.CreateEncodedCertBlob(x509Certificate2Collection);
					cmsg_SIGNED_ENCODE_INFO.cSigners = 1U;
					cmsg_SIGNED_ENCODE_INFO.rgSigners = safeLocalAllocHandle.DangerousGetHandle();
					cmsg_SIGNED_ENCODE_INFO.cCertEncoded = (uint)x509Certificate2Collection.Count;
					if (x509Certificate2Collection.Count > 0)
					{
						cmsg_SIGNED_ENCODE_INFO.rgCertEncoded = safeLocalAllocHandle2.DangerousGetHandle();
					}
					if (string.Compare(this.ContentInfo.ContentType.Value, "1.2.840.113549.1.7.1", StringComparison.OrdinalIgnoreCase) == 0)
					{
						safeCryptMsgHandle = CAPI.CryptMsgOpenToEncode(65537U, this.Detached ? 4U : 0U, 2U, new IntPtr((void*)(&cmsg_SIGNED_ENCODE_INFO)), IntPtr.Zero, IntPtr.Zero);
					}
					else
					{
						safeCryptMsgHandle = CAPI.CryptMsgOpenToEncode(65537U, this.Detached ? 4U : 0U, 2U, new IntPtr((void*)(&cmsg_SIGNED_ENCODE_INFO)), this.ContentInfo.ContentType.Value, IntPtr.Zero);
					}
					if (safeCryptMsgHandle == null || safeCryptMsgHandle.IsInvalid)
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					if (this.ContentInfo.Content.Length != 0 && !CAPI.CAPISafe.CryptMsgUpdate(safeCryptMsgHandle, this.ContentInfo.pContent, (uint)this.ContentInfo.Content.Length, true))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
					encodedMessage = PkcsUtils.GetContent(safeCryptMsgHandle);
					safeCryptMsgHandle.Dispose();
					safeLocalAllocHandle2.Dispose();
				}
				finally
				{
					Marshal.DestroyStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPI.CMSG_SIGNER_ENCODE_INFO));
					safeLocalAllocHandle.Dispose();
				}
			}
			finally
			{
				cmsg_SIGNER_ENCODE_INFO.Dispose();
				safeCryptProvHandle.Dispose();
			}
			safeCryptMsgHandle = SignedCms.OpenToDecode(encodedMessage, this.ContentInfo, this.Detached);
			if (this.m_safeCryptMsgHandle != null && !this.m_safeCryptMsgHandle.IsInvalid)
			{
				this.m_safeCryptMsgHandle.Dispose();
			}
			this.m_safeCryptMsgHandle = safeCryptMsgHandle;
			GC.KeepAlive(signer);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001A610 File Offset: 0x00018810
		[SecuritySafeCritical]
		private void CoSign(CmsSigner signer, bool silent)
		{
			SafeCryptProvHandle safeCryptProvHandle;
			CAPI.CMSG_SIGNER_ENCODE_INFO cmsg_SIGNER_ENCODE_INFO = PkcsUtils.CreateSignerEncodeInfo(signer, silent, out safeCryptProvHandle);
			try
			{
				SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CMSG_SIGNER_ENCODE_INFO))));
				try
				{
					Marshal.StructureToPtr(cmsg_SIGNER_ENCODE_INFO, safeLocalAllocHandle.DangerousGetHandle(), false);
					if (!CAPI.CryptMsgControl(this.m_safeCryptMsgHandle, 0U, 6U, safeLocalAllocHandle.DangerousGetHandle()))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
				}
				finally
				{
					Marshal.DestroyStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPI.CMSG_SIGNER_ENCODE_INFO));
					safeLocalAllocHandle.Dispose();
				}
			}
			finally
			{
				cmsg_SIGNER_ENCODE_INFO.Dispose();
				safeCryptProvHandle.Dispose();
			}
			PkcsUtils.AddCertsToMessage(this.m_safeCryptMsgHandle, this.Certificates, PkcsUtils.CreateBagOfCertificates(signer));
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001A6D8 File Offset: 0x000188D8
		[SecuritySafeCritical]
		private static SafeCryptMsgHandle OpenToDecode(byte[] encodedMessage, ContentInfo contentInfo, bool detached)
		{
			SafeCryptMsgHandle safeCryptMsgHandle = CAPI.CAPISafe.CryptMsgOpenToDecode(65537U, detached ? 4U : 0U, 0U, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (safeCryptMsgHandle == null || safeCryptMsgHandle.IsInvalid)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			if (!CAPI.CAPISafe.CryptMsgUpdate(safeCryptMsgHandle, encodedMessage, (uint)encodedMessage.Length, true))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			if (2U != PkcsUtils.GetMessageType(safeCryptMsgHandle))
			{
				throw new CryptographicException(-2146889724);
			}
			if (detached)
			{
				byte[] content = contentInfo.Content;
				if (content != null && content.Length != 0 && !CAPI.CAPISafe.CryptMsgUpdate(safeCryptMsgHandle, content, (uint)content.Length, true))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			return safeCryptMsgHandle;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001A774 File Offset: 0x00018974
		private static void CheckSignatures(SignerInfoCollection signers, X509Certificate2Collection extraStore, bool verifySignatureOnly)
		{
			if (signers == null || signers.Count < 1)
			{
				throw new CryptographicException(-2146885618);
			}
			foreach (SignerInfo signerInfo in signers)
			{
				signerInfo.CheckSignature(extraStore, verifySignatureOnly);
				if (signerInfo.CounterSignerInfos.Count > 0)
				{
					SignedCms.CheckSignatures(signerInfo.CounterSignerInfos, extraStore, verifySignatureOnly);
				}
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001A7D4 File Offset: 0x000189D4
		private static void CheckHashes(SignerInfoCollection signers)
		{
			if (signers == null || signers.Count < 1)
			{
				throw new CryptographicException(-2146885618);
			}
			foreach (SignerInfo signerInfo in signers)
			{
				if (signerInfo.SignerIdentifier.Type == SubjectIdentifierType.NoSignature)
				{
					signerInfo.CheckHash();
				}
			}
		}

		// Token: 0x04000512 RID: 1298
		[SecurityCritical]
		private SafeCryptMsgHandle m_safeCryptMsgHandle;

		// Token: 0x04000513 RID: 1299
		private int m_version;

		// Token: 0x04000514 RID: 1300
		private SubjectIdentifierType m_signerIdentifierType;

		// Token: 0x04000515 RID: 1301
		private ContentInfo m_contentInfo;

		// Token: 0x04000516 RID: 1302
		private bool m_detached;
	}
}
