using System;
using System.Collections.Generic;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000C3 RID: 195
	internal abstract class NestBaseOp : PhysicalOp
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0003BE08 File Offset: 0x0003A008
		internal List<SortKey> PrefixSortKeys
		{
			get
			{
				return this.m_prefixSortKeys;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0003BE10 File Offset: 0x0003A010
		internal VarVec Outputs
		{
			get
			{
				return this.m_outputs;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0003BE18 File Offset: 0x0003A018
		internal List<CollectionInfo> CollectionInfo
		{
			get
			{
				return this.m_collectionInfoList;
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0003BE20 File Offset: 0x0003A020
		internal NestBaseOp(OpType opType, List<SortKey> prefixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList) : base(opType)
		{
			this.m_outputs = outputVars;
			this.m_collectionInfoList = collectionInfoList;
			this.m_prefixSortKeys = prefixSortKeys;
		}

		// Token: 0x04000959 RID: 2393
		private List<SortKey> m_prefixSortKeys;

		// Token: 0x0400095A RID: 2394
		private VarVec m_outputs;

		// Token: 0x0400095B RID: 2395
		private List<CollectionInfo> m_collectionInfoList;
	}
}
