using System;
using System.Collections.ObjectModel;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000040 RID: 64
	internal class OnlineAnnouncementChannelDispatcher : ChannelDispatcherBase
	{
		// Token: 0x0600031C RID: 796 RVA: 0x00008CDF File Offset: 0x00006EDF
		internal OnlineAnnouncementChannelDispatcher(ServiceHostBase serviceHostBase, Collection<AnnouncementEndpoint> announcementEndpoints, Collection<EndpointDiscoveryMetadata> publishedEndpoints, DiscoveryMessageSequenceGenerator discoveryMessageSequenceGenerator)
		{
			this.serviceHostBase = serviceHostBase;
			this.announcementEndpoints = announcementEndpoints;
			this.publishedEndpoints = publishedEndpoints;
			this.discoveryMessageSequenceGenerator = discoveryMessageSequenceGenerator;
			this.thisLock = new object();
			this.InitChannelDispatchers(serviceHostBase);
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00008D16 File Offset: 0x00006F16
		public override ServiceHostBase Host
		{
			get
			{
				return this.serviceHostBase;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00006B84 File Offset: 0x00004D84
		public override IChannelListener Listener
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00008C6A File Offset: 0x00006E6A
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				return TimeSpan.FromMinutes(1.0);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00008C6A File Offset: 0x00006E6A
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				return TimeSpan.FromMinutes(1.0);
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00008D20 File Offset: 0x00006F20
		private void OnChannelDispatcherOpened(object sender, EventArgs e)
		{
			bool flag = false;
			object obj = this.thisLock;
			lock (obj)
			{
				int num = this.dispatchersToWait - 1;
				this.dispatchersToWait = num;
				if (num == 0 && this.announceOnlineAsyncResult != null)
				{
					flag = true;
					this.dispatchersToWait--;
				}
			}
			if (flag)
			{
				this.announceOnlineAsyncResult.Start(this.asyncOpenTimeoutHelper.RemainingTime(), false);
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00008DA4 File Offset: 0x00006FA4
		private void InitChannelDispatchers(ServiceHostBase serviceHostBase)
		{
			this.dispatchersToWait = serviceHostBase.ChannelDispatchers.Count;
			EventHandler value = new EventHandler(this.OnChannelDispatcherOpened);
			foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
			{
				channelDispatcherBase.Opened += value;
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00008E10 File Offset: 0x00007010
		protected override void OnAbort()
		{
			if (this.announceOnlineAsyncResult != null)
			{
				this.announceOnlineAsyncResult.Cancel();
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00008E28 File Offset: 0x00007028
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			bool flag = false;
			this.asyncOpenTimeoutHelper = new TimeoutHelper(timeout);
			this.asyncOpenTimeoutHelper.RemainingTime();
			this.announceOnlineAsyncResult = new AnnouncementDispatcherAsyncResult(this.announcementEndpoints, this.publishedEndpoints, this.discoveryMessageSequenceGenerator, true, callback, state);
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.dispatchersToWait == 0)
				{
					flag = true;
					this.dispatchersToWait--;
				}
			}
			if (base.State != CommunicationState.Opening)
			{
				this.announceOnlineAsyncResult.Cancel();
			}
			else if (flag)
			{
				this.announceOnlineAsyncResult.Start(this.asyncOpenTimeoutHelper.RemainingTime(), true);
			}
			return this.announceOnlineAsyncResult;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00008EEC File Offset: 0x000070EC
		protected override void OnEndOpen(IAsyncResult result)
		{
			AnnouncementDispatcherAsyncResult.End(result);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00008EF4 File Offset: 0x000070F4
		protected override void OnOpen(TimeSpan timeout)
		{
			this.announceOnlineAsyncResult = new AnnouncementDispatcherAsyncResult(this.announcementEndpoints, this.publishedEndpoints, this.discoveryMessageSequenceGenerator, true, null, null);
			if (base.State != CommunicationState.Opening)
			{
				this.announceOnlineAsyncResult.Cancel();
			}
			else
			{
				this.announceOnlineAsyncResult.Start(timeout, true);
			}
			AnnouncementDispatcherAsyncResult.End(this.announceOnlineAsyncResult);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00008F4F File Offset: 0x0000714F
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000031C9 File Offset: 0x000013C9
		protected override void OnEndClose(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000030E1 File Offset: 0x000012E1
		protected override void OnClose(TimeSpan timeout)
		{
		}

		// Token: 0x040000B1 RID: 177
		private object thisLock;

		// Token: 0x040000B2 RID: 178
		private Collection<AnnouncementEndpoint> announcementEndpoints;

		// Token: 0x040000B3 RID: 179
		private Collection<EndpointDiscoveryMetadata> publishedEndpoints;

		// Token: 0x040000B4 RID: 180
		private int dispatchersToWait;

		// Token: 0x040000B5 RID: 181
		private AnnouncementDispatcherAsyncResult announceOnlineAsyncResult;

		// Token: 0x040000B6 RID: 182
		private DiscoveryMessageSequenceGenerator discoveryMessageSequenceGenerator;

		// Token: 0x040000B7 RID: 183
		private ServiceHostBase serviceHostBase;

		// Token: 0x040000B8 RID: 184
		private TimeoutHelper asyncOpenTimeoutHelper;
	}
}
