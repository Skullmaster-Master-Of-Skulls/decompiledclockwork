using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x0200018C RID: 396
	internal abstract class HashRepartitionStream<TInputOutput, THashKey, TOrderKey> : PartitionedStream<Pair<TInputOutput, THashKey>, TOrderKey>
	{
		// Token: 0x06000E20 RID: 3616 RVA: 0x00032073 File Offset: 0x00030273
		internal HashRepartitionStream(int partitionsCount, IComparer<TOrderKey> orderKeyComparer, IEqualityComparer<THashKey> hashKeyComparer, IEqualityComparer<TInputOutput> elementComparer) : base(partitionsCount, orderKeyComparer, OrdinalIndexState.Shuffled)
		{
			this.m_keyComparer = hashKeyComparer;
			this.m_elementComparer = elementComparer;
			this.m_distributionMod = 503;
			checked
			{
				while (this.m_distributionMod < partitionsCount)
				{
					this.m_distributionMod *= 2;
				}
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000320B1 File Offset: 0x000302B1
		internal int GetHashCode(TInputOutput element)
		{
			return (int.MaxValue & ((this.m_elementComparer == null) ? ((element == null) ? 0 : element.GetHashCode()) : this.m_elementComparer.GetHashCode(element))) % this.m_distributionMod;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x000320EE File Offset: 0x000302EE
		internal int GetHashCode(THashKey key)
		{
			return (int.MaxValue & ((this.m_keyComparer == null) ? ((key == null) ? 0 : key.GetHashCode()) : this.m_keyComparer.GetHashCode(key))) % this.m_distributionMod;
		}

		// Token: 0x04000851 RID: 2129
		private readonly IEqualityComparer<THashKey> m_keyComparer;

		// Token: 0x04000852 RID: 2130
		private readonly IEqualityComparer<TInputOutput> m_elementComparer;

		// Token: 0x04000853 RID: 2131
		private readonly int m_distributionMod;

		// Token: 0x04000854 RID: 2132
		private const int NULL_ELEMENT_HASH_CODE = 0;
	}
}
