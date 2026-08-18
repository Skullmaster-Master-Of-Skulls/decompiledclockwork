using System;

namespace System.Collections.Specialized
{
	// Token: 0x020003A8 RID: 936
	public class CollectionsUtil
	{
		// Token: 0x060022FC RID: 8956 RVA: 0x000A6579 File Offset: 0x000A4779
		public static Hashtable CreateCaseInsensitiveHashtable()
		{
			return new Hashtable(StringComparer.CurrentCultureIgnoreCase);
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x000A6585 File Offset: 0x000A4785
		public static Hashtable CreateCaseInsensitiveHashtable(int capacity)
		{
			return new Hashtable(capacity, StringComparer.CurrentCultureIgnoreCase);
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000A6592 File Offset: 0x000A4792
		public static Hashtable CreateCaseInsensitiveHashtable(IDictionary d)
		{
			return new Hashtable(d, StringComparer.CurrentCultureIgnoreCase);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000A659F File Offset: 0x000A479F
		public static SortedList CreateCaseInsensitiveSortedList()
		{
			return new SortedList(CaseInsensitiveComparer.Default);
		}
	}
}
