using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000176 RID: 374
	internal sealed class SynchronousChannel<T>
	{
		// Token: 0x06000DCF RID: 3535 RVA: 0x00031140 File Offset: 0x0002F340
		internal SynchronousChannel()
		{
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00031148 File Offset: 0x0002F348
		internal void Init()
		{
			this.m_queue = new Queue<T>();
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00031155 File Offset: 0x0002F355
		internal void Enqueue(T item)
		{
			this.m_queue.Enqueue(item);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00031163 File Offset: 0x0002F363
		internal T Dequeue()
		{
			return this.m_queue.Dequeue();
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00031170 File Offset: 0x0002F370
		internal void SetDone()
		{
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00031172 File Offset: 0x0002F372
		internal void CopyTo(T[] array, int arrayIndex)
		{
			this.m_queue.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00031181 File Offset: 0x0002F381
		internal int Count
		{
			get
			{
				return this.m_queue.Count;
			}
		}

		// Token: 0x04000811 RID: 2065
		private Queue<T> m_queue;
	}
}
