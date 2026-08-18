using System;
using System.Collections.Generic;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000029 RID: 41
	public static class ImmutableQueue
	{
		// Token: 0x06000283 RID: 643 RVA: 0x000078D4 File Offset: 0x00005AD4
		public static ImmutableQueue<T> Create<T>()
		{
			return ImmutableQueue<T>.Empty;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000078DB File Offset: 0x00005ADB
		public static ImmutableQueue<T> Create<T>(T item)
		{
			return ImmutableQueue<T>.Empty.Enqueue(item);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000078E8 File Offset: 0x00005AE8
		public static ImmutableQueue<T> CreateRange<T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableQueue<T> immutableQueue = ImmutableQueue<T>.Empty;
			foreach (T value in items)
			{
				immutableQueue = immutableQueue.Enqueue(value);
			}
			return immutableQueue;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00007944 File Offset: 0x00005B44
		public static ImmutableQueue<T> Create<T>(params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			ImmutableQueue<T> immutableQueue = ImmutableQueue<T>.Empty;
			foreach (T value in items)
			{
				immutableQueue = immutableQueue.Enqueue(value);
			}
			return immutableQueue;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007983 File Offset: 0x00005B83
		public static IImmutableQueue<T> Dequeue<T>(this IImmutableQueue<T> queue, out T value)
		{
			Requires.NotNull<IImmutableQueue<T>>(queue, "queue");
			value = queue.Peek();
			return queue.Dequeue();
		}
	}
}
