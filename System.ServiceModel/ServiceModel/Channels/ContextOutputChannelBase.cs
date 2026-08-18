using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007B5 RID: 1973
	internal abstract class ContextOutputChannelBase<TChannel> : LayeredChannel<TChannel> where TChannel : class, IOutputChannel
	{
		// Token: 0x06004A97 RID: 19095 RVA: 0x001120FD File Offset: 0x001102FD
		protected ContextOutputChannelBase(ChannelManagerBase channelManager, TChannel innerChannel) : base(channelManager, innerChannel)
		{
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06004A98 RID: 19096 RVA: 0x00112107 File Offset: 0x00110307
		public EndpointAddress RemoteAddress
		{
			get
			{
				return base.InnerChannel.RemoteAddress;
			}
		}

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06004A99 RID: 19097 RVA: 0x00112119 File Offset: 0x00110319
		public Uri Via
		{
			get
			{
				return base.InnerChannel.Via;
			}
		}

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06004A9A RID: 19098
		protected abstract ContextProtocol ContextProtocol { get; }

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06004A9B RID: 19099
		protected abstract bool IsClient { get; }

		// Token: 0x06004A9C RID: 19100 RVA: 0x0011212B File Offset: 0x0011032B
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ContextOutputChannelBase<TChannel>.SendAsyncResult(message, this, this.ContextProtocol, timeout, callback, state);
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x0011213E File Offset: 0x0011033E
		public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
		{
			return this.BeginSend(message, base.DefaultSendTimeout, callback, state);
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x0011214F File Offset: 0x0011034F
		public void EndSend(IAsyncResult result)
		{
			ContextOutputChannelBase<TChannel>.SendAsyncResult.End(result);
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x00112157 File Offset: 0x00110357
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IContextManager))
			{
				return (T)((object)this.ContextProtocol);
			}
			return base.GetProperty<T>();
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x00112188 File Offset: 0x00110388
		public void Send(Message message, TimeSpan timeout)
		{
			CorrelationCallbackMessageProperty correlationCallbackMessageProperty = null;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			Message message2 = message;
			if (message != null)
			{
				this.ContextProtocol.OnOutgoingMessage(message, null);
				if (CorrelationCallbackMessageProperty.TryGet(message, out correlationCallbackMessageProperty))
				{
					ContextExchangeCorrelationHelper.AddOutgoingCorrelationCallbackData(correlationCallbackMessageProperty, message, this.IsClient);
					if (correlationCallbackMessageProperty.IsFullyDefined)
					{
						message2 = correlationCallbackMessageProperty.FinalizeCorrelation(message, timeoutHelper.RemainingTime());
					}
				}
			}
			try
			{
				base.InnerChannel.Send(message2, timeoutHelper.RemainingTime());
			}
			finally
			{
				if (message != null && message != message2)
				{
					message2.Close();
				}
			}
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x00112218 File Offset: 0x00110418
		public void Send(Message message)
		{
			this.Send(message, base.DefaultSendTimeout);
		}

		// Token: 0x02000CF9 RID: 3321
		private class SendAsyncResult : AsyncResult
		{
			// Token: 0x06007A9B RID: 31387 RVA: 0x001C8998 File Offset: 0x001C6B98
			public SendAsyncResult(Message message, ContextOutputChannelBase<TChannel> channel, ContextProtocol contextProtocol, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.channel = channel;
				this.sendMessage = message;
				this.message = message;
				this.timeoutHelper = new TimeoutHelper(timeout);
				bool flag = true;
				if (message != null)
				{
					contextProtocol.OnOutgoingMessage(message, null);
					if (CorrelationCallbackMessageProperty.TryGet(message, out this.correlationCallback))
					{
						ContextExchangeCorrelationHelper.AddOutgoingCorrelationCallbackData(this.correlationCallback, message, this.channel.IsClient);
						if (this.correlationCallback.IsFullyDefined)
						{
							IAsyncResult asyncResult = this.correlationCallback.BeginFinalizeCorrelation(this.message, this.timeoutHelper.RemainingTime(), ContextOutputChannelBase<TChannel>.SendAsyncResult.onFinalizeCorrelation, this);
							if (asyncResult.CompletedSynchronously && this.OnFinalizeCorrelationCompleted(asyncResult))
							{
								base.Complete(true);
							}
							flag = false;
						}
					}
				}
				if (flag)
				{
					IAsyncResult asyncResult2 = this.channel.InnerChannel.BeginSend(this.message, this.timeoutHelper.RemainingTime(), ContextOutputChannelBase<TChannel>.SendAsyncResult.onSend, this);
					if (asyncResult2.CompletedSynchronously)
					{
						this.OnSendCompleted(asyncResult2);
						base.Complete(true);
					}
				}
			}

			// Token: 0x06007A9C RID: 31388 RVA: 0x001C8A98 File Offset: 0x001C6C98
			public static void End(IAsyncResult result)
			{
				ContextOutputChannelBase<TChannel>.SendAsyncResult sendAsyncResult = AsyncResult.End<ContextOutputChannelBase<TChannel>.SendAsyncResult>(result);
			}

			// Token: 0x06007A9D RID: 31389 RVA: 0x001C8AAC File Offset: 0x001C6CAC
			private static void OnFinalizeCorrelationCompletedCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ContextOutputChannelBase<TChannel>.SendAsyncResult sendAsyncResult = (ContextOutputChannelBase<TChannel>.SendAsyncResult)result.AsyncState;
				Exception exception = null;
				bool flag;
				try
				{
					flag = sendAsyncResult.OnFinalizeCorrelationCompleted(result);
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
					sendAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007A9E RID: 31390 RVA: 0x001C8B08 File Offset: 0x001C6D08
			private static void OnSendCompletedCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ContextOutputChannelBase<TChannel>.SendAsyncResult sendAsyncResult = (ContextOutputChannelBase<TChannel>.SendAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					sendAsyncResult.OnSendCompleted(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				sendAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007A9F RID: 31391 RVA: 0x001C8B5C File Offset: 0x001C6D5C
			private bool OnFinalizeCorrelationCompleted(IAsyncResult result)
			{
				this.sendMessage = this.correlationCallback.EndFinalizeCorrelation(result);
				bool flag = true;
				IAsyncResult asyncResult;
				try
				{
					asyncResult = this.channel.InnerChannel.BeginSend(this.sendMessage, this.timeoutHelper.RemainingTime(), ContextOutputChannelBase<TChannel>.SendAsyncResult.onSend, this);
					flag = false;
				}
				finally
				{
					if (flag && this.message != null && this.message != this.sendMessage)
					{
						this.sendMessage.Close();
					}
				}
				if (asyncResult.CompletedSynchronously)
				{
					this.OnSendCompleted(asyncResult);
					return true;
				}
				return false;
			}

			// Token: 0x06007AA0 RID: 31392 RVA: 0x001C8BF8 File Offset: 0x001C6DF8
			private void OnSendCompleted(IAsyncResult result)
			{
				try
				{
					this.channel.InnerChannel.EndSend(result);
				}
				finally
				{
					if (this.message != null && this.message != this.sendMessage)
					{
						this.sendMessage.Close();
					}
				}
			}

			// Token: 0x04004617 RID: 17943
			private static AsyncCallback onFinalizeCorrelation = Fx.ThunkCallback(new AsyncCallback(ContextOutputChannelBase<TChannel>.SendAsyncResult.OnFinalizeCorrelationCompletedCallback));

			// Token: 0x04004618 RID: 17944
			private static AsyncCallback onSend = Fx.ThunkCallback(new AsyncCallback(ContextOutputChannelBase<TChannel>.SendAsyncResult.OnSendCompletedCallback));

			// Token: 0x04004619 RID: 17945
			private ContextOutputChannelBase<TChannel> channel;

			// Token: 0x0400461A RID: 17946
			private CorrelationCallbackMessageProperty correlationCallback;

			// Token: 0x0400461B RID: 17947
			private Message message;

			// Token: 0x0400461C RID: 17948
			private Message sendMessage;

			// Token: 0x0400461D RID: 17949
			private TimeoutHelper timeoutHelper;
		}
	}
}
