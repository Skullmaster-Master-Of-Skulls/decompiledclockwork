using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000DF RID: 223
	internal sealed class DistinctOp : RelOp
	{
		// Token: 0x06000C9D RID: 3229 RVA: 0x0003C3B7 File Offset: 0x0003A5B7
		private DistinctOp() : base(OpType.Distinct)
		{
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0003C3C1 File Offset: 0x0003A5C1
		internal DistinctOp(VarVec keyVars) : this()
		{
			this.m_keys = keyVars;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0003C3D0 File Offset: 0x0003A5D0
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0003C3D8 File Offset: 0x0003A5D8
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0003C3E2 File Offset: 0x0003A5E2
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000987 RID: 2439
		private VarVec m_keys;

		// Token: 0x04000988 RID: 2440
		internal static readonly DistinctOp Pattern = new DistinctOp();
	}
}
