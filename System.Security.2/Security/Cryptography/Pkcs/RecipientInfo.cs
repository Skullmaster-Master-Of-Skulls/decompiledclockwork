using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000077 RID: 119
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class RecipientInfo
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x000044A9 File Offset: 0x000026A9
		internal RecipientInfo()
		{
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00016F30 File Offset: 0x00015130
		[SecurityCritical]
		internal RecipientInfo(RecipientInfoType recipientInfoType, RecipientSubType recipientSubType, SafeLocalAllocHandle pCmsgRecipientInfo, object cmsgRecipientInfo, uint index)
		{
			if (recipientInfoType < RecipientInfoType.Unknown || recipientInfoType > RecipientInfoType.KeyAgreement)
			{
				recipientInfoType = RecipientInfoType.Unknown;
			}
			if (recipientSubType < RecipientSubType.Unknown || recipientSubType > RecipientSubType.PublicKeyAgreement)
			{
				recipientSubType = RecipientSubType.Unknown;
			}
			this.m_recipentInfoType = recipientInfoType;
			this.m_recipientSubType = recipientSubType;
			this.m_pCmsgRecipientInfo = pCmsgRecipientInfo;
			this.m_cmsgRecipientInfo = cmsgRecipientInfo;
			this.m_index = index;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00016F7E File Offset: 0x0001517E
		public RecipientInfoType Type
		{
			get
			{
				return this.m_recipentInfoType;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600047E RID: 1150
		public abstract int Version { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600047F RID: 1151
		public abstract SubjectIdentifier RecipientIdentifier { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000480 RID: 1152
		public abstract AlgorithmIdentifier KeyEncryptionAlgorithm { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000481 RID: 1153
		public abstract byte[] EncryptedKey { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00016F86 File Offset: 0x00015186
		internal RecipientSubType SubType
		{
			get
			{
				return this.m_recipientSubType;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00016F8E File Offset: 0x0001518E
		internal SafeLocalAllocHandle pCmsgRecipientInfo
		{
			[SecurityCritical]
			get
			{
				return this.m_pCmsgRecipientInfo;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00016F96 File Offset: 0x00015196
		internal object CmsgRecipientInfo
		{
			get
			{
				return this.m_cmsgRecipientInfo;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00016F9E File Offset: 0x0001519E
		internal uint Index
		{
			get
			{
				return this.m_index;
			}
		}

		// Token: 0x040004DE RID: 1246
		private RecipientInfoType m_recipentInfoType;

		// Token: 0x040004DF RID: 1247
		private RecipientSubType m_recipientSubType;

		// Token: 0x040004E0 RID: 1248
		[SecurityCritical]
		private SafeLocalAllocHandle m_pCmsgRecipientInfo;

		// Token: 0x040004E1 RID: 1249
		private object m_cmsgRecipientInfo;

		// Token: 0x040004E2 RID: 1250
		private uint m_index;
	}
}
