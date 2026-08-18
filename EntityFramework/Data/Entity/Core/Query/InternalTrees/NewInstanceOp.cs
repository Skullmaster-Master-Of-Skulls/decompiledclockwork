using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000606 RID: 1542
	internal sealed class NewInstanceOp : ScalarOp
	{
		// Token: 0x06003CBD RID: 15549 RVA: 0x0011968C File Offset: 0x0011788C
		internal NewInstanceOp(TypeUsage type) : base(OpType.NewInstance, type)
		{
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x00119697 File Offset: 0x00117897
		private NewInstanceOp() : base(OpType.NewInstance)
		{
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x001196A1 File Offset: 0x001178A1
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003CC0 RID: 15552 RVA: 0x001196AB File Offset: 0x001178AB
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040016BB RID: 5819
		internal static readonly NewInstanceOp Pattern = new NewInstanceOp();
	}
}
