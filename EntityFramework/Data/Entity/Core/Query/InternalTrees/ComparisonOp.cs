using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005D7 RID: 1495
	internal sealed class ComparisonOp : ScalarOp
	{
		// Token: 0x06003BBE RID: 15294 RVA: 0x00118618 File Offset: 0x00116818
		internal ComparisonOp(OpType opType, TypeUsage type) : base(opType, type)
		{
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x00118622 File Offset: 0x00116822
		private ComparisonOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06003BC0 RID: 15296 RVA: 0x0011862B File Offset: 0x0011682B
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x0011862E File Offset: 0x0011682E
		// (set) Token: 0x06003BC2 RID: 15298 RVA: 0x00118636 File Offset: 0x00116836
		internal bool UseDatabaseNullSemantics { get; set; }

		// Token: 0x06003BC3 RID: 15299 RVA: 0x0011863F File Offset: 0x0011683F
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x00118649 File Offset: 0x00116849
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400166C RID: 5740
		internal static readonly ComparisonOp PatternEq = new ComparisonOp(OpType.EQ);
	}
}
