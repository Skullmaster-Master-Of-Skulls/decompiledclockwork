using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002F1 RID: 753
	internal sealed class SecuritySessionClientSettings<TChannel> : IChannelSecureConversationSessionSettings, ISecurityCommunicationObject
	{
		// Token: 0x060018DD RID: 6365 RVA: 0x0005C608 File Offset: 0x0005A808
		public SecuritySessionClientSettings()
		{
			this.keyRenewalInterval = SecuritySessionClientSettings.defaultKeyRenewalInterval;
			this.keyRolloverInterval = SecuritySessionClientSettings.defaultKeyRolloverInterval;
			this.tolerateTransportFailures = true;
			this.communicationObject = new WrapperSecurityCommunicationObject(this);
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x0005C656 File Offset: 0x0005A856
		private IChannelFactory InnerChannelFactory
		{
			get
			{
				return this.innerChannelFactory;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x0005C65E File Offset: 0x0005A85E
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x0005C666 File Offset: 0x0005A866
		internal ChannelBuilder ChannelBuilder
		{
			get
			{
				return this.channelBuilder;
			}
			set
			{
				this.channelBuilder = value;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x0005C66F File Offset: 0x0005A86F
		private SecurityChannelFactory<TChannel> SecurityChannelFactory
		{
			get
			{
				return this.securityChannelFactory;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x0005C677 File Offset: 0x0005A877
		// (set) Token: 0x060018E3 RID: 6371 RVA: 0x0005C67F File Offset: 0x0005A87F
		public SecurityProtocolFactory SessionProtocolFactory
		{
			get
			{
				return this.sessionProtocolFactory;
			}
			set
			{
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.sessionProtocolFactory = value;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x0005C693 File Offset: 0x0005A893
		// (set) Token: 0x060018E5 RID: 6373 RVA: 0x0005C69B File Offset: 0x0005A89B
		public TimeSpan KeyRenewalInterval
		{
			get
			{
				return this.keyRenewalInterval;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("TimeSpanMustbeGreaterThanTimeSpanZero")));
				}
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.keyRenewalInterval = value;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x0005C6DB File Offset: 0x0005A8DB
		// (set) Token: 0x060018E7 RID: 6375 RVA: 0x0005C6E3 File Offset: 0x0005A8E3
		public TimeSpan KeyRolloverInterval
		{
			get
			{
				return this.keyRolloverInterval;
			}
			set
			{
				if (value <= TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("TimeSpanMustbeGreaterThanTimeSpanZero")));
				}
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.keyRolloverInterval = value;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x0005C723 File Offset: 0x0005A923
		// (set) Token: 0x060018E9 RID: 6377 RVA: 0x0005C72B File Offset: 0x0005A92B
		public bool TolerateTransportFailures
		{
			get
			{
				return this.tolerateTransportFailures;
			}
			set
			{
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.tolerateTransportFailures = value;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x0005C73F File Offset: 0x0005A93F
		// (set) Token: 0x060018EB RID: 6379 RVA: 0x0005C747 File Offset: 0x0005A947
		public bool CanRenewSession
		{
			get
			{
				return this.canRenewSession;
			}
			set
			{
				this.canRenewSession = value;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x0005C750 File Offset: 0x0005A950
		// (set) Token: 0x060018ED RID: 6381 RVA: 0x0005C758 File Offset: 0x0005A958
		public SecurityTokenParameters IssuedSecurityTokenParameters
		{
			get
			{
				return this.issuedTokenParameters;
			}
			set
			{
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.issuedTokenParameters = value;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x0005C76C File Offset: 0x0005A96C
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x0005C774 File Offset: 0x0005A974
		public SecurityStandardsManager SecurityStandardsManager
		{
			get
			{
				return this.standardsManager;
			}
			set
			{
				this.communicationObject.ThrowIfDisposedOrImmutable();
				this.standardsManager = value;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x0005C788 File Offset: 0x0005A988
		public TimeSpan DefaultOpenTimeout
		{
			get
			{
				return ServiceDefaults.OpenTimeout;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0005C78F File Offset: 0x0005A98F
		public TimeSpan DefaultCloseTimeout
		{
			get
			{
				return ServiceDefaults.CloseTimeout;
			}
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0005C798 File Offset: 0x0005A998
		internal IChannelFactory CreateInnerChannelFactory()
		{
			if (this.ChannelBuilder.CanBuildChannelFactory<IDuplexSessionChannel>())
			{
				return this.ChannelBuilder.BuildChannelFactory<IDuplexSessionChannel>();
			}
			if (this.ChannelBuilder.CanBuildChannelFactory<IDuplexChannel>())
			{
				return this.ChannelBuilder.BuildChannelFactory<IDuplexChannel>();
			}
			if (this.ChannelBuilder.CanBuildChannelFactory<IRequestChannel>())
			{
				return this.ChannelBuilder.BuildChannelFactory<IRequestChannel>();
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0005C7FF File Offset: 0x0005A9FF
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.communicationObject.BeginClose(timeout, callback, state);
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0005C80F File Offset: 0x0005AA0F
		public void EndClose(IAsyncResult result)
		{
			this.communicationObject.EndClose(result);
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0005C81D File Offset: 0x0005AA1D
		IAsyncResult ISecurityCommunicationObject.OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OperationWithTimeoutAsyncResult(new OperationWithTimeoutCallback(this.OnClose), timeout, callback, state);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0005C834 File Offset: 0x0005AA34
		IAsyncResult ISecurityCommunicationObject.OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OperationWithTimeoutAsyncResult(new OperationWithTimeoutCallback(this.OnOpen), timeout, callback, state);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0005C84B File Offset: 0x0005AA4B
		public void OnClosed()
		{
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0005C84D File Offset: 0x0005AA4D
		public void OnClosing()
		{
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0005C84F File Offset: 0x0005AA4F
		void ISecurityCommunicationObject.OnEndClose(IAsyncResult result)
		{
			OperationWithTimeoutAsyncResult.End(result);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0005C857 File Offset: 0x0005AA57
		void ISecurityCommunicationObject.OnEndOpen(IAsyncResult result)
		{
			OperationWithTimeoutAsyncResult.End(result);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0005C85F File Offset: 0x0005AA5F
		public void OnFaulted()
		{
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0005C861 File Offset: 0x0005AA61
		public void OnOpened()
		{
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0005C863 File Offset: 0x0005AA63
		public void OnOpening()
		{
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0005C865 File Offset: 0x0005AA65
		public void OnClose(TimeSpan timeout)
		{
			if (this.sessionProtocolFactory != null)
			{
				this.sessionProtocolFactory.Close(false, timeout);
			}
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0005C87C File Offset: 0x0005AA7C
		public void OnAbort()
		{
			if (this.sessionProtocolFactory != null)
			{
				this.sessionProtocolFactory.Close(true, TimeSpan.Zero);
			}
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0005C898 File Offset: 0x0005AA98
		public void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.sessionProtocolFactory == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecuritySessionProtocolFactoryShouldBeSetBeforeThisOperation")));
			}
			if (this.standardsManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityStandardsManagerNotSet", new object[]
				{
					base.GetType().ToString()
				})));
			}
			if (this.issuedTokenParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("IssuedSecurityTokenParametersNotSet", new object[]
				{
					base.GetType()
				})));
			}
			if (this.keyRenewalInterval < this.keyRolloverInterval)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("KeyRolloverGreaterThanKeyRenewal")));
			}
			this.issuedTokenRenewalThreshold = this.sessionProtocolFactory.SecurityBindingElement.LocalClientSettings.CookieRenewalThresholdPercentage;
			this.ConfigureSessionProtocolFactory();
			this.sessionProtocolFactory.Open(true, timeoutHelper.RemainingTime());
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0005C997 File Offset: 0x0005AB97
		internal void Close(TimeSpan timeout)
		{
			this.communicationObject.Close(timeout);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0005C9A5 File Offset: 0x0005ABA5
		internal void Abort()
		{
			this.communicationObject.Abort();
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0005C9B2 File Offset: 0x0005ABB2
		internal void Open(SecurityChannelFactory<TChannel> securityChannelFactory, IChannelFactory innerChannelFactory, ChannelBuilder channelBuilder, TimeSpan timeout)
		{
			this.securityChannelFactory = securityChannelFactory;
			this.innerChannelFactory = innerChannelFactory;
			this.channelBuilder = channelBuilder;
			this.communicationObject.Open(timeout);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0005C9D6 File Offset: 0x0005ABD6
		internal TChannel OnCreateChannel(EndpointAddress remoteAddress, Uri via)
		{
			return this.OnCreateChannel(remoteAddress, via, null);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0005C9E4 File Offset: 0x0005ABE4
		internal TChannel OnCreateChannel(EndpointAddress remoteAddress, Uri via, MessageFilter filter)
		{
			this.communicationObject.ThrowIfClosed();
			if (filter != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
			if (typeof(TChannel) == typeof(IRequestSessionChannel))
			{
				return (TChannel)((object)new SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel(this, remoteAddress, via));
			}
			if (typeof(TChannel) == typeof(IDuplexSessionChannel))
			{
				return (TChannel)((object)new SecuritySessionClientSettings<TChannel>.ClientSecurityDuplexSessionChannel(this, remoteAddress, via));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ChannelTypeNotSupported", new object[]
			{
				typeof(TChannel)
			}), "TChannel"));
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0005CA94 File Offset: 0x0005AC94
		private void ConfigureSessionProtocolFactory()
		{
			if (this.sessionProtocolFactory is SessionSymmetricMessageSecurityProtocolFactory)
			{
				AddressingVersion addressing = MessageVersion.Default.Addressing;
				if (this.channelBuilder != null)
				{
					MessageEncodingBindingElement messageEncodingBindingElement = this.channelBuilder.Binding.Elements.Find<MessageEncodingBindingElement>();
					if (messageEncodingBindingElement != null)
					{
						addressing = messageEncodingBindingElement.MessageVersion.Addressing;
					}
				}
				if (addressing != AddressingVersion.WSAddressing10 && addressing != AddressingVersion.WSAddressingAugust2004)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
					{
						addressing
					})));
				}
				SessionSymmetricMessageSecurityProtocolFactory sessionSymmetricMessageSecurityProtocolFactory = (SessionSymmetricMessageSecurityProtocolFactory)this.sessionProtocolFactory;
				if (!sessionSymmetricMessageSecurityProtocolFactory.ApplyIntegrity || !sessionSymmetricMessageSecurityProtocolFactory.RequireIntegrity)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecuritySessionRequiresMessageIntegrity")));
				}
				MessagePartSpecification parts = new MessagePartSpecification(true);
				sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, this.SecurityStandardsManager.SecureConversationDriver.CloseResponseAction);
				sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, this.SecurityStandardsManager.SecureConversationDriver.CloseAction);
				sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, addressing.FaultAction);
				sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, addressing.DefaultFaultAction);
				sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingSignatureParts.AddParts(parts, "http://schemas.microsoft.com/ws/2006/05/security/SecureConversationFault");
				sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.IncomingSignatureParts.AddParts(parts, this.SecurityStandardsManager.SecureConversationDriver.CloseAction);
				sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.IncomingSignatureParts.AddParts(parts, this.SecurityStandardsManager.SecureConversationDriver.CloseResponseAction);
				if (sessionSymmetricMessageSecurityProtocolFactory.ApplyConfidentiality)
				{
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.IncomingEncryptionParts.AddParts(MessagePartSpecification.NoParts, this.SecurityStandardsManager.SecureConversationDriver.CloseAction);
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.IncomingEncryptionParts.AddParts(MessagePartSpecification.NoParts, this.SecurityStandardsManager.SecureConversationDriver.CloseResponseAction);
				}
				if (sessionSymmetricMessageSecurityProtocolFactory.RequireConfidentiality)
				{
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(MessagePartSpecification.NoParts, this.SecurityStandardsManager.SecureConversationDriver.CloseResponseAction);
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(MessagePartSpecification.NoParts, this.SecurityStandardsManager.SecureConversationDriver.CloseAction);
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(parts, addressing.FaultAction);
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(parts, addressing.DefaultFaultAction);
					sessionSymmetricMessageSecurityProtocolFactory.ProtectionRequirements.OutgoingEncryptionParts.AddParts(parts, "http://schemas.microsoft.com/ws/2006/05/security/SecureConversationFault");
					return;
				}
				return;
			}
			else
			{
				if (this.sessionProtocolFactory is SessionSymmetricTransportSecurityProtocolFactory)
				{
					SessionSymmetricTransportSecurityProtocolFactory sessionSymmetricTransportSecurityProtocolFactory = (SessionSymmetricTransportSecurityProtocolFactory)this.sessionProtocolFactory;
					sessionSymmetricTransportSecurityProtocolFactory.AddTimestamp = true;
					sessionSymmetricTransportSecurityProtocolFactory.SecurityTokenParameters.RequireDerivedKeys = false;
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
		}

		// Token: 0x04001C64 RID: 7268
		private SecurityProtocolFactory sessionProtocolFactory;

		// Token: 0x04001C65 RID: 7269
		private TimeSpan keyRenewalInterval;

		// Token: 0x04001C66 RID: 7270
		private TimeSpan keyRolloverInterval;

		// Token: 0x04001C67 RID: 7271
		private bool tolerateTransportFailures;

		// Token: 0x04001C68 RID: 7272
		private SecurityChannelFactory<TChannel> securityChannelFactory;

		// Token: 0x04001C69 RID: 7273
		private IChannelFactory innerChannelFactory;

		// Token: 0x04001C6A RID: 7274
		private ChannelBuilder channelBuilder;

		// Token: 0x04001C6B RID: 7275
		private WrapperSecurityCommunicationObject communicationObject;

		// Token: 0x04001C6C RID: 7276
		private SecurityStandardsManager standardsManager;

		// Token: 0x04001C6D RID: 7277
		private SecurityTokenParameters issuedTokenParameters;

		// Token: 0x04001C6E RID: 7278
		private int issuedTokenRenewalThreshold;

		// Token: 0x04001C6F RID: 7279
		private bool canRenewSession = true;

		// Token: 0x04001C70 RID: 7280
		private object thisLock = new object();

		// Token: 0x02000B55 RID: 2901
		private abstract class ClientSecuritySessionChannel : ChannelBase
		{
			// Token: 0x0600713A RID: 28986 RVA: 0x001A5AC0 File Offset: 0x001A3CC0
			protected ClientSecuritySessionChannel(SecuritySessionClientSettings<TChannel> settings, EndpointAddress to, Uri via) : base(settings.SecurityChannelFactory)
			{
				this.settings = settings;
				this.to = to;
				this.via = via;
				this.keyRenewalCompletedEvent = new InterruptibleWaitObject(false);
				this.messageVersion = settings.SecurityChannelFactory.MessageVersion;
				this.channelParameters = new ChannelParameterCollection(this);
				this.InitializeChannelBinder();
				this.webHeaderCollection = new WebHeaderCollection();
			}

			// Token: 0x17001A62 RID: 6754
			// (get) Token: 0x0600713B RID: 28987 RVA: 0x001A5B40 File Offset: 0x001A3D40
			protected SecuritySessionClientSettings<TChannel> Settings
			{
				get
				{
					return this.settings;
				}
			}

			// Token: 0x17001A63 RID: 6755
			// (get) Token: 0x0600713C RID: 28988 RVA: 0x001A5B48 File Offset: 0x001A3D48
			protected IClientReliableChannelBinder ChannelBinder
			{
				get
				{
					return this.channelBinder;
				}
			}

			// Token: 0x17001A64 RID: 6756
			// (get) Token: 0x0600713D RID: 28989 RVA: 0x001A5B50 File Offset: 0x001A3D50
			public EndpointAddress RemoteAddress
			{
				get
				{
					return this.to;
				}
			}

			// Token: 0x17001A65 RID: 6757
			// (get) Token: 0x0600713E RID: 28990 RVA: 0x001A5B58 File Offset: 0x001A3D58
			public Uri Via
			{
				get
				{
					return this.via;
				}
			}

			// Token: 0x17001A66 RID: 6758
			// (get) Token: 0x0600713F RID: 28991 RVA: 0x001A5B60 File Offset: 0x001A3D60
			protected bool SendCloseHandshake
			{
				get
				{
					return this.sendCloseHandshake;
				}
			}

			// Token: 0x17001A67 RID: 6759
			// (get) Token: 0x06007140 RID: 28992 RVA: 0x001A5B68 File Offset: 0x001A3D68
			protected EndpointAddress InternalLocalAddress
			{
				get
				{
					if (this.channelBinder != null)
					{
						return this.channelBinder.LocalAddress;
					}
					return null;
				}
			}

			// Token: 0x17001A68 RID: 6760
			// (get) Token: 0x06007141 RID: 28993 RVA: 0x001A5B7F File Offset: 0x001A3D7F
			protected virtual bool CanDoSecurityCorrelation
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001A69 RID: 6761
			// (get) Token: 0x06007142 RID: 28994 RVA: 0x001A5B82 File Offset: 0x001A3D82
			public MessageVersion MessageVersion
			{
				get
				{
					return this.messageVersion;
				}
			}

			// Token: 0x17001A6A RID: 6762
			// (get) Token: 0x06007143 RID: 28995 RVA: 0x001A5B8A File Offset: 0x001A3D8A
			protected bool IsInputClosed
			{
				get
				{
					return this.isInputClosed;
				}
			}

			// Token: 0x17001A6B RID: 6763
			// (get) Token: 0x06007144 RID: 28996 RVA: 0x001A5B94 File Offset: 0x001A3D94
			protected bool IsOutputClosed
			{
				get
				{
					return this.isOutputClosed;
				}
			}

			// Token: 0x17001A6C RID: 6764
			// (get) Token: 0x06007145 RID: 28997
			protected abstract bool ExpectClose { get; }

			// Token: 0x17001A6D RID: 6765
			// (get) Token: 0x06007146 RID: 28998
			protected abstract string SessionId { get; }

			// Token: 0x06007147 RID: 28999 RVA: 0x001A5BA0 File Offset: 0x001A3DA0
			public override T GetProperty<T>()
			{
				if (typeof(T) == typeof(ChannelParameterCollection))
				{
					return this.channelParameters as T;
				}
				if (typeof(T) == typeof(FaultConverter) && this.channelBinder != null)
				{
					return new SecurityChannelFaultConverter(this.channelBinder.Channel) as T;
				}
				if (typeof(T) == typeof(WebHeaderCollection))
				{
					return (T)((object)this.webHeaderCollection);
				}
				T property = base.GetProperty<T>();
				if (property == null && this.channelBinder != null && this.channelBinder.Channel != null)
				{
					property = this.channelBinder.Channel.GetProperty<T>();
				}
				return property;
			}

			// Token: 0x06007148 RID: 29000
			protected abstract void InitializeSession(SecurityToken sessionToken);

			// Token: 0x06007149 RID: 29001 RVA: 0x001A5C74 File Offset: 0x001A3E74
			private void InitializeSecurityState(SecurityToken sessionToken)
			{
				this.InitializeSession(sessionToken);
				this.currentSessionToken = sessionToken;
				this.previousSessionToken = null;
				List<SecurityToken> list = new List<SecurityToken>(1);
				list.Add(sessionToken);
				((IInitiatorSecuritySessionProtocol)this.securityProtocol).SetIdentityCheckAuthenticator(new GenericXmlSecurityTokenAuthenticator());
				((IInitiatorSecuritySessionProtocol)this.securityProtocol).SetIncomingSessionTokens(list);
				((IInitiatorSecuritySessionProtocol)this.securityProtocol).SetOutgoingSessionToken(sessionToken);
				if (this.CanDoSecurityCorrelation)
				{
					((IInitiatorSecuritySessionProtocol)this.securityProtocol).ReturnCorrelationState = true;
				}
				this.keyRenewalTime = this.GetKeyRenewalTime(sessionToken);
			}

			// Token: 0x0600714A RID: 29002 RVA: 0x001A5D04 File Offset: 0x001A3F04
			private void SetupSessionTokenProvider()
			{
				InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = new InitiatorServiceModelSecurityTokenRequirement();
				this.Settings.IssuedSecurityTokenParameters.InitializeSecurityTokenRequirement(initiatorServiceModelSecurityTokenRequirement);
				initiatorServiceModelSecurityTokenRequirement.KeyUsage = SecurityKeyUsage.Signature;
				initiatorServiceModelSecurityTokenRequirement.SupportSecurityContextCancellation = true;
				initiatorServiceModelSecurityTokenRequirement.SecurityAlgorithmSuite = this.Settings.SessionProtocolFactory.OutgoingAlgorithmSuite;
				initiatorServiceModelSecurityTokenRequirement.SecurityBindingElement = this.Settings.SessionProtocolFactory.SecurityBindingElement;
				initiatorServiceModelSecurityTokenRequirement.TargetAddress = this.to;
				initiatorServiceModelSecurityTokenRequirement.Via = this.Via;
				initiatorServiceModelSecurityTokenRequirement.MessageSecurityVersion = this.Settings.SessionProtocolFactory.MessageSecurityVersion.SecurityTokenVersion;
				initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.PrivacyNoticeUriProperty] = this.Settings.SessionProtocolFactory.PrivacyNoticeUri;
				initiatorServiceModelSecurityTokenRequirement.WebHeaders = this.webHeaderCollection;
				if (this.channelParameters != null)
				{
					initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.ChannelParametersCollectionProperty] = this.channelParameters;
				}
				initiatorServiceModelSecurityTokenRequirement.Properties[ServiceModelSecurityTokenRequirement.PrivacyNoticeVersionProperty] = this.Settings.SessionProtocolFactory.PrivacyNoticeVersion;
				if (this.channelBinder.LocalAddress != null)
				{
					initiatorServiceModelSecurityTokenRequirement.DuplexClientLocalAddress = this.channelBinder.LocalAddress;
				}
				this.sessionTokenProvider = this.Settings.SessionProtocolFactory.SecurityTokenManager.CreateSecurityTokenProvider(initiatorServiceModelSecurityTokenRequirement);
			}

			// Token: 0x0600714B RID: 29003 RVA: 0x001A5E44 File Offset: 0x001A4044
			private void OpenCore(SecurityToken sessionToken, TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.securityProtocol = this.Settings.SessionProtocolFactory.CreateSecurityProtocol(this.to, this.Via, null, true, timeoutHelper.RemainingTime());
				if (!(this.securityProtocol is IInitiatorSecuritySessionProtocol))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ProtocolMisMatch", new object[]
					{
						"IInitiatorSecuritySessionProtocol",
						base.GetType().ToString()
					})));
				}
				this.securityProtocol.Open(timeoutHelper.RemainingTime());
				this.channelBinder.Open(timeoutHelper.RemainingTime());
				this.InitializeSecurityState(sessionToken);
			}

			// Token: 0x0600714C RID: 29004 RVA: 0x001A5EF2 File Offset: 0x001A40F2
			protected override void OnFaulted()
			{
				this.AbortCore();
				this.inputSessionClosedHandle.Fault(this);
				this.keyRenewalCompletedEvent.Fault(this);
				this.outputSessionCloseHandle.Fault(this);
				base.OnFaulted();
			}

			// Token: 0x0600714D RID: 29005 RVA: 0x001A5F24 File Offset: 0x001A4124
			protected override void OnOpen(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.SetupSessionTokenProvider();
				SecurityUtils.OpenTokenProviderIfRequired(this.sessionTokenProvider, timeoutHelper.RemainingTime());
				using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
				{
					if (DiagnosticUtility.ShouldUseActivity)
					{
						ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivitySecuritySetup"), ActivityType.SecuritySetup);
					}
					SecurityToken token = this.sessionTokenProvider.GetToken(timeoutHelper.RemainingTime());
					this.sendCloseHandshake = true;
					this.OpenCore(token, timeoutHelper.RemainingTime());
				}
			}

			// Token: 0x0600714E RID: 29006 RVA: 0x001A5FC0 File Offset: 0x001A41C0
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				ServiceModelActivity activity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateAsyncActivity() : null;
				IAsyncResult result;
				using (ServiceModelActivity.BoundOperation(activity, true))
				{
					if (DiagnosticUtility.ShouldUseActivity)
					{
						ServiceModelActivity.Start(activity, SR.GetString("ActivitySecuritySetup"), ActivityType.SecuritySetup);
					}
					result = new SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.OpenAsyncResult(this, timeout, callback, state);
				}
				return result;
			}

			// Token: 0x0600714F RID: 29007 RVA: 0x001A6028 File Offset: 0x001A4228
			protected override void OnEndOpen(IAsyncResult result)
			{
				SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.OpenAsyncResult.End(result);
			}

			// Token: 0x06007150 RID: 29008 RVA: 0x001A6030 File Offset: 0x001A4230
			private void InitializeChannelBinder()
			{
				ChannelBuilder channelBuilder = this.Settings.ChannelBuilder;
				TolerateFaultsMode faultMode = this.Settings.TolerateTransportFailures ? TolerateFaultsMode.Always : TolerateFaultsMode.Never;
				if (channelBuilder.CanBuildChannelFactory<IDuplexSessionChannel>())
				{
					this.channelBinder = ClientReliableChannelBinder<IDuplexSessionChannel>.CreateBinder(this.RemoteAddress, this.Via, (IChannelFactory<IDuplexSessionChannel>)this.Settings.InnerChannelFactory, MaskingMode.None, faultMode, this.channelParameters, this.DefaultCloseTimeout, base.DefaultSendTimeout);
				}
				else if (channelBuilder.CanBuildChannelFactory<IDuplexChannel>())
				{
					this.channelBinder = ClientReliableChannelBinder<IDuplexChannel>.CreateBinder(this.RemoteAddress, this.Via, (IChannelFactory<IDuplexChannel>)this.Settings.InnerChannelFactory, MaskingMode.None, faultMode, this.channelParameters, this.DefaultCloseTimeout, base.DefaultSendTimeout);
					this.isCompositeDuplexConnection = true;
				}
				else if (channelBuilder.CanBuildChannelFactory<IRequestChannel>())
				{
					this.channelBinder = ClientReliableChannelBinder<IRequestChannel>.CreateBinder(this.RemoteAddress, this.Via, (IChannelFactory<IRequestChannel>)this.Settings.InnerChannelFactory, MaskingMode.None, faultMode, this.channelParameters, this.DefaultCloseTimeout, base.DefaultSendTimeout);
				}
				else if (channelBuilder.CanBuildChannelFactory<IRequestSessionChannel>())
				{
					this.channelBinder = ClientReliableChannelBinder<IRequestSessionChannel>.CreateBinder(this.RemoteAddress, this.Via, (IChannelFactory<IRequestSessionChannel>)this.Settings.InnerChannelFactory, MaskingMode.None, faultMode, this.channelParameters, this.DefaultCloseTimeout, base.DefaultSendTimeout);
				}
				this.channelBinder.Faulted += this.OnInnerFaulted;
			}

			// Token: 0x06007151 RID: 29009 RVA: 0x001A6191 File Offset: 0x001A4391
			private void OnInnerFaulted(IReliableChannelBinder sender, Exception exception)
			{
				base.Fault(exception);
			}

			// Token: 0x06007152 RID: 29010 RVA: 0x001A619C File Offset: 0x001A439C
			protected virtual bool OnCloseResponseReceived()
			{
				bool flag = false;
				bool flag2 = false;
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					flag2 = this.sentClose;
					if (flag2 && !this.isInputClosed)
					{
						this.isInputClosed = true;
						flag = true;
					}
				}
				if (!flag2)
				{
					base.Fault(new ProtocolException(SR.GetString("UnexpectedSecuritySessionCloseResponse")));
					return false;
				}
				if (flag)
				{
					this.inputSessionClosedHandle.Set();
				}
				return true;
			}

			// Token: 0x06007153 RID: 29011 RVA: 0x001A6224 File Offset: 0x001A4424
			protected virtual bool OnCloseReceived()
			{
				if (!this.ExpectClose)
				{
					base.Fault(new ProtocolException(SR.GetString("UnexpectedSecuritySessionClose")));
					return false;
				}
				bool flag = false;
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (!this.isInputClosed)
					{
						this.isInputClosed = true;
						this.receivedClose = true;
						flag = true;
					}
				}
				if (flag)
				{
					this.inputSessionClosedHandle.Set();
				}
				return true;
			}

			// Token: 0x06007154 RID: 29012 RVA: 0x001A62AC File Offset: 0x001A44AC
			private Message PrepareCloseMessage()
			{
				object thisLock = base.ThisLock;
				SecurityToken token;
				lock (thisLock)
				{
					token = this.currentSessionToken;
				}
				RequestSecurityToken requestSecurityToken = new RequestSecurityToken(this.Settings.SecurityStandardsManager);
				requestSecurityToken.RequestType = this.Settings.SecurityStandardsManager.TrustDriver.RequestTypeClose;
				requestSecurityToken.CloseTarget = this.Settings.IssuedSecurityTokenParameters.CreateKeyIdentifierClause(token, SecurityTokenReferenceStyle.External);
				requestSecurityToken.MakeReadOnly();
				Message message = Message.CreateMessage(this.MessageVersion, ActionHeader.Create(this.Settings.SecurityStandardsManager.SecureConversationDriver.CloseAction, this.MessageVersion.Addressing), requestSecurityToken);
				RequestReplyCorrelator.PrepareRequest(message);
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
				if (this.InternalLocalAddress != null)
				{
					message.Headers.ReplyTo = this.InternalLocalAddress;
				}
				else if (message.Version.Addressing == AddressingVersion.WSAddressing10)
				{
					message.Headers.ReplyTo = null;
				}
				else
				{
					if (message.Version.Addressing != AddressingVersion.WSAddressingAugust2004)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
						{
							message.Version.Addressing
						})));
					}
					message.Headers.ReplyTo = EndpointAddress.AnonymousAddress;
				}
				if (TraceUtility.PropagateUserActivity || TraceUtility.ShouldPropagateActivity)
				{
					TraceUtility.AddAmbientActivityToMessage(message);
				}
				return message;
			}

			// Token: 0x06007155 RID: 29013 RVA: 0x001A648C File Offset: 0x001A468C
			protected SecurityProtocolCorrelationState SendCloseMessage(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				Message message = this.PrepareCloseMessage();
				SecurityProtocolCorrelationState result;
				try
				{
					result = this.securityProtocol.SecureOutgoingMessage(ref message, timeoutHelper.RemainingTime(), null);
					this.ChannelBinder.Send(message, timeoutHelper.RemainingTime());
				}
				finally
				{
					message.Close();
				}
				SecurityTraceRecordHelper.TraceCloseMessageSent(this.currentSessionToken, this.RemoteAddress);
				return result;
			}

			// Token: 0x06007156 RID: 29014 RVA: 0x001A64FC File Offset: 0x001A46FC
			protected void SendCloseResponseMessage(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				Message message = null;
				try
				{
					message = this.closeResponse;
					this.securityProtocol.SecureOutgoingMessage(ref message, timeoutHelper.RemainingTime(), null);
					this.ChannelBinder.Send(message, timeoutHelper.RemainingTime());
					SecurityTraceRecordHelper.TraceCloseResponseMessageSent(this.currentSessionToken, this.RemoteAddress);
				}
				finally
				{
					message.Close();
				}
			}

			// Token: 0x06007157 RID: 29015 RVA: 0x001A6570 File Offset: 0x001A4770
			private IAsyncResult BeginSendCloseMessage(TimeSpan timeout, AsyncCallback callback, object state)
			{
				IAsyncResult result;
				using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
				{
					if (DiagnosticUtility.ShouldUseActivity)
					{
						ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivitySecurityClose"), ActivityType.SecuritySetup);
					}
					Message message = this.PrepareCloseMessage();
					result = new SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResult(message, this, timeout, callback, state, true);
				}
				return result;
			}

			// Token: 0x06007158 RID: 29016 RVA: 0x001A65D8 File Offset: 0x001A47D8
			private SecurityProtocolCorrelationState EndSendCloseMessage(IAsyncResult result)
			{
				SecurityProtocolCorrelationState result2 = SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResult.End(result);
				SecurityTraceRecordHelper.TraceCloseMessageSent(this.currentSessionToken, this.RemoteAddress);
				return result2;
			}

			// Token: 0x06007159 RID: 29017 RVA: 0x001A65FE File Offset: 0x001A47FE
			private IAsyncResult BeginSendCloseResponseMessage(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResult(this.closeResponse, this, timeout, callback, state, true);
			}

			// Token: 0x0600715A RID: 29018 RVA: 0x001A6610 File Offset: 0x001A4810
			private void EndSendCloseResponseMessage(IAsyncResult result)
			{
				SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResult.End(result);
				SecurityTraceRecordHelper.TraceCloseResponseMessageSent(this.currentSessionToken, this.RemoteAddress);
			}

			// Token: 0x0600715B RID: 29019 RVA: 0x001A662C File Offset: 0x001A482C
			private MessageFault GetProtocolFault(ref Message message, out bool isKeyRenewalFault, out bool isSessionAbortedFault)
			{
				isKeyRenewalFault = false;
				isSessionAbortedFault = false;
				MessageFault result = null;
				using (MessageBuffer messageBuffer = message.CreateBufferedCopy(int.MaxValue))
				{
					message = messageBuffer.CreateMessage();
					Message message2 = messageBuffer.CreateMessage();
					MessageFault messageFault = MessageFault.CreateFault(message2, 16384);
					if (messageFault.Code.IsSenderFault)
					{
						FaultCode subCode = messageFault.Code.SubCode;
						if (subCode != null)
						{
							SecurityStandardsManager standardsManager = this.securityProtocol.SecurityProtocolFactory.StandardsManager;
							SecureConversationDriver secureConversationDriver = standardsManager.SecureConversationDriver;
							if (subCode.Namespace == secureConversationDriver.Namespace.Value && subCode.Name == secureConversationDriver.RenewNeededFaultCode.Value)
							{
								result = messageFault;
								isKeyRenewalFault = true;
							}
							else if (subCode.Namespace == "http://schemas.microsoft.com/ws/2006/05/security" && subCode.Name == "SecuritySessionAborted")
							{
								result = messageFault;
								isSessionAbortedFault = true;
							}
						}
					}
				}
				return result;
			}

			// Token: 0x0600715C RID: 29020 RVA: 0x001A672C File Offset: 0x001A492C
			private void ProcessKeyRenewalFault()
			{
				SecurityTraceRecordHelper.TraceSessionKeyRenewalFault(this.currentSessionToken, this.RemoteAddress);
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.keyRenewalTime = DateTime.UtcNow;
				}
			}

			// Token: 0x0600715D RID: 29021 RVA: 0x001A6784 File Offset: 0x001A4984
			private void ProcessSessionAbortedFault(MessageFault sessionAbortedFault)
			{
				SecurityTraceRecordHelper.TraceRemoteSessionAbortedFault(this.currentSessionToken, this.RemoteAddress);
				base.Fault(new FaultException(sessionAbortedFault));
			}

			// Token: 0x0600715E RID: 29022 RVA: 0x001A67A4 File Offset: 0x001A49A4
			private void ProcessCloseResponse(Message response)
			{
				if (response.Headers.Action != this.Settings.SecurityStandardsManager.SecureConversationDriver.CloseResponseAction.Value)
				{
					throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("InvalidCloseResponseAction", new object[]
					{
						response.Headers.Action
					})), response);
				}
				RequestSecurityTokenResponse requestSecurityTokenResponse = null;
				XmlDictionaryReader readerAtBodyContents = response.GetReaderAtBodyContents();
				using (readerAtBodyContents)
				{
					if (this.Settings.SecurityStandardsManager.MessageSecurityVersion.TrustVersion != TrustVersion.WSTrustFeb2005)
					{
						if (this.Settings.SecurityStandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrust13)
						{
							RequestSecurityTokenResponseCollection requestSecurityTokenResponseCollection = this.Settings.SecurityStandardsManager.TrustDriver.CreateRequestSecurityTokenResponseCollection(readerAtBodyContents);
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
								goto IL_12B;
							}
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
					}
					requestSecurityTokenResponse = this.Settings.SecurityStandardsManager.TrustDriver.CreateRequestSecurityTokenResponse(readerAtBodyContents);
					IL_12B:
					response.ReadFromBodyContentsToEnd(readerAtBodyContents);
				}
				if (!requestSecurityTokenResponse.IsRequestedTokenClosed)
				{
					throw TraceUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SessionTokenWasNotClosed")), response);
				}
			}

			// Token: 0x0600715F RID: 29023 RVA: 0x001A692C File Offset: 0x001A4B2C
			private void PrepareReply(Message request, Message reply)
			{
				if (request.Headers.ReplyTo != null)
				{
					request.Headers.ReplyTo.ApplyTo(reply);
				}
				else if (request.Headers.From != null)
				{
					request.Headers.From.ApplyTo(reply);
				}
				if (request.Headers.MessageId != null)
				{
					reply.Headers.RelatesTo = request.Headers.MessageId;
				}
				TraceUtility.CopyActivity(request, reply);
				if (TraceUtility.PropagateUserActivity || TraceUtility.ShouldPropagateActivity)
				{
					TraceUtility.AddActivityHeader(reply);
				}
			}

			// Token: 0x06007160 RID: 29024 RVA: 0x001A69C7 File Offset: 0x001A4BC7
			private bool DoesSkiClauseMatchSigningToken(SecurityContextKeyIdentifierClause skiClause, Message request)
			{
				return this.SessionId != null && skiClause.ContextId.ToString() == this.SessionId;
			}

			// Token: 0x06007161 RID: 29025 RVA: 0x001A69EC File Offset: 0x001A4BEC
			private void ProcessCloseMessage(Message message)
			{
				XmlDictionaryReader readerAtBodyContents = message.GetReaderAtBodyContents();
				RequestSecurityToken requestSecurityToken;
				using (readerAtBodyContents)
				{
					requestSecurityToken = this.Settings.SecurityStandardsManager.TrustDriver.CreateRequestSecurityToken(readerAtBodyContents);
					message.ReadFromBodyContentsToEnd(readerAtBodyContents);
				}
				if (requestSecurityToken.RequestType != null && requestSecurityToken.RequestType != this.Settings.SecurityStandardsManager.TrustDriver.RequestTypeClose)
				{
					throw TraceUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("InvalidRstRequestType", new object[]
					{
						requestSecurityToken.RequestType
					})), message);
				}
				if (requestSecurityToken.CloseTarget == null)
				{
					throw TraceUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("NoCloseTargetSpecified")), message);
				}
				SecurityContextKeyIdentifierClause securityContextKeyIdentifierClause = requestSecurityToken.CloseTarget as SecurityContextKeyIdentifierClause;
				if (securityContextKeyIdentifierClause == null || !this.DoesSkiClauseMatchSigningToken(securityContextKeyIdentifierClause, message))
				{
					throw TraceUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("BadCloseTarget", new object[]
					{
						requestSecurityToken.CloseTarget
					})), message);
				}
				RequestSecurityTokenResponse requestSecurityTokenResponse = new RequestSecurityTokenResponse(this.Settings.SecurityStandardsManager);
				requestSecurityTokenResponse.Context = requestSecurityToken.Context;
				requestSecurityTokenResponse.IsRequestedTokenClosed = true;
				requestSecurityTokenResponse.MakeReadOnly();
				Message reply;
				if (this.Settings.SecurityStandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrustFeb2005)
				{
					reply = Message.CreateMessage(message.Version, ActionHeader.Create(this.Settings.SecurityStandardsManager.SecureConversationDriver.CloseResponseAction, message.Version.Addressing), requestSecurityTokenResponse);
				}
				else
				{
					if (this.Settings.SecurityStandardsManager.MessageSecurityVersion.TrustVersion != TrustVersion.WSTrust13)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
					}
					RequestSecurityTokenResponseCollection body = new RequestSecurityTokenResponseCollection(new List<RequestSecurityTokenResponse>
					{
						requestSecurityTokenResponse
					}, this.Settings.SecurityStandardsManager);
					reply = Message.CreateMessage(message.Version, ActionHeader.Create(this.Settings.SecurityStandardsManager.SecureConversationDriver.CloseResponseAction, message.Version.Addressing), body);
				}
				this.PrepareReply(message, reply);
				this.closeResponse = reply;
			}

			// Token: 0x06007162 RID: 29026 RVA: 0x001A6C04 File Offset: 0x001A4E04
			private bool ShouldWrapException(Exception e)
			{
				return e is FormatException || e is XmlException;
			}

			// Token: 0x06007163 RID: 29027 RVA: 0x001A6C1C File Offset: 0x001A4E1C
			protected Message ProcessIncomingMessage(Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState, out MessageFault protocolFault)
			{
				protocolFault = null;
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.DoKeyRolloverIfNeeded();
				}
				try
				{
					this.VerifyIncomingMessage(ref message, timeout, correlationState);
					string action = message.Headers.Action;
					if (action == this.Settings.SecurityStandardsManager.SecureConversationDriver.CloseResponseAction.Value)
					{
						SecurityTraceRecordHelper.TraceCloseResponseReceived(this.currentSessionToken, this.RemoteAddress);
						this.ProcessCloseResponse(message);
						this.OnCloseResponseReceived();
					}
					else if (action == this.Settings.SecurityStandardsManager.SecureConversationDriver.CloseAction.Value)
					{
						SecurityTraceRecordHelper.TraceCloseMessageReceived(this.currentSessionToken, this.RemoteAddress);
						this.ProcessCloseMessage(message);
						this.OnCloseReceived();
					}
					else
					{
						if (!(action == "http://schemas.microsoft.com/ws/2006/05/security/SecureConversationFault"))
						{
							return message;
						}
						bool flag2;
						bool flag3;
						protocolFault = this.GetProtocolFault(ref message, out flag2, out flag3);
						if (flag2)
						{
							this.ProcessKeyRenewalFault();
						}
						else
						{
							if (!flag3)
							{
								return message;
							}
							this.ProcessSessionAbortedFault(protocolFault);
						}
					}
				}
				catch (Exception ex)
				{
					if (ex is CommunicationException || ex is TimeoutException || Fx.IsFatal(ex) || !this.ShouldWrapException(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("MessageSecurityVerificationFailed"), ex));
				}
				message.Close();
				return null;
			}

			// Token: 0x06007164 RID: 29028 RVA: 0x001A6DA0 File Offset: 0x001A4FA0
			protected Message ProcessRequestContext(RequestContext requestContext, TimeSpan timeout, SecurityProtocolCorrelationState correlationState)
			{
				if (requestContext == null)
				{
					return null;
				}
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				Message requestMessage = requestContext.RequestMessage;
				Message message = requestMessage;
				Message result;
				try
				{
					Exception ex = null;
					try
					{
						MessageFault messageFault;
						return this.ProcessIncomingMessage(requestMessage, timeoutHelper.RemainingTime(), correlationState, out messageFault);
					}
					catch (MessageSecurityException ex2)
					{
						if (!this.isCompositeDuplexConnection)
						{
							if (message.IsFault)
							{
								MessageFault fault = MessageFault.CreateFault(message, 16384);
								if (SecurityUtils.IsSecurityFault(fault, this.settings.sessionProtocolFactory.StandardsManager))
								{
									ex = SecurityUtils.CreateSecurityFaultException(fault);
								}
							}
							else
							{
								ex = ex2;
							}
						}
					}
					if (ex != null)
					{
						base.Fault(ex);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
					}
					result = null;
				}
				finally
				{
					requestContext.Close(timeoutHelper.RemainingTime());
				}
				return result;
			}

			// Token: 0x06007165 RID: 29029 RVA: 0x001A6E68 File Offset: 0x001A5068
			private void DoKeyRolloverIfNeeded()
			{
				if (DateTime.UtcNow >= this.keyRolloverTime && this.previousSessionToken != null)
				{
					SecurityTraceRecordHelper.TracePreviousSessionKeyDiscarded(this.previousSessionToken, this.currentSessionToken, this.RemoteAddress);
					this.previousSessionToken = null;
					List<SecurityToken> list = new List<SecurityToken>(1);
					list.Add(this.currentSessionToken);
					((IInitiatorSecuritySessionProtocol)this.securityProtocol).SetIncomingSessionTokens(list);
				}
			}

			// Token: 0x06007166 RID: 29030 RVA: 0x001A6ED4 File Offset: 0x001A50D4
			private DateTime GetKeyRenewalTime(SecurityToken token)
			{
				TimeSpan timeout = TimeSpan.FromTicks((token.ValidTo.Ticks - token.ValidFrom.Ticks) * (long)this.settings.issuedTokenRenewalThreshold / 100L);
				DateTime dateTime = TimeoutHelper.Add(token.ValidFrom, timeout);
				DateTime dateTime2 = TimeoutHelper.Add(token.ValidFrom, this.settings.keyRenewalInterval);
				if (dateTime < dateTime2)
				{
					return dateTime;
				}
				return dateTime2;
			}

			// Token: 0x06007167 RID: 29031 RVA: 0x001A6F45 File Offset: 0x001A5145
			private bool IsKeyRenewalNeeded()
			{
				return DateTime.UtcNow >= this.keyRenewalTime;
			}

			// Token: 0x06007168 RID: 29032 RVA: 0x001A6F58 File Offset: 0x001A5158
			private void UpdateSessionTokens(SecurityToken newToken)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.previousSessionToken = this.currentSessionToken;
					this.keyRolloverTime = TimeoutHelper.Add(DateTime.UtcNow, this.Settings.KeyRolloverInterval);
					this.currentSessionToken = newToken;
					this.keyRenewalTime = this.GetKeyRenewalTime(newToken);
					List<SecurityToken> list = new List<SecurityToken>(2);
					list.Add(this.previousSessionToken);
					list.Add(this.currentSessionToken);
					((IInitiatorSecuritySessionProtocol)this.securityProtocol).SetIncomingSessionTokens(list);
					((IInitiatorSecuritySessionProtocol)this.securityProtocol).SetOutgoingSessionToken(this.currentSessionToken);
					SecurityTraceRecordHelper.TraceSessionKeyRenewed(this.currentSessionToken, this.previousSessionToken, this.RemoteAddress);
				}
			}

			// Token: 0x06007169 RID: 29033 RVA: 0x001A702C File Offset: 0x001A522C
			private void RenewKey(TimeSpan timeout)
			{
				if (!this.settings.CanRenewSession)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SessionKeyExpiredException(SR.GetString("SessionKeyRenewalNotSupported")));
				}
				object thisLock = base.ThisLock;
				bool flag2;
				lock (thisLock)
				{
					if (!this.isKeyRenewalOngoing)
					{
						this.isKeyRenewalOngoing = true;
						this.keyRenewalCompletedEvent.Reset();
						flag2 = true;
					}
					else
					{
						flag2 = false;
					}
				}
				if (flag2)
				{
					try
					{
						using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
						{
							if (DiagnosticUtility.ShouldUseActivity)
							{
								ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivitySecurityRenew"), ActivityType.SecuritySetup);
							}
							SecurityToken newToken = this.sessionTokenProvider.RenewToken(timeout, this.currentSessionToken);
							this.UpdateSessionTokens(newToken);
							return;
						}
					}
					finally
					{
						object thisLock2 = base.ThisLock;
						lock (thisLock2)
						{
							this.isKeyRenewalOngoing = false;
							this.keyRenewalCompletedEvent.Set();
						}
					}
				}
				this.keyRenewalCompletedEvent.Wait(timeout);
				object thisLock3 = base.ThisLock;
				lock (thisLock3)
				{
					if (this.IsKeyRenewalNeeded())
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SessionKeyExpiredException(SR.GetString("UnableToRenewSessionKey")));
					}
				}
			}

			// Token: 0x0600716A RID: 29034 RVA: 0x001A71C0 File Offset: 0x001A53C0
			private bool CheckIfKeyRenewalNeeded()
			{
				bool result = false;
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					result = this.IsKeyRenewalNeeded();
					this.DoKeyRolloverIfNeeded();
				}
				return result;
			}

			// Token: 0x0600716B RID: 29035 RVA: 0x001A720C File Offset: 0x001A540C
			protected IAsyncResult BeginSecureOutgoingMessage(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				if (!this.CheckIfKeyRenewalNeeded())
				{
					SecurityProtocolCorrelationState parameter = this.securityProtocol.SecureOutgoingMessage(ref message, timeout, null);
					return new CompletedAsyncResult<Message, SecurityProtocolCorrelationState>(message, parameter, callback, state);
				}
				return new SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.KeyRenewalAsyncResult(message, this, timeout, callback, state);
			}

			// Token: 0x0600716C RID: 29036 RVA: 0x001A724C File Offset: 0x001A544C
			protected Message EndSecureOutgoingMessage(IAsyncResult result, out SecurityProtocolCorrelationState correlationState)
			{
				if (result is CompletedAsyncResult<Message, SecurityProtocolCorrelationState>)
				{
					return CompletedAsyncResult<Message, SecurityProtocolCorrelationState>.End(result, out correlationState);
				}
				TimeSpan timeout;
				Message result2 = SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.KeyRenewalAsyncResult.End(result, out timeout);
				correlationState = this.securityProtocol.SecureOutgoingMessage(ref result2, timeout, null);
				return result2;
			}

			// Token: 0x0600716D RID: 29037 RVA: 0x001A7284 File Offset: 0x001A5484
			protected SecurityProtocolCorrelationState SecureOutgoingMessage(ref Message message, TimeSpan timeout)
			{
				bool flag = this.CheckIfKeyRenewalNeeded();
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (flag)
				{
					this.RenewKey(timeoutHelper.RemainingTime());
				}
				return this.securityProtocol.SecureOutgoingMessage(ref message, timeoutHelper.RemainingTime(), null);
			}

			// Token: 0x0600716E RID: 29038 RVA: 0x001A72C4 File Offset: 0x001A54C4
			protected void VerifyIncomingMessage(ref Message message, TimeSpan timeout, SecurityProtocolCorrelationState correlationState)
			{
				this.securityProtocol.VerifyIncomingMessage(ref message, timeout, new SecurityProtocolCorrelationState[]
				{
					correlationState
				});
			}

			// Token: 0x0600716F RID: 29039 RVA: 0x001A72DE File Offset: 0x001A54DE
			protected virtual void AbortCore()
			{
				if (this.channelBinder != null)
				{
					this.channelBinder.Abort();
				}
				if (this.sessionTokenProvider != null)
				{
					SecurityUtils.AbortTokenProviderIfRequired(this.sessionTokenProvider);
				}
			}

			// Token: 0x06007170 RID: 29040 RVA: 0x001A7308 File Offset: 0x001A5508
			protected virtual void CloseCore(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				try
				{
					if (this.channelBinder != null)
					{
						this.channelBinder.Close(timeoutHelper.RemainingTime());
					}
					if (this.sessionTokenProvider != null)
					{
						SecurityUtils.CloseTokenProviderIfRequired(this.sessionTokenProvider, timeoutHelper.RemainingTime());
					}
					this.keyRenewalCompletedEvent.Abort(this);
					this.inputSessionClosedHandle.Abort(this);
				}
				catch (CommunicationObjectAbortedException)
				{
					if (base.State != CommunicationState.Closed)
					{
						throw;
					}
				}
			}

			// Token: 0x06007171 RID: 29041 RVA: 0x001A738C File Offset: 0x001A558C
			protected virtual IAsyncResult BeginCloseCore(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseCoreAsyncResult(this, timeout, callback, state);
			}

			// Token: 0x06007172 RID: 29042 RVA: 0x001A7397 File Offset: 0x001A5597
			protected virtual void EndCloseCore(IAsyncResult result)
			{
				SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseCoreAsyncResult.End(result);
			}

			// Token: 0x06007173 RID: 29043 RVA: 0x001A739F File Offset: 0x001A559F
			protected IAsyncResult BeginReceiveInternal(TimeSpan timeout, SecurityProtocolCorrelationState correlationState, AsyncCallback callback, object state)
			{
				return new SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.ReceiveAsyncResult(this, timeout, correlationState, callback, state);
			}

			// Token: 0x06007174 RID: 29044 RVA: 0x001A73AC File Offset: 0x001A55AC
			protected Message EndReceiveInternal(IAsyncResult result)
			{
				return SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.ReceiveAsyncResult.End(result);
			}

			// Token: 0x06007175 RID: 29045 RVA: 0x001A73B4 File Offset: 0x001A55B4
			protected Message ReceiveInternal(TimeSpan timeout, SecurityProtocolCorrelationState correlationState)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				while (!this.isInputClosed)
				{
					RequestContext requestContext;
					if (this.ChannelBinder.TryReceive(timeoutHelper.RemainingTime(), out requestContext))
					{
						if (requestContext == null)
						{
							return null;
						}
						Message message = this.ProcessRequestContext(requestContext, timeoutHelper.RemainingTime(), correlationState);
						if (message != null)
						{
							return message;
						}
					}
					if (timeoutHelper.RemainingTime() == TimeSpan.Zero)
					{
						break;
					}
				}
				return null;
			}

			// Token: 0x06007176 RID: 29046 RVA: 0x001A741C File Offset: 0x001A561C
			protected bool CloseSession(TimeSpan timeout, out bool wasAborted)
			{
				bool result;
				using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
				{
					if (DiagnosticUtility.ShouldUseActivity)
					{
						ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivitySecurityClose"), ActivityType.SecuritySetup);
					}
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					wasAborted = false;
					try
					{
						this.CloseOutputSession(timeoutHelper.RemainingTime());
						return this.inputSessionClosedHandle.Wait(timeoutHelper.RemainingTime(), false);
					}
					catch (CommunicationObjectAbortedException)
					{
						if (base.State != CommunicationState.Closed)
						{
							throw;
						}
						wasAborted = true;
					}
					result = false;
				}
				return result;
			}

			// Token: 0x06007177 RID: 29047 RVA: 0x001A74C0 File Offset: 0x001A56C0
			protected IAsyncResult BeginCloseSession(TimeSpan timeout, AsyncCallback callback, object state)
			{
				IAsyncResult result;
				using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateAsyncActivity() : null)
				{
					if (DiagnosticUtility.ShouldUseActivity)
					{
						ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivitySecurityClose"), ActivityType.SecuritySetup);
					}
					result = new SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseSessionAsyncResult(timeout, this, callback, state);
				}
				return result;
			}

			// Token: 0x06007178 RID: 29048 RVA: 0x001A7520 File Offset: 0x001A5720
			protected bool EndCloseSession(IAsyncResult result, out bool wasAborted)
			{
				return SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseSessionAsyncResult.End(result, out wasAborted);
			}

			// Token: 0x06007179 RID: 29049 RVA: 0x001A752C File Offset: 0x001A572C
			private void DetermineCloseMessageToSend(out bool sendClose, out bool sendCloseResponse)
			{
				sendClose = false;
				sendCloseResponse = false;
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (!this.isOutputClosed)
					{
						this.isOutputClosed = true;
						if (this.receivedClose)
						{
							sendCloseResponse = true;
						}
						else
						{
							sendClose = true;
							this.sentClose = true;
						}
						this.outputSessionCloseHandle.Reset();
					}
				}
			}

			// Token: 0x0600717A RID: 29050 RVA: 0x001A75A4 File Offset: 0x001A57A4
			protected virtual SecurityProtocolCorrelationState CloseOutputSession(TimeSpan timeout)
			{
				base.ThrowIfFaulted();
				if (!this.SendCloseHandshake)
				{
					return null;
				}
				bool flag;
				bool flag2;
				this.DetermineCloseMessageToSend(out flag, out flag2);
				if (flag || flag2)
				{
					try
					{
						if (flag)
						{
							return this.SendCloseMessage(timeout);
						}
						this.SendCloseResponseMessage(timeout);
						return null;
					}
					finally
					{
						this.outputSessionCloseHandle.Set();
					}
				}
				return null;
			}

			// Token: 0x0600717B RID: 29051 RVA: 0x001A7608 File Offset: 0x001A5808
			protected virtual IAsyncResult BeginCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state)
			{
				base.ThrowIfFaulted();
				if (!this.SendCloseHandshake)
				{
					return new CompletedAsyncResult(callback, state);
				}
				bool flag;
				bool flag2;
				this.DetermineCloseMessageToSend(out flag, out flag2);
				if (flag || flag2)
				{
					bool flag3 = true;
					try
					{
						IAsyncResult result;
						if (flag)
						{
							result = this.BeginSendCloseMessage(timeout, callback, state);
						}
						else
						{
							result = this.BeginSendCloseResponseMessage(timeout, callback, state);
						}
						flag3 = false;
						return result;
					}
					finally
					{
						if (flag3)
						{
							this.outputSessionCloseHandle.Set();
						}
					}
				}
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x0600717C RID: 29052 RVA: 0x001A7688 File Offset: 0x001A5888
			protected virtual SecurityProtocolCorrelationState EndCloseOutputSession(IAsyncResult result)
			{
				if (result is CompletedAsyncResult)
				{
					CompletedAsyncResult.End(result);
					return null;
				}
				object thisLock = base.ThisLock;
				bool flag2;
				lock (thisLock)
				{
					flag2 = this.sentClose;
				}
				SecurityProtocolCorrelationState result2;
				try
				{
					if (flag2)
					{
						result2 = this.EndSendCloseMessage(result);
					}
					else
					{
						this.EndSendCloseResponseMessage(result);
						result2 = null;
					}
				}
				finally
				{
					this.outputSessionCloseHandle.Set();
				}
				return result2;
			}

			// Token: 0x0600717D RID: 29053 RVA: 0x001A770C File Offset: 0x001A590C
			protected void CheckOutputOpen()
			{
				base.ThrowIfClosedOrNotOpen();
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.isOutputClosed)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new CommunicationException(SR.GetString("OutputNotExpected")));
					}
				}
			}

			// Token: 0x0600717E RID: 29054 RVA: 0x001A7770 File Offset: 0x001A5970
			protected override void OnAbort()
			{
				this.AbortCore();
				this.inputSessionClosedHandle.Abort(this);
				this.keyRenewalCompletedEvent.Abort(this);
				this.outputSessionCloseHandle.Abort(this);
			}

			// Token: 0x0600717F RID: 29055 RVA: 0x001A779C File Offset: 0x001A599C
			protected override void OnClose(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (this.SendCloseHandshake)
				{
					bool flag2;
					bool flag = this.CloseSession(timeout, out flag2);
					if (flag2)
					{
						return;
					}
					if (!flag)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new TimeoutException(SR.GetString("ClientSecurityCloseTimeout", new object[]
						{
							timeout
						})));
					}
					try
					{
						if (!this.outputSessionCloseHandle.Wait(timeoutHelper.RemainingTime(), false))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new TimeoutException(SR.GetString("ClientSecurityOutputSessionCloseTimeout", new object[]
							{
								timeoutHelper.OriginalTimeout
							})));
						}
					}
					catch (CommunicationObjectAbortedException)
					{
						if (base.State == CommunicationState.Closed)
						{
							return;
						}
						throw;
					}
				}
				this.CloseCore(timeoutHelper.RemainingTime());
			}

			// Token: 0x06007180 RID: 29056 RVA: 0x001A7868 File Offset: 0x001A5A68
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult(this, timeout, callback, state);
			}

			// Token: 0x06007181 RID: 29057 RVA: 0x001A7873 File Offset: 0x001A5A73
			protected override void OnEndClose(IAsyncResult result)
			{
				SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult.End(result);
			}

			// Token: 0x04004060 RID: 16480
			private EndpointAddress to;

			// Token: 0x04004061 RID: 16481
			private Uri via;

			// Token: 0x04004062 RID: 16482
			private IClientReliableChannelBinder channelBinder;

			// Token: 0x04004063 RID: 16483
			private ChannelParameterCollection channelParameters;

			// Token: 0x04004064 RID: 16484
			private SecurityToken currentSessionToken;

			// Token: 0x04004065 RID: 16485
			private SecurityToken previousSessionToken;

			// Token: 0x04004066 RID: 16486
			private DateTime keyRenewalTime;

			// Token: 0x04004067 RID: 16487
			private DateTime keyRolloverTime;

			// Token: 0x04004068 RID: 16488
			private SecurityProtocol securityProtocol;

			// Token: 0x04004069 RID: 16489
			private SecuritySessionClientSettings<TChannel> settings;

			// Token: 0x0400406A RID: 16490
			private SecurityTokenProvider sessionTokenProvider;

			// Token: 0x0400406B RID: 16491
			private bool isKeyRenewalOngoing;

			// Token: 0x0400406C RID: 16492
			private InterruptibleWaitObject keyRenewalCompletedEvent;

			// Token: 0x0400406D RID: 16493
			private bool sentClose;

			// Token: 0x0400406E RID: 16494
			private bool receivedClose;

			// Token: 0x0400406F RID: 16495
			private volatile bool isOutputClosed;

			// Token: 0x04004070 RID: 16496
			private volatile bool isInputClosed;

			// Token: 0x04004071 RID: 16497
			private InterruptibleWaitObject inputSessionClosedHandle = new InterruptibleWaitObject(false);

			// Token: 0x04004072 RID: 16498
			private bool sendCloseHandshake;

			// Token: 0x04004073 RID: 16499
			private MessageVersion messageVersion;

			// Token: 0x04004074 RID: 16500
			private bool isCompositeDuplexConnection;

			// Token: 0x04004075 RID: 16501
			private Message closeResponse;

			// Token: 0x04004076 RID: 16502
			private InterruptibleWaitObject outputSessionCloseHandle = new InterruptibleWaitObject(true);

			// Token: 0x04004077 RID: 16503
			private WebHeaderCollection webHeaderCollection;

			// Token: 0x02000EE0 RID: 3808
			private class CloseCoreAsyncResult : TraceAsyncResult
			{
				// Token: 0x060084B6 RID: 33974 RVA: 0x001EA1A0 File Offset: 0x001E83A0
				public CloseCoreAsyncResult(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.channel = channel;
					this.timeoutHelper = new TimeoutHelper(timeout);
					bool flag = false;
					if (channel.channelBinder != null)
					{
						try
						{
							IAsyncResult asyncResult = this.channel.channelBinder.BeginClose(this.timeoutHelper.RemainingTime(), SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseCoreAsyncResult.closeChannelBinderCallback, this);
							if (!asyncResult.CompletedSynchronously)
							{
								return;
							}
							this.channel.channelBinder.EndClose(asyncResult);
						}
						catch (CommunicationObjectAbortedException)
						{
							if (this.channel.State != CommunicationState.Closed)
							{
								throw;
							}
							flag = true;
						}
					}
					if (!flag)
					{
						flag = this.OnChannelBinderClosed();
					}
					if (flag)
					{
						base.Complete(true);
					}
				}

				// Token: 0x060084B7 RID: 33975 RVA: 0x001EA24C File Offset: 0x001E844C
				private static void ChannelBinderCloseCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseCoreAsyncResult closeCoreAsyncResult = (SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseCoreAsyncResult)result.AsyncState;
					Exception exception = null;
					bool flag = false;
					try
					{
						try
						{
							closeCoreAsyncResult.channel.channelBinder.EndClose(result);
						}
						catch (CommunicationObjectAbortedException)
						{
							if (closeCoreAsyncResult.channel.State != CommunicationState.Closed)
							{
								throw;
							}
							flag = true;
						}
						if (!flag)
						{
							flag = closeCoreAsyncResult.OnChannelBinderClosed();
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
					}
					if (flag)
					{
						closeCoreAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x060084B8 RID: 33976 RVA: 0x001EA2E0 File Offset: 0x001E84E0
				private bool OnChannelBinderClosed()
				{
					if (this.channel.sessionTokenProvider != null)
					{
						try
						{
							IAsyncResult asyncResult = SecurityUtils.BeginCloseTokenProviderIfRequired(this.channel.sessionTokenProvider, this.timeoutHelper.RemainingTime(), SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseCoreAsyncResult.closeTokenProviderCallback, this);
							if (!asyncResult.CompletedSynchronously)
							{
								return false;
							}
							SecurityUtils.EndCloseTokenProviderIfRequired(asyncResult);
						}
						catch (CommunicationObjectAbortedException)
						{
							if (this.channel.State != CommunicationState.Closed)
							{
								throw;
							}
							return true;
						}
					}
					return this.OnTokenProviderClosed();
				}

				// Token: 0x060084B9 RID: 33977 RVA: 0x001EA360 File Offset: 0x001E8560
				private static void CloseTokenProviderCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseCoreAsyncResult closeCoreAsyncResult = (SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseCoreAsyncResult)result.AsyncState;
					Exception exception = null;
					bool flag = false;
					try
					{
						try
						{
							SecurityUtils.EndCloseTokenProviderIfRequired(result);
						}
						catch (CommunicationObjectAbortedException)
						{
							if (closeCoreAsyncResult.channel.State != CommunicationState.Closed)
							{
								throw;
							}
							flag = true;
						}
						if (!flag)
						{
							flag = closeCoreAsyncResult.OnTokenProviderClosed();
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
					}
					if (flag)
					{
						closeCoreAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x060084BA RID: 33978 RVA: 0x001EA3E8 File Offset: 0x001E85E8
				private bool OnTokenProviderClosed()
				{
					this.channel.keyRenewalCompletedEvent.Abort(this.channel);
					this.channel.inputSessionClosedHandle.Abort(this.channel);
					return true;
				}

				// Token: 0x060084BB RID: 33979 RVA: 0x001EA417 File Offset: 0x001E8617
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseCoreAsyncResult>(result);
				}

				// Token: 0x04004CD4 RID: 19668
				private static AsyncCallback closeChannelBinderCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseCoreAsyncResult.ChannelBinderCloseCallback));

				// Token: 0x04004CD5 RID: 19669
				private static AsyncCallback closeTokenProviderCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseCoreAsyncResult.CloseTokenProviderCallback));

				// Token: 0x04004CD6 RID: 19670
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004CD7 RID: 19671
				private SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel channel;
			}

			// Token: 0x02000EE1 RID: 3809
			private class ReceiveAsyncResult : TraceAsyncResult
			{
				// Token: 0x060084BD RID: 33981 RVA: 0x001EA450 File Offset: 0x001E8650
				public ReceiveAsyncResult(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel channel, TimeSpan timeout, SecurityProtocolCorrelationState correlationState, AsyncCallback callback, object state) : base(callback, state)
				{
					this.channel = channel;
					this.correlationState = correlationState;
					this.timeoutHelper = new TimeoutHelper(timeout);
					IAsyncResult asyncResult = channel.ChannelBinder.BeginTryReceive(this.timeoutHelper.RemainingTime(), SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.ReceiveAsyncResult.onReceive, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					bool flag = this.CompleteReceive(asyncResult);
					if (flag)
					{
						base.Complete(true);
					}
				}

				// Token: 0x060084BE RID: 33982 RVA: 0x001EA4BC File Offset: 0x001E86BC
				private bool CompleteReceive(IAsyncResult result)
				{
					while (!this.channel.isInputClosed)
					{
						RequestContext requestContext;
						if (this.channel.ChannelBinder.EndTryReceive(result, out requestContext))
						{
							if (requestContext == null)
							{
								break;
							}
							this.message = this.channel.ProcessRequestContext(requestContext, this.timeoutHelper.RemainingTime(), this.correlationState);
							if (this.message != null || this.channel.isInputClosed)
							{
								break;
							}
						}
						TimeSpan t = this.timeoutHelper.RemainingTime();
						if (t == TimeSpan.Zero)
						{
							break;
						}
						result = this.channel.ChannelBinder.BeginTryReceive(this.timeoutHelper.RemainingTime(), SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.ReceiveAsyncResult.onReceive, this);
						if (!result.CompletedSynchronously)
						{
							return false;
						}
					}
					return true;
				}

				// Token: 0x060084BF RID: 33983 RVA: 0x001EA57C File Offset: 0x001E877C
				public static Message End(IAsyncResult result)
				{
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.ReceiveAsyncResult receiveAsyncResult = AsyncResult.End<SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.ReceiveAsyncResult>(result);
					return receiveAsyncResult.message;
				}

				// Token: 0x060084C0 RID: 33984 RVA: 0x001EA598 File Offset: 0x001E8798
				private static void OnReceive(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.ReceiveAsyncResult receiveAsyncResult = (SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.ReceiveAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						flag = receiveAsyncResult.CompleteReceive(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						flag = true;
						exception = ex;
					}
					if (flag)
					{
						receiveAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x04004CD8 RID: 19672
				private static AsyncCallback onReceive = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.ReceiveAsyncResult.OnReceive));

				// Token: 0x04004CD9 RID: 19673
				private SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel channel;

				// Token: 0x04004CDA RID: 19674
				private Message message;

				// Token: 0x04004CDB RID: 19675
				private SecurityProtocolCorrelationState correlationState;

				// Token: 0x04004CDC RID: 19676
				private TimeoutHelper timeoutHelper;
			}

			// Token: 0x02000EE2 RID: 3810
			private class OpenAsyncResult : TraceAsyncResult
			{
				// Token: 0x060084C2 RID: 33986 RVA: 0x001EA60C File Offset: 0x001E880C
				public OpenAsyncResult(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel sessionChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.sessionChannel = sessionChannel;
					this.sessionChannel.SetupSessionTokenProvider();
					IAsyncResult asyncResult = SecurityUtils.BeginOpenTokenProviderIfRequired(this.sessionChannel.sessionTokenProvider, this.timeoutHelper.RemainingTime(), SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.OpenAsyncResult.openTokenProviderCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					SecurityUtils.EndOpenTokenProviderIfRequired(asyncResult);
					bool flag = this.OnTokenProviderOpened();
					if (flag)
					{
						base.Complete(true);
					}
				}

				// Token: 0x060084C3 RID: 33987 RVA: 0x001EA684 File Offset: 0x001E8884
				private static void OpenTokenProviderCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.OpenAsyncResult openAsyncResult = (SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.OpenAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						SecurityUtils.EndOpenTokenProviderIfRequired(result);
						flag = openAsyncResult.OnTokenProviderOpened();
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						flag = true;
						exception = ex;
					}
					if (flag)
					{
						openAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x060084C4 RID: 33988 RVA: 0x001EA6E8 File Offset: 0x001E88E8
				private bool OnTokenProviderOpened()
				{
					IAsyncResult asyncResult = this.sessionChannel.sessionTokenProvider.BeginGetToken(this.timeoutHelper.RemainingTime(), SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.OpenAsyncResult.getTokenCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					SecurityToken sessionToken = this.sessionChannel.sessionTokenProvider.EndGetToken(asyncResult);
					return this.OnTokenObtained(sessionToken);
				}

				// Token: 0x060084C5 RID: 33989 RVA: 0x001EA73A File Offset: 0x001E893A
				private bool OnTokenObtained(SecurityToken sessionToken)
				{
					this.sessionChannel.sendCloseHandshake = true;
					this.sessionChannel.OpenCore(sessionToken, this.timeoutHelper.RemainingTime());
					return true;
				}

				// Token: 0x060084C6 RID: 33990 RVA: 0x001EA760 File Offset: 0x001E8960
				private static void GetTokenCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.OpenAsyncResult openAsyncResult = (SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.OpenAsyncResult)result.AsyncState;
					try
					{
						using (ServiceModelActivity.BoundOperation(openAsyncResult.CallbackActivity))
						{
							bool flag = false;
							Exception exception = null;
							try
							{
								SecurityToken sessionToken = openAsyncResult.sessionChannel.sessionTokenProvider.EndGetToken(result);
								flag = openAsyncResult.OnTokenObtained(sessionToken);
							}
							catch (Exception ex)
							{
								if (Fx.IsFatal(ex))
								{
									throw;
								}
								flag = true;
								exception = ex;
							}
							if (flag)
							{
								openAsyncResult.Complete(false, exception);
							}
						}
					}
					finally
					{
						if (openAsyncResult.CallbackActivity != null)
						{
							openAsyncResult.CallbackActivity.Dispose();
						}
					}
				}

				// Token: 0x060084C7 RID: 33991 RVA: 0x001EA818 File Offset: 0x001E8A18
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.OpenAsyncResult>(result);
					ServiceModelActivity.Stop(((SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.OpenAsyncResult)result).CallbackActivity);
				}

				// Token: 0x04004CDD RID: 19677
				private static readonly AsyncCallback getTokenCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.OpenAsyncResult.GetTokenCallback));

				// Token: 0x04004CDE RID: 19678
				private static readonly AsyncCallback openTokenProviderCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.OpenAsyncResult.OpenTokenProviderCallback));

				// Token: 0x04004CDF RID: 19679
				private SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel sessionChannel;

				// Token: 0x04004CE0 RID: 19680
				private TimeoutHelper timeoutHelper;
			}

			// Token: 0x02000EE3 RID: 3811
			private class CloseSessionAsyncResult : TraceAsyncResult
			{
				// Token: 0x060084C9 RID: 33993 RVA: 0x001EA860 File Offset: 0x001E8A60
				public CloseSessionAsyncResult(TimeSpan timeout, SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel sessionChannel, AsyncCallback callback, object state) : base(callback, state)
				{
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.sessionChannel = sessionChannel;
					bool flag = false;
					try
					{
						IAsyncResult asyncResult = this.sessionChannel.BeginCloseOutputSession(this.timeoutHelper.RemainingTime(), SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseSessionAsyncResult.closeOutputSessionCallback, this);
						if (!asyncResult.CompletedSynchronously)
						{
							return;
						}
						this.sessionChannel.EndCloseOutputSession(asyncResult);
					}
					catch (CommunicationObjectAbortedException)
					{
						if (this.sessionChannel.State != CommunicationState.Closed)
						{
							throw;
						}
						flag = true;
						this.wasAborted = true;
					}
					if (!this.wasAborted)
					{
						flag = this.OnOutputSessionClosed();
					}
					if (flag)
					{
						base.Complete(true);
					}
				}

				// Token: 0x060084CA RID: 33994 RVA: 0x001EA908 File Offset: 0x001E8B08
				private static void CloseOutputSessionCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseSessionAsyncResult closeSessionAsyncResult = (SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseSessionAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						try
						{
							closeSessionAsyncResult.sessionChannel.EndCloseOutputSession(result);
						}
						catch (CommunicationObjectAbortedException)
						{
							if (closeSessionAsyncResult.sessionChannel.State != CommunicationState.Closed)
							{
								throw;
							}
							closeSessionAsyncResult.wasAborted = true;
							flag = true;
						}
						if (!closeSessionAsyncResult.wasAborted)
						{
							flag = closeSessionAsyncResult.OnOutputSessionClosed();
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
					}
					if (flag)
					{
						closeSessionAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x060084CB RID: 33995 RVA: 0x001EA9A4 File Offset: 0x001E8BA4
				private bool OnOutputSessionClosed()
				{
					try
					{
						IAsyncResult asyncResult = this.sessionChannel.inputSessionClosedHandle.BeginWait(this.timeoutHelper.RemainingTime(), true, SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseSessionAsyncResult.shutdownWaitCallback, this);
						if (!asyncResult.CompletedSynchronously)
						{
							return false;
						}
						this.sessionChannel.inputSessionClosedHandle.EndWait(asyncResult);
						this.closeCompleted = true;
					}
					catch (CommunicationObjectAbortedException)
					{
						if (this.sessionChannel.State != CommunicationState.Closed)
						{
							throw;
						}
						this.wasAborted = true;
					}
					catch (TimeoutException)
					{
						this.closeCompleted = false;
					}
					return true;
				}

				// Token: 0x060084CC RID: 33996 RVA: 0x001EAA40 File Offset: 0x001E8C40
				private static void ShutdownWaitCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseSessionAsyncResult closeSessionAsyncResult = (SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseSessionAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						closeSessionAsyncResult.sessionChannel.inputSessionClosedHandle.EndWait(result);
						closeSessionAsyncResult.closeCompleted = true;
					}
					catch (CommunicationObjectAbortedException)
					{
						if (closeSessionAsyncResult.sessionChannel.State != CommunicationState.Closed)
						{
							throw;
						}
						closeSessionAsyncResult.wasAborted = true;
					}
					catch (TimeoutException)
					{
						closeSessionAsyncResult.closeCompleted = false;
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					closeSessionAsyncResult.Complete(false, exception);
				}

				// Token: 0x060084CD RID: 33997 RVA: 0x001EAAE4 File Offset: 0x001E8CE4
				public static bool End(IAsyncResult result, out bool wasAborted)
				{
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseSessionAsyncResult closeSessionAsyncResult = AsyncResult.End<SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseSessionAsyncResult>(result);
					wasAborted = closeSessionAsyncResult.wasAborted;
					ServiceModelActivity.Stop(closeSessionAsyncResult.CallbackActivity);
					return closeSessionAsyncResult.closeCompleted;
				}

				// Token: 0x04004CE1 RID: 19681
				private static readonly AsyncCallback closeOutputSessionCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseSessionAsyncResult.CloseOutputSessionCallback));

				// Token: 0x04004CE2 RID: 19682
				private static readonly AsyncCallback shutdownWaitCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseSessionAsyncResult.ShutdownWaitCallback));

				// Token: 0x04004CE3 RID: 19683
				private SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel sessionChannel;

				// Token: 0x04004CE4 RID: 19684
				private bool closeCompleted;

				// Token: 0x04004CE5 RID: 19685
				private bool wasAborted;

				// Token: 0x04004CE6 RID: 19686
				private TimeoutHelper timeoutHelper;
			}

			// Token: 0x02000EE4 RID: 3812
			private class CloseAsyncResult : TraceAsyncResult
			{
				// Token: 0x060084CF RID: 33999 RVA: 0x001EAB40 File Offset: 0x001E8D40
				public CloseAsyncResult(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel sessionChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					sessionChannel.ThrowIfFaulted();
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.sessionChannel = sessionChannel;
					if (!sessionChannel.SendCloseHandshake)
					{
						if (this.CloseCore())
						{
							base.Complete(true);
						}
						return;
					}
					bool flag = false;
					IAsyncResult asyncResult = this.sessionChannel.BeginCloseSession(this.timeoutHelper.RemainingTime(), SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult.closeSessionCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					bool flag2 = this.sessionChannel.EndCloseSession(asyncResult, out flag);
					if (flag)
					{
						base.Complete(true);
						return;
					}
					if (!flag2)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new TimeoutException(SR.GetString("ClientSecurityCloseTimeout", new object[]
						{
							timeout
						})));
					}
					bool flag3 = this.OnWaitForOutputSessionClose(out flag);
					if (flag || flag3)
					{
						base.Complete(true);
					}
				}

				// Token: 0x060084D0 RID: 34000 RVA: 0x001EAC0C File Offset: 0x001E8E0C
				private static void CloseSessionCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult closeAsyncResult = (SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						bool flag3;
						bool flag2 = closeAsyncResult.sessionChannel.EndCloseSession(result, out flag3);
						if (flag3)
						{
							flag = true;
						}
						else
						{
							if (!flag2)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new TimeoutException(SR.GetString("ClientSecurityCloseTimeout", new object[]
								{
									closeAsyncResult.timeoutHelper.OriginalTimeout
								})));
							}
							flag = closeAsyncResult.OnWaitForOutputSessionClose(out flag3);
							if (flag3)
							{
								flag = true;
							}
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
					}
					if (flag)
					{
						closeAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x060084D1 RID: 34001 RVA: 0x001EACC0 File Offset: 0x001E8EC0
				private bool OnWaitForOutputSessionClose(out bool wasAborted)
				{
					wasAborted = false;
					bool flag = false;
					try
					{
						IAsyncResult asyncResult = this.sessionChannel.outputSessionCloseHandle.BeginWait(this.timeoutHelper.RemainingTime(), true, SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult.outputSessionClosedCallback, this);
						if (!asyncResult.CompletedSynchronously)
						{
							return false;
						}
						this.sessionChannel.outputSessionCloseHandle.EndWait(asyncResult);
						flag = true;
					}
					catch (TimeoutException)
					{
						flag = false;
					}
					catch (CommunicationObjectAbortedException)
					{
						if (this.sessionChannel.State != CommunicationState.Closed)
						{
							throw;
						}
						wasAborted = true;
					}
					if (wasAborted)
					{
						return true;
					}
					if (!flag)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new TimeoutException(SR.GetString("ClientSecurityOutputSessionCloseTimeout", new object[]
						{
							this.timeoutHelper.OriginalTimeout
						})));
					}
					return this.CloseCore();
				}

				// Token: 0x060084D2 RID: 34002 RVA: 0x001EAD94 File Offset: 0x001E8F94
				private static void OutputSessionClosedCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult closeAsyncResult = (SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult)result.AsyncState;
					Exception exception = null;
					bool flag = false;
					try
					{
						bool flag2 = false;
						bool flag3 = false;
						try
						{
							closeAsyncResult.sessionChannel.outputSessionCloseHandle.EndWait(result);
							flag2 = true;
						}
						catch (TimeoutException)
						{
							flag2 = false;
						}
						catch (CommunicationObjectFaultedException)
						{
							if (closeAsyncResult.sessionChannel.State != CommunicationState.Closed)
							{
								throw;
							}
							flag3 = true;
						}
						if (!flag3)
						{
							if (!flag2)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new TimeoutException(SR.GetString("ClientSecurityOutputSessionCloseTimeout", new object[]
								{
									closeAsyncResult.timeoutHelper.OriginalTimeout
								})));
							}
							flag = closeAsyncResult.CloseCore();
						}
						else
						{
							flag = true;
						}
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
						flag = true;
					}
					if (flag)
					{
						closeAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x060084D3 RID: 34003 RVA: 0x001EAE80 File Offset: 0x001E9080
				private bool CloseCore()
				{
					IAsyncResult asyncResult = this.sessionChannel.BeginCloseCore(this.timeoutHelper.RemainingTime(), SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult.closeCoreCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					this.sessionChannel.EndCloseCore(asyncResult);
					return true;
				}

				// Token: 0x060084D4 RID: 34004 RVA: 0x001EAEC4 File Offset: 0x001E90C4
				private static void CloseCoreCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult closeAsyncResult = (SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						closeAsyncResult.sessionChannel.EndCloseCore(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					closeAsyncResult.Complete(false, exception);
				}

				// Token: 0x060084D5 RID: 34005 RVA: 0x001EAF20 File Offset: 0x001E9120
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult>(result);
				}

				// Token: 0x04004CE7 RID: 19687
				private static readonly AsyncCallback closeSessionCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult.CloseSessionCallback));

				// Token: 0x04004CE8 RID: 19688
				private static readonly AsyncCallback outputSessionClosedCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult.OutputSessionClosedCallback));

				// Token: 0x04004CE9 RID: 19689
				private static readonly AsyncCallback closeCoreCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.CloseAsyncResult.CloseCoreCallback));

				// Token: 0x04004CEA RID: 19690
				private SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel sessionChannel;

				// Token: 0x04004CEB RID: 19691
				private TimeoutHelper timeoutHelper;
			}

			// Token: 0x02000EE5 RID: 3813
			private class KeyRenewalAsyncResult : TraceAsyncResult
			{
				// Token: 0x060084D7 RID: 34007 RVA: 0x001EAF7B File Offset: 0x001E917B
				public KeyRenewalAsyncResult(Message message, SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel sessionChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.message = message;
					this.sessionChannel = sessionChannel;
					ActionItem.Schedule(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.KeyRenewalAsyncResult.renewKeyCallback, this);
				}

				// Token: 0x060084D8 RID: 34008 RVA: 0x001EAFAC File Offset: 0x001E91AC
				private static void RenewKeyCallback(object state)
				{
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.KeyRenewalAsyncResult keyRenewalAsyncResult = (SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.KeyRenewalAsyncResult)state;
					Exception exception = null;
					try
					{
						using ((keyRenewalAsyncResult.CallbackActivity == null) ? null : ServiceModelActivity.BoundOperation(keyRenewalAsyncResult.CallbackActivity))
						{
							keyRenewalAsyncResult.sessionChannel.RenewKey(keyRenewalAsyncResult.timeoutHelper.RemainingTime());
						}
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					keyRenewalAsyncResult.Complete(false, exception);
				}

				// Token: 0x060084D9 RID: 34009 RVA: 0x001EB030 File Offset: 0x001E9230
				public static Message End(IAsyncResult result, out TimeSpan remainingTime)
				{
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.KeyRenewalAsyncResult keyRenewalAsyncResult = AsyncResult.End<SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.KeyRenewalAsyncResult>(result);
					remainingTime = keyRenewalAsyncResult.timeoutHelper.RemainingTime();
					return keyRenewalAsyncResult.message;
				}

				// Token: 0x04004CEC RID: 19692
				private static readonly Action<object> renewKeyCallback = new Action<object>(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.KeyRenewalAsyncResult.RenewKeyCallback);

				// Token: 0x04004CED RID: 19693
				private Message message;

				// Token: 0x04004CEE RID: 19694
				private SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel sessionChannel;

				// Token: 0x04004CEF RID: 19695
				private TimeoutHelper timeoutHelper;
			}

			// Token: 0x02000EE6 RID: 3814
			internal abstract class SecureSendAsyncResultBase : TraceAsyncResult
			{
				// Token: 0x060084DB RID: 34011 RVA: 0x001EB070 File Offset: 0x001E9270
				protected SecureSendAsyncResultBase(Message message, SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel sessionChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.message = message;
					this.sessionChannel = sessionChannel;
					this.timeoutHelper = new TimeoutHelper(timeout);
					IAsyncResult asyncResult = this.sessionChannel.BeginSecureOutgoingMessage(message, this.timeoutHelper.RemainingTime(), SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResultBase.secureOutgoingMessageCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.message = this.sessionChannel.EndSecureOutgoingMessage(asyncResult, out this.correlationState);
					this.didSecureOutgoingMessageCompleteSynchronously = true;
				}

				// Token: 0x17001D3C RID: 7484
				// (get) Token: 0x060084DC RID: 34012 RVA: 0x001EB0E7 File Offset: 0x001E92E7
				protected bool DidSecureOutgoingMessageCompleteSynchronously
				{
					get
					{
						return this.didSecureOutgoingMessageCompleteSynchronously;
					}
				}

				// Token: 0x17001D3D RID: 7485
				// (get) Token: 0x060084DD RID: 34013 RVA: 0x001EB0EF File Offset: 0x001E92EF
				protected TimeoutHelper TimeoutHelper
				{
					get
					{
						return this.timeoutHelper;
					}
				}

				// Token: 0x17001D3E RID: 7486
				// (get) Token: 0x060084DE RID: 34014 RVA: 0x001EB0F7 File Offset: 0x001E92F7
				protected IClientReliableChannelBinder ChannelBinder
				{
					get
					{
						return this.sessionChannel.ChannelBinder;
					}
				}

				// Token: 0x17001D3F RID: 7487
				// (get) Token: 0x060084DF RID: 34015 RVA: 0x001EB104 File Offset: 0x001E9304
				protected Message Message
				{
					get
					{
						return this.message;
					}
				}

				// Token: 0x17001D40 RID: 7488
				// (get) Token: 0x060084E0 RID: 34016 RVA: 0x001EB10C File Offset: 0x001E930C
				protected SecurityProtocolCorrelationState SecurityCorrelationState
				{
					get
					{
						return this.correlationState;
					}
				}

				// Token: 0x060084E1 RID: 34017
				protected abstract bool OnMessageSecured();

				// Token: 0x060084E2 RID: 34018 RVA: 0x001EB114 File Offset: 0x001E9314
				private static void SecureOutgoingMessageCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResultBase secureSendAsyncResultBase = (SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResultBase)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						secureSendAsyncResultBase.message = secureSendAsyncResultBase.sessionChannel.EndSecureOutgoingMessage(result, out secureSendAsyncResultBase.correlationState);
						flag = secureSendAsyncResultBase.OnMessageSecured();
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						flag = true;
						exception = ex;
					}
					if (flag)
					{
						secureSendAsyncResultBase.Complete(false, exception);
					}
				}

				// Token: 0x04004CF0 RID: 19696
				private static readonly AsyncCallback secureOutgoingMessageCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResultBase.SecureOutgoingMessageCallback));

				// Token: 0x04004CF1 RID: 19697
				private Message message;

				// Token: 0x04004CF2 RID: 19698
				private SecurityProtocolCorrelationState correlationState;

				// Token: 0x04004CF3 RID: 19699
				private SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel sessionChannel;

				// Token: 0x04004CF4 RID: 19700
				private bool didSecureOutgoingMessageCompleteSynchronously;

				// Token: 0x04004CF5 RID: 19701
				private TimeoutHelper timeoutHelper;
			}

			// Token: 0x02000EE7 RID: 3815
			internal sealed class SecureSendAsyncResult : SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResultBase
			{
				// Token: 0x060084E4 RID: 34020 RVA: 0x001EB1A0 File Offset: 0x001E93A0
				public SecureSendAsyncResult(Message message, SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel sessionChannel, TimeSpan timeout, AsyncCallback callback, object state, bool autoCloseMessage) : base(message, sessionChannel, timeout, callback, state)
				{
					this.autoCloseMessage = autoCloseMessage;
					if (!base.DidSecureOutgoingMessageCompleteSynchronously)
					{
						return;
					}
					bool flag = this.OnMessageSecured();
					if (flag)
					{
						base.Complete(true);
					}
				}

				// Token: 0x060084E5 RID: 34021 RVA: 0x001EB1DC File Offset: 0x001E93DC
				protected override bool OnMessageSecured()
				{
					bool flag = true;
					bool result;
					try
					{
						IAsyncResult asyncResult = base.ChannelBinder.BeginSend(base.Message, base.TimeoutHelper.RemainingTime(), SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResult.sendCallback, this);
						if (!asyncResult.CompletedSynchronously)
						{
							flag = false;
							result = false;
						}
						else
						{
							base.ChannelBinder.EndSend(asyncResult);
							result = true;
						}
					}
					finally
					{
						if (flag && this.autoCloseMessage && base.Message != null)
						{
							base.Message.Close();
						}
					}
					return result;
				}

				// Token: 0x060084E6 RID: 34022 RVA: 0x001EB260 File Offset: 0x001E9460
				private static void SendCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResult secureSendAsyncResult = (SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						secureSendAsyncResult.ChannelBinder.EndSend(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					finally
					{
						if (secureSendAsyncResult.autoCloseMessage && secureSendAsyncResult.Message != null)
						{
							secureSendAsyncResult.Message.Close();
						}
						if (secureSendAsyncResult.CallbackActivity != null && DiagnosticUtility.ShouldUseActivity)
						{
							secureSendAsyncResult.CallbackActivity.Stop();
						}
					}
					secureSendAsyncResult.Complete(false, exception);
				}

				// Token: 0x060084E7 RID: 34023 RVA: 0x001EB2FC File Offset: 0x001E94FC
				public static SecurityProtocolCorrelationState End(IAsyncResult result)
				{
					SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResult secureSendAsyncResult = AsyncResult.End<SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResult>(result);
					return secureSendAsyncResult.SecurityCorrelationState;
				}

				// Token: 0x04004CF6 RID: 19702
				private static readonly AsyncCallback sendCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResult.SendCallback));

				// Token: 0x04004CF7 RID: 19703
				private bool autoCloseMessage;
			}

			// Token: 0x02000EE8 RID: 3816
			protected class SoapSecurityOutputSession : ISecureConversationSession, ISecuritySession, ISession, IOutputSession
			{
				// Token: 0x060084E9 RID: 34025 RVA: 0x001EB32E File Offset: 0x001E952E
				public SoapSecurityOutputSession(SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel channel)
				{
					this.channel = channel;
				}

				// Token: 0x060084EA RID: 34026 RVA: 0x001EB340 File Offset: 0x001E9540
				internal void Initialize(SecurityToken sessionToken, SecuritySessionClientSettings<TChannel> settings)
				{
					if (sessionToken == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sessionToken");
					}
					if (settings == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settings");
					}
					Claim primaryIdentityClaim = SecurityUtils.GetPrimaryIdentityClaim(((GenericXmlSecurityToken)sessionToken).AuthorizationPolicies);
					if (primaryIdentityClaim != null)
					{
						this.remoteIdentity = EndpointIdentity.CreateIdentity(primaryIdentityClaim);
					}
					this.standardsManager = settings.SessionProtocolFactory.StandardsManager;
					this.sessionId = this.GetSessionId(sessionToken, this.standardsManager);
					this.sessionTokenIdentifier = settings.IssuedSecurityTokenParameters.CreateKeyIdentifierClause(sessionToken, SecurityTokenReferenceStyle.External);
				}

				// Token: 0x060084EB RID: 34027 RVA: 0x001EB3CC File Offset: 0x001E95CC
				private UniqueId GetSessionId(SecurityToken sessionToken, SecurityStandardsManager standardsManager)
				{
					GenericXmlSecurityToken genericXmlSecurityToken = sessionToken as GenericXmlSecurityToken;
					if (genericXmlSecurityToken == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SessionTokenIsNotGenericXmlToken", new object[]
						{
							sessionToken,
							typeof(GenericXmlSecurityToken)
						})));
					}
					return standardsManager.SecureConversationDriver.GetSecurityContextTokenId(XmlDictionaryReader.CreateDictionaryReader(new XmlNodeReader(genericXmlSecurityToken.TokenXml)));
				}

				// Token: 0x17001D41 RID: 7489
				// (get) Token: 0x060084EC RID: 34028 RVA: 0x001EB42F File Offset: 0x001E962F
				public string Id
				{
					get
					{
						if (this.sessionId == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ChannelMustBeOpenedToGetSessionId")));
						}
						return this.sessionId.ToString();
					}
				}

				// Token: 0x17001D42 RID: 7490
				// (get) Token: 0x060084ED RID: 34029 RVA: 0x001EB464 File Offset: 0x001E9664
				public EndpointIdentity RemoteIdentity
				{
					get
					{
						return this.remoteIdentity;
					}
				}

				// Token: 0x060084EE RID: 34030 RVA: 0x001EB46C File Offset: 0x001E966C
				public void WriteSessionTokenIdentifier(XmlDictionaryWriter writer)
				{
					this.channel.ThrowIfDisposedOrNotOpen();
					this.standardsManager.SecurityTokenSerializer.WriteKeyIdentifierClause(writer, this.sessionTokenIdentifier);
				}

				// Token: 0x060084EF RID: 34031 RVA: 0x001EB490 File Offset: 0x001E9690
				public bool TryReadSessionTokenIdentifier(XmlReader reader)
				{
					this.channel.ThrowIfDisposedOrNotOpen();
					if (!this.standardsManager.SecurityTokenSerializer.CanReadKeyIdentifierClause(reader))
					{
						return false;
					}
					SecurityContextKeyIdentifierClause securityContextKeyIdentifierClause = this.standardsManager.SecurityTokenSerializer.ReadKeyIdentifierClause(reader) as SecurityContextKeyIdentifierClause;
					return securityContextKeyIdentifierClause != null && securityContextKeyIdentifierClause.Matches(this.sessionId, null);
				}

				// Token: 0x04004CF8 RID: 19704
				private SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel channel;

				// Token: 0x04004CF9 RID: 19705
				private EndpointIdentity remoteIdentity;

				// Token: 0x04004CFA RID: 19706
				private UniqueId sessionId;

				// Token: 0x04004CFB RID: 19707
				private SecurityKeyIdentifierClause sessionTokenIdentifier;

				// Token: 0x04004CFC RID: 19708
				private SecurityStandardsManager standardsManager;
			}
		}

		// Token: 0x02000B56 RID: 2902
		private abstract class ClientSecuritySimplexSessionChannel : SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel
		{
			// Token: 0x06007182 RID: 29058 RVA: 0x001A787B File Offset: 0x001A5A7B
			protected ClientSecuritySimplexSessionChannel(SecuritySessionClientSettings<TChannel> settings, EndpointAddress to, Uri via) : base(settings, to, via)
			{
				this.outputSession = new SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SoapSecurityOutputSession(this);
			}

			// Token: 0x17001A6E RID: 6766
			// (get) Token: 0x06007183 RID: 29059 RVA: 0x001A7892 File Offset: 0x001A5A92
			public IOutputSession Session
			{
				get
				{
					return this.outputSession;
				}
			}

			// Token: 0x17001A6F RID: 6767
			// (get) Token: 0x06007184 RID: 29060 RVA: 0x001A789A File Offset: 0x001A5A9A
			protected override bool ExpectClose
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001A70 RID: 6768
			// (get) Token: 0x06007185 RID: 29061 RVA: 0x001A789D File Offset: 0x001A5A9D
			protected override string SessionId
			{
				get
				{
					return this.Session.Id;
				}
			}

			// Token: 0x06007186 RID: 29062 RVA: 0x001A78AA File Offset: 0x001A5AAA
			protected override void InitializeSession(SecurityToken sessionToken)
			{
				this.outputSession.Initialize(sessionToken, base.Settings);
			}

			// Token: 0x04004078 RID: 16504
			private SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SoapSecurityOutputSession outputSession;
		}

		// Token: 0x02000B57 RID: 2903
		private sealed class SecurityRequestSessionChannel : SecuritySessionClientSettings<TChannel>.ClientSecuritySimplexSessionChannel, IRequestSessionChannel, IRequestChannel, IChannel, ICommunicationObject, ISessionChannel<IOutputSession>
		{
			// Token: 0x06007187 RID: 29063 RVA: 0x001A78BE File Offset: 0x001A5ABE
			public SecurityRequestSessionChannel(SecuritySessionClientSettings<TChannel> settings, EndpointAddress to, Uri via) : base(settings, to, via)
			{
			}

			// Token: 0x17001A71 RID: 6769
			// (get) Token: 0x06007188 RID: 29064 RVA: 0x001A78C9 File Offset: 0x001A5AC9
			protected override bool CanDoSecurityCorrelation
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06007189 RID: 29065 RVA: 0x001A78CC File Offset: 0x001A5ACC
			protected override SecurityProtocolCorrelationState CloseOutputSession(TimeSpan timeout)
			{
				base.ThrowIfFaulted();
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				SecurityProtocolCorrelationState correlationState = base.CloseOutputSession(timeoutHelper.RemainingTime());
				Message message = base.ReceiveInternal(timeoutHelper.RemainingTime(), correlationState);
				if (message != null)
				{
					using (message)
					{
						ProtocolException exception = ProtocolException.ReceiveShutdownReturnedNonNull(message);
						throw TraceUtility.ThrowHelperWarning(exception, message);
					}
				}
				return null;
			}

			// Token: 0x0600718A RID: 29066 RVA: 0x001A7938 File Offset: 0x001A5B38
			protected override IAsyncResult BeginCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state)
			{
				base.ThrowIfFaulted();
				return new SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.CloseOutputSessionAsyncResult(this, timeout, callback, state);
			}

			// Token: 0x0600718B RID: 29067 RVA: 0x001A7949 File Offset: 0x001A5B49
			protected override SecurityProtocolCorrelationState EndCloseOutputSession(IAsyncResult result)
			{
				SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.CloseOutputSessionAsyncResult.End(result);
				return null;
			}

			// Token: 0x0600718C RID: 29068 RVA: 0x001A7952 File Offset: 0x001A5B52
			private IAsyncResult BeginBaseCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return base.BeginCloseOutputSession(timeout, callback, state);
			}

			// Token: 0x0600718D RID: 29069 RVA: 0x001A795D File Offset: 0x001A5B5D
			private SecurityProtocolCorrelationState EndBaseCloseOutputSession(IAsyncResult result)
			{
				return base.EndCloseOutputSession(result);
			}

			// Token: 0x0600718E RID: 29070 RVA: 0x001A7966 File Offset: 0x001A5B66
			public Message Request(Message message)
			{
				return this.Request(message, base.DefaultSendTimeout);
			}

			// Token: 0x0600718F RID: 29071 RVA: 0x001A7978 File Offset: 0x001A5B78
			private Message ProcessReply(Message reply, TimeSpan timeout, SecurityProtocolCorrelationState correlationState)
			{
				if (reply == null)
				{
					return null;
				}
				Message message = null;
				MessageFault messageFault = null;
				Exception ex = null;
				try
				{
					message = base.ProcessIncomingMessage(reply, timeout, correlationState, out messageFault);
				}
				catch (MessageSecurityException)
				{
					if (reply.IsFault)
					{
						MessageFault fault = MessageFault.CreateFault(reply, 16384);
						if (SecurityUtils.IsSecurityFault(fault, base.Settings.standardsManager))
						{
							ex = SecurityUtils.CreateSecurityFaultException(fault);
						}
					}
					if (ex == null)
					{
						throw;
					}
				}
				if (ex != null)
				{
					base.Fault(ex);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
				}
				if (message == null && messageFault != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("SecuritySessionFaultReplyWasSent"), new FaultException(messageFault)));
				}
				return message;
			}

			// Token: 0x06007190 RID: 29072 RVA: 0x001A7A28 File Offset: 0x001A5C28
			public Message Request(Message message, TimeSpan timeout)
			{
				base.ThrowIfFaulted();
				base.CheckOutputOpen();
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				SecurityProtocolCorrelationState correlationState = base.SecureOutgoingMessage(ref message, timeoutHelper.RemainingTime());
				Message reply = base.ChannelBinder.Request(message, timeoutHelper.RemainingTime());
				return this.ProcessReply(reply, timeoutHelper.RemainingTime(), correlationState);
			}

			// Token: 0x06007191 RID: 29073 RVA: 0x001A7A7C File Offset: 0x001A5C7C
			public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state)
			{
				return this.BeginRequest(message, base.DefaultSendTimeout, callback, state);
			}

			// Token: 0x06007192 RID: 29074 RVA: 0x001A7A8D File Offset: 0x001A5C8D
			public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				base.ThrowIfFaulted();
				base.CheckOutputOpen();
				return new SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.SecureRequestAsyncResult(message, this, timeout, callback, state);
			}

			// Token: 0x06007193 RID: 29075 RVA: 0x001A7AA8 File Offset: 0x001A5CA8
			public Message EndRequest(IAsyncResult result)
			{
				SecurityProtocolCorrelationState correlationState;
				TimeSpan timeout;
				Message reply = SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.SecureRequestAsyncResult.EndAsReply(result, out correlationState, out timeout);
				return this.ProcessReply(reply, timeout, correlationState);
			}

			// Token: 0x02000EE9 RID: 3817
			private sealed class SecureRequestAsyncResult : SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResultBase
			{
				// Token: 0x060084F0 RID: 34032 RVA: 0x001EB4E8 File Offset: 0x001E96E8
				public SecureRequestAsyncResult(Message request, SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel sessionChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(request, sessionChannel, timeout, callback, state)
				{
					if (!base.DidSecureOutgoingMessageCompleteSynchronously)
					{
						return;
					}
					bool flag = this.OnMessageSecured();
					if (flag)
					{
						base.Complete(true);
					}
				}

				// Token: 0x060084F1 RID: 34033 RVA: 0x001EB51C File Offset: 0x001E971C
				protected override bool OnMessageSecured()
				{
					IAsyncResult asyncResult = base.ChannelBinder.BeginRequest(base.Message, base.TimeoutHelper.RemainingTime(), SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.SecureRequestAsyncResult.requestCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					this.reply = base.ChannelBinder.EndRequest(asyncResult);
					return true;
				}

				// Token: 0x060084F2 RID: 34034 RVA: 0x001EB56C File Offset: 0x001E976C
				private static void RequestCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.SecureRequestAsyncResult secureRequestAsyncResult = (SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.SecureRequestAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						secureRequestAsyncResult.reply = secureRequestAsyncResult.ChannelBinder.EndRequest(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					secureRequestAsyncResult.Complete(false, exception);
				}

				// Token: 0x060084F3 RID: 34035 RVA: 0x001EB5CC File Offset: 0x001E97CC
				public static Message EndAsReply(IAsyncResult result, out SecurityProtocolCorrelationState correlationState, out TimeSpan remainingTime)
				{
					SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.SecureRequestAsyncResult secureRequestAsyncResult = AsyncResult.End<SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.SecureRequestAsyncResult>(result);
					correlationState = secureRequestAsyncResult.SecurityCorrelationState;
					remainingTime = secureRequestAsyncResult.TimeoutHelper.RemainingTime();
					return secureRequestAsyncResult.reply;
				}

				// Token: 0x04004CFD RID: 19709
				private static readonly AsyncCallback requestCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.SecureRequestAsyncResult.RequestCallback));

				// Token: 0x04004CFE RID: 19710
				private Message reply;
			}

			// Token: 0x02000EEA RID: 3818
			private class CloseOutputSessionAsyncResult : TraceAsyncResult
			{
				// Token: 0x060084F5 RID: 34037 RVA: 0x001EB61C File Offset: 0x001E981C
				public CloseOutputSessionAsyncResult(SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel requestChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.requestChannel = requestChannel;
					IAsyncResult asyncResult = this.requestChannel.BeginBaseCloseOutputSession(this.timeoutHelper.RemainingTime(), SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.CloseOutputSessionAsyncResult.baseCloseOutputSessionCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.correlationState = this.requestChannel.EndBaseCloseOutputSession(asyncResult);
					bool flag = this.OnBaseOutputSessionClosed();
					if (flag)
					{
						base.Complete(true);
					}
				}

				// Token: 0x060084F6 RID: 34038 RVA: 0x001EB690 File Offset: 0x001E9890
				private static void BaseCloseOutputSessionCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.CloseOutputSessionAsyncResult closeOutputSessionAsyncResult = (SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.CloseOutputSessionAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						closeOutputSessionAsyncResult.correlationState = closeOutputSessionAsyncResult.requestChannel.EndBaseCloseOutputSession(result);
						flag = closeOutputSessionAsyncResult.OnBaseOutputSessionClosed();
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						flag = true;
						exception = ex;
					}
					if (flag)
					{
						closeOutputSessionAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x060084F7 RID: 34039 RVA: 0x001EB700 File Offset: 0x001E9900
				private bool OnBaseOutputSessionClosed()
				{
					IAsyncResult asyncResult = this.requestChannel.BeginReceiveInternal(this.timeoutHelper.RemainingTime(), this.correlationState, SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.CloseOutputSessionAsyncResult.receiveInternalCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					Message message = this.requestChannel.EndReceiveInternal(asyncResult);
					return this.OnMessageReceived(message);
				}

				// Token: 0x060084F8 RID: 34040 RVA: 0x001EB750 File Offset: 0x001E9950
				private static void ReceiveInternalCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.CloseOutputSessionAsyncResult closeOutputSessionAsyncResult = (SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.CloseOutputSessionAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception = null;
					try
					{
						Message message = closeOutputSessionAsyncResult.requestChannel.EndReceiveInternal(result);
						flag = closeOutputSessionAsyncResult.OnMessageReceived(message);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						flag = true;
						exception = ex;
					}
					if (flag)
					{
						closeOutputSessionAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x060084F9 RID: 34041 RVA: 0x001EB7BC File Offset: 0x001E99BC
				private bool OnMessageReceived(Message message)
				{
					if (message != null)
					{
						try
						{
							ProtocolException exception = ProtocolException.ReceiveShutdownReturnedNonNull(message);
							throw TraceUtility.ThrowHelperWarning(exception, message);
						}
						finally
						{
							if (message != null)
							{
								((IDisposable)message).Dispose();
							}
						}
					}
					return true;
				}

				// Token: 0x060084FA RID: 34042 RVA: 0x001EB7F8 File Offset: 0x001E99F8
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.CloseOutputSessionAsyncResult>(result);
				}

				// Token: 0x04004CFF RID: 19711
				private static readonly AsyncCallback baseCloseOutputSessionCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.CloseOutputSessionAsyncResult.BaseCloseOutputSessionCallback));

				// Token: 0x04004D00 RID: 19712
				private static readonly AsyncCallback receiveInternalCallback = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel.CloseOutputSessionAsyncResult.ReceiveInternalCallback));

				// Token: 0x04004D01 RID: 19713
				private SecuritySessionClientSettings<TChannel>.SecurityRequestSessionChannel requestChannel;

				// Token: 0x04004D02 RID: 19714
				private SecurityProtocolCorrelationState correlationState;

				// Token: 0x04004D03 RID: 19715
				private TimeoutHelper timeoutHelper;
			}
		}

		// Token: 0x02000B58 RID: 2904
		private class ClientSecurityDuplexSessionChannel : SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel, IDuplexSessionChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel, ISessionChannel<IDuplexSession>
		{
			// Token: 0x06007194 RID: 29076 RVA: 0x001A7ACC File Offset: 0x001A5CCC
			public ClientSecurityDuplexSessionChannel(SecuritySessionClientSettings<TChannel> settings, EndpointAddress to, Uri via) : base(settings, to, via)
			{
				this.session = new SecuritySessionClientSettings<TChannel>.ClientSecurityDuplexSessionChannel.SoapSecurityClientDuplexSession(this);
				this.queue = TraceUtility.CreateInputQueue<Message>();
				this.startReceiving = new Action(this.StartReceiving);
				this.completeLater = new Action<object>(this.CompleteLater);
			}

			// Token: 0x17001A72 RID: 6770
			// (get) Token: 0x06007195 RID: 29077 RVA: 0x001A7B1D File Offset: 0x001A5D1D
			public EndpointAddress LocalAddress
			{
				get
				{
					return base.InternalLocalAddress;
				}
			}

			// Token: 0x17001A73 RID: 6771
			// (get) Token: 0x06007196 RID: 29078 RVA: 0x001A7B25 File Offset: 0x001A5D25
			public IDuplexSession Session
			{
				get
				{
					return this.session;
				}
			}

			// Token: 0x17001A74 RID: 6772
			// (get) Token: 0x06007197 RID: 29079 RVA: 0x001A7B2D File Offset: 0x001A5D2D
			protected override bool ExpectClose
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001A75 RID: 6773
			// (get) Token: 0x06007198 RID: 29080 RVA: 0x001A7B30 File Offset: 0x001A5D30
			protected override string SessionId
			{
				get
				{
					return this.session.Id;
				}
			}

			// Token: 0x06007199 RID: 29081 RVA: 0x001A7B3D File Offset: 0x001A5D3D
			public Message Receive()
			{
				return this.Receive(base.DefaultReceiveTimeout);
			}

			// Token: 0x0600719A RID: 29082 RVA: 0x001A7B4B File Offset: 0x001A5D4B
			public Message Receive(TimeSpan timeout)
			{
				return InputChannel.HelpReceive(this, timeout);
			}

			// Token: 0x0600719B RID: 29083 RVA: 0x001A7B54 File Offset: 0x001A5D54
			public IAsyncResult BeginReceive(AsyncCallback callback, object state)
			{
				return this.BeginReceive(base.DefaultReceiveTimeout, callback, state);
			}

			// Token: 0x0600719C RID: 29084 RVA: 0x001A7B64 File Offset: 0x001A5D64
			public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return InputChannel.HelpBeginReceive(this, timeout, callback, state);
			}

			// Token: 0x0600719D RID: 29085 RVA: 0x001A7B6F File Offset: 0x001A5D6F
			public Message EndReceive(IAsyncResult result)
			{
				return InputChannel.HelpEndReceive(result);
			}

			// Token: 0x0600719E RID: 29086 RVA: 0x001A7B77 File Offset: 0x001A5D77
			public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				base.ThrowIfFaulted();
				return this.queue.BeginDequeue(timeout, callback, state);
			}

			// Token: 0x0600719F RID: 29087 RVA: 0x001A7B90 File Offset: 0x001A5D90
			public bool EndTryReceive(IAsyncResult result, out Message message)
			{
				bool result2 = this.queue.EndDequeue(result, out message);
				if (message == null)
				{
					base.ThrowIfFaulted();
				}
				return result2;
			}

			// Token: 0x060071A0 RID: 29088 RVA: 0x001A7BB6 File Offset: 0x001A5DB6
			protected override void OnOpened()
			{
				base.OnOpened();
				this.StartReceiving();
			}

			// Token: 0x060071A1 RID: 29089 RVA: 0x001A7BC4 File Offset: 0x001A5DC4
			public bool TryReceive(TimeSpan timeout, out Message message)
			{
				base.ThrowIfFaulted();
				bool result = this.queue.Dequeue(timeout, out message);
				if (message == null)
				{
					base.ThrowIfFaulted();
				}
				return result;
			}

			// Token: 0x060071A2 RID: 29090 RVA: 0x001A7BF0 File Offset: 0x001A5DF0
			public void Send(Message message)
			{
				this.Send(message, base.DefaultSendTimeout);
			}

			// Token: 0x060071A3 RID: 29091 RVA: 0x001A7C00 File Offset: 0x001A5E00
			public void Send(Message message, TimeSpan timeout)
			{
				base.ThrowIfFaulted();
				base.CheckOutputOpen();
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.SecureOutgoingMessage(ref message, timeoutHelper.RemainingTime());
				base.ChannelBinder.Send(message, timeoutHelper.RemainingTime());
			}

			// Token: 0x060071A4 RID: 29092 RVA: 0x001A7C44 File Offset: 0x001A5E44
			public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
			{
				return this.BeginSend(message, base.DefaultSendTimeout, callback, state);
			}

			// Token: 0x060071A5 RID: 29093 RVA: 0x001A7C55 File Offset: 0x001A5E55
			public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				base.ThrowIfFaulted();
				base.CheckOutputOpen();
				return new SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResult(message, this, timeout, callback, state, false);
			}

			// Token: 0x060071A6 RID: 29094 RVA: 0x001A7C6F File Offset: 0x001A5E6F
			public void EndSend(IAsyncResult result)
			{
				SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SecureSendAsyncResult.End(result);
			}

			// Token: 0x060071A7 RID: 29095 RVA: 0x001A7C78 File Offset: 0x001A5E78
			protected override void InitializeSession(SecurityToken sessionToken)
			{
				this.session.Initialize(sessionToken, base.Settings);
			}

			// Token: 0x060071A8 RID: 29096 RVA: 0x001A7C8C File Offset: 0x001A5E8C
			private void StartReceiving()
			{
				IAsyncResult asyncResult = this.IssueReceive();
				if (asyncResult != null && asyncResult.CompletedSynchronously)
				{
					ActionItem.Schedule(this.completeLater, asyncResult);
				}
			}

			// Token: 0x060071A9 RID: 29097 RVA: 0x001A7CB8 File Offset: 0x001A5EB8
			private IAsyncResult IssueReceive()
			{
				while (base.State != CommunicationState.Closed && base.State != CommunicationState.Faulted && !base.IsInputClosed)
				{
					IAsyncResult result;
					try
					{
						result = base.BeginReceiveInternal(TimeSpan.MaxValue, null, SecuritySessionClientSettings<TChannel>.ClientSecurityDuplexSessionChannel.onReceive, this);
					}
					catch (CommunicationException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
						continue;
					}
					catch (TimeoutException ex)
					{
						if (TD.ReceiveTimeoutIsEnabled())
						{
							TD.ReceiveTimeout(ex.Message);
						}
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
						continue;
					}
					return result;
				}
				return null;
			}

			// Token: 0x060071AA RID: 29098 RVA: 0x001A7D3C File Offset: 0x001A5F3C
			private void CompleteLater(object obj)
			{
				this.CompleteReceive((IAsyncResult)obj);
			}

			// Token: 0x060071AB RID: 29099 RVA: 0x001A7D4A File Offset: 0x001A5F4A
			private static void OnReceive(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				((SecuritySessionClientSettings<TChannel>.ClientSecurityDuplexSessionChannel)result.AsyncState).CompleteReceive(result);
			}

			// Token: 0x060071AC RID: 29100 RVA: 0x001A7D68 File Offset: 0x001A5F68
			private void CompleteReceive(IAsyncResult result)
			{
				Message message = null;
				bool flag = false;
				try
				{
					message = base.EndReceiveInternal(result);
					flag = true;
				}
				catch (MessageSecurityException)
				{
					flag = false;
				}
				catch (CommunicationException exception)
				{
					flag = true;
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				catch (TimeoutException ex)
				{
					flag = true;
					if (TD.ReceiveTimeoutIsEnabled())
					{
						TD.ReceiveTimeout(ex.Message);
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				}
				if (flag)
				{
					IAsyncResult asyncResult = this.IssueReceive();
					if (asyncResult != null && asyncResult.CompletedSynchronously)
					{
						ActionItem.Schedule(this.completeLater, asyncResult);
					}
				}
				if (message != null)
				{
					try
					{
						this.queue.EnqueueAndDispatch(message);
					}
					catch (Exception exception2)
					{
						if (Fx.IsFatal(exception2))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Warning);
					}
				}
			}

			// Token: 0x060071AD RID: 29101 RVA: 0x001A7E38 File Offset: 0x001A6038
			protected override void AbortCore()
			{
				try
				{
					this.queue.Dispose();
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				catch (TimeoutException ex)
				{
					if (TD.CloseTimeoutIsEnabled())
					{
						TD.CloseTimeout(ex.Message);
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				}
				base.AbortCore();
			}

			// Token: 0x060071AE RID: 29102 RVA: 0x001A7E9C File Offset: 0x001A609C
			public bool WaitForMessage(TimeSpan timeout)
			{
				return this.queue.WaitForItem(timeout);
			}

			// Token: 0x060071AF RID: 29103 RVA: 0x001A7EAA File Offset: 0x001A60AA
			public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.queue.BeginWaitForItem(timeout, callback, state);
			}

			// Token: 0x060071B0 RID: 29104 RVA: 0x001A7EBA File Offset: 0x001A60BA
			public bool EndWaitForMessage(IAsyncResult result)
			{
				return this.queue.EndWaitForItem(result);
			}

			// Token: 0x060071B1 RID: 29105 RVA: 0x001A7EC8 File Offset: 0x001A60C8
			protected override void OnFaulted()
			{
				this.queue.Shutdown(() => base.GetPendingException());
				base.OnFaulted();
			}

			// Token: 0x060071B2 RID: 29106 RVA: 0x001A7EE7 File Offset: 0x001A60E7
			protected override bool OnCloseResponseReceived()
			{
				if (base.OnCloseResponseReceived())
				{
					this.queue.Shutdown();
					return true;
				}
				return false;
			}

			// Token: 0x060071B3 RID: 29107 RVA: 0x001A7EFF File Offset: 0x001A60FF
			protected override bool OnCloseReceived()
			{
				if (base.OnCloseReceived())
				{
					this.queue.Shutdown();
					return true;
				}
				return false;
			}

			// Token: 0x04004079 RID: 16505
			private static AsyncCallback onReceive = Fx.ThunkCallback(new AsyncCallback(SecuritySessionClientSettings<TChannel>.ClientSecurityDuplexSessionChannel.OnReceive));

			// Token: 0x0400407A RID: 16506
			private SecuritySessionClientSettings<TChannel>.ClientSecurityDuplexSessionChannel.SoapSecurityClientDuplexSession session;

			// Token: 0x0400407B RID: 16507
			private InputQueue<Message> queue;

			// Token: 0x0400407C RID: 16508
			private Action startReceiving;

			// Token: 0x0400407D RID: 16509
			private Action<object> completeLater;

			// Token: 0x02000EEB RID: 3819
			private class SoapSecurityClientDuplexSession : SecuritySessionClientSettings<TChannel>.ClientSecuritySessionChannel.SoapSecurityOutputSession, IDuplexSession, IInputSession, ISession, IOutputSession
			{
				// Token: 0x060084FC RID: 34044 RVA: 0x001EB82F File Offset: 0x001E9A2F
				public SoapSecurityClientDuplexSession(SecuritySessionClientSettings<TChannel>.ClientSecurityDuplexSessionChannel channel) : base(channel)
				{
					this.channel = channel;
				}

				// Token: 0x060084FD RID: 34045 RVA: 0x001EB83F File Offset: 0x001E9A3F
				internal new void Initialize(SecurityToken sessionToken, SecuritySessionClientSettings<TChannel> settings)
				{
					base.Initialize(sessionToken, settings);
					this.initialized = true;
				}

				// Token: 0x060084FE RID: 34046 RVA: 0x001EB850 File Offset: 0x001E9A50
				private void CheckInitialized()
				{
					if (!this.initialized)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ChannelNotOpen")));
					}
				}

				// Token: 0x060084FF RID: 34047 RVA: 0x001EB874 File Offset: 0x001E9A74
				public void CloseOutputSession()
				{
					this.CloseOutputSession(this.channel.DefaultCloseTimeout);
				}

				// Token: 0x06008500 RID: 34048 RVA: 0x001EB888 File Offset: 0x001E9A88
				public void CloseOutputSession(TimeSpan timeout)
				{
					this.CheckInitialized();
					this.channel.ThrowIfFaulted();
					this.channel.ThrowIfNotOpened();
					Exception ex = null;
					try
					{
						this.channel.CloseOutputSession(timeout);
					}
					catch (CommunicationObjectAbortedException)
					{
						if (this.channel.State != CommunicationState.Closed)
						{
							throw;
						}
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
					}
					if (ex != null)
					{
						this.channel.Fault(ex);
						throw ex;
					}
				}

				// Token: 0x06008501 RID: 34049 RVA: 0x001EB914 File Offset: 0x001E9B14
				public IAsyncResult BeginCloseOutputSession(AsyncCallback callback, object state)
				{
					return this.BeginCloseOutputSession(this.channel.DefaultCloseTimeout, callback, state);
				}

				// Token: 0x06008502 RID: 34050 RVA: 0x001EB92C File Offset: 0x001E9B2C
				public IAsyncResult BeginCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state)
				{
					this.CheckInitialized();
					this.channel.ThrowIfFaulted();
					this.channel.ThrowIfNotOpened();
					Exception ex = null;
					try
					{
						return this.channel.BeginCloseOutputSession(timeout, callback, state);
					}
					catch (CommunicationObjectAbortedException)
					{
						if (this.channel.State != CommunicationState.Closed)
						{
							throw;
						}
						return new CompletedAsyncResult(callback, state);
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
					}
					if (ex == null)
					{
						return null;
					}
					this.channel.Fault(ex);
					if (ex is CommunicationException)
					{
						throw ex;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
				}

				// Token: 0x06008503 RID: 34051 RVA: 0x001EB9D8 File Offset: 0x001E9BD8
				public void EndCloseOutputSession(IAsyncResult result)
				{
					if (result is CompletedAsyncResult)
					{
						CompletedAsyncResult.End(result);
						return;
					}
					Exception ex = null;
					try
					{
						this.channel.EndCloseOutputSession(result);
					}
					catch (CommunicationObjectAbortedException)
					{
						if (this.channel.State != CommunicationState.Closed)
						{
							throw;
						}
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
					}
					if (ex == null)
					{
						return;
					}
					this.channel.Fault(ex);
					if (ex is CommunicationException)
					{
						throw ex;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
				}

				// Token: 0x04004D04 RID: 19716
				private SecuritySessionClientSettings<TChannel>.ClientSecurityDuplexSessionChannel channel;

				// Token: 0x04004D05 RID: 19717
				private bool initialized;
			}
		}
	}
}
