using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Specialized
{
	// Token: 0x020000F5 RID: 245
	internal class BackCompatibleStringComparer : IEqualityComparer
	{
		// Token: 0x060003BE RID: 958 RVA: 0x00002843 File Offset: 0x00000A43
		internal BackCompatibleStringComparer()
		{
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000C09C File Offset: 0x0000A29C
		public unsafe static int GetHashCode(string obj)
		{
			char* ptr = obj;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
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

		// Token: 0x060003C0 RID: 960 RVA: 0x0000C0DA File Offset: 0x0000A2DA
		bool IEqualityComparer.Equals(object a, object b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		public virtual int GetHashCode(object o)
		{
			string text = o as string;
			if (text == null)
			{
				return o.GetHashCode();
			}
			return BackCompatibleStringComparer.GetHashCode(text);
		}

		// Token: 0x04000408 RID: 1032
		internal static IEqualityComparer Default = new BackCompatibleStringComparer();
	}
}
