using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x0200078D RID: 1933
	public class ProcessThreadCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06003BCB RID: 15307 RVA: 0x000FE6F0 File Offset: 0x000FD6F0
		protected ProcessThreadCollection()
		{
		}

		// Token: 0x06003BCC RID: 15308 RVA: 0x000FE6F8 File Offset: 0x000FD6F8
		public ProcessThreadCollection(ProcessThread[] processThreads)
		{
			base.InnerList.AddRange(processThreads);
		}

		// Token: 0x17000E17 RID: 3607
		public ProcessThread this[int index]
		{
			get
			{
				return (ProcessThread)base.InnerList[index];
			}
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x000FE71F File Offset: 0x000FD71F
		public int Add(ProcessThread thread)
		{
			return base.InnerList.Add(thread);
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x000FE72D File Offset: 0x000FD72D
		public void Insert(int index, ProcessThread thread)
		{
			base.InnerList.Insert(index, thread);
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x000FE73C File Offset: 0x000FD73C
		public int IndexOf(ProcessThread thread)
		{
			return base.InnerList.IndexOf(thread);
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x000FE74A File Offset: 0x000FD74A
		public bool Contains(ProcessThread thread)
		{
			return base.InnerList.Contains(thread);
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x000FE758 File Offset: 0x000FD758
		public void Remove(ProcessThread thread)
		{
			base.InnerList.Remove(thread);
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x000FE766 File Offset: 0x000FD766
		public void CopyTo(ProcessThread[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}
	}
}
