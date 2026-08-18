using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000A2 RID: 162
	internal class EntityColumnMap : TypedColumnMap
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x000361A5 File Offset: 0x000343A5
		internal EntityColumnMap(TypeUsage type, string name, ColumnMap[] properties, EntityIdentity entityIdentity) : base(type, name, properties)
		{
			this.m_entityIdentity = entityIdentity;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x000361B8 File Offset: 0x000343B8
		internal EntityIdentity EntityIdentity
		{
			get
			{
				return this.m_entityIdentity;
			}
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000361C0 File Offset: 0x000343C0
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000361CA File Offset: 0x000343CA
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x000361D4 File Offset: 0x000343D4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "E{0}", new object[]
			{
				base.ToString()
			});
		}

		// Token: 0x040008BD RID: 2237
		private EntityIdentity m_entityIdentity;
	}
}
