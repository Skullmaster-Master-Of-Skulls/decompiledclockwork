using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Linq
{
	// Token: 0x02000160 RID: 352
	internal sealed class SystemCore_EnumerableDebugView
	{
		// Token: 0x06000C31 RID: 3121 RVA: 0x0002D322 File Offset: 0x0002B522
		public SystemCore_EnumerableDebugView(IEnumerable enumerable)
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			this.enumerable = enumerable;
			this.count = 0;
			this.cachedCollection = null;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0002D350 File Offset: 0x0002B550
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public object[] Items
		{
			get
			{
				List<object> list = new List<object>();
				IEnumerator enumerator = this.enumerable.GetEnumerator();
				if (enumerator != null)
				{
					this.count = 0;
					while (enumerator.MoveNext())
					{
						object item = enumerator.Current;
						list.Add(item);
						this.count++;
					}
				}
				if (this.count == 0)
				{
					throw new SystemCore_EnumerableDebugViewEmptyException();
				}
				this.cachedCollection = new object[this.count];
				list.CopyTo(this.cachedCollection, 0);
				return this.cachedCollection;
			}
		}

		// Token: 0x0400079D RID: 1949
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IEnumerable enumerable;

		// Token: 0x0400079E RID: 1950
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private object[] cachedCollection;

		// Token: 0x0400079F RID: 1951
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int count;
	}
}
