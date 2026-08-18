using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x0200009D RID: 157
	internal class RecordColumnMap : StructuredColumnMap
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x00035EFB File Offset: 0x000340FB
		internal RecordColumnMap(TypeUsage type, string name, ColumnMap[] properties, SimpleColumnMap nullSentinel) : base(type, name, properties)
		{
			this.m_nullSentinel = nullSentinel;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00035F0E File Offset: 0x0003410E
		internal override SimpleColumnMap NullSentinel
		{
			get
			{
				return this.m_nullSentinel;
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00035F16 File Offset: 0x00034116
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00035F20 File Offset: 0x00034120
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x040008B6 RID: 2230
		private SimpleColumnMap m_nullSentinel;
	}
}
