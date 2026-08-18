using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000624 RID: 1572
	internal class SimpleEntityIdentity : EntityIdentity
	{
		// Token: 0x06003D5D RID: 15709 RVA: 0x0011B0BF File Offset: 0x001192BF
		internal SimpleEntityIdentity(EntitySet entitySet, SimpleColumnMap[] keyColumns) : base(keyColumns)
		{
			this.m_entitySet = entitySet;
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06003D5E RID: 15710 RVA: 0x0011B0CF File Offset: 0x001192CF
		internal EntitySet EntitySet
		{
			get
			{
				return this.m_entitySet;
			}
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x0011B0D8 File Offset: 0x001192D8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "[(ES={0}) (Keys={", new object[]
			{
				this.EntitySet.Name
			});
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

		// Token: 0x0400172E RID: 5934
		private readonly EntitySet m_entitySet;
	}
}
