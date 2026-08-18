using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002B4 RID: 692
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventKeyword
	{
		// Token: 0x06001910 RID: 6416 RVA: 0x0005B458 File Offset: 0x00059658
		internal EventKeyword(long value, ProviderMetadata pmReference)
		{
			this.value = value;
			this.pmReference = pmReference;
			this.syncObject = new object();
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0005B479 File Offset: 0x00059679
		internal EventKeyword(string name, long value, string displayName)
		{
			this.value = value;
			this.name = name;
			this.displayName = displayName;
			this.dataReady = true;
			this.syncObject = new object();
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0005B4A8 File Offset: 0x000596A8
		internal void PrepareData()
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
					IEnumerable<EventKeyword> keywords = this.pmReference.Keywords;
					this.name = null;
					this.displayName = null;
					this.dataReady = true;
					foreach (EventKeyword eventKeyword in keywords)
					{
						if (eventKeyword.Value == this.value)
						{
							this.name = eventKeyword.Name;
							this.displayName = eventKeyword.DisplayName;
							break;
						}
					}
				}
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x0005B570 File Offset: 0x00059770
		public string Name
		{
			get
			{
				this.PrepareData();
				return this.name;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x0005B57E File Offset: 0x0005977E
		public long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x0005B586 File Offset: 0x00059786
		public string DisplayName
		{
			get
			{
				this.PrepareData();
				return this.displayName;
			}
		}

		// Token: 0x04000C36 RID: 3126
		private long value;

		// Token: 0x04000C37 RID: 3127
		private string name;

		// Token: 0x04000C38 RID: 3128
		private string displayName;

		// Token: 0x04000C39 RID: 3129
		private bool dataReady;

		// Token: 0x04000C3A RID: 3130
		private ProviderMetadata pmReference;

		// Token: 0x04000C3B RID: 3131
		private object syncObject;
	}
}
