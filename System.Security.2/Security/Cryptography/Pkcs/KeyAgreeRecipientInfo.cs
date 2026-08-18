using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000079 RID: 121
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class KeyAgreeRecipientInfo : RecipientInfo
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x0001717F File Offset: 0x0001537F
		private KeyAgreeRecipientInfo()
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00017188 File Offset: 0x00015388
		[SecurityCritical]
		internal KeyAgreeRecipientInfo(SafeLocalAllocHandle pRecipientInfo, CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO certIdRecipient, uint index, uint subIndex) : base(RecipientInfoType.KeyAgreement, RecipientSubType.CertIdKeyAgreement, pRecipientInfo, certIdRecipient, index)
		{
			IntPtr ptr = Marshal.ReadIntPtr(new IntPtr(checked((long)certIdRecipient.rgpRecipientEncryptedKeys + (long)(unchecked((ulong)subIndex) * (ulong)(unchecked((long)Marshal.SizeOf(typeof(IntPtr))))))));
			CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO encryptedKeyInfo = (CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO)Marshal.PtrToStructure(ptr, typeof(CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO));
			this.Reset(1U, certIdRecipient.dwVersion, encryptedKeyInfo, subIndex);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000171F8 File Offset: 0x000153F8
		[SecurityCritical]
		internal KeyAgreeRecipientInfo(SafeLocalAllocHandle pRecipientInfo, CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO publicKeyRecipient, uint index, uint subIndex) : base(RecipientInfoType.KeyAgreement, RecipientSubType.PublicKeyAgreement, pRecipientInfo, publicKeyRecipient, index)
		{
			IntPtr ptr = Marshal.ReadIntPtr(new IntPtr(checked((long)publicKeyRecipient.rgpRecipientEncryptedKeys + (long)(unchecked((ulong)subIndex) * (ulong)(unchecked((long)Marshal.SizeOf(typeof(IntPtr))))))));
			CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO encryptedKeyInfo = (CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO)Marshal.PtrToStructure(ptr, typeof(CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO));
			this.Reset(2U, publicKeyRecipient.dwVersion, encryptedKeyInfo, subIndex);
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00017266 File Offset: 0x00015466
		public override int Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00017270 File Offset: 0x00015470
		public SubjectIdentifierOrKey OriginatorIdentifierOrKey
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_originatorIdentifier == null)
				{
					if (this.m_originatorChoice == 1U)
					{
						CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO cmsg_KEY_AGREE_CERT_ID_RECIPIENT_INFO = (CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO)base.CmsgRecipientInfo;
						this.m_originatorIdentifier = new SubjectIdentifierOrKey(cmsg_KEY_AGREE_CERT_ID_RECIPIENT_INFO.OriginatorCertId);
					}
					else
					{
						CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO cmsg_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO = (CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO)base.CmsgRecipientInfo;
						this.m_originatorIdentifier = new SubjectIdentifierOrKey(cmsg_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO.OriginatorPublicKeyInfo);
					}
				}
				return this.m_originatorIdentifier;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000172D0 File Offset: 0x000154D0
		public override SubjectIdentifier RecipientIdentifier
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_recipientIdentifier == null)
				{
					this.m_recipientIdentifier = new SubjectIdentifier(this.m_encryptedKeyInfo.RecipientId);
				}
				return this.m_recipientIdentifier;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000172F8 File Offset: 0x000154F8
		public DateTime Date
		{
			get
			{
				if (this.m_date == DateTime.MinValue)
				{
					if (this.RecipientIdentifier.Type != SubjectIdentifierType.SubjectKeyIdentifier)
					{
						throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_Key_Agree_Date_Not_Available"));
					}
					long fileTime = (long)((ulong)this.m_encryptedKeyInfo.Date.dwHighDateTime << 32 | (ulong)this.m_encryptedKeyInfo.Date.dwLowDateTime);
					this.m_date = DateTime.FromFileTimeUtc(fileTime);
				}
				return this.m_date;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00017370 File Offset: 0x00015570
		public CryptographicAttributeObject OtherKeyAttribute
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_otherKeyAttribute == null)
				{
					if (this.RecipientIdentifier.Type != SubjectIdentifierType.SubjectKeyIdentifier)
					{
						throw new InvalidOperationException(SecurityResources.GetResourceString("Cryptography_Cms_Key_Agree_Other_Key_Attribute_Not_Available"));
					}
					if (this.m_encryptedKeyInfo.pOtherAttr != IntPtr.Zero)
					{
						CAPI.CRYPT_ATTRIBUTE_TYPE_VALUE cryptAttribute = (CAPI.CRYPT_ATTRIBUTE_TYPE_VALUE)Marshal.PtrToStructure(this.m_encryptedKeyInfo.pOtherAttr, typeof(CAPI.CRYPT_ATTRIBUTE_TYPE_VALUE));
						this.m_otherKeyAttribute = new CryptographicAttributeObject(cryptAttribute);
					}
				}
				return this.m_otherKeyAttribute;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x000173EC File Offset: 0x000155EC
		public override AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_encryptionAlgorithm == null)
				{
					if (this.m_originatorChoice == 1U)
					{
						CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO cmsg_KEY_AGREE_CERT_ID_RECIPIENT_INFO = (CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO)base.CmsgRecipientInfo;
						this.m_encryptionAlgorithm = new AlgorithmIdentifier(cmsg_KEY_AGREE_CERT_ID_RECIPIENT_INFO.KeyEncryptionAlgorithm);
					}
					else
					{
						CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO cmsg_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO = (CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO)base.CmsgRecipientInfo;
						this.m_encryptionAlgorithm = new AlgorithmIdentifier(cmsg_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO.KeyEncryptionAlgorithm);
					}
				}
				return this.m_encryptionAlgorithm;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0001744C File Offset: 0x0001564C
		public override byte[] EncryptedKey
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_encryptedKey.Length == 0 && this.m_encryptedKeyInfo.EncryptedKey.cbData > 0U)
				{
					this.m_encryptedKey = new byte[this.m_encryptedKeyInfo.EncryptedKey.cbData];
					Marshal.Copy(this.m_encryptedKeyInfo.EncryptedKey.pbData, this.m_encryptedKey, 0, this.m_encryptedKey.Length);
				}
				return this.m_encryptedKey;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000174BA File Offset: 0x000156BA
		internal CAPI.CERT_ID RecipientId
		{
			get
			{
				return this.m_encryptedKeyInfo.RecipientId;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x000174C7 File Offset: 0x000156C7
		internal uint SubIndex
		{
			get
			{
				return this.m_subIndex;
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000174D0 File Offset: 0x000156D0
		private void Reset(uint originatorChoice, uint version, CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO encryptedKeyInfo, uint subIndex)
		{
			this.m_encryptedKeyInfo = encryptedKeyInfo;
			this.m_originatorChoice = originatorChoice;
			this.m_version = (int)version;
			this.m_originatorIdentifier = null;
			this.m_userKeyMaterial = new byte[0];
			this.m_encryptionAlgorithm = null;
			this.m_recipientIdentifier = null;
			this.m_encryptedKey = new byte[0];
			this.m_date = DateTime.MinValue;
			this.m_otherKeyAttribute = null;
			this.m_subIndex = subIndex;
		}

		// Token: 0x040004E7 RID: 1255
		private CAPI.CMSG_RECIPIENT_ENCRYPTED_KEY_INFO m_encryptedKeyInfo;

		// Token: 0x040004E8 RID: 1256
		private uint m_originatorChoice;

		// Token: 0x040004E9 RID: 1257
		private int m_version;

		// Token: 0x040004EA RID: 1258
		private SubjectIdentifierOrKey m_originatorIdentifier;

		// Token: 0x040004EB RID: 1259
		private byte[] m_userKeyMaterial;

		// Token: 0x040004EC RID: 1260
		private AlgorithmIdentifier m_encryptionAlgorithm;

		// Token: 0x040004ED RID: 1261
		private SubjectIdentifier m_recipientIdentifier;

		// Token: 0x040004EE RID: 1262
		private byte[] m_encryptedKey;

		// Token: 0x040004EF RID: 1263
		private DateTime m_date;

		// Token: 0x040004F0 RID: 1264
		private CryptographicAttributeObject m_otherKeyAttribute;

		// Token: 0x040004F1 RID: 1265
		private uint m_subIndex;
	}
}
