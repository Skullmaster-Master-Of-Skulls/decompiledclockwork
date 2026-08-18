using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000107 RID: 263
	internal sealed class ElementOp : ScalarOp
	{
		// Token: 0x06000D7B RID: 3451 RVA: 0x0003CF99 File Offset: 0x0003B199
		internal ElementOp(TypeUsage type) : base(OpType.Element, type)
		{
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0003CFA4 File Offset: 0x0003B1A4
		private ElementOp() : base(OpType.Element)
		{
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0003CFAE File Offset: 0x0003B1AE
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0003CFB8 File Offset: 0x0003B1B8
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009C6 RID: 2502
		internal static readonly ElementOp Pattern = new ElementOp();
	}
}
