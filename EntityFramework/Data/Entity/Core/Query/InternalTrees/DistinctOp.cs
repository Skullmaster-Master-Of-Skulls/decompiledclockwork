using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005EB RID: 1515
	internal sealed class DistinctOp : RelOp
	{
		// Token: 0x06003C1B RID: 15387 RVA: 0x00118BE0 File Offset: 0x00116DE0
		private DistinctOp() : base(OpType.Distinct)
		{
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x00118BEA File Offset: 0x00116DEA
		internal DistinctOp(VarVec keyVars) : this()
		{
			this.m_keys = keyVars;
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06003C1D RID: 15389 RVA: 0x00118BF9 File Offset: 0x00116DF9
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x00118BFC File Offset: 0x00116DFC
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x06003C1F RID: 15391 RVA: 0x00118C04 File Offset: 0x00116E04
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x00118C0E File Offset: 0x00116E0E
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400168A RID: 5770
		private readonly VarVec m_keys;

		// Token: 0x0400168B RID: 5771
		internal static readonly DistinctOp Pattern = new DistinctOp();
	}
}
