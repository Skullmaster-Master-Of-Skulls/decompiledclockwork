using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002E4 RID: 740
	internal class OracleLpSelectTermSpecific : OracleLpSelectTerm
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x0010C100 File Offset: 0x0010A300
		// (set) Token: 0x06001ADA RID: 6874 RVA: 0x0010C108 File Offset: 0x0010A308
		public OracleLpExpression Expression
		{
			get
			{
				return this.m_vExpression;
			}
			set
			{
				this.m_vExpression = value;
				if (this.m_vExpression != null)
				{
					this.m_vExpression.Parent = this;
				}
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x0010C128 File Offset: 0x0010A328
		public OracleLpTextFragment ExpressionText
		{
			get
			{
				if (this.m_vExpression != null)
				{
					return this.m_vExpression.Text;
				}
				return null;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x0010C140 File Offset: 0x0010A340
		// (set) Token: 0x06001ADD RID: 6877 RVA: 0x0010C148 File Offset: 0x0010A348
		public OracleLpName Alias
		{
			get
			{
				return this.m_vAlias;
			}
			set
			{
				this.m_vAlias = value;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x0010C154 File Offset: 0x0010A354
		// (set) Token: 0x06001ADF RID: 6879 RVA: 0x0010C15C File Offset: 0x0010A35C
		public int BindRefStart
		{
			get
			{
				return this.m_vBindRefStart;
			}
			set
			{
				this.m_vBindRefStart = value;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x0010C168 File Offset: 0x0010A368
		// (set) Token: 0x06001AE1 RID: 6881 RVA: 0x0010C170 File Offset: 0x0010A370
		public int BindRefEnd
		{
			get
			{
				return this.m_vBindRefEnd;
			}
			set
			{
				this.m_vBindRefEnd = value;
			}
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0010C17C File Offset: 0x0010A37C
		public OracleLpSelectTermSpecific(OracleLpSelectClause sc) : base(sc)
		{
			this.m_vType = OracleLpSelectTermType.SPECIFIC;
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0010C19C File Offset: 0x0010A39C
		public override void Resolve()
		{
			OracleLpQueryBlock oracleLpQueryBlock = this.m_vParent.Parent as OracleLpQueryBlock;
			if (this.m_vColumnDescriptors == null)
			{
				this.m_vColumnDescriptors = new List<OracleLpColumnDescriptor>();
			}
			OracleLpColumnDescriptor oracleLpColumnDescriptor = null;
			if (this.m_vExpression == null)
			{
				oracleLpColumnDescriptor = new OracleLpColumnDescriptor();
				oracleLpColumnDescriptor.BaseColumnName = new OracleLpName(this.ExpressionText.Fragment);
				oracleLpColumnDescriptor.ColumnName = oracleLpColumnDescriptor.BaseColumnName;
				oracleLpColumnDescriptor.ColumnType = OracleLpColumnType.Expression;
			}
			else
			{
				switch (this.m_vExpression.ExpressionType)
				{
				case OracleLpExpressionType.SCALAR_SUBQUERY_EXPRESSION:
					oracleLpColumnDescriptor = new OracleLpColumnDescriptor((this.m_vExpression as OracleLpScalarSubqueryExpression).Subquery.ColumnDescriptors[0]);
					this.m_vBindRefStart = (this.m_vBindRefEnd = -1);
					oracleLpColumnDescriptor.ColumnName = new OracleLpName(this.m_vExpression.Text.Fragment);
					oracleLpColumnDescriptor.IsShowing = true;
					goto IL_2DD;
				case OracleLpExpressionType.SIMPLE_EXPRESSION:
					switch (((OracleLpSimpleExpression)this.m_vExpression).SimpleExpressionType)
					{
					case OracleLpSimpleExpressionType.COLUMN:
					{
						OracleLpColumn column = ((OracleLpColumnExpression)this.m_vExpression).Column;
						oracleLpColumnDescriptor = oracleLpQueryBlock.FromClause.FindColumn(column.SchemaName, column.ParentObjectName, column.Name);
						goto IL_2DD;
					}
					case OracleLpSimpleExpressionType.PSEUDOCOLUMN:
					{
						oracleLpColumnDescriptor = new OracleLpColumnDescriptor();
						oracleLpColumnDescriptor.ColumnType = OracleLpColumnType.PseudoColumn;
						OracleLpPseudoColumnExpression oracleLpPseudoColumnExpression = (OracleLpPseudoColumnExpression)this.m_vExpression;
						switch (oracleLpPseudoColumnExpression.PseudoColumnExpressionType)
						{
						case OracleLpPseudoColumnExpressionType.CONNECT_BY_ROOT:
						{
							OracleLpExpression vExpression = this.m_vExpression;
							this.m_vExpression = ((OracleLpPseudoColumnExpression)this.m_vExpression).Expression;
							this.Resolve();
							this.m_vExpression = vExpression;
							oracleLpColumnDescriptor = this.m_vColumnDescriptors[this.m_vColumnDescriptors.Count - 1];
							oracleLpColumnDescriptor.ColumnType = OracleLpColumnType.PseudoColumn;
							oracleLpColumnDescriptor.PseudoColumnType = OracleLpPseudoColumnType.CONNECT_BY_ROOT;
							oracleLpColumnDescriptor = null;
							break;
						}
						case OracleLpPseudoColumnExpressionType.ROWNUM:
							oracleLpColumnDescriptor.BaseColumnName = new OracleLpName("ROWNUM");
							oracleLpColumnDescriptor.ColumnName = oracleLpColumnDescriptor.BaseColumnName;
							oracleLpColumnDescriptor.PseudoColumnType = OracleLpPseudoColumnType.ROWNUM;
							break;
						case OracleLpPseudoColumnExpressionType.ROWID:
							oracleLpColumnDescriptor.BaseColumnName = new OracleLpName("ROWID");
							oracleLpColumnDescriptor.ColumnName = oracleLpColumnDescriptor.BaseColumnName;
							oracleLpColumnDescriptor.PseudoColumnType = OracleLpPseudoColumnType.ROWID;
							break;
						}
						OracleLpQteNamedObject oracleLpQteNamedObject = oracleLpQueryBlock.FromClause.FindNamedObject(oracleLpPseudoColumnExpression.SchemaName, oracleLpPseudoColumnExpression.ParentObjectName);
						if (oracleLpQteNamedObject != null)
						{
							oracleLpColumnDescriptor.BaseTableName = oracleLpQteNamedObject.ObjectName;
							oracleLpColumnDescriptor.BaseSchemaName = oracleLpQteNamedObject.SchemaName;
							goto IL_2DD;
						}
						goto IL_2DD;
					}
					case OracleLpSimpleExpressionType.CONSTANT:
					{
						oracleLpColumnDescriptor = new OracleLpColumnDescriptor();
						OracleLpConstantExpression oracleLpConstantExpression = (OracleLpConstantExpression)this.m_vExpression;
						object expressionValue = oracleLpConstantExpression.ExpressionValue;
						oracleLpColumnDescriptor.BaseColumnName = new OracleLpName(expressionValue.ToString());
						oracleLpColumnDescriptor.ColumnName = oracleLpColumnDescriptor.BaseColumnName;
						oracleLpColumnDescriptor.ColumnType = OracleLpColumnType.Constant;
						goto IL_2DD;
					}
					case OracleLpSimpleExpressionType.SEQUENCE:
					case OracleLpSimpleExpressionType.NULL:
						goto IL_2DD;
					default:
						goto IL_2DD;
					}
					break;
				}
				oracleLpColumnDescriptor = new OracleLpColumnDescriptor();
				oracleLpColumnDescriptor.BaseColumnName = new OracleLpName(this.m_vExpression.ToString());
				oracleLpColumnDescriptor.ColumnName = oracleLpColumnDescriptor.BaseColumnName;
				oracleLpColumnDescriptor.ColumnType = OracleLpColumnType.Expression;
			}
			IL_2DD:
			if (oracleLpColumnDescriptor != null)
			{
				if (this.m_vAlias != null)
				{
					oracleLpColumnDescriptor.ColumnName = this.m_vAlias;
				}
				if (this.m_vBindRefEnd != this.m_vBindRefStart)
				{
					OracleLpSelectStatement oracleLpSelectStatement = base.TopElement as OracleLpSelectStatement;
					for (int i = this.m_vBindRefStart; i < this.m_vBindRefEnd; i++)
					{
						oracleLpColumnDescriptor.AddBindReference(oracleLpSelectStatement.BindParameters[i]);
					}
				}
				this.m_vColumnDescriptors.Add(oracleLpColumnDescriptor);
			}
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0010C4F4 File Offset: 0x0010A6F4
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("Alias: ");
			sb.Append((this.m_vAlias == null) ? "none" : (this.m_vAlias.DbName ?? "none"));
			sb.Append("  Expression: ");
			if (this.m_vExpression != null)
			{
				this.m_vExpression.ToString(sb);
			}
			else
			{
				sb.Append(this.m_vExpression.Text.Fragment);
			}
			sb.Append('\n');
		}

		// Token: 0x04001CE6 RID: 7398
		protected OracleLpExpression m_vExpression;

		// Token: 0x04001CE7 RID: 7399
		protected OracleLpName m_vAlias;

		// Token: 0x04001CE8 RID: 7400
		protected int m_vBindRefStart = -1;

		// Token: 0x04001CE9 RID: 7401
		protected int m_vBindRefEnd = -1;
	}
}
