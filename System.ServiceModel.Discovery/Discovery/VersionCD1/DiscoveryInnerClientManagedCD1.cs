using System;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000061 RID: 97
	internal class DiscoveryInnerClientManagedCD1 : ClientBase<IDiscoveryContractManagedCD1>, IDiscoveryInnerClient
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x0000F503 File Offset: 0x0000D703
		internal DiscoveryInnerClientManagedCD1(DiscoveryEndpoint discoveryEndpoint, IDiscoveryInnerClientResponse responseReceiver) : base(discoveryEndpoint)
		{
			this.responseReceiver = responseReceiver;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000F513 File Offset: 0x0000D713
		public new ClientCredentials ClientCredentials
		{
			get
			{
				return base.ClientCredentials;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0000F51B File Offset: 0x0000D71B
		public new ChannelFactory ChannelFactory
		{
			get
			{
				return base.ChannelFactory;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0000F523 File Offset: 0x0000D723
		public new IClientChannel InnerChannel
		{
			get
			{
				return base.InnerChannel;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000F52B File Offset: 0x0000D72B
		public new ServiceEndpoint Endpoint
		{
			get
			{
				return base.Endpoint;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0000EE96 File Offset: 0x0000D096
		public ICommunicationObject InnerCommunicationObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000C68B File Offset: 0x0000A88B
		public bool IsRequestResponse
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000F534 File Offset: 0x0000D734
		public IAsyncResult BeginProbeOperation(FindCriteria findCriteria, AsyncCallback callback, object state)
		{
			ProbeMessageCD1 probeMessageCD = new ProbeMessageCD1();
			probeMessageCD.Probe = FindCriteriaCD1.FromFindCriteria(findCriteria);
			return base.Channel.BeginProbeOperation(probeMessageCD, callback, state);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000F564 File Offset: 0x0000D764
		public IAsyncResult BeginResolveOperation(ResolveCriteria resolveCriteria, AsyncCallback callback, object state)
		{
			ResolveMessageCD1 resolveMessageCD = new ResolveMessageCD1();
			resolveMessageCD.Resolve = ResolveCriteriaCD1.FromResolveCriteria(resolveCriteria);
			return base.Channel.BeginResolveOperation(resolveMessageCD, callback, state);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000F594 File Offset: 0x0000D794
		public void EndProbeOperation(IAsyncResult result)
		{
			ProbeMatchesMessageCD1 probeMatchesMessageCD = base.Channel.EndProbeOperation(result);
			AsyncOperationContext asyncOperationContext = (AsyncOperationContext)result.AsyncState;
			if (probeMatchesMessageCD != null && probeMatchesMessageCD.ProbeMatches != null)
			{
				this.responseReceiver.ProbeMatchOperation(asyncOperationContext.OperationId, DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(probeMatchesMessageCD.MessageSequence), DiscoveryUtility.ToEndpointDiscoveryMetadataCollection(probeMatchesMessageCD.ProbeMatches), true);
				return;
			}
			this.responseReceiver.PostFindCompletedAndRemove(asyncOperationContext.OperationId, false, null);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000F604 File Offset: 0x0000D804
		public void EndResolveOperation(IAsyncResult result)
		{
			ResolveMatchesMessageCD1 resolveMatchesMessageCD = base.Channel.EndResolveOperation(result);
			AsyncOperationContext asyncOperationContext = (AsyncOperationContext)result.AsyncState;
			if (resolveMatchesMessageCD != null && resolveMatchesMessageCD.ResolveMatches != null && resolveMatchesMessageCD.ResolveMatches.ResolveMatch != null)
			{
				this.responseReceiver.ResolveMatchOperation(asyncOperationContext.OperationId, DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(resolveMatchesMessageCD.MessageSequence), resolveMatchesMessageCD.ResolveMatches.ResolveMatch.ToEndpointDiscoveryMetadata());
				return;
			}
			this.responseReceiver.PostResolveCompletedAndRemove(asyncOperationContext.OperationId, false, null);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000F684 File Offset: 0x0000D884
		public void ProbeOperation(FindCriteria findCriteria)
		{
			ProbeMessageCD1 probeMessageCD = new ProbeMessageCD1();
			probeMessageCD.Probe = FindCriteriaCD1.FromFindCriteria(findCriteria);
			ProbeMatchesMessageCD1 probeMatchesMessageCD = base.Channel.ProbeOperation(probeMessageCD);
			if (probeMatchesMessageCD != null && probeMatchesMessageCD.ProbeMatches != null)
			{
				this.responseReceiver.ProbeMatchOperation(OperationContext.Current.IncomingMessageHeaders.RelatesTo, DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(probeMatchesMessageCD.MessageSequence), DiscoveryUtility.ToEndpointDiscoveryMetadataCollection(probeMatchesMessageCD.ProbeMatches), true);
				return;
			}
			this.responseReceiver.PostFindCompletedAndRemove(OperationContext.Current.IncomingMessageHeaders.RelatesTo, false, null);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000F70C File Offset: 0x0000D90C
		public void ResolveOperation(ResolveCriteria resolveCriteria)
		{
			ResolveMessageCD1 resolveMessageCD = new ResolveMessageCD1();
			resolveMessageCD.Resolve = ResolveCriteriaCD1.FromResolveCriteria(resolveCriteria);
			ResolveMatchesMessageCD1 resolveMatchesMessageCD = base.Channel.ResolveOperation(resolveMessageCD);
			if (resolveMatchesMessageCD != null && resolveMatchesMessageCD.ResolveMatches != null && resolveMatchesMessageCD.ResolveMatches.ResolveMatch != null)
			{
				this.responseReceiver.ResolveMatchOperation(OperationContext.Current.IncomingMessageHeaders.RelatesTo, DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(resolveMatchesMessageCD.MessageSequence), resolveMatchesMessageCD.ResolveMatches.ResolveMatch.ToEndpointDiscoveryMetadata());
				return;
			}
			this.responseReceiver.PostResolveCompletedAndRemove(OperationContext.Current.IncomingMessageHeaders.RelatesTo, false, null);
		}

		// Token: 0x0400013D RID: 317
		private IDiscoveryInnerClientResponse responseReceiver;
	}
}
