using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI
{
	// Token: 0x020002CF RID: 719
	internal static class OrderedDictionaryStateHelper
	{
		// Token: 0x06002060 RID: 8288 RVA: 0x00067FD4 File Offset: 0x000661D4
		public static void LoadViewState(IOrderedDictionary dictionary, ArrayList state)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (state == null)
			{
				throw new ArgumentNullException("state");
			}
			if (state != null)
			{
				for (int i = 0; i < state.Count; i++)
				{
					Pair pair = (Pair)state[i];
					dictionary.Add(pair.First, pair.Second);
				}
			}
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00068030 File Offset: 0x00066230
		public static ArrayList SaveViewState(IOrderedDictionary dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			ArrayList arrayList = new ArrayList(dictionary.Count);
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				arrayList.Add(new Pair(dictionaryEntry.Key, dictionaryEntry.Value));
			}
			return arrayList;
		}
	}
}
