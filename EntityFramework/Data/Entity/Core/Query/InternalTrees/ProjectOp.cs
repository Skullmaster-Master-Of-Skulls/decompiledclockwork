using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000614 RID: 1556
	internal sealed class ProjectOp : RelOp
	{
		// Token: 0x06003D10 RID: 15632 RVA: 0x0011ACC6 File Offset: 0x00118EC6
		private ProjectOp() : base(OpType.Project)
		{
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x0011ACD0 File Offset: 0x00118ED0
		internal ProjectOp(VarVec vars) : this()
		{
			this.m_vars = vars;
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06003D12 RID: 15634 RVA: 0x0011ACDF File Offset: 0x00118EDF
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06003D13 RID: 15635 RVA: 0x0011ACE2 File Offset: 0x00118EE2
		internal VarVec Outputs
		{
			get
			{
				return this.m_vars;
			}
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x0011ACEA File Offset: 0x00118EEA
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x0011ACF4 File Offset: 0x00118EF4
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001717 RID: 5911
		private readonly VarVec m_vars;

		// Token: 0x04001718 RID: 5912
		internal static readonly ProjectOp Pattern = new ProjectOp();
	}
}
