using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002B5 RID: 693
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventLevel
	{
		// Token: 0x06001916 RID: 6422 RVA: 0x0005B594 File Offset: 0x00059794
		internal EventLevel(int value, ProviderMetadata pmReference)
		{
			this.value = value;
			this.pmReference = pmReference;
			this.syncObject = new object();
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0005B5B5 File Offset: 0x000597B5
		internal EventLevel(string name, int value, string displayName)
		{
			this.value = value;
			this.name = name;
			this.displayName = displayName;
			this.dataReady = true;
			this.syncObject = new object();
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0005B5E4 File Offset: 0x000597E4
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
					IEnumerable<EventLevel> levels = this.pmReference.Levels;
					this.name = null;
					this.displayName = null;
					this.dataReady = true;
					foreach (EventLevel eventLevel in levels)
					{
						if (eventLevel.Value == this.value)
						{
							this.name = eventLevel.Name;
							this.displayName = eventLevel.DisplayName;
							break;
						}
					}
				}
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x0005B6AC File Offset: 0x000598AC
		public string Name
		{
			get
			{
				this.PrepareData();
				return this.name;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x0005B6BA File Offset: 0x000598BA
		public int Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x0005B6C2 File Offset: 0x000598C2
		public string DisplayName
		{
			get
			{
				this.PrepareData();
				return this.displayName;
			}
		}

		// Token: 0x04000C3C RID: 3132
		private int value;

		// Token: 0x04000C3D RID: 3133
		private string name;

		// Token: 0x04000C3E RID: 3134
		private string displayName;

		// Token: 0x04000C3F RID: 3135
		private bool dataReady;

		// Token: 0x04000C40 RID: 3136
		private ProviderMetadata pmReference;

		// Token: 0x04000C41 RID: 3137
		private object syncObject;
	}
}
