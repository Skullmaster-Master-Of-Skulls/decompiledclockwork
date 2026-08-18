using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000181 RID: 385
	internal class ArrayMergeHelper<TInputOutput> : IMergeHelper<TInputOutput>
	{
		// Token: 0x06000DF4 RID: 3572 RVA: 0x000316A4 File Offset: 0x0002F8A4
		public ArrayMergeHelper(QuerySettings settings, QueryResults<TInputOutput> queryResults)
		{
			this.m_settings = settings;
			this.m_queryResults = queryResults;
			int count = this.m_queryResults.Count;
			this.m_outputArray = new TInputOutput[count];
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x000316DD File Offset: 0x0002F8DD
		private void ToArrayElement(int index)
		{
			this.m_outputArray[index] = this.m_queryResults[index];
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x000316F8 File Offset: 0x0002F8F8
		public void Execute()
		{
			ParallelQuery<int> source = ParallelEnumerable.Range(0, this.m_queryResults.Count);
			source = new QueryExecutionOption<int>(QueryOperator<int>.AsQueryOperator(source), this.m_settings);
			source.ForAll(new Action<int>(this.ToArrayElement));
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003173B File Offset: 0x0002F93B
		public IEnumerator<TInputOutput> GetEnumerator()
		{
			return this.GetResultsAsArray().GetEnumerator();
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00031748 File Offset: 0x0002F948
		public TInputOutput[] GetResultsAsArray()
		{
			return this.m_outputArray;
		}

		// Token: 0x04000825 RID: 2085
		private QueryResults<TInputOutput> m_queryResults;

		// Token: 0x04000826 RID: 2086
		private TInputOutput[] m_outputArray;

		// Token: 0x04000827 RID: 2087
		private QuerySettings m_settings;
	}
}
