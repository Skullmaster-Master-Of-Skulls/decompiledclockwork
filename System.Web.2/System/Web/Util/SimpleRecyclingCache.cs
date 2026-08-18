using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x02000220 RID: 544
	internal class SimpleRecyclingCache
	{
		// Token: 0x06001A1F RID: 6687 RVA: 0x00051D37 File Offset: 0x0004FF37
		internal SimpleRecyclingCache()
		{
			this.CreateHashtable();
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x00051D45 File Offset: 0x0004FF45
		private void CreateHashtable()
		{
			SimpleRecyclingCache._hashtable = new Hashtable(100, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000774 RID: 1908
		internal object this[object key]
		{
			get
			{
				return SimpleRecyclingCache._hashtable[key];
			}
			set
			{
				lock (this)
				{
					if (SimpleRecyclingCache._hashtable.Count >= 100)
					{
						SimpleRecyclingCache._hashtable.Clear();
					}
					SimpleRecyclingCache._hashtable[key] = value;
				}
			}
		}

		// Token: 0x04001816 RID: 6166
		private const int MAX_SIZE = 100;

		// Token: 0x04001817 RID: 6167
		private static Hashtable _hashtable;
	}
}
