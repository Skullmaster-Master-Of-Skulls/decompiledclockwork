using System;
using System.Data.Metadata.Edm;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000B1 RID: 177
	internal class ColumnMD
	{
		// Token: 0x06000B3C RID: 2876 RVA: 0x0003937B File Offset: 0x0003757B
		internal ColumnMD(TableMD table, string name, TypeUsage type)
		{
			this.m_name = name;
			this.m_type = type;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00039391 File Offset: 0x00037591
		internal ColumnMD(TableMD table, EdmMember property) : this(table, property.Name, property.TypeUsage)
		{
			this.m_property = property;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x000393AD File Offset: 0x000375AD
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x000393B5 File Offset: 0x000375B5
		internal TypeUsage Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000393BD File Offset: 0x000375BD
		internal bool IsNullable
		{
			get
			{
				return this.m_property == null || TypeSemantics.IsNullable(this.m_property);
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x000393AD File Offset: 0x000375AD
		public override string ToString()
		{
			return this.m_name;
		}

		// Token: 0x040008E2 RID: 2274
		private string m_name;

		// Token: 0x040008E3 RID: 2275
		private TypeUsage m_type;

		// Token: 0x040008E4 RID: 2276
		private EdmMember m_property;
	}
}
