using System;
using System.Runtime;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x02000092 RID: 146
	internal class DiscoveryInnerClientAdhoc11 : IDiscoveryInnerClient, IDiscoveryResponseContract11
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x000114EB File Offset: 0x0000F6EB
		public DiscoveryInnerClientAdhoc11(DiscoveryEndpoint discoveryEndpoint, IDiscoveryInnerClientResponse responseReceiver)
		{
			this.responseReceiver = responseReceiver;
			if (discoveryEndpoint.Behaviors.Find<DiscoveryCallbackBehavior>() == null)
			{
				discoveryEndpoint.Behaviors.Insert(0, new DiscoveryCallbackBehavior());
			}
			this.duplexInnerClient = new DiscoveryInnerClientAdhoc11.DuplexClient11(this, discoveryEndpoint);
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00011525 File Offset: 0x0000F725
		public ClientCredentials ClientCredentials
		{
			get
			{
				return this.duplexInnerClient.ClientCredentials;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00011532 File Offset: 0x0000F732
		public ChannelFactory ChannelFactory
		{
			get
			{
				return this.duplexInnerClient.ChannelFactory;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001153F File Offset: 0x0000F73F
		public IClientChannel InnerChannel
		{
			get
			{
				return this.duplexInnerClient.InnerChannel;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001154C File Offset: 0x0000F74C
		public ServiceEndpoint Endpoint
		{
			get
			{
				return this.duplexInnerClient.Endpoint;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00011559 File Offset: 0x0000F759
		public ICommunicationObject InnerCommunicationObject
		{
			get
			{
				return this.duplexInnerClient;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0000F28D File Offset: 0x0000D48D
		public bool IsRequestResponse
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00011564 File Offset: 0x0000F764
		public IAsyncResult BeginProbeOperation(FindCriteria findCriteria, AsyncCallback callback, object state)
		{
			ProbeMessage11 probeMessage = new ProbeMessage11();
			probeMessage.Probe = FindCriteria11.FromFindCriteria(findCriteria);
			return this.duplexInnerClient.BeginProbeOperation(probeMessage, callback, state);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00011594 File Offset: 0x0000F794
		public IAsyncResult BeginResolveOperation(ResolveCriteria resolveCriteria, AsyncCallback callback, object state)
		{
			ResolveMessage11 resolveMessage = new ResolveMessage11();
			resolveMessage.Resolve = ResolveCriteria11.FromResolveCriteria(resolveCriteria);
			return this.duplexInnerClient.BeginResolveOperation(resolveMessage, callback, state);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000115C1 File Offset: 0x0000F7C1
		public void EndProbeOperation(IAsyncResult result)
		{
			this.duplexInnerClient.EndProbeOperation(result);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000115CF File Offset: 0x0000F7CF
		public void EndResolveOperation(IAsyncResult result)
		{
			this.duplexInnerClient.EndResolveOperation(result);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000115E0 File Offset: 0x0000F7E0
		public void ProbeOperation(FindCriteria findCriteria)
		{
			ProbeMessage11 probeMessage = new ProbeMessage11();
			probeMessage.Probe = FindCriteria11.FromFindCriteria(findCriteria);
			this.duplexInnerClient.ProbeOperation(probeMessage);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001160C File Offset: 0x0000F80C
		public void ResolveOperation(ResolveCriteria resolveCriteria)
		{
			ResolveMessage11 resolveMessage = new ResolveMessage11();
			resolveMessage.Resolve = ResolveCriteria11.FromResolveCriteria(resolveCriteria);
			this.duplexInnerClient.ResolveOperation(resolveMessage);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00011638 File Offset: 0x0000F838
		public IAsyncResult BeginProbeMatchOperation(ProbeMatchesMessage11 response, AsyncCallback callback, object state)
		{
			if (response.MessageSequence != null && response.ProbeMatches != null)
			{
				this.responseReceiver.ProbeMatchOperation(OperationContext.Current.IncomingMessageHeaders.RelatesTo, response.MessageSequence.ToDiscoveryMessageSequence(), DiscoveryUtility.ToEndpointDiscoveryMetadataCollection(response.ProbeMatches), false);
			}
			else if (TD.DiscoveryMessageWithNullMessageSequenceIsEnabled() && response.MessageSequence == null)
			{
				TD.DiscoveryMessageWithNullMessageSequence("ProbeMatches", OperationContext.Current.IncomingMessageHeaders.MessageId.ToString());
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000031C9 File Offset: 0x000013C9
		public void EndProbeMatchOperation(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000116BC File Offset: 0x0000F8BC
		public IAsyncResult BeginResolveMatchOperation(ResolveMatchesMessage11 response, AsyncCallback callback, object state)
		{
			if (response.MessageSequence != null && response.ResolveMatches != null && response.ResolveMatches.ResolveMatch != null)
			{
				this.responseReceiver.ResolveMatchOperation(OperationContext.Current.IncomingMessageHeaders.RelatesTo, response.MessageSequence.ToDiscoveryMessageSequence(), response.ResolveMatches.ResolveMatch.ToEndpointDiscoveryMetadata());
			}
			else if (TD.DiscoveryMessageWithNullMessageSequenceIsEnabled() && response.MessageSequence == null)
			{
				TD.DiscoveryMessageWithNullMessageSequence("ResolveMatches", OperationContext.Current.IncomingMessageHeaders.MessageId.ToString());
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000031C9 File Offset: 0x000013C9
		public void EndResolveMatchOperation(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00011754 File Offset: 0x0000F954
		public IAsyncResult BeginHelloOperation(HelloMessage11 message, AsyncCallback callback, object state)
		{
			if (message.MessageSequence != null && message.Hello != null)
			{
				this.responseReceiver.HelloOperation(OperationContext.Current.IncomingMessageHeaders.RelatesTo, message.MessageSequence.ToDiscoveryMessageSequence(), message.Hello.ToEndpointDiscoveryMetadata());
			}
			else if (TD.DiscoveryMessageWithNullMessageSequenceIsEnabled() && message.MessageSequence == null)
			{
				TD.DiscoveryMessageWithNullMessageSequence("Hello", OperationContext.Current.IncomingMessageHeaders.MessageId.ToString());
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000031C9 File Offset: 0x000013C9
		public void EndHelloOperation(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x04000188 RID: 392
		private IDiscoveryInnerClientResponse responseReceiver;

		// Token: 0x04000189 RID: 393
		private DiscoveryInnerClientAdhoc11.DuplexClient11 duplexInnerClient;

		// Token: 0x020000F2 RID: 242
		private class DuplexClient11 : DuplexClientBase<IDiscoveryContractAdhoc11>
		{
			// Token: 0x06000866 RID: 2150 RVA: 0x000156BF File Offset: 0x000138BF
			public DuplexClient11(object callbackInstance, DiscoveryEndpoint discoveryEndpoint) : base(callbackInstance, discoveryEndpoint)
			{
			}

			// Token: 0x06000867 RID: 2151 RVA: 0x000156C9 File Offset: 0x000138C9
			public void ProbeOperation(ProbeMessage11 request)
			{
				base.Channel.ProbeOperation(request);
			}

			// Token: 0x06000868 RID: 2152 RVA: 0x000156D7 File Offset: 0x000138D7
			public void ResolveOperation(ResolveMessage11 request)
			{
				base.Channel.ResolveOperation(request);
			}

			// Token: 0x06000869 RID: 2153 RVA: 0x000156E5 File Offset: 0x000138E5
			public IAsyncResult BeginProbeOperation(ProbeMessage11 request, AsyncCallback callback, object state)
			{
				return base.Channel.BeginProbeOperation(request, callback, state);
			}

			// Token: 0x0600086A RID: 2154 RVA: 0x000156F5 File Offset: 0x000138F5
			public IAsyncResult BeginResolveOperation(ResolveMessage11 request, AsyncCallback callback, object state)
			{
				return base.Channel.BeginResolveOperation(request, callback, state);
			}

			// Token: 0x0600086B RID: 2155 RVA: 0x00015705 File Offset: 0x00013905
			public void EndProbeOperation(IAsyncResult result)
			{
				base.Channel.EndProbeOperation(result);
			}

			// Token: 0x0600086C RID: 2156 RVA: 0x00015713 File Offset: 0x00013913
			public void EndResolveOperation(IAsyncResult result)
			{
				base.Channel.EndResolveOperation(result);
			}
		}
	}
}
