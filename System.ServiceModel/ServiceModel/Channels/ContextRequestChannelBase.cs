using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007BD RID: 1981
	internal abstract class ContextRequestChannelBase<TChannel> : LayeredChannel<TChannel> where TChannel : class, IRequestChannel
	{
		// Token: 0x06004AD7 RID: 19159 RVA: 0x00112737 File Offset: 0x00110937
		protected ContextRequestChannelBase(ChannelManagerBase channelManager, TChannel innerChannel, ContextExchangeMechanism contextExchangeMechanism, Uri callbackAddress, bool contextManagementEnabled) : base(channelManager, innerChannel)
		{
			this.contextProtocol = new ClientContextProtocol(contextExchangeMechanism, innerChannel.Via, this, callbackAddress, contextManagementEnabled);
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06004AD8 RID: 19160 RVA: 0x0011275D File Offset: 0x0011095D
		public EndpointAddress RemoteAddress
		{
			get
			{
				return base.InnerChannel.RemoteAddress;
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06004AD9 RID: 19161 RVA: 0x0011276F File Offset: 0x0011096F
		public Uri Via
		{
			get
			{
				return base.InnerChannel.Via;
			}
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x00112781 File Offset: 0x00110981
		public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.contextProtocol.OnOutgoingMessage(message, null);
			return new ContextRequestChannelBase<TChannel>.RequestAsyncResult(message, base.InnerChannel, timeout, callback, state);
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x001127A5 File Offset: 0x001109A5
		public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state)
		{
			return this.BeginRequest(message, base.DefaultSendTimeout, callback, state);
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x001127B8 File Offset: 0x001109B8
		public Message EndRequest(IAsyncResult result)
		{
			Message message = ContextRequestChannelBase<TChannel>.RequestAsyncResult.End(result);
			if (message != null)
			{
				this.contextProtocol.OnIncomingMessage(message);
			}
			return message;
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x001127DC File Offset: 0x001109DC
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IContextManager) && this.contextProtocol is IContextManager)
			{
				return (T)((object)this.contextProtocol);
			}
			return base.GetProperty<T>();
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x00112818 File Offset: 0x00110A18
		public Message Request(Message message, TimeSpan timeout)
		{
			CorrelationCallbackMessageProperty correlationCallbackMessageProperty = null;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			Message message2 = message;
			this.contextProtocol.OnOutgoingMessage(message, null);
			if (message != null && CorrelationCallbackMessageProperty.TryGet(message, out correlationCallbackMessageProperty))
			{
				ContextExchangeCorrelationHelper.AddOutgoingCorrelationCallbackData(correlationCallbackMessageProperty, message, true);
				if (correlationCallbackMessageProperty.IsFullyDefined)
				{
					message2 = correlationCallbackMessageProperty.FinalizeCorrelation(message, timeoutHelper.RemainingTime());
				}
			}
			Message message3 = null;
			try
			{
				message3 = base.InnerChannel.Request(message2, timeout);
				if (message3 != null)
				{
					this.contextProtocol.OnIncomingMessage(message3);
				}
			}
			finally
			{
				if (message != null && message != message2)
				{
					message2.Close();
				}
			}
			return message3;
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x001128B0 File Offset: 0x00110AB0
		public Message Request(Message message)
		{
			return this.Request(message, base.DefaultSendTimeout);
		}

		// Token: 0x04002F27 RID: 12071
		private ContextProtocol contextProtocol;

		// Token: 0x02000CFA RID: 3322
		private class RequestAsyncResult : AsyncResult
		{
			// Token: 0x06007AA2 RID: 31394 RVA: 0x001C8C80 File Offset: 0x001C6E80
			public RequestAsyncResult(Message message, IRequestChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.channel = channel;
				this.requestMessage = message;
				this.message = message;
				this.timeoutHelper = new TimeoutHelper(timeout);
				bool flag = true;
				if (message != null && CorrelationCallbackMessageProperty.TryGet(message, out this.correlationCallback))
				{
					ContextExchangeCorrelationHelper.AddOutgoingCorrelationCallbackData(this.correlationCallback, message, true);
					if (this.correlationCallback.IsFullyDefined)
					{
						IAsyncResult asyncResult = this.correlationCallback.BeginFinalizeCorrelation(this.message, this.timeoutHelper.RemainingTime(), ContextRequestChannelBase<TChannel>.RequestAsyncResult.onFinalizeCorrelation, this);
						if (asyncResult.CompletedSynchronously && this.OnFinalizeCorrelationCompleted(asyncResult))
						{
							base.Complete(true);
						}
						flag = false;
					}
				}
				if (flag)
				{
					IAsyncResult asyncResult2 = this.channel.BeginRequest(this.message, this.timeoutHelper.RemainingTime(), ContextRequestChannelBase<TChannel>.RequestAsyncResult.onRequest, this);
					if (asyncResult2.CompletedSynchronously)
					{
						this.OnRequestCompleted(asyncResult2);
						base.Complete(true);
					}
				}
			}

			// Token: 0x06007AA3 RID: 31395 RVA: 0x001C8D60 File Offset: 0x001C6F60
			public static Message End(IAsyncResult result)
			{
				ContextRequestChannelBase<TChannel>.RequestAsyncResult requestAsyncResult = AsyncResult.End<ContextRequestChannelBase<TChannel>.RequestAsyncResult>(result);
				return requestAsyncResult.replyMessage;
			}

			// Token: 0x06007AA4 RID: 31396 RVA: 0x001C8D7C File Offset: 0x001C6F7C
			private static void OnFinalizeCorrelationCompletedCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ContextRequestChannelBase<TChannel>.RequestAsyncResult requestAsyncResult = (ContextRequestChannelBase<TChannel>.RequestAsyncResult)result.AsyncState;
				Exception exception = null;
				bool flag;
				try
				{
					flag = requestAsyncResult.OnFinalizeCorrelationCompleted(result);
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
					requestAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007AA5 RID: 31397 RVA: 0x001C8DD8 File Offset: 0x001C6FD8
			private static void OnRequestCompletedCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ContextRequestChannelBase<TChannel>.RequestAsyncResult requestAsyncResult = (ContextRequestChannelBase<TChannel>.RequestAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					requestAsyncResult.OnRequestCompleted(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				requestAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007AA6 RID: 31398 RVA: 0x001C8E2C File Offset: 0x001C702C
			private bool OnFinalizeCorrelationCompleted(IAsyncResult result)
			{
				this.requestMessage = this.correlationCallback.EndFinalizeCorrelation(result);
				this.requestMessage.Properties.Remove(CorrelationCallbackMessageProperty.Name);
				bool flag = true;
				IAsyncResult asyncResult;
				try
				{
					asyncResult = this.channel.BeginRequest(this.requestMessage, this.timeoutHelper.RemainingTime(), ContextRequestChannelBase<TChannel>.RequestAsyncResult.onRequest, this);
					flag = false;
				}
				finally
				{
					if (flag && this.message != null && this.message != this.requestMessage)
					{
						this.requestMessage.Close();
					}
				}
				if (asyncResult.CompletedSynchronously)
				{
					this.OnRequestCompleted(asyncResult);
					return true;
				}
				return false;
			}

			// Token: 0x06007AA7 RID: 31399 RVA: 0x001C8ED4 File Offset: 0x001C70D4
			private void OnRequestCompleted(IAsyncResult result)
			{
				try
				{
					this.replyMessage = this.channel.EndRequest(result);
				}
				finally
				{
					if (this.message != null && this.message != this.requestMessage)
					{
						this.requestMessage.Close();
					}
				}
			}

			// Token: 0x0400461E RID: 17950
			private static AsyncCallback onFinalizeCorrelation = Fx.ThunkCallback(new AsyncCallback(ContextRequestChannelBase<TChannel>.RequestAsyncResult.OnFinalizeCorrelationCompletedCallback));

			// Token: 0x0400461F RID: 17951
			private static AsyncCallback onRequest = Fx.ThunkCallback(new AsyncCallback(ContextRequestChannelBase<TChannel>.RequestAsyncResult.OnRequestCompletedCallback));

			// Token: 0x04004620 RID: 17952
			private IRequestChannel channel;

			// Token: 0x04004621 RID: 17953
			private CorrelationCallbackMessageProperty correlationCallback;

			// Token: 0x04004622 RID: 17954
			private Message message;

			// Token: 0x04004623 RID: 17955
			private Message replyMessage;

			// Token: 0x04004624 RID: 17956
			private Message requestMessage;

			// Token: 0x04004625 RID: 17957
			private TimeoutHelper timeoutHelper;
		}
	}
}
