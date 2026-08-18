using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200091E RID: 2334
	internal abstract class ReliableListenerOverSession<TChannel, TReliableChannel, TInnerChannel, TInnerSession, TItem> : ReliableChannelListener<TChannel, TReliableChannel, TInnerChannel> where TChannel : class, IChannel where TReliableChannel : class, IChannel where TInnerChannel : class, IChannel, ISessionChannel<TInnerSession> where TInnerSession : ISession where TItem : IDisposable
	{
		// Token: 0x0600598C RID: 22924 RVA: 0x0014763D File Offset: 0x0014583D
		protected ReliableListenerOverSession(ReliableSessionBindingElement binding, BindingContext context) : base(binding, context)
		{
			this.asyncHandleReceiveComplete = new Action<object>(this.AsyncHandleReceiveComplete);
			this.onReceiveComplete = Fx.ThunkCallback(new AsyncCallback(this.OnReceiveComplete));
		}

		// Token: 0x0600598D RID: 22925 RVA: 0x00147670 File Offset: 0x00145870
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
						tinnerChannel.Close();
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
				if (titem != null)
				{
					this.HandleReceiveComplete(titem, tinnerChannel);
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

		// Token: 0x0600598E RID: 22926
		protected abstract IAsyncResult BeginTryReceiveItem(TInnerChannel channel, AsyncCallback callback, object state);

		// Token: 0x0600598F RID: 22927
		protected abstract void DisposeItem(TItem item);

		// Token: 0x06005990 RID: 22928
		protected abstract void EndTryReceiveItem(TInnerChannel channel, IAsyncResult result, out TItem item);

		// Token: 0x06005991 RID: 22929
		protected abstract Message GetMessage(TItem item);

		// Token: 0x06005992 RID: 22930 RVA: 0x00147728 File Offset: 0x00145928
		private void HandleReceiveComplete(TItem item, TInnerChannel channel)
		{
			WsrmMessageInfo wsrmMessageInfo = WsrmMessageInfo.Get(base.MessageVersion, base.ReliableMessagingVersion, channel, channel.Session as ISecureConversationSession, this.GetMessage(item));
			if (wsrmMessageInfo.ParsingException != null)
			{
				this.DisposeItem(item);
				channel.Abort();
				return;
			}
			TReliableChannel treliableChannel = default(TReliableChannel);
			bool flag = false;
			bool newChannel = false;
			Message message = null;
			if (wsrmMessageInfo.FaultReply != null)
			{
				message = wsrmMessageInfo.FaultReply;
			}
			else if (wsrmMessageInfo.CreateSequenceInfo == null)
			{
				UniqueId uniqueId;
				treliableChannel = base.GetChannel(wsrmMessageInfo, out uniqueId);
				if (treliableChannel == null && uniqueId == null)
				{
					this.DisposeItem(item);
					channel.Abort();
					return;
				}
				if (treliableChannel == null)
				{
					message = new UnknownSequenceFault(uniqueId).CreateMessage(base.MessageVersion, base.ReliableMessagingVersion);
				}
			}
			else
			{
				treliableChannel = base.ProcessCreateSequence(wsrmMessageInfo, channel, out flag, out newChannel);
				if (treliableChannel == null)
				{
					message = wsrmMessageInfo.FaultReply;
				}
			}
			if (treliableChannel != null)
			{
				this.ProcessSequencedItem(channel, item, treliableChannel, wsrmMessageInfo, newChannel);
				if (flag)
				{
					base.Dispatch();
					return;
				}
			}
			else
			{
				try
				{
					this.SendReply(message, channel, item);
					channel.Close();
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
					channel.Abort();
				}
				finally
				{
					message.Close();
					this.DisposeItem(item);
				}
			}
		}

		// Token: 0x06005993 RID: 22931 RVA: 0x001478A0 File Offset: 0x00145AA0
		private void OnReceiveComplete(IAsyncResult result)
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
							tinnerChannel.Close();
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
					if (titem != null)
					{
						this.HandleReceiveComplete(titem, tinnerChannel);
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

		// Token: 0x06005994 RID: 22932 RVA: 0x00147958 File Offset: 0x00145B58
		protected override void ProcessChannel(TInnerChannel channel)
		{
			try
			{
				IAsyncResult asyncResult = this.BeginTryReceiveItem(channel, this.onReceiveComplete, channel);
				if (asyncResult.CompletedSynchronously)
				{
					ActionItem.Schedule(this.asyncHandleReceiveComplete, asyncResult);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
				channel.Abort();
			}
		}

		// Token: 0x06005995 RID: 22933
		protected abstract void ProcessSequencedItem(TInnerChannel channel, TItem item, TReliableChannel reliableChannel, WsrmMessageInfo info, bool newChannel);

		// Token: 0x06005996 RID: 22934
		protected abstract void SendReply(Message reply, TInnerChannel channel, TItem item);

		// Token: 0x04003668 RID: 13928
		private Action<object> asyncHandleReceiveComplete;

		// Token: 0x04003669 RID: 13929
		private AsyncCallback onReceiveComplete;
	}
}
