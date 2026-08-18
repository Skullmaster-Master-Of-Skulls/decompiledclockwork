using System;

namespace System.Diagnostics
{
	// Token: 0x0200074B RID: 1867
	public class EntryWrittenEventArgs : EventArgs
	{
		// Token: 0x060038F8 RID: 14584 RVA: 0x000F0832 File Offset: 0x000EF832
		public EntryWrittenEventArgs()
		{
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x000F083A File Offset: 0x000EF83A
		public EntryWrittenEventArgs(EventLogEntry entry)
		{
			this.entry = entry;
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x060038FA RID: 14586 RVA: 0x000F0849 File Offset: 0x000EF849
		public EventLogEntry Entry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x04003280 RID: 12928
		private EventLogEntry entry;
	}
}
