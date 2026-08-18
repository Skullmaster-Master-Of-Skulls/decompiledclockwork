using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000623 RID: 1571
	internal class SimpleCollectionColumnMap : CollectionColumnMap
	{
		// Token: 0x06003D5A RID: 15706 RVA: 0x0011B09C File Offset: 0x0011929C
		internal SimpleCollectionColumnMap(TypeUsage type, string name, ColumnMap elementMap, SimpleColumnMap[] keys, SimpleColumnMap[] foreignKeys) : base(type, name, elementMap, keys, foreignKeys)
		{
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x0011B0AB File Offset: 0x001192AB
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x0011B0B5 File Offset: 0x001192B5
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}
	}
}
