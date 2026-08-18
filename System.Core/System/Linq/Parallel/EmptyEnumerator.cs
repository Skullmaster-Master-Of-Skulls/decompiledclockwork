using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000178 RID: 376
	internal class EmptyEnumerator<T> : QueryOperatorEnumerator<T, int>, IEnumerator<!0>, IDisposable, IEnumerator
	{
		// Token: 0x06000DD9 RID: 3545 RVA: 0x000311D7 File Offset: 0x0002F3D7
		internal override bool MoveNext(ref T currentElement, ref int currentKey)
		{
			return false;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x000311DC File Offset: 0x0002F3DC
		public T Current
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x000311F2 File Offset: 0x0002F3F2
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x000311F5 File Offset: 0x0002F3F5
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000311F8 File Offset: 0x0002F3F8
		void IEnumerator.Reset()
		{
		}
	}
}
