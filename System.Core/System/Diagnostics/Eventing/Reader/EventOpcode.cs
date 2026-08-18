using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002C1 RID: 705
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventOpcode
	{
		// Token: 0x06001995 RID: 6549 RVA: 0x0005D103 File Offset: 0x0005B303
		internal EventOpcode(int value, ProviderMetadata pmReference)
		{
			this.value = value;
			this.pmReference = pmReference;
			this.syncObject = new object();
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0005D124 File Offset: 0x0005B324
		internal EventOpcode(string name, int value, string displayName)
		{
			this.value = value;
			this.name = name;
			this.displayName = displayName;
			this.dataReady = true;
			this.syncObject = new object();
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0005D154 File Offset: 0x0005B354
		internal void PrepareData()
		{
			object obj = this.syncObject;
			lock (obj)
			{
				if (!this.dataReady)
				{
					IEnumerable<EventOpcode> opcodes = this.pmReference.Opcodes;
					this.name = null;
					this.displayName = null;
					this.dataReady = true;
					foreach (EventOpcode eventOpcode in opcodes)
					{
						if (eventOpcode.Value == this.value)
						{
							this.name = eventOpcode.Name;
							this.displayName = eventOpcode.DisplayName;
							this.dataReady = true;
							break;
						}
					}
				}
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x0005D220 File Offset: 0x0005B420
		public string Name
		{
			get
			{
				this.PrepareData();
				return this.name;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001999 RID: 6553 RVA: 0x0005D22E File Offset: 0x0005B42E
		public int Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x0005D236 File Offset: 0x0005B436
		public string DisplayName
		{
			get
			{
				this.PrepareData();
				return this.displayName;
			}
		}

		// Token: 0x04000C8A RID: 3210
		private int value;

		// Token: 0x04000C8B RID: 3211
		private string name;

		// Token: 0x04000C8C RID: 3212
		private string displayName;

		// Token: 0x04000C8D RID: 3213
		private bool dataReady;

		// Token: 0x04000C8E RID: 3214
		private ProviderMetadata pmReference;

		// Token: 0x04000C8F RID: 3215
		private object syncObject;
	}
}
