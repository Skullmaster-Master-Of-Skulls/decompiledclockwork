using System;
using System.Runtime;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000049 RID: 73
	internal abstract class ResolveRequestResponseAsyncResult<TResolveMessage, TResponseMessage> : AsyncResult
	{
		// Token: 0x0600038F RID: 911 RVA: 0x0000A4C0 File Offset: 0x000086C0
		protected ResolveRequestResponseAsyncResult(TResolveMessage resolveMessage, IDiscoveryServiceImplementation discoveryServiceImpl, AsyncCallback callback, object state) : base(callback, state)
		{
			this.discoveryServiceImpl = discoveryServiceImpl;
			if (!this.Validate(resolveMessage))
			{
				base.Complete(true);
				return;
			}
			this.context = new DiscoveryOperationContext(OperationContext.Current);
			this.resolveCriteria = this.GetResolveCriteria(resolveMessage);
			if (this.ProcessResolveRequest())
			{
				base.Complete(true);
				return;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000A51B File Offset: 0x0000871B
		protected DiscoveryOperationContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000A523 File Offset: 0x00008723
		protected virtual bool Validate(TResolveMessage resolveMessage)
		{
			return DiscoveryService.EnsureMessageId() && this.ValidateContent(resolveMessage) && this.EnsureNotDuplicate();
		}

		// Token: 0x06000392 RID: 914
		protected abstract bool ValidateContent(TResolveMessage resolveMessage);

		// Token: 0x06000393 RID: 915
		protected abstract ResolveCriteria GetResolveCriteria(TResolveMessage resolveMessage);

		// Token: 0x06000394 RID: 916
		protected abstract TResponseMessage GetResolveResponse(DiscoveryMessageSequence discoveryMessageSequence, EndpointDiscoveryMetadata matchingEndpoints);

		// Token: 0x06000395 RID: 917 RVA: 0x0000A53D File Offset: 0x0000873D
		protected TResponseMessage End()
		{
			this.context.AddressRequestResponseMessage(OperationContext.Current);
			return this.GetResolveResponse(this.discoveryServiceImpl.GetNextMessageSequence(), this.matchingEndpoint);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000A568 File Offset: 0x00008768
		private static bool OnOnResolveCompleted(IAsyncResult result)
		{
			ResolveRequestResponseAsyncResult<TResolveMessage, TResponseMessage> resolveRequestResponseAsyncResult = (ResolveRequestResponseAsyncResult<TResolveMessage, TResponseMessage>)result.AsyncState;
			resolveRequestResponseAsyncResult.matchingEndpoint = resolveRequestResponseAsyncResult.discoveryServiceImpl.EndResolve(result);
			return true;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000A594 File Offset: 0x00008794
		private bool ProcessResolveRequest()
		{
			IAsyncResult asyncResult = this.discoveryServiceImpl.BeginResolve(this.resolveCriteria, base.PrepareAsyncCompletion(ResolveRequestResponseAsyncResult<TResolveMessage, TResponseMessage>.onOnResolveCompletedCallback), this);
			return asyncResult.CompletedSynchronously && ResolveRequestResponseAsyncResult<TResolveMessage, TResponseMessage>.OnOnResolveCompleted(asyncResult);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000A5D0 File Offset: 0x000087D0
		private bool EnsureNotDuplicate()
		{
			bool flag = this.discoveryServiceImpl.IsDuplicate(OperationContext.Current.IncomingMessageHeaders.MessageId);
			if (flag && TD.DuplicateDiscoveryMessageIsEnabled())
			{
				TD.DuplicateDiscoveryMessage(this.context.EventTraceActivity, "Resolve", OperationContext.Current.IncomingMessageHeaders.MessageId.ToString());
			}
			return !flag;
		}

		// Token: 0x040000F8 RID: 248
		private readonly ResolveCriteria resolveCriteria;

		// Token: 0x040000F9 RID: 249
		private readonly IDiscoveryServiceImplementation discoveryServiceImpl;

		// Token: 0x040000FA RID: 250
		private readonly DiscoveryOperationContext context;

		// Token: 0x040000FB RID: 251
		private static AsyncResult.AsyncCompletion onOnResolveCompletedCallback = new AsyncResult.AsyncCompletion(ResolveRequestResponseAsyncResult<TResolveMessage, TResponseMessage>.OnOnResolveCompleted);

		// Token: 0x040000FC RID: 252
		private EndpointDiscoveryMetadata matchingEndpoint;
	}
}
