using System;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x0200009C RID: 156
	internal abstract class StructuredColumnMap : ColumnMap
	{
		// Token: 0x06000A0C RID: 2572 RVA: 0x00035E6B File Offset: 0x0003406B
		internal StructuredColumnMap(TypeUsage type, string name, ColumnMap[] properties) : base(type, name)
		{
			this.m_properties = properties;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00006174 File Offset: 0x00004374
		internal virtual SimpleColumnMap NullSentinel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00035E7C File Offset: 0x0003407C
		internal ColumnMap[] Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00035E84 File Offset: 0x00034084
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

		// Token: 0x040008B5 RID: 2229
		private readonly ColumnMap[] m_properties;
	}
}
