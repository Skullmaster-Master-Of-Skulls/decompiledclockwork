using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004D5 RID: 1237
	internal class TrieSegmentComparer : IComparer<TrieSegment>
	{
		// Token: 0x06002EE7 RID: 12007 RVA: 0x000B57CA File Offset: 0x000B39CA
		public int Compare(TrieSegment t1, TrieSegment t2)
		{
			return (int)(t1.FirstChar - t2.FirstChar);
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000B57D9 File Offset: 0x000B39D9
		public bool Equals(TrieSegment t1, TrieSegment t2)
		{
			return t1.FirstChar == t2.FirstChar;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000B57E9 File Offset: 0x000B39E9
		public int GetHashCode(TrieSegment t)
		{
			return t.GetHashCode();
		}
	}
}
