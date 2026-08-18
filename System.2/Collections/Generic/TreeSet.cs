using System;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x020003C8 RID: 968
	[Serializable]
	internal class TreeSet<T> : SortedSet<T>
	{
		// Token: 0x060024DF RID: 9439 RVA: 0x000ABB8F File Offset: 0x000A9D8F
		public TreeSet()
		{
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000ABB97 File Offset: 0x000A9D97
		public TreeSet(IComparer<T> comparer) : base(comparer)
		{
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000ABBA0 File Offset: 0x000A9DA0
		public TreeSet(ICollection<T> collection) : base(collection)
		{
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000ABBA9 File Offset: 0x000A9DA9
		public TreeSet(ICollection<T> collection, IComparer<T> comparer) : base(collection, comparer)
		{
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000ABBB3 File Offset: 0x000A9DB3
		public TreeSet(SerializationInfo siInfo, StreamingContext context) : base(siInfo, context)
		{
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000ABBC0 File Offset: 0x000A9DC0
		internal override bool AddIfNotPresent(T item)
		{
			bool flag = base.AddIfNotPresent(item);
			if (!flag)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
			}
			return flag;
		}
	}
}
