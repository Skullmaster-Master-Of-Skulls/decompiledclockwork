using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200001D RID: 29
	internal class DmlFunctionSqlGenerator
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x000061D4 File Offset: 0x000043D4
		public DmlFunctionSqlGenerator(SqlGenerator sqlGenerator)
		{
			this._sqlGenerator = sqlGenerator;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000621C File Offset: 0x0000441C
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		public string GenerateInsert(ICollection<DbInsertCommandTree> commandTrees)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DbInsertCommandTree dbInsertCommandTree = commandTrees.First<DbInsertCommandTree>();
			List<SqlParameter> list;
			stringBuilder.Append(DmlSqlGenerator.GenerateInsertSql(dbInsertCommandTree, this._sqlGenerator, out list, false, true, false));
			stringBuilder.AppendLine();
			EntityType entityType = (EntityType)((DbScanExpression)dbInsertCommandTree.Target.Expression).Target.ElementType;
			stringBuilder.Append(this.IntroduceRequiredLocalVariables(entityType, dbInsertCommandTree));
			foreach (DbInsertCommandTree tree in commandTrees.Skip(1))
			{
				stringBuilder.Append(DmlSqlGenerator.GenerateInsertSql(tree, this._sqlGenerator, out list, false, true, false));
				stringBuilder.AppendLine();
			}
			List<DbInsertCommandTree> list2 = (from ct in commandTrees
			where ct.Returning != null
			select ct).ToList<DbInsertCommandTree>();
			if (list2.Any<DbInsertCommandTree>())
			{
				DmlFunctionSqlGenerator.ReturningSelectSqlGenerator returningSelectSqlGenerator = new DmlFunctionSqlGenerator.ReturningSelectSqlGenerator();
				foreach (DbInsertCommandTree dbInsertCommandTree2 in list2)
				{
					dbInsertCommandTree2.Target.Expression.Accept(returningSelectSqlGenerator);
					dbInsertCommandTree2.Returning.Accept(returningSelectSqlGenerator);
				}
				using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator3 = entityType.KeyProperties.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						EdmProperty keyProperty = enumerator3.Current;
						DbExpression right = (from DbSetClause sc in dbInsertCommandTree.SetClauses
						where ((DbPropertyExpression)sc.Property).Property == keyProperty
						select sc.Value).SingleOrDefault<DbExpression>() ?? keyProperty.TypeUsage.Parameter(keyProperty.Name);
						dbInsertCommandTree.Target.Variable.Property(keyProperty).Equal(right).Accept(returningSelectSqlGenerator);
					}
				}
				stringBuilder.Append(returningSelectSqlGenerator.Sql);
			}
			return stringBuilder.ToString().TrimEnd(new char[0]);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006488 File Offset: 0x00004688
		private string IntroduceRequiredLocalVariables(EntityType entityType, DbInsertCommandTree commandTree)
		{
			List<EdmProperty> list = (from p in entityType.KeyProperties
			where p.IsStoreGeneratedIdentity
			select p).ToList<EdmProperty>();
			SqlStringBuilder sqlStringBuilder = new SqlStringBuilder
			{
				UpperCaseKeywords = true
			};
			if (list.Any<EdmProperty>())
			{
				foreach (EdmProperty edmProperty in list)
				{
					sqlStringBuilder.Append((sqlStringBuilder.Length == 0) ? "DECLARE " : ", ");
					sqlStringBuilder.Append("@");
					sqlStringBuilder.Append(edmProperty.Name);
					sqlStringBuilder.Append(" ");
					sqlStringBuilder.Append(DmlSqlGenerator.GetVariableType(this._sqlGenerator, edmProperty));
				}
				sqlStringBuilder.AppendLine();
				DmlSqlGenerator.ExpressionTranslator translator = new DmlSqlGenerator.ExpressionTranslator(sqlStringBuilder, commandTree, true, this._sqlGenerator, entityType.KeyProperties, true);
				DmlSqlGenerator.GenerateReturningSql(sqlStringBuilder, commandTree, entityType, translator, commandTree.Returning, DmlSqlGenerator.UseGeneratedValuesVariable(commandTree, this._sqlGenerator.SqlVersion));
				sqlStringBuilder.AppendLine();
				sqlStringBuilder.AppendLine();
			}
			return sqlStringBuilder.ToString();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000065D0 File Offset: 0x000047D0
		public string GenerateUpdate(ICollection<DbUpdateCommandTree> commandTrees, string rowsAffectedParameter)
		{
			if (!commandTrees.Any<DbUpdateCommandTree>())
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			List<SqlParameter> list;
			stringBuilder.AppendLine(DmlSqlGenerator.GenerateUpdateSql(commandTrees.First<DbUpdateCommandTree>(), this._sqlGenerator, out list, false, true));
			foreach (DbUpdateCommandTree tree in commandTrees.Skip(1))
			{
				stringBuilder.Append(DmlSqlGenerator.GenerateUpdateSql(tree, this._sqlGenerator, out list, false, true));
				stringBuilder.AppendLine("AND @@ROWCOUNT > 0");
				stringBuilder.AppendLine();
			}
			List<DbUpdateCommandTree> list2 = (from ct in commandTrees
			where ct.Returning != null
			select ct).ToList<DbUpdateCommandTree>();
			if (list2.Any<DbUpdateCommandTree>())
			{
				DmlFunctionSqlGenerator.ReturningSelectSqlGenerator returningSelectSqlGenerator = new DmlFunctionSqlGenerator.ReturningSelectSqlGenerator();
				foreach (DbUpdateCommandTree dbUpdateCommandTree in list2)
				{
					dbUpdateCommandTree.Target.Expression.Accept(returningSelectSqlGenerator);
					dbUpdateCommandTree.Returning.Accept(returningSelectSqlGenerator);
					dbUpdateCommandTree.Predicate.Accept(returningSelectSqlGenerator);
				}
				stringBuilder.AppendLine(returningSelectSqlGenerator.Sql);
				stringBuilder.AppendLine();
			}
			DmlFunctionSqlGenerator.AppendSetRowsAffected(stringBuilder, rowsAffectedParameter);
			return stringBuilder.ToString().TrimEnd(new char[0]);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006740 File Offset: 0x00004940
		public string GenerateDelete(ICollection<DbDeleteCommandTree> commandTrees, string rowsAffectedParameter)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<SqlParameter> list;
			stringBuilder.AppendLine(DmlSqlGenerator.GenerateDeleteSql(commandTrees.First<DbDeleteCommandTree>(), this._sqlGenerator, out list, true, true));
			stringBuilder.AppendLine();
			foreach (DbDeleteCommandTree tree in commandTrees.Skip(1))
			{
				stringBuilder.AppendLine(DmlSqlGenerator.GenerateDeleteSql(tree, this._sqlGenerator, out list, true, true));
				stringBuilder.AppendLine("AND @@ROWCOUNT > 0");
				stringBuilder.AppendLine();
			}
			DmlFunctionSqlGenerator.AppendSetRowsAffected(stringBuilder, rowsAffectedParameter);
			return stringBuilder.ToString().TrimEnd(new char[0]);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000067F4 File Offset: 0x000049F4
		private static void AppendSetRowsAffected(StringBuilder sql, string rowsAffectedParameter)
		{
			if (!string.IsNullOrWhiteSpace(rowsAffectedParameter))
			{
				sql.Append("SET @");
				sql.Append(rowsAffectedParameter);
				sql.AppendLine(" = @@ROWCOUNT");
				sql.AppendLine();
			}
		}

		// Token: 0x04000057 RID: 87
		private readonly SqlGenerator _sqlGenerator;

		// Token: 0x0200001E RID: 30
		private sealed class ReturningSelectSqlGenerator : BasicExpressionVisitor
		{
			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060001BA RID: 442 RVA: 0x00006828 File Offset: 0x00004A28
			public string Sql
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine(this._select.ToString());
					stringBuilder.AppendLine(this._from.ToString());
					stringBuilder.Append("WHERE @@ROWCOUNT > 0");
					stringBuilder.Append(this._where);
					return stringBuilder.ToString();
				}
			}

			// Token: 0x060001BB RID: 443 RVA: 0x00006880 File Offset: 0x00004A80
			public override void Visit(DbNewInstanceExpression newInstanceExpression)
			{
				ReadOnlyMetadataCollection<EdmProperty> properties = ((RowType)newInstanceExpression.ResultType.EdmType).Properties;
				for (int i = 0; i < properties.Count; i++)
				{
					this._select.Append((this._select.Length == 0) ? "SELECT " : ", ");
					this._nextPropertyAlias = properties[i].Name;
					newInstanceExpression.Arguments[i].Accept(this);
				}
				this._nextPropertyAlias = null;
			}

			// Token: 0x060001BC RID: 444 RVA: 0x00006904 File Offset: 0x00004B04
			public override void Visit(DbScanExpression scanExpression)
			{
				string value = SqlGenerator.GetTargetTSql(scanExpression.Target) + " AS " + (this._currentTableAlias = "t" + this._aliasCount++);
				EntityTypeBase elementType = scanExpression.Target.ElementType;
				if (this._from.Length == 0)
				{
					this._baseTable = (EntityType)elementType;
					this._from.Append("FROM ");
					this._from.Append(value);
					return;
				}
				this._from.AppendLine();
				this._from.Append("JOIN ");
				this._from.Append(value);
				this._from.Append(" ON ");
				for (int i = 0; i < elementType.KeyMembers.Count; i++)
				{
					if (i > 0)
					{
						this._from.Append(" AND ");
					}
					this._from.Append(this._currentTableAlias + ".");
					this._from.Append(SqlGenerator.QuoteIdentifier(elementType.KeyMembers[i].Name));
					this._from.Append(" = t0.");
					this._from.Append(SqlGenerator.QuoteIdentifier(this._baseTable.KeyMembers[i].Name));
				}
			}

			// Token: 0x060001BD RID: 445 RVA: 0x00006A78 File Offset: 0x00004C78
			public override void Visit(DbPropertyExpression propertyExpression)
			{
				this._select.Append(this._currentTableAlias);
				this._select.Append(".");
				this._select.Append(SqlGenerator.QuoteIdentifier(propertyExpression.Property.Name));
				if (!string.IsNullOrWhiteSpace(this._nextPropertyAlias) && !string.Equals(this._nextPropertyAlias, propertyExpression.Property.Name, StringComparison.Ordinal))
				{
					this._select.Append(" AS ");
					this._select.Append(this._nextPropertyAlias);
				}
			}

			// Token: 0x060001BE RID: 446 RVA: 0x00006B0D File Offset: 0x00004D0D
			public override void Visit(DbParameterReferenceExpression expression)
			{
				this._where.Append("@" + expression.ParameterName);
			}

			// Token: 0x060001BF RID: 447 RVA: 0x00006B2B File Offset: 0x00004D2B
			public override void Visit(DbIsNullExpression expression)
			{
			}

			// Token: 0x060001C0 RID: 448 RVA: 0x00006B30 File Offset: 0x00004D30
			public override void Visit(DbComparisonExpression comparisonExpression)
			{
				EdmMember property = ((DbPropertyExpression)comparisonExpression.Left).Property;
				if (this._baseTable.KeyMembers.Contains(property))
				{
					this._where.Append(" AND t0.");
					this._where.Append(SqlGenerator.QuoteIdentifier(property.Name));
					this._where.Append(" = ");
					comparisonExpression.Right.Accept(this);
				}
			}

			// Token: 0x0400005C RID: 92
			private readonly StringBuilder _select = new StringBuilder();

			// Token: 0x0400005D RID: 93
			private readonly StringBuilder _from = new StringBuilder();

			// Token: 0x0400005E RID: 94
			private readonly StringBuilder _where = new StringBuilder();

			// Token: 0x0400005F RID: 95
			private int _aliasCount;

			// Token: 0x04000060 RID: 96
			private string _currentTableAlias;

			// Token: 0x04000061 RID: 97
			private EntityType _baseTable;

			// Token: 0x04000062 RID: 98
			private string _nextPropertyAlias;
		}
	}
}
