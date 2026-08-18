using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x0200033B RID: 827
	public class ServiceCredentialsSecurityTokenManager : SecurityTokenManager, IEndpointIdentityProvider
	{
		// Token: 0x06001DFE RID: 7678 RVA: 0x0006EE07 File Offset: 0x0006D007
		public ServiceCredentialsSecurityTokenManager(ServiceCredentials parent)
		{
			if (parent == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parent");
			}
			this.parent = parent;
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x0006EE29 File Offset: 0x0006D029
		public ServiceCredentials ServiceCredentials
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x0006EE34 File Offset: 0x0006D034
		public override SecurityTokenSerializer CreateSecurityTokenSerializer(SecurityTokenVersion version)
		{
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("version");
			}
			MessageSecurityTokenVersion messageSecurityTokenVersion = version as MessageSecurityTokenVersion;
			if (messageSecurityTokenVersion != null)
			{
				SamlSerializer samlSerializer;
				if (this.parent.IssuedTokenAuthentication != null)
				{
					samlSerializer = this.parent.IssuedTokenAuthentication.SamlSerializer;
				}
				else
				{
					samlSerializer = new SamlSerializer();
				}
				return new WSSecurityTokenSerializer(messageSecurityTokenVersion.SecurityVersion, messageSecurityTokenVersion.TrustVersion, messageSecurityTokenVersion.SecureConversationVersion, messageSecurityTokenVersion.EmitBspRequiredAttributes, samlSerializer, this.parent.SecureConversationAuthentication.SecurityStateEncoder, this.parent.SecureConversationAuthentication.SecurityContextClaimTypes);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SecurityTokenManagerCannotCreateSerializerForVersion", new object[]
			{
				version
			})));
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x0006EEE8 File Offset: 0x0006D0E8
		protected SecurityTokenAuthenticator CreateSecureConversationTokenAuthenticator(RecipientServiceModelSecurityTokenRequirement recipientRequirement, bool preserveBootstrapTokens, out SecurityTokenResolver sctResolver)
		{
			SecurityBindingElement securityBindingElement = recipientRequirement.SecurityBindingElement;
			if (securityBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("TokenAuthenticatorRequiresSecurityBindingElement", new object[]
				{
					recipientRequirement
				}));
			}
			bool flag = !recipientRequirement.SupportSecurityContextCancellation;
			LocalServiceSecuritySettings localServiceSettings = securityBindingElement.LocalServiceSettings;
			IMessageFilterTable<EndpointAddress> propertyOrDefault = recipientRequirement.GetPropertyOrDefault<IMessageFilterTable<EndpointAddress>>(ServiceModelSecurityTokenRequirement.EndpointFilterTableProperty, null);
			if (!flag)
			{
				sctResolver = new SecurityContextSecurityTokenResolver(int.MaxValue, false);
				return new SecuritySessionSecurityTokenAuthenticator
				{
					BootstrapSecurityBindingElement = SecurityUtils.GetIssuerSecurityBindingElement(recipientRequirement),
					IssuedSecurityTokenParameters = recipientRequirement.GetProperty<SecurityTokenParameters>(ServiceModelSecurityTokenRequirement.IssuedSecurityTokenParametersProperty),
					IssuedTokenCache = (ISecurityContextSecurityTokenCache)sctResolver,
					IssuerBindingContext = recipientRequirement.GetProperty<BindingContext>(ServiceModelSecurityTokenRequirement.IssuerBindingContextProperty),
					KeyEntropyMode = securityBindingElement.KeyEntropyMode,
					ListenUri = recipientRequirement.ListenUri,
					SecurityAlgorithmSuite = recipientRequirement.SecurityAlgorithmSuite,
					SessionTokenLifetime = TimeSpan.MaxValue,
					KeyRenewalInterval = securityBindingElement.LocalServiceSettings.SessionKeyRenewalInterval,
					StandardsManager = SecurityUtils.CreateSecurityStandardsManager(recipientRequirement, this),
					EndpointFilterTable = propertyOrDefault,
					MaximumConcurrentNegotiations = localServiceSettings.MaxStatefulNegotiations,
					NegotiationTimeout = localServiceSettings.NegotiationTimeout,
					PreserveBootstrapTokens = preserveBootstrapTokens
				};
			}
			sctResolver = new SecurityContextSecurityTokenResolver(localServiceSettings.MaxCachedCookies, true, localServiceSettings.MaxClockSkew);
			return new AcceleratedTokenAuthenticator
			{
				BootstrapSecurityBindingElement = SecurityUtils.GetIssuerSecurityBindingElement(recipientRequirement),
				KeyEntropyMode = securityBindingElement.KeyEntropyMode,
				EncryptStateInServiceToken = true,
				IssuedSecurityTokenParameters = recipientRequirement.GetProperty<SecurityTokenParameters>(ServiceModelSecurityTokenRequirement.IssuedSecurityTokenParametersProperty),
				IssuedTokenCache = (ISecurityContextSecurityTokenCache)sctResolver,
				IssuerBindingContext = recipientRequirement.GetProperty<BindingContext>(ServiceModelSecurityTokenRequirement.IssuerBindingContextProperty),
				ListenUri = recipientRequirement.ListenUri,
				SecurityAlgorithmSuite = recipientRequirement.SecurityAlgorithmSuite,
				StandardsManager = SecurityUtils.CreateSecurityStandardsManager(recipientRequirement, this),
				SecurityStateEncoder = this.parent.SecureConversationAuthentication.SecurityStateEncoder,
				KnownTypes = this.parent.SecureConversationAuthentication.SecurityContextClaimTypes,
				PreserveBootstrapTokens = preserveBootstrapTokens,
				MaximumCachedNegotiationState = localServiceSettings.MaxStatefulNegotiations,
				NegotiationTimeout = localServiceSettings.NegotiationTimeout,
				ServiceTokenLifetime = localServiceSettings.IssuedCookieLifetime,
				MaximumConcurrentNegotiations = localServiceSettings.MaxStatefulNegotiations,
				AuditLogLocation = recipientRequirement.AuditLogLocation,
				SuppressAuditFailure = recipientRequirement.SuppressAuditFailure,
				MessageAuthenticationAuditLevel = recipientRequirement.MessageAuthenticationAuditLevel,
				EndpointFilterTable = propertyOrDefault
			};
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x0006F148 File Offset: 0x0006D348
		private SecurityTokenAuthenticator CreateSpnegoSecurityTokenAuthenticator(RecipientServiceModelSecurityTokenRequirement recipientRequirement, out SecurityTokenResolver sctResolver)
		{
			SecurityBindingElement securityBindingElement = recipientRequirement.SecurityBindingElement;
			if (securityBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("TokenAuthenticatorRequiresSecurityBindingElement", new object[]
				{
					recipientRequirement
				}));
			}
			bool encryptStateInServiceToken = !recipientRequirement.SupportSecurityContextCancellation;
			LocalServiceSecuritySettings localServiceSettings = securityBindingElement.LocalServiceSettings;
			sctResolver = new SecurityContextSecurityTokenResolver(localServiceSettings.MaxCachedCookies, true);
			ExtendedProtectionPolicy extendedProtectionPolicy = null;
			recipientRequirement.TryGetProperty<ExtendedProtectionPolicy>(ServiceModelSecurityTokenRequirement.ExtendedProtectionPolicy, out extendedProtectionPolicy);
			SpnegoTokenAuthenticator spnegoTokenAuthenticator = new SpnegoTokenAuthenticator();
			spnegoTokenAuthenticator.ExtendedProtectionPolicy = extendedProtectionPolicy;
			spnegoTokenAuthenticator.AllowUnauthenticatedCallers = this.parent.WindowsAuthentication.AllowAnonymousLogons;
			spnegoTokenAuthenticator.ExtractGroupsForWindowsAccounts = this.parent.WindowsAuthentication.IncludeWindowsGroups;
			spnegoTokenAuthenticator.IsClientAnonymous = false;
			spnegoTokenAuthenticator.EncryptStateInServiceToken = encryptStateInServiceToken;
			spnegoTokenAuthenticator.IssuedSecurityTokenParameters = recipientRequirement.GetProperty<SecurityTokenParameters>(ServiceModelSecurityTokenRequirement.IssuedSecurityTokenParametersProperty);
			spnegoTokenAuthenticator.IssuedTokenCache = (ISecurityContextSecurityTokenCache)sctResolver;
			spnegoTokenAuthenticator.IssuerBindingContext = recipientRequirement.GetProperty<BindingContext>(ServiceModelSecurityTokenRequirement.IssuerBindingContextProperty);
			spnegoTokenAuthenticator.ListenUri = recipientRequirement.ListenUri;
			spnegoTokenAuthenticator.SecurityAlgorithmSuite = recipientRequirement.SecurityAlgorithmSuite;
			spnegoTokenAuthenticator.StandardsManager = SecurityUtils.CreateSecurityStandardsManager(recipientRequirement, this);
			spnegoTokenAuthenticator.SecurityStateEncoder = this.parent.SecureConversationAuthentication.SecurityStateEncoder;
			spnegoTokenAuthenticator.KnownTypes = this.parent.SecureConversationAuthentication.SecurityContextClaimTypes;
			if (securityBindingElement is TransportSecurityBindingElement)
			{
				spnegoTokenAuthenticator.MaxMessageSize = SecurityUtils.GetMaxNegotiationBufferSize(spnegoTokenAuthenticator.IssuerBindingContext);
			}
			spnegoTokenAuthenticator.MaximumCachedNegotiationState = localServiceSettings.MaxStatefulNegotiations;
			spnegoTokenAuthenticator.NegotiationTimeout = localServiceSettings.NegotiationTimeout;
			spnegoTokenAuthenticator.ServiceTokenLifetime = localServiceSettings.IssuedCookieLifetime;
			spnegoTokenAuthenticator.MaximumConcurrentNegotiations = localServiceSettings.MaxStatefulNegotiations;
			spnegoTokenAuthenticator.AuditLogLocation = recipientRequirement.AuditLogLocation;
			spnegoTokenAuthenticator.SuppressAuditFailure = recipientRequirement.SuppressAuditFailure;
			spnegoTokenAuthenticator.MessageAuthenticationAuditLevel = recipientRequirement.MessageAuthenticationAuditLevel;
			return spnegoTokenAuthenticator;
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x0006F2FC File Offset: 0x0006D4FC
		private SecurityTokenAuthenticator CreateTlsnegoClientX509TokenAuthenticator(RecipientServiceModelSecurityTokenRequirement recipientRequirement)
		{
			SecurityTokenResolver securityTokenResolver;
			return this.CreateSecurityTokenAuthenticator(new RecipientServiceModelSecurityTokenRequirement
			{
				TokenType = SecurityTokenTypes.X509Certificate,
				KeyUsage = SecurityKeyUsage.Signature,
				ListenUri = recipientRequirement.ListenUri,
				KeyType = SecurityKeyType.AsymmetricKey,
				SecurityBindingElement = recipientRequirement.SecurityBindingElement
			}, out securityTokenResolver);
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x0006F34C File Offset: 0x0006D54C
		private SecurityTokenProvider CreateTlsnegoServerX509TokenProvider(RecipientServiceModelSecurityTokenRequirement recipientRequirement)
		{
			return this.CreateSecurityTokenProvider(new RecipientServiceModelSecurityTokenRequirement
			{
				TokenType = SecurityTokenTypes.X509Certificate,
				KeyUsage = SecurityKeyUsage.Exchange,
				ListenUri = recipientRequirement.ListenUri,
				KeyType = SecurityKeyType.AsymmetricKey,
				SecurityBindingElement = recipientRequirement.SecurityBindingElement
			});
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x0006F398 File Offset: 0x0006D598
		private SecurityTokenAuthenticator CreateTlsnegoSecurityTokenAuthenticator(RecipientServiceModelSecurityTokenRequirement recipientRequirement, bool requireClientCertificate, out SecurityTokenResolver sctResolver)
		{
			SecurityBindingElement securityBindingElement = recipientRequirement.SecurityBindingElement;
			if (securityBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("TokenAuthenticatorRequiresSecurityBindingElement", new object[]
				{
					recipientRequirement
				}));
			}
			bool encryptStateInServiceToken = !recipientRequirement.SupportSecurityContextCancellation;
			LocalServiceSecuritySettings localServiceSettings = securityBindingElement.LocalServiceSettings;
			sctResolver = new SecurityContextSecurityTokenResolver(localServiceSettings.MaxCachedCookies, true);
			TlsnegoTokenAuthenticator tlsnegoTokenAuthenticator = new TlsnegoTokenAuthenticator();
			tlsnegoTokenAuthenticator.IsClientAnonymous = !requireClientCertificate;
			if (requireClientCertificate)
			{
				tlsnegoTokenAuthenticator.ClientTokenAuthenticator = this.CreateTlsnegoClientX509TokenAuthenticator(recipientRequirement);
				tlsnegoTokenAuthenticator.MapCertificateToWindowsAccount = this.ServiceCredentials.ClientCertificate.Authentication.MapClientCertificateToWindowsAccount;
			}
			tlsnegoTokenAuthenticator.EncryptStateInServiceToken = encryptStateInServiceToken;
			tlsnegoTokenAuthenticator.IssuedSecurityTokenParameters = recipientRequirement.GetProperty<SecurityTokenParameters>(ServiceModelSecurityTokenRequirement.IssuedSecurityTokenParametersProperty);
			tlsnegoTokenAuthenticator.IssuedTokenCache = (ISecurityContextSecurityTokenCache)sctResolver;
			tlsnegoTokenAuthenticator.IssuerBindingContext = recipientRequirement.GetProperty<BindingContext>(ServiceModelSecurityTokenRequirement.IssuerBindingContextProperty);
			tlsnegoTokenAuthenticator.ListenUri = recipientRequirement.ListenUri;
			tlsnegoTokenAuthenticator.SecurityAlgorithmSuite = recipientRequirement.SecurityAlgorithmSuite;
			tlsnegoTokenAuthenticator.StandardsManager = SecurityUtils.CreateSecurityStandardsManager(recipientRequirement, this);
			tlsnegoTokenAuthenticator.SecurityStateEncoder = this.parent.SecureConversationAuthentication.SecurityStateEncoder;
			tlsnegoTokenAuthenticator.KnownTypes = this.parent.SecureConversationAuthentication.SecurityContextClaimTypes;
			tlsnegoTokenAuthenticator.ServerTokenProvider = this.CreateTlsnegoServerX509TokenProvider(recipientRequirement);
			tlsnegoTokenAuthenticator.MaximumCachedNegotiationState = localServiceSettings.MaxStatefulNegotiations;
			tlsnegoTokenAuthenticator.NegotiationTimeout = localServiceSettings.NegotiationTimeout;
			tlsnegoTokenAuthenticator.ServiceTokenLifetime = localServiceSettings.IssuedCookieLifetime;
			tlsnegoTokenAuthenticator.MaximumConcurrentNegotiations = localServiceSettings.MaxStatefulNegotiations;
			if (securityBindingElement is TransportSecurityBindingElement)
			{
				tlsnegoTokenAuthenticator.MaxMessageSize = SecurityUtils.GetMaxNegotiationBufferSize(tlsnegoTokenAuthenticator.IssuerBindingContext);
			}
			tlsnegoTokenAuthenticator.AuditLogLocation = recipientRequirement.AuditLogLocation;
			tlsnegoTokenAuthenticator.SuppressAuditFailure = recipientRequirement.SuppressAuditFailure;
			tlsnegoTokenAuthenticator.MessageAuthenticationAuditLevel = recipientRequirement.MessageAuthenticationAuditLevel;
			return tlsnegoTokenAuthenticator;
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x0006F52C File Offset: 0x0006D72C
		private X509SecurityTokenAuthenticator CreateClientX509TokenAuthenticator()
		{
			X509ClientCertificateAuthentication authentication = this.parent.ClientCertificate.Authentication;
			return new X509SecurityTokenAuthenticator(authentication.GetCertificateValidator(), authentication.MapClientCertificateToWindowsAccount, authentication.IncludeWindowsGroups);
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x0006F564 File Offset: 0x0006D764
		private SamlSecurityTokenAuthenticator CreateSamlTokenAuthenticator(RecipientServiceModelSecurityTokenRequirement recipientRequirement, out SecurityTokenResolver outOfBandTokenResolver)
		{
			if (recipientRequirement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("recipientRequirement");
			}
			Collection<SecurityToken> collection = new Collection<SecurityToken>();
			if (this.parent.ServiceCertificate.Certificate != null)
			{
				collection.Add(new X509SecurityToken(this.parent.ServiceCertificate.Certificate));
			}
			List<SecurityTokenAuthenticator> list = new List<SecurityTokenAuthenticator>();
			if (this.parent.IssuedTokenAuthentication.KnownCertificates != null && this.parent.IssuedTokenAuthentication.KnownCertificates.Count > 0)
			{
				for (int i = 0; i < this.parent.IssuedTokenAuthentication.KnownCertificates.Count; i++)
				{
					collection.Add(new X509SecurityToken(this.parent.IssuedTokenAuthentication.KnownCertificates[i]));
				}
			}
			X509CertificateValidator certificateValidator = this.parent.IssuedTokenAuthentication.GetCertificateValidator();
			list.Add(new X509SecurityTokenAuthenticator(certificateValidator));
			if (this.parent.IssuedTokenAuthentication.AllowUntrustedRsaIssuers)
			{
				list.Add(new RsaSecurityTokenAuthenticator());
			}
			outOfBandTokenResolver = ((collection.Count > 0) ? SecurityTokenResolver.CreateDefaultSecurityTokenResolver(new ReadOnlyCollection<SecurityToken>(collection), false) : null);
			SamlSecurityTokenAuthenticator samlSecurityTokenAuthenticator;
			if (recipientRequirement.SecurityBindingElement == null || recipientRequirement.SecurityBindingElement.LocalServiceSettings == null)
			{
				samlSecurityTokenAuthenticator = new SamlSecurityTokenAuthenticator(list);
			}
			else
			{
				samlSecurityTokenAuthenticator = new SamlSecurityTokenAuthenticator(list, recipientRequirement.SecurityBindingElement.LocalServiceSettings.MaxClockSkew);
			}
			samlSecurityTokenAuthenticator.AudienceUriMode = this.parent.IssuedTokenAuthentication.AudienceUriMode;
			IList<string> allowedAudienceUris = samlSecurityTokenAuthenticator.AllowedAudienceUris;
			if (this.parent.IssuedTokenAuthentication.AllowedAudienceUris != null)
			{
				for (int j = 0; j < this.parent.IssuedTokenAuthentication.AllowedAudienceUris.Count; j++)
				{
					allowedAudienceUris.Add(this.parent.IssuedTokenAuthentication.AllowedAudienceUris[j]);
				}
			}
			if (recipientRequirement.ListenUri != null)
			{
				allowedAudienceUris.Add(recipientRequirement.ListenUri.AbsoluteUri);
			}
			return samlSecurityTokenAuthenticator;
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x0006F74C File Offset: 0x0006D94C
		private X509SecurityTokenProvider CreateServerX509TokenProvider()
		{
			if (this.parent.ServiceCertificate.Certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ServiceCertificateNotProvidedOnServiceCredentials")));
			}
			SecurityUtils.EnsureCertificateCanDoKeyExchange(this.parent.ServiceCertificate.Certificate);
			return new ServiceX509SecurityTokenProvider(this.parent.ServiceCertificate.Certificate);
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x0006F7AF File Offset: 0x0006D9AF
		protected bool IsIssuedSecurityTokenRequirement(SecurityTokenRequirement requirement)
		{
			return requirement != null && requirement.Properties.ContainsKey(ServiceModelSecurityTokenRequirement.IssuerAddressProperty);
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x0006F7C8 File Offset: 0x0006D9C8
		public override SecurityTokenAuthenticator CreateSecurityTokenAuthenticator(SecurityTokenRequirement tokenRequirement, out SecurityTokenResolver outOfBandTokenResolver)
		{
			if (tokenRequirement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenRequirement");
			}
			string tokenType = tokenRequirement.TokenType;
			outOfBandTokenResolver = null;
			SecurityTokenAuthenticator securityTokenAuthenticator = null;
			if (tokenRequirement is InitiatorServiceModelSecurityTokenRequirement && tokenType == SecurityTokenTypes.X509Certificate && tokenRequirement.KeyUsage == SecurityKeyUsage.Exchange)
			{
				return new X509SecurityTokenAuthenticator(X509CertificateValidator.None, false);
			}
			RecipientServiceModelSecurityTokenRequirement recipientServiceModelSecurityTokenRequirement = tokenRequirement as RecipientServiceModelSecurityTokenRequirement;
			if (recipientServiceModelSecurityTokenRequirement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SecurityTokenManagerCannotCreateAuthenticatorForRequirement", new object[]
				{
					tokenRequirement
				})));
			}
			if (tokenType == SecurityTokenTypes.X509Certificate)
			{
				securityTokenAuthenticator = this.CreateClientX509TokenAuthenticator();
			}
			else if (tokenType == SecurityTokenTypes.Kerberos)
			{
				securityTokenAuthenticator = new ServiceCredentialsSecurityTokenManager.KerberosSecurityTokenAuthenticatorWrapper(new KerberosSecurityTokenAuthenticator(this.parent.WindowsAuthentication.IncludeWindowsGroups));
			}
			else if (tokenType == SecurityTokenTypes.UserName)
			{
				if (this.parent.UserNameAuthentication.UserNamePasswordValidationMode == UserNamePasswordValidationMode.Windows)
				{
					if (this.parent.UserNameAuthentication.CacheLogonTokens)
					{
						securityTokenAuthenticator = new WindowsUserNameCachingSecurityTokenAuthenticator(this.parent.UserNameAuthentication.IncludeWindowsGroups, this.parent.UserNameAuthentication.MaxCachedLogonTokens, this.parent.UserNameAuthentication.CachedLogonTokenLifetime);
					}
					else
					{
						securityTokenAuthenticator = new WindowsUserNameSecurityTokenAuthenticator(this.parent.UserNameAuthentication.IncludeWindowsGroups);
					}
				}
				else
				{
					securityTokenAuthenticator = new CustomUserNameSecurityTokenAuthenticator(this.parent.UserNameAuthentication.GetUserNamePasswordValidator());
				}
			}
			else if (tokenType == SecurityTokenTypes.Rsa)
			{
				securityTokenAuthenticator = new RsaSecurityTokenAuthenticator();
			}
			else if (tokenType == ServiceModelSecurityTokenTypes.AnonymousSslnego)
			{
				securityTokenAuthenticator = this.CreateTlsnegoSecurityTokenAuthenticator(recipientServiceModelSecurityTokenRequirement, false, out outOfBandTokenResolver);
			}
			else if (tokenType == ServiceModelSecurityTokenTypes.MutualSslnego)
			{
				securityTokenAuthenticator = this.CreateTlsnegoSecurityTokenAuthenticator(recipientServiceModelSecurityTokenRequirement, true, out outOfBandTokenResolver);
			}
			else if (tokenType == ServiceModelSecurityTokenTypes.Spnego)
			{
				securityTokenAuthenticator = this.CreateSpnegoSecurityTokenAuthenticator(recipientServiceModelSecurityTokenRequirement, out outOfBandTokenResolver);
			}
			else if (tokenType == ServiceModelSecurityTokenTypes.SecureConversation)
			{
				securityTokenAuthenticator = this.CreateSecureConversationTokenAuthenticator(recipientServiceModelSecurityTokenRequirement, false, out outOfBandTokenResolver);
			}
			else if (tokenType == SecurityTokenTypes.Saml || tokenType == "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1" || tokenType == "urn:oasis:names:tc:SAML:1.0:assertion" || (tokenType == null && this.IsIssuedSecurityTokenRequirement(recipientServiceModelSecurityTokenRequirement)))
			{
				securityTokenAuthenticator = this.CreateSamlTokenAuthenticator(recipientServiceModelSecurityTokenRequirement, out outOfBandTokenResolver);
			}
			if (securityTokenAuthenticator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SecurityTokenManagerCannotCreateAuthenticatorForRequirement", new object[]
				{
					tokenRequirement
				})));
			}
			return securityTokenAuthenticator;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x0006FA18 File Offset: 0x0006DC18
		private SecurityTokenProvider CreateLocalSecurityTokenProvider(RecipientServiceModelSecurityTokenRequirement recipientRequirement)
		{
			string tokenType = recipientRequirement.TokenType;
			SecurityTokenProvider result = null;
			if (tokenType == SecurityTokenTypes.X509Certificate)
			{
				result = this.CreateServerX509TokenProvider();
			}
			else if (tokenType == ServiceModelSecurityTokenTypes.SspiCredential)
			{
				AuthenticationSchemes authenticationSchemes;
				bool flag = recipientRequirement.TryGetProperty<AuthenticationSchemes>(ServiceModelSecurityTokenRequirement.HttpAuthenticationSchemeProperty, out authenticationSchemes);
				if (flag && authenticationSchemes.IsSet(AuthenticationSchemes.Basic) && authenticationSchemes.IsNotSet(AuthenticationSchemes.Digest | AuthenticationSchemes.Negotiate | AuthenticationSchemes.Ntlm))
				{
					result = new SspiSecurityTokenProvider(null, this.parent.UserNameAuthentication.IncludeWindowsGroups, false);
				}
				else
				{
					if (flag && authenticationSchemes.IsSet(AuthenticationSchemes.Basic) && this.parent.WindowsAuthentication.IncludeWindowsGroups != this.parent.UserNameAuthentication.IncludeWindowsGroups)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SecurityTokenProviderIncludeWindowsGroupsInconsistent", new object[]
						{
							authenticationSchemes - AuthenticationSchemes.Basic,
							this.parent.UserNameAuthentication.IncludeWindowsGroups,
							this.parent.WindowsAuthentication.IncludeWindowsGroups
						})));
					}
					result = new SspiSecurityTokenProvider(null, this.parent.WindowsAuthentication.IncludeWindowsGroups, this.parent.WindowsAuthentication.AllowAnonymousLogons);
				}
			}
			return result;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x0006FB4C File Offset: 0x0006DD4C
		private SecurityTokenProvider CreateUncorrelatedDuplexSecurityTokenProvider(InitiatorServiceModelSecurityTokenRequirement initiatorRequirement)
		{
			string tokenType = initiatorRequirement.TokenType;
			SecurityTokenProvider result = null;
			if (tokenType == SecurityTokenTypes.X509Certificate)
			{
				if (initiatorRequirement.KeyUsage == SecurityKeyUsage.Exchange)
				{
					if (this.parent.ClientCertificate.Certificate == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ClientCertificateNotProvidedOnServiceCredentials")));
					}
					result = new X509SecurityTokenProvider(this.parent.ClientCertificate.Certificate);
				}
				else
				{
					result = this.CreateServerX509TokenProvider();
				}
			}
			return result;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x0006FBC8 File Offset: 0x0006DDC8
		public override SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement requirement)
		{
			if (requirement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requirement");
			}
			RecipientServiceModelSecurityTokenRequirement recipientServiceModelSecurityTokenRequirement = requirement as RecipientServiceModelSecurityTokenRequirement;
			SecurityTokenProvider securityTokenProvider = null;
			if (recipientServiceModelSecurityTokenRequirement != null)
			{
				securityTokenProvider = this.CreateLocalSecurityTokenProvider(recipientServiceModelSecurityTokenRequirement);
			}
			else if (requirement is InitiatorServiceModelSecurityTokenRequirement)
			{
				securityTokenProvider = this.CreateUncorrelatedDuplexSecurityTokenProvider((InitiatorServiceModelSecurityTokenRequirement)requirement);
			}
			if (securityTokenProvider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SecurityTokenManagerCannotCreateProviderForRequirement", new object[]
				{
					requirement
				})));
			}
			return securityTokenProvider;
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x0006FC3C File Offset: 0x0006DE3C
		public virtual EndpointIdentity GetIdentityOfSelf(SecurityTokenRequirement tokenRequirement)
		{
			if (tokenRequirement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenRequirement");
			}
			if (tokenRequirement is RecipientServiceModelSecurityTokenRequirement)
			{
				string tokenType = tokenRequirement.TokenType;
				if (tokenType == SecurityTokenTypes.X509Certificate || tokenType == ServiceModelSecurityTokenTypes.AnonymousSslnego || tokenType == ServiceModelSecurityTokenTypes.MutualSslnego)
				{
					if (this.parent.ServiceCertificate.Certificate != null)
					{
						return EndpointIdentity.CreateX509CertificateIdentity(this.parent.ServiceCertificate.Certificate);
					}
				}
				else
				{
					if (tokenType == SecurityTokenTypes.Kerberos || tokenType == ServiceModelSecurityTokenTypes.Spnego)
					{
						return SecurityUtils.CreateWindowsIdentity();
					}
					if (tokenType == ServiceModelSecurityTokenTypes.SecureConversation)
					{
						SecurityBindingElement secureConversationSecurityBindingElement = ((RecipientServiceModelSecurityTokenRequirement)tokenRequirement).SecureConversationSecurityBindingElement;
						if (secureConversationSecurityBindingElement != null)
						{
							if (secureConversationSecurityBindingElement == null || secureConversationSecurityBindingElement is TransportSecurityBindingElement)
							{
								return null;
							}
							SecurityTokenParameters securityTokenParameters = (secureConversationSecurityBindingElement is SymmetricSecurityBindingElement) ? ((SymmetricSecurityBindingElement)secureConversationSecurityBindingElement).ProtectionTokenParameters : ((AsymmetricSecurityBindingElement)secureConversationSecurityBindingElement).RecipientTokenParameters;
							SecurityTokenRequirement securityTokenRequirement = new RecipientServiceModelSecurityTokenRequirement();
							securityTokenParameters.InitializeSecurityTokenRequirement(securityTokenRequirement);
							return this.GetIdentityOfSelf(securityTokenRequirement);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04001E58 RID: 7768
		private ServiceCredentials parent;

		// Token: 0x02000B7C RID: 2940
		internal class KerberosSecurityTokenAuthenticatorWrapper : CommunicationObjectSecurityTokenAuthenticator
		{
			// Token: 0x060072CB RID: 29387 RVA: 0x001ACB47 File Offset: 0x001AAD47
			public KerberosSecurityTokenAuthenticatorWrapper(KerberosSecurityTokenAuthenticator innerAuthenticator)
			{
				this.innerAuthenticator = innerAuthenticator;
			}

			// Token: 0x060072CC RID: 29388 RVA: 0x001ACB56 File Offset: 0x001AAD56
			public override void OnOpening()
			{
				base.OnOpening();
				if (this.credentialsHandle == null)
				{
					this.credentialsHandle = SecurityUtils.GetCredentialsHandle("Kerberos", null, true, new string[0]);
				}
			}

			// Token: 0x060072CD RID: 29389 RVA: 0x001ACB7E File Offset: 0x001AAD7E
			public override void OnClose(TimeSpan timeout)
			{
				base.OnClose(timeout);
				this.FreeCredentialsHandle();
			}

			// Token: 0x060072CE RID: 29390 RVA: 0x001ACB8D File Offset: 0x001AAD8D
			public override void OnAbort()
			{
				base.OnAbort();
				this.FreeCredentialsHandle();
			}

			// Token: 0x060072CF RID: 29391 RVA: 0x001ACB9B File Offset: 0x001AAD9B
			private void FreeCredentialsHandle()
			{
				if (this.credentialsHandle != null)
				{
					this.credentialsHandle.Close();
					this.credentialsHandle = null;
				}
			}

			// Token: 0x060072D0 RID: 29392 RVA: 0x001ACBB7 File Offset: 0x001AADB7
			protected override bool CanValidateTokenCore(SecurityToken token)
			{
				return this.innerAuthenticator.CanValidateToken(token);
			}

			// Token: 0x060072D1 RID: 29393 RVA: 0x001ACBC8 File Offset: 0x001AADC8
			internal ReadOnlyCollection<IAuthorizationPolicy> ValidateToken(SecurityToken token, ChannelBinding channelBinding, ExtendedProtectionPolicy protectionPolicy)
			{
				KerberosReceiverSecurityToken kerberosReceiverSecurityToken = (KerberosReceiverSecurityToken)token;
				kerberosReceiverSecurityToken.Initialize(this.credentialsHandle, channelBinding, protectionPolicy);
				return this.innerAuthenticator.ValidateToken(kerberosReceiverSecurityToken);
			}

			// Token: 0x060072D2 RID: 29394 RVA: 0x001ACBF6 File Offset: 0x001AADF6
			protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
			{
				return this.ValidateToken(token, null, null);
			}

			// Token: 0x040040FB RID: 16635
			private KerberosSecurityTokenAuthenticator innerAuthenticator;

			// Token: 0x040040FC RID: 16636
			private SafeFreeCredentials credentialsHandle;
		}
	}
}
