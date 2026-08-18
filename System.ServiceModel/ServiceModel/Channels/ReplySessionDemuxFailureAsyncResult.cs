using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000735 RID: 1845
	internal class ReplySessionDemuxFailureAsyncResult : ReplyChannelDemuxFailureAsyncResult
	{
		// Token: 0x0600462F RID: 17967 RVA: 0x00106405 File Offset: 0x00104605
		public ReplySessionDemuxFailureAsyncResult(IChannelDemuxFailureHandler demuxFailureHandler, RequestContext requestContext, IReplySessionChannel channel, AsyncCallback callback, object state) : base(demuxFailureHandler, requestContext, callback, state)
		{
			this.channel = channel;
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x0010641C File Offset: 0x0010461C
		protected override bool OnDemuxFailureHandled()
		{
			base.OnDemuxFailureHandled();
			IAsyncResult asyncResult = this.channel.BeginClose(ReplySessionDemuxFailureAsyncResult.closeChannelCallback, this);
			if (!asyncResult.CompletedSynchronously)
			{
				return false;
			}
			this.channel.EndClose(asyncResult);
			return true;
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x0010645C File Offset: 0x0010465C
		private static void ChannelCloseCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ReplySessionDemuxFailureAsyncResult replySessionDemuxFailureAsyncResult = (ReplySessionDemuxFailureAsyncResult)result.AsyncState;
			Exception exception = null;
			try
			{
				replySessionDemuxFailureAsyncResult.channel.EndClose(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				exception = ex;
			}
			replySessionDemuxFailureAsyncResult.Complete(false, exception);
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x001064B8 File Offset: 0x001046B8
		public new static void End(IAsyncResult result)
		{
			AsyncResult.End<ReplySessionDemuxFailureAsyncResult>(result);
		}

		// Token: 0x04002D7A RID: 11642
		private static AsyncCallback closeChannelCallback = Fx.ThunkCallback(new AsyncCallback(ReplySessionDemuxFailureAsyncResult.ChannelCloseCallback));

		// Token: 0x04002D7B RID: 11643
		private IReplySessionChannel channel;
	}
}
