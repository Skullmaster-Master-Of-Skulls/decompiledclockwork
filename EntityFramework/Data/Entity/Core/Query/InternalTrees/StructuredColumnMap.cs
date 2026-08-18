using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005D8 RID: 1496
	internal abstract class StructuredColumnMap : ColumnMap
	{
		// Token: 0x06003BC6 RID: 15302 RVA: 0x00118661 File Offset: 0x00116861
		internal StructuredColumnMap(TypeUsage type, string name, ColumnMap[] properties) : base(type, name)
		{
			this.m_properties = properties;
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x00118672 File Offset: 0x00116872
		internal virtual SimpleColumnMap NullSentinel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06003BC8 RID: 15304 RVA: 0x00118675 File Offset: 0x00116875
		internal ColumnMap[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x00118680 File Offset: 0x00116880
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			stringBuilder.Append("{");
			foreach (ColumnMap columnMap in this.Properties)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", new object[]
				{
					text,
					columnMap
				});
				text = ",";
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0400166E RID: 5742
		private readonly ColumnMap[] m_properties;
	}
}
