using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x02000501 RID: 1281
	public class ProcessThreadCollection : ReadOnlyCollectionBase
	{
		// Token: 0x060030B3 RID: 12467 RVA: 0x000DC310 File Offset: 0x000DA510
		protected ProcessThreadCollection()
		{
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000DC318 File Offset: 0x000DA518
		public ProcessThreadCollection(ProcessThread[] processThreads)
		{
			base.InnerList.AddRange(processThreads);
		}

		// Token: 0x17000BF9 RID: 3065
		public ProcessThread this[int index]
		{
			get
			{
				return (ProcessThread)base.InnerList[index];
			}
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000DC33F File Offset: 0x000DA53F
		public int Add(ProcessThread thread)
		{
			return base.InnerList.Add(thread);
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x000DC34D File Offset: 0x000DA54D
		public void Insert(int index, ProcessThread thread)
		{
			base.InnerList.Insert(index, thread);
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000DC35C File Offset: 0x000DA55C
		public int IndexOf(ProcessThread thread)
		{
			return base.InnerList.IndexOf(thread);
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000DC36A File Offset: 0x000DA56A
		public bool Contains(ProcessThread thread)
		{
			return base.InnerList.Contains(thread);
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000DC378 File Offset: 0x000DA578
		public void Remove(ProcessThread thread)
		{
			base.InnerList.Remove(thread);
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000DC386 File Offset: 0x000DA586
		public void CopyTo(ProcessThread[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}
	}
}
