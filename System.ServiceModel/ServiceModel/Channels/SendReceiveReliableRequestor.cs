using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000941 RID: 2369
	internal sealed class SendReceiveReliableRequestor : ReliableRequestor
	{
		// Token: 0x170015F9 RID: 5625
		// (set) Token: 0x06005B0D RID: 23309 RVA: 0x0014E347 File Offset: 0x0014C547
		public bool TimeoutIsSafe
		{
			set
			{
				this.timeoutIsSafe = value;
			}
		}

		// Token: 0x06005B0E RID: 23310 RVA: 0x0014E350 File Offset: 0x0014C550
		public override WsrmMessageInfo GetInfo()
		{
			throw Fx.AssertAndThrow("Not Supported.");
		}

		// Token: 0x06005B0F RID: 23311 RVA: 0x0014E35C File Offset: 0x0014C55C
		private TimeSpan GetReceiveTimeout(TimeSpan timeoutRemaining)
		{
			if (timeoutRemaining < ReliableMessagingConstants.RequestorReceiveTime || !this.timeoutIsSafe)
			{
				return timeoutRemaining;
			}
			return ReliableMessagingConstants.RequestorReceiveTime;
		}

		// Token: 0x06005B10 RID: 23312 RVA: 0x0014E37C File Offset: 0x0014C57C
		protected override Message OnRequest(Message request, TimeSpan timeout, bool last)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.Binder.Send(request, timeoutHelper.RemainingTime(), MaskingMode.None);
			TimeSpan receiveTimeout = this.GetReceiveTimeout(timeoutHelper.RemainingTime());
			RequestContext requestContext;
			base.Binder.TryReceive(receiveTimeout, out requestContext, MaskingMode.None);
			if (requestContext == null)
			{
				return null;
			}
			return requestContext.RequestMessage;
		}

		// Token: 0x06005B11 RID: 23313 RVA: 0x0014E3CE File Offset: 0x0014C5CE
		protected override IAsyncResult OnBeginRequest(Message request, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new SendReceiveReliableRequestor.SendReceiveAsyncResult(this, request, timeout, callback, state);
		}

		// Token: 0x06005B12 RID: 23314 RVA: 0x0014E3DB File Offset: 0x0014C5DB
		protected override Message OnEndRequest(bool last, IAsyncResult result)
		{
			return SendReceiveReliableRequestor.SendReceiveAsyncResult.End(result);
		}

		// Token: 0x06005B13 RID: 23315 RVA: 0x0014E3E3 File Offset: 0x0014C5E3
		public override void SetInfo(WsrmMessageInfo info)
		{
			throw Fx.AssertAndThrow("Not Supported.");
		}

		// Token: 0x040036D6 RID: 14038
		private bool timeoutIsSafe;

		// Token: 0x02000DCB RID: 3531
		private class SendReceiveAsyncResult : AsyncResult
		{
			// Token: 0x0600800B RID: 32779 RVA: 0x001DC526 File Offset: 0x001DA726
			internal SendReceiveAsyncResult(SendReceiveReliableRequestor requestor, Message request, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.requestor = requestor;
				this.request = request;
				this.timeoutHelper = new TimeoutHelper(timeout);
				if (this.BeginSend())
				{
					base.Complete(true);
				}
			}

			// Token: 0x0600800C RID: 32780 RVA: 0x001DC55C File Offset: 0x001DA75C
			private bool BeginSend()
			{
				IAsyncResult asyncResult = this.requestor.Binder.BeginSend(this.request, this.timeoutHelper.RemainingTime(), MaskingMode.None, SendReceiveReliableRequestor.SendReceiveAsyncResult.sendCallback, this);
				return asyncResult.CompletedSynchronously && this.EndSend(asyncResult);
			}

			// Token: 0x0600800D RID: 32781 RVA: 0x001DC5A4 File Offset: 0x001DA7A4
			public static Message End(IAsyncResult result)
			{
				SendReceiveReliableRequestor.SendReceiveAsyncResult sendReceiveAsyncResult = AsyncResult.End<SendReceiveReliableRequestor.SendReceiveAsyncResult>(result);
				return sendReceiveAsyncResult.response;
			}

			// Token: 0x0600800E RID: 32782 RVA: 0x001DC5C0 File Offset: 0x001DA7C0
			private bool EndSend(IAsyncResult result)
			{
				this.requestor.Binder.EndSend(result);
				TimeSpan receiveTimeout = this.requestor.GetReceiveTimeout(this.timeoutHelper.RemainingTime());
				IAsyncResult asyncResult = this.requestor.Binder.BeginTryReceive(receiveTimeout, MaskingMode.None, SendReceiveReliableRequestor.SendReceiveAsyncResult.tryReceiveCallback, this);
				return asyncResult.CompletedSynchronously && this.EndTryReceive(asyncResult);
			}

			// Token: 0x0600800F RID: 32783 RVA: 0x001DC620 File Offset: 0x001DA820
			private bool EndTryReceive(IAsyncResult result)
			{
				RequestContext requestContext;
				this.requestor.Binder.EndTryReceive(result, out requestContext);
				this.response = ((requestContext != null) ? requestContext.RequestMessage : null);
				return true;
			}

			// Token: 0x06008010 RID: 32784 RVA: 0x001DC654 File Offset: 0x001DA854
			private static void SendCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SendReceiveReliableRequestor.SendReceiveAsyncResult sendReceiveAsyncResult = (SendReceiveReliableRequestor.SendReceiveAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception;
					try
					{
						flag = sendReceiveAsyncResult.EndSend(result);
						exception = null;
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
						sendReceiveAsyncResult.Complete(false, exception);
					}
				}
			}

			// Token: 0x06008011 RID: 32785 RVA: 0x001DC6B0 File Offset: 0x001DA8B0
			private static void TryReceiveCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					SendReceiveReliableRequestor.SendReceiveAsyncResult sendReceiveAsyncResult = (SendReceiveReliableRequestor.SendReceiveAsyncResult)result.AsyncState;
					bool flag = false;
					Exception exception;
					try
					{
						flag = sendReceiveAsyncResult.EndTryReceive(result);
						exception = null;
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
						sendReceiveAsyncResult.Complete(false, exception);
					}
				}
			}

			// Token: 0x04004932 RID: 18738
			private static AsyncCallback sendCallback = Fx.ThunkCallback(new AsyncCallback(SendReceiveReliableRequestor.SendReceiveAsyncResult.SendCallback));

			// Token: 0x04004933 RID: 18739
			private static AsyncCallback tryReceiveCallback = Fx.ThunkCallback(new AsyncCallback(SendReceiveReliableRequestor.SendReceiveAsyncResult.TryReceiveCallback));

			// Token: 0x04004934 RID: 18740
			private Message request;

			// Token: 0x04004935 RID: 18741
			private SendReceiveReliableRequestor requestor;

			// Token: 0x04004936 RID: 18742
			private Message response;

			// Token: 0x04004937 RID: 18743
			private TimeoutHelper timeoutHelper;
		}
	}
}
