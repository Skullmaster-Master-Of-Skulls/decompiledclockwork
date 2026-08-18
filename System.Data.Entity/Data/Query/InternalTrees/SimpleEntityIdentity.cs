using System;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000A8 RID: 168
	internal class SimpleEntityIdentity : EntityIdentity
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x0003632D File Offset: 0x0003452D
		internal SimpleEntityIdentity(EntitySet entitySet, SimpleColumnMap[] keyColumns) : base(keyColumns)
		{
			this.m_entitySet = entitySet;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0003633D File Offset: 0x0003453D
		internal EntitySet EntitySet
		{
			get
			{
				return this.m_entitySet;
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00036348 File Offset: 0x00034548
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

		// Token: 0x040008C5 RID: 2245
		private EntitySet m_entitySet;
	}
}
