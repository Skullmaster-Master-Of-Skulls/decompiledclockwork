using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Configuration;
using System.IdentityModel.Tokens;
using System.Xml;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001A9 RID: 425
	public abstract class SecurityTokenResolver : ICustomIdentityConfiguration
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003FAF0 File Offset: 0x0003DCF0
		public SecurityToken ResolveToken(SecurityKeyIdentifier keyIdentifier)
		{
			if (keyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifier");
			}
			SecurityToken result;
			if (!this.TryResolveTokenCore(keyIdentifier, out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("UnableToResolveTokenReference", new object[]
				{
					keyIdentifier
				})));
			}
			return result;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003FB40 File Offset: 0x0003DD40
		public bool TryResolveToken(SecurityKeyIdentifier keyIdentifier, out SecurityToken token)
		{
			if (keyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifier");
			}
			return this.TryResolveTokenCore(keyIdentifier, out token);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003FB60 File Offset: 0x0003DD60
		public SecurityToken ResolveToken(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			SecurityToken result;
			if (!this.TryResolveTokenCore(keyIdentifierClause, out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("UnableToResolveTokenReference", new object[]
				{
					keyIdentifierClause
				})));
			}
			return result;
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003FBB0 File Offset: 0x0003DDB0
		public bool TryResolveToken(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			return this.TryResolveTokenCore(keyIdentifierClause, out token);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0003FBD0 File Offset: 0x0003DDD0
		public SecurityKey ResolveSecurityKey(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			SecurityKey result;
			if (!this.TryResolveSecurityKeyCore(keyIdentifierClause, out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("UnableToResolveKeyReference", new object[]
				{
					keyIdentifierClause
				})));
			}
			return result;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003FC20 File Offset: 0x0003DE20
		public bool TryResolveSecurityKey(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityKey key)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			return this.TryResolveSecurityKeyCore(keyIdentifierClause, out key);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0000443A File Offset: 0x0000263A
		public virtual void LoadCustomConfiguration(XmlNodeList nodelist)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID0023", new object[]
			{
				base.GetType().AssemblyQualifiedName
			})));
		}

		// Token: 0x06000DE7 RID: 3559
		protected abstract bool TryResolveTokenCore(SecurityKeyIdentifier keyIdentifier, out SecurityToken token);

		// Token: 0x06000DE8 RID: 3560
		protected abstract bool TryResolveTokenCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token);

		// Token: 0x06000DE9 RID: 3561
		protected abstract bool TryResolveSecurityKeyCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityKey key);

		// Token: 0x06000DEA RID: 3562 RVA: 0x0003FC3D File Offset: 0x0003DE3D
		public static SecurityTokenResolver CreateDefaultSecurityTokenResolver(ReadOnlyCollection<SecurityToken> tokens, bool canMatchLocalId)
		{
			return new SecurityTokenResolver.SimpleTokenResolver(tokens, canMatchLocalId);
		}

		// Token: 0x02000291 RID: 657
		private class SimpleTokenResolver : SecurityTokenResolver
		{
			// Token: 0x0600134E RID: 4942 RVA: 0x00052817 File Offset: 0x00050A17
			public SimpleTokenResolver(ReadOnlyCollection<SecurityToken> tokens, bool canMatchLocalId)
			{
				if (tokens == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokens");
				}
				this.tokens = tokens;
				this.canMatchLocalId = canMatchLocalId;
			}

			// Token: 0x0600134F RID: 4943 RVA: 0x00052840 File Offset: 0x00050A40
			protected override bool TryResolveSecurityKeyCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityKey key)
			{
				if (keyIdentifierClause == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
				}
				key = null;
				for (int i = 0; i < this.tokens.Count; i++)
				{
					SecurityKey securityKey = this.tokens[i].ResolveKeyIdentifierClause(keyIdentifierClause);
					if (securityKey != null)
					{
						key = securityKey;
						return true;
					}
				}
				if (keyIdentifierClause is EncryptedKeyIdentifierClause)
				{
					EncryptedKeyIdentifierClause encryptedKeyIdentifierClause = (EncryptedKeyIdentifierClause)keyIdentifierClause;
					SecurityKeyIdentifier encryptingKeyIdentifier = encryptedKeyIdentifierClause.EncryptingKeyIdentifier;
					if (encryptingKeyIdentifier != null && encryptingKeyIdentifier.Count > 0)
					{
						for (int j = 0; j < encryptingKeyIdentifier.Count; j++)
						{
							SecurityKey securityKey2 = null;
							if (base.TryResolveSecurityKey(encryptingKeyIdentifier[j], out securityKey2))
							{
								byte[] encryptedKey = encryptedKeyIdentifierClause.GetEncryptedKey();
								string encryptionMethod = encryptedKeyIdentifierClause.EncryptionMethod;
								byte[] symmetricKey = securityKey2.DecryptKey(encryptionMethod, encryptedKey);
								key = new InMemorySymmetricSecurityKey(symmetricKey, false);
								return true;
							}
						}
					}
				}
				return key != null;
			}

			// Token: 0x06001350 RID: 4944 RVA: 0x00052910 File Offset: 0x00050B10
			protected override bool TryResolveTokenCore(SecurityKeyIdentifier keyIdentifier, out SecurityToken token)
			{
				if (keyIdentifier == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifier");
				}
				token = null;
				for (int i = 0; i < keyIdentifier.Count; i++)
				{
					SecurityToken securityToken = this.ResolveSecurityToken(keyIdentifier[i]);
					if (securityToken != null)
					{
						token = securityToken;
						break;
					}
				}
				return token != null;
			}

			// Token: 0x06001351 RID: 4945 RVA: 0x00052960 File Offset: 0x00050B60
			protected override bool TryResolveTokenCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token)
			{
				if (keyIdentifierClause == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
				}
				token = null;
				SecurityToken securityToken = this.ResolveSecurityToken(keyIdentifierClause);
				if (securityToken != null)
				{
					token = securityToken;
				}
				return token != null;
			}

			// Token: 0x06001352 RID: 4946 RVA: 0x00052998 File Offset: 0x00050B98
			private SecurityToken ResolveSecurityToken(SecurityKeyIdentifierClause keyIdentifierClause)
			{
				if (keyIdentifierClause == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
				}
				if (!this.canMatchLocalId && keyIdentifierClause is LocalIdKeyIdentifierClause)
				{
					return null;
				}
				for (int i = 0; i < this.tokens.Count; i++)
				{
					if (this.tokens[i].MatchesKeyIdentifierClause(keyIdentifierClause))
					{
						return this.tokens[i];
					}
				}
				return null;
			}

			// Token: 0x04001132 RID: 4402
			private ReadOnlyCollection<SecurityToken> tokens;

			// Token: 0x04001133 RID: 4403
			private bool canMatchLocalId;
		}
	}
}
