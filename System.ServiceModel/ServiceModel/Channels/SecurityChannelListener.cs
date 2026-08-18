using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime;
using System.Security;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Activation;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200098B RID: 2443
	internal sealed class SecurityChannelListener<TChannel> : DelegatingChannelListener<TChannel> where TChannel : class, IChannel
	{
		// Token: 0x06005EA3 RID: 24227 RVA: 0x0015DEAC File Offset: 0x0015C0AC
		public SecurityChannelListener(SecurityBindingElement bindingElement, BindingContext context) : base(true, context.Binding)
		{
			this.securityCapabilities = bindingElement.GetProperty<ISecurityCapabilities>(context);
			this.extendedProtectionPolicyHasSupport = SecurityUtils.IsSecurityBindingSuitableForChannelBinding(bindingElement as TransportSecurityBindingElement);
		}

		// Token: 0x06005EA4 RID: 24228 RVA: 0x0015DEE0 File Offset: 0x0015C0E0
		internal SecurityChannelListener(SecurityProtocolFactory protocolFactory, IChannelListener innerChannelListener) : base(true, null, innerChannelListener)
		{
			this.securityProtocolFactory = protocolFactory;
		}

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06005EA5 RID: 24229 RVA: 0x0015DEF9 File Offset: 0x0015C0F9
		public ChannelBuilder ChannelBuilder
		{
			get
			{
				base.ThrowIfDisposed();
				return this.channelBuilder;
			}
		}

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x06005EA6 RID: 24230 RVA: 0x0015DF07 File Offset: 0x0015C107
		// (set) Token: 0x06005EA7 RID: 24231 RVA: 0x0015DF15 File Offset: 0x0015C115
		public SecurityProtocolFactory SecurityProtocolFactory
		{
			get
			{
				base.ThrowIfDisposed();
				return this.securityProtocolFactory;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				base.ThrowIfDisposedOrImmutable();
				this.securityProtocolFactory = value;
			}
		}

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x06005EA8 RID: 24232 RVA: 0x0015DF37 File Offset: 0x0015C137
		// (set) Token: 0x06005EA9 RID: 24233 RVA: 0x0015DF3F File Offset: 0x0015C13F
		public bool SessionMode
		{
			get
			{
				return this.sessionMode;
			}
			set
			{
				base.ThrowIfDisposedOrImmutable();
				this.sessionMode = value;
			}
		}

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x06005EAA RID: 24234 RVA: 0x0015DF50 File Offset: 0x0015C150
		public SecuritySessionServerSettings SessionServerSettings
		{
			get
			{
				base.ThrowIfDisposed();
				if (this.sessionServerSettings == null)
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (this.sessionServerSettings == null)
						{
							SecuritySessionServerSettings securitySessionServerSettings = new SecuritySessionServerSettings();
							Thread.MemoryBarrier();
							this.sessionServerSettings = securitySessionServerSettings;
						}
					}
				}
				return this.sessionServerSettings;
			}
		}

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x06005EAB RID: 24235 RVA: 0x0015DFB8 File Offset: 0x0015C1B8
		private bool SupportsDuplex
		{
			get
			{
				this.ThrowIfProtocolFactoryNotSet();
				return this.securityProtocolFactory.SupportsDuplex;
			}
		}

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x06005EAC RID: 24236 RVA: 0x0015DFCB File Offset: 0x0015C1CB
		private bool SupportsRequestReply
		{
			get
			{
				this.ThrowIfProtocolFactoryNotSet();
				return this.securityProtocolFactory.SupportsRequestReply;
			}
		}

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x06005EAD RID: 24237 RVA: 0x0015DFDE File Offset: 0x0015C1DE
		// (set) Token: 0x06005EAE RID: 24238 RVA: 0x0015DFE6 File Offset: 0x0015C1E6
		public bool SendUnsecuredFaults
		{
			get
			{
				return this.sendUnsecuredFaults;
			}
			set
			{
				base.ThrowIfDisposedOrImmutable();
				this.sendUnsecuredFaults = value;
			}
		}

		// Token: 0x06005EAF RID: 24239 RVA: 0x0015DFF8 File Offset: 0x0015C1F8
		private void ComputeEndpointIdentity()
		{
			EndpointIdentity endpointIdentity = null;
			if (base.State == CommunicationState.Opened)
			{
				if (this.SecurityProtocolFactory != null)
				{
					endpointIdentity = this.SecurityProtocolFactory.GetIdentityOfSelf();
				}
				else if (this.SessionServerSettings != null && this.SessionServerSettings.SessionProtocolFactory != null)
				{
					endpointIdentity = this.SessionServerSettings.SessionProtocolFactory.GetIdentityOfSelf();
				}
			}
			if (endpointIdentity == null)
			{
				endpointIdentity = base.GetProperty<EndpointIdentity>();
			}
			this.identity = endpointIdentity;
		}

		// Token: 0x06005EB0 RID: 24240 RVA: 0x0015E060 File Offset: 0x0015C260
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(SecurityProtocolFactory))
			{
				return (T)((object)this.SecurityProtocolFactory);
			}
			if (this.SessionMode && typeof(T) == typeof(IListenerSecureConversationSessionSettings))
			{
				return (T)((object)this.SessionServerSettings);
			}
			if (typeof(T) == typeof(EndpointIdentity))
			{
				return (T)((object)this.identity);
			}
			if (typeof(T) == typeof(Collection<ISecurityContextSecurityTokenCache>))
			{
				if (this.SecurityProtocolFactory != null)
				{
					return (T)((object)this.SecurityProtocolFactory.GetProperty<Collection<ISecurityContextSecurityTokenCache>>());
				}
				return (T)((object)base.GetProperty<Collection<ISecurityContextSecurityTokenCache>>());
			}
			else
			{
				if (typeof(T) == typeof(ISecurityCapabilities))
				{
					return (T)((object)this.securityCapabilities);
				}
				if (typeof(T) == typeof(ILogonTokenCacheManager))
				{
					List<ILogonTokenCacheManager> list = new List<ILogonTokenCacheManager>();
					if (this.SecurityProtocolFactory != null && this.securityProtocolFactory.ChannelSupportingTokenAuthenticatorSpecification.Count > 0)
					{
						foreach (SupportingTokenAuthenticatorSpecification supportingTokenAuthenticatorSpecification in this.securityProtocolFactory.ChannelSupportingTokenAuthenticatorSpecification)
						{
							if (supportingTokenAuthenticatorSpecification.TokenAuthenticator is ILogonTokenCacheManager)
							{
								list.Add(supportingTokenAuthenticatorSpecification.TokenAuthenticator as ILogonTokenCacheManager);
							}
						}
					}
					if (this.SessionServerSettings.SessionProtocolFactory != null && this.SessionServerSettings.SessionTokenAuthenticator is ILogonTokenCacheManager)
					{
						list.Add(this.SessionServerSettings.SessionTokenAuthenticator as ILogonTokenCacheManager);
					}
					return (T)((object)new SecurityChannelListener<TChannel>.AggregateLogonTokenCacheManager(new ReadOnlyCollection<ILogonTokenCacheManager>(list)));
				}
				return base.GetProperty<T>();
			}
		}

		// Token: 0x06005EB1 RID: 24241 RVA: 0x0015E238 File Offset: 0x0015C438
		protected override void OnAbort()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.hasSecurityStateReference)
				{
					this.hasSecurityStateReference = false;
					if (this.settingsLifetimeManager != null)
					{
						this.settingsLifetimeManager.Abort();
					}
				}
			}
			base.OnAbort();
		}

		// Token: 0x06005EB2 RID: 24242 RVA: 0x0015E29C File Offset: 0x0015C49C
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.SessionMode && this.sessionServerSettings != null)
			{
				this.sessionServerSettings.StopAcceptingNewWork();
			}
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(this.OnBeginCloseSharedState), new ChainedEndHandler(this.OnEndCloseSharedState), new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose));
		}

		// Token: 0x06005EB3 RID: 24243 RVA: 0x0015E2FC File Offset: 0x0015C4FC
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06005EB4 RID: 24244 RVA: 0x0015E304 File Offset: 0x0015C504
		private IAsyncResult OnBeginCloseSharedState(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new SecurityChannelListener<TChannel>.CloseSharedStateAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06005EB5 RID: 24245 RVA: 0x0015E30F File Offset: 0x0015C50F
		private void OnEndCloseSharedState(IAsyncResult result)
		{
			SecurityChannelListener<TChannel>.CloseSharedStateAsyncResult.End(result);
		}

		// Token: 0x06005EB6 RID: 24246 RVA: 0x0015E317 File Offset: 0x0015C517
		internal IAsyncResult OnBeginOpenListenerState(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new SecurityChannelListener<TChannel>.OpenListenerStateAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06005EB7 RID: 24247 RVA: 0x0015E322 File Offset: 0x0015C522
		internal void OnEndOpenListenerState(IAsyncResult result)
		{
			SecurityChannelListener<TChannel>.OpenListenerStateAsyncResult.End(result);
		}

		// Token: 0x06005EB8 RID: 24248 RVA: 0x0015E32C File Offset: 0x0015C52C
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.ThrowIfInnerListenerNotSet();
			this.EnableChannelBindingSupport();
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginOpen), new ChainedEndHandler(base.OnEndOpen), new ChainedBeginHandler(this.OnBeginOpenListenerState), new ChainedEndHandler(this.OnEndOpenListenerState));
		}

		// Token: 0x06005EB9 RID: 24249 RVA: 0x0015E37D File Offset: 0x0015C57D
		protected override void OnEndOpen(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06005EBA RID: 24250 RVA: 0x0015E385 File Offset: 0x0015C585
		protected override void OnOpened()
		{
			base.OnOpened();
			this.ComputeEndpointIdentity();
		}

		// Token: 0x06005EBB RID: 24251 RVA: 0x0015E394 File Offset: 0x0015C594
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.sessionServerSettings != null)
			{
				this.sessionServerSettings.StopAcceptingNewWork();
			}
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.hasSecurityStateReference)
				{
					this.hasSecurityStateReference = false;
					this.settingsLifetimeManager.Close(timeoutHelper.RemainingTime());
				}
			}
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06005EBC RID: 24252 RVA: 0x0015E418 File Offset: 0x0015C618
		internal void InitializeListener(ChannelBuilder channelBuilder)
		{
			this.channelBuilder = channelBuilder;
			if (this.SessionMode)
			{
				this.sessionServerSettings.ChannelBuilder = this.ChannelBuilder;
				this.InnerChannelListener = this.sessionServerSettings.CreateInnerChannelListener();
				base.Acceptor = this.sessionServerSettings.CreateAcceptor<TChannel>();
				return;
			}
			this.InnerChannelListener = this.ChannelBuilder.BuildChannelListener<TChannel>();
			base.Acceptor = new SecurityChannelListener<TChannel>.SecurityChannelAcceptor(this, (IChannelListener<TChannel>)this.InnerChannelListener, this.securityProtocolFactory.CreateListenerSecurityState());
		}

		// Token: 0x06005EBD RID: 24253 RVA: 0x0015E49C File Offset: 0x0015C69C
		private void InitializeListenerSecurityState()
		{
			if (this.SessionMode)
			{
				this.SessionServerSettings.SessionProtocolFactory.ListenUri = this.Uri;
				this.SessionServerSettings.SecurityChannelListener = this;
			}
			else
			{
				this.ThrowIfProtocolFactoryNotSet();
				this.securityProtocolFactory.ListenUri = this.Uri;
			}
			this.settingsLifetimeManager = new SecurityListenerSettingsLifetimeManager(this.securityProtocolFactory, this.sessionServerSettings, this.sessionMode, this.InnerChannelListener);
			if (this.sessionServerSettings != null)
			{
				this.sessionServerSettings.SettingsLifetimeManager = this.settingsLifetimeManager;
			}
			this.hasSecurityStateReference = true;
		}

		// Token: 0x06005EBE RID: 24254 RVA: 0x0015E530 File Offset: 0x0015C730
		protected override void OnOpen(TimeSpan timeout)
		{
			base.ThrowIfInnerListenerNotSet();
			this.EnableChannelBindingSupport();
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (base.State == CommunicationState.Closing && base.State == CommunicationState.Closed)
				{
					return;
				}
				this.InitializeListenerSecurityState();
			}
			this.settingsLifetimeManager.Open(timeoutHelper.RemainingTime());
		}

		// Token: 0x06005EBF RID: 24255 RVA: 0x0015E5B8 File Offset: 0x0015C7B8
		private void EnableChannelBindingSupport()
		{
			ExtendedProtectionPolicy property = this.InnerChannelListener.GetProperty<ExtendedProtectionPolicy>();
			if (property != null)
			{
				if (property.CustomChannelBinding != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ExtendedProtectionPolicyCustomChannelBindingNotSupported")));
				}
				if (property.PolicyEnforcement == PolicyEnforcement.Never)
				{
					return;
				}
				IChannelBindingProvider property2 = this.InnerChannelListener.GetProperty<IChannelBindingProvider>();
				if (property.PolicyEnforcement == PolicyEnforcement.Always && (SecurityUtils.IsChannelBindingDisabled || !this.extendedProtectionPolicyHasSupport || (property2 == null && property.ProtectionScenario != ProtectionScenario.TrustedProxy)))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityChannelListenerChannelExtendedProtectionNotSupported")));
				}
				if (SecurityUtils.IsChannelBindingDisabled || !this.extendedProtectionPolicyHasSupport)
				{
					return;
				}
				if (property2 != null)
				{
					property2.EnableChannelBindingSupport();
				}
			}
			if (this.securityProtocolFactory != null)
			{
				this.securityProtocolFactory.ExtendedProtectionPolicy = property;
			}
		}

		// Token: 0x06005EC0 RID: 24256 RVA: 0x0015E679 File Offset: 0x0015C879
		private void ThrowIfProtocolFactoryNotSet()
		{
			if (this.securityProtocolFactory == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityProtocolFactoryShouldBeSetBeforeThisOperation")));
			}
		}

		// Token: 0x06005EC1 RID: 24257 RVA: 0x0015E6A0 File Offset: 0x0015C8A0
		protected override void OnFaulted()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.hasSecurityStateReference)
				{
					this.hasSecurityStateReference = false;
					if (this.settingsLifetimeManager != null)
					{
						this.settingsLifetimeManager.Abort();
					}
				}
			}
			base.OnFaulted();
		}

		// Token: 0x04003805 RID: 14341
		private ChannelBuilder channelBuilder;

		// Token: 0x04003806 RID: 14342
		private SecurityProtocolFactory securityProtocolFactory;

		// Token: 0x04003807 RID: 14343
		private SecuritySessionServerSettings sessionServerSettings;

		// Token: 0x04003808 RID: 14344
		private bool sessionMode;

		// Token: 0x04003809 RID: 14345
		private bool sendUnsecuredFaults = true;

		// Token: 0x0400380A RID: 14346
		private SecurityListenerSettingsLifetimeManager settingsLifetimeManager;

		// Token: 0x0400380B RID: 14347
		private bool hasSecurityStateReference;

		// Token: 0x0400380C RID: 14348
		private bool extendedProtectionPolicyHasSupport;

		// Token: 0x0400380D RID: 14349
		private ISecurityCapabilities securityCapabilities;

		// Token: 0x0400380E RID: 14350
		private EndpointIdentity identity;

		// Token: 0x02000E02 RID: 3586
		private class AggregateLogonTokenCacheManager : ILogonTokenCacheManager
		{
			// Token: 0x06008126 RID: 33062 RVA: 0x001DF2F3 File Offset: 0x001DD4F3
			public AggregateLogonTokenCacheManager(ReadOnlyCollection<ILogonTokenCacheManager> cacheManagers)
			{
				if (cacheManagers == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("cacheManagers");
				}
				this.cacheManagers = cacheManagers;
			}

			// Token: 0x06008127 RID: 33063 RVA: 0x001DF318 File Offset: 0x001DD518
			public bool RemoveCachedLogonToken(string username)
			{
				bool flag = false;
				if (!flag && this.cacheManagers != null)
				{
					for (int i = 0; i < this.cacheManagers.Count; i++)
					{
						flag = this.cacheManagers[i].RemoveCachedLogonToken(username);
						if (flag)
						{
							break;
						}
					}
				}
				return flag;
			}

			// Token: 0x06008128 RID: 33064 RVA: 0x001DF360 File Offset: 0x001DD560
			public void FlushLogonTokenCache()
			{
				if (this.cacheManagers != null)
				{
					for (int i = 0; i < this.cacheManagers.Count; i++)
					{
						this.cacheManagers[i].FlushLogonTokenCache();
					}
				}
			}

			// Token: 0x040049AD RID: 18861
			private ReadOnlyCollection<ILogonTokenCacheManager> cacheManagers;
		}

		// Token: 0x02000E03 RID: 3587
		internal sealed class SecurityChannelAcceptor : LayeredChannelAcceptor<TChannel, TChannel>
		{
			// Token: 0x06008129 RID: 33065 RVA: 0x001DF39C File Offset: 0x001DD59C
			public SecurityChannelAcceptor(ChannelManagerBase channelManager, IChannelListener<TChannel> innerListener, object listenerSecurityProtocolState) : base(channelManager, innerListener)
			{
				this.listenerSecurityProtocolState = listenerSecurityProtocolState;
			}

			// Token: 0x17001C8C RID: 7308
			// (get) Token: 0x0600812A RID: 33066 RVA: 0x001DF3AD File Offset: 0x001DD5AD
			private SecurityChannelListener<TChannel> SecurityChannelListener
			{
				get
				{
					return (SecurityChannelListener<TChannel>)base.ChannelManager;
				}
			}

			// Token: 0x0600812B RID: 33067 RVA: 0x001DF3BC File Offset: 0x001DD5BC
			protected override TChannel OnAcceptChannel(TChannel innerChannel)
			{
				SecurityChannelListener<TChannel> securityChannelListener = this.SecurityChannelListener;
				SecurityProtocol securityProtocol = securityChannelListener.SecurityProtocolFactory.CreateSecurityProtocol(null, null, this.listenerSecurityProtocolState, typeof(TChannel) == typeof(IReplyChannel) || typeof(TChannel) == typeof(IReplySessionChannel), TimeSpan.Zero);
				object obj;
				if (typeof(TChannel) == typeof(IInputChannel))
				{
					obj = new SecurityChannelListener<TChannel>.SecurityInputChannel(securityChannelListener, (IInputChannel)((object)innerChannel), securityProtocol, securityChannelListener.settingsLifetimeManager);
				}
				else if (typeof(TChannel) == typeof(IInputSessionChannel))
				{
					obj = new SecurityChannelListener<TChannel>.SecurityInputSessionChannel(securityChannelListener, (IInputSessionChannel)((object)innerChannel), securityProtocol, securityChannelListener.settingsLifetimeManager);
				}
				else if (securityChannelListener.SupportsDuplex && typeof(TChannel) == typeof(IDuplexChannel))
				{
					obj = new SecurityChannelListener<TChannel>.SecurityDuplexChannel(securityChannelListener, (IDuplexChannel)((object)innerChannel), securityProtocol, securityChannelListener.settingsLifetimeManager);
				}
				else if (securityChannelListener.SupportsDuplex && typeof(TChannel) == typeof(IDuplexSessionChannel))
				{
					obj = new SecurityChannelListener<TChannel>.SecurityDuplexSessionChannel(securityChannelListener, (IDuplexSessionChannel)((object)innerChannel), securityProtocol, securityChannelListener.settingsLifetimeManager);
				}
				else if (securityChannelListener.SupportsRequestReply && typeof(TChannel) == typeof(IReplyChannel))
				{
					obj = new SecurityChannelListener<TChannel>.SecurityReplyChannel(securityChannelListener, (IReplyChannel)((object)innerChannel), securityProtocol, securityChannelListener.settingsLifetimeManager);
				}
				else
				{
					if (!securityChannelListener.SupportsRequestReply || !(typeof(TChannel) == typeof(IReplySessionChannel)))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedChannelInterfaceType", new object[]
						{
							typeof(TChannel)
						})));
					}
					obj = new SecurityChannelListener<TChannel>.SecurityReplySessionChannel(securityChannelListener, (IReplySessionChannel)((object)innerChannel), securityProtocol, securityChannelListener.settingsLifetimeManager);
				}
				return (TChannel)((object)obj);
			}

			// Token: 0x040049AE RID: 18862
			private readonly object listenerSecurityProtocolState;
		}

		// Token: 0x02000E04 RID: 3588
		private class CloseSharedStateAsyncResult : AsyncResult
		{
			// Token: 0x0600812C RID: 33068 RVA: 0x001DF5C4 File Offset: 0x001DD7C4
			public CloseSharedStateAsyncResult(SecurityChannelListener<TChannel> securityListener, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.securityListener = securityListener;
				object thisLock = this.securityListener.ThisLock;
				lock (thisLock)
				{
					if (this.securityListener.hasSecurityStateReference)
					{
						this.securityListener.hasSecurityStateReference = false;
						IAsyncResult asyncResult = this.securityListener.settingsLifetimeManager.BeginClose(timeout, SecurityChannelListener<TChannel>.CloseSharedStateAsyncResult.lifetimeManagerCloseCallback, this);
						if (!asyncResult.CompletedSynchronously)
						{
							return;
						}
						this.securityListener.settingsLifetimeManager.EndClose(asyncResult);
					}
				}
				base.Complete(true);
			}

			// Token: 0x0600812D RID: 33069 RVA: 0x001DF668 File Offset: 0x001DD868
			private static void LifetimeManagerCloseCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				SecurityChannelListener<TChannel>.CloseSharedStateAsyncResult closeSharedStateAsyncResult = (SecurityChannelListener<TChannel>.CloseSharedStateAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					closeSharedStateAsyncResult.securityListener.settingsLifetimeManager.EndClose(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				closeSharedStateAsyncResult.Complete(false, exception);
			}

			// Token: 0x0600812E RID: 33070 RVA: 0x001DF6C8 File Offset: 0x001DD8C8
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<SecurityChannelListener<TChannel>.CloseSharedStateAsyncResult>(result);
			}

			// Token: 0x040049AF RID: 18863
			private static AsyncCallback lifetimeManagerCloseCallback = Fx.ThunkCallback(new AsyncCallback(SecurityChannelListener<TChannel>.CloseSharedStateAsyncResult.LifetimeManagerCloseCallback));

			// Token: 0x040049B0 RID: 18864
			private SecurityChannelListener<TChannel> securityListener;
		}

		// Token: 0x02000E05 RID: 3589
		private class OpenListenerStateAsyncResult : AsyncResult
		{
			// Token: 0x06008130 RID: 33072 RVA: 0x001DF6EC File Offset: 0x001DD8EC
			public OpenListenerStateAsyncResult(SecurityChannelListener<TChannel> securityListener, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.securityListener = securityListener;
				object thisLock = this.securityListener.ThisLock;
				bool flag2;
				lock (thisLock)
				{
					if (this.securityListener.State == CommunicationState.Closed || this.securityListener.State == CommunicationState.Closing)
					{
						flag2 = false;
					}
					else
					{
						flag2 = true;
						this.securityListener.InitializeListenerSecurityState();
					}
				}
				if (flag2)
				{
					IAsyncResult asyncResult = this.securityListener.settingsLifetimeManager.BeginOpen(timeout, SecurityChannelListener<TChannel>.OpenListenerStateAsyncResult.lifetimeManagerOpenCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.securityListener.settingsLifetimeManager.EndOpen(asyncResult);
				}
				base.Complete(true);
			}

			// Token: 0x06008131 RID: 33073 RVA: 0x001DF7A4 File Offset: 0x001DD9A4
			private static void LifetimeManagerOpenCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				SecurityChannelListener<TChannel>.OpenListenerStateAsyncResult openListenerStateAsyncResult = (SecurityChannelListener<TChannel>.OpenListenerStateAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					openListenerStateAsyncResult.securityListener.settingsLifetimeManager.EndOpen(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				openListenerStateAsyncResult.Complete(false, exception);
			}

			// Token: 0x06008132 RID: 33074 RVA: 0x001DF804 File Offset: 0x001DDA04
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<SecurityChannelListener<TChannel>.OpenListenerStateAsyncResult>(result);
			}

			// Token: 0x040049B1 RID: 18865
			private static AsyncCallback lifetimeManagerOpenCallback = Fx.ThunkCallback(new AsyncCallback(SecurityChannelListener<TChannel>.OpenListenerStateAsyncResult.LifetimeManagerOpenCallback));

			// Token: 0x040049B2 RID: 18866
			private SecurityChannelListener<TChannel> securityListener;
		}

		// Token: 0x02000E06 RID: 3590
		private abstract class ServerSecurityChannel<UChannel> : SecurityChannel<UChannel> where UChannel : class, IChannel
		{
			// Token: 0x06008134 RID: 33076 RVA: 0x001DF825 File Offset: 0x001DDA25
			protected ServerSecurityChannel(ChannelManagerBase channelManager, UChannel innerChannel, SecurityProtocol securityProtocol, SecurityListenerSettingsLifetimeManager settingsLifetimeManager) : base(channelManager, innerChannel, securityProtocol)
			{
				if (settingsLifetimeManager == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("settingsLifetimeManager");
				}
				this.settingsLifetimeManager = settingsLifetimeManager;
			}

			// Token: 0x06008135 RID: 33077 RVA: 0x001DF84C File Offset: 0x001DDA4C
			internal void InternalThrowIfFaulted()
			{
				base.ThrowIfFaulted();
			}

			// Token: 0x06008136 RID: 33078 RVA: 0x001DF854 File Offset: 0x001DDA54
			protected override void OnOpened()
			{
				base.OnOpened();
				this.secureConversationCloseAction = base.SecurityProtocol.SecurityProtocolFactory.StandardsManager.SecureConversationDriver.CloseAction.Value;
			}

			// Token: 0x06008137 RID: 33079 RVA: 0x001DF884 File Offset: 0x001DDA84
			protected override void OnOpen(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.SecurityProtocol.Open(timeoutHelper.RemainingTime());
				base.OnOpen(timeoutHelper.RemainingTime());
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (base.State != CommunicationState.Closed && base.State != CommunicationState.Closing)
					{
						this.hasSecurityStateReference = true;
						this.settingsLifetimeManager.AddReference();
					}
				}
			}

			// Token: 0x06008138 RID: 33080 RVA: 0x001DF90C File Offset: 0x001DDB0C
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.SecurityProtocol.Open(timeoutHelper.RemainingTime());
				return base.OnBeginOpen(timeoutHelper.RemainingTime(), callback, state);
			}

			// Token: 0x06008139 RID: 33081 RVA: 0x001DF944 File Offset: 0x001DDB44
			protected override void OnEndOpen(IAsyncResult result)
			{
				base.OnEndOpen(result);
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (base.State != CommunicationState.Closed && base.State != CommunicationState.Closing)
					{
						this.hasSecurityStateReference = true;
						this.settingsLifetimeManager.AddReference();
					}
				}
			}

			// Token: 0x0600813A RID: 33082 RVA: 0x001DF9AC File Offset: 0x001DDBAC
			protected override void OnAbort()
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.hasSecurityStateReference)
					{
						this.hasSecurityStateReference = false;
						this.settingsLifetimeManager.Abort();
					}
				}
				base.OnAbort();
			}

			// Token: 0x0600813B RID: 33083 RVA: 0x001DFA08 File Offset: 0x001DDC08
			protected override void OnFaulted()
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.hasSecurityStateReference)
					{
						this.hasSecurityStateReference = false;
						this.settingsLifetimeManager.Abort();
					}
				}
				base.OnFaulted();
			}

			// Token: 0x0600813C RID: 33084 RVA: 0x001DFA64 File Offset: 0x001DDC64
			protected override void OnClose(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.hasSecurityStateReference)
					{
						this.hasSecurityStateReference = false;
						this.settingsLifetimeManager.Close(timeoutHelper.RemainingTime());
					}
				}
				base.OnClose(timeoutHelper.RemainingTime());
			}

			// Token: 0x0600813D RID: 33085 RVA: 0x001DFAD4 File Offset: 0x001DDCD4
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(this.OnBeginCloseSharedState), new ChainedEndHandler(this.OnEndCloseSharedState), new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose));
			}

			// Token: 0x0600813E RID: 33086 RVA: 0x001DFB0E File Offset: 0x001DDD0E
			protected override void OnEndClose(IAsyncResult result)
			{
				ChainedAsyncResult.End(result);
			}

			// Token: 0x0600813F RID: 33087 RVA: 0x001DFB16 File Offset: 0x001DDD16
			private IAsyncResult OnBeginCloseSharedState(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel>.CloseSharedStateAsyncResult(this, timeout, callback, state);
			}

			// Token: 0x06008140 RID: 33088 RVA: 0x001DFB21 File Offset: 0x001DDD21
			private void OnEndCloseSharedState(IAsyncResult result)
			{
				SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel>.CloseSharedStateAsyncResult.End(result);
			}

			// Token: 0x06008141 RID: 33089 RVA: 0x001DFB2C File Offset: 0x001DDD2C
			private static MessageFault GetSecureConversationCloseNotSupportedFault()
			{
				if (SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel>.secureConversationCloseNotSupportedFault == null)
				{
					FaultCode code = FaultCode.CreateSenderFaultCode("SecureConversationCancellationNotAllowed", "http://schemas.microsoft.com/ws/2006/05/security");
					FaultReason reason = new FaultReason(SR.GetString("SecureConversationCancelNotAllowedFaultReason"), CultureInfo.InvariantCulture);
					SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel>.secureConversationCloseNotSupportedFault = MessageFault.CreateFault(code, reason);
				}
				return SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel>.secureConversationCloseNotSupportedFault;
			}

			// Token: 0x06008142 RID: 33090 RVA: 0x001DFB76 File Offset: 0x001DDD76
			private void ThrowIfSecureConversationCloseMessage(Message message)
			{
				if (message.Headers.Action == this.secureConversationCloseAction)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("SecureConversationCancelNotAllowedFaultReason"), null, SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel>.GetSecureConversationCloseNotSupportedFault()));
				}
			}

			// Token: 0x06008143 RID: 33091 RVA: 0x001DFBB0 File Offset: 0x001DDDB0
			[SecurityCritical]
			private IDisposable ApplyHostingIntegrationContext(Message message)
			{
				IDisposable result = null;
				IAspNetMessageProperty hostingProperty = AspNetEnvironment.Current.GetHostingProperty(message);
				if (hostingProperty != null)
				{
					result = hostingProperty.ApplyIntegrationContext();
				}
				return result;
			}

			// Token: 0x06008144 RID: 33092 RVA: 0x001DFBD8 File Offset: 0x001DDDD8
			[SecuritySafeCritical]
			internal SecurityProtocolCorrelationState VerifyIncomingMessage(ref Message message, TimeSpan timeout, params SecurityProtocolCorrelationState[] correlationState)
			{
				if (message == null)
				{
					return null;
				}
				this.ThrowIfSecureConversationCloseMessage(message);
				SecurityProtocolCorrelationState result;
				using (this.ApplyHostingIntegrationContext(message))
				{
					result = base.SecurityProtocol.VerifyIncomingMessage(ref message, timeout, correlationState);
				}
				return result;
			}

			// Token: 0x06008145 RID: 33093 RVA: 0x001DFC28 File Offset: 0x001DDE28
			[SecuritySafeCritical]
			internal void VerifyIncomingMessage(ref Message message, TimeSpan timeout)
			{
				if (message == null)
				{
					return;
				}
				this.ThrowIfSecureConversationCloseMessage(message);
				using (this.ApplyHostingIntegrationContext(message))
				{
					base.SecurityProtocol.VerifyIncomingMessage(ref message, timeout);
				}
			}

			// Token: 0x040049B3 RID: 18867
			private static MessageFault secureConversationCloseNotSupportedFault;

			// Token: 0x040049B4 RID: 18868
			private string secureConversationCloseAction;

			// Token: 0x040049B5 RID: 18869
			private SecurityListenerSettingsLifetimeManager settingsLifetimeManager;

			// Token: 0x040049B6 RID: 18870
			private bool hasSecurityStateReference;

			// Token: 0x02000F7C RID: 3964
			private class CloseSharedStateAsyncResult : AsyncResult
			{
				// Token: 0x060087F7 RID: 34807 RVA: 0x001F97E0 File Offset: 0x001F79E0
				public CloseSharedStateAsyncResult(SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel> securityChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.securityChannel = securityChannel;
					object thisLock = this.securityChannel.ThisLock;
					lock (thisLock)
					{
						if (this.securityChannel.hasSecurityStateReference)
						{
							this.securityChannel.hasSecurityStateReference = false;
							IAsyncResult asyncResult = this.securityChannel.settingsLifetimeManager.BeginClose(timeout, SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel>.CloseSharedStateAsyncResult.lifetimeManagerCloseCallback, this);
							if (!asyncResult.CompletedSynchronously)
							{
								return;
							}
							this.securityChannel.settingsLifetimeManager.EndClose(asyncResult);
						}
					}
					base.Complete(true);
				}

				// Token: 0x060087F8 RID: 34808 RVA: 0x001F9884 File Offset: 0x001F7A84
				private static void LifetimeManagerCloseCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel>.CloseSharedStateAsyncResult closeSharedStateAsyncResult = (SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel>.CloseSharedStateAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						closeSharedStateAsyncResult.securityChannel.settingsLifetimeManager.EndClose(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					closeSharedStateAsyncResult.Complete(false, exception);
				}

				// Token: 0x060087F9 RID: 34809 RVA: 0x001F98E4 File Offset: 0x001F7AE4
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel>.CloseSharedStateAsyncResult>(result);
				}

				// Token: 0x04004F55 RID: 20309
				private static AsyncCallback lifetimeManagerCloseCallback = Fx.ThunkCallback(new AsyncCallback(SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel>.CloseSharedStateAsyncResult.LifetimeManagerCloseCallback));

				// Token: 0x04004F56 RID: 20310
				private SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel> securityChannel;
			}
		}

		// Token: 0x02000E07 RID: 3591
		private class SecurityInputChannel : SecurityChannelListener<TChannel>.ServerSecurityChannel<IInputChannel>, IInputChannel, IChannel, ICommunicationObject
		{
			// Token: 0x06008146 RID: 33094 RVA: 0x001DFC74 File Offset: 0x001DDE74
			public SecurityInputChannel(ChannelManagerBase channelManager, IInputChannel innerChannel, SecurityProtocol securityProtocol, SecurityListenerSettingsLifetimeManager settingsLifetimeManager) : base(channelManager, innerChannel, securityProtocol, settingsLifetimeManager)
			{
			}

			// Token: 0x17001C8D RID: 7309
			// (get) Token: 0x06008147 RID: 33095 RVA: 0x001DFC81 File Offset: 0x001DDE81
			public EndpointAddress LocalAddress
			{
				get
				{
					return base.InnerChannel.LocalAddress;
				}
			}

			// Token: 0x06008148 RID: 33096 RVA: 0x001DFC8E File Offset: 0x001DDE8E
			public Message Receive()
			{
				return this.Receive(base.DefaultReceiveTimeout);
			}

			// Token: 0x06008149 RID: 33097 RVA: 0x001DFC9C File Offset: 0x001DDE9C
			public Message Receive(TimeSpan timeout)
			{
				return InputChannel.HelpReceive(this, timeout);
			}

			// Token: 0x0600814A RID: 33098 RVA: 0x001DFCA5 File Offset: 0x001DDEA5
			public IAsyncResult BeginReceive(AsyncCallback callback, object state)
			{
				return this.BeginReceive(base.DefaultReceiveTimeout, callback, state);
			}

			// Token: 0x0600814B RID: 33099 RVA: 0x001DFCB5 File Offset: 0x001DDEB5
			public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return InputChannel.HelpBeginReceive(this, timeout, callback, state);
			}

			// Token: 0x0600814C RID: 33100 RVA: 0x001DFCC0 File Offset: 0x001DDEC0
			public Message EndReceive(IAsyncResult result)
			{
				return InputChannel.HelpEndReceive(result);
			}

			// Token: 0x0600814D RID: 33101 RVA: 0x001DFCC8 File Offset: 0x001DDEC8
			public virtual IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				if (base.DoneReceivingInCurrentState())
				{
					return new DoneReceivingAsyncResult(callback, state);
				}
				return new SecurityChannelListener<TChannel>.InputChannelReceiveMessageAndVerifySecurityAsyncResult(this, base.InnerChannel, timeout, callback, state);
			}

			// Token: 0x0600814E RID: 33102 RVA: 0x001DFCEC File Offset: 0x001DDEEC
			public virtual bool EndTryReceive(IAsyncResult result, out Message message)
			{
				DoneReceivingAsyncResult doneReceivingAsyncResult = result as DoneReceivingAsyncResult;
				if (doneReceivingAsyncResult != null)
				{
					return DoneReceivingAsyncResult.End(doneReceivingAsyncResult, out message);
				}
				return SecurityChannelListener<TChannel>.InputChannelReceiveMessageAndVerifySecurityAsyncResult.End(result, out message);
			}

			// Token: 0x0600814F RID: 33103 RVA: 0x001DFD14 File Offset: 0x001DDF14
			public virtual bool TryReceive(TimeSpan timeout, out Message message)
			{
				if (base.DoneReceivingInCurrentState())
				{
					message = null;
					return true;
				}
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				while (base.State != CommunicationState.Closed && base.State != CommunicationState.Faulted)
				{
					if (!base.InnerChannel.TryReceive(timeoutHelper.RemainingTime(), out message))
					{
						return false;
					}
					try
					{
						base.VerifyIncomingMessage(ref message, timeoutHelper.RemainingTime());
					}
					catch (MessageSecurityException)
					{
						message = null;
						if (timeoutHelper.RemainingTime() == TimeSpan.Zero)
						{
							return false;
						}
						continue;
					}
					IL_71:
					base.ThrowIfFaulted();
					return true;
				}
				message = null;
				goto IL_71;
			}

			// Token: 0x06008150 RID: 33104 RVA: 0x001DFDAC File Offset: 0x001DDFAC
			public bool WaitForMessage(TimeSpan timeout)
			{
				return base.InnerChannel.WaitForMessage(timeout);
			}

			// Token: 0x06008151 RID: 33105 RVA: 0x001DFDBA File Offset: 0x001DDFBA
			public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return base.InnerChannel.BeginWaitForMessage(timeout, callback, state);
			}

			// Token: 0x06008152 RID: 33106 RVA: 0x001DFDCA File Offset: 0x001DDFCA
			public bool EndWaitForMessage(IAsyncResult result)
			{
				return base.InnerChannel.EndWaitForMessage(result);
			}
		}

		// Token: 0x02000E08 RID: 3592
		private sealed class SecurityInputSessionChannel : SecurityChannelListener<TChannel>.SecurityInputChannel, IInputSessionChannel, IInputChannel, IChannel, ICommunicationObject, ISessionChannel<IInputSession>
		{
			// Token: 0x06008153 RID: 33107 RVA: 0x001DFDD8 File Offset: 0x001DDFD8
			public SecurityInputSessionChannel(ChannelManagerBase channelManager, IInputSessionChannel innerChannel, SecurityProtocol securityProtocol, SecurityListenerSettingsLifetimeManager settingsLifetimeManager) : base(channelManager, innerChannel, securityProtocol, settingsLifetimeManager)
			{
			}

			// Token: 0x17001C8E RID: 7310
			// (get) Token: 0x06008154 RID: 33108 RVA: 0x001DFDE5 File Offset: 0x001DDFE5
			public IInputSession Session
			{
				get
				{
					return ((IInputSessionChannel)base.InnerChannel).Session;
				}
			}
		}

		// Token: 0x02000E09 RID: 3593
		private class SecurityDuplexChannel : SecurityChannelListener<TChannel>.SecurityInputChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel
		{
			// Token: 0x06008155 RID: 33109 RVA: 0x001DFDF7 File Offset: 0x001DDFF7
			public SecurityDuplexChannel(ChannelManagerBase channelManager, IDuplexChannel innerChannel, SecurityProtocol securityProtocol, SecurityListenerSettingsLifetimeManager settingsLifetimeManager) : base(channelManager, innerChannel, securityProtocol, settingsLifetimeManager)
			{
				this.innerDuplexChannel = innerChannel;
			}

			// Token: 0x17001C8F RID: 7311
			// (get) Token: 0x06008156 RID: 33110 RVA: 0x001DFE0B File Offset: 0x001DE00B
			public EndpointAddress RemoteAddress
			{
				get
				{
					return this.innerDuplexChannel.RemoteAddress;
				}
			}

			// Token: 0x17001C90 RID: 7312
			// (get) Token: 0x06008157 RID: 33111 RVA: 0x001DFE18 File Offset: 0x001DE018
			public Uri Via
			{
				get
				{
					return this.innerDuplexChannel.Via;
				}
			}

			// Token: 0x17001C91 RID: 7313
			// (get) Token: 0x06008158 RID: 33112 RVA: 0x001DFE25 File Offset: 0x001DE025
			protected IDuplexChannel InnerDuplexChannel
			{
				get
				{
					return this.innerDuplexChannel;
				}
			}

			// Token: 0x06008159 RID: 33113 RVA: 0x001DFE2D File Offset: 0x001DE02D
			public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
			{
				return this.BeginSend(message, base.DefaultSendTimeout, callback, state);
			}

			// Token: 0x0600815A RID: 33114 RVA: 0x001DFE3E File Offset: 0x001DE03E
			public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				base.ThrowIfFaulted();
				base.ThrowIfDisposedOrNotOpen(message);
				return new SecurityChannel<IInputChannel>.OutputChannelSendAsyncResult(message, base.SecurityProtocol, this.innerDuplexChannel, timeout, callback, state);
			}

			// Token: 0x0600815B RID: 33115 RVA: 0x001DFE63 File Offset: 0x001DE063
			public void EndSend(IAsyncResult result)
			{
				SecurityChannel<IInputChannel>.OutputChannelSendAsyncResult.End(result);
			}

			// Token: 0x0600815C RID: 33116 RVA: 0x001DFE6B File Offset: 0x001DE06B
			public void Send(Message message)
			{
				this.Send(message, base.DefaultSendTimeout);
			}

			// Token: 0x0600815D RID: 33117 RVA: 0x001DFE7C File Offset: 0x001DE07C
			public void Send(Message message, TimeSpan timeout)
			{
				base.ThrowIfFaulted();
				base.ThrowIfDisposedOrNotOpen(message);
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.SecurityProtocol.SecureOutgoingMessage(ref message, timeoutHelper.RemainingTime());
				this.innerDuplexChannel.Send(message, timeoutHelper.RemainingTime());
			}

			// Token: 0x040049B7 RID: 18871
			private readonly IDuplexChannel innerDuplexChannel;
		}

		// Token: 0x02000E0A RID: 3594
		private sealed class SecurityDuplexSessionChannel : SecurityChannelListener<TChannel>.SecurityDuplexChannel, IDuplexSessionChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel, ISessionChannel<IDuplexSession>
		{
			// Token: 0x0600815E RID: 33118 RVA: 0x001DFEC5 File Offset: 0x001DE0C5
			public SecurityDuplexSessionChannel(SecurityChannelListener<TChannel> channelManager, IDuplexSessionChannel innerChannel, SecurityProtocol securityProtocol, SecurityListenerSettingsLifetimeManager settingsLifetimeManager) : base(channelManager, innerChannel, securityProtocol, settingsLifetimeManager)
			{
				this.sendUnsecuredFaults = channelManager.SendUnsecuredFaults;
			}

			// Token: 0x17001C92 RID: 7314
			// (get) Token: 0x0600815F RID: 33119 RVA: 0x001DFEDE File Offset: 0x001DE0DE
			public IDuplexSession Session
			{
				get
				{
					return ((IDuplexSessionChannel)base.InnerChannel).Session;
				}
			}

			// Token: 0x17001C93 RID: 7315
			// (get) Token: 0x06008160 RID: 33120 RVA: 0x001DFEF0 File Offset: 0x001DE0F0
			public bool SendUnsecuredFaults
			{
				get
				{
					return this.sendUnsecuredFaults;
				}
			}

			// Token: 0x06008161 RID: 33121 RVA: 0x001DFEF8 File Offset: 0x001DE0F8
			private void SendFaultIfRequired(Exception e, Message unverifiedMessage, TimeSpan timeout)
			{
				if (!this.sendUnsecuredFaults)
				{
					return;
				}
				MessageFault messageFault = SecurityUtils.CreateSecurityMessageFault(e, base.SecurityProtocol.SecurityProtocolFactory.StandardsManager);
				if (messageFault == null)
				{
					return;
				}
				try
				{
					using (Message message = Message.CreateMessage(unverifiedMessage.Version, messageFault, unverifiedMessage.Version.Addressing.DefaultFaultAction))
					{
						if (unverifiedMessage.Headers.MessageId != null)
						{
							message.InitializeReply(unverifiedMessage);
						}
						((IDuplexChannel)base.InnerChannel).Send(message, timeout);
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
				}
			}

			// Token: 0x06008162 RID: 33122 RVA: 0x001DFFAC File Offset: 0x001DE1AC
			public override bool TryReceive(TimeSpan timeout, out Message message)
			{
				if (base.DoneReceivingInCurrentState())
				{
					message = null;
					return true;
				}
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				while (base.State != CommunicationState.Closed && base.State != CommunicationState.Faulted)
				{
					if (!base.InnerChannel.TryReceive(timeoutHelper.RemainingTime(), out message))
					{
						return false;
					}
					Message unverifiedMessage = message;
					Exception ex = null;
					try
					{
						base.VerifyIncomingMessage(ref message, timeoutHelper.RemainingTime());
						goto IL_87;
					}
					catch (MessageSecurityException ex2)
					{
						message = null;
						ex = ex2;
					}
					if (ex == null)
					{
						continue;
					}
					this.SendFaultIfRequired(ex, unverifiedMessage, timeoutHelper.RemainingTime());
					if (timeoutHelper.RemainingTime() == TimeSpan.Zero)
					{
						return false;
					}
					continue;
					IL_87:
					base.ThrowIfFaulted();
					return true;
				}
				message = null;
				goto IL_87;
			}

			// Token: 0x06008163 RID: 33123 RVA: 0x001E0058 File Offset: 0x001DE258
			public override IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				if (base.DoneReceivingInCurrentState())
				{
					return new DoneReceivingAsyncResult(callback, state);
				}
				return new SecurityChannelListener<TChannel>.DuplexSessionReceiveMessageAndVerifySecurityAsyncResult(this, base.InnerDuplexChannel, timeout, callback, state);
			}

			// Token: 0x06008164 RID: 33124 RVA: 0x001E007C File Offset: 0x001DE27C
			public override bool EndTryReceive(IAsyncResult result, out Message message)
			{
				DoneReceivingAsyncResult doneReceivingAsyncResult = result as DoneReceivingAsyncResult;
				if (doneReceivingAsyncResult != null)
				{
					return DoneReceivingAsyncResult.End(doneReceivingAsyncResult, out message);
				}
				return SecurityChannelListener<TChannel>.DuplexSessionReceiveMessageAndVerifySecurityAsyncResult.End(result, out message);
			}

			// Token: 0x040049B8 RID: 18872
			private bool sendUnsecuredFaults;
		}

		// Token: 0x02000E0B RID: 3595
		private class SecurityReplyChannel : SecurityChannelListener<TChannel>.ServerSecurityChannel<IReplyChannel>, IReplyChannel, IChannel, ICommunicationObject
		{
			// Token: 0x06008165 RID: 33125 RVA: 0x001E00A2 File Offset: 0x001DE2A2
			public SecurityReplyChannel(SecurityChannelListener<TChannel> channelManager, IReplyChannel innerChannel, SecurityProtocol securityProtocol, SecurityListenerSettingsLifetimeManager settingsLifetimeManager) : base(channelManager, innerChannel, securityProtocol, settingsLifetimeManager)
			{
				this.sendUnsecuredFaults = channelManager.SendUnsecuredFaults;
			}

			// Token: 0x17001C94 RID: 7316
			// (get) Token: 0x06008166 RID: 33126 RVA: 0x001E00BB File Offset: 0x001DE2BB
			public EndpointAddress LocalAddress
			{
				get
				{
					return base.InnerChannel.LocalAddress;
				}
			}

			// Token: 0x17001C95 RID: 7317
			// (get) Token: 0x06008167 RID: 33127 RVA: 0x001E00C8 File Offset: 0x001DE2C8
			public bool SendUnsecuredFaults
			{
				get
				{
					return this.sendUnsecuredFaults;
				}
			}

			// Token: 0x06008168 RID: 33128 RVA: 0x001E00D0 File Offset: 0x001DE2D0
			public RequestContext ReceiveRequest()
			{
				return this.ReceiveRequest(base.DefaultReceiveTimeout);
			}

			// Token: 0x06008169 RID: 33129 RVA: 0x001E00DE File Offset: 0x001DE2DE
			public RequestContext ReceiveRequest(TimeSpan timeout)
			{
				return ReplyChannel.HelpReceiveRequest(this, timeout);
			}

			// Token: 0x0600816A RID: 33130 RVA: 0x001E00E7 File Offset: 0x001DE2E7
			public IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state)
			{
				return this.BeginReceiveRequest(base.DefaultReceiveTimeout, callback, state);
			}

			// Token: 0x0600816B RID: 33131 RVA: 0x001E00F7 File Offset: 0x001DE2F7
			public IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return ReplyChannel.HelpBeginReceiveRequest(this, timeout, callback, state);
			}

			// Token: 0x0600816C RID: 33132 RVA: 0x001E0102 File Offset: 0x001DE302
			public RequestContext EndReceiveRequest(IAsyncResult result)
			{
				return ReplyChannel.HelpEndReceiveRequest(result);
			}

			// Token: 0x0600816D RID: 33133 RVA: 0x001E010A File Offset: 0x001DE30A
			public IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
			{
				if (base.DoneReceivingInCurrentState())
				{
					return new DoneReceivingAsyncResult(callback, state);
				}
				return new SecurityChannelListener<TChannel>.ReceiveRequestAndVerifySecurityAsyncResult(this, base.InnerChannel, timeout, callback, state);
			}

			// Token: 0x0600816E RID: 33134 RVA: 0x001E012C File Offset: 0x001DE32C
			public bool EndTryReceiveRequest(IAsyncResult result, out RequestContext requestContext)
			{
				DoneReceivingAsyncResult doneReceivingAsyncResult = result as DoneReceivingAsyncResult;
				if (doneReceivingAsyncResult != null)
				{
					return DoneReceivingAsyncResult.End(doneReceivingAsyncResult, out requestContext);
				}
				return SecurityChannelListener<TChannel>.ReceiveRequestAndVerifySecurityAsyncResult.End(result, out requestContext);
			}

			// Token: 0x0600816F RID: 33135 RVA: 0x001E0154 File Offset: 0x001DE354
			internal RequestContext ProcessReceivedRequest(RequestContext requestContext, TimeSpan timeout)
			{
				if (requestContext == null)
				{
					return null;
				}
				Message requestMessage = requestContext.RequestMessage;
				if (requestMessage == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("ReceivedMessageInRequestContextNull", new object[]
					{
						base.InnerChannel
					})));
				}
				SecurityProtocolCorrelationState correlationState = base.VerifyIncomingMessage(ref requestMessage, timeout, null);
				return new SecurityChannelListener<TChannel>.SecurityRequestContext(requestMessage, requestContext, base.SecurityProtocol, correlationState, base.DefaultSendTimeout, this.DefaultCloseTimeout);
			}

			// Token: 0x06008170 RID: 33136 RVA: 0x001E01C0 File Offset: 0x001DE3C0
			private void SendFaultIfRequired(Exception e, RequestContext innerContext, TimeSpan timeout)
			{
				if (!this.sendUnsecuredFaults)
				{
					return;
				}
				MessageFault messageFault = SecurityUtils.CreateSecurityMessageFault(e, base.SecurityProtocol.SecurityProtocolFactory.StandardsManager);
				if (messageFault == null)
				{
					return;
				}
				Message requestMessage = innerContext.RequestMessage;
				Message message = Message.CreateMessage(requestMessage.Version, messageFault, requestMessage.Version.Addressing.DefaultFaultAction);
				if (requestMessage.Headers.MessageId != null)
				{
					message.InitializeReply(requestMessage);
				}
				try
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					innerContext.Reply(message, timeoutHelper.RemainingTime());
					innerContext.Close(timeoutHelper.RemainingTime());
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
				}
				finally
				{
					message.Close();
					innerContext.Abort();
				}
			}

			// Token: 0x06008171 RID: 33137 RVA: 0x001E0290 File Offset: 0x001DE490
			public bool TryReceiveRequest(TimeSpan timeout, out RequestContext requestContext)
			{
				if (base.DoneReceivingInCurrentState())
				{
					requestContext = null;
					return true;
				}
				requestContext = null;
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				while (base.State != CommunicationState.Closed && base.State != CommunicationState.Faulted)
				{
					RequestContext requestContext2;
					if (!base.InnerChannel.TryReceiveRequest(timeoutHelper.RemainingTime(), out requestContext2))
					{
						requestContext = null;
						return false;
					}
					Exception ex = null;
					try
					{
						requestContext = this.ProcessReceivedRequest(requestContext2, timeoutHelper.RemainingTime());
						goto IL_8A;
					}
					catch (MessageSecurityException ex2)
					{
						ex = ex2;
					}
					if (ex == null)
					{
						continue;
					}
					this.SendFaultIfRequired(ex, requestContext2, timeoutHelper.RemainingTime());
					if (timeoutHelper.RemainingTime() == TimeSpan.Zero)
					{
						return false;
					}
					continue;
					IL_8A:
					base.ThrowIfFaulted();
					return true;
				}
				requestContext = null;
				goto IL_8A;
			}

			// Token: 0x06008172 RID: 33138 RVA: 0x001E0340 File Offset: 0x001DE540
			public bool WaitForRequest(TimeSpan timeout)
			{
				return base.InnerChannel.WaitForRequest(timeout);
			}

			// Token: 0x06008173 RID: 33139 RVA: 0x001E034E File Offset: 0x001DE54E
			public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return base.InnerChannel.BeginWaitForRequest(timeout, callback, state);
			}

			// Token: 0x06008174 RID: 33140 RVA: 0x001E035E File Offset: 0x001DE55E
			public bool EndWaitForRequest(IAsyncResult result)
			{
				return base.InnerChannel.EndWaitForRequest(result);
			}

			// Token: 0x040049B9 RID: 18873
			private bool sendUnsecuredFaults;
		}

		// Token: 0x02000E0C RID: 3596
		private sealed class SecurityReplySessionChannel : SecurityChannelListener<TChannel>.SecurityReplyChannel, IReplySessionChannel, IReplyChannel, IChannel, ICommunicationObject, ISessionChannel<IInputSession>
		{
			// Token: 0x06008175 RID: 33141 RVA: 0x001E036C File Offset: 0x001DE56C
			public SecurityReplySessionChannel(SecurityChannelListener<TChannel> channelManager, IReplySessionChannel innerChannel, SecurityProtocol securityProtocol, SecurityListenerSettingsLifetimeManager settingsLifetimeManager) : base(channelManager, innerChannel, securityProtocol, settingsLifetimeManager)
			{
			}

			// Token: 0x17001C96 RID: 7318
			// (get) Token: 0x06008176 RID: 33142 RVA: 0x001E0379 File Offset: 0x001DE579
			public IInputSession Session
			{
				get
				{
					return ((IReplySessionChannel)base.InnerChannel).Session;
				}
			}
		}

		// Token: 0x02000E0D RID: 3597
		private sealed class SecurityRequestContext : RequestContextBase
		{
			// Token: 0x06008177 RID: 33143 RVA: 0x001E038B File Offset: 0x001DE58B
			public SecurityRequestContext(Message requestMessage, RequestContext innerContext, SecurityProtocol securityProtocol, SecurityProtocolCorrelationState correlationState, TimeSpan defaultSendTimeout, TimeSpan defaultCloseTimeout) : base(requestMessage, defaultCloseTimeout, defaultSendTimeout)
			{
				this.innerContext = innerContext;
				this.securityProtocol = securityProtocol;
				this.correlationState = correlationState;
			}

			// Token: 0x06008178 RID: 33144 RVA: 0x001E03AE File Offset: 0x001DE5AE
			protected override void OnAbort()
			{
				this.innerContext.Abort();
			}

			// Token: 0x06008179 RID: 33145 RVA: 0x001E03BB File Offset: 0x001DE5BB
			protected override void OnClose(TimeSpan timeout)
			{
				this.innerContext.Close(timeout);
			}

			// Token: 0x0600817A RID: 33146 RVA: 0x001E03C9 File Offset: 0x001DE5C9
			protected override IAsyncResult OnBeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				if (message != null)
				{
					return new SecurityChannelListener<TChannel>.SecurityRequestContext.RequestContextSendAsyncResult(message, this.securityProtocol, this.innerContext, timeout, callback, state, this.correlationState);
				}
				return this.innerContext.BeginReply(message, timeout, callback, state);
			}

			// Token: 0x0600817B RID: 33147 RVA: 0x001E03FB File Offset: 0x001DE5FB
			protected override void OnEndReply(IAsyncResult result)
			{
				if (result is SecurityChannelListener<TChannel>.SecurityRequestContext.RequestContextSendAsyncResult)
				{
					SecurityChannelListener<TChannel>.SecurityRequestContext.RequestContextSendAsyncResult.End(result);
					return;
				}
				this.innerContext.EndReply(result);
			}

			// Token: 0x0600817C RID: 33148 RVA: 0x001E0418 File Offset: 0x001DE618
			protected override void OnReply(Message message, TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (message != null)
				{
					this.securityProtocol.SecureOutgoingMessage(ref message, timeoutHelper.RemainingTime(), this.correlationState);
				}
				this.innerContext.Reply(message, timeoutHelper.RemainingTime());
			}

			// Token: 0x040049BA RID: 18874
			private readonly RequestContext innerContext;

			// Token: 0x040049BB RID: 18875
			private readonly SecurityProtocol securityProtocol;

			// Token: 0x040049BC RID: 18876
			private readonly SecurityProtocolCorrelationState correlationState;

			// Token: 0x02000F7D RID: 3965
			private sealed class RequestContextSendAsyncResult : ApplySecurityAndSendAsyncResult<RequestContext>
			{
				// Token: 0x060087FB RID: 34811 RVA: 0x001F9905 File Offset: 0x001F7B05
				public RequestContextSendAsyncResult(Message message, SecurityProtocol protocol, RequestContext context, TimeSpan timeout, AsyncCallback callback, object state, SecurityProtocolCorrelationState correlationState) : base(protocol, context, timeout, callback, state)
				{
					base.Begin(message, correlationState);
				}

				// Token: 0x060087FC RID: 34812 RVA: 0x001F991E File Offset: 0x001F7B1E
				protected override IAsyncResult BeginSendCore(RequestContext context, Message message, TimeSpan timeout, AsyncCallback callback, object state)
				{
					return context.BeginReply(message, timeout, callback, state);
				}

				// Token: 0x060087FD RID: 34813 RVA: 0x001F992C File Offset: 0x001F7B2C
				internal static void End(IAsyncResult result)
				{
					SecurityChannelListener<TChannel>.SecurityRequestContext.RequestContextSendAsyncResult self = result as SecurityChannelListener<TChannel>.SecurityRequestContext.RequestContextSendAsyncResult;
					ApplySecurityAndSendAsyncResult<RequestContext>.OnEnd(self);
				}

				// Token: 0x060087FE RID: 34814 RVA: 0x001F9946 File Offset: 0x001F7B46
				protected override void EndSendCore(RequestContext context, IAsyncResult result)
				{
					context.EndReply(result);
				}

				// Token: 0x060087FF RID: 34815 RVA: 0x001F994F File Offset: 0x001F7B4F
				protected override void OnSendCompleteCore(TimeSpan timeout)
				{
				}
			}
		}

		// Token: 0x02000E0E RID: 3598
		private abstract class ReceiveItemAndVerifySecurityAsyncResult<TItem, UChannel> : AsyncResult where TItem : class where UChannel : class, IChannel
		{
			// Token: 0x0600817D RID: 33149 RVA: 0x001E045E File Offset: 0x001DE65E
			public ReceiveItemAndVerifySecurityAsyncResult(SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel> channel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.channel = channel;
			}

			// Token: 0x0600817E RID: 33150 RVA: 0x001E047C File Offset: 0x001DE67C
			protected void Start()
			{
				bool flag = false;
				Exception ex = null;
				try
				{
					flag = this.StartInnerReceive();
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (flag || ex != null)
				{
					base.Complete(false, ex);
				}
			}

			// Token: 0x17001C97 RID: 7319
			// (get) Token: 0x0600817F RID: 33151 RVA: 0x001E04C4 File Offset: 0x001DE6C4
			protected TItem Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x17001C98 RID: 7320
			// (get) Token: 0x06008180 RID: 33152 RVA: 0x001E04CC File Offset: 0x001DE6CC
			protected bool ReceiveCompleted
			{
				get
				{
					return this.receiveCompleted;
				}
			}

			// Token: 0x17001C99 RID: 7321
			// (get) Token: 0x06008181 RID: 33153
			protected abstract bool CanSendFault { get; }

			// Token: 0x17001C9A RID: 7322
			// (get) Token: 0x06008182 RID: 33154
			protected abstract SecurityStandardsManager StandardsManager { get; }

			// Token: 0x06008183 RID: 33155
			protected abstract IAsyncResult BeginTryReceiveItem(TimeSpan timeout, AsyncCallback callback, object state);

			// Token: 0x06008184 RID: 33156
			protected abstract bool EndTryReceiveItem(IAsyncResult result, out TItem innerItem);

			// Token: 0x06008185 RID: 33157
			protected abstract TItem ProcessInnerItem(TItem innerItem, TimeSpan timeout);

			// Token: 0x06008186 RID: 33158
			protected abstract Message CreateFaultMessage(MessageFault fault, TItem innerItem);

			// Token: 0x06008187 RID: 33159
			protected abstract IAsyncResult BeginSendFault(TItem innerItem, Message faultMessage, TimeSpan timeout, AsyncCallback callback, object state);

			// Token: 0x06008188 RID: 33160
			protected abstract void EndSendFault(TItem innerItem, IAsyncResult result);

			// Token: 0x06008189 RID: 33161
			protected abstract void CloseInnerItem(TItem innerItem, TimeSpan timeout);

			// Token: 0x0600818A RID: 33162
			protected abstract void AbortInnerItem(TItem innerItem);

			// Token: 0x0600818B RID: 33163 RVA: 0x001E04D4 File Offset: 0x001DE6D4
			private bool StartInnerReceive()
			{
				bool result;
				try
				{
					this.channel.InternalThrowIfFaulted();
					if (this.channel.State == CommunicationState.Closed)
					{
						this.item = default(TItem);
						this.receiveCompleted = true;
						result = true;
					}
					else
					{
						IAsyncResult asyncResult = this.BeginTryReceiveItem(this.timeoutHelper.RemainingTime(), SecurityChannelListener<TChannel>.ReceiveItemAndVerifySecurityAsyncResult<TItem, UChannel>.innerTryReceiveCompletedCallback, this);
						if (!asyncResult.CompletedSynchronously)
						{
							result = false;
						}
						else if (!this.EndTryReceiveItem(asyncResult, out this.innerItem))
						{
							this.receiveCompleted = false;
							result = true;
						}
						else
						{
							result = this.OnInnerReceiveDone();
						}
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					base.Complete(false, exception);
					result = false;
				}
				return result;
			}

			// Token: 0x0600818C RID: 33164 RVA: 0x001E0584 File Offset: 0x001DE784
			private static void InnerTryReceiveCompletedCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				SecurityChannelListener<TChannel>.ReceiveItemAndVerifySecurityAsyncResult<TItem, UChannel> receiveItemAndVerifySecurityAsyncResult = (SecurityChannelListener<TChannel>.ReceiveItemAndVerifySecurityAsyncResult<TItem, UChannel>)result.AsyncState;
				bool flag = false;
				Exception exception = null;
				try
				{
					if (!receiveItemAndVerifySecurityAsyncResult.EndTryReceiveItem(result, out receiveItemAndVerifySecurityAsyncResult.innerItem))
					{
						receiveItemAndVerifySecurityAsyncResult.receiveCompleted = false;
						flag = true;
					}
					else
					{
						flag = receiveItemAndVerifySecurityAsyncResult.OnInnerReceiveDone();
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
					receiveItemAndVerifySecurityAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x0600818D RID: 33165 RVA: 0x001E0600 File Offset: 0x001DE800
			private bool OnInnerReceiveDone()
			{
				this.channel.InternalThrowIfFaulted();
				Exception ex = null;
				try
				{
					this.item = this.ProcessInnerItem(this.innerItem, this.timeoutHelper.RemainingTime());
					this.receiveCompleted = true;
				}
				catch (MessageSecurityException ex2)
				{
					ex = ex2;
				}
				return ex == null || ((!this.CanSendFault || this.OnSecurityException(ex)) && this.OnFaultSent());
			}

			// Token: 0x0600818E RID: 33166 RVA: 0x001E0678 File Offset: 0x001DE878
			private bool OnFaultSent()
			{
				this.innerItem = default(TItem);
				if (this.timeoutHelper.RemainingTime() == TimeSpan.Zero)
				{
					this.receiveCompleted = false;
					return true;
				}
				return this.StartInnerReceive();
			}

			// Token: 0x0600818F RID: 33167 RVA: 0x001E06AC File Offset: 0x001DE8AC
			private bool OnSecurityException(Exception e)
			{
				MessageFault messageFault = SecurityUtils.CreateSecurityMessageFault(e, this.StandardsManager);
				if (messageFault == null)
				{
					return true;
				}
				this.faultMessage = this.CreateFaultMessage(messageFault, this.innerItem);
				return this.SendFault(this.faultMessage, e);
			}

			// Token: 0x06008190 RID: 33168 RVA: 0x001E06EC File Offset: 0x001DE8EC
			private bool SendFault(Message faultMessage, Exception e)
			{
				bool flag = false;
				try
				{
					IAsyncResult asyncResult = this.BeginSendFault(this.innerItem, faultMessage, this.timeoutHelper.RemainingTime(), Fx.ThunkCallback(new AsyncCallback(this.SendFaultCallback)), e);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					flag = true;
					this.EndSendFault(this.innerItem, asyncResult);
					this.CloseInnerItem(this.innerItem, this.timeoutHelper.RemainingTime());
				}
				catch (Exception exception)
				{
					if (faultMessage != null)
					{
						faultMessage.Close();
					}
					if (Fx.IsFatal(exception))
					{
						throw;
					}
				}
				finally
				{
					if (flag)
					{
						this.AbortInnerItem(this.innerItem);
						if (faultMessage != null)
						{
							faultMessage.Close();
						}
					}
				}
				return true;
			}

			// Token: 0x06008191 RID: 33169 RVA: 0x001E07A8 File Offset: 0x001DE9A8
			private void SendFaultCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception ex = (Exception)result.AsyncState;
				try
				{
					this.EndSendFault(this.innerItem, result);
					this.CloseInnerItem(this.innerItem, this.timeoutHelper.RemainingTime());
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
				}
				finally
				{
					if (this.faultMessage != null)
					{
						this.faultMessage.Close();
					}
					this.AbortInnerItem(this.innerItem);
				}
				bool flag = false;
				Exception exception2 = null;
				try
				{
					flag = this.OnFaultSent();
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					flag = true;
					exception2 = ex2;
				}
				if (flag)
				{
					base.Complete(false, exception2);
				}
			}

			// Token: 0x040049BD RID: 18877
			private static AsyncCallback innerTryReceiveCompletedCallback = Fx.ThunkCallback(new AsyncCallback(SecurityChannelListener<TChannel>.ReceiveItemAndVerifySecurityAsyncResult<TItem, UChannel>.InnerTryReceiveCompletedCallback));

			// Token: 0x040049BE RID: 18878
			protected bool receiveCompleted;

			// Token: 0x040049BF RID: 18879
			protected TimeoutHelper timeoutHelper;

			// Token: 0x040049C0 RID: 18880
			private TItem innerItem;

			// Token: 0x040049C1 RID: 18881
			private TItem item;

			// Token: 0x040049C2 RID: 18882
			private SecurityChannelListener<TChannel>.ServerSecurityChannel<UChannel> channel;

			// Token: 0x040049C3 RID: 18883
			private Message faultMessage;
		}

		// Token: 0x02000E0F RID: 3599
		private sealed class ReceiveRequestAndVerifySecurityAsyncResult : SecurityChannelListener<TChannel>.ReceiveItemAndVerifySecurityAsyncResult<RequestContext, IReplyChannel>
		{
			// Token: 0x06008193 RID: 33171 RVA: 0x001E088C File Offset: 0x001DEA8C
			public ReceiveRequestAndVerifySecurityAsyncResult(SecurityChannelListener<TChannel>.SecurityReplyChannel channel, IReplyChannel innerChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(channel, timeout, callback, state)
			{
				this.channel = channel;
				this.innerChannel = innerChannel;
				ActionItem.Schedule(new Action<object>(SecurityChannelListener<TChannel>.ReceiveRequestAndVerifySecurityAsyncResult.ReceiveMessage), this);
			}

			// Token: 0x17001C9B RID: 7323
			// (get) Token: 0x06008194 RID: 33172 RVA: 0x001E08BA File Offset: 0x001DEABA
			protected override bool CanSendFault
			{
				get
				{
					return this.channel.SendUnsecuredFaults;
				}
			}

			// Token: 0x17001C9C RID: 7324
			// (get) Token: 0x06008195 RID: 33173 RVA: 0x001E08C7 File Offset: 0x001DEAC7
			protected override SecurityStandardsManager StandardsManager
			{
				get
				{
					return this.channel.SecurityProtocol.SecurityProtocolFactory.StandardsManager;
				}
			}

			// Token: 0x06008196 RID: 33174 RVA: 0x001E08E0 File Offset: 0x001DEAE0
			private static void ReceiveMessage(object state)
			{
				SecurityChannelListener<TChannel>.ReceiveRequestAndVerifySecurityAsyncResult receiveRequestAndVerifySecurityAsyncResult = state as SecurityChannelListener<TChannel>.ReceiveRequestAndVerifySecurityAsyncResult;
				if (receiveRequestAndVerifySecurityAsyncResult == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException());
				}
				receiveRequestAndVerifySecurityAsyncResult.Start();
			}

			// Token: 0x06008197 RID: 33175 RVA: 0x001E090D File Offset: 0x001DEB0D
			protected override void AbortInnerItem(RequestContext innerItem)
			{
				innerItem.Abort();
			}

			// Token: 0x06008198 RID: 33176 RVA: 0x001E0915 File Offset: 0x001DEB15
			protected override void CloseInnerItem(RequestContext innerItem, TimeSpan timeout)
			{
				innerItem.Close(timeout);
			}

			// Token: 0x06008199 RID: 33177 RVA: 0x001E091E File Offset: 0x001DEB1E
			protected override IAsyncResult BeginTryReceiveItem(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerChannel.BeginTryReceiveRequest(timeout, callback, state);
			}

			// Token: 0x0600819A RID: 33178 RVA: 0x001E092E File Offset: 0x001DEB2E
			protected override bool EndTryReceiveItem(IAsyncResult result, out RequestContext innerItem)
			{
				return this.innerChannel.EndTryReceiveRequest(result, out innerItem);
			}

			// Token: 0x0600819B RID: 33179 RVA: 0x001E093D File Offset: 0x001DEB3D
			protected override RequestContext ProcessInnerItem(RequestContext innerItem, TimeSpan timeout)
			{
				return this.channel.ProcessReceivedRequest(innerItem, timeout);
			}

			// Token: 0x0600819C RID: 33180 RVA: 0x001E094C File Offset: 0x001DEB4C
			protected override Message CreateFaultMessage(MessageFault fault, RequestContext innerItem)
			{
				Message requestMessage = innerItem.RequestMessage;
				Message message = Message.CreateMessage(requestMessage.Version, fault, requestMessage.Version.Addressing.DefaultFaultAction);
				if (requestMessage.Headers.MessageId != null)
				{
					message.InitializeReply(requestMessage);
				}
				return message;
			}

			// Token: 0x0600819D RID: 33181 RVA: 0x001E0998 File Offset: 0x001DEB98
			protected override IAsyncResult BeginSendFault(RequestContext innerItem, Message faultMessage, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return innerItem.BeginReply(faultMessage, timeout, callback, state);
			}

			// Token: 0x0600819E RID: 33182 RVA: 0x001E09A6 File Offset: 0x001DEBA6
			protected override void EndSendFault(RequestContext innerItem, IAsyncResult result)
			{
				innerItem.EndReply(result);
			}

			// Token: 0x0600819F RID: 33183 RVA: 0x001E09B0 File Offset: 0x001DEBB0
			public static bool End(IAsyncResult result, out RequestContext requestContext)
			{
				SecurityChannelListener<TChannel>.ReceiveRequestAndVerifySecurityAsyncResult receiveRequestAndVerifySecurityAsyncResult = AsyncResult.End<SecurityChannelListener<TChannel>.ReceiveRequestAndVerifySecurityAsyncResult>(result);
				requestContext = receiveRequestAndVerifySecurityAsyncResult.Item;
				return receiveRequestAndVerifySecurityAsyncResult.ReceiveCompleted;
			}

			// Token: 0x040049C4 RID: 18884
			private SecurityChannelListener<TChannel>.SecurityReplyChannel channel;

			// Token: 0x040049C5 RID: 18885
			private IReplyChannel innerChannel;
		}

		// Token: 0x02000E10 RID: 3600
		private sealed class DuplexSessionReceiveMessageAndVerifySecurityAsyncResult : SecurityChannelListener<TChannel>.ReceiveItemAndVerifySecurityAsyncResult<Message, IInputChannel>
		{
			// Token: 0x060081A0 RID: 33184 RVA: 0x001E09D2 File Offset: 0x001DEBD2
			public DuplexSessionReceiveMessageAndVerifySecurityAsyncResult(SecurityChannelListener<TChannel>.SecurityDuplexSessionChannel channel, IDuplexChannel innerChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(channel, timeout, callback, state)
			{
				this.innerChannel = innerChannel;
				this.channel = channel;
				ActionItem.Schedule(new Action<object>(SecurityChannelListener<TChannel>.DuplexSessionReceiveMessageAndVerifySecurityAsyncResult.ReceiveMessage), this);
			}

			// Token: 0x17001C9D RID: 7325
			// (get) Token: 0x060081A1 RID: 33185 RVA: 0x001E0A00 File Offset: 0x001DEC00
			protected override bool CanSendFault
			{
				get
				{
					return this.channel.SendUnsecuredFaults;
				}
			}

			// Token: 0x17001C9E RID: 7326
			// (get) Token: 0x060081A2 RID: 33186 RVA: 0x001E0A0D File Offset: 0x001DEC0D
			protected override SecurityStandardsManager StandardsManager
			{
				get
				{
					return this.channel.SecurityProtocol.SecurityProtocolFactory.StandardsManager;
				}
			}

			// Token: 0x060081A3 RID: 33187 RVA: 0x001E0A24 File Offset: 0x001DEC24
			private static void ReceiveMessage(object state)
			{
				SecurityChannelListener<TChannel>.DuplexSessionReceiveMessageAndVerifySecurityAsyncResult duplexSessionReceiveMessageAndVerifySecurityAsyncResult = state as SecurityChannelListener<TChannel>.DuplexSessionReceiveMessageAndVerifySecurityAsyncResult;
				if (duplexSessionReceiveMessageAndVerifySecurityAsyncResult != null)
				{
					duplexSessionReceiveMessageAndVerifySecurityAsyncResult.Start();
				}
			}

			// Token: 0x060081A4 RID: 33188 RVA: 0x001E0A41 File Offset: 0x001DEC41
			protected override void AbortInnerItem(Message innerItem)
			{
			}

			// Token: 0x060081A5 RID: 33189 RVA: 0x001E0A43 File Offset: 0x001DEC43
			protected override void CloseInnerItem(Message innerItem, TimeSpan timeout)
			{
				innerItem.Close();
			}

			// Token: 0x060081A6 RID: 33190 RVA: 0x001E0A4B File Offset: 0x001DEC4B
			protected override IAsyncResult BeginTryReceiveItem(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerChannel.BeginTryReceive(timeout, callback, state);
			}

			// Token: 0x060081A7 RID: 33191 RVA: 0x001E0A5B File Offset: 0x001DEC5B
			protected override bool EndTryReceiveItem(IAsyncResult result, out Message innerItem)
			{
				return this.innerChannel.EndTryReceive(result, out innerItem);
			}

			// Token: 0x060081A8 RID: 33192 RVA: 0x001E0A6C File Offset: 0x001DEC6C
			protected override Message ProcessInnerItem(Message innerItem, TimeSpan timeout)
			{
				if (innerItem == null)
				{
					return null;
				}
				Message result = innerItem;
				this.channel.VerifyIncomingMessage(ref result, timeout);
				return result;
			}

			// Token: 0x060081A9 RID: 33193 RVA: 0x001E0A90 File Offset: 0x001DEC90
			protected override Message CreateFaultMessage(MessageFault fault, Message innerItem)
			{
				Message message = Message.CreateMessage(innerItem.Version, fault, innerItem.Version.Addressing.DefaultFaultAction);
				if (innerItem.Headers.MessageId != null)
				{
					message.InitializeReply(innerItem);
				}
				return message;
			}

			// Token: 0x060081AA RID: 33194 RVA: 0x001E0AD5 File Offset: 0x001DECD5
			protected override IAsyncResult BeginSendFault(Message innerItem, Message faultMessage, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerChannel.BeginSend(faultMessage, timeout, callback, state);
			}

			// Token: 0x060081AB RID: 33195 RVA: 0x001E0AE8 File Offset: 0x001DECE8
			protected override void EndSendFault(Message innerItem, IAsyncResult result)
			{
				this.innerChannel.EndSend(result);
			}

			// Token: 0x060081AC RID: 33196 RVA: 0x001E0AF8 File Offset: 0x001DECF8
			public static bool End(IAsyncResult result, out Message message)
			{
				SecurityChannelListener<TChannel>.DuplexSessionReceiveMessageAndVerifySecurityAsyncResult duplexSessionReceiveMessageAndVerifySecurityAsyncResult = AsyncResult.End<SecurityChannelListener<TChannel>.DuplexSessionReceiveMessageAndVerifySecurityAsyncResult>(result);
				message = duplexSessionReceiveMessageAndVerifySecurityAsyncResult.Item;
				return duplexSessionReceiveMessageAndVerifySecurityAsyncResult.ReceiveCompleted;
			}

			// Token: 0x040049C6 RID: 18886
			private IDuplexChannel innerChannel;

			// Token: 0x040049C7 RID: 18887
			private SecurityChannelListener<TChannel>.SecurityDuplexSessionChannel channel;
		}

		// Token: 0x02000E11 RID: 3601
		private sealed class InputChannelReceiveMessageAndVerifySecurityAsyncResult : SecurityChannelListener<TChannel>.ReceiveItemAndVerifySecurityAsyncResult<Message, IInputChannel>
		{
			// Token: 0x060081AD RID: 33197 RVA: 0x001E0B1A File Offset: 0x001DED1A
			public InputChannelReceiveMessageAndVerifySecurityAsyncResult(SecurityChannelListener<TChannel>.SecurityInputChannel channel, IInputChannel innerChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(channel, timeout, callback, state)
			{
				this.innerChannel = innerChannel;
				this.channel = channel;
				ActionItem.Schedule(new Action<object>(SecurityChannelListener<TChannel>.InputChannelReceiveMessageAndVerifySecurityAsyncResult.ReceiveMessage), this);
			}

			// Token: 0x17001C9F RID: 7327
			// (get) Token: 0x060081AE RID: 33198 RVA: 0x001E0B48 File Offset: 0x001DED48
			protected override SecurityStandardsManager StandardsManager
			{
				get
				{
					return this.channel.SecurityProtocol.SecurityProtocolFactory.StandardsManager;
				}
			}

			// Token: 0x060081AF RID: 33199 RVA: 0x001E0B60 File Offset: 0x001DED60
			private static void ReceiveMessage(object state)
			{
				SecurityChannelListener<TChannel>.InputChannelReceiveMessageAndVerifySecurityAsyncResult inputChannelReceiveMessageAndVerifySecurityAsyncResult = state as SecurityChannelListener<TChannel>.InputChannelReceiveMessageAndVerifySecurityAsyncResult;
				if (inputChannelReceiveMessageAndVerifySecurityAsyncResult == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException());
				}
				inputChannelReceiveMessageAndVerifySecurityAsyncResult.Start();
			}

			// Token: 0x17001CA0 RID: 7328
			// (get) Token: 0x060081B0 RID: 33200 RVA: 0x001E0B8D File Offset: 0x001DED8D
			protected override bool CanSendFault
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060081B1 RID: 33201 RVA: 0x001E0B90 File Offset: 0x001DED90
			protected override void AbortInnerItem(Message innerItem)
			{
			}

			// Token: 0x060081B2 RID: 33202 RVA: 0x001E0B92 File Offset: 0x001DED92
			protected override void CloseInnerItem(Message innerItem, TimeSpan timeout)
			{
				innerItem.Close();
			}

			// Token: 0x060081B3 RID: 33203 RVA: 0x001E0B9A File Offset: 0x001DED9A
			protected override IAsyncResult BeginTryReceiveItem(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerChannel.BeginTryReceive(timeout, callback, state);
			}

			// Token: 0x060081B4 RID: 33204 RVA: 0x001E0BAA File Offset: 0x001DEDAA
			protected override bool EndTryReceiveItem(IAsyncResult result, out Message innerItem)
			{
				return this.innerChannel.EndTryReceive(result, out innerItem);
			}

			// Token: 0x060081B5 RID: 33205 RVA: 0x001E0BBC File Offset: 0x001DEDBC
			protected override Message ProcessInnerItem(Message innerItem, TimeSpan timeout)
			{
				if (innerItem == null)
				{
					return null;
				}
				Message result = innerItem;
				this.channel.VerifyIncomingMessage(ref result, timeout);
				return result;
			}

			// Token: 0x060081B6 RID: 33206 RVA: 0x001E0BDF File Offset: 0x001DEDDF
			protected override Message CreateFaultMessage(MessageFault fault, Message innerItem)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x060081B7 RID: 33207 RVA: 0x001E0BF0 File Offset: 0x001DEDF0
			protected override IAsyncResult BeginSendFault(Message innerItem, Message faultMessage, TimeSpan timeout, AsyncCallback callback, object state)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x060081B8 RID: 33208 RVA: 0x001E0C01 File Offset: 0x001DEE01
			protected override void EndSendFault(Message innerItem, IAsyncResult result)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x060081B9 RID: 33209 RVA: 0x001E0C14 File Offset: 0x001DEE14
			public static bool End(IAsyncResult result, out Message message)
			{
				SecurityChannelListener<TChannel>.InputChannelReceiveMessageAndVerifySecurityAsyncResult inputChannelReceiveMessageAndVerifySecurityAsyncResult = AsyncResult.End<SecurityChannelListener<TChannel>.InputChannelReceiveMessageAndVerifySecurityAsyncResult>(result);
				message = inputChannelReceiveMessageAndVerifySecurityAsyncResult.Item;
				return inputChannelReceiveMessageAndVerifySecurityAsyncResult.ReceiveCompleted;
			}

			// Token: 0x040049C8 RID: 18888
			private IInputChannel innerChannel;

			// Token: 0x040049C9 RID: 18889
			private SecurityChannelListener<TChannel>.SecurityInputChannel channel;
		}
	}
}
