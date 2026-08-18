using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Dynamic.Utils
{
	// Token: 0x020000D3 RID: 211
	internal class CacheDict<TKey, TValue>
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x000154EC File Offset: 0x000136EC
		internal CacheDict(int size)
		{
			int num = CacheDict<TKey, TValue>.AlignSize(size);
			this.mask = num - 1;
			this.entries = new CacheDict<TKey, TValue>.Entry[num];
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001551B File Offset: 0x0001371B
		private static int AlignSize(int size)
		{
			size--;
			size |= size >> 1;
			size |= size >> 2;
			size |= size >> 4;
			size |= size >> 8;
			size |= size >> 16;
			return size + 1;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001554C File Offset: 0x0001374C
		internal bool TryGetValue(TKey key, out TValue value)
		{
			int hashCode = key.GetHashCode();
			int num = hashCode & this.mask;
			CacheDict<TKey, TValue>.Entry entry = Volatile.Read<CacheDict<TKey, TValue>.Entry>(ref this.entries[num]);
			if (entry != null && entry.hash == hashCode)
			{
				TKey key2 = entry.key;
				if (key2.Equals(key))
				{
					value = entry.value;
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000155C0 File Offset: 0x000137C0
		internal void Add(TKey key, TValue value)
		{
			int hashCode = key.GetHashCode();
			int num = hashCode & this.mask;
			CacheDict<TKey, TValue>.Entry entry = Volatile.Read<CacheDict<TKey, TValue>.Entry>(ref this.entries[num]);
			if (entry != null && entry.hash == hashCode)
			{
				TKey key2 = entry.key;
				if (key2.Equals(key))
				{
					return;
				}
			}
			Volatile.Write<CacheDict<TKey, TValue>.Entry>(ref this.entries[num], new CacheDict<TKey, TValue>.Entry(hashCode, key, value));
		}

		// Token: 0x17000164 RID: 356
		internal TValue this[TKey key]
		{
			get
			{
				TValue result;
				if (this.TryGetValue(key, out result))
				{
					return result;
				}
				throw new KeyNotFoundException();
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x040005C3 RID: 1475
		protected readonly int mask;

		// Token: 0x040005C4 RID: 1476
		protected readonly CacheDict<TKey, TValue>.Entry[] entries;

		// Token: 0x0200031B RID: 795
		internal class Entry
		{
			// Token: 0x06001AEC RID: 6892 RVA: 0x00062D40 File Offset: 0x00060F40
			internal Entry(int hash, TKey key, TValue value)
			{
				this.hash = hash;
				this.key = key;
				this.value = value;
			}

			// Token: 0x04000E50 RID: 3664
			internal readonly int hash;

			// Token: 0x04000E51 RID: 3665
			internal readonly TKey key;

			// Token: 0x04000E52 RID: 3666
			internal readonly TValue value;
		}
	}
}
