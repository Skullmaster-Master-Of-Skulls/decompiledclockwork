using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007AB RID: 1963
	internal class ContextChannelRequestContext : RequestContext
	{
		// Token: 0x06004A42 RID: 19010 RVA: 0x001113C0 File Offset: 0x0010F5C0
		public ContextChannelRequestContext(RequestContext innerContext, ContextProtocol contextProtocol, TimeSpan defaultSendTimeout)
		{
			if (innerContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("innerContext");
			}
			if (contextProtocol == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("contextProtocol");
			}
			this.innerContext = innerContext;
			this.contextProtocol = contextProtocol;
			this.defaultSendTimeout = defaultSendTimeout;
		}

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06004A43 RID: 19011 RVA: 0x0011140E File Offset: 0x0010F60E
		public override Message RequestMessage
		{
			get
			{
				return this.innerContext.RequestMessage;
			}
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x0011141B File Offset: 0x0010F61B
		public override void Abort()
		{
			this.innerContext.Abort();
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x00111428 File Offset: 0x0010F628
		public override IAsyncResult BeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ContextChannelRequestContext.ReplyAsyncResult(message, this, timeout, callback, state);
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x00111435 File Offset: 0x0010F635
		public override IAsyncResult BeginReply(Message message, AsyncCallback callback, object state)
		{
			return this.BeginReply(message, this.defaultSendTimeout, callback, state);
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x00111446 File Offset: 0x0010F646
		public override void Close(TimeSpan timeout)
		{
			this.innerContext.Close(timeout);
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x00111454 File Offset: 0x0010F654
		public override void Close()
		{
			this.innerContext.Close();
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x00111461 File Offset: 0x0010F661
		public override void EndReply(IAsyncResult result)
		{
			ContextChannelRequestContext.ReplyAsyncResult.End(result);
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x0011146C File Offset: 0x0010F66C
		public override void Reply(Message message, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			Message message2 = message;
			if (message != null)
			{
				this.contextProtocol.OnOutgoingMessage(message, this);
				CorrelationCallbackMessageProperty correlationCallbackMessageProperty;
				if (CorrelationCallbackMessageProperty.TryGet(message, out correlationCallbackMessageProperty))
				{
					ContextExchangeCorrelationHelper.AddOutgoingCorrelationCallbackData(correlationCallbackMessageProperty, message, false);
					if (correlationCallbackMessageProperty.IsFullyDefined)
					{
						message2 = correlationCallbackMessageProperty.FinalizeCorrelation(message, timeoutHelper.RemainingTime());
						message2.Properties.Remove(CorrelationCallbackMessageProperty.Name);
					}
				}
			}
			try
			{
				this.innerContext.Reply(message2, timeoutHelper.RemainingTime());
			}
			finally
			{
				if (message != null && message != message2)
				{
					message2.Close();
				}
			}
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x00111504 File Offset: 0x0010F704
		public override void Reply(Message message)
		{
			this.Reply(message, this.defaultSendTimeout);
		}

		// Token: 0x04002F0A RID: 12042
		private ContextProtocol contextProtocol;

		// Token: 0x04002F0B RID: 12043
		private TimeSpan defaultSendTimeout;

		// Token: 0x04002F0C RID: 12044
		private RequestContext innerContext;

		// Token: 0x02000CF5 RID: 3317
		private class ReplyAsyncResult : AsyncResult
		{
			// Token: 0x06007A8D RID: 31373 RVA: 0x001C84A8 File Offset: 0x001C66A8
			public ReplyAsyncResult(Message message, ContextChannelRequestContext context, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.context = context;
				this.replyMessage = message;
				this.message = message;
				this.timeoutHelper = new TimeoutHelper(timeout);
				bool flag = true;
				if (message != null)
				{
					this.context.contextProtocol.OnOutgoingMessage(message, this.context);
					if (CorrelationCallbackMessageProperty.TryGet(message, out this.correlationCallback))
					{
						ContextExchangeCorrelationHelper.AddOutgoingCorrelationCallbackData(this.correlationCallback, message, false);
						if (this.correlationCallback.IsFullyDefined)
						{
							IAsyncResult asyncResult = this.correlationCallback.BeginFinalizeCorrelation(this.message, this.timeoutHelper.RemainingTime(), ContextChannelRequestContext.ReplyAsyncResult.onFinalizeCorrelation, this);
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
					IAsyncResult asyncResult2 = this.context.innerContext.BeginReply(this.message, this.timeoutHelper.RemainingTime(), ContextChannelRequestContext.ReplyAsyncResult.onReply, this);
					if (asyncResult2.CompletedSynchronously)
					{
						this.OnReplyCompleted(asyncResult2);
						base.Complete(true);
					}
				}
			}

			// Token: 0x06007A8E RID: 31374 RVA: 0x001C85A4 File Offset: 0x001C67A4
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ContextChannelRequestContext.ReplyAsyncResult>(result);
			}

			// Token: 0x06007A8F RID: 31375 RVA: 0x001C85B0 File Offset: 0x001C67B0
			private static void OnFinalizeCorrelationCompletedCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ContextChannelRequestContext.ReplyAsyncResult replyAsyncResult = (ContextChannelRequestContext.ReplyAsyncResult)result.AsyncState;
				Exception exception = null;
				bool flag;
				try
				{
					flag = replyAsyncResult.OnFinalizeCorrelationCompleted(result);
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
					replyAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06007A90 RID: 31376 RVA: 0x001C860C File Offset: 0x001C680C
			private static void OnReplyCompletedCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ContextChannelRequestContext.ReplyAsyncResult replyAsyncResult = (ContextChannelRequestContext.ReplyAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					replyAsyncResult.OnReplyCompleted(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				replyAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007A91 RID: 31377 RVA: 0x001C8660 File Offset: 0x001C6860
			private bool OnFinalizeCorrelationCompleted(IAsyncResult result)
			{
				this.replyMessage = this.correlationCallback.EndFinalizeCorrelation(result);
				bool flag = true;
				IAsyncResult asyncResult;
				try
				{
					asyncResult = this.context.innerContext.BeginReply(this.replyMessage, this.timeoutHelper.RemainingTime(), ContextChannelRequestContext.ReplyAsyncResult.onReply, this);
					flag = false;
				}
				finally
				{
					if (flag && this.message != null && this.message != this.replyMessage)
					{
						this.replyMessage.Close();
					}
				}
				if (asyncResult.CompletedSynchronously)
				{
					this.OnReplyCompleted(asyncResult);
					return true;
				}
				return false;
			}

			// Token: 0x06007A92 RID: 31378 RVA: 0x001C86F4 File Offset: 0x001C68F4
			private void OnReplyCompleted(IAsyncResult result)
			{
				try
				{
					this.context.innerContext.EndReply(result);
				}
				finally
				{
					if (this.message != null && this.message != this.replyMessage)
					{
						this.replyMessage.Close();
					}
				}
			}

			// Token: 0x0400460C RID: 17932
			private static AsyncCallback onFinalizeCorrelation = Fx.ThunkCallback(new AsyncCallback(ContextChannelRequestContext.ReplyAsyncResult.OnFinalizeCorrelationCompletedCallback));

			// Token: 0x0400460D RID: 17933
			private static AsyncCallback onReply = Fx.ThunkCallback(new AsyncCallback(ContextChannelRequestContext.ReplyAsyncResult.OnReplyCompletedCallback));

			// Token: 0x0400460E RID: 17934
			private ContextChannelRequestContext context;

			// Token: 0x0400460F RID: 17935
			private CorrelationCallbackMessageProperty correlationCallback;

			// Token: 0x04004610 RID: 17936
			private Message message;

			// Token: 0x04004611 RID: 17937
			private Message replyMessage;

			// Token: 0x04004612 RID: 17938
			private TimeoutHelper timeoutHelper;
		}
	}
}
