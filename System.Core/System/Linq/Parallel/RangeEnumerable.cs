using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200017E RID: 382
	internal class RangeEnumerable : ParallelQuery<int>, IParallelPartitionable<int>
	{
		// Token: 0x06000DE9 RID: 3561 RVA: 0x00031287 File Offset: 0x0002F487
		internal RangeEnumerable(int from, int count) : base(QuerySettings.Empty)
		{
			this.m_from = from;
			this.m_count = count;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x000312A4 File Offset: 0x0002F4A4
		public QueryOperatorEnumerator<int, int>[] GetPartitions(int partitionCount)
		{
			int num = this.m_count / partitionCount;
			int num2 = this.m_count % partitionCount;
			int num3 = 0;
			QueryOperatorEnumerator<int, int>[] array = new QueryOperatorEnumerator<int, int>[partitionCount];
			for (int i = 0; i < partitionCount; i++)
			{
				int num4 = (i < num2) ? (num + 1) : num;
				array[i] = new RangeEnumerable.RangeEnumerator(this.m_from + num3, num4, num3);
				num3 += num4;
			}
			return array;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00031303 File Offset: 0x0002F503
		public override IEnumerator<int> GetEnumerator()
		{
			return new RangeEnumerable.RangeEnumerator(this.m_from, this.m_count, 0).AsClassicEnumerator();
		}

		// Token: 0x0400081C RID: 2076
		private int m_from;

		// Token: 0x0400081D RID: 2077
		private int m_count;

		// Token: 0x020003AD RID: 941
		private class RangeEnumerator : QueryOperatorEnumerator<int, int>
		{
			// Token: 0x06001D40 RID: 7488 RVA: 0x00067FE8 File Offset: 0x000661E8
			internal RangeEnumerator(int from, int count, int initialIndex)
			{
				this.m_from = from;
				this.m_count = count;
				this.m_initialIndex = initialIndex;
			}

			// Token: 0x06001D41 RID: 7489 RVA: 0x00068008 File Offset: 0x00066208
			internal override bool MoveNext(ref int currentElement, ref int currentKey)
			{
				if (this.m_currentCount == null)
				{
					this.m_currentCount = new Shared<int>(-1);
				}
				int num = this.m_currentCount.Value + 1;
				if (num < this.m_count)
				{
					this.m_currentCount.Value = num;
					currentElement = num + this.m_from;
					currentKey = num + this.m_initialIndex;
					return true;
				}
				return false;
			}

			// Token: 0x06001D42 RID: 7490 RVA: 0x00068063 File Offset: 0x00066263
			internal override void Reset()
			{
				this.m_currentCount = null;
			}

			// Token: 0x040010FD RID: 4349
			private readonly int m_from;

			// Token: 0x040010FE RID: 4350
			private readonly int m_count;

			// Token: 0x040010FF RID: 4351
			private readonly int m_initialIndex;

			// Token: 0x04001100 RID: 4352
			private Shared<int> m_currentCount;
		}
	}
}
