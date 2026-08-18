using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000D3 RID: 211
	internal sealed class ProjectOp : RelOp
	{
		// Token: 0x06000C58 RID: 3160 RVA: 0x0003C0FB File Offset: 0x0003A2FB
		private ProjectOp() : base(OpType.Project)
		{
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0003C105 File Offset: 0x0003A305
		internal ProjectOp(VarVec vars) : this()
		{
			this.m_vars = vars;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00033532 File Offset: 0x00031732
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0003C114 File Offset: 0x0003A314
		internal VarVec Outputs
		{
			get
			{
				return this.m_vars;
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0003C11C File Offset: 0x0003A31C
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0003C126 File Offset: 0x0003A326
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000973 RID: 2419
		private VarVec m_vars;

		// Token: 0x04000974 RID: 2420
		internal static readonly ProjectOp Pattern = new ProjectOp();
	}
}
