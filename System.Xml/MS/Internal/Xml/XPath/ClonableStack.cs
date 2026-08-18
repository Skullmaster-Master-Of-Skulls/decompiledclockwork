using System;
using System.Collections.Generic;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000130 RID: 304
	internal sealed class ClonableStack<T> : List<T>
	{
		// Token: 0x060011B9 RID: 4537 RVA: 0x0004E696 File Offset: 0x0004D696
		public ClonableStack()
		{
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0004E69E File Offset: 0x0004D69E
		public ClonableStack(int capacity) : base(capacity)
		{
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0004E6A7 File Offset: 0x0004D6A7
		private ClonableStack(IEnumerable<T> collection) : base(collection)
		{
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0004E6B0 File Offset: 0x0004D6B0
		public void Push(T value)
		{
			base.Add(value);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0004E6BC File Offset: 0x0004D6BC
		public T Pop()
		{
			int index = base.Count - 1;
			T result = base[index];
			base.RemoveAt(index);
			return result;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0004E6E2 File Offset: 0x0004D6E2
		public T Peek()
		{
			return base[base.Count - 1];
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0004E6F2 File Offset: 0x0004D6F2
		public ClonableStack<T> Clone()
		{
			return new ClonableStack<T>(this);
		}
	}
}
