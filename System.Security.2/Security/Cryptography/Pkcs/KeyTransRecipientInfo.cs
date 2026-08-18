using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000078 RID: 120
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class KeyTransRecipientInfo : RecipientInfo
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x00016FA8 File Offset: 0x000151A8
		[SecurityCritical]
		internal unsafe KeyTransRecipientInfo(SafeLocalAllocHandle pRecipientInfo, CAPI.CERT_INFO certInfo, uint index) : base(RecipientInfoType.KeyTransport, RecipientSubType.Pkcs7KeyTransport, pRecipientInfo, certInfo, index)
		{
			int version = 2;
			byte* ptr = (byte*)((void*)certInfo.SerialNumber.pbData);
			int num = 0;
			while ((long)num < (long)((ulong)certInfo.SerialNumber.cbData))
			{
				if (*(ptr++) != 0)
				{
					version = 0;
					break;
				}
				num++;
			}
			this.Reset(version);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00017003 File Offset: 0x00015203
		[SecurityCritical]
		internal KeyTransRecipientInfo(SafeLocalAllocHandle pRecipientInfo, CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO keyTrans, uint index) : base(RecipientInfoType.KeyTransport, RecipientSubType.CmsKeyTransport, pRecipientInfo, keyTrans, index)
		{
			this.Reset((int)keyTrans.dwVersion);
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00017021 File Offset: 0x00015221
		public override int Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0001702C File Offset: 0x0001522C
		public override SubjectIdentifier RecipientIdentifier
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_recipientIdentifier == null)
				{
					if (base.SubType == RecipientSubType.CmsKeyTransport)
					{
						CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO cmsg_KEY_TRANS_RECIPIENT_INFO = (CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO)base.CmsgRecipientInfo;
						this.m_recipientIdentifier = new SubjectIdentifier(cmsg_KEY_TRANS_RECIPIENT_INFO.RecipientId);
					}
					else
					{
						CAPI.CERT_INFO certInfo = (CAPI.CERT_INFO)base.CmsgRecipientInfo;
						this.m_recipientIdentifier = new SubjectIdentifier(certInfo);
					}
				}
				return this.m_recipientIdentifier;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00017088 File Offset: 0x00015288
		public override AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_encryptionAlgorithm == null)
				{
					if (base.SubType == RecipientSubType.CmsKeyTransport)
					{
						CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO cmsg_KEY_TRANS_RECIPIENT_INFO = (CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO)base.CmsgRecipientInfo;
						this.m_encryptionAlgorithm = new AlgorithmIdentifier(cmsg_KEY_TRANS_RECIPIENT_INFO.KeyEncryptionAlgorithm);
					}
					else
					{
						CAPI.CERT_INFO cert_INFO = (CAPI.CERT_INFO)base.CmsgRecipientInfo;
						this.m_encryptionAlgorithm = new AlgorithmIdentifier(cert_INFO.SignatureAlgorithm);
					}
				}
				return this.m_encryptionAlgorithm;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x000170E8 File Offset: 0x000152E8
		public override byte[] EncryptedKey
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_encryptedKey.Length == 0 && base.SubType == RecipientSubType.CmsKeyTransport)
				{
					CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO cmsg_KEY_TRANS_RECIPIENT_INFO = (CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO)base.CmsgRecipientInfo;
					if (cmsg_KEY_TRANS_RECIPIENT_INFO.EncryptedKey.cbData > 0U)
					{
						this.m_encryptedKey = new byte[cmsg_KEY_TRANS_RECIPIENT_INFO.EncryptedKey.cbData];
						Marshal.Copy(cmsg_KEY_TRANS_RECIPIENT_INFO.EncryptedKey.pbData, this.m_encryptedKey, 0, this.m_encryptedKey.Length);
					}
				}
				return this.m_encryptedKey;
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001715C File Offset: 0x0001535C
		private void Reset(int version)
		{
			this.m_version = version;
			this.m_recipientIdentifier = null;
			this.m_encryptionAlgorithm = null;
			this.m_encryptedKey = new byte[0];
		}

		// Token: 0x040004E3 RID: 1251
		private int m_version;

		// Token: 0x040004E4 RID: 1252
		private SubjectIdentifier m_recipientIdentifier;

		// Token: 0x040004E5 RID: 1253
		private AlgorithmIdentifier m_encryptionAlgorithm;

		// Token: 0x040004E6 RID: 1254
		private byte[] m_encryptedKey;
	}
}
