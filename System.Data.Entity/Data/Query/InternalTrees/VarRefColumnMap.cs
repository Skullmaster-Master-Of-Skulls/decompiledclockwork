using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000AA RID: 170
	internal class VarRefColumnMap : SimpleColumnMap
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00036499 File Offset: 0x00034699
		internal Var Var
		{
			get
			{
				return this.m_var;
			}
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000364A1 File Offset: 0x000346A1
		internal VarRefColumnMap(TypeUsage type, string name, Var v) : base(type, name)
		{
			this.m_var = v;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x000364B2 File Offset: 0x000346B2
		internal VarRefColumnMap(Var v) : this(v.Type, null, v)
		{
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000364C2 File Offset: 0x000346C2
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000364CC File Offset: 0x000346CC
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000364D6 File Offset: 0x000346D6
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

		// Token: 0x040008C8 RID: 2248
		private Var m_var;
	}
}
