using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002E5 RID: 741
	internal class OracleLpSelectTermAll : OracleLpSelectTerm
	{
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x0010C588 File Offset: 0x0010A788
		// (set) Token: 0x06001AE6 RID: 6886 RVA: 0x0010C590 File Offset: 0x0010A790
		public OracleLpName ParentObjectName
		{
			get
			{
				return this.m_vParentObjectName;
			}
			set
			{
				this.m_vParentObjectName = value;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x0010C59C File Offset: 0x0010A79C
		// (set) Token: 0x06001AE8 RID: 6888 RVA: 0x0010C5A4 File Offset: 0x0010A7A4
		public OracleLpName SchemaName
		{
			get
			{
				return this.m_vSchemaName;
			}
			set
			{
				this.m_vSchemaName = value;
			}
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0010C5B0 File Offset: 0x0010A7B0
		public OracleLpSelectTermAll(OracleLpSelectClause sc) : base(sc)
		{
			this.m_vType = OracleLpSelectTermType.ALL;
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0010C5C0 File Offset: 0x0010A7C0
		public override void Resolve()
		{
			OracleLpQueryBlock oracleLpQueryBlock = this.m_vParent.Parent as OracleLpQueryBlock;
			this.m_vColumnDescriptors = new List<OracleLpColumnDescriptor>();
			if (this.m_vParentObjectName == null)
			{
				using (List<OracleLpStatementDataContainer>.Enumerator enumerator = oracleLpQueryBlock.FromClause.Terms.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						OracleLpStatementDataContainer oracleLpStatementDataContainer = enumerator.Current;
						oracleLpStatementDataContainer.ColumnDescriptors.ForEach(delegate(OracleLpColumnDescriptor cd)
						{
							if (cd.IsShowing)
							{
								this.m_vColumnDescriptors.Add(cd);
							}
						});
					}
					return;
				}
			}
			IOracleLpColumnDescriptorContainer oracleLpColumnDescriptorContainer = oracleLpQueryBlock.FromClause.FindColumnContainer(this.m_vSchemaName, this.m_vParentObjectName);
			if (oracleLpColumnDescriptorContainer == null)
			{
				throw new OracleLpException(OracleLpExceptionType.MissingReference, OracleLpExceptionError.MissingTable_View_Query, string.Format(OracleLpErrorStrings.GetErrorString(OracleLpExceptionError.MissingTable_View_Query), (this.m_vSchemaName == null) ? "*" : (this.m_vSchemaName.DbName ?? "*"), this.m_vParentObjectName.DbName));
			}
			oracleLpColumnDescriptorContainer.ColumnDescriptors.ForEach(delegate(OracleLpColumnDescriptor cd)
			{
				if (cd.IsShowing)
				{
					this.m_vColumnDescriptors.Add(cd);
				}
			});
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x0010C6D8 File Offset: 0x0010A8D8
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("All columns (*)");
			sb.Append("  Parent Object: ");
			sb.Append((this.m_vParentObjectName == null) ? "none" : (this.m_vParentObjectName.DbName ?? "none"));
			sb.Append("  Schema: ");
			sb.Append((this.m_vSchemaName == null) ? "none" : (this.m_vSchemaName.DbName ?? "none"));
			sb.Append('\n');
		}

		// Token: 0x04001CEA RID: 7402
		protected OracleLpName m_vParentObjectName;

		// Token: 0x04001CEB RID: 7403
		protected OracleLpName m_vSchemaName;
	}
}
