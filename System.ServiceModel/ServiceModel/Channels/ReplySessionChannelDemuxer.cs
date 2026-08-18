using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200072C RID: 1836
	internal class ReplySessionChannelDemuxer : SessionChannelDemuxer<IReplySessionChannel, RequestContext>
	{
		// Token: 0x060045D5 RID: 17877 RVA: 0x00105744 File Offset: 0x00103944
		public ReplySessionChannelDemuxer(BindingContext context, TimeSpan peekTimeout, int maxPendingSessions) : base(context, peekTimeout, maxPendingSessions)
		{
		}

		// Token: 0x060045D6 RID: 17878 RVA: 0x0010574F File Offset: 0x0010394F
		protected override void AbortItem(RequestContext request)
		{
			TypedChannelDemuxer.AbortMessage(request);
			request.Abort();
		}

		// Token: 0x060045D7 RID: 17879 RVA: 0x0010575D File Offset: 0x0010395D
		protected override IAsyncResult BeginReceive(IReplySessionChannel channel, AsyncCallback callback, object state)
		{
			return channel.BeginReceiveRequest(callback, state);
		}

		// Token: 0x060045D8 RID: 17880 RVA: 0x00105767 File Offset: 0x00103967
		protected override IAsyncResult BeginReceive(IReplySessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return channel.BeginReceiveRequest(timeout, callback, state);
		}

		// Token: 0x060045D9 RID: 17881 RVA: 0x00105773 File Offset: 0x00103973
		protected override IReplySessionChannel CreateChannel(ChannelManagerBase channelManager, IReplySessionChannel innerChannel, RequestContext firstRequest)
		{
			return new ReplySessionChannelWrapper(channelManager, innerChannel, firstRequest);
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x00105780 File Offset: 0x00103980
		private void EndpointNotFoundCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ReplySessionChannelDemuxer.ChannelAndRequestAsyncState channelAndRequestAsyncState = (ReplySessionChannelDemuxer.ChannelAndRequestAsyncState)result.AsyncState;
			bool flag = true;
			try
			{
				ReplySessionDemuxFailureAsyncResult.End(result);
				flag = false;
			}
			catch (TimeoutException ex)
			{
				if (TD.SendTimeoutIsEnabled())
				{
					TD.SendTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (ObjectDisposedException exception2)
			{
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
			}
			catch (Exception exception3)
			{
				if (Fx.IsFatal(exception3))
				{
					throw;
				}
				base.HandleUnknownException(exception3);
			}
			finally
			{
				if (flag)
				{
					this.AbortItem(channelAndRequestAsyncState.request);
					channelAndRequestAsyncState.channel.Abort();
				}
			}
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x00105854 File Offset: 0x00103A54
		protected override void EndpointNotFound(IReplySessionChannel channel, RequestContext request)
		{
			bool flag = true;
			try
			{
				if (base.DemuxFailureHandler != null)
				{
					try
					{
						ReplySessionDemuxFailureAsyncResult replySessionDemuxFailureAsyncResult = new ReplySessionDemuxFailureAsyncResult(base.DemuxFailureHandler, request, channel, Fx.ThunkCallback(new AsyncCallback(this.EndpointNotFoundCallback)), new ReplySessionChannelDemuxer.ChannelAndRequestAsyncState(channel, request));
						replySessionDemuxFailureAsyncResult.Start();
						if (!replySessionDemuxFailureAsyncResult.CompletedSynchronously)
						{
							flag = false;
						}
						else
						{
							ReplySessionDemuxFailureAsyncResult.End(replySessionDemuxFailureAsyncResult);
							flag = false;
						}
					}
					catch (CommunicationException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					}
					catch (TimeoutException ex)
					{
						if (TD.SendTimeoutIsEnabled())
						{
							TD.SendTimeout(ex.Message);
						}
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					}
					catch (ObjectDisposedException exception2)
					{
						DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
					}
					catch (Exception exception3)
					{
						if (Fx.IsFatal(exception3))
						{
							throw;
						}
						base.HandleUnknownException(exception3);
					}
				}
			}
			finally
			{
				if (flag)
				{
					this.AbortItem(request);
					channel.Abort();
				}
			}
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x00105954 File Offset: 0x00103B54
		protected override RequestContext EndReceive(IReplySessionChannel channel, IAsyncResult result)
		{
			return channel.EndReceiveRequest(result);
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x0010595D File Offset: 0x00103B5D
		protected override Message GetMessage(RequestContext request)
		{
			return request.RequestMessage;
		}

		// Token: 0x02000CCF RID: 3279
		private struct ChannelAndRequestAsyncState
		{
			// Token: 0x060079CC RID: 31180 RVA: 0x001C65F1 File Offset: 0x001C47F1
			public ChannelAndRequestAsyncState(IChannel channel, RequestContext request)
			{
				this.channel = channel;
				this.request = request;
			}

			// Token: 0x040045AF RID: 17839
			public IChannel channel;

			// Token: 0x040045B0 RID: 17840
			public RequestContext request;
		}
	}
}
