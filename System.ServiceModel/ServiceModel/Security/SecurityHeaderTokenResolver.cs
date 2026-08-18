using System;
using System.Globalization;
using System.IdentityModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002BC RID: 700
	internal sealed class SecurityHeaderTokenResolver : SecurityTokenResolver, IWrappedTokenKeyResolver
	{
		// Token: 0x06001618 RID: 5656 RVA: 0x00053F87 File Offset: 0x00052187
		public SecurityHeaderTokenResolver() : this(null)
		{
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00053F90 File Offset: 0x00052190
		public SecurityHeaderTokenResolver(ReceiveSecurityHeader securityHeader)
		{
			this.tokens = new SecurityHeaderTokenResolver.SecurityTokenEntry[10];
			this.securityHeader = securityHeader;
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x00053FAC File Offset: 0x000521AC
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x00053FB4 File Offset: 0x000521B4
		public SecurityToken ExpectedWrapper
		{
			get
			{
				return this.expectedWrapper;
			}
			set
			{
				this.expectedWrapper = value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x00053FBD File Offset: 0x000521BD
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x00053FC5 File Offset: 0x000521C5
		public SecurityTokenParameters ExpectedWrapperTokenParameters
		{
			get
			{
				return this.expectedWrapperTokenParameters;
			}
			set
			{
				this.expectedWrapperTokenParameters = value;
			}
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x00053FCE File Offset: 0x000521CE
		public void Add(SecurityToken token)
		{
			this.Add(token, SecurityTokenReferenceStyle.Internal, null);
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x00053FDC File Offset: 0x000521DC
		public void Add(SecurityToken token, SecurityTokenReferenceStyle allowedReferenceStyle, SecurityTokenParameters tokenParameters)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			if (allowedReferenceStyle == SecurityTokenReferenceStyle.External && tokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("ResolvingExternalTokensRequireSecurityTokenParameters"));
			}
			this.EnsureCapacityToAddToken();
			SecurityHeaderTokenResolver.SecurityTokenEntry[] array = this.tokens;
			int num = this.tokenCount;
			this.tokenCount = num + 1;
			array[num] = new SecurityHeaderTokenResolver.SecurityTokenEntry(token, tokenParameters, allowedReferenceStyle);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00054044 File Offset: 0x00052244
		private void EnsureCapacityToAddToken()
		{
			if (this.tokenCount == this.tokens.Length)
			{
				SecurityHeaderTokenResolver.SecurityTokenEntry[] destinationArray = new SecurityHeaderTokenResolver.SecurityTokenEntry[this.tokens.Length * 2];
				Array.Copy(this.tokens, 0, destinationArray, 0, this.tokenCount);
				this.tokens = destinationArray;
			}
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0005408C File Offset: 0x0005228C
		public bool CheckExternalWrapperMatch(SecurityKeyIdentifier keyIdentifier)
		{
			if (this.expectedWrapper == null || this.expectedWrapperTokenParameters == null)
			{
				return false;
			}
			for (int i = 0; i < keyIdentifier.Count; i++)
			{
				if (this.expectedWrapperTokenParameters.MatchesKeyIdentifierClause(this.expectedWrapper, keyIdentifier[i], SecurityTokenReferenceStyle.External))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x000540DC File Offset: 0x000522DC
		internal SecurityToken ResolveToken(SecurityKeyIdentifier keyIdentifier, bool matchOnlyExternalTokens, bool resolveIntrinsicKeyClause)
		{
			if (keyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifier");
			}
			for (int i = 0; i < keyIdentifier.Count; i++)
			{
				SecurityToken securityToken = this.ResolveToken(keyIdentifier[i], matchOnlyExternalTokens, resolveIntrinsicKeyClause);
				if (securityToken != null)
				{
					return securityToken;
				}
			}
			return null;
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x00054124 File Offset: 0x00052324
		private SecurityKey ResolveSecurityKeyCore(SecurityKeyIdentifierClause keyIdentifierClause, bool createIntrinsicKeys)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("keyIdentifierClause"));
			}
			SecurityKey securityKey;
			for (int i = 0; i < this.tokenCount; i++)
			{
				securityKey = this.tokens[i].Token.ResolveKeyIdentifierClause(keyIdentifierClause);
				if (securityKey != null)
				{
					return securityKey;
				}
			}
			if (createIntrinsicKeys && SecurityUtils.TryCreateKeyFromIntrinsicKeyClause(keyIdentifierClause, this, out securityKey))
			{
				return securityKey;
			}
			return null;
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x00054188 File Offset: 0x00052388
		private bool MatchDirectReference(SecurityToken token, SecurityKeyIdentifierClause keyClause)
		{
			LocalIdKeyIdentifierClause localIdKeyIdentifierClause = keyClause as LocalIdKeyIdentifierClause;
			return localIdKeyIdentifierClause != null && token.MatchesKeyIdentifierClause(localIdKeyIdentifierClause);
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000541A8 File Offset: 0x000523A8
		internal SecurityToken ResolveToken(SecurityKeyIdentifierClause keyIdentifierClause, bool matchOnlyExternal, bool resolveIntrinsicKeyClause)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			SecurityToken securityToken = null;
			for (int i = 0; i < this.tokenCount; i++)
			{
				if (!matchOnlyExternal || this.tokens[i].AllowedReferenceStyle == SecurityTokenReferenceStyle.External)
				{
					SecurityToken token = this.tokens[i].Token;
					if (this.tokens[i].TokenParameters != null && this.tokens[i].TokenParameters.MatchesKeyIdentifierClause(token, keyIdentifierClause, this.tokens[i].AllowedReferenceStyle))
					{
						securityToken = token;
						break;
					}
					if (this.tokens[i].TokenParameters == null && this.tokens[i].AllowedReferenceStyle == SecurityTokenReferenceStyle.Internal && this.MatchDirectReference(token, keyIdentifierClause))
					{
						securityToken = token;
						break;
					}
				}
			}
			if (securityToken == null && keyIdentifierClause is EncryptedKeyIdentifierClause)
			{
				EncryptedKeyIdentifierClause encryptedKeyIdentifierClause = (EncryptedKeyIdentifierClause)keyIdentifierClause;
				SecurityKeyIdentifier encryptingKeyIdentifier = encryptedKeyIdentifierClause.EncryptingKeyIdentifier;
				SecurityToken securityToken2;
				if (this.expectedWrapper != null && this.CheckExternalWrapperMatch(encryptingKeyIdentifier))
				{
					securityToken2 = this.expectedWrapper;
				}
				else
				{
					securityToken2 = this.ResolveToken(encryptingKeyIdentifier, true, resolveIntrinsicKeyClause);
				}
				if (securityToken2 != null)
				{
					securityToken = SecurityUtils.CreateTokenFromEncryptedKeyClause(encryptedKeyIdentifierClause, securityToken2);
				}
			}
			if (securityToken == null && keyIdentifierClause is X509RawDataKeyIdentifierClause && !matchOnlyExternal && resolveIntrinsicKeyClause)
			{
				securityToken = new X509SecurityToken(new X509Certificate2(((X509RawDataKeyIdentifierClause)keyIdentifierClause).GetX509RawData()));
			}
			byte[] derivationNonce = keyIdentifierClause.GetDerivationNonce();
			if (securityToken != null && derivationNonce != null)
			{
				if (SecurityUtils.GetSecurityKey<SymmetricSecurityKey>(securityToken) == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnableToDeriveKeyFromKeyInfoClause", new object[]
					{
						keyIdentifierClause,
						securityToken
					})));
				}
				int num = (keyIdentifierClause.DerivationLength == 0) ? 32 : keyIdentifierClause.DerivationLength;
				if (num > this.securityHeader.MaxDerivedKeyLength)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("DerivedKeyLengthSpecifiedInImplicitDerivedKeyClauseTooLong", new object[]
					{
						keyIdentifierClause.ToString(),
						num,
						this.securityHeader.MaxDerivedKeyLength
					})));
				}
				bool flag = false;
				for (int j = 0; j < this.tokenCount; j++)
				{
					DerivedKeySecurityToken derivedKeySecurityToken = this.tokens[j].Token as DerivedKeySecurityToken;
					if (derivedKeySecurityToken != null && derivedKeySecurityToken.Length == num && CryptoHelper.IsEqual(derivedKeySecurityToken.Nonce, derivationNonce) && derivedKeySecurityToken.TokenToDerive.MatchesKeyIdentifierClause(keyIdentifierClause))
					{
						securityToken = this.tokens[j].Token;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					string keyDerivationAlgorithm = SecurityUtils.GetKeyDerivationAlgorithm(this.securityHeader.StandardsManager.MessageSecurityVersion.SecureConversationVersion);
					securityToken = new DerivedKeySecurityToken(-1, 0, num, null, derivationNonce, securityToken, keyIdentifierClause, keyDerivationAlgorithm, SecurityUtils.GenerateId());
					((DerivedKeySecurityToken)securityToken).InitializeDerivedKey(num);
					this.Add(securityToken, SecurityTokenReferenceStyle.Internal, null);
					this.securityHeader.EnsureDerivedKeyLimitNotReached();
				}
			}
			return securityToken;
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0005447C File Offset: 0x0005267C
		public override string ToString()
		{
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				stringWriter.WriteLine("SecurityTokenResolver");
				stringWriter.WriteLine("    (");
				stringWriter.WriteLine("    TokenCount = {0},", this.tokenCount);
				for (int i = 0; i < this.tokenCount; i++)
				{
					stringWriter.WriteLine("    TokenEntry[{0}] = (AllowedReferenceStyle={1}, Token={2}, Parameters={3})", new object[]
					{
						i,
						this.tokens[i].AllowedReferenceStyle,
						this.tokens[i].Token.GetType(),
						this.tokens[i].TokenParameters
					});
				}
				stringWriter.WriteLine("    )");
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00054560 File Offset: 0x00052760
		protected override bool TryResolveTokenCore(SecurityKeyIdentifier keyIdentifier, out SecurityToken token)
		{
			token = this.ResolveToken(keyIdentifier, false, true);
			return token != null;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00054572 File Offset: 0x00052772
		internal bool TryResolveToken(SecurityKeyIdentifier keyIdentifier, bool matchOnlyExternalTokens, bool resolveIntrinsicKeyClause, out SecurityToken token)
		{
			token = this.ResolveToken(keyIdentifier, matchOnlyExternalTokens, resolveIntrinsicKeyClause);
			return token != null;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00054586 File Offset: 0x00052786
		protected override bool TryResolveTokenCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityToken token)
		{
			token = this.ResolveToken(keyIdentifierClause, false, true);
			return token != null;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00054598 File Offset: 0x00052798
		internal bool TryResolveToken(SecurityKeyIdentifierClause keyIdentifierClause, bool matchOnlyExternalTokens, bool resolveIntrinsicKeyClause, out SecurityToken token)
		{
			token = this.ResolveToken(keyIdentifierClause, matchOnlyExternalTokens, resolveIntrinsicKeyClause);
			return token != null;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x000545AC File Offset: 0x000527AC
		internal bool TryResolveSecurityKey(SecurityKeyIdentifierClause keyIdentifierClause, bool createIntrinsicKeys, out SecurityKey key)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			key = this.ResolveSecurityKeyCore(keyIdentifierClause, createIntrinsicKeys);
			return key != null;
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x000545D0 File Offset: 0x000527D0
		protected override bool TryResolveSecurityKeyCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityKey key)
		{
			key = this.ResolveSecurityKeyCore(keyIdentifierClause, true);
			return key != null;
		}

		// Token: 0x04001BAC RID: 7084
		private const int InitialTokenArraySize = 10;

		// Token: 0x04001BAD RID: 7085
		private int tokenCount;

		// Token: 0x04001BAE RID: 7086
		private SecurityHeaderTokenResolver.SecurityTokenEntry[] tokens;

		// Token: 0x04001BAF RID: 7087
		private SecurityToken expectedWrapper;

		// Token: 0x04001BB0 RID: 7088
		private SecurityTokenParameters expectedWrapperTokenParameters;

		// Token: 0x04001BB1 RID: 7089
		private ReceiveSecurityHeader securityHeader;

		// Token: 0x02000B49 RID: 2889
		private struct SecurityTokenEntry
		{
			// Token: 0x060070E2 RID: 28898 RVA: 0x001A431B File Offset: 0x001A251B
			public SecurityTokenEntry(SecurityToken token, SecurityTokenParameters tokenParameters, SecurityTokenReferenceStyle allowedReferenceStyle)
			{
				this.token = token;
				this.tokenParameters = tokenParameters;
				this.allowedReferenceStyle = allowedReferenceStyle;
			}

			// Token: 0x17001A55 RID: 6741
			// (get) Token: 0x060070E3 RID: 28899 RVA: 0x001A4332 File Offset: 0x001A2532
			public SecurityToken Token
			{
				get
				{
					return this.token;
				}
			}

			// Token: 0x17001A56 RID: 6742
			// (get) Token: 0x060070E4 RID: 28900 RVA: 0x001A433A File Offset: 0x001A253A
			public SecurityTokenParameters TokenParameters
			{
				get
				{
					return this.tokenParameters;
				}
			}

			// Token: 0x17001A57 RID: 6743
			// (get) Token: 0x060070E5 RID: 28901 RVA: 0x001A4342 File Offset: 0x001A2542
			public SecurityTokenReferenceStyle AllowedReferenceStyle
			{
				get
				{
					return this.allowedReferenceStyle;
				}
			}

			// Token: 0x04004033 RID: 16435
			private SecurityTokenParameters tokenParameters;

			// Token: 0x04004034 RID: 16436
			private SecurityToken token;

			// Token: 0x04004035 RID: 16437
			private SecurityTokenReferenceStyle allowedReferenceStyle;
		}
	}
}
