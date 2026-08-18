using System;
using System.Collections.Generic;
using System.Text;
using OracleInternal.Common;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002E8 RID: 744
	public sealed class OracleLpSelectStatement : OracleLpStatement, IOracleLpColumnDescriptorContainer, IOracleLpNamedObjectContainer
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x0010C8F4 File Offset: 0x0010AAF4
		// (set) Token: 0x06001AFC RID: 6908 RVA: 0x0010C8FC File Offset: 0x0010AAFC
		internal OracleLpSubquery Subquery
		{
			get
			{
				return this.m_vSubquery;
			}
			set
			{
				this.m_vSubquery = value;
				if (this.m_vSubquery != null)
				{
					this.m_vSubquery.Parent = this;
				}
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x0010C91C File Offset: 0x0010AB1C
		public override OracleLpStatementType StatementType
		{
			get
			{
				return OracleLpStatementType.Select;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001AFE RID: 6910 RVA: 0x0010C920 File Offset: 0x0010AB20
		public List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				if (this.m_vColumnDescriptors == null)
				{
					this.Resolve();
				}
				return this.m_vColumnDescriptors;
			}
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0010C938 File Offset: 0x0010AB38
		internal OracleLpSelectStatement(OracleLpTextFragment text, IOracleMetadata odpContext) : base(text, odpContext)
		{
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0010C944 File Offset: 0x0010AB44
		public void Resolve()
		{
			if (this.m_vNamedObjectsReferences == null)
			{
				this.m_vNamedObjectsReferences = new List<OracleLpQteNamedObject>();
				this.RetrieveNamedObjectReferences(this);
				this.m_vColumnDescriptors = this.m_vSubquery.ColumnDescriptors;
			}
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0010C974 File Offset: 0x0010AB74
		public void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vSubquery.RetrieveNamedObjectReferences(this);
			if (this.m_vNamedObjectsReferences.Count == 0)
			{
				return;
			}
			List<OracleLpTable> list = new List<OracleLpTable>();
			foreach (OracleLpQteNamedObject oracleLpQteNamedObject in this.m_vNamedObjectsReferences)
			{
				list.Add(new OracleLpTable((oracleLpQteNamedObject.SchemaName == null) ? null : oracleLpQteNamedObject.SchemaName.DbName, (oracleLpQteNamedObject.ObjectName == null) ? null : oracleLpQteNamedObject.ObjectName.DbName, (oracleLpQteNamedObject.Dblink == null) ? null : oracleLpQteNamedObject.Dblink.ToString()));
			}
			List<OracleLpQteNamedObject>.Enumerator enumerator2 = this.m_vNamedObjectsReferences.GetEnumerator();
			enumerator2.MoveNext();
			foreach (OracleLpTableColumns tabCols in this.m_vODPContext.GetColumnInformation(list))
			{
				OracleLpQteNamedObject oracleLpQteNamedObject2 = enumerator2.Current;
				oracleLpQteNamedObject2.RetrieveColumnsInformation(tabCols);
				enumerator2.MoveNext();
			}
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0010CA9C File Offset: 0x0010AC9C
		internal override void ToString(StringBuilder sb)
		{
			base.ToString(sb);
			this.m_vSubquery.ToString(sb);
			sb.Append('\n');
			sb.Append("Column descriptors\n");
			sb.Append("==================\n");
			this.ColumnDescriptors.ForEach(delegate(OracleLpColumnDescriptor t)
			{
				sb.Append(t.ToString());
			});
			sb.Append("\n\n");
		}

		// Token: 0x04001CF2 RID: 7410
		private OracleLpSubquery m_vSubquery;

		// Token: 0x04001CF3 RID: 7411
		private List<OracleLpColumnDescriptor> m_vColumnDescriptors;
	}
}
