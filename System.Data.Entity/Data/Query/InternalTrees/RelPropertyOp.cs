using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000FD RID: 253
	internal sealed class RelPropertyOp : ScalarOp
	{
		// Token: 0x06000D42 RID: 3394 RVA: 0x0003CD01 File Offset: 0x0003AF01
		private RelPropertyOp() : base(OpType.RelProperty)
		{
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0003CD0B File Offset: 0x0003AF0B
		internal RelPropertyOp(TypeUsage type, RelProperty property) : base(OpType.RelProperty, type)
		{
			this.m_property = property;
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00017938 File Offset: 0x00015B38
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0003CD1D File Offset: 0x0003AF1D
		public RelProperty PropertyInfo
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0003CD25 File Offset: 0x0003AF25
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0003CD2F File Offset: 0x0003AF2F
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x040009B7 RID: 2487
		private readonly RelProperty m_property;

		// Token: 0x040009B8 RID: 2488
		internal static readonly RelPropertyOp Pattern = new RelPropertyOp();
	}
}
