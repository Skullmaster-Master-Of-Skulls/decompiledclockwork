using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Web.Http.Validation
{
	// Token: 0x02000192 RID: 402
	internal class ReferenceEqualityComparer : IEqualityComparer<object>
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00022A29 File Offset: 0x00020C29
		public static ReferenceEqualityComparer Instance
		{
			get
			{
				return ReferenceEqualityComparer._instance;
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00022A30 File Offset: 0x00020C30
		public bool Equals(object x, object y)
		{
			return object.ReferenceEquals(x, y);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00022A39 File Offset: 0x00020C39
		public int GetHashCode(object obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x0400030D RID: 781
		private static readonly ReferenceEqualityComparer _instance = new ReferenceEqualityComparer();
	}
}
