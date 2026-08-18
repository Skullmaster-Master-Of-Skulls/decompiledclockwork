using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x020000C1 RID: 193
	public class ClientCredentialsSecurityTokenManager : SecurityTokenManager
	{
		// Token: 0x0600035E RID: 862 RVA: 0x00013352 File Offset: 0x00011552
		public ClientCredentialsSecurityTokenManager(ClientCredentials clientCredentials)
		{
			if (clientCredentials == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("clientCredentials");
			}
			this.parent = clientCredentials;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00013374 File Offset: 0x00011574
		public ClientCredentials ClientCredentials
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001337C File Offset: 0x0001157C
		private string GetServicePrincipalName(InitiatorServiceModelSecurityTokenRequirement initiatorRequirement)
		{
			EndpointAddress targetAddress = initiatorRequirement.TargetAddress;
			if (targetAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("TokenRequirementDoesNotSpecifyTargetAddress", new object[]
				{
					initiatorRequirement
				}));
			}
			SecurityBindingElement securityBindingElement = initiatorRequirement.SecurityBindingElement;
			IdentityVerifier identityVerifier;
			if (securityBindingElement != null)
			{
				identityVerifier = securityBindingElement.LocalClientSettings.IdentityVerifier;
			}
			else
			{
				identityVerifier = IdentityVerifier.CreateDefault();
			}
			EndpointIdentity identity;
			identityVerifier.TryGetIdentity(targetAddress, out identity);
			return SecurityUtils.GetSpnFromIdentity(identity, targetAddress);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000133E8 File Offset: 0x000115E8
		private SspiSecurityToken GetSpnegoClientCredential(InitiatorServiceModelSecurityTokenRequirement initiatorRequirement)
		{
			InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = new InitiatorServiceModelSecurityTokenRequirement();
			initiatorServiceModelSecurityTokenRequirement.TargetAddress = initiatorRequirement.TargetAddress;
			initiatorServiceModelSecurityTokenRequirement.TokenType = ServiceModelSecurityTokenTypes.SspiCredential;
			initiatorServiceModelSecurityTokenRequirement.Via = initiatorRequirement.Via;
			initiatorServiceModelSecurityTokenRequirement.RequireCryptographicToken = false;
			initiatorServiceModelSecurityTokenRequirement.SecurityBindingElement = initiatorRequirement.SecurityBindingElement;
			initiatorServiceModelSecurityTokenRequirement.MessageSecurityVersion = initiatorRequirement.MessageSecurityVersion;
			ChannelParameterCollection value;
			if (initiatorRequirement.TryGetProperty<ChannelParameterCollection>(ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty, out value))
			{
				initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty] = value;
			}
			SecurityTokenProvider securityTokenProvider = this.CreateSecurityTokenProvider(initiatorServiceModelSecurityTokenRequirement);
			SecurityUtils.OpenTokenProviderIfRequired(securityTokenProvider, TimeSpan.Zero);
			SspiSecurityToken result = (SspiSecurityToken)securityTokenProvider.GetToken(TimeSpan.Zero);
			SecurityUtils.AbortTokenProviderIfRequired(securityTokenProvider);
			return result;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00013488 File Offset: 0x00011688
		private SecurityTokenProvider CreateSpnegoTokenProvider(InitiatorServiceModelSecurityTokenRequirement initiatorRequirement)
		{
			EndpointAddress targetAddress = initiatorRequirement.TargetAddress;
			if (targetAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("TokenRequirementDoesNotSpecifyTargetAddress", new object[]
				{
					initiatorRequirement
				}));
			}
			SecurityBindingElement securityBindingElement = initiatorRequirement.SecurityBindingElement;
			if (securityBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("TokenProviderRequiresSecurityBindingElement", new object[]
				{
					initiatorRequirement
				}));
			}
			SspiIssuanceChannelParameter sspiIssuanceChannelParameter = this.GetSspiIssuanceChannelParameter(initiatorRequirement);
			bool flag = sspiIssuanceChannelParameter == null || sspiIssuanceChannelParameter.GetTokenOnOpen;
			LocalClientSecuritySettings localClientSettings = securityBindingElement.LocalClientSettings;
			BindingContext property = initiatorRequirement.GetProperty<BindingContext>(ServiceModelSecurityTokenRequirement.IssuerBindingContextProperty);
			SpnegoTokenProvider spnegoTokenProvider = new SpnegoTokenProvider((sspiIssuanceChannelParameter != null) ? sspiIssuanceChannelParameter.CredentialsHandle : null, securityBindingElement);
			SspiSecurityToken spnegoClientCredential = this.GetSpnegoClientCredential(initiatorRequirement);
			spnegoTokenProvider.ClientCredential = spnegoClientCredential.NetworkCredential;
			spnegoTokenProvider.IssuerAddress = initiatorRequirement.IssuerAddress;
			spnegoTokenProvider.AllowedImpersonationLevel = this.parent.Windows.AllowedImpersonationLevel;
			spnegoTokenProvider.AllowNtlm = spnegoClientCredential.AllowNtlm;
			spnegoTokenProvider.IdentityVerifier = localClientSettings.IdentityVerifier;
			spnegoTokenProvider.SecurityAlgorithmSuite = initiatorRequirement.SecurityAlgorithmSuite;
			spnegoTokenProvider.AuthenticateServer = !initiatorRequirement.Properties.ContainsKey(ServiceModelSecurityTokenRequirement.SupportingTokenAttachmentModeProperty);
			spnegoTokenProvider.NegotiateTokenOnOpen = flag;
			spnegoTokenProvider.CacheServiceTokens = (flag || localClientSettings.CacheCookies);
			spnegoTokenProvider.IssuerBindingContext = property;
			spnegoTokenProvider.MaxServiceTokenCachingTime = localClientSettings.MaxCookieCachingTime;
			spnegoTokenProvider.ServiceTokenValidityThresholdPercentage = localClientSettings.CookieRenewalThresholdPercentage;
			spnegoTokenProvider.StandardsManager = SecurityUtils.CreateSecurityStandardsManager(initiatorRequirement, this);
			spnegoTokenProvider.TargetAddress = targetAddress;
			spnegoTokenProvider.Via = initiatorRequirement.GetPropertyOrDefault<Uri>(ServiceModelSecurityTokenRequirement.ViaProperty, null);
			spnegoTokenProvider.ApplicationProtectionRequirements = ((property != null) ? property.BindingParameters.Find<ChannelProtectionRequirements>() : null);
			spnegoTokenProvider.InteractiveNegoExLogonEnabled = this.ClientCredentials.SupportInteractive;
			return spnegoTokenProvider;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00013644 File Offset: 0x00011844
		private SecurityTokenProvider CreateTlsnegoClientX509TokenProvider(InitiatorServiceModelSecurityTokenRequirement initiatorRequirement)
		{
			InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = new InitiatorServiceModelSecurityTokenRequirement();
			initiatorServiceModelSecurityTokenRequirement.TokenType = SecurityTokenTypes.X509Certificate;
			initiatorServiceModelSecurityTokenRequirement.TargetAddress = initiatorRequirement.TargetAddress;
			initiatorServiceModelSecurityTokenRequirement.SecurityBindingElement = initiatorRequirement.SecurityBindingElement;
			initiatorServiceModelSecurityTokenRequirement.SecurityAlgorithmSuite = initiatorRequirement.SecurityAlgorithmSuite;
			initiatorServiceModelSecurityTokenRequirement.RequireCryptographicToken = true;
			initiatorServiceModelSecurityTokenRequirement.MessageSecurityVersion = initiatorRequirement.MessageSecurityVersion;
			initiatorServiceModelSecurityTokenRequirement.KeyUsage = SecurityKeyUsage.Signature;
			initiatorServiceModelSecurityTokenRequirement.KeyType = SecurityKeyType.AsymmetricKey;
			initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.MessageDirectionProperty] = MessageDirection.Output;
			ChannelParameterCollection value;
			if (initiatorRequirement.TryGetProperty<ChannelParameterCollection>(ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty, out value))
			{
				initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty] = value;
			}
			return this.CreateSecurityTokenProvider(initiatorServiceModelSecurityTokenRequirement);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000136E4 File Offset: 0x000118E4
		private SecurityTokenAuthenticator CreateTlsnegoServerX509TokenAuthenticator(InitiatorServiceModelSecurityTokenRequirement initiatorRequirement)
		{
			InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = new InitiatorServiceModelSecurityTokenRequirement();
			initiatorServiceModelSecurityTokenRequirement.TokenType = SecurityTokenTypes.X509Certificate;
			initiatorServiceModelSecurityTokenRequirement.RequireCryptographicToken = true;
			initiatorServiceModelSecurityTokenRequirement.SecurityBindingElement = initiatorRequirement.SecurityBindingElement;
			initiatorServiceModelSecurityTokenRequirement.MessageSecurityVersion = initiatorRequirement.MessageSecurityVersion;
			initiatorServiceModelSecurityTokenRequirement.KeyUsage = SecurityKeyUsage.Exchange;
			initiatorServiceModelSecurityTokenRequirement.KeyType = SecurityKeyType.AsymmetricKey;
			initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.MessageDirectionProperty] = MessageDirection.Input;
			ChannelParameterCollection value;
			if (initiatorRequirement.TryGetProperty<ChannelParameterCollection>(ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty, out value))
			{
				initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty] = value;
			}
			SecurityTokenResolver securityTokenResolver;
			return this.CreateSecurityTokenAuthenticator(initiatorServiceModelSecurityTokenRequirement, out securityTokenResolver);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00013770 File Offset: 0x00011970
		private SspiIssuanceChannelParameter GetSspiIssuanceChannelParameter(SecurityTokenRequirement initiatorRequirement)
		{
			ChannelParameterCollection channelParameterCollection;
			if (initiatorRequirement.TryGetProperty<ChannelParameterCollection>(ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty, out channelParameterCollection) && channelParameterCollection != null)
			{
				for (int i = 0; i < channelParameterCollection.Count; i++)
				{
					if (channelParameterCollection[i] is SspiIssuanceChannelParameter)
					{
						return (SspiIssuanceChannelParameter)channelParameterCollection[i];
					}
				}
			}
			return null;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000137BC File Offset: 0x000119BC
		private SecurityTokenProvider CreateTlsnegoTokenProvider(InitiatorServiceModelSecurityTokenRequirement initiatorRequirement, bool requireClientCertificate)
		{
			EndpointAddress targetAddress = initiatorRequirement.TargetAddress;
			if (targetAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("TokenRequirementDoesNotSpecifyTargetAddress", new object[]
				{
					initiatorRequirement
				}));
			}
			SecurityBindingElement securityBindingElement = initiatorRequirement.SecurityBindingElement;
			if (securityBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("TokenProviderRequiresSecurityBindingElement", new object[]
				{
					initiatorRequirement
				}));
			}
			SspiIssuanceChannelParameter sspiIssuanceChannelParameter = this.GetSspiIssuanceChannelParameter(initiatorRequirement);
			bool flag = sspiIssuanceChannelParameter != null && sspiIssuanceChannelParameter.GetTokenOnOpen;
			LocalClientSecuritySettings localClientSettings = securityBindingElement.LocalClientSettings;
			BindingContext property = initiatorRequirement.GetProperty<BindingContext>(ServiceModelSecurityTokenRequirement.IssuerBindingContextProperty);
			TlsnegoTokenProvider tlsnegoTokenProvider = new TlsnegoTokenProvider();
			tlsnegoTokenProvider.IssuerAddress = initiatorRequirement.IssuerAddress;
			tlsnegoTokenProvider.NegotiateTokenOnOpen = flag;
			tlsnegoTokenProvider.CacheServiceTokens = (flag || localClientSettings.CacheCookies);
			if (requireClientCertificate)
			{
				tlsnegoTokenProvider.ClientTokenProvider = this.CreateTlsnegoClientX509TokenProvider(initiatorRequirement);
			}
			tlsnegoTokenProvider.IssuerBindingContext = property;
			tlsnegoTokenProvider.ApplicationProtectionRequirements = ((property != null) ? property.BindingParameters.Find<ChannelProtectionRequirements>() : null);
			tlsnegoTokenProvider.MaxServiceTokenCachingTime = localClientSettings.MaxCookieCachingTime;
			tlsnegoTokenProvider.SecurityAlgorithmSuite = initiatorRequirement.SecurityAlgorithmSuite;
			tlsnegoTokenProvider.ServerTokenAuthenticator = this.CreateTlsnegoServerX509TokenAuthenticator(initiatorRequirement);
			tlsnegoTokenProvider.ServiceTokenValidityThresholdPercentage = localClientSettings.CookieRenewalThresholdPercentage;
			tlsnegoTokenProvider.StandardsManager = SecurityUtils.CreateSecurityStandardsManager(initiatorRequirement, this);
			tlsnegoTokenProvider.TargetAddress = initiatorRequirement.TargetAddress;
			tlsnegoTokenProvider.Via = initiatorRequirement.GetPropertyOrDefault<Uri>(ServiceModelSecurityTokenRequirement.ViaProperty, null);
			return tlsnegoTokenProvider;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00013918 File Offset: 0x00011B18
		private SecurityTokenProvider CreateSecureConversationSecurityTokenProvider(InitiatorServiceModelSecurityTokenRequirement initiatorRequirement)
		{
			EndpointAddress targetAddress = initiatorRequirement.TargetAddress;
			if (targetAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("TokenRequirementDoesNotSpecifyTargetAddress", new object[]
				{
					initiatorRequirement
				}));
			}
			SecurityBindingElement securityBindingElement = initiatorRequirement.SecurityBindingElement;
			if (securityBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("TokenProviderRequiresSecurityBindingElement", new object[]
				{
					initiatorRequirement
				}));
			}
			LocalClientSecuritySettings localClientSettings = securityBindingElement.LocalClientSettings;
			BindingContext property = initiatorRequirement.GetProperty<BindingContext>(ServiceModelSecurityTokenRequirement.IssuerBindingContextProperty);
			ChannelParameterCollection propertyOrDefault = initiatorRequirement.GetPropertyOrDefault<ChannelParameterCollection>(ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty, null);
			bool supportSecurityContextCancellation = initiatorRequirement.SupportSecurityContextCancellation;
			if (supportSecurityContextCancellation)
			{
				SecuritySessionSecurityTokenProvider securitySessionSecurityTokenProvider = new SecuritySessionSecurityTokenProvider(this.GetCredentialsHandle(initiatorRequirement));
				securitySessionSecurityTokenProvider.BootstrapSecurityBindingElement = SecurityUtils.GetIssuerSecurityBindingElement(initiatorRequirement);
				securitySessionSecurityTokenProvider.IssuedSecurityTokenParameters = initiatorRequirement.GetProperty<SecurityTokenParameters>(ServiceModelSecurityTokenRequirement.IssuedSecurityTokenParametersProperty);
				securitySessionSecurityTokenProvider.IssuerBindingContext = property;
				securitySessionSecurityTokenProvider.KeyEntropyMode = securityBindingElement.KeyEntropyMode;
				securitySessionSecurityTokenProvider.SecurityAlgorithmSuite = initiatorRequirement.SecurityAlgorithmSuite;
				securitySessionSecurityTokenProvider.StandardsManager = SecurityUtils.CreateSecurityStandardsManager(initiatorRequirement, this);
				securitySessionSecurityTokenProvider.TargetAddress = targetAddress;
				securitySessionSecurityTokenProvider.Via = initiatorRequirement.GetPropertyOrDefault<Uri>(ServiceModelSecurityTokenRequirement.ViaProperty, null);
				Uri privacyNoticeUri;
				if (initiatorRequirement.TryGetProperty<Uri>(ServiceModelSecurityTokenRequirement.PrivacyNoticeUriProperty, out privacyNoticeUri))
				{
					securitySessionSecurityTokenProvider.PrivacyNoticeUri = privacyNoticeUri;
				}
				int privacyNoticeVersion;
				if (initiatorRequirement.TryGetProperty<int>(ServiceModelSecurityTokenRequirement.PrivacyNoticeVersionProperty, out privacyNoticeVersion))
				{
					securitySessionSecurityTokenProvider.PrivacyNoticeVersion = privacyNoticeVersion;
				}
				EndpointAddress localAddress;
				if (initiatorRequirement.TryGetProperty<EndpointAddress>(ServiceModelSecurityTokenRequirement.DuplexClientLocalAddressProperty, out localAddress))
				{
					securitySessionSecurityTokenProvider.LocalAddress = localAddress;
				}
				securitySessionSecurityTokenProvider.ChannelParameters = propertyOrDefault;
				securitySessionSecurityTokenProvider.WebHeaders = initiatorRequirement.WebHeaders;
				return securitySessionSecurityTokenProvider;
			}
			AcceleratedTokenProvider acceleratedTokenProvider = new AcceleratedTokenProvider(this.GetCredentialsHandle(initiatorRequirement));
			acceleratedTokenProvider.IssuerAddress = initiatorRequirement.IssuerAddress;
			acceleratedTokenProvider.BootstrapSecurityBindingElement = SecurityUtils.GetIssuerSecurityBindingElement(initiatorRequirement);
			acceleratedTokenProvider.CacheServiceTokens = localClientSettings.CacheCookies;
			acceleratedTokenProvider.IssuerBindingContext = property;
			acceleratedTokenProvider.KeyEntropyMode = securityBindingElement.KeyEntropyMode;
			acceleratedTokenProvider.MaxServiceTokenCachingTime = localClientSettings.MaxCookieCachingTime;
			acceleratedTokenProvider.SecurityAlgorithmSuite = initiatorRequirement.SecurityAlgorithmSuite;
			acceleratedTokenProvider.ServiceTokenValidityThresholdPercentage = localClientSettings.CookieRenewalThresholdPercentage;
			acceleratedTokenProvider.StandardsManager = SecurityUtils.CreateSecurityStandardsManager(initiatorRequirement, this);
			acceleratedTokenProvider.TargetAddress = targetAddress;
			acceleratedTokenProvider.Via = initiatorRequirement.GetPropertyOrDefault<Uri>(ServiceModelSecurityTokenRequirement.ViaProperty, null);
			Uri privacyNoticeUri2;
			if (initiatorRequirement.TryGetProperty<Uri>(ServiceModelSecurityTokenRequirement.PrivacyNoticeUriProperty, out privacyNoticeUri2))
			{
				acceleratedTokenProvider.PrivacyNoticeUri = privacyNoticeUri2;
			}
			acceleratedTokenProvider.ChannelParameters = propertyOrDefault;
			int privacyNoticeVersion2;
			if (initiatorRequirement.TryGetProperty<int>(ServiceModelSecurityTokenRequirement.PrivacyNoticeVersionProperty, out privacyNoticeVersion2))
			{
				acceleratedTokenProvider.PrivacyNoticeVersion = privacyNoticeVersion2;
			}
			return acceleratedTokenProvider;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00013B5C File Offset: 0x00011D5C
		private SecurityTokenProvider CreateServerX509TokenProvider(EndpointAddress targetAddress)
		{
			X509Certificate2 x509Certificate = null;
			if (targetAddress != null)
			{
				this.parent.ServiceCertificate.ScopedCertificates.TryGetValue(targetAddress.Uri, out x509Certificate);
			}
			if (x509Certificate == null)
			{
				x509Certificate = this.parent.ServiceCertificate.DefaultCertificate;
			}
			if (x509Certificate == null && targetAddress.Identity != null && targetAddress.Identity.GetType() == typeof(X509CertificateEndpointIdentity))
			{
				x509Certificate = ((X509CertificateEndpointIdentity)targetAddress.Identity).Certificates[0];
			}
			if (x509Certificate != null)
			{
				return new X509SecurityTokenProvider(x509Certificate);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ServiceCertificateNotProvidedOnClientCredentials", new object[]
			{
				targetAddress.Uri
			})));
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00013C16 File Offset: 0x00011E16
		private X509SecurityTokenAuthenticator CreateServerX509TokenAuthenticator()
		{
			return new X509SecurityTokenAuthenticator(this.parent.ServiceCertificate.Authentication.GetCertificateValidator(), false);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00013C33 File Offset: 0x00011E33
		private X509SecurityTokenAuthenticator CreateServerSslX509TokenAuthenticator()
		{
			if (this.parent.ServiceCertificate.SslCertificateAuthentication != null)
			{
				return new X509SecurityTokenAuthenticator(this.parent.ServiceCertificate.SslCertificateAuthentication.GetCertificateValidator(), false);
			}
			return this.CreateServerX509TokenAuthenticator();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00013C6C File Offset: 0x00011E6C
		private bool IsDigestAuthenticationScheme(SecurityTokenRequirement requirement)
		{
			if (!requirement.Properties.ContainsKey(ServiceModelSecurityTokenRequirement.HttpAuthenticationSchemeProperty))
			{
				return false;
			}
			AuthenticationSchemes authenticationSchemes = (AuthenticationSchemes)requirement.Properties[ServiceModelSecurityTokenRequirement.HttpAuthenticationSchemeProperty];
			if (!authenticationSchemes.IsSingleton())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("HttpRequiresSingleAuthScheme", new object[]
				{
					authenticationSchemes
				}));
			}
			return authenticationSchemes == AuthenticationSchemes.Digest;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00013CD8 File Offset: 0x00011ED8
		protected internal bool IsIssuedSecurityTokenRequirement(SecurityTokenRequirement requirement)
		{
			return requirement != null && requirement.Properties.ContainsKey(ServiceModelSecurityTokenRequirement.IssuerAddressProperty) && !(requirement.TokenType == ServiceModelSecurityTokenTypes.AnonymousSslnego) && !(requirement.TokenType == ServiceModelSecurityTokenTypes.MutualSslnego) && !(requirement.TokenType == ServiceModelSecurityTokenTypes.SecureConversation) && !(requirement.TokenType == ServiceModelSecurityTokenTypes.Spnego);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00013D48 File Offset: 0x00011F48
		private void CopyIssuerChannelBehaviorsAndAddSecurityCredentials(IssuedSecurityTokenProvider federationTokenProvider, KeyedByTypeCollection<IEndpointBehavior> issuerChannelBehaviors, EndpointAddress issuerAddress)
		{
			if (issuerChannelBehaviors != null)
			{
				foreach (IEndpointBehavior endpointBehavior in issuerChannelBehaviors)
				{
					if (endpointBehavior is SecurityCredentialsManager)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("IssuerChannelBehaviorsCannotContainSecurityCredentialsManager", new object[]
						{
							issuerAddress,
							typeof(SecurityCredentialsManager)
						})));
					}
					federationTokenProvider.IssuerChannelBehaviors.Add(endpointBehavior);
				}
			}
			federationTokenProvider.IssuerChannelBehaviors.Add(this.parent);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00013DE4 File Offset: 0x00011FE4
		private SecurityKeyEntropyMode GetIssuerBindingKeyEntropyModeOrDefault(Binding issuerBinding)
		{
			BindingElementCollection bindingElementCollection = issuerBinding.CreateBindingElements();
			SecurityBindingElement securityBindingElement = bindingElementCollection.Find<SecurityBindingElement>();
			if (securityBindingElement != null)
			{
				return securityBindingElement.KeyEntropyMode;
			}
			return this.parent.IssuedToken.DefaultKeyEntropyMode;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00013E1C File Offset: 0x0001201C
		private void GetIssuerBindingSecurityVersion(Binding issuerBinding, MessageSecurityVersion issuedTokenParametersDefaultMessageSecurityVersion, SecurityBindingElement outerSecurityBindingElement, out MessageSecurityVersion messageSecurityVersion, out SecurityTokenSerializer tokenSerializer)
		{
			messageSecurityVersion = null;
			if (issuerBinding != null)
			{
				BindingElementCollection bindingElementCollection = issuerBinding.CreateBindingElements();
				SecurityBindingElement securityBindingElement = bindingElementCollection.Find<SecurityBindingElement>();
				if (securityBindingElement != null)
				{
					messageSecurityVersion = securityBindingElement.MessageSecurityVersion;
				}
			}
			if (messageSecurityVersion == null)
			{
				if (issuedTokenParametersDefaultMessageSecurityVersion != null)
				{
					messageSecurityVersion = issuedTokenParametersDefaultMessageSecurityVersion;
				}
				else if (outerSecurityBindingElement != null)
				{
					messageSecurityVersion = outerSecurityBindingElement.MessageSecurityVersion;
				}
			}
			if (messageSecurityVersion == null)
			{
				messageSecurityVersion = MessageSecurityVersion.Default;
			}
			tokenSerializer = this.CreateSecurityTokenSerializer(messageSecurityVersion.SecurityTokenVersion);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00013E84 File Offset: 0x00012084
		private IssuedSecurityTokenProvider CreateIssuedSecurityTokenProvider(InitiatorServiceModelSecurityTokenRequirement initiatorRequirement, FederatedClientCredentialsParameters actAsOnBehalfOfParameters)
		{
			if (initiatorRequirement.TargetAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("TokenRequirementDoesNotSpecifyTargetAddress", new object[]
				{
					initiatorRequirement
				}));
			}
			SecurityBindingElement securityBindingElement = initiatorRequirement.SecurityBindingElement;
			if (securityBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("TokenProviderRequiresSecurityBindingElement", new object[]
				{
					initiatorRequirement
				}));
			}
			EndpointAddress endpointAddress = initiatorRequirement.IssuerAddress;
			Binding binding = initiatorRequirement.IssuerBinding;
			bool flag = endpointAddress == null || endpointAddress.Equals(EndpointAddress.AnonymousAddress);
			if (flag)
			{
				endpointAddress = this.parent.IssuedToken.LocalIssuerAddress;
				binding = this.parent.IssuedToken.LocalIssuerBinding;
			}
			if (endpointAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("StsAddressNotSet", new object[]
				{
					initiatorRequirement.TargetAddress
				})));
			}
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("StsBindingNotSet", new object[]
				{
					endpointAddress
				})));
			}
			Uri uri = endpointAddress.Uri;
			KeyedByTypeCollection<IEndpointBehavior> localIssuerChannelBehaviors;
			if (!this.parent.IssuedToken.IssuerChannelBehaviors.TryGetValue(endpointAddress.Uri, out localIssuerChannelBehaviors) && flag)
			{
				localIssuerChannelBehaviors = this.parent.IssuedToken.LocalIssuerChannelBehaviors;
			}
			IssuedSecurityTokenProvider issuedSecurityTokenProvider = new IssuedSecurityTokenProvider(this.GetCredentialsHandle(initiatorRequirement));
			issuedSecurityTokenProvider.TokenHandlerCollectionManager = this.parent.SecurityTokenHandlerCollectionManager;
			issuedSecurityTokenProvider.TargetAddress = initiatorRequirement.TargetAddress;
			this.CopyIssuerChannelBehaviorsAndAddSecurityCredentials(issuedSecurityTokenProvider, localIssuerChannelBehaviors, endpointAddress);
			issuedSecurityTokenProvider.CacheIssuedTokens = this.parent.IssuedToken.CacheIssuedTokens;
			issuedSecurityTokenProvider.IdentityVerifier = securityBindingElement.LocalClientSettings.IdentityVerifier;
			issuedSecurityTokenProvider.IssuerAddress = endpointAddress;
			issuedSecurityTokenProvider.IssuerBinding = binding;
			issuedSecurityTokenProvider.KeyEntropyMode = this.GetIssuerBindingKeyEntropyModeOrDefault(binding);
			issuedSecurityTokenProvider.MaxIssuedTokenCachingTime = this.parent.IssuedToken.MaxIssuedTokenCachingTime;
			issuedSecurityTokenProvider.SecurityAlgorithmSuite = initiatorRequirement.SecurityAlgorithmSuite;
			IssuedSecurityTokenParameters property = initiatorRequirement.GetProperty<IssuedSecurityTokenParameters>(ServiceModelSecurityTokenRequirement.IssuedSecurityTokenParametersProperty);
			MessageSecurityVersion messageSecurityVersion;
			SecurityTokenSerializer securityTokenSerializer;
			this.GetIssuerBindingSecurityVersion(binding, property.DefaultMessageSecurityVersion, initiatorRequirement.SecurityBindingElement, out messageSecurityVersion, out securityTokenSerializer);
			issuedSecurityTokenProvider.MessageSecurityVersion = messageSecurityVersion;
			issuedSecurityTokenProvider.SecurityTokenSerializer = securityTokenSerializer;
			issuedSecurityTokenProvider.IssuedTokenRenewalThresholdPercentage = this.parent.IssuedToken.IssuedTokenRenewalThresholdPercentage;
			IEnumerable<XmlElement> enumerable = property.CreateRequestParameters(messageSecurityVersion, securityTokenSerializer);
			if (enumerable != null)
			{
				foreach (XmlElement item in enumerable)
				{
					issuedSecurityTokenProvider.TokenRequestParameters.Add(item);
				}
			}
			ChannelParameterCollection channelParameters;
			if (initiatorRequirement.TryGetProperty<ChannelParameterCollection>(ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty, out channelParameters))
			{
				issuedSecurityTokenProvider.ChannelParameters = channelParameters;
			}
			issuedSecurityTokenProvider.SetupActAsOnBehalfOfParameters(actAsOnBehalfOfParameters);
			return issuedSecurityTokenProvider;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00014138 File Offset: 0x00012338
		public override SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement tokenRequirement)
		{
			return this.CreateSecurityTokenProvider(tokenRequirement, false);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00014144 File Offset: 0x00012344
		internal SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement tokenRequirement, bool disableInfoCard)
		{
			if (tokenRequirement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenRequirement");
			}
			SecurityTokenProvider securityTokenProvider = null;
			if (disableInfoCard || !this.CardSpaceTryCreateSecurityTokenProviderStub(tokenRequirement, this, out securityTokenProvider))
			{
				if (tokenRequirement is RecipientServiceModelSecurityTokenRequirement && tokenRequirement.TokenType == SecurityTokenTypes.X509Certificate && tokenRequirement.KeyUsage == SecurityKeyUsage.Exchange)
				{
					if (this.parent.ClientCertificate.Certificate == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ClientCertificateNotProvidedOnClientCredentials")));
					}
					securityTokenProvider = new X509SecurityTokenProvider(this.parent.ClientCertificate.Certificate);
				}
				else if (tokenRequirement is InitiatorServiceModelSecurityTokenRequirement)
				{
					InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = tokenRequirement as InitiatorServiceModelSecurityTokenRequirement;
					string tokenType = initiatorServiceModelSecurityTokenRequirement.TokenType;
					if (this.IsIssuedSecurityTokenRequirement(initiatorServiceModelSecurityTokenRequirement))
					{
						FederatedClientCredentialsParameters federatedClientCredentialsParameters = this.FindFederatedChannelParameters(tokenRequirement);
						if (federatedClientCredentialsParameters != null && federatedClientCredentialsParameters.IssuedSecurityToken != null)
						{
							return new SimpleSecurityTokenProvider(federatedClientCredentialsParameters.IssuedSecurityToken, tokenRequirement);
						}
						securityTokenProvider = this.CreateIssuedSecurityTokenProvider(initiatorServiceModelSecurityTokenRequirement, federatedClientCredentialsParameters);
					}
					else if (tokenType == SecurityTokenTypes.X509Certificate)
					{
						if (initiatorServiceModelSecurityTokenRequirement.Properties.ContainsKey(SecurityTokenRequirement.KeyUsageProperty) && initiatorServiceModelSecurityTokenRequirement.KeyUsage == SecurityKeyUsage.Exchange)
						{
							securityTokenProvider = this.CreateServerX509TokenProvider(initiatorServiceModelSecurityTokenRequirement.TargetAddress);
						}
						else
						{
							if (this.parent.ClientCertificate.Certificate == null)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ClientCertificateNotProvidedOnClientCredentials")));
							}
							securityTokenProvider = new X509SecurityTokenProvider(this.parent.ClientCertificate.Certificate);
						}
					}
					else if (tokenType == SecurityTokenTypes.Kerberos)
					{
						string servicePrincipalName = this.GetServicePrincipalName(initiatorServiceModelSecurityTokenRequirement);
						securityTokenProvider = new ClientCredentialsSecurityTokenManager.KerberosSecurityTokenProviderWrapper(new KerberosSecurityTokenProvider(servicePrincipalName, this.parent.Windows.AllowedImpersonationLevel, SecurityUtils.GetNetworkCredentialOrDefault(this.parent.Windows.ClientCredential)), this.GetCredentialsHandle(initiatorServiceModelSecurityTokenRequirement));
					}
					else if (tokenType == SecurityTokenTypes.UserName)
					{
						if (this.parent.UserName.UserName == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UserNamePasswordNotProvidedOnClientCredentials")));
						}
						securityTokenProvider = new UserNameSecurityTokenProvider(this.parent.UserName.UserName, this.parent.UserName.Password);
					}
					else if (tokenType == ServiceModelSecurityTokenTypes.SspiCredential)
					{
						if (this.IsDigestAuthenticationScheme(initiatorServiceModelSecurityTokenRequirement))
						{
							NetworkCredential networkCredentialOrDefault = SecurityUtils.GetNetworkCredentialOrDefault(this.parent.HttpDigest.ClientCredential);
							SecurityUtils.FixNetworkCredential(ref networkCredentialOrDefault, true);
							securityTokenProvider = new SspiSecurityTokenProvider(networkCredentialOrDefault, true, this.parent.HttpDigest.AllowedImpersonationLevel);
						}
						else
						{
							securityTokenProvider = new SspiSecurityTokenProvider(SecurityUtils.GetNetworkCredentialOrDefault(this.parent.Windows.ClientCredential), this.parent.Windows.AllowNtlm, this.parent.Windows.AllowedImpersonationLevel);
						}
					}
					else if (tokenType == ServiceModelSecurityTokenTypes.Spnego)
					{
						securityTokenProvider = this.CreateSpnegoTokenProvider(initiatorServiceModelSecurityTokenRequirement);
					}
					else if (tokenType == ServiceModelSecurityTokenTypes.MutualSslnego)
					{
						securityTokenProvider = this.CreateTlsnegoTokenProvider(initiatorServiceModelSecurityTokenRequirement, true);
					}
					else if (tokenType == ServiceModelSecurityTokenTypes.AnonymousSslnego)
					{
						securityTokenProvider = this.CreateTlsnegoTokenProvider(initiatorServiceModelSecurityTokenRequirement, false);
					}
					else if (tokenType == ServiceModelSecurityTokenTypes.SecureConversation)
					{
						securityTokenProvider = this.CreateSecureConversationSecurityTokenProvider(initiatorServiceModelSecurityTokenRequirement);
					}
				}
			}
			if (securityTokenProvider == null && !tokenRequirement.IsOptionalToken)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SecurityTokenManagerCannotCreateProviderForRequirement", new object[]
				{
					tokenRequirement
				})));
			}
			return securityTokenProvider;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00014488 File Offset: 0x00012688
		private bool CardSpaceTryCreateSecurityTokenProviderStub(SecurityTokenRequirement tokenRequirement, ClientCredentialsSecurityTokenManager clientCredentialsTokenManager, out SecurityTokenProvider provider)
		{
			return InfoCardHelper.TryCreateSecurityTokenProvider(tokenRequirement, clientCredentialsTokenManager, out provider);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00014492 File Offset: 0x00012692
		protected SecurityTokenSerializer CreateSecurityTokenSerializer(SecurityVersion version)
		{
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("version"));
			}
			return this.CreateSecurityTokenSerializer(MessageSecurityTokenVersion.GetSecurityTokenVersion(version, true));
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000144BC File Offset: 0x000126BC
		public override SecurityTokenSerializer CreateSecurityTokenSerializer(SecurityTokenVersion version)
		{
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("version");
			}
			if (this.parent != null && this.parent.UseIdentityConfiguration)
			{
				return this.WrapTokenHandlersAsSecurityTokenSerializer(version);
			}
			MessageSecurityTokenVersion messageSecurityTokenVersion = version as MessageSecurityTokenVersion;
			if (messageSecurityTokenVersion != null)
			{
				return new WSSecurityTokenSerializer(messageSecurityTokenVersion.SecurityVersion, messageSecurityTokenVersion.TrustVersion, messageSecurityTokenVersion.SecureConversationVersion, messageSecurityTokenVersion.EmitBspRequiredAttributes, null, null, null);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SecurityTokenManagerCannotCreateSerializerForVersion", new object[]
			{
				version
			})));
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00014548 File Offset: 0x00012748
		private SecurityTokenSerializer WrapTokenHandlersAsSecurityTokenSerializer(SecurityTokenVersion version)
		{
			TrustVersion trustVersion = TrustVersion.WSTrust13;
			SecureConversationVersion secureConversationVersion = SecureConversationVersion.WSSecureConversation13;
			SecurityVersion securityVersion = SecurityVersion.WSSecurity11;
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
			}
			securityVersion = FederatedSecurityTokenManager.GetSecurityVersion(version);
			SecurityTokenHandlerCollectionManager securityTokenHandlerCollectionManager = this.parent.SecurityTokenHandlerCollectionManager;
			return new WsSecurityTokenSerializerAdapter(securityTokenHandlerCollectionManager[""], securityVersion, trustVersion, secureConversationVersion, false, null, null, null);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00014640 File Offset: 0x00012840
		public override SecurityTokenAuthenticator CreateSecurityTokenAuthenticator(SecurityTokenRequirement tokenRequirement, out SecurityTokenResolver outOfBandTokenResolver)
		{
			if (tokenRequirement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenRequirement");
			}
			outOfBandTokenResolver = null;
			SecurityTokenAuthenticator securityTokenAuthenticator = null;
			InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = tokenRequirement as InitiatorServiceModelSecurityTokenRequirement;
			if (initiatorServiceModelSecurityTokenRequirement != null)
			{
				string tokenType = initiatorServiceModelSecurityTokenRequirement.TokenType;
				if (this.IsIssuedSecurityTokenRequirement(initiatorServiceModelSecurityTokenRequirement))
				{
					return new GenericXmlSecurityTokenAuthenticator();
				}
				if (tokenType == SecurityTokenTypes.X509Certificate)
				{
					if (initiatorServiceModelSecurityTokenRequirement.IsOutOfBandToken)
					{
						securityTokenAuthenticator = new X509SecurityTokenAuthenticator(X509CertificateValidator.None);
					}
					else if (initiatorServiceModelSecurityTokenRequirement.PreferSslCertificateAuthenticator)
					{
						securityTokenAuthenticator = this.CreateServerSslX509TokenAuthenticator();
					}
					else
					{
						securityTokenAuthenticator = this.CreateServerX509TokenAuthenticator();
					}
				}
				else if (tokenType == SecurityTokenTypes.Rsa)
				{
					securityTokenAuthenticator = new RsaSecurityTokenAuthenticator();
				}
				else if (tokenType == SecurityTokenTypes.Kerberos)
				{
					securityTokenAuthenticator = new KerberosRequestorSecurityTokenAuthenticator();
				}
				else if (tokenType == ServiceModelSecurityTokenTypes.SecureConversation || tokenType == ServiceModelSecurityTokenTypes.MutualSslnego || tokenType == ServiceModelSecurityTokenTypes.AnonymousSslnego || tokenType == ServiceModelSecurityTokenTypes.Spnego)
				{
					securityTokenAuthenticator = new GenericXmlSecurityTokenAuthenticator();
				}
			}
			else if (tokenRequirement is RecipientServiceModelSecurityTokenRequirement && tokenRequirement.TokenType == SecurityTokenTypes.X509Certificate)
			{
				securityTokenAuthenticator = this.CreateServerX509TokenAuthenticator();
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

		// Token: 0x06000378 RID: 888 RVA: 0x0001477C File Offset: 0x0001297C
		private SafeFreeCredentials GetCredentialsHandle(InitiatorServiceModelSecurityTokenRequirement initiatorRequirement)
		{
			SspiIssuanceChannelParameter sspiIssuanceChannelParameter = this.GetSspiIssuanceChannelParameter(initiatorRequirement);
			if (sspiIssuanceChannelParameter == null)
			{
				return null;
			}
			return sspiIssuanceChannelParameter.CredentialsHandle;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001479C File Offset: 0x0001299C
		internal FederatedClientCredentialsParameters FindFederatedChannelParameters(SecurityTokenRequirement tokenRequirement)
		{
			FederatedClientCredentialsParameters federatedClientCredentialsParameters = null;
			ChannelParameterCollection channelParameterCollection = null;
			if (tokenRequirement.TryGetProperty<ChannelParameterCollection>(ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty, out channelParameterCollection) && channelParameterCollection != null)
			{
				foreach (object obj in channelParameterCollection)
				{
					federatedClientCredentialsParameters = (obj as FederatedClientCredentialsParameters);
					if (federatedClientCredentialsParameters != null)
					{
						break;
					}
				}
			}
			return federatedClientCredentialsParameters;
		}

		// Token: 0x04000975 RID: 2421
		private ClientCredentials parent;

		// Token: 0x02000AD4 RID: 2772
		internal class KerberosSecurityTokenProviderWrapper : CommunicationObjectSecurityTokenProvider
		{
			// Token: 0x06006E72 RID: 28274 RVA: 0x0019BBB9 File Offset: 0x00199DB9
			public KerberosSecurityTokenProviderWrapper(KerberosSecurityTokenProvider innerProvider, SafeFreeCredentials credentialsHandle)
			{
				this.innerProvider = innerProvider;
				this.credentialsHandle = credentialsHandle;
			}

			// Token: 0x06006E73 RID: 28275 RVA: 0x0019BBCF File Offset: 0x00199DCF
			public override void OnOpening()
			{
				base.OnOpening();
				if (this.credentialsHandle == null)
				{
					this.credentialsHandle = SecurityUtils.GetCredentialsHandle("Kerberos", this.innerProvider.NetworkCredential, false, new string[0]);
					this.ownCredentialsHandle = true;
				}
			}

			// Token: 0x06006E74 RID: 28276 RVA: 0x0019BC08 File Offset: 0x00199E08
			public override void OnClose(TimeSpan timeout)
			{
				base.OnClose(timeout);
				this.FreeCredentialsHandle();
			}

			// Token: 0x06006E75 RID: 28277 RVA: 0x0019BC17 File Offset: 0x00199E17
			public override void OnAbort()
			{
				base.OnAbort();
				this.FreeCredentialsHandle();
			}

			// Token: 0x06006E76 RID: 28278 RVA: 0x0019BC25 File Offset: 0x00199E25
			private void FreeCredentialsHandle()
			{
				if (this.credentialsHandle != null)
				{
					if (this.ownCredentialsHandle)
					{
						this.credentialsHandle.Close();
					}
					this.credentialsHandle = null;
				}
			}

			// Token: 0x06006E77 RID: 28279 RVA: 0x0019BC4C File Offset: 0x00199E4C
			internal SecurityToken GetToken(TimeSpan timeout, ChannelBinding channelbinding)
			{
				return new KerberosRequestorSecurityToken(this.innerProvider.ServicePrincipalName, this.innerProvider.TokenImpersonationLevel, this.innerProvider.NetworkCredential, SecurityUniqueId.Create().Value, this.credentialsHandle, channelbinding);
			}

			// Token: 0x06006E78 RID: 28280 RVA: 0x0019BC93 File Offset: 0x00199E93
			protected override SecurityToken GetTokenCore(TimeSpan timeout)
			{
				return this.GetToken(timeout, null);
			}

			// Token: 0x04003F10 RID: 16144
			private KerberosSecurityTokenProvider innerProvider;

			// Token: 0x04003F11 RID: 16145
			private SafeFreeCredentials credentialsHandle;

			// Token: 0x04003F12 RID: 16146
			private bool ownCredentialsHandle;
		}
	}
}
