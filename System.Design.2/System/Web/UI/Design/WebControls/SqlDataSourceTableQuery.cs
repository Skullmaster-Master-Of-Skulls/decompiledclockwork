using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200011B RID: 283
	internal sealed class SqlDataSourceTableQuery
	{
		// Token: 0x06000A4E RID: 2638 RVA: 0x00041FF1 File Offset: 0x000401F1
		public SqlDataSourceTableQuery(DesignerDataConnection designerDataConnection, DesignerDataTableBase designerDataTable)
		{
			this._designerDataConnection = designerDataConnection;
			this._designerDataTable = designerDataTable;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00042028 File Offset: 0x00040228
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x00042030 File Offset: 0x00040230
		public bool AsteriskField
		{
			get
			{
				return this._asteriskField;
			}
			set
			{
				this._asteriskField = value;
				if (value)
				{
					this.Fields.Clear();
				}
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00042047 File Offset: 0x00040247
		public DesignerDataConnection DesignerDataConnection
		{
			get
			{
				return this._designerDataConnection;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0004204F File Offset: 0x0004024F
		public DesignerDataTableBase DesignerDataTable
		{
			get
			{
				return this._designerDataTable;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00042057 File Offset: 0x00040257
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x0004205F File Offset: 0x0004025F
		public bool Distinct
		{
			get
			{
				return this._distinct;
			}
			set
			{
				this._distinct = value;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00042068 File Offset: 0x00040268
		public IList<DesignerDataColumn> Fields
		{
			get
			{
				return this._fields;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00042070 File Offset: 0x00040270
		public IList<SqlDataSourceFilterClause> FilterClauses
		{
			get
			{
				return this._filterClauses;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00042078 File Offset: 0x00040278
		public IList<SqlDataSourceOrderClause> OrderClauses
		{
			get
			{
				return this._orderClauses;
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00042080 File Offset: 0x00040280
		private void AppendWhereClauseParameter(StringBuilder commandText, SqlDataSourceColumnData columnData, string oldValuesFormatString)
		{
			string escapedName = columnData.EscapedName;
			string oldValueParameterPlaceHolder = columnData.GetOldValueParameterPlaceHolder(oldValuesFormatString);
			if (columnData.Column.Nullable)
			{
				commandText.Append("((");
				commandText.Append(escapedName);
				commandText.Append(" = ");
				commandText.Append(oldValueParameterPlaceHolder);
				commandText.Append(") OR (");
				commandText.Append(escapedName);
				commandText.Append(" IS NULL AND ");
				commandText.Append(oldValueParameterPlaceHolder);
				commandText.Append(" IS NULL))");
				return;
			}
			commandText.Append(escapedName);
			commandText.Append(" = ");
			commandText.Append(oldValueParameterPlaceHolder);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00042122 File Offset: 0x00040322
		private bool CanAutoGenerateQueries()
		{
			return !this.Distinct && (this.AsteriskField || this._fields.Count != 0);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00042148 File Offset: 0x00040348
		public SqlDataSourceTableQuery Clone()
		{
			SqlDataSourceTableQuery sqlDataSourceTableQuery = new SqlDataSourceTableQuery(this.DesignerDataConnection, this.DesignerDataTable);
			sqlDataSourceTableQuery.Distinct = this.Distinct;
			sqlDataSourceTableQuery.AsteriskField = this.AsteriskField;
			foreach (DesignerDataColumn item in this.Fields)
			{
				sqlDataSourceTableQuery.Fields.Add(item);
			}
			foreach (SqlDataSourceFilterClause item2 in this.FilterClauses)
			{
				sqlDataSourceTableQuery.FilterClauses.Add(item2);
			}
			foreach (SqlDataSourceOrderClause item3 in this.OrderClauses)
			{
				sqlDataSourceTableQuery.OrderClauses.Add(item3);
			}
			return sqlDataSourceTableQuery;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00042250 File Offset: 0x00040450
		public SqlDataSourceQuery GetDeleteQuery(string oldValuesFormatString, bool includeOldValues)
		{
			if (!this.CanAutoGenerateQueries())
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder("DELETE FROM ");
			stringBuilder.Append(this.GetTableName());
			SqlDataSourceQuery whereClause = this.GetWhereClause(oldValuesFormatString, includeOldValues);
			if (whereClause == null)
			{
				return null;
			}
			stringBuilder.Append(whereClause.Command);
			return new SqlDataSourceQuery(stringBuilder.ToString(), SqlDataSourceCommandType.Text, whereClause.Parameters);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x000422AC File Offset: 0x000404AC
		public SqlDataSourceQuery GetInsertQuery()
		{
			if (!this.CanAutoGenerateQueries())
			{
				return null;
			}
			List<Parameter> list = new List<Parameter>();
			StringBuilder stringBuilder = new StringBuilder("INSERT INTO ");
			stringBuilder.Append(this.GetTableName());
			List<SqlDataSourceColumnData> effectiveColumns = this.GetEffectiveColumns();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			bool flag = true;
			foreach (SqlDataSourceColumnData sqlDataSourceColumnData in effectiveColumns)
			{
				if (!sqlDataSourceColumnData.Column.Identity)
				{
					if (!flag)
					{
						stringBuilder2.Append(", ");
						stringBuilder3.Append(", ");
					}
					stringBuilder2.Append(sqlDataSourceColumnData.EscapedName);
					stringBuilder3.Append(sqlDataSourceColumnData.ParameterPlaceholder);
					DbProviderFactory dbProviderFactory = SqlDataSourceDesigner.GetDbProviderFactory(this.DesignerDataConnection.ProviderName);
					list.Add(SqlDataSourceDesigner.CreateParameter(dbProviderFactory, sqlDataSourceColumnData.WebParameterName, sqlDataSourceColumnData.Column.DataType));
					flag = false;
				}
			}
			if (flag)
			{
				return null;
			}
			stringBuilder.Append(" (");
			stringBuilder.Append(stringBuilder2.ToString());
			stringBuilder.Append(") VALUES (");
			stringBuilder.Append(stringBuilder3.ToString());
			stringBuilder.Append(")");
			return new SqlDataSourceQuery(stringBuilder.ToString(), SqlDataSourceCommandType.Text, list);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0004240C File Offset: 0x0004060C
		private List<SqlDataSourceColumnData> GetEffectiveColumns()
		{
			StringCollection usedNames = new StringCollection();
			List<SqlDataSourceColumnData> list = new List<SqlDataSourceColumnData>();
			if (this.AsteriskField)
			{
				using (IEnumerator enumerator = this._designerDataTable.Columns.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DesignerDataColumn column = (DesignerDataColumn)obj;
						list.Add(new SqlDataSourceColumnData(this.DesignerDataConnection, column, usedNames));
					}
					return list;
				}
			}
			foreach (DesignerDataColumn column2 in this._fields)
			{
				list.Add(new SqlDataSourceColumnData(this.DesignerDataConnection, column2, usedNames));
			}
			return list;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000424E0 File Offset: 0x000406E0
		public SqlDataSourceQuery GetSelectQuery()
		{
			if (!this._asteriskField && this._fields.Count == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(2048);
			stringBuilder.Append("SELECT");
			if (this._distinct)
			{
				stringBuilder.Append(" DISTINCT");
			}
			if (this._asteriskField)
			{
				stringBuilder.Append(" ");
				SqlDataSourceColumnData sqlDataSourceColumnData = new SqlDataSourceColumnData(this.DesignerDataConnection, null);
				stringBuilder.Append(sqlDataSourceColumnData.SelectName);
			}
			if (this._fields.Count > 0)
			{
				stringBuilder.Append(" ");
				bool flag = true;
				List<SqlDataSourceColumnData> effectiveColumns = this.GetEffectiveColumns();
				foreach (SqlDataSourceColumnData sqlDataSourceColumnData2 in effectiveColumns)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(sqlDataSourceColumnData2.SelectName);
					flag = false;
				}
			}
			stringBuilder.Append(" FROM");
			stringBuilder.Append(" " + this.GetTableName());
			List<Parameter> list = new List<Parameter>();
			if (this._filterClauses.Count > 0)
			{
				stringBuilder.Append(" WHERE ");
				if (this._filterClauses.Count > 1)
				{
					stringBuilder.Append("(");
				}
				bool flag2 = true;
				foreach (SqlDataSourceFilterClause sqlDataSourceFilterClause in this._filterClauses)
				{
					if (!flag2)
					{
						stringBuilder.Append(" AND ");
					}
					stringBuilder.Append("(" + sqlDataSourceFilterClause.ToString() + ")");
					flag2 = false;
					if (sqlDataSourceFilterClause.Parameter != null)
					{
						list.Add(sqlDataSourceFilterClause.Parameter);
					}
				}
				if (this._filterClauses.Count > 1)
				{
					stringBuilder.Append(")");
				}
			}
			if (this._orderClauses.Count > 0)
			{
				stringBuilder.Append(" ORDER BY ");
				bool flag3 = true;
				foreach (SqlDataSourceOrderClause sqlDataSourceOrderClause in this._orderClauses)
				{
					if (!flag3)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(sqlDataSourceOrderClause.ToString());
					flag3 = false;
				}
			}
			string command = stringBuilder.ToString();
			return new SqlDataSourceQuery(command, SqlDataSourceCommandType.Text, list.ToArray());
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0004276C File Offset: 0x0004096C
		public string GetTableName()
		{
			return SqlDataSourceColumnData.EscapeObjectName(this.DesignerDataConnection, this.DesignerDataTable.Name);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00042784 File Offset: 0x00040984
		public SqlDataSourceQuery GetUpdateQuery(string oldValuesFormatString, bool includeOldValues)
		{
			if (!this.CanAutoGenerateQueries())
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder("UPDATE ");
			stringBuilder.Append(this.GetTableName());
			stringBuilder.Append(" SET ");
			List<SqlDataSourceColumnData> effectiveColumns = this.GetEffectiveColumns();
			List<Parameter> list = new List<Parameter>();
			bool flag = true;
			foreach (SqlDataSourceColumnData sqlDataSourceColumnData in effectiveColumns)
			{
				if (!sqlDataSourceColumnData.Column.PrimaryKey)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(sqlDataSourceColumnData.EscapedName);
					stringBuilder.Append(" = ");
					stringBuilder.Append(sqlDataSourceColumnData.ParameterPlaceholder);
					flag = false;
					DbProviderFactory dbProviderFactory = SqlDataSourceDesigner.GetDbProviderFactory(this.DesignerDataConnection.ProviderName);
					list.Add(SqlDataSourceDesigner.CreateParameter(dbProviderFactory, sqlDataSourceColumnData.WebParameterName, sqlDataSourceColumnData.Column.DataType));
				}
			}
			if (flag)
			{
				return null;
			}
			SqlDataSourceQuery whereClause = this.GetWhereClause(oldValuesFormatString, includeOldValues);
			if (whereClause == null)
			{
				return null;
			}
			stringBuilder.Append(whereClause.Command);
			foreach (object obj in whereClause.Parameters)
			{
				Parameter item = (Parameter)obj;
				list.Add(item);
			}
			return new SqlDataSourceQuery(stringBuilder.ToString(), SqlDataSourceCommandType.Text, list);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0004290C File Offset: 0x00040B0C
		private SqlDataSourceQuery GetWhereClause(string oldValuesFormatString, bool includeOldValues)
		{
			List<SqlDataSourceColumnData> effectiveColumns = this.GetEffectiveColumns();
			List<Parameter> list = new List<Parameter>();
			if (effectiveColumns.Count == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" WHERE ");
			int num = 0;
			DbProviderFactory dbProviderFactory = SqlDataSourceDesigner.GetDbProviderFactory(this.DesignerDataConnection.ProviderName);
			foreach (SqlDataSourceColumnData sqlDataSourceColumnData in effectiveColumns)
			{
				if (sqlDataSourceColumnData.Column.PrimaryKey)
				{
					if (num > 0)
					{
						stringBuilder.Append(" AND ");
					}
					num++;
					this.AppendWhereClauseParameter(stringBuilder, sqlDataSourceColumnData, oldValuesFormatString);
					list.Add(SqlDataSourceDesigner.CreateParameter(dbProviderFactory, sqlDataSourceColumnData.GetOldValueWebParameterName(oldValuesFormatString), sqlDataSourceColumnData.Column.DataType));
				}
			}
			if (num == 0)
			{
				return null;
			}
			if (includeOldValues)
			{
				foreach (SqlDataSourceColumnData sqlDataSourceColumnData2 in effectiveColumns)
				{
					if (!sqlDataSourceColumnData2.Column.PrimaryKey)
					{
						stringBuilder.Append(" AND ");
						num++;
						this.AppendWhereClauseParameter(stringBuilder, sqlDataSourceColumnData2, oldValuesFormatString);
						Parameter item = SqlDataSourceDesigner.CreateParameter(dbProviderFactory, sqlDataSourceColumnData2.GetOldValueWebParameterName(oldValuesFormatString), sqlDataSourceColumnData2.Column.DataType);
						list.Add(item);
						if (sqlDataSourceColumnData2.Column.Nullable && !SqlDataSourceDesigner.SupportsNamedParameters(dbProviderFactory))
						{
							list.Add(item);
						}
					}
				}
			}
			return new SqlDataSourceQuery(stringBuilder.ToString(), SqlDataSourceCommandType.Text, list);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00042A9C File Offset: 0x00040C9C
		public bool IsPrimaryKeySelected()
		{
			List<SqlDataSourceColumnData> effectiveColumns = this.GetEffectiveColumns();
			if (effectiveColumns.Count == 0)
			{
				return false;
			}
			int num = 0;
			foreach (object obj in this._designerDataTable.Columns)
			{
				DesignerDataColumn designerDataColumn = (DesignerDataColumn)obj;
				if (designerDataColumn.PrimaryKey)
				{
					num++;
				}
			}
			if (num == 0)
			{
				return false;
			}
			int num2 = 0;
			foreach (SqlDataSourceColumnData sqlDataSourceColumnData in effectiveColumns)
			{
				if (sqlDataSourceColumnData.Column.PrimaryKey)
				{
					num2++;
				}
			}
			return num == num2;
		}

		// Token: 0x04000638 RID: 1592
		private DesignerDataConnection _designerDataConnection;

		// Token: 0x04000639 RID: 1593
		private DesignerDataTableBase _designerDataTable;

		// Token: 0x0400063A RID: 1594
		private IList<SqlDataSourceFilterClause> _filterClauses = new List<SqlDataSourceFilterClause>();

		// Token: 0x0400063B RID: 1595
		private IList<SqlDataSourceOrderClause> _orderClauses = new List<SqlDataSourceOrderClause>();

		// Token: 0x0400063C RID: 1596
		private bool _distinct;

		// Token: 0x0400063D RID: 1597
		private bool _asteriskField;

		// Token: 0x0400063E RID: 1598
		private IList<DesignerDataColumn> _fields = new List<DesignerDataColumn>();
	}
}
