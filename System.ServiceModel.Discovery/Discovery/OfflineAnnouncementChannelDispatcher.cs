using System;
using System.Collections.ObjectModel;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200003F RID: 63
	internal sealed class OfflineAnnouncementChannelDispatcher : ChannelDispatcherBase
	{
		// Token: 0x06000310 RID: 784 RVA: 0x00008C3C File Offset: 0x00006E3C
		internal OfflineAnnouncementChannelDispatcher(ServiceHostBase serviceHostBase, Collection<AnnouncementEndpoint> announcementEndpoints, Collection<EndpointDiscoveryMetadata> publishedEndpoints, DiscoveryMessageSequenceGenerator discoveryMessageSequenceGenerator)
		{
			this.serviceHostBase = serviceHostBase;
			this.closeListener = new OfflineAnnouncementChannelDispatcher.CloseListener(announcementEndpoints, publishedEndpoints, discoveryMessageSequenceGenerator);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00008C5A File Offset: 0x00006E5A
		public override ServiceHostBase Host
		{
			get
			{
				return this.serviceHostBase;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00008C62 File Offset: 0x00006E62
		public override IChannelListener Listener
		{
			get
			{
				return this.closeListener;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00008C6A File Offset: 0x00006E6A
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				return TimeSpan.FromMinutes(1.0);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00008C6A File Offset: 0x00006E6A
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				return TimeSpan.FromMinutes(1.0);
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00008C7A File Offset: 0x00006E7A
		protected override void OnAbort()
		{
			this.closeListener.Abort();
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00008C87 File Offset: 0x00006E87
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.closeListener.BeginClose(timeout, callback, state);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00008C97 File Offset: 0x00006E97
		protected override void OnEndClose(IAsyncResult result)
		{
			this.closeListener.EndClose(result);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00008CA5 File Offset: 0x00006EA5
		protected override void OnClose(TimeSpan timeout)
		{
			this.closeListener.Close(timeout);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00008CB3 File Offset: 0x00006EB3
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.closeListener.BeginOpen(timeout, callback, state);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00008CC3 File Offset: 0x00006EC3
		protected override void OnEndOpen(IAsyncResult result)
		{
			this.closeListener.EndOpen(result);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00008CD1 File Offset: 0x00006ED1
		protected override void OnOpen(TimeSpan timeout)
		{
			this.closeListener.Open(timeout);
		}

		// Token: 0x040000AF RID: 175
		private ServiceHostBase serviceHostBase;

		// Token: 0x040000B0 RID: 176
		private IChannelListener closeListener;

		// Token: 0x020000D9 RID: 217
		private class CloseListener : CommunicationObject, IChannelListener, ICommunicationObject
		{
			// Token: 0x06000812 RID: 2066 RVA: 0x00014FF6 File Offset: 0x000131F6
			public CloseListener(Collection<AnnouncementEndpoint> announcementEndpoints, Collection<EndpointDiscoveryMetadata> publishedEndpoints, DiscoveryMessageSequenceGenerator discoveryMessageSequenceGenerator)
			{
				this.announcementEndpoints = announcementEndpoints;
				this.publishedEndpoints = publishedEndpoints;
				this.discoveryMessageSequenceGenerator = discoveryMessageSequenceGenerator;
				this.abortAnnouncement = false;
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001501A File Offset: 0x0001321A
			public Uri Uri
			{
				get
				{
					return new Uri("urn:schemas-microsoft-org:ws:2008:07:discovery");
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06000814 RID: 2068 RVA: 0x00008C6A File Offset: 0x00006E6A
			protected override TimeSpan DefaultCloseTimeout
			{
				get
				{
					return TimeSpan.FromMinutes(1.0);
				}
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06000815 RID: 2069 RVA: 0x00008C6A File Offset: 0x00006E6A
			protected override TimeSpan DefaultOpenTimeout
			{
				get
				{
					return TimeSpan.FromMinutes(1.0);
				}
			}

			// Token: 0x06000816 RID: 2070 RVA: 0x00015026 File Offset: 0x00013226
			protected override void OnAbort()
			{
				this.abortAnnouncement = true;
				if (this.announceOfflineAsyncResult != null)
				{
					this.announceOfflineAsyncResult.Cancel();
				}
			}

			// Token: 0x06000817 RID: 2071 RVA: 0x00015042 File Offset: 0x00013242
			protected override void OnClose(TimeSpan timeout)
			{
				this.OnEndClose(this.OnBeginClose(timeout, null, null));
			}

			// Token: 0x06000818 RID: 2072 RVA: 0x00015054 File Offset: 0x00013254
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				this.announceOfflineAsyncResult = new AnnouncementDispatcherAsyncResult(this.announcementEndpoints, this.publishedEndpoints, this.discoveryMessageSequenceGenerator, false, callback, state);
				if (this.abortAnnouncement)
				{
					this.announceOfflineAsyncResult.Cancel();
				}
				else
				{
					this.announceOfflineAsyncResult.Start(timeout, true);
				}
				return this.announceOfflineAsyncResult;
			}

			// Token: 0x06000819 RID: 2073 RVA: 0x00008EEC File Offset: 0x000070EC
			protected override void OnEndClose(IAsyncResult result)
			{
				AnnouncementDispatcherAsyncResult.End(result);
			}

			// Token: 0x0600081A RID: 2074 RVA: 0x00008F4F File Offset: 0x0000714F
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x0600081B RID: 2075 RVA: 0x000031C9 File Offset: 0x000013C9
			protected override void OnEndOpen(IAsyncResult result)
			{
				CompletedAsyncResult.End(result);
			}

			// Token: 0x0600081C RID: 2076 RVA: 0x000030E1 File Offset: 0x000012E1
			protected override void OnOpen(TimeSpan timeout)
			{
			}

			// Token: 0x0600081D RID: 2077 RVA: 0x000150A9 File Offset: 0x000132A9
			public IAsyncResult BeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new CompletedAsyncResult<bool>(true, callback, state);
			}

			// Token: 0x0600081E RID: 2078 RVA: 0x000150B3 File Offset: 0x000132B3
			public bool EndWaitForChannel(IAsyncResult result)
			{
				return CompletedAsyncResult<bool>.End(result);
			}

			// Token: 0x0600081F RID: 2079 RVA: 0x000150BC File Offset: 0x000132BC
			public virtual T GetProperty<T>() where T : class
			{
				if (typeof(T) == typeof(IChannelListener))
				{
					return (T)((object)this);
				}
				return default(T);
			}

			// Token: 0x06000820 RID: 2080 RVA: 0x0000C68B File Offset: 0x0000A88B
			public bool WaitForChannel(TimeSpan timeout)
			{
				return true;
			}

			// Token: 0x04000213 RID: 531
			private Collection<AnnouncementEndpoint> announcementEndpoints;

			// Token: 0x04000214 RID: 532
			private Collection<EndpointDiscoveryMetadata> publishedEndpoints;

			// Token: 0x04000215 RID: 533
			private AnnouncementDispatcherAsyncResult announceOfflineAsyncResult;

			// Token: 0x04000216 RID: 534
			private DiscoveryMessageSequenceGenerator discoveryMessageSequenceGenerator;

			// Token: 0x04000217 RID: 535
			private bool abortAnnouncement;
		}
	}
}
