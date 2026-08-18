using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Data.Objects
{
	// Token: 0x02000149 RID: 329
	internal sealed class ObjectReferenceEqualityComparer : IEqualityComparer<object>
	{
		// Token: 0x06001840 RID: 6208 RVA: 0x000534A7 File Offset: 0x000516A7
		bool IEqualityComparer<object>.Equals(object x, object y)
		{
			return x == y;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x000534AD File Offset: 0x000516AD
		int IEqualityComparer<object>.GetHashCode(object obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}
	}
}
