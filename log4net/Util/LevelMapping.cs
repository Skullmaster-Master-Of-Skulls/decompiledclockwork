using System;
using System.Collections;
using log4net.Core;

namespace log4net.Util
{
	// Token: 0x020000FC RID: 252
	public sealed class LevelMapping : IOptionHandler
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x000170DC File Offset: 0x000152DC
		public void Add(LevelMappingEntry entry)
		{
			if (this.m_entriesMap.ContainsKey(entry.Level))
			{
				this.m_entriesMap.Remove(entry.Level);
			}
			this.m_entriesMap.Add(entry.Level, entry);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00017114 File Offset: 0x00015314
		public LevelMappingEntry Lookup(Level level)
		{
			if (this.m_entries != null)
			{
				foreach (LevelMappingEntry levelMappingEntry in this.m_entries)
				{
					if (level >= levelMappingEntry.Level)
					{
						return levelMappingEntry;
					}
				}
			}
			return null;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00017158 File Offset: 0x00015358
		public void ActivateOptions()
		{
			Level[] array = new Level[this.m_entriesMap.Count];
			LevelMappingEntry[] array2 = new LevelMappingEntry[this.m_entriesMap.Count];
			this.m_entriesMap.Keys.CopyTo(array, 0);
			this.m_entriesMap.Values.CopyTo(array2, 0);
			Array.Sort<Level, LevelMappingEntry>(array, array2, 0, array.Length, null);
			Array.Reverse(array2, 0, array2.Length);
			foreach (LevelMappingEntry levelMappingEntry in array2)
			{
				levelMappingEntry.ActivateOptions();
			}
			this.m_entries = array2;
		}

		// Token: 0x040002B3 RID: 691
		private Hashtable m_entriesMap = new Hashtable();

		// Token: 0x040002B4 RID: 692
		private LevelMappingEntry[] m_entries;
	}
}
