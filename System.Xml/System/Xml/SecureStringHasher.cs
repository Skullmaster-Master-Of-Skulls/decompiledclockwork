using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x02000066 RID: 102
	internal class SecureStringHasher : IEqualityComparer<string>
	{
		// Token: 0x0600037F RID: 895 RVA: 0x00011C3B File Offset: 0x00010C3B
		public SecureStringHasher()
		{
			this.hashCodeRandomizer = Environment.TickCount;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00011C4E File Offset: 0x00010C4E
		public SecureStringHasher(int hashCodeRandomizer)
		{
			this.hashCodeRandomizer = hashCodeRandomizer;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00011C5D File Offset: 0x00010C5D
		public int Compare(string x, string y)
		{
			return string.Compare(x, y, StringComparison.Ordinal);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011C67 File Offset: 0x00010C67
		public bool Equals(string x, string y)
		{
			return string.Equals(x, y, StringComparison.Ordinal);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00011C74 File Offset: 0x00010C74
		public int GetHashCode(string key)
		{
			int num = this.hashCodeRandomizer;
			for (int i = 0; i < key.Length; i++)
			{
				num += (num << 7 ^ (int)key[i]);
			}
			num -= num >> 17;
			num -= num >> 11;
			return num - (num >> 5);
		}

		// Token: 0x040005C3 RID: 1475
		private int hashCodeRandomizer;
	}
}
