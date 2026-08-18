using System;
using System.Collections.Generic;
using System.Threading;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x02000024 RID: 36
	public static class ImmutableInterlocked
	{
		// Token: 0x060001FE RID: 510 RVA: 0x00006778 File Offset: 0x00004978
		public static bool Update<T>(ref T location, Func<T, T> transformer) where T : class
		{
			Requires.NotNull<Func<T, T>>(transformer, "transformer");
			T t = Volatile.Read<T>(ref location);
			for (;;)
			{
				T t2 = transformer(t);
				if (t == t2)
				{
					break;
				}
				T t3 = Interlocked.CompareExchange<T>(ref location, t2, t);
				bool flag = t == t3;
				t = t3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000067D0 File Offset: 0x000049D0
		public static bool Update<T, TArg>(ref T location, Func<T, TArg, T> transformer, TArg transformerArgument) where T : class
		{
			Requires.NotNull<Func<T, TArg, T>>(transformer, "transformer");
			T t = Volatile.Read<T>(ref location);
			for (;;)
			{
				T t2 = transformer(t, transformerArgument);
				if (t == t2)
				{
					break;
				}
				T t3 = Interlocked.CompareExchange<T>(ref location, t2, t);
				bool flag = t == t3;
				t = t3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006826 File Offset: 0x00004A26
		public static ImmutableArray<T> InterlockedExchange<T>(ref ImmutableArray<T> location, ImmutableArray<T> value)
		{
			return new ImmutableArray<T>(Interlocked.Exchange<T[]>(ref location.array, value.array));
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000683E File Offset: 0x00004A3E
		public static ImmutableArray<T> InterlockedCompareExchange<T>(ref ImmutableArray<T> location, ImmutableArray<T> value, ImmutableArray<T> comparand)
		{
			return new ImmutableArray<T>(Interlocked.CompareExchange<T[]>(ref location.array, value.array, comparand.array));
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000685C File Offset: 0x00004A5C
		public static bool InterlockedInitialize<T>(ref ImmutableArray<T> location, ImmutableArray<T> value)
		{
			return ImmutableInterlocked.InterlockedCompareExchange<T>(ref location, value, default(ImmutableArray<T>)).IsDefault;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00006884 File Offset: 0x00004A84
		public static TValue GetOrAdd<TKey, TValue, TArg>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
		{
			Requires.NotNull<Func<TKey, TArg, TValue>>(valueFactory, "valueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
			TValue tvalue;
			if (immutableDictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = valueFactory(key, factoryArgument);
			return ImmutableInterlocked.GetOrAdd<TKey, TValue>(ref location, key, tvalue);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000068CC File Offset: 0x00004ACC
		public static TValue GetOrAdd<TKey, TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TValue> valueFactory)
		{
			Requires.NotNull<Func<TKey, TValue>>(valueFactory, "valueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
			TValue tvalue;
			if (immutableDictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = valueFactory(key);
			return ImmutableInterlocked.GetOrAdd<TKey, TValue>(ref location, key, tvalue);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006914 File Offset: 0x00004B14
		public static TValue GetOrAdd<TKey, TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue result;
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (immutableDictionary.TryGetValue(key, out result))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> value2 = immutableDictionary.Add(key, value);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary2;
				immutableDictionary = immutableDictionary2;
				if (flag)
				{
					return value;
				}
			}
			return result;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006960 File Offset: 0x00004B60
		public static TValue AddOrUpdate<TKey, TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
		{
			Requires.NotNull<Func<TKey, TValue>>(addValueFactory, "addValueFactory");
			Requires.NotNull<Func<TKey, TValue, TValue>>(updateValueFactory, "updateValueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue tvalue;
			bool flag;
			do
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue arg;
				if (immutableDictionary.TryGetValue(key, out arg))
				{
					tvalue = updateValueFactory(key, arg);
				}
				else
				{
					tvalue = addValueFactory(key);
				}
				ImmutableDictionary<TKey, TValue> value = immutableDictionary.SetItem(key, tvalue);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value, immutableDictionary);
				flag = (immutableDictionary == immutableDictionary2);
				immutableDictionary = immutableDictionary2;
			}
			while (!flag);
			return tvalue;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000069D8 File Offset: 0x00004BD8
		public static TValue AddOrUpdate<TKey, TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
		{
			Requires.NotNull<Func<TKey, TValue, TValue>>(updateValueFactory, "updateValueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue tvalue;
			bool flag;
			do
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue arg;
				if (immutableDictionary.TryGetValue(key, out arg))
				{
					tvalue = updateValueFactory(key, arg);
				}
				else
				{
					tvalue = addValue;
				}
				ImmutableDictionary<TKey, TValue> value = immutableDictionary.SetItem(key, tvalue);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value, immutableDictionary);
				flag = (immutableDictionary == immutableDictionary2);
				immutableDictionary = immutableDictionary2;
			}
			while (!flag);
			return tvalue;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006A3C File Offset: 0x00004C3C
		public static bool TryAdd<TKey, TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (immutableDictionary.ContainsKey(key))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> value2 = immutableDictionary.Add(key, value);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary2;
				immutableDictionary = immutableDictionary2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006A84 File Offset: 0x00004C84
		public static bool TryUpdate<TKey, TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue newValue, TValue comparisonValue)
		{
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue x;
				if (!immutableDictionary.TryGetValue(key, out x) || !@default.Equals(x, comparisonValue))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> value = immutableDictionary.SetItem(key, newValue);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary2;
				immutableDictionary = immutableDictionary2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006AE4 File Offset: 0x00004CE4
		public static bool TryRemove<TKey, TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, out TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (!immutableDictionary.TryGetValue(key, out value))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> value2 = immutableDictionary.Remove(key);
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, value2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary2;
				immutableDictionary = immutableDictionary2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006B2C File Offset: 0x00004D2C
		public static bool TryPop<T>(ref ImmutableStack<T> location, out T value)
		{
			ImmutableStack<T> immutableStack = Volatile.Read<ImmutableStack<T>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableStack<T>>(immutableStack, "location");
				if (immutableStack.IsEmpty)
				{
					break;
				}
				ImmutableStack<T> value2 = immutableStack.Pop(out value);
				ImmutableStack<T> immutableStack2 = Interlocked.CompareExchange<ImmutableStack<T>>(ref location, value2, immutableStack);
				bool flag = immutableStack == immutableStack2;
				immutableStack = immutableStack2;
				if (flag)
				{
					return true;
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006B78 File Offset: 0x00004D78
		public static void Push<T>(ref ImmutableStack<T> location, T value)
		{
			ImmutableStack<T> immutableStack = Volatile.Read<ImmutableStack<T>>(ref location);
			bool flag;
			do
			{
				Requires.NotNull<ImmutableStack<T>>(immutableStack, "location");
				ImmutableStack<T> value2 = immutableStack.Push(value);
				ImmutableStack<T> immutableStack2 = Interlocked.CompareExchange<ImmutableStack<T>>(ref location, value2, immutableStack);
				flag = (immutableStack == immutableStack2);
				immutableStack = immutableStack2;
			}
			while (!flag);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006BB4 File Offset: 0x00004DB4
		public static bool TryDequeue<T>(ref ImmutableQueue<T> location, out T value)
		{
			ImmutableQueue<T> immutableQueue = Volatile.Read<ImmutableQueue<T>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableQueue<T>>(immutableQueue, "location");
				if (immutableQueue.IsEmpty)
				{
					break;
				}
				ImmutableQueue<T> value2 = immutableQueue.Dequeue(out value);
				ImmutableQueue<T> immutableQueue2 = Interlocked.CompareExchange<ImmutableQueue<T>>(ref location, value2, immutableQueue);
				bool flag = immutableQueue == immutableQueue2;
				immutableQueue = immutableQueue2;
				if (flag)
				{
					return true;
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006C00 File Offset: 0x00004E00
		public static void Enqueue<T>(ref ImmutableQueue<T> location, T value)
		{
			ImmutableQueue<T> immutableQueue = Volatile.Read<ImmutableQueue<T>>(ref location);
			bool flag;
			do
			{
				Requires.NotNull<ImmutableQueue<T>>(immutableQueue, "location");
				ImmutableQueue<T> value2 = immutableQueue.Enqueue(value);
				ImmutableQueue<T> immutableQueue2 = Interlocked.CompareExchange<ImmutableQueue<T>>(ref location, value2, immutableQueue);
				flag = (immutableQueue == immutableQueue2);
				immutableQueue = immutableQueue2;
			}
			while (!flag);
		}
	}
}
