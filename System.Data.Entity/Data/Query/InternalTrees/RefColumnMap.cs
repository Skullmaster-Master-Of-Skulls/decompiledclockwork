using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000A3 RID: 163
	internal class RefColumnMap : ColumnMap
	{
		// Token: 0x06000A2C RID: 2604 RVA: 0x00036201 File Offset: 0x00034401
		internal RefColumnMap(TypeUsage type, string name, EntityIdentity entityIdentity) : base(type, name)
		{
			this.m_entityIdentity = entityIdentity;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00036212 File Offset: 0x00034412
		internal EntityIdentity EntityIdentity
		{
			get
			{
				return this.m_entityIdentity;
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0003621A File Offset: 0x0003441A
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00036224 File Offset: 0x00034424
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x040008BE RID: 2238
		private EntityIdentity m_entityIdentity;
	}
}
