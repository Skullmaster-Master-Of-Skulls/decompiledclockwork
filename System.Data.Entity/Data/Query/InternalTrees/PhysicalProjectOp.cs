using System;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000C1 RID: 193
	internal class PhysicalProjectOp : PhysicalOp
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0003BD53 File Offset: 0x00039F53
		internal SimpleCollectionColumnMap ColumnMap
		{
			get
			{
				return this.m_columnMap;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0003BD5B File Offset: 0x00039F5B
		internal VarList Outputs
		{
			get
			{
				return this.m_outputVars;
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0003BD63 File Offset: 0x00039F63
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0003BD6D File Offset: 0x00039F6D
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0003BD77 File Offset: 0x00039F77
		internal PhysicalProjectOp(VarList outputVars, SimpleCollectionColumnMap columnMap) : this()
		{
			this.m_outputVars = outputVars;
			this.m_columnMap = columnMap;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0003BD8D File Offset: 0x00039F8D
		private PhysicalProjectOp() : base(OpType.PhysicalProject)
		{
		}

		// Token: 0x04000950 RID: 2384
		internal static readonly PhysicalProjectOp Pattern = new PhysicalProjectOp();

		// Token: 0x04000951 RID: 2385
		private SimpleCollectionColumnMap m_columnMap;

		// Token: 0x04000952 RID: 2386
		private VarList m_outputVars;
	}
}
