using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000177 RID: 375
	internal class EmptyEnumerable<T> : ParallelQuery<T>
	{
		// Token: 0x06000DD6 RID: 3542 RVA: 0x0003118E File Offset: 0x0002F38E
		private EmptyEnumerable() : base(QuerySettings.Empty)
		{
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0003119B File Offset: 0x0002F39B
		internal static EmptyEnumerable<T> Instance
		{
			get
			{
				if (EmptyEnumerable<T>.s_instance == null)
				{
					EmptyEnumerable<T>.s_instance = new EmptyEnumerable<T>();
				}
				return EmptyEnumerable<T>.s_instance;
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x000311B9 File Offset: 0x0002F3B9
		public override IEnumerator<T> GetEnumerator()
		{
			if (EmptyEnumerable<T>.s_enumeratorInstance == null)
			{
				EmptyEnumerable<T>.s_enumeratorInstance = new EmptyEnumerator<T>();
			}
			return EmptyEnumerable<T>.s_enumeratorInstance;
		}

		// Token: 0x04000812 RID: 2066
		private static volatile EmptyEnumerable<T> s_instance;

		// Token: 0x04000813 RID: 2067
		private static volatile EmptyEnumerator<T> s_enumeratorInstance;
	}
}
