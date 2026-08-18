using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel
{
	// Token: 0x0200013C RID: 316
	[__DynamicallyInvokable]
	public sealed class MessageSecurityOverTcp
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x00022D37 File Offset: 0x00020F37
		[__DynamicallyInvokable]
		public MessageSecurityOverTcp()
		{
			this.clientCredentialType = MessageCredentialType.Windows;
			this.algorithmSuite = SecurityAlgorithmSuite.Default;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00022D51 File Offset: 0x00020F51
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x00022D59 File Offset: 0x00020F59
		[DefaultValue(MessageCredentialType.Windows)]
		[__DynamicallyInvokable]
		public MessageCredentialType ClientCredentialType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.clientCredentialType;
			}
			[__DynamicallyInvokable]
			set
			{
				if (!MessageCredentialTypeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.clientCredentialType = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00022D7F File Offset: 0x00020F7F
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00022D87 File Offset: 0x00020F87
		[DefaultValue(typeof(SecurityAlgorithmSuite), "Default")]
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

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00022DAA File Offset: 0x00020FAA
		internal bool WasAlgorithmSuiteSet
		{
			get
			{
				return this.wasAlgorithmSuiteSet;
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00022DB4 File Offset: 0x00020FB4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal SecurityBindingElement CreateSecurityBindingElement(bool isSecureTransportMode, bool isReliableSession, BindingElement transportBindingElement)
		{
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
					securityBindingElement = SecurityBindingElement.CreateIssuedTokenOverTransportBindingElement(IssuedSecurityTokenParameters.CreateInfoCardParameters(new SecurityStandardsManager(), this.algorithmSuite));
					break;
				default:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}
				securityBindingElement2 = SecurityBindingElement.CreateSecureConversationBindingElement(securityBindingElement);
			}
			else
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
					securityBindingElement = SecurityBindingElement.CreateIssuedTokenForSslBindingElement(IssuedSecurityTokenParameters.CreateInfoCardParameters(new SecurityStandardsManager(), this.algorithmSuite), true);
					break;
				default:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}
				securityBindingElement2 = SecurityBindingElement.CreateSecureConversationBindingElement(securityBindingElement, true);
			}
			securityBindingElement2.DefaultAlgorithmSuite = (securityBindingElement.DefaultAlgorithmSuite = this.AlgorithmSuite);
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
			securityBindingElement.LocalServiceSettings.IssuedCookieLifetime = NegotiationTokenAuthenticator<SspiNegotiationTokenAuthenticatorState>.defaultServerIssuedTransitionTokenLifetime;
			securityBindingElement2.MessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11;
			securityBindingElement.MessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11;
			return securityBindingElement2;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00022F44 File Offset: 0x00021144
		internal static bool TryCreate(SecurityBindingElement sbe, bool isReliableSession, BindingElement transportBindingElement, out MessageSecurityOverTcp messageSecurity)
		{
			messageSecurity = null;
			if (sbe == null)
			{
				return false;
			}
			if (!sbe.IncludeTimestamp)
			{
				return false;
			}
			if (sbe.MessageSecurityVersion != MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11 && sbe.MessageSecurityVersion != MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10)
			{
				return false;
			}
			if (sbe.SecurityHeaderLayout != SecurityHeaderLayout.Strict)
			{
				return false;
			}
			SecurityBindingElement securityBindingElement;
			if (!SecurityBindingElement.IsSecureConversationBinding(sbe, true, out securityBindingElement))
			{
				return false;
			}
			bool flag = securityBindingElement is TransportSecurityBindingElement;
			MessageCredentialType messageCredentialType;
			IssuedSecurityTokenParameters parameters;
			if (flag)
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
					if (!IssuedSecurityTokenParameters.IsInfoCardParameters(parameters, new SecurityStandardsManager(securityBindingElement.MessageSecurityVersion, new WSSecurityTokenSerializer(securityBindingElement.MessageSecurityVersion.SecurityVersion, securityBindingElement.MessageSecurityVersion.TrustVersion, securityBindingElement.MessageSecurityVersion.SecureConversationVersion, true, null, null, null))))
					{
						return false;
					}
					messageCredentialType = MessageCredentialType.IssuedToken;
				}
			}
			else if (SecurityBindingElement.IsUserNameForSslBinding(securityBindingElement, true))
			{
				messageCredentialType = MessageCredentialType.UserName;
			}
			else if (SecurityBindingElement.IsSslNegotiationBinding(securityBindingElement, true, true))
			{
				messageCredentialType = MessageCredentialType.Certificate;
			}
			else if (SecurityBindingElement.IsSspiNegotiationBinding(securityBindingElement, true))
			{
				messageCredentialType = MessageCredentialType.Windows;
			}
			else if (SecurityBindingElement.IsIssuedTokenForSslBinding(securityBindingElement, true, out parameters))
			{
				if (!IssuedSecurityTokenParameters.IsInfoCardParameters(parameters, new SecurityStandardsManager(securityBindingElement.MessageSecurityVersion, new WSSecurityTokenSerializer(securityBindingElement.MessageSecurityVersion.SecurityVersion, securityBindingElement.MessageSecurityVersion.TrustVersion, securityBindingElement.MessageSecurityVersion.SecureConversationVersion, true, null, null, null))))
				{
					return false;
				}
				messageCredentialType = MessageCredentialType.IssuedToken;
			}
			else
			{
				if (!SecurityBindingElement.IsSslNegotiationBinding(securityBindingElement, false, true))
				{
					return false;
				}
				messageCredentialType = MessageCredentialType.None;
			}
			messageSecurity = new MessageSecurityOverTcp();
			messageSecurity.ClientCredentialType = messageCredentialType;
			if (messageCredentialType != MessageCredentialType.IssuedToken)
			{
				messageSecurity.AlgorithmSuite = securityBindingElement.DefaultAlgorithmSuite;
			}
			return true;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000230CD File Offset: 0x000212CD
		internal bool InternalShouldSerialize()
		{
			return this.ClientCredentialType != MessageCredentialType.Windows || this.AlgorithmSuite != NetTcpDefaults.MessageSecurityAlgorithmSuite;
		}

		// Token: 0x04000B43 RID: 2883
		internal const MessageCredentialType DefaultClientCredentialType = MessageCredentialType.Windows;

		// Token: 0x04000B44 RID: 2884
		private MessageCredentialType clientCredentialType;

		// Token: 0x04000B45 RID: 2885
		private SecurityAlgorithmSuite algorithmSuite;

		// Token: 0x04000B46 RID: 2886
		private bool wasAlgorithmSuiteSet;
	}
}
