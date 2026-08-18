using System;
using System.Collections.Generic;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000011 RID: 17
	internal sealed class ClonableStack<T> : List<T>
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002F19 File Offset: 0x00001119
		public ClonableStack()
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F21 File Offset: 0x00001121
		public ClonableStack(int capacity) : base(capacity)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F2A File Offset: 0x0000112A
		private ClonableStack(IEnumerable<T> collection) : base(collection)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F33 File Offset: 0x00001133
		public void Push(T value)
		{
			base.Add(value);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002F3C File Offset: 0x0000113C
		public T Pop()
		{
			int index = base.Count - 1;
			T result = base[index];
			base.RemoveAt(index);
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002F62 File Offset: 0x00001162
		public T Peek()
		{
			return base[base.Count - 1];
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002F72 File Offset: 0x00001172
		public ClonableStack<T> Clone()
		{
			return new ClonableStack<T>(this);
		}
	}
}
