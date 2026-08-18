using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000F4 RID: 244
	internal sealed class IsOfOp : ScalarOp
	{
		// Token: 0x06000D0A RID: 3338 RVA: 0x0003CAD6 File Offset: 0x0003ACD6
		internal IsOfOp(TypeUsage isOfType, bool isOfOnly, TypeUsage type) : base(OpType.IsOf, type)
		{
			this.m_isOfType = isOfType;
			this.m_isOfOnly = isOfOnly;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0003CAEF File Offset: 0x0003ACEF
		private IsOfOp() : base(OpType.IsOf)
		{
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0003CAF9 File Offset: 0x0003ACF9
		internal TypeUsage IsOfType
		{
			get
			{
				return this.m_isOfType;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0003CB01 File Offset: 0x0003AD01
		internal bool IsOfOnly
		{
			get
			{
				return this.m_isOfOnly;
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0003CB09 File Offset: 0x0003AD09
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0003CB13 File Offset: 0x0003AD13
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009A7 RID: 2471
		private TypeUsage m_isOfType;

		// Token: 0x040009A8 RID: 2472
		private bool m_isOfOnly;

		// Token: 0x040009A9 RID: 2473
		internal static readonly IsOfOp Pattern = new IsOfOp();
	}
}
