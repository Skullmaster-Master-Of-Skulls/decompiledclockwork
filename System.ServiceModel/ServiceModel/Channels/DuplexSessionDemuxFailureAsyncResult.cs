using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000736 RID: 1846
	internal class DuplexSessionDemuxFailureAsyncResult : AsyncResult
	{
		// Token: 0x06004634 RID: 17972 RVA: 0x001064DC File Offset: 0x001046DC
		public DuplexSessionDemuxFailureAsyncResult(IChannelDemuxFailureHandler demuxFailureHandler, IDuplexSessionChannel channel, Message message, AsyncCallback callback, object state) : base(callback, state)
		{
			if (demuxFailureHandler == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("demuxFailureHandler");
			}
			if (channel == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channel");
			}
			this.demuxFailureHandler = demuxFailureHandler;
			this.channel = channel;
			this.message = message;
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x00106530 File Offset: 0x00104730
		public void Start()
		{
			IAsyncResult asyncResult = this.demuxFailureHandler.BeginHandleDemuxFailure(this.message, this.channel, DuplexSessionDemuxFailureAsyncResult.demuxFailureHandlerCallback, this);
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

		// Token: 0x06004636 RID: 17974 RVA: 0x00106580 File Offset: 0x00104780
		private bool OnDemuxFailureHandled()
		{
			IAsyncResult asyncResult = this.channel.BeginClose(DuplexSessionDemuxFailureAsyncResult.channelCloseCallback, this);
			if (!asyncResult.CompletedSynchronously)
			{
				return false;
			}
			this.channel.EndClose(asyncResult);
			this.message.Close();
			return true;
		}

		// Token: 0x06004637 RID: 17975 RVA: 0x001065C4 File Offset: 0x001047C4
		private static void DemuxFailureHandlerCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			DuplexSessionDemuxFailureAsyncResult duplexSessionDemuxFailureAsyncResult = (DuplexSessionDemuxFailureAsyncResult)result.AsyncState;
			bool flag = false;
			Exception exception = null;
			try
			{
				duplexSessionDemuxFailureAsyncResult.demuxFailureHandler.EndHandleDemuxFailure(result);
				flag = duplexSessionDemuxFailureAsyncResult.OnDemuxFailureHandled();
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
				duplexSessionDemuxFailureAsyncResult.Complete(false, exception);
			}
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x0010662C File Offset: 0x0010482C
		private static void ChannelCloseCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			DuplexSessionDemuxFailureAsyncResult duplexSessionDemuxFailureAsyncResult = (DuplexSessionDemuxFailureAsyncResult)result.AsyncState;
			Exception exception = null;
			try
			{
				duplexSessionDemuxFailureAsyncResult.channel.EndClose(result);
				duplexSessionDemuxFailureAsyncResult.message.Close();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				exception = ex;
			}
			duplexSessionDemuxFailureAsyncResult.Complete(false, exception);
		}

		// Token: 0x06004639 RID: 17977 RVA: 0x00106690 File Offset: 0x00104890
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<DuplexSessionDemuxFailureAsyncResult>(result);
		}

		// Token: 0x04002D7C RID: 11644
		private static AsyncCallback demuxFailureHandlerCallback = Fx.ThunkCallback(new AsyncCallback(DuplexSessionDemuxFailureAsyncResult.DemuxFailureHandlerCallback));

		// Token: 0x04002D7D RID: 11645
		private static AsyncCallback channelCloseCallback = Fx.ThunkCallback(new AsyncCallback(DuplexSessionDemuxFailureAsyncResult.ChannelCloseCallback));

		// Token: 0x04002D7E RID: 11646
		private IChannelDemuxFailureHandler demuxFailureHandler;

		// Token: 0x04002D7F RID: 11647
		private IDuplexSessionChannel channel;

		// Token: 0x04002D80 RID: 11648
		private Message message;
	}
}
