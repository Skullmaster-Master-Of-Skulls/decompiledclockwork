using System;

namespace System.Diagnostics
{
	// Token: 0x020004C8 RID: 1224
	public class EntryWrittenEventArgs : EventArgs
	{
		// Token: 0x06002DAF RID: 11695 RVA: 0x000CDA5E File Offset: 0x000CBC5E
		public EntryWrittenEventArgs()
		{
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x000CDA66 File Offset: 0x000CBC66
		public EntryWrittenEventArgs(EventLogEntry entry)
		{
			this.entry = entry;
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x000CDA75 File Offset: 0x000CBC75
		public EventLogEntry Entry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x04002737 RID: 10039
		private EventLogEntry entry;
	}
}
