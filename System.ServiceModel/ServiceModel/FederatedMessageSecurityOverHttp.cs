using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000134 RID: 308
	public sealed class FederatedMessageSecurityOverHttp
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x000222EB File Offset: 0x000204EB
		public FederatedMessageSecurityOverHttp()
		{
			this.negotiateServiceCredential = true;
			this.algorithmSuite = SecurityAlgorithmSuite.Default;
			this.issuedKeyType = SecurityKeyType.SymmetricKey;
			this.claimTypeRequirements = new Collection<ClaimTypeRequirement>();
			this.tokenRequestParameters = new Collection<XmlElement>();
			this.establishSecurityContext = true;
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00022329 File Offset: 0x00020529
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x00022331 File Offset: 0x00020531
		public bool NegotiateServiceCredential
		{
			get
			{
				return this.negotiateServiceCredential;
			}
			set
			{
				this.negotiateServiceCredential = value;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0002233A File Offset: 0x0002053A
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x00022342 File Offset: 0x00020542
		public SecurityAlgorithmSuite AlgorithmSuite
		{
			get
			{
				return this.algorithmSuite;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.algorithmSuite = value;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0002235E File Offset: 0x0002055E
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x00022366 File Offset: 0x00020566
		public bool EstablishSecurityContext
		{
			get
			{
				return this.establishSecurityContext;
			}
			set
			{
				this.establishSecurityContext = value;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0002236F File Offset: 0x0002056F
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x00022377 File Offset: 0x00020577
		[DefaultValue(null)]
		public EndpointAddress IssuerAddress
		{
			get
			{
				return this.issuerAddress;
			}
			set
			{
				this.issuerAddress = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00022380 File Offset: 0x00020580
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x00022388 File Offset: 0x00020588
		[DefaultValue(null)]
		public EndpointAddress IssuerMetadataAddress
		{
			get
			{
				return this.issuerMetadataAddress;
			}
			set
			{
				this.issuerMetadataAddress = value;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00022391 File Offset: 0x00020591
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x00022399 File Offset: 0x00020599
		[DefaultValue(null)]
		public Binding IssuerBinding
		{
			get
			{
				return this.issuerBinding;
			}
			set
			{
				this.issuerBinding = value;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x000223A2 File Offset: 0x000205A2
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x000223AA File Offset: 0x000205AA
		[DefaultValue(null)]
		public string IssuedTokenType
		{
			get
			{
				return this.issuedTokenType;
			}
			set
			{
				this.issuedTokenType = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x000223B3 File Offset: 0x000205B3
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x000223BB File Offset: 0x000205BB
		public SecurityKeyType IssuedKeyType
		{
			get
			{
				return this.issuedKeyType;
			}
			set
			{
				if (!SecurityKeyTypeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.issuedKeyType = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x000223E1 File Offset: 0x000205E1
		public Collection<ClaimTypeRequirement> ClaimTypeRequirements
		{
			get
			{
				return this.claimTypeRequirements;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x000223E9 File Offset: 0x000205E9
		public Collection<XmlElement> TokenRequestParameters
		{
			get
			{
				return this.tokenRequestParameters;
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000223F4 File Offset: 0x000205F4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal SecurityBindingElement CreateSecurityBindingElement(bool isSecureTransportMode, bool isReliableSession, MessageSecurityVersion version)
		{
			if (this.IssuedKeyType == SecurityKeyType.BearerKey && version.TrustVersion == TrustVersion.WSTrustFeb2005)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BearerKeyIncompatibleWithWSFederationHttpBinding")));
			}
			if (isReliableSession && !this.EstablishSecurityContext)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecureConversationRequiredByReliableSession")));
			}
			bool emitBspRequiredAttributes = true;
			IssuedSecurityTokenParameters issuedSecurityTokenParameters = new IssuedSecurityTokenParameters(this.IssuedTokenType, this.IssuerAddress, this.IssuerBinding);
			issuedSecurityTokenParameters.IssuerMetadataAddress = this.issuerMetadataAddress;
			issuedSecurityTokenParameters.KeyType = this.IssuedKeyType;
			if (this.IssuedKeyType == SecurityKeyType.SymmetricKey)
			{
				issuedSecurityTokenParameters.KeySize = this.AlgorithmSuite.DefaultSymmetricKeyLength;
			}
			else
			{
				issuedSecurityTokenParameters.KeySize = 0;
			}
			foreach (ClaimTypeRequirement item in this.claimTypeRequirements)
			{
				issuedSecurityTokenParameters.ClaimTypeRequirements.Add(item);
			}
			foreach (XmlElement item2 in this.TokenRequestParameters)
			{
				issuedSecurityTokenParameters.AdditionalRequestParameters.Add(item2);
			}
			WSSecurityTokenSerializer tokenSerializer = new WSSecurityTokenSerializer(version.SecurityVersion, version.TrustVersion, version.SecureConversationVersion, emitBspRequiredAttributes, null, null, null);
			SecurityStandardsManager standardsManager = new SecurityStandardsManager(version, tokenSerializer);
			issuedSecurityTokenParameters.AddAlgorithmParameters(this.AlgorithmSuite, standardsManager, this.issuedKeyType);
			SecurityBindingElement securityBindingElement;
			if (isSecureTransportMode)
			{
				securityBindingElement = SecurityBindingElement.CreateIssuedTokenOverTransportBindingElement(issuedSecurityTokenParameters);
			}
			else if (this.negotiateServiceCredential)
			{
				securityBindingElement = SecurityBindingElement.CreateIssuedTokenForSslBindingElement(issuedSecurityTokenParameters, version.SecurityPolicyVersion != SecurityPolicyVersion.WSSecurityPolicy11);
			}
			else
			{
				securityBindingElement = SecurityBindingElement.CreateIssuedTokenForCertificateBindingElement(issuedSecurityTokenParameters);
			}
			securityBindingElement.MessageSecurityVersion = version;
			securityBindingElement.DefaultAlgorithmSuite = this.AlgorithmSuite;
			SecurityBindingElement securityBindingElement2;
			if (this.EstablishSecurityContext)
			{
				securityBindingElement2 = SecurityBindingElement.CreateSecureConversationBindingElement(securityBindingElement, true);
			}
			else
			{
				securityBindingElement2 = securityBindingElement;
			}
			securityBindingElement2.MessageSecurityVersion = version;
			securityBindingElement2.DefaultAlgorithmSuite = this.AlgorithmSuite;
			securityBindingElement2.IncludeTimestamp = true;
			if (!isReliableSession)
			{
				securityBindingElement2.LocalServiceSettings.ReconnectTransportOnFailure = false;
				securityBindingElement2.LocalClientSettings.ReconnectTransportOnFailure = false;
			}
			else
			{
				securityBindingElement2.LocalServiceSettings.ReconnectTransportOnFailure = true;
				securityBindingElement2.LocalClientSettings.ReconnectTransportOnFailure = true;
			}
			if (this.establishSecurityContext)
			{
				securityBindingElement.LocalServiceSettings.IssuedCookieLifetime = NegotiationTokenAuthenticator<SspiNegotiationTokenAuthenticatorState>.defaultServerIssuedTransitionTokenLifetime;
			}
			return securityBindingElement2;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00022644 File Offset: 0x00020844
		internal static bool TryCreate(SecurityBindingElement sbe, bool isSecureTransportMode, bool isReliableSession, MessageSecurityVersion version, out FederatedMessageSecurityOverHttp messageSecurity)
		{
			messageSecurity = null;
			if (!sbe.IncludeTimestamp)
			{
				return false;
			}
			if (sbe.SecurityHeaderLayout != SecurityHeaderLayout.Strict)
			{
				return false;
			}
			bool emitBspRequiredAttributes = true;
			SecurityBindingElement securityBindingElement;
			bool flag = SecurityBindingElement.IsSecureConversationBinding(sbe, true, out securityBindingElement);
			securityBindingElement = (flag ? securityBindingElement : sbe);
			if (isSecureTransportMode && !(securityBindingElement is TransportSecurityBindingElement))
			{
				return false;
			}
			bool flag2 = true;
			IssuedSecurityTokenParameters issuedSecurityTokenParameters;
			if (isSecureTransportMode)
			{
				if (!SecurityBindingElement.IsIssuedTokenOverTransportBinding(securityBindingElement, out issuedSecurityTokenParameters))
				{
					return false;
				}
			}
			else if (SecurityBindingElement.IsIssuedTokenForSslBinding(securityBindingElement, version.SecurityPolicyVersion != SecurityPolicyVersion.WSSecurityPolicy11, out issuedSecurityTokenParameters))
			{
				flag2 = true;
			}
			else
			{
				if (!SecurityBindingElement.IsIssuedTokenForCertificateBinding(securityBindingElement, out issuedSecurityTokenParameters))
				{
					return false;
				}
				flag2 = false;
			}
			if (issuedSecurityTokenParameters.KeyType == SecurityKeyType.BearerKey && version.TrustVersion == TrustVersion.WSTrustFeb2005)
			{
				return false;
			}
			WSSecurityTokenSerializer tokenSerializer = new WSSecurityTokenSerializer(version.SecurityVersion, version.TrustVersion, version.SecureConversationVersion, emitBspRequiredAttributes, null, null, null);
			SecurityStandardsManager standardsManager = new SecurityStandardsManager(version, tokenSerializer);
			Collection<XmlElement> collection;
			if (!issuedSecurityTokenParameters.DoAlgorithmsMatch(sbe.DefaultAlgorithmSuite, standardsManager, out collection))
			{
				return false;
			}
			messageSecurity = new FederatedMessageSecurityOverHttp();
			messageSecurity.AlgorithmSuite = sbe.DefaultAlgorithmSuite;
			messageSecurity.NegotiateServiceCredential = flag2;
			messageSecurity.EstablishSecurityContext = flag;
			messageSecurity.IssuedTokenType = issuedSecurityTokenParameters.TokenType;
			messageSecurity.IssuerAddress = issuedSecurityTokenParameters.IssuerAddress;
			messageSecurity.IssuerBinding = issuedSecurityTokenParameters.IssuerBinding;
			messageSecurity.IssuerMetadataAddress = issuedSecurityTokenParameters.IssuerMetadataAddress;
			messageSecurity.IssuedKeyType = issuedSecurityTokenParameters.KeyType;
			foreach (ClaimTypeRequirement item in issuedSecurityTokenParameters.ClaimTypeRequirements)
			{
				messageSecurity.ClaimTypeRequirements.Add(item);
			}
			foreach (XmlElement item2 in collection)
			{
				messageSecurity.TokenRequestParameters.Add(item2);
			}
			return issuedSecurityTokenParameters.AlternativeIssuerEndpoints == null || issuedSecurityTokenParameters.AlternativeIssuerEndpoints.Count <= 0;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00022844 File Offset: 0x00020A44
		internal bool InternalShouldSerialize()
		{
			return this.ShouldSerializeAlgorithmSuite() || this.ShouldSerializeClaimTypeRequirements() || this.ShouldSerializeNegotiateServiceCredential() || this.ShouldSerializeEstablishSecurityContext() || this.ShouldSerializeIssuedKeyType() || this.ShouldSerializeTokenRequestParameters();
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00022876 File Offset: 0x00020A76
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeAlgorithmSuite()
		{
			return this.AlgorithmSuite != SecurityAlgorithmSuite.Default;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00022888 File Offset: 0x00020A88
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeClaimTypeRequirements()
		{
			return this.ClaimTypeRequirements.Count > 0;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00022898 File Offset: 0x00020A98
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeNegotiateServiceCredential()
		{
			return !this.NegotiateServiceCredential;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x000228A3 File Offset: 0x00020AA3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeEstablishSecurityContext()
		{
			return !this.EstablishSecurityContext;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x000228AE File Offset: 0x00020AAE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeIssuedKeyType()
		{
			return this.IssuedKeyType > SecurityKeyType.SymmetricKey;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000228B9 File Offset: 0x00020AB9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTokenRequestParameters()
		{
			return this.TokenRequestParameters.Count > 0;
		}

		// Token: 0x04000B1B RID: 2843
		internal const bool DefaultNegotiateServiceCredential = true;

		// Token: 0x04000B1C RID: 2844
		internal const SecurityKeyType DefaultIssuedKeyType = SecurityKeyType.SymmetricKey;

		// Token: 0x04000B1D RID: 2845
		internal const bool DefaultEstablishSecurityContext = true;

		// Token: 0x04000B1E RID: 2846
		private bool establishSecurityContext;

		// Token: 0x04000B1F RID: 2847
		private bool negotiateServiceCredential;

		// Token: 0x04000B20 RID: 2848
		private SecurityAlgorithmSuite algorithmSuite;

		// Token: 0x04000B21 RID: 2849
		private EndpointAddress issuerAddress;

		// Token: 0x04000B22 RID: 2850
		private EndpointAddress issuerMetadataAddress;

		// Token: 0x04000B23 RID: 2851
		private Binding issuerBinding;

		// Token: 0x04000B24 RID: 2852
		private Collection<ClaimTypeRequirement> claimTypeRequirements;

		// Token: 0x04000B25 RID: 2853
		private string issuedTokenType;

		// Token: 0x04000B26 RID: 2854
		private SecurityKeyType issuedKeyType;

		// Token: 0x04000B27 RID: 2855
		private Collection<XmlElement> tokenRequestParameters;
	}
}
