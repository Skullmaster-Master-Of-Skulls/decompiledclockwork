using System;
using System.Collections;
using a;

namespace MailBee
{
	// Token: 0x02000036 RID: 54
	public abstract class SortableByPriorityCollection : CollectionBase
	{
		// Token: 0x06000171 RID: 369 RVA: 0x00007DD6 File Offset: 0x00006DD6
		internal int i()
		{
			if (base.List.Count <= 0)
			{
				return 999;
			}
			return ((ax)base.List[0]).get_Priority();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007E04 File Offset: 0x00006E04
		internal int c(int A_0)
		{
			int num = 0;
			for (int i = 0; i < base.List.Count; i++)
			{
				if (((ax)base.List[i]).get_Priority() <= A_0)
				{
					num++;
				}
				else if (num > 0)
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007E4F File Offset: 0x00006E4F
		internal int h()
		{
			return this.c(this.i());
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007E60 File Offset: 0x00006E60
		public void SortByPriority()
		{
			if (base.List.Count > 1)
			{
				int priority = ((ax)base.List[0]).get_Priority();
				int num = priority;
				for (int i = 1; i < base.List.Count; i++)
				{
					priority = ((ax)base.List[i]).get_Priority();
					if (priority < num)
					{
						base.InnerList.Sort(ar.a());
						return;
					}
					num = priority;
				}
			}
		}
	}
}
