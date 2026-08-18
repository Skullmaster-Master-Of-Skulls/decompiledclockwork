using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001C2 RID: 450
	internal class ListQueryResults<T> : QueryResults<T>
	{
		// Token: 0x06000EEB RID: 3819 RVA: 0x0003541B File Offset: 0x0003361B
		internal ListQueryResults(IList<T> source, int partitionCount, bool useStriping)
		{
			this.m_source = source;
			this.m_partitionCount = partitionCount;
			this.m_useStriping = useStriping;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00035438 File Offset: 0x00033638
		internal override void GivePartitionedStream(IPartitionedStreamRecipient<T> recipient)
		{
			PartitionedStream<T, int> partitionedStream = this.GetPartitionedStream();
			recipient.Receive<int>(partitionedStream);
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x00035453 File Offset: 0x00033653
		internal override bool IsIndexible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00035456 File Offset: 0x00033656
		internal override int ElementsCount
		{
			get
			{
				return this.m_source.Count;
			}
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00035463 File Offset: 0x00033663
		internal override T GetElement(int index)
		{
			return this.m_source[index];
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00035471 File Offset: 0x00033671
		internal PartitionedStream<T, int> GetPartitionedStream()
		{
			return ExchangeUtilities.PartitionDataSource<T>(this.m_source, this.m_partitionCount, this.m_useStriping);
		}

		// Token: 0x040008A4 RID: 2212
		private IList<T> m_source;

		// Token: 0x040008A5 RID: 2213
		private int m_partitionCount;

		// Token: 0x040008A6 RID: 2214
		private bool m_useStriping;
	}
}
