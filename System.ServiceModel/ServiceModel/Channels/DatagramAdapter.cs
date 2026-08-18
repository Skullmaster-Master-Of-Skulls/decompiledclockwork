using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200099C RID: 2460
	internal class DatagramAdapter
	{
		// Token: 0x06006003 RID: 24579 RVA: 0x001663DD File Offset: 0x001645DD
		internal static IOutputChannel GetOutputChannel(DatagramAdapter.Source<IOutputSessionChannel> channelSource, IDefaultCommunicationTimeouts timeouts)
		{
			return new DatagramAdapter.OutputDatagramAdapterChannel(channelSource, timeouts);
		}

		// Token: 0x06006004 RID: 24580 RVA: 0x001663E6 File Offset: 0x001645E6
		internal static IRequestChannel GetRequestChannel(DatagramAdapter.Source<IRequestSessionChannel> channelSource, IDefaultCommunicationTimeouts timeouts)
		{
			return new DatagramAdapter.RequestDatagramAdapterChannel(channelSource, timeouts);
		}

		// Token: 0x06006005 RID: 24581 RVA: 0x001663EF File Offset: 0x001645EF
		internal static IChannelListener<IInputChannel> GetInputListener(IChannelListener<IInputSessionChannel> inner, ServiceThrottle throttle, IDefaultCommunicationTimeouts timeouts)
		{
			return new DatagramAdapter.InputDatagramAdapterListener(inner, throttle, timeouts);
		}

		// Token: 0x06006006 RID: 24582 RVA: 0x001663F9 File Offset: 0x001645F9
		internal static IChannelListener<IReplyChannel> GetReplyListener(IChannelListener<IReplySessionChannel> inner, ServiceThrottle throttle, IDefaultCommunicationTimeouts timeouts)
		{
			return new DatagramAdapter.ReplyDatagramAdapterListener(inner, throttle, timeouts);
		}

		// Token: 0x02000E13 RID: 3603
		// (Invoke) Token: 0x060081BF RID: 33215
		internal delegate T Source<T>();

		// Token: 0x02000E14 RID: 3604
		private abstract class DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType> : DelegatingChannelListener<TChannel>, ISessionThrottleNotification where TChannel : class, IChannel where TSessionChannel : class, IChannel where ItemType : class
		{
			// Token: 0x060081C2 RID: 33218 RVA: 0x001E0C6C File Offset: 0x001DEE6C
			protected DatagramAdapterListenerBase(IChannelListener<TSessionChannel> listener, ServiceThrottle throttle, IDefaultCommunicationTimeouts timeouts) : base(timeouts, listener)
			{
				if (listener == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("listener");
				}
				this.channels = new DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.SessionChannelCollection(this.ThisLock);
				this.listener = listener;
				this.throttle = throttle;
				this.channelPumpAfterExceptionDelegate = new Action(this.ChannelPump);
			}

			// Token: 0x17001CA4 RID: 7332
			// (get) Token: 0x060081C3 RID: 33219 RVA: 0x001E0CC5 File Offset: 0x001DEEC5
			internal DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.SessionChannelCollection Channels
			{
				get
				{
					return this.channels;
				}
			}

			// Token: 0x17001CA5 RID: 7333
			// (get) Token: 0x060081C4 RID: 33220 RVA: 0x001E0CCD File Offset: 0x001DEECD
			internal new object ThisLock
			{
				get
				{
					return base.ThisLock;
				}
			}

			// Token: 0x060081C5 RID: 33221
			protected abstract IAsyncResult CallBeginReceive(TSessionChannel channel, AsyncCallback callback, object state);

			// Token: 0x060081C6 RID: 33222
			protected abstract ItemType CallEndReceive(TSessionChannel channel, IAsyncResult result);

			// Token: 0x060081C7 RID: 33223
			protected abstract void Enqueue(ItemType item, Action callback);

			// Token: 0x060081C8 RID: 33224
			protected abstract void Enqueue(Exception exception, Action callback);

			// Token: 0x060081C9 RID: 33225 RVA: 0x001E0CD5 File Offset: 0x001DEED5
			private static void AcceptCallbackStatic(IAsyncResult result)
			{
				((DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>)result.AsyncState).AcceptCallback(result);
			}

			// Token: 0x060081CA RID: 33226 RVA: 0x001E0CE8 File Offset: 0x001DEEE8
			private void AcceptCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously && this.FinishAccept(result))
				{
					this.ChannelPump();
				}
			}

			// Token: 0x060081CB RID: 33227 RVA: 0x001E0D04 File Offset: 0x001DEF04
			private void AcceptLoopDone()
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					bool flag2 = this.acceptLoopDone;
					this.acceptLoopDone = true;
					if (this.waiter != null)
					{
						this.waiter.Signal();
					}
				}
			}

			// Token: 0x060081CC RID: 33228 RVA: 0x001E0D60 File Offset: 0x001DEF60
			private static void ChannelPump(object state)
			{
				((DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>)state).ChannelPump();
			}

			// Token: 0x060081CD RID: 33229 RVA: 0x001E0D70 File Offset: 0x001DEF70
			private void ChannelPump()
			{
				while (this.listener.State == CommunicationState.Opened)
				{
					IAsyncResult asyncResult = null;
					Exception ex = null;
					try
					{
						asyncResult = this.listener.BeginAcceptChannel(TimeSpan.MaxValue, DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.acceptCallbackDelegate, this);
					}
					catch (ObjectDisposedException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					}
					catch (CommunicationException exception2)
					{
						DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
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
						this.Enqueue(ex, this.channelPumpAfterExceptionDelegate);
						return;
					}
					if (!asyncResult.CompletedSynchronously || !this.FinishAccept(asyncResult))
					{
						break;
					}
				}
			}

			// Token: 0x060081CE RID: 33230 RVA: 0x001E0E1C File Offset: 0x001DF01C
			private bool FinishAccept(IAsyncResult result)
			{
				TSessionChannel tsessionChannel = default(TSessionChannel);
				Exception ex = null;
				try
				{
					tsessionChannel = this.listener.EndAcceptChannel(result);
				}
				catch (ObjectDisposedException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				catch (CommunicationException exception2)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
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
					this.Enqueue(ex, this.channelPumpAfterExceptionDelegate);
				}
				else if (tsessionChannel == null)
				{
					this.AcceptLoopDone();
				}
				else if (base.State == CommunicationState.Opened)
				{
					DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.DatagramAdapterReceiver.Pump(this, tsessionChannel);
				}
				else
				{
					try
					{
						tsessionChannel.Close();
					}
					catch (CommunicationException exception3)
					{
						DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
					}
					catch (TimeoutException ex3)
					{
						if (TD.CloseTimeoutIsEnabled())
						{
							TD.CloseTimeout(ex3.Message);
						}
						DiagnosticUtility.TraceHandledException(ex3, TraceEventType.Information);
					}
					catch (Exception ex4)
					{
						if (Fx.IsFatal(ex4))
						{
							throw;
						}
						ex = ex4;
					}
					if (ex != null)
					{
						this.Enqueue(ex, this.channelPumpAfterExceptionDelegate);
					}
				}
				return tsessionChannel != null && this.throttle.AcquireSession(this);
			}

			// Token: 0x060081CF RID: 33231 RVA: 0x001E0F5C File Offset: 0x001DF15C
			internal void DecrementUsageCount()
			{
				object thisLock = this.ThisLock;
				bool flag2;
				lock (thisLock)
				{
					this.usageCount--;
					flag2 = (this.usageCount == 0);
				}
				if (flag2)
				{
					this.channels.AbortChannels();
				}
			}

			// Token: 0x060081D0 RID: 33232 RVA: 0x001E0FBC File Offset: 0x001DF1BC
			internal void IncrementUsageCount()
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.usageCount++;
				}
			}

			// Token: 0x060081D1 RID: 33233 RVA: 0x001E1004 File Offset: 0x001DF204
			protected override void OnOpen(TimeSpan timeout)
			{
				base.OnOpen(timeout);
				ActionItem.Schedule(DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.channelPumpDelegate, this);
			}

			// Token: 0x060081D2 RID: 33234 RVA: 0x001E1018 File Offset: 0x001DF218
			protected override void OnEndOpen(IAsyncResult result)
			{
				base.OnEndOpen(result);
				ActionItem.Schedule(DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.channelPumpDelegate, this);
			}

			// Token: 0x060081D3 RID: 33235 RVA: 0x001E102C File Offset: 0x001DF22C
			public void ThrottleAcquired()
			{
				ActionItem.Schedule(DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.channelPumpDelegate, this);
			}

			// Token: 0x060081D4 RID: 33236 RVA: 0x001E103C File Offset: 0x001DF23C
			protected override void OnClose(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.OnClose(timeoutHelper.RemainingTime());
				this.WaitForAcceptLoop(timeoutHelper.RemainingTime());
			}

			// Token: 0x060081D5 RID: 33237 RVA: 0x001E106B File Offset: 0x001DF26B
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), new ChainedBeginHandler(this.BeginWaitForAcceptLoop), new ChainedEndHandler(this.EndWaitForAcceptLoop));
			}

			// Token: 0x060081D6 RID: 33238 RVA: 0x001E10A5 File Offset: 0x001DF2A5
			protected override void OnEndClose(IAsyncResult result)
			{
				ChainedAsyncResult.End(result);
			}

			// Token: 0x060081D7 RID: 33239 RVA: 0x001E10B0 File Offset: 0x001DF2B0
			private void WaitForAcceptLoop(TimeSpan timeout)
			{
				DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.SyncWaiter syncWaiter = null;
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (!this.acceptLoopDone)
					{
						syncWaiter = new DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.SyncWaiter(this);
						this.waiter = syncWaiter;
					}
				}
				if (syncWaiter != null)
				{
					syncWaiter.Wait(timeout);
				}
			}

			// Token: 0x060081D8 RID: 33240 RVA: 0x001E1110 File Offset: 0x001DF310
			private IAsyncResult BeginWaitForAcceptLoop(TimeSpan timeout, AsyncCallback callback, object state)
			{
				DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.AsyncWaiter asyncWaiter = null;
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (!this.acceptLoopDone)
					{
						asyncWaiter = new DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.AsyncWaiter(timeout, callback, state);
						this.waiter = asyncWaiter;
					}
				}
				if (asyncWaiter != null)
				{
					return asyncWaiter;
				}
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x060081D9 RID: 33241 RVA: 0x001E1170 File Offset: 0x001DF370
			private void EndWaitForAcceptLoop(IAsyncResult result)
			{
				if (result is CompletedAsyncResult)
				{
					CompletedAsyncResult.End(result);
					return;
				}
				DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.AsyncWaiter.End(result);
			}

			// Token: 0x040049CD RID: 18893
			private static AsyncCallback acceptCallbackDelegate = Fx.ThunkCallback(new AsyncCallback(DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.AcceptCallbackStatic));

			// Token: 0x040049CE RID: 18894
			private static Action<object> channelPumpDelegate = new Action<object>(DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.ChannelPump);

			// Token: 0x040049CF RID: 18895
			private Action channelPumpAfterExceptionDelegate;

			// Token: 0x040049D0 RID: 18896
			private DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.SessionChannelCollection channels;

			// Token: 0x040049D1 RID: 18897
			private IChannelListener<TSessionChannel> listener;

			// Token: 0x040049D2 RID: 18898
			private ServiceThrottle throttle;

			// Token: 0x040049D3 RID: 18899
			private int usageCount;

			// Token: 0x040049D4 RID: 18900
			private bool acceptLoopDone;

			// Token: 0x040049D5 RID: 18901
			private DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.IWaiter waiter;

			// Token: 0x02000F7E RID: 3966
			private class DatagramAdapterReceiver
			{
				// Token: 0x06008800 RID: 34816 RVA: 0x001F9954 File Offset: 0x001F7B54
				private DatagramAdapterReceiver(DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType> parent, TSessionChannel channel)
				{
					this.parent = parent;
					this.channel = channel;
					if (DiagnosticUtility.ShouldUseActivity)
					{
						this.activity = ServiceModelActivity.Current;
					}
					if (DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.DatagramAdapterReceiver.faultedDelegate == null)
					{
						DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.DatagramAdapterReceiver.faultedDelegate = new EventHandler(DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.DatagramAdapterReceiver.FaultedCallback);
					}
					this.channel.Faulted += DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.DatagramAdapterReceiver.faultedDelegate;
					this.channel.Closed += this.ClosedCallback;
					this.itemDequeuedDelegate = new Action(this.StartNextReceive);
					this.parent.channels.Add(channel);
					try
					{
						channel.Open();
					}
					catch (CommunicationException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					}
					catch (TimeoutException ex)
					{
						if (TD.OpenTimeoutIsEnabled())
						{
							TD.OpenTimeout(ex.Message);
						}
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					}
					catch (Exception exception2)
					{
						if (Fx.IsFatal(exception2))
						{
							throw;
						}
						if (DiagnosticUtility.ShouldTraceWarning)
						{
							TraceUtility.TraceEvent(TraceEventType.Warning, 524351, SR.GetString("TraceCodeFailedToOpenIncomingChannel"));
						}
						channel.Abort();
						this.parent.Enqueue(exception2, null);
					}
				}

				// Token: 0x06008801 RID: 34817 RVA: 0x001F9A90 File Offset: 0x001F7C90
				private void ClosedCallback(object sender, EventArgs e)
				{
					TSessionChannel item = (TSessionChannel)((object)sender);
					this.parent.channels.Remove(item);
					this.parent.throttle.DeactivateChannel();
				}

				// Token: 0x06008802 RID: 34818 RVA: 0x001F9AC6 File Offset: 0x001F7CC6
				private static void FaultedCallback(object sender, EventArgs e)
				{
					((IChannel)sender).Abort();
				}

				// Token: 0x06008803 RID: 34819 RVA: 0x001F9AD3 File Offset: 0x001F7CD3
				private static void StartNextReceive(object state)
				{
					((DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.DatagramAdapterReceiver)state).StartNextReceive();
				}

				// Token: 0x06008804 RID: 34820 RVA: 0x001F9AE0 File Offset: 0x001F7CE0
				private void StartNextReceive()
				{
					if (this.channel.State == CommunicationState.Opened)
					{
						using (ServiceModelActivity.BoundOperation(this.activity))
						{
							IAsyncResult asyncResult = null;
							Exception ex = null;
							try
							{
								asyncResult = this.parent.CallBeginReceive(this.channel, DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.DatagramAdapterReceiver.receiveCallbackDelegate, this);
							}
							catch (ObjectDisposedException exception)
							{
								DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
							}
							catch (CommunicationException exception2)
							{
								DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
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
								this.parent.Enqueue(ex, this.itemDequeuedDelegate);
							}
							else if (asyncResult.CompletedSynchronously)
							{
								this.FinishReceive(asyncResult);
							}
						}
					}
				}

				// Token: 0x06008805 RID: 34821 RVA: 0x001F9BBC File Offset: 0x001F7DBC
				internal static void Pump(DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType> listener, TSessionChannel channel)
				{
					DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.DatagramAdapterReceiver state = new DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.DatagramAdapterReceiver(listener, channel);
					ActionItem.Schedule(DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.DatagramAdapterReceiver.startNextReceiveDelegate, state);
				}

				// Token: 0x06008806 RID: 34822 RVA: 0x001F9BDC File Offset: 0x001F7DDC
				private static void ReceiveCallbackStatic(IAsyncResult result)
				{
					if (!result.CompletedSynchronously)
					{
						((DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.DatagramAdapterReceiver)result.AsyncState).FinishReceive(result);
					}
				}

				// Token: 0x06008807 RID: 34823 RVA: 0x001F9BF8 File Offset: 0x001F7DF8
				private void FinishReceive(IAsyncResult result)
				{
					ItemType itemType = default(ItemType);
					Exception ex = null;
					try
					{
						itemType = this.parent.CallEndReceive(this.channel, result);
					}
					catch (ObjectDisposedException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					}
					catch (CommunicationException exception2)
					{
						DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
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
						this.parent.Enqueue(ex, this.itemDequeuedDelegate);
						return;
					}
					if (itemType != null)
					{
						this.parent.Enqueue(itemType, this.itemDequeuedDelegate);
						return;
					}
					try
					{
						this.channel.Close();
					}
					catch (CommunicationException exception3)
					{
						DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
					}
					catch (TimeoutException ex3)
					{
						if (TD.CloseTimeoutIsEnabled())
						{
							TD.CloseTimeout(ex3.Message);
						}
						DiagnosticUtility.TraceHandledException(ex3, TraceEventType.Information);
					}
					catch (Exception ex4)
					{
						if (Fx.IsFatal(ex4))
						{
							throw;
						}
						ex = ex4;
					}
					if (ex != null)
					{
						this.parent.Enqueue(ex, this.itemDequeuedDelegate);
					}
				}

				// Token: 0x04004F57 RID: 20311
				private static AsyncCallback receiveCallbackDelegate = Fx.ThunkCallback(new AsyncCallback(DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.DatagramAdapterReceiver.ReceiveCallbackStatic));

				// Token: 0x04004F58 RID: 20312
				private static Action<object> startNextReceiveDelegate = new Action<object>(DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.DatagramAdapterReceiver.StartNextReceive);

				// Token: 0x04004F59 RID: 20313
				private static EventHandler faultedDelegate;

				// Token: 0x04004F5A RID: 20314
				private DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType> parent;

				// Token: 0x04004F5B RID: 20315
				private TSessionChannel channel;

				// Token: 0x04004F5C RID: 20316
				private Action itemDequeuedDelegate;

				// Token: 0x04004F5D RID: 20317
				private ServiceModelActivity activity;
			}

			// Token: 0x02000F7F RID: 3967
			internal class SessionChannelCollection : SynchronizedCollection<TSessionChannel>
			{
				// Token: 0x06008809 RID: 34825 RVA: 0x001F9D55 File Offset: 0x001F7F55
				internal SessionChannelCollection(object syncRoot) : base(syncRoot)
				{
					this.onChannelClosed = new EventHandler(this.OnChannelClosed);
					this.onChannelFaulted = new EventHandler(this.OnChannelFaulted);
				}

				// Token: 0x0600880A RID: 34826 RVA: 0x001F9D84 File Offset: 0x001F7F84
				public void AbortChannels()
				{
					object syncRoot = base.SyncRoot;
					lock (syncRoot)
					{
						for (int i = base.Count - 1; i >= 0; i--)
						{
							base[i].Abort();
						}
					}
				}

				// Token: 0x0600880B RID: 34827 RVA: 0x001F9DE4 File Offset: 0x001F7FE4
				private void AddingChannel(TSessionChannel channel)
				{
					channel.Faulted += this.onChannelFaulted;
					channel.Closed += this.onChannelClosed;
				}

				// Token: 0x0600880C RID: 34828 RVA: 0x001F9E08 File Offset: 0x001F8008
				private void RemovingChannel(TSessionChannel channel)
				{
					channel.Faulted -= this.onChannelFaulted;
					channel.Closed -= this.onChannelClosed;
					channel.Abort();
				}

				// Token: 0x0600880D RID: 34829 RVA: 0x001F9E38 File Offset: 0x001F8038
				private void OnChannelClosed(object sender, EventArgs args)
				{
					TSessionChannel item = (TSessionChannel)((object)sender);
					base.Remove(item);
				}

				// Token: 0x0600880E RID: 34830 RVA: 0x001F9E54 File Offset: 0x001F8054
				private void OnChannelFaulted(object sender, EventArgs args)
				{
					TSessionChannel item = (TSessionChannel)((object)sender);
					base.Remove(item);
				}

				// Token: 0x0600880F RID: 34831 RVA: 0x001F9E70 File Offset: 0x001F8070
				protected override void ClearItems()
				{
					List<TSessionChannel> items = base.Items;
					for (int i = 0; i < items.Count; i++)
					{
						this.RemovingChannel(items[i]);
					}
					base.ClearItems();
				}

				// Token: 0x06008810 RID: 34832 RVA: 0x001F9EA8 File Offset: 0x001F80A8
				protected override void InsertItem(int index, TSessionChannel item)
				{
					this.AddingChannel(item);
					base.InsertItem(index, item);
				}

				// Token: 0x06008811 RID: 34833 RVA: 0x001F9EBC File Offset: 0x001F80BC
				protected override void RemoveItem(int index)
				{
					TSessionChannel channel = base.Items[index];
					base.RemoveItem(index);
					this.RemovingChannel(channel);
				}

				// Token: 0x06008812 RID: 34834 RVA: 0x001F9EE4 File Offset: 0x001F80E4
				protected override void SetItem(int index, TSessionChannel item)
				{
					TSessionChannel channel = base.Items[index];
					this.AddingChannel(item);
					base.SetItem(index, item);
					this.RemovingChannel(channel);
				}

				// Token: 0x04004F5E RID: 20318
				private EventHandler onChannelClosed;

				// Token: 0x04004F5F RID: 20319
				private EventHandler onChannelFaulted;
			}

			// Token: 0x02000F80 RID: 3968
			internal interface IWaiter
			{
				// Token: 0x06008813 RID: 34835
				void Signal();
			}

			// Token: 0x02000F81 RID: 3969
			internal class AsyncWaiter : AsyncResult, DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.IWaiter
			{
				// Token: 0x06008814 RID: 34836 RVA: 0x001F9F14 File Offset: 0x001F8114
				internal AsyncWaiter(TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					if (timeout != TimeSpan.MaxValue)
					{
						this.timer = new IOThreadTimer(DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.AsyncWaiter.timerCallback, this, false);
						this.timer.Set(timeout);
					}
				}

				// Token: 0x06008815 RID: 34837 RVA: 0x001F9F49 File Offset: 0x001F8149
				internal static bool End(IAsyncResult result)
				{
					AsyncResult.End<DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.AsyncWaiter>(result);
					return !((DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.AsyncWaiter)result).timedOut;
				}

				// Token: 0x06008816 RID: 34838 RVA: 0x001F9F60 File Offset: 0x001F8160
				public void Signal()
				{
					if (this.timer == null || this.timer.Cancel())
					{
						base.Complete(false);
					}
				}

				// Token: 0x06008817 RID: 34839 RVA: 0x001F9F80 File Offset: 0x001F8180
				private static void TimerCallback(object state)
				{
					DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.AsyncWaiter asyncWaiter = (DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.AsyncWaiter)state;
					asyncWaiter.timedOut = true;
					asyncWaiter.Complete(false);
				}

				// Token: 0x04004F60 RID: 20320
				private static Action<object> timerCallback = new Action<object>(DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.AsyncWaiter.TimerCallback);

				// Token: 0x04004F61 RID: 20321
				private bool timedOut;

				// Token: 0x04004F62 RID: 20322
				private readonly IOThreadTimer timer;
			}

			// Token: 0x02000F82 RID: 3970
			internal class SyncWaiter : DatagramAdapter.DatagramAdapterListenerBase<TChannel, TSessionChannel, ItemType>.IWaiter
			{
				// Token: 0x06008819 RID: 34841 RVA: 0x001F9FB5 File Offset: 0x001F81B5
				internal SyncWaiter(object thisLock)
				{
					this.thisLock = thisLock;
				}

				// Token: 0x17001D9E RID: 7582
				// (get) Token: 0x0600881A RID: 34842 RVA: 0x001F9FC4 File Offset: 0x001F81C4
				private object ThisLock
				{
					get
					{
						return this.thisLock;
					}
				}

				// Token: 0x0600881B RID: 34843 RVA: 0x001F9FCC File Offset: 0x001F81CC
				public void Signal()
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						this.didSignal = true;
						if (this.wait != null)
						{
							this.wait.Set();
						}
					}
				}

				// Token: 0x0600881C RID: 34844 RVA: 0x001FA024 File Offset: 0x001F8224
				public bool Wait(TimeSpan timeout)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (!this.didSignal)
						{
							this.wait = new ManualResetEvent(false);
						}
					}
					if (this.wait == null || TimeoutHelper.WaitOne(this.wait, timeout))
					{
						if (this.wait != null)
						{
							this.wait.Close();
							this.wait = null;
						}
						return true;
					}
					object obj2 = this.ThisLock;
					lock (obj2)
					{
						this.wait.Close();
						this.wait = null;
					}
					return false;
				}

				// Token: 0x04004F63 RID: 20323
				private bool didSignal;

				// Token: 0x04004F64 RID: 20324
				private object thisLock;

				// Token: 0x04004F65 RID: 20325
				private ManualResetEvent wait;
			}
		}

		// Token: 0x02000E15 RID: 3605
		private class InputDatagramAdapterListener : DatagramAdapter.DatagramAdapterListenerBase<IInputChannel, IInputSessionChannel, Message>
		{
			// Token: 0x060081DB RID: 33243 RVA: 0x001E11B1 File Offset: 0x001DF3B1
			internal InputDatagramAdapterListener(IChannelListener<IInputSessionChannel> listener, ServiceThrottle throttle, IDefaultCommunicationTimeouts timeouts) : base(listener, throttle, timeouts)
			{
				this.acceptor = new DatagramAdapter.InputDatagramAdapterAcceptor(this);
				base.Acceptor = this.acceptor;
			}

			// Token: 0x060081DC RID: 33244 RVA: 0x001E11D4 File Offset: 0x001DF3D4
			protected override IAsyncResult CallBeginReceive(IInputSessionChannel channel, AsyncCallback callback, object state)
			{
				return channel.BeginReceive(TimeSpan.MaxValue, callback, state);
			}

			// Token: 0x060081DD RID: 33245 RVA: 0x001E11E3 File Offset: 0x001DF3E3
			protected override Message CallEndReceive(IInputSessionChannel channel, IAsyncResult result)
			{
				return channel.EndReceive(result);
			}

			// Token: 0x060081DE RID: 33246 RVA: 0x001E11EC File Offset: 0x001DF3EC
			protected override void Enqueue(Message message, Action callback)
			{
				this.acceptor.Enqueue(message, callback);
			}

			// Token: 0x060081DF RID: 33247 RVA: 0x001E11FB File Offset: 0x001DF3FB
			protected override void Enqueue(Exception exception, Action callback)
			{
				this.acceptor.Enqueue(exception, callback);
			}

			// Token: 0x040049D6 RID: 18902
			private SingletonChannelAcceptor<IInputChannel, InputChannel, Message> acceptor;
		}

		// Token: 0x02000E16 RID: 3606
		private class InputDatagramAdapterAcceptor : InputChannelAcceptor
		{
			// Token: 0x060081E0 RID: 33248 RVA: 0x001E120A File Offset: 0x001DF40A
			internal InputDatagramAdapterAcceptor(DatagramAdapter.InputDatagramAdapterListener listener) : base(listener)
			{
				this.listener = listener;
			}

			// Token: 0x060081E1 RID: 33249 RVA: 0x001E121A File Offset: 0x001DF41A
			protected override InputChannel OnCreateChannel()
			{
				return new DatagramAdapter.InputDatagramAdapterChannel(this.listener);
			}

			// Token: 0x040049D7 RID: 18903
			internal DatagramAdapter.InputDatagramAdapterListener listener;
		}

		// Token: 0x02000E17 RID: 3607
		private class InputDatagramAdapterChannel : InputChannel
		{
			// Token: 0x060081E2 RID: 33250 RVA: 0x001E1227 File Offset: 0x001DF427
			internal InputDatagramAdapterChannel(DatagramAdapter.InputDatagramAdapterListener listener) : base(listener, null)
			{
				this.listener = listener;
			}

			// Token: 0x060081E3 RID: 33251 RVA: 0x001E1238 File Offset: 0x001DF438
			public override T GetProperty<T>()
			{
				object thisLock = this.listener.ThisLock;
				T result;
				lock (thisLock)
				{
					if (this.listener.Channels.Count > 0)
					{
						result = this.listener.Channels[0].GetProperty<T>();
					}
					else
					{
						result = default(T);
					}
				}
				return result;
			}

			// Token: 0x060081E4 RID: 33252 RVA: 0x001E12B0 File Offset: 0x001DF4B0
			protected override void OnOpening()
			{
				this.listener.IncrementUsageCount();
				base.OnOpening();
			}

			// Token: 0x060081E5 RID: 33253 RVA: 0x001E12C3 File Offset: 0x001DF4C3
			protected override void OnClosed()
			{
				base.OnClosed();
				this.listener.DecrementUsageCount();
			}

			// Token: 0x040049D8 RID: 18904
			private DatagramAdapter.InputDatagramAdapterListener listener;
		}

		// Token: 0x02000E18 RID: 3608
		private class ReplyDatagramAdapterListener : DatagramAdapter.DatagramAdapterListenerBase<IReplyChannel, IReplySessionChannel, RequestContext>
		{
			// Token: 0x060081E6 RID: 33254 RVA: 0x001E12D6 File Offset: 0x001DF4D6
			internal ReplyDatagramAdapterListener(IChannelListener<IReplySessionChannel> listener, ServiceThrottle throttle, IDefaultCommunicationTimeouts timeouts) : base(listener, throttle, timeouts)
			{
				this.acceptor = new DatagramAdapter.ReplyDatagramAdapterAcceptor(this);
				base.Acceptor = this.acceptor;
			}

			// Token: 0x060081E7 RID: 33255 RVA: 0x001E12F9 File Offset: 0x001DF4F9
			protected override IAsyncResult CallBeginReceive(IReplySessionChannel channel, AsyncCallback callback, object state)
			{
				return channel.BeginReceiveRequest(TimeSpan.MaxValue, callback, state);
			}

			// Token: 0x060081E8 RID: 33256 RVA: 0x001E1308 File Offset: 0x001DF508
			protected override RequestContext CallEndReceive(IReplySessionChannel channel, IAsyncResult result)
			{
				return channel.EndReceiveRequest(result);
			}

			// Token: 0x060081E9 RID: 33257 RVA: 0x001E1311 File Offset: 0x001DF511
			protected override void Enqueue(RequestContext request, Action callback)
			{
				this.acceptor.Enqueue(request, callback);
			}

			// Token: 0x060081EA RID: 33258 RVA: 0x001E1320 File Offset: 0x001DF520
			protected override void Enqueue(Exception exception, Action callback)
			{
				this.acceptor.Enqueue(exception, callback);
			}

			// Token: 0x040049D9 RID: 18905
			private SingletonChannelAcceptor<IReplyChannel, ReplyChannel, RequestContext> acceptor;
		}

		// Token: 0x02000E19 RID: 3609
		private class ReplyDatagramAdapterAcceptor : ReplyChannelAcceptor
		{
			// Token: 0x060081EB RID: 33259 RVA: 0x001E132F File Offset: 0x001DF52F
			internal ReplyDatagramAdapterAcceptor(DatagramAdapter.ReplyDatagramAdapterListener listener) : base(listener)
			{
				this.listener = listener;
			}

			// Token: 0x060081EC RID: 33260 RVA: 0x001E133F File Offset: 0x001DF53F
			protected override ReplyChannel OnCreateChannel()
			{
				return new DatagramAdapter.ReplyDatagramAdapterChannel(this.listener);
			}

			// Token: 0x040049DA RID: 18906
			internal DatagramAdapter.ReplyDatagramAdapterListener listener;
		}

		// Token: 0x02000E1A RID: 3610
		private class ReplyDatagramAdapterChannel : ReplyChannel
		{
			// Token: 0x060081ED RID: 33261 RVA: 0x001E134C File Offset: 0x001DF54C
			internal ReplyDatagramAdapterChannel(DatagramAdapter.ReplyDatagramAdapterListener listener) : base(listener, null)
			{
				this.listener = listener;
			}

			// Token: 0x060081EE RID: 33262 RVA: 0x001E1360 File Offset: 0x001DF560
			public override T GetProperty<T>()
			{
				object thisLock = this.listener.ThisLock;
				T result;
				lock (thisLock)
				{
					if (this.listener.Channels.Count > 0)
					{
						result = this.listener.Channels[0].GetProperty<T>();
					}
					else
					{
						result = default(T);
					}
				}
				return result;
			}

			// Token: 0x060081EF RID: 33263 RVA: 0x001E13D8 File Offset: 0x001DF5D8
			protected override void OnOpening()
			{
				this.listener.IncrementUsageCount();
				base.OnOpening();
			}

			// Token: 0x060081F0 RID: 33264 RVA: 0x001E13EB File Offset: 0x001DF5EB
			protected override void OnClosed()
			{
				base.OnClosed();
				this.listener.DecrementUsageCount();
			}

			// Token: 0x040049DB RID: 18907
			private DatagramAdapter.ReplyDatagramAdapterListener listener;
		}

		// Token: 0x02000E1B RID: 3611
		private abstract class DatagramAdapterChannelBase<TSessionChannel> : CommunicationObject, IChannel, ICommunicationObject where TSessionChannel : class, IChannel
		{
			// Token: 0x060081F1 RID: 33265 RVA: 0x001E1400 File Offset: 0x001DF600
			protected DatagramAdapterChannelBase(DatagramAdapter.Source<TSessionChannel> channelSource, IDefaultCommunicationTimeouts timeouts)
			{
				if (channelSource == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channelSource");
				}
				this.channelParameters = new ChannelParameterCollection(this);
				this.channelSource = channelSource;
				this.defaultCloseTimeout = timeouts.CloseTimeout;
				this.defaultOpenTimeout = timeouts.OpenTimeout;
				this.defaultSendTimeout = timeouts.SendTimeout;
				this.activeChannels = new List<TSessionChannel>();
			}

			// Token: 0x17001CA6 RID: 7334
			// (get) Token: 0x060081F2 RID: 33266 RVA: 0x001E1468 File Offset: 0x001DF668
			protected ChannelParameterCollection ChannelParameters
			{
				get
				{
					return this.channelParameters;
				}
			}

			// Token: 0x17001CA7 RID: 7335
			// (get) Token: 0x060081F3 RID: 33267 RVA: 0x001E1470 File Offset: 0x001DF670
			protected override TimeSpan DefaultCloseTimeout
			{
				get
				{
					return this.defaultCloseTimeout;
				}
			}

			// Token: 0x17001CA8 RID: 7336
			// (get) Token: 0x060081F4 RID: 33268 RVA: 0x001E1478 File Offset: 0x001DF678
			protected override TimeSpan DefaultOpenTimeout
			{
				get
				{
					return this.defaultOpenTimeout;
				}
			}

			// Token: 0x17001CA9 RID: 7337
			// (get) Token: 0x060081F5 RID: 33269 RVA: 0x001E1480 File Offset: 0x001DF680
			protected TimeSpan DefaultSendTimeout
			{
				get
				{
					return this.defaultSendTimeout;
				}
			}

			// Token: 0x060081F6 RID: 33270 RVA: 0x001E1488 File Offset: 0x001DF688
			protected override void OnOpen(TimeSpan timeout)
			{
			}

			// Token: 0x060081F7 RID: 33271 RVA: 0x001E148A File Offset: 0x001DF68A
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x060081F8 RID: 33272 RVA: 0x001E1493 File Offset: 0x001DF693
			protected override void OnEndOpen(IAsyncResult result)
			{
				CompletedAsyncResult.End(result);
			}

			// Token: 0x060081F9 RID: 33273 RVA: 0x001E149C File Offset: 0x001DF69C
			protected TSessionChannel TakeChannel()
			{
				object thisLock = base.ThisLock;
				TSessionChannel tsessionChannel;
				lock (thisLock)
				{
					base.ThrowIfDisposedOrNotOpen();
					if (this.channel == null)
					{
						tsessionChannel = this.channelSource();
					}
					else
					{
						tsessionChannel = this.channel;
						this.channel = default(TSessionChannel);
					}
					this.activeChannels.Add(tsessionChannel);
				}
				return tsessionChannel;
			}

			// Token: 0x060081FA RID: 33274 RVA: 0x001E1518 File Offset: 0x001DF718
			protected bool ReturnChannel(TSessionChannel channel)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					if (this.channel == null)
					{
						this.activeChannels.Remove(channel);
						this.channel = channel;
						return true;
					}
				}
				return false;
			}

			// Token: 0x060081FB RID: 33275 RVA: 0x001E157C File Offset: 0x001DF77C
			protected void RemoveChannel(TSessionChannel channel)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.activeChannels.Remove(channel);
				}
			}

			// Token: 0x060081FC RID: 33276 RVA: 0x001E15C4 File Offset: 0x001DF7C4
			public T GetProperty<T>() where T : class
			{
				if (typeof(T) == typeof(ChannelParameterCollection))
				{
					return (T)((object)this.channelParameters);
				}
				TSessionChannel tsessionChannel = this.channelSource();
				tsessionChannel.Abort();
				return tsessionChannel.GetProperty<T>();
			}

			// Token: 0x060081FD RID: 33277 RVA: 0x001E161C File Offset: 0x001DF81C
			protected override void OnAbort()
			{
				object thisLock = base.ThisLock;
				TSessionChannel tsessionChannel;
				TSessionChannel[] array;
				lock (thisLock)
				{
					tsessionChannel = this.channel;
					array = new TSessionChannel[this.activeChannels.Count];
					this.activeChannels.CopyTo(array);
				}
				if (tsessionChannel != null)
				{
					tsessionChannel.Abort();
				}
				foreach (TSessionChannel tsessionChannel2 in array)
				{
					tsessionChannel2.Abort();
				}
			}

			// Token: 0x060081FE RID: 33278 RVA: 0x001E16BC File Offset: 0x001DF8BC
			protected override void OnClose(TimeSpan timeout)
			{
				object thisLock = base.ThisLock;
				TSessionChannel tsessionChannel;
				TSessionChannel[] array;
				lock (thisLock)
				{
					tsessionChannel = this.channel;
					array = new TSessionChannel[this.activeChannels.Count];
					this.activeChannels.CopyTo(array);
				}
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (tsessionChannel != null)
				{
					tsessionChannel.Close(timeoutHelper.RemainingTime());
				}
				foreach (TSessionChannel tsessionChannel2 in array)
				{
					tsessionChannel2.Close(timeoutHelper.RemainingTime());
				}
			}

			// Token: 0x060081FF RID: 33279 RVA: 0x001E1774 File Offset: 0x001DF974
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				object thisLock = base.ThisLock;
				TSessionChannel tsessionChannel;
				TSessionChannel[] array;
				lock (thisLock)
				{
					tsessionChannel = this.channel;
					array = new TSessionChannel[this.activeChannels.Count];
					this.activeChannels.CopyTo(array);
				}
				if (this.channel == null)
				{
					return new CloseCollectionAsyncResult(timeout, callback, state, array);
				}
				ChainedBeginHandler begin = new ChainedBeginHandler(tsessionChannel.BeginClose);
				ChainedEndHandler end = new ChainedEndHandler(tsessionChannel.EndClose);
				ICommunicationObject[] objs = array;
				return new ChainedCloseAsyncResult(timeout, callback, state, begin, end, objs);
			}

			// Token: 0x06008200 RID: 33280 RVA: 0x001E181C File Offset: 0x001DFA1C
			protected override void OnEndClose(IAsyncResult result)
			{
				if (result is CloseCollectionAsyncResult)
				{
					CloseCollectionAsyncResult.End(result);
					return;
				}
				ChainedAsyncResult.End(result);
			}

			// Token: 0x040049DC RID: 18908
			private ChannelParameterCollection channelParameters;

			// Token: 0x040049DD RID: 18909
			private DatagramAdapter.Source<TSessionChannel> channelSource;

			// Token: 0x040049DE RID: 18910
			private TSessionChannel channel;

			// Token: 0x040049DF RID: 18911
			private TimeSpan defaultCloseTimeout;

			// Token: 0x040049E0 RID: 18912
			private TimeSpan defaultOpenTimeout;

			// Token: 0x040049E1 RID: 18913
			private TimeSpan defaultSendTimeout;

			// Token: 0x040049E2 RID: 18914
			private List<TSessionChannel> activeChannels;
		}

		// Token: 0x02000E1C RID: 3612
		private class OutputDatagramAdapterChannel : DatagramAdapter.DatagramAdapterChannelBase<IOutputSessionChannel>, IOutputChannel, IChannel, ICommunicationObject
		{
			// Token: 0x06008201 RID: 33281 RVA: 0x001E1834 File Offset: 0x001DFA34
			internal OutputDatagramAdapterChannel(DatagramAdapter.Source<IOutputSessionChannel> channelSource, IDefaultCommunicationTimeouts timeouts) : base(channelSource, timeouts)
			{
				IOutputSessionChannel outputSessionChannel = channelSource();
				try
				{
					this.remoteAddress = outputSessionChannel.RemoteAddress;
					this.via = outputSessionChannel.Via;
					outputSessionChannel.Close();
				}
				finally
				{
					outputSessionChannel.Abort();
				}
			}

			// Token: 0x17001CAA RID: 7338
			// (get) Token: 0x06008202 RID: 33282 RVA: 0x001E188C File Offset: 0x001DFA8C
			public EndpointAddress RemoteAddress
			{
				get
				{
					return this.remoteAddress;
				}
			}

			// Token: 0x17001CAB RID: 7339
			// (get) Token: 0x06008203 RID: 33283 RVA: 0x001E1894 File Offset: 0x001DFA94
			public Uri Via
			{
				get
				{
					return this.via;
				}
			}

			// Token: 0x06008204 RID: 33284 RVA: 0x001E189C File Offset: 0x001DFA9C
			public void Send(Message message)
			{
				this.Send(message, base.DefaultSendTimeout);
			}

			// Token: 0x06008205 RID: 33285 RVA: 0x001E18AC File Offset: 0x001DFAAC
			public void Send(Message message, TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				IOutputSessionChannel outputSessionChannel = base.TakeChannel();
				bool flag = true;
				try
				{
					if (outputSessionChannel.State == CommunicationState.Created)
					{
						base.ChannelParameters.PropagateChannelParameters(outputSessionChannel);
						outputSessionChannel.Open(timeoutHelper.RemainingTime());
					}
					outputSessionChannel.Send(message, timeoutHelper.RemainingTime());
					flag = false;
				}
				finally
				{
					if (flag)
					{
						outputSessionChannel.Abort();
						base.RemoveChannel(outputSessionChannel);
					}
				}
				if (base.ReturnChannel(outputSessionChannel))
				{
					return;
				}
				try
				{
					outputSessionChannel.Close(timeoutHelper.RemainingTime());
				}
				finally
				{
					base.RemoveChannel(outputSessionChannel);
				}
			}

			// Token: 0x06008206 RID: 33286 RVA: 0x001E194C File Offset: 0x001DFB4C
			public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
			{
				return this.BeginSend(message, base.DefaultSendTimeout, callback, state);
			}

			// Token: 0x06008207 RID: 33287 RVA: 0x001E195D File Offset: 0x001DFB5D
			public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new DatagramAdapter.OutputDatagramAdapterChannel.SendAsyncResult(this, message, timeout, callback, state);
			}

			// Token: 0x06008208 RID: 33288 RVA: 0x001E196A File Offset: 0x001DFB6A
			public void EndSend(IAsyncResult result)
			{
				DatagramAdapter.OutputDatagramAdapterChannel.SendAsyncResult.End(result);
			}

			// Token: 0x040049E3 RID: 18915
			private EndpointAddress remoteAddress;

			// Token: 0x040049E4 RID: 18916
			private Uri via;

			// Token: 0x02000F83 RID: 3971
			private class SendAsyncResult : AsyncResult
			{
				// Token: 0x0600881D RID: 34845 RVA: 0x001FA0E0 File Offset: 0x001F82E0
				public SendAsyncResult(DatagramAdapter.OutputDatagramAdapterChannel adapter, Message message, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.adapter = adapter;
					this.message = message;
					this.timeoutHelper = new TimeoutHelper(timeout);
					IOutputSessionChannel outputSessionChannel = this.adapter.TakeChannel();
					try
					{
						if (outputSessionChannel.State == CommunicationState.Created)
						{
							this.adapter.ChannelParameters.PropagateChannelParameters(outputSessionChannel);
							outputSessionChannel.BeginOpen(this.timeoutHelper.RemainingTime(), Fx.ThunkCallback(new AsyncCallback(this.OnOpenComplete)), outputSessionChannel);
						}
						else
						{
							outputSessionChannel.BeginSend(message, this.timeoutHelper.RemainingTime(), Fx.ThunkCallback(new AsyncCallback(this.OnSendComplete)), outputSessionChannel);
						}
					}
					catch
					{
						outputSessionChannel.Abort();
						this.adapter.RemoveChannel(outputSessionChannel);
						throw;
					}
				}

				// Token: 0x0600881E RID: 34846 RVA: 0x001FA1B0 File Offset: 0x001F83B0
				private void OnOpenComplete(IAsyncResult result)
				{
					this.hasCompletedAsynchronously &= result.CompletedSynchronously;
					IOutputSessionChannel outputSessionChannel = (IOutputSessionChannel)result.AsyncState;
					try
					{
						outputSessionChannel.EndOpen(result);
						outputSessionChannel.BeginSend(this.message, this.timeoutHelper.RemainingTime(), Fx.ThunkCallback(new AsyncCallback(this.OnSendComplete)), outputSessionChannel);
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
						outputSessionChannel.Abort();
						this.adapter.RemoveChannel(outputSessionChannel);
						base.Complete(this.hasCompletedAsynchronously, exception);
					}
				}

				// Token: 0x0600881F RID: 34847 RVA: 0x001FA24C File Offset: 0x001F844C
				private void OnSendComplete(IAsyncResult result)
				{
					this.hasCompletedAsynchronously &= result.CompletedSynchronously;
					IOutputSessionChannel outputSessionChannel = (IOutputSessionChannel)result.AsyncState;
					try
					{
						outputSessionChannel.EndSend(result);
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
						outputSessionChannel.Abort();
						this.adapter.RemoveChannel(outputSessionChannel);
						base.Complete(this.hasCompletedAsynchronously, exception);
						return;
					}
					if (this.adapter.ReturnChannel(outputSessionChannel))
					{
						base.Complete(this.hasCompletedAsynchronously);
						return;
					}
					try
					{
						outputSessionChannel.BeginClose(this.timeoutHelper.RemainingTime(), Fx.ThunkCallback(new AsyncCallback(this.OnCloseComplete)), outputSessionChannel);
					}
					catch (Exception exception2)
					{
						if (Fx.IsFatal(exception2))
						{
							throw;
						}
						this.adapter.RemoveChannel(outputSessionChannel);
						base.Complete(this.hasCompletedAsynchronously, exception2);
					}
				}

				// Token: 0x06008820 RID: 34848 RVA: 0x001FA334 File Offset: 0x001F8534
				private void OnCloseComplete(IAsyncResult result)
				{
					this.hasCompletedAsynchronously &= result.CompletedSynchronously;
					IOutputSessionChannel outputSessionChannel = (IOutputSessionChannel)result.AsyncState;
					Exception exception = null;
					try
					{
						outputSessionChannel.EndClose(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					this.adapter.RemoveChannel(outputSessionChannel);
					base.Complete(this.hasCompletedAsynchronously, exception);
				}

				// Token: 0x06008821 RID: 34849 RVA: 0x001FA3A4 File Offset: 0x001F85A4
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<DatagramAdapter.OutputDatagramAdapterChannel.SendAsyncResult>(result);
				}

				// Token: 0x04004F66 RID: 20326
				private DatagramAdapter.OutputDatagramAdapterChannel adapter;

				// Token: 0x04004F67 RID: 20327
				private Message message;

				// Token: 0x04004F68 RID: 20328
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004F69 RID: 20329
				private bool hasCompletedAsynchronously = true;
			}
		}

		// Token: 0x02000E1D RID: 3613
		private class RequestDatagramAdapterChannel : DatagramAdapter.DatagramAdapterChannelBase<IRequestSessionChannel>, IRequestChannel, IChannel, ICommunicationObject
		{
			// Token: 0x06008209 RID: 33289 RVA: 0x001E1974 File Offset: 0x001DFB74
			internal RequestDatagramAdapterChannel(DatagramAdapter.Source<IRequestSessionChannel> channelSource, IDefaultCommunicationTimeouts timeouts) : base(channelSource, timeouts)
			{
				IRequestSessionChannel requestSessionChannel = channelSource();
				try
				{
					this.remoteAddress = requestSessionChannel.RemoteAddress;
					this.via = requestSessionChannel.Via;
					requestSessionChannel.Close();
				}
				finally
				{
					requestSessionChannel.Abort();
				}
			}

			// Token: 0x17001CAC RID: 7340
			// (get) Token: 0x0600820A RID: 33290 RVA: 0x001E19CC File Offset: 0x001DFBCC
			public EndpointAddress RemoteAddress
			{
				get
				{
					return this.remoteAddress;
				}
			}

			// Token: 0x17001CAD RID: 7341
			// (get) Token: 0x0600820B RID: 33291 RVA: 0x001E19D4 File Offset: 0x001DFBD4
			public Uri Via
			{
				get
				{
					return this.via;
				}
			}

			// Token: 0x0600820C RID: 33292 RVA: 0x001E19DC File Offset: 0x001DFBDC
			public Message Request(Message request)
			{
				return this.Request(request, base.DefaultSendTimeout);
			}

			// Token: 0x0600820D RID: 33293 RVA: 0x001E19EC File Offset: 0x001DFBEC
			public Message Request(Message request, TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				IRequestSessionChannel requestSessionChannel = base.TakeChannel();
				bool flag = true;
				Message result = null;
				try
				{
					if (requestSessionChannel.State == CommunicationState.Created)
					{
						base.ChannelParameters.PropagateChannelParameters(requestSessionChannel);
						requestSessionChannel.Open(timeoutHelper.RemainingTime());
					}
					result = requestSessionChannel.Request(request, timeoutHelper.RemainingTime());
					flag = false;
				}
				finally
				{
					if (flag)
					{
						requestSessionChannel.Abort();
						base.RemoveChannel(requestSessionChannel);
					}
				}
				if (base.ReturnChannel(requestSessionChannel))
				{
					return result;
				}
				try
				{
					requestSessionChannel.Close(timeoutHelper.RemainingTime());
				}
				finally
				{
					base.RemoveChannel(requestSessionChannel);
				}
				return result;
			}

			// Token: 0x0600820E RID: 33294 RVA: 0x001E1A94 File Offset: 0x001DFC94
			public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state)
			{
				return this.BeginRequest(message, base.DefaultSendTimeout, callback, state);
			}

			// Token: 0x0600820F RID: 33295 RVA: 0x001E1AA5 File Offset: 0x001DFCA5
			public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new DatagramAdapter.RequestDatagramAdapterChannel.RequestAsyncResult(this, message, timeout, callback, state);
			}

			// Token: 0x06008210 RID: 33296 RVA: 0x001E1AB2 File Offset: 0x001DFCB2
			public Message EndRequest(IAsyncResult result)
			{
				return DatagramAdapter.RequestDatagramAdapterChannel.RequestAsyncResult.End(result);
			}

			// Token: 0x040049E5 RID: 18917
			private EndpointAddress remoteAddress;

			// Token: 0x040049E6 RID: 18918
			private Uri via;

			// Token: 0x02000F84 RID: 3972
			private class RequestAsyncResult : AsyncResult
			{
				// Token: 0x06008822 RID: 34850 RVA: 0x001FA3B0 File Offset: 0x001F85B0
				public RequestAsyncResult(DatagramAdapter.RequestDatagramAdapterChannel adapter, Message message, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.adapter = adapter;
					this.message = message;
					this.timeoutHelper = new TimeoutHelper(timeout);
					IRequestSessionChannel requestSessionChannel = this.adapter.TakeChannel();
					try
					{
						if (requestSessionChannel.State == CommunicationState.Created)
						{
							this.adapter.ChannelParameters.PropagateChannelParameters(requestSessionChannel);
							requestSessionChannel.BeginOpen(this.timeoutHelper.RemainingTime(), Fx.ThunkCallback(new AsyncCallback(this.OnOpenComplete)), requestSessionChannel);
						}
						else
						{
							requestSessionChannel.BeginRequest(message, this.timeoutHelper.RemainingTime(), Fx.ThunkCallback(new AsyncCallback(this.OnRequestComplete)), requestSessionChannel);
						}
					}
					catch
					{
						requestSessionChannel.Abort();
						this.adapter.RemoveChannel(requestSessionChannel);
						throw;
					}
				}

				// Token: 0x06008823 RID: 34851 RVA: 0x001FA480 File Offset: 0x001F8680
				private void OnOpenComplete(IAsyncResult result)
				{
					this.hasCompletedAsynchronously &= result.CompletedSynchronously;
					IRequestSessionChannel requestSessionChannel = (IRequestSessionChannel)result.AsyncState;
					try
					{
						requestSessionChannel.EndOpen(result);
						requestSessionChannel.BeginRequest(this.message, this.timeoutHelper.RemainingTime(), Fx.ThunkCallback(new AsyncCallback(this.OnRequestComplete)), requestSessionChannel);
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
						requestSessionChannel.Abort();
						this.adapter.RemoveChannel(requestSessionChannel);
						base.Complete(this.hasCompletedAsynchronously, exception);
					}
				}

				// Token: 0x06008824 RID: 34852 RVA: 0x001FA51C File Offset: 0x001F871C
				private void OnRequestComplete(IAsyncResult result)
				{
					this.hasCompletedAsynchronously &= result.CompletedSynchronously;
					IRequestSessionChannel requestSessionChannel = (IRequestSessionChannel)result.AsyncState;
					try
					{
						this.reply = requestSessionChannel.EndRequest(result);
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
						requestSessionChannel.Abort();
						this.adapter.RemoveChannel(requestSessionChannel);
						base.Complete(this.hasCompletedAsynchronously, exception);
						return;
					}
					if (this.adapter.ReturnChannel(requestSessionChannel))
					{
						base.Complete(this.hasCompletedAsynchronously);
						return;
					}
					try
					{
						requestSessionChannel.BeginClose(this.timeoutHelper.RemainingTime(), Fx.ThunkCallback(new AsyncCallback(this.OnCloseComplete)), requestSessionChannel);
					}
					catch (Exception exception2)
					{
						if (Fx.IsFatal(exception2))
						{
							throw;
						}
						this.adapter.RemoveChannel(requestSessionChannel);
						base.Complete(this.hasCompletedAsynchronously, exception2);
					}
				}

				// Token: 0x06008825 RID: 34853 RVA: 0x001FA608 File Offset: 0x001F8808
				private void OnCloseComplete(IAsyncResult result)
				{
					this.hasCompletedAsynchronously &= result.CompletedSynchronously;
					IRequestSessionChannel requestSessionChannel = (IRequestSessionChannel)result.AsyncState;
					Exception exception = null;
					try
					{
						requestSessionChannel.EndClose(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
						exception = ex;
					}
					this.adapter.RemoveChannel(requestSessionChannel);
					base.Complete(this.hasCompletedAsynchronously, exception);
				}

				// Token: 0x06008826 RID: 34854 RVA: 0x001FA678 File Offset: 0x001F8878
				public static Message End(IAsyncResult result)
				{
					DatagramAdapter.RequestDatagramAdapterChannel.RequestAsyncResult requestAsyncResult = AsyncResult.End<DatagramAdapter.RequestDatagramAdapterChannel.RequestAsyncResult>(result);
					return requestAsyncResult.reply;
				}

				// Token: 0x04004F6A RID: 20330
				private DatagramAdapter.RequestDatagramAdapterChannel adapter;

				// Token: 0x04004F6B RID: 20331
				private Message message;

				// Token: 0x04004F6C RID: 20332
				private Message reply;

				// Token: 0x04004F6D RID: 20333
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004F6E RID: 20334
				private bool hasCompletedAsynchronously = true;
			}
		}
	}
}
