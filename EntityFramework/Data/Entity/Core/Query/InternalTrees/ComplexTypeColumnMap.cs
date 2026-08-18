using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005DA RID: 1498
	internal class ComplexTypeColumnMap : TypedColumnMap
	{
		// Token: 0x06003BCB RID: 15307 RVA: 0x0011870B File Offset: 0x0011690B
		internal ComplexTypeColumnMap(TypeUsage type, string name, ColumnMap[] properties, SimpleColumnMap nullSentinel) : base(type, name, properties)
		{
			this.m_nullSentinel = nullSentinel;
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06003BCC RID: 15308 RVA: 0x0011871E File Offset: 0x0011691E
		internal override SimpleColumnMap NullSentinel
		{
			get
			{
				return this.m_nullSentinel;
			}
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x00118726 File Offset: 0x00116926
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x00118730 File Offset: 0x00116930
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x0011873C File Offset: 0x0011693C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "C{0}", new object[]
			{
				base.ToString()
			});
		}

		// Token: 0x0400166F RID: 5743
		private readonly SimpleColumnMap m_nullSentinel;
	}
}
