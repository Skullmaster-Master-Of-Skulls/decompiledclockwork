using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005F6 RID: 1526
	internal sealed class GetEntityRefOp : ScalarOp
	{
		// Token: 0x06003C62 RID: 15458 RVA: 0x00119106 File Offset: 0x00117306
		internal GetEntityRefOp(TypeUsage type) : base(OpType.GetEntityRef, type)
		{
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x00119111 File Offset: 0x00117311
		private GetEntityRefOp() : base(OpType.GetEntityRef)
		{
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06003C64 RID: 15460 RVA: 0x0011911B File Offset: 0x0011731B
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x0011911E File Offset: 0x0011731E
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x00119128 File Offset: 0x00117328
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016A1 RID: 5793
		internal static readonly GetEntityRefOp Pattern = new GetEntityRefOp();
	}
}
