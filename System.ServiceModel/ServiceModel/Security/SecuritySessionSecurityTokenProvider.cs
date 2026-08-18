using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.Net;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000320 RID: 800
	internal class SecuritySessionSecurityTokenProvider : CommunicationObjectSecurityTokenProvider
	{
		// Token: 0x06001BE4 RID: 7140 RVA: 0x00069108 File Offset: 0x00067308
		public SecuritySessionSecurityTokenProvider(SafeFreeCredentials credentialsHandle)
		{
			this.credentialsHandle = credentialsHandle;
			this.standardsManager = SecurityStandardsManager.DefaultInstance;
			this.keyEntropyMode = SecurityKeyEntropyMode.CombinedEntropy;
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x00069134 File Offset: 0x00067334
		// (set) Token: 0x06001BE6 RID: 7142 RVA: 0x0006913C File Offset: 0x0006733C
		public WebHeaderCollection WebHeaders
		{
			get
			{
				return this.webHeaderCollection;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.webHeaderCollection = value;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x00069150 File Offset: 0x00067350
		// (set) Token: 0x06001BE8 RID: 7144 RVA: 0x00069158 File Offset: 0x00067358
		public SecurityAlgorithmSuite SecurityAlgorithmSuite
		{
			get
			{
				return this.securityAlgorithmSuite;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.securityAlgorithmSuite = value;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x0006916C File Offset: 0x0006736C
		// (set) Token: 0x06001BEA RID: 7146 RVA: 0x00069174 File Offset: 0x00067374
		public SecurityKeyEntropyMode KeyEntropyMode
		{
			get
			{
				return this.keyEntropyMode;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				SecurityKeyEntropyModeHelper.Validate(value);
				this.keyEntropyMode = value;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x0006918E File Offset: 0x0006738E
		private MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x00069196 File Offset: 0x00067396
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x0006919E File Offset: 0x0006739E
		public EndpointAddress TargetAddress
		{
			get
			{
				return this.targetAddress;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.targetAddress = value;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x000691B2 File Offset: 0x000673B2
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x000691BA File Offset: 0x000673BA
		public EndpointAddress LocalAddress
		{
			get
			{
				return this.localAddress;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.localAddress = value;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x000691CE File Offset: 0x000673CE
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x000691D6 File Offset: 0x000673D6
		public Uri Via
		{
			get
			{
				return this.via;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.via = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x000691EA File Offset: 0x000673EA
		// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x000691F2 File Offset: 0x000673F2
		public BindingContext IssuerBindingContext
		{
			get
			{
				return this.issuerBindingContext;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.issuerBindingContext = value.Clone();
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x0006921E File Offset: 0x0006741E
		// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x00069226 File Offset: 0x00067426
		public SecurityBindingElement BootstrapSecurityBindingElement
		{
			get
			{
				return this.bootstrapSecurityBindingElement;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.bootstrapSecurityBindingElement = (SecurityBindingElement)value.Clone();
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x00069257 File Offset: 0x00067457
		// (set) Token: 0x06001BF7 RID: 7159 RVA: 0x00069260 File Offset: 0x00067460
		public SecurityStandardsManager StandardsManager
		{
			get
			{
				return this.standardsManager;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				if (!value.TrustDriver.IsSessionSupported)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("TrustDriverVersionDoesNotSupportSession"), "value"));
				}
				if (!value.SecureConversationDriver.IsSessionSupported)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SecureConversationDriverVersionDoesNotSupportSession"), "value"));
				}
				this.standardsManager = value;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x000692EF File Offset: 0x000674EF
		// (set) Token: 0x06001BF9 RID: 7161 RVA: 0x000692F7 File Offset: 0x000674F7
		public SecurityTokenParameters IssuedSecurityTokenParameters
		{
			get
			{
				return this.issuedTokenParameters;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.issuedTokenParameters = value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x0006930B File Offset: 0x0006750B
		// (set) Token: 0x06001BFB RID: 7163 RVA: 0x00069313 File Offset: 0x00067513
		public Uri PrivacyNoticeUri
		{
			get
			{
				return this.privacyNoticeUri;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.privacyNoticeUri = value;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x00069327 File Offset: 0x00067527
		// (set) Token: 0x06001BFD RID: 7165 RVA: 0x0006932F File Offset: 0x0006752F
		public ChannelParameterCollection ChannelParameters
		{
			get
			{
				return this.channelParameters;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.channelParameters = value;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x00069343 File Offset: 0x00067543
		// (set) Token: 0x06001BFF RID: 7167 RVA: 0x0006934B File Offset: 0x0006754B
		public int PrivacyNoticeVersion
		{
			get
			{
				return this.privacyNoticeVersion;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.privacyNoticeVersion = value;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x0006935F File Offset: 0x0006755F
		public virtual XmlDictionaryString IssueAction
		{
			get
			{
				return this.standardsManager.SecureConversationDriver.IssueAction;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x00069371 File Offset: 0x00067571
		public virtual XmlDictionaryString IssueResponseAction
		{
			get
			{
				return this.standardsManager.SecureConversationDriver.IssueResponseAction;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x00069383 File Offset: 0x00067583
		public virtual XmlDictionaryString RenewAction
		{
			get
			{
				return this.standardsManager.SecureConversationDriver.RenewAction;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x00069395 File Offset: 0x00067595
		public virtual XmlDictionaryString RenewResponseAction
		{
			get
			{
				return this.standardsManager.SecureConversationDriver.RenewResponseAction;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x000693A7 File Offset: 0x000675A7
		public virtual XmlDictionaryString CloseAction
		{
			get
			{
				return this.standardsManager.SecureConversationDriver.CloseAction;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x000693B9 File Offset: 0x000675B9
		public virtual XmlDictionaryString CloseResponseAction
		{
			get
			{
				return this.standardsManager.SecureConversationDriver.CloseResponseAction;
			}
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x000693CB File Offset: 0x000675CB
		public override void OnAbort()
		{
			if (this.rstChannelFactory != null)
			{
				this.rstChannelFactory.Abort();
				this.rstChannelFactory = null;
			}
			this.FreeCredentialsHandle();
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000693F0 File Offset: 0x000675F0
		public override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.targetAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TargetAddressIsNotSet", new object[]
				{
					base.GetType()
				})));
			}
			if (this.IssuerBindingContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("IssuerBuildContextNotSet", new object[]
				{
					base.GetType()
				})));
			}
			if (this.IssuedSecurityTokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("IssuedSecurityTokenParametersNotSet", new object[]
				{
					base.GetType()
				})));
			}
			if (this.BootstrapSecurityBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BootstrapSecurityBindingElementNotSet", new object[]
				{
					base.GetType()
				})));
			}
			if (this.SecurityAlgorithmSuite == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityAlgorithmSuiteNotSet", new object[]
				{
					base.GetType()
				})));
			}
			this.InitializeFactories();
			this.rstChannelFactory.Open(timeoutHelper.RemainingTime());
			this.sctUri = this.StandardsManager.SecureConversationDriver.TokenTypeUri;
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x00069530 File Offset: 0x00067730
		public override void OnOpening()
		{
			base.OnOpening();
			if (this.credentialsHandle == null)
			{
				if (this.IssuerBindingContext == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("IssuerBuildContextNotSet", new object[]
					{
						base.GetType()
					})));
				}
				if (this.BootstrapSecurityBindingElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BootstrapSecurityBindingElementNotSet", new object[]
					{
						base.GetType()
					})));
				}
				this.credentialsHandle = SecurityUtils.GetCredentialsHandle(this.bootstrapSecurityBindingElement, this.issuerBindingContext);
				this.ownCredentialsHandle = true;
			}
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x000695D0 File Offset: 0x000677D0
		public override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.rstChannelFactory != null)
			{
				this.rstChannelFactory.Close(timeoutHelper.RemainingTime());
				this.rstChannelFactory = null;
			}
			this.FreeCredentialsHandle();
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x0006960C File Offset: 0x0006780C
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

		// Token: 0x06001C0B RID: 7179 RVA: 0x00069630 File Offset: 0x00067830
		private void InitializeFactories()
		{
			ISecurityCapabilities property = this.BootstrapSecurityBindingElement.GetProperty<ISecurityCapabilities>(this.IssuerBindingContext);
			SecurityCredentialsManager securityCredentialsManager = this.IssuerBindingContext.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				securityCredentialsManager = ClientCredentials.CreateDefaultCredentials();
			}
			BindingContext bindingContext = this.IssuerBindingContext;
			this.bootstrapSecurityBindingElement.ReaderQuotas = bindingContext.GetInnerProperty<XmlDictionaryReaderQuotas>();
			if (this.bootstrapSecurityBindingElement.ReaderQuotas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EncodingBindingElementDoesNotHandleReaderQuotas")));
			}
			TransportBindingElement transportBindingElement = bindingContext.RemainingBindingElements.Find<TransportBindingElement>();
			if (transportBindingElement != null)
			{
				this.bootstrapSecurityBindingElement.MaxReceivedMessageSize = transportBindingElement.MaxReceivedMessageSize;
			}
			SecurityProtocolFactory securityProtocolFactory = this.BootstrapSecurityBindingElement.CreateSecurityProtocolFactory<IRequestChannel>(this.IssuerBindingContext.Clone(), securityCredentialsManager, false, this.IssuerBindingContext.Clone());
			if (securityProtocolFactory is MessageSecurityProtocolFactory)
			{
				MessageSecurityProtocolFactory messageSecurityProtocolFactory = securityProtocolFactory as MessageSecurityProtocolFactory;
				messageSecurityProtocolFactory.ApplyConfidentiality = (messageSecurityProtocolFactory.ApplyIntegrity = (messageSecurityProtocolFactory.RequireConfidentiality = (messageSecurityProtocolFactory.RequireIntegrity = true)));
				messageSecurityProtocolFactory.ProtectionRequirements.IncomingSignatureParts.ChannelParts.IsBodyIncluded = true;
				messageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.ChannelParts.IsBodyIncluded = true;
				MessagePartSpecification parts = new MessagePartSpecification(true);
				messageSecurityProtocolFactory.ProtectionRequirements.IncomingSignatureParts.AddParts(parts, this.IssueAction);
				messageSecurityProtocolFactory.ProtectionRequirements.IncomingEncryptionParts.AddParts(parts, this.IssueAction);
				messageSecurityProtocolFactory.ProtectionRequirements.IncomingSignatureParts.AddParts(parts, this.RenewAction);
				messageSecurityProtocolFactory.ProtectionRequirements.IncomingEncryptionParts.AddParts(parts, this.RenewAction);
				messageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, this.IssueResponseAction);
				messageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(parts, this.IssueResponseAction);
				messageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, this.RenewResponseAction);
				messageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(parts, this.RenewResponseAction);
			}
			securityProtocolFactory.PrivacyNoticeUri = this.PrivacyNoticeUri;
			securityProtocolFactory.PrivacyNoticeVersion = this.privacyNoticeVersion;
			if (this.localAddress != null)
			{
				MessageFilter filter = new SessionActionFilter(this.standardsManager, new string[]
				{
					this.IssueResponseAction.Value,
					this.RenewResponseAction.Value
				});
				bindingContext.BindingParameters.Add(new LocalAddressProvider(this.localAddress, filter));
			}
			ChannelBuilder channelBuilder = new ChannelBuilder(bindingContext, true);
			IChannelFactory<IRequestChannel> innerChannelFactory;
			if (channelBuilder.CanBuildChannelFactory<IRequestChannel>())
			{
				innerChannelFactory = channelBuilder.BuildChannelFactory<IRequestChannel>();
				this.requiresManualReplyAddressing = true;
			}
			else
			{
				ServiceChannelFactory serviceChannelFactory = ServiceChannelFactory.BuildChannelFactory(channelBuilder, new ClientRuntime("RequestSecuritySession", "http://tempuri.org/")
				{
					UseSynchronizationContext = false,
					AddTransactionFlowProperties = false,
					ValidateMustUnderstand = false
				});
				ClientOperation clientOperation = new ClientOperation(serviceChannelFactory.ClientRuntime, "Issue", this.IssueAction.Value);
				clientOperation.Formatter = SecuritySessionSecurityTokenProvider.operationFormatter;
				serviceChannelFactory.ClientRuntime.Operations.Add(clientOperation);
				ClientOperation clientOperation2 = new ClientOperation(serviceChannelFactory.ClientRuntime, "Renew", this.RenewAction.Value);
				clientOperation2.Formatter = SecuritySessionSecurityTokenProvider.operationFormatter;
				serviceChannelFactory.ClientRuntime.Operations.Add(clientOperation2);
				innerChannelFactory = new SecuritySessionSecurityTokenProvider.RequestChannelFactory(serviceChannelFactory);
				this.requiresManualReplyAddressing = false;
			}
			SecurityChannelFactory<IRequestChannel> securityChannelFactory = new SecurityChannelFactory<IRequestChannel>(property, this.IssuerBindingContext, channelBuilder, securityProtocolFactory, innerChannelFactory);
			if (transportBindingElement != null && securityChannelFactory.SecurityProtocolFactory != null)
			{
				securityChannelFactory.SecurityProtocolFactory.ExtendedProtectionPolicy = transportBindingElement.GetProperty<ExtendedProtectionPolicy>(bindingContext);
			}
			this.rstChannelFactory = securityChannelFactory;
			this.messageVersion = securityChannelFactory.MessageVersion;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x000699C8 File Offset: 0x00067BC8
		protected override IAsyncResult BeginGetTokenCore(TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			return new SecuritySessionSecurityTokenProvider.SessionOperationAsyncResult(this, SecuritySessionOperation.Issue, this.TargetAddress, this.Via, null, timeout, callback, state);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x000699EC File Offset: 0x00067BEC
		protected override SecurityToken EndGetTokenCore(IAsyncResult result)
		{
			return SecuritySessionSecurityTokenProvider.SessionOperationAsyncResult.End(result);
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x000699F4 File Offset: 0x00067BF4
		protected override SecurityToken GetTokenCore(TimeSpan timeout)
		{
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			return this.DoOperation(SecuritySessionOperation.Issue, this.targetAddress, this.via, null, timeout);
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00069A16 File Offset: 0x00067C16
		protected override IAsyncResult BeginRenewTokenCore(TimeSpan timeout, SecurityToken tokenToBeRenewed, AsyncCallback callback, object state)
		{
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			return new SecuritySessionSecurityTokenProvider.SessionOperationAsyncResult(this, SecuritySessionOperation.Renew, this.TargetAddress, this.Via, tokenToBeRenewed, timeout, callback, state);
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00069A3B File Offset: 0x00067C3B
		protected override SecurityToken EndRenewTokenCore(IAsyncResult result)
		{
			return SecuritySessionSecurityTokenProvider.SessionOperationAsyncResult.End(result);
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00069A43 File Offset: 0x00067C43
		protected override SecurityToken RenewTokenCore(TimeSpan timeout, SecurityToken tokenToBeRenewed)
		{
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			return this.DoOperation(SecuritySessionOperation.Renew, this.targetAddress, this.via, tokenToBeRenewed, timeout);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00069A68 File Offset: 0x00067C68
		private IRequestChannel CreateChannel(SecuritySessionOperation operation, EndpointAddress target, Uri via)
		{
			if (operation == SecuritySessionOperation.Issue || operation == SecuritySessionOperation.Renew)
			{
				IChannelFactory<IRequestChannel> channelFactory = this.rstChannelFactory;
				IRequestChannel requestChannel;
				if (via != null)
				{
					requestChannel = channelFactory.CreateChannel(target, via);
				}
				else
				{
					requestChannel = channelFactory.CreateChannel(target);
				}
				if (this.channelParameters != null)
				{
					this.channelParameters.PropagateChannelParameters(requestChannel);
				}
				if (this.ownCredentialsHandle)
				{
					ChannelParameterCollection property = requestChannel.GetProperty<ChannelParameterCollection>();
					if (property != null)
					{
						property.Add(new SspiIssuanceChannelParameter(true, this.credentialsHandle));
					}
				}
				return requestChannel;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00069AEB File Offset: 0x00067CEB
		private Message CreateRequest(SecuritySessionOperation operation, EndpointAddress target, SecurityToken currentToken, out object requestState)
		{
			if (operation == SecuritySessionOperation.Issue)
			{
				return this.CreateIssueRequest(target, out requestState);
			}
			if (operation == SecuritySessionOperation.Renew)
			{
				return this.CreateRenewRequest(target, currentToken, out requestState);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x00069B1C File Offset: 0x00067D1C
		private GenericXmlSecurityToken ProcessReply(Message reply, SecuritySessionOperation operation, object requestState)
		{
			SecuritySessionSecurityTokenProvider.ThrowIfFault(reply, this.targetAddress);
			GenericXmlSecurityToken result = null;
			if (operation == SecuritySessionOperation.Issue)
			{
				result = this.ProcessIssueResponse(reply, requestState);
			}
			else if (operation == SecuritySessionOperation.Renew)
			{
				result = this.ProcessRenewResponse(reply, requestState);
			}
			return result;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00069B54 File Offset: 0x00067D54
		private void OnOperationSuccess(SecuritySessionOperation operation, EndpointAddress target, SecurityToken issuedToken, SecurityToken currentToken)
		{
			SecurityTraceRecordHelper.TraceSecuritySessionOperationSuccess(operation, target, currentToken, issuedToken);
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00069B60 File Offset: 0x00067D60
		private void OnOperationFailure(SecuritySessionOperation operation, EndpointAddress target, SecurityToken currentToken, Exception e, IChannel channel)
		{
			SecurityTraceRecordHelper.TraceSecuritySessionOperationFailure(operation, target, currentToken, e);
			if (channel != null)
			{
				channel.Abort();
			}
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00069B78 File Offset: 0x00067D78
		private GenericXmlSecurityToken DoOperation(SecuritySessionOperation operation, EndpointAddress target, Uri via, SecurityToken currentToken, TimeSpan timeout)
		{
			if (target == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("target");
			}
			if (operation == SecuritySessionOperation.Renew && currentToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("currentToken");
			}
			IRequestChannel requestChannel = null;
			GenericXmlSecurityToken result;
			try
			{
				SecurityTraceRecordHelper.TraceBeginSecuritySessionOperation(operation, target, currentToken);
				requestChannel = this.CreateChannel(operation, target, via);
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				requestChannel.Open(timeoutHelper.RemainingTime());
				object requestState;
				GenericXmlSecurityToken genericXmlSecurityToken;
				using (Message message = this.CreateRequest(operation, target, currentToken, out requestState))
				{
					EventTraceActivity eventTraceActivity = null;
					if (TD.MessageReceivedFromTransportIsEnabled())
					{
						eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
					}
					TraceUtility.ProcessOutgoingMessage(message, eventTraceActivity);
					using (Message message2 = requestChannel.Request(message, timeoutHelper.RemainingTime()))
					{
						if (message2 == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("FailToRecieveReplyFromNegotiation")));
						}
						if (eventTraceActivity == null && TD.MessageReceivedFromTransportIsEnabled())
						{
							eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message2);
						}
						TraceUtility.ProcessIncomingMessage(message2, eventTraceActivity);
						SecuritySessionSecurityTokenProvider.ThrowIfFault(message2, this.targetAddress);
						genericXmlSecurityToken = this.ProcessReply(message2, operation, requestState);
						this.ValidateKeySize(genericXmlSecurityToken);
					}
				}
				requestChannel.Close(timeoutHelper.RemainingTime());
				this.OnOperationSuccess(operation, target, genericXmlSecurityToken, currentToken);
				result = genericXmlSecurityToken;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (ex is TimeoutException)
				{
					ex = new TimeoutException(SR.GetString("ClientSecuritySessionRequestTimeout", new object[]
					{
						timeout
					}), ex);
				}
				this.OnOperationFailure(operation, target, currentToken, ex, requestChannel);
				throw;
			}
			return result;
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00069D24 File Offset: 0x00067F24
		private byte[] GenerateEntropy(int entropySize)
		{
			byte[] array = DiagnosticUtility.Utility.AllocateByteArray(entropySize / 8);
			CryptoHelper.FillRandomBytes(array);
			return array;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00069D48 File Offset: 0x00067F48
		private RequestSecurityToken CreateRst(EndpointAddress target, out object requestState)
		{
			RequestSecurityToken requestSecurityToken = new RequestSecurityToken(this.standardsManager);
			requestSecurityToken.KeySize = this.SecurityAlgorithmSuite.DefaultSymmetricKeyLength;
			requestSecurityToken.TokenType = this.sctUri;
			if (this.KeyEntropyMode == SecurityKeyEntropyMode.ClientEntropy || this.KeyEntropyMode == SecurityKeyEntropyMode.CombinedEntropy)
			{
				byte[] array = this.GenerateEntropy(requestSecurityToken.KeySize);
				requestSecurityToken.SetRequestorEntropy(array);
				requestState = array;
			}
			else
			{
				requestState = null;
			}
			return requestSecurityToken;
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00069DAC File Offset: 0x00067FAC
		private void PrepareRequest(Message message)
		{
			RequestReplyCorrelator.PrepareRequest(message);
			if (this.requiresManualReplyAddressing)
			{
				if (this.localAddress != null)
				{
					message.Headers.ReplyTo = this.LocalAddress;
				}
				else
				{
					message.Headers.ReplyTo = EndpointAddress.AnonymousAddress;
				}
			}
			if (this.webHeaderCollection != null && this.webHeaderCollection.Count > 0)
			{
				object obj = null;
				HttpRequestMessageProperty httpRequestMessageProperty;
				if (message.Properties.TryGetValue(HttpRequestMessageProperty.Name, out obj))
				{
					httpRequestMessageProperty = (obj as HttpRequestMessageProperty);
				}
				else
				{
					httpRequestMessageProperty = new HttpRequestMessageProperty();
					message.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessageProperty);
				}
				if (httpRequestMessageProperty != null && httpRequestMessageProperty.Headers != null)
				{
					httpRequestMessageProperty.Headers.Add(this.webHeaderCollection);
				}
			}
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00069E64 File Offset: 0x00068064
		protected virtual Message CreateIssueRequest(EndpointAddress target, out object requestState)
		{
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			RequestSecurityToken requestSecurityToken = this.CreateRst(target, out requestState);
			requestSecurityToken.RequestType = this.StandardsManager.TrustDriver.RequestTypeIssue;
			requestSecurityToken.MakeReadOnly();
			Message message = Message.CreateMessage(this.MessageVersion, ActionHeader.Create(this.IssueAction, this.MessageVersion.Addressing), requestSecurityToken);
			this.PrepareRequest(message);
			return message;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00069ECC File Offset: 0x000680CC
		private GenericXmlSecurityToken ExtractToken(Message response, object requestState)
		{
			SecurityMessageProperty security = response.Properties.Security;
			ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies;
			if (security != null && security.ServiceSecurityContext != null)
			{
				authorizationPolicies = security.ServiceSecurityContext.AuthorizationPolicies;
			}
			else
			{
				authorizationPolicies = EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
			}
			RequestSecurityTokenResponse requestSecurityTokenResponse = null;
			XmlDictionaryReader readerAtBodyContents = response.GetReaderAtBodyContents();
			using (readerAtBodyContents)
			{
				if (this.StandardsManager.MessageSecurityVersion.TrustVersion != TrustVersion.WSTrustFeb2005)
				{
					if (this.StandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrust13)
					{
						RequestSecurityTokenResponseCollection requestSecurityTokenResponseCollection = this.StandardsManager.TrustDriver.CreateRequestSecurityTokenResponseCollection(readerAtBodyContents);
						using (IEnumerator<RequestSecurityTokenResponse> enumerator = requestSecurityTokenResponseCollection.RstrCollection.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								RequestSecurityTokenResponse requestSecurityTokenResponse2 = enumerator.Current;
								if (requestSecurityTokenResponse != null)
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("MoreThanOneRSTRInRSTRC")));
								}
								requestSecurityTokenResponse = requestSecurityTokenResponse2;
							}
							goto IL_EF;
						}
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}
				requestSecurityTokenResponse = this.StandardsManager.TrustDriver.CreateRequestSecurityTokenResponse(readerAtBodyContents);
				IL_EF:
				response.ReadFromBodyContentsToEnd(readerAtBodyContents);
			}
			byte[] requestorEntropy;
			if (requestState != null)
			{
				requestorEntropy = (byte[])requestState;
			}
			else
			{
				requestorEntropy = null;
			}
			return requestSecurityTokenResponse.GetIssuedToken(null, null, this.KeyEntropyMode, requestorEntropy, this.sctUri, authorizationPolicies, this.SecurityAlgorithmSuite.DefaultSymmetricKeyLength, false);
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x0006A030 File Offset: 0x00068230
		protected virtual GenericXmlSecurityToken ProcessIssueResponse(Message response, object requestState)
		{
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			return this.ExtractToken(response, requestState);
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x0006A048 File Offset: 0x00068248
		protected virtual Message CreateRenewRequest(EndpointAddress target, SecurityToken currentSessionToken, out object requestState)
		{
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			RequestSecurityToken requestSecurityToken = this.CreateRst(target, out requestState);
			requestSecurityToken.RequestType = this.StandardsManager.TrustDriver.RequestTypeRenew;
			requestSecurityToken.RenewTarget = this.IssuedSecurityTokenParameters.CreateKeyIdentifierClause(currentSessionToken, SecurityTokenReferenceStyle.External);
			requestSecurityToken.MakeReadOnly();
			Message message = Message.CreateMessage(this.MessageVersion, ActionHeader.Create(this.RenewAction, this.MessageVersion.Addressing), requestSecurityToken);
			SecurityMessageProperty securityMessageProperty = new SecurityMessageProperty();
			securityMessageProperty.OutgoingSupportingTokens.Add(new SupportingTokenSpecification(currentSessionToken, EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance, SecurityTokenAttachmentMode.Endorsing, this.IssuedSecurityTokenParameters));
			message.Properties.Security = securityMessageProperty;
			this.PrepareRequest(message);
			return message;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x0006A0F4 File Offset: 0x000682F4
		protected virtual GenericXmlSecurityToken ProcessRenewResponse(Message response, object requestState)
		{
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			if (response.Headers.Action != this.RenewResponseAction.Value)
			{
				throw TraceUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("InvalidRenewResponseAction", new object[]
				{
					response.Headers.Action
				})), response);
			}
			return this.ExtractToken(response, requestState);
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x0006A15B File Offset: 0x0006835B
		protected static void ThrowIfFault(Message message, EndpointAddress target)
		{
			SecurityUtils.ThrowIfNegotiationFault(message, target);
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x0006A164 File Offset: 0x00068364
		protected void ValidateKeySize(GenericXmlSecurityToken issuedToken)
		{
			base.CommunicationObject.ThrowIfClosedOrNotOpen();
			ReadOnlyCollection<SecurityKey> securityKeys = issuedToken.SecurityKeys;
			if (securityKeys == null || securityKeys.Count != 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("CannotObtainIssuedTokenKeySize")));
			}
			SymmetricSecurityKey symmetricSecurityKey = securityKeys[0] as SymmetricSecurityKey;
			if (symmetricSecurityKey == null)
			{
				return;
			}
			if (this.SecurityAlgorithmSuite.IsSymmetricKeyLengthSupported(symmetricSecurityKey.KeySize))
			{
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("InvalidIssuedTokenKeySize", new object[]
			{
				symmetricSecurityKey.KeySize
			})));
		}

		// Token: 0x04001DB0 RID: 7600
		private static readonly MessageOperationFormatter operationFormatter = new MessageOperationFormatter();

		// Token: 0x04001DB1 RID: 7601
		private BindingContext issuerBindingContext;

		// Token: 0x04001DB2 RID: 7602
		private IChannelFactory<IRequestChannel> rstChannelFactory;

		// Token: 0x04001DB3 RID: 7603
		private SecurityAlgorithmSuite securityAlgorithmSuite;

		// Token: 0x04001DB4 RID: 7604
		private SecurityStandardsManager standardsManager;

		// Token: 0x04001DB5 RID: 7605
		private object thisLock = new object();

		// Token: 0x04001DB6 RID: 7606
		private SecurityKeyEntropyMode keyEntropyMode;

		// Token: 0x04001DB7 RID: 7607
		private SecurityTokenParameters issuedTokenParameters;

		// Token: 0x04001DB8 RID: 7608
		private bool requiresManualReplyAddressing;

		// Token: 0x04001DB9 RID: 7609
		private EndpointAddress targetAddress;

		// Token: 0x04001DBA RID: 7610
		private SecurityBindingElement bootstrapSecurityBindingElement;

		// Token: 0x04001DBB RID: 7611
		private Uri via;

		// Token: 0x04001DBC RID: 7612
		private string sctUri;

		// Token: 0x04001DBD RID: 7613
		private Uri privacyNoticeUri;

		// Token: 0x04001DBE RID: 7614
		private int privacyNoticeVersion;

		// Token: 0x04001DBF RID: 7615
		private MessageVersion messageVersion;

		// Token: 0x04001DC0 RID: 7616
		private EndpointAddress localAddress;

		// Token: 0x04001DC1 RID: 7617
		private ChannelParameterCollection channelParameters;

		// Token: 0x04001DC2 RID: 7618
		private SafeFreeCredentials credentialsHandle;

		// Token: 0x04001DC3 RID: 7619
		private bool ownCredentialsHandle;

		// Token: 0x04001DC4 RID: 7620
		private WebHeaderCollection webHeaderCollection;

		// Token: 0x02000B73 RID: 2931
		private class SessionOperationAsyncResult : AsyncResult
		{
			// Token: 0x06007292 RID: 29330 RVA: 0x001ABC48 File Offset: 0x001A9E48
			public SessionOperationAsyncResult(SecuritySessionSecurityTokenProvider requestor, SecuritySessionOperation operation, EndpointAddress target, Uri via, SecurityToken currentToken, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.requestor = requestor;
				this.operation = operation;
				this.target = target;
				this.via = via;
				this.currentToken = currentToken;
				this.timeoutHelper = new TimeoutHelper(timeout);
				SecurityTraceRecordHelper.TraceBeginSecuritySessionOperation(operation, target, currentToken);
				bool flag = false;
				try
				{
					flag = this.StartOperation();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.OnOperationFailure(ex);
					throw;
				}
				if (flag)
				{
					this.OnOperationComplete();
					base.Complete(true);
				}
			}

			// Token: 0x06007293 RID: 29331 RVA: 0x001ABCDC File Offset: 0x001A9EDC
			private bool StartOperation()
			{
				this.channel = this.requestor.CreateChannel(this.operation, this.target, this.via);
				IAsyncResult asyncResult = this.channel.BeginOpen(this.timeoutHelper.RemainingTime(), SecuritySessionSecurityTokenProvider.SessionOperationAsyncResult.openChannelCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return false;
				}
				this.channel.EndOpen(asyncResult);
				return this.OnChannelOpened();
			}

			// Token: 0x06007294 RID: 29332 RVA: 0x001ABD48 File Offset: 0x001A9F48
			private static void OpenChannelCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				SecuritySessionSecurityTokenProvider.SessionOperationAsyncResult sessionOperationAsyncResult = (SecuritySessionSecurityTokenProvider.SessionOperationAsyncResult)result.AsyncState;
				bool flag = false;
				Exception ex = null;
				try
				{
					sessionOperationAsyncResult.channel.EndOpen(result);
					flag = sessionOperationAsyncResult.OnChannelOpened();
					if (flag)
					{
						sessionOperationAsyncResult.OnOperationComplete();
					}
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					flag = true;
					ex = ex2;
					sessionOperationAsyncResult.OnOperationFailure(ex);
				}
				if (flag)
				{
					sessionOperationAsyncResult.Complete(false, ex);
				}
			}

			// Token: 0x06007295 RID: 29333 RVA: 0x001ABDC0 File Offset: 0x001A9FC0
			private bool OnChannelOpened()
			{
				object requestState;
				Message message = this.requestor.CreateRequest(this.operation, this.target, this.currentToken, out requestState);
				if (message == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NullSessionRequestMessage", new object[]
					{
						this.operation.ToString()
					})));
				}
				SecuritySessionSecurityTokenProvider.ChannelOpenAsyncResultWrapper channelOpenAsyncResultWrapper = new SecuritySessionSecurityTokenProvider.ChannelOpenAsyncResultWrapper();
				channelOpenAsyncResultWrapper.Message = message;
				channelOpenAsyncResultWrapper.RequestState = requestState;
				bool flag = true;
				bool result;
				try
				{
					IAsyncResult asyncResult = this.channel.BeginRequest(message, this.timeoutHelper.RemainingTime(), Fx.ThunkCallback(new AsyncCallback(this.RequestCallback)), channelOpenAsyncResultWrapper);
					if (!asyncResult.CompletedSynchronously)
					{
						flag = false;
						result = false;
					}
					else
					{
						Message reply = this.channel.EndRequest(asyncResult);
						result = this.OnReplyReceived(reply, requestState);
					}
				}
				finally
				{
					if (flag)
					{
						channelOpenAsyncResultWrapper.Message.Close();
					}
				}
				return result;
			}

			// Token: 0x06007296 RID: 29334 RVA: 0x001ABEB4 File Offset: 0x001AA0B4
			private void RequestCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				SecuritySessionSecurityTokenProvider.ChannelOpenAsyncResultWrapper channelOpenAsyncResultWrapper = (SecuritySessionSecurityTokenProvider.ChannelOpenAsyncResultWrapper)result.AsyncState;
				object requestState = channelOpenAsyncResultWrapper.RequestState;
				bool flag = false;
				Exception exception = null;
				try
				{
					Message reply = this.channel.EndRequest(result);
					flag = this.OnReplyReceived(reply, requestState);
					if (flag)
					{
						this.OnOperationComplete();
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
					this.OnOperationFailure(ex);
				}
				finally
				{
					if (channelOpenAsyncResultWrapper.Message != null)
					{
						channelOpenAsyncResultWrapper.Message.Close();
					}
				}
				if (flag)
				{
					base.Complete(false, exception);
				}
			}

			// Token: 0x06007297 RID: 29335 RVA: 0x001ABF5C File Offset: 0x001AA15C
			private bool OnReplyReceived(Message reply, object requestState)
			{
				if (reply == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("FailToRecieveReplyFromNegotiation")));
				}
				try
				{
					this.issuedToken = this.requestor.ProcessReply(reply, this.operation, requestState);
					this.requestor.ValidateKeySize(this.issuedToken);
				}
				finally
				{
					if (reply != null)
					{
						((IDisposable)reply).Dispose();
					}
				}
				return this.OnReplyProcessed();
			}

			// Token: 0x06007298 RID: 29336 RVA: 0x001ABFD4 File Offset: 0x001AA1D4
			private bool OnReplyProcessed()
			{
				IAsyncResult asyncResult = this.channel.BeginClose(this.timeoutHelper.RemainingTime(), SecuritySessionSecurityTokenProvider.SessionOperationAsyncResult.closeChannelCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return false;
				}
				this.channel.EndClose(asyncResult);
				return true;
			}

			// Token: 0x06007299 RID: 29337 RVA: 0x001AC018 File Offset: 0x001AA218
			private static void CloseChannelCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				SecuritySessionSecurityTokenProvider.SessionOperationAsyncResult sessionOperationAsyncResult = (SecuritySessionSecurityTokenProvider.SessionOperationAsyncResult)result.AsyncState;
				Exception ex = null;
				try
				{
					sessionOperationAsyncResult.channel.EndClose(result);
					sessionOperationAsyncResult.OnOperationComplete();
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
					sessionOperationAsyncResult.OnOperationFailure(ex);
				}
				sessionOperationAsyncResult.Complete(false, ex);
			}

			// Token: 0x0600729A RID: 29338 RVA: 0x001AC080 File Offset: 0x001AA280
			private void OnOperationFailure(Exception e)
			{
				try
				{
					this.requestor.OnOperationFailure(this.operation, this.target, this.currentToken, e, this.channel);
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
			}

			// Token: 0x0600729B RID: 29339 RVA: 0x001AC0D0 File Offset: 0x001AA2D0
			private void OnOperationComplete()
			{
				this.requestor.OnOperationSuccess(this.operation, this.target, this.issuedToken, this.currentToken);
			}

			// Token: 0x0600729C RID: 29340 RVA: 0x001AC0F8 File Offset: 0x001AA2F8
			public static SecurityToken End(IAsyncResult result)
			{
				SecuritySessionSecurityTokenProvider.SessionOperationAsyncResult sessionOperationAsyncResult = AsyncResult.End<SecuritySessionSecurityTokenProvider.SessionOperationAsyncResult>(result);
				return sessionOperationAsyncResult.issuedToken;
			}

			// Token: 0x040040DA RID: 16602
			private static AsyncCallback openChannelCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionSecurityTokenProvider.SessionOperationAsyncResult.OpenChannelCallback));

			// Token: 0x040040DB RID: 16603
			private static AsyncCallback closeChannelCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionSecurityTokenProvider.SessionOperationAsyncResult.CloseChannelCallback));

			// Token: 0x040040DC RID: 16604
			private SecuritySessionSecurityTokenProvider requestor;

			// Token: 0x040040DD RID: 16605
			private SecuritySessionOperation operation;

			// Token: 0x040040DE RID: 16606
			private EndpointAddress target;

			// Token: 0x040040DF RID: 16607
			private Uri via;

			// Token: 0x040040E0 RID: 16608
			private SecurityToken currentToken;

			// Token: 0x040040E1 RID: 16609
			private GenericXmlSecurityToken issuedToken;

			// Token: 0x040040E2 RID: 16610
			private IRequestChannel channel;

			// Token: 0x040040E3 RID: 16611
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x02000B74 RID: 2932
		private class ChannelOpenAsyncResultWrapper
		{
			// Token: 0x040040E4 RID: 16612
			public object RequestState;

			// Token: 0x040040E5 RID: 16613
			public Message Message;
		}

		// Token: 0x02000B75 RID: 2933
		internal class RequestChannelFactory : ChannelFactoryBase<IRequestChannel>
		{
			// Token: 0x0600729F RID: 29343 RVA: 0x001AC148 File Offset: 0x001AA348
			public RequestChannelFactory(ServiceChannelFactory serviceChannelFactory)
			{
				this.serviceChannelFactory = serviceChannelFactory;
			}

			// Token: 0x060072A0 RID: 29344 RVA: 0x001AC157 File Offset: 0x001AA357
			protected override IRequestChannel OnCreateChannel(EndpointAddress address, Uri via)
			{
				return this.serviceChannelFactory.CreateChannel<IRequestChannel>(address, via);
			}

			// Token: 0x060072A1 RID: 29345 RVA: 0x001AC166 File Offset: 0x001AA366
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.serviceChannelFactory.BeginOpen(timeout, callback, state);
			}

			// Token: 0x060072A2 RID: 29346 RVA: 0x001AC176 File Offset: 0x001AA376
			protected override void OnEndOpen(IAsyncResult result)
			{
				this.serviceChannelFactory.EndOpen(result);
			}

			// Token: 0x060072A3 RID: 29347 RVA: 0x001AC184 File Offset: 0x001AA384
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new ChainedCloseAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), new ICommunicationObject[]
				{
					this.serviceChannelFactory
				});
			}

			// Token: 0x060072A4 RID: 29348 RVA: 0x001AC1C0 File Offset: 0x001AA3C0
			protected override void OnEndClose(IAsyncResult result)
			{
				ChainedAsyncResult.End(result);
			}

			// Token: 0x060072A5 RID: 29349 RVA: 0x001AC1C8 File Offset: 0x001AA3C8
			protected override void OnClose(TimeSpan timeout)
			{
				base.OnClose(timeout);
				this.serviceChannelFactory.Close(timeout);
			}

			// Token: 0x060072A6 RID: 29350 RVA: 0x001AC1DD File Offset: 0x001AA3DD
			protected override void OnOpen(TimeSpan timeout)
			{
				this.serviceChannelFactory.Open(timeout);
			}

			// Token: 0x060072A7 RID: 29351 RVA: 0x001AC1EB File Offset: 0x001AA3EB
			protected override void OnAbort()
			{
				this.serviceChannelFactory.Abort();
				base.OnAbort();
			}

			// Token: 0x060072A8 RID: 29352 RVA: 0x001AC1FE File Offset: 0x001AA3FE
			public override T GetProperty<T>()
			{
				return this.serviceChannelFactory.GetProperty<T>();
			}

			// Token: 0x040040E6 RID: 16614
			private ServiceChannelFactory serviceChannelFactory;
		}
	}
}
