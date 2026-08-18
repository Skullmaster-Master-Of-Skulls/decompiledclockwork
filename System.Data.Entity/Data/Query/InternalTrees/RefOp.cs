using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000105 RID: 261
	internal sealed class RefOp : ScalarOp
	{
		// Token: 0x06000D6E RID: 3438 RVA: 0x0003CF20 File Offset: 0x0003B120
		internal RefOp(EntitySet entitySet, TypeUsage type) : base(OpType.Ref, type)
		{
			this.m_entitySet = entitySet;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0003CF32 File Offset: 0x0003B132
		private RefOp() : base(OpType.Ref)
		{
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0003CF3C File Offset: 0x0003B13C
		internal EntitySet EntitySet
		{
			get
			{
				return this.m_entitySet;
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0003CF44 File Offset: 0x0003B144
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0003CF4E File Offset: 0x0003B14E
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009C3 RID: 2499
		private EntitySet m_entitySet;

		// Token: 0x040009C4 RID: 2500
		internal static readonly RefOp Pattern = new RefOp();
	}
}
