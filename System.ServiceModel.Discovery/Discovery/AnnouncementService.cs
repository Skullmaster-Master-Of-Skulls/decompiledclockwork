using System;
using System.Runtime;
using System.ServiceModel.Discovery.Version11;
using System.ServiceModel.Discovery.VersionApril2005;
using System.ServiceModel.Discovery.VersionCD1;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000008 RID: 8
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	public class AnnouncementService : IAnnouncementContractApril2005, IAnnouncementContract11, IAnnouncementContractCD1, IAnnouncementServiceImplementation
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002FC0 File Offset: 0x000011C0
		public AnnouncementService() : this(2056)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002FCD File Offset: 0x000011CD
		public AnnouncementService(int duplicateMessageHistoryLength)
		{
			if (duplicateMessageHistoryLength < 0)
			{
				throw FxTrace.Exception.ArgumentOutOfRange("duplicateMessageHistoryLength", duplicateMessageHistoryLength, SR.DiscoveryNegativeDuplicateMessageHistoryLength);
			}
			if (duplicateMessageHistoryLength > 0)
			{
				this.duplicateDetector = new DuplicateDetector<UniqueId>(duplicateMessageHistoryLength);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000067 RID: 103 RVA: 0x00003004 File Offset: 0x00001204
		// (remove) Token: 0x06000068 RID: 104 RVA: 0x0000303C File Offset: 0x0000123C
		public event EventHandler<AnnouncementEventArgs> OnlineAnnouncementReceived;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000069 RID: 105 RVA: 0x00003074 File Offset: 0x00001274
		// (remove) Token: 0x0600006A RID: 106 RVA: 0x000030AC File Offset: 0x000012AC
		public event EventHandler<AnnouncementEventArgs> OfflineAnnouncementReceived;

		// Token: 0x0600006B RID: 107 RVA: 0x000030E1 File Offset: 0x000012E1
		void IAnnouncementContractApril2005.HelloOperation(HelloMessageApril2005 message)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000030E3 File Offset: 0x000012E3
		IAsyncResult IAnnouncementContractApril2005.BeginHelloOperation(HelloMessageApril2005 message, AsyncCallback callback, object state)
		{
			return new HelloOperationApril2005AsyncResult(this, message, callback, state);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000030EE File Offset: 0x000012EE
		void IAnnouncementContractApril2005.EndHelloOperation(IAsyncResult result)
		{
			HelloOperationApril2005AsyncResult.End(result);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000030E1 File Offset: 0x000012E1
		void IAnnouncementContractApril2005.ByeOperation(ByeMessageApril2005 message)
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000030F6 File Offset: 0x000012F6
		IAsyncResult IAnnouncementContractApril2005.BeginByeOperation(ByeMessageApril2005 message, AsyncCallback callback, object state)
		{
			return new ByeOperationApril2005AsyncResult(this, message, callback, state);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003101 File Offset: 0x00001301
		void IAnnouncementContractApril2005.EndByeOperation(IAsyncResult result)
		{
			ByeOperationApril2005AsyncResult.End(result);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000030E1 File Offset: 0x000012E1
		void IAnnouncementContract11.HelloOperation(HelloMessage11 message)
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003109 File Offset: 0x00001309
		IAsyncResult IAnnouncementContract11.BeginHelloOperation(HelloMessage11 message, AsyncCallback callback, object state)
		{
			return new HelloOperation11AsyncResult(this, message, callback, state);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003114 File Offset: 0x00001314
		void IAnnouncementContract11.EndHelloOperation(IAsyncResult result)
		{
			HelloOperation11AsyncResult.End(result);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000030E1 File Offset: 0x000012E1
		void IAnnouncementContract11.ByeOperation(ByeMessage11 message)
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000311C File Offset: 0x0000131C
		IAsyncResult IAnnouncementContract11.BeginByeOperation(ByeMessage11 message, AsyncCallback callback, object state)
		{
			return new ByeOperation11AsyncResult(this, message, callback, state);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003127 File Offset: 0x00001327
		void IAnnouncementContract11.EndByeOperation(IAsyncResult result)
		{
			ByeOperation11AsyncResult.End(result);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000030E1 File Offset: 0x000012E1
		void IAnnouncementContractCD1.HelloOperation(HelloMessageCD1 message)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000312F File Offset: 0x0000132F
		IAsyncResult IAnnouncementContractCD1.BeginHelloOperation(HelloMessageCD1 message, AsyncCallback callback, object state)
		{
			return new HelloOperationCD1AsyncResult(this, message, callback, state);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000313A File Offset: 0x0000133A
		void IAnnouncementContractCD1.EndHelloOperation(IAsyncResult result)
		{
			HelloOperationCD1AsyncResult.End(result);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000030E1 File Offset: 0x000012E1
		void IAnnouncementContractCD1.ByeOperation(ByeMessageCD1 message)
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003142 File Offset: 0x00001342
		IAsyncResult IAnnouncementContractCD1.BeginByeOperation(ByeMessageCD1 message, AsyncCallback callback, object state)
		{
			return new ByeOperationCD1AsyncResult(this, message, callback, state);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000314D File Offset: 0x0000134D
		void IAnnouncementContractCD1.EndByeOperation(IAsyncResult result)
		{
			ByeOperationCD1AsyncResult.End(result);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003155 File Offset: 0x00001355
		bool IAnnouncementServiceImplementation.IsDuplicate(UniqueId messageId)
		{
			return this.duplicateDetector != null && !this.duplicateDetector.AddIfNotDuplicate(messageId);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003170 File Offset: 0x00001370
		IAsyncResult IAnnouncementServiceImplementation.OnBeginOnlineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			return this.OnBeginOnlineAnnouncement(messageSequence, endpointDiscoveryMetadata, callback, state);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000317D File Offset: 0x0000137D
		void IAnnouncementServiceImplementation.OnEndOnlineAnnouncement(IAsyncResult result)
		{
			this.OnEndOnlineAnnouncement(result);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003186 File Offset: 0x00001386
		IAsyncResult IAnnouncementServiceImplementation.OnBeginOfflineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			return this.OnBeginOfflineAnnouncement(messageSequence, endpointDiscoveryMetadata, callback, state);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003193 File Offset: 0x00001393
		void IAnnouncementServiceImplementation.OnEndOfflineAnnouncement(IAsyncResult result)
		{
			this.OnEndOfflineAnnouncement(result);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000319C File Offset: 0x0000139C
		protected virtual IAsyncResult OnBeginOnlineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			EventHandler<AnnouncementEventArgs> onlineAnnouncementReceived = this.OnlineAnnouncementReceived;
			if (onlineAnnouncementReceived != null)
			{
				onlineAnnouncementReceived(this, new AnnouncementEventArgs(messageSequence, endpointDiscoveryMetadata));
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000031C9 File Offset: 0x000013C9
		protected virtual void OnEndOnlineAnnouncement(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000031D4 File Offset: 0x000013D4
		protected virtual IAsyncResult OnBeginOfflineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
		{
			EventHandler<AnnouncementEventArgs> offlineAnnouncementReceived = this.OfflineAnnouncementReceived;
			if (offlineAnnouncementReceived != null)
			{
				offlineAnnouncementReceived(this, new AnnouncementEventArgs(messageSequence, endpointDiscoveryMetadata));
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000031C9 File Offset: 0x000013C9
		protected virtual void OnEndOfflineAnnouncement(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x0400001D RID: 29
		private DuplicateDetector<UniqueId> duplicateDetector;
	}
}
