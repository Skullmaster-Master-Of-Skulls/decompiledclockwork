using System;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000015 RID: 21
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public class WrappedKeySecurityToken : SecurityToken
	{
		// Token: 0x0600008C RID: 140 RVA: 0x0000325E File Offset: 0x0000145E
		internal WrappedKeySecurityToken(string id, byte[] keyToWrap, ISspiNegotiation wrappingSspiContext) : this(id, keyToWrap, (wrappingSspiContext != null) ? wrappingSspiContext.KeyEncryptionAlgorithm : null, wrappingSspiContext, null)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003276 File Offset: 0x00001476
		public WrappedKeySecurityToken(string id, byte[] keyToWrap, string wrappingAlgorithm, SecurityToken wrappingToken, SecurityKeyIdentifier wrappingTokenReference) : this(id, keyToWrap, wrappingAlgorithm, null, wrappingToken, wrappingTokenReference)
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003288 File Offset: 0x00001488
		internal WrappedKeySecurityToken(string id, byte[] keyToWrap, string wrappingAlgorithm, XmlDictionaryString wrappingAlgorithmDictionaryString, SecurityToken wrappingToken, SecurityKeyIdentifier wrappingTokenReference) : this(id, keyToWrap, wrappingAlgorithm, wrappingAlgorithmDictionaryString, wrappingToken, wrappingTokenReference, null, null)
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000032A8 File Offset: 0x000014A8
		internal WrappedKeySecurityToken(string id, byte[] keyToWrap, string wrappingAlgorithm, ISspiNegotiation wrappingSspiContext, byte[] wrappedKey) : this(id, keyToWrap, wrappingAlgorithm, null)
		{
			if (wrappingSspiContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappingSspiContext");
			}
			this.wrappingSspiContext = wrappingSspiContext;
			if (wrappedKey == null)
			{
				this.wrappedKey = wrappingSspiContext.Encrypt(keyToWrap);
			}
			else
			{
				this.wrappedKey = wrappedKey;
			}
			this.serializeCarriedKeyName = false;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003300 File Offset: 0x00001500
		internal WrappedKeySecurityToken(string id, byte[] keyToWrap, string wrappingAlgorithm, SecurityToken wrappingToken, SecurityKeyIdentifier wrappingTokenReference, byte[] wrappedKey, SecurityKey wrappingSecurityKey) : this(id, keyToWrap, wrappingAlgorithm, null, wrappingToken, wrappingTokenReference, wrappedKey, wrappingSecurityKey)
		{
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003320 File Offset: 0x00001520
		private WrappedKeySecurityToken(string id, byte[] keyToWrap, string wrappingAlgorithm, XmlDictionaryString wrappingAlgorithmDictionaryString, SecurityToken wrappingToken, SecurityKeyIdentifier wrappingTokenReference, byte[] wrappedKey, SecurityKey wrappingSecurityKey) : this(id, keyToWrap, wrappingAlgorithm, wrappingAlgorithmDictionaryString)
		{
			if (wrappingToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappingToken");
			}
			this.wrappingToken = wrappingToken;
			this.wrappingTokenReference = wrappingTokenReference;
			if (wrappedKey == null)
			{
				this.wrappedKey = SecurityUtils.EncryptKey(wrappingToken, wrappingAlgorithm, keyToWrap);
			}
			else
			{
				this.wrappedKey = wrappedKey;
			}
			this.wrappingSecurityKey = wrappingSecurityKey;
			this.serializeCarriedKeyName = true;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003388 File Offset: 0x00001588
		private WrappedKeySecurityToken(string id, byte[] keyToWrap, string wrappingAlgorithm, XmlDictionaryString wrappingAlgorithmDictionaryString)
		{
			if (id == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("id");
			}
			if (wrappingAlgorithm == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("wrappingAlgorithm");
			}
			if (keyToWrap == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityKeyToWrap");
			}
			this.id = id;
			this.effectiveTime = DateTime.UtcNow;
			this.securityKey = SecurityUtils.CreateSymmetricSecurityKeys(keyToWrap);
			this.wrappingAlgorithm = wrappingAlgorithm;
			this.wrappingAlgorithmDictionaryString = wrappingAlgorithmDictionaryString;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003401 File Offset: 0x00001601
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003409 File Offset: 0x00001609
		public override DateTime ValidFrom
		{
			get
			{
				return this.effectiveTime;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003213 File Offset: 0x00001413
		public override DateTime ValidTo
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003411 File Offset: 0x00001611
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003419 File Offset: 0x00001619
		internal EncryptedKey EncryptedKey
		{
			get
			{
				return this.encryptedKey;
			}
			set
			{
				this.encryptedKey = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003422 File Offset: 0x00001622
		internal ReferenceList ReferenceList
		{
			get
			{
				if (this.encryptedKey != null)
				{
					return this.encryptedKey.ReferenceList;
				}
				return null;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003439 File Offset: 0x00001639
		public string WrappingAlgorithm
		{
			get
			{
				return this.wrappingAlgorithm;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003441 File Offset: 0x00001641
		internal SecurityKey WrappingSecurityKey
		{
			get
			{
				return this.wrappingSecurityKey;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003449 File Offset: 0x00001649
		public SecurityToken WrappingToken
		{
			get
			{
				return this.wrappingToken;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003451 File Offset: 0x00001651
		public SecurityKeyIdentifier WrappingTokenReference
		{
			get
			{
				return this.wrappingTokenReference;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003459 File Offset: 0x00001659
		internal string CarriedKeyName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000345C File Offset: 0x0000165C
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				return this.securityKey;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003464 File Offset: 0x00001664
		internal byte[] GetHash()
		{
			if (this.wrappedKeyHash == null)
			{
				this.EnsureEncryptedKeySetUp();
				using (HashAlgorithm hashAlgorithm = CryptoHelper.NewSha1HashAlgorithm())
				{
					this.wrappedKeyHash = hashAlgorithm.ComputeHash(this.encryptedKey.GetWrappedKey());
				}
			}
			return this.wrappedKeyHash;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000034C0 File Offset: 0x000016C0
		public byte[] GetWrappedKey()
		{
			return SecurityUtils.CloneBuffer(this.wrappedKey);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000034D0 File Offset: 0x000016D0
		internal void EnsureEncryptedKeySetUp()
		{
			if (this.encryptedKey == null)
			{
				EncryptedKey encryptedKey = new EncryptedKey();
				encryptedKey.Id = this.Id;
				if (this.serializeCarriedKeyName)
				{
					encryptedKey.CarriedKeyName = this.CarriedKeyName;
				}
				else
				{
					encryptedKey.CarriedKeyName = null;
				}
				encryptedKey.EncryptionMethod = this.WrappingAlgorithm;
				encryptedKey.EncryptionMethodDictionaryString = this.wrappingAlgorithmDictionaryString;
				encryptedKey.SetUpKeyWrap(this.wrappedKey);
				if (this.WrappingTokenReference != null)
				{
					encryptedKey.KeyIdentifier = this.WrappingTokenReference;
				}
				this.encryptedKey = encryptedKey;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003553 File Offset: 0x00001753
		public override bool CanCreateKeyIdentifierClause<T>()
		{
			return typeof(T) == typeof(EncryptedKeyHashIdentifierClause) || base.CanCreateKeyIdentifierClause<T>();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003578 File Offset: 0x00001778
		public override T CreateKeyIdentifierClause<T>()
		{
			if (typeof(T) == typeof(EncryptedKeyHashIdentifierClause))
			{
				return new EncryptedKeyHashIdentifierClause(this.GetHash()) as T;
			}
			return base.CreateKeyIdentifierClause<T>();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000035B4 File Offset: 0x000017B4
		public override bool MatchesKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			EncryptedKeyHashIdentifierClause encryptedKeyHashIdentifierClause = keyIdentifierClause as EncryptedKeyHashIdentifierClause;
			if (encryptedKeyHashIdentifierClause != null)
			{
				return encryptedKeyHashIdentifierClause.Matches(this.GetHash());
			}
			return base.MatchesKeyIdentifierClause(keyIdentifierClause);
		}

		// Token: 0x04000082 RID: 130
		private string id;

		// Token: 0x04000083 RID: 131
		private DateTime effectiveTime;

		// Token: 0x04000084 RID: 132
		private EncryptedKey encryptedKey;

		// Token: 0x04000085 RID: 133
		private ReadOnlyCollection<SecurityKey> securityKey;

		// Token: 0x04000086 RID: 134
		private byte[] wrappedKey;

		// Token: 0x04000087 RID: 135
		private string wrappingAlgorithm;

		// Token: 0x04000088 RID: 136
		private ISspiNegotiation wrappingSspiContext;

		// Token: 0x04000089 RID: 137
		private SecurityToken wrappingToken;

		// Token: 0x0400008A RID: 138
		private SecurityKey wrappingSecurityKey;

		// Token: 0x0400008B RID: 139
		private SecurityKeyIdentifier wrappingTokenReference;

		// Token: 0x0400008C RID: 140
		private bool serializeCarriedKeyName;

		// Token: 0x0400008D RID: 141
		private byte[] wrappedKeyHash;

		// Token: 0x0400008E RID: 142
		private XmlDictionaryString wrappingAlgorithmDictionaryString;
	}
}
