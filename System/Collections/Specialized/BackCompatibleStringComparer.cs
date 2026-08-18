using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Specialized
{
	// Token: 0x020007A4 RID: 1956
	internal class BackCompatibleStringComparer : IEqualityComparer
	{
		// Token: 0x06003C38 RID: 15416 RVA: 0x001015A7 File Offset: 0x001005A7
		internal BackCompatibleStringComparer()
		{
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x001015B0 File Offset: 0x001005B0
		public unsafe static int GetHashCode(string obj)
		{
			IntPtr intPtr2;
			IntPtr intPtr = intPtr2 = obj;
			if (intPtr != 0)
			{
				intPtr2 = (IntPtr)((int)intPtr + RuntimeHelpers.OffsetToStringData);
			}
			char* ptr = intPtr2;
			int num = 5381;
			char* ptr2 = ptr;
			int num2;
			while ((num2 = (int)(*ptr2)) != 0)
			{
				num = ((num << 5) + num ^ num2);
				ptr2++;
			}
			return num;
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x001015F1 File Offset: 0x001005F1
		bool IEqualityComparer.Equals(object a, object b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x001015FC File Offset: 0x001005FC
		public virtual int GetHashCode(object o)
		{
			string text = o as string;
			if (text == null)
			{
				return o.GetHashCode();
			}
			return BackCompatibleStringComparer.GetHashCode(text);
		}

		// Token: 0x04003521 RID: 13601
		internal static IEqualityComparer Default = new BackCompatibleStringComparer();
	}
}
