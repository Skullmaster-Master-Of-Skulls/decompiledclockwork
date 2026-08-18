using System;
using System.Runtime;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x020002D3 RID: 723
	internal abstract class ApplySecurityAndSendAsyncResult<MessageSenderType> : AsyncResult where MessageSenderType : class
	{
		// Token: 0x060017A6 RID: 6054 RVA: 0x0005A3DB File Offset: 0x000585DB
		public ApplySecurityAndSendAsyncResult(SecurityProtocol binding, MessageSenderType channel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
		{
			this.binding = binding;
			this.channel = channel;
			this.timeoutHelper = new TimeoutHelper(timeout);
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0005A401 File Offset: 0x00058601
		protected SecurityProtocolCorrelationState CorrelationState
		{
			get
			{
				return this.newCorrelationState;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x0005A409 File Offset: 0x00058609
		protected SecurityProtocol SecurityProtocol
		{
			get
			{
				return this.binding;
			}
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0005A414 File Offset: 0x00058614
		protected void Begin(Message message, SecurityProtocolCorrelationState correlationState)
		{
			IAsyncResult asyncResult = this.binding.BeginSecureOutgoingMessage(message, this.timeoutHelper.RemainingTime(), correlationState, ApplySecurityAndSendAsyncResult<MessageSenderType>.sharedCallback, this);
			if (asyncResult.CompletedSynchronously)
			{
				this.binding.EndSecureOutgoingMessage(asyncResult, out message, out this.newCorrelationState);
				bool flag = this.OnSecureOutgoingMessageComplete(message);
				if (flag)
				{
					base.Complete(true);
				}
			}
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0005A46E File Offset: 0x0005866E
		protected static void OnEnd(ApplySecurityAndSendAsyncResult<MessageSenderType> self)
		{
			AsyncResult.End<ApplySecurityAndSendAsyncResult<MessageSenderType>>(self);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0005A478 File Offset: 0x00058678
		private bool OnSecureOutgoingMessageComplete(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("message"));
			}
			this.secureOutgoingMessageDone = true;
			IAsyncResult asyncResult = this.BeginSendCore(this.channel, message, this.timeoutHelper.RemainingTime(), ApplySecurityAndSendAsyncResult<MessageSenderType>.sharedCallback, this);
			if (!asyncResult.CompletedSynchronously)
			{
				return false;
			}
			this.EndSendCore(this.channel, asyncResult);
			return this.OnSendComplete();
		}

		// Token: 0x060017AC RID: 6060
		protected abstract IAsyncResult BeginSendCore(MessageSenderType channel, Message message, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060017AD RID: 6061
		protected abstract void EndSendCore(MessageSenderType channel, IAsyncResult result);

		// Token: 0x060017AE RID: 6062 RVA: 0x0005A4E2 File Offset: 0x000586E2
		private bool OnSendComplete()
		{
			this.OnSendCompleteCore(this.timeoutHelper.RemainingTime());
			return true;
		}

		// Token: 0x060017AF RID: 6063
		protected abstract void OnSendCompleteCore(TimeSpan timeout);

		// Token: 0x060017B0 RID: 6064 RVA: 0x0005A4F8 File Offset: 0x000586F8
		private static void SharedCallback(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("result"));
			}
			if (result.CompletedSynchronously)
			{
				return;
			}
			ApplySecurityAndSendAsyncResult<MessageSenderType> applySecurityAndSendAsyncResult = result.AsyncState as ApplySecurityAndSendAsyncResult<MessageSenderType>;
			if (applySecurityAndSendAsyncResult == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidAsyncResult"), "result"));
			}
			bool flag = false;
			Exception exception = null;
			try
			{
				if (!applySecurityAndSendAsyncResult.secureOutgoingMessageDone)
				{
					Message message;
					applySecurityAndSendAsyncResult.binding.EndSecureOutgoingMessage(result, out message, out applySecurityAndSendAsyncResult.newCorrelationState);
					flag = applySecurityAndSendAsyncResult.OnSecureOutgoingMessageComplete(message);
				}
				else
				{
					applySecurityAndSendAsyncResult.EndSendCore(applySecurityAndSendAsyncResult.channel, result);
					flag = applySecurityAndSendAsyncResult.OnSendComplete();
				}
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
				applySecurityAndSendAsyncResult.Complete(false, exception);
			}
		}

		// Token: 0x04001C2D RID: 7213
		private readonly MessageSenderType channel;

		// Token: 0x04001C2E RID: 7214
		private readonly SecurityProtocol binding;

		// Token: 0x04001C2F RID: 7215
		private volatile bool secureOutgoingMessageDone;

		// Token: 0x04001C30 RID: 7216
		private static AsyncCallback sharedCallback = Fx.ThunkCallback(new AsyncCallback(ApplySecurityAndSendAsyncResult<MessageSenderType>.SharedCallback));

		// Token: 0x04001C31 RID: 7217
		private SecurityProtocolCorrelationState newCorrelationState;

		// Token: 0x04001C32 RID: 7218
		private TimeoutHelper timeoutHelper;
	}
}
