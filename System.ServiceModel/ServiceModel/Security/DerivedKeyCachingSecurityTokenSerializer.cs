using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000316 RID: 790
	internal class DerivedKeyCachingSecurityTokenSerializer : SecurityTokenSerializer
	{
		// Token: 0x06001B40 RID: 6976 RVA: 0x0006602C File Offset: 0x0006422C
		internal DerivedKeyCachingSecurityTokenSerializer(int cacheSize, bool isInitiator, WSSecureConversation secureConversation, SecurityTokenSerializer innerTokenSerializer)
		{
			if (innerTokenSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("innerTokenSerializer");
			}
			if (secureConversation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("secureConversation");
			}
			if (cacheSize <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("cacheSize", SR.GetString("ValueMustBeGreaterThanZero")));
			}
			this.cachedTokens = new DerivedKeyCachingSecurityTokenSerializer.DerivedKeySecurityTokenCache[cacheSize];
			this.isInitiator = isInitiator;
			this.secureConversation = secureConversation;
			this.innerTokenSerializer = innerTokenSerializer;
			this.thisLock = new object();
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x000660B6 File Offset: 0x000642B6
		protected override bool CanReadKeyIdentifierClauseCore(XmlReader reader)
		{
			return this.innerTokenSerializer.CanReadKeyIdentifierClause(reader);
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x000660C4 File Offset: 0x000642C4
		protected override bool CanReadKeyIdentifierCore(XmlReader reader)
		{
			return this.innerTokenSerializer.CanReadKeyIdentifier(reader);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x000660D2 File Offset: 0x000642D2
		protected override bool CanReadTokenCore(XmlReader reader)
		{
			return this.innerTokenSerializer.CanReadToken(reader);
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000660E0 File Offset: 0x000642E0
		protected override SecurityToken ReadTokenCore(XmlReader reader, SecurityTokenResolver tokenResolver)
		{
			XmlDictionaryReader reader2 = XmlDictionaryReader.CreateDictionaryReader(reader);
			if (this.secureConversation.IsAtDerivedKeyToken(reader2))
			{
				string id;
				string derivationAlgorithm;
				string label;
				int length;
				byte[] nonce;
				int offset;
				int generation;
				SecurityKeyIdentifierClause tokenToDeriveIdentifier;
				SecurityToken tokenToDerive;
				this.secureConversation.ReadDerivedKeyTokenParameters(reader2, tokenResolver, out id, out derivationAlgorithm, out label, out length, out nonce, out offset, out generation, out tokenToDeriveIdentifier, out tokenToDerive);
				DerivedKeySecurityToken cachedToken = this.GetCachedToken(id, generation, offset, length, label, nonce, tokenToDerive, tokenToDeriveIdentifier, derivationAlgorithm);
				if (cachedToken != null)
				{
					return cachedToken;
				}
				object obj = this.thisLock;
				lock (obj)
				{
					cachedToken = this.GetCachedToken(id, generation, offset, length, label, nonce, tokenToDerive, tokenToDeriveIdentifier, derivationAlgorithm);
					if (cachedToken != null)
					{
						return cachedToken;
					}
					SecurityToken securityToken = this.secureConversation.CreateDerivedKeyToken(id, derivationAlgorithm, label, length, nonce, offset, generation, tokenToDeriveIdentifier, tokenToDerive);
					DerivedKeySecurityToken derivedKeySecurityToken = securityToken as DerivedKeySecurityToken;
					if (derivedKeySecurityToken != null)
					{
						int num = this.indexToCache;
						if (this.indexToCache == 2147483647)
						{
							this.indexToCache = 0;
						}
						else
						{
							int num2 = this.indexToCache + 1;
							this.indexToCache = num2;
							this.indexToCache = num2 % this.cachedTokens.Length;
						}
						this.cachedTokens[num] = new DerivedKeyCachingSecurityTokenSerializer.DerivedKeySecurityTokenCache(derivedKeySecurityToken);
					}
					return securityToken;
				}
			}
			return this.innerTokenSerializer.ReadToken(reader, tokenResolver);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x00066228 File Offset: 0x00064428
		protected override bool CanWriteKeyIdentifierClauseCore(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			return this.innerTokenSerializer.CanWriteKeyIdentifierClause(keyIdentifierClause);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x00066236 File Offset: 0x00064436
		protected override bool CanWriteKeyIdentifierCore(SecurityKeyIdentifier keyIdentifier)
		{
			return this.innerTokenSerializer.CanWriteKeyIdentifier(keyIdentifier);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x00066244 File Offset: 0x00064444
		protected override bool CanWriteTokenCore(SecurityToken token)
		{
			return this.innerTokenSerializer.CanWriteToken(token);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x00066252 File Offset: 0x00064452
		protected override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlReader reader)
		{
			return this.innerTokenSerializer.ReadKeyIdentifierClause(reader);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x00066260 File Offset: 0x00064460
		protected override SecurityKeyIdentifier ReadKeyIdentifierCore(XmlReader reader)
		{
			return this.innerTokenSerializer.ReadKeyIdentifier(reader);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0006626E File Offset: 0x0006446E
		protected override void WriteKeyIdentifierClauseCore(XmlWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
		{
			this.innerTokenSerializer.WriteKeyIdentifierClause(writer, keyIdentifierClause);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0006627D File Offset: 0x0006447D
		protected override void WriteKeyIdentifierCore(XmlWriter writer, SecurityKeyIdentifier keyIdentifier)
		{
			this.innerTokenSerializer.WriteKeyIdentifier(writer, keyIdentifier);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0006628C File Offset: 0x0006448C
		protected override void WriteTokenCore(XmlWriter writer, SecurityToken token)
		{
			this.innerTokenSerializer.WriteToken(writer, token);
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0006629C File Offset: 0x0006449C
		private bool IsMatch(DerivedKeyCachingSecurityTokenSerializer.DerivedKeySecurityTokenCache cachedToken, string id, int generation, int offset, int length, string label, byte[] nonce, SecurityToken tokenToDerive, string derivationAlgorithm)
		{
			return cachedToken.Generation == generation && cachedToken.Offset == offset && cachedToken.Length == length && cachedToken.Label == label && cachedToken.KeyDerivationAlgorithm == derivationAlgorithm && cachedToken.IsSourceKeyEqual(tokenToDerive) && CryptoHelper.IsEqual(cachedToken.Nonce, nonce) && cachedToken.SecurityKeys != null;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0006630C File Offset: 0x0006450C
		private DerivedKeySecurityToken GetCachedToken(string id, int generation, int offset, int length, string label, byte[] nonce, SecurityToken tokenToDerive, SecurityKeyIdentifierClause tokenToDeriveIdentifier, string derivationAlgorithm)
		{
			for (int i = 0; i < this.cachedTokens.Length; i++)
			{
				DerivedKeyCachingSecurityTokenSerializer.DerivedKeySecurityTokenCache derivedKeySecurityTokenCache = this.cachedTokens[i];
				if (derivedKeySecurityTokenCache != null && this.IsMatch(derivedKeySecurityTokenCache, id, generation, offset, length, label, nonce, tokenToDerive, derivationAlgorithm))
				{
					DerivedKeySecurityToken derivedKeySecurityToken = new DerivedKeySecurityToken(generation, offset, length, label, nonce, tokenToDerive, tokenToDeriveIdentifier, derivationAlgorithm, id);
					derivedKeySecurityToken.InitializeDerivedKey(derivedKeySecurityTokenCache.SecurityKeys);
					return derivedKeySecurityToken;
				}
			}
			return null;
		}

		// Token: 0x04001D65 RID: 7525
		private DerivedKeyCachingSecurityTokenSerializer.DerivedKeySecurityTokenCache[] cachedTokens;

		// Token: 0x04001D66 RID: 7526
		private WSSecureConversation secureConversation;

		// Token: 0x04001D67 RID: 7527
		private SecurityTokenSerializer innerTokenSerializer;

		// Token: 0x04001D68 RID: 7528
		private bool isInitiator;

		// Token: 0x04001D69 RID: 7529
		private int indexToCache;

		// Token: 0x04001D6A RID: 7530
		private object thisLock;

		// Token: 0x02000B6C RID: 2924
		private class DerivedKeySecurityTokenCache
		{
			// Token: 0x0600725C RID: 29276 RVA: 0x001AB07C File Offset: 0x001A927C
			public DerivedKeySecurityTokenCache(DerivedKeySecurityToken cachedToken)
			{
				this.keyToDerive = ((SymmetricSecurityKey)cachedToken.TokenToDerive.SecurityKeys[0]).GetSymmetricKey();
				this.generation = cachedToken.Generation;
				this.offset = cachedToken.Offset;
				this.length = cachedToken.Length;
				this.label = cachedToken.Label;
				this.keyDerivationAlgorithm = cachedToken.KeyDerivationAlgorithm;
				this.nonce = cachedToken.Nonce;
				this.cachedToken = cachedToken;
			}

			// Token: 0x17001A86 RID: 6790
			// (get) Token: 0x0600725D RID: 29277 RVA: 0x001AB0FF File Offset: 0x001A92FF
			public int Generation
			{
				get
				{
					return this.generation;
				}
			}

			// Token: 0x17001A87 RID: 6791
			// (get) Token: 0x0600725E RID: 29278 RVA: 0x001AB107 File Offset: 0x001A9307
			public int Offset
			{
				get
				{
					return this.offset;
				}
			}

			// Token: 0x17001A88 RID: 6792
			// (get) Token: 0x0600725F RID: 29279 RVA: 0x001AB10F File Offset: 0x001A930F
			public int Length
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x17001A89 RID: 6793
			// (get) Token: 0x06007260 RID: 29280 RVA: 0x001AB117 File Offset: 0x001A9317
			public string Label
			{
				get
				{
					return this.label;
				}
			}

			// Token: 0x17001A8A RID: 6794
			// (get) Token: 0x06007261 RID: 29281 RVA: 0x001AB11F File Offset: 0x001A931F
			public string KeyDerivationAlgorithm
			{
				get
				{
					return this.keyDerivationAlgorithm;
				}
			}

			// Token: 0x17001A8B RID: 6795
			// (get) Token: 0x06007262 RID: 29282 RVA: 0x001AB127 File Offset: 0x001A9327
			public byte[] Nonce
			{
				get
				{
					return this.nonce;
				}
			}

			// Token: 0x17001A8C RID: 6796
			// (get) Token: 0x06007263 RID: 29283 RVA: 0x001AB130 File Offset: 0x001A9330
			public ReadOnlyCollection<SecurityKey> SecurityKeys
			{
				get
				{
					lock (this)
					{
						ReadOnlyCollection<SecurityKey> readOnlyCollection;
						if (this.keys == null && this.cachedToken.TryGetSecurityKeys(out readOnlyCollection))
						{
							this.keys = readOnlyCollection;
							this.cachedToken = null;
						}
					}
					return this.keys;
				}
			}

			// Token: 0x06007264 RID: 29284 RVA: 0x001AB190 File Offset: 0x001A9390
			public bool IsSourceKeyEqual(SecurityToken token)
			{
				if (token.SecurityKeys.Count != 1)
				{
					return false;
				}
				SymmetricSecurityKey symmetricSecurityKey = token.SecurityKeys[0] as SymmetricSecurityKey;
				return symmetricSecurityKey != null && CryptoHelper.IsEqual(this.keyToDerive, symmetricSecurityKey.GetSymmetricKey());
			}

			// Token: 0x040040B7 RID: 16567
			private byte[] keyToDerive;

			// Token: 0x040040B8 RID: 16568
			private int generation;

			// Token: 0x040040B9 RID: 16569
			private int offset;

			// Token: 0x040040BA RID: 16570
			private int length;

			// Token: 0x040040BB RID: 16571
			private string label;

			// Token: 0x040040BC RID: 16572
			private string keyDerivationAlgorithm;

			// Token: 0x040040BD RID: 16573
			private byte[] nonce;

			// Token: 0x040040BE RID: 16574
			private ReadOnlyCollection<SecurityKey> keys;

			// Token: 0x040040BF RID: 16575
			private DerivedKeySecurityToken cachedToken;
		}
	}
}
