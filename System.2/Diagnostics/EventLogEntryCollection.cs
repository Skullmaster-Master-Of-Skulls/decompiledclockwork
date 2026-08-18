using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x020004CE RID: 1230
	public class EventLogEntryCollection : ICollection, IEnumerable
	{
		// Token: 0x06002E70 RID: 11888 RVA: 0x000D1C6B File Offset: 0x000CFE6B
		internal EventLogEntryCollection(EventLogInternal log)
		{
			this.log = log;
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x000D1C7A File Offset: 0x000CFE7A
		public int Count
		{
			get
			{
				return this.log.EntryCount;
			}
		}

		// Token: 0x17000B3C RID: 2876
		public virtual EventLogEntry this[int index]
		{
			get
			{
				return this.log.GetEntryAt(index);
			}
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x000D1C95 File Offset: 0x000CFE95
		public void CopyTo(EventLogEntry[] entries, int index)
		{
			((ICollection)this).CopyTo(entries, index);
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x000D1C9F File Offset: 0x000CFE9F
		public IEnumerator GetEnumerator()
		{
			return new EventLogEntryCollection.EntriesEnumerator(this);
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000D1CA7 File Offset: 0x000CFEA7
		internal EventLogEntry GetEntryAtNoThrow(int index)
		{
			return this.log.GetEntryAtNoThrow(index);
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x000D1CB5 File Offset: 0x000CFEB5
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002E77 RID: 11895 RVA: 0x000D1CB8 File Offset: 0x000CFEB8
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000D1CBC File Offset: 0x000CFEBC
		void ICollection.CopyTo(Array array, int index)
		{
			EventLogEntry[] allEntries = this.log.GetAllEntries();
			Array.Copy(allEntries, 0, array, index, allEntries.Length);
		}

		// Token: 0x04002770 RID: 10096
		private EventLogInternal log;

		// Token: 0x0200087F RID: 2175
		private class EntriesEnumerator : IEnumerator
		{
			// Token: 0x06004580 RID: 17792 RVA: 0x00121E3F File Offset: 0x0012003F
			internal EntriesEnumerator(EventLogEntryCollection entries)
			{
				this.entries = entries;
			}

			// Token: 0x17000FB9 RID: 4025
			// (get) Token: 0x06004581 RID: 17793 RVA: 0x00121E55 File Offset: 0x00120055
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

			// Token: 0x06004582 RID: 17794 RVA: 0x00121E75 File Offset: 0x00120075
			public bool MoveNext()
			{
				this.num++;
				this.cachedEntry = this.entries.GetEntryAtNoThrow(this.num);
				return this.cachedEntry != null;
			}

			// Token: 0x06004583 RID: 17795 RVA: 0x00121EA5 File Offset: 0x001200A5
			public void Reset()
			{
				this.num = -1;
			}

			// Token: 0x04003745 RID: 14149
			private EventLogEntryCollection entries;

			// Token: 0x04003746 RID: 14150
			private int num = -1;

			// Token: 0x04003747 RID: 14151
			private EventLogEntry cachedEntry;
		}
	}
}
