using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Validation;

namespace System.Collections.Immutable
{
	// Token: 0x0200001A RID: 26
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public struct ImmutableArray<T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable, IList<!0>, ICollection<!0>, IEquatable<ImmutableArray<T>>, IImmutableList<T>, IList, ICollection, IImmutableArray, IStructuralComparable, IStructuralEquatable
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00003832 File Offset: 0x00001A32
		internal ImmutableArray(T[] items)
		{
			this.array = items;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000383B File Offset: 0x00001A3B
		public static bool operator ==(ImmutableArray<T> left, ImmutableArray<T> right)
		{
			return left.Equals(right);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003845 File Offset: 0x00001A45
		public static bool operator !=(ImmutableArray<T> left, ImmutableArray<T> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003854 File Offset: 0x00001A54
		public static bool operator ==(ImmutableArray<T>? left, ImmutableArray<T>? right)
		{
			return left.GetValueOrDefault().Equals(right.GetValueOrDefault());
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003878 File Offset: 0x00001A78
		public static bool operator !=(ImmutableArray<T>? left, ImmutableArray<T>? right)
		{
			return !left.GetValueOrDefault().Equals(right.GetValueOrDefault());
		}

		// Token: 0x17000028 RID: 40
		public T this[int index]
		{
			get
			{
				return this.array[index];
			}
		}

		// Token: 0x17000029 RID: 41
		T IList<!0>.this[int index]
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000038D6 File Offset: 0x00001AD6
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000038D9 File Offset: 0x00001AD9
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000038E4 File Offset: 0x00001AE4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public int Length
		{
			get
			{
				return this.array.Length;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000038F0 File Offset: 0x00001AF0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int ICollection<!0>.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003914 File Offset: 0x00001B14
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int IReadOnlyCollection<!0>.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		// Token: 0x1700002F RID: 47
		T IReadOnlyList<!0>.this[int index]
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000395B File Offset: 0x00001B5B
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsDefault
		{
			get
			{
				return this.array == null;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003968 File Offset: 0x00001B68
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public bool IsDefaultOrEmpty
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				return immutableArray.array == null || immutableArray.array.Length == 0;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003990 File Offset: 0x00001B90
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		Array IImmutableArray.Array
		{
			get
			{
				return this.array;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003998 File Offset: 0x00001B98
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				if (!immutableArray.IsDefault)
				{
					return string.Format(CultureInfo.CurrentCulture, "Length = {0}", new object[]
					{
						immutableArray.Length
					});
				}
				return "Uninitialized";
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000039E0 File Offset: 0x00001BE0
		public int IndexOf(T item)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, 0, immutableArray.Length, EqualityComparer<T>.Default);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003A0C File Offset: 0x00001C0C
		public int IndexOf(T item, int startIndex, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, startIndex, immutableArray.Length - startIndex, equalityComparer);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003A34 File Offset: 0x00001C34
		public int IndexOf(T item, int startIndex)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.IndexOf(item, startIndex, immutableArray.Length - startIndex, EqualityComparer<T>.Default);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003A5F File Offset: 0x00001C5F
		public int IndexOf(T item, int startIndex, int count)
		{
			return this.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003A70 File Offset: 0x00001C70
		public int IndexOf(T item, int startIndex, int count, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			if (count == 0 && startIndex == 0)
			{
				return -1;
			}
			Requires.Range(startIndex >= 0 && startIndex < immutableArray.Length, "startIndex", null);
			Requires.Range(count >= 0 && startIndex + count <= immutableArray.Length, "count", null);
			if (equalityComparer == EqualityComparer<T>.Default)
			{
				return Array.IndexOf<T>(immutableArray.array, item, startIndex, count);
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (equalityComparer.Equals(immutableArray.array[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003B1C File Offset: 0x00001D1C
		public int LastIndexOf(T item)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0)
			{
				return -1;
			}
			return immutableArray.LastIndexOf(item, immutableArray.Length - 1, immutableArray.Length, EqualityComparer<T>.Default);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003B58 File Offset: 0x00001D58
		public int LastIndexOf(T item, int startIndex)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0 && startIndex == 0)
			{
				return -1;
			}
			return immutableArray.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003B8B File Offset: 0x00001D8B
		public int LastIndexOf(T item, int startIndex, int count)
		{
			return this.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003B9C File Offset: 0x00001D9C
		public int LastIndexOf(T item, int startIndex, int count, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			if (startIndex == 0 && count == 0)
			{
				return -1;
			}
			Requires.Range(startIndex >= 0 && startIndex < immutableArray.Length, "startIndex", null);
			Requires.Range(count >= 0 && startIndex - count + 1 >= 0, "count", null);
			if (equalityComparer == EqualityComparer<T>.Default)
			{
				return Array.LastIndexOf<T>(immutableArray.array, item, startIndex, count);
			}
			for (int i = startIndex; i >= startIndex - count + 1; i--)
			{
				if (equalityComparer.Equals(item, immutableArray.array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003C45 File Offset: 0x00001E45
		public bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003C54 File Offset: 0x00001E54
		public void CopyTo(T[] destination)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, 0, destination, 0, immutableArray.Length);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003C84 File Offset: 0x00001E84
		public void CopyTo(T[] destination, int destinationIndex)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, 0, destination, destinationIndex, immutableArray.Length);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Array.Copy(immutableArray.array, sourceIndex, destination, destinationIndex, length);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003CE0 File Offset: 0x00001EE0
		public ImmutableArray<T> Insert(int index, T item)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			if (immutableArray.Length == 0)
			{
				return ImmutableArray.Create<T>(item);
			}
			T[] array = new T[immutableArray.Length + 1];
			Array.Copy(immutableArray.array, 0, array, 0, index);
			array[index] = item;
			Array.Copy(immutableArray.array, index, array, index + 1, immutableArray.Length - index);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003D70 File Offset: 0x00001F70
		public ImmutableArray<T> InsertRange(int index, IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			Requires.NotNull<IEnumerable<T>>(items, "items");
			if (immutableArray.Length == 0)
			{
				return ImmutableArray.CreateRange<T>(items);
			}
			int count = ImmutableExtensions.GetCount<T>(ref items);
			if (count == 0)
			{
				return immutableArray;
			}
			T[] array = new T[immutableArray.Length + count];
			Array.Copy(immutableArray.array, 0, array, 0, index);
			int num = index;
			foreach (T t in items)
			{
				array[num++] = t;
			}
			Array.Copy(immutableArray.array, index, array, index + count, immutableArray.Length - index);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003E58 File Offset: 0x00002058
		public ImmutableArray<T> InsertRange(int index, ImmutableArray<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			ImmutableArray<T>.ThrowNullRefIfNotInitialized(items);
			Requires.Range(index >= 0 && index <= immutableArray.Length, "index", null);
			if (immutableArray.IsEmpty)
			{
				return new ImmutableArray<T>(items.array);
			}
			if (items.IsEmpty)
			{
				return new ImmutableArray<T>(immutableArray.array);
			}
			return immutableArray.InsertRange(index, items.array);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003ED0 File Offset: 0x000020D0
		public ImmutableArray<T> Add(T item)
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.Length == 0)
			{
				return ImmutableArray.Create<T>(item);
			}
			return immutableArray.Insert(immutableArray.Length, item);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003F04 File Offset: 0x00002104
		public ImmutableArray<T> AddRange(IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.InsertRange(immutableArray.Length, items);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003F28 File Offset: 0x00002128
		public ImmutableArray<T> AddRange(ImmutableArray<T> items)
		{
			ImmutableArray<T> result = this;
			result.ThrowNullRefIfNotInitialized();
			ImmutableArray<T>.ThrowNullRefIfNotInitialized(items);
			if (result.IsEmpty)
			{
				return new ImmutableArray<T>(items.array);
			}
			if (items.IsEmpty)
			{
				return result;
			}
			return result.AddRange(items.array);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003F78 File Offset: 0x00002178
		public ImmutableArray<T> SetItem(int index, T item)
		{
			ImmutableArray<T> immutableArray = this;
			Requires.Range(index >= 0 && index < immutableArray.Length, "index", null);
			T[] array = new T[immutableArray.Length];
			Array.Copy(immutableArray.array, array, immutableArray.Length);
			array[index] = item;
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003FD6 File Offset: 0x000021D6
		public ImmutableArray<T> Replace(T oldValue, T newValue)
		{
			return this.Replace(oldValue, newValue, EqualityComparer<T>.Default);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003FE8 File Offset: 0x000021E8
		public ImmutableArray<T> Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			int num = immutableArray.IndexOf(oldValue, equalityComparer);
			if (num < 0)
			{
				throw new ArgumentException(SR.CannotFindOldValue, "oldValue");
			}
			return immutableArray.SetItem(num, newValue);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004027 File Offset: 0x00002227
		public ImmutableArray<T> Remove(T item)
		{
			return this.Remove(item, EqualityComparer<T>.Default);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004038 File Offset: 0x00002238
		public ImmutableArray<T> Remove(T item, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			int num = immutableArray.IndexOf(item, equalityComparer);
			if (num >= 0)
			{
				return immutableArray.RemoveAt(num);
			}
			return new ImmutableArray<T>(immutableArray.array);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004079 File Offset: 0x00002279
		public ImmutableArray<T> RemoveAt(int index)
		{
			return this.RemoveRange(index, 1);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004084 File Offset: 0x00002284
		public ImmutableArray<T> RemoveRange(int index, int length)
		{
			ImmutableArray<T> immutableArray = this;
			Requires.Range(index >= 0 && index < immutableArray.Length, "index", null);
			Requires.Range(length >= 0 && index + length <= immutableArray.Length, "length", null);
			T[] array = new T[immutableArray.Length - length];
			Array.Copy(immutableArray.array, 0, array, 0, index);
			Array.Copy(immutableArray.array, index + length, array, index, immutableArray.Length - index - length);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004114 File Offset: 0x00002314
		public ImmutableArray<T> RemoveRange(IEnumerable<T> items)
		{
			return this.RemoveRange(items, EqualityComparer<T>.Default);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004124 File Offset: 0x00002324
		public ImmutableArray<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<IEnumerable<T>>(items, "items");
			Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
			SortedSet<int> sortedSet = new SortedSet<int>();
			foreach (T item in items)
			{
				int num = immutableArray.IndexOf(item, equalityComparer);
				while (num >= 0 && !sortedSet.Add(num) && num + 1 < immutableArray.Length)
				{
					num = immutableArray.IndexOf(item, num + 1, equalityComparer);
				}
			}
			return immutableArray.RemoveAtRange(sortedSet);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000041D4 File Offset: 0x000023D4
		public ImmutableArray<T> RemoveRange(ImmutableArray<T> items)
		{
			return this.RemoveRange(items.array);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000041E2 File Offset: 0x000023E2
		public ImmutableArray<T> RemoveRange(ImmutableArray<T> items, IEqualityComparer<T> equalityComparer)
		{
			return this.RemoveRange(items.array, equalityComparer);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000041F4 File Offset: 0x000023F4
		public ImmutableArray<T> RemoveAll(Predicate<T> match)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<Predicate<T>>(match, "match");
			if (immutableArray.IsEmpty)
			{
				return new ImmutableArray<T>(immutableArray.array);
			}
			List<int> list = null;
			for (int i = 0; i < immutableArray.array.Length; i++)
			{
				if (match(immutableArray.array[i]))
				{
					if (list == null)
					{
						list = new List<int>();
					}
					list.Add(i);
				}
			}
			if (list == null)
			{
				return immutableArray;
			}
			return immutableArray.RemoveAtRange(list);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004275 File Offset: 0x00002475
		public ImmutableArray<T> Clear()
		{
			return ImmutableArray<T>.Empty;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000427C File Offset: 0x0000247C
		public ImmutableArray<T> Sort()
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(0, immutableArray.Length, Comparer<T>.Default);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000042A4 File Offset: 0x000024A4
		public ImmutableArray<T> Sort(IComparer<T> comparer)
		{
			ImmutableArray<T> immutableArray = this;
			return immutableArray.Sort(0, immutableArray.Length, comparer);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000042C8 File Offset: 0x000024C8
		public ImmutableArray<T> Sort(int index, int count, IComparer<T> comparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.Range(index >= 0, "index", null);
			Requires.Range(count >= 0 && index + count <= immutableArray.Length, "count", null);
			if (comparer == null)
			{
				comparer = Comparer<T>.Default;
			}
			if (count > 1)
			{
				bool flag = false;
				for (int i = index + 1; i < index + count; i++)
				{
					if (comparer.Compare(immutableArray.array[i - 1], immutableArray.array[i]) > 0)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					T[] array = new T[immutableArray.Length];
					Array.Copy(immutableArray.array, array, immutableArray.Length);
					Array.Sort<T>(array, index, count, comparer);
					return new ImmutableArray<T>(array);
				}
			}
			return new ImmutableArray<T>(immutableArray.array);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000439C File Offset: 0x0000259C
		public ImmutableArray<T>.Builder ToBuilder()
		{
			ImmutableArray<T> items = this;
			if (items.Length == 0)
			{
				return new ImmutableArray<T>.Builder();
			}
			ImmutableArray<T>.Builder builder = new ImmutableArray<T>.Builder(items.Length);
			builder.AddRange(items);
			return builder;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000043D4 File Offset: 0x000025D4
		public ImmutableArray<T>.Enumerator GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			return new ImmutableArray<T>.Enumerator(immutableArray.array);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000043FC File Offset: 0x000025FC
		public override int GetHashCode()
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.array != null)
			{
				return immutableArray.array.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004425 File Offset: 0x00002625
		public override bool Equals(object obj)
		{
			return obj is ImmutableArray<T> && this.Equals((ImmutableArray<T>)obj);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000443D File Offset: 0x0000263D
		public bool Equals(ImmutableArray<T> other)
		{
			return this.array == other.array;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000444D File Offset: 0x0000264D
		public static ImmutableArray<T> CastUp<TDerived>(ImmutableArray<TDerived> items) where TDerived : class, T
		{
			return new ImmutableArray<T>(items.array);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000445A File Offset: 0x0000265A
		public ImmutableArray<TOther> CastArray<TOther>() where TOther : class
		{
			return new ImmutableArray<TOther>((TOther[])this.array);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000446C File Offset: 0x0000266C
		public ImmutableArray<TOther> As<TOther>() where TOther : class
		{
			return new ImmutableArray<TOther>(this.array as TOther[]);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004480 File Offset: 0x00002680
		public IEnumerable<TResult> OfType<TResult>()
		{
			ImmutableArray<T> immutableArray = this;
			if (immutableArray.array == null || immutableArray.array.Length == 0)
			{
				return Enumerable.Empty<TResult>();
			}
			return immutableArray.array.OfType<TResult>();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00002D65 File Offset: 0x00000F65
		void IList<!0>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00002D65 File Offset: 0x00000F65
		void IList<!0>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00002D65 File Offset: 0x00000F65
		void ICollection<!0>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00002D65 File Offset: 0x00000F65
		void ICollection<!0>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00002D65 File Offset: 0x00000F65
		bool ICollection<!0>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000044DC File Offset: 0x000026DC
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return ImmutableArray<T>.EnumeratorObject.Create(immutableArray.array);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004504 File Offset: 0x00002704
		IEnumerator IEnumerable.GetEnumerator()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return ImmutableArray<T>.EnumeratorObject.Create(immutableArray.array);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000452C File Offset: 0x0000272C
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Clear()
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Clear();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004554 File Offset: 0x00002754
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Add(T value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Add(value);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000457C File Offset: 0x0000277C
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.AddRange(IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.AddRange(items);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000045A4 File Offset: 0x000027A4
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Insert(int index, T element)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Insert(index, element);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000045D0 File Offset: 0x000027D0
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.InsertRange(int index, IEnumerable<T> items)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.InsertRange(index, items);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000045FC File Offset: 0x000027FC
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Remove(T value, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Remove(value, equalityComparer);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004628 File Offset: 0x00002828
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveAll(Predicate<T> match)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveAll(match);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004650 File Offset: 0x00002850
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveRange(items, equalityComparer);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000467C File Offset: 0x0000287C
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveRange(int index, int count)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveRange(index, count);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000046A8 File Offset: 0x000028A8
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.RemoveAt(int index)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.RemoveAt(index);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000046D0 File Offset: 0x000028D0
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.SetItem(int index, T value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.SetItem(index, value);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000046FC File Offset: 0x000028FC
		[ExcludeFromCodeCoverage]
		IImmutableList<T> IImmutableList<!0>.Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Replace(oldValue, newValue, equalityComparer);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00002D65 File Offset: 0x00000F65
		[ExcludeFromCodeCoverage]
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00002D65 File Offset: 0x00000F65
		[ExcludeFromCodeCoverage]
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004734 File Offset: 0x00002934
		[ExcludeFromCodeCoverage]
		bool IList.Contains(object value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.Contains((T)((object)value));
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000475C File Offset: 0x0000295C
		[ExcludeFromCodeCoverage]
		int IList.IndexOf(object value)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			return immutableArray.IndexOf((T)((object)value));
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00002D65 File Offset: 0x00000F65
		[ExcludeFromCodeCoverage]
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000038D6 File Offset: 0x00001AD6
		[ExcludeFromCodeCoverage]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000038D6 File Offset: 0x00001AD6
		[ExcludeFromCodeCoverage]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004794 File Offset: 0x00002994
		[ExcludeFromCodeCoverage]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		int ICollection.Count
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray.Length;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000038D6 File Offset: 0x00001AD6
		[ExcludeFromCodeCoverage]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00002D65 File Offset: 0x00000F65
		[ExcludeFromCodeCoverage]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00002D65 File Offset: 0x00000F65
		[ExcludeFromCodeCoverage]
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00002D65 File Offset: 0x00000F65
		[ExcludeFromCodeCoverage]
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000039 RID: 57
		[ExcludeFromCodeCoverage]
		object IList.this[int index]
		{
			get
			{
				ImmutableArray<T> immutableArray = this;
				immutableArray.ThrowInvalidOperationIfNotInitialized();
				return immutableArray[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004800 File Offset: 0x00002A00
		[ExcludeFromCodeCoverage]
		void ICollection.CopyTo(Array array, int index)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowInvalidOperationIfNotInitialized();
			Array.Copy(immutableArray.array, 0, array, index, immutableArray.Length);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004830 File Offset: 0x00002A30
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			Array array = other as Array;
			if (array == null)
			{
				IImmutableArray immutableArray2 = other as IImmutableArray;
				if (immutableArray2 != null)
				{
					if (immutableArray.array == null && immutableArray2.Array == null)
					{
						return true;
					}
					if (immutableArray.array == null)
					{
						return false;
					}
					array = immutableArray2.Array;
				}
			}
			return immutableArray.array.Equals(array, comparer);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004888 File Offset: 0x00002A88
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			IStructuralEquatable structuralEquatable = immutableArray.array;
			if (structuralEquatable == null)
			{
				return immutableArray.GetHashCode();
			}
			return structuralEquatable.GetHashCode(comparer);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000048BC File Offset: 0x00002ABC
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			ImmutableArray<T> immutableArray = this;
			Array array = other as Array;
			if (array == null)
			{
				IImmutableArray immutableArray2 = other as IImmutableArray;
				if (immutableArray2 != null)
				{
					if (immutableArray.array == null && immutableArray2.Array == null)
					{
						return 0;
					}
					if (immutableArray.array == null ^ immutableArray2.Array == null)
					{
						throw new ArgumentException(SR.ArrayInitializedStateNotEqual, "other");
					}
					array = immutableArray2.Array;
				}
			}
			if (array != null)
			{
				return immutableArray.array.CompareTo(array, comparer);
			}
			throw new ArgumentException(SR.ArrayLengthsNotEqual, "other");
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004942 File Offset: 0x00002B42
		internal void ThrowNullRefIfNotInitialized()
		{
			int num = this.array.Length;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000494D File Offset: 0x00002B4D
		private void ThrowInvalidOperationIfNotInitialized()
		{
			if (this.IsDefault)
			{
				throw new InvalidOperationException(SR.InvalidOperationOnDefaultArray);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004962 File Offset: 0x00002B62
		void IImmutableArray.ThrowInvalidOperationIfNotInitialized()
		{
			this.ThrowInvalidOperationIfNotInitialized();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000496C File Offset: 0x00002B6C
		private ImmutableArray<T> RemoveAtRange(ICollection<int> indexesToRemove)
		{
			ImmutableArray<T> immutableArray = this;
			immutableArray.ThrowNullRefIfNotInitialized();
			Requires.NotNull<ICollection<int>>(indexesToRemove, "indexesToRemove");
			if (indexesToRemove.Count == 0)
			{
				return new ImmutableArray<T>(immutableArray.array);
			}
			T[] array = new T[immutableArray.Length - indexesToRemove.Count];
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			foreach (int num4 in indexesToRemove)
			{
				int num5 = (num3 == -1) ? num4 : (num4 - num3 - 1);
				Array.Copy(immutableArray.array, num + num2, array, num, num5);
				num2++;
				num += num5;
				num3 = num4;
			}
			Array.Copy(immutableArray.array, num + num2, array, num, immutableArray.Length - (num + num2));
			return new ImmutableArray<T>(array);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004A50 File Offset: 0x00002C50
		private static void ThrowNullRefIfNotInitialized(ImmutableArray<T> array)
		{
			array.ThrowNullRefIfNotInitialized();
		}

		// Token: 0x04000010 RID: 16
		public static readonly ImmutableArray<T> Empty = new ImmutableArray<T>(new T[0]);

		// Token: 0x04000011 RID: 17
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		internal T[] array;

		// Token: 0x02000044 RID: 68
		[DebuggerDisplay("Count = {Count}")]
		[DebuggerTypeProxy(typeof(ImmutableArrayBuilderDebuggerProxy<>))]
		public sealed class Builder : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<!0>, IReadOnlyCollection<!0>
		{
			// Token: 0x0600039D RID: 925 RVA: 0x00009B9F File Offset: 0x00007D9F
			internal Builder(int capacity)
			{
				Requires.Range(capacity >= 0, "capacity", null);
				this._elements = new T[capacity];
				this._count = 0;
			}

			// Token: 0x0600039E RID: 926 RVA: 0x00009BCC File Offset: 0x00007DCC
			internal Builder() : this(8)
			{
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x0600039F RID: 927 RVA: 0x00009BD5 File Offset: 0x00007DD5
			// (set) Token: 0x060003A0 RID: 928 RVA: 0x00009BE0 File Offset: 0x00007DE0
			public int Capacity
			{
				get
				{
					return this._elements.Length;
				}
				set
				{
					if (value < this._count)
					{
						throw new ArgumentException(SR.CapacityMustBeGreaterThanOrEqualToCount, "value");
					}
					if (value != this._elements.Length)
					{
						if (value > 0)
						{
							T[] array = new T[value];
							if (this._count > 0)
							{
								Array.Copy(this._elements, 0, array, 0, this._count);
							}
							this._elements = array;
							return;
						}
						this._elements = ImmutableArray<T>.Empty.array;
					}
				}
			}

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060003A1 RID: 929 RVA: 0x00009C51 File Offset: 0x00007E51
			// (set) Token: 0x060003A2 RID: 930 RVA: 0x00009C5C File Offset: 0x00007E5C
			public int Count
			{
				get
				{
					return this._count;
				}
				set
				{
					Requires.Range(value >= 0, "value", null);
					if (value < this._count)
					{
						if (this._count - value > 64)
						{
							Array.Clear(this._elements, value, this._count - value);
						}
						else
						{
							for (int i = value; i < this.Count; i++)
							{
								this._elements[i] = default(T);
							}
						}
					}
					else if (value > this._count)
					{
						this.EnsureCapacity(value);
					}
					this._count = value;
				}
			}

			// Token: 0x170000A6 RID: 166
			public T this[int index]
			{
				get
				{
					if (index >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
					return this._elements[index];
				}
				set
				{
					if (index >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
					this._elements[index] = value;
				}
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060003A5 RID: 933 RVA: 0x000020FC File Offset: 0x000002FC
			bool ICollection<!0>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060003A6 RID: 934 RVA: 0x00009D23 File Offset: 0x00007F23
			public ImmutableArray<T> ToImmutable()
			{
				if (this.Count == 0)
				{
					return ImmutableArray<T>.Empty;
				}
				return new ImmutableArray<T>(this.ToArray());
			}

			// Token: 0x060003A7 RID: 935 RVA: 0x00009D3E File Offset: 0x00007F3E
			public ImmutableArray<T> MoveToImmutable()
			{
				if (this.Capacity != this.Count)
				{
					throw new InvalidOperationException(SR.CapacityMustEqualCountOnMove);
				}
				T[] elements = this._elements;
				this._elements = ImmutableArray<T>.Empty.array;
				this._count = 0;
				return new ImmutableArray<T>(elements);
			}

			// Token: 0x060003A8 RID: 936 RVA: 0x00009D7B File Offset: 0x00007F7B
			public void Clear()
			{
				this.Count = 0;
			}

			// Token: 0x060003A9 RID: 937 RVA: 0x00009D84 File Offset: 0x00007F84
			public void Insert(int index, T item)
			{
				Requires.Range(index >= 0 && index <= this.Count, "index", null);
				this.EnsureCapacity(this.Count + 1);
				if (index < this.Count)
				{
					Array.Copy(this._elements, index, this._elements, index + 1, this.Count - index);
				}
				this._count++;
				this._elements[index] = item;
			}

			// Token: 0x060003AA RID: 938 RVA: 0x00009E00 File Offset: 0x00008000
			public void Add(T item)
			{
				this.EnsureCapacity(this.Count + 1);
				T[] elements = this._elements;
				int count = this._count;
				this._count = count + 1;
				elements[count] = item;
			}

			// Token: 0x060003AB RID: 939 RVA: 0x00009E38 File Offset: 0x00008038
			public void AddRange(IEnumerable<T> items)
			{
				Requires.NotNull<IEnumerable<T>>(items, "items");
				int num;
				if (items.TryGetCount(out num))
				{
					this.EnsureCapacity(this.Count + num);
				}
				foreach (T item in items)
				{
					this.Add(item);
				}
			}

			// Token: 0x060003AC RID: 940 RVA: 0x00009EA4 File Offset: 0x000080A4
			public void AddRange(params T[] items)
			{
				Requires.NotNull<T[]>(items, "items");
				int count = this.Count;
				this.Count += items.Length;
				T[] elements = this._elements;
				for (int i = 0; i < items.Length; i++)
				{
					elements[count + i] = items[i];
				}
			}

			// Token: 0x060003AD RID: 941 RVA: 0x00009EF8 File Offset: 0x000080F8
			public void AddRange<TDerived>(TDerived[] items) where TDerived : T
			{
				Requires.NotNull<TDerived[]>(items, "items");
				int count = this.Count;
				this.Count += items.Length;
				T[] elements = this._elements;
				for (int i = 0; i < items.Length; i++)
				{
					elements[count + i] = (T)((object)items[i]);
				}
			}

			// Token: 0x060003AE RID: 942 RVA: 0x00009F58 File Offset: 0x00008158
			public void AddRange(T[] items, int length)
			{
				Requires.NotNull<T[]>(items, "items");
				Requires.Range(length >= 0, "length", null);
				int count = this.Count;
				this.Count += length;
				T[] elements = this._elements;
				for (int i = 0; i < length; i++)
				{
					elements[count + i] = items[i];
				}
			}

			// Token: 0x060003AF RID: 943 RVA: 0x00009FBA File Offset: 0x000081BA
			public void AddRange(ImmutableArray<T> items)
			{
				this.AddRange(items, items.Length);
			}

			// Token: 0x060003B0 RID: 944 RVA: 0x00009FCA File Offset: 0x000081CA
			public void AddRange(ImmutableArray<T> items, int length)
			{
				Requires.Range(length >= 0, "length", null);
				if (items.array != null)
				{
					this.AddRange(items.array, length);
				}
			}

			// Token: 0x060003B1 RID: 945 RVA: 0x00009FF3 File Offset: 0x000081F3
			public void AddRange<TDerived>(ImmutableArray<TDerived> items) where TDerived : T
			{
				if (items.array != null)
				{
					this.AddRange<TDerived>(items.array);
				}
			}

			// Token: 0x060003B2 RID: 946 RVA: 0x0000A009 File Offset: 0x00008209
			public void AddRange(ImmutableArray<T>.Builder items)
			{
				Requires.NotNull<ImmutableArray<T>.Builder>(items, "items");
				this.AddRange(items._elements, items.Count);
			}

			// Token: 0x060003B3 RID: 947 RVA: 0x0000A028 File Offset: 0x00008228
			public void AddRange<TDerived>(ImmutableArray<TDerived>.Builder items) where TDerived : T
			{
				Requires.NotNull<ImmutableArray<TDerived>.Builder>(items, "items");
				this.AddRange<TDerived>(items._elements, items.Count);
			}

			// Token: 0x060003B4 RID: 948 RVA: 0x0000A048 File Offset: 0x00008248
			public bool Remove(T element)
			{
				int num = this.IndexOf(element);
				if (num >= 0)
				{
					this.RemoveAt(num);
					return true;
				}
				return false;
			}

			// Token: 0x060003B5 RID: 949 RVA: 0x0000A06C File Offset: 0x0000826C
			public void RemoveAt(int index)
			{
				Requires.Range(index >= 0 && index < this.Count, "index", null);
				if (index < this.Count - 1)
				{
					Array.Copy(this._elements, index + 1, this._elements, index, this.Count - index - 1);
				}
				int count = this.Count;
				this.Count = count - 1;
			}

			// Token: 0x060003B6 RID: 950 RVA: 0x0000A0CE File Offset: 0x000082CE
			public bool Contains(T item)
			{
				return this.IndexOf(item) >= 0;
			}

			// Token: 0x060003B7 RID: 951 RVA: 0x0000A0E0 File Offset: 0x000082E0
			public T[] ToArray()
			{
				T[] array = new T[this.Count];
				T[] elements = this._elements;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = elements[i];
				}
				return array;
			}

			// Token: 0x060003B8 RID: 952 RVA: 0x0000A120 File Offset: 0x00008320
			public void CopyTo(T[] array, int index)
			{
				Requires.NotNull<T[]>(array, "array");
				Requires.Range(index >= 0 && index + this.Count <= array.Length, "start", null);
				Array.Copy(this._elements, 0, array, index, this.Count);
			}

			// Token: 0x060003B9 RID: 953 RVA: 0x0000A170 File Offset: 0x00008370
			private void EnsureCapacity(int capacity)
			{
				if (this._elements.Length < capacity)
				{
					int newSize = Math.Max(this._elements.Length * 2, capacity);
					Array.Resize<T>(ref this._elements, newSize);
				}
			}

			// Token: 0x060003BA RID: 954 RVA: 0x0000A1A5 File Offset: 0x000083A5
			public int IndexOf(T item)
			{
				return this.IndexOf(item, 0, this._count, EqualityComparer<T>.Default);
			}

			// Token: 0x060003BB RID: 955 RVA: 0x0000A1BA File Offset: 0x000083BA
			public int IndexOf(T item, int startIndex)
			{
				return this.IndexOf(item, startIndex, this.Count - startIndex, EqualityComparer<T>.Default);
			}

			// Token: 0x060003BC RID: 956 RVA: 0x0000A1D1 File Offset: 0x000083D1
			public int IndexOf(T item, int startIndex, int count)
			{
				return this.IndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x060003BD RID: 957 RVA: 0x0000A1E4 File Offset: 0x000083E4
			public int IndexOf(T item, int startIndex, int count, IEqualityComparer<T> equalityComparer)
			{
				Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
				if (count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				Requires.Range(count >= 0 && startIndex + count <= this.Count, "count", null);
				if (equalityComparer == EqualityComparer<T>.Default)
				{
					return Array.IndexOf<T>(this._elements, item, startIndex, count);
				}
				for (int i = startIndex; i < startIndex + count; i++)
				{
					if (equalityComparer.Equals(this._elements[i], item))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x060003BE RID: 958 RVA: 0x0000A27F File Offset: 0x0000847F
			public int LastIndexOf(T item)
			{
				if (this.Count == 0)
				{
					return -1;
				}
				return this.LastIndexOf(item, this.Count - 1, this.Count, EqualityComparer<T>.Default);
			}

			// Token: 0x060003BF RID: 959 RVA: 0x0000A2A5 File Offset: 0x000084A5
			public int LastIndexOf(T item, int startIndex)
			{
				if (this.Count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				return this.LastIndexOf(item, startIndex, startIndex + 1, EqualityComparer<T>.Default);
			}

			// Token: 0x060003C0 RID: 960 RVA: 0x0000A2DF File Offset: 0x000084DF
			public int LastIndexOf(T item, int startIndex, int count)
			{
				return this.LastIndexOf(item, startIndex, count, EqualityComparer<T>.Default);
			}

			// Token: 0x060003C1 RID: 961 RVA: 0x0000A2F0 File Offset: 0x000084F0
			public int LastIndexOf(T item, int startIndex, int count, IEqualityComparer<T> equalityComparer)
			{
				Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "equalityComparer");
				if (count == 0 && startIndex == 0)
				{
					return -1;
				}
				Requires.Range(startIndex >= 0 && startIndex < this.Count, "startIndex", null);
				Requires.Range(count >= 0 && startIndex - count + 1 >= 0, "count", null);
				if (equalityComparer == EqualityComparer<T>.Default)
				{
					return Array.LastIndexOf<T>(this._elements, item, startIndex, count);
				}
				for (int i = startIndex; i >= startIndex - count + 1; i--)
				{
					if (equalityComparer.Equals(item, this._elements[i]))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x060003C2 RID: 962 RVA: 0x0000A38A File Offset: 0x0000858A
			public void Reverse()
			{
				Array.Reverse(this._elements, 0, this._count);
			}

			// Token: 0x060003C3 RID: 963 RVA: 0x0000A39E File Offset: 0x0000859E
			public void Sort()
			{
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this.Count, Comparer<T>.Default);
				}
			}

			// Token: 0x060003C4 RID: 964 RVA: 0x0000A3C0 File Offset: 0x000085C0
			public void Sort(IComparer<T> comparer)
			{
				if (this.Count > 1)
				{
					Array.Sort<T>(this._elements, 0, this._count, comparer);
				}
			}

			// Token: 0x060003C5 RID: 965 RVA: 0x0000A3E0 File Offset: 0x000085E0
			public void Sort(int index, int count, IComparer<T> comparer)
			{
				Requires.Range(index >= 0, "index", null);
				Requires.Range(count >= 0 && index + count <= this.Count, "count", null);
				if (count > 1)
				{
					Array.Sort<T>(this._elements, index, count, comparer);
				}
			}

			// Token: 0x060003C6 RID: 966 RVA: 0x0000A431 File Offset: 0x00008631
			public IEnumerator<T> GetEnumerator()
			{
				int num;
				for (int i = 0; i < this.Count; i = num + 1)
				{
					yield return this[i];
					num = i;
				}
				yield break;
			}

			// Token: 0x060003C7 RID: 967 RVA: 0x0000A440 File Offset: 0x00008640
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060003C8 RID: 968 RVA: 0x0000A440 File Offset: 0x00008640
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060003C9 RID: 969 RVA: 0x0000A450 File Offset: 0x00008650
			private void AddRange<TDerived>(TDerived[] items, int length) where TDerived : T
			{
				this.EnsureCapacity(this.Count + length);
				int count = this.Count;
				this.Count += length;
				T[] elements = this._elements;
				for (int i = 0; i < length; i++)
				{
					elements[count + i] = (T)((object)items[i]);
				}
			}

			// Token: 0x04000063 RID: 99
			private T[] _elements;

			// Token: 0x04000064 RID: 100
			private int _count;
		}

		// Token: 0x02000045 RID: 69
		public struct Enumerator
		{
			// Token: 0x060003CA RID: 970 RVA: 0x0000A4AD File Offset: 0x000086AD
			internal Enumerator(T[] array)
			{
				this._array = array;
				this._index = -1;
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x060003CB RID: 971 RVA: 0x0000A4BD File Offset: 0x000086BD
			public T Current
			{
				get
				{
					return this._array[this._index];
				}
			}

			// Token: 0x060003CC RID: 972 RVA: 0x0000A4D0 File Offset: 0x000086D0
			public bool MoveNext()
			{
				int num = this._index + 1;
				this._index = num;
				return num < this._array.Length;
			}

			// Token: 0x04000065 RID: 101
			private readonly T[] _array;

			// Token: 0x04000066 RID: 102
			private int _index;
		}

		// Token: 0x02000046 RID: 70
		private class EnumeratorObject : IEnumerator<!0>, IEnumerator, IDisposable
		{
			// Token: 0x060003CD RID: 973 RVA: 0x0000A4F8 File Offset: 0x000086F8
			private EnumeratorObject(T[] array)
			{
				this._index = -1;
				this._array = array;
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x060003CE RID: 974 RVA: 0x0000A50E File Offset: 0x0000870E
			public T Current
			{
				get
				{
					if (this._index < this._array.Length)
					{
						return this._array[this._index];
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x060003CF RID: 975 RVA: 0x0000A537 File Offset: 0x00008737
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060003D0 RID: 976 RVA: 0x0000A544 File Offset: 0x00008744
			public bool MoveNext()
			{
				int num = this._index + 1;
				int num2 = this._array.Length;
				if (num <= num2)
				{
					this._index = num;
					return num < num2;
				}
				return false;
			}

			// Token: 0x060003D1 RID: 977 RVA: 0x0000A574 File Offset: 0x00008774
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x060003D2 RID: 978 RVA: 0x0000A57D File Offset: 0x0000877D
			public void Dispose()
			{
			}

			// Token: 0x060003D3 RID: 979 RVA: 0x0000A57F File Offset: 0x0000877F
			internal static IEnumerator<T> Create(T[] array)
			{
				if (array.Length != 0)
				{
					return new ImmutableArray<T>.EnumeratorObject(array);
				}
				return ImmutableArray<T>.EnumeratorObject.s_EmptyEnumerator;
			}

			// Token: 0x04000067 RID: 103
			private static readonly IEnumerator<T> s_EmptyEnumerator = new ImmutableArray<T>.EnumeratorObject(ImmutableArray<T>.Empty.array);

			// Token: 0x04000068 RID: 104
			private readonly T[] _array;

			// Token: 0x04000069 RID: 105
			private int _index;
		}
	}
}
