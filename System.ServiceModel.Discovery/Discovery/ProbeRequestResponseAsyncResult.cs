using System;
using System.Collections.ObjectModel;
using System.Runtime;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000042 RID: 66
	internal abstract class ProbeRequestResponseAsyncResult<TProbeMessage, TResponseMessage> : AsyncResult
	{
		// Token: 0x06000344 RID: 836 RVA: 0x0000945C File Offset: 0x0000765C
		protected ProbeRequestResponseAsyncResult(TProbeMessage probeMessage, IDiscoveryServiceImplementation discoveryServiceImpl, AsyncCallback callback, object state) : base(callback, state)
		{
			this.discoveryServiceImpl = discoveryServiceImpl;
			this.findCompletedLock = new object();
			if (!this.Validate(probeMessage))
			{
				base.Complete(true);
				return;
			}
			this.context = new DiscoveryOperationContext(OperationContext.Current);
			this.findRequest = new ProbeRequestResponseAsyncResult<TProbeMessage, TResponseMessage>.FindRequestResponseContext(this.GetFindCriteria(probeMessage), this);
			if (this.ProcessFindRequest())
			{
				base.Complete(true);
				return;
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000094C8 File Offset: 0x000076C8
		protected virtual bool Validate(TProbeMessage probeMessage)
		{
			return DiscoveryService.EnsureMessageId() && this.ValidateContent(probeMessage) && this.EnsureNotDuplicate();
		}

		// Token: 0x06000346 RID: 838
		protected abstract bool ValidateContent(TProbeMessage probeMessage);

		// Token: 0x06000347 RID: 839
		protected abstract FindCriteria GetFindCriteria(TProbeMessage probeMessage);

		// Token: 0x06000348 RID: 840
		protected abstract TResponseMessage GetProbeResponse(DiscoveryMessageSequence discoveryMessageSequence, Collection<EndpointDiscoveryMetadata> matchingEndpoints);

		// Token: 0x06000349 RID: 841 RVA: 0x000094E2 File Offset: 0x000076E2
		protected TResponseMessage End()
		{
			this.context.AddressRequestResponseMessage(OperationContext.Current);
			return this.GetProbeResponse(this.discoveryServiceImpl.GetNextMessageSequence(), this.findRequest.MatchingEndpoints);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00009510 File Offset: 0x00007710
		private static bool OnOnFindCompleted(IAsyncResult result)
		{
			ProbeRequestResponseAsyncResult<TProbeMessage, TResponseMessage> probeRequestResponseAsyncResult = (ProbeRequestResponseAsyncResult<TProbeMessage, TResponseMessage>)result.AsyncState;
			object obj = probeRequestResponseAsyncResult.findCompletedLock;
			lock (obj)
			{
				probeRequestResponseAsyncResult.isFindCompleted = true;
			}
			probeRequestResponseAsyncResult.discoveryServiceImpl.EndFind(result);
			return true;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000956C File Offset: 0x0000776C
		private bool ProcessFindRequest()
		{
			IAsyncResult asyncResult = this.discoveryServiceImpl.BeginFind(this.findRequest, base.PrepareAsyncCompletion(ProbeRequestResponseAsyncResult<TProbeMessage, TResponseMessage>.onOnFindCompletedCallback), this);
			return asyncResult.CompletedSynchronously && ProbeRequestResponseAsyncResult<TProbeMessage, TResponseMessage>.OnOnFindCompleted(asyncResult);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000095A8 File Offset: 0x000077A8
		private bool EnsureNotDuplicate()
		{
			bool flag = this.discoveryServiceImpl.IsDuplicate(OperationContext.Current.IncomingMessageHeaders.MessageId);
			if (flag && TD.DuplicateDiscoveryMessageIsEnabled())
			{
				TD.DuplicateDiscoveryMessage(this.context.EventTraceActivity, "Probe", OperationContext.Current.IncomingMessageHeaders.MessageId.ToString());
			}
			return !flag;
		}

		// Token: 0x040000C6 RID: 198
		private readonly IDiscoveryServiceImplementation discoveryServiceImpl;

		// Token: 0x040000C7 RID: 199
		private readonly ProbeRequestResponseAsyncResult<TProbeMessage, TResponseMessage>.FindRequestResponseContext findRequest;

		// Token: 0x040000C8 RID: 200
		private readonly DiscoveryOperationContext context;

		// Token: 0x040000C9 RID: 201
		private static AsyncResult.AsyncCompletion onOnFindCompletedCallback = new AsyncResult.AsyncCompletion(ProbeRequestResponseAsyncResult<TProbeMessage, TResponseMessage>.OnOnFindCompleted);

		// Token: 0x040000CA RID: 202
		private bool isFindCompleted;

		// Token: 0x040000CB RID: 203
		private object findCompletedLock;

		// Token: 0x020000DD RID: 221
		private class FindRequestResponseContext : FindRequestContext
		{
			// Token: 0x0600082C RID: 2092 RVA: 0x00015280 File Offset: 0x00013480
			public FindRequestResponseContext(FindCriteria criteria, ProbeRequestResponseAsyncResult<TProbeMessage, TResponseMessage> probeRequestResponseAsyncResult) : base(criteria)
			{
				this.matchingEndpoints = new Collection<EndpointDiscoveryMetadata>();
				this.probeRequestResponseAsyncResult = probeRequestResponseAsyncResult;
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001529B File Offset: 0x0001349B
			public Collection<EndpointDiscoveryMetadata> MatchingEndpoints
			{
				get
				{
					return this.matchingEndpoints;
				}
			}

			// Token: 0x0600082E RID: 2094 RVA: 0x000152A4 File Offset: 0x000134A4
			protected override void OnAddMatchingEndpoint(EndpointDiscoveryMetadata matchingEndpoint)
			{
				object findCompletedLock = this.probeRequestResponseAsyncResult.findCompletedLock;
				lock (findCompletedLock)
				{
					if (this.probeRequestResponseAsyncResult.isFindCompleted)
					{
						throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryCannotAddMatchingEndpoint));
					}
					this.matchingEndpoints.Add(matchingEndpoint);
				}
			}

			// Token: 0x0400021D RID: 541
			private Collection<EndpointDiscoveryMetadata> matchingEndpoints;

			// Token: 0x0400021E RID: 542
			private readonly ProbeRequestResponseAsyncResult<TProbeMessage, TResponseMessage> probeRequestResponseAsyncResult;
		}
	}
}
