using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001D5 RID: 469
	internal class OracleLpFromClauseAnsi : OracleLpFromClauseBase
	{
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x000C3F1C File Offset: 0x000C211C
		protected List<OracleLpTablePrimary> TablePrimaryList
		{
			get
			{
				if (this.m_vTablePrimaryList == null)
				{
					this.m_vTablePrimaryList = new List<OracleLpTablePrimary>();
					foreach (OracleLpStatementDataContainer oracleLpStatementDataContainer in this.Terms)
					{
						OracleLpTableReferenceAnsi oracleLpTableReferenceAnsi = (OracleLpTableReferenceAnsi)oracleLpStatementDataContainer;
						this.m_vTablePrimaryList.AddRange(oracleLpTableReferenceAnsi.TablePrimaryList);
					}
				}
				return this.m_vTablePrimaryList;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x000C3F98 File Offset: 0x000C2198
		public override List<OracleLpStatementDataContainer> Terms
		{
			get
			{
				return this.m_vTerms;
			}
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x000C3FA0 File Offset: 0x000C21A0
		public OracleLpFromClauseAnsi(OracleLpQueryBlock parent) : base(parent)
		{
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x000C3FB4 File Offset: 0x000C21B4
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

		// Token: 0x060011B9 RID: 4537 RVA: 0x000C4184 File Offset: 0x000C2384
		public override IOracleLpColumnDescriptorContainer FindColumnContainer(OracleLpName schema, OracleLpName parent)
		{
			OracleLpTablePrimary oracleLpTablePrimary = null;
			string text = (parent == null) ? null : parent.DbName;
			if (text == null)
			{
				return this.TablePrimaryList[0];
			}
			foreach (OracleLpTablePrimary oracleLpTablePrimary2 in this.TablePrimaryList)
			{
				if (oracleLpTablePrimary2.TablePrimaryType == OracleLpTablePrimaryType.TablePrimaryElement)
				{
					OracleLpTablePrimaryTablePrimaryElement oracleLpTablePrimaryTablePrimaryElement = (OracleLpTablePrimaryTablePrimaryElement)oracleLpTablePrimary2;
					string text2 = (oracleLpTablePrimaryTablePrimaryElement.Alias == null) ? null : oracleLpTablePrimaryTablePrimaryElement.Alias.DbName;
					if (text2 != null && text2 == text)
					{
						oracleLpTablePrimary = oracleLpTablePrimary2;
						break;
					}
					switch (oracleLpTablePrimaryTablePrimaryElement.TablePrimaryElement.TablePrimaryElementType)
					{
					case OracleLpTablePrimaryElementType.QueryTableExpression:
					case OracleLpTablePrimaryElementType.ContainersClause:
					{
						OracleLpQteNamedObject oracleLpQteNamedObject = ((OracleLpTablePrimaryElementQueryTableExpression)oracleLpTablePrimaryTablePrimaryElement.TablePrimaryElement).QueryTableExpression as OracleLpQteNamedObject;
						if (oracleLpQteNamedObject != null && ((oracleLpQteNamedObject.ObjectName == null) ? null : oracleLpQteNamedObject.ObjectName.DbName) == text && (schema == null || schema.DbName == ((oracleLpQteNamedObject.SchemaName == null) ? null : oracleLpQteNamedObject.SchemaName.DbName)))
						{
							oracleLpTablePrimary = oracleLpTablePrimary2;
						}
						break;
					}
					}
					if (oracleLpTablePrimary != null)
					{
						break;
					}
				}
			}
			return oracleLpTablePrimary;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x000C42C8 File Offset: 0x000C24C8
		public override OracleLpQteNamedObject FindNamedObject(OracleLpName schema, OracleLpName parent)
		{
			OracleLpTableReference oracleLpTableReference = (OracleLpTableReference)this.FindColumnContainer(schema, parent);
			if (oracleLpTableReference != null && oracleLpTableReference.QueryTableExpression.QueryTableExpressionType == OracleLpQueryTableExpressionType.NamedObject)
			{
				return (OracleLpQteNamedObject)oracleLpTableReference.QueryTableExpression;
			}
			return null;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x000C4304 File Offset: 0x000C2504
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

		// Token: 0x040013FD RID: 5117
		protected List<OracleLpTablePrimary> m_vTablePrimaryList;

		// Token: 0x040013FE RID: 5118
		protected List<OracleLpStatementDataContainer> m_vTerms = new List<OracleLpStatementDataContainer>();
	}
}
