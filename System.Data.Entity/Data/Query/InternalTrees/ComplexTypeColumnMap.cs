using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000A1 RID: 161
	internal class ComplexTypeColumnMap : TypedColumnMap
	{
		// Token: 0x06000A22 RID: 2594 RVA: 0x00036148 File Offset: 0x00034348
		internal ComplexTypeColumnMap(TypeUsage type, string name, ColumnMap[] properties, SimpleColumnMap nullSentinel) : base(type, name, properties)
		{
			this.m_nullSentinel = nullSentinel;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0003615B File Offset: 0x0003435B
		internal override SimpleColumnMap NullSentinel
		{
			get
			{
				return this.m_nullSentinel;
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00036163 File Offset: 0x00034363
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0003616D File Offset: 0x0003436D
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00036178 File Offset: 0x00034378
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "C{0}", new object[]
			{
				base.ToString()
			});
		}

		// Token: 0x040008BC RID: 2236
		private SimpleColumnMap m_nullSentinel;
	}
}
