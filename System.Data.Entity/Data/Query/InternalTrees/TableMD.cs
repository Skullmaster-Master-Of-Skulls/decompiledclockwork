using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Data.Query.PlanCompiler;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000B0 RID: 176
	internal class TableMD
	{
		// Token: 0x06000B34 RID: 2868 RVA: 0x00039220 File Offset: 0x00037420
		private TableMD(EntitySetBase extent)
		{
			this.m_columns = new List<ColumnMD>();
			this.m_keys = new List<ColumnMD>();
			this.m_extent = extent;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00039245 File Offset: 0x00037445
		internal TableMD(TypeUsage type, EntitySetBase extent) : this(extent)
		{
			this.m_columns.Add(new ColumnMD(this, "element", type));
			this.m_flattened = !TypeUtils.IsStructuredType(type);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00039274 File Offset: 0x00037474
		internal TableMD(IEnumerable<EdmProperty> properties, IEnumerable<EdmMember> keyProperties, EntitySetBase extent) : this(extent)
		{
			Dictionary<string, ColumnMD> dictionary = new Dictionary<string, ColumnMD>();
			this.m_flattened = true;
			foreach (EdmProperty edmProperty in properties)
			{
				ColumnMD columnMD = new ColumnMD(this, edmProperty);
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

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00039340 File Offset: 0x00037540
		internal EntitySetBase Extent
		{
			get
			{
				return this.m_extent;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00039348 File Offset: 0x00037548
		internal List<ColumnMD> Columns
		{
			get
			{
				return this.m_columns;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00039350 File Offset: 0x00037550
		internal List<ColumnMD> Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00039358 File Offset: 0x00037558
		internal bool Flattened
		{
			get
			{
				return this.m_flattened;
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00039360 File Offset: 0x00037560
		public override string ToString()
		{
			if (this.m_extent == null)
			{
				return "Transient";
			}
			return this.m_extent.Name;
		}

		// Token: 0x040008DE RID: 2270
		private List<ColumnMD> m_columns;

		// Token: 0x040008DF RID: 2271
		private List<ColumnMD> m_keys;

		// Token: 0x040008E0 RID: 2272
		private EntitySetBase m_extent;

		// Token: 0x040008E1 RID: 2273
		private bool m_flattened;
	}
}
