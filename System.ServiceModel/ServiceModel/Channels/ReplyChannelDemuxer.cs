using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000724 RID: 1828
	internal class ReplyChannelDemuxer : DatagramChannelDemuxer<IReplyChannel, RequestContext>
	{
		// Token: 0x06004573 RID: 17779 RVA: 0x001040E5 File Offset: 0x001022E5
		public ReplyChannelDemuxer(BindingContext context) : base(context)
		{
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x001040EE File Offset: 0x001022EE
		protected override void AbortItem(RequestContext request)
		{
			TypedChannelDemuxer.AbortMessage(request);
			request.Abort();
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x001040FC File Offset: 0x001022FC
		protected override IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceiveRequest(timeout, callback, state);
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x0010410C File Offset: 0x0010230C
		protected override LayeredChannelListener<TChannel> CreateListener<TChannel>(ChannelDemuxerFilter filter)
		{
			if (typeof(TChannel) == typeof(IInputChannel))
			{
				SingletonChannelListener<IInputChannel, InputChannel, Message> singletonChannelListener = new SingletonChannelListener<IInputChannel, InputChannel, Message>(filter, this);
				singletonChannelListener.Acceptor = new InputChannelAcceptor(singletonChannelListener);
				return (LayeredChannelListener<TChannel>)singletonChannelListener;
			}
			if (typeof(TChannel) == typeof(IReplyChannel))
			{
				SingletonChannelListener<IReplyChannel, ReplyChannel, RequestContext> singletonChannelListener2 = new SingletonChannelListener<IReplyChannel, ReplyChannel, RequestContext>(filter, this);
				singletonChannelListener2.Acceptor = new ReplyChannelAcceptor(singletonChannelListener2);
				return (LayeredChannelListener<TChannel>)singletonChannelListener2;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x00104194 File Offset: 0x00102394
		protected override void Dispatch(IChannelListener listener)
		{
			SingletonChannelListener<IInputChannel, InputChannel, Message> singletonChannelListener = listener as SingletonChannelListener<IInputChannel, InputChannel, Message>;
			if (singletonChannelListener != null)
			{
				singletonChannelListener.Dispatch();
				return;
			}
			SingletonChannelListener<IReplyChannel, ReplyChannel, RequestContext> singletonChannelListener2 = listener as SingletonChannelListener<IReplyChannel, ReplyChannel, RequestContext>;
			if (singletonChannelListener2 != null)
			{
				singletonChannelListener2.Dispatch();
				return;
			}
			throw Fx.AssertAndThrow("ReplyChannelDemuxer.Dispatch (false)");
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x001041D0 File Offset: 0x001023D0
		private void EndpointNotFoundCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			RequestContext item = (RequestContext)result.AsyncState;
			bool flag = true;
			try
			{
				ReplyChannelDemuxFailureAsyncResult.End(result);
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
					this.AbortItem(item);
				}
			}
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x00104294 File Offset: 0x00102494
		protected override void EndpointNotFound(RequestContext request)
		{
			bool flag = true;
			try
			{
				if (base.DemuxFailureHandler != null)
				{
					try
					{
						ReplyChannelDemuxFailureAsyncResult replyChannelDemuxFailureAsyncResult = new ReplyChannelDemuxFailureAsyncResult(base.DemuxFailureHandler, request, Fx.ThunkCallback(new AsyncCallback(this.EndpointNotFoundCallback)), request);
						replyChannelDemuxFailureAsyncResult.Start();
						if (!replyChannelDemuxFailureAsyncResult.CompletedSynchronously)
						{
							flag = false;
						}
						else
						{
							ReplyChannelDemuxFailureAsyncResult.End(replyChannelDemuxFailureAsyncResult);
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
				}
			}
		}

		// Token: 0x0600457A RID: 17786 RVA: 0x00104380 File Offset: 0x00102580
		protected override RequestContext EndReceive(IAsyncResult result)
		{
			return base.InnerChannel.EndReceiveRequest(result);
		}

		// Token: 0x0600457B RID: 17787 RVA: 0x00104390 File Offset: 0x00102590
		protected override void EnqueueAndDispatch(IChannelListener listener, RequestContext request, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			SingletonChannelListener<IInputChannel, InputChannel, Message> singletonChannelListener = listener as SingletonChannelListener<IInputChannel, InputChannel, Message>;
			if (singletonChannelListener != null)
			{
				singletonChannelListener.EnqueueAndDispatch(request.RequestMessage, dequeuedCallback, canDispatchOnThisThread);
				try
				{
					request.Close();
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				catch (TimeoutException ex)
				{
					if (TD.CloseTimeoutIsEnabled())
					{
						TD.CloseTimeout(ex.Message);
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				}
			}
			SingletonChannelListener<IReplyChannel, ReplyChannel, RequestContext> singletonChannelListener2 = listener as SingletonChannelListener<IReplyChannel, ReplyChannel, RequestContext>;
			if (singletonChannelListener2 != null)
			{
				singletonChannelListener2.EnqueueAndDispatch(request, dequeuedCallback, canDispatchOnThisThread);
				return;
			}
			throw Fx.AssertAndThrow("ReplyChannelDemuxer.EnqueueAndDispatch (false)");
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x00104420 File Offset: 0x00102620
		protected override void EnqueueAndDispatch(IChannelListener listener, Exception exception, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			SingletonChannelListener<IInputChannel, InputChannel, Message> singletonChannelListener = listener as SingletonChannelListener<IInputChannel, InputChannel, Message>;
			if (singletonChannelListener != null)
			{
				singletonChannelListener.EnqueueAndDispatch(exception, dequeuedCallback, canDispatchOnThisThread);
				return;
			}
			SingletonChannelListener<IReplyChannel, ReplyChannel, RequestContext> singletonChannelListener2 = listener as SingletonChannelListener<IReplyChannel, ReplyChannel, RequestContext>;
			if (singletonChannelListener2 != null)
			{
				singletonChannelListener2.EnqueueAndDispatch(exception, dequeuedCallback, canDispatchOnThisThread);
				return;
			}
			throw Fx.AssertAndThrow("ReplyChannelDemuxer.EnqueueAndDispatch (false)");
		}

		// Token: 0x0600457D RID: 17789 RVA: 0x00104461 File Offset: 0x00102661
		protected override Message GetMessage(RequestContext request)
		{
			return request.RequestMessage;
		}
	}
}
