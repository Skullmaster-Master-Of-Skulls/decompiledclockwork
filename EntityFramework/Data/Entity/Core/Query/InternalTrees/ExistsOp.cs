using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005F0 RID: 1520
	internal sealed class ExistsOp : ScalarOp
	{
		// Token: 0x06003C37 RID: 15415 RVA: 0x00118D3D File Offset: 0x00116F3D
		internal ExistsOp(TypeUsage type) : base(OpType.Exists, type)
		{
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x00118D48 File Offset: 0x00116F48
		private ExistsOp() : base(OpType.Exists)
		{
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06003C39 RID: 15417 RVA: 0x00118D52 File Offset: 0x00116F52
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x00118D55 File Offset: 0x00116F55
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x00118D5F File Offset: 0x00116F5F
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001691 RID: 5777
		internal static readonly ExistsOp Pattern = new ExistsOp();
	}
}
