using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000734 RID: 1844
	internal class ReplyChannelDemuxFailureAsyncResult : AsyncResult
	{
		// Token: 0x06004629 RID: 17961 RVA: 0x001062D8 File Offset: 0x001044D8
		public ReplyChannelDemuxFailureAsyncResult(IChannelDemuxFailureHandler demuxFailureHandler, RequestContext requestContext, AsyncCallback callback, object state) : base(callback, state)
		{
			if (demuxFailureHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("demuxFailureHandler");
			}
			if (requestContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestContext");
			}
			this.demuxFailureHandler = demuxFailureHandler;
			this.requestContext = requestContext;
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x00106318 File Offset: 0x00104518
		public void Start()
		{
			IAsyncResult asyncResult = this.demuxFailureHandler.BeginHandleDemuxFailure(this.requestContext.RequestMessage, this.requestContext, ReplyChannelDemuxFailureAsyncResult.demuxFailureHandlerCallback, this);
			if (!asyncResult.CompletedSynchronously)
			{
				return;
			}
			this.demuxFailureHandler.EndHandleDemuxFailure(asyncResult);
			if (this.OnDemuxFailureHandled())
			{
				base.Complete(true);
			}
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x0010636C File Offset: 0x0010456C
		protected virtual bool OnDemuxFailureHandled()
		{
			this.requestContext.Close();
			return true;
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x0010637C File Offset: 0x0010457C
		private static void DemuxFailureHandlerCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ReplyChannelDemuxFailureAsyncResult replyChannelDemuxFailureAsyncResult = (ReplyChannelDemuxFailureAsyncResult)result.AsyncState;
			bool flag = false;
			Exception exception = null;
			try
			{
				replyChannelDemuxFailureAsyncResult.demuxFailureHandler.EndHandleDemuxFailure(result);
				flag = replyChannelDemuxFailureAsyncResult.OnDemuxFailureHandled();
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
				replyChannelDemuxFailureAsyncResult.Complete(false, exception);
			}
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x001063E4 File Offset: 0x001045E4
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ReplyChannelDemuxFailureAsyncResult>(result);
		}

		// Token: 0x04002D77 RID: 11639
		private static AsyncCallback demuxFailureHandlerCallback = Fx.ThunkCallback(new AsyncCallback(ReplyChannelDemuxFailureAsyncResult.DemuxFailureHandlerCallback));

		// Token: 0x04002D78 RID: 11640
		private IChannelDemuxFailureHandler demuxFailureHandler;

		// Token: 0x04002D79 RID: 11641
		private RequestContext requestContext;
	}
}
