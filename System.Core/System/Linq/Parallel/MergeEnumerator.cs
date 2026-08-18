using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000184 RID: 388
	internal abstract class MergeEnumerator<TInputOutput> : IEnumerator<!0>, IDisposable, IEnumerator
	{
		// Token: 0x06000E00 RID: 3584 RVA: 0x0003196C File Offset: 0x0002FB6C
		protected MergeEnumerator(QueryTaskGroupState taskGroupState)
		{
			this.m_taskGroupState = taskGroupState;
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000E01 RID: 3585
		public abstract TInputOutput Current { get; }

		// Token: 0x06000E02 RID: 3586
		public abstract bool MoveNext();

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0003197B File Offset: 0x0002FB7B
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<TInputOutput>)this).Current;
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00031988 File Offset: 0x0002FB88
		public virtual void Reset()
		{
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003198A File Offset: 0x0002FB8A
		public virtual void Dispose()
		{
			if (!this.m_taskGroupState.IsAlreadyEnded)
			{
				this.m_taskGroupState.QueryEnd(true);
			}
		}

		// Token: 0x0400082F RID: 2095
		protected QueryTaskGroupState m_taskGroupState;
	}
}
