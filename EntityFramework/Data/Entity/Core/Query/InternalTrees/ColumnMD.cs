using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005D4 RID: 1492
	internal class ColumnMD
	{
		// Token: 0x06003BAE RID: 15278 RVA: 0x0011850B File Offset: 0x0011670B
		internal ColumnMD(string name, TypeUsage type)
		{
			this.m_name = name;
			this.m_type = type;
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x00118521 File Offset: 0x00116721
		internal ColumnMD(EdmMember property) : this(property.Name, property.TypeUsage)
		{
			this.m_property = property;
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x0011853C File Offset: 0x0011673C
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06003BB1 RID: 15281 RVA: 0x00118544 File Offset: 0x00116744
		internal TypeUsage Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06003BB2 RID: 15282 RVA: 0x0011854C File Offset: 0x0011674C
		internal bool IsNullable
		{
			get
			{
				return this.m_property == null || TypeSemantics.IsNullable(this.m_property);
			}
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x00118563 File Offset: 0x00116763
		public override string ToString()
		{
			return this.m_name;
		}

		// Token: 0x04001664 RID: 5732
		private readonly string m_name;

		// Token: 0x04001665 RID: 5733
		private readonly TypeUsage m_type;

		// Token: 0x04001666 RID: 5734
		private readonly EdmMember m_property;
	}
}
