using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004D6 RID: 1238
	internal class TrieSegmentKeyComparer : IItemComparer<char, TrieSegment>
	{
		// Token: 0x06002EEB RID: 12011 RVA: 0x000B57F9 File Offset: 0x000B39F9
		public int Compare(char c, TrieSegment t)
		{
			return (int)(c - t.FirstChar);
		}
	}
}
