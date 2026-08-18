using System;

namespace System.Xml
{
	// Token: 0x02000076 RID: 118
	[__DynamicallyInvokable]
	public class NameTable : XmlNameTable
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x0000F10D File Offset: 0x0000D30D
		[__DynamicallyInvokable]
		public NameTable()
		{
			this.mask = 31;
			this.entries = new NameTable.Entry[this.mask + 1];
			this.marvinHashSeed = MarvinHash.DefaultSeed;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000F13C File Offset: 0x0000D33C
		[__DynamicallyInvokable]
		public override string Add(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key.Length == 0)
			{
				return string.Empty;
			}
			int num = this.ComputeHash32(key);
			for (NameTable.Entry entry = this.entries[num & this.mask]; entry != null; entry = entry.next)
			{
				if (entry.hashCode == num && entry.str.Equals(key))
				{
					return entry.str;
				}
			}
			return this.AddEntry(key, num);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		[__DynamicallyInvokable]
		public override string Add(char[] key, int start, int len)
		{
			if (len == 0)
			{
				return string.Empty;
			}
			if (start >= key.Length || start < 0 || (long)start + (long)len > (long)key.Length)
			{
				throw new IndexOutOfRangeException();
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			int num = this.ComputeHash32(key, start, len);
			for (NameTable.Entry entry = this.entries[num & this.mask]; entry != null; entry = entry.next)
			{
				if (entry.hashCode == num && NameTable.TextEquals(entry.str, key, start, len))
				{
					return entry.str;
				}
			}
			return this.AddEntry(new string(key, start, len), num);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000F244 File Offset: 0x0000D444
		[__DynamicallyInvokable]
		public override string Get(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0)
			{
				return string.Empty;
			}
			int num = this.ComputeHash32(value);
			for (NameTable.Entry entry = this.entries[num & this.mask]; entry != null; entry = entry.next)
			{
				if (entry.hashCode == num && entry.str.Equals(value))
				{
					return entry.str;
				}
			}
			return null;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		[__DynamicallyInvokable]
		public override string Get(char[] key, int start, int len)
		{
			if (len == 0)
			{
				return string.Empty;
			}
			if (start >= key.Length || start < 0 || (long)start + (long)len > (long)key.Length)
			{
				throw new IndexOutOfRangeException();
			}
			if (len < 0)
			{
				return null;
			}
			int num = this.ComputeHash32(key, start, len);
			for (NameTable.Entry entry = this.entries[num & this.mask]; entry != null; entry = entry.next)
			{
				if (entry.hashCode == num && NameTable.TextEquals(entry.str, key, start, len))
				{
					return entry.str;
				}
			}
			return null;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000F330 File Offset: 0x0000D530
		private string AddEntry(string str, int hashCode)
		{
			int num = hashCode & this.mask;
			NameTable.Entry entry = new NameTable.Entry(str, hashCode, this.entries[num]);
			this.entries[num] = entry;
			int num2 = this.count;
			this.count = num2 + 1;
			if (num2 == this.mask)
			{
				this.Grow();
			}
			return entry.str;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000F384 File Offset: 0x0000D584
		private void Grow()
		{
			int num = this.mask * 2 + 1;
			NameTable.Entry[] array = this.entries;
			NameTable.Entry[] array2 = new NameTable.Entry[num + 1];
			foreach (NameTable.Entry entry in array)
			{
				while (entry != null)
				{
					int num2 = entry.hashCode & num;
					NameTable.Entry next = entry.next;
					entry.next = array2[num2];
					array2[num2] = entry;
					entry = next;
				}
			}
			this.entries = array2;
			this.mask = num;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000F3FC File Offset: 0x0000D5FC
		private static bool TextEquals(string str1, char[] str2, int str2Start, int str2Length)
		{
			if (str1.Length != str2Length)
			{
				return false;
			}
			for (int i = 0; i < str1.Length; i++)
			{
				if (str1[i] != str2[str2Start + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000F436 File Offset: 0x0000D636
		private int ComputeHash32(string key)
		{
			return MarvinHash.ComputeHash32(key, this.marvinHashSeed);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000F444 File Offset: 0x0000D644
		private int ComputeHash32(char[] key, int start, int len)
		{
			return MarvinHash.ComputeHash32(key, start, len, this.marvinHashSeed);
		}

		// Token: 0x040001C6 RID: 454
		private NameTable.Entry[] entries;

		// Token: 0x040001C7 RID: 455
		private int count;

		// Token: 0x040001C8 RID: 456
		private int mask;

		// Token: 0x040001C9 RID: 457
		private int hashCodeRandomizer;

		// Token: 0x040001CA RID: 458
		private ulong marvinHashSeed;

		// Token: 0x02000310 RID: 784
		private class Entry
		{
			// Token: 0x06002DB6 RID: 11702 RVA: 0x000EDAAC File Offset: 0x000EBCAC
			internal Entry(string str, int hashCode, NameTable.Entry next)
			{
				this.str = str;
				this.hashCode = hashCode;
				this.next = next;
			}

			// Token: 0x04001476 RID: 5238
			internal string str;

			// Token: 0x04001477 RID: 5239
			internal int hashCode;

			// Token: 0x04001478 RID: 5240
			internal NameTable.Entry next;
		}
	}
}
