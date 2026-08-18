using System;
using System.ComponentModel;
using System.Runtime;
using System.ServiceModel.Description;
using System.Threading;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x0200008F RID: 143
	internal class AnnouncementInnerClient11 : ClientBase<IAnnouncementContract11>, IAnnouncementInnerClient
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x0001102D File Offset: 0x0000F22D
		public AnnouncementInnerClient11(AnnouncementEndpoint announcementEndpoint) : base(announcementEndpoint)
		{
			this.discoveryMessageSequenceGenerator = new DiscoveryMessageSequenceGenerator();
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000647 RID: 1607 RVA: 0x00011044 File Offset: 0x0000F244
		// (remove) Token: 0x06000648 RID: 1608 RVA: 0x0001107C File Offset: 0x0000F27C
		private event EventHandler<AsyncCompletedEventArgs> HelloOperationCompletedEventHandler;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000649 RID: 1609 RVA: 0x000110B4 File Offset: 0x0000F2B4
		// (remove) Token: 0x0600064A RID: 1610 RVA: 0x000110EC File Offset: 0x0000F2EC
		private event EventHandler<AsyncCompletedEventArgs> ByeOperationCompletedEventHandler;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600064B RID: 1611 RVA: 0x00011121 File Offset: 0x0000F321
		// (remove) Token: 0x0600064C RID: 1612 RVA: 0x0001112A File Offset: 0x0000F32A
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

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600064D RID: 1613 RVA: 0x00011133 File Offset: 0x0000F333
		// (remove) Token: 0x0600064E RID: 1614 RVA: 0x0001113C File Offset: 0x0000F33C
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

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00011145 File Offset: 0x0000F345
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x0001114D File Offset: 0x0000F34D
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

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00011156 File Offset: 0x0000F356
		public new ChannelFactory ChannelFactory
		{
			get
			{
				return base.ChannelFactory;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001115E File Offset: 0x0000F35E
		public new IClientChannel InnerChannel
		{
			get
			{
				return base.InnerChannel;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00011166 File Offset: 0x0000F366
		public new ServiceEndpoint Endpoint
		{
			get
			{
				return base.Endpoint;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0000EE96 File Offset: 0x0000D096
		public ICommunicationObject InnerCommunicationObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00011170 File Offset: 0x0000F370
		public void HelloOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			HelloMessage11 message = HelloMessage11.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			base.Channel.HelloOperation(message);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001119C File Offset: 0x0000F39C
		public void ByeOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			ByeMessage11 message = ByeMessage11.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			base.Channel.ByeOperation(message);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000111C8 File Offset: 0x0000F3C8
		public IAsyncResult BeginHelloOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			HelloMessage11 message = HelloMessage11.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			return base.Channel.BeginHelloOperation(message, callback, state);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000111F5 File Offset: 0x0000F3F5
		public void EndHelloOperation(IAsyncResult result)
		{
			base.Channel.EndHelloOperation(result);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00011204 File Offset: 0x0000F404
		public IAsyncResult BeginByeOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			ByeMessage11 message = ByeMessage11.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			return base.Channel.BeginByeOperation(message, callback, state);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00011231 File Offset: 0x0000F431
		public void EndByeOperation(IAsyncResult result)
		{
			base.Channel.EndByeOperation(result);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00011240 File Offset: 0x0000F440
		public void HelloOperationAsync(EndpointDiscoveryMetadata endpointDiscoveryMetadata, object userState)
		{
			HelloMessage11 helloMessage = HelloMessage11.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			if (this.onBeginHelloOperationDelegate == null)
			{
				this.onBeginHelloOperationDelegate = new ClientBase<IAnnouncementContract11>.BeginOperationDelegate(this.OnBeginHelloOperation);
			}
			if (this.onEndHelloOperationDelegate == null)
			{
				this.onEndHelloOperationDelegate = new ClientBase<IAnnouncementContract11>.EndOperationDelegate(this.OnEndHelloOperation);
			}
			if (this.onHelloOperationCompletedDelegate == null)
			{
				this.onHelloOperationCompletedDelegate = Fx.ThunkCallback(new SendOrPostCallback(this.OnHelloOperationCompleted));
			}
			base.InvokeAsync(this.onBeginHelloOperationDelegate, new object[]
			{
				helloMessage
			}, this.onEndHelloOperationDelegate, this.onHelloOperationCompletedDelegate, userState);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000112D8 File Offset: 0x0000F4D8
		public void ByeOperationAsync(EndpointDiscoveryMetadata endpointDiscoveryMetadata, object userState)
		{
			ByeMessage11 byeMessage = ByeMessage11.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			if (this.onBeginByeOperationDelegate == null)
			{
				this.onBeginByeOperationDelegate = new ClientBase<IAnnouncementContract11>.BeginOperationDelegate(this.OnBeginByeOperation);
			}
			if (this.onEndByeOperationDelegate == null)
			{
				this.onEndByeOperationDelegate = new ClientBase<IAnnouncementContract11>.EndOperationDelegate(this.OnEndByeOperation);
			}
			if (this.onByeOperationCompletedDelegate == null)
			{
				this.onByeOperationCompletedDelegate = Fx.ThunkCallback(new SendOrPostCallback(this.OnByeOperationCompleted));
			}
			base.InvokeAsync(this.onBeginByeOperationDelegate, new object[]
			{
				byeMessage
			}, this.onEndByeOperationDelegate, this.onByeOperationCompletedDelegate, userState);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001136D File Offset: 0x0000F56D
		private IAsyncResult BeginHelloOperation(HelloMessage11 message, AsyncCallback callback, object state)
		{
			return base.Channel.BeginHelloOperation(message, callback, state);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001137D File Offset: 0x0000F57D
		private IAsyncResult BeginByeOperation(ByeMessage11 message, AsyncCallback callback, object state)
		{
			return base.Channel.BeginByeOperation(message, callback, state);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00011390 File Offset: 0x0000F590
		private IAsyncResult OnBeginHelloOperation(object[] inValues, AsyncCallback callback, object asyncState)
		{
			HelloMessage11 message = (HelloMessage11)inValues[0];
			return this.BeginHelloOperation(message, callback, asyncState);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x000113AF File Offset: 0x0000F5AF
		private object[] OnEndHelloOperation(IAsyncResult result)
		{
			this.EndHelloOperation(result);
			return null;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000113BC File Offset: 0x0000F5BC
		private void OnHelloOperationCompleted(object state)
		{
			if (this.HelloOperationCompletedEventHandler != null)
			{
				ClientBase<IAnnouncementContract11>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IAnnouncementContract11>.InvokeAsyncCompletedEventArgs)state;
				this.HelloOperationCompletedEventHandler(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000113FC File Offset: 0x0000F5FC
		private IAsyncResult OnBeginByeOperation(object[] inValues, AsyncCallback callback, object asyncState)
		{
			ByeMessage11 message = (ByeMessage11)inValues[0];
			return this.BeginByeOperation(message, callback, asyncState);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001141B File Offset: 0x0000F61B
		private object[] OnEndByeOperation(IAsyncResult result)
		{
			this.EndByeOperation(result);
			return null;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00011428 File Offset: 0x0000F628
		private void OnByeOperationCompleted(object state)
		{
			if (this.ByeOperationCompletedEventHandler != null)
			{
				ClientBase<IAnnouncementContract11>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IAnnouncementContract11>.InvokeAsyncCompletedEventArgs)state;
				this.ByeOperationCompletedEventHandler(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00011467 File Offset: 0x0000F667
		ClientCredentials IAnnouncementInnerClient.get_ClientCredentials()
		{
			return base.ClientCredentials;
		}

		// Token: 0x0400017D RID: 381
		private DiscoveryMessageSequenceGenerator discoveryMessageSequenceGenerator;

		// Token: 0x0400017E RID: 382
		private ClientBase<IAnnouncementContract11>.BeginOperationDelegate onBeginHelloOperationDelegate;

		// Token: 0x0400017F RID: 383
		private ClientBase<IAnnouncementContract11>.EndOperationDelegate onEndHelloOperationDelegate;

		// Token: 0x04000180 RID: 384
		private SendOrPostCallback onHelloOperationCompletedDelegate;

		// Token: 0x04000181 RID: 385
		private ClientBase<IAnnouncementContract11>.BeginOperationDelegate onBeginByeOperationDelegate;

		// Token: 0x04000182 RID: 386
		private ClientBase<IAnnouncementContract11>.EndOperationDelegate onEndByeOperationDelegate;

		// Token: 0x04000183 RID: 387
		private SendOrPostCallback onByeOperationCompletedDelegate;
	}
}
