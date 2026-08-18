using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002CC RID: 716
	internal class OracleLpFromClause : OracleLpFromClauseBase
	{
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x0010A8F4 File Offset: 0x00108AF4
		protected List<OracleLpTableReference> TableReferences
		{
			get
			{
				if (this.m_vTableReferences == null)
				{
					this.m_vTableReferences = new List<OracleLpTableReference>();
					foreach (OracleLpStatementDataContainer oracleLpStatementDataContainer in this.Terms)
					{
						OracleLpFromListTerm oracleLpFromListTerm = (OracleLpFromListTerm)oracleLpStatementDataContainer;
						this.m_vTableReferences.Add(oracleLpFromListTerm.TableReference);
						if (oracleLpFromListTerm.Type == OracleLpFromListTermType.JoinClause)
						{
							foreach (OracleLpSpecificJoinClause oracleLpSpecificJoinClause in oracleLpFromListTerm.JoinClauses)
							{
								this.m_vTableReferences.Add(oracleLpSpecificJoinClause.TableReference);
							}
						}
					}
				}
				return this.m_vTableReferences;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x0010A9C8 File Offset: 0x00108BC8
		public override List<OracleLpStatementDataContainer> Terms
		{
			get
			{
				return this.m_vTerms;
			}
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0010A9D0 File Offset: 0x00108BD0
		public OracleLpFromClause(OracleLpStatementElement parent) : base(parent)
		{
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x0010A9E4 File Offset: 0x00108BE4
		public override OracleLpColumnDescriptor FindColumn(OracleLpName schema, OracleLpName parent, OracleLpName colName)
		{
			OracleLpColumnDescriptor oracleLpColumnDescriptor = null;
			string text = (parent == null) ? null : parent.DbName;
			string text2 = (schema == null) ? null : schema.DbName;
			string text3 = (colName == null) ? null : colName.DbName;
			OracleLpColumnDescriptor oracleLpColumnDescriptor3;
			if (text == null)
			{
				foreach (OracleLpStatementDataContainer oracleLpStatementDataContainer in this.Terms)
				{
					foreach (OracleLpColumnDescriptor oracleLpColumnDescriptor2 in oracleLpStatementDataContainer.ColumnDescriptors)
					{
						if (oracleLpColumnDescriptor2.ColumnName.DbName == text3)
						{
							if (oracleLpColumnDescriptor != null)
							{
								throw new OracleLpException(OracleLpExceptionType.AmbiguousDefinition, OracleLpExceptionError.AmbiguousColumn, string.Format(OracleLpErrorStrings.GetErrorString(OracleLpExceptionError.AmbiguousColumn), text3));
							}
							oracleLpColumnDescriptor = oracleLpColumnDescriptor2;
						}
					}
				}
				if (oracleLpColumnDescriptor == null)
				{
					oracleLpColumnDescriptor3 = new OracleLpColumnDescriptor();
					oracleLpColumnDescriptor3.BaseColumnName = colName;
					oracleLpColumnDescriptor3.ColumnName = colName;
				}
				else
				{
					oracleLpColumnDescriptor3 = new OracleLpColumnDescriptor(oracleLpColumnDescriptor);
					oracleLpColumnDescriptor3.IsShowing = true;
				}
			}
			else
			{
				IOracleLpColumnDescriptorContainer oracleLpColumnDescriptorContainer = this.FindColumnContainer(schema, parent);
				if (oracleLpColumnDescriptorContainer == null)
				{
					throw new OracleLpException(OracleLpExceptionType.MissingReference, OracleLpExceptionError.MissingTable_View_Query, string.Format(OracleLpErrorStrings.GetErrorString(OracleLpExceptionError.MissingTable_View_Query), (text2 == null) ? "*" : text2, text));
				}
				foreach (OracleLpColumnDescriptor oracleLpColumnDescriptor4 in oracleLpColumnDescriptorContainer.ColumnDescriptors)
				{
					if (oracleLpColumnDescriptor4.ColumnName.DbName == text3)
					{
						oracleLpColumnDescriptor = oracleLpColumnDescriptor4;
						break;
					}
				}
				if (oracleLpColumnDescriptor == null)
				{
					throw new OracleLpException(OracleLpExceptionType.MissingReference, OracleLpExceptionError.MissingColumnFromReference, string.Format(OracleLpErrorStrings.GetErrorString(OracleLpExceptionError.MissingColumnFromReference), text3, text));
				}
				oracleLpColumnDescriptor3 = new OracleLpColumnDescriptor(oracleLpColumnDescriptor);
				oracleLpColumnDescriptor3.IsShowing = true;
			}
			return oracleLpColumnDescriptor3;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0010ABB4 File Offset: 0x00108DB4
		public override IOracleLpColumnDescriptorContainer FindColumnContainer(OracleLpName schema, OracleLpName parent)
		{
			string text = (parent == null) ? null : parent.DbName;
			if (text == null)
			{
				return this.TableReferences[0];
			}
			OracleLpTableReference result = null;
			foreach (OracleLpTableReference oracleLpTableReference in this.TableReferences)
			{
				string text2 = (oracleLpTableReference.Alias == null) ? null : oracleLpTableReference.Alias.DbName;
				if (text2 != null && text2 == text)
				{
					result = oracleLpTableReference;
					break;
				}
				if (oracleLpTableReference.QueryTableExpression.QueryTableExpressionType == OracleLpQueryTableExpressionType.NamedObject)
				{
					OracleLpQteNamedObject oracleLpQteNamedObject = (OracleLpQteNamedObject)oracleLpTableReference.QueryTableExpression;
					if (((oracleLpQteNamedObject.ObjectName == null) ? null : oracleLpQteNamedObject.ObjectName.DbName) == text && (schema == null || schema.DbName == ((oracleLpQteNamedObject.SchemaName == null) ? null : oracleLpQteNamedObject.SchemaName.DbName)))
					{
						result = oracleLpTableReference;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0010ACB8 File Offset: 0x00108EB8
		public override OracleLpQteNamedObject FindNamedObject(OracleLpName schema, OracleLpName parent)
		{
			OracleLpTableReference oracleLpTableReference = (OracleLpTableReference)this.FindColumnContainer(schema, parent);
			if (oracleLpTableReference != null && oracleLpTableReference.QueryTableExpression.QueryTableExpressionType == OracleLpQueryTableExpressionType.NamedObject)
			{
				return (OracleLpQteNamedObject)oracleLpTableReference.QueryTableExpression;
			}
			return null;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0010ACF4 File Offset: 0x00108EF4
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("From List:\n");
			this.m_vTerms.ForEach(delegate(OracleLpStatementDataContainer ft)
			{
				ft.ToString(sb);
			});
		}

		// Token: 0x04001C82 RID: 7298
		protected List<OracleLpTableReference> m_vTableReferences;

		// Token: 0x04001C83 RID: 7299
		protected List<OracleLpStatementDataContainer> m_vTerms = new List<OracleLpStatementDataContainer>();
	}
}
