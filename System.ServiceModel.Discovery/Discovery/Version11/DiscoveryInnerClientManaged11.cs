using System;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x02000093 RID: 147
	internal class DiscoveryInnerClientManaged11 : ClientBase<IDiscoveryContractManaged11>, IDiscoveryInnerClient
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x000117D7 File Offset: 0x0000F9D7
		internal DiscoveryInnerClientManaged11(DiscoveryEndpoint discoveryEndpoint, IDiscoveryInnerClientResponse responseReceiver) : base(discoveryEndpoint)
		{
			this.responseReceiver = responseReceiver;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x000117E7 File Offset: 0x0000F9E7
		public new ClientCredentials ClientCredentials
		{
			get
			{
				return base.ClientCredentials;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x000117EF File Offset: 0x0000F9EF
		public new ChannelFactory ChannelFactory
		{
			get
			{
				return base.ChannelFactory;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000117F7 File Offset: 0x0000F9F7
		public new IClientChannel InnerChannel
		{
			get
			{
				return base.InnerChannel;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x000117FF File Offset: 0x0000F9FF
		public new ServiceEndpoint Endpoint
		{
			get
			{
				return base.Endpoint;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0000EE96 File Offset: 0x0000D096
		public ICommunicationObject InnerCommunicationObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0000C68B File Offset: 0x0000A88B
		public bool IsRequestResponse
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00011808 File Offset: 0x0000FA08
		public IAsyncResult BeginProbeOperation(FindCriteria findCriteria, AsyncCallback callback, object state)
		{
			ProbeMessage11 probeMessage = new ProbeMessage11();
			probeMessage.Probe = FindCriteria11.FromFindCriteria(findCriteria);
			return base.Channel.BeginProbeOperation(probeMessage, callback, state);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00011838 File Offset: 0x0000FA38
		public IAsyncResult BeginResolveOperation(ResolveCriteria resolveCriteria, AsyncCallback callback, object state)
		{
			ResolveMessage11 resolveMessage = new ResolveMessage11();
			resolveMessage.Resolve = ResolveCriteria11.FromResolveCriteria(resolveCriteria);
			return base.Channel.BeginResolveOperation(resolveMessage, callback, state);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00011868 File Offset: 0x0000FA68
		public void EndProbeOperation(IAsyncResult result)
		{
			ProbeMatchesMessage11 probeMatchesMessage = base.Channel.EndProbeOperation(result);
			AsyncOperationContext asyncOperationContext = (AsyncOperationContext)result.AsyncState;
			if (probeMatchesMessage != null && probeMatchesMessage.ProbeMatches != null)
			{
				this.responseReceiver.ProbeMatchOperation(asyncOperationContext.OperationId, DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(probeMatchesMessage.MessageSequence), DiscoveryUtility.ToEndpointDiscoveryMetadataCollection(probeMatchesMessage.ProbeMatches), true);
				return;
			}
			this.responseReceiver.PostFindCompletedAndRemove(asyncOperationContext.OperationId, false, null);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000118D8 File Offset: 0x0000FAD8
		public void EndResolveOperation(IAsyncResult result)
		{
			ResolveMatchesMessage11 resolveMatchesMessage = base.Channel.EndResolveOperation(result);
			AsyncOperationContext asyncOperationContext = (AsyncOperationContext)result.AsyncState;
			if (resolveMatchesMessage != null && resolveMatchesMessage.ResolveMatches != null && resolveMatchesMessage.ResolveMatches.ResolveMatch != null)
			{
				this.responseReceiver.ResolveMatchOperation(asyncOperationContext.OperationId, DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(resolveMatchesMessage.MessageSequence), resolveMatchesMessage.ResolveMatches.ResolveMatch.ToEndpointDiscoveryMetadata());
				return;
			}
			this.responseReceiver.PostResolveCompletedAndRemove(asyncOperationContext.OperationId, false, null);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00011958 File Offset: 0x0000FB58
		public void ProbeOperation(FindCriteria findCriteria)
		{
			ProbeMessage11 probeMessage = new ProbeMessage11();
			probeMessage.Probe = FindCriteria11.FromFindCriteria(findCriteria);
			ProbeMatchesMessage11 probeMatchesMessage = base.Channel.ProbeOperation(probeMessage);
			if (probeMatchesMessage != null && probeMatchesMessage.ProbeMatches != null)
			{
				this.responseReceiver.ProbeMatchOperation(OperationContext.Current.IncomingMessageHeaders.RelatesTo, DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(probeMatchesMessage.MessageSequence), DiscoveryUtility.ToEndpointDiscoveryMetadataCollection(probeMatchesMessage.ProbeMatches), true);
				return;
			}
			this.responseReceiver.PostFindCompletedAndRemove(OperationContext.Current.IncomingMessageHeaders.RelatesTo, false, null);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000119E0 File Offset: 0x0000FBE0
		public void ResolveOperation(ResolveCriteria resolveCriteria)
		{
			ResolveMessage11 resolveMessage = new ResolveMessage11();
			resolveMessage.Resolve = ResolveCriteria11.FromResolveCriteria(resolveCriteria);
			ResolveMatchesMessage11 resolveMatchesMessage = base.Channel.ResolveOperation(resolveMessage);
			if (resolveMatchesMessage != null && resolveMatchesMessage.ResolveMatches != null && resolveMatchesMessage.ResolveMatches.ResolveMatch != null)
			{
				this.responseReceiver.ResolveMatchOperation(OperationContext.Current.IncomingMessageHeaders.RelatesTo, DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(resolveMatchesMessage.MessageSequence), resolveMatchesMessage.ResolveMatches.ResolveMatch.ToEndpointDiscoveryMetadata());
				return;
			}
			this.responseReceiver.PostResolveCompletedAndRemove(OperationContext.Current.IncomingMessageHeaders.RelatesTo, false, null);
		}

		// Token: 0x0400018A RID: 394
		private IDiscoveryInnerClientResponse responseReceiver;
	}
}
