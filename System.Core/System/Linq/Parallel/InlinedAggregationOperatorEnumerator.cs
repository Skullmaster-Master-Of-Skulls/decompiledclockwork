using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001AC RID: 428
	internal abstract class InlinedAggregationOperatorEnumerator<TIntermediate> : QueryOperatorEnumerator<TIntermediate, int>
	{
		// Token: 0x06000EA9 RID: 3753 RVA: 0x00034411 File Offset: 0x00032611
		internal InlinedAggregationOperatorEnumerator(int partitionIndex, CancellationToken cancellationToken)
		{
			this.m_partitionIndex = partitionIndex;
			this.m_cancellationToken = cancellationToken;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00034427 File Offset: 0x00032627
		internal sealed override bool MoveNext(ref TIntermediate currentElement, ref int currentKey)
		{
			if (!this.m_done && this.MoveNextCore(ref currentElement))
			{
				currentKey = this.m_partitionIndex;
				this.m_done = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000EAB RID: 3755
		protected abstract bool MoveNextCore(ref TIntermediate currentElement);

		// Token: 0x0400089A RID: 2202
		private int m_partitionIndex;

		// Token: 0x0400089B RID: 2203
		private bool m_done;

		// Token: 0x0400089C RID: 2204
		protected CancellationToken m_cancellationToken;
	}
}
