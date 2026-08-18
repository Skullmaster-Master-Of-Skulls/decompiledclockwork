using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net.Security;
using System.Runtime;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000990 RID: 2448
	[__DynamicallyInvokable]
	public abstract class SecurityBindingElement : BindingElement
	{
		// Token: 0x06005EF5 RID: 24309 RVA: 0x0016026C File Offset: 0x0015E46C
		internal SecurityBindingElement()
		{
			this.messageSecurityVersion = MessageSecurityVersion.Default;
			this.keyEntropyMode = SecurityKeyEntropyMode.CombinedEntropy;
			this.includeTimestamp = true;
			this.defaultAlgorithmSuite = SecurityBindingElement.defaultDefaultAlgorithmSuite;
			this.localClientSettings = new LocalClientSecuritySettings();
			this.localServiceSettings = new LocalServiceSecuritySettings();
			this.endpointSupportingTokenParameters = new SupportingTokenParameters();
			this.optionalEndpointSupportingTokenParameters = new SupportingTokenParameters();
			this.operationSupportingTokenParameters = new Dictionary<string, SupportingTokenParameters>();
			this.optionalOperationSupportingTokenParameters = new Dictionary<string, SupportingTokenParameters>();
			this.securityHeaderLayout = SecurityHeaderLayout.Strict;
			this.allowInsecureTransport = false;
			this.enableUnsecuredResponse = false;
			this.protectTokens = false;
		}

		// Token: 0x06005EF6 RID: 24310 RVA: 0x00160310 File Offset: 0x0015E510
		internal SecurityBindingElement(SecurityBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			if (elementToBeCloned == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("elementToBeCloned");
			}
			this.defaultAlgorithmSuite = elementToBeCloned.defaultAlgorithmSuite;
			this.includeTimestamp = elementToBeCloned.includeTimestamp;
			this.keyEntropyMode = elementToBeCloned.keyEntropyMode;
			this.messageSecurityVersion = elementToBeCloned.messageSecurityVersion;
			this.securityHeaderLayout = elementToBeCloned.securityHeaderLayout;
			this.endpointSupportingTokenParameters = elementToBeCloned.endpointSupportingTokenParameters.Clone();
			this.optionalEndpointSupportingTokenParameters = elementToBeCloned.optionalEndpointSupportingTokenParameters.Clone();
			this.operationSupportingTokenParameters = new Dictionary<string, SupportingTokenParameters>();
			foreach (string key in elementToBeCloned.operationSupportingTokenParameters.Keys)
			{
				this.operationSupportingTokenParameters[key] = elementToBeCloned.operationSupportingTokenParameters[key].Clone();
			}
			this.optionalOperationSupportingTokenParameters = new Dictionary<string, SupportingTokenParameters>();
			foreach (string key2 in elementToBeCloned.optionalOperationSupportingTokenParameters.Keys)
			{
				this.optionalOperationSupportingTokenParameters[key2] = elementToBeCloned.optionalOperationSupportingTokenParameters[key2].Clone();
			}
			this.localClientSettings = elementToBeCloned.localClientSettings.Clone();
			this.localServiceSettings = elementToBeCloned.localServiceSettings.Clone();
			this.internalDuplexBindingElement = elementToBeCloned.internalDuplexBindingElement;
			this.maxReceivedMessageSize = elementToBeCloned.maxReceivedMessageSize;
			this.readerQuotas = elementToBeCloned.readerQuotas;
			this.doNotEmitTrust = elementToBeCloned.doNotEmitTrust;
			this.allowInsecureTransport = elementToBeCloned.allowInsecureTransport;
			this.enableUnsecuredResponse = elementToBeCloned.enableUnsecuredResponse;
			this.supportsExtendedProtectionPolicy = elementToBeCloned.supportsExtendedProtectionPolicy;
			this.protectTokens = elementToBeCloned.protectTokens;
		}

		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x06005EF7 RID: 24311 RVA: 0x001604F8 File Offset: 0x0015E6F8
		// (set) Token: 0x06005EF8 RID: 24312 RVA: 0x00160500 File Offset: 0x0015E700
		internal bool SupportsExtendedProtectionPolicy
		{
			get
			{
				return this.supportsExtendedProtectionPolicy;
			}
			set
			{
				this.supportsExtendedProtectionPolicy = value;
			}
		}

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x06005EF9 RID: 24313 RVA: 0x00160509 File Offset: 0x0015E709
		[__DynamicallyInvokable]
		public SupportingTokenParameters EndpointSupportingTokenParameters
		{
			[__DynamicallyInvokable]
			get
			{
				return this.endpointSupportingTokenParameters;
			}
		}

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x06005EFA RID: 24314 RVA: 0x00160511 File Offset: 0x0015E711
		public SupportingTokenParameters OptionalEndpointSupportingTokenParameters
		{
			get
			{
				return this.optionalEndpointSupportingTokenParameters;
			}
		}

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x06005EFB RID: 24315 RVA: 0x00160519 File Offset: 0x0015E719
		public IDictionary<string, SupportingTokenParameters> OperationSupportingTokenParameters
		{
			get
			{
				return this.operationSupportingTokenParameters;
			}
		}

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x06005EFC RID: 24316 RVA: 0x00160521 File Offset: 0x0015E721
		public IDictionary<string, SupportingTokenParameters> OptionalOperationSupportingTokenParameters
		{
			get
			{
				return this.optionalOperationSupportingTokenParameters;
			}
		}

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x06005EFD RID: 24317 RVA: 0x00160529 File Offset: 0x0015E729
		// (set) Token: 0x06005EFE RID: 24318 RVA: 0x00160531 File Offset: 0x0015E731
		[__DynamicallyInvokable]
		public SecurityHeaderLayout SecurityHeaderLayout
		{
			[__DynamicallyInvokable]
			get
			{
				return this.securityHeaderLayout;
			}
			[__DynamicallyInvokable]
			set
			{
				if (!SecurityHeaderLayoutHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.securityHeaderLayout = value;
			}
		}

		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x06005EFF RID: 24319 RVA: 0x00160557 File Offset: 0x0015E757
		// (set) Token: 0x06005F00 RID: 24320 RVA: 0x0016055F File Offset: 0x0015E75F
		[__DynamicallyInvokable]
		public MessageSecurityVersion MessageSecurityVersion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.messageSecurityVersion;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.messageSecurityVersion = value;
			}
		}

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x06005F01 RID: 24321 RVA: 0x00160580 File Offset: 0x0015E780
		// (set) Token: 0x06005F02 RID: 24322 RVA: 0x00160588 File Offset: 0x0015E788
		public bool EnableUnsecuredResponse
		{
			get
			{
				return this.enableUnsecuredResponse;
			}
			set
			{
				this.enableUnsecuredResponse = value;
			}
		}

		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x06005F03 RID: 24323 RVA: 0x00160591 File Offset: 0x0015E791
		// (set) Token: 0x06005F04 RID: 24324 RVA: 0x00160599 File Offset: 0x0015E799
		[__DynamicallyInvokable]
		public bool IncludeTimestamp
		{
			[__DynamicallyInvokable]
			get
			{
				return this.includeTimestamp;
			}
			[__DynamicallyInvokable]
			set
			{
				this.includeTimestamp = value;
			}
		}

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x06005F05 RID: 24325 RVA: 0x001605A2 File Offset: 0x0015E7A2
		// (set) Token: 0x06005F06 RID: 24326 RVA: 0x001605AA File Offset: 0x0015E7AA
		public bool AllowInsecureTransport
		{
			get
			{
				return this.allowInsecureTransport;
			}
			set
			{
				this.allowInsecureTransport = value;
			}
		}

		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x06005F07 RID: 24327 RVA: 0x001605B3 File Offset: 0x0015E7B3
		// (set) Token: 0x06005F08 RID: 24328 RVA: 0x001605BB File Offset: 0x0015E7BB
		public SecurityAlgorithmSuite DefaultAlgorithmSuite
		{
			get
			{
				return this.defaultAlgorithmSuite;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.defaultAlgorithmSuite = value;
			}
		}

		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x06005F09 RID: 24329 RVA: 0x001605DC File Offset: 0x0015E7DC
		// (set) Token: 0x06005F0A RID: 24330 RVA: 0x001605E4 File Offset: 0x0015E7E4
		public bool ProtectTokens
		{
			get
			{
				return this.protectTokens;
			}
			set
			{
				this.protectTokens = value;
			}
		}

		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x06005F0B RID: 24331 RVA: 0x001605ED File Offset: 0x0015E7ED
		[__DynamicallyInvokable]
		public LocalClientSecuritySettings LocalClientSettings
		{
			[__DynamicallyInvokable]
			get
			{
				return this.localClientSettings;
			}
		}

		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x06005F0C RID: 24332 RVA: 0x001605F5 File Offset: 0x0015E7F5
		public LocalServiceSecuritySettings LocalServiceSettings
		{
			get
			{
				return this.localServiceSettings;
			}
		}

		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x06005F0D RID: 24333 RVA: 0x001605FD File Offset: 0x0015E7FD
		// (set) Token: 0x06005F0E RID: 24334 RVA: 0x00160605 File Offset: 0x0015E805
		public SecurityKeyEntropyMode KeyEntropyMode
		{
			get
			{
				return this.keyEntropyMode;
			}
			set
			{
				if (!SecurityKeyEntropyModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.keyEntropyMode = value;
			}
		}

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x06005F0F RID: 24335 RVA: 0x0016062B File Offset: 0x0015E82B
		internal virtual bool SessionMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x06005F10 RID: 24336 RVA: 0x0016062E File Offset: 0x0015E82E
		internal virtual bool SupportsDuplex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x06005F11 RID: 24337 RVA: 0x00160631 File Offset: 0x0015E831
		internal virtual bool SupportsRequestReply
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x06005F12 RID: 24338 RVA: 0x00160634 File Offset: 0x0015E834
		// (set) Token: 0x06005F13 RID: 24339 RVA: 0x0016063C File Offset: 0x0015E83C
		internal long MaxReceivedMessageSize
		{
			get
			{
				return this.maxReceivedMessageSize;
			}
			set
			{
				this.maxReceivedMessageSize = value;
			}
		}

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x06005F14 RID: 24340 RVA: 0x00160645 File Offset: 0x0015E845
		// (set) Token: 0x06005F15 RID: 24341 RVA: 0x0016064D File Offset: 0x0015E84D
		internal bool DoNotEmitTrust
		{
			get
			{
				return this.doNotEmitTrust;
			}
			set
			{
				this.doNotEmitTrust = value;
			}
		}

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x06005F16 RID: 24342 RVA: 0x00160656 File Offset: 0x0015E856
		// (set) Token: 0x06005F17 RID: 24343 RVA: 0x0016065E File Offset: 0x0015E85E
		internal XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.readerQuotas;
			}
			set
			{
				this.readerQuotas = value;
			}
		}

		// Token: 0x06005F18 RID: 24344 RVA: 0x00160668 File Offset: 0x0015E868
		private void GetSupportingTokensCapabilities(ICollection<SecurityTokenParameters> parameters, out bool supportsClientAuth, out bool supportsWindowsIdentity)
		{
			supportsClientAuth = false;
			supportsWindowsIdentity = false;
			foreach (SecurityTokenParameters securityTokenParameters in parameters)
			{
				if (securityTokenParameters.SupportsClientAuthentication)
				{
					supportsClientAuth = true;
				}
				if (securityTokenParameters.SupportsClientWindowsIdentity)
				{
					supportsWindowsIdentity = true;
				}
			}
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x001606C8 File Offset: 0x0015E8C8
		private void GetSupportingTokensCapabilities(SupportingTokenParameters requirements, out bool supportsClientAuth, out bool supportsWindowsIdentity)
		{
			supportsClientAuth = false;
			supportsWindowsIdentity = false;
			bool flag;
			bool flag2;
			this.GetSupportingTokensCapabilities(requirements.Endorsing, out flag, out flag2);
			supportsClientAuth = (supportsClientAuth || flag);
			supportsWindowsIdentity = (supportsWindowsIdentity || flag2);
			this.GetSupportingTokensCapabilities(requirements.SignedEndorsing, out flag, out flag2);
			supportsClientAuth = (supportsClientAuth || flag);
			supportsWindowsIdentity = (supportsWindowsIdentity || flag2);
			this.GetSupportingTokensCapabilities(requirements.SignedEncrypted, out flag, out flag2);
			supportsClientAuth = (supportsClientAuth || flag);
			supportsWindowsIdentity = (supportsWindowsIdentity || flag2);
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x0016072F File Offset: 0x0015E92F
		internal void GetSupportingTokensCapabilities(out bool supportsClientAuth, out bool supportsWindowsIdentity)
		{
			this.GetSupportingTokensCapabilities(this.EndpointSupportingTokenParameters, out supportsClientAuth, out supportsWindowsIdentity);
		}

		// Token: 0x06005F1B RID: 24347 RVA: 0x00160740 File Offset: 0x0015E940
		internal void AddDemuxerForSecureConversation(ChannelBuilder builder, BindingContext secureConversationBindingContext)
		{
			int num = 0;
			bool flag = false;
			for (int i = 0; i < builder.Binding.Elements.Count; i++)
			{
				if (!(builder.Binding.Elements[i] is MessageEncodingBindingElement) && !(builder.Binding.Elements[i] is StreamUpgradeBindingElement))
				{
					if (builder.Binding.Elements[i] is ChannelDemuxerBindingElement)
					{
						num++;
					}
					else
					{
						if (builder.Binding.Elements[i] is TransportBindingElement)
						{
							break;
						}
						flag = true;
					}
				}
			}
			if (num == 1 && !flag)
			{
				return;
			}
			ChannelDemuxerBindingElement channelDemuxerBindingElement = new ChannelDemuxerBindingElement(false);
			channelDemuxerBindingElement.MaxPendingSessions = this.LocalServiceSettings.MaxPendingSessions;
			channelDemuxerBindingElement.PeekTimeout = this.LocalServiceSettings.NegotiationTimeout;
			builder.Binding.Elements.Insert(0, channelDemuxerBindingElement);
			secureConversationBindingContext.RemainingBindingElements.Insert(0, channelDemuxerBindingElement);
		}

		// Token: 0x06005F1C RID: 24348 RVA: 0x00160824 File Offset: 0x0015EA24
		internal void ApplyPropertiesOnDemuxer(ChannelBuilder builder, BindingContext context)
		{
			Collection<ChannelDemuxerBindingElement> collection = builder.Binding.Elements.FindAll<ChannelDemuxerBindingElement>();
			foreach (ChannelDemuxerBindingElement channelDemuxerBindingElement in collection)
			{
				if (channelDemuxerBindingElement != null)
				{
					channelDemuxerBindingElement.MaxPendingSessions = this.LocalServiceSettings.MaxPendingSessions;
					channelDemuxerBindingElement.PeekTimeout = this.LocalServiceSettings.NegotiationTimeout;
				}
			}
		}

		// Token: 0x06005F1D RID: 24349 RVA: 0x0016089C File Offset: 0x0015EA9C
		private static BindingContext CreateIssuerBindingContextForNegotiation(BindingContext issuerBindingContext)
		{
			TransportBindingElement transportBindingElement = issuerBindingContext.RemainingBindingElements.Find<TransportBindingElement>();
			if (transportBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TransportBindingElementNotFound")));
			}
			ChannelDemuxerBindingElement channelDemuxerBindingElement = null;
			for (int i = 0; i < issuerBindingContext.RemainingBindingElements.Count; i++)
			{
				if (issuerBindingContext.RemainingBindingElements[i] is ChannelDemuxerBindingElement)
				{
					channelDemuxerBindingElement = (ChannelDemuxerBindingElement)issuerBindingContext.RemainingBindingElements[i];
				}
			}
			if (channelDemuxerBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ChannelDemuxerBindingElementNotFound")));
			}
			CustomBinding customBinding = new CustomBinding(new BindingElementCollection
			{
				channelDemuxerBindingElement.Clone(),
				transportBindingElement.Clone()
			});
			customBinding.OpenTimeout = issuerBindingContext.Binding.OpenTimeout;
			customBinding.CloseTimeout = issuerBindingContext.Binding.CloseTimeout;
			customBinding.SendTimeout = issuerBindingContext.Binding.SendTimeout;
			customBinding.ReceiveTimeout = issuerBindingContext.Binding.ReceiveTimeout;
			if (issuerBindingContext.ListenUriBaseAddress != null)
			{
				return new BindingContext(customBinding, new BindingParameterCollection(issuerBindingContext.BindingParameters), issuerBindingContext.ListenUriBaseAddress, issuerBindingContext.ListenUriRelativeAddress, issuerBindingContext.ListenUriMode);
			}
			return new BindingContext(customBinding, new BindingParameterCollection(issuerBindingContext.BindingParameters));
		}

		// Token: 0x06005F1E RID: 24350 RVA: 0x001609DE File Offset: 0x0015EBDE
		protected static void SetIssuerBindingContextIfRequired(SecurityTokenParameters parameters, BindingContext issuerBindingContext)
		{
			if (parameters is SslSecurityTokenParameters)
			{
				((SslSecurityTokenParameters)parameters).IssuerBindingContext = SecurityBindingElement.CreateIssuerBindingContextForNegotiation(issuerBindingContext);
				return;
			}
			if (parameters is SspiSecurityTokenParameters)
			{
				((SspiSecurityTokenParameters)parameters).IssuerBindingContext = SecurityBindingElement.CreateIssuerBindingContextForNegotiation(issuerBindingContext);
			}
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x00160A14 File Offset: 0x0015EC14
		private static void SetIssuerBindingContextIfRequired(SupportingTokenParameters supportingParameters, BindingContext issuerBindingContext)
		{
			for (int i = 0; i < supportingParameters.Endorsing.Count; i++)
			{
				SecurityBindingElement.SetIssuerBindingContextIfRequired(supportingParameters.Endorsing[i], issuerBindingContext);
			}
			for (int j = 0; j < supportingParameters.SignedEndorsing.Count; j++)
			{
				SecurityBindingElement.SetIssuerBindingContextIfRequired(supportingParameters.SignedEndorsing[j], issuerBindingContext);
			}
			for (int k = 0; k < supportingParameters.Signed.Count; k++)
			{
				SecurityBindingElement.SetIssuerBindingContextIfRequired(supportingParameters.Signed[k], issuerBindingContext);
			}
			for (int l = 0; l < supportingParameters.SignedEncrypted.Count; l++)
			{
				SecurityBindingElement.SetIssuerBindingContextIfRequired(supportingParameters.SignedEncrypted[l], issuerBindingContext);
			}
		}

		// Token: 0x06005F20 RID: 24352 RVA: 0x00160AC4 File Offset: 0x0015ECC4
		private void SetIssuerBindingContextIfRequired(BindingContext issuerBindingContext)
		{
			SecurityBindingElement.SetIssuerBindingContextIfRequired(this.EndpointSupportingTokenParameters, issuerBindingContext);
			SecurityBindingElement.SetIssuerBindingContextIfRequired(this.OptionalEndpointSupportingTokenParameters, issuerBindingContext);
			foreach (SupportingTokenParameters supportingParameters in this.OperationSupportingTokenParameters.Values)
			{
				SecurityBindingElement.SetIssuerBindingContextIfRequired(supportingParameters, issuerBindingContext);
			}
			foreach (SupportingTokenParameters supportingParameters2 in this.OptionalOperationSupportingTokenParameters.Values)
			{
				SecurityBindingElement.SetIssuerBindingContextIfRequired(supportingParameters2, issuerBindingContext);
			}
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x00160B70 File Offset: 0x0015ED70
		internal bool RequiresChannelDemuxer(SecurityTokenParameters parameters)
		{
			return parameters is SecureConversationSecurityTokenParameters || parameters is SslSecurityTokenParameters || parameters is SspiSecurityTokenParameters;
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x00160B90 File Offset: 0x0015ED90
		internal virtual bool RequiresChannelDemuxer()
		{
			foreach (SecurityTokenParameters parameters in this.EndpointSupportingTokenParameters.Endorsing)
			{
				if (this.RequiresChannelDemuxer(parameters))
				{
					return true;
				}
			}
			foreach (SecurityTokenParameters parameters2 in this.EndpointSupportingTokenParameters.SignedEndorsing)
			{
				if (this.RequiresChannelDemuxer(parameters2))
				{
					return true;
				}
			}
			foreach (SecurityTokenParameters parameters3 in this.OptionalEndpointSupportingTokenParameters.Endorsing)
			{
				if (this.RequiresChannelDemuxer(parameters3))
				{
					return true;
				}
			}
			foreach (SecurityTokenParameters parameters4 in this.OptionalEndpointSupportingTokenParameters.SignedEndorsing)
			{
				if (this.RequiresChannelDemuxer(parameters4))
				{
					return true;
				}
			}
			foreach (SupportingTokenParameters supportingTokenParameters in this.OperationSupportingTokenParameters.Values)
			{
				foreach (SecurityTokenParameters parameters5 in supportingTokenParameters.Endorsing)
				{
					if (this.RequiresChannelDemuxer(parameters5))
					{
						return true;
					}
				}
				foreach (SecurityTokenParameters parameters6 in supportingTokenParameters.SignedEndorsing)
				{
					if (this.RequiresChannelDemuxer(parameters6))
					{
						return true;
					}
				}
			}
			foreach (SupportingTokenParameters supportingTokenParameters2 in this.OptionalOperationSupportingTokenParameters.Values)
			{
				foreach (SecurityTokenParameters parameters7 in supportingTokenParameters2.Endorsing)
				{
					if (this.RequiresChannelDemuxer(parameters7))
					{
						return true;
					}
				}
				foreach (SecurityTokenParameters parameters8 in supportingTokenParameters2.SignedEndorsing)
				{
					if (this.RequiresChannelDemuxer(parameters8))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005F23 RID: 24355 RVA: 0x00160E98 File Offset: 0x0015F098
		internal bool IsUnderlyingListenerDuplex<TChannel>(BindingContext context)
		{
			return typeof(TChannel) == typeof(IDuplexSessionChannel) && context.CanBuildInnerChannelListener<IDuplexChannel>() && !context.CanBuildInnerChannelListener<IDuplexSessionChannel>();
		}

		// Token: 0x06005F24 RID: 24356 RVA: 0x00160EC8 File Offset: 0x0015F0C8
		private void SetPrivacyNoticeUriIfRequired(SecurityProtocolFactory factory, Binding binding)
		{
			PrivacyNoticeBindingElement privacyNoticeBindingElement = binding.CreateBindingElements().Find<PrivacyNoticeBindingElement>();
			if (privacyNoticeBindingElement != null)
			{
				factory.PrivacyNoticeUri = privacyNoticeBindingElement.Url;
				factory.PrivacyNoticeVersion = privacyNoticeBindingElement.Version;
			}
		}

		// Token: 0x06005F25 RID: 24357 RVA: 0x00160EFC File Offset: 0x0015F0FC
		internal void ConfigureProtocolFactory(SecurityProtocolFactory factory, SecurityCredentialsManager credentialsManager, bool isForService, BindingContext issuerBindingContext, Binding binding)
		{
			if (factory == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("factory"));
			}
			if (credentialsManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("credentialsManager"));
			}
			factory.AddTimestamp = this.IncludeTimestamp;
			factory.IncomingAlgorithmSuite = this.DefaultAlgorithmSuite;
			factory.OutgoingAlgorithmSuite = this.DefaultAlgorithmSuite;
			factory.SecurityHeaderLayout = this.SecurityHeaderLayout;
			if (!isForService)
			{
				factory.TimestampValidityDuration = this.LocalClientSettings.TimestampValidityDuration;
				factory.DetectReplays = this.LocalClientSettings.DetectReplays;
				factory.MaxCachedNonces = this.LocalClientSettings.ReplayCacheSize;
				factory.MaxClockSkew = this.LocalClientSettings.MaxClockSkew;
				factory.ReplayWindow = this.LocalClientSettings.ReplayWindow;
				if (this.LocalClientSettings.DetectReplays)
				{
					factory.NonceCache = this.LocalClientSettings.NonceCache;
				}
			}
			else
			{
				factory.TimestampValidityDuration = this.LocalServiceSettings.TimestampValidityDuration;
				factory.DetectReplays = this.LocalServiceSettings.DetectReplays;
				factory.MaxCachedNonces = this.LocalServiceSettings.ReplayCacheSize;
				factory.MaxClockSkew = this.LocalServiceSettings.MaxClockSkew;
				factory.ReplayWindow = this.LocalServiceSettings.ReplayWindow;
				if (this.LocalServiceSettings.DetectReplays)
				{
					factory.NonceCache = this.LocalServiceSettings.NonceCache;
				}
			}
			factory.SecurityBindingElement = (SecurityBindingElement)this.Clone();
			factory.SecurityBindingElement.SetIssuerBindingContextIfRequired(issuerBindingContext);
			factory.SecurityTokenManager = credentialsManager.CreateSecurityTokenManager();
			SecurityTokenSerializer tokenSerializer = factory.SecurityTokenManager.CreateSecurityTokenSerializer(this.messageSecurityVersion.SecurityTokenVersion);
			factory.StandardsManager = new SecurityStandardsManager(this.messageSecurityVersion, tokenSerializer);
			if (!isForService)
			{
				this.SetPrivacyNoticeUriIfRequired(factory, binding);
			}
		}

		// Token: 0x06005F26 RID: 24358
		internal abstract SecurityProtocolFactory CreateSecurityProtocolFactory<TChannel>(BindingContext context, SecurityCredentialsManager credentialsManager, bool isForService, BindingContext issuanceBindingContext);

		// Token: 0x06005F27 RID: 24359 RVA: 0x001610B8 File Offset: 0x0015F2B8
		[__DynamicallyInvokable]
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (!this.CanBuildChannelFactory<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}), "TChannel"));
			}
			this.readerQuotas = context.GetInnerProperty<XmlDictionaryReaderQuotas>();
			if (this.readerQuotas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EncodingBindingElementDoesNotHandleReaderQuotas")));
			}
			TransportBindingElement transportBindingElement = null;
			if (context.RemainingBindingElements != null)
			{
				transportBindingElement = context.RemainingBindingElements.Find<TransportBindingElement>();
			}
			if (transportBindingElement != null)
			{
				this.maxReceivedMessageSize = transportBindingElement.MaxReceivedMessageSize;
			}
			IChannelFactory<TChannel> channelFactory = this.BuildChannelFactoryCore<TChannel>(context);
			if (transportBindingElement != null)
			{
				SecurityChannelFactory<TChannel> securityChannelFactory = channelFactory as SecurityChannelFactory<TChannel>;
				if (securityChannelFactory != null && securityChannelFactory.SecurityProtocolFactory != null)
				{
					securityChannelFactory.SecurityProtocolFactory.ExtendedProtectionPolicy = transportBindingElement.GetProperty<ExtendedProtectionPolicy>(context);
				}
			}
			return channelFactory;
		}

		// Token: 0x06005F28 RID: 24360
		[__DynamicallyInvokable]
		protected abstract IChannelFactory<TChannel> BuildChannelFactoryCore<TChannel>(BindingContext context);

		// Token: 0x06005F29 RID: 24361 RVA: 0x00161198 File Offset: 0x0015F398
		[__DynamicallyInvokable]
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			InternalDuplexBindingElement.AddDuplexFactorySupport(context, ref this.internalDuplexBindingElement);
			if (this.SessionMode)
			{
				return this.CanBuildSessionChannelFactory<TChannel>(context);
			}
			return context.CanBuildInnerChannelFactory<TChannel>() && (typeof(TChannel) == typeof(IOutputChannel) || typeof(TChannel) == typeof(IOutputSessionChannel) || (this.SupportsDuplex && (typeof(TChannel) == typeof(IDuplexChannel) || typeof(TChannel) == typeof(IDuplexSessionChannel))) || (this.SupportsRequestReply && (typeof(TChannel) == typeof(IRequestChannel) || typeof(TChannel) == typeof(IRequestSessionChannel))));
		}

		// Token: 0x06005F2A RID: 24362 RVA: 0x00161298 File Offset: 0x0015F498
		private bool CanBuildSessionChannelFactory<TChannel>(BindingContext context)
		{
			if (!context.CanBuildInnerChannelFactory<IRequestChannel>() && !context.CanBuildInnerChannelFactory<IRequestSessionChannel>() && !context.CanBuildInnerChannelFactory<IDuplexChannel>() && !context.CanBuildInnerChannelFactory<IDuplexSessionChannel>())
			{
				return false;
			}
			if (typeof(TChannel) == typeof(IRequestSessionChannel))
			{
				return context.CanBuildInnerChannelFactory<IRequestChannel>() || context.CanBuildInnerChannelFactory<IRequestSessionChannel>();
			}
			return typeof(TChannel) == typeof(IDuplexSessionChannel) && (context.CanBuildInnerChannelFactory<IDuplexChannel>() || context.CanBuildInnerChannelFactory<IDuplexSessionChannel>());
		}

		// Token: 0x06005F2B RID: 24363 RVA: 0x00161320 File Offset: 0x0015F520
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (!this.CanBuildChannelListener<TChannel>(context))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ChannelTypeNotSupported", new object[]
				{
					typeof(TChannel)
				}), "TChannel"));
			}
			this.readerQuotas = context.GetInnerProperty<XmlDictionaryReaderQuotas>();
			if (this.readerQuotas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EncodingBindingElementDoesNotHandleReaderQuotas")));
			}
			TransportBindingElement transportBindingElement = null;
			if (context.RemainingBindingElements != null)
			{
				transportBindingElement = context.RemainingBindingElements.Find<TransportBindingElement>();
			}
			if (transportBindingElement != null)
			{
				this.maxReceivedMessageSize = transportBindingElement.MaxReceivedMessageSize;
			}
			return this.BuildChannelListenerCore<TChannel>(context);
		}

		// Token: 0x06005F2C RID: 24364
		protected abstract IChannelListener<TChannel> BuildChannelListenerCore<TChannel>(BindingContext context) where TChannel : class, IChannel;

		// Token: 0x06005F2D RID: 24365 RVA: 0x001613D8 File Offset: 0x0015F5D8
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			InternalDuplexBindingElement.AddDuplexListenerSupport(context, ref this.internalDuplexBindingElement);
			if (this.SessionMode)
			{
				return this.CanBuildSessionChannelListener<TChannel>(context);
			}
			return context.CanBuildInnerChannelListener<TChannel>() && (typeof(TChannel) == typeof(IInputChannel) || typeof(TChannel) == typeof(IInputSessionChannel) || (this.SupportsDuplex && (typeof(TChannel) == typeof(IDuplexChannel) || typeof(TChannel) == typeof(IDuplexSessionChannel))) || (this.SupportsRequestReply && (typeof(TChannel) == typeof(IReplyChannel) || typeof(TChannel) == typeof(IReplySessionChannel))));
		}

		// Token: 0x06005F2E RID: 24366 RVA: 0x001614D8 File Offset: 0x0015F6D8
		private bool CanBuildSessionChannelListener<TChannel>(BindingContext context) where TChannel : class, IChannel
		{
			if (!context.CanBuildInnerChannelListener<IReplyChannel>() && !context.CanBuildInnerChannelListener<IReplySessionChannel>() && !context.CanBuildInnerChannelListener<IDuplexChannel>() && !context.CanBuildInnerChannelListener<IDuplexSessionChannel>())
			{
				return false;
			}
			if (typeof(TChannel) == typeof(IReplySessionChannel))
			{
				return context.CanBuildInnerChannelListener<IReplyChannel>() || context.CanBuildInnerChannelListener<IReplySessionChannel>();
			}
			return typeof(TChannel) == typeof(IDuplexSessionChannel) && (context.CanBuildInnerChannelListener<IDuplexChannel>() || context.CanBuildInnerChannelListener<IDuplexSessionChannel>());
		}

		// Token: 0x06005F2F RID: 24367 RVA: 0x00161560 File Offset: 0x0015F760
		public virtual void SetKeyDerivation(bool requireDerivedKeys)
		{
			this.EndpointSupportingTokenParameters.SetKeyDerivation(requireDerivedKeys);
			this.OptionalEndpointSupportingTokenParameters.SetKeyDerivation(requireDerivedKeys);
			foreach (SupportingTokenParameters supportingTokenParameters in this.OperationSupportingTokenParameters.Values)
			{
				supportingTokenParameters.SetKeyDerivation(requireDerivedKeys);
			}
			foreach (SupportingTokenParameters supportingTokenParameters2 in this.OptionalOperationSupportingTokenParameters.Values)
			{
				supportingTokenParameters2.SetKeyDerivation(requireDerivedKeys);
			}
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x0016160C File Offset: 0x0015F80C
		internal virtual bool IsSetKeyDerivation(bool requireDerivedKeys)
		{
			if (!this.EndpointSupportingTokenParameters.IsSetKeyDerivation(requireDerivedKeys))
			{
				return false;
			}
			if (!this.OptionalEndpointSupportingTokenParameters.IsSetKeyDerivation(requireDerivedKeys))
			{
				return false;
			}
			foreach (SupportingTokenParameters supportingTokenParameters in this.OperationSupportingTokenParameters.Values)
			{
				if (!supportingTokenParameters.IsSetKeyDerivation(requireDerivedKeys))
				{
					return false;
				}
			}
			foreach (SupportingTokenParameters supportingTokenParameters2 in this.OptionalOperationSupportingTokenParameters.Values)
			{
				if (!supportingTokenParameters2.IsSetKeyDerivation(requireDerivedKeys))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005F31 RID: 24369 RVA: 0x001616D0 File Offset: 0x0015F8D0
		internal ChannelProtectionRequirements GetProtectionRequirements(AddressingVersion addressing, ProtectionLevel defaultProtectionLevel)
		{
			if (addressing == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addressing");
			}
			ChannelProtectionRequirements channelProtectionRequirements = new ChannelProtectionRequirements();
			ProtectionLevel supportedRequestProtectionLevel = base.GetIndividualProperty<ISecurityCapabilities>().SupportedRequestProtectionLevel;
			ProtectionLevel supportedResponseProtectionLevel = base.GetIndividualProperty<ISecurityCapabilities>().SupportedResponseProtectionLevel;
			bool flag = ProtectionLevelHelper.IsStrongerOrEqual(supportedRequestProtectionLevel, defaultProtectionLevel) && ProtectionLevelHelper.IsStrongerOrEqual(supportedResponseProtectionLevel, defaultProtectionLevel);
			if (flag)
			{
				MessagePartSpecification messagePartSpecification = new MessagePartSpecification();
				MessagePartSpecification messagePartSpecification2 = new MessagePartSpecification();
				if (defaultProtectionLevel != ProtectionLevel.None)
				{
					messagePartSpecification.IsBodyIncluded = true;
					if (defaultProtectionLevel == ProtectionLevel.EncryptAndSign)
					{
						messagePartSpecification2.IsBodyIncluded = true;
					}
				}
				messagePartSpecification.MakeReadOnly();
				messagePartSpecification2.MakeReadOnly();
				if (addressing.FaultAction != null)
				{
					channelProtectionRequirements.IncomingSignatureParts.AddParts(messagePartSpecification, addressing.FaultAction);
					channelProtectionRequirements.OutgoingSignatureParts.AddParts(messagePartSpecification, addressing.FaultAction);
					channelProtectionRequirements.IncomingEncryptionParts.AddParts(messagePartSpecification2, addressing.FaultAction);
					channelProtectionRequirements.OutgoingEncryptionParts.AddParts(messagePartSpecification2, addressing.FaultAction);
				}
				if (addressing.DefaultFaultAction != null)
				{
					channelProtectionRequirements.IncomingSignatureParts.AddParts(messagePartSpecification, addressing.DefaultFaultAction);
					channelProtectionRequirements.OutgoingSignatureParts.AddParts(messagePartSpecification, addressing.DefaultFaultAction);
					channelProtectionRequirements.IncomingEncryptionParts.AddParts(messagePartSpecification2, addressing.DefaultFaultAction);
					channelProtectionRequirements.OutgoingEncryptionParts.AddParts(messagePartSpecification2, addressing.DefaultFaultAction);
				}
				channelProtectionRequirements.IncomingSignatureParts.AddParts(messagePartSpecification, "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher/fault");
				channelProtectionRequirements.OutgoingSignatureParts.AddParts(messagePartSpecification, "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher/fault");
				channelProtectionRequirements.IncomingEncryptionParts.AddParts(messagePartSpecification2, "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher/fault");
				channelProtectionRequirements.OutgoingEncryptionParts.AddParts(messagePartSpecification2, "http://schemas.microsoft.com/net/2005/12/windowscommunicationfoundation/dispatcher/fault");
			}
			return channelProtectionRequirements;
		}

		// Token: 0x06005F32 RID: 24370 RVA: 0x0016184C File Offset: 0x0015FA4C
		[__DynamicallyInvokable]
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				return (T)((object)this.GetSecurityCapabilities(context));
			}
			if (typeof(T) == typeof(IdentityVerifier))
			{
				return (T)((object)this.localClientSettings.IdentityVerifier);
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x06005F33 RID: 24371
		internal abstract ISecurityCapabilities GetIndividualISecurityCapabilities();

		// Token: 0x06005F34 RID: 24372 RVA: 0x001618C8 File Offset: 0x0015FAC8
		private ISecurityCapabilities GetSecurityCapabilities(BindingContext context)
		{
			ISecurityCapabilities individualISecurityCapabilities = this.GetIndividualISecurityCapabilities();
			ISecurityCapabilities innerProperty = context.GetInnerProperty<ISecurityCapabilities>();
			if (innerProperty == null)
			{
				return individualISecurityCapabilities;
			}
			bool supportsClientAuthentication = individualISecurityCapabilities.SupportsClientAuthentication;
			bool supportsClientWindowsIdentity = individualISecurityCapabilities.SupportsClientWindowsIdentity;
			bool supportsServerAuth = individualISecurityCapabilities.SupportsServerAuthentication || innerProperty.SupportsServerAuthentication;
			ProtectionLevel requestProtectionLevel = ProtectionLevelHelper.Max(individualISecurityCapabilities.SupportedRequestProtectionLevel, innerProperty.SupportedRequestProtectionLevel);
			ProtectionLevel responseProtectionLevel = ProtectionLevelHelper.Max(individualISecurityCapabilities.SupportedResponseProtectionLevel, innerProperty.SupportedResponseProtectionLevel);
			return new SecurityCapabilities(supportsClientAuthentication, supportsServerAuth, supportsClientWindowsIdentity, requestProtectionLevel, responseProtectionLevel);
		}

		// Token: 0x06005F35 RID: 24373 RVA: 0x0016193C File Offset: 0x0015FB3C
		public static SecurityBindingElement CreateMutualCertificateBindingElement()
		{
			return SecurityBindingElement.CreateMutualCertificateBindingElement(MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11);
		}

		// Token: 0x06005F36 RID: 24374 RVA: 0x00161948 File Offset: 0x0015FB48
		internal static bool IsMutualCertificateBinding(SecurityBindingElement sbe)
		{
			return SecurityBindingElement.IsMutualCertificateBinding(sbe, false);
		}

		// Token: 0x06005F37 RID: 24375 RVA: 0x00161954 File Offset: 0x0015FB54
		public static AsymmetricSecurityBindingElement CreateCertificateSignatureBindingElement()
		{
			return new AsymmetricSecurityBindingElement(new X509SecurityTokenParameters(X509KeyIdentifierClauseType.Any, SecurityTokenInclusionMode.Never, false), new X509SecurityTokenParameters(X509KeyIdentifierClauseType.Any, SecurityTokenInclusionMode.AlwaysToRecipient, false))
			{
				IsCertificateSignatureBinding = true,
				LocalClientSettings = 
				{
					DetectReplays = false
				},
				MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt
			};
		}

		// Token: 0x06005F38 RID: 24376 RVA: 0x00161992 File Offset: 0x0015FB92
		public static SecurityBindingElement CreateMutualCertificateBindingElement(MessageSecurityVersion version)
		{
			return SecurityBindingElement.CreateMutualCertificateBindingElement(version, false);
		}

		// Token: 0x06005F39 RID: 24377 RVA: 0x0016199C File Offset: 0x0015FB9C
		public static SecurityBindingElement CreateMutualCertificateBindingElement(MessageSecurityVersion version, bool allowSerializedSigningTokenOnReply)
		{
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("version");
			}
			SecurityBindingElement securityBindingElement;
			if (version.SecurityVersion == SecurityVersion.WSSecurity10)
			{
				securityBindingElement = new AsymmetricSecurityBindingElement(new X509SecurityTokenParameters(X509KeyIdentifierClauseType.Any, SecurityTokenInclusionMode.Never, false), new X509SecurityTokenParameters(X509KeyIdentifierClauseType.Any, SecurityTokenInclusionMode.AlwaysToRecipient, false), allowSerializedSigningTokenOnReply);
			}
			else
			{
				securityBindingElement = new SymmetricSecurityBindingElement(new X509SecurityTokenParameters(X509KeyIdentifierClauseType.Thumbprint, SecurityTokenInclusionMode.Never));
				securityBindingElement.EndpointSupportingTokenParameters.Endorsing.Add(new X509SecurityTokenParameters(X509KeyIdentifierClauseType.Thumbprint, SecurityTokenInclusionMode.AlwaysToRecipient, false));
				((SymmetricSecurityBindingElement)securityBindingElement).RequireSignatureConfirmation = true;
			}
			securityBindingElement.MessageSecurityVersion = version;
			return securityBindingElement;
		}

		// Token: 0x06005F3A RID: 24378 RVA: 0x00161A1C File Offset: 0x0015FC1C
		internal static bool IsMutualCertificateDuplexBinding(SecurityBindingElement sbe)
		{
			AsymmetricSecurityBindingElement asymmetricSecurityBindingElement = sbe as AsymmetricSecurityBindingElement;
			if (asymmetricSecurityBindingElement == null)
			{
				return false;
			}
			X509SecurityTokenParameters x509SecurityTokenParameters = asymmetricSecurityBindingElement.RecipientTokenParameters as X509SecurityTokenParameters;
			if (x509SecurityTokenParameters == null || (x509SecurityTokenParameters.X509ReferenceStyle != X509KeyIdentifierClauseType.Any && x509SecurityTokenParameters.X509ReferenceStyle != X509KeyIdentifierClauseType.Thumbprint) || x509SecurityTokenParameters.InclusionMode != SecurityTokenInclusionMode.AlwaysToInitiator)
			{
				return false;
			}
			X509SecurityTokenParameters x509SecurityTokenParameters2 = asymmetricSecurityBindingElement.InitiatorTokenParameters as X509SecurityTokenParameters;
			return x509SecurityTokenParameters2 != null && (x509SecurityTokenParameters2.X509ReferenceStyle == X509KeyIdentifierClauseType.Any || x509SecurityTokenParameters2.X509ReferenceStyle == X509KeyIdentifierClauseType.Thumbprint) && x509SecurityTokenParameters2.InclusionMode == SecurityTokenInclusionMode.AlwaysToRecipient && sbe.EndpointSupportingTokenParameters.IsEmpty();
		}

		// Token: 0x06005F3B RID: 24379 RVA: 0x00161A9C File Offset: 0x0015FC9C
		internal static bool IsMutualCertificateBinding(SecurityBindingElement sbe, bool allowSerializedSigningTokenOnReply)
		{
			AsymmetricSecurityBindingElement asymmetricSecurityBindingElement = sbe as AsymmetricSecurityBindingElement;
			if (asymmetricSecurityBindingElement != null)
			{
				X509SecurityTokenParameters x509SecurityTokenParameters = asymmetricSecurityBindingElement.RecipientTokenParameters as X509SecurityTokenParameters;
				if (x509SecurityTokenParameters == null || x509SecurityTokenParameters.X509ReferenceStyle != X509KeyIdentifierClauseType.Any || x509SecurityTokenParameters.InclusionMode != SecurityTokenInclusionMode.Never)
				{
					return false;
				}
				X509SecurityTokenParameters x509SecurityTokenParameters2 = asymmetricSecurityBindingElement.InitiatorTokenParameters as X509SecurityTokenParameters;
				if (x509SecurityTokenParameters2 == null || x509SecurityTokenParameters2.X509ReferenceStyle != X509KeyIdentifierClauseType.Any || x509SecurityTokenParameters2.InclusionMode != SecurityTokenInclusionMode.AlwaysToRecipient)
				{
					return false;
				}
				if (!sbe.EndpointSupportingTokenParameters.IsEmpty())
				{
					return false;
				}
			}
			else
			{
				SymmetricSecurityBindingElement symmetricSecurityBindingElement = sbe as SymmetricSecurityBindingElement;
				if (symmetricSecurityBindingElement == null)
				{
					return false;
				}
				X509SecurityTokenParameters x509SecurityTokenParameters3 = symmetricSecurityBindingElement.ProtectionTokenParameters as X509SecurityTokenParameters;
				if (x509SecurityTokenParameters3 == null || x509SecurityTokenParameters3.X509ReferenceStyle != X509KeyIdentifierClauseType.Thumbprint || x509SecurityTokenParameters3.InclusionMode != SecurityTokenInclusionMode.Never)
				{
					return false;
				}
				SupportingTokenParameters supportingTokenParameters = sbe.EndpointSupportingTokenParameters;
				if (supportingTokenParameters.Signed.Count != 0 || supportingTokenParameters.SignedEncrypted.Count != 0 || supportingTokenParameters.Endorsing.Count != 1 || supportingTokenParameters.SignedEndorsing.Count != 0)
				{
					return false;
				}
				x509SecurityTokenParameters3 = (supportingTokenParameters.Endorsing[0] as X509SecurityTokenParameters);
				if (x509SecurityTokenParameters3 == null || x509SecurityTokenParameters3.X509ReferenceStyle != X509KeyIdentifierClauseType.Thumbprint || x509SecurityTokenParameters3.InclusionMode != SecurityTokenInclusionMode.AlwaysToRecipient)
				{
					return false;
				}
				if (!symmetricSecurityBindingElement.RequireSignatureConfirmation)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x00161BB8 File Offset: 0x0015FDB8
		public static SymmetricSecurityBindingElement CreateAnonymousForCertificateBindingElement()
		{
			return new SymmetricSecurityBindingElement(new X509SecurityTokenParameters(X509KeyIdentifierClauseType.Thumbprint, SecurityTokenInclusionMode.Never))
			{
				MessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11,
				RequireSignatureConfirmation = true
			};
		}

		// Token: 0x06005F3D RID: 24381 RVA: 0x00161BE8 File Offset: 0x0015FDE8
		internal static bool IsAnonymousForCertificateBinding(SecurityBindingElement sbe)
		{
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = sbe as SymmetricSecurityBindingElement;
			if (symmetricSecurityBindingElement == null)
			{
				return false;
			}
			if (!symmetricSecurityBindingElement.RequireSignatureConfirmation)
			{
				return false;
			}
			X509SecurityTokenParameters x509SecurityTokenParameters = symmetricSecurityBindingElement.ProtectionTokenParameters as X509SecurityTokenParameters;
			return x509SecurityTokenParameters != null && x509SecurityTokenParameters.X509ReferenceStyle == X509KeyIdentifierClauseType.Thumbprint && x509SecurityTokenParameters.InclusionMode == SecurityTokenInclusionMode.Never && sbe.EndpointSupportingTokenParameters.IsEmpty();
		}

		// Token: 0x06005F3E RID: 24382 RVA: 0x00161C3E File Offset: 0x0015FE3E
		public static AsymmetricSecurityBindingElement CreateMutualCertificateDuplexBindingElement()
		{
			return SecurityBindingElement.CreateMutualCertificateDuplexBindingElement(MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11);
		}

		// Token: 0x06005F3F RID: 24383 RVA: 0x00161C4C File Offset: 0x0015FE4C
		public static AsymmetricSecurityBindingElement CreateMutualCertificateDuplexBindingElement(MessageSecurityVersion version)
		{
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("version");
			}
			AsymmetricSecurityBindingElement asymmetricSecurityBindingElement;
			if (version.SecurityVersion == SecurityVersion.WSSecurity10)
			{
				asymmetricSecurityBindingElement = new AsymmetricSecurityBindingElement(new X509SecurityTokenParameters(X509KeyIdentifierClauseType.Any, SecurityTokenInclusionMode.AlwaysToInitiator, false), new X509SecurityTokenParameters(X509KeyIdentifierClauseType.Any, SecurityTokenInclusionMode.AlwaysToRecipient, false));
			}
			else
			{
				asymmetricSecurityBindingElement = new AsymmetricSecurityBindingElement(new X509SecurityTokenParameters(X509KeyIdentifierClauseType.Thumbprint, SecurityTokenInclusionMode.AlwaysToInitiator, false), new X509SecurityTokenParameters(X509KeyIdentifierClauseType.Thumbprint, SecurityTokenInclusionMode.AlwaysToRecipient, false));
			}
			asymmetricSecurityBindingElement.MessageSecurityVersion = version;
			return asymmetricSecurityBindingElement;
		}

		// Token: 0x06005F40 RID: 24384 RVA: 0x00161CB0 File Offset: 0x0015FEB0
		public static SymmetricSecurityBindingElement CreateUserNameForCertificateBindingElement()
		{
			return new SymmetricSecurityBindingElement(new X509SecurityTokenParameters(X509KeyIdentifierClauseType.Thumbprint, SecurityTokenInclusionMode.Never))
			{
				EndpointSupportingTokenParameters = 
				{
					SignedEncrypted = 
					{
						new UserNameSecurityTokenParameters()
					}
				},
				MessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11
			};
		}

		// Token: 0x06005F41 RID: 24385 RVA: 0x00161CEC File Offset: 0x0015FEEC
		internal static bool IsUserNameForCertificateBinding(SecurityBindingElement sbe)
		{
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = sbe as SymmetricSecurityBindingElement;
			if (symmetricSecurityBindingElement == null)
			{
				return false;
			}
			X509SecurityTokenParameters x509SecurityTokenParameters = symmetricSecurityBindingElement.ProtectionTokenParameters as X509SecurityTokenParameters;
			if (x509SecurityTokenParameters == null || x509SecurityTokenParameters.X509ReferenceStyle != X509KeyIdentifierClauseType.Thumbprint || x509SecurityTokenParameters.InclusionMode != SecurityTokenInclusionMode.Never)
			{
				return false;
			}
			SupportingTokenParameters supportingTokenParameters = sbe.EndpointSupportingTokenParameters;
			return supportingTokenParameters.Signed.Count == 0 && supportingTokenParameters.SignedEncrypted.Count == 1 && supportingTokenParameters.Endorsing.Count == 0 && supportingTokenParameters.SignedEndorsing.Count == 0 && supportingTokenParameters.SignedEncrypted[0] is UserNameSecurityTokenParameters;
		}

		// Token: 0x06005F42 RID: 24386 RVA: 0x00161D80 File Offset: 0x0015FF80
		public static SymmetricSecurityBindingElement CreateKerberosBindingElement()
		{
			return new SymmetricSecurityBindingElement(new KerberosSecurityTokenParameters())
			{
				DefaultAlgorithmSuite = SecurityAlgorithmSuite.KerberosDefault
			};
		}

		// Token: 0x06005F43 RID: 24387 RVA: 0x00161DA4 File Offset: 0x0015FFA4
		internal static bool IsKerberosBinding(SecurityBindingElement sbe)
		{
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = sbe as SymmetricSecurityBindingElement;
			return symmetricSecurityBindingElement != null && symmetricSecurityBindingElement.ProtectionTokenParameters is KerberosSecurityTokenParameters && sbe.EndpointSupportingTokenParameters.IsEmpty();
		}

		// Token: 0x06005F44 RID: 24388 RVA: 0x00161DDE File Offset: 0x0015FFDE
		public static SymmetricSecurityBindingElement CreateSspiNegotiationBindingElement()
		{
			return SecurityBindingElement.CreateSspiNegotiationBindingElement(false);
		}

		// Token: 0x06005F45 RID: 24389 RVA: 0x00161DE8 File Offset: 0x0015FFE8
		public static SymmetricSecurityBindingElement CreateSspiNegotiationBindingElement(bool requireCancellation)
		{
			return new SymmetricSecurityBindingElement(new SspiSecurityTokenParameters(requireCancellation));
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x00161E04 File Offset: 0x00160004
		internal static bool IsSspiNegotiationBinding(SecurityBindingElement sbe, bool requireCancellation)
		{
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = sbe as SymmetricSecurityBindingElement;
			if (symmetricSecurityBindingElement == null)
			{
				return false;
			}
			if (!sbe.EndpointSupportingTokenParameters.IsEmpty())
			{
				return false;
			}
			SspiSecurityTokenParameters sspiSecurityTokenParameters = symmetricSecurityBindingElement.ProtectionTokenParameters as SspiSecurityTokenParameters;
			return sspiSecurityTokenParameters != null && sspiSecurityTokenParameters.RequireCancellation == requireCancellation;
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x00161E46 File Offset: 0x00160046
		public static SymmetricSecurityBindingElement CreateSslNegotiationBindingElement(bool requireClientCertificate)
		{
			return SecurityBindingElement.CreateSslNegotiationBindingElement(requireClientCertificate, false);
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x00161E50 File Offset: 0x00160050
		public static SymmetricSecurityBindingElement CreateSslNegotiationBindingElement(bool requireClientCertificate, bool requireCancellation)
		{
			return new SymmetricSecurityBindingElement(new SslSecurityTokenParameters(requireClientCertificate, requireCancellation));
		}

		// Token: 0x06005F49 RID: 24393 RVA: 0x00161E6C File Offset: 0x0016006C
		internal static bool IsSslNegotiationBinding(SecurityBindingElement sbe, bool requireClientCertificate, bool requireCancellation)
		{
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = sbe as SymmetricSecurityBindingElement;
			if (symmetricSecurityBindingElement == null)
			{
				return false;
			}
			if (!sbe.EndpointSupportingTokenParameters.IsEmpty())
			{
				return false;
			}
			SslSecurityTokenParameters sslSecurityTokenParameters = symmetricSecurityBindingElement.ProtectionTokenParameters as SslSecurityTokenParameters;
			return sslSecurityTokenParameters != null && sslSecurityTokenParameters.RequireClientCertificate == requireClientCertificate && sslSecurityTokenParameters.RequireCancellation == requireCancellation;
		}

		// Token: 0x06005F4A RID: 24394 RVA: 0x00161EBC File Offset: 0x001600BC
		public static SymmetricSecurityBindingElement CreateIssuedTokenBindingElement(IssuedSecurityTokenParameters issuedTokenParameters)
		{
			if (issuedTokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuedTokenParameters");
			}
			if (issuedTokenParameters.KeyType != SecurityKeyType.SymmetricKey)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("IssuedTokenAuthenticationModeRequiresSymmetricIssuedKey"));
			}
			return new SymmetricSecurityBindingElement(issuedTokenParameters);
		}

		// Token: 0x06005F4B RID: 24395 RVA: 0x00161F04 File Offset: 0x00160104
		public static SymmetricSecurityBindingElement CreateIssuedTokenForCertificateBindingElement(IssuedSecurityTokenParameters issuedTokenParameters)
		{
			if (issuedTokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuedTokenParameters");
			}
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = new SymmetricSecurityBindingElement(new X509SecurityTokenParameters(X509KeyIdentifierClauseType.Thumbprint, SecurityTokenInclusionMode.Never));
			if (issuedTokenParameters.KeyType == SecurityKeyType.BearerKey)
			{
				symmetricSecurityBindingElement.EndpointSupportingTokenParameters.SignedEncrypted.Add(issuedTokenParameters);
				symmetricSecurityBindingElement.MessageSecurityVersion = MessageSecurityVersion.WSSXDefault;
			}
			else
			{
				symmetricSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Add(issuedTokenParameters);
				symmetricSecurityBindingElement.MessageSecurityVersion = MessageSecurityVersion.Default;
			}
			symmetricSecurityBindingElement.RequireSignatureConfirmation = true;
			return symmetricSecurityBindingElement;
		}

		// Token: 0x06005F4C RID: 24396 RVA: 0x00161F7C File Offset: 0x0016017C
		internal static bool IsIssuedTokenForCertificateBinding(SecurityBindingElement sbe, out IssuedSecurityTokenParameters issuedTokenParameters)
		{
			issuedTokenParameters = null;
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = sbe as SymmetricSecurityBindingElement;
			if (symmetricSecurityBindingElement == null)
			{
				return false;
			}
			if (!symmetricSecurityBindingElement.RequireSignatureConfirmation)
			{
				return false;
			}
			X509SecurityTokenParameters x509SecurityTokenParameters = symmetricSecurityBindingElement.ProtectionTokenParameters as X509SecurityTokenParameters;
			if (x509SecurityTokenParameters == null || x509SecurityTokenParameters.X509ReferenceStyle != X509KeyIdentifierClauseType.Thumbprint || x509SecurityTokenParameters.InclusionMode != SecurityTokenInclusionMode.Never)
			{
				return false;
			}
			SupportingTokenParameters supportingTokenParameters = symmetricSecurityBindingElement.EndpointSupportingTokenParameters;
			if (supportingTokenParameters.Signed.Count != 0 || (supportingTokenParameters.SignedEncrypted.Count == 0 && supportingTokenParameters.Endorsing.Count == 0) || supportingTokenParameters.SignedEndorsing.Count != 0)
			{
				return false;
			}
			if (supportingTokenParameters.SignedEncrypted.Count == 1 && supportingTokenParameters.Endorsing.Count == 0)
			{
				issuedTokenParameters = (supportingTokenParameters.SignedEncrypted[0] as IssuedSecurityTokenParameters);
				if (issuedTokenParameters != null && issuedTokenParameters.KeyType != SecurityKeyType.BearerKey)
				{
					return false;
				}
			}
			else if (supportingTokenParameters.Endorsing.Count == 1 && supportingTokenParameters.SignedEncrypted.Count == 0)
			{
				issuedTokenParameters = (supportingTokenParameters.Endorsing[0] as IssuedSecurityTokenParameters);
				if (issuedTokenParameters != null && issuedTokenParameters.KeyType != SecurityKeyType.SymmetricKey && issuedTokenParameters.KeyType != SecurityKeyType.AsymmetricKey)
				{
					return false;
				}
			}
			return issuedTokenParameters != null;
		}

		// Token: 0x06005F4D RID: 24397 RVA: 0x0016208C File Offset: 0x0016028C
		public static SymmetricSecurityBindingElement CreateIssuedTokenForSslBindingElement(IssuedSecurityTokenParameters issuedTokenParameters)
		{
			return SecurityBindingElement.CreateIssuedTokenForSslBindingElement(issuedTokenParameters, false);
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x00162095 File Offset: 0x00160295
		internal static bool IsIssuedTokenForSslBinding(SecurityBindingElement sbe, out IssuedSecurityTokenParameters issuedTokenParameters)
		{
			return SecurityBindingElement.IsIssuedTokenForSslBinding(sbe, false, out issuedTokenParameters);
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x001620A0 File Offset: 0x001602A0
		public static SymmetricSecurityBindingElement CreateIssuedTokenForSslBindingElement(IssuedSecurityTokenParameters issuedTokenParameters, bool requireCancellation)
		{
			if (issuedTokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuedTokenParameters");
			}
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = new SymmetricSecurityBindingElement(new SslSecurityTokenParameters(false, requireCancellation));
			if (issuedTokenParameters.KeyType == SecurityKeyType.BearerKey)
			{
				symmetricSecurityBindingElement.EndpointSupportingTokenParameters.SignedEncrypted.Add(issuedTokenParameters);
				symmetricSecurityBindingElement.MessageSecurityVersion = MessageSecurityVersion.WSSXDefault;
			}
			else
			{
				symmetricSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Add(issuedTokenParameters);
				symmetricSecurityBindingElement.MessageSecurityVersion = MessageSecurityVersion.Default;
			}
			symmetricSecurityBindingElement.RequireSignatureConfirmation = true;
			return symmetricSecurityBindingElement;
		}

		// Token: 0x06005F50 RID: 24400 RVA: 0x00162118 File Offset: 0x00160318
		internal static bool IsIssuedTokenForSslBinding(SecurityBindingElement sbe, bool requireCancellation, out IssuedSecurityTokenParameters issuedTokenParameters)
		{
			issuedTokenParameters = null;
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = sbe as SymmetricSecurityBindingElement;
			if (symmetricSecurityBindingElement == null)
			{
				return false;
			}
			if (!symmetricSecurityBindingElement.RequireSignatureConfirmation)
			{
				return false;
			}
			SslSecurityTokenParameters sslSecurityTokenParameters = symmetricSecurityBindingElement.ProtectionTokenParameters as SslSecurityTokenParameters;
			if (sslSecurityTokenParameters == null)
			{
				return false;
			}
			if (sslSecurityTokenParameters.RequireClientCertificate || sslSecurityTokenParameters.RequireCancellation != requireCancellation)
			{
				return false;
			}
			SupportingTokenParameters supportingTokenParameters = symmetricSecurityBindingElement.EndpointSupportingTokenParameters;
			if (supportingTokenParameters.Signed.Count != 0 || (supportingTokenParameters.SignedEncrypted.Count == 0 && supportingTokenParameters.Endorsing.Count == 0) || supportingTokenParameters.SignedEndorsing.Count != 0)
			{
				return false;
			}
			if (supportingTokenParameters.SignedEncrypted.Count == 1 && supportingTokenParameters.Endorsing.Count == 0)
			{
				issuedTokenParameters = (supportingTokenParameters.SignedEncrypted[0] as IssuedSecurityTokenParameters);
				if (issuedTokenParameters != null && issuedTokenParameters.KeyType != SecurityKeyType.BearerKey)
				{
					return false;
				}
			}
			else if (supportingTokenParameters.Endorsing.Count == 1 && supportingTokenParameters.SignedEncrypted.Count == 0)
			{
				issuedTokenParameters = (supportingTokenParameters.Endorsing[0] as IssuedSecurityTokenParameters);
				if (issuedTokenParameters != null && issuedTokenParameters.KeyType != SecurityKeyType.SymmetricKey && issuedTokenParameters.KeyType != SecurityKeyType.AsymmetricKey)
				{
					return false;
				}
			}
			return issuedTokenParameters != null;
		}

		// Token: 0x06005F51 RID: 24401 RVA: 0x00162229 File Offset: 0x00160429
		public static SymmetricSecurityBindingElement CreateUserNameForSslBindingElement()
		{
			return SecurityBindingElement.CreateUserNameForSslBindingElement(false);
		}

		// Token: 0x06005F52 RID: 24402 RVA: 0x00162234 File Offset: 0x00160434
		public static SymmetricSecurityBindingElement CreateUserNameForSslBindingElement(bool requireCancellation)
		{
			return new SymmetricSecurityBindingElement(new SslSecurityTokenParameters(false, requireCancellation))
			{
				EndpointSupportingTokenParameters = 
				{
					SignedEncrypted = 
					{
						new UserNameSecurityTokenParameters()
					}
				},
				MessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11
			};
		}

		// Token: 0x06005F53 RID: 24403 RVA: 0x00162270 File Offset: 0x00160470
		internal static bool IsUserNameForSslBinding(SecurityBindingElement sbe, bool requireCancellation)
		{
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = sbe as SymmetricSecurityBindingElement;
			if (symmetricSecurityBindingElement == null)
			{
				return false;
			}
			SupportingTokenParameters supportingTokenParameters = sbe.EndpointSupportingTokenParameters;
			if (supportingTokenParameters.Signed.Count != 0 || supportingTokenParameters.SignedEncrypted.Count != 1 || supportingTokenParameters.Endorsing.Count != 0 || supportingTokenParameters.SignedEndorsing.Count != 0)
			{
				return false;
			}
			if (!(supportingTokenParameters.SignedEncrypted[0] is UserNameSecurityTokenParameters))
			{
				return false;
			}
			SslSecurityTokenParameters sslSecurityTokenParameters = symmetricSecurityBindingElement.ProtectionTokenParameters as SslSecurityTokenParameters;
			return sslSecurityTokenParameters != null && sslSecurityTokenParameters.RequireCancellation == requireCancellation && !sslSecurityTokenParameters.RequireClientCertificate;
		}

		// Token: 0x06005F54 RID: 24404 RVA: 0x00162304 File Offset: 0x00160504
		[__DynamicallyInvokable]
		public static TransportSecurityBindingElement CreateUserNameOverTransportBindingElement()
		{
			return new TransportSecurityBindingElement
			{
				EndpointSupportingTokenParameters = 
				{
					SignedEncrypted = 
					{
						new UserNameSecurityTokenParameters()
					}
				},
				IncludeTimestamp = true,
				LocalClientSettings = 
				{
					DetectReplays = false
				},
				LocalServiceSettings = 
				{
					DetectReplays = false
				}
			};
		}

		// Token: 0x06005F55 RID: 24405 RVA: 0x0016234C File Offset: 0x0016054C
		internal static bool IsUserNameOverTransportBinding(SecurityBindingElement sbe)
		{
			if (!sbe.IncludeTimestamp)
			{
				return false;
			}
			if (!(sbe is TransportSecurityBindingElement))
			{
				return false;
			}
			SupportingTokenParameters supportingTokenParameters = sbe.EndpointSupportingTokenParameters;
			return supportingTokenParameters.Signed.Count == 0 && supportingTokenParameters.SignedEncrypted.Count == 1 && supportingTokenParameters.Endorsing.Count == 0 && supportingTokenParameters.SignedEndorsing.Count == 0 && supportingTokenParameters.SignedEncrypted[0] is UserNameSecurityTokenParameters;
		}

		// Token: 0x06005F56 RID: 24406 RVA: 0x001623C3 File Offset: 0x001605C3
		public static TransportSecurityBindingElement CreateCertificateOverTransportBindingElement()
		{
			return SecurityBindingElement.CreateCertificateOverTransportBindingElement(MessageSecurityVersion.Default);
		}

		// Token: 0x06005F57 RID: 24407 RVA: 0x001623D0 File Offset: 0x001605D0
		public static TransportSecurityBindingElement CreateCertificateOverTransportBindingElement(MessageSecurityVersion version)
		{
			if (version == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("version");
			}
			X509KeyIdentifierClauseType x509ReferenceStyle;
			if (version.SecurityVersion == SecurityVersion.WSSecurity10)
			{
				x509ReferenceStyle = X509KeyIdentifierClauseType.Any;
			}
			else
			{
				x509ReferenceStyle = X509KeyIdentifierClauseType.Thumbprint;
			}
			TransportSecurityBindingElement transportSecurityBindingElement = new TransportSecurityBindingElement();
			X509SecurityTokenParameters item = new X509SecurityTokenParameters(x509ReferenceStyle, SecurityTokenInclusionMode.AlwaysToRecipient, false);
			transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Add(item);
			transportSecurityBindingElement.IncludeTimestamp = true;
			transportSecurityBindingElement.LocalClientSettings.DetectReplays = false;
			transportSecurityBindingElement.LocalServiceSettings.DetectReplays = false;
			transportSecurityBindingElement.MessageSecurityVersion = version;
			return transportSecurityBindingElement;
		}

		// Token: 0x06005F58 RID: 24408 RVA: 0x0016244C File Offset: 0x0016064C
		internal static bool IsCertificateOverTransportBinding(SecurityBindingElement sbe)
		{
			if (!sbe.IncludeTimestamp)
			{
				return false;
			}
			if (!(sbe is TransportSecurityBindingElement))
			{
				return false;
			}
			SupportingTokenParameters supportingTokenParameters = sbe.EndpointSupportingTokenParameters;
			if (supportingTokenParameters.Signed.Count != 0 || supportingTokenParameters.SignedEncrypted.Count != 0 || supportingTokenParameters.Endorsing.Count != 1 || supportingTokenParameters.SignedEndorsing.Count != 0)
			{
				return false;
			}
			X509SecurityTokenParameters x509SecurityTokenParameters = supportingTokenParameters.Endorsing[0] as X509SecurityTokenParameters;
			return x509SecurityTokenParameters != null && x509SecurityTokenParameters.InclusionMode == SecurityTokenInclusionMode.AlwaysToRecipient && (x509SecurityTokenParameters.X509ReferenceStyle == X509KeyIdentifierClauseType.Any || x509SecurityTokenParameters.X509ReferenceStyle == X509KeyIdentifierClauseType.Thumbprint);
		}

		// Token: 0x06005F59 RID: 24409 RVA: 0x001624E0 File Offset: 0x001606E0
		public static TransportSecurityBindingElement CreateKerberosOverTransportBindingElement()
		{
			TransportSecurityBindingElement transportSecurityBindingElement = new TransportSecurityBindingElement();
			KerberosSecurityTokenParameters kerberosSecurityTokenParameters = new KerberosSecurityTokenParameters();
			kerberosSecurityTokenParameters.RequireDerivedKeys = false;
			transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Add(kerberosSecurityTokenParameters);
			transportSecurityBindingElement.IncludeTimestamp = true;
			transportSecurityBindingElement.LocalClientSettings.DetectReplays = false;
			transportSecurityBindingElement.LocalServiceSettings.DetectReplays = false;
			transportSecurityBindingElement.DefaultAlgorithmSuite = SecurityAlgorithmSuite.KerberosDefault;
			transportSecurityBindingElement.SupportsExtendedProtectionPolicy = true;
			return transportSecurityBindingElement;
		}

		// Token: 0x06005F5A RID: 24410 RVA: 0x00162543 File Offset: 0x00160743
		public static TransportSecurityBindingElement CreateSspiNegotiationOverTransportBindingElement()
		{
			return SecurityBindingElement.CreateSspiNegotiationOverTransportBindingElement(true);
		}

		// Token: 0x06005F5B RID: 24411 RVA: 0x0016254C File Offset: 0x0016074C
		public static TransportSecurityBindingElement CreateSspiNegotiationOverTransportBindingElement(bool requireCancellation)
		{
			TransportSecurityBindingElement transportSecurityBindingElement = new TransportSecurityBindingElement();
			SspiSecurityTokenParameters sspiSecurityTokenParameters = new SspiSecurityTokenParameters(requireCancellation);
			sspiSecurityTokenParameters.RequireDerivedKeys = false;
			transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Add(sspiSecurityTokenParameters);
			transportSecurityBindingElement.IncludeTimestamp = true;
			transportSecurityBindingElement.LocalClientSettings.DetectReplays = false;
			transportSecurityBindingElement.LocalServiceSettings.DetectReplays = false;
			transportSecurityBindingElement.SupportsExtendedProtectionPolicy = true;
			return transportSecurityBindingElement;
		}

		// Token: 0x06005F5C RID: 24412 RVA: 0x001625A8 File Offset: 0x001607A8
		internal static bool IsSspiNegotiationOverTransportBinding(SecurityBindingElement sbe, bool requireCancellation)
		{
			if (!sbe.IncludeTimestamp)
			{
				return false;
			}
			SupportingTokenParameters supportingTokenParameters = sbe.EndpointSupportingTokenParameters;
			if (supportingTokenParameters.Signed.Count != 0 || supportingTokenParameters.SignedEncrypted.Count != 0 || supportingTokenParameters.Endorsing.Count != 1 || supportingTokenParameters.SignedEndorsing.Count != 0)
			{
				return false;
			}
			SspiSecurityTokenParameters sspiSecurityTokenParameters = supportingTokenParameters.Endorsing[0] as SspiSecurityTokenParameters;
			return sspiSecurityTokenParameters != null && !sspiSecurityTokenParameters.RequireDerivedKeys && sspiSecurityTokenParameters.RequireCancellation == requireCancellation && sbe is TransportSecurityBindingElement;
		}

		// Token: 0x06005F5D RID: 24413 RVA: 0x00162634 File Offset: 0x00160834
		public static TransportSecurityBindingElement CreateIssuedTokenOverTransportBindingElement(IssuedSecurityTokenParameters issuedTokenParameters)
		{
			if (issuedTokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuedTokenParameters");
			}
			issuedTokenParameters.RequireDerivedKeys = false;
			TransportSecurityBindingElement transportSecurityBindingElement = new TransportSecurityBindingElement();
			if (issuedTokenParameters.KeyType == SecurityKeyType.BearerKey)
			{
				transportSecurityBindingElement.EndpointSupportingTokenParameters.Signed.Add(issuedTokenParameters);
				transportSecurityBindingElement.MessageSecurityVersion = MessageSecurityVersion.WSSXDefault;
			}
			else
			{
				transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Add(issuedTokenParameters);
				transportSecurityBindingElement.MessageSecurityVersion = MessageSecurityVersion.Default;
			}
			transportSecurityBindingElement.LocalClientSettings.DetectReplays = false;
			transportSecurityBindingElement.LocalServiceSettings.DetectReplays = false;
			transportSecurityBindingElement.IncludeTimestamp = true;
			return transportSecurityBindingElement;
		}

		// Token: 0x06005F5E RID: 24414 RVA: 0x001626C4 File Offset: 0x001608C4
		internal static bool IsIssuedTokenOverTransportBinding(SecurityBindingElement sbe, out IssuedSecurityTokenParameters issuedTokenParameters)
		{
			issuedTokenParameters = null;
			if (!(sbe is TransportSecurityBindingElement))
			{
				return false;
			}
			if (!sbe.IncludeTimestamp)
			{
				return false;
			}
			SupportingTokenParameters supportingTokenParameters = sbe.EndpointSupportingTokenParameters;
			if (supportingTokenParameters.SignedEncrypted.Count != 0 || (supportingTokenParameters.Signed.Count == 0 && supportingTokenParameters.Endorsing.Count == 0) || supportingTokenParameters.SignedEndorsing.Count != 0)
			{
				return false;
			}
			if (supportingTokenParameters.Signed.Count == 1 && supportingTokenParameters.Endorsing.Count == 0)
			{
				issuedTokenParameters = (supportingTokenParameters.Signed[0] as IssuedSecurityTokenParameters);
				if (issuedTokenParameters != null && issuedTokenParameters.KeyType != SecurityKeyType.BearerKey)
				{
					return false;
				}
			}
			else if (supportingTokenParameters.Endorsing.Count == 1 && supportingTokenParameters.Signed.Count == 0)
			{
				issuedTokenParameters = (supportingTokenParameters.Endorsing[0] as IssuedSecurityTokenParameters);
				if (issuedTokenParameters != null && issuedTokenParameters.KeyType != SecurityKeyType.SymmetricKey && issuedTokenParameters.KeyType != SecurityKeyType.AsymmetricKey)
				{
					return false;
				}
			}
			return issuedTokenParameters != null && !issuedTokenParameters.RequireDerivedKeys;
		}

		// Token: 0x06005F5F RID: 24415 RVA: 0x001627BC File Offset: 0x001609BC
		[__DynamicallyInvokable]
		public static SecurityBindingElement CreateSecureConversationBindingElement(SecurityBindingElement bootstrapSecurity)
		{
			return SecurityBindingElement.CreateSecureConversationBindingElement(bootstrapSecurity, true, null);
		}

		// Token: 0x06005F60 RID: 24416 RVA: 0x001627C6 File Offset: 0x001609C6
		internal static bool IsSecureConversationBinding(SecurityBindingElement sbe, out SecurityBindingElement bootstrapSecurity)
		{
			return SecurityBindingElement.IsSecureConversationBinding(sbe, true, out bootstrapSecurity);
		}

		// Token: 0x06005F61 RID: 24417 RVA: 0x001627D0 File Offset: 0x001609D0
		public static SecurityBindingElement CreateSecureConversationBindingElement(SecurityBindingElement bootstrapSecurity, bool requireCancellation)
		{
			return SecurityBindingElement.CreateSecureConversationBindingElement(bootstrapSecurity, requireCancellation, null);
		}

		// Token: 0x06005F62 RID: 24418 RVA: 0x001627DC File Offset: 0x001609DC
		public static SecurityBindingElement CreateSecureConversationBindingElement(SecurityBindingElement bootstrapSecurity, bool requireCancellation, ChannelProtectionRequirements bootstrapProtectionRequirements)
		{
			if (bootstrapSecurity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bootstrapBinding");
			}
			SecurityBindingElement result;
			if (bootstrapSecurity is TransportSecurityBindingElement)
			{
				TransportSecurityBindingElement transportSecurityBindingElement = new TransportSecurityBindingElement();
				SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = new SecureConversationSecurityTokenParameters(bootstrapSecurity, requireCancellation, bootstrapProtectionRequirements);
				secureConversationSecurityTokenParameters.RequireDerivedKeys = false;
				transportSecurityBindingElement.EndpointSupportingTokenParameters.Endorsing.Add(secureConversationSecurityTokenParameters);
				transportSecurityBindingElement.LocalClientSettings.DetectReplays = false;
				transportSecurityBindingElement.LocalServiceSettings.DetectReplays = false;
				transportSecurityBindingElement.IncludeTimestamp = true;
				result = transportSecurityBindingElement;
			}
			else
			{
				result = new SymmetricSecurityBindingElement(new SecureConversationSecurityTokenParameters(bootstrapSecurity, requireCancellation, bootstrapProtectionRequirements))
				{
					RequireSignatureConfirmation = false
				};
			}
			return result;
		}

		// Token: 0x06005F63 RID: 24419 RVA: 0x00162868 File Offset: 0x00160A68
		internal static bool IsSecureConversationBinding(SecurityBindingElement sbe, bool requireCancellation, out SecurityBindingElement bootstrapSecurity)
		{
			bootstrapSecurity = null;
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = sbe as SymmetricSecurityBindingElement;
			if (symmetricSecurityBindingElement != null)
			{
				if (symmetricSecurityBindingElement.RequireSignatureConfirmation)
				{
					return false;
				}
				SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = symmetricSecurityBindingElement.ProtectionTokenParameters as SecureConversationSecurityTokenParameters;
				if (secureConversationSecurityTokenParameters == null)
				{
					return false;
				}
				if (secureConversationSecurityTokenParameters.RequireCancellation != requireCancellation)
				{
					return false;
				}
				bootstrapSecurity = secureConversationSecurityTokenParameters.BootstrapSecurityBindingElement;
			}
			else
			{
				if (!sbe.IncludeTimestamp)
				{
					return false;
				}
				if (!(sbe is TransportSecurityBindingElement))
				{
					return false;
				}
				SupportingTokenParameters supportingTokenParameters = sbe.EndpointSupportingTokenParameters;
				if (supportingTokenParameters.Signed.Count != 0 || supportingTokenParameters.SignedEncrypted.Count != 0 || supportingTokenParameters.Endorsing.Count != 1 || supportingTokenParameters.SignedEndorsing.Count != 0)
				{
					return false;
				}
				SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters2 = supportingTokenParameters.Endorsing[0] as SecureConversationSecurityTokenParameters;
				if (secureConversationSecurityTokenParameters2 == null)
				{
					return false;
				}
				if (secureConversationSecurityTokenParameters2.RequireCancellation != requireCancellation)
				{
					return false;
				}
				bootstrapSecurity = secureConversationSecurityTokenParameters2.BootstrapSecurityBindingElement;
			}
			return (bootstrapSecurity == null || bootstrapSecurity.SecurityHeaderLayout == SecurityHeaderLayout.Strict) && bootstrapSecurity != null;
		}

		// Token: 0x06005F64 RID: 24420 RVA: 0x00162944 File Offset: 0x00160B44
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0}:", new object[]
			{
				base.GetType().ToString()
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "DefaultAlgorithmSuite: {0}", new object[]
			{
				this.defaultAlgorithmSuite.ToString()
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "IncludeTimestamp: {0}", new object[]
			{
				this.includeTimestamp.ToString()
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "KeyEntropyMode: {0}", new object[]
			{
				this.keyEntropyMode.ToString()
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "MessageSecurityVersion: {0}", new object[]
			{
				this.MessageSecurityVersion.ToString()
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "SecurityHeaderLayout: {0}", new object[]
			{
				this.securityHeaderLayout.ToString()
			}));
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "ProtectTokens: {0}", new object[]
			{
				this.protectTokens.ToString()
			}));
			stringBuilder.AppendLine("EndpointSupportingTokenParameters:");
			stringBuilder.AppendLine("  " + this.EndpointSupportingTokenParameters.ToString().Trim().Replace("\n", "\n  "));
			stringBuilder.AppendLine("OptionalEndpointSupportingTokenParameters:");
			stringBuilder.AppendLine("  " + this.OptionalEndpointSupportingTokenParameters.ToString().Trim().Replace("\n", "\n  "));
			if (this.operationSupportingTokenParameters.Count == 0)
			{
				stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "OperationSupportingTokenParameters: none", new object[0]));
			}
			else
			{
				foreach (string text in this.OperationSupportingTokenParameters.Keys)
				{
					stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "OperationSupportingTokenParameters[\"{0}\"]:", new object[]
					{
						text
					}));
					stringBuilder.AppendLine("  " + this.OperationSupportingTokenParameters[text].ToString().Trim().Replace("\n", "\n  "));
				}
			}
			if (this.optionalOperationSupportingTokenParameters.Count == 0)
			{
				stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "OptionalOperationSupportingTokenParameters: none", new object[0]));
			}
			else
			{
				foreach (string text2 in this.OptionalOperationSupportingTokenParameters.Keys)
				{
					stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "OptionalOperationSupportingTokenParameters[\"{0}\"]:", new object[]
					{
						text2
					}));
					stringBuilder.AppendLine("  " + this.OptionalOperationSupportingTokenParameters[text2].ToString().Trim().Replace("\n", "\n  "));
				}
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x06005F65 RID: 24421 RVA: 0x00162C90 File Offset: 0x00160E90
		internal static ChannelProtectionRequirements ComputeProtectionRequirements(SecurityBindingElement security, BindingParameterCollection parameterCollection, BindingElementCollection bindingElements, bool isForService)
		{
			if (parameterCollection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameterCollection");
			}
			if (bindingElements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingElements");
			}
			if (security == null)
			{
				return null;
			}
			ChannelProtectionRequirements channelProtectionRequirements = null;
			if (security is SymmetricSecurityBindingElement || security is AsymmetricSecurityBindingElement)
			{
				channelProtectionRequirements = new ChannelProtectionRequirements();
				ChannelProtectionRequirements channelProtectionRequirements2 = parameterCollection.Find<ChannelProtectionRequirements>();
				if (channelProtectionRequirements2 != null)
				{
					channelProtectionRequirements.Add(channelProtectionRequirements2);
				}
				SecurityBindingElement.AddBindingProtectionRequirements(channelProtectionRequirements, bindingElements, !isForService);
			}
			return channelProtectionRequirements;
		}

		// Token: 0x06005F66 RID: 24422 RVA: 0x00162D00 File Offset: 0x00160F00
		private static void AddBindingProtectionRequirements(ChannelProtectionRequirements requirements, BindingElementCollection bindingElements, bool isForChannel)
		{
			CustomBinding binding = new CustomBinding(bindingElements);
			BindingContext bindingContext = new BindingContext(binding, new BindingParameterCollection());
			foreach (BindingElement bindingElement in bindingElements)
			{
				if (bindingElement != null)
				{
					bindingContext.RemainingBindingElements.Clear();
					bindingContext.RemainingBindingElements.Add(bindingElement);
					ChannelProtectionRequirements innerProperty = bindingContext.GetInnerProperty<ChannelProtectionRequirements>();
					if (innerProperty != null)
					{
						requirements.Add(innerProperty);
					}
				}
			}
		}

		// Token: 0x06005F67 RID: 24423 RVA: 0x00162D84 File Offset: 0x00160F84
		internal void ApplyAuditBehaviorSettings(BindingContext context, SecurityProtocolFactory factory)
		{
			ServiceSecurityAuditBehavior serviceSecurityAuditBehavior = context.BindingParameters.Find<ServiceSecurityAuditBehavior>();
			if (serviceSecurityAuditBehavior != null)
			{
				factory.AuditLogLocation = serviceSecurityAuditBehavior.AuditLogLocation;
				factory.SuppressAuditFailure = serviceSecurityAuditBehavior.SuppressAuditFailure;
				factory.ServiceAuthorizationAuditLevel = serviceSecurityAuditBehavior.ServiceAuthorizationAuditLevel;
				factory.MessageAuthenticationAuditLevel = serviceSecurityAuditBehavior.MessageAuthenticationAuditLevel;
				return;
			}
			factory.AuditLogLocation = AuditLogLocation.Default;
			factory.SuppressAuditFailure = true;
			factory.ServiceAuthorizationAuditLevel = AuditLevel.None;
			factory.MessageAuthenticationAuditLevel = AuditLevel.None;
		}

		// Token: 0x06005F68 RID: 24424 RVA: 0x00162DF0 File Offset: 0x00160FF0
		internal override bool IsMatch(BindingElement b)
		{
			if (b == null)
			{
				return false;
			}
			SecurityBindingElement securityBindingElement = b as SecurityBindingElement;
			return securityBindingElement != null && SecurityElementBase.AreBindingsMatching(this, securityBindingElement);
		}

		// Token: 0x06005F69 RID: 24425 RVA: 0x00162E15 File Offset: 0x00161015
		private static void AddAssertionIfNotNull(PolicyConversionContext policyContext, XmlElement assertion)
		{
			if (policyContext != null && assertion != null)
			{
				policyContext.GetBindingAssertions().Add(assertion);
			}
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x00162E2C File Offset: 0x0016102C
		private static void AddAssertionIfNotNull(PolicyConversionContext policyContext, Collection<XmlElement> assertions)
		{
			if (policyContext != null && assertions != null)
			{
				PolicyAssertionCollection bindingAssertions = policyContext.GetBindingAssertions();
				for (int i = 0; i < assertions.Count; i++)
				{
					bindingAssertions.Add(assertions[i]);
				}
			}
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x00162E64 File Offset: 0x00161064
		private static void AddAssertionIfNotNull(PolicyConversionContext policyContext, OperationDescription operation, XmlElement assertion)
		{
			if (policyContext != null && assertion != null)
			{
				policyContext.GetOperationBindingAssertions(operation).Add(assertion);
			}
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x00162E7C File Offset: 0x0016107C
		private static void AddAssertionIfNotNull(PolicyConversionContext policyContext, OperationDescription operation, Collection<XmlElement> assertions)
		{
			if (policyContext != null && assertions != null)
			{
				PolicyAssertionCollection operationBindingAssertions = policyContext.GetOperationBindingAssertions(operation);
				for (int i = 0; i < assertions.Count; i++)
				{
					operationBindingAssertions.Add(assertions[i]);
				}
			}
		}

		// Token: 0x06005F6D RID: 24429 RVA: 0x00162EB5 File Offset: 0x001610B5
		private static void AddAssertionIfNotNull(PolicyConversionContext policyContext, MessageDescription message, XmlElement assertion)
		{
			if (policyContext != null && assertion != null)
			{
				policyContext.GetMessageBindingAssertions(message).Add(assertion);
			}
		}

		// Token: 0x06005F6E RID: 24430 RVA: 0x00162ECA File Offset: 0x001610CA
		private static void AddAssertionIfNotNull(PolicyConversionContext policyContext, FaultDescription message, XmlElement assertion)
		{
			if (policyContext != null && assertion != null)
			{
				policyContext.GetFaultBindingAssertions(message).Add(assertion);
			}
		}

		// Token: 0x06005F6F RID: 24431 RVA: 0x00162EE0 File Offset: 0x001610E0
		internal static void ExportPolicy(MetadataExporter exporter, PolicyConversionContext context)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exporter");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			SecurityTraceRecordHelper.TraceExportChannelBindingEntry();
			SecurityBindingElement securityBindingElement = null;
			BindingElementCollection bindingElementCollection = new BindingElementCollection();
			if (context != null && context.BindingElements != null)
			{
				foreach (BindingElement bindingElement in context.BindingElements)
				{
					if (bindingElement is SecurityBindingElement)
					{
						securityBindingElement = (SecurityBindingElement)bindingElement;
					}
					else
					{
						if (securityBindingElement != null || bindingElement is MessageEncodingBindingElement || bindingElement is ITransportTokenAssertionProvider)
						{
							bindingElementCollection.Add(bindingElement);
						}
						if (bindingElement is ITransportTokenAssertionProvider)
						{
							ITransportTokenAssertionProvider transportTokenAssertionProvider = (ITransportTokenAssertionProvider)bindingElement;
						}
					}
				}
			}
			exporter.State["SecureConversationBootstrapBindingElementsBelowSecurityKey"] = bindingElementCollection;
			bool flag = false;
			try
			{
				if (securityBindingElement is SymmetricSecurityBindingElement)
				{
					SecurityBindingElement.ExportSymmetricSecurityBindingElement((SymmetricSecurityBindingElement)securityBindingElement, exporter, context);
					SecurityBindingElement.ExportOperationScopeSupportingTokensPolicy(securityBindingElement, exporter, context);
					SecurityBindingElement.ExportMessageScopeProtectionPolicy(securityBindingElement, exporter, context);
				}
				else if (securityBindingElement is AsymmetricSecurityBindingElement)
				{
					SecurityBindingElement.ExportAsymmetricSecurityBindingElement((AsymmetricSecurityBindingElement)securityBindingElement, exporter, context);
					SecurityBindingElement.ExportOperationScopeSupportingTokensPolicy(securityBindingElement, exporter, context);
					SecurityBindingElement.ExportMessageScopeProtectionPolicy(securityBindingElement, exporter, context);
				}
				flag = true;
			}
			finally
			{
				try
				{
					exporter.State.Remove("SecureConversationBootstrapBindingElementsBelowSecurityKey");
				}
				catch (Exception exception)
				{
					if (flag || Fx.IsFatal(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06005F70 RID: 24432 RVA: 0x00163050 File Offset: 0x00161250
		internal static void ExportPolicyForTransportTokenAssertionProviders(MetadataExporter exporter, PolicyConversionContext context)
		{
			if (exporter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("exporter");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			SecurityTraceRecordHelper.TraceExportChannelBindingEntry();
			SecurityBindingElement securityBindingElement = null;
			ITransportTokenAssertionProvider transportTokenAssertionProvider = null;
			BindingElementCollection bindingElementCollection = new BindingElementCollection();
			if (context != null && context.BindingElements != null)
			{
				foreach (BindingElement bindingElement in context.BindingElements)
				{
					if (bindingElement is SecurityBindingElement)
					{
						securityBindingElement = (SecurityBindingElement)bindingElement;
					}
					else
					{
						if (securityBindingElement != null || bindingElement is MessageEncodingBindingElement || bindingElement is ITransportTokenAssertionProvider)
						{
							bindingElementCollection.Add(bindingElement);
						}
						if (bindingElement is ITransportTokenAssertionProvider)
						{
							transportTokenAssertionProvider = (ITransportTokenAssertionProvider)bindingElement;
						}
					}
				}
			}
			exporter.State["SecureConversationBootstrapBindingElementsBelowSecurityKey"] = bindingElementCollection;
			bool flag = false;
			try
			{
				if (securityBindingElement is TransportSecurityBindingElement)
				{
					if (transportTokenAssertionProvider == null && !securityBindingElement.AllowInsecureTransport)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ExportOfBindingWithTransportSecurityBindingElementAndNoTransportSecurityNotSupported")));
					}
					SecurityBindingElement.ExportTransportSecurityBindingElement((TransportSecurityBindingElement)securityBindingElement, transportTokenAssertionProvider, exporter, context);
					SecurityBindingElement.ExportOperationScopeSupportingTokensPolicy(securityBindingElement, exporter, context);
				}
				else if (transportTokenAssertionProvider != null)
				{
					TransportSecurityBindingElement transportSecurityBindingElement = new TransportSecurityBindingElement();
					if (securityBindingElement == null)
					{
						transportSecurityBindingElement.IncludeTimestamp = false;
					}
					HttpsTransportBindingElement httpsTransportBindingElement = transportTokenAssertionProvider as HttpsTransportBindingElement;
					if (httpsTransportBindingElement != null && httpsTransportBindingElement.MessageSecurityVersion != null)
					{
						transportSecurityBindingElement.MessageSecurityVersion = httpsTransportBindingElement.MessageSecurityVersion;
					}
					SecurityBindingElement.ExportTransportSecurityBindingElement(transportSecurityBindingElement, transportTokenAssertionProvider, exporter, context);
				}
				flag = true;
			}
			finally
			{
				try
				{
					exporter.State.Remove("SecureConversationBootstrapBindingElementsBelowSecurityKey");
				}
				catch (Exception exception)
				{
					if (flag || Fx.IsFatal(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06005F71 RID: 24433 RVA: 0x001631FC File Offset: 0x001613FC
		private static bool RequiresWsspTrust(SecurityBindingElement sbe)
		{
			return sbe != null && !sbe.doNotEmitTrust;
		}

		// Token: 0x06005F72 RID: 24434 RVA: 0x0016320C File Offset: 0x0016140C
		private static void ExportAsymmetricSecurityBindingElement(AsymmetricSecurityBindingElement binding, MetadataExporter exporter, PolicyConversionContext policyContext)
		{
			WSSecurityPolicy securityPolicyDriver = WSSecurityPolicy.GetSecurityPolicyDriver(binding.MessageSecurityVersion);
			SecurityBindingElement.AddAssertionIfNotNull(policyContext, securityPolicyDriver.CreateWsspAsymmetricBindingAssertion(exporter, policyContext, binding));
			SecurityBindingElement.AddAssertionIfNotNull(policyContext, securityPolicyDriver.CreateWsspSupportingTokensAssertion(exporter, binding.EndpointSupportingTokenParameters.Signed, binding.EndpointSupportingTokenParameters.SignedEncrypted, binding.EndpointSupportingTokenParameters.Endorsing, binding.EndpointSupportingTokenParameters.SignedEndorsing, binding.OptionalEndpointSupportingTokenParameters.Signed, binding.OptionalEndpointSupportingTokenParameters.SignedEncrypted, binding.OptionalEndpointSupportingTokenParameters.Endorsing, binding.OptionalEndpointSupportingTokenParameters.SignedEndorsing));
			SecurityBindingElement.AddAssertionIfNotNull(policyContext, securityPolicyDriver.CreateWsspWssAssertion(exporter, binding));
			if (SecurityBindingElement.RequiresWsspTrust(binding))
			{
				SecurityBindingElement.AddAssertionIfNotNull(policyContext, securityPolicyDriver.CreateWsspTrustAssertion(exporter, binding.KeyEntropyMode));
			}
		}

		// Token: 0x06005F73 RID: 24435 RVA: 0x001632C4 File Offset: 0x001614C4
		private static void ExportTransportSecurityBindingElement(TransportSecurityBindingElement binding, ITransportTokenAssertionProvider transportTokenAssertionProvider, MetadataExporter exporter, PolicyConversionContext policyContext)
		{
			WSSecurityPolicy securityPolicyDriver = WSSecurityPolicy.GetSecurityPolicyDriver(binding.MessageSecurityVersion);
			if (transportTokenAssertionProvider == null && binding.AllowInsecureTransport && policyContext != null && policyContext.BindingElements != null)
			{
				foreach (BindingElement bindingElement in policyContext.BindingElements)
				{
					if (bindingElement is HttpTransportBindingElement)
					{
						transportTokenAssertionProvider = new HttpsTransportBindingElement();
						break;
					}
					if (bindingElement is TcpTransportBindingElement)
					{
						transportTokenAssertionProvider = new SslStreamSecurityBindingElement();
						break;
					}
				}
			}
			XmlElement xmlElement = transportTokenAssertionProvider.GetTransportTokenAssertion();
			if (xmlElement == null)
			{
				if (transportTokenAssertionProvider is HttpsTransportBindingElement)
				{
					xmlElement = securityPolicyDriver.CreateWsspHttpsTokenAssertion(exporter, (HttpsTransportBindingElement)transportTokenAssertionProvider);
				}
				if (xmlElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoTransportTokenAssertionProvided", new object[]
					{
						transportTokenAssertionProvider.GetType().ToString()
					})));
				}
			}
			AddressingVersion addressingVersion = AddressingVersion.WSAddressing10;
			MessageEncodingBindingElement messageEncodingBindingElement = policyContext.BindingElements.Find<MessageEncodingBindingElement>();
			if (messageEncodingBindingElement != null)
			{
				addressingVersion = messageEncodingBindingElement.MessageVersion.Addressing;
			}
			SecurityBindingElement.AddAssertionIfNotNull(policyContext, securityPolicyDriver.CreateWsspTransportBindingAssertion(exporter, binding, xmlElement));
			Collection<XmlElement> collection = securityPolicyDriver.CreateWsspSupportingTokensAssertion(exporter, binding.EndpointSupportingTokenParameters.Signed, binding.EndpointSupportingTokenParameters.SignedEncrypted, binding.EndpointSupportingTokenParameters.Endorsing, binding.EndpointSupportingTokenParameters.SignedEndorsing, binding.OptionalEndpointSupportingTokenParameters.Signed, binding.OptionalEndpointSupportingTokenParameters.SignedEncrypted, binding.OptionalEndpointSupportingTokenParameters.Endorsing, binding.OptionalEndpointSupportingTokenParameters.SignedEndorsing, addressingVersion);
			SecurityBindingElement.AddAssertionIfNotNull(policyContext, collection);
			if (collection.Count > 0 || SecurityBindingElement.HasEndorsingSupportingTokensAtOperationScope(binding))
			{
				SecurityBindingElement.AddAssertionIfNotNull(policyContext, securityPolicyDriver.CreateWsspWssAssertion(exporter, binding));
				if (SecurityBindingElement.RequiresWsspTrust(binding))
				{
					SecurityBindingElement.AddAssertionIfNotNull(policyContext, securityPolicyDriver.CreateWsspTrustAssertion(exporter, binding.KeyEntropyMode));
				}
			}
		}

		// Token: 0x06005F74 RID: 24436 RVA: 0x0016347C File Offset: 0x0016167C
		private static bool HasEndorsingSupportingTokensAtOperationScope(SecurityBindingElement binding)
		{
			foreach (SupportingTokenParameters supportingTokenParameters in binding.OperationSupportingTokenParameters.Values)
			{
				if (supportingTokenParameters.Endorsing.Count > 0 || supportingTokenParameters.SignedEndorsing.Count > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x001634EC File Offset: 0x001616EC
		private static void ExportSymmetricSecurityBindingElement(SymmetricSecurityBindingElement binding, MetadataExporter exporter, PolicyConversionContext policyContext)
		{
			WSSecurityPolicy securityPolicyDriver = WSSecurityPolicy.GetSecurityPolicyDriver(binding.MessageSecurityVersion);
			SecurityBindingElement.AddAssertionIfNotNull(policyContext, securityPolicyDriver.CreateWsspSymmetricBindingAssertion(exporter, policyContext, binding));
			SecurityBindingElement.AddAssertionIfNotNull(policyContext, securityPolicyDriver.CreateWsspSupportingTokensAssertion(exporter, binding.EndpointSupportingTokenParameters.Signed, binding.EndpointSupportingTokenParameters.SignedEncrypted, binding.EndpointSupportingTokenParameters.Endorsing, binding.EndpointSupportingTokenParameters.SignedEndorsing, binding.OptionalEndpointSupportingTokenParameters.Signed, binding.OptionalEndpointSupportingTokenParameters.SignedEncrypted, binding.OptionalEndpointSupportingTokenParameters.Endorsing, binding.OptionalEndpointSupportingTokenParameters.SignedEndorsing));
			SecurityBindingElement.AddAssertionIfNotNull(policyContext, securityPolicyDriver.CreateWsspWssAssertion(exporter, binding));
			if (SecurityBindingElement.RequiresWsspTrust(binding))
			{
				SecurityBindingElement.AddAssertionIfNotNull(policyContext, securityPolicyDriver.CreateWsspTrustAssertion(exporter, binding.KeyEntropyMode));
			}
		}

		// Token: 0x06005F76 RID: 24438 RVA: 0x001635A4 File Offset: 0x001617A4
		private static void ExportMessageScopeProtectionPolicy(SecurityBindingElement security, MetadataExporter exporter, PolicyConversionContext policyContext)
		{
			ChannelProtectionRequirements channelProtectionRequirements = SecurityBindingElement.ComputeProtectionRequirements(security, new BindingParameterCollection
			{
				ChannelProtectionRequirements.CreateFromContract(policyContext.Contract, policyContext.BindingElements.Find<SecurityBindingElement>().GetIndividualProperty<ISecurityCapabilities>(), false)
			}, policyContext.BindingElements, true);
			channelProtectionRequirements.MakeReadOnly();
			WSSecurityPolicy securityPolicyDriver = WSSecurityPolicy.GetSecurityPolicyDriver(security.MessageSecurityVersion);
			foreach (OperationDescription operationDescription in policyContext.Contract.Operations)
			{
				foreach (MessageDescription messageDescription in operationDescription.Messages)
				{
					ScopedMessagePartSpecification scopedMessagePartSpecification;
					if (messageDescription.Direction == MessageDirection.Input)
					{
						scopedMessagePartSpecification = channelProtectionRequirements.IncomingSignatureParts;
					}
					else
					{
						scopedMessagePartSpecification = channelProtectionRequirements.OutgoingSignatureParts;
					}
					MessagePartSpecification parts;
					if (scopedMessagePartSpecification.TryGetParts(messageDescription.Action, out parts))
					{
						SecurityBindingElement.AddAssertionIfNotNull(policyContext, messageDescription, securityPolicyDriver.CreateWsspSignedPartsAssertion(parts));
					}
					if (messageDescription.Direction == MessageDirection.Input)
					{
						scopedMessagePartSpecification = channelProtectionRequirements.IncomingEncryptionParts;
					}
					else
					{
						scopedMessagePartSpecification = channelProtectionRequirements.OutgoingEncryptionParts;
					}
					if (scopedMessagePartSpecification.TryGetParts(messageDescription.Action, out parts))
					{
						SecurityBindingElement.AddAssertionIfNotNull(policyContext, messageDescription, securityPolicyDriver.CreateWsspEncryptedPartsAssertion(parts));
					}
				}
				foreach (FaultDescription faultDescription in operationDescription.Faults)
				{
					MessagePartSpecification parts2;
					if (channelProtectionRequirements.OutgoingSignatureParts.TryGetParts(faultDescription.Action, out parts2))
					{
						SecurityBindingElement.AddAssertionIfNotNull(policyContext, faultDescription, securityPolicyDriver.CreateWsspSignedPartsAssertion(parts2));
					}
					if (channelProtectionRequirements.OutgoingEncryptionParts.TryGetParts(faultDescription.Action, out parts2))
					{
						SecurityBindingElement.AddAssertionIfNotNull(policyContext, faultDescription, securityPolicyDriver.CreateWsspEncryptedPartsAssertion(parts2));
					}
				}
			}
		}

		// Token: 0x06005F77 RID: 24439 RVA: 0x001637A8 File Offset: 0x001619A8
		private static void ExportOperationScopeSupportingTokensPolicy(SecurityBindingElement binding, MetadataExporter exporter, PolicyConversionContext policyContext)
		{
			WSSecurityPolicy securityPolicyDriver = WSSecurityPolicy.GetSecurityPolicyDriver(binding.MessageSecurityVersion);
			if (binding.OperationSupportingTokenParameters.Count == 0 && binding.OptionalOperationSupportingTokenParameters.Count == 0)
			{
				return;
			}
			foreach (OperationDescription operationDescription in policyContext.Contract.Operations)
			{
				foreach (MessageDescription messageDescription in operationDescription.Messages)
				{
					if (messageDescription.Direction == MessageDirection.Input)
					{
						SupportingTokenParameters supportingTokenParameters = null;
						SupportingTokenParameters supportingTokenParameters2 = null;
						if (binding.OperationSupportingTokenParameters.ContainsKey(messageDescription.Action))
						{
							supportingTokenParameters = binding.OperationSupportingTokenParameters[messageDescription.Action];
						}
						if (binding.OptionalOperationSupportingTokenParameters.ContainsKey(messageDescription.Action))
						{
							supportingTokenParameters2 = binding.OptionalOperationSupportingTokenParameters[messageDescription.Action];
						}
						if (supportingTokenParameters != null || supportingTokenParameters2 != null)
						{
							SecurityBindingElement.AddAssertionIfNotNull(policyContext, operationDescription, securityPolicyDriver.CreateWsspSupportingTokensAssertion(exporter, (supportingTokenParameters == null) ? null : supportingTokenParameters.Signed, (supportingTokenParameters == null) ? null : supportingTokenParameters.SignedEncrypted, (supportingTokenParameters == null) ? null : supportingTokenParameters.Endorsing, (supportingTokenParameters == null) ? null : supportingTokenParameters.SignedEndorsing, (supportingTokenParameters2 == null) ? null : supportingTokenParameters2.Signed, (supportingTokenParameters2 == null) ? null : supportingTokenParameters2.SignedEncrypted, (supportingTokenParameters2 == null) ? null : supportingTokenParameters2.Endorsing, (supportingTokenParameters2 == null) ? null : supportingTokenParameters2.SignedEndorsing));
						}
					}
				}
			}
		}

		// Token: 0x0400381C RID: 14364
		internal const string defaultAlgorithmSuiteString = "Default";

		// Token: 0x0400381D RID: 14365
		internal static readonly SecurityAlgorithmSuite defaultDefaultAlgorithmSuite = SecurityAlgorithmSuite.Default;

		// Token: 0x0400381E RID: 14366
		internal const bool defaultIncludeTimestamp = true;

		// Token: 0x0400381F RID: 14367
		internal const bool defaultAllowInsecureTransport = false;

		// Token: 0x04003820 RID: 14368
		internal const MessageProtectionOrder defaultMessageProtectionOrder = MessageProtectionOrder.SignBeforeEncryptAndEncryptSignature;

		// Token: 0x04003821 RID: 14369
		internal const bool defaultRequireSignatureConfirmation = false;

		// Token: 0x04003822 RID: 14370
		internal const bool defaultEnableUnsecuredResponse = false;

		// Token: 0x04003823 RID: 14371
		internal const bool defaultProtectTokens = false;

		// Token: 0x04003824 RID: 14372
		private SecurityAlgorithmSuite defaultAlgorithmSuite;

		// Token: 0x04003825 RID: 14373
		private SupportingTokenParameters endpointSupportingTokenParameters;

		// Token: 0x04003826 RID: 14374
		private SupportingTokenParameters optionalEndpointSupportingTokenParameters;

		// Token: 0x04003827 RID: 14375
		private bool includeTimestamp;

		// Token: 0x04003828 RID: 14376
		private SecurityKeyEntropyMode keyEntropyMode;

		// Token: 0x04003829 RID: 14377
		private Dictionary<string, SupportingTokenParameters> operationSupportingTokenParameters;

		// Token: 0x0400382A RID: 14378
		private Dictionary<string, SupportingTokenParameters> optionalOperationSupportingTokenParameters;

		// Token: 0x0400382B RID: 14379
		private LocalClientSecuritySettings localClientSettings;

		// Token: 0x0400382C RID: 14380
		private LocalServiceSecuritySettings localServiceSettings;

		// Token: 0x0400382D RID: 14381
		private MessageSecurityVersion messageSecurityVersion;

		// Token: 0x0400382E RID: 14382
		private SecurityHeaderLayout securityHeaderLayout;

		// Token: 0x0400382F RID: 14383
		private InternalDuplexBindingElement internalDuplexBindingElement;

		// Token: 0x04003830 RID: 14384
		private long maxReceivedMessageSize = 65536L;

		// Token: 0x04003831 RID: 14385
		private XmlDictionaryReaderQuotas readerQuotas;

		// Token: 0x04003832 RID: 14386
		private bool doNotEmitTrust;

		// Token: 0x04003833 RID: 14387
		private bool supportsExtendedProtectionPolicy;

		// Token: 0x04003834 RID: 14388
		private bool allowInsecureTransport;

		// Token: 0x04003835 RID: 14389
		private bool enableUnsecuredResponse;

		// Token: 0x04003836 RID: 14390
		private bool protectTokens;
	}
}
