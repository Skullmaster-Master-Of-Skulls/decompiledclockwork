using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200027A RID: 634
	internal static class InfoCardHelper
	{
		// Token: 0x06001215 RID: 4629 RVA: 0x00042BDC File Offset: 0x00040DDC
		public static bool TryCreateSecurityTokenProvider(SecurityTokenRequirement tokenRequirement, ClientCredentialsSecurityTokenManager clientCredentialsTokenManager, out SecurityTokenProvider provider)
		{
			if (tokenRequirement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenRequirement");
			}
			if (clientCredentialsTokenManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("clientCredentialsTokenManager");
			}
			provider = null;
			if (clientCredentialsTokenManager.ClientCredentials.SupportInteractive && (!(null != clientCredentialsTokenManager.ClientCredentials.IssuedToken.LocalIssuerAddress) || clientCredentialsTokenManager.ClientCredentials.IssuedToken.LocalIssuerBinding == null) && clientCredentialsTokenManager.IsIssuedSecurityTokenRequirement(tokenRequirement))
			{
				InfoCardChannelParameter infoCardChannelParameter = null;
				ChannelParameterCollection channelParameterCollection;
				if (tokenRequirement.TryGetProperty<ChannelParameterCollection>(ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty, out channelParameterCollection))
				{
					foreach (object obj in channelParameterCollection)
					{
						if (obj is InfoCardChannelParameter)
						{
							infoCardChannelParameter = (InfoCardChannelParameter)obj;
							break;
						}
					}
				}
				if (infoCardChannelParameter == null || !infoCardChannelParameter.RequiresInfoCard)
				{
					return false;
				}
				EndpointAddress property = tokenRequirement.GetProperty<EndpointAddress>(ServiceModelSecurityTokenRequirement.TargetAddressProperty);
				IssuedSecurityTokenParameters property2 = tokenRequirement.GetProperty<IssuedSecurityTokenParameters>(ServiceModelSecurityTokenRequirement.IssuedSecurityTokenParametersProperty);
				Uri uri;
				if (!tokenRequirement.TryGetProperty<Uri>(ServiceModelSecurityTokenRequirement.PrivacyNoticeUriProperty, out uri))
				{
					uri = null;
				}
				int num;
				if (!tokenRequirement.TryGetProperty<int>(ServiceModelSecurityTokenRequirement.PrivacyNoticeVersionProperty, out num))
				{
					num = 0;
				}
				provider = InfoCardHelper.CreateTokenProviderForNextLeg(tokenRequirement, property, property2.IssuerAddress, infoCardChannelParameter.RelyingPartyIssuer, clientCredentialsTokenManager, infoCardChannelParameter);
			}
			return provider != null;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00042D20 File Offset: 0x00040F20
		public static bool IsInfocardRequired(Binding binding, ClientCredentials clientCreds, SecurityTokenManager clientCredentialsTokenManager, EndpointAddress target, out CardSpacePolicyElement[] infocardChain, out Uri relyingPartyIssuer)
		{
			infocardChain = null;
			bool flag = false;
			relyingPartyIssuer = null;
			if (!clientCreds.SupportInteractive || (null != clientCreds.IssuedToken.LocalIssuerAddress && clientCreds.IssuedToken.LocalIssuerBinding != null))
			{
				return false;
			}
			IssuedSecurityTokenParameters issuedSecurityTokenParameters = InfoCardHelper.TryGetNextStsIssuedTokenParameters(binding);
			if (issuedSecurityTokenParameters != null)
			{
				Uri firstPrivacyNoticeLink;
				int firstPrivacyNoticeVersion;
				InfoCardHelper.GetPrivacyNoticeLinkFromIssuerBinding(binding, out firstPrivacyNoticeLink, out firstPrivacyNoticeVersion);
				InfoCardHelper.PolicyElement[] policyChain = InfoCardHelper.GetPolicyChain(target, binding, issuedSecurityTokenParameters, firstPrivacyNoticeLink, firstPrivacyNoticeVersion, clientCredentialsTokenManager);
				relyingPartyIssuer = null;
				if (policyChain != null)
				{
					flag = InfoCardHelper.RequiresInfoCard(policyChain, out relyingPartyIssuer);
				}
				if (flag)
				{
					infocardChain = new CardSpacePolicyElement[policyChain.Length];
					for (int i = 0; i < policyChain.Length; i++)
					{
						infocardChain[i] = policyChain[i].ToCardSpacePolicyElement();
					}
				}
			}
			return flag;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x00042DC7 File Offset: 0x00040FC7
		private static Uri SelfIssuerUri
		{
			get
			{
				if (InfoCardHelper.selfIssuerUri == null)
				{
					InfoCardHelper.selfIssuerUri = new Uri("http://schemas.microsoft.com/ws/2005/05/identity/issuer/self");
				}
				return InfoCardHelper.selfIssuerUri;
			}
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00042DEC File Offset: 0x00040FEC
		private static InfoCardHelper.PolicyElement[] GetPolicyChain(EndpointAddress target, Binding outerBinding, IssuedSecurityTokenParameters parameters, Uri firstPrivacyNoticeLink, int firstPrivacyNoticeVersion, SecurityTokenManager clientCredentialsTokenManager)
		{
			EndpointAddress target2 = target;
			IssuedSecurityTokenParameters issuedSecurityTokenParameters = parameters;
			List<InfoCardHelper.PolicyElement> list = new List<InfoCardHelper.PolicyElement>();
			Uri privacyNoticeLink = firstPrivacyNoticeLink;
			int privacyNoticeVersion = firstPrivacyNoticeVersion;
			bool flag = false;
			while (issuedSecurityTokenParameters != null)
			{
				MessageSecurityVersion bindingSecurityVersionOrDefault;
				if (issuedSecurityTokenParameters.IssuerBinding == null)
				{
					bindingSecurityVersionOrDefault = InfoCardHelper.GetBindingSecurityVersionOrDefault(outerBinding);
				}
				else
				{
					bindingSecurityVersionOrDefault = InfoCardHelper.GetBindingSecurityVersionOrDefault(issuedSecurityTokenParameters.IssuerBinding);
				}
				list.Add(new InfoCardHelper.PolicyElement(target2, issuedSecurityTokenParameters.IssuerAddress, issuedSecurityTokenParameters.CreateRequestParameters(bindingSecurityVersionOrDefault, clientCredentialsTokenManager.CreateSecurityTokenSerializer(bindingSecurityVersionOrDefault.SecurityTokenVersion)), privacyNoticeLink, privacyNoticeVersion, flag, issuedSecurityTokenParameters.IssuerBinding));
				flag = InfoCardHelper.IsReferralToManagedIssuer(issuedSecurityTokenParameters.IssuerBinding);
				InfoCardHelper.GetPrivacyNoticeLinkFromIssuerBinding(issuedSecurityTokenParameters.IssuerBinding, out privacyNoticeLink, out privacyNoticeVersion);
				target2 = issuedSecurityTokenParameters.IssuerAddress;
				outerBinding = issuedSecurityTokenParameters.IssuerBinding;
				issuedSecurityTokenParameters = InfoCardHelper.TryGetNextStsIssuedTokenParameters(issuedSecurityTokenParameters.IssuerBinding);
			}
			if (flag)
			{
				list.Add(new InfoCardHelper.PolicyElement(target2, null, null, privacyNoticeLink, privacyNoticeVersion, flag, null));
			}
			return list.ToArray();
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00042EC4 File Offset: 0x000410C4
		private static bool RequiresInfoCard(InfoCardHelper.PolicyElement[] chain, out Uri relyingPartyIssuer)
		{
			relyingPartyIssuer = null;
			if (chain.Length == 0)
			{
				return false;
			}
			int num = chain.Length - 1;
			int num2 = -1;
			bool flag = false;
			if (1 == chain.Length)
			{
				if (null == chain[num].Issuer || chain[num].Issuer.IsAnonymous || InfoCardHelper.SelfIssuerUri.Equals(chain[num].Issuer.Uri) || (null != chain[num].Issuer && chain[num].Binding == null))
				{
					num2 = num;
					flag = true;
				}
				else if (!chain[num].IsManagedIssuer)
				{
					flag = false;
				}
			}
			else
			{
				if (chain[num].IsManagedIssuer)
				{
					num2 = num - 1;
					flag = true;
				}
				else if (null == chain[num].Issuer || chain[num].Issuer.IsAnonymous || InfoCardHelper.SelfIssuerUri.Equals(chain[num].Issuer.Uri) || (null != chain[num].Issuer && chain[num].Binding == null))
				{
					num2 = num;
					flag = true;
				}
				else
				{
					flag = false;
				}
				for (int i = 0; i < num; i++)
				{
					if (chain[i].IsManagedIssuer || InfoCardHelper.SelfIssuerUri.Equals(chain[i].Issuer.Uri) || null == chain[i].Issuer || chain[i].Issuer.IsAnonymous)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InfoCardInvalidChain")));
					}
				}
			}
			if (flag)
			{
				relyingPartyIssuer = ((null == chain[num2].Issuer) ? null : chain[num2].Issuer.Uri);
			}
			return flag;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00043054 File Offset: 0x00041254
		private static SecurityTokenProvider CreateTokenProviderForNextLeg(SecurityTokenRequirement tokenRequirement, EndpointAddress target, EndpointAddress issuerAddress, Uri relyingPartyIssuer, ClientCredentialsSecurityTokenManager clientCredentialsTokenManager, InfoCardChannelParameter infocardChannelParameter)
		{
			if ((null == relyingPartyIssuer && null == issuerAddress) || issuerAddress.Uri == relyingPartyIssuer)
			{
				return new InfoCardHelper.InternalInfoCardTokenProvider(infocardChannelParameter);
			}
			IssuedSecurityTokenProvider issuedSecurityTokenProvider = (IssuedSecurityTokenProvider)clientCredentialsTokenManager.CreateSecurityTokenProvider(tokenRequirement, true);
			issuedSecurityTokenProvider.IssuerChannelBehaviors.Remove<SecurityCredentialsManager>();
			issuedSecurityTokenProvider.IssuerChannelBehaviors.Add(new InfoCardHelper.InternalClientCredentials(clientCredentialsTokenManager.ClientCredentials, target, relyingPartyIssuer, infocardChannelParameter));
			return issuedSecurityTokenProvider;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000430C0 File Offset: 0x000412C0
		public static MessageSecurityVersion GetBindingSecurityVersionOrDefault(Binding binding)
		{
			if (binding != null)
			{
				SecurityBindingElement securityBindingElement = binding.CreateBindingElements().Find<SecurityBindingElement>();
				if (securityBindingElement != null)
				{
					return securityBindingElement.MessageSecurityVersion;
				}
			}
			return MessageSecurityVersion.Default;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000430EC File Offset: 0x000412EC
		private static bool IsReferralToManagedIssuer(Binding issuerBinding)
		{
			bool result = false;
			if (issuerBinding != null)
			{
				UseManagedPresentationBindingElement useManagedPresentationBindingElement = issuerBinding.CreateBindingElements().Find<UseManagedPresentationBindingElement>();
				if (useManagedPresentationBindingElement != null)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00043110 File Offset: 0x00041310
		private static void GetPrivacyNoticeLinkFromIssuerBinding(Binding issuerBinding, out Uri privacyNotice, out int privacyNoticeVersion)
		{
			privacyNotice = null;
			privacyNoticeVersion = 0;
			if (issuerBinding != null)
			{
				PrivacyNoticeBindingElement privacyNoticeBindingElement = issuerBinding.CreateBindingElements().Find<PrivacyNoticeBindingElement>();
				if (privacyNoticeBindingElement != null)
				{
					privacyNotice = privacyNoticeBindingElement.Url;
					privacyNoticeVersion = privacyNoticeBindingElement.Version;
				}
			}
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00043148 File Offset: 0x00041348
		private static IssuedSecurityTokenParameters TryGetNextStsIssuedTokenParameters(Binding currentStsBinding)
		{
			if (currentStsBinding == null)
			{
				return null;
			}
			BindingElementCollection bindingElementCollection = currentStsBinding.CreateBindingElements();
			SecurityBindingElement securityBindingEle = bindingElementCollection.Find<SecurityBindingElement>();
			return InfoCardHelper.TryGetNextStsIssuedTokenParameters(securityBindingEle);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00043170 File Offset: 0x00041370
		private static IssuedSecurityTokenParameters TryGetNextStsIssuedTokenParameters(SecurityBindingElement securityBindingEle)
		{
			if (securityBindingEle == null)
			{
				return null;
			}
			InfoCardHelper.ThrowOnMultipleAssignment<IssuedSecurityTokenParameters> throwOnMultipleAssignment = new InfoCardHelper.ThrowOnMultipleAssignment<IssuedSecurityTokenParameters>(SR.GetString("TooManyIssuedSecurityTokenParameters"));
			InfoCardHelper.FindInfoCardIssuerBinding(securityBindingEle, throwOnMultipleAssignment);
			return throwOnMultipleAssignment.Value;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x000431A0 File Offset: 0x000413A0
		private static void FindInfoCardIssuerBinding(SecurityBindingElement secBindingElement, InfoCardHelper.ThrowOnMultipleAssignment<IssuedSecurityTokenParameters> issuedSecurityTokenParameters)
		{
			if (secBindingElement == null)
			{
				return;
			}
			InfoCardHelper.SecurityTokenParametersEnumerable securityTokenParametersEnumerable = new InfoCardHelper.SecurityTokenParametersEnumerable(secBindingElement);
			foreach (SecurityTokenParameters securityTokenParameters in securityTokenParametersEnumerable)
			{
				IssuedSecurityTokenParameters issuedSecurityTokenParameters2 = securityTokenParameters as IssuedSecurityTokenParameters;
				if (issuedSecurityTokenParameters2 != null && (issuedSecurityTokenParameters2.IssuerBinding == null || issuedSecurityTokenParameters2.IssuerAddress == null || issuedSecurityTokenParameters2.IssuerAddress.IsAnonymous || InfoCardHelper.SelfIssuerUri.Equals(issuedSecurityTokenParameters2.IssuerAddress) || InfoCardHelper.IsReferralToManagedIssuer(issuedSecurityTokenParameters2.IssuerBinding)))
				{
					if (issuedSecurityTokenParameters != null)
					{
						issuedSecurityTokenParameters.Value = issuedSecurityTokenParameters2;
					}
				}
				else if (securityTokenParameters is SecureConversationSecurityTokenParameters)
				{
					IssuedSecurityTokenParameters issuedSecurityTokenParameters3 = InfoCardHelper.TryGetNextStsIssuedTokenParameters(((SecureConversationSecurityTokenParameters)securityTokenParameters).BootstrapSecurityBindingElement);
					if (issuedSecurityTokenParameters3 != null && issuedSecurityTokenParameters != null)
					{
						issuedSecurityTokenParameters.Value = issuedSecurityTokenParameters3;
					}
				}
				else if (issuedSecurityTokenParameters2 != null && issuedSecurityTokenParameters2.IssuerBinding != null)
				{
					BindingElementCollection bindingElementCollection = issuedSecurityTokenParameters2.IssuerBinding.CreateBindingElements();
					SecurityBindingElement securityBindingEle = bindingElementCollection.Find<SecurityBindingElement>();
					IssuedSecurityTokenParameters issuedSecurityTokenParameters4 = InfoCardHelper.TryGetNextStsIssuedTokenParameters(securityBindingEle);
					if (issuedSecurityTokenParameters4 != null && issuedSecurityTokenParameters != null)
					{
						issuedSecurityTokenParameters.Value = issuedSecurityTokenParameters2;
					}
				}
			}
		}

		// Token: 0x040019CD RID: 6605
		private const string WSIdentityNamespace = "http://schemas.microsoft.com/ws/2005/05/identity";

		// Token: 0x040019CE RID: 6606
		private const string IsManagedElementName = "IsManaged";

		// Token: 0x040019CF RID: 6607
		private static Uri selfIssuerUri;

		// Token: 0x02000B18 RID: 2840
		private class ThrowOnMultipleAssignment<T>
		{
			// Token: 0x17001A00 RID: 6656
			// (get) Token: 0x06006F8B RID: 28555 RVA: 0x0019E1A4 File Offset: 0x0019C3A4
			// (set) Token: 0x06006F8C RID: 28556 RVA: 0x0019E1AC File Offset: 0x0019C3AC
			public T Value
			{
				get
				{
					return this.m_value;
				}
				set
				{
					if (this.m_value != null && value != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(this.m_errorString);
					}
					if (this.m_value == null)
					{
						this.m_value = value;
					}
				}
			}

			// Token: 0x06006F8D RID: 28557 RVA: 0x0019E1E8 File Offset: 0x0019C3E8
			public ThrowOnMultipleAssignment(string errorString)
			{
				this.m_errorString = errorString;
			}

			// Token: 0x04003FCD RID: 16333
			private string m_errorString;

			// Token: 0x04003FCE RID: 16334
			private T m_value;
		}

		// Token: 0x02000B19 RID: 2841
		private class InternalInfoCardTokenProvider : SecurityTokenProvider, IDisposable
		{
			// Token: 0x06006F8E RID: 28558 RVA: 0x0019E1F7 File Offset: 0x0019C3F7
			public InternalInfoCardTokenProvider(InfoCardChannelParameter infocardChannelParameter)
			{
				this.m_infocardChannelParameter = infocardChannelParameter;
			}

			// Token: 0x06006F8F RID: 28559 RVA: 0x0019E208 File Offset: 0x0019C408
			protected override SecurityToken GetTokenCore(TimeSpan timeout)
			{
				if (this.m_infocardChannelParameter == null || this.m_infocardChannelParameter.Token == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("NoTokenInChannelParameters")));
				}
				if (this.m_infocardChannelParameter.Token.ValidTo < DateTime.UtcNow)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ExpiredSecurityTokenException(SR.GetString("ExpiredTokenInChannelParameters")));
				}
				return this.m_infocardChannelParameter.Token;
			}

			// Token: 0x06006F90 RID: 28560 RVA: 0x0019E285 File Offset: 0x0019C485
			public void Dispose()
			{
			}

			// Token: 0x04003FCF RID: 16335
			private InfoCardChannelParameter m_infocardChannelParameter;
		}

		// Token: 0x02000B1A RID: 2842
		private class InternalClientCredentials : ClientCredentials
		{
			// Token: 0x06006F91 RID: 28561 RVA: 0x0019E287 File Offset: 0x0019C487
			public InternalClientCredentials(ClientCredentials infocardCredentials, EndpointAddress target, Uri relyingPartyIssuer, InfoCardChannelParameter infocardChannelParameter) : base(infocardCredentials)
			{
				this.m_relyingPartyIssuer = relyingPartyIssuer;
				this.m_clientCredentials = infocardCredentials;
				this.m_infocardChannelParameter = infocardChannelParameter;
			}

			// Token: 0x06006F92 RID: 28562 RVA: 0x0019E2A6 File Offset: 0x0019C4A6
			private InternalClientCredentials(InfoCardHelper.InternalClientCredentials other) : base(other)
			{
				this.m_relyingPartyIssuer = other.m_relyingPartyIssuer;
				this.m_clientCredentials = other.m_clientCredentials;
				this.m_infocardChannelParameter = other.InfoCardChannelParameter;
			}

			// Token: 0x17001A01 RID: 6657
			// (get) Token: 0x06006F93 RID: 28563 RVA: 0x0019E2D3 File Offset: 0x0019C4D3
			public InfoCardChannelParameter InfoCardChannelParameter
			{
				get
				{
					return this.m_infocardChannelParameter;
				}
			}

			// Token: 0x06006F94 RID: 28564 RVA: 0x0019E2DB File Offset: 0x0019C4DB
			public override SecurityTokenManager CreateSecurityTokenManager()
			{
				return new InfoCardHelper.InternalClientCredentials.InternalClientCredentialsSecurityTokenManager(this, this.m_infocardChannelParameter);
			}

			// Token: 0x06006F95 RID: 28565 RVA: 0x0019E2E9 File Offset: 0x0019C4E9
			public override void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
			{
			}

			// Token: 0x06006F96 RID: 28566 RVA: 0x0019E2EB File Offset: 0x0019C4EB
			protected override ClientCredentials CloneCore()
			{
				return new InfoCardHelper.InternalClientCredentials(this);
			}

			// Token: 0x04003FD0 RID: 16336
			private Uri m_relyingPartyIssuer;

			// Token: 0x04003FD1 RID: 16337
			private ClientCredentials m_clientCredentials;

			// Token: 0x04003FD2 RID: 16338
			private InfoCardChannelParameter m_infocardChannelParameter;

			// Token: 0x02000ED5 RID: 3797
			private class InternalClientCredentialsSecurityTokenManager : ClientCredentialsSecurityTokenManager
			{
				// Token: 0x0600848F RID: 33935 RVA: 0x001E9AF8 File Offset: 0x001E7CF8
				public InternalClientCredentialsSecurityTokenManager(InfoCardHelper.InternalClientCredentials internalClientCredentials, InfoCardChannelParameter infocardChannelParameter) : base(internalClientCredentials)
				{
					this.m_relyingPartyIssuer = internalClientCredentials.m_relyingPartyIssuer;
					this.m_infocardChannelParameter = infocardChannelParameter;
				}

				// Token: 0x06008490 RID: 33936 RVA: 0x001E9B14 File Offset: 0x001E7D14
				public override SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement tokenRequirement)
				{
					if (base.IsIssuedSecurityTokenRequirement(tokenRequirement))
					{
						EndpointAddress property = tokenRequirement.GetProperty<EndpointAddress>(ServiceModelSecurityTokenRequirement.TargetAddressProperty);
						IssuedSecurityTokenParameters property2 = tokenRequirement.GetProperty<IssuedSecurityTokenParameters>(ServiceModelSecurityTokenRequirement.IssuedSecurityTokenParametersProperty);
						return InfoCardHelper.CreateTokenProviderForNextLeg(tokenRequirement, property, property2.IssuerAddress, this.m_relyingPartyIssuer, this, this.m_infocardChannelParameter);
					}
					return base.CreateSecurityTokenProvider(tokenRequirement);
				}

				// Token: 0x04004CB8 RID: 19640
				private Uri m_relyingPartyIssuer;

				// Token: 0x04004CB9 RID: 19641
				private InfoCardChannelParameter m_infocardChannelParameter;
			}
		}

		// Token: 0x02000B1B RID: 2843
		private class PolicyElement
		{
			// Token: 0x17001A02 RID: 6658
			// (get) Token: 0x06006F97 RID: 28567 RVA: 0x0019E2F3 File Offset: 0x0019C4F3
			public bool IsManagedIssuer
			{
				get
				{
					return this.m_isManagedIssuer;
				}
			}

			// Token: 0x17001A03 RID: 6659
			// (get) Token: 0x06006F98 RID: 28568 RVA: 0x0019E2FB File Offset: 0x0019C4FB
			public EndpointAddress Issuer
			{
				get
				{
					return this.m_issuer;
				}
			}

			// Token: 0x17001A04 RID: 6660
			// (get) Token: 0x06006F99 RID: 28569 RVA: 0x0019E303 File Offset: 0x0019C503
			public Binding Binding
			{
				get
				{
					return this.m_binding;
				}
			}

			// Token: 0x06006F9A RID: 28570 RVA: 0x0019E30B File Offset: 0x0019C50B
			public PolicyElement(EndpointAddress target, EndpointAddress issuer, Collection<XmlElement> parameters, Uri privacyNoticeLink, int privacyNoticeVersion, bool isManagedIssuer, Binding binding)
			{
				this.m_target = target;
				this.m_issuer = issuer;
				this.m_parameters = parameters;
				this.m_policyNoticeLink = privacyNoticeLink;
				this.m_policyNoticeVersion = privacyNoticeVersion;
				this.m_isManagedIssuer = isManagedIssuer;
				this.m_binding = binding;
			}

			// Token: 0x06006F9B RID: 28571 RVA: 0x0019E348 File Offset: 0x0019C548
			public CardSpacePolicyElement ToCardSpacePolicyElement()
			{
				return new CardSpacePolicyElement(this.EndPointAddressToXmlElement(this.m_target), this.EndPointAddressToXmlElement(this.m_issuer), this.m_parameters, this.m_policyNoticeLink, this.m_policyNoticeVersion, this.m_isManagedIssuer);
			}

			// Token: 0x06006F9C RID: 28572 RVA: 0x0019E380 File Offset: 0x0019C580
			private XmlElement EndPointAddressToXmlElement(EndpointAddress epr)
			{
				if (null == epr)
				{
					return null;
				}
				XmlElement result;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (XmlWriter xmlWriter = new XmlTextWriter(memoryStream, Encoding.UTF8))
					{
						epr.WriteTo(AddressingVersion.WSAddressing10, xmlWriter);
						xmlWriter.Flush();
						memoryStream.Flush();
						memoryStream.Seek(0L, SeekOrigin.Begin);
						using (XmlReader xmlReader = XmlReader.Create(memoryStream))
						{
							XmlDocument xmlDocument = new XmlDocument();
							result = (XmlElement)xmlDocument.ReadNode(xmlReader);
						}
					}
				}
				return result;
			}

			// Token: 0x04003FD3 RID: 16339
			private EndpointAddress m_target;

			// Token: 0x04003FD4 RID: 16340
			private EndpointAddress m_issuer;

			// Token: 0x04003FD5 RID: 16341
			private Collection<XmlElement> m_parameters;

			// Token: 0x04003FD6 RID: 16342
			private Uri m_policyNoticeLink;

			// Token: 0x04003FD7 RID: 16343
			private int m_policyNoticeVersion;

			// Token: 0x04003FD8 RID: 16344
			private bool m_isManagedIssuer;

			// Token: 0x04003FD9 RID: 16345
			private Binding m_binding;
		}

		// Token: 0x02000B1C RID: 2844
		private class SecurityTokenParametersEnumerable : IEnumerable<SecurityTokenParameters>, IEnumerable
		{
			// Token: 0x06006F9D RID: 28573 RVA: 0x0019E434 File Offset: 0x0019C634
			public SecurityTokenParametersEnumerable(SecurityBindingElement sbe)
			{
				if (sbe == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sbe");
				}
				this.sbe = sbe;
			}

			// Token: 0x06006F9E RID: 28574 RVA: 0x0019E456 File Offset: 0x0019C656
			public IEnumerator<SecurityTokenParameters> GetEnumerator()
			{
				foreach (SecurityTokenParameters securityTokenParameters in this.sbe.EndpointSupportingTokenParameters.Endorsing)
				{
					if (securityTokenParameters != null)
					{
						yield return securityTokenParameters;
					}
				}
				IEnumerator<SecurityTokenParameters> enumerator = null;
				foreach (SecurityTokenParameters securityTokenParameters2 in this.sbe.EndpointSupportingTokenParameters.SignedEndorsing)
				{
					if (securityTokenParameters2 != null)
					{
						yield return securityTokenParameters2;
					}
				}
				enumerator = null;
				foreach (SupportingTokenParameters str in this.sbe.OperationSupportingTokenParameters.Values)
				{
					if (str != null)
					{
						foreach (SecurityTokenParameters securityTokenParameters3 in str.Endorsing)
						{
							if (securityTokenParameters3 != null)
							{
								yield return securityTokenParameters3;
							}
						}
						enumerator = null;
						foreach (SecurityTokenParameters securityTokenParameters4 in str.SignedEndorsing)
						{
							if (securityTokenParameters4 != null)
							{
								yield return securityTokenParameters4;
							}
						}
						enumerator = null;
					}
					str = null;
				}
				IEnumerator<SupportingTokenParameters> enumerator2 = null;
				if (this.sbe is SymmetricSecurityBindingElement)
				{
					SymmetricSecurityBindingElement symmetricSecurityBindingElement = (SymmetricSecurityBindingElement)this.sbe;
					if (symmetricSecurityBindingElement.ProtectionTokenParameters != null)
					{
						yield return symmetricSecurityBindingElement.ProtectionTokenParameters;
					}
				}
				else if (this.sbe is AsymmetricSecurityBindingElement)
				{
					AsymmetricSecurityBindingElement asymmetricSecurityBindingElement = (AsymmetricSecurityBindingElement)this.sbe;
					if (asymmetricSecurityBindingElement.RecipientTokenParameters != null)
					{
						yield return asymmetricSecurityBindingElement.RecipientTokenParameters;
					}
				}
				yield break;
				yield break;
			}

			// Token: 0x06006F9F RID: 28575 RVA: 0x0019E465 File Offset: 0x0019C665
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}

			// Token: 0x04003FDA RID: 16346
			private SecurityBindingElement sbe;
		}
	}
}
