using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200006A RID: 106
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EnvelopedCms
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x00014444 File Offset: 0x00012644
		private static AlgorithmIdentifier GetDefaultEncryptionAlgorithm()
		{
			string oidValue = LocalAppContextSwitches.EnvelopedCmsUseLegacyDefaultAlgorithm ? "1.2.840.113549.3.7" : "2.16.840.1.101.3.4.1.42";
			return new AlgorithmIdentifier(Oid.FromOidValue(oidValue, OidGroup.EncryptionAlgorithm));
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00014471 File Offset: 0x00012671
		public EnvelopedCms() : this(SubjectIdentifierType.IssuerAndSerialNumber, new ContentInfo(Oid.FromOidValue("1.2.840.113549.1.7.1", OidGroup.ExtensionOrAttribute), new byte[0]), EnvelopedCms.GetDefaultEncryptionAlgorithm())
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00014495 File Offset: 0x00012695
		public EnvelopedCms(ContentInfo contentInfo) : this(SubjectIdentifierType.IssuerAndSerialNumber, contentInfo, EnvelopedCms.GetDefaultEncryptionAlgorithm())
		{
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000144A4 File Offset: 0x000126A4
		public EnvelopedCms(SubjectIdentifierType recipientIdentifierType, ContentInfo contentInfo) : this(recipientIdentifierType, contentInfo, EnvelopedCms.GetDefaultEncryptionAlgorithm())
		{
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000144B3 File Offset: 0x000126B3
		public EnvelopedCms(ContentInfo contentInfo, AlgorithmIdentifier encryptionAlgorithm) : this(SubjectIdentifierType.IssuerAndSerialNumber, contentInfo, encryptionAlgorithm)
		{
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000144C0 File Offset: 0x000126C0
		[SecuritySafeCritical]
		public EnvelopedCms(SubjectIdentifierType recipientIdentifierType, ContentInfo contentInfo, AlgorithmIdentifier encryptionAlgorithm)
		{
			if (contentInfo == null)
			{
				throw new ArgumentNullException("contentInfo");
			}
			if (contentInfo.Content == null)
			{
				throw new ArgumentNullException("contentInfo.Content");
			}
			if (encryptionAlgorithm == null)
			{
				throw new ArgumentNullException("encryptionAlgorithm");
			}
			this.m_safeCryptMsgHandle = SafeCryptMsgHandle.InvalidHandle;
			this.m_version = ((recipientIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier) ? 2 : 0);
			this.m_recipientIdentifierType = recipientIdentifierType;
			this.m_contentInfo = contentInfo;
			this.m_encryptionAlgorithm = encryptionAlgorithm;
			this.m_encryptionAlgorithm.Parameters = new byte[0];
			this.m_certificates = new X509Certificate2Collection();
			this.m_unprotectedAttributes = new CryptographicAttributeObjectCollection();
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00014557 File Offset: 0x00012757
		public int Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0001455F File Offset: 0x0001275F
		public ContentInfo ContentInfo
		{
			get
			{
				return this.m_contentInfo;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00014567 File Offset: 0x00012767
		public AlgorithmIdentifier ContentEncryptionAlgorithm
		{
			get
			{
				return this.m_encryptionAlgorithm;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0001456F File Offset: 0x0001276F
		public X509Certificate2Collection Certificates
		{
			get
			{
				return this.m_certificates;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00014577 File Offset: 0x00012777
		public CryptographicAttributeObjectCollection UnprotectedAttributes
		{
			get
			{
				return this.m_unprotectedAttributes;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0001457F File Offset: 0x0001277F
		public RecipientInfoCollection RecipientInfos
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
				{
					return new RecipientInfoCollection();
				}
				return new RecipientInfoCollection(this.m_safeCryptMsgHandle);
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000145A7 File Offset: 0x000127A7
		[SecuritySafeCritical]
		public byte[] Encode()
		{
			if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
			{
				throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_MessageNotEncrypted"));
			}
			return PkcsUtils.GetContent(this.m_safeCryptMsgHandle);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000145DC File Offset: 0x000127DC
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
			this.m_safeCryptMsgHandle = EnvelopedCms.OpenToDecode(encodedMessage);
			this.m_version = (int)PkcsUtils.GetVersion(this.m_safeCryptMsgHandle);
			Oid contentType = PkcsUtils.GetContentType(this.m_safeCryptMsgHandle);
			byte[] content = PkcsUtils.GetContent(this.m_safeCryptMsgHandle);
			this.m_contentInfo = new ContentInfo(contentType, content);
			this.m_encryptionAlgorithm = PkcsUtils.GetAlgorithmIdentifier(this.m_safeCryptMsgHandle);
			this.m_certificates = PkcsUtils.GetCertificates(this.m_safeCryptMsgHandle);
			this.m_unprotectedAttributes = PkcsUtils.GetUnprotectedAttributes(this.m_safeCryptMsgHandle);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001468C File Offset: 0x0001288C
		public void Encrypt()
		{
			this.Encrypt(new CmsRecipientCollection());
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00014699 File Offset: 0x00012899
		public void Encrypt(CmsRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			this.Encrypt(new CmsRecipientCollection(recipient));
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000146B8 File Offset: 0x000128B8
		public void Encrypt(CmsRecipientCollection recipients)
		{
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			if (this.ContentInfo.Content.Length == 0)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Envelope_Empty_Content"));
			}
			if (recipients.Count == 0)
			{
				recipients = PkcsUtils.SelectRecipients(this.m_recipientIdentifierType);
			}
			this.EncryptContent(recipients);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001470D File Offset: 0x0001290D
		public void Decrypt()
		{
			this.DecryptContent(this.RecipientInfos, null);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001471C File Offset: 0x0001291C
		public void Decrypt(RecipientInfo recipientInfo)
		{
			if (recipientInfo == null)
			{
				throw new ArgumentNullException("recipientInfo");
			}
			this.DecryptContent(new RecipientInfoCollection(recipientInfo), null);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00014739 File Offset: 0x00012939
		public void Decrypt(X509Certificate2Collection extraStore)
		{
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			this.DecryptContent(this.RecipientInfos, extraStore);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00014756 File Offset: 0x00012956
		public void Decrypt(RecipientInfo recipientInfo, X509Certificate2Collection extraStore)
		{
			if (recipientInfo == null)
			{
				throw new ArgumentNullException("recipientInfo");
			}
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			this.DecryptContent(new RecipientInfoCollection(recipientInfo), extraStore);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00014784 File Offset: 0x00012984
		[SecuritySafeCritical]
		private unsafe void DecryptContent(RecipientInfoCollection recipientInfos, X509Certificate2Collection extraStore)
		{
			int num = -2146889717;
			if (this.m_safeCryptMsgHandle == null || this.m_safeCryptMsgHandle.IsInvalid)
			{
				throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_NoEncryptedMessageToEncode"));
			}
			for (int i = 0; i < recipientInfos.Count; i++)
			{
				RecipientInfo recipientInfo = recipientInfos[i];
				EnvelopedCms.CMSG_DECRYPT_PARAM cmsg_DECRYPT_PARAM = default(EnvelopedCms.CMSG_DECRYPT_PARAM);
				int num2 = EnvelopedCms.GetCspParams(recipientInfo, extraStore, ref cmsg_DECRYPT_PARAM);
				if (num2 == 0)
				{
					CspParameters parameters = new CspParameters();
					if (X509Utils.GetPrivateKeyInfo(cmsg_DECRYPT_PARAM.safeCertContextHandle, ref parameters))
					{
						KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
						KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Open | KeyContainerPermissionFlags.Decrypt);
						keyContainerPermission.AccessEntries.Add(accessEntry);
						keyContainerPermission.Demand();
					}
					RecipientInfoType type = recipientInfo.Type;
					if (type != RecipientInfoType.KeyTransport)
					{
						if (type != RecipientInfoType.KeyAgreement)
						{
							throw new CryptographicException(-2147483647);
						}
						SafeCertContextHandle safeCertContextHandle = SafeCertContextHandle.InvalidHandle;
						KeyAgreeRecipientInfo keyAgreeRecipientInfo = (KeyAgreeRecipientInfo)recipientInfo;
						CAPI.CMSG_CMS_RECIPIENT_INFO cmsg_CMS_RECIPIENT_INFO = (CAPI.CMSG_CMS_RECIPIENT_INFO)Marshal.PtrToStructure(keyAgreeRecipientInfo.pCmsgRecipientInfo.DangerousGetHandle(), typeof(CAPI.CMSG_CMS_RECIPIENT_INFO));
						CAPI.CMSG_CTRL_KEY_AGREE_DECRYPT_PARA cmsg_CTRL_KEY_AGREE_DECRYPT_PARA = new CAPI.CMSG_CTRL_KEY_AGREE_DECRYPT_PARA(Marshal.SizeOf(typeof(CAPI.CMSG_CTRL_KEY_AGREE_DECRYPT_PARA)));
						cmsg_CTRL_KEY_AGREE_DECRYPT_PARA.hCryptProv = cmsg_DECRYPT_PARAM.safeCryptProvHandle.DangerousGetHandle();
						cmsg_CTRL_KEY_AGREE_DECRYPT_PARA.dwKeySpec = cmsg_DECRYPT_PARAM.keySpec;
						cmsg_CTRL_KEY_AGREE_DECRYPT_PARA.pKeyAgree = cmsg_CMS_RECIPIENT_INFO.pRecipientInfo;
						cmsg_CTRL_KEY_AGREE_DECRYPT_PARA.dwRecipientIndex = keyAgreeRecipientInfo.Index;
						cmsg_CTRL_KEY_AGREE_DECRYPT_PARA.dwRecipientEncryptedKeyIndex = keyAgreeRecipientInfo.SubIndex;
						if (keyAgreeRecipientInfo.SubType == RecipientSubType.CertIdKeyAgreement)
						{
							CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO cmsg_KEY_AGREE_CERT_ID_RECIPIENT_INFO = (CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO)keyAgreeRecipientInfo.CmsgRecipientInfo;
							SafeCertStoreHandle hCertStore = EnvelopedCms.BuildOriginatorStore(this.Certificates, extraStore);
							safeCertContextHandle = CAPI.CertFindCertificateInStore(hCertStore, 65537U, 0U, 1048576U, new IntPtr((void*)(&cmsg_KEY_AGREE_CERT_ID_RECIPIENT_INFO.OriginatorCertId)), SafeCertContextHandle.InvalidHandle);
							if (safeCertContextHandle == null || safeCertContextHandle.IsInvalid)
							{
								num2 = -2146885628;
								goto IL_2C5;
							}
							CAPI.CERT_CONTEXT cert_CONTEXT = (CAPI.CERT_CONTEXT)Marshal.PtrToStructure(safeCertContextHandle.DangerousGetHandle(), typeof(CAPI.CERT_CONTEXT));
							CAPI.CERT_INFO cert_INFO = (CAPI.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPI.CERT_INFO));
							cmsg_CTRL_KEY_AGREE_DECRYPT_PARA.OriginatorPublicKey = cert_INFO.SubjectPublicKeyInfo.PublicKey;
						}
						else
						{
							CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO cmsg_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO = (CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO)keyAgreeRecipientInfo.CmsgRecipientInfo;
							cmsg_CTRL_KEY_AGREE_DECRYPT_PARA.OriginatorPublicKey = cmsg_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO.OriginatorPublicKeyInfo.PublicKey;
						}
						if (!CAPI.CryptMsgControl(this.m_safeCryptMsgHandle, 0U, 17U, new IntPtr((void*)(&cmsg_CTRL_KEY_AGREE_DECRYPT_PARA))))
						{
							num2 = Marshal.GetHRForLastWin32Error();
						}
						GC.KeepAlive(cmsg_CTRL_KEY_AGREE_DECRYPT_PARA);
						GC.KeepAlive(safeCertContextHandle);
					}
					else
					{
						CAPI.CMSG_CTRL_DECRYPT_PARA cmsg_CTRL_DECRYPT_PARA = new CAPI.CMSG_CTRL_DECRYPT_PARA(Marshal.SizeOf(typeof(CAPI.CMSG_CTRL_DECRYPT_PARA)));
						cmsg_CTRL_DECRYPT_PARA.hCryptProv = cmsg_DECRYPT_PARAM.safeCryptProvHandle.DangerousGetHandle();
						cmsg_CTRL_DECRYPT_PARA.dwKeySpec = cmsg_DECRYPT_PARAM.keySpec;
						cmsg_CTRL_DECRYPT_PARA.dwRecipientIndex = recipientInfo.Index;
						if (!CAPI.CryptMsgControl(this.m_safeCryptMsgHandle, 0U, 2U, new IntPtr((void*)(&cmsg_CTRL_DECRYPT_PARA))))
						{
							num2 = Marshal.GetHRForLastWin32Error();
						}
						GC.KeepAlive(cmsg_CTRL_DECRYPT_PARA);
					}
					IL_2C5:
					GC.KeepAlive(cmsg_DECRYPT_PARAM);
				}
				if (num2 == 0)
				{
					uint num3 = 0U;
					SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
					PkcsUtils.GetParam(this.m_safeCryptMsgHandle, 2U, 0U, out invalidHandle, out num3);
					if (num3 > 0U)
					{
						Oid contentType = PkcsUtils.GetContentType(this.m_safeCryptMsgHandle);
						byte[] array = new byte[num3];
						Marshal.Copy(invalidHandle.DangerousGetHandle(), array, 0, (int)num3);
						this.m_contentInfo = new ContentInfo(contentType, array);
					}
					invalidHandle.Dispose();
					num = 0;
					break;
				}
				num = num2;
			}
			if (num != 0)
			{
				throw new CryptographicException(num);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00014AE4 File Offset: 0x00012CE4
		[SecuritySafeCritical]
		private unsafe void EncryptContent(CmsRecipientCollection recipients)
		{
			EnvelopedCms.CMSG_ENCRYPT_PARAM cmsg_ENCRYPT_PARAM = default(EnvelopedCms.CMSG_ENCRYPT_PARAM);
			if (recipients.Count < 1)
			{
				throw new CryptographicException(-2146889717);
			}
			foreach (CmsRecipient cmsRecipient in recipients)
			{
				if (cmsRecipient.Certificate == null)
				{
					throw new ArgumentNullException(SecurityResources.GetResourceString("Cryptography_Cms_RecipientCertificateNotFound"));
				}
				if (PkcsUtils.GetRecipientInfoType(cmsRecipient.Certificate) == RecipientInfoType.KeyAgreement || cmsRecipient.RecipientIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier)
				{
					cmsg_ENCRYPT_PARAM.useCms = true;
				}
			}
			if (!cmsg_ENCRYPT_PARAM.useCms && (this.Certificates.Count > 0 || this.UnprotectedAttributes.Count > 0))
			{
				cmsg_ENCRYPT_PARAM.useCms = true;
			}
			if (cmsg_ENCRYPT_PARAM.useCms && !PkcsUtils.CmsSupported())
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Not_Supported"));
			}
			CAPI.CMSG_ENVELOPED_ENCODE_INFO cmsg_ENVELOPED_ENCODE_INFO = new CAPI.CMSG_ENVELOPED_ENCODE_INFO(Marshal.SizeOf(typeof(CAPI.CMSG_ENVELOPED_ENCODE_INFO)));
			SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CMSG_ENVELOPED_ENCODE_INFO))));
			EnvelopedCms.SetCspParams(this.ContentEncryptionAlgorithm, ref cmsg_ENCRYPT_PARAM);
			cmsg_ENVELOPED_ENCODE_INFO.ContentEncryptionAlgorithm.pszObjId = this.ContentEncryptionAlgorithm.Oid.Value;
			if (cmsg_ENCRYPT_PARAM.pvEncryptionAuxInfo != null && !cmsg_ENCRYPT_PARAM.pvEncryptionAuxInfo.IsInvalid)
			{
				cmsg_ENVELOPED_ENCODE_INFO.pvEncryptionAuxInfo = cmsg_ENCRYPT_PARAM.pvEncryptionAuxInfo.DangerousGetHandle();
			}
			cmsg_ENVELOPED_ENCODE_INFO.cRecipients = (uint)recipients.Count;
			List<SafeCertContextHandle> obj = null;
			if (cmsg_ENCRYPT_PARAM.useCms)
			{
				EnvelopedCms.SetCmsRecipientParams(recipients, this.Certificates, this.UnprotectedAttributes, this.ContentEncryptionAlgorithm, ref cmsg_ENCRYPT_PARAM);
				cmsg_ENVELOPED_ENCODE_INFO.rgCmsRecipients = cmsg_ENCRYPT_PARAM.rgpRecipients.DangerousGetHandle();
				if (cmsg_ENCRYPT_PARAM.rgCertEncoded != null && !cmsg_ENCRYPT_PARAM.rgCertEncoded.IsInvalid)
				{
					cmsg_ENVELOPED_ENCODE_INFO.cCertEncoded = (uint)this.Certificates.Count;
					cmsg_ENVELOPED_ENCODE_INFO.rgCertEncoded = cmsg_ENCRYPT_PARAM.rgCertEncoded.DangerousGetHandle();
				}
				if (cmsg_ENCRYPT_PARAM.rgUnprotectedAttr != null && !cmsg_ENCRYPT_PARAM.rgUnprotectedAttr.IsInvalid)
				{
					cmsg_ENVELOPED_ENCODE_INFO.cUnprotectedAttr = (uint)this.UnprotectedAttributes.Count;
					cmsg_ENVELOPED_ENCODE_INFO.rgUnprotectedAttr = cmsg_ENCRYPT_PARAM.rgUnprotectedAttr.DangerousGetHandle();
				}
			}
			else
			{
				EnvelopedCms.SetPkcs7RecipientParams(recipients, ref cmsg_ENCRYPT_PARAM, out obj);
				cmsg_ENVELOPED_ENCODE_INFO.rgpRecipients = cmsg_ENCRYPT_PARAM.rgpRecipients.DangerousGetHandle();
			}
			Marshal.StructureToPtr(cmsg_ENVELOPED_ENCODE_INFO, safeLocalAllocHandle.DangerousGetHandle(), false);
			try
			{
				SafeCryptMsgHandle safeCryptMsgHandle = CAPI.CryptMsgOpenToEncode(65537U, 0U, 3U, safeLocalAllocHandle.DangerousGetHandle(), this.ContentInfo.ContentType.Value, IntPtr.Zero);
				if (safeCryptMsgHandle == null || safeCryptMsgHandle.IsInvalid)
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				if (this.m_safeCryptMsgHandle != null && !this.m_safeCryptMsgHandle.IsInvalid)
				{
					this.m_safeCryptMsgHandle.Dispose();
				}
				this.m_safeCryptMsgHandle = safeCryptMsgHandle;
			}
			finally
			{
				Marshal.DestroyStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPI.CMSG_ENVELOPED_ENCODE_INFO));
				safeLocalAllocHandle.Dispose();
			}
			byte[] array = new byte[0];
			if (string.Compare(this.ContentInfo.ContentType.Value, "1.2.840.113549.1.7.1", StringComparison.OrdinalIgnoreCase) == 0)
			{
				byte[] content = this.ContentInfo.Content;
				byte[] array2;
				byte* value;
				if ((array2 = content) == null || array2.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array2[0];
				}
				CAPI.CRYPTOAPI_BLOB cryptoapi_BLOB = default(CAPI.CRYPTOAPI_BLOB);
				cryptoapi_BLOB.cbData = (uint)content.Length;
				cryptoapi_BLOB.pbData = new IntPtr((void*)value);
				if (!CAPI.EncodeObject(new IntPtr(25L), new IntPtr((void*)(&cryptoapi_BLOB)), out array))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				array2 = null;
			}
			else
			{
				array = this.ContentInfo.Content;
			}
			if (array.Length != 0 && !CAPI.CAPISafe.CryptMsgUpdate(this.m_safeCryptMsgHandle, array, (uint)array.Length, true))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			GC.KeepAlive(cmsg_ENCRYPT_PARAM);
			GC.KeepAlive(recipients);
			GC.KeepAlive(obj);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00014E94 File Offset: 0x00013094
		[SecuritySafeCritical]
		private static SafeCryptMsgHandle OpenToDecode(byte[] encodedMessage)
		{
			SafeCryptMsgHandle safeCryptMsgHandle = CAPI.CAPISafe.CryptMsgOpenToDecode(65537U, 0U, 0U, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (safeCryptMsgHandle == null || safeCryptMsgHandle.IsInvalid)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			if (!CAPI.CAPISafe.CryptMsgUpdate(safeCryptMsgHandle, encodedMessage, (uint)encodedMessage.Length, true))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			if (3U != PkcsUtils.GetMessageType(safeCryptMsgHandle))
			{
				throw new CryptographicException(-2146889724);
			}
			return safeCryptMsgHandle;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00014F04 File Offset: 0x00013104
		[SecurityCritical]
		private unsafe static int GetCspParams(RecipientInfo recipientInfo, X509Certificate2Collection extraStore, ref EnvelopedCms.CMSG_DECRYPT_PARAM cmsgDecryptParam)
		{
			int result = -2146889717;
			SafeCertContextHandle safeCertContextHandle = SafeCertContextHandle.InvalidHandle;
			SafeCertStoreHandle safeCertStoreHandle = EnvelopedCms.BuildDecryptorStore(extraStore);
			RecipientInfoType type = recipientInfo.Type;
			if (type != RecipientInfoType.KeyTransport)
			{
				if (type != RecipientInfoType.KeyAgreement)
				{
					result = -2147483647;
				}
				else
				{
					KeyAgreeRecipientInfo keyAgreeRecipientInfo = (KeyAgreeRecipientInfo)recipientInfo;
					CAPI.CERT_ID recipientId = keyAgreeRecipientInfo.RecipientId;
					safeCertContextHandle = CAPI.CertFindCertificateInStore(safeCertStoreHandle, 65537U, 0U, 1048576U, new IntPtr((void*)(&recipientId)), SafeCertContextHandle.InvalidHandle);
				}
			}
			else if (recipientInfo.SubType == RecipientSubType.Pkcs7KeyTransport)
			{
				safeCertContextHandle = CAPI.CertFindCertificateInStore(safeCertStoreHandle, 65537U, 0U, 720896U, recipientInfo.pCmsgRecipientInfo.DangerousGetHandle(), SafeCertContextHandle.InvalidHandle);
			}
			else
			{
				safeCertContextHandle = CAPI.CertFindCertificateInStore(safeCertStoreHandle, 65537U, 0U, 1048576U, new IntPtr((void*)(&((CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO)recipientInfo.CmsgRecipientInfo).RecipientId)), SafeCertContextHandle.InvalidHandle);
			}
			safeCertStoreHandle.Dispose();
			if (safeCertContextHandle != null && !safeCertContextHandle.IsInvalid)
			{
				SafeCryptProvHandle safeCryptProvHandle;
				uint keySpec;
				result = PkcsUtils.GetCertPrivateKey(safeCertContextHandle, out safeCryptProvHandle, out keySpec);
				if (safeCryptProvHandle != null && !safeCryptProvHandle.IsInvalid)
				{
					cmsgDecryptParam.safeCryptProvHandle = safeCryptProvHandle;
				}
				else
				{
					cmsgDecryptParam.safeCryptProvHandle = null;
				}
				cmsgDecryptParam.safeCertContextHandle = safeCertContextHandle;
				cmsgDecryptParam.keySpec = keySpec;
			}
			return result;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001501C File Offset: 0x0001321C
		[SecurityCritical]
		private static void SetCspParams(AlgorithmIdentifier contentEncryptionAlgorithm, ref EnvelopedCms.CMSG_ENCRYPT_PARAM encryptParam)
		{
			encryptParam.safeCryptProvHandle = SafeCryptProvHandle.InvalidHandle;
			encryptParam.pvEncryptionAuxInfo = SafeLocalAllocHandle.InvalidHandle;
			SafeCryptProvHandle invalidHandle = SafeCryptProvHandle.InvalidHandle;
			if (!CAPI.CryptAcquireContext(ref invalidHandle, IntPtr.Zero, IntPtr.Zero, 1U, 4026531840U))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			uint num = X509Utils.OidToAlgId(contentEncryptionAlgorithm.Oid.Value);
			if (num == 26114U || num == 26625U)
			{
				CAPI.CMSG_RC2_AUX_INFO cmsg_RC2_AUX_INFO = new CAPI.CMSG_RC2_AUX_INFO(Marshal.SizeOf(typeof(CAPI.CMSG_RC2_AUX_INFO)));
				uint num2 = (uint)contentEncryptionAlgorithm.KeyLength;
				if (num2 == 0U)
				{
					num2 = (uint)PkcsUtils.GetMaxKeyLength(invalidHandle, num);
				}
				cmsg_RC2_AUX_INFO.dwBitLen = num2;
				SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CMSG_RC2_AUX_INFO))));
				Marshal.StructureToPtr(cmsg_RC2_AUX_INFO, safeLocalAllocHandle.DangerousGetHandle(), false);
				encryptParam.pvEncryptionAuxInfo = safeLocalAllocHandle;
			}
			encryptParam.safeCryptProvHandle = invalidHandle;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000150FC File Offset: 0x000132FC
		[SecurityCritical]
		private unsafe static void SetCmsRecipientParams(CmsRecipientCollection recipients, X509Certificate2Collection certificates, CryptographicAttributeObjectCollection unprotectedAttributes, AlgorithmIdentifier contentEncryptionAlgorithm, ref EnvelopedCms.CMSG_ENCRYPT_PARAM encryptParam)
		{
			recipients = recipients.DeepCopy();
			certificates = new X509Certificate2Collection(certificates);
			uint[] array = new uint[recipients.Count];
			int num = 0;
			checked
			{
				int num2 = recipients.Count * Marshal.SizeOf(typeof(CAPI.CMSG_RECIPIENT_ENCODE_INFO));
				int num3 = num2;
				for (int i = 0; i < recipients.Count; i++)
				{
					array[i] = (uint)PkcsUtils.GetRecipientInfoType(recipients[i].Certificate);
					if (array[i] == 1U)
					{
						num3 += Marshal.SizeOf(typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO));
					}
					else
					{
						if (array[i] != 2U)
						{
							throw new CryptographicException(-2146889726);
						}
						num++;
						num3 += Marshal.SizeOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO));
					}
				}
				encryptParam.rgpRecipients = CAPI.LocalAlloc(64U, new IntPtr(num3));
				encryptParam.rgCertEncoded = SafeLocalAllocHandle.InvalidHandle;
				encryptParam.rgUnprotectedAttr = SafeLocalAllocHandle.InvalidHandle;
				encryptParam.rgSubjectKeyIdentifier = new SafeLocalAllocHandle[recipients.Count];
				encryptParam.rgszObjId = new SafeLocalAllocHandle[recipients.Count];
				if (num > 0)
				{
					encryptParam.rgszKeyWrapObjId = new SafeLocalAllocHandle[num];
					encryptParam.rgKeyWrapAuxInfo = new SafeLocalAllocHandle[num];
					encryptParam.rgEphemeralIdentifier = new SafeLocalAllocHandle[num];
					encryptParam.rgszEphemeralObjId = new SafeLocalAllocHandle[num];
					encryptParam.rgUserKeyingMaterial = new SafeLocalAllocHandle[num];
					encryptParam.prgpEncryptedKey = new SafeLocalAllocHandle[num];
					encryptParam.rgpEncryptedKey = new SafeLocalAllocHandle[num];
				}
				if (certificates.Count > 0)
				{
					encryptParam.rgCertEncoded = CAPI.LocalAlloc(64U, new IntPtr(certificates.Count * Marshal.SizeOf(typeof(CAPI.CRYPTOAPI_BLOB))));
					for (int i = 0; i < certificates.Count; i++)
					{
						CAPI.CERT_CONTEXT cert_CONTEXT = (CAPI.CERT_CONTEXT)Marshal.PtrToStructure(X509Utils.GetCertContext(certificates[i]).DangerousGetHandle(), typeof(CAPI.CERT_CONTEXT));
						CAPI.CRYPTOAPI_BLOB* ptr = (CAPI.CRYPTOAPI_BLOB*)((void*)new IntPtr((long)encryptParam.rgCertEncoded.DangerousGetHandle() + unchecked((long)(checked(i * Marshal.SizeOf(typeof(CAPI.CRYPTOAPI_BLOB)))))));
						ptr->cbData = cert_CONTEXT.cbCertEncoded;
						ptr->pbData = cert_CONTEXT.pbCertEncoded;
					}
				}
				if (unprotectedAttributes.Count > 0)
				{
					encryptParam.rgUnprotectedAttr = new SafeLocalAllocHandle(PkcsUtils.CreateCryptAttributes(unprotectedAttributes));
				}
				num = 0;
				IntPtr intPtr = new IntPtr((long)encryptParam.rgpRecipients.DangerousGetHandle() + unchecked((long)num2));
				for (int i = 0; i < recipients.Count; i++)
				{
					CmsRecipient cmsRecipient = recipients[i];
					X509Certificate2 certificate = cmsRecipient.Certificate;
					CAPI.CERT_CONTEXT cert_CONTEXT2 = (CAPI.CERT_CONTEXT)Marshal.PtrToStructure(X509Utils.GetCertContext(certificate).DangerousGetHandle(), typeof(CAPI.CERT_CONTEXT));
					CAPI.CERT_INFO cert_INFO = (CAPI.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT2.pCertInfo, typeof(CAPI.CERT_INFO));
					CAPI.CMSG_RECIPIENT_ENCODE_INFO* ptr2 = (CAPI.CMSG_RECIPIENT_ENCODE_INFO*)((void*)new IntPtr((long)encryptParam.rgpRecipients.DangerousGetHandle() + unchecked((long)(checked(i * Marshal.SizeOf(typeof(CAPI.CMSG_RECIPIENT_ENCODE_INFO)))))));
					ptr2->dwRecipientChoice = array[i];
					ptr2->pRecipientInfo = intPtr;
					if (array[i] == 1U)
					{
						IntPtr ptr3 = new IntPtr((long)intPtr + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO), "cbSize"));
						Marshal.WriteInt32(ptr3, Marshal.SizeOf(typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO)));
						IntPtr value = new IntPtr((long)intPtr + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO), "KeyEncryptionAlgorithm"));
						byte[] bytes = Encoding.ASCII.GetBytes(cert_INFO.SubjectPublicKeyInfo.Algorithm.pszObjId);
						encryptParam.rgszObjId[i] = CAPI.LocalAlloc(64U, new IntPtr(bytes.Length + 1));
						Marshal.Copy(bytes, 0, encryptParam.rgszObjId[i].DangerousGetHandle(), bytes.Length);
						IntPtr ptr4 = new IntPtr((long)value + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER), "pszObjId"));
						Marshal.WriteIntPtr(ptr4, encryptParam.rgszObjId[i].DangerousGetHandle());
						IntPtr value2 = new IntPtr((long)value + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER), "Parameters"));
						IntPtr ptr5 = new IntPtr((long)value2 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
						Marshal.WriteInt32(ptr5, (int)cert_INFO.SubjectPublicKeyInfo.Algorithm.Parameters.cbData);
						IntPtr ptr6 = new IntPtr((long)value2 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
						Marshal.WriteIntPtr(ptr6, cert_INFO.SubjectPublicKeyInfo.Algorithm.Parameters.pbData);
						IntPtr value3 = new IntPtr((long)intPtr + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO), "RecipientPublicKey"));
						ptr5 = new IntPtr((long)value3 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_BIT_BLOB), "cbData"));
						Marshal.WriteInt32(ptr5, (int)cert_INFO.SubjectPublicKeyInfo.PublicKey.cbData);
						ptr6 = new IntPtr((long)value3 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_BIT_BLOB), "pbData"));
						Marshal.WriteIntPtr(ptr6, cert_INFO.SubjectPublicKeyInfo.PublicKey.pbData);
						IntPtr ptr7 = new IntPtr((long)value3 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_BIT_BLOB), "cUnusedBits"));
						Marshal.WriteInt32(ptr7, (int)cert_INFO.SubjectPublicKeyInfo.PublicKey.cUnusedBits);
						IntPtr value4 = new IntPtr((long)intPtr + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO), "RecipientId"));
						if (cmsRecipient.RecipientIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier)
						{
							uint num4 = 0U;
							SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
							if (!CAPI.CAPISafe.CertGetCertificateContextProperty(X509Utils.GetCertContext(certificate), 20U, safeLocalAllocHandle, ref num4))
							{
								throw new CryptographicException(Marshal.GetLastWin32Error());
							}
							safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr((long)(unchecked((ulong)num4))));
							if (!CAPI.CAPISafe.CertGetCertificateContextProperty(X509Utils.GetCertContext(certificate), 20U, safeLocalAllocHandle, ref num4))
							{
								throw new CryptographicException(Marshal.GetLastWin32Error());
							}
							encryptParam.rgSubjectKeyIdentifier[i] = safeLocalAllocHandle;
							IntPtr ptr8 = new IntPtr((long)value4 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ID), "dwIdChoice"));
							Marshal.WriteInt32(ptr8, 2);
							IntPtr value5 = new IntPtr((long)value4 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ID), "Value"));
							ptr5 = new IntPtr((long)value5 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
							Marshal.WriteInt32(ptr5, (int)num4);
							ptr6 = new IntPtr((long)value5 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
							Marshal.WriteIntPtr(ptr6, safeLocalAllocHandle.DangerousGetHandle());
						}
						else
						{
							IntPtr ptr9 = new IntPtr((long)value4 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ID), "dwIdChoice"));
							Marshal.WriteInt32(ptr9, 1);
							IntPtr value6 = new IntPtr((long)value4 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ID), "Value"));
							IntPtr value7 = new IntPtr((long)value6 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ISSUER_SERIAL_NUMBER), "Issuer"));
							ptr5 = new IntPtr((long)value7 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
							Marshal.WriteInt32(ptr5, (int)cert_INFO.Issuer.cbData);
							ptr6 = new IntPtr((long)value7 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
							Marshal.WriteIntPtr(ptr6, cert_INFO.Issuer.pbData);
							IntPtr value8 = new IntPtr((long)value6 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ISSUER_SERIAL_NUMBER), "SerialNumber"));
							ptr5 = new IntPtr((long)value8 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
							Marshal.WriteInt32(ptr5, (int)cert_INFO.SerialNumber.cbData);
							ptr6 = new IntPtr((long)value8 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
							Marshal.WriteIntPtr(ptr6, cert_INFO.SerialNumber.pbData);
						}
						intPtr = new IntPtr((long)intPtr + unchecked((long)Marshal.SizeOf(typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_ENCODE_INFO))));
					}
					else if (array[i] == 2U)
					{
						IntPtr ptr10 = new IntPtr((long)intPtr + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "cbSize"));
						Marshal.WriteInt32(ptr10, Marshal.SizeOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO)));
						IntPtr value9 = new IntPtr((long)intPtr + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "KeyEncryptionAlgorithm"));
						byte[] bytes2 = Encoding.ASCII.GetBytes("1.2.840.113549.1.9.16.3.5");
						encryptParam.rgszObjId[i] = CAPI.LocalAlloc(64U, new IntPtr(bytes2.Length + 1));
						Marshal.Copy(bytes2, 0, encryptParam.rgszObjId[i].DangerousGetHandle(), bytes2.Length);
						IntPtr ptr11 = new IntPtr((long)value9 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER), "pszObjId"));
						Marshal.WriteIntPtr(ptr11, encryptParam.rgszObjId[i].DangerousGetHandle());
						IntPtr value10 = new IntPtr((long)intPtr + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "KeyWrapAlgorithm"));
						uint num5 = X509Utils.OidToAlgId(contentEncryptionAlgorithm.Oid.Value);
						if (num5 == 26114U)
						{
							bytes2 = Encoding.ASCII.GetBytes("1.2.840.113549.1.9.16.3.7");
						}
						else
						{
							bytes2 = Encoding.ASCII.GetBytes("1.2.840.113549.1.9.16.3.6");
						}
						encryptParam.rgszKeyWrapObjId[num] = CAPI.LocalAlloc(64U, new IntPtr(bytes2.Length + 1));
						Marshal.Copy(bytes2, 0, encryptParam.rgszKeyWrapObjId[num].DangerousGetHandle(), bytes2.Length);
						ptr11 = new IntPtr((long)value10 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER), "pszObjId"));
						Marshal.WriteIntPtr(ptr11, encryptParam.rgszKeyWrapObjId[num].DangerousGetHandle());
						if (num5 == 26114U)
						{
							IntPtr ptr12 = new IntPtr((long)intPtr + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "pvKeyWrapAuxInfo"));
							Marshal.WriteIntPtr(ptr12, encryptParam.pvEncryptionAuxInfo.DangerousGetHandle());
						}
						IntPtr ptr13 = new IntPtr((long)intPtr + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "dwKeyChoice"));
						Marshal.WriteInt32(ptr13, 1);
						IntPtr ptr14 = new IntPtr((long)intPtr + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "pEphemeralAlgorithmOrSenderId"));
						encryptParam.rgEphemeralIdentifier[num] = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER))));
						Marshal.WriteIntPtr(ptr14, encryptParam.rgEphemeralIdentifier[num].DangerousGetHandle());
						bytes2 = Encoding.ASCII.GetBytes(cert_INFO.SubjectPublicKeyInfo.Algorithm.pszObjId);
						encryptParam.rgszEphemeralObjId[num] = CAPI.LocalAlloc(64U, new IntPtr(bytes2.Length + 1));
						Marshal.Copy(bytes2, 0, encryptParam.rgszEphemeralObjId[num].DangerousGetHandle(), bytes2.Length);
						ptr11 = new IntPtr((long)encryptParam.rgEphemeralIdentifier[num].DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER), "pszObjId"));
						Marshal.WriteIntPtr(ptr11, encryptParam.rgszEphemeralObjId[num].DangerousGetHandle());
						IntPtr value11 = new IntPtr((long)encryptParam.rgEphemeralIdentifier[num].DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER), "Parameters"));
						IntPtr ptr15 = new IntPtr((long)value11 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
						Marshal.WriteInt32(ptr15, (int)cert_INFO.SubjectPublicKeyInfo.Algorithm.Parameters.cbData);
						IntPtr ptr16 = new IntPtr((long)value11 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
						Marshal.WriteIntPtr(ptr16, cert_INFO.SubjectPublicKeyInfo.Algorithm.Parameters.pbData);
						IntPtr ptr17 = new IntPtr((long)intPtr + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "cRecipientEncryptedKeys"));
						Marshal.WriteInt32(ptr17, 1);
						encryptParam.prgpEncryptedKey[num] = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(IntPtr))));
						IntPtr ptr18 = new IntPtr((long)intPtr + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO), "rgpRecipientEncryptedKeys"));
						Marshal.WriteIntPtr(ptr18, encryptParam.prgpEncryptedKey[num].DangerousGetHandle());
						encryptParam.rgpEncryptedKey[num] = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_ENCODE_INFO))));
						Marshal.WriteIntPtr(encryptParam.prgpEncryptedKey[num].DangerousGetHandle(), encryptParam.rgpEncryptedKey[num].DangerousGetHandle());
						ptr10 = new IntPtr((long)encryptParam.rgpEncryptedKey[num].DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_ENCODE_INFO), "cbSize"));
						Marshal.WriteInt32(ptr10, Marshal.SizeOf(typeof(CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_ENCODE_INFO)));
						IntPtr value12 = new IntPtr((long)encryptParam.rgpEncryptedKey[num].DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_ENCODE_INFO), "RecipientPublicKey"));
						ptr15 = new IntPtr((long)value12 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_BIT_BLOB), "cbData"));
						Marshal.WriteInt32(ptr15, (int)cert_INFO.SubjectPublicKeyInfo.PublicKey.cbData);
						ptr16 = new IntPtr((long)value12 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_BIT_BLOB), "pbData"));
						Marshal.WriteIntPtr(ptr16, cert_INFO.SubjectPublicKeyInfo.PublicKey.pbData);
						IntPtr ptr19 = new IntPtr((long)value12 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_BIT_BLOB), "cUnusedBits"));
						Marshal.WriteInt32(ptr19, (int)cert_INFO.SubjectPublicKeyInfo.PublicKey.cUnusedBits);
						IntPtr value13 = new IntPtr((long)encryptParam.rgpEncryptedKey[num].DangerousGetHandle() + (long)Marshal.OffsetOf(typeof(CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_ENCODE_INFO), "RecipientId"));
						IntPtr ptr20 = new IntPtr((long)value13 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ID), "dwIdChoice"));
						if (cmsRecipient.RecipientIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier)
						{
							Marshal.WriteInt32(ptr20, 2);
							IntPtr value14 = new IntPtr((long)value13 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ID), "Value"));
							uint num6 = 0U;
							SafeLocalAllocHandle safeLocalAllocHandle2 = SafeLocalAllocHandle.InvalidHandle;
							if (!CAPI.CAPISafe.CertGetCertificateContextProperty(X509Utils.GetCertContext(certificate), 20U, safeLocalAllocHandle2, ref num6))
							{
								throw new CryptographicException(Marshal.GetLastWin32Error());
							}
							safeLocalAllocHandle2 = CAPI.LocalAlloc(64U, new IntPtr((long)(unchecked((ulong)num6))));
							if (!CAPI.CAPISafe.CertGetCertificateContextProperty(X509Utils.GetCertContext(certificate), 20U, safeLocalAllocHandle2, ref num6))
							{
								throw new CryptographicException(Marshal.GetLastWin32Error());
							}
							encryptParam.rgSubjectKeyIdentifier[num] = safeLocalAllocHandle2;
							ptr15 = new IntPtr((long)value14 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
							Marshal.WriteInt32(ptr15, (int)num6);
							ptr16 = new IntPtr((long)value14 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
							Marshal.WriteIntPtr(ptr16, safeLocalAllocHandle2.DangerousGetHandle());
						}
						else
						{
							Marshal.WriteInt32(ptr20, 1);
							IntPtr value15 = new IntPtr((long)value13 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ID), "Value"));
							IntPtr value16 = new IntPtr((long)value15 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ISSUER_SERIAL_NUMBER), "Issuer"));
							ptr15 = new IntPtr((long)value16 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
							Marshal.WriteInt32(ptr15, (int)cert_INFO.Issuer.cbData);
							ptr16 = new IntPtr((long)value16 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
							Marshal.WriteIntPtr(ptr16, cert_INFO.Issuer.pbData);
							IntPtr value17 = new IntPtr((long)value15 + (long)Marshal.OffsetOf(typeof(CAPI.CERT_ISSUER_SERIAL_NUMBER), "SerialNumber"));
							ptr15 = new IntPtr((long)value17 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
							Marshal.WriteInt32(ptr15, (int)cert_INFO.SerialNumber.cbData);
							ptr16 = new IntPtr((long)value17 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
							Marshal.WriteIntPtr(ptr16, cert_INFO.SerialNumber.pbData);
						}
						num++;
						intPtr = new IntPtr((long)intPtr + unchecked((long)Marshal.SizeOf(typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_ENCODE_INFO))));
					}
				}
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000162BC File Offset: 0x000144BC
		[SecurityCritical]
		private static void SetPkcs7RecipientParams(CmsRecipientCollection recipients, ref EnvelopedCms.CMSG_ENCRYPT_PARAM encryptParam, out List<SafeCertContextHandle> certContexts)
		{
			int count = recipients.Count;
			certContexts = new List<SafeCertContextHandle>();
			uint num = checked((uint)count * (uint)Marshal.SizeOf(typeof(IntPtr)));
			encryptParam.rgpRecipients = CAPI.LocalAlloc(64U, new IntPtr((long)((ulong)num)));
			IntPtr intPtr = encryptParam.rgpRecipients.DangerousGetHandle();
			checked
			{
				for (int i = 0; i < count; i++)
				{
					SafeCertContextHandle certContext = X509Utils.GetCertContext(recipients[i].Certificate);
					certContexts.Add(certContext);
					IntPtr ptr = certContext.DangerousGetHandle();
					CAPI.CERT_CONTEXT cert_CONTEXT = (CAPI.CERT_CONTEXT)Marshal.PtrToStructure(ptr, typeof(CAPI.CERT_CONTEXT));
					Marshal.WriteIntPtr(intPtr, cert_CONTEXT.pCertInfo);
					intPtr = new IntPtr((long)intPtr + unchecked((long)Marshal.SizeOf(typeof(IntPtr))));
				}
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00016384 File Offset: 0x00014584
		[SecurityCritical]
		private static SafeCertStoreHandle BuildDecryptorStore(X509Certificate2Collection extraStore)
		{
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			try
			{
				X509Store x509Store = new X509Store("MY", StoreLocation.CurrentUser);
				x509Store.Open(OpenFlags.OpenExistingOnly | OpenFlags.IncludeArchived);
				x509Certificate2Collection.AddRange(x509Store.Certificates);
				x509Store.Close();
			}
			catch (SecurityException)
			{
			}
			try
			{
				X509Store x509Store2 = new X509Store("MY", StoreLocation.LocalMachine);
				x509Store2.Open(OpenFlags.OpenExistingOnly | OpenFlags.IncludeArchived);
				x509Certificate2Collection.AddRange(x509Store2.Certificates);
				x509Store2.Close();
			}
			catch (SecurityException)
			{
			}
			if (x509Certificate2Collection.Count == 0 && extraStore.Count == 0)
			{
				throw new CryptographicException(-2146889717);
			}
			SafeCertStoreHandle result;
			try
			{
				result = X509Utils.ExportToMemoryStore(x509Certificate2Collection, extraStore);
			}
			finally
			{
				foreach (X509Certificate2 x509Certificate in x509Certificate2Collection)
				{
					x509Certificate.Reset();
				}
			}
			return result;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00016464 File Offset: 0x00014664
		[SecurityCritical]
		private static SafeCertStoreHandle BuildOriginatorStore(X509Certificate2Collection bagOfCerts, X509Certificate2Collection extraStore)
		{
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			try
			{
				X509Store x509Store = new X509Store("AddressBook", StoreLocation.CurrentUser);
				x509Store.Open(OpenFlags.OpenExistingOnly | OpenFlags.IncludeArchived);
				x509Certificate2Collection.AddRange(x509Store.Certificates);
				x509Store.Close();
			}
			catch (SecurityException)
			{
			}
			try
			{
				X509Store x509Store2 = new X509Store("AddressBook", StoreLocation.LocalMachine);
				x509Store2.Open(OpenFlags.OpenExistingOnly | OpenFlags.IncludeArchived);
				x509Certificate2Collection.AddRange(x509Store2.Certificates);
				x509Store2.Close();
			}
			catch (SecurityException)
			{
			}
			X509Certificate2Collection x509Certificate2Collection2;
			if (bagOfCerts != null && extraStore != null)
			{
				x509Certificate2Collection2 = new X509Certificate2Collection();
				x509Certificate2Collection2.AddRange(bagOfCerts);
				x509Certificate2Collection2.AddRange(extraStore);
			}
			else if (bagOfCerts != null)
			{
				x509Certificate2Collection2 = bagOfCerts;
			}
			else if (extraStore != null)
			{
				x509Certificate2Collection2 = extraStore;
			}
			else
			{
				x509Certificate2Collection2 = null;
			}
			if (x509Certificate2Collection.Count == 0 && x509Certificate2Collection2.Count == 0)
			{
				throw new CryptographicException(-2146885628);
			}
			SafeCertStoreHandle result;
			try
			{
				result = X509Utils.ExportToMemoryStore(x509Certificate2Collection, x509Certificate2Collection2);
			}
			finally
			{
				foreach (X509Certificate2 x509Certificate in x509Certificate2Collection)
				{
					x509Certificate.Reset();
				}
			}
			return result;
		}

		// Token: 0x040004B5 RID: 1205
		[SecurityCritical]
		private SafeCryptMsgHandle m_safeCryptMsgHandle;

		// Token: 0x040004B6 RID: 1206
		private int m_version;

		// Token: 0x040004B7 RID: 1207
		private SubjectIdentifierType m_recipientIdentifierType;

		// Token: 0x040004B8 RID: 1208
		private ContentInfo m_contentInfo;

		// Token: 0x040004B9 RID: 1209
		private AlgorithmIdentifier m_encryptionAlgorithm;

		// Token: 0x040004BA RID: 1210
		private X509Certificate2Collection m_certificates;

		// Token: 0x040004BB RID: 1211
		private CryptographicAttributeObjectCollection m_unprotectedAttributes;

		// Token: 0x020000E0 RID: 224
		[SecurityCritical]
		private struct CMSG_DECRYPT_PARAM
		{
			// Token: 0x04000686 RID: 1670
			internal SafeCertContextHandle safeCertContextHandle;

			// Token: 0x04000687 RID: 1671
			internal SafeCryptProvHandle safeCryptProvHandle;

			// Token: 0x04000688 RID: 1672
			internal uint keySpec;
		}

		// Token: 0x020000E1 RID: 225
		[SecurityCritical]
		private struct CMSG_ENCRYPT_PARAM
		{
			// Token: 0x04000689 RID: 1673
			internal bool useCms;

			// Token: 0x0400068A RID: 1674
			internal SafeCryptProvHandle safeCryptProvHandle;

			// Token: 0x0400068B RID: 1675
			internal SafeLocalAllocHandle pvEncryptionAuxInfo;

			// Token: 0x0400068C RID: 1676
			internal SafeLocalAllocHandle rgpRecipients;

			// Token: 0x0400068D RID: 1677
			internal SafeLocalAllocHandle rgCertEncoded;

			// Token: 0x0400068E RID: 1678
			internal SafeLocalAllocHandle rgUnprotectedAttr;

			// Token: 0x0400068F RID: 1679
			internal SafeLocalAllocHandle[] rgSubjectKeyIdentifier;

			// Token: 0x04000690 RID: 1680
			internal SafeLocalAllocHandle[] rgszObjId;

			// Token: 0x04000691 RID: 1681
			internal SafeLocalAllocHandle[] rgszKeyWrapObjId;

			// Token: 0x04000692 RID: 1682
			internal SafeLocalAllocHandle[] rgKeyWrapAuxInfo;

			// Token: 0x04000693 RID: 1683
			internal SafeLocalAllocHandle[] rgEphemeralIdentifier;

			// Token: 0x04000694 RID: 1684
			internal SafeLocalAllocHandle[] rgszEphemeralObjId;

			// Token: 0x04000695 RID: 1685
			internal SafeLocalAllocHandle[] rgUserKeyingMaterial;

			// Token: 0x04000696 RID: 1686
			internal SafeLocalAllocHandle[] prgpEncryptedKey;

			// Token: 0x04000697 RID: 1687
			internal SafeLocalAllocHandle[] rgpEncryptedKey;
		}
	}
}
