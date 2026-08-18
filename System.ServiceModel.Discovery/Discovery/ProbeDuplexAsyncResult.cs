using System;
using System.Collections.ObjectModel;
using System.Runtime;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000041 RID: 65
	internal abstract class ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel> : AsyncResult
	{
		// Token: 0x0600032A RID: 810 RVA: 0x00008F58 File Offset: 0x00007158
		protected ProbeDuplexAsyncResult(TProbeMessage probeMessage, IDiscoveryServiceImplementation discoveryServiceImpl, IMulticastSuppressionImplementation multicastSuppressionImpl, AsyncCallback callback, object state) : base(callback, state)
		{
			this.discoveryServiceImpl = discoveryServiceImpl;
			this.multicastSuppressionImpl = multicastSuppressionImpl;
			this.findCompletedLock = new object();
			if (!this.Validate(probeMessage))
			{
				base.Complete(true);
				return;
			}
			this.context = new DiscoveryOperationContext(OperationContext.Current);
			this.findRequest = new ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.DuplexFindContext(this.GetFindCriteria(probeMessage), this);
			this.timeoutHelper = new TimeoutHelper(this.findRequest.Criteria.Duration);
			this.timeoutHelper.RemainingTime();
			this.Process();
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00008FE9 File Offset: 0x000071E9
		protected DiscoveryOperationContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00008FF1 File Offset: 0x000071F1
		private TResponseChannel ResponseChannel
		{
			get
			{
				if (this.responseChannel == null)
				{
					this.responseChannel = this.context.GetCallbackChannel<TResponseChannel>();
				}
				return this.responseChannel;
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00009017 File Offset: 0x00007217
		protected virtual bool Validate(TProbeMessage probeMessage)
		{
			return DiscoveryService.EnsureMessageId() && DiscoveryService.EnsureReplyTo() && this.ValidateContent(probeMessage) && this.EnsureNotDuplicate();
		}

		// Token: 0x0600032E RID: 814
		protected abstract bool ValidateContent(TProbeMessage probeMessage);

		// Token: 0x0600032F RID: 815
		protected abstract FindCriteria GetFindCriteria(TProbeMessage probeMessage);

		// Token: 0x06000330 RID: 816
		protected abstract IAsyncResult BeginSendFindResponse(TResponseChannel responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata matchingEndpoint, AsyncCallback callback, object state);

		// Token: 0x06000331 RID: 817
		protected abstract void EndSendFindResponse(TResponseChannel responseChannel, IAsyncResult result);

		// Token: 0x06000332 RID: 818
		protected abstract IAsyncResult BeginSendProxyAnnouncement(TResponseChannel responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata proxyEndpointDiscoveryMetadata, AsyncCallback callback, object state);

		// Token: 0x06000333 RID: 819
		protected abstract void EndSendProxyAnnouncement(TResponseChannel responseChannel, IAsyncResult result);

		// Token: 0x06000334 RID: 820 RVA: 0x00009038 File Offset: 0x00007238
		private static bool OnShouldRedirectFindCompleted(IAsyncResult result)
		{
			Collection<EndpointDiscoveryMetadata> redirectionEndpoints = null;
			ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel> probeDuplexAsyncResult = (ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>)result.AsyncState;
			if (probeDuplexAsyncResult.multicastSuppressionImpl.EndShouldRedirectFind(result, out redirectionEndpoints))
			{
				return probeDuplexAsyncResult.SendProxyAnnouncements(redirectionEndpoints);
			}
			return probeDuplexAsyncResult.ProcessFindRequest();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00009071 File Offset: 0x00007271
		private static bool OnSendProxyAnnouncementsCompleted(IAsyncResult result)
		{
			ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.ProxyAnnouncementsSendAsyncResult.End(result);
			return true;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000907C File Offset: 0x0000727C
		private static void OnFindCompleted(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel> probeDuplexAsyncResult = (ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>)result.AsyncState;
			probeDuplexAsyncResult.FinishFind(result);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000090A8 File Offset: 0x000072A8
		private static bool OnSendFindResponsesCompleted(IAsyncResult result)
		{
			ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.FindResponsesSendAsyncResult.End(result);
			ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel> probeDuplexAsyncResult = (ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>)result.AsyncState;
			if (probeDuplexAsyncResult.findException != null)
			{
				throw FxTrace.Exception.AsError(probeDuplexAsyncResult.findException);
			}
			return true;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000090E4 File Offset: 0x000072E4
		private void FinishFind(IAsyncResult result)
		{
			try
			{
				object obj = this.findCompletedLock;
				lock (obj)
				{
					this.isFindCompleted = true;
				}
				this.discoveryServiceImpl.EndFind(result);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				this.findException = exception;
			}
			finally
			{
				this.findRequest.MatchingEndpoints.Shutdown();
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00009170 File Offset: 0x00007370
		private void Process()
		{
			if (this.multicastSuppressionImpl != null && this.context.DiscoveryMode == ServiceDiscoveryMode.Adhoc)
			{
				if (this.SuppressFindRequest())
				{
					base.Complete(true);
					return;
				}
			}
			else if (this.ProcessFindRequest())
			{
				base.Complete(true);
				return;
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000091A8 File Offset: 0x000073A8
		private bool SuppressFindRequest()
		{
			IAsyncResult asyncResult = this.multicastSuppressionImpl.BeginShouldRedirectFind(this.findRequest.Criteria, base.PrepareAsyncCompletion(ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.onShouldRedirectFindCompletedCallback), this);
			return asyncResult.CompletedSynchronously && ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.OnShouldRedirectFindCompleted(asyncResult);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000091E8 File Offset: 0x000073E8
		private bool SendProxyAnnouncements(Collection<EndpointDiscoveryMetadata> redirectionEndpoints)
		{
			if (redirectionEndpoints == null || redirectionEndpoints.Count == 0)
			{
				return true;
			}
			IAsyncResult asyncResult = new ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.ProxyAnnouncementsSendAsyncResult(this, redirectionEndpoints, base.PrepareAsyncCompletion(ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.onSendProxyAnnouncementsCompletedCallback), this);
			return asyncResult.CompletedSynchronously && ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.OnSendProxyAnnouncementsCompleted(asyncResult);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00009228 File Offset: 0x00007428
		private bool ProcessFindRequest()
		{
			IAsyncResult asyncResult = this.discoveryServiceImpl.BeginFind(this.findRequest, ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.onFindCompletedCallback, this);
			if (asyncResult.CompletedSynchronously)
			{
				this.FinishFind(asyncResult);
			}
			return this.SendFindResponses();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00009264 File Offset: 0x00007464
		private bool SendFindResponses()
		{
			IAsyncResult asyncResult = new ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.FindResponsesSendAsyncResult(this, base.PrepareAsyncCompletion(ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.onSendFindResponsesCompletedCallback), this);
			return asyncResult.CompletedSynchronously && ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.OnSendFindResponsesCompleted(asyncResult);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00009294 File Offset: 0x00007494
		private bool EnsureNotDuplicate()
		{
			bool flag = this.discoveryServiceImpl.IsDuplicate(OperationContext.Current.IncomingMessageHeaders.MessageId);
			if (flag && TD.DuplicateDiscoveryMessageIsEnabled())
			{
				TD.DuplicateDiscoveryMessage(this.context.EventTraceActivity, "Probe", OperationContext.Current.IncomingMessageHeaders.MessageId.ToString());
			}
			return !flag;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000092F4 File Offset: 0x000074F4
		private IAsyncResult BeginSendFindResponse(EndpointDiscoveryMetadata matchingEndpoint, TimeSpan timeout, AsyncCallback callback, object state)
		{
			IContextChannel contextChannel = (IContextChannel)((object)this.ResponseChannel);
			IAsyncResult result;
			using (new OperationContextScope(contextChannel))
			{
				this.context.AddressDuplexResponseMessage(OperationContext.Current);
				contextChannel.OperationTimeout = timeout;
				result = this.BeginSendFindResponse(this.ResponseChannel, this.discoveryServiceImpl.GetNextMessageSequence(), matchingEndpoint, callback, state);
			}
			return result;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000936C File Offset: 0x0000756C
		private void EndSendFindResponse(IAsyncResult result)
		{
			this.EndSendFindResponse(this.ResponseChannel, result);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000937C File Offset: 0x0000757C
		private IAsyncResult BeginSendProxyAnnouncement(EndpointDiscoveryMetadata proxyEndpoint, TimeSpan timeout, AsyncCallback callback, object state)
		{
			IContextChannel contextChannel = (IContextChannel)((object)this.ResponseChannel);
			IAsyncResult result;
			using (new OperationContextScope(contextChannel))
			{
				this.context.AddressDuplexResponseMessage(OperationContext.Current);
				contextChannel.OperationTimeout = timeout;
				result = this.BeginSendProxyAnnouncement(this.ResponseChannel, this.discoveryServiceImpl.GetNextMessageSequence(), proxyEndpoint, callback, state);
			}
			return result;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000093F4 File Offset: 0x000075F4
		private void EndSendProxyAnnouncement(IAsyncResult result)
		{
			this.EndSendProxyAnnouncement(this.ResponseChannel, result);
		}

		// Token: 0x040000B9 RID: 185
		private readonly IDiscoveryServiceImplementation discoveryServiceImpl;

		// Token: 0x040000BA RID: 186
		private readonly IMulticastSuppressionImplementation multicastSuppressionImpl;

		// Token: 0x040000BB RID: 187
		private readonly ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.DuplexFindContext findRequest;

		// Token: 0x040000BC RID: 188
		private readonly DiscoveryOperationContext context;

		// Token: 0x040000BD RID: 189
		private readonly TimeoutHelper timeoutHelper;

		// Token: 0x040000BE RID: 190
		private static AsyncResult.AsyncCompletion onShouldRedirectFindCompletedCallback = new AsyncResult.AsyncCompletion(ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.OnShouldRedirectFindCompleted);

		// Token: 0x040000BF RID: 191
		private static AsyncResult.AsyncCompletion onSendProxyAnnouncementsCompletedCallback = new AsyncResult.AsyncCompletion(ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.OnSendProxyAnnouncementsCompleted);

		// Token: 0x040000C0 RID: 192
		private static AsyncCallback onFindCompletedCallback = Fx.ThunkCallback(new AsyncCallback(ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.OnFindCompleted));

		// Token: 0x040000C1 RID: 193
		private static AsyncResult.AsyncCompletion onSendFindResponsesCompletedCallback = new AsyncResult.AsyncCompletion(ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.OnSendFindResponsesCompleted);

		// Token: 0x040000C2 RID: 194
		private bool isFindCompleted;

		// Token: 0x040000C3 RID: 195
		private object findCompletedLock;

		// Token: 0x040000C4 RID: 196
		private TResponseChannel responseChannel;

		// Token: 0x040000C5 RID: 197
		private Exception findException;

		// Token: 0x020000DA RID: 218
		private class ProxyAnnouncementsSendAsyncResult : RandomDelaySendsAsyncResult
		{
			// Token: 0x06000821 RID: 2081 RVA: 0x000150F4 File Offset: 0x000132F4
			public ProxyAnnouncementsSendAsyncResult(ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel> probeDuplexAsyncResult, Collection<EndpointDiscoveryMetadata> redirectionEndpoints, AsyncCallback callback, object state) : base(redirectionEndpoints.Count, probeDuplexAsyncResult.context.MaxResponseDelay, callback, state)
			{
				this.probeDuplexAsyncResult = probeDuplexAsyncResult;
				this.redirectionEndpoints = redirectionEndpoints;
				base.Start(this.probeDuplexAsyncResult.timeoutHelper.RemainingTime());
			}

			// Token: 0x06000822 RID: 2082 RVA: 0x00015142 File Offset: 0x00013342
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.ProxyAnnouncementsSendAsyncResult>(result);
			}

			// Token: 0x06000823 RID: 2083 RVA: 0x0001514B File Offset: 0x0001334B
			protected override IAsyncResult OnBeginSend(int index, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.probeDuplexAsyncResult.BeginSendProxyAnnouncement(this.redirectionEndpoints[index], timeout, callback, state);
			}

			// Token: 0x06000824 RID: 2084 RVA: 0x00015168 File Offset: 0x00013368
			protected override void OnEndSend(IAsyncResult result)
			{
				this.probeDuplexAsyncResult.EndSendProxyAnnouncement(result);
			}

			// Token: 0x04000218 RID: 536
			private ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel> probeDuplexAsyncResult;

			// Token: 0x04000219 RID: 537
			private Collection<EndpointDiscoveryMetadata> redirectionEndpoints;
		}

		// Token: 0x020000DB RID: 219
		private class FindResponsesSendAsyncResult : RandomDelayQueuedSendsAsyncResult<EndpointDiscoveryMetadata>
		{
			// Token: 0x06000825 RID: 2085 RVA: 0x00015178 File Offset: 0x00013378
			public FindResponsesSendAsyncResult(ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel> probeDuplexAsyncResult, AsyncCallback callback, object state) : base(probeDuplexAsyncResult.context.MaxResponseDelay, probeDuplexAsyncResult.findRequest.MatchingEndpoints, callback, state)
			{
				this.probeDuplexAsyncResult = probeDuplexAsyncResult;
				base.Start(this.probeDuplexAsyncResult.timeoutHelper.RemainingTime());
			}

			// Token: 0x06000826 RID: 2086 RVA: 0x000151C3 File Offset: 0x000133C3
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel>.FindResponsesSendAsyncResult>(result);
			}

			// Token: 0x06000827 RID: 2087 RVA: 0x000151CC File Offset: 0x000133CC
			protected override IAsyncResult OnBeginSendItem(EndpointDiscoveryMetadata item, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.probeDuplexAsyncResult.BeginSendFindResponse(item, timeout, callback, state);
			}

			// Token: 0x06000828 RID: 2088 RVA: 0x000151DE File Offset: 0x000133DE
			protected override void OnEndSendItem(IAsyncResult result)
			{
				this.probeDuplexAsyncResult.EndSendFindResponse(result);
			}

			// Token: 0x0400021A RID: 538
			private readonly ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel> probeDuplexAsyncResult;
		}

		// Token: 0x020000DC RID: 220
		private class DuplexFindContext : FindRequestContext
		{
			// Token: 0x06000829 RID: 2089 RVA: 0x000151EC File Offset: 0x000133EC
			public DuplexFindContext(FindCriteria criteria, ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel> probeDuplexAsyncResult) : base(criteria)
			{
				this.matchingEndpoints = new InputQueue<EndpointDiscoveryMetadata>();
				this.probeDuplexAsyncResult = probeDuplexAsyncResult;
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x0600082A RID: 2090 RVA: 0x00015207 File Offset: 0x00013407
			public InputQueue<EndpointDiscoveryMetadata> MatchingEndpoints
			{
				get
				{
					return this.matchingEndpoints;
				}
			}

			// Token: 0x0600082B RID: 2091 RVA: 0x00015210 File Offset: 0x00013410
			protected override void OnAddMatchingEndpoint(EndpointDiscoveryMetadata matchingEndpoint)
			{
				object findCompletedLock = this.probeDuplexAsyncResult.findCompletedLock;
				lock (findCompletedLock)
				{
					if (this.probeDuplexAsyncResult.isFindCompleted)
					{
						throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryCannotAddMatchingEndpoint));
					}
					this.matchingEndpoints.EnqueueAndDispatch(matchingEndpoint, null, false);
				}
			}

			// Token: 0x0400021B RID: 539
			private readonly InputQueue<EndpointDiscoveryMetadata> matchingEndpoints;

			// Token: 0x0400021C RID: 540
			private readonly ProbeDuplexAsyncResult<TProbeMessage, TResponseChannel> probeDuplexAsyncResult;
		}
	}
}
