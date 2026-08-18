using System;
using System.Collections.Generic;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000034 RID: 52
	public static class ImmutableStack
	{
		// Token: 0x06000347 RID: 839 RVA: 0x00009090 File Offset: 0x00007290
		public static ImmutableStack<T> Create<T>()
		{
			return ImmutableStack<T>.Empty;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00009097 File Offset: 0x00007297
		public static ImmutableStack<T> Create<T>(T item)
		{
			return ImmutableStack<T>.Empty.Push(item);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000090A4 File Offset: 0x000072A4
		public static ImmutableStack<T> CreateRange<T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
			foreach (T value in items)
			{
				immutableStack = immutableStack.Push(value);
			}
			return immutableStack;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00009100 File Offset: 0x00007300
		public static ImmutableStack<T> Create<T>(params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
			foreach (T value in items)
			{
				immutableStack = immutableStack.Push(value);
			}
			return immutableStack;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000913F File Offset: 0x0000733F
		public static IImmutableStack<T> Pop<T>(this IImmutableStack<T> stack, out T value)
		{
			Requires.NotNull<IImmutableStack<T>>(stack, "stack");
			value = stack.Peek();
			return stack.Pop();
		}
	}
}
