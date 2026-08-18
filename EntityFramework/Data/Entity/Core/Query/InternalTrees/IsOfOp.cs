using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005FE RID: 1534
	internal sealed class IsOfOp : ScalarOp
	{
		// Token: 0x06003C8F RID: 15503 RVA: 0x001192DA File Offset: 0x001174DA
		internal IsOfOp(TypeUsage isOfType, bool isOfOnly, TypeUsage type) : base(OpType.IsOf, type)
		{
			this.m_isOfType = isOfType;
			this.m_isOfOnly = isOfOnly;
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x001192F3 File Offset: 0x001174F3
		private IsOfOp() : base(OpType.IsOf)
		{
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06003C91 RID: 15505 RVA: 0x001192FD File Offset: 0x001174FD
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06003C92 RID: 15506 RVA: 0x00119300 File Offset: 0x00117500
		internal TypeUsage IsOfType
		{
			get
			{
				return this.m_isOfType;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06003C93 RID: 15507 RVA: 0x00119308 File Offset: 0x00117508
		internal bool IsOfOnly
		{
			get
			{
				return this.m_isOfOnly;
			}
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x00119310 File Offset: 0x00117510
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x0011931A File Offset: 0x0011751A
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016AC RID: 5804
		private readonly TypeUsage m_isOfType;

		// Token: 0x040016AD RID: 5805
		private readonly bool m_isOfOnly;

		// Token: 0x040016AE RID: 5806
		internal static readonly IsOfOp Pattern = new IsOfOp();
	}
}
