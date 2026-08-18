using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000604 RID: 1540
	internal abstract class NestBaseOp : PhysicalOp
	{
		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06003CB4 RID: 15540 RVA: 0x0011961C File Offset: 0x0011781C
		internal List<SortKey> PrefixSortKeys
		{
			get
			{
				return this.m_prefixSortKeys;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x00119624 File Offset: 0x00117824
		internal VarVec Outputs
		{
			get
			{
				return this.m_outputs;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x0011962C File Offset: 0x0011782C
		internal List<CollectionInfo> CollectionInfo
		{
			get
			{
				return this.m_collectionInfoList;
			}
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x00119634 File Offset: 0x00117834
		internal NestBaseOp(OpType opType, List<SortKey> prefixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList) : base(opType)
		{
			this.m_outputs = outputVars;
			this.m_collectionInfoList = collectionInfoList;
			this.m_prefixSortKeys = prefixSortKeys;
		}

		// Token: 0x040016B7 RID: 5815
		private readonly List<SortKey> m_prefixSortKeys;

		// Token: 0x040016B8 RID: 5816
		private readonly VarVec m_outputs;

		// Token: 0x040016B9 RID: 5817
		private readonly List<CollectionInfo> m_collectionInfoList;
	}
}
