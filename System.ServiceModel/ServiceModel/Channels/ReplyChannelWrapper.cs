using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000730 RID: 1840
	internal class ReplyChannelWrapper : ChannelWrapper<IReplyChannel, RequestContext>, IReplyChannel, IChannel, ICommunicationObject
	{
		// Token: 0x060045FA RID: 17914 RVA: 0x00105C20 File Offset: 0x00103E20
		public ReplyChannelWrapper(ChannelManagerBase channelManager, IReplyChannel innerChannel, RequestContext firstRequest) : base(channelManager, innerChannel, firstRequest)
		{
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x060045FB RID: 17915 RVA: 0x00105C2B File Offset: 0x00103E2B
		public EndpointAddress LocalAddress
		{
			get
			{
				return base.InnerChannel.LocalAddress;
			}
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x00105C38 File Offset: 0x00103E38
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x00105C41 File Offset: 0x00103E41
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x00105C49 File Offset: 0x00103E49
		protected override void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x00105C4C File Offset: 0x00103E4C
		protected override void CloseFirstItem(TimeSpan timeout)
		{
			RequestContext firstItem = base.GetFirstItem();
			if (firstItem != null)
			{
				try
				{
					firstItem.RequestMessage.Close();
					firstItem.Close(timeout);
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
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x00105CBC File Offset: 0x00103EBC
		public RequestContext ReceiveRequest()
		{
			RequestContext firstItem = base.GetFirstItem();
			if (firstItem != null)
			{
				return firstItem;
			}
			return base.InnerChannel.ReceiveRequest();
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x00105CE0 File Offset: 0x00103EE0
		public RequestContext ReceiveRequest(TimeSpan timeout)
		{
			RequestContext firstItem = base.GetFirstItem();
			if (firstItem != null)
			{
				return firstItem;
			}
			return base.InnerChannel.ReceiveRequest(timeout);
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x00105D08 File Offset: 0x00103F08
		public IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state)
		{
			RequestContext firstItem = base.GetFirstItem();
			if (firstItem != null)
			{
				return new ChannelWrapper<IReplyChannel, RequestContext>.ReceiveAsyncResult(firstItem, callback, state);
			}
			return base.InnerChannel.BeginReceiveRequest(callback, state);
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x00105D38 File Offset: 0x00103F38
		public IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			RequestContext firstItem = base.GetFirstItem();
			if (firstItem != null)
			{
				return new ChannelWrapper<IReplyChannel, RequestContext>.ReceiveAsyncResult(firstItem, callback, state);
			}
			return base.InnerChannel.BeginReceiveRequest(timeout, callback, state);
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x00105D66 File Offset: 0x00103F66
		public RequestContext EndReceiveRequest(IAsyncResult result)
		{
			if (result is ChannelWrapper<IReplyChannel, RequestContext>.ReceiveAsyncResult)
			{
				return ChannelWrapper<IReplyChannel, RequestContext>.ReceiveAsyncResult.End(result);
			}
			return base.InnerChannel.EndReceiveRequest(result);
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x00105D83 File Offset: 0x00103F83
		public bool TryReceiveRequest(TimeSpan timeout, out RequestContext request)
		{
			request = base.GetFirstItem();
			return request != null || base.InnerChannel.TryReceiveRequest(timeout, out request);
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x00105DA0 File Offset: 0x00103FA0
		public IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			RequestContext firstItem = base.GetFirstItem();
			if (firstItem != null)
			{
				return new ChannelWrapper<IReplyChannel, RequestContext>.ReceiveAsyncResult(firstItem, callback, state);
			}
			return base.InnerChannel.BeginTryReceiveRequest(timeout, callback, state);
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x00105DCE File Offset: 0x00103FCE
		public bool EndTryReceiveRequest(IAsyncResult result, out RequestContext request)
		{
			if (result is ChannelWrapper<IReplyChannel, RequestContext>.ReceiveAsyncResult)
			{
				request = ChannelWrapper<IReplyChannel, RequestContext>.ReceiveAsyncResult.End(result);
				return true;
			}
			return base.InnerChannel.EndTryReceiveRequest(result, out request);
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x00105DEF File Offset: 0x00103FEF
		public bool WaitForRequest(TimeSpan timeout)
		{
			return base.HaveFirstItem() || base.InnerChannel.WaitForRequest(timeout);
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x00105E07 File Offset: 0x00104007
		public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (base.HaveFirstItem())
			{
				return new ChannelWrapper<IReplyChannel, RequestContext>.WaitAsyncResult(callback, state);
			}
			return base.InnerChannel.BeginWaitForRequest(timeout, callback, state);
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x00105E27 File Offset: 0x00104027
		public bool EndWaitForRequest(IAsyncResult result)
		{
			if (result is ChannelWrapper<IReplyChannel, RequestContext>.WaitAsyncResult)
			{
				return ChannelWrapper<IReplyChannel, RequestContext>.WaitAsyncResult.End(result);
			}
			return base.InnerChannel.EndWaitForRequest(result);
		}
	}
}
