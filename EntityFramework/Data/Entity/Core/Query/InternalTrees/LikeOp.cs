using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000601 RID: 1537
	internal sealed class LikeOp : ScalarOp
	{
		// Token: 0x06003CA5 RID: 15525 RVA: 0x001194B9 File Offset: 0x001176B9
		internal LikeOp(TypeUsage boolType) : base(OpType.Like, boolType)
		{
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x001194C4 File Offset: 0x001176C4
		private LikeOp() : base(OpType.Like)
		{
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06003CA7 RID: 15527 RVA: 0x001194CE File Offset: 0x001176CE
		internal override int Arity
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x001194D1 File Offset: 0x001176D1
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x001194DB File Offset: 0x001176DB
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016B3 RID: 5811
		internal static readonly LikeOp Pattern = new LikeOp();
	}
}
