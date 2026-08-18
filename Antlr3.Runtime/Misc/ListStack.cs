using System;
using System.Collections.Generic;

namespace Antlr.Runtime.Misc
{
	// Token: 0x02000029 RID: 41
	public class ListStack<T> : List<T>
	{
		// Token: 0x060001DF RID: 479 RVA: 0x00005F68 File Offset: 0x00004168
		public T Peek()
		{
			return this.Peek(0);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005F74 File Offset: 0x00004174
		public T Peek(int depth)
		{
			T result;
			if (!this.TryPeek(depth, out result))
			{
				throw new InvalidOperationException();
			}
			return result;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005F93 File Offset: 0x00004193
		public bool TryPeek(out T item)
		{
			return this.TryPeek(0, out item);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005F9D File Offset: 0x0000419D
		public bool TryPeek(int depth, out T item)
		{
			if (depth >= base.Count)
			{
				item = default(T);
				return false;
			}
			item = base[base.Count - depth - 1];
			return true;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005FC8 File Offset: 0x000041C8
		public T Pop()
		{
			T result;
			if (!this.TryPop(out result))
			{
				throw new InvalidOperationException();
			}
			return result;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005FE6 File Offset: 0x000041E6
		public bool TryPop(out T item)
		{
			if (base.Count == 0)
			{
				item = default(T);
				return false;
			}
			item = base[base.Count - 1];
			base.RemoveAt(base.Count - 1);
			return true;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000601C File Offset: 0x0000421C
		public void Push(T item)
		{
			base.Add(item);
		}
	}
}
