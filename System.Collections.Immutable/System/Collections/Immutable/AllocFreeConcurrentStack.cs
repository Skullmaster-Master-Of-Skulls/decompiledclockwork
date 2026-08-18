using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000008 RID: 8
	internal static class AllocFreeConcurrentStack<T>
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002ACC File Offset: 0x00000CCC
		public static void TryAdd(T item)
		{
			Stack<RefAsValueType<T>> stack = AllocFreeConcurrentStack<T>.t_stack;
			if (stack == null)
			{
				stack = (AllocFreeConcurrentStack<T>.t_stack = new Stack<RefAsValueType<T>>(35));
			}
			if (stack.Count < 35)
			{
				stack.Push(new RefAsValueType<T>(item));
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B08 File Offset: 0x00000D08
		public static bool TryTake(out T item)
		{
			Stack<RefAsValueType<T>> stack = AllocFreeConcurrentStack<T>.t_stack;
			if (stack != null && stack.Count > 0)
			{
				item = stack.Pop().Value;
				return true;
			}
			item = default(T);
			return false;
		}

		// Token: 0x04000003 RID: 3
		private const int MaxSize = 35;

		// Token: 0x04000004 RID: 4
		[ThreadStatic]
		private static Stack<RefAsValueType<T>> t_stack;
	}
}
