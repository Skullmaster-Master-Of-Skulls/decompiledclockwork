using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000BB RID: 187
	internal abstract class Op
	{
		// Token: 0x06000BE7 RID: 3047 RVA: 0x0003BCD1 File Offset: 0x00039ED1
		internal Op(OpType opType)
		{
			this.m_opType = opType;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0003BCE0 File Offset: 0x00039EE0
		internal OpType OpType
		{
			get
			{
				return this.m_opType;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0003BCE8 File Offset: 0x00039EE8
		internal virtual int Arity
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x000173E2 File Offset: 0x000155E2
		internal virtual bool IsScalarOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x000173E2 File Offset: 0x000155E2
		internal virtual bool IsRulePatternOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x000173E2 File Offset: 0x000155E2
		internal virtual bool IsRelOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x000173E2 File Offset: 0x000155E2
		internal virtual bool IsAncillaryOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x000173E2 File Offset: 0x000155E2
		internal virtual bool IsPhysicalOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x000173E2 File Offset: 0x000155E2
		internal virtual bool IsEquivalent(Op other)
		{
			return false;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00006174 File Offset: 0x00004374
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x0003BCEB File Offset: 0x00039EEB
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

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0003BCF2 File Offset: 0x00039EF2
		[DebuggerNonUserCode]
		internal virtual void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0003BCFC File Offset: 0x00039EFC
		[DebuggerNonUserCode]
		internal virtual TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x0400094D RID: 2381
		private OpType m_opType;

		// Token: 0x0400094E RID: 2382
		internal const int ArityVarying = -1;
	}
}
