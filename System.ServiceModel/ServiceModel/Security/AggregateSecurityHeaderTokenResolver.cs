using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Security
{
	// Token: 0x020002A9 RID: 681
	internal class AggregateSecurityHeaderTokenResolver : AggregateTokenResolver
	{
		// Token: 0x06001505 RID: 5381 RVA: 0x0004F14C File Offset: 0x0004D34C
		public AggregateSecurityHeaderTokenResolver(SecurityHeaderTokenResolver tokenResolver, ReadOnlyCollection<SecurityTokenResolver> outOfBandTokenResolvers) : base(outOfBandTokenResolvers)
		{
			if (tokenResolver == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenResolver");
			}
			this.tokenResolver = tokenResolver;
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0004F170 File Offset: 0x0004D370
		protected override bool TryResolveSecurityKeyCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityKey key)
		{
			key = null;
			bool flag = this.tokenResolver.TryResolveSecurityKey(keyIdentifierClause, false, out key);
			if (!flag)
			{
				flag = base.TryResolveSecurityKeyCore(keyIdentifierClause, out key);
			}
			if (!flag)
			{
				flag = SecurityUtils.TryCreateKeyFromIntrinsicKeyClause(keyIdentifierClause, this, out key);
			}
			return flag;
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0004F1AC File Offset: 0x0004D3AC
		protected override bool TryResolveTokenCore(SecurityKeyIdentifier keyIdentifier, out SecurityToken token)
		{
			token = null;
			bool flag = this.tokenResolver.TryResolveToken(keyIdentifier, false, false, out token);
			if (!flag)
			{
				flag = base.TryResolveTokenCore(keyIdentifier, out token);
			}
			if (!flag)
			{
				for (int i = 0; i < keyIdentifier.Count; i++)
				{
					if (this.TryResolveTokenFromIntrinsicKeyClause(keyIdentifier[i], out token))
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0004F204 File Offset: 0x0004D404
		private bool TryResolveTokenFromIntrinsicKeyClause(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token)
		{
			token = null;
			if (keyIdentifierClause is RsaKeyIdentifierClause)
			{
				token = new RsaSecurityToken(((RsaKeyIdentifierClause)keyIdentifierClause).Rsa);
				return true;
			}
			if (keyIdentifierClause is X509RawDataKeyIdentifierClause)
			{
				token = new X509SecurityToken(new X509Certificate2(((X509RawDataKeyIdentifierClause)keyIdentifierClause).GetX509RawData()), false);
				return true;
			}
			if (keyIdentifierClause is EncryptedKeyIdentifierClause)
			{
				EncryptedKeyIdentifierClause encryptedKeyIdentifierClause = (EncryptedKeyIdentifierClause)keyIdentifierClause;
				SecurityKeyIdentifier encryptingKeyIdentifier = encryptedKeyIdentifierClause.EncryptingKeyIdentifier;
				SecurityToken unwrappingToken;
				if (base.TryResolveToken(encryptingKeyIdentifier, out unwrappingToken))
				{
					token = SecurityUtils.CreateTokenFromEncryptedKeyClause(encryptedKeyIdentifierClause, unwrappingToken);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0004F280 File Offset: 0x0004D480
		protected override bool TryResolveTokenCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token)
		{
			token = null;
			bool flag = this.tokenResolver.TryResolveToken(keyIdentifierClause, false, false, out token);
			if (!flag)
			{
				flag = base.TryResolveTokenCore(keyIdentifierClause, out token);
			}
			if (!flag)
			{
				flag = this.TryResolveTokenFromIntrinsicKeyClause(keyIdentifierClause, out token);
			}
			return flag;
		}

		// Token: 0x04001B0B RID: 6923
		private SecurityHeaderTokenResolver tokenResolver;
	}
}
