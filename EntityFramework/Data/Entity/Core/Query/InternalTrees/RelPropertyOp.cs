using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200061A RID: 1562
	internal sealed class RelPropertyOp : ScalarOp
	{
		// Token: 0x06003D35 RID: 15669 RVA: 0x0011AF09 File Offset: 0x00119109
		private RelPropertyOp() : base(OpType.RelProperty)
		{
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x0011AF13 File Offset: 0x00119113
		internal RelPropertyOp(TypeUsage type, RelProperty property) : base(OpType.RelProperty, type)
		{
			this.m_property = property;
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06003D37 RID: 15671 RVA: 0x0011AF25 File Offset: 0x00119125
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06003D38 RID: 15672 RVA: 0x0011AF28 File Offset: 0x00119128
		public RelProperty PropertyInfo
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x0011AF30 File Offset: 0x00119130
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x0011AF3A File Offset: 0x0011913A
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04001722 RID: 5922
		private readonly RelProperty m_property;

		// Token: 0x04001723 RID: 5923
		internal static readonly RelPropertyOp Pattern = new RelPropertyOp();
	}
}
