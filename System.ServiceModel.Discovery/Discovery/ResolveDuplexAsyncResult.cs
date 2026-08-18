using System;
using System.Collections.ObjectModel;
using System.Runtime;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000048 RID: 72
	internal abstract class ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel> : AsyncResult
	{
		// Token: 0x06000378 RID: 888 RVA: 0x0000A088 File Offset: 0x00008288
		protected ResolveDuplexAsyncResult(TResolveMessage resolveMessage, IDiscoveryServiceImplementation discoveryServiceImpl, IMulticastSuppressionImplementation multicastSuppressionImpl, AsyncCallback callback, object state) : base(callback, state)
		{
			this.discoveryServiceImpl = discoveryServiceImpl;
			this.multicastSuppressionImpl = multicastSuppressionImpl;
			if (!this.Validate(resolveMessage))
			{
				base.Complete(true);
				return;
			}
			this.context = new DiscoveryOperationContext(OperationContext.Current);
			this.resolveCriteria = this.GetResolveCriteria(resolveMessage);
			this.timeoutHelper = new TimeoutHelper(this.resolveCriteria.Duration);
			this.timeoutHelper.RemainingTime();
			this.Process();
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000A103 File Offset: 0x00008303
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

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000A129 File Offset: 0x00008329
		protected DiscoveryOperationContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000A131 File Offset: 0x00008331
		protected virtual bool Validate(TResolveMessage resolveMessage)
		{
			return DiscoveryService.EnsureMessageId() && DiscoveryService.EnsureReplyTo() && this.ValidateContent(resolveMessage) && this.EnsureNotDuplicate();
		}

		// Token: 0x0600037C RID: 892
		protected abstract bool ValidateContent(TResolveMessage resolveMessage);

		// Token: 0x0600037D RID: 893
		protected abstract ResolveCriteria GetResolveCriteria(TResolveMessage resolveMessage);

		// Token: 0x0600037E RID: 894
		protected abstract IAsyncResult BeginSendResolveResponse(TResponseChannel responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata matchingEndpoint, AsyncCallback callback, object state);

		// Token: 0x0600037F RID: 895
		protected abstract void EndSendResolveResponse(TResponseChannel responseChannel, IAsyncResult result);

		// Token: 0x06000380 RID: 896
		protected abstract IAsyncResult BeginSendProxyAnnouncement(TResponseChannel responseChannel, DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata proxyEndpointDiscoveryMetadata, AsyncCallback callback, object state);

		// Token: 0x06000381 RID: 897
		protected abstract void EndSendProxyAnnouncement(TResponseChannel responseChannel, IAsyncResult result);

		// Token: 0x06000382 RID: 898 RVA: 0x0000A154 File Offset: 0x00008354
		private static bool OnShouldRedirectResolveCompleted(IAsyncResult result)
		{
			Collection<EndpointDiscoveryMetadata> redirectionEndpoints = null;
			ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel> resolveDuplexAsyncResult = (ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>)result.AsyncState;
			if (resolveDuplexAsyncResult.multicastSuppressionImpl.EndShouldRedirectResolve(result, out redirectionEndpoints))
			{
				return resolveDuplexAsyncResult.SendProxyAnnouncements(redirectionEndpoints);
			}
			return resolveDuplexAsyncResult.ProcessResolveRequest();
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000A18D File Offset: 0x0000838D
		private static bool OnSendProxyAnnouncementsCompleted(IAsyncResult result)
		{
			ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.ProxyAnnouncementsSendAsyncResult.End(result);
			return true;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000A198 File Offset: 0x00008398
		private static bool OnOnResolveCompleted(IAsyncResult result)
		{
			ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel> resolveDuplexAsyncResult = (ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>)result.AsyncState;
			EndpointDiscoveryMetadata matchingEndpoint = resolveDuplexAsyncResult.discoveryServiceImpl.EndResolve(result);
			return resolveDuplexAsyncResult.SendResolveResponse(matchingEndpoint);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000A1C8 File Offset: 0x000083C8
		private static bool OnSendResolveResponseCompleted(IAsyncResult result)
		{
			ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel> resolveDuplexAsyncResult = (ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>)result.AsyncState;
			resolveDuplexAsyncResult.EndSendResolveResponse(resolveDuplexAsyncResult.ResponseChannel, result);
			return true;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000A1EF File Offset: 0x000083EF
		private void Process()
		{
			if (this.multicastSuppressionImpl != null && this.context.DiscoveryMode == ServiceDiscoveryMode.Adhoc)
			{
				if (this.SuppressResolveRequest())
				{
					base.Complete(true);
					return;
				}
			}
			else if (this.ProcessResolveRequest())
			{
				base.Complete(true);
				return;
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000A228 File Offset: 0x00008428
		private bool SuppressResolveRequest()
		{
			IAsyncResult asyncResult = this.multicastSuppressionImpl.BeginShouldRedirectResolve(this.resolveCriteria, base.PrepareAsyncCompletion(ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.onShouldRedirectResolveCompletedCallback), this);
			return asyncResult.CompletedSynchronously && ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.OnShouldRedirectResolveCompleted(asyncResult);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000A264 File Offset: 0x00008464
		private bool SendProxyAnnouncements(Collection<EndpointDiscoveryMetadata> redirectionEndpoints)
		{
			if (redirectionEndpoints == null || redirectionEndpoints.Count == 0)
			{
				return true;
			}
			IAsyncResult asyncResult = new ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.ProxyAnnouncementsSendAsyncResult(this, redirectionEndpoints, base.PrepareAsyncCompletion(ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.onSendProxyAnnouncementsCompletedCallback), this);
			return asyncResult.CompletedSynchronously && ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.OnSendProxyAnnouncementsCompleted(asyncResult);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000A2A4 File Offset: 0x000084A4
		private bool ProcessResolveRequest()
		{
			IAsyncResult asyncResult = this.discoveryServiceImpl.BeginResolve(this.resolveCriteria, base.PrepareAsyncCompletion(ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.onOnResolveCompletedCallback), this);
			return asyncResult.CompletedSynchronously && ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.OnOnResolveCompleted(asyncResult);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000A2E0 File Offset: 0x000084E0
		private bool SendResolveResponse(EndpointDiscoveryMetadata matchingEndpoint)
		{
			if (matchingEndpoint == null)
			{
				return true;
			}
			IContextChannel contextChannel = (IContextChannel)((object)this.ResponseChannel);
			IAsyncResult asyncResult = null;
			using (new OperationContextScope(contextChannel))
			{
				this.context.AddressDuplexResponseMessage(OperationContext.Current);
				contextChannel.OperationTimeout = this.timeoutHelper.RemainingTime();
				asyncResult = this.BeginSendResolveResponse(this.ResponseChannel, this.discoveryServiceImpl.GetNextMessageSequence(), matchingEndpoint, base.PrepareAsyncCompletion(ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.onSendResolveResponseCompletedCallback), this);
			}
			return asyncResult.CompletedSynchronously && ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.OnSendResolveResponseCompleted(asyncResult);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000A384 File Offset: 0x00008584
		private bool EnsureNotDuplicate()
		{
			bool flag = this.discoveryServiceImpl.IsDuplicate(OperationContext.Current.IncomingMessageHeaders.MessageId);
			if (flag && TD.DuplicateDiscoveryMessageIsEnabled())
			{
				TD.DuplicateDiscoveryMessage(this.context.EventTraceActivity, "Resolve", OperationContext.Current.IncomingMessageHeaders.MessageId.ToString());
			}
			return !flag;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000A3E4 File Offset: 0x000085E4
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

		// Token: 0x0600038D RID: 909 RVA: 0x0000A45C File Offset: 0x0000865C
		private void EndSendProxyAnnouncement(IAsyncResult result)
		{
			this.EndSendProxyAnnouncement(this.ResponseChannel, result);
		}

		// Token: 0x040000EE RID: 238
		private readonly IDiscoveryServiceImplementation discoveryServiceImpl;

		// Token: 0x040000EF RID: 239
		private readonly IMulticastSuppressionImplementation multicastSuppressionImpl;

		// Token: 0x040000F0 RID: 240
		private readonly ResolveCriteria resolveCriteria;

		// Token: 0x040000F1 RID: 241
		private readonly DiscoveryOperationContext context;

		// Token: 0x040000F2 RID: 242
		private readonly TimeoutHelper timeoutHelper;

		// Token: 0x040000F3 RID: 243
		private static AsyncResult.AsyncCompletion onShouldRedirectResolveCompletedCallback = new AsyncResult.AsyncCompletion(ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.OnShouldRedirectResolveCompleted);

		// Token: 0x040000F4 RID: 244
		private static AsyncResult.AsyncCompletion onSendProxyAnnouncementsCompletedCallback = new AsyncResult.AsyncCompletion(ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.OnSendProxyAnnouncementsCompleted);

		// Token: 0x040000F5 RID: 245
		private static AsyncResult.AsyncCompletion onOnResolveCompletedCallback = new AsyncResult.AsyncCompletion(ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.OnOnResolveCompleted);

		// Token: 0x040000F6 RID: 246
		private static AsyncResult.AsyncCompletion onSendResolveResponseCompletedCallback = new AsyncResult.AsyncCompletion(ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.OnSendResolveResponseCompleted);

		// Token: 0x040000F7 RID: 247
		private TResponseChannel responseChannel;

		// Token: 0x020000E9 RID: 233
		private class ProxyAnnouncementsSendAsyncResult : RandomDelaySendsAsyncResult
		{
			// Token: 0x06000841 RID: 2113 RVA: 0x000154C8 File Offset: 0x000136C8
			public ProxyAnnouncementsSendAsyncResult(ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel> resolveDuplexAsyncResult, Collection<EndpointDiscoveryMetadata> redirectionEndpoints, AsyncCallback callback, object state) : base(redirectionEndpoints.Count, resolveDuplexAsyncResult.context.MaxResponseDelay, callback, state)
			{
				this.resolveDuplexAsyncResult = resolveDuplexAsyncResult;
				this.redirectionEndpoints = redirectionEndpoints;
				base.Start(this.resolveDuplexAsyncResult.timeoutHelper.RemainingTime());
			}

			// Token: 0x06000842 RID: 2114 RVA: 0x00015516 File Offset: 0x00013716
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel>.ProxyAnnouncementsSendAsyncResult>(result);
			}

			// Token: 0x06000843 RID: 2115 RVA: 0x0001551F File Offset: 0x0001371F
			protected override IAsyncResult OnBeginSend(int index, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.resolveDuplexAsyncResult.BeginSendProxyAnnouncement(this.redirectionEndpoints[index], timeout, callback, state);
			}

			// Token: 0x06000844 RID: 2116 RVA: 0x0001553C File Offset: 0x0001373C
			protected override void OnEndSend(IAsyncResult result)
			{
				this.resolveDuplexAsyncResult.EndSendProxyAnnouncement(result);
			}

			// Token: 0x04000286 RID: 646
			private ResolveDuplexAsyncResult<TResolveMessage, TResponseChannel> resolveDuplexAsyncResult;

			// Token: 0x04000287 RID: 647
			private Collection<EndpointDiscoveryMetadata> redirectionEndpoints;
		}
	}
}
