using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000897 RID: 2199
	internal class DuplexSessionOneWayChannelListener : DelegatingChannelListener<IInputChannel>
	{
		// Token: 0x06005378 RID: 21368 RVA: 0x001335E0 File Offset: 0x001317E0
		public DuplexSessionOneWayChannelListener(OneWayBindingElement bindingElement, BindingContext context) : base(true, context.Binding, context.BuildInnerChannelListener<IDuplexSessionChannel>())
		{
			this.acceptLock = new object();
			this.inputChannelAcceptor = new DuplexSessionOneWayChannelListener.DuplexSessionOneWayInputChannelAcceptor(this);
			this.packetRoutable = bindingElement.PacketRoutable;
			this.maxAcceptedChannels = bindingElement.MaxAcceptedChannels;
			base.Acceptor = this.inputChannelAcceptor;
			this.idleTimeout = bindingElement.ChannelPoolSettings.IdleTimeout;
			this.onOpenInnerChannel = Fx.ThunkCallback(new AsyncCallback(this.OnOpenInnerChannel));
			this.ownsInnerListener = true;
			this.onInnerChannelClosed = new EventHandler(this.OnInnerChannelClosed);
		}

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x06005379 RID: 21369 RVA: 0x0013367C File Offset: 0x0013187C
		private bool IsAcceptNecessary
		{
			get
			{
				return !this.acceptPending && this.activeChannels < this.maxAcceptedChannels && this.innerChannelListener.State == CommunicationState.Opened;
			}
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x001336A4 File Offset: 0x001318A4
		protected override void OnOpening()
		{
			this.innerChannelListener = (IChannelListener<IDuplexSessionChannel>)this.InnerChannelListener;
			this.inputChannelAcceptor.TransferInnerChannelListener(this.innerChannelListener);
			this.ownsInnerListener = false;
			base.OnOpening();
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x001336D5 File Offset: 0x001318D5
		protected override void OnOpened()
		{
			base.OnOpened();
			ActionItem.Schedule(new Action<object>(this.AcceptLoop), null);
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x001336EF File Offset: 0x001318EF
		protected override void OnAbort()
		{
			base.OnAbort();
			if (this.ownsInnerListener && this.innerChannelListener != null)
			{
				this.innerChannelListener.Abort();
			}
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x00133712 File Offset: 0x00131912
		private void AcceptLoop(object state)
		{
			this.AcceptLoop(null);
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x0013371C File Offset: 0x0013191C
		private void AcceptLoop(IAsyncResult pendingResult)
		{
			IDuplexSessionChannel duplexSessionChannel = null;
			if (pendingResult != null)
			{
				if (!this.ProcessEndAccept(pendingResult, out duplexSessionChannel))
				{
					return;
				}
				pendingResult = null;
			}
			object obj = this.acceptLock;
			lock (obj)
			{
				while (this.IsAcceptNecessary)
				{
					Exception ex = null;
					try
					{
						IAsyncResult asyncResult = null;
						try
						{
							asyncResult = this.innerChannelListener.BeginAcceptChannel(TimeSpan.MaxValue, DuplexSessionOneWayChannelListener.onAcceptInnerChannel, this);
						}
						catch (CommunicationException exception)
						{
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
							continue;
						}
						this.acceptPending = true;
						if (!asyncResult.CompletedSynchronously)
						{
							break;
						}
						if (this.handleAcceptCallback == null)
						{
							this.handleAcceptCallback = new Action<object>(this.HandleAcceptCallback);
						}
						if (duplexSessionChannel != null)
						{
							ActionItem.Schedule(this.handleAcceptCallback, duplexSessionChannel);
							duplexSessionChannel = null;
						}
						IDuplexSessionChannel duplexSessionChannel2 = null;
						if (!this.ProcessEndAccept(asyncResult, out duplexSessionChannel2))
						{
							return;
						}
						if (duplexSessionChannel2 != null)
						{
							ActionItem.Schedule(this.handleAcceptCallback, duplexSessionChannel2);
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
						this.inputChannelAcceptor.Enqueue(ex, null, false);
					}
				}
			}
			if (duplexSessionChannel != null)
			{
				this.HandleAcceptComplete(duplexSessionChannel);
			}
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x00133854 File Offset: 0x00131A54
		private bool ProcessEndAccept(IAsyncResult result, out IDuplexSessionChannel channel)
		{
			channel = null;
			Exception ex = null;
			bool flag = false;
			try
			{
				channel = this.innerChannelListener.EndAcceptChannel(result);
				flag = true;
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				ex = ex2;
			}
			if (flag)
			{
				if (channel == null)
				{
					this.inputChannelAcceptor.Close();
					return false;
				}
				channel.Closed += this.onInnerChannelClosed;
				bool flag2 = false;
				object obj = this.acceptLock;
				lock (obj)
				{
					this.acceptPending = false;
					this.activeChannels++;
					if (this.activeChannels >= this.maxAcceptedChannels)
					{
						flag2 = true;
					}
				}
				if (DiagnosticUtility.ShouldTraceWarning && flag2)
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 262181, SR.GetString("TraceCodeMaxAcceptedChannelsReached"), new StringTraceRecord("MaxAcceptedChannels", this.maxAcceptedChannels.ToString(CultureInfo.InvariantCulture)), this, null);
				}
			}
			else if (ex != null)
			{
				bool canDispatchOnThisThread = this.innerChannelListener.State != CommunicationState.Opened;
				if (this.onExceptionDequeued == null)
				{
					this.onExceptionDequeued = new Action(this.OnExceptionDequeued);
				}
				this.inputChannelAcceptor.Enqueue(ex, this.onExceptionDequeued, canDispatchOnThisThread);
			}
			else
			{
				object obj2 = this.acceptLock;
				lock (obj2)
				{
					this.acceptPending = false;
				}
			}
			return true;
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x001339F0 File Offset: 0x00131BF0
		private void OnExceptionDequeued()
		{
			object obj = this.acceptLock;
			lock (obj)
			{
				this.acceptPending = false;
			}
			this.AcceptLoop(null);
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x00133A38 File Offset: 0x00131C38
		private static void OnAcceptInnerChannel(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			DuplexSessionOneWayChannelListener duplexSessionOneWayChannelListener = (DuplexSessionOneWayChannelListener)result.AsyncState;
			duplexSessionOneWayChannelListener.AcceptLoop(result);
		}

		// Token: 0x06005382 RID: 21378 RVA: 0x00133A61 File Offset: 0x00131C61
		private void HandleAcceptCallback(object state)
		{
			this.HandleAcceptComplete((IDuplexSessionChannel)state);
		}

		// Token: 0x06005383 RID: 21379 RVA: 0x00133A70 File Offset: 0x00131C70
		private void OnInnerChannelClosed(object sender, EventArgs e)
		{
			IDuplexSessionChannel duplexSessionChannel = (IDuplexSessionChannel)sender;
			duplexSessionChannel.Closed -= this.onInnerChannelClosed;
			object obj = this.acceptLock;
			lock (obj)
			{
				this.activeChannels--;
			}
			this.AcceptLoop(null);
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x00133AD4 File Offset: 0x00131CD4
		private void HandleAcceptComplete(IDuplexSessionChannel channel)
		{
			Exception ex = null;
			bool flag = false;
			this.inputChannelAcceptor.PrepareChannel(channel);
			IAsyncResult asyncResult = null;
			try
			{
				asyncResult = channel.BeginOpen(this.idleTimeout, this.onOpenInnerChannel, channel);
				flag = true;
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (TimeoutException ex2)
			{
				if (TD.OpenTimeoutIsEnabled())
				{
					TD.OpenTimeout(ex2.Message);
				}
				DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Information);
			}
			catch (Exception ex3)
			{
				if (Fx.IsFatal(ex3))
				{
					throw;
				}
				ex = ex3;
			}
			finally
			{
				if (!flag && channel != null)
				{
					channel.Abort();
				}
			}
			if (flag)
			{
				if (asyncResult.CompletedSynchronously)
				{
					this.CompleteOpen(channel, asyncResult);
					return;
				}
			}
			else if (ex != null)
			{
				this.inputChannelAcceptor.Enqueue(ex, null);
			}
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x00133BAC File Offset: 0x00131DAC
		private void OnOpenInnerChannel(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			IDuplexSessionChannel channel = (IDuplexSessionChannel)result.AsyncState;
			this.CompleteOpen(channel, result);
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x00133BD8 File Offset: 0x00131DD8
		private void CompleteOpen(IDuplexSessionChannel channel, IAsyncResult result)
		{
			Exception ex = null;
			bool flag = false;
			try
			{
				channel.EndOpen(result);
				flag = true;
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (TimeoutException ex2)
			{
				if (TD.OpenTimeoutIsEnabled())
				{
					TD.OpenTimeout(ex2.Message);
				}
				DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Information);
			}
			catch (Exception ex3)
			{
				if (Fx.IsFatal(ex3))
				{
					throw;
				}
				ex = ex3;
			}
			finally
			{
				if (!flag)
				{
					channel.Abort();
				}
			}
			if (flag)
			{
				this.inputChannelAcceptor.AcceptInnerChannel(this, channel);
				return;
			}
			if (ex != null)
			{
				this.inputChannelAcceptor.Enqueue(ex, null);
			}
		}

		// Token: 0x040032C5 RID: 12997
		private IChannelListener<IDuplexSessionChannel> innerChannelListener;

		// Token: 0x040032C6 RID: 12998
		private DuplexSessionOneWayChannelListener.DuplexSessionOneWayInputChannelAcceptor inputChannelAcceptor;

		// Token: 0x040032C7 RID: 12999
		private bool packetRoutable;

		// Token: 0x040032C8 RID: 13000
		private int maxAcceptedChannels;

		// Token: 0x040032C9 RID: 13001
		private bool acceptPending;

		// Token: 0x040032CA RID: 13002
		private int activeChannels;

		// Token: 0x040032CB RID: 13003
		private TimeSpan idleTimeout;

		// Token: 0x040032CC RID: 13004
		private static AsyncCallback onAcceptInnerChannel = Fx.ThunkCallback(new AsyncCallback(DuplexSessionOneWayChannelListener.OnAcceptInnerChannel));

		// Token: 0x040032CD RID: 13005
		private AsyncCallback onOpenInnerChannel;

		// Token: 0x040032CE RID: 13006
		private EventHandler onInnerChannelClosed;

		// Token: 0x040032CF RID: 13007
		private Action onExceptionDequeued;

		// Token: 0x040032D0 RID: 13008
		private Action<object> handleAcceptCallback;

		// Token: 0x040032D1 RID: 13009
		private bool ownsInnerListener;

		// Token: 0x040032D2 RID: 13010
		private object acceptLock;

		// Token: 0x02000D71 RID: 3441
		private class DuplexSessionOneWayInputChannelAcceptor : InputChannelAcceptor
		{
			// Token: 0x06007E22 RID: 32290 RVA: 0x001D6F20 File Offset: 0x001D5120
			public DuplexSessionOneWayInputChannelAcceptor(DuplexSessionOneWayChannelListener listener) : base(listener)
			{
				this.receivers = new ChannelTracker<IDuplexSessionChannel, DuplexSessionOneWayChannelListener.ChannelReceiver>();
			}

			// Token: 0x06007E23 RID: 32291 RVA: 0x001D6F34 File Offset: 0x001D5134
			public void TransferInnerChannelListener(IChannelListener<IDuplexSessionChannel> innerChannelListener)
			{
				bool flag = false;
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.innerChannelListener = innerChannelListener;
					if (base.State == CommunicationState.Closing || base.State == CommunicationState.Closed)
					{
						flag = true;
					}
				}
				if (flag)
				{
					innerChannelListener.Abort();
				}
			}

			// Token: 0x06007E24 RID: 32292 RVA: 0x001D6F94 File Offset: 0x001D5194
			public void AcceptInnerChannel(DuplexSessionOneWayChannelListener listener, IDuplexSessionChannel channel)
			{
				DuplexSessionOneWayChannelListener.ChannelReceiver channelReceiver = new DuplexSessionOneWayChannelListener.ChannelReceiver(listener, channel);
				this.receivers.Add(channel, channelReceiver);
				channelReceiver.StartReceiving();
			}

			// Token: 0x06007E25 RID: 32293 RVA: 0x001D6FBC File Offset: 0x001D51BC
			public void PrepareChannel(IDuplexSessionChannel channel)
			{
				this.receivers.PrepareChannel(channel);
			}

			// Token: 0x06007E26 RID: 32294 RVA: 0x001D6FCA File Offset: 0x001D51CA
			protected override InputChannel OnCreateChannel()
			{
				return new DuplexSessionOneWayChannelListener.DuplexSessionOneWayInputChannelAcceptor.DuplexSessionOneWayInputChannel(base.ChannelManager, null);
			}

			// Token: 0x06007E27 RID: 32295 RVA: 0x001D6FD8 File Offset: 0x001D51D8
			protected override void OnOpen(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.OnOpen(timeoutHelper.RemainingTime());
				this.receivers.Open(timeoutHelper.RemainingTime());
				this.innerChannelListener.Open(timeoutHelper.RemainingTime());
			}

			// Token: 0x06007E28 RID: 32296 RVA: 0x001D7020 File Offset: 0x001D5220
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new ChainedOpenAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginOpen), new ChainedEndHandler(base.OnEndOpen), new ICommunicationObject[]
				{
					this.receivers,
					this.innerChannelListener
				});
			}

			// Token: 0x06007E29 RID: 32297 RVA: 0x001D7065 File Offset: 0x001D5265
			protected override void OnEndOpen(IAsyncResult result)
			{
				ChainedAsyncResult.End(result);
			}

			// Token: 0x06007E2A RID: 32298 RVA: 0x001D706D File Offset: 0x001D526D
			protected override void OnAbort()
			{
				base.OnAbort();
				if (!this.TransferReceivers())
				{
					this.receivers.Abort();
					if (this.innerChannelListener != null)
					{
						this.innerChannelListener.Abort();
					}
				}
			}

			// Token: 0x06007E2B RID: 32299 RVA: 0x001D709C File Offset: 0x001D529C
			protected override void OnClose(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.OnClose(timeoutHelper.RemainingTime());
				if (!this.TransferReceivers())
				{
					this.receivers.Close(timeoutHelper.RemainingTime());
					this.innerChannelListener.Close(timeoutHelper.RemainingTime());
				}
			}

			// Token: 0x06007E2C RID: 32300 RVA: 0x001D70EC File Offset: 0x001D52EC
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				List<ICommunicationObject> list = new List<ICommunicationObject>();
				if (!this.TransferReceivers())
				{
					list.Add(this.receivers);
					list.Add(this.innerChannelListener);
				}
				return new ChainedCloseAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), list);
			}

			// Token: 0x06007E2D RID: 32301 RVA: 0x001D7140 File Offset: 0x001D5340
			protected override void OnEndClose(IAsyncResult result)
			{
				ChainedAsyncResult.End(result);
			}

			// Token: 0x06007E2E RID: 32302 RVA: 0x001D7148 File Offset: 0x001D5348
			private bool TransferReceivers()
			{
				DuplexSessionOneWayChannelListener.DuplexSessionOneWayInputChannelAcceptor.DuplexSessionOneWayInputChannel duplexSessionOneWayInputChannel = (DuplexSessionOneWayChannelListener.DuplexSessionOneWayInputChannelAcceptor.DuplexSessionOneWayInputChannel)base.GetCurrentChannel();
				return duplexSessionOneWayInputChannel != null && duplexSessionOneWayInputChannel.TransferReceivers(this.receivers, this.innerChannelListener);
			}

			// Token: 0x0400485E RID: 18526
			private ChannelTracker<IDuplexSessionChannel, DuplexSessionOneWayChannelListener.ChannelReceiver> receivers;

			// Token: 0x0400485F RID: 18527
			private IChannelListener<IDuplexSessionChannel> innerChannelListener;

			// Token: 0x02000F6B RID: 3947
			private class DuplexSessionOneWayInputChannel : InputChannel
			{
				// Token: 0x060087A7 RID: 34727 RVA: 0x001F82D2 File Offset: 0x001F64D2
				public DuplexSessionOneWayInputChannel(ChannelManagerBase channelManager, EndpointAddress localAddress) : base(channelManager, localAddress)
				{
				}

				// Token: 0x060087A8 RID: 34728 RVA: 0x001F82DC File Offset: 0x001F64DC
				public bool TransferReceivers(ChannelTracker<IDuplexSessionChannel, DuplexSessionOneWayChannelListener.ChannelReceiver> receivers, IChannelListener<IDuplexSessionChannel> innerChannelListener)
				{
					object thisLock = base.ThisLock;
					bool result;
					lock (thisLock)
					{
						if (base.State != CommunicationState.Opened)
						{
							result = false;
						}
						else
						{
							this.receivers = receivers;
							this.innerChannelListener = innerChannelListener;
							result = true;
						}
					}
					return result;
				}

				// Token: 0x060087A9 RID: 34729 RVA: 0x001F8334 File Offset: 0x001F6534
				protected override void OnAbort()
				{
					if (this.receivers != null)
					{
						this.receivers.Abort();
						this.innerChannelListener.Abort();
					}
					base.OnAbort();
				}

				// Token: 0x060087AA RID: 34730 RVA: 0x001F835C File Offset: 0x001F655C
				protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
				{
					List<ICommunicationObject> list = new List<ICommunicationObject>();
					if (this.receivers != null)
					{
						list.Add(this.receivers);
						list.Add(this.innerChannelListener);
					}
					return new ChainedCloseAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), list);
				}

				// Token: 0x060087AB RID: 34731 RVA: 0x001F83B0 File Offset: 0x001F65B0
				protected override void OnEndClose(IAsyncResult result)
				{
					ChainedAsyncResult.End(result);
				}

				// Token: 0x060087AC RID: 34732 RVA: 0x001F83B8 File Offset: 0x001F65B8
				protected override void OnClose(TimeSpan timeout)
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					if (this.receivers != null)
					{
						this.receivers.Close(timeoutHelper.RemainingTime());
						this.innerChannelListener.Close(timeoutHelper.RemainingTime());
					}
					base.OnClose(timeoutHelper.RemainingTime());
				}

				// Token: 0x04004F14 RID: 20244
				private ChannelTracker<IDuplexSessionChannel, DuplexSessionOneWayChannelListener.ChannelReceiver> receivers;

				// Token: 0x04004F15 RID: 20245
				private IChannelListener<IDuplexSessionChannel> innerChannelListener;
			}
		}

		// Token: 0x02000D72 RID: 3442
		private class ChannelReceiver
		{
			// Token: 0x06007E2F RID: 32303 RVA: 0x001D7178 File Offset: 0x001D5378
			public ChannelReceiver(DuplexSessionOneWayChannelListener parent, IDuplexSessionChannel channel)
			{
				this.channel = channel;
				this.acceptor = parent.inputChannelAcceptor;
				this.idleTimeout = parent.idleTimeout;
				this.validateHeader = parent.packetRoutable;
				this.onMessageDequeued = new Action(this.OnMessageDequeued);
			}

			// Token: 0x06007E30 RID: 32304 RVA: 0x001D71C8 File Offset: 0x001D53C8
			private void StartReceivingCallback(object state)
			{
				((DuplexSessionOneWayChannelListener.ChannelReceiver)state).StartReceiving();
			}

			// Token: 0x06007E31 RID: 32305 RVA: 0x001D71D8 File Offset: 0x001D53D8
			public void StartReceiving()
			{
				Exception ex = null;
				while (this.channel.State == CommunicationState.Opened)
				{
					IAsyncResult asyncResult = null;
					try
					{
						asyncResult = this.channel.BeginTryReceive(this.idleTimeout, DuplexSessionOneWayChannelListener.ChannelReceiver.onReceive, this);
					}
					catch (CommunicationException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
						goto IL_76;
					}
					if (asyncResult == null)
					{
						continue;
					}
					if (asyncResult.CompletedSynchronously)
					{
						bool flag2;
						bool flag = this.OnCompleteReceive(asyncResult, out flag2);
						if (flag2)
						{
							this.Dispatch();
						}
						if (flag)
						{
							continue;
						}
					}
					IL_76:
					if (ex != null)
					{
						this.acceptor.Enqueue(ex, this.onMessageDequeued);
					}
					return;
				}
				this.channel.Abort();
				goto IL_76;
			}

			// Token: 0x06007E32 RID: 32306 RVA: 0x001D728C File Offset: 0x001D548C
			private bool EnqueueMessage(Message message)
			{
				if (this.validateHeader)
				{
					if (!PacketRoutableHeader.TryValidateMessage(message))
					{
						this.channel.Abort();
						message.Close();
						return false;
					}
					this.validateHeader = false;
				}
				return this.acceptor.EnqueueWithoutDispatch(message, this.onMessageDequeued);
			}

			// Token: 0x06007E33 RID: 32307 RVA: 0x001D72CA File Offset: 0x001D54CA
			private void OnStartReceiveLater(object state)
			{
				this.StartReceiving();
			}

			// Token: 0x06007E34 RID: 32308 RVA: 0x001D72D2 File Offset: 0x001D54D2
			private void OnDispatchItemsLater(object state)
			{
				this.Dispatch();
			}

			// Token: 0x06007E35 RID: 32309 RVA: 0x001D72DA File Offset: 0x001D54DA
			private void Dispatch()
			{
				this.acceptor.DispatchItems();
			}

			// Token: 0x06007E36 RID: 32310 RVA: 0x001D72E8 File Offset: 0x001D54E8
			private bool OnCompleteReceive(IAsyncResult result, out bool dispatchLater)
			{
				Exception ex = null;
				Message message = null;
				bool result2 = false;
				dispatchLater = false;
				try
				{
					if (!this.channel.EndTryReceive(result, out message))
					{
						this.channel.Abort();
					}
					else if (message == null)
					{
						this.channel.Close();
					}
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					result2 = (this.channel.State == CommunicationState.Opened);
				}
				catch (TimeoutException ex2)
				{
					if (TD.CloseTimeoutIsEnabled())
					{
						TD.CloseTimeout(ex2.Message);
					}
					DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Information);
					result2 = (this.channel.State == CommunicationState.Opened);
				}
				catch (Exception ex3)
				{
					if (Fx.IsFatal(ex3))
					{
						throw;
					}
					ex = ex3;
				}
				if (message != null)
				{
					dispatchLater = this.EnqueueMessage(message);
				}
				else if (ex != null)
				{
					dispatchLater = this.acceptor.EnqueueWithoutDispatch(ex, this.onMessageDequeued);
				}
				return result2;
			}

			// Token: 0x06007E37 RID: 32311 RVA: 0x001D73D4 File Offset: 0x001D55D4
			private void OnMessageDequeued()
			{
				IAsyncResult asyncResult = null;
				Exception ex = null;
				try
				{
					asyncResult = this.channel.BeginTryReceive(this.idleTimeout, DuplexSessionOneWayChannelListener.ChannelReceiver.onReceive, this);
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (asyncResult != null)
				{
					if (asyncResult.CompletedSynchronously)
					{
						bool flag;
						if (this.OnCompleteReceive(asyncResult, out flag))
						{
							if (this.onStartReceiveLater == null)
							{
								this.onStartReceiveLater = new Action<object>(this.OnStartReceiveLater);
							}
							ActionItem.Schedule(this.onStartReceiveLater, null);
						}
						if (flag)
						{
							if (this.onDispatchItemsLater == null)
							{
								this.onDispatchItemsLater = new Action<object>(this.OnDispatchItemsLater);
							}
							ActionItem.Schedule(this.onDispatchItemsLater, null);
							return;
						}
					}
				}
				else
				{
					if (ex != null)
					{
						this.acceptor.Enqueue(ex, this.onMessageDequeued, false);
						return;
					}
					if (this.channel.State == CommunicationState.Opened)
					{
						if (DuplexSessionOneWayChannelListener.ChannelReceiver.startReceivingCallback == null)
						{
							DuplexSessionOneWayChannelListener.ChannelReceiver.startReceivingCallback = new Action<object>(this.StartReceivingCallback);
						}
						ActionItem.Schedule(DuplexSessionOneWayChannelListener.ChannelReceiver.startReceivingCallback, this);
					}
				}
			}

			// Token: 0x06007E38 RID: 32312 RVA: 0x001D74E8 File Offset: 0x001D56E8
			private static void OnReceive(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				DuplexSessionOneWayChannelListener.ChannelReceiver channelReceiver = (DuplexSessionOneWayChannelListener.ChannelReceiver)result.AsyncState;
				bool flag;
				if (channelReceiver.OnCompleteReceive(result, out flag))
				{
					channelReceiver.StartReceiving();
				}
				if (flag)
				{
					channelReceiver.Dispatch();
				}
			}

			// Token: 0x04004860 RID: 18528
			private Action onMessageDequeued;

			// Token: 0x04004861 RID: 18529
			private static AsyncCallback onReceive = Fx.ThunkCallback(new AsyncCallback(DuplexSessionOneWayChannelListener.ChannelReceiver.OnReceive));

			// Token: 0x04004862 RID: 18530
			private DuplexSessionOneWayChannelListener.DuplexSessionOneWayInputChannelAcceptor acceptor;

			// Token: 0x04004863 RID: 18531
			private IDuplexSessionChannel channel;

			// Token: 0x04004864 RID: 18532
			private TimeSpan idleTimeout;

			// Token: 0x04004865 RID: 18533
			private static Action<object> startReceivingCallback;

			// Token: 0x04004866 RID: 18534
			private Action<object> onStartReceiveLater;

			// Token: 0x04004867 RID: 18535
			private Action<object> onDispatchItemsLater;

			// Token: 0x04004868 RID: 18536
			private bool validateHeader;
		}
	}
}
