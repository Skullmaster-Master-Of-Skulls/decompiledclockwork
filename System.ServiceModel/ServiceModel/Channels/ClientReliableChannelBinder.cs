using System;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000907 RID: 2311
	internal abstract class ClientReliableChannelBinder<TChannel> : ReliableChannelBinder<TChannel>, IClientReliableChannelBinder, IReliableChannelBinder where TChannel : class, IChannel
	{
		// Token: 0x0600582E RID: 22574 RVA: 0x001440A8 File Offset: 0x001422A8
		protected ClientReliableChannelBinder(EndpointAddress to, Uri via, IChannelFactory<TChannel> factory, MaskingMode maskingMode, TolerateFaultsMode faultMode, ChannelParameterCollection channelParameters, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(factory.CreateChannel(to, via), maskingMode, faultMode, defaultCloseTimeout, defaultSendTimeout)
		{
			if (channelParameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channelParameters");
			}
			this.to = to;
			this.via = via;
			this.factory = factory;
			this.channelParameters = channelParameters;
		}

		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x0600582F RID: 22575 RVA: 0x001440FC File Offset: 0x001422FC
		protected override bool CanGetChannelForReceive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x06005830 RID: 22576 RVA: 0x001440FF File Offset: 0x001422FF
		public override bool CanSendAsynchronously
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x06005831 RID: 22577 RVA: 0x00144102 File Offset: 0x00142302
		public override ChannelParameterCollection ChannelParameters
		{
			get
			{
				return this.channelParameters;
			}
		}

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x06005832 RID: 22578 RVA: 0x0014410A File Offset: 0x0014230A
		protected override bool MustCloseChannel
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x06005833 RID: 22579 RVA: 0x0014410D File Offset: 0x0014230D
		protected override bool MustOpenChannel
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x06005834 RID: 22580 RVA: 0x00144110 File Offset: 0x00142310
		public Uri Via
		{
			get
			{
				return this.via;
			}
		}

		// Token: 0x06005835 RID: 22581 RVA: 0x00144118 File Offset: 0x00142318
		public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.BeginRequest(message, timeout, base.DefaultMaskingMode, callback, state);
		}

		// Token: 0x06005836 RID: 22582 RVA: 0x0014412C File Offset: 0x0014232C
		public IAsyncResult BeginRequest(Message message, TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state)
		{
			ClientReliableChannelBinder<TChannel>.RequestAsyncResult requestAsyncResult = new ClientReliableChannelBinder<TChannel>.RequestAsyncResult(this, callback, state);
			requestAsyncResult.Start(message, timeout, maskingMode);
			return requestAsyncResult;
		}

		// Token: 0x06005837 RID: 22583 RVA: 0x00144150 File Offset: 0x00142350
		protected override IAsyncResult BeginTryGetChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			CommunicationState state2 = base.State;
			TChannel data;
			if (state2 == CommunicationState.Created || state2 == CommunicationState.Opening || state2 == CommunicationState.Opened)
			{
				data = this.factory.CreateChannel(this.to, this.via);
			}
			else
			{
				data = default(TChannel);
			}
			return new CompletedAsyncResult<TChannel>(data, callback, state);
		}

		// Token: 0x06005838 RID: 22584 RVA: 0x0014419C File Offset: 0x0014239C
		public static IClientReliableChannelBinder CreateBinder(EndpointAddress to, Uri via, IChannelFactory<TChannel> factory, MaskingMode maskingMode, TolerateFaultsMode faultMode, ChannelParameterCollection channelParameters, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout)
		{
			Type typeFromHandle = typeof(TChannel);
			if (typeFromHandle == typeof(IDuplexChannel))
			{
				return new ClientReliableChannelBinder<TChannel>.DuplexClientReliableChannelBinder(to, via, (IChannelFactory<IDuplexChannel>)factory, maskingMode, channelParameters, defaultCloseTimeout, defaultSendTimeout);
			}
			if (typeFromHandle == typeof(IDuplexSessionChannel))
			{
				return new ClientReliableChannelBinder<TChannel>.DuplexSessionClientReliableChannelBinder(to, via, (IChannelFactory<IDuplexSessionChannel>)factory, maskingMode, faultMode, channelParameters, defaultCloseTimeout, defaultSendTimeout);
			}
			if (typeFromHandle == typeof(IRequestChannel))
			{
				return new ClientReliableChannelBinder<TChannel>.RequestClientReliableChannelBinder(to, via, (IChannelFactory<IRequestChannel>)factory, maskingMode, channelParameters, defaultCloseTimeout, defaultSendTimeout);
			}
			if (typeFromHandle == typeof(IRequestSessionChannel))
			{
				return new ClientReliableChannelBinder<TChannel>.RequestSessionClientReliableChannelBinder(to, via, (IChannelFactory<IRequestSessionChannel>)factory, maskingMode, faultMode, channelParameters, defaultCloseTimeout, defaultSendTimeout);
			}
			throw Fx.AssertAndThrow("ClientReliableChannelBinder supports creation of IDuplexChannel, IDuplexSessionChannel, IRequestChannel, and IRequestSessionChannel only.");
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x0014425E File Offset: 0x0014245E
		public Message EndRequest(IAsyncResult result)
		{
			return ClientReliableChannelBinder<TChannel>.RequestAsyncResult.End(result);
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x00144268 File Offset: 0x00142468
		protected override bool EndTryGetChannel(IAsyncResult result)
		{
			TChannel tchannel = CompletedAsyncResult<TChannel>.End(result);
			if (tchannel != null && !base.Synchronizer.SetChannel(tchannel))
			{
				tchannel.Abort();
			}
			return true;
		}

		// Token: 0x0600583B RID: 22587 RVA: 0x0014429E File Offset: 0x0014249E
		public bool EnsureChannelForRequest()
		{
			return base.Synchronizer.EnsureChannel();
		}

		// Token: 0x0600583C RID: 22588 RVA: 0x001442AB File Offset: 0x001424AB
		protected override void OnAbort()
		{
		}

		// Token: 0x0600583D RID: 22589 RVA: 0x001442AD File Offset: 0x001424AD
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x0600583E RID: 22590 RVA: 0x001442B6 File Offset: 0x001424B6
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x0600583F RID: 22591 RVA: 0x001442BF File Offset: 0x001424BF
		protected virtual IAsyncResult OnBeginRequest(TChannel channel, Message message, TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state)
		{
			throw Fx.AssertAndThrow("The derived class does not support the OnBeginRequest operation.");
		}

		// Token: 0x06005840 RID: 22592 RVA: 0x001442CB File Offset: 0x001424CB
		protected override void OnClose(TimeSpan timeout)
		{
		}

		// Token: 0x06005841 RID: 22593 RVA: 0x001442CD File Offset: 0x001424CD
		protected override void OnEndClose(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x001442D5 File Offset: 0x001424D5
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06005843 RID: 22595 RVA: 0x001442DD File Offset: 0x001424DD
		protected virtual Message OnEndRequest(TChannel channel, MaskingMode maskingMode, IAsyncResult result)
		{
			throw Fx.AssertAndThrow("The derived class does not support the OnEndRequest operation.");
		}

		// Token: 0x06005844 RID: 22596 RVA: 0x001442E9 File Offset: 0x001424E9
		protected override void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x06005845 RID: 22597 RVA: 0x001442EB File Offset: 0x001424EB
		protected virtual Message OnRequest(TChannel channel, Message message, TimeSpan timeout, MaskingMode maskingMode)
		{
			throw Fx.AssertAndThrow("The derived class does not support the OnRequest operation.");
		}

		// Token: 0x06005846 RID: 22598 RVA: 0x001442F7 File Offset: 0x001424F7
		public Message Request(Message message, TimeSpan timeout)
		{
			return this.Request(message, timeout, base.DefaultMaskingMode);
		}

		// Token: 0x06005847 RID: 22599 RVA: 0x00144308 File Offset: 0x00142508
		public Message Request(Message message, TimeSpan timeout, MaskingMode maskingMode)
		{
			if (!base.ValidateOutputOperation(message, timeout, maskingMode))
			{
				return null;
			}
			bool autoAborted = false;
			Message result;
			try
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				TChannel tchannel;
				if (!base.Synchronizer.TryGetChannelForOutput(timeoutHelper.RemainingTime(), maskingMode, out tchannel))
				{
					if (!ReliableChannelBinderHelper.MaskHandled(maskingMode))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("TimeoutOnRequest", new object[]
						{
							timeout
						})));
					}
					result = null;
				}
				else if (tchannel == null)
				{
					result = null;
				}
				else
				{
					try
					{
						result = this.OnRequest(tchannel, message, timeoutHelper.RemainingTime(), maskingMode);
					}
					finally
					{
						autoAborted = base.Synchronizer.Aborting;
						base.Synchronizer.ReturnChannel();
					}
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (!base.HandleException(ex, maskingMode, autoAborted))
				{
					throw;
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x001443EC File Offset: 0x001425EC
		protected override bool TryGetChannel(TimeSpan timeout)
		{
			CommunicationState state = base.State;
			TChannel tchannel = default(TChannel);
			if (state == CommunicationState.Created || state == CommunicationState.Opening || state == CommunicationState.Opened)
			{
				tchannel = this.factory.CreateChannel(this.to, this.via);
				if (!base.Synchronizer.SetChannel(tchannel))
				{
					tchannel.Abort();
				}
			}
			else
			{
				tchannel = default(TChannel);
			}
			return true;
		}

		// Token: 0x0400361C RID: 13852
		private ChannelParameterCollection channelParameters;

		// Token: 0x0400361D RID: 13853
		private IChannelFactory<TChannel> factory;

		// Token: 0x0400361E RID: 13854
		private EndpointAddress to;

		// Token: 0x0400361F RID: 13855
		private Uri via;

		// Token: 0x02000DB0 RID: 3504
		private abstract class DuplexClientReliableChannelBinder<TDuplexChannel> : ClientReliableChannelBinder<TDuplexChannel> where TDuplexChannel : class, IDuplexChannel
		{
			// Token: 0x06007F2F RID: 32559 RVA: 0x001D9308 File Offset: 0x001D7508
			public DuplexClientReliableChannelBinder(EndpointAddress to, Uri via, IChannelFactory<TDuplexChannel> factory, MaskingMode maskingMode, TolerateFaultsMode faultMode, ChannelParameterCollection channelParameters, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(to, via, factory, maskingMode, faultMode, channelParameters, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x17001C57 RID: 7255
			// (get) Token: 0x06007F30 RID: 32560 RVA: 0x001D9328 File Offset: 0x001D7528
			public override EndpointAddress LocalAddress
			{
				get
				{
					IDuplexChannel duplexChannel = base.Synchronizer.CurrentChannel;
					if (duplexChannel == null)
					{
						return null;
					}
					return duplexChannel.LocalAddress;
				}
			}

			// Token: 0x17001C58 RID: 7256
			// (get) Token: 0x06007F31 RID: 32561 RVA: 0x001D9354 File Offset: 0x001D7554
			public override EndpointAddress RemoteAddress
			{
				get
				{
					IDuplexChannel duplexChannel = base.Synchronizer.CurrentChannel;
					if (duplexChannel == null)
					{
						return null;
					}
					return duplexChannel.RemoteAddress;
				}
			}

			// Token: 0x06007F32 RID: 32562 RVA: 0x001D937D File Offset: 0x001D757D
			protected override IAsyncResult OnBeginSend(TDuplexChannel channel, Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return channel.BeginSend(message, timeout, callback, state);
			}

			// Token: 0x06007F33 RID: 32563 RVA: 0x001D9390 File Offset: 0x001D7590
			protected override IAsyncResult OnBeginTryReceive(TDuplexChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return channel.BeginTryReceive(timeout, callback, state);
			}

			// Token: 0x06007F34 RID: 32564 RVA: 0x001D93A1 File Offset: 0x001D75A1
			protected override void OnEndSend(TDuplexChannel channel, IAsyncResult result)
			{
				channel.EndSend(result);
			}

			// Token: 0x06007F35 RID: 32565 RVA: 0x001D93B0 File Offset: 0x001D75B0
			protected override bool OnEndTryReceive(TDuplexChannel channel, IAsyncResult result, out RequestContext requestContext)
			{
				Message message;
				bool flag = channel.EndTryReceive(result, out message);
				if (flag && message == null)
				{
					this.OnReadNullMessage();
				}
				requestContext = base.WrapMessage(message);
				return flag;
			}

			// Token: 0x06007F36 RID: 32566 RVA: 0x001D93E2 File Offset: 0x001D75E2
			protected virtual void OnReadNullMessage()
			{
			}

			// Token: 0x06007F37 RID: 32567 RVA: 0x001D93E4 File Offset: 0x001D75E4
			protected override void OnSend(TDuplexChannel channel, Message message, TimeSpan timeout)
			{
				channel.Send(message, timeout);
			}

			// Token: 0x06007F38 RID: 32568 RVA: 0x001D93F4 File Offset: 0x001D75F4
			protected override bool OnTryReceive(TDuplexChannel channel, TimeSpan timeout, out RequestContext requestContext)
			{
				Message message;
				bool flag = channel.TryReceive(timeout, out message);
				if (flag && message == null)
				{
					this.OnReadNullMessage();
				}
				requestContext = base.WrapMessage(message);
				return flag;
			}
		}

		// Token: 0x02000DB1 RID: 3505
		private sealed class DuplexClientReliableChannelBinder : ClientReliableChannelBinder<TChannel>.DuplexClientReliableChannelBinder<IDuplexChannel>
		{
			// Token: 0x06007F39 RID: 32569 RVA: 0x001D9428 File Offset: 0x001D7628
			public DuplexClientReliableChannelBinder(EndpointAddress to, Uri via, IChannelFactory<IDuplexChannel> factory, MaskingMode maskingMode, ChannelParameterCollection channelParameters, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(to, via, factory, maskingMode, TolerateFaultsMode.Never, channelParameters, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x17001C59 RID: 7257
			// (get) Token: 0x06007F3A RID: 32570 RVA: 0x001D9447 File Offset: 0x001D7647
			public override bool HasSession
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06007F3B RID: 32571 RVA: 0x001D944A File Offset: 0x001D764A
			public override ISession GetInnerSession()
			{
				return null;
			}

			// Token: 0x06007F3C RID: 32572 RVA: 0x001D944D File Offset: 0x001D764D
			protected override bool HasSecuritySession(IDuplexChannel channel)
			{
				return false;
			}
		}

		// Token: 0x02000DB2 RID: 3506
		private sealed class DuplexSessionClientReliableChannelBinder : ClientReliableChannelBinder<TChannel>.DuplexClientReliableChannelBinder<IDuplexSessionChannel>
		{
			// Token: 0x06007F3D RID: 32573 RVA: 0x001D9450 File Offset: 0x001D7650
			public DuplexSessionClientReliableChannelBinder(EndpointAddress to, Uri via, IChannelFactory<IDuplexSessionChannel> factory, MaskingMode maskingMode, TolerateFaultsMode faultMode, ChannelParameterCollection channelParameters, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(to, via, factory, maskingMode, faultMode, channelParameters, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x17001C5A RID: 7258
			// (get) Token: 0x06007F3E RID: 32574 RVA: 0x001D9470 File Offset: 0x001D7670
			public override bool HasSession
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06007F3F RID: 32575 RVA: 0x001D9473 File Offset: 0x001D7673
			public override ISession GetInnerSession()
			{
				return base.Synchronizer.CurrentChannel.Session;
			}

			// Token: 0x06007F40 RID: 32576 RVA: 0x001D9485 File Offset: 0x001D7685
			protected override IAsyncResult BeginCloseChannel(IDuplexSessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return ReliableChannelBinderHelper.BeginCloseDuplexSessionChannel(this, channel, timeout, callback, state);
			}

			// Token: 0x06007F41 RID: 32577 RVA: 0x001D9492 File Offset: 0x001D7692
			protected override void CloseChannel(IDuplexSessionChannel channel, TimeSpan timeout)
			{
				ReliableChannelBinderHelper.CloseDuplexSessionChannel(this, channel, timeout);
			}

			// Token: 0x06007F42 RID: 32578 RVA: 0x001D949C File Offset: 0x001D769C
			protected override void EndCloseChannel(IDuplexSessionChannel channel, IAsyncResult result)
			{
				ReliableChannelBinderHelper.EndCloseDuplexSessionChannel(channel, result);
			}

			// Token: 0x06007F43 RID: 32579 RVA: 0x001D94A5 File Offset: 0x001D76A5
			protected override bool HasSecuritySession(IDuplexSessionChannel channel)
			{
				return channel.Session is ISecuritySession;
			}

			// Token: 0x06007F44 RID: 32580 RVA: 0x001D94B5 File Offset: 0x001D76B5
			protected override void OnReadNullMessage()
			{
				base.Synchronizer.OnReadEof();
			}
		}

		// Token: 0x02000DB3 RID: 3507
		private abstract class RequestClientReliableChannelBinder<TRequestChannel> : ClientReliableChannelBinder<TRequestChannel> where TRequestChannel : class, IRequestChannel
		{
			// Token: 0x06007F45 RID: 32581 RVA: 0x001D94C4 File Offset: 0x001D76C4
			public RequestClientReliableChannelBinder(EndpointAddress to, Uri via, IChannelFactory<TRequestChannel> factory, MaskingMode maskingMode, TolerateFaultsMode faultMode, ChannelParameterCollection channelParameters, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(to, via, factory, maskingMode, faultMode, channelParameters, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x06007F46 RID: 32582 RVA: 0x001D94E4 File Offset: 0x001D76E4
			public override IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.GetInputMessages().BeginDequeue(timeout, callback, state);
			}

			// Token: 0x06007F47 RID: 32583 RVA: 0x001D94F4 File Offset: 0x001D76F4
			public override bool EndTryReceive(IAsyncResult result, out RequestContext requestContext)
			{
				Message message;
				bool result2 = this.GetInputMessages().EndDequeue(result, out message);
				requestContext = base.WrapMessage(message);
				return result2;
			}

			// Token: 0x06007F48 RID: 32584 RVA: 0x001D951A File Offset: 0x001D771A
			protected void EnqueueMessageIfNotNull(Message message)
			{
				if (message != null)
				{
					this.GetInputMessages().EnqueueAndDispatch(message);
				}
			}

			// Token: 0x06007F49 RID: 32585 RVA: 0x001D952C File Offset: 0x001D772C
			private InputQueue<Message> GetInputMessages()
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (base.State == CommunicationState.Created)
					{
						throw Fx.AssertAndThrow("The method GetInputMessages() cannot be called when the binder is in the Created state.");
					}
					if (base.State == CommunicationState.Opening)
					{
						throw Fx.AssertAndThrow("The method GetInputMessages() cannot be called when the binder is in the Opening state.");
					}
					if (this.inputMessages == null)
					{
						this.inputMessages = TraceUtility.CreateInputQueue<Message>();
					}
				}
				return this.inputMessages;
			}

			// Token: 0x17001C5B RID: 7259
			// (get) Token: 0x06007F4A RID: 32586 RVA: 0x001D95A8 File Offset: 0x001D77A8
			public override EndpointAddress LocalAddress
			{
				get
				{
					return EndpointAddress.AnonymousAddress;
				}
			}

			// Token: 0x17001C5C RID: 7260
			// (get) Token: 0x06007F4B RID: 32587 RVA: 0x001D95B0 File Offset: 0x001D77B0
			public override EndpointAddress RemoteAddress
			{
				get
				{
					IRequestChannel requestChannel = base.Synchronizer.CurrentChannel;
					if (requestChannel == null)
					{
						return null;
					}
					return requestChannel.RemoteAddress;
				}
			}

			// Token: 0x06007F4C RID: 32588 RVA: 0x001D95D9 File Offset: 0x001D77D9
			protected override IAsyncResult OnBeginRequest(TRequestChannel channel, Message message, TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state)
			{
				return channel.BeginRequest(message, timeout, callback, state);
			}

			// Token: 0x06007F4D RID: 32589 RVA: 0x001D95EC File Offset: 0x001D77EC
			protected override IAsyncResult OnBeginSend(TRequestChannel channel, Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return channel.BeginRequest(message, timeout, callback, state);
			}

			// Token: 0x06007F4E RID: 32590 RVA: 0x001D95FF File Offset: 0x001D77FF
			protected override Message OnEndRequest(TRequestChannel channel, MaskingMode maskingMode, IAsyncResult result)
			{
				return channel.EndRequest(result);
			}

			// Token: 0x06007F4F RID: 32591 RVA: 0x001D9610 File Offset: 0x001D7810
			protected override void OnEndSend(TRequestChannel channel, IAsyncResult result)
			{
				Message message = channel.EndRequest(result);
				this.EnqueueMessageIfNotNull(message);
			}

			// Token: 0x06007F50 RID: 32592 RVA: 0x001D9631 File Offset: 0x001D7831
			protected override Message OnRequest(TRequestChannel channel, Message message, TimeSpan timeout, MaskingMode maskingMode)
			{
				return channel.Request(message, timeout);
			}

			// Token: 0x06007F51 RID: 32593 RVA: 0x001D9640 File Offset: 0x001D7840
			protected override void OnSend(TRequestChannel channel, Message message, TimeSpan timeout)
			{
				message = channel.Request(message, timeout);
				this.EnqueueMessageIfNotNull(message);
			}

			// Token: 0x06007F52 RID: 32594 RVA: 0x001D9658 File Offset: 0x001D7858
			protected override void OnShutdown()
			{
				if (this.inputMessages != null)
				{
					this.inputMessages.Close();
				}
			}

			// Token: 0x06007F53 RID: 32595 RVA: 0x001D9670 File Offset: 0x001D7870
			public override bool TryReceive(TimeSpan timeout, out RequestContext requestContext)
			{
				Message message;
				bool result = this.GetInputMessages().Dequeue(timeout, out message);
				requestContext = base.WrapMessage(message);
				return result;
			}

			// Token: 0x040048E2 RID: 18658
			private InputQueue<Message> inputMessages;
		}

		// Token: 0x02000DB4 RID: 3508
		private sealed class RequestAsyncResult : ReliableChannelBinder<TChannel>.OutputAsyncResult<ClientReliableChannelBinder<TChannel>>
		{
			// Token: 0x06007F54 RID: 32596 RVA: 0x001D9696 File Offset: 0x001D7896
			public RequestAsyncResult(ClientReliableChannelBinder<TChannel> binder, AsyncCallback callback, object state) : base(binder, callback, state)
			{
			}

			// Token: 0x06007F55 RID: 32597 RVA: 0x001D96A1 File Offset: 0x001D78A1
			protected override IAsyncResult BeginOutput(ClientReliableChannelBinder<TChannel> binder, TChannel channel, Message message, TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state)
			{
				return binder.OnBeginRequest(channel, message, timeout, maskingMode, callback, state);
			}

			// Token: 0x06007F56 RID: 32598 RVA: 0x001D96B4 File Offset: 0x001D78B4
			public static Message End(IAsyncResult result)
			{
				ClientReliableChannelBinder<TChannel>.RequestAsyncResult requestAsyncResult = AsyncResult.End<ClientReliableChannelBinder<TChannel>.RequestAsyncResult>(result);
				return requestAsyncResult.reply;
			}

			// Token: 0x06007F57 RID: 32599 RVA: 0x001D96CE File Offset: 0x001D78CE
			protected override void EndOutput(ClientReliableChannelBinder<TChannel> binder, TChannel channel, MaskingMode maskingMode, IAsyncResult result)
			{
				this.reply = binder.OnEndRequest(channel, maskingMode, result);
			}

			// Token: 0x06007F58 RID: 32600 RVA: 0x001D96E0 File Offset: 0x001D78E0
			protected override string GetTimeoutString(TimeSpan timeout)
			{
				return SR.GetString("TimeoutOnRequest", new object[]
				{
					timeout
				});
			}

			// Token: 0x040048E3 RID: 18659
			private Message reply;
		}

		// Token: 0x02000DB5 RID: 3509
		private sealed class RequestClientReliableChannelBinder : ClientReliableChannelBinder<TChannel>.RequestClientReliableChannelBinder<IRequestChannel>
		{
			// Token: 0x06007F59 RID: 32601 RVA: 0x001D96FC File Offset: 0x001D78FC
			public RequestClientReliableChannelBinder(EndpointAddress to, Uri via, IChannelFactory<IRequestChannel> factory, MaskingMode maskingMode, ChannelParameterCollection channelParameters, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(to, via, factory, maskingMode, TolerateFaultsMode.Never, channelParameters, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x17001C5D RID: 7261
			// (get) Token: 0x06007F5A RID: 32602 RVA: 0x001D971B File Offset: 0x001D791B
			public override bool HasSession
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06007F5B RID: 32603 RVA: 0x001D971E File Offset: 0x001D791E
			public override ISession GetInnerSession()
			{
				return null;
			}

			// Token: 0x06007F5C RID: 32604 RVA: 0x001D9721 File Offset: 0x001D7921
			protected override bool HasSecuritySession(IRequestChannel channel)
			{
				return false;
			}
		}

		// Token: 0x02000DB6 RID: 3510
		private sealed class RequestSessionClientReliableChannelBinder : ClientReliableChannelBinder<TChannel>.RequestClientReliableChannelBinder<IRequestSessionChannel>
		{
			// Token: 0x06007F5D RID: 32605 RVA: 0x001D9724 File Offset: 0x001D7924
			public RequestSessionClientReliableChannelBinder(EndpointAddress to, Uri via, IChannelFactory<IRequestSessionChannel> factory, MaskingMode maskingMode, TolerateFaultsMode faultMode, ChannelParameterCollection channelParameters, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(to, via, factory, maskingMode, faultMode, channelParameters, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x17001C5E RID: 7262
			// (get) Token: 0x06007F5E RID: 32606 RVA: 0x001D9744 File Offset: 0x001D7944
			public override bool HasSession
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06007F5F RID: 32607 RVA: 0x001D9747 File Offset: 0x001D7947
			public override ISession GetInnerSession()
			{
				return base.Synchronizer.CurrentChannel.Session;
			}

			// Token: 0x06007F60 RID: 32608 RVA: 0x001D9759 File Offset: 0x001D7959
			protected override bool HasSecuritySession(IRequestSessionChannel channel)
			{
				return channel.Session is ISecuritySession;
			}
		}
	}
}
