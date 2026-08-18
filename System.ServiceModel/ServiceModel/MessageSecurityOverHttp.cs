using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel
{
	// Token: 0x0200013D RID: 317
	public class MessageSecurityOverHttp
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x000230EA File Offset: 0x000212EA
		public MessageSecurityOverHttp()
		{
			this.clientCredentialType = MessageCredentialType.Windows;
			this.negotiateServiceCredential = true;
			this.algorithmSuite = SecurityAlgorithmSuite.Default;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0002310B File Offset: 0x0002130B
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x00023113 File Offset: 0x00021313
		public MessageCredentialType ClientCredentialType
		{
			get
			{
				return this.clientCredentialType;
			}
			set
			{
				if (!MessageCredentialTypeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.clientCredentialType = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00023139 File Offset: 0x00021339
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x00023141 File Offset: 0x00021341
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

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0002314A File Offset: 0x0002134A
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x00023152 File Offset: 0x00021352
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
				this.wasAlgorithmSuiteSet = true;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00023175 File Offset: 0x00021375
		internal bool WasAlgorithmSuiteSet
		{
			get
			{
				return this.wasAlgorithmSuiteSet;
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0002317D File Offset: 0x0002137D
		protected virtual bool IsSecureConversationEnabled()
		{
			return true;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00023180 File Offset: 0x00021380
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal SecurityBindingElement CreateSecurityBindingElement(bool isSecureTransportMode, bool isReliableSession, MessageSecurityVersion version)
		{
			if (isReliableSession && !this.IsSecureConversationEnabled())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecureConversationRequiredByReliableSession")));
			}
			bool flag = false;
			bool emitBspRequiredAttributes = true;
			SecurityBindingElement securityBindingElement;
			SecurityBindingElement securityBindingElement2;
			if (isSecureTransportMode)
			{
				switch (this.clientCredentialType)
				{
				case MessageCredentialType.None:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ClientCredentialTypeMustBeSpecifiedForMixedMode")));
				case MessageCredentialType.Windows:
					securityBindingElement = SecurityBindingElement.CreateSspiNegotiationOverTransportBindingElement(true);
					break;
				case MessageCredentialType.UserName:
					securityBindingElement = SecurityBindingElement.CreateUserNameOverTransportBindingElement();
					break;
				case MessageCredentialType.Certificate:
					securityBindingElement = SecurityBindingElement.CreateCertificateOverTransportBindingElement();
					break;
				case MessageCredentialType.IssuedToken:
					securityBindingElement = SecurityBindingElement.CreateIssuedTokenOverTransportBindingElement(IssuedSecurityTokenParameters.CreateInfoCardParameters(new SecurityStandardsManager(new WSSecurityTokenSerializer(emitBspRequiredAttributes)), this.algorithmSuite));
					break;
				default:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}
				if (this.IsSecureConversationEnabled())
				{
					securityBindingElement2 = SecurityBindingElement.CreateSecureConversationBindingElement(securityBindingElement, true);
				}
				else
				{
					securityBindingElement2 = securityBindingElement;
				}
			}
			else
			{
				if (this.negotiateServiceCredential)
				{
					switch (this.clientCredentialType)
					{
					case MessageCredentialType.None:
						securityBindingElement = SecurityBindingElement.CreateSslNegotiationBindingElement(false, true);
						break;
					case MessageCredentialType.Windows:
						securityBindingElement = SecurityBindingElement.CreateSspiNegotiationBindingElement(true);
						break;
					case MessageCredentialType.UserName:
						securityBindingElement = SecurityBindingElement.CreateUserNameForSslBindingElement(true);
						break;
					case MessageCredentialType.Certificate:
						securityBindingElement = SecurityBindingElement.CreateSslNegotiationBindingElement(true, true);
						break;
					case MessageCredentialType.IssuedToken:
						securityBindingElement = SecurityBindingElement.CreateIssuedTokenForSslBindingElement(IssuedSecurityTokenParameters.CreateInfoCardParameters(new SecurityStandardsManager(new WSSecurityTokenSerializer(emitBspRequiredAttributes)), this.algorithmSuite), true);
						break;
					default:
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
					}
				}
				else
				{
					switch (this.clientCredentialType)
					{
					case MessageCredentialType.None:
						securityBindingElement = SecurityBindingElement.CreateAnonymousForCertificateBindingElement();
						break;
					case MessageCredentialType.Windows:
						securityBindingElement = SecurityBindingElement.CreateKerberosBindingElement();
						flag = true;
						break;
					case MessageCredentialType.UserName:
						securityBindingElement = SecurityBindingElement.CreateUserNameForCertificateBindingElement();
						break;
					case MessageCredentialType.Certificate:
						securityBindingElement = SecurityBindingElement.CreateMutualCertificateBindingElement();
						break;
					case MessageCredentialType.IssuedToken:
						securityBindingElement = SecurityBindingElement.CreateIssuedTokenForCertificateBindingElement(IssuedSecurityTokenParameters.CreateInfoCardParameters(new SecurityStandardsManager(new WSSecurityTokenSerializer(emitBspRequiredAttributes)), this.algorithmSuite));
						break;
					default:
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
					}
				}
				if (this.IsSecureConversationEnabled())
				{
					securityBindingElement2 = SecurityBindingElement.CreateSecureConversationBindingElement(securityBindingElement, true);
				}
				else
				{
					securityBindingElement2 = securityBindingElement;
				}
			}
			if (this.wasAlgorithmSuiteSet || !flag)
			{
				securityBindingElement2.DefaultAlgorithmSuite = (securityBindingElement.DefaultAlgorithmSuite = this.AlgorithmSuite);
			}
			else if (flag)
			{
				securityBindingElement2.DefaultAlgorithmSuite = (securityBindingElement.DefaultAlgorithmSuite = SecurityAlgorithmSuite.KerberosDefault);
			}
			securityBindingElement2.IncludeTimestamp = true;
			securityBindingElement.MessageSecurityVersion = version;
			securityBindingElement2.MessageSecurityVersion = version;
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
			if (this.IsSecureConversationEnabled())
			{
				securityBindingElement.LocalServiceSettings.IssuedCookieLifetime = NegotiationTokenAuthenticator<SspiNegotiationTokenAuthenticatorState>.defaultServerIssuedTransitionTokenLifetime;
			}
			return securityBindingElement2;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0002341C File Offset: 0x0002161C
		internal static bool TryCreate<TSecurity>(SecurityBindingElement sbe, bool isSecureTransportMode, bool isReliableSession, out TSecurity messageSecurity) where TSecurity : MessageSecurityOverHttp
		{
			messageSecurity = default(TSecurity);
			if (!sbe.IncludeTimestamp)
			{
				return false;
			}
			if (sbe.SecurityHeaderLayout != SecurityHeaderLayout.Strict)
			{
				return false;
			}
			bool flag = true;
			SecurityAlgorithmSuite @default = SecurityAlgorithmSuite.Default;
			SecurityBindingElement securityBindingElement;
			bool flag2;
			if (!SecurityBindingElement.IsSecureConversationBinding(sbe, true, out securityBindingElement))
			{
				flag2 = false;
				securityBindingElement = sbe;
			}
			else
			{
				flag2 = true;
			}
			if (!flag2 && typeof(TSecurity).Equals(typeof(MessageSecurityOverHttp)))
			{
				return false;
			}
			if (!flag2 && isReliableSession)
			{
				return false;
			}
			if (isSecureTransportMode && !(securityBindingElement is TransportSecurityBindingElement))
			{
				return false;
			}
			MessageCredentialType messageCredentialType;
			IssuedSecurityTokenParameters parameters;
			if (isSecureTransportMode)
			{
				if (SecurityBindingElement.IsUserNameOverTransportBinding(securityBindingElement))
				{
					messageCredentialType = MessageCredentialType.UserName;
				}
				else if (SecurityBindingElement.IsCertificateOverTransportBinding(securityBindingElement))
				{
					messageCredentialType = MessageCredentialType.Certificate;
				}
				else if (SecurityBindingElement.IsSspiNegotiationOverTransportBinding(securityBindingElement, true))
				{
					messageCredentialType = MessageCredentialType.Windows;
				}
				else
				{
					if (!SecurityBindingElement.IsIssuedTokenOverTransportBinding(securityBindingElement, out parameters))
					{
						return false;
					}
					if (!IssuedSecurityTokenParameters.IsInfoCardParameters(parameters, new SecurityStandardsManager(sbe.MessageSecurityVersion, new WSSecurityTokenSerializer(sbe.MessageSecurityVersion.SecurityVersion, sbe.MessageSecurityVersion.TrustVersion, sbe.MessageSecurityVersion.SecureConversationVersion, true, null, null, null))))
					{
						return false;
					}
					messageCredentialType = MessageCredentialType.IssuedToken;
				}
			}
			else if (SecurityBindingElement.IsSslNegotiationBinding(securityBindingElement, false, true))
			{
				flag = true;
				messageCredentialType = MessageCredentialType.None;
			}
			else if (SecurityBindingElement.IsUserNameForSslBinding(securityBindingElement, true))
			{
				flag = true;
				messageCredentialType = MessageCredentialType.UserName;
			}
			else if (SecurityBindingElement.IsSslNegotiationBinding(securityBindingElement, true, true))
			{
				flag = true;
				messageCredentialType = MessageCredentialType.Certificate;
			}
			else if (SecurityBindingElement.IsSspiNegotiationBinding(securityBindingElement, true))
			{
				flag = true;
				messageCredentialType = MessageCredentialType.Windows;
			}
			else if (SecurityBindingElement.IsIssuedTokenForSslBinding(securityBindingElement, true, out parameters))
			{
				if (!IssuedSecurityTokenParameters.IsInfoCardParameters(parameters, new SecurityStandardsManager(sbe.MessageSecurityVersion, new WSSecurityTokenSerializer(sbe.MessageSecurityVersion.SecurityVersion, sbe.MessageSecurityVersion.TrustVersion, sbe.MessageSecurityVersion.SecureConversationVersion, true, null, null, null))))
				{
					return false;
				}
				flag = true;
				messageCredentialType = MessageCredentialType.IssuedToken;
			}
			else if (SecurityBindingElement.IsUserNameForCertificateBinding(securityBindingElement))
			{
				flag = false;
				messageCredentialType = MessageCredentialType.UserName;
			}
			else if (SecurityBindingElement.IsMutualCertificateBinding(securityBindingElement))
			{
				flag = false;
				messageCredentialType = MessageCredentialType.Certificate;
			}
			else if (SecurityBindingElement.IsKerberosBinding(securityBindingElement))
			{
				flag = false;
				messageCredentialType = MessageCredentialType.Windows;
			}
			else if (SecurityBindingElement.IsIssuedTokenForCertificateBinding(securityBindingElement, out parameters))
			{
				if (!IssuedSecurityTokenParameters.IsInfoCardParameters(parameters, new SecurityStandardsManager(sbe.MessageSecurityVersion, new WSSecurityTokenSerializer(sbe.MessageSecurityVersion.SecurityVersion, sbe.MessageSecurityVersion.TrustVersion, sbe.MessageSecurityVersion.SecureConversationVersion, true, null, null, null))))
				{
					return false;
				}
				flag = false;
				messageCredentialType = MessageCredentialType.IssuedToken;
			}
			else
			{
				if (!SecurityBindingElement.IsAnonymousForCertificateBinding(securityBindingElement))
				{
					return false;
				}
				flag = false;
				messageCredentialType = MessageCredentialType.None;
			}
			if (typeof(NonDualMessageSecurityOverHttp).Equals(typeof(TSecurity)))
			{
				messageSecurity = (TSecurity)((object)new NonDualMessageSecurityOverHttp());
				((NonDualMessageSecurityOverHttp)((object)messageSecurity)).EstablishSecurityContext = flag2;
			}
			else
			{
				messageSecurity = (TSecurity)((object)new MessageSecurityOverHttp());
			}
			messageSecurity.ClientCredentialType = messageCredentialType;
			messageSecurity.NegotiateServiceCredential = flag;
			messageSecurity.AlgorithmSuite = sbe.DefaultAlgorithmSuite;
			return true;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x000236D8 File Offset: 0x000218D8
		internal bool InternalShouldSerialize()
		{
			return this.ShouldSerializeAlgorithmSuite() || this.ShouldSerializeClientCredentialType() || this.ShouldSerializeNegotiateServiceCredential();
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000236F2 File Offset: 0x000218F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeAlgorithmSuite()
		{
			return this.AlgorithmSuite != SecurityAlgorithmSuite.Default;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00023704 File Offset: 0x00021904
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeClientCredentialType()
		{
			return this.ClientCredentialType != MessageCredentialType.Windows;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00023712 File Offset: 0x00021912
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeNegotiateServiceCredential()
		{
			return !this.NegotiateServiceCredential;
		}

		// Token: 0x04000B47 RID: 2887
		internal const MessageCredentialType DefaultClientCredentialType = MessageCredentialType.Windows;

		// Token: 0x04000B48 RID: 2888
		internal const bool DefaultNegotiateServiceCredential = true;

		// Token: 0x04000B49 RID: 2889
		private MessageCredentialType clientCredentialType;

		// Token: 0x04000B4A RID: 2890
		private bool negotiateServiceCredential;

		// Token: 0x04000B4B RID: 2891
		private SecurityAlgorithmSuite algorithmSuite;

		// Token: 0x04000B4C RID: 2892
		private bool wasAlgorithmSuiteSet;
	}
}
