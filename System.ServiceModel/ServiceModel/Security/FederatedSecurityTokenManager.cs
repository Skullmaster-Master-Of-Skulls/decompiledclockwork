using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x02000358 RID: 856
	internal sealed class FederatedSecurityTokenManager : ServiceCredentialsSecurityTokenManager
	{
		// Token: 0x06001F76 RID: 8054 RVA: 0x00074EE8 File Offset: 0x000730E8
		public FederatedSecurityTokenManager(ServiceCredentials parentCredentials) : base(parentCredentials)
		{
			if (parentCredentials == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parentCredentials");
			}
			if (parentCredentials.IdentityConfiguration == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parentCredentials.IdentityConfiguration");
			}
			this._exceptionMapper = parentCredentials.ExceptionMapper;
			this._securityTokenHandlerCollection = parentCredentials.IdentityConfiguration.SecurityTokenHandlers;
			this._tokenCache = this._securityTokenHandlerCollection.Configuration.Caches.SessionSecurityTokenCache;
			this._cookieTransforms = SessionSecurityTokenHandler.DefaultCookieTransforms;
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x00074F75 File Offset: 0x00073175
		public SecurityTokenHandlerCollection SecurityTokenHandlers
		{
			get
			{
				return this._securityTokenHandlerCollection;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x00074F7D File Offset: 0x0007317D
		// (set) Token: 0x06001F79 RID: 8057 RVA: 0x00074F85 File Offset: 0x00073185
		public ExceptionMapper ExceptionMapper
		{
			get
			{
				return this._exceptionMapper;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._exceptionMapper = value;
			}
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x00074FA4 File Offset: 0x000731A4
		public override SecurityTokenAuthenticator CreateSecurityTokenAuthenticator(SecurityTokenRequirement tokenRequirement, out SecurityTokenResolver outOfBandTokenResolver)
		{
			if (tokenRequirement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenRequirement");
			}
			outOfBandTokenResolver = null;
			string tokenType = tokenRequirement.TokenType;
			if (string.IsNullOrEmpty(tokenType))
			{
				return this.CreateSamlSecurityTokenAuthenticator(tokenRequirement, out outOfBandTokenResolver);
			}
			SecurityTokenHandler securityTokenHandler = this._securityTokenHandlerCollection[tokenType];
			SecurityTokenAuthenticator result;
			if (securityTokenHandler != null && securityTokenHandler.CanValidateToken)
			{
				outOfBandTokenResolver = this.GetDefaultOutOfBandTokenResolver();
				if (StringComparer.Ordinal.Equals(tokenType, SecurityTokenTypes.UserName))
				{
					UserNameSecurityTokenHandler userNameSecurityTokenHandler = securityTokenHandler as UserNameSecurityTokenHandler;
					if (userNameSecurityTokenHandler == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4072", new object[]
						{
							securityTokenHandler.GetType(),
							tokenType,
							typeof(UserNameSecurityTokenHandler)
						})));
					}
					result = new WrappedUserNameSecurityTokenAuthenticator(userNameSecurityTokenHandler, this._exceptionMapper);
				}
				else if (StringComparer.Ordinal.Equals(tokenType, SecurityTokenTypes.Kerberos))
				{
					result = this.CreateInnerSecurityTokenAuthenticator(tokenRequirement, out outOfBandTokenResolver);
				}
				else if (StringComparer.Ordinal.Equals(tokenType, SecurityTokenTypes.Rsa))
				{
					RsaSecurityTokenHandler rsaSecurityTokenHandler = securityTokenHandler as RsaSecurityTokenHandler;
					if (rsaSecurityTokenHandler == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4072", new object[]
						{
							securityTokenHandler.GetType(),
							tokenType,
							typeof(RsaSecurityTokenHandler)
						})));
					}
					result = new WrappedRsaSecurityTokenAuthenticator(rsaSecurityTokenHandler, this._exceptionMapper);
				}
				else if (StringComparer.Ordinal.Equals(tokenType, SecurityTokenTypes.X509Certificate))
				{
					X509SecurityTokenHandler x509SecurityTokenHandler = securityTokenHandler as X509SecurityTokenHandler;
					if (x509SecurityTokenHandler == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4072", new object[]
						{
							securityTokenHandler.GetType(),
							tokenType,
							typeof(X509SecurityTokenHandler)
						})));
					}
					result = new WrappedX509SecurityTokenAuthenticator(x509SecurityTokenHandler, this._exceptionMapper);
				}
				else if (StringComparer.Ordinal.Equals(tokenType, "urn:oasis:names:tc:SAML:1.0:assertion") || StringComparer.Ordinal.Equals(tokenType, "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1"))
				{
					SamlSecurityTokenHandler samlSecurityTokenHandler = securityTokenHandler as SamlSecurityTokenHandler;
					if (samlSecurityTokenHandler == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4072", new object[]
						{
							securityTokenHandler.GetType(),
							tokenType,
							typeof(SamlSecurityTokenHandler)
						})));
					}
					if (samlSecurityTokenHandler.Configuration == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
					}
					result = new WrappedSaml11SecurityTokenAuthenticator(samlSecurityTokenHandler, this._exceptionMapper);
					outOfBandTokenResolver = samlSecurityTokenHandler.Configuration.ServiceTokenResolver;
				}
				else if (StringComparer.Ordinal.Equals(tokenType, "urn:oasis:names:tc:SAML:2.0:assertion") || StringComparer.Ordinal.Equals(tokenType, "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0"))
				{
					Saml2SecurityTokenHandler saml2SecurityTokenHandler = securityTokenHandler as Saml2SecurityTokenHandler;
					if (saml2SecurityTokenHandler == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4072", new object[]
						{
							securityTokenHandler.GetType(),
							tokenType,
							typeof(Saml2SecurityTokenHandler)
						})));
					}
					if (saml2SecurityTokenHandler.Configuration == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
					}
					result = new WrappedSaml2SecurityTokenAuthenticator(saml2SecurityTokenHandler, this._exceptionMapper);
					outOfBandTokenResolver = saml2SecurityTokenHandler.Configuration.ServiceTokenResolver;
				}
				else if (StringComparer.Ordinal.Equals(tokenType, ServiceModelSecurityTokenTypes.SecureConversation))
				{
					RecipientServiceModelSecurityTokenRequirement recipientServiceModelSecurityTokenRequirement = tokenRequirement as RecipientServiceModelSecurityTokenRequirement;
					if (recipientServiceModelSecurityTokenRequirement == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID4240", new object[]
						{
							tokenRequirement.GetType().ToString()
						}));
					}
					result = this.SetupSecureConversationWrapper(recipientServiceModelSecurityTokenRequirement, securityTokenHandler as SessionSecurityTokenHandler, out outOfBandTokenResolver);
				}
				else
				{
					result = new SecurityTokenAuthenticatorAdapter(securityTokenHandler, this._exceptionMapper);
				}
			}
			else if (tokenType == ServiceModelSecurityTokenTypes.SecureConversation || tokenType == ServiceModelSecurityTokenTypes.MutualSslnego || tokenType == ServiceModelSecurityTokenTypes.AnonymousSslnego || tokenType == ServiceModelSecurityTokenTypes.SecurityContext || tokenType == ServiceModelSecurityTokenTypes.Spnego)
			{
				RecipientServiceModelSecurityTokenRequirement recipientServiceModelSecurityTokenRequirement2 = tokenRequirement as RecipientServiceModelSecurityTokenRequirement;
				if (recipientServiceModelSecurityTokenRequirement2 == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID4240", new object[]
					{
						tokenRequirement.GetType().ToString()
					}));
				}
				result = this.SetupSecureConversationWrapper(recipientServiceModelSecurityTokenRequirement2, null, out outOfBandTokenResolver);
			}
			else
			{
				result = this.CreateInnerSecurityTokenAuthenticator(tokenRequirement, out outOfBandTokenResolver);
			}
			return result;
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x000753B8 File Offset: 0x000735B8
		private SecurityTokenAuthenticator SetupSecureConversationWrapper(RecipientServiceModelSecurityTokenRequirement tokenRequirement, SessionSecurityTokenHandler tokenHandler, out SecurityTokenResolver outOfBandTokenResolver)
		{
			SecurityTokenAuthenticator securityTokenAuthenticator = base.CreateSecurityTokenAuthenticator(tokenRequirement, out outOfBandTokenResolver);
			SessionSecurityTokenHandler sessionSecurityTokenHandler = tokenHandler;
			if (tokenHandler == null)
			{
				sessionSecurityTokenHandler = new SessionSecurityTokenHandler(this._cookieTransforms, SessionSecurityTokenHandler.DefaultTokenLifetime);
				sessionSecurityTokenHandler.ContainingCollection = this._securityTokenHandlerCollection;
				sessionSecurityTokenHandler.Configuration = this._securityTokenHandlerCollection.Configuration;
			}
			if (base.ServiceCredentials != null)
			{
				sessionSecurityTokenHandler.Configuration.MaxClockSkew = base.ServiceCredentials.IdentityConfiguration.MaxClockSkew;
			}
			SctClaimsHandler sctClaimsHandler = new SctClaimsHandler(this._securityTokenHandlerCollection, FederatedSecurityTokenManager.GetNormalizedEndpointId(tokenRequirement));
			WrappedSessionSecurityTokenAuthenticator wrappedSessionSecurityTokenAuthenticator = new WrappedSessionSecurityTokenAuthenticator(sessionSecurityTokenHandler, securityTokenAuthenticator, sctClaimsHandler, this._exceptionMapper);
			WrappedTokenCache wrappedTokenCache = new WrappedTokenCache(this._tokenCache, sctClaimsHandler);
			FederatedSecurityTokenManager.SetWrappedTokenCache(wrappedTokenCache, securityTokenAuthenticator, wrappedSessionSecurityTokenAuthenticator, sctClaimsHandler);
			outOfBandTokenResolver = wrappedTokenCache;
			return wrappedSessionSecurityTokenAuthenticator;
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x00075464 File Offset: 0x00073664
		private static void SetWrappedTokenCache(WrappedTokenCache wrappedTokenCache, SecurityTokenAuthenticator sta, WrappedSessionSecurityTokenAuthenticator wssta, SctClaimsHandler claimsHandler)
		{
			if (sta is SecuritySessionSecurityTokenAuthenticator)
			{
				(sta as SecuritySessionSecurityTokenAuthenticator).IssuedTokenCache = wrappedTokenCache;
			}
			else if (sta is AcceleratedTokenAuthenticator)
			{
				(sta as AcceleratedTokenAuthenticator).IssuedTokenCache = wrappedTokenCache;
			}
			else if (sta is SpnegoTokenAuthenticator)
			{
				(sta as SpnegoTokenAuthenticator).IssuedTokenCache = wrappedTokenCache;
			}
			else if (sta is TlsnegoTokenAuthenticator)
			{
				(sta as TlsnegoTokenAuthenticator).IssuedTokenCache = wrappedTokenCache;
			}
			IIssuanceSecurityTokenAuthenticator issuanceSecurityTokenAuthenticator = sta as IIssuanceSecurityTokenAuthenticator;
			if (issuanceSecurityTokenAuthenticator != null)
			{
				issuanceSecurityTokenAuthenticator.IssuedSecurityTokenHandler = new IssuedSecurityTokenHandler(claimsHandler.OnTokenIssued);
				issuanceSecurityTokenAuthenticator.RenewedSecurityTokenHandler = new RenewedSecurityTokenHandler(claimsHandler.OnTokenRenewed);
			}
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x000754F8 File Offset: 0x000736F8
		public override SecurityTokenSerializer CreateSecurityTokenSerializer(SecurityTokenVersion version)
		{
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("version");
			}
			TrustVersion trustVersion = null;
			SecureConversationVersion secureConversationVersion = null;
			foreach (string x in version.GetSecuritySpecifications())
			{
				if (StringComparer.Ordinal.Equals(x, "http://schemas.xmlsoap.org/ws/2005/02/trust"))
				{
					trustVersion = TrustVersion.WSTrustFeb2005;
				}
				else if (StringComparer.Ordinal.Equals(x, "http://docs.oasis-open.org/ws-sx/ws-trust/200512"))
				{
					trustVersion = TrustVersion.WSTrust13;
				}
				else if (StringComparer.Ordinal.Equals(x, "http://schemas.xmlsoap.org/ws/2005/02/sc"))
				{
					secureConversationVersion = SecureConversationVersion.WSSecureConversationFeb2005;
				}
				else if (StringComparer.Ordinal.Equals(x, "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512"))
				{
					secureConversationVersion = SecureConversationVersion.WSSecureConversation13;
				}
				if (trustVersion != null && secureConversationVersion != null)
				{
					break;
				}
			}
			if (trustVersion == null)
			{
				trustVersion = TrustVersion.WSTrust13;
			}
			if (secureConversationVersion == null)
			{
				secureConversationVersion = SecureConversationVersion.WSSecureConversation13;
			}
			return new WsSecurityTokenSerializerAdapter(this._securityTokenHandlerCollection, FederatedSecurityTokenManager.GetSecurityVersion(version), trustVersion, secureConversationVersion, false, base.ServiceCredentials.IssuedTokenAuthentication.SamlSerializer, base.ServiceCredentials.SecureConversationAuthentication.SecurityStateEncoder, base.ServiceCredentials.SecureConversationAuthentication.SecurityContextClaimTypes)
			{
				MapExceptionsToSoapFaults = true,
				ExceptionMapper = this._exceptionMapper
			};
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x00075638 File Offset: 0x00073838
		private SecurityTokenResolver GetDefaultOutOfBandTokenResolver()
		{
			if (this._defaultTokenResolver == null)
			{
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					if (this._defaultTokenResolver == null)
					{
						List<SecurityToken> list = new List<SecurityToken>();
						if (base.ServiceCredentials.ServiceCertificate.Certificate != null)
						{
							list.Add(new X509SecurityToken(base.ServiceCredentials.ServiceCertificate.Certificate));
						}
						if (base.ServiceCredentials.IssuedTokenAuthentication.KnownCertificates != null && base.ServiceCredentials.IssuedTokenAuthentication.KnownCertificates.Count > 0)
						{
							for (int i = 0; i < base.ServiceCredentials.IssuedTokenAuthentication.KnownCertificates.Count; i++)
							{
								list.Add(new X509SecurityToken(base.ServiceCredentials.IssuedTokenAuthentication.KnownCertificates[i]));
							}
						}
						this._defaultTokenResolver = SecurityTokenResolver.CreateDefaultSecurityTokenResolver(list.AsReadOnly(), false);
					}
				}
			}
			return this._defaultTokenResolver;
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x00075740 File Offset: 0x00073940
		internal static SecurityVersion GetSecurityVersion(SecurityTokenVersion tokenVersion)
		{
			if (tokenVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenVersion");
			}
			if (tokenVersion is MessageSecurityTokenVersion)
			{
				SecurityVersion securityVersion = (tokenVersion as MessageSecurityTokenVersion).SecurityVersion;
				if (securityVersion != null)
				{
					return securityVersion;
				}
			}
			else
			{
				if (tokenVersion.GetSecuritySpecifications().Contains("http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd"))
				{
					return SecurityVersion.WSSecurity11;
				}
				if (tokenVersion.GetSecuritySpecifications().Contains("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"))
				{
					return SecurityVersion.WSSecurity10;
				}
			}
			return SecurityVersion.WSSecurity11;
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x000757B0 File Offset: 0x000739B0
		private SecurityTokenAuthenticator CreateInnerSecurityTokenAuthenticator(SecurityTokenRequirement tokenRequirement, out SecurityTokenResolver outOfBandTokenResolver)
		{
			SecurityTokenAuthenticator securityTokenAuthenticator = base.CreateSecurityTokenAuthenticator(tokenRequirement, out outOfBandTokenResolver);
			SctClaimsHandler sctClaimsHandler = new SctClaimsHandler(this._securityTokenHandlerCollection, FederatedSecurityTokenManager.GetNormalizedEndpointId(tokenRequirement));
			FederatedSecurityTokenManager.SetWrappedTokenCache(new WrappedTokenCache(this._tokenCache, sctClaimsHandler), securityTokenAuthenticator, null, sctClaimsHandler);
			return securityTokenAuthenticator;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x000757F0 File Offset: 0x000739F0
		private SecurityTokenAuthenticator CreateSamlSecurityTokenAuthenticator(SecurityTokenRequirement tokenRequirement, out SecurityTokenResolver outOfBandTokenResolver)
		{
			outOfBandTokenResolver = null;
			SamlSecurityTokenHandler samlSecurityTokenHandler = this._securityTokenHandlerCollection["urn:oasis:names:tc:SAML:1.0:assertion"] as SamlSecurityTokenHandler;
			Saml2SecurityTokenHandler saml2SecurityTokenHandler = this._securityTokenHandlerCollection["urn:oasis:names:tc:SAML:2.0:assertion"] as Saml2SecurityTokenHandler;
			if (samlSecurityTokenHandler != null && samlSecurityTokenHandler.Configuration == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			if (saml2SecurityTokenHandler != null && saml2SecurityTokenHandler.Configuration == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			SecurityTokenAuthenticator result;
			if (samlSecurityTokenHandler != null && saml2SecurityTokenHandler != null)
			{
				WrappedSaml11SecurityTokenAuthenticator wrappedSaml11SecurityTokenAuthenticator = new WrappedSaml11SecurityTokenAuthenticator(samlSecurityTokenHandler, this._exceptionMapper);
				WrappedSaml2SecurityTokenAuthenticator wrappedSaml2SecurityTokenAuthenticator = new WrappedSaml2SecurityTokenAuthenticator(saml2SecurityTokenHandler, this._exceptionMapper);
				result = new WrappedSamlSecurityTokenAuthenticator(wrappedSaml11SecurityTokenAuthenticator, wrappedSaml2SecurityTokenAuthenticator);
				outOfBandTokenResolver = new AggregateTokenResolver(new List<SecurityTokenResolver>
				{
					samlSecurityTokenHandler.Configuration.ServiceTokenResolver,
					saml2SecurityTokenHandler.Configuration.ServiceTokenResolver
				});
			}
			else if (samlSecurityTokenHandler == null && saml2SecurityTokenHandler != null)
			{
				result = new WrappedSaml2SecurityTokenAuthenticator(saml2SecurityTokenHandler, this._exceptionMapper);
				outOfBandTokenResolver = saml2SecurityTokenHandler.Configuration.ServiceTokenResolver;
			}
			else if (samlSecurityTokenHandler != null && saml2SecurityTokenHandler == null)
			{
				result = new WrappedSaml11SecurityTokenAuthenticator(samlSecurityTokenHandler, this._exceptionMapper);
				outOfBandTokenResolver = samlSecurityTokenHandler.Configuration.ServiceTokenResolver;
			}
			else
			{
				result = this.CreateInnerSecurityTokenAuthenticator(tokenRequirement, out outOfBandTokenResolver);
			}
			return result;
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x0007591C File Offset: 0x00073B1C
		public static string GetNormalizedEndpointId(SecurityTokenRequirement tokenRequirement)
		{
			if (tokenRequirement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenRequirement");
			}
			Uri uri = null;
			if (tokenRequirement.Properties.ContainsKey(FederatedSecurityTokenManager.ListenUriProperty))
			{
				uri = (tokenRequirement.Properties[FederatedSecurityTokenManager.ListenUriProperty] as Uri);
			}
			if (uri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID4287", new object[]
				{
					tokenRequirement
				}));
			}
			if (uri.IsDefaultPort)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}://NormalizedHostName{1}", new object[]
				{
					uri.Scheme,
					uri.AbsolutePath
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}://NormalizedHostName:{1}{2}", new object[]
			{
				uri.Scheme,
				uri.Port,
				uri.AbsolutePath
			});
		}

		// Token: 0x04001EE1 RID: 7905
		private static string ListenUriProperty = "http://schemas.microsoft.com/ws/2006/05/servicemodel/securitytokenrequirement/ListenUri";

		// Token: 0x04001EE2 RID: 7906
		private ExceptionMapper _exceptionMapper;

		// Token: 0x04001EE3 RID: 7907
		private SecurityTokenResolver _defaultTokenResolver;

		// Token: 0x04001EE4 RID: 7908
		private SecurityTokenHandlerCollection _securityTokenHandlerCollection;

		// Token: 0x04001EE5 RID: 7909
		private object _syncObject = new object();

		// Token: 0x04001EE6 RID: 7910
		private ReadOnlyCollection<CookieTransform> _cookieTransforms;

		// Token: 0x04001EE7 RID: 7911
		private SessionSecurityTokenCache _tokenCache;
	}
}
