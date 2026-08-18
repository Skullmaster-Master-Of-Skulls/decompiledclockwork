using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000A5 RID: 165
	internal class SimpleCollectionColumnMap : CollectionColumnMap
	{
		// Token: 0x06000A34 RID: 2612 RVA: 0x0003627B File Offset: 0x0003447B
		internal SimpleCollectionColumnMap(TypeUsage type, string name, ColumnMap elementMap, SimpleColumnMap[] keys, SimpleColumnMap[] foreignKeys) : base(type, name, elementMap, keys, foreignKeys)
		{
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0003628A File Offset: 0x0003448A
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00036294 File Offset: 0x00034494
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}
	}
}
