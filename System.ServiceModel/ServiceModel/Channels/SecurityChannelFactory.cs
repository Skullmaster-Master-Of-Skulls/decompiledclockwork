using System;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200098A RID: 2442
	internal sealed class SecurityChannelFactory<TChannel> : LayeredChannelFactory<TChannel>
	{
		// Token: 0x06005E88 RID: 24200 RVA: 0x0015D8D5 File Offset: 0x0015BAD5
		public SecurityChannelFactory(ISecurityCapabilities securityCapabilities, BindingContext context, SecuritySessionClientSettings<TChannel> sessionClientSettings) : this(securityCapabilities, context, sessionClientSettings.ChannelBuilder, sessionClientSettings.CreateInnerChannelFactory())
		{
			this.sessionMode = true;
			this.sessionClientSettings = sessionClientSettings;
		}

		// Token: 0x06005E89 RID: 24201 RVA: 0x0015D8F9 File Offset: 0x0015BAF9
		public SecurityChannelFactory(ISecurityCapabilities securityCapabilities, BindingContext context, ChannelBuilder channelBuilder, SecurityProtocolFactory protocolFactory) : this(securityCapabilities, context, channelBuilder, protocolFactory, channelBuilder.BuildChannelFactory<TChannel>())
		{
		}

		// Token: 0x06005E8A RID: 24202 RVA: 0x0015D90C File Offset: 0x0015BB0C
		public SecurityChannelFactory(ISecurityCapabilities securityCapabilities, BindingContext context, ChannelBuilder channelBuilder, SecurityProtocolFactory protocolFactory, IChannelFactory innerChannelFactory) : this(securityCapabilities, context, channelBuilder, innerChannelFactory)
		{
			this.securityProtocolFactory = protocolFactory;
		}

		// Token: 0x06005E8B RID: 24203 RVA: 0x0015D921 File Offset: 0x0015BB21
		private SecurityChannelFactory(ISecurityCapabilities securityCapabilities, BindingContext context, ChannelBuilder channelBuilder, IChannelFactory innerChannelFactory) : base(context.Binding, innerChannelFactory)
		{
			this.channelBuilder = channelBuilder;
			this.messageVersion = context.Binding.MessageVersion;
			this.securityCapabilities = securityCapabilities;
		}

		// Token: 0x06005E8C RID: 24204 RVA: 0x0015D950 File Offset: 0x0015BB50
		internal SecurityChannelFactory(Binding binding, SecurityProtocolFactory protocolFactory, IChannelFactory innerChannelFactory) : base(binding, innerChannelFactory)
		{
			this.securityProtocolFactory = protocolFactory;
		}

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x06005E8D RID: 24205 RVA: 0x0015D961 File Offset: 0x0015BB61
		public ChannelBuilder ChannelBuilder
		{
			get
			{
				return this.channelBuilder;
			}
		}

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x06005E8E RID: 24206 RVA: 0x0015D969 File Offset: 0x0015BB69
		public SecurityProtocolFactory SecurityProtocolFactory
		{
			get
			{
				return this.securityProtocolFactory;
			}
		}

		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x06005E8F RID: 24207 RVA: 0x0015D971 File Offset: 0x0015BB71
		public SecuritySessionClientSettings<TChannel> SessionClientSettings
		{
			get
			{
				return this.sessionClientSettings;
			}
		}

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x06005E90 RID: 24208 RVA: 0x0015D979 File Offset: 0x0015BB79
		public bool SessionMode
		{
			get
			{
				return this.sessionMode;
			}
		}

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x06005E91 RID: 24209 RVA: 0x0015D981 File Offset: 0x0015BB81
		private bool SupportsDuplex
		{
			get
			{
				this.ThrowIfProtocolFactoryNotSet();
				return this.securityProtocolFactory.SupportsDuplex;
			}
		}

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x06005E92 RID: 24210 RVA: 0x0015D994 File Offset: 0x0015BB94
		private bool SupportsRequestReply
		{
			get
			{
				this.ThrowIfProtocolFactoryNotSet();
				return this.securityProtocolFactory.SupportsRequestReply;
			}
		}

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x06005E93 RID: 24211 RVA: 0x0015D9A7 File Offset: 0x0015BBA7
		public MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
		}

		// Token: 0x06005E94 RID: 24212 RVA: 0x0015D9AF File Offset: 0x0015BBAF
		private void CloseProtocolFactory(bool aborted, TimeSpan timeout)
		{
			if (this.securityProtocolFactory != null && !this.SessionMode)
			{
				this.securityProtocolFactory.Close(aborted, timeout);
				this.securityProtocolFactory = null;
			}
		}

		// Token: 0x06005E95 RID: 24213 RVA: 0x0015D9D8 File Offset: 0x0015BBD8
		public override T GetProperty<T>()
		{
			if (this.SessionMode && typeof(T) == typeof(IChannelSecureConversationSessionSettings))
			{
				return (T)((object)this.SessionClientSettings);
			}
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				return (T)((object)this.securityCapabilities);
			}
			return base.GetProperty<T>();
		}

		// Token: 0x06005E96 RID: 24214 RVA: 0x0015DA41 File Offset: 0x0015BC41
		protected override void OnAbort()
		{
			base.OnAbort();
			this.CloseProtocolFactory(true, TimeSpan.Zero);
			if (this.sessionClientSettings != null)
			{
				this.sessionClientSettings.Abort();
			}
		}

		// Token: 0x06005E97 RID: 24215 RVA: 0x0015DA68 File Offset: 0x0015BC68
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			List<OperationWithTimeoutBeginCallback> list = new List<OperationWithTimeoutBeginCallback>();
			List<OperationEndCallback> list2 = new List<OperationEndCallback>();
			list.Add(new OperationWithTimeoutBeginCallback(base.OnBeginClose));
			list2.Add(new OperationEndCallback(base.OnEndClose));
			if (this.securityProtocolFactory != null && !this.SessionMode)
			{
				list.Add(new OperationWithTimeoutBeginCallback(this.securityProtocolFactory.BeginClose));
				list2.Add(new OperationEndCallback(this.securityProtocolFactory.EndClose));
			}
			if (this.sessionClientSettings != null)
			{
				list.Add(new OperationWithTimeoutBeginCallback(this.sessionClientSettings.BeginClose));
				list2.Add(new OperationEndCallback(this.sessionClientSettings.EndClose));
			}
			return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, list.ToArray(), list2.ToArray(), callback, state);
		}

		// Token: 0x06005E98 RID: 24216 RVA: 0x0015DB2D File Offset: 0x0015BD2D
		protected override void OnEndClose(IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
		}

		// Token: 0x06005E99 RID: 24217 RVA: 0x0015DB38 File Offset: 0x0015BD38
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnClose(timeout);
			this.CloseProtocolFactory(false, timeoutHelper.RemainingTime());
			if (this.sessionClientSettings != null)
			{
				this.sessionClientSettings.Close(timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x06005E9A RID: 24218 RVA: 0x0015DB7C File Offset: 0x0015BD7C
		protected override TChannel OnCreateChannel(EndpointAddress address, Uri via)
		{
			base.ThrowIfDisposed();
			if (this.SessionMode)
			{
				return this.sessionClientSettings.OnCreateChannel(address, via);
			}
			if (typeof(TChannel) == typeof(IOutputChannel))
			{
				return (TChannel)((object)new SecurityChannelFactory<TChannel>.SecurityOutputChannel(this, this.securityProtocolFactory, ((IChannelFactory<IOutputChannel>)base.InnerChannelFactory).CreateChannel(address, via), address, via));
			}
			if (typeof(TChannel) == typeof(IOutputSessionChannel))
			{
				return (TChannel)((object)new SecurityChannelFactory<TChannel>.SecurityOutputSessionChannel(this, this.securityProtocolFactory, ((IChannelFactory<IOutputSessionChannel>)base.InnerChannelFactory).CreateChannel(address, via), address, via));
			}
			if (typeof(TChannel) == typeof(IDuplexChannel))
			{
				return (TChannel)((object)new SecurityChannelFactory<TChannel>.SecurityDuplexChannel(this, this.securityProtocolFactory, ((IChannelFactory<IDuplexChannel>)base.InnerChannelFactory).CreateChannel(address, via), address, via));
			}
			if (typeof(TChannel) == typeof(IDuplexSessionChannel))
			{
				return (TChannel)((object)new SecurityChannelFactory<TChannel>.SecurityDuplexSessionChannel(this, this.securityProtocolFactory, ((IChannelFactory<IDuplexSessionChannel>)base.InnerChannelFactory).CreateChannel(address, via), address, via));
			}
			if (typeof(TChannel) == typeof(IRequestChannel))
			{
				return (TChannel)((object)new SecurityChannelFactory<TChannel>.SecurityRequestChannel(this, this.securityProtocolFactory, ((IChannelFactory<IRequestChannel>)base.InnerChannelFactory).CreateChannel(address, via), address, via));
			}
			return (TChannel)((object)new SecurityChannelFactory<TChannel>.SecurityRequestSessionChannel(this, this.securityProtocolFactory, ((IChannelFactory<IRequestSessionChannel>)base.InnerChannelFactory).CreateChannel(address, via), address, via));
		}

		// Token: 0x06005E9B RID: 24219 RVA: 0x0015DD10 File Offset: 0x0015BF10
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.OnOpenCore(timeoutHelper.RemainingTime());
			base.OnOpen(timeoutHelper.RemainingTime());
			this.SetBufferManager();
		}

		// Token: 0x06005E9C RID: 24220 RVA: 0x0015DD48 File Offset: 0x0015BF48
		private void SetBufferManager()
		{
			ITransportFactorySettings property = this.GetProperty<ITransportFactorySettings>();
			if (property == null)
			{
				return;
			}
			BufferManager bufferManager = property.BufferManager;
			if (bufferManager == null)
			{
				return;
			}
			if (this.SessionMode && this.SessionClientSettings != null && this.SessionClientSettings.SessionProtocolFactory != null)
			{
				this.SessionClientSettings.SessionProtocolFactory.StreamBufferManager = bufferManager;
				return;
			}
			this.ThrowIfProtocolFactoryNotSet();
			this.securityProtocolFactory.StreamBufferManager = bufferManager;
		}

		// Token: 0x06005E9D RID: 24221 RVA: 0x0015DDAC File Offset: 0x0015BFAC
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new OperationWithTimeoutAsyncResult(new OperationWithTimeoutCallback(this.OnOpen), timeout, callback, state);
		}

		// Token: 0x06005E9E RID: 24222 RVA: 0x0015DDC3 File Offset: 0x0015BFC3
		protected override void OnEndOpen(IAsyncResult result)
		{
			OperationWithTimeoutAsyncResult.End(result);
		}

		// Token: 0x06005E9F RID: 24223 RVA: 0x0015DDCC File Offset: 0x0015BFCC
		private void OnOpenCore(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.SessionMode)
			{
				this.SessionClientSettings.Open(this, base.InnerChannelFactory, this.ChannelBuilder, timeoutHelper.RemainingTime());
				return;
			}
			this.ThrowIfProtocolFactoryNotSet();
			this.securityProtocolFactory.Open(true, timeoutHelper.RemainingTime());
		}

		// Token: 0x06005EA0 RID: 24224 RVA: 0x0015DE22 File Offset: 0x0015C022
		private void ThrowIfDuplexNotSupported()
		{
			if (!this.SupportsDuplex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityProtocolFactoryDoesNotSupportDuplex", new object[]
				{
					this.securityProtocolFactory
				})));
			}
		}

		// Token: 0x06005EA1 RID: 24225 RVA: 0x0015DE55 File Offset: 0x0015C055
		private void ThrowIfProtocolFactoryNotSet()
		{
			if (this.securityProtocolFactory == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityProtocolFactoryShouldBeSetBeforeThisOperation")));
			}
		}

		// Token: 0x06005EA2 RID: 24226 RVA: 0x0015DE79 File Offset: 0x0015C079
		private void ThrowIfRequestReplyNotSupported()
		{
			if (!this.SupportsRequestReply)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecurityProtocolFactoryDoesNotSupportRequestReply", new object[]
				{
					this.securityProtocolFactory
				})));
			}
		}

		// Token: 0x040037FF RID: 14335
		private ChannelBuilder channelBuilder;

		// Token: 0x04003800 RID: 14336
		private SecurityProtocolFactory securityProtocolFactory;

		// Token: 0x04003801 RID: 14337
		private SecuritySessionClientSettings<TChannel> sessionClientSettings;

		// Token: 0x04003802 RID: 14338
		private bool sessionMode;

		// Token: 0x04003803 RID: 14339
		private MessageVersion messageVersion;

		// Token: 0x04003804 RID: 14340
		private ISecurityCapabilities securityCapabilities;

		// Token: 0x02000DF9 RID: 3577
		private abstract class ClientSecurityChannel<UChannel> : SecurityChannel<UChannel> where UChannel : class, IChannel
		{
			// Token: 0x060080F0 RID: 33008 RVA: 0x001DEBD5 File Offset: 0x001DCDD5
			protected ClientSecurityChannel(ChannelManagerBase factory, SecurityProtocolFactory securityProtocolFactory, UChannel innerChannel, EndpointAddress to, Uri via) : base(factory, innerChannel)
			{
				this.to = to;
				this.via = via;
				this.securityProtocolFactory = securityProtocolFactory;
				this.channelParameters = new ChannelParameterCollection(this);
			}

			// Token: 0x17001C82 RID: 7298
			// (get) Token: 0x060080F1 RID: 33009 RVA: 0x001DEC02 File Offset: 0x001DCE02
			protected SecurityProtocolFactory SecurityProtocolFactory
			{
				get
				{
					return this.securityProtocolFactory;
				}
			}

			// Token: 0x17001C83 RID: 7299
			// (get) Token: 0x060080F2 RID: 33010 RVA: 0x001DEC0A File Offset: 0x001DCE0A
			public EndpointAddress RemoteAddress
			{
				get
				{
					return this.to;
				}
			}

			// Token: 0x17001C84 RID: 7300
			// (get) Token: 0x060080F3 RID: 33011 RVA: 0x001DEC12 File Offset: 0x001DCE12
			public Uri Via
			{
				get
				{
					return this.via;
				}
			}

			// Token: 0x060080F4 RID: 33012 RVA: 0x001DEC1C File Offset: 0x001DCE1C
			protected bool TryGetSecurityFaultException(Message faultMessage, out Exception faultException)
			{
				faultException = null;
				if (!faultMessage.IsFault)
				{
					return false;
				}
				MessageFault fault = MessageFault.CreateFault(faultMessage, 16384);
				faultException = SecurityUtils.CreateSecurityFaultException(fault);
				return true;
			}

			// Token: 0x060080F5 RID: 33013 RVA: 0x001DEC4B File Offset: 0x001DCE4B
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				this.EnableChannelBindingSupport();
				return new SecurityChannelFactory<TChannel>.ClientSecurityChannel<UChannel>.OpenAsyncResult(this, timeout, callback, state);
			}

			// Token: 0x060080F6 RID: 33014 RVA: 0x001DEC5C File Offset: 0x001DCE5C
			protected override void OnEndOpen(IAsyncResult result)
			{
				SecurityChannelFactory<TChannel>.ClientSecurityChannel<UChannel>.OpenAsyncResult.End(result);
			}

			// Token: 0x060080F7 RID: 33015 RVA: 0x001DEC64 File Offset: 0x001DCE64
			protected override void OnOpen(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.EnableChannelBindingSupport();
				SecurityProtocol securityProtocol = this.SecurityProtocolFactory.CreateSecurityProtocol(this.to, this.Via, null, typeof(TChannel) == typeof(IRequestChannel), timeoutHelper.RemainingTime());
				this.OnProtocolCreationComplete(securityProtocol);
				base.SecurityProtocol.Open(timeoutHelper.RemainingTime());
				base.OnOpen(timeoutHelper.RemainingTime());
			}

			// Token: 0x060080F8 RID: 33016 RVA: 0x001DECE0 File Offset: 0x001DCEE0
			private void EnableChannelBindingSupport()
			{
				if (this.securityProtocolFactory != null && this.securityProtocolFactory.ExtendedProtectionPolicy != null && this.securityProtocolFactory.ExtendedProtectionPolicy.CustomChannelBinding != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ExtendedProtectionPolicyCustomChannelBindingNotSupported")));
				}
				if (SecurityUtils.IsChannelBindingDisabled || !SecurityUtils.IsSecurityBindingSuitableForChannelBinding(this.SecurityProtocolFactory.SecurityBindingElement as TransportSecurityBindingElement))
				{
					return;
				}
				if (base.InnerChannel != null)
				{
					IChannelBindingProvider property = base.InnerChannel.GetProperty<IChannelBindingProvider>();
					if (property != null)
					{
						property.EnableChannelBindingSupport();
					}
				}
			}

			// Token: 0x060080F9 RID: 33017 RVA: 0x001DED74 File Offset: 0x001DCF74
			private void OnProtocolCreationComplete(SecurityProtocol securityProtocol)
			{
				base.SecurityProtocol = securityProtocol;
				base.SecurityProtocol.ChannelParameters = this.channelParameters;
			}

			// Token: 0x060080FA RID: 33018 RVA: 0x001DED8E File Offset: 0x001DCF8E
			public override T GetProperty<T>()
			{
				if (typeof(T) == typeof(ChannelParameterCollection))
				{
					return (T)((object)this.channelParameters);
				}
				return base.GetProperty<T>();
			}

			// Token: 0x040049A6 RID: 18854
			private EndpointAddress to;

			// Token: 0x040049A7 RID: 18855
			private Uri via;

			// Token: 0x040049A8 RID: 18856
			private SecurityProtocolFactory securityProtocolFactory;

			// Token: 0x040049A9 RID: 18857
			private ChannelParameterCollection channelParameters;

			// Token: 0x02000F7B RID: 3963
			private sealed class OpenAsyncResult : AsyncResult
			{
				// Token: 0x060087F0 RID: 34800 RVA: 0x001F9550 File Offset: 0x001F7750
				public OpenAsyncResult(SecurityChannelFactory<TChannel>.ClientSecurityChannel<UChannel> clientChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.clientChannel = clientChannel;
					SecurityProtocol securityProtocol = this.clientChannel.SecurityProtocolFactory.CreateSecurityProtocol(this.clientChannel.to, this.clientChannel.Via, null, typeof(TChannel) == typeof(IRequestChannel), this.timeoutHelper.RemainingTime());
					bool flag = this.OnCreateSecurityProtocolComplete(securityProtocol);
					if (flag)
					{
						base.Complete(true);
					}
				}

				// Token: 0x060087F1 RID: 34801 RVA: 0x001F95D7 File Offset: 0x001F77D7
				internal static void End(IAsyncResult result)
				{
					AsyncResult.End<SecurityChannelFactory<TChannel>.ClientSecurityChannel<UChannel>.OpenAsyncResult>(result);
				}

				// Token: 0x060087F2 RID: 34802 RVA: 0x001F95E0 File Offset: 0x001F77E0
				private bool OnCreateSecurityProtocolComplete(SecurityProtocol securityProtocol)
				{
					this.clientChannel.OnProtocolCreationComplete(securityProtocol);
					IAsyncResult asyncResult = securityProtocol.BeginOpen(this.timeoutHelper.RemainingTime(), SecurityChannelFactory<TChannel>.ClientSecurityChannel<UChannel>.OpenAsyncResult.openSecurityProtocolCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					securityProtocol.EndOpen(asyncResult);
					return this.OnSecurityProtocolOpenComplete();
				}

				// Token: 0x060087F3 RID: 34803 RVA: 0x001F9628 File Offset: 0x001F7828
				private static void OpenSecurityProtocolCallback(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecurityChannelFactory<TChannel>.ClientSecurityChannel<UChannel>.OpenAsyncResult openAsyncResult = result.AsyncState as SecurityChannelFactory<TChannel>.ClientSecurityChannel<UChannel>.OpenAsyncResult;
					if (openAsyncResult == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidAsyncResult"), "result"));
					}
					Exception exception = null;
					bool flag = false;
					try
					{
						openAsyncResult.clientChannel.SecurityProtocol.EndOpen(result);
						flag = openAsyncResult.OnSecurityProtocolOpenComplete();
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
						openAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x060087F4 RID: 34804 RVA: 0x001F96B8 File Offset: 0x001F78B8
				private bool OnSecurityProtocolOpenComplete()
				{
					IAsyncResult asyncResult = this.clientChannel.InnerChannel.BeginOpen(this.timeoutHelper.RemainingTime(), SecurityChannelFactory<TChannel>.ClientSecurityChannel<UChannel>.OpenAsyncResult.openInnerChannelCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					this.clientChannel.InnerChannel.EndOpen(asyncResult);
					return true;
				}

				// Token: 0x060087F5 RID: 34805 RVA: 0x001F9710 File Offset: 0x001F7910
				private static void OpenInnerChannelCallback(IAsyncResult result)
				{
					if (result == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("result"));
					}
					if (result.CompletedSynchronously)
					{
						return;
					}
					SecurityChannelFactory<TChannel>.ClientSecurityChannel<UChannel>.OpenAsyncResult openAsyncResult = result.AsyncState as SecurityChannelFactory<TChannel>.ClientSecurityChannel<UChannel>.OpenAsyncResult;
					if (openAsyncResult == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidAsyncResult"), "result"));
					}
					Exception exception = null;
					try
					{
						openAsyncResult.clientChannel.InnerChannel.EndOpen(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					openAsyncResult.Complete(false, exception);
				}

				// Token: 0x04004F51 RID: 20305
				private readonly SecurityChannelFactory<TChannel>.ClientSecurityChannel<UChannel> clientChannel;

				// Token: 0x04004F52 RID: 20306
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004F53 RID: 20307
				private static readonly AsyncCallback openInnerChannelCallback = Fx.ThunkCallback(new AsyncCallback(SecurityChannelFactory<TChannel>.ClientSecurityChannel<UChannel>.OpenAsyncResult.OpenInnerChannelCallback));

				// Token: 0x04004F54 RID: 20308
				private static readonly AsyncCallback openSecurityProtocolCallback = Fx.ThunkCallback(new AsyncCallback(SecurityChannelFactory<TChannel>.ClientSecurityChannel<UChannel>.OpenAsyncResult.OpenSecurityProtocolCallback));
			}
		}

		// Token: 0x02000DFA RID: 3578
		private class SecurityOutputChannel : SecurityChannelFactory<TChannel>.ClientSecurityChannel<IOutputChannel>, IOutputChannel, IChannel, ICommunicationObject
		{
			// Token: 0x060080FB RID: 33019 RVA: 0x001DEDBD File Offset: 0x001DCFBD
			public SecurityOutputChannel(ChannelManagerBase factory, SecurityProtocolFactory securityProtocolFactory, IOutputChannel innerChannel, EndpointAddress to, Uri via) : base(factory, securityProtocolFactory, innerChannel, to, via)
			{
			}

			// Token: 0x060080FC RID: 33020 RVA: 0x001DEDCC File Offset: 0x001DCFCC
			public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
			{
				return this.BeginSend(message, base.DefaultSendTimeout, callback, state);
			}

			// Token: 0x060080FD RID: 33021 RVA: 0x001DEDDD File Offset: 0x001DCFDD
			public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				base.ThrowIfFaulted();
				base.ThrowIfDisposedOrNotOpen(message);
				return new SecurityChannel<IOutputChannel>.OutputChannelSendAsyncResult(message, base.SecurityProtocol, base.InnerChannel, timeout, callback, state);
			}

			// Token: 0x060080FE RID: 33022 RVA: 0x001DEE02 File Offset: 0x001DD002
			public void EndSend(IAsyncResult result)
			{
				SecurityChannel<IOutputChannel>.OutputChannelSendAsyncResult.End(result);
			}

			// Token: 0x060080FF RID: 33023 RVA: 0x001DEE0A File Offset: 0x001DD00A
			public void Send(Message message)
			{
				this.Send(message, base.DefaultSendTimeout);
			}

			// Token: 0x06008100 RID: 33024 RVA: 0x001DEE1C File Offset: 0x001DD01C
			public void Send(Message message, TimeSpan timeout)
			{
				base.ThrowIfFaulted();
				base.ThrowIfDisposedOrNotOpen(message);
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.SecurityProtocol.SecureOutgoingMessage(ref message, timeoutHelper.RemainingTime());
				base.InnerChannel.Send(message, timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x02000DFB RID: 3579
		private sealed class SecurityOutputSessionChannel : SecurityChannelFactory<TChannel>.SecurityOutputChannel, IOutputSessionChannel, IOutputChannel, IChannel, ICommunicationObject, ISessionChannel<IOutputSession>
		{
			// Token: 0x06008101 RID: 33025 RVA: 0x001DEE65 File Offset: 0x001DD065
			public SecurityOutputSessionChannel(ChannelManagerBase factory, SecurityProtocolFactory securityProtocolFactory, IOutputSessionChannel innerChannel, EndpointAddress to, Uri via) : base(factory, securityProtocolFactory, innerChannel, to, via)
			{
			}

			// Token: 0x17001C85 RID: 7301
			// (get) Token: 0x06008102 RID: 33026 RVA: 0x001DEE74 File Offset: 0x001DD074
			public IOutputSession Session
			{
				get
				{
					return ((IOutputSessionChannel)base.InnerChannel).Session;
				}
			}
		}

		// Token: 0x02000DFC RID: 3580
		private class SecurityRequestChannel : SecurityChannelFactory<TChannel>.ClientSecurityChannel<IRequestChannel>, IRequestChannel, IChannel, ICommunicationObject
		{
			// Token: 0x06008103 RID: 33027 RVA: 0x001DEE86 File Offset: 0x001DD086
			public SecurityRequestChannel(ChannelManagerBase factory, SecurityProtocolFactory securityProtocolFactory, IRequestChannel innerChannel, EndpointAddress to, Uri via) : base(factory, securityProtocolFactory, innerChannel, to, via)
			{
			}

			// Token: 0x06008104 RID: 33028 RVA: 0x001DEE95 File Offset: 0x001DD095
			public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state)
			{
				return this.BeginRequest(message, base.DefaultSendTimeout, callback, state);
			}

			// Token: 0x06008105 RID: 33029 RVA: 0x001DEEA6 File Offset: 0x001DD0A6
			public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				base.ThrowIfFaulted();
				base.ThrowIfDisposedOrNotOpen(message);
				return new SecurityChannelFactory<TChannel>.RequestChannelSendAsyncResult(message, base.SecurityProtocol, base.InnerChannel, this, timeout, callback, state);
			}

			// Token: 0x06008106 RID: 33030 RVA: 0x001DEECC File Offset: 0x001DD0CC
			public Message EndRequest(IAsyncResult result)
			{
				return SecurityChannelFactory<TChannel>.RequestChannelSendAsyncResult.End(result);
			}

			// Token: 0x06008107 RID: 33031 RVA: 0x001DEED4 File Offset: 0x001DD0D4
			public Message Request(Message message)
			{
				return this.Request(message, base.DefaultSendTimeout);
			}

			// Token: 0x06008108 RID: 33032 RVA: 0x001DEEE4 File Offset: 0x001DD0E4
			internal Message ProcessReply(Message reply, SecurityProtocolCorrelationState correlationState, TimeSpan timeout)
			{
				if (reply != null)
				{
					if (DiagnosticUtility.ShouldUseActivity)
					{
						ServiceModelActivity serviceModelActivity = TraceUtility.ExtractActivity(reply);
						if (serviceModelActivity != null && correlationState != null && correlationState.Activity != null && serviceModelActivity.Id != correlationState.Activity.Id)
						{
							using (ServiceModelActivity.BoundOperation(serviceModelActivity))
							{
								if (FxTrace.Trace != null)
								{
									FxTrace.Trace.TraceTransfer(correlationState.Activity.Id);
								}
								serviceModelActivity.Stop();
							}
						}
					}
					ServiceModelActivity activity2 = (correlationState == null) ? null : correlationState.Activity;
					using (ServiceModelActivity.BoundOperation(activity2))
					{
						if (DiagnosticUtility.ShouldUseActivity)
						{
							TraceUtility.SetActivity(reply, activity2);
						}
						Message faultMessage = reply;
						Exception ex = null;
						try
						{
							base.SecurityProtocol.VerifyIncomingMessage(ref reply, timeout, new SecurityProtocolCorrelationState[]
							{
								correlationState
							});
						}
						catch (MessageSecurityException)
						{
							base.TryGetSecurityFaultException(faultMessage, out ex);
							if (ex == null)
							{
								throw;
							}
						}
						if (ex != null)
						{
							base.Fault(ex);
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(ex);
						}
					}
				}
				return reply;
			}

			// Token: 0x06008109 RID: 33033 RVA: 0x001DF008 File Offset: 0x001DD208
			public Message Request(Message message, TimeSpan timeout)
			{
				base.ThrowIfFaulted();
				base.ThrowIfDisposedOrNotOpen(message);
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				SecurityProtocolCorrelationState correlationState = base.SecurityProtocol.SecureOutgoingMessage(ref message, timeoutHelper.RemainingTime(), null);
				Message reply = base.InnerChannel.Request(message, timeoutHelper.RemainingTime());
				return this.ProcessReply(reply, correlationState, timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x02000DFD RID: 3581
		private sealed class SecurityRequestSessionChannel : SecurityChannelFactory<TChannel>.SecurityRequestChannel, IRequestSessionChannel, IRequestChannel, IChannel, ICommunicationObject, ISessionChannel<IOutputSession>
		{
			// Token: 0x0600810A RID: 33034 RVA: 0x001DF063 File Offset: 0x001DD263
			public SecurityRequestSessionChannel(ChannelManagerBase factory, SecurityProtocolFactory securityProtocolFactory, IRequestSessionChannel innerChannel, EndpointAddress to, Uri via) : base(factory, securityProtocolFactory, innerChannel, to, via)
			{
			}

			// Token: 0x17001C86 RID: 7302
			// (get) Token: 0x0600810B RID: 33035 RVA: 0x001DF072 File Offset: 0x001DD272
			public IOutputSession Session
			{
				get
				{
					return ((IRequestSessionChannel)base.InnerChannel).Session;
				}
			}
		}

		// Token: 0x02000DFE RID: 3582
		private class SecurityDuplexChannel : SecurityChannelFactory<TChannel>.SecurityOutputChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel
		{
			// Token: 0x0600810C RID: 33036 RVA: 0x001DF084 File Offset: 0x001DD284
			public SecurityDuplexChannel(ChannelManagerBase factory, SecurityProtocolFactory securityProtocolFactory, IDuplexChannel innerChannel, EndpointAddress to, Uri via) : base(factory, securityProtocolFactory, innerChannel, to, via)
			{
			}

			// Token: 0x17001C87 RID: 7303
			// (get) Token: 0x0600810D RID: 33037 RVA: 0x001DF093 File Offset: 0x001DD293
			internal IDuplexChannel InnerDuplexChannel
			{
				get
				{
					return (IDuplexChannel)base.InnerChannel;
				}
			}

			// Token: 0x17001C88 RID: 7304
			// (get) Token: 0x0600810E RID: 33038 RVA: 0x001DF0A0 File Offset: 0x001DD2A0
			public EndpointAddress LocalAddress
			{
				get
				{
					return this.InnerDuplexChannel.LocalAddress;
				}
			}

			// Token: 0x17001C89 RID: 7305
			// (get) Token: 0x0600810F RID: 33039 RVA: 0x001DF0AD File Offset: 0x001DD2AD
			internal virtual bool AcceptUnsecuredFaults
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008110 RID: 33040 RVA: 0x001DF0B0 File Offset: 0x001DD2B0
			public Message Receive()
			{
				return this.Receive(base.DefaultReceiveTimeout);
			}

			// Token: 0x06008111 RID: 33041 RVA: 0x001DF0BE File Offset: 0x001DD2BE
			public Message Receive(TimeSpan timeout)
			{
				return InputChannel.HelpReceive(this, timeout);
			}

			// Token: 0x06008112 RID: 33042 RVA: 0x001DF0C7 File Offset: 0x001DD2C7
			public IAsyncResult BeginReceive(AsyncCallback callback, object state)
			{
				return this.BeginReceive(base.DefaultReceiveTimeout, callback, state);
			}

			// Token: 0x06008113 RID: 33043 RVA: 0x001DF0D7 File Offset: 0x001DD2D7
			public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return InputChannel.HelpBeginReceive(this, timeout, callback, state);
			}

			// Token: 0x06008114 RID: 33044 RVA: 0x001DF0E2 File Offset: 0x001DD2E2
			public Message EndReceive(IAsyncResult result)
			{
				return InputChannel.HelpEndReceive(result);
			}

			// Token: 0x06008115 RID: 33045 RVA: 0x001DF0EC File Offset: 0x001DD2EC
			public virtual IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				if (base.DoneReceivingInCurrentState())
				{
					return new DoneReceivingAsyncResult(callback, state);
				}
				SecurityChannelFactory<TChannel>.ClientDuplexReceiveMessageAndVerifySecurityAsyncResult clientDuplexReceiveMessageAndVerifySecurityAsyncResult = new SecurityChannelFactory<TChannel>.ClientDuplexReceiveMessageAndVerifySecurityAsyncResult(this, this.InnerDuplexChannel, timeout, callback, state);
				clientDuplexReceiveMessageAndVerifySecurityAsyncResult.Start();
				return clientDuplexReceiveMessageAndVerifySecurityAsyncResult;
			}

			// Token: 0x06008116 RID: 33046 RVA: 0x001DF120 File Offset: 0x001DD320
			public virtual bool EndTryReceive(IAsyncResult result, out Message message)
			{
				DoneReceivingAsyncResult doneReceivingAsyncResult = result as DoneReceivingAsyncResult;
				if (doneReceivingAsyncResult != null)
				{
					return DoneReceivingAsyncResult.End(doneReceivingAsyncResult, out message);
				}
				return ReceiveMessageAndVerifySecurityAsyncResultBase.End(result, out message);
			}

			// Token: 0x06008117 RID: 33047 RVA: 0x001DF148 File Offset: 0x001DD348
			internal Message ProcessMessage(Message message, TimeSpan timeout)
			{
				if (message == null)
				{
					return null;
				}
				Message faultMessage = message;
				Exception ex = null;
				try
				{
					base.SecurityProtocol.VerifyIncomingMessage(ref message, timeout);
				}
				catch (MessageSecurityException)
				{
					base.TryGetSecurityFaultException(faultMessage, out ex);
					if (ex == null)
					{
						throw;
					}
				}
				if (ex != null)
				{
					if (this.AcceptUnsecuredFaults)
					{
						base.Fault(ex);
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(ex);
				}
				return message;
			}

			// Token: 0x06008118 RID: 33048 RVA: 0x001DF1B0 File Offset: 0x001DD3B0
			public bool TryReceive(TimeSpan timeout, out Message message)
			{
				if (base.DoneReceivingInCurrentState())
				{
					message = null;
					return true;
				}
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (!this.InnerDuplexChannel.TryReceive(timeoutHelper.RemainingTime(), out message))
				{
					return false;
				}
				message = this.ProcessMessage(message, timeoutHelper.RemainingTime());
				return true;
			}

			// Token: 0x06008119 RID: 33049 RVA: 0x001DF1FB File Offset: 0x001DD3FB
			public bool WaitForMessage(TimeSpan timeout)
			{
				return this.InnerDuplexChannel.WaitForMessage(timeout);
			}

			// Token: 0x0600811A RID: 33050 RVA: 0x001DF209 File Offset: 0x001DD409
			public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.InnerDuplexChannel.BeginWaitForMessage(timeout, callback, state);
			}

			// Token: 0x0600811B RID: 33051 RVA: 0x001DF219 File Offset: 0x001DD419
			public bool EndWaitForMessage(IAsyncResult result)
			{
				return this.InnerDuplexChannel.EndWaitForMessage(result);
			}
		}

		// Token: 0x02000DFF RID: 3583
		private sealed class SecurityDuplexSessionChannel : SecurityChannelFactory<TChannel>.SecurityDuplexChannel, IDuplexSessionChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel, ISessionChannel<IDuplexSession>
		{
			// Token: 0x0600811C RID: 33052 RVA: 0x001DF227 File Offset: 0x001DD427
			public SecurityDuplexSessionChannel(ChannelManagerBase factory, SecurityProtocolFactory securityProtocolFactory, IDuplexSessionChannel innerChannel, EndpointAddress to, Uri via) : base(factory, securityProtocolFactory, innerChannel, to, via)
			{
			}

			// Token: 0x17001C8A RID: 7306
			// (get) Token: 0x0600811D RID: 33053 RVA: 0x001DF236 File Offset: 0x001DD436
			public IDuplexSession Session
			{
				get
				{
					return ((IDuplexSessionChannel)base.InnerChannel).Session;
				}
			}

			// Token: 0x17001C8B RID: 7307
			// (get) Token: 0x0600811E RID: 33054 RVA: 0x001DF248 File Offset: 0x001DD448
			internal override bool AcceptUnsecuredFaults
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x02000E00 RID: 3584
		private sealed class RequestChannelSendAsyncResult : ApplySecurityAndSendAsyncResult<IRequestChannel>
		{
			// Token: 0x0600811F RID: 33055 RVA: 0x001DF24B File Offset: 0x001DD44B
			public RequestChannelSendAsyncResult(Message message, SecurityProtocol protocol, IRequestChannel channel, SecurityChannelFactory<TChannel>.SecurityRequestChannel securityChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(protocol, channel, timeout, callback, state)
			{
				this.securityChannel = securityChannel;
				base.Begin(message, null);
			}

			// Token: 0x06008120 RID: 33056 RVA: 0x001DF26B File Offset: 0x001DD46B
			protected override IAsyncResult BeginSendCore(IRequestChannel channel, Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return channel.BeginRequest(message, timeout, callback, state);
			}

			// Token: 0x06008121 RID: 33057 RVA: 0x001DF27C File Offset: 0x001DD47C
			internal static Message End(IAsyncResult result)
			{
				SecurityChannelFactory<TChannel>.RequestChannelSendAsyncResult requestChannelSendAsyncResult = result as SecurityChannelFactory<TChannel>.RequestChannelSendAsyncResult;
				ApplySecurityAndSendAsyncResult<IRequestChannel>.OnEnd(requestChannelSendAsyncResult);
				return requestChannelSendAsyncResult.reply;
			}

			// Token: 0x06008122 RID: 33058 RVA: 0x001DF29C File Offset: 0x001DD49C
			protected override void EndSendCore(IRequestChannel channel, IAsyncResult result)
			{
				this.reply = channel.EndRequest(result);
			}

			// Token: 0x06008123 RID: 33059 RVA: 0x001DF2AB File Offset: 0x001DD4AB
			protected override void OnSendCompleteCore(TimeSpan timeout)
			{
				this.reply = this.securityChannel.ProcessReply(this.reply, base.CorrelationState, timeout);
			}

			// Token: 0x040049AA RID: 18858
			private Message reply;

			// Token: 0x040049AB RID: 18859
			private SecurityChannelFactory<TChannel>.SecurityRequestChannel securityChannel;
		}

		// Token: 0x02000E01 RID: 3585
		private class ClientDuplexReceiveMessageAndVerifySecurityAsyncResult : ReceiveMessageAndVerifySecurityAsyncResultBase
		{
			// Token: 0x06008124 RID: 33060 RVA: 0x001DF2CB File Offset: 0x001DD4CB
			public ClientDuplexReceiveMessageAndVerifySecurityAsyncResult(SecurityChannelFactory<TChannel>.SecurityDuplexChannel channel, IDuplexChannel innerChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(innerChannel, timeout, callback, state)
			{
				this.channel = channel;
			}

			// Token: 0x06008125 RID: 33061 RVA: 0x001DF2E0 File Offset: 0x001DD4E0
			protected override bool OnInnerReceiveDone(ref Message message, TimeSpan timeout)
			{
				message = this.channel.ProcessMessage(message, timeout);
				return true;
			}

			// Token: 0x040049AC RID: 18860
			private SecurityChannelFactory<TChannel>.SecurityDuplexChannel channel;
		}
	}
}
