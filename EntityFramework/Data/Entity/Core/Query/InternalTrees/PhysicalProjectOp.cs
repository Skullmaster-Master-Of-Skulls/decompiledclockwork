using System;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000613 RID: 1555
	internal class PhysicalProjectOp : PhysicalOp
	{
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06003D09 RID: 15625 RVA: 0x0011AC76 File Offset: 0x00118E76
		internal SimpleCollectionColumnMap ColumnMap
		{
			get
			{
				return this.m_columnMap;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x0011AC7E File Offset: 0x00118E7E
		internal VarList Outputs
		{
			get
			{
				return this.m_outputVars;
			}
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x0011AC86 File Offset: 0x00118E86
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x0011AC90 File Offset: 0x00118E90
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x0011AC9A File Offset: 0x00118E9A
		internal PhysicalProjectOp(VarList outputVars, SimpleCollectionColumnMap columnMap) : this()
		{
			this.m_outputVars = outputVars;
			this.m_columnMap = columnMap;
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x0011ACB0 File Offset: 0x00118EB0
		private PhysicalProjectOp() : base(OpType.PhysicalProject)
		{
		}

		// Token: 0x04001714 RID: 5908
		internal static readonly PhysicalProjectOp Pattern = new PhysicalProjectOp();

		// Token: 0x04001715 RID: 5909
		private readonly SimpleCollectionColumnMap m_columnMap;

		// Token: 0x04001716 RID: 5910
		private readonly VarList m_outputVars;
	}
}
