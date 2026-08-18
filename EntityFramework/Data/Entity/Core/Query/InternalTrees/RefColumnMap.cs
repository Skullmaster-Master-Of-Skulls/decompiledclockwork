using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000617 RID: 1559
	internal class RefColumnMap : ColumnMap
	{
		// Token: 0x06003D23 RID: 15651 RVA: 0x0011ADB6 File Offset: 0x00118FB6
		internal RefColumnMap(TypeUsage type, string name, EntityIdentity entityIdentity) : base(type, name)
		{
			this.m_entityIdentity = entityIdentity;
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06003D24 RID: 15652 RVA: 0x0011ADC7 File Offset: 0x00118FC7
		internal EntityIdentity EntityIdentity
		{
			get
			{
				return this.m_entityIdentity;
			}
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x0011ADCF File Offset: 0x00118FCF
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x0011ADD9 File Offset: 0x00118FD9
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x0400171C RID: 5916
		private readonly EntityIdentity m_entityIdentity;
	}
}
