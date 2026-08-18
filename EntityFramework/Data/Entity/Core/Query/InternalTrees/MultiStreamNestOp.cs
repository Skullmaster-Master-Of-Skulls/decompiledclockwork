using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200063E RID: 1598
	internal class MultiStreamNestOp : NestBaseOp
	{
		// Token: 0x06003EC5 RID: 16069 RVA: 0x0011FAE6 File Offset: 0x0011DCE6
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x0011FAF0 File Offset: 0x0011DCF0
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x0011FAFA File Offset: 0x0011DCFA
		internal MultiStreamNestOp(List<SortKey> prefixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList) : base(OpType.MultiStreamNest, prefixSortKeys, outputVars, collectionInfoList)
		{
		}
	}
}
