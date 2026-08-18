using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005E8 RID: 1512
	internal class DiscriminatedEntityIdentity : EntityIdentity
	{
		// Token: 0x06003C0C RID: 15372 RVA: 0x00118A96 File Offset: 0x00116C96
		internal DiscriminatedEntityIdentity(SimpleColumnMap entitySetColumn, EntitySet[] entitySetMap, SimpleColumnMap[] keyColumns) : base(keyColumns)
		{
			this.m_entitySetColumn = entitySetColumn;
			this.m_entitySetMap = entitySetMap;
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06003C0D RID: 15373 RVA: 0x00118AAD File Offset: 0x00116CAD
		internal SimpleColumnMap EntitySetColumnMap
		{
			get
			{
				return this.m_entitySetColumn;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06003C0E RID: 15374 RVA: 0x00118AB5 File Offset: 0x00116CB5
		internal EntitySet[] EntitySetMap
		{
			get
			{
				return this.m_entitySetMap;
			}
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x00118AC0 File Offset: 0x00116CC0
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

		// Token: 0x04001683 RID: 5763
		private readonly SimpleColumnMap m_entitySetColumn;

		// Token: 0x04001684 RID: 5764
		private readonly EntitySet[] m_entitySetMap;
	}
}
