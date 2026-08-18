using System;
using System.ComponentModel;
using System.Runtime;
using System.ServiceModel.Description;
using System.Threading;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x02000077 RID: 119
	internal class AnnouncementInnerClientApril2005 : ClientBase<IAnnouncementContractApril2005>, IAnnouncementInnerClient
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x000100A3 File Offset: 0x0000E2A3
		public AnnouncementInnerClientApril2005(AnnouncementEndpoint announcementEndpoint) : base(announcementEndpoint)
		{
			this.discoveryMessageSequenceGenerator = new DiscoveryMessageSequenceGenerator();
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000596 RID: 1430 RVA: 0x000100B8 File Offset: 0x0000E2B8
		// (remove) Token: 0x06000597 RID: 1431 RVA: 0x000100F0 File Offset: 0x0000E2F0
		private event EventHandler<AsyncCompletedEventArgs> HelloOperationCompletedEventHandler;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000598 RID: 1432 RVA: 0x00010128 File Offset: 0x0000E328
		// (remove) Token: 0x06000599 RID: 1433 RVA: 0x00010160 File Offset: 0x0000E360
		private event EventHandler<AsyncCompletedEventArgs> ByeOperationCompletedEventHandler;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x0600059A RID: 1434 RVA: 0x00010195 File Offset: 0x0000E395
		// (remove) Token: 0x0600059B RID: 1435 RVA: 0x0001019E File Offset: 0x0000E39E
		event EventHandler<AsyncCompletedEventArgs> IAnnouncementInnerClient.HelloOperationCompleted
		{
			add
			{
				this.HelloOperationCompletedEventHandler += value;
			}
			remove
			{
				this.HelloOperationCompletedEventHandler -= value;
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x0600059C RID: 1436 RVA: 0x000101A7 File Offset: 0x0000E3A7
		// (remove) Token: 0x0600059D RID: 1437 RVA: 0x000101B0 File Offset: 0x0000E3B0
		event EventHandler<AsyncCompletedEventArgs> IAnnouncementInnerClient.ByeOperationCompleted
		{
			add
			{
				this.ByeOperationCompletedEventHandler += value;
			}
			remove
			{
				this.ByeOperationCompletedEventHandler -= value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x000101B9 File Offset: 0x0000E3B9
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x000101C1 File Offset: 0x0000E3C1
		public DiscoveryMessageSequenceGenerator DiscoveryMessageSequenceGenerator
		{
			get
			{
				return this.discoveryMessageSequenceGenerator;
			}
			set
			{
				this.discoveryMessageSequenceGenerator = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x000101CA File Offset: 0x0000E3CA
		public new ChannelFactory ChannelFactory
		{
			get
			{
				return base.ChannelFactory;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x000101D2 File Offset: 0x0000E3D2
		public new IClientChannel InnerChannel
		{
			get
			{
				return base.InnerChannel;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x000101DA File Offset: 0x0000E3DA
		public new ServiceEndpoint Endpoint
		{
			get
			{
				return base.Endpoint;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000EE96 File Offset: 0x0000D096
		public ICommunicationObject InnerCommunicationObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public void HelloOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			HelloMessageApril2005 message = HelloMessageApril2005.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			base.Channel.HelloOperation(message);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00010210 File Offset: 0x0000E410
		public void ByeOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			ByeMessageApril2005 message = ByeMessageApril2005.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			base.Channel.ByeOperation(message);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001023C File Offset: 0x0000E43C
		public IAsyncResult BeginHelloOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			HelloMessageApril2005 message = HelloMessageApril2005.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			return base.Channel.BeginHelloOperation(message, callback, state);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00010269 File Offset: 0x0000E469
		private IAsyncResult BeginHelloOperation(HelloMessageApril2005 message, AsyncCallback callback, object state)
		{
			return base.Channel.BeginHelloOperation(message, callback, state);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00010279 File Offset: 0x0000E479
		public void EndHelloOperation(IAsyncResult result)
		{
			base.Channel.EndHelloOperation(result);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00010288 File Offset: 0x0000E488
		public IAsyncResult BeginByeOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			ByeMessageApril2005 message = ByeMessageApril2005.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			return base.Channel.BeginByeOperation(message, callback, state);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000102B5 File Offset: 0x0000E4B5
		private IAsyncResult BeginByeOperation(ByeMessageApril2005 message, AsyncCallback callback, object state)
		{
			return base.Channel.BeginByeOperation(message, callback, state);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000102C5 File Offset: 0x0000E4C5
		public void EndByeOperation(IAsyncResult result)
		{
			base.Channel.EndByeOperation(result);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000102D4 File Offset: 0x0000E4D4
		private IAsyncResult OnBeginHelloOperation(object[] inValues, AsyncCallback callback, object asyncState)
		{
			HelloMessageApril2005 message = (HelloMessageApril2005)inValues[0];
			return this.BeginHelloOperation(message, callback, asyncState);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000102F3 File Offset: 0x0000E4F3
		private object[] OnEndHelloOperation(IAsyncResult result)
		{
			this.EndHelloOperation(result);
			return null;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00010300 File Offset: 0x0000E500
		private void OnHelloOperationCompleted(object state)
		{
			if (this.HelloOperationCompletedEventHandler != null)
			{
				ClientBase<IAnnouncementContractApril2005>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IAnnouncementContractApril2005>.InvokeAsyncCompletedEventArgs)state;
				this.HelloOperationCompletedEventHandler(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00010340 File Offset: 0x0000E540
		public void HelloOperationAsync(EndpointDiscoveryMetadata endpointDiscoveryMetadata, object userState)
		{
			HelloMessageApril2005 helloMessageApril = HelloMessageApril2005.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			if (this.onBeginHelloOperationDelegate == null)
			{
				this.onBeginHelloOperationDelegate = new ClientBase<IAnnouncementContractApril2005>.BeginOperationDelegate(this.OnBeginHelloOperation);
			}
			if (this.onEndHelloOperationDelegate == null)
			{
				this.onEndHelloOperationDelegate = new ClientBase<IAnnouncementContractApril2005>.EndOperationDelegate(this.OnEndHelloOperation);
			}
			if (this.onHelloOperationCompletedDelegate == null)
			{
				this.onHelloOperationCompletedDelegate = Fx.ThunkCallback(new SendOrPostCallback(this.OnHelloOperationCompleted));
			}
			base.InvokeAsync(this.onBeginHelloOperationDelegate, new object[]
			{
				helloMessageApril
			}, this.onEndHelloOperationDelegate, this.onHelloOperationCompletedDelegate, userState);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000103D8 File Offset: 0x0000E5D8
		private IAsyncResult OnBeginByeOperation(object[] inValues, AsyncCallback callback, object asyncState)
		{
			ByeMessageApril2005 message = (ByeMessageApril2005)inValues[0];
			return this.BeginByeOperation(message, callback, asyncState);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000103F7 File Offset: 0x0000E5F7
		private object[] OnEndByeOperation(IAsyncResult result)
		{
			this.EndByeOperation(result);
			return null;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00010404 File Offset: 0x0000E604
		private void OnByeOperationCompleted(object state)
		{
			if (this.ByeOperationCompletedEventHandler != null)
			{
				ClientBase<IAnnouncementContractApril2005>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IAnnouncementContractApril2005>.InvokeAsyncCompletedEventArgs)state;
				this.ByeOperationCompletedEventHandler(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00010444 File Offset: 0x0000E644
		public void ByeOperationAsync(EndpointDiscoveryMetadata endpointDiscoveryMetadata, object userState)
		{
			ByeMessageApril2005 byeMessageApril = ByeMessageApril2005.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			if (this.onBeginByeOperationDelegate == null)
			{
				this.onBeginByeOperationDelegate = new ClientBase<IAnnouncementContractApril2005>.BeginOperationDelegate(this.OnBeginByeOperation);
			}
			if (this.onEndByeOperationDelegate == null)
			{
				this.onEndByeOperationDelegate = new ClientBase<IAnnouncementContractApril2005>.EndOperationDelegate(this.OnEndByeOperation);
			}
			if (this.onByeOperationCompletedDelegate == null)
			{
				this.onByeOperationCompletedDelegate = Fx.ThunkCallback(new SendOrPostCallback(this.OnByeOperationCompleted));
			}
			base.InvokeAsync(this.onBeginByeOperationDelegate, new object[]
			{
				byeMessageApril
			}, this.onEndByeOperationDelegate, this.onByeOperationCompletedDelegate, userState);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000104D9 File Offset: 0x0000E6D9
		ClientCredentials IAnnouncementInnerClient.get_ClientCredentials()
		{
			return base.ClientCredentials;
		}

		// Token: 0x04000157 RID: 343
		private DiscoveryMessageSequenceGenerator discoveryMessageSequenceGenerator;

		// Token: 0x04000158 RID: 344
		private ClientBase<IAnnouncementContractApril2005>.BeginOperationDelegate onBeginHelloOperationDelegate;

		// Token: 0x04000159 RID: 345
		private ClientBase<IAnnouncementContractApril2005>.EndOperationDelegate onEndHelloOperationDelegate;

		// Token: 0x0400015A RID: 346
		private SendOrPostCallback onHelloOperationCompletedDelegate;

		// Token: 0x0400015B RID: 347
		private ClientBase<IAnnouncementContractApril2005>.BeginOperationDelegate onBeginByeOperationDelegate;

		// Token: 0x0400015C RID: 348
		private ClientBase<IAnnouncementContractApril2005>.EndOperationDelegate onEndByeOperationDelegate;

		// Token: 0x0400015D RID: 349
		private SendOrPostCallback onByeOperationCompletedDelegate;
	}
}
