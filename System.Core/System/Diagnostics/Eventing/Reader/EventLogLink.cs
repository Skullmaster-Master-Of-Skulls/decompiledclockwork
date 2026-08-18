using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002AF RID: 687
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventLogLink
	{
		// Token: 0x060018E2 RID: 6370 RVA: 0x0005B1DF File Offset: 0x000593DF
		internal EventLogLink(uint channelId, ProviderMetadata pmReference)
		{
			this.channelId = channelId;
			this.pmReference = pmReference;
			this.syncObject = new object();
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0005B200 File Offset: 0x00059400
		internal EventLogLink(string channelName, bool isImported, string displayName, uint channelId)
		{
			this.channelName = channelName;
			this.isImported = isImported;
			this.displayName = displayName;
			this.channelId = channelId;
			this.dataReady = true;
			this.syncObject = new object();
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0005B238 File Offset: 0x00059438
		private void PrepareData()
		{
			if (this.dataReady)
			{
				return;
			}
			object obj = this.syncObject;
			lock (obj)
			{
				if (!this.dataReady)
				{
					IEnumerable<EventLogLink> logLinks = this.pmReference.LogLinks;
					this.channelName = null;
					this.isImported = false;
					this.displayName = null;
					this.dataReady = true;
					foreach (EventLogLink eventLogLink in logLinks)
					{
						if (eventLogLink.ChannelId == this.channelId)
						{
							this.channelName = eventLogLink.LogName;
							this.isImported = eventLogLink.IsImported;
							this.displayName = eventLogLink.DisplayName;
							this.dataReady = true;
							break;
						}
					}
				}
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x0005B320 File Offset: 0x00059520
		public string LogName
		{
			get
			{
				this.PrepareData();
				return this.channelName;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x0005B32E File Offset: 0x0005952E
		public bool IsImported
		{
			get
			{
				this.PrepareData();
				return this.isImported;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x0005B33C File Offset: 0x0005953C
		public string DisplayName
		{
			get
			{
				this.PrepareData();
				return this.displayName;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x0005B34A File Offset: 0x0005954A
		internal uint ChannelId
		{
			get
			{
				return this.channelId;
			}
		}

		// Token: 0x04000C2B RID: 3115
		private string channelName;

		// Token: 0x04000C2C RID: 3116
		private bool isImported;

		// Token: 0x04000C2D RID: 3117
		private string displayName;

		// Token: 0x04000C2E RID: 3118
		private uint channelId;

		// Token: 0x04000C2F RID: 3119
		private bool dataReady;

		// Token: 0x04000C30 RID: 3120
		private ProviderMetadata pmReference;

		// Token: 0x04000C31 RID: 3121
		private object syncObject;
	}
}
