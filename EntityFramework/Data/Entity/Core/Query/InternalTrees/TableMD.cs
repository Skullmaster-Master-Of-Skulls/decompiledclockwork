using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.PlanCompiler;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200062D RID: 1581
	internal class TableMD
	{
		// Token: 0x06003D88 RID: 15752 RVA: 0x0011B46D File Offset: 0x0011966D
		private TableMD(EntitySetBase extent)
		{
			this.m_columns = new List<ColumnMD>();
			this.m_keys = new List<ColumnMD>();
			this.m_extent = extent;
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x0011B492 File Offset: 0x00119692
		internal TableMD(TypeUsage type, EntitySetBase extent) : this(extent)
		{
			this.m_columns.Add(new ColumnMD("element", type));
			this.m_flattened = !TypeUtils.IsStructuredType(type);
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x0011B4C0 File Offset: 0x001196C0
		internal TableMD(IEnumerable<EdmProperty> properties, IEnumerable<EdmMember> keyProperties, EntitySetBase extent) : this(extent)
		{
			Dictionary<string, ColumnMD> dictionary = new Dictionary<string, ColumnMD>();
			this.m_flattened = true;
			foreach (EdmProperty edmProperty in properties)
			{
				ColumnMD columnMD = new ColumnMD(edmProperty);
				this.m_columns.Add(columnMD);
				dictionary[edmProperty.Name] = columnMD;
			}
			foreach (EdmMember edmMember in keyProperties)
			{
				ColumnMD item;
				if (dictionary.TryGetValue(edmMember.Name, out item))
				{
					this.m_keys.Add(item);
				}
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06003D8B RID: 15755 RVA: 0x0011B58C File Offset: 0x0011978C
		internal EntitySetBase Extent
		{
			get
			{
				return this.m_extent;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x0011B594 File Offset: 0x00119794
		internal List<ColumnMD> Columns
		{
			get
			{
				return this.m_columns;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06003D8D RID: 15757 RVA: 0x0011B59C File Offset: 0x0011979C
		internal List<ColumnMD> Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06003D8E RID: 15758 RVA: 0x0011B5A4 File Offset: 0x001197A4
		internal bool Flattened
		{
			get
			{
				return this.m_flattened;
			}
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x0011B5AC File Offset: 0x001197AC
		public override string ToString()
		{
			if (this.m_extent == null)
			{
				return "Transient";
			}
			return this.m_extent.Name;
		}

		// Token: 0x0400173F RID: 5951
		private readonly List<ColumnMD> m_columns;

		// Token: 0x04001740 RID: 5952
		private readonly List<ColumnMD> m_keys;

		// Token: 0x04001741 RID: 5953
		private readonly EntitySetBase m_extent;

		// Token: 0x04001742 RID: 5954
		private readonly bool m_flattened;
	}
}
