using System;
using System.Runtime;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x020002DC RID: 732
	internal abstract class ReceiveMessageAndVerifySecurityAsyncResultBase : AsyncResult
	{
		// Token: 0x060017E6 RID: 6118 RVA: 0x0005AEFC File Offset: 0x000590FC
		protected ReceiveMessageAndVerifySecurityAsyncResultBase(IInputChannel innerChannel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
		{
			this.timeoutHelper = new TimeoutHelper(timeout);
			this.innerChannel = innerChannel;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0005AF1C File Offset: 0x0005911C
		public void Start()
		{
			IAsyncResult asyncResult = this.innerChannel.BeginTryReceive(this.timeoutHelper.RemainingTime(), ReceiveMessageAndVerifySecurityAsyncResultBase.innerTryReceiveCompletedCallback, this);
			if (!asyncResult.CompletedSynchronously)
			{
				return;
			}
			if (!this.innerChannel.EndTryReceive(asyncResult, out this.message))
			{
				this.receiveCompleted = false;
			}
			else
			{
				this.receiveCompleted = true;
				if (!this.OnInnerReceiveDone(ref this.message, this.timeoutHelper.RemainingTime()))
				{
					return;
				}
			}
			base.Complete(true);
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0005AF98 File Offset: 0x00059198
		private static void InnerTryReceiveCompletedCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ReceiveMessageAndVerifySecurityAsyncResultBase receiveMessageAndVerifySecurityAsyncResultBase = (ReceiveMessageAndVerifySecurityAsyncResultBase)result.AsyncState;
			Exception exception = null;
			bool flag = false;
			try
			{
				if (!receiveMessageAndVerifySecurityAsyncResultBase.innerChannel.EndTryReceive(result, out receiveMessageAndVerifySecurityAsyncResultBase.message))
				{
					receiveMessageAndVerifySecurityAsyncResultBase.receiveCompleted = false;
					flag = true;
				}
				else
				{
					receiveMessageAndVerifySecurityAsyncResultBase.receiveCompleted = true;
					flag = receiveMessageAndVerifySecurityAsyncResultBase.OnInnerReceiveDone(ref receiveMessageAndVerifySecurityAsyncResultBase.message, receiveMessageAndVerifySecurityAsyncResultBase.timeoutHelper.RemainingTime());
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
				receiveMessageAndVerifySecurityAsyncResultBase.Complete(false, exception);
			}
		}

		// Token: 0x060017E9 RID: 6121
		protected abstract bool OnInnerReceiveDone(ref Message message, TimeSpan timeout);

		// Token: 0x060017EA RID: 6122 RVA: 0x0005B030 File Offset: 0x00059230
		public static bool End(IAsyncResult result, out Message message)
		{
			ReceiveMessageAndVerifySecurityAsyncResultBase receiveMessageAndVerifySecurityAsyncResultBase = AsyncResult.End<ReceiveMessageAndVerifySecurityAsyncResultBase>(result);
			message = receiveMessageAndVerifySecurityAsyncResultBase.message;
			return receiveMessageAndVerifySecurityAsyncResultBase.receiveCompleted;
		}

		// Token: 0x04001C3F RID: 7231
		private static AsyncCallback innerTryReceiveCompletedCallback = Fx.ThunkCallback(new AsyncCallback(ReceiveMessageAndVerifySecurityAsyncResultBase.InnerTryReceiveCompletedCallback));

		// Token: 0x04001C40 RID: 7232
		private Message message;

		// Token: 0x04001C41 RID: 7233
		private bool receiveCompleted;

		// Token: 0x04001C42 RID: 7234
		private TimeoutHelper timeoutHelper;

		// Token: 0x04001C43 RID: 7235
		private IInputChannel innerChannel;
	}
}
