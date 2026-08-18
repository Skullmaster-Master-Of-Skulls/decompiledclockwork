using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000646 RID: 1606
	internal class VarRefColumnMap : SimpleColumnMap
	{
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06003EEE RID: 16110 RVA: 0x00120400 File Offset: 0x0011E600
		internal Var Var
		{
			get
			{
				return this.m_var;
			}
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x00120408 File Offset: 0x0011E608
		internal VarRefColumnMap(TypeUsage type, string name, Var v) : base(type, name)
		{
			this.m_var = v;
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x00120419 File Offset: 0x0011E619
		internal VarRefColumnMap(Var v) : this(v.Type, null, v)
		{
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x00120429 File Offset: 0x0011E629
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x00120433 File Offset: 0x0011E633
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x00120440 File Offset: 0x0011E640
		public override string ToString()
		{
			if (!base.IsNamed)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
				{
					this.m_var.Id
				});
			}
			return base.Name;
		}

		// Token: 0x04001785 RID: 6021
		private readonly Var m_var;
	}
}
