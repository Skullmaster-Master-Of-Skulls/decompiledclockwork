using System;
using System.Collections;

namespace DynamicScreens
{
	// Token: 0x0200004D RID: 77
	public class DynamicControlCacheCollection : CollectionBase
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x00037C98 File Offset: 0x00036C98
		public int Add(DynamicControlCache controlCache)
		{
			return base.List.Add(controlCache);
		}

		// Token: 0x1700012E RID: 302
		public DynamicControlCache this[int index]
		{
			get
			{
				return (DynamicControlCache)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}
	}
}
