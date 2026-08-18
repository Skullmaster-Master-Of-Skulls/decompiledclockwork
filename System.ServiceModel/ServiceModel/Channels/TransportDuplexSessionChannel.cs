using System;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000883 RID: 2179
	internal abstract class TransportDuplexSessionChannel : TransportOutputChannel, IDuplexSessionChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel, ISessionChannel<IDuplexSession>
	{
		// Token: 0x06005298 RID: 21144 RVA: 0x00130B90 File Offset: 0x0012ED90
		protected TransportDuplexSessionChannel(ChannelManagerBase manager, ITransportFactorySettings settings, EndpointAddress localAddress, Uri localVia, EndpointAddress remoteAddresss, Uri via) : base(manager, remoteAddresss, via, settings.ManualAddressing, settings.MessageVersion)
		{
			this.localAddress = localAddress;
			this.localVia = localVia;
			this.bufferManager = settings.BufferManager;
			this.sendLock = new ThreadNeutralSemaphore(1);
			this.messageEncoder = settings.MessageEncoderFactory.CreateSessionEncoder();
			this.Session = new TransportDuplexSessionChannel.ConnectionDuplexSession(this);
		}

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x06005299 RID: 21145 RVA: 0x00130BF8 File Offset: 0x0012EDF8
		public EndpointAddress LocalAddress
		{
			get
			{
				return this.localAddress;
			}
		}

		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x0600529A RID: 21146 RVA: 0x00130C00 File Offset: 0x0012EE00
		// (set) Token: 0x0600529B RID: 21147 RVA: 0x00130C08 File Offset: 0x0012EE08
		public SecurityMessageProperty RemoteSecurity
		{
			get
			{
				return this.remoteSecurity;
			}
			protected set
			{
				this.remoteSecurity = value;
			}
		}

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x0600529C RID: 21148 RVA: 0x00130C11 File Offset: 0x0012EE11
		// (set) Token: 0x0600529D RID: 21149 RVA: 0x00130C19 File Offset: 0x0012EE19
		public IDuplexSession Session
		{
			get
			{
				return this.duplexSession;
			}
			protected set
			{
				this.duplexSession = value;
			}
		}

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x0600529E RID: 21150 RVA: 0x00130C22 File Offset: 0x0012EE22
		public ThreadNeutralSemaphore SendLock
		{
			get
			{
				return this.sendLock;
			}
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x0600529F RID: 21151 RVA: 0x00130C2A File Offset: 0x0012EE2A
		protected ChannelBinding ChannelBinding
		{
			get
			{
				return this.channelBindingToken;
			}
		}

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x060052A0 RID: 21152 RVA: 0x00130C32 File Offset: 0x0012EE32
		protected BufferManager BufferManager
		{
			get
			{
				return this.bufferManager;
			}
		}

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x060052A1 RID: 21153 RVA: 0x00130C3A File Offset: 0x0012EE3A
		protected Uri LocalVia
		{
			get
			{
				return this.localVia;
			}
		}

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x060052A2 RID: 21154 RVA: 0x00130C42 File Offset: 0x0012EE42
		// (set) Token: 0x060052A3 RID: 21155 RVA: 0x00130C4A File Offset: 0x0012EE4A
		protected MessageEncoder MessageEncoder
		{
			get
			{
				return this.messageEncoder;
			}
			set
			{
				this.messageEncoder = value;
			}
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x060052A4 RID: 21156 RVA: 0x00130C53 File Offset: 0x0012EE53
		protected SynchronizedMessageSource MessageSource
		{
			get
			{
				return this.messageSource;
			}
		}

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x060052A5 RID: 21157
		protected abstract bool IsStreamedOutput { get; }

		// Token: 0x060052A6 RID: 21158 RVA: 0x00130C5B File Offset: 0x0012EE5B
		public Message Receive()
		{
			return this.Receive(base.DefaultReceiveTimeout);
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x00130C6C File Offset: 0x0012EE6C
		public Message Receive(TimeSpan timeout)
		{
			Message message = null;
			if (base.DoneReceivingInCurrentState())
			{
				return null;
			}
			bool flag = true;
			Message result;
			try
			{
				message = this.messageSource.Receive(timeout);
				this.OnReceiveMessage(message);
				flag = false;
				result = message;
			}
			finally
			{
				if (flag)
				{
					if (message != null)
					{
						message.Close();
						message = null;
					}
					base.Fault();
				}
			}
			return result;
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x00130CC8 File Offset: 0x0012EEC8
		public IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			return this.BeginReceive(base.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x00130CD8 File Offset: 0x0012EED8
		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (base.DoneReceivingInCurrentState())
			{
				return new DoneReceivingAsyncResult(callback, state);
			}
			bool flag = true;
			IAsyncResult result;
			try
			{
				IAsyncResult asyncResult = this.messageSource.BeginReceive(timeout, callback, state);
				flag = false;
				result = asyncResult;
			}
			finally
			{
				if (flag)
				{
					base.Fault();
				}
			}
			return result;
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x00130D28 File Offset: 0x0012EF28
		public Message EndReceive(IAsyncResult result)
		{
			base.ThrowIfNotOpened();
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			DoneReceivingAsyncResult doneReceivingAsyncResult = result as DoneReceivingAsyncResult;
			if (doneReceivingAsyncResult != null)
			{
				DoneReceivingAsyncResult.End(doneReceivingAsyncResult);
				return null;
			}
			bool flag = true;
			Message message = null;
			Message result2;
			try
			{
				message = this.messageSource.EndReceive(result);
				this.OnReceiveMessage(message);
				flag = false;
				result2 = message;
			}
			finally
			{
				if (flag)
				{
					if (message != null)
					{
						message.Close();
						message = null;
					}
					base.Fault();
				}
			}
			return result2;
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x00130DA8 File Offset: 0x0012EFA8
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new TransportDuplexSessionChannel.TryReceiveAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x00130DB3 File Offset: 0x0012EFB3
		public bool EndTryReceive(IAsyncResult result, out Message message)
		{
			return TransportDuplexSessionChannel.TryReceiveAsyncResult.End(result, out message);
		}

		// Token: 0x060052AD RID: 21165 RVA: 0x00130DBC File Offset: 0x0012EFBC
		public bool TryReceive(TimeSpan timeout, out Message message)
		{
			bool result;
			try
			{
				message = this.Receive(timeout);
				result = true;
			}
			catch (TimeoutException ex)
			{
				if (TD.ReceiveTimeoutIsEnabled())
				{
					TD.ReceiveTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				message = null;
				result = false;
			}
			return result;
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x00130E08 File Offset: 0x0012F008
		public bool WaitForMessage(TimeSpan timeout)
		{
			if (base.DoneReceivingInCurrentState())
			{
				return true;
			}
			bool flag = true;
			bool result;
			try
			{
				bool flag2 = this.messageSource.WaitForMessage(timeout);
				flag = !flag2;
				result = flag2;
			}
			finally
			{
				if (flag)
				{
					base.Fault();
				}
			}
			return result;
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x00130E54 File Offset: 0x0012F054
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (base.DoneReceivingInCurrentState())
			{
				return new DoneReceivingAsyncResult(callback, state);
			}
			bool flag = true;
			IAsyncResult result;
			try
			{
				IAsyncResult asyncResult = this.messageSource.BeginWaitForMessage(timeout, callback, state);
				flag = false;
				result = asyncResult;
			}
			finally
			{
				if (flag)
				{
					base.Fault();
				}
			}
			return result;
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x00130EA4 File Offset: 0x0012F0A4
		public bool EndWaitForMessage(IAsyncResult result)
		{
			base.ThrowIfNotOpened();
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			DoneReceivingAsyncResult doneReceivingAsyncResult = result as DoneReceivingAsyncResult;
			if (doneReceivingAsyncResult != null)
			{
				return DoneReceivingAsyncResult.End(doneReceivingAsyncResult);
			}
			bool flag = true;
			bool result2;
			try
			{
				bool flag2 = this.messageSource.EndWaitForMessage(result);
				flag = !flag2;
				result2 = flag2;
			}
			finally
			{
				if (flag)
				{
					base.Fault();
				}
			}
			return result2;
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x00130F10 File Offset: 0x0012F110
		protected void SetChannelBinding(ChannelBinding channelBinding)
		{
			this.channelBindingToken = channelBinding;
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x00130F19 File Offset: 0x0012F119
		protected void SetMessageSource(IMessageSource messageSource)
		{
			this.messageSource = new SynchronizedMessageSource(messageSource);
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x00130F27 File Offset: 0x0012F127
		protected IAsyncResult BeginCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new TransportDuplexSessionChannel.CloseOutputSessionAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x00130F32 File Offset: 0x0012F132
		protected void EndCloseOutputSession(IAsyncResult result)
		{
			TransportDuplexSessionChannel.CloseOutputSessionAsyncResult.End(result);
		}

		// Token: 0x060052B5 RID: 21173
		protected abstract void CloseOutputSessionCore(TimeSpan timeout);

		// Token: 0x060052B6 RID: 21174 RVA: 0x00130F3C File Offset: 0x0012F13C
		protected void CloseOutputSession(TimeSpan timeout)
		{
			base.ThrowIfNotOpened();
			base.ThrowIfFaulted();
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (!this.sendLock.TryEnter(timeoutHelper.RemainingTime()))
			{
				if (TD.CloseTimeoutIsEnabled())
				{
					TD.CloseTimeout(SR.GetString("CloseTimedOut", new object[]
					{
						timeout
					}));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("CloseTimedOut", new object[]
				{
					timeout
				}), ThreadNeutralSemaphore.CreateEnterTimedOutException(timeout)));
			}
			try
			{
				base.ThrowIfFaulted();
				if (!this.isOutputSessionClosed)
				{
					this.isOutputSessionClosed = true;
					bool flag = true;
					try
					{
						this.CloseOutputSessionCore(timeout);
						this.OnOutputSessionClosed(ref timeoutHelper);
						flag = false;
					}
					finally
					{
						if (flag)
						{
							base.Fault();
						}
					}
				}
			}
			finally
			{
				this.sendLock.Exit();
			}
		}

		// Token: 0x060052B7 RID: 21175
		protected abstract void ReturnConnectionIfNecessary(bool abort, TimeSpan timeout);

		// Token: 0x060052B8 RID: 21176 RVA: 0x00131028 File Offset: 0x0012F228
		protected override void OnAbort()
		{
			this.ReturnConnectionIfNecessary(true, TimeSpan.Zero);
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x00131036 File Offset: 0x0012F236
		protected override void OnFaulted()
		{
			base.OnFaulted();
			this.ReturnConnectionIfNecessary(true, TimeSpan.Zero);
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x0013104C File Offset: 0x0012F24C
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.CloseOutputSession(timeoutHelper.RemainingTime());
			if (!this.isInputSessionClosed)
			{
				this.EnsureInputClosed(timeoutHelper.RemainingTime());
				this.OnInputSessionClosed();
			}
			this.CompleteClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x00131096 File Offset: 0x0012F296
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new TransportDuplexSessionChannel.CloseAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x001310A1 File Offset: 0x0012F2A1
		protected override void OnEndClose(IAsyncResult result)
		{
			TransportDuplexSessionChannel.CloseAsyncResult.End(result);
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x001310A9 File Offset: 0x0012F2A9
		protected override void OnClosed()
		{
			base.OnClosed();
			ChannelBindingUtility.Dispose(ref this.channelBindingToken);
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x001310BC File Offset: 0x0012F2BC
		protected virtual void OnReceiveMessage(Message message)
		{
			if (message == null)
			{
				this.OnInputSessionClosed();
				return;
			}
			this.PrepareMessage(message);
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x001310CF File Offset: 0x0012F2CF
		protected void ApplyChannelBinding(Message message)
		{
			ChannelBindingUtility.TryAddToMessage(this.channelBindingToken, message, false);
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x001310E0 File Offset: 0x0012F2E0
		protected virtual void PrepareMessage(Message message)
		{
			message.Properties.Via = this.localVia;
			this.ApplyChannelBinding(message);
			if (FxTrace.Trace.IsEnd2EndActivityTracingEnabled)
			{
				EventTraceActivity eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
				Guid activityIdFromThread = EventTraceActivity.GetActivityIdFromThread();
				if (eventTraceActivity == null)
				{
					eventTraceActivity = EventTraceActivity.GetFromThreadOrCreate(false);
					EventTraceActivityHelper.TryAttachActivity(message, eventTraceActivity);
				}
				if (TD.MessageReceivedByTransportIsEnabled())
				{
					TD.MessageReceivedByTransport(eventTraceActivity, (this.LocalAddress != null && this.LocalAddress.Uri != null) ? this.LocalAddress.Uri.AbsoluteUri : string.Empty, activityIdFromThread);
				}
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262163, SR.GetString("TraceCodeMessageReceived"), MessageTransmitTraceRecord.CreateReceiveTraceRecord(message, this.LocalAddress), this, null, message);
			}
		}

		// Token: 0x060052C1 RID: 21185
		protected abstract AsyncCompletionResult StartWritingBufferedMessage(Message message, ArraySegment<byte> messageData, bool allowOutputBatching, TimeSpan timeout, WaitCallback callback, object state);

		// Token: 0x060052C2 RID: 21186
		protected abstract AsyncCompletionResult BeginCloseOutput(TimeSpan timeout, WaitCallback callback, object state);

		// Token: 0x060052C3 RID: 21187 RVA: 0x001311A1 File Offset: 0x0012F3A1
		protected virtual void FinishWritingMessage()
		{
		}

		// Token: 0x060052C4 RID: 21188
		protected abstract ArraySegment<byte> EncodeMessage(Message message);

		// Token: 0x060052C5 RID: 21189
		protected abstract void OnSendCore(Message message, TimeSpan timeout);

		// Token: 0x060052C6 RID: 21190
		protected abstract AsyncCompletionResult StartWritingStreamedMessage(Message message, TimeSpan timeout, WaitCallback callback, object state);

		// Token: 0x060052C7 RID: 21191 RVA: 0x001311A4 File Offset: 0x0012F3A4
		protected override void OnSend(Message message, TimeSpan timeout)
		{
			base.ThrowIfDisposedOrNotOpen();
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (!this.sendLock.TryEnter(timeoutHelper.RemainingTime()))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("SendToViaTimedOut", new object[]
				{
					this.Via,
					timeout
				}), ThreadNeutralSemaphore.CreateEnterTimedOutException(timeout)));
			}
			try
			{
				base.ThrowIfDisposedOrNotOpen();
				this.ThrowIfOutputSessionClosed();
				bool flag = false;
				try
				{
					this.ApplyChannelBinding(message);
					this.OnSendCore(message, timeoutHelper.RemainingTime());
					flag = true;
					if (TD.MessageSentByTransportIsEnabled())
					{
						EventTraceActivity eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
						TD.MessageSentByTransport(eventTraceActivity, this.RemoteAddress.Uri.AbsoluteUri);
					}
				}
				finally
				{
					if (!flag)
					{
						base.Fault();
					}
				}
			}
			finally
			{
				this.sendLock.Exit();
			}
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x0013128C File Offset: 0x0012F48C
		protected override IAsyncResult OnBeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.ThrowIfDisposedOrNotOpen();
			return new TransportDuplexSessionChannel.SendAsyncResult(this, message, timeout, this.IsStreamedOutput, callback, state);
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x001312A5 File Offset: 0x0012F4A5
		protected override void OnEndSend(IAsyncResult result)
		{
			TransportDuplexSessionChannel.SendAsyncResult.End(result);
		}

		// Token: 0x060052CA RID: 21194
		protected abstract void CompleteClose(TimeSpan timeout);

		// Token: 0x060052CB RID: 21195 RVA: 0x001312AD File Offset: 0x0012F4AD
		private void ThrowIfOutputSessionClosed()
		{
			if (this.isOutputSessionClosed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SendCannotBeCalledAfterCloseOutputSession")));
			}
		}

		// Token: 0x060052CC RID: 21196 RVA: 0x001312D4 File Offset: 0x0012F4D4
		private void EnsureInputClosed(TimeSpan timeout)
		{
			Message message = this.MessageSource.Receive(timeout);
			if (message != null)
			{
				using (message)
				{
					ProtocolException exception = ProtocolException.ReceiveShutdownReturnedNonNull(message);
					throw TraceUtility.ThrowHelperError(exception, message);
				}
			}
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x0013131C File Offset: 0x0012F51C
		private void OnInputSessionClosed()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (!this.isInputSessionClosed)
				{
					this.isInputSessionClosed = true;
				}
			}
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x00131368 File Offset: 0x0012F568
		private void OnOutputSessionClosed(ref TimeoutHelper timeoutHelper)
		{
			bool flag = false;
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.isInputSessionClosed)
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.ReturnConnectionIfNecessary(false, timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x0400327F RID: 12927
		private BufferManager bufferManager;

		// Token: 0x04003280 RID: 12928
		private IDuplexSession duplexSession;

		// Token: 0x04003281 RID: 12929
		private bool isInputSessionClosed;

		// Token: 0x04003282 RID: 12930
		private bool isOutputSessionClosed;

		// Token: 0x04003283 RID: 12931
		private MessageEncoder messageEncoder;

		// Token: 0x04003284 RID: 12932
		private SynchronizedMessageSource messageSource;

		// Token: 0x04003285 RID: 12933
		private SecurityMessageProperty remoteSecurity;

		// Token: 0x04003286 RID: 12934
		private EndpointAddress localAddress;

		// Token: 0x04003287 RID: 12935
		private ThreadNeutralSemaphore sendLock;

		// Token: 0x04003288 RID: 12936
		private Uri localVia;

		// Token: 0x04003289 RID: 12937
		private ChannelBinding channelBindingToken;

		// Token: 0x02000D62 RID: 3426
		internal class ConnectionDuplexSession : IDuplexSession, IInputSession, ISession, IOutputSession
		{
			// Token: 0x06007D9D RID: 32157 RVA: 0x001D5AB0 File Offset: 0x001D3CB0
			public ConnectionDuplexSession(TransportDuplexSessionChannel channel)
			{
				this.channel = channel;
			}

			// Token: 0x17001C12 RID: 7186
			// (get) Token: 0x06007D9E RID: 32158 RVA: 0x001D5AC0 File Offset: 0x001D3CC0
			public string Id
			{
				get
				{
					if (this.id == null)
					{
						TransportDuplexSessionChannel obj = this.channel;
						lock (obj)
						{
							if (this.id == null)
							{
								this.id = TransportDuplexSessionChannel.ConnectionDuplexSession.UriGenerator.Next();
							}
						}
					}
					return this.id;
				}
			}

			// Token: 0x17001C13 RID: 7187
			// (get) Token: 0x06007D9F RID: 32159 RVA: 0x001D5B20 File Offset: 0x001D3D20
			public TransportDuplexSessionChannel Channel
			{
				get
				{
					return this.channel;
				}
			}

			// Token: 0x17001C14 RID: 7188
			// (get) Token: 0x06007DA0 RID: 32160 RVA: 0x001D5B28 File Offset: 0x001D3D28
			private static UriGenerator UriGenerator
			{
				get
				{
					if (TransportDuplexSessionChannel.ConnectionDuplexSession.uriGenerator == null)
					{
						TransportDuplexSessionChannel.ConnectionDuplexSession.uriGenerator = new UriGenerator();
					}
					return TransportDuplexSessionChannel.ConnectionDuplexSession.uriGenerator;
				}
			}

			// Token: 0x06007DA1 RID: 32161 RVA: 0x001D5B40 File Offset: 0x001D3D40
			public IAsyncResult BeginCloseOutputSession(AsyncCallback callback, object state)
			{
				return this.BeginCloseOutputSession(this.channel.DefaultCloseTimeout, callback, state);
			}

			// Token: 0x06007DA2 RID: 32162 RVA: 0x001D5B55 File Offset: 0x001D3D55
			public IAsyncResult BeginCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.channel.BeginCloseOutputSession(timeout, callback, state);
			}

			// Token: 0x06007DA3 RID: 32163 RVA: 0x001D5B65 File Offset: 0x001D3D65
			public void EndCloseOutputSession(IAsyncResult result)
			{
				this.channel.EndCloseOutputSession(result);
			}

			// Token: 0x06007DA4 RID: 32164 RVA: 0x001D5B73 File Offset: 0x001D3D73
			public void CloseOutputSession()
			{
				this.CloseOutputSession(this.channel.DefaultCloseTimeout);
			}

			// Token: 0x06007DA5 RID: 32165 RVA: 0x001D5B86 File Offset: 0x001D3D86
			public void CloseOutputSession(TimeSpan timeout)
			{
				this.channel.CloseOutputSession(timeout);
			}

			// Token: 0x0400482F RID: 18479
			private static UriGenerator uriGenerator;

			// Token: 0x04004830 RID: 18480
			private TransportDuplexSessionChannel channel;

			// Token: 0x04004831 RID: 18481
			private string id;
		}

		// Token: 0x02000D63 RID: 3427
		private class CloseAsyncResult : AsyncResult
		{
			// Token: 0x06007DA6 RID: 32166 RVA: 0x001D5B94 File Offset: 0x001D3D94
			public CloseAsyncResult(TransportDuplexSessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.channel = channel;
				this.timeoutHelper = new TimeoutHelper(timeout);
				IAsyncResult asyncResult = this.channel.BeginCloseOutputSession(this.timeoutHelper.RemainingTime(), TransportDuplexSessionChannel.CloseAsyncResult.onCloseOutputSession, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				if (!this.HandleCloseOutputSession(asyncResult, true))
				{
					return;
				}
				base.Complete(true);
			}

			// Token: 0x06007DA7 RID: 32167 RVA: 0x001D5BF5 File Offset: 0x001D3DF5
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<TransportDuplexSessionChannel.CloseAsyncResult>(result);
			}

			// Token: 0x06007DA8 RID: 32168 RVA: 0x001D5C00 File Offset: 0x001D3E00
			private static void OnCloseOutputSession(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				TransportDuplexSessionChannel.CloseAsyncResult closeAsyncResult = (TransportDuplexSessionChannel.CloseAsyncResult)result.AsyncState;
				bool flag = false;
				Exception exception = null;
				try
				{
					flag = closeAsyncResult.HandleCloseOutputSession(result, false);
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

			// Token: 0x06007DA9 RID: 32169 RVA: 0x001D5C60 File Offset: 0x001D3E60
			private static void OnCloseInputSession(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				TransportDuplexSessionChannel.CloseAsyncResult closeAsyncResult = (TransportDuplexSessionChannel.CloseAsyncResult)result.AsyncState;
				bool flag = false;
				Exception exception = null;
				try
				{
					flag = closeAsyncResult.HandleCloseInputSession(result, false);
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

			// Token: 0x06007DAA RID: 32170 RVA: 0x001D5CC0 File Offset: 0x001D3EC0
			private static void OnCompleteCloseScheduled(object state)
			{
				TransportDuplexSessionChannel.CloseAsyncResult closeAsyncResult = (TransportDuplexSessionChannel.CloseAsyncResult)state;
				Exception exception = null;
				try
				{
					closeAsyncResult.OnCompleteCloseScheduled();
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

			// Token: 0x06007DAB RID: 32171 RVA: 0x001D5D08 File Offset: 0x001D3F08
			private bool HandleCloseOutputSession(IAsyncResult result, bool isStillSynchronous)
			{
				this.channel.EndCloseOutputSession(result);
				if (this.channel.isInputSessionClosed)
				{
					return this.ScheduleCompleteClose(isStillSynchronous);
				}
				IAsyncResult asyncResult = this.channel.messageSource.BeginReceive(this.timeoutHelper.RemainingTime(), TransportDuplexSessionChannel.CloseAsyncResult.onCloseInputSession, this);
				return asyncResult.CompletedSynchronously && this.HandleCloseInputSession(asyncResult, isStillSynchronous);
			}

			// Token: 0x06007DAC RID: 32172 RVA: 0x001D5D6C File Offset: 0x001D3F6C
			private bool HandleCloseInputSession(IAsyncResult result, bool isStillSynchronous)
			{
				Message message = this.channel.messageSource.EndReceive(result);
				if (message != null)
				{
					using (message)
					{
						ProtocolException exception = ProtocolException.ReceiveShutdownReturnedNonNull(message);
						throw TraceUtility.ThrowHelperError(exception, message);
					}
				}
				this.channel.OnInputSessionClosed();
				return this.ScheduleCompleteClose(isStillSynchronous);
			}

			// Token: 0x06007DAD RID: 32173 RVA: 0x001D5DCC File Offset: 0x001D3FCC
			private bool ScheduleCompleteClose(bool isStillSynchronous)
			{
				if (isStillSynchronous)
				{
					if (TransportDuplexSessionChannel.CloseAsyncResult.onCompleteCloseScheduled == null)
					{
						TransportDuplexSessionChannel.CloseAsyncResult.onCompleteCloseScheduled = new Action<object>(TransportDuplexSessionChannel.CloseAsyncResult.OnCompleteCloseScheduled);
					}
					ActionItem.Schedule(TransportDuplexSessionChannel.CloseAsyncResult.onCompleteCloseScheduled, this);
					return false;
				}
				this.OnCompleteCloseScheduled();
				return true;
			}

			// Token: 0x06007DAE RID: 32174 RVA: 0x001D5DFD File Offset: 0x001D3FFD
			private void OnCompleteCloseScheduled()
			{
				this.channel.CompleteClose(this.timeoutHelper.RemainingTime());
			}

			// Token: 0x04004832 RID: 18482
			private static AsyncCallback onCloseOutputSession = Fx.ThunkCallback(new AsyncCallback(TransportDuplexSessionChannel.CloseAsyncResult.OnCloseOutputSession));

			// Token: 0x04004833 RID: 18483
			private static AsyncCallback onCloseInputSession = Fx.ThunkCallback(new AsyncCallback(TransportDuplexSessionChannel.CloseAsyncResult.OnCloseInputSession));

			// Token: 0x04004834 RID: 18484
			private static Action<object> onCompleteCloseScheduled;

			// Token: 0x04004835 RID: 18485
			private TransportDuplexSessionChannel channel;

			// Token: 0x04004836 RID: 18486
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x02000D64 RID: 3428
		private class CloseOutputSessionAsyncResult : AsyncResult
		{
			// Token: 0x06007DB0 RID: 32176 RVA: 0x001D5E44 File Offset: 0x001D4044
			public CloseOutputSessionAsyncResult(TransportDuplexSessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				channel.ThrowIfNotOpened();
				channel.ThrowIfFaulted();
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.channel = channel;
				if (!channel.sendLock.EnterAsync(this.timeoutHelper.RemainingTime(), TransportDuplexSessionChannel.CloseOutputSessionAsyncResult.onEnterComplete, this))
				{
					return;
				}
				bool flag = false;
				bool flag2 = false;
				try
				{
					flag = this.WriteEndBytes();
					flag2 = true;
				}
				finally
				{
					if (!flag2)
					{
						this.Cleanup(false, true);
					}
				}
				if (flag)
				{
					this.Cleanup(true, true);
					base.Complete(true);
				}
			}

			// Token: 0x06007DB1 RID: 32177 RVA: 0x001D5ED8 File Offset: 0x001D40D8
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<TransportDuplexSessionChannel.CloseOutputSessionAsyncResult>(result);
			}

			// Token: 0x06007DB2 RID: 32178 RVA: 0x001D5EE4 File Offset: 0x001D40E4
			private static void OnEnterComplete(object state, Exception asyncException)
			{
				TransportDuplexSessionChannel.CloseOutputSessionAsyncResult closeOutputSessionAsyncResult = (TransportDuplexSessionChannel.CloseOutputSessionAsyncResult)state;
				bool flag = false;
				Exception ex = asyncException;
				if (ex != null)
				{
					flag = true;
				}
				else
				{
					try
					{
						flag = closeOutputSessionAsyncResult.WriteEndBytes();
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						flag = true;
						ex = ex2;
					}
				}
				if (flag)
				{
					closeOutputSessionAsyncResult.Cleanup(ex == null, asyncException == null);
					closeOutputSessionAsyncResult.Complete(false, ex);
				}
			}

			// Token: 0x06007DB3 RID: 32179 RVA: 0x001D5F48 File Offset: 0x001D4148
			private static void OnWriteComplete(object asyncState)
			{
				TransportDuplexSessionChannel.CloseOutputSessionAsyncResult closeOutputSessionAsyncResult = (TransportDuplexSessionChannel.CloseOutputSessionAsyncResult)asyncState;
				Exception ex = null;
				try
				{
					closeOutputSessionAsyncResult.HandleWriteEndBytesComplete();
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				closeOutputSessionAsyncResult.Cleanup(ex == null, true);
				closeOutputSessionAsyncResult.Complete(false, ex);
			}

			// Token: 0x06007DB4 RID: 32180 RVA: 0x001D5F98 File Offset: 0x001D4198
			private bool WriteEndBytes()
			{
				this.channel.ThrowIfFaulted();
				if (this.channel.isOutputSessionClosed)
				{
					return true;
				}
				this.channel.isOutputSessionClosed = true;
				if (this.channel.BeginCloseOutput(this.timeoutHelper.RemainingTime(), TransportDuplexSessionChannel.CloseOutputSessionAsyncResult.onWriteComplete, this) == AsyncCompletionResult.Queued)
				{
					return false;
				}
				this.HandleWriteEndBytesComplete();
				return true;
			}

			// Token: 0x06007DB5 RID: 32181 RVA: 0x001D5FF4 File Offset: 0x001D41F4
			private void HandleWriteEndBytesComplete()
			{
				this.channel.FinishWritingMessage();
				this.channel.OnOutputSessionClosed(ref this.timeoutHelper);
			}

			// Token: 0x06007DB6 RID: 32182 RVA: 0x001D6014 File Offset: 0x001D4214
			private void Cleanup(bool success, bool lockTaken)
			{
				try
				{
					if (!success)
					{
						this.channel.Fault();
					}
				}
				finally
				{
					if (lockTaken)
					{
						this.channel.sendLock.Exit();
					}
				}
			}

			// Token: 0x04004837 RID: 18487
			private static WaitCallback onWriteComplete = Fx.ThunkCallback(new WaitCallback(TransportDuplexSessionChannel.CloseOutputSessionAsyncResult.OnWriteComplete));

			// Token: 0x04004838 RID: 18488
			private static FastAsyncCallback onEnterComplete = new FastAsyncCallback(TransportDuplexSessionChannel.CloseOutputSessionAsyncResult.OnEnterComplete);

			// Token: 0x04004839 RID: 18489
			private TransportDuplexSessionChannel channel;

			// Token: 0x0400483A RID: 18490
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x02000D65 RID: 3429
		private class SendAsyncResult : TraceAsyncResult
		{
			// Token: 0x06007DB8 RID: 32184 RVA: 0x001D6084 File Offset: 0x001D4284
			public SendAsyncResult(TransportDuplexSessionChannel channel, Message message, TimeSpan timeout, bool streamedOutput, AsyncCallback callback, object state) : base(callback, state)
			{
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.channel = channel;
				this.message = message;
				this.streamedOutput = streamedOutput;
				if (!channel.sendLock.EnterAsync(this.timeoutHelper.RemainingTime(), TransportDuplexSessionChannel.SendAsyncResult.onEnterComplete, this))
				{
					return;
				}
				bool flag = false;
				bool flag2 = false;
				try
				{
					flag = this.WriteCore();
					flag2 = true;
				}
				finally
				{
					if (!flag2)
					{
						this.Cleanup(false, true);
					}
				}
				if (flag)
				{
					this.Cleanup(true, true);
					base.Complete(true);
				}
			}

			// Token: 0x06007DB9 RID: 32185 RVA: 0x001D611C File Offset: 0x001D431C
			public static void End(IAsyncResult result)
			{
				if (TD.MessageSentByTransportIsEnabled())
				{
					TransportDuplexSessionChannel.SendAsyncResult sendAsyncResult = result as TransportDuplexSessionChannel.SendAsyncResult;
					if (sendAsyncResult != null)
					{
						TD.MessageSentByTransport(sendAsyncResult.eventTraceActivity, sendAsyncResult.channel.RemoteAddress.Uri.AbsoluteUri);
					}
				}
				AsyncResult.End<TransportDuplexSessionChannel.SendAsyncResult>(result);
			}

			// Token: 0x06007DBA RID: 32186 RVA: 0x001D6164 File Offset: 0x001D4364
			private static void OnEnterComplete(object state, Exception asyncException)
			{
				TransportDuplexSessionChannel.SendAsyncResult sendAsyncResult = (TransportDuplexSessionChannel.SendAsyncResult)state;
				bool flag = false;
				Exception ex = asyncException;
				if (ex != null)
				{
					flag = true;
				}
				else
				{
					try
					{
						flag = sendAsyncResult.WriteCore();
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						flag = true;
						ex = ex2;
					}
				}
				if (flag)
				{
					sendAsyncResult.Cleanup(ex == null, asyncException == null);
					sendAsyncResult.Complete(false, ex);
				}
			}

			// Token: 0x06007DBB RID: 32187 RVA: 0x001D61C8 File Offset: 0x001D43C8
			private static void OnWriteComplete(object asyncState)
			{
				TransportDuplexSessionChannel.SendAsyncResult sendAsyncResult = (TransportDuplexSessionChannel.SendAsyncResult)asyncState;
				Exception ex = null;
				try
				{
					sendAsyncResult.channel.FinishWritingMessage();
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				sendAsyncResult.Cleanup(ex == null, true);
				sendAsyncResult.Complete(false, ex);
			}

			// Token: 0x06007DBC RID: 32188 RVA: 0x001D6220 File Offset: 0x001D4420
			private bool WriteCore()
			{
				this.channel.ThrowIfDisposedOrNotOpen();
				this.channel.ThrowIfOutputSessionClosed();
				this.channel.ApplyChannelBinding(this.message);
				Message message = this.message;
				this.message = null;
				if (TD.MessageSentByTransportIsEnabled())
				{
					this.eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
				}
				AsyncCompletionResult asyncCompletionResult;
				if (this.streamedOutput)
				{
					asyncCompletionResult = this.channel.StartWritingStreamedMessage(message, this.timeoutHelper.RemainingTime(), TransportDuplexSessionChannel.SendAsyncResult.onWriteComplete, this);
				}
				else
				{
					bool allowOutputBatching = message.Properties.AllowOutputBatching;
					ArraySegment<byte> messageData = this.channel.EncodeMessage(message);
					this.buffer = messageData.Array;
					asyncCompletionResult = this.channel.StartWritingBufferedMessage(message, messageData, allowOutputBatching, this.timeoutHelper.RemainingTime(), TransportDuplexSessionChannel.SendAsyncResult.onWriteComplete, this);
				}
				if (asyncCompletionResult == AsyncCompletionResult.Queued)
				{
					return false;
				}
				this.channel.FinishWritingMessage();
				return true;
			}

			// Token: 0x06007DBD RID: 32189 RVA: 0x001D62F4 File Offset: 0x001D44F4
			private void Cleanup(bool success, bool lockTaken)
			{
				try
				{
					if (!success)
					{
						this.channel.Fault();
					}
				}
				finally
				{
					if (lockTaken)
					{
						this.channel.sendLock.Exit();
					}
				}
				if (this.buffer != null)
				{
					this.channel.bufferManager.ReturnBuffer(this.buffer);
					this.buffer = null;
				}
			}

			// Token: 0x0400483B RID: 18491
			private static WaitCallback onWriteComplete = Fx.ThunkCallback(new WaitCallback(TransportDuplexSessionChannel.SendAsyncResult.OnWriteComplete));

			// Token: 0x0400483C RID: 18492
			private static FastAsyncCallback onEnterComplete = new FastAsyncCallback(TransportDuplexSessionChannel.SendAsyncResult.OnEnterComplete);

			// Token: 0x0400483D RID: 18493
			private TransportDuplexSessionChannel channel;

			// Token: 0x0400483E RID: 18494
			private Message message;

			// Token: 0x0400483F RID: 18495
			private byte[] buffer;

			// Token: 0x04004840 RID: 18496
			private TimeoutHelper timeoutHelper;

			// Token: 0x04004841 RID: 18497
			private bool streamedOutput;

			// Token: 0x04004842 RID: 18498
			private EventTraceActivity eventTraceActivity;
		}

		// Token: 0x02000D66 RID: 3430
		private class TryReceiveAsyncResult : AsyncResult
		{
			// Token: 0x06007DBF RID: 32191 RVA: 0x001D6388 File Offset: 0x001D4588
			public TryReceiveAsyncResult(TransportDuplexSessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.channel = channel;
				bool flag = false;
				try
				{
					IAsyncResult asyncResult = this.channel.BeginReceive(timeout, TransportDuplexSessionChannel.TryReceiveAsyncResult.onReceive, this);
					if (asyncResult.CompletedSynchronously)
					{
						this.CompleteReceive(asyncResult);
						flag = true;
					}
				}
				catch (TimeoutException ex)
				{
					if (TD.ReceiveTimeoutIsEnabled())
					{
						TD.ReceiveTimeout(ex.Message);
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					flag = true;
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007DC0 RID: 32192 RVA: 0x001D6408 File Offset: 0x001D4608
			public static bool End(IAsyncResult result, out Message message)
			{
				TransportDuplexSessionChannel.TryReceiveAsyncResult tryReceiveAsyncResult = AsyncResult.End<TransportDuplexSessionChannel.TryReceiveAsyncResult>(result);
				message = tryReceiveAsyncResult.message;
				return tryReceiveAsyncResult.receiveSuccess;
			}

			// Token: 0x06007DC1 RID: 32193 RVA: 0x001D642C File Offset: 0x001D462C
			private static void OnReceive(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				TransportDuplexSessionChannel.TryReceiveAsyncResult tryReceiveAsyncResult = (TransportDuplexSessionChannel.TryReceiveAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					tryReceiveAsyncResult.CompleteReceive(result);
				}
				catch (TimeoutException ex)
				{
					if (TD.ReceiveTimeoutIsEnabled())
					{
						TD.ReceiveTimeout(ex.Message);
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					exception = ex2;
				}
				tryReceiveAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007DC2 RID: 32194 RVA: 0x001D64A8 File Offset: 0x001D46A8
			private void CompleteReceive(IAsyncResult result)
			{
				this.message = this.channel.EndReceive(result);
				this.receiveSuccess = true;
			}

			// Token: 0x04004843 RID: 18499
			private static AsyncCallback onReceive = Fx.ThunkCallback(new AsyncCallback(TransportDuplexSessionChannel.TryReceiveAsyncResult.OnReceive));

			// Token: 0x04004844 RID: 18500
			private TransportDuplexSessionChannel channel;

			// Token: 0x04004845 RID: 18501
			private bool receiveSuccess;

			// Token: 0x04004846 RID: 18502
			private Message message;
		}
	}
}
