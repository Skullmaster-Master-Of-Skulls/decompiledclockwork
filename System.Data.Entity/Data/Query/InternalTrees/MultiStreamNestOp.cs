using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000C5 RID: 197
	internal class MultiStreamNestOp : NestBaseOp
	{
		// Token: 0x06000C1C RID: 3100 RVA: 0x0003BE90 File Offset: 0x0003A090
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0003BE9A File Offset: 0x0003A09A
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0003BEA4 File Offset: 0x0003A0A4
		internal MultiStreamNestOp(List<SortKey> prefixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList) : base(OpType.MultiStreamNest, prefixSortKeys, outputVars, collectionInfoList)
		{
		}
	}
}
