using System;
using System.Runtime;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000060 RID: 96
	internal class DiscoveryInnerClientAdhocCD1 : IDiscoveryInnerClient, IDiscoveryResponseContractCD1
	{
		// Token: 0x060004F1 RID: 1265 RVA: 0x0000F217 File Offset: 0x0000D417
		public DiscoveryInnerClientAdhocCD1(DiscoveryEndpoint discoveryEndpoint, IDiscoveryInnerClientResponse responseReceiver)
		{
			this.responseReceiver = responseReceiver;
			if (discoveryEndpoint.Behaviors.Find<DiscoveryCallbackBehavior>() == null)
			{
				discoveryEndpoint.Behaviors.Insert(0, new DiscoveryCallbackBehavior());
			}
			this.duplexInnerClient = new DiscoveryInnerClientAdhocCD1.DuplexClientCD1(this, discoveryEndpoint);
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0000F251 File Offset: 0x0000D451
		public ClientCredentials ClientCredentials
		{
			get
			{
				return this.duplexInnerClient.ClientCredentials;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000F25E File Offset: 0x0000D45E
		public ChannelFactory ChannelFactory
		{
			get
			{
				return this.duplexInnerClient.ChannelFactory;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000F26B File Offset: 0x0000D46B
		public IClientChannel InnerChannel
		{
			get
			{
				return this.duplexInnerClient.InnerChannel;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000F278 File Offset: 0x0000D478
		public ServiceEndpoint Endpoint
		{
			get
			{
				return this.duplexInnerClient.Endpoint;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000F285 File Offset: 0x0000D485
		public ICommunicationObject InnerCommunicationObject
		{
			get
			{
				return this.duplexInnerClient;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000F28D File Offset: 0x0000D48D
		public bool IsRequestResponse
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000F290 File Offset: 0x0000D490
		public IAsyncResult BeginProbeOperation(FindCriteria findCriteria, AsyncCallback callback, object state)
		{
			ProbeMessageCD1 probeMessageCD = new ProbeMessageCD1();
			probeMessageCD.Probe = FindCriteriaCD1.FromFindCriteria(findCriteria);
			return this.duplexInnerClient.BeginProbeOperation(probeMessageCD, callback, state);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000F2C0 File Offset: 0x0000D4C0
		public IAsyncResult BeginResolveOperation(ResolveCriteria resolveCriteria, AsyncCallback callback, object state)
		{
			ResolveMessageCD1 resolveMessageCD = new ResolveMessageCD1();
			resolveMessageCD.Resolve = ResolveCriteriaCD1.FromResolveCriteria(resolveCriteria);
			return this.duplexInnerClient.BeginResolveOperation(resolveMessageCD, callback, state);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000F2ED File Offset: 0x0000D4ED
		public void EndProbeOperation(IAsyncResult result)
		{
			this.duplexInnerClient.EndProbeOperation(result);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000F2FB File Offset: 0x0000D4FB
		public void EndResolveOperation(IAsyncResult result)
		{
			this.duplexInnerClient.EndResolveOperation(result);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000F30C File Offset: 0x0000D50C
		public void ProbeOperation(FindCriteria findCriteria)
		{
			ProbeMessageCD1 probeMessageCD = new ProbeMessageCD1();
			probeMessageCD.Probe = FindCriteriaCD1.FromFindCriteria(findCriteria);
			this.duplexInnerClient.ProbeOperation(probeMessageCD);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000F338 File Offset: 0x0000D538
		public void ResolveOperation(ResolveCriteria resolveCriteria)
		{
			ResolveMessageCD1 resolveMessageCD = new ResolveMessageCD1();
			resolveMessageCD.Resolve = ResolveCriteriaCD1.FromResolveCriteria(resolveCriteria);
			this.duplexInnerClient.ResolveOperation(resolveMessageCD);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000F364 File Offset: 0x0000D564
		public IAsyncResult BeginProbeMatchOperation(ProbeMatchesMessageCD1 response, AsyncCallback callback, object state)
		{
			if (response.MessageSequence != null && response.ProbeMatches != null)
			{
				this.responseReceiver.ProbeMatchOperation(OperationContext.Current.IncomingMessageHeaders.RelatesTo, response.MessageSequence.ToDiscoveryMessageSequence(), DiscoveryUtility.ToEndpointDiscoveryMetadataCollection(response.ProbeMatches), false);
			}
			else if (response.MessageSequence == null && TD.DiscoveryMessageWithNullMessageSequenceIsEnabled())
			{
				TD.DiscoveryMessageWithNullMessageSequence("ProbeMatches", OperationContext.Current.IncomingMessageHeaders.MessageId.ToString());
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000031C9 File Offset: 0x000013C9
		public void EndProbeMatchOperation(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		public IAsyncResult BeginResolveMatchOperation(ResolveMatchesMessageCD1 response, AsyncCallback callback, object state)
		{
			if (response.MessageSequence != null && response.ResolveMatches != null && response.ResolveMatches.ResolveMatch != null)
			{
				this.responseReceiver.ResolveMatchOperation(OperationContext.Current.IncomingMessageHeaders.RelatesTo, response.MessageSequence.ToDiscoveryMessageSequence(), response.ResolveMatches.ResolveMatch.ToEndpointDiscoveryMetadata());
			}
			else if (response.MessageSequence == null && TD.DiscoveryMessageWithNullMessageSequenceIsEnabled())
			{
				TD.DiscoveryMessageWithNullMessageSequence("ResolveMatches", OperationContext.Current.IncomingMessageHeaders.MessageId.ToString());
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000031C9 File Offset: 0x000013C9
		public void EndResolveMatchOperation(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000F480 File Offset: 0x0000D680
		public IAsyncResult BeginHelloOperation(HelloMessageCD1 message, AsyncCallback callback, object state)
		{
			if (message.MessageSequence != null && message.Hello != null)
			{
				this.responseReceiver.HelloOperation(OperationContext.Current.IncomingMessageHeaders.RelatesTo, message.MessageSequence.ToDiscoveryMessageSequence(), message.Hello.ToEndpointDiscoveryMetadata());
			}
			else if (message.MessageSequence == null && TD.DiscoveryMessageWithNullMessageSequenceIsEnabled())
			{
				TD.DiscoveryMessageWithNullMessageSequence("Hello", OperationContext.Current.IncomingMessageHeaders.MessageId.ToString());
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000031C9 File Offset: 0x000013C9
		public void EndHelloOperation(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x0400013B RID: 315
		private IDiscoveryInnerClientResponse responseReceiver;

		// Token: 0x0400013C RID: 316
		private DiscoveryInnerClientAdhocCD1.DuplexClientCD1 duplexInnerClient;

		// Token: 0x020000F0 RID: 240
		private class DuplexClientCD1 : DuplexClientBase<IDiscoveryContractAdhocCD1>
		{
			// Token: 0x06000858 RID: 2136 RVA: 0x000155DD File Offset: 0x000137DD
			public DuplexClientCD1(object callbackInstance, DiscoveryEndpoint discoveryEndpoint) : base(callbackInstance, discoveryEndpoint)
			{
			}

			// Token: 0x06000859 RID: 2137 RVA: 0x000155E7 File Offset: 0x000137E7
			public void ProbeOperation(ProbeMessageCD1 request)
			{
				base.Channel.ProbeOperation(request);
			}

			// Token: 0x0600085A RID: 2138 RVA: 0x000155F5 File Offset: 0x000137F5
			public void ResolveOperation(ResolveMessageCD1 request)
			{
				base.Channel.ResolveOperation(request);
			}

			// Token: 0x0600085B RID: 2139 RVA: 0x00015603 File Offset: 0x00013803
			public IAsyncResult BeginProbeOperation(ProbeMessageCD1 request, AsyncCallback callback, object state)
			{
				return base.Channel.BeginProbeOperation(request, callback, state);
			}

			// Token: 0x0600085C RID: 2140 RVA: 0x00015613 File Offset: 0x00013813
			public IAsyncResult BeginResolveOperation(ResolveMessageCD1 request, AsyncCallback callback, object state)
			{
				return base.Channel.BeginResolveOperation(request, callback, state);
			}

			// Token: 0x0600085D RID: 2141 RVA: 0x00015623 File Offset: 0x00013823
			public void EndProbeOperation(IAsyncResult result)
			{
				base.Channel.EndProbeOperation(result);
			}

			// Token: 0x0600085E RID: 2142 RVA: 0x00015631 File Offset: 0x00013831
			public void EndResolveOperation(IAsyncResult result)
			{
				base.Channel.EndResolveOperation(result);
			}
		}
	}
}
