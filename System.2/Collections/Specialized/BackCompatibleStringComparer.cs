using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Specialized
{
	// Token: 0x020003B9 RID: 953
	internal class BackCompatibleStringComparer : IEqualityComparer
	{
		// Token: 0x060023EF RID: 9199 RVA: 0x000A8F9D File Offset: 0x000A719D
		internal BackCompatibleStringComparer()
		{
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x000A8FA8 File Offset: 0x000A71A8
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

		// Token: 0x060023F1 RID: 9201 RVA: 0x000A8FE6 File Offset: 0x000A71E6
		bool IEqualityComparer.Equals(object a, object b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000A8FF0 File Offset: 0x000A71F0
		public virtual int GetHashCode(object o)
		{
			string text = o as string;
			if (text == null)
			{
				return o.GetHashCode();
			}
			return BackCompatibleStringComparer.GetHashCode(text);
		}

		// Token: 0x04001FF9 RID: 8185
		internal static IEqualityComparer Default = new BackCompatibleStringComparer();
	}
}
