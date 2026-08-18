using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002C2 RID: 706
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventTask
	{
		// Token: 0x0600199B RID: 6555 RVA: 0x0005D244 File Offset: 0x0005B444
		internal EventTask(int value, ProviderMetadata pmReference)
		{
			this.value = value;
			this.pmReference = pmReference;
			this.syncObject = new object();
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0005D265 File Offset: 0x0005B465
		internal EventTask(string name, int value, string displayName, Guid guid)
		{
			this.value = value;
			this.name = name;
			this.displayName = displayName;
			this.guid = guid;
			this.dataReady = true;
			this.syncObject = new object();
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0005D29C File Offset: 0x0005B49C
		internal void PrepareData()
		{
			object obj = this.syncObject;
			lock (obj)
			{
				if (!this.dataReady)
				{
					IEnumerable<EventTask> tasks = this.pmReference.Tasks;
					this.name = null;
					this.displayName = null;
					this.guid = Guid.Empty;
					this.dataReady = true;
					foreach (EventTask eventTask in tasks)
					{
						if (eventTask.Value == this.value)
						{
							this.name = eventTask.Name;
							this.displayName = eventTask.DisplayName;
							this.guid = eventTask.EventGuid;
							this.dataReady = true;
							break;
						}
					}
				}
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x0005D380 File Offset: 0x0005B580
		public string Name
		{
			get
			{
				this.PrepareData();
				return this.name;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x0005D38E File Offset: 0x0005B58E
		public int Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x0005D396 File Offset: 0x0005B596
		public string DisplayName
		{
			get
			{
				this.PrepareData();
				return this.displayName;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x0005D3A4 File Offset: 0x0005B5A4
		public Guid EventGuid
		{
			get
			{
				this.PrepareData();
				return this.guid;
			}
		}

		// Token: 0x04000C90 RID: 3216
		private int value;

		// Token: 0x04000C91 RID: 3217
		private string name;

		// Token: 0x04000C92 RID: 3218
		private string displayName;

		// Token: 0x04000C93 RID: 3219
		private Guid guid;

		// Token: 0x04000C94 RID: 3220
		private bool dataReady;

		// Token: 0x04000C95 RID: 3221
		private ProviderMetadata pmReference;

		// Token: 0x04000C96 RID: 3222
		private object syncObject;
	}
}
