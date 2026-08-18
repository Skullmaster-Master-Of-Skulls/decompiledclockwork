using System;
using System.Runtime;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x0200007A RID: 122
	internal class DiscoveryInnerClientApril2005<TChannel> : IDiscoveryInnerClient, IDiscoveryResponseContractApril2005 where TChannel : class, IDiscoveryContractApril2005
	{
		// Token: 0x060005C0 RID: 1472 RVA: 0x0001055D File Offset: 0x0000E75D
		public DiscoveryInnerClientApril2005(DiscoveryEndpoint discoveryEndpoint, IDiscoveryInnerClientResponse responseReceiver)
		{
			this.responseReceiver = responseReceiver;
			if (discoveryEndpoint.Behaviors.Find<DiscoveryCallbackBehavior>() == null)
			{
				discoveryEndpoint.Behaviors.Insert(0, new DiscoveryCallbackBehavior());
			}
			this.duplexInnerClient = new DiscoveryInnerClientApril2005<TChannel>.DuplexClientApril2005(this, discoveryEndpoint);
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00010597 File Offset: 0x0000E797
		public ClientCredentials ClientCredentials
		{
			get
			{
				return this.duplexInnerClient.ClientCredentials;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x000105A4 File Offset: 0x0000E7A4
		public ChannelFactory ChannelFactory
		{
			get
			{
				return this.duplexInnerClient.ChannelFactory;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x000105B1 File Offset: 0x0000E7B1
		public IClientChannel InnerChannel
		{
			get
			{
				return this.duplexInnerClient.InnerChannel;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x000105BE File Offset: 0x0000E7BE
		public ServiceEndpoint Endpoint
		{
			get
			{
				return this.duplexInnerClient.Endpoint;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x000105CB File Offset: 0x0000E7CB
		public ICommunicationObject InnerCommunicationObject
		{
			get
			{
				return this.duplexInnerClient;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000F28D File Offset: 0x0000D48D
		public bool IsRequestResponse
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000105D4 File Offset: 0x0000E7D4
		public IAsyncResult BeginProbeOperation(FindCriteria findCriteria, AsyncCallback callback, object state)
		{
			ProbeMessageApril2005 probeMessageApril = new ProbeMessageApril2005();
			probeMessageApril.Probe = FindCriteriaApril2005.FromFindCriteria(findCriteria);
			return this.duplexInnerClient.BeginProbeOperation(probeMessageApril, callback, state);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00010604 File Offset: 0x0000E804
		public IAsyncResult BeginResolveOperation(ResolveCriteria resolveCriteria, AsyncCallback callback, object state)
		{
			ResolveMessageApril2005 resolveMessageApril = new ResolveMessageApril2005();
			resolveMessageApril.Resolve = ResolveCriteriaApril2005.FromResolveCriteria(resolveCriteria);
			return this.duplexInnerClient.BeginResolveOperation(resolveMessageApril, callback, state);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00010631 File Offset: 0x0000E831
		public void EndProbeOperation(IAsyncResult result)
		{
			this.duplexInnerClient.EndProbeOperation(result);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001063F File Offset: 0x0000E83F
		public void EndResolveOperation(IAsyncResult result)
		{
			this.duplexInnerClient.EndResolveOperation(result);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00010650 File Offset: 0x0000E850
		public void ProbeOperation(FindCriteria findCriteria)
		{
			ProbeMessageApril2005 probeMessageApril = new ProbeMessageApril2005();
			probeMessageApril.Probe = FindCriteriaApril2005.FromFindCriteria(findCriteria);
			this.duplexInnerClient.ProbeOperation(probeMessageApril);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001067C File Offset: 0x0000E87C
		public void ResolveOperation(ResolveCriteria resolveCriteria)
		{
			ResolveMessageApril2005 resolveMessageApril = new ResolveMessageApril2005();
			resolveMessageApril.Resolve = ResolveCriteriaApril2005.FromResolveCriteria(resolveCriteria);
			this.duplexInnerClient.ResolveOperation(resolveMessageApril);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000106A8 File Offset: 0x0000E8A8
		public IAsyncResult BeginProbeMatchOperation(ProbeMatchesMessageApril2005 response, AsyncCallback callback, object state)
		{
			if (response.ProbeMatches != null)
			{
				this.responseReceiver.ProbeMatchOperation(OperationContext.Current.IncomingMessageHeaders.RelatesTo, DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(response.MessageSequence), DiscoveryUtility.ToEndpointDiscoveryMetadataCollection(response.ProbeMatches), false);
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000031C9 File Offset: 0x000013C9
		public void EndProbeMatchOperation(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000106F8 File Offset: 0x0000E8F8
		public IAsyncResult BeginResolveMatchOperation(ResolveMatchesMessageApril2005 response, AsyncCallback callback, object state)
		{
			if (response.ResolveMatches != null && response.ResolveMatches.ResolveMatch != null)
			{
				this.responseReceiver.ResolveMatchOperation(OperationContext.Current.IncomingMessageHeaders.RelatesTo, DiscoveryUtility.ToDiscoveryMessageSequenceOrNull(response.MessageSequence), response.ResolveMatches.ResolveMatch.ToEndpointDiscoveryMetadata());
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000031C9 File Offset: 0x000013C9
		public void EndResolveMatchOperation(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00010758 File Offset: 0x0000E958
		public IAsyncResult BeginHelloOperation(HelloMessageApril2005 message, AsyncCallback callback, object state)
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

		// Token: 0x060005D2 RID: 1490 RVA: 0x000031C9 File Offset: 0x000013C9
		public void EndHelloOperation(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x04000162 RID: 354
		private IDiscoveryInnerClientResponse responseReceiver;

		// Token: 0x04000163 RID: 355
		private DiscoveryInnerClientApril2005<TChannel>.DuplexClientApril2005 duplexInnerClient;

		// Token: 0x020000F1 RID: 241
		private class DuplexClientApril2005 : DuplexClientBase<TChannel>
		{
			// Token: 0x0600085F RID: 2143 RVA: 0x0001563F File Offset: 0x0001383F
			public DuplexClientApril2005(object callbackInstance, DiscoveryEndpoint discoveryEndpoint) : base(callbackInstance, discoveryEndpoint)
			{
			}

			// Token: 0x06000860 RID: 2144 RVA: 0x00015649 File Offset: 0x00013849
			public void ProbeOperation(ProbeMessageApril2005 request)
			{
				base.Channel.ProbeOperation(request);
			}

			// Token: 0x06000861 RID: 2145 RVA: 0x0001565C File Offset: 0x0001385C
			public void ResolveOperation(ResolveMessageApril2005 request)
			{
				base.Channel.ResolveOperation(request);
			}

			// Token: 0x06000862 RID: 2146 RVA: 0x0001566F File Offset: 0x0001386F
			public IAsyncResult BeginProbeOperation(ProbeMessageApril2005 request, AsyncCallback callback, object state)
			{
				return base.Channel.BeginProbeOperation(request, callback, state);
			}

			// Token: 0x06000863 RID: 2147 RVA: 0x00015684 File Offset: 0x00013884
			public IAsyncResult BeginResolveOperation(ResolveMessageApril2005 request, AsyncCallback callback, object state)
			{
				return base.Channel.BeginResolveOperation(request, callback, state);
			}

			// Token: 0x06000864 RID: 2148 RVA: 0x00015699 File Offset: 0x00013899
			public void EndProbeOperation(IAsyncResult result)
			{
				base.Channel.EndProbeOperation(result);
			}

			// Token: 0x06000865 RID: 2149 RVA: 0x000156AC File Offset: 0x000138AC
			public void EndResolveOperation(IAsyncResult result)
			{
				base.Channel.EndResolveOperation(result);
			}
		}
	}
}
