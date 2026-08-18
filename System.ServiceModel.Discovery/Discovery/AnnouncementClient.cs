using System;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery.Configuration;
using System.Threading.Tasks;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000003 RID: 3
	public sealed class AnnouncementClient : ICommunicationObject, IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AnnouncementClient() : this("*")
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		public AnnouncementClient(string endpointConfigurationName)
		{
			if (endpointConfigurationName == null)
			{
				throw FxTrace.Exception.ArgumentNull("endpointConfigurationName");
			}
			AnnouncementEndpoint announcementEndpoint = ConfigurationUtility.LookupEndpointFromClientSection<AnnouncementEndpoint>(endpointConfigurationName);
			this.Initialize(announcementEndpoint);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002094 File Offset: 0x00000294
		public AnnouncementClient(AnnouncementEndpoint announcementEndpoint)
		{
			if (announcementEndpoint == null)
			{
				throw FxTrace.Exception.ArgumentNull("announcementEndpoint");
			}
			this.Initialize(announcementEndpoint);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000004 RID: 4 RVA: 0x000020B6 File Offset: 0x000002B6
		// (remove) Token: 0x06000005 RID: 5 RVA: 0x000020DE File Offset: 0x000002DE
		public event EventHandler<AsyncCompletedEventArgs> AnnounceOnlineCompleted
		{
			add
			{
				if (this.InternalAnnounceOnlineCompleted == null)
				{
					this.innerClient.HelloOperationCompleted += this.OnInnerClientHelloCompleted;
				}
				this.InternalAnnounceOnlineCompleted += value;
			}
			remove
			{
				this.InternalAnnounceOnlineCompleted -= value;
				if (this.InternalAnnounceOnlineCompleted == null)
				{
					this.innerClient.HelloOperationCompleted -= this.OnInnerClientHelloCompleted;
				}
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000006 RID: 6 RVA: 0x00002106 File Offset: 0x00000306
		// (remove) Token: 0x06000007 RID: 7 RVA: 0x0000212E File Offset: 0x0000032E
		public event EventHandler<AsyncCompletedEventArgs> AnnounceOfflineCompleted
		{
			add
			{
				if (this.InternalAnnounceOfflineCompleted == null)
				{
					this.innerClient.ByeOperationCompleted += this.OnInnerClientByeCompleted;
				}
				this.InternalAnnounceOfflineCompleted += value;
			}
			remove
			{
				this.InternalAnnounceOfflineCompleted -= value;
				if (this.InternalAnnounceOfflineCompleted == null)
				{
					this.innerClient.ByeOperationCompleted -= this.OnInnerClientByeCompleted;
				}
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000008 RID: 8 RVA: 0x00002158 File Offset: 0x00000358
		// (remove) Token: 0x06000009 RID: 9 RVA: 0x00002190 File Offset: 0x00000390
		private event EventHandler<AsyncCompletedEventArgs> InternalAnnounceOnlineCompleted;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600000A RID: 10 RVA: 0x000021C8 File Offset: 0x000003C8
		// (remove) Token: 0x0600000B RID: 11 RVA: 0x00002200 File Offset: 0x00000400
		private event EventHandler<AsyncCompletedEventArgs> InternalAnnounceOfflineCompleted;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600000C RID: 12 RVA: 0x00002235 File Offset: 0x00000435
		// (remove) Token: 0x0600000D RID: 13 RVA: 0x0000225D File Offset: 0x0000045D
		event EventHandler ICommunicationObject.Closed
		{
			add
			{
				if (this.InternalClosed == null)
				{
					this.InnerCommunicationObject.Closed += this.OnInnerCommunicationObjectClosed;
				}
				this.InternalClosed += value;
			}
			remove
			{
				this.InternalClosed -= value;
				if (this.InternalClosed == null)
				{
					this.InnerCommunicationObject.Closed -= this.OnInnerCommunicationObjectClosed;
				}
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600000E RID: 14 RVA: 0x00002285 File Offset: 0x00000485
		// (remove) Token: 0x0600000F RID: 15 RVA: 0x000022AD File Offset: 0x000004AD
		event EventHandler ICommunicationObject.Closing
		{
			add
			{
				if (this.InternalClosing == null)
				{
					this.InnerCommunicationObject.Closing += this.OnInnerCommunicationObjectClosing;
				}
				this.InternalClosing += value;
			}
			remove
			{
				this.InternalClosing -= value;
				if (this.InternalClosing == null)
				{
					this.InnerCommunicationObject.Closing -= this.OnInnerCommunicationObjectClosing;
				}
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000010 RID: 16 RVA: 0x000022D5 File Offset: 0x000004D5
		// (remove) Token: 0x06000011 RID: 17 RVA: 0x000022FD File Offset: 0x000004FD
		event EventHandler ICommunicationObject.Faulted
		{
			add
			{
				if (this.InternalFaulted == null)
				{
					this.InnerCommunicationObject.Faulted += this.OnInnerCommunicationObjectFaulted;
				}
				this.InternalFaulted += value;
			}
			remove
			{
				this.InternalFaulted -= value;
				if (this.InternalFaulted == null)
				{
					this.InnerCommunicationObject.Faulted -= this.OnInnerCommunicationObjectFaulted;
				}
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000012 RID: 18 RVA: 0x00002325 File Offset: 0x00000525
		// (remove) Token: 0x06000013 RID: 19 RVA: 0x0000234D File Offset: 0x0000054D
		event EventHandler ICommunicationObject.Opened
		{
			add
			{
				if (this.InternalOpened == null)
				{
					this.InnerCommunicationObject.Opened += this.OnInnerCommunicationObjectOpened;
				}
				this.InternalOpened += value;
			}
			remove
			{
				this.InternalOpened -= value;
				if (this.InternalOpened == null)
				{
					this.InnerCommunicationObject.Opened -= this.OnInnerCommunicationObjectOpened;
				}
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000014 RID: 20 RVA: 0x00002375 File Offset: 0x00000575
		// (remove) Token: 0x06000015 RID: 21 RVA: 0x0000239D File Offset: 0x0000059D
		event EventHandler ICommunicationObject.Opening
		{
			add
			{
				if (this.InternalOpening == null)
				{
					this.InnerCommunicationObject.Opening += this.OnInnerCommunicationObjectOpening;
				}
				this.InternalOpening += value;
			}
			remove
			{
				this.InternalOpening -= value;
				if (this.InternalOpening == null)
				{
					this.InnerCommunicationObject.Opening -= this.OnInnerCommunicationObjectOpening;
				}
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000016 RID: 22 RVA: 0x000023C8 File Offset: 0x000005C8
		// (remove) Token: 0x06000017 RID: 23 RVA: 0x00002400 File Offset: 0x00000600
		private event EventHandler InternalClosed;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000018 RID: 24 RVA: 0x00002438 File Offset: 0x00000638
		// (remove) Token: 0x06000019 RID: 25 RVA: 0x00002470 File Offset: 0x00000670
		private event EventHandler InternalClosing;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600001A RID: 26 RVA: 0x000024A8 File Offset: 0x000006A8
		// (remove) Token: 0x0600001B RID: 27 RVA: 0x000024E0 File Offset: 0x000006E0
		private event EventHandler InternalFaulted;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600001C RID: 28 RVA: 0x00002518 File Offset: 0x00000718
		// (remove) Token: 0x0600001D RID: 29 RVA: 0x00002550 File Offset: 0x00000750
		private event EventHandler InternalOpened;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600001E RID: 30 RVA: 0x00002588 File Offset: 0x00000788
		// (remove) Token: 0x0600001F RID: 31 RVA: 0x000025C0 File Offset: 0x000007C0
		private event EventHandler InternalOpening;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000025F5 File Offset: 0x000007F5
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002602 File Offset: 0x00000802
		public DiscoveryMessageSequenceGenerator MessageSequenceGenerator
		{
			get
			{
				return this.innerClient.DiscoveryMessageSequenceGenerator;
			}
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				if (((ICommunicationObject)this).State != CommunicationState.Created)
				{
					throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoverySetMessageSequenceInvalidState));
				}
				this.innerClient.DiscoveryMessageSequenceGenerator = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002640 File Offset: 0x00000840
		public ChannelFactory ChannelFactory
		{
			get
			{
				return this.InnerClient.ChannelFactory;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000264D File Offset: 0x0000084D
		public ClientCredentials ClientCredentials
		{
			get
			{
				return this.InnerClient.ClientCredentials;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000265A File Offset: 0x0000085A
		public ServiceEndpoint Endpoint
		{
			get
			{
				return this.InnerClient.Endpoint;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002667 File Offset: 0x00000867
		public IClientChannel InnerChannel
		{
			get
			{
				return this.InnerClient.InnerChannel;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002674 File Offset: 0x00000874
		CommunicationState ICommunicationObject.State
		{
			get
			{
				return this.InnerCommunicationObject.State;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002681 File Offset: 0x00000881
		private IAnnouncementInnerClient InnerClient
		{
			get
			{
				return this.innerClient;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002689 File Offset: 0x00000889
		private ICommunicationObject InnerCommunicationObject
		{
			get
			{
				return this.InnerClient.InnerCommunicationObject;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002696 File Offset: 0x00000896
		public void Open()
		{
			((ICommunicationObject)this).Open();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000269E File Offset: 0x0000089E
		public void AnnounceOnlineAsync(EndpointDiscoveryMetadata discoveryMetadata)
		{
			this.AnnounceOnlineAsync(discoveryMetadata, null);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026A8 File Offset: 0x000008A8
		public void AnnounceOnlineAsync(EndpointDiscoveryMetadata discoveryMetadata, object userState)
		{
			if (discoveryMetadata == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryMetadata");
			}
			using (new AnnouncementClient.AnnouncementOperationContextScope(this.InnerChannel))
			{
				this.InnerClient.HelloOperationAsync(discoveryMetadata, userState);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002700 File Offset: 0x00000900
		public void AnnounceOfflineAsync(EndpointDiscoveryMetadata discoveryMetadata)
		{
			this.AnnounceOfflineAsync(discoveryMetadata, null);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000270C File Offset: 0x0000090C
		public void AnnounceOfflineAsync(EndpointDiscoveryMetadata discoveryMetadata, object userState)
		{
			if (discoveryMetadata == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryMetadata");
			}
			using (new AnnouncementClient.AnnouncementOperationContextScope(this.InnerChannel))
			{
				this.InnerClient.ByeOperationAsync(discoveryMetadata, userState);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002764 File Offset: 0x00000964
		public void AnnounceOnline(EndpointDiscoveryMetadata discoveryMetadata)
		{
			if (discoveryMetadata == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryMetadata");
			}
			using (new AnnouncementClient.AnnouncementOperationContextScope(this.InnerChannel))
			{
				this.InnerClient.HelloOperation(discoveryMetadata);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027B8 File Offset: 0x000009B8
		public void AnnounceOffline(EndpointDiscoveryMetadata discoveryMetadata)
		{
			if (discoveryMetadata == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryMetadata");
			}
			using (new AnnouncementClient.AnnouncementOperationContextScope(this.InnerChannel))
			{
				this.InnerClient.ByeOperation(discoveryMetadata);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000280C File Offset: 0x00000A0C
		public IAsyncResult BeginAnnounceOnline(EndpointDiscoveryMetadata discoveryMetadata, AsyncCallback callback, object state)
		{
			if (discoveryMetadata == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryMetadata");
			}
			IAsyncResult result;
			using (new AnnouncementClient.AnnouncementOperationContextScope(this.InnerChannel))
			{
				result = this.InnerClient.BeginHelloOperation(discoveryMetadata, callback, state);
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002864 File Offset: 0x00000A64
		public void EndAnnounceOnline(IAsyncResult result)
		{
			this.InnerClient.EndHelloOperation(result);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002872 File Offset: 0x00000A72
		public Task AnnounceOnlineTaskAsync(EndpointDiscoveryMetadata discoveryMetadata)
		{
			return Task.Factory.FromAsync<EndpointDiscoveryMetadata>(new Func<EndpointDiscoveryMetadata, AsyncCallback, object, IAsyncResult>(this.BeginAnnounceOnline), new Action<IAsyncResult>(this.EndAnnounceOnline), discoveryMetadata, null);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002898 File Offset: 0x00000A98
		public Task AnnounceOfflineTaskAsync(EndpointDiscoveryMetadata discoveryMetadata)
		{
			return Task.Factory.FromAsync<EndpointDiscoveryMetadata>(new Func<EndpointDiscoveryMetadata, AsyncCallback, object, IAsyncResult>(this.BeginAnnounceOffline), new Action<IAsyncResult>(this.EndAnnounceOffline), discoveryMetadata, null);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028C0 File Offset: 0x00000AC0
		public IAsyncResult BeginAnnounceOffline(EndpointDiscoveryMetadata discoveryMetadata, AsyncCallback callback, object state)
		{
			if (discoveryMetadata == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryMetadata");
			}
			IAsyncResult result;
			using (new AnnouncementClient.AnnouncementOperationContextScope(this.InnerChannel))
			{
				result = this.InnerClient.BeginByeOperation(discoveryMetadata, callback, state);
			}
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002918 File Offset: 0x00000B18
		public void EndAnnounceOffline(IAsyncResult result)
		{
			this.InnerClient.EndByeOperation(result);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002926 File Offset: 0x00000B26
		public void Close()
		{
			((ICommunicationObject)this).Close();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000292E File Offset: 0x00000B2E
		void ICommunicationObject.Open()
		{
			this.InnerCommunicationObject.Open();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000293B File Offset: 0x00000B3B
		void ICommunicationObject.Open(TimeSpan timeout)
		{
			this.InnerCommunicationObject.Open(timeout);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002949 File Offset: 0x00000B49
		IAsyncResult ICommunicationObject.BeginOpen(AsyncCallback callback, object state)
		{
			return this.InnerCommunicationObject.BeginOpen(callback, state);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002958 File Offset: 0x00000B58
		IAsyncResult ICommunicationObject.BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.InnerCommunicationObject.BeginOpen(timeout, callback, state);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002968 File Offset: 0x00000B68
		void ICommunicationObject.EndOpen(IAsyncResult result)
		{
			this.InnerCommunicationObject.EndOpen(result);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002976 File Offset: 0x00000B76
		void ICommunicationObject.Close()
		{
			this.InnerCommunicationObject.Close();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002983 File Offset: 0x00000B83
		void ICommunicationObject.Close(TimeSpan timeout)
		{
			this.InnerCommunicationObject.Close(timeout);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002991 File Offset: 0x00000B91
		IAsyncResult ICommunicationObject.BeginClose(AsyncCallback callback, object state)
		{
			return this.InnerCommunicationObject.BeginClose(callback, state);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029A0 File Offset: 0x00000BA0
		IAsyncResult ICommunicationObject.BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.InnerCommunicationObject.BeginClose(timeout, callback, state);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000029B0 File Offset: 0x00000BB0
		void ICommunicationObject.EndClose(IAsyncResult result)
		{
			this.InnerCommunicationObject.EndClose(result);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029BE File Offset: 0x00000BBE
		void ICommunicationObject.Abort()
		{
			this.InnerCommunicationObject.Abort();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029CB File Offset: 0x00000BCB
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029D4 File Offset: 0x00000BD4
		private void Initialize(AnnouncementEndpoint announcementEndpoint)
		{
			if (announcementEndpoint.Binding != null && announcementEndpoint.Binding.MessageVersion.Addressing == AddressingVersion.None)
			{
				throw FxTrace.Exception.Argument("announcementEndpoint", SR.EndpointWithInvalidMessageVersion(announcementEndpoint.GetType().Name, AddressingVersion.None, base.GetType().Name, AddressingVersion.WSAddressing10, AddressingVersion.WSAddressingAugust2004));
			}
			this.innerClient = announcementEndpoint.DiscoveryVersion.Implementation.CreateAnnouncementInnerClient(announcementEndpoint);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A51 File Offset: 0x00000C51
		private void RaiseEvent(EventHandler handler, EventArgs e)
		{
			if (handler != null)
			{
				handler(this, e);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A5E File Offset: 0x00000C5E
		private void OnInnerCommunicationObjectClosed(object sender, EventArgs e)
		{
			this.RaiseEvent(this.InternalClosed, e);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A6D File Offset: 0x00000C6D
		private void OnInnerCommunicationObjectClosing(object sender, EventArgs e)
		{
			this.RaiseEvent(this.InternalClosing, e);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A7C File Offset: 0x00000C7C
		private void OnInnerCommunicationObjectFaulted(object sender, EventArgs e)
		{
			this.RaiseEvent(this.InternalFaulted, e);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A8B File Offset: 0x00000C8B
		private void OnInnerCommunicationObjectOpened(object sender, EventArgs e)
		{
			this.RaiseEvent(this.InternalOpened, e);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A9A File Offset: 0x00000C9A
		private void OnInnerCommunicationObjectOpening(object sender, EventArgs e)
		{
			this.RaiseEvent(this.InternalOpening, e);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002AAC File Offset: 0x00000CAC
		private void OnInnerClientHelloCompleted(object sender, AsyncCompletedEventArgs e)
		{
			EventHandler<AsyncCompletedEventArgs> internalAnnounceOnlineCompleted = this.InternalAnnounceOnlineCompleted;
			if (internalAnnounceOnlineCompleted != null)
			{
				internalAnnounceOnlineCompleted(this, e);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002ACC File Offset: 0x00000CCC
		private void OnInnerClientByeCompleted(object sender, AsyncCompletedEventArgs e)
		{
			EventHandler<AsyncCompletedEventArgs> internalAnnounceOfflineCompleted = this.InternalAnnounceOfflineCompleted;
			if (internalAnnounceOfflineCompleted != null)
			{
				internalAnnounceOfflineCompleted(this, e);
			}
		}

		// Token: 0x04000007 RID: 7
		private IAnnouncementInnerClient innerClient;

		// Token: 0x020000C4 RID: 196
		private sealed class AnnouncementOperationContextScope : IDisposable
		{
			// Token: 0x060007C1 RID: 1985 RVA: 0x0001429C File Offset: 0x0001249C
			public AnnouncementOperationContextScope(IClientChannel clientChannel)
			{
				if (DiscoveryUtility.IsCompatible(OperationContext.Current, clientChannel))
				{
					this.originalMessageId = OperationContext.Current.OutgoingMessageHeaders.MessageId;
				}
				else
				{
					this.operationContextScope = new OperationContextScope(clientChannel);
				}
				if (this.originalMessageId == null)
				{
					OperationContext.Current.OutgoingMessageHeaders.MessageId = new UniqueId();
				}
			}

			// Token: 0x060007C2 RID: 1986 RVA: 0x00014301 File Offset: 0x00012501
			public void Dispose()
			{
				if (this.operationContextScope != null)
				{
					this.operationContextScope.Dispose();
					return;
				}
				OperationContext.Current.OutgoingMessageHeaders.MessageId = this.originalMessageId;
			}

			// Token: 0x040001DE RID: 478
			private OperationContextScope operationContextScope;

			// Token: 0x040001DF RID: 479
			private UniqueId originalMessageId;
		}
	}
}
