using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata.Decoding
{
	// Token: 0x02000144 RID: 324
	internal struct ArrayShape
	{
		// Token: 0x06000A66 RID: 2662 RVA: 0x0001DFA4 File Offset: 0x0001C1A4
		public ArrayShape(int rank, ImmutableArray<int> sizes, ImmutableArray<int> lowerBounds)
		{
			this._rank = rank;
			this._sizes = sizes;
			this._lowerBounds = lowerBounds;
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0001DFBB File Offset: 0x0001C1BB
		public int Rank
		{
			get
			{
				return this._rank;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0001DFC3 File Offset: 0x0001C1C3
		public ImmutableArray<int> Sizes
		{
			get
			{
				return this._sizes;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0001DFCB File Offset: 0x0001C1CB
		public ImmutableArray<int> LowerBounds
		{
			get
			{
				return this._lowerBounds;
			}
		}

		// Token: 0x040008C7 RID: 2247
		private readonly int _rank;

		// Token: 0x040008C8 RID: 2248
		private readonly ImmutableArray<int> _sizes;

		// Token: 0x040008C9 RID: 2249
		private readonly ImmutableArray<int> _lowerBounds;
	}
}
