using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200027A RID: 634
	[Serializable]
	public sealed class ObjectReferenceEqualityComparer : IEqualityComparer<object>
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0006C1A1 File Offset: 0x0006A3A1
		public static ObjectReferenceEqualityComparer Default
		{
			get
			{
				return ObjectReferenceEqualityComparer._default;
			}
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0006C1A8 File Offset: 0x0006A3A8
		bool IEqualityComparer<object>.Equals(object x, object y)
		{
			return object.ReferenceEquals(x, y);
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0006C1B1 File Offset: 0x0006A3B1
		int IEqualityComparer<object>.GetHashCode(object obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x040007E8 RID: 2024
		private static readonly ObjectReferenceEqualityComparer _default = new ObjectReferenceEqualityComparer();
	}
}
