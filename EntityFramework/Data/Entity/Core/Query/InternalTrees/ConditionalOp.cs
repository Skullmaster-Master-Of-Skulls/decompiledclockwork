using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005DC RID: 1500
	internal sealed class ConditionalOp : ScalarOp
	{
		// Token: 0x06003BD1 RID: 15313 RVA: 0x00118776 File Offset: 0x00116976
		internal ConditionalOp(OpType optype, TypeUsage type) : base(optype, type)
		{
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x00118780 File Offset: 0x00116980
		private ConditionalOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x00118789 File Offset: 0x00116989
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x00118793 File Offset: 0x00116993
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001670 RID: 5744
		internal static readonly ConditionalOp PatternAnd = new ConditionalOp(OpType.And);

		// Token: 0x04001671 RID: 5745
		internal static readonly ConditionalOp PatternOr = new ConditionalOp(OpType.Or);

		// Token: 0x04001672 RID: 5746
		internal static readonly ConditionalOp PatternIn = new ConditionalOp(OpType.In);

		// Token: 0x04001673 RID: 5747
		internal static readonly ConditionalOp PatternNot = new ConditionalOp(OpType.Not);

		// Token: 0x04001674 RID: 5748
		internal static readonly ConditionalOp PatternIsNull = new ConditionalOp(OpType.IsNull);
	}
}
