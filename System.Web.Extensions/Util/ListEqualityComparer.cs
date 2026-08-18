using System;
using System.Collections;

namespace System.Web.Util
{
	// Token: 0x02000036 RID: 54
	internal sealed class ListEqualityComparer : IEqualityComparer
	{
		// Token: 0x0600021A RID: 538 RVA: 0x00002050 File Offset: 0x00000250
		private ListEqualityComparer()
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000D350 File Offset: 0x0000B550
		bool IEqualityComparer.Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			IList list = (IList)x;
			IList list2 = (IList)y;
			if (list.Count != list2.Count)
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (!object.Equals(list[i], list2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000D3AC File Offset: 0x0000B5AC
		int IEqualityComparer.GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			foreach (object o in ((IList)obj))
			{
				hashCodeCombiner.AddObject(o);
			}
			return hashCodeCombiner.CombinedHash32;
		}

		// Token: 0x040000D9 RID: 217
		internal static readonly ListEqualityComparer Instance = new ListEqualityComparer();
	}
}
