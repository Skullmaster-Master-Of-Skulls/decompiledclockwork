using System;
using System.ComponentModel;
using System.Runtime;
using System.ServiceModel.Description;
using System.Threading;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x0200005D RID: 93
	internal class AnnouncementInnerClientCD1 : ClientBase<IAnnouncementContractCD1>, IAnnouncementInnerClient
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x0000ED55 File Offset: 0x0000CF55
		public AnnouncementInnerClientCD1(AnnouncementEndpoint announcementEndpoint) : base(announcementEndpoint)
		{
			this.discoveryMessageSequenceGenerator = new DiscoveryMessageSequenceGenerator();
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060004C7 RID: 1223 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		// (remove) Token: 0x060004C8 RID: 1224 RVA: 0x0000EDA4 File Offset: 0x0000CFA4
		private event EventHandler<AsyncCompletedEventArgs> HelloOperationCompletedEventHandler;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060004C9 RID: 1225 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		// (remove) Token: 0x060004CA RID: 1226 RVA: 0x0000EE14 File Offset: 0x0000D014
		private event EventHandler<AsyncCompletedEventArgs> ByeOperationCompletedEventHandler;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060004CB RID: 1227 RVA: 0x0000EE49 File Offset: 0x0000D049
		// (remove) Token: 0x060004CC RID: 1228 RVA: 0x0000EE52 File Offset: 0x0000D052
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

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060004CD RID: 1229 RVA: 0x0000EE5B File Offset: 0x0000D05B
		// (remove) Token: 0x060004CE RID: 1230 RVA: 0x0000EE64 File Offset: 0x0000D064
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

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000EE6D File Offset: 0x0000D06D
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000EE75 File Offset: 0x0000D075
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000EE7E File Offset: 0x0000D07E
		public new ChannelFactory ChannelFactory
		{
			get
			{
				return base.ChannelFactory;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000EE86 File Offset: 0x0000D086
		public new IClientChannel InnerChannel
		{
			get
			{
				return base.InnerChannel;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000EE8E File Offset: 0x0000D08E
		public new ServiceEndpoint Endpoint
		{
			get
			{
				return base.Endpoint;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000EE96 File Offset: 0x0000D096
		public ICommunicationObject InnerCommunicationObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000EE9C File Offset: 0x0000D09C
		public void HelloOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			HelloMessageCD1 message = HelloMessageCD1.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			base.Channel.HelloOperation(message);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000EEC8 File Offset: 0x0000D0C8
		public void ByeOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			ByeMessageCD1 message = ByeMessageCD1.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			base.Channel.ByeOperation(message);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000EEF4 File Offset: 0x0000D0F4
		public IAsyncResult BeginHelloOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			HelloMessageCD1 message = HelloMessageCD1.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			return base.Channel.BeginHelloOperation(message, callback, state);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000EF21 File Offset: 0x0000D121
		public void EndHelloOperation(IAsyncResult result)
		{
			base.Channel.EndHelloOperation(result);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000EF30 File Offset: 0x0000D130
		public IAsyncResult BeginByeOperation(EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			ByeMessageCD1 message = ByeMessageCD1.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			return base.Channel.BeginByeOperation(message, callback, state);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000EF5D File Offset: 0x0000D15D
		public void EndByeOperation(IAsyncResult result)
		{
			base.Channel.EndByeOperation(result);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000EF6C File Offset: 0x0000D16C
		public void HelloOperationAsync(EndpointDiscoveryMetadata endpointDiscoveryMetadata, object userState)
		{
			HelloMessageCD1 helloMessageCD = HelloMessageCD1.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			if (this.onBeginHelloOperationDelegate == null)
			{
				this.onBeginHelloOperationDelegate = new ClientBase<IAnnouncementContractCD1>.BeginOperationDelegate(this.OnBeginHelloOperation);
			}
			if (this.onEndHelloOperationDelegate == null)
			{
				this.onEndHelloOperationDelegate = new ClientBase<IAnnouncementContractCD1>.EndOperationDelegate(this.OnEndHelloOperation);
			}
			if (this.onHelloOperationCompletedDelegate == null)
			{
				this.onHelloOperationCompletedDelegate = Fx.ThunkCallback(new SendOrPostCallback(this.OnHelloOperationCompleted));
			}
			base.InvokeAsync(this.onBeginHelloOperationDelegate, new object[]
			{
				helloMessageCD
			}, this.onEndHelloOperationDelegate, this.onHelloOperationCompletedDelegate, userState);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000F004 File Offset: 0x0000D204
		public void ByeOperationAsync(EndpointDiscoveryMetadata endpointDiscoveryMetadata, object userState)
		{
			ByeMessageCD1 byeMessageCD = ByeMessageCD1.Create(this.DiscoveryMessageSequenceGenerator.Next(), endpointDiscoveryMetadata);
			if (this.onBeginByeOperationDelegate == null)
			{
				this.onBeginByeOperationDelegate = new ClientBase<IAnnouncementContractCD1>.BeginOperationDelegate(this.OnBeginByeOperation);
			}
			if (this.onEndByeOperationDelegate == null)
			{
				this.onEndByeOperationDelegate = new ClientBase<IAnnouncementContractCD1>.EndOperationDelegate(this.OnEndByeOperation);
			}
			if (this.onByeOperationCompletedDelegate == null)
			{
				this.onByeOperationCompletedDelegate = Fx.ThunkCallback(new SendOrPostCallback(this.OnByeOperationCompleted));
			}
			base.InvokeAsync(this.onBeginByeOperationDelegate, new object[]
			{
				byeMessageCD
			}, this.onEndByeOperationDelegate, this.onByeOperationCompletedDelegate, userState);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000F099 File Offset: 0x0000D299
		private IAsyncResult BeginHelloOperation(HelloMessageCD1 message, AsyncCallback callback, object state)
		{
			return base.Channel.BeginHelloOperation(message, callback, state);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000F0A9 File Offset: 0x0000D2A9
		private IAsyncResult BeginByeOperation(ByeMessageCD1 message, AsyncCallback callback, object state)
		{
			return base.Channel.BeginByeOperation(message, callback, state);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000F0BC File Offset: 0x0000D2BC
		private IAsyncResult OnBeginHelloOperation(object[] inValues, AsyncCallback callback, object asyncState)
		{
			HelloMessageCD1 message = (HelloMessageCD1)inValues[0];
			return this.BeginHelloOperation(message, callback, asyncState);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000F0DB File Offset: 0x0000D2DB
		private object[] OnEndHelloOperation(IAsyncResult result)
		{
			this.EndHelloOperation(result);
			return null;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000F0E8 File Offset: 0x0000D2E8
		private void OnHelloOperationCompleted(object state)
		{
			if (this.HelloOperationCompletedEventHandler != null)
			{
				ClientBase<IAnnouncementContractCD1>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IAnnouncementContractCD1>.InvokeAsyncCompletedEventArgs)state;
				this.HelloOperationCompletedEventHandler(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000F128 File Offset: 0x0000D328
		private IAsyncResult OnBeginByeOperation(object[] inValues, AsyncCallback callback, object asyncState)
		{
			ByeMessageCD1 message = (ByeMessageCD1)inValues[0];
			return this.BeginByeOperation(message, callback, asyncState);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000F147 File Offset: 0x0000D347
		private object[] OnEndByeOperation(IAsyncResult result)
		{
			this.EndByeOperation(result);
			return null;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000F154 File Offset: 0x0000D354
		private void OnByeOperationCompleted(object state)
		{
			if (this.ByeOperationCompletedEventHandler != null)
			{
				ClientBase<IAnnouncementContractCD1>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<IAnnouncementContractCD1>.InvokeAsyncCompletedEventArgs)state;
				this.ByeOperationCompletedEventHandler(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000F193 File Offset: 0x0000D393
		ClientCredentials IAnnouncementInnerClient.get_ClientCredentials()
		{
			return base.ClientCredentials;
		}

		// Token: 0x04000130 RID: 304
		private DiscoveryMessageSequenceGenerator discoveryMessageSequenceGenerator;

		// Token: 0x04000131 RID: 305
		private ClientBase<IAnnouncementContractCD1>.BeginOperationDelegate onBeginHelloOperationDelegate;

		// Token: 0x04000132 RID: 306
		private ClientBase<IAnnouncementContractCD1>.EndOperationDelegate onEndHelloOperationDelegate;

		// Token: 0x04000133 RID: 307
		private SendOrPostCallback onHelloOperationCompletedDelegate;

		// Token: 0x04000134 RID: 308
		private ClientBase<IAnnouncementContractCD1>.BeginOperationDelegate onBeginByeOperationDelegate;

		// Token: 0x04000135 RID: 309
		private ClientBase<IAnnouncementContractCD1>.EndOperationDelegate onEndByeOperationDelegate;

		// Token: 0x04000136 RID: 310
		private SendOrPostCallback onByeOperationCompletedDelegate;
	}
}
