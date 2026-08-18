using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200017F RID: 383
	internal class RepeatEnumerable<TResult> : ParallelQuery<TResult>, IParallelPartitionable<TResult>
	{
		// Token: 0x06000DEC RID: 3564 RVA: 0x0003131C File Offset: 0x0002F51C
		internal RepeatEnumerable(TResult element, int count) : base(QuerySettings.Empty)
		{
			this.m_element = element;
			this.m_count = count;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00031338 File Offset: 0x0002F538
		public QueryOperatorEnumerator<TResult, int>[] GetPartitions(int partitionCount)
		{
			int num = (this.m_count + partitionCount - 1) / partitionCount;
			QueryOperatorEnumerator<TResult, int>[] array = new QueryOperatorEnumerator<TResult, int>[partitionCount];
			int i = 0;
			int num2 = 0;
			while (i < partitionCount)
			{
				if (num2 + num > this.m_count)
				{
					array[i] = new RepeatEnumerable<TResult>.RepeatEnumerator(this.m_element, (num2 < this.m_count) ? (this.m_count - num2) : 0, num2);
				}
				else
				{
					array[i] = new RepeatEnumerable<TResult>.RepeatEnumerator(this.m_element, num, num2);
				}
				i++;
				num2 += num;
			}
			return array;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x000313AC File Offset: 0x0002F5AC
		public override IEnumerator<TResult> GetEnumerator()
		{
			return new RepeatEnumerable<TResult>.RepeatEnumerator(this.m_element, this.m_count, 0).AsClassicEnumerator();
		}

		// Token: 0x0400081E RID: 2078
		private TResult m_element;

		// Token: 0x0400081F RID: 2079
		private int m_count;

		// Token: 0x020003AE RID: 942
		private class RepeatEnumerator : QueryOperatorEnumerator<TResult, int>
		{
			// Token: 0x06001D43 RID: 7491 RVA: 0x0006806C File Offset: 0x0006626C
			internal RepeatEnumerator(TResult element, int count, int indexOffset)
			{
				this.m_element = element;
				this.m_count = count;
				this.m_indexOffset = indexOffset;
			}

			// Token: 0x06001D44 RID: 7492 RVA: 0x0006808C File Offset: 0x0006628C
			internal override bool MoveNext(ref TResult currentElement, ref int currentKey)
			{
				if (this.m_currentIndex == null)
				{
					this.m_currentIndex = new Shared<int>(-1);
				}
				if (this.m_currentIndex.Value < this.m_count - 1)
				{
					this.m_currentIndex.Value++;
					currentElement = this.m_element;
					currentKey = this.m_currentIndex.Value + this.m_indexOffset;
					return true;
				}
				return false;
			}

			// Token: 0x06001D45 RID: 7493 RVA: 0x000680F8 File Offset: 0x000662F8
			internal override void Reset()
			{
				this.m_currentIndex = null;
			}

			// Token: 0x04001101 RID: 4353
			private readonly TResult m_element;

			// Token: 0x04001102 RID: 4354
			private readonly int m_count;

			// Token: 0x04001103 RID: 4355
			private readonly int m_indexOffset;

			// Token: 0x04001104 RID: 4356
			private Shared<int> m_currentIndex;
		}
	}
}
