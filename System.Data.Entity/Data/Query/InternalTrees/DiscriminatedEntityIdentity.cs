using System;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000A9 RID: 169
	internal class DiscriminatedEntityIdentity : EntityIdentity
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x000363E3 File Offset: 0x000345E3
		internal DiscriminatedEntityIdentity(SimpleColumnMap entitySetColumn, EntitySet[] entitySetMap, SimpleColumnMap[] keyColumns) : base(keyColumns)
		{
			this.m_entitySetColumn = entitySetColumn;
			this.m_entitySetMap = entitySetMap;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x000363FA File Offset: 0x000345FA
		internal SimpleColumnMap EntitySetColumnMap
		{
			get
			{
				return this.m_entitySetColumn;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00036402 File Offset: 0x00034602
		internal EntitySet[] EntitySetMap
		{
			get
			{
				return this.m_entitySetMap;
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0003640C File Offset: 0x0003460C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "[(Keys={", new object[0]);
			foreach (SimpleColumnMap simpleColumnMap in base.Keys)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", new object[]
				{
					text,
					simpleColumnMap
				});
				text = ",";
			}
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "})]", new object[0]);
			return stringBuilder.ToString();
		}

		// Token: 0x040008C6 RID: 2246
		private SimpleColumnMap m_entitySetColumn;

		// Token: 0x040008C7 RID: 2247
		private EntitySet[] m_entitySetMap;
	}
}
