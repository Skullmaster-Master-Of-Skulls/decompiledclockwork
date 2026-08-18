using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x02000324 RID: 804
	internal sealed class ByValueEqualityComparer : IEqualityComparer<object>
	{
		// Token: 0x06001BBB RID: 7099 RVA: 0x000884F8 File Offset: 0x000866F8
		private ByValueEqualityComparer()
		{
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x00088500 File Offset: 0x00086700
		public bool Equals(object x, object y)
		{
			if (object.Equals(x, y))
			{
				return true;
			}
			byte[] array = x as byte[];
			byte[] array2 = y as byte[];
			return array != null && array2 != null && ByValueEqualityComparer.CompareBinaryValues(array, array2);
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x00088538 File Offset: 0x00086738
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			byte[] array = obj as byte[];
			if (array != null)
			{
				return ByValueEqualityComparer.ComputeBinaryHashCode(array);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x00088564 File Offset: 0x00086764
		internal static int ComputeBinaryHashCode(byte[] bytes)
		{
			int num = 0;
			int i = 0;
			int num2 = Math.Min(bytes.Length, 7);
			while (i < num2)
			{
				num = (num << 5 ^ (int)bytes[i]);
				i++;
			}
			return num;
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x00088594 File Offset: 0x00086794
		internal static bool CompareBinaryValues(byte[] first, byte[] second)
		{
			if (first.Length != second.Length)
			{
				return false;
			}
			for (int i = 0; i < first.Length; i++)
			{
				if (first[i] != second[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040009B8 RID: 2488
		internal static readonly ByValueEqualityComparer Default = new ByValueEqualityComparer();
	}
}
