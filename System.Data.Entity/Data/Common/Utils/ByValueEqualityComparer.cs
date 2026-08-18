using System;
using System.Collections.Generic;

namespace System.Data.Common.Utils
{
	// Token: 0x0200038C RID: 908
	internal sealed class ByValueEqualityComparer : IEqualityComparer<object>
	{
		// Token: 0x06003275 RID: 12917 RVA: 0x00002050 File Offset: 0x00000250
		private ByValueEqualityComparer()
		{
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x000C5304 File Offset: 0x000C3504
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

		// Token: 0x06003277 RID: 12919 RVA: 0x000C533C File Offset: 0x000C353C
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

		// Token: 0x06003278 RID: 12920 RVA: 0x000C5368 File Offset: 0x000C3568
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

		// Token: 0x06003279 RID: 12921 RVA: 0x000C5398 File Offset: 0x000C3598
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

		// Token: 0x04001652 RID: 5714
		internal static readonly ByValueEqualityComparer Default = new ByValueEqualityComparer();
	}
}
