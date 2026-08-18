using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000943 RID: 2371
	internal abstract class ReliableOutputAsyncResult : AsyncResult
	{
		// Token: 0x06005B23 RID: 23331 RVA: 0x0014E661 File Offset: 0x0014C861
		protected ReliableOutputAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06005B24 RID: 23332 RVA: 0x0014E66B File Offset: 0x0014C86B
		// (set) Token: 0x06005B25 RID: 23333 RVA: 0x0014E673 File Offset: 0x0014C873
		public IReliableChannelBinder Binder
		{
			protected get
			{
				return this.binder;
			}
			set
			{
				this.binder = value;
			}
		}

		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x06005B26 RID: 23334 RVA: 0x0014E67C File Offset: 0x0014C87C
		protected Exception HandledException
		{
			get
			{
				return this.handledException;
			}
		}

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x06005B27 RID: 23335 RVA: 0x0014E684 File Offset: 0x0014C884
		// (set) Token: 0x06005B28 RID: 23336 RVA: 0x0014E68C File Offset: 0x0014C88C
		public MaskingMode MaskingMode
		{
			get
			{
				return this.maskingMode;
			}
			set
			{
				this.maskingMode = value;
			}
		}

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06005B29 RID: 23337 RVA: 0x0014E695 File Offset: 0x0014C895
		// (set) Token: 0x06005B2A RID: 23338 RVA: 0x0014E69D File Offset: 0x0014C89D
		public MessageAttemptInfo MessageAttemptInfo
		{
			get
			{
				return this.messageAttemptInfo;
			}
			set
			{
				this.messageAttemptInfo = value;
			}
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06005B2B RID: 23339 RVA: 0x0014E6A6 File Offset: 0x0014C8A6
		// (set) Token: 0x06005B2C RID: 23340 RVA: 0x0014E6B3 File Offset: 0x0014C8B3
		public Message Message
		{
			protected get
			{
				return this.messageAttemptInfo.Message;
			}
			set
			{
				this.messageAttemptInfo = new MessageAttemptInfo(value, 0L, 0, null);
			}
		}

		// Token: 0x17001600 RID: 5632
		// (set) Token: 0x06005B2D RID: 23341 RVA: 0x0014E6C5 File Offset: 0x0014C8C5
		public bool SaveHandledException
		{
			set
			{
				this.saveHandledException = value;
			}
		}

		// Token: 0x06005B2E RID: 23342 RVA: 0x0014E6D0 File Offset: 0x0014C8D0
		public void Begin(TimeSpan timeout)
		{
			bool flag;
			if (this.saveHandledException)
			{
				flag = this.BeginInternal(timeout);
			}
			else
			{
				try
				{
					flag = this.BeginInternal(timeout);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex) || !this.HandleException(ex))
					{
						throw;
					}
					flag = true;
				}
			}
			if (flag)
			{
				base.Complete(true);
			}
		}

		// Token: 0x06005B2F RID: 23343 RVA: 0x0014E72C File Offset: 0x0014C92C
		private bool BeginInternal(TimeSpan timeout)
		{
			bool flag = true;
			bool result;
			try
			{
				IAsyncResult asyncResult = this.BeginOperation(timeout, ReliableOutputAsyncResult.operationCallback, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.EndOperation(asyncResult);
					result = true;
				}
				else
				{
					flag = false;
					result = false;
				}
			}
			finally
			{
				if (flag)
				{
					this.Message.Close();
				}
			}
			return result;
		}

		// Token: 0x06005B30 RID: 23344
		protected abstract IAsyncResult BeginOperation(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06005B31 RID: 23345
		protected abstract void EndOperation(IAsyncResult result);

		// Token: 0x06005B32 RID: 23346 RVA: 0x0014E784 File Offset: 0x0014C984
		private static void OperationCallback(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				ReliableOutputAsyncResult reliableOutputAsyncResult = (ReliableOutputAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					reliableOutputAsyncResult.EndOperation(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!reliableOutputAsyncResult.HandleException(ex))
					{
						exception = ex;
					}
				}
				finally
				{
					reliableOutputAsyncResult.Message.Close();
				}
				reliableOutputAsyncResult.Complete(false, exception);
			}
		}

		// Token: 0x06005B33 RID: 23347 RVA: 0x0014E7F8 File Offset: 0x0014C9F8
		private bool HandleException(Exception e)
		{
			if (this.saveHandledException && this.Binder.IsHandleable(e))
			{
				this.handledException = e;
				return true;
			}
			return false;
		}

		// Token: 0x040036DC RID: 14044
		private IReliableChannelBinder binder;

		// Token: 0x040036DD RID: 14045
		private Exception handledException;

		// Token: 0x040036DE RID: 14046
		private MaskingMode maskingMode;

		// Token: 0x040036DF RID: 14047
		private MessageAttemptInfo messageAttemptInfo;

		// Token: 0x040036E0 RID: 14048
		private static AsyncCallback operationCallback = Fx.ThunkCallback(new AsyncCallback(ReliableOutputAsyncResult.OperationCallback));

		// Token: 0x040036E1 RID: 14049
		private bool saveHandledException;
	}
}
