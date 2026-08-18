using System;
using System.Collections.ObjectModel;
using System.Runtime;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000007 RID: 7
	internal class AnnouncementSendsAsyncResult : RandomDelaySendsAsyncResult
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00002EC8 File Offset: 0x000010C8
		internal AnnouncementSendsAsyncResult(AnnouncementClient announcementClient, Collection<EndpointDiscoveryMetadata> publishedEndpoints, Collection<UniqueId> messageIds, bool online, TimeSpan maxDelay, Random random, AsyncCallback callback, object state) : base(publishedEndpoints.Count, maxDelay, announcementClient, random, callback, state)
		{
			this.announcementClient = announcementClient;
			this.publishedEndpoints = publishedEndpoints;
			this.messageIds = messageIds;
			this.online = online;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002EFC File Offset: 0x000010FC
		protected override IAsyncResult OnBeginSend(int index, TimeSpan timeout, AsyncCallback callback, object state)
		{
			IAsyncResult result;
			using (new OperationContextScope(this.announcementClient.InnerChannel))
			{
				OperationContext.Current.OutgoingMessageHeaders.MessageId = this.messageIds[index];
				if (this.online)
				{
					result = this.announcementClient.BeginAnnounceOnline(this.publishedEndpoints[index], callback, state);
				}
				else
				{
					result = this.announcementClient.BeginAnnounceOffline(this.publishedEndpoints[index], callback, state);
				}
			}
			return result;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002F94 File Offset: 0x00001194
		protected override void OnEndSend(IAsyncResult result)
		{
			if (this.online)
			{
				this.announcementClient.EndAnnounceOnline(result);
				return;
			}
			this.announcementClient.EndAnnounceOffline(result);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002FB7 File Offset: 0x000011B7
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<AnnouncementSendsAsyncResult>(result);
		}

		// Token: 0x04000019 RID: 25
		private AnnouncementClient announcementClient;

		// Token: 0x0400001A RID: 26
		private Collection<EndpointDiscoveryMetadata> publishedEndpoints;

		// Token: 0x0400001B RID: 27
		private Collection<UniqueId> messageIds;

		// Token: 0x0400001C RID: 28
		private bool online;
	}
}
