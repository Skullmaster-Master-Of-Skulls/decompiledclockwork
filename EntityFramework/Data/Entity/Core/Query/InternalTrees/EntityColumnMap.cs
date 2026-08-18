using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005ED RID: 1517
	internal class EntityColumnMap : TypedColumnMap
	{
		// Token: 0x06003C28 RID: 15400 RVA: 0x00118C5C File Offset: 0x00116E5C
		internal EntityColumnMap(TypeUsage type, string name, ColumnMap[] properties, EntityIdentity entityIdentity) : base(type, name, properties)
		{
			this.m_entityIdentity = entityIdentity;
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06003C29 RID: 15401 RVA: 0x00118C6F File Offset: 0x00116E6F
		internal EntityIdentity EntityIdentity
		{
			get
			{
				return this.m_entityIdentity;
			}
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x00118C77 File Offset: 0x00116E77
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x00118C81 File Offset: 0x00116E81
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x00118C8C File Offset: 0x00116E8C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "E{0}", new object[]
			{
				base.ToString()
			});
		}

		// Token: 0x0400168D RID: 5773
		private readonly EntityIdentity m_entityIdentity;
	}
}
