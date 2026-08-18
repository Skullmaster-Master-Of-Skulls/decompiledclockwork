using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x02000753 RID: 1875
	public class EventLogEntryCollection : ICollection, IEnumerable
	{
		// Token: 0x0600398C RID: 14732 RVA: 0x000F4717 File Offset: 0x000F3717
		internal EventLogEntryCollection(EventLog log)
		{
			this.log = log;
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x000F4726 File Offset: 0x000F3726
		public int Count
		{
			get
			{
				return this.log.EntryCount;
			}
		}

		// Token: 0x17000D5D RID: 3421
		public virtual EventLogEntry this[int index]
		{
			get
			{
				return this.log.GetEntryAt(index);
			}
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x000F4741 File Offset: 0x000F3741
		public void CopyTo(EventLogEntry[] entries, int index)
		{
			((ICollection)this).CopyTo(entries, index);
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x000F474B File Offset: 0x000F374B
		public IEnumerator GetEnumerator()
		{
			return new EventLogEntryCollection.EntriesEnumerator(this);
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x000F4753 File Offset: 0x000F3753
		internal EventLogEntry GetEntryAtNoThrow(int index)
		{
			return this.log.GetEntryAtNoThrow(index);
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x000F4761 File Offset: 0x000F3761
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x000F4764 File Offset: 0x000F3764
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x000F4768 File Offset: 0x000F3768
		void ICollection.CopyTo(Array array, int index)
		{
			EventLogEntry[] allEntries = this.log.GetAllEntries();
			Array.Copy(allEntries, 0, array, index, allEntries.Length);
		}

		// Token: 0x040032C5 RID: 12997
		private EventLog log;

		// Token: 0x02000754 RID: 1876
		private class EntriesEnumerator : IEnumerator
		{
			// Token: 0x06003995 RID: 14741 RVA: 0x000F478D File Offset: 0x000F378D
			internal EntriesEnumerator(EventLogEntryCollection entries)
			{
				this.entries = entries;
			}

			// Token: 0x17000D60 RID: 3424
			// (get) Token: 0x06003996 RID: 14742 RVA: 0x000F47A3 File Offset: 0x000F37A3
			public object Current
			{
				get
				{
					if (this.cachedEntry == null)
					{
						throw new InvalidOperationException(SR.GetString("NoCurrentEntry"));
					}
					return this.cachedEntry;
				}
			}

			// Token: 0x06003997 RID: 14743 RVA: 0x000F47C3 File Offset: 0x000F37C3
			public bool MoveNext()
			{
				this.num++;
				this.cachedEntry = this.entries.GetEntryAtNoThrow(this.num);
				return this.cachedEntry != null;
			}

			// Token: 0x06003998 RID: 14744 RVA: 0x000F47F6 File Offset: 0x000F37F6
			public void Reset()
			{
				this.num = -1;
			}

			// Token: 0x040032C6 RID: 12998
			private EventLogEntryCollection entries;

			// Token: 0x040032C7 RID: 12999
			private int num = -1;

			// Token: 0x040032C8 RID: 13000
			private EventLogEntry cachedEntry;
		}
	}
}
