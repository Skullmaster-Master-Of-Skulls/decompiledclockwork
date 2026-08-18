using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005C6 RID: 1478
	internal abstract class Op
	{
		// Token: 0x06003B19 RID: 15129 RVA: 0x00117EB3 File Offset: 0x001160B3
		internal Op(OpType opType)
		{
			this.m_opType = opType;
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06003B1A RID: 15130 RVA: 0x00117EC2 File Offset: 0x001160C2
		internal OpType OpType
		{
			get
			{
				return this.m_opType;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x00117ECA File Offset: 0x001160CA
		internal virtual int Arity
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06003B1C RID: 15132 RVA: 0x00117ECD File Offset: 0x001160CD
		internal virtual bool IsScalarOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x00117ED0 File Offset: 0x001160D0
		internal virtual bool IsRulePatternOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06003B1E RID: 15134 RVA: 0x00117ED3 File Offset: 0x001160D3
		internal virtual bool IsRelOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x00117ED6 File Offset: 0x001160D6
		internal virtual bool IsAncillaryOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06003B20 RID: 15136 RVA: 0x00117ED9 File Offset: 0x001160D9
		internal virtual bool IsPhysicalOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x00117EDC File Offset: 0x001160DC
		internal virtual bool IsEquivalent(Op other)
		{
			return false;
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x00117EDF File Offset: 0x001160DF
		// (set) Token: 0x06003B23 RID: 15139 RVA: 0x00117EE2 File Offset: 0x001160E2
		internal virtual TypeUsage Type
		{
			get
			{
				return null;
			}
			set
			{
				throw Error.NotSupported();
			}
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x00117EE9 File Offset: 0x001160E9
		[DebuggerNonUserCode]
		internal virtual void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x00117EF3 File Offset: 0x001160F3
		[DebuggerNonUserCode]
		internal virtual TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400164F RID: 5711
		internal const int ArityVarying = -1;

		// Token: 0x04001650 RID: 5712
		private readonly OpType m_opType;
	}
}
