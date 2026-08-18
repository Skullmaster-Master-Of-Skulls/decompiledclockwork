using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000616 RID: 1558
	internal class RecordColumnMap : StructuredColumnMap
	{
		// Token: 0x06003D1F RID: 15647 RVA: 0x0011AD87 File Offset: 0x00118F87
		internal RecordColumnMap(TypeUsage type, string name, ColumnMap[] properties, SimpleColumnMap nullSentinel) : base(type, name, properties)
		{
			this.m_nullSentinel = nullSentinel;
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06003D20 RID: 15648 RVA: 0x0011AD9A File Offset: 0x00118F9A
		internal override SimpleColumnMap NullSentinel
		{
			get
			{
				return this.m_nullSentinel;
			}
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x0011ADA2 File Offset: 0x00118FA2
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x0011ADAC File Offset: 0x00118FAC
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x0400171B RID: 5915
		private readonly SimpleColumnMap m_nullSentinel;
	}
}
