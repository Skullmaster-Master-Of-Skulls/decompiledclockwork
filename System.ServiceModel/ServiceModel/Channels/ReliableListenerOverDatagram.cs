using System;
using System.Runtime;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200091B RID: 2331
	internal abstract class ReliableListenerOverDatagram<TChannel, TReliableChannel, TInnerChannel, TItem> : ReliableChannelListener<TChannel, TReliableChannel, TInnerChannel> where TChannel : class, IChannel where TReliableChannel : class, IChannel where TInnerChannel : class, IChannel where TItem : class, IDisposable
	{
		// Token: 0x0600596A RID: 22890 RVA: 0x00146F80 File Offset: 0x00145180
		protected ReliableListenerOverDatagram(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context)
		{
			this.asyncHandleReceiveComplete = new Action<object>(this.AsyncHandleReceiveComplete);
			this.onTryReceiveComplete = Fx.ThunkCallback(new AsyncCallback(this.OnTryReceiveComplete));
			this.channelTracker = new ChannelTracker<TInnerChannel, object>();
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x00146FC0 File Offset: 0x001451C0
		private void AsyncHandleReceiveComplete(object state)
		{
			try
			{
				IAsyncResult asyncResult = (IAsyncResult)state;
				TInnerChannel tinnerChannel = (TInnerChannel)((object)asyncResult.AsyncState);
				TItem titem = default(TItem);
				try
				{
					this.EndTryReceiveItem(tinnerChannel, asyncResult, out titem);
					if (titem == null)
					{
						return;
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!base.HandleException(ex, tinnerChannel))
					{
						tinnerChannel.Abort();
						return;
					}
				}
				if (titem != null && this.HandleReceiveComplete(titem, tinnerChannel))
				{
					this.StartReceiving(tinnerChannel, true);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				base.Fault(exception);
			}
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x00147078 File Offset: 0x00145278
		private bool BeginProcessItem(TItem item, WsrmMessageInfo info, TInnerChannel channel, out TReliableChannel reliableChannel, out bool newChannel, out bool dispatch)
		{
			dispatch = false;
			reliableChannel = default(TReliableChannel);
			newChannel = false;
			Message message;
			if (info.FaultReply != null)
			{
				message = info.FaultReply;
			}
			else if (info.CreateSequenceInfo == null)
			{
				UniqueId uniqueId;
				reliableChannel = base.GetChannel(info, out uniqueId);
				if (reliableChannel != null)
				{
					return true;
				}
				if (uniqueId == null)
				{
					this.DisposeItem(item);
					return true;
				}
				message = new UnknownSequenceFault(uniqueId).CreateMessage(base.MessageVersion, base.ReliableMessagingVersion);
			}
			else
			{
				reliableChannel = base.ProcessCreateSequence(info, channel, out dispatch, out newChannel);
				if (reliableChannel != null)
				{
					return true;
				}
				message = info.FaultReply;
			}
			try
			{
				this.SendReply(message, channel, item);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (!base.HandleException(ex, channel))
				{
					channel.Abort();
					return false;
				}
			}
			finally
			{
				message.Close();
				this.DisposeItem(item);
			}
			return true;
		}

		// Token: 0x0600596D RID: 22893
		protected abstract IAsyncResult BeginTryReceiveItem(TInnerChannel channel, AsyncCallback callback, object state);

		// Token: 0x0600596E RID: 22894
		protected abstract void DisposeItem(TItem item);

		// Token: 0x0600596F RID: 22895
		protected abstract void EndTryReceiveItem(TInnerChannel channel, IAsyncResult result, out TItem item);

		// Token: 0x06005970 RID: 22896 RVA: 0x00147188 File Offset: 0x00145388
		private void EndProcessItem(TItem item, WsrmMessageInfo info, TReliableChannel channel, bool dispatch)
		{
			this.ProcessSequencedItem(channel, item, info);
			if (dispatch)
			{
				base.Dispatch();
			}
		}

		// Token: 0x06005971 RID: 22897
		protected abstract Message GetMessage(TItem item);

		// Token: 0x06005972 RID: 22898 RVA: 0x001471A0 File Offset: 0x001453A0
		private bool HandleReceiveComplete(TItem item, TInnerChannel channel)
		{
			Message message = null;
			try
			{
				message = this.GetMessage(item);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (!base.HandleException(ex, this))
				{
					throw;
				}
				item.Dispose();
				return true;
			}
			WsrmMessageInfo wsrmMessageInfo = WsrmMessageInfo.Get(base.MessageVersion, base.ReliableMessagingVersion, channel, null, message);
			if (wsrmMessageInfo.ParsingException != null)
			{
				this.DisposeItem(item);
				return true;
			}
			TReliableChannel treliableChannel;
			bool flag;
			bool flag2;
			if (!this.BeginProcessItem(item, wsrmMessageInfo, channel, out treliableChannel, out flag, out flag2))
			{
				return false;
			}
			if (treliableChannel == null)
			{
				this.DisposeItem(item);
				return true;
			}
			if (flag2 || !flag)
			{
				this.StartReceiving(channel, false);
				this.EndProcessItem(item, wsrmMessageInfo, treliableChannel, flag2);
				return false;
			}
			this.EndProcessItem(item, wsrmMessageInfo, treliableChannel, flag2);
			return true;
		}

		// Token: 0x06005973 RID: 22899 RVA: 0x00147270 File Offset: 0x00145470
		private void OnTryReceiveComplete(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				try
				{
					TInnerChannel tinnerChannel = (TInnerChannel)((object)result.AsyncState);
					TItem titem = default(TItem);
					try
					{
						this.EndTryReceiveItem(tinnerChannel, result, out titem);
						if (titem == null)
						{
							return;
						}
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						if (!base.HandleException(ex, tinnerChannel))
						{
							tinnerChannel.Abort();
							return;
						}
					}
					if (titem != null && this.HandleReceiveComplete(titem, tinnerChannel))
					{
						this.StartReceiving(tinnerChannel, true);
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					base.Fault(exception);
				}
			}
		}

		// Token: 0x06005974 RID: 22900 RVA: 0x00147328 File Offset: 0x00145528
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(this.channelTracker.BeginOpen), new ChainedEndHandler(this.channelTracker.EndOpen), new ChainedBeginHandler(base.OnBeginOpen), new ChainedEndHandler(base.OnEndOpen));
		}

		// Token: 0x06005975 RID: 22901 RVA: 0x00147379 File Offset: 0x00145579
		protected override void OnEndOpen(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06005976 RID: 22902 RVA: 0x00147384 File Offset: 0x00145584
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.channelTracker.Open(timeoutHelper.RemainingTime());
			base.OnOpen(timeoutHelper.RemainingTime());
		}

		// Token: 0x06005977 RID: 22903 RVA: 0x001473B8 File Offset: 0x001455B8
		protected override void OnInnerChannelAccepted(TInnerChannel channel)
		{
			base.OnInnerChannelAccepted(channel);
			this.channelTracker.PrepareChannel(channel);
		}

		// Token: 0x06005978 RID: 22904 RVA: 0x001473D0 File Offset: 0x001455D0
		protected override void ProcessChannel(TInnerChannel channel)
		{
			try
			{
				this.channelTracker.Add(channel, null);
				this.StartReceiving(channel, false);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				base.Fault(exception);
			}
		}

		// Token: 0x06005979 RID: 22905 RVA: 0x00147418 File Offset: 0x00145618
		protected override void AbortInnerListener()
		{
			base.AbortInnerListener();
			this.channelTracker.Abort();
		}

		// Token: 0x0600597A RID: 22906 RVA: 0x0014742C File Offset: 0x0014562C
		protected override void CloseInnerListener(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.CloseInnerListener(timeoutHelper.RemainingTime());
			this.channelTracker.Close(timeoutHelper.RemainingTime());
		}

		// Token: 0x0600597B RID: 22907 RVA: 0x00147460 File Offset: 0x00145660
		protected override IAsyncResult BeginCloseInnerListener(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.BeginCloseInnerListener), new ChainedEndHandler(base.EndCloseInnerListener), new ChainedBeginHandler(this.channelTracker.BeginClose), new ChainedEndHandler(this.channelTracker.EndClose));
		}

		// Token: 0x0600597C RID: 22908 RVA: 0x001474B1 File Offset: 0x001456B1
		protected override void EndCloseInnerListener(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x0600597D RID: 22909
		protected abstract void ProcessSequencedItem(TReliableChannel reliableChannel, TItem item, WsrmMessageInfo info);

		// Token: 0x0600597E RID: 22910
		protected abstract void SendReply(Message reply, TInnerChannel channel, TItem item);

		// Token: 0x0600597F RID: 22911 RVA: 0x001474BC File Offset: 0x001456BC
		private void StartReceiving(TInnerChannel channel, bool canBlock)
		{
			TItem titem;
			do
			{
				titem = default(TItem);
				try
				{
					IAsyncResult asyncResult = this.BeginTryReceiveItem(channel, this.onTryReceiveComplete, channel);
					if (!asyncResult.CompletedSynchronously)
					{
						break;
					}
					if (!canBlock)
					{
						ActionItem.Schedule(this.asyncHandleReceiveComplete, asyncResult);
						break;
					}
					this.EndTryReceiveItem(channel, asyncResult, out titem);
					if (titem == null)
					{
						break;
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!base.HandleException(ex, channel))
					{
						channel.Abort();
						break;
					}
				}
			}
			while (titem == null || this.HandleReceiveComplete(titem, channel));
		}

		// Token: 0x04003665 RID: 13925
		private Action<object> asyncHandleReceiveComplete;

		// Token: 0x04003666 RID: 13926
		private AsyncCallback onTryReceiveComplete;

		// Token: 0x04003667 RID: 13927
		private ChannelTracker<TInnerChannel, object> channelTracker;
	}
}
