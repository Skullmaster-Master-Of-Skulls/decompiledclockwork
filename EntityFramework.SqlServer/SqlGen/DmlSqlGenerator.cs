using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200001F RID: 31
	internal static class DmlSqlGenerator
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00006BD0 File Offset: 0x00004DD0
		internal static string GenerateUpdateSql(DbUpdateCommandTree tree, SqlGenerator sqlGenerator, out List<SqlParameter> parameters, bool generateReturningSql = true, bool upperCaseKeywords = true)
		{
			SqlStringBuilder sqlStringBuilder = new SqlStringBuilder(256)
			{
				UpperCaseKeywords = upperCaseKeywords
			};
			DmlSqlGenerator.ExpressionTranslator expressionTranslator = new DmlSqlGenerator.ExpressionTranslator(sqlStringBuilder, tree, null != tree.Returning, sqlGenerator, null, true);
			if (tree.SetClauses.Count == 0)
			{
				sqlStringBuilder.AppendKeyword("declare ");
				sqlStringBuilder.AppendLine("@p int");
			}
			sqlStringBuilder.AppendKeyword("update ");
			tree.Target.Expression.Accept(expressionTranslator);
			sqlStringBuilder.AppendLine();
			bool flag = true;
			sqlStringBuilder.AppendKeyword("set ");
			foreach (DbModificationClause dbModificationClause in tree.SetClauses)
			{
				DbSetClause dbSetClause = (DbSetClause)dbModificationClause;
				if (flag)
				{
					flag = false;
				}
				else
				{
					sqlStringBuilder.Append(", ");
				}
				dbSetClause.Property.Accept(expressionTranslator);
				sqlStringBuilder.Append(" = ");
				dbSetClause.Value.Accept(expressionTranslator);
			}
			if (flag)
			{
				sqlStringBuilder.Append("@p = 0");
			}
			sqlStringBuilder.AppendLine();
			sqlStringBuilder.AppendKeyword("where ");
			tree.Predicate.Accept(expressionTranslator);
			sqlStringBuilder.AppendLine();
			if (generateReturningSql)
			{
				DmlSqlGenerator.GenerateReturningSql(sqlStringBuilder, tree, null, expressionTranslator, tree.Returning, false);
			}
			parameters = expressionTranslator.Parameters;
			return sqlStringBuilder.ToString();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006D34 File Offset: 0x00004F34
		internal static string GenerateDeleteSql(DbDeleteCommandTree tree, SqlGenerator sqlGenerator, out List<SqlParameter> parameters, bool upperCaseKeywords = true, bool createParameters = true)
		{
			SqlStringBuilder sqlStringBuilder = new SqlStringBuilder(256)
			{
				UpperCaseKeywords = upperCaseKeywords
			};
			DmlSqlGenerator.ExpressionTranslator expressionTranslator = new DmlSqlGenerator.ExpressionTranslator(sqlStringBuilder, tree, false, sqlGenerator, null, createParameters);
			sqlStringBuilder.AppendKeyword("delete ");
			tree.Target.Expression.Accept(expressionTranslator);
			sqlStringBuilder.AppendLine();
			sqlStringBuilder.AppendKeyword("where ");
			tree.Predicate.Accept(expressionTranslator);
			parameters = expressionTranslator.Parameters;
			return sqlStringBuilder.ToString();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006DAC File Offset: 0x00004FAC
		internal static string GenerateInsertSql(DbInsertCommandTree tree, SqlGenerator sqlGenerator, out List<SqlParameter> parameters, bool generateReturningSql = true, bool upperCaseKeywords = true, bool createParameters = true)
		{
			SqlStringBuilder sqlStringBuilder = new SqlStringBuilder(256)
			{
				UpperCaseKeywords = upperCaseKeywords
			};
			DmlSqlGenerator.ExpressionTranslator expressionTranslator = new DmlSqlGenerator.ExpressionTranslator(sqlStringBuilder, tree, null != tree.Returning, sqlGenerator, null, createParameters);
			bool flag = DmlSqlGenerator.UseGeneratedValuesVariable(tree, sqlGenerator.SqlVersion);
			EntityType entityType = (EntityType)((DbScanExpression)tree.Target.Expression).Target.ElementType;
			if (flag)
			{
				sqlStringBuilder.AppendKeyword("declare ").Append("@generated_keys").Append(" table(");
				bool flag2 = true;
				foreach (EdmMember edmMember in entityType.KeyMembers)
				{
					if (flag2)
					{
						flag2 = false;
					}
					else
					{
						sqlStringBuilder.Append(", ");
					}
					sqlStringBuilder.Append(DmlSqlGenerator.GenerateMemberTSql(edmMember)).Append(" ").Append(DmlSqlGenerator.GetVariableType(sqlGenerator, edmMember));
					Facet facet;
					if (edmMember.TypeUsage.Facets.TryGetValue("Collation", false, out facet))
					{
						string text = facet.Value as string;
						if (!string.IsNullOrEmpty(text))
						{
							sqlStringBuilder.AppendKeyword(" collate ").Append(text);
						}
					}
				}
				sqlStringBuilder.AppendLine(")");
			}
			sqlStringBuilder.AppendKeyword("insert ");
			tree.Target.Expression.Accept(expressionTranslator);
			if (0 < tree.SetClauses.Count)
			{
				sqlStringBuilder.Append("(");
				bool flag3 = true;
				foreach (DbModificationClause dbModificationClause in tree.SetClauses)
				{
					DbSetClause dbSetClause = (DbSetClause)dbModificationClause;
					if (flag3)
					{
						flag3 = false;
					}
					else
					{
						sqlStringBuilder.Append(", ");
					}
					dbSetClause.Property.Accept(expressionTranslator);
				}
				sqlStringBuilder.AppendLine(")");
			}
			else
			{
				sqlStringBuilder.AppendLine();
			}
			if (flag)
			{
				sqlStringBuilder.AppendKeyword("output ");
				bool flag4 = true;
				foreach (EdmMember member in entityType.KeyMembers)
				{
					if (flag4)
					{
						flag4 = false;
					}
					else
					{
						sqlStringBuilder.Append(", ");
					}
					sqlStringBuilder.Append("inserted.");
					sqlStringBuilder.Append(DmlSqlGenerator.GenerateMemberTSql(member));
				}
				sqlStringBuilder.AppendKeyword(" into ").AppendLine("@generated_keys");
			}
			if (0 < tree.SetClauses.Count)
			{
				bool flag5 = true;
				sqlStringBuilder.AppendKeyword("values (");
				foreach (DbModificationClause dbModificationClause2 in tree.SetClauses)
				{
					DbSetClause dbSetClause2 = (DbSetClause)dbModificationClause2;
					if (flag5)
					{
						flag5 = false;
					}
					else
					{
						sqlStringBuilder.Append(", ");
					}
					dbSetClause2.Value.Accept(expressionTranslator);
					expressionTranslator.RegisterMemberValue(dbSetClause2.Property, dbSetClause2.Value);
				}
				sqlStringBuilder.AppendLine(")");
			}
			else
			{
				sqlStringBuilder.AppendKeyword("default values");
				sqlStringBuilder.AppendLine();
			}
			if (generateReturningSql)
			{
				DmlSqlGenerator.GenerateReturningSql(sqlStringBuilder, tree, entityType, expressionTranslator, tree.Returning, flag);
			}
			parameters = expressionTranslator.Parameters;
			return sqlStringBuilder.ToString();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007134 File Offset: 0x00005334
		internal static string GetVariableType(SqlGenerator sqlGenerator, EdmMember column)
		{
			string text = SqlGenerator.GenerateSqlForStoreType(sqlGenerator.SqlVersion, column.TypeUsage);
			if (text == "rowversion" || text == "timestamp")
			{
				text = "binary(8)";
			}
			return text;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007188 File Offset: 0x00005388
		internal static bool UseGeneratedValuesVariable(DbInsertCommandTree tree, SqlVersion sqlVersion)
		{
			bool result = false;
			if (sqlVersion > SqlVersion.Sql8 && tree.Returning != null)
			{
				HashSet<EdmMember> hashSet = new HashSet<EdmMember>(from DbSetClause s in tree.SetClauses
				select ((DbPropertyExpression)s.Property).Property);
				bool flag = false;
				foreach (EdmMember edmMember in ((DbScanExpression)tree.Target.Expression).Target.ElementType.KeyMembers)
				{
					if (!hashSet.Contains(edmMember))
					{
						if (flag)
						{
							result = true;
							break;
						}
						flag = true;
						if (!DmlSqlGenerator.IsValidScopeIdentityColumnType(edmMember.TypeUsage))
						{
							result = true;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000725C File Offset: 0x0000545C
		internal static string GenerateMemberTSql(EdmMember member)
		{
			return SqlGenerator.QuoteIdentifier(member.Name);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000726C File Offset: 0x0000546C
		internal static void GenerateReturningSql(SqlStringBuilder commandText, DbModificationCommandTree tree, EntityType tableType, DmlSqlGenerator.ExpressionTranslator translator, DbExpression returning, bool useGeneratedValuesVariable)
		{
			if (returning == null)
			{
				return;
			}
			commandText.AppendKeyword("select ");
			if (useGeneratedValuesVariable)
			{
				translator.PropertyAlias = "t";
			}
			returning.Accept(translator);
			if (useGeneratedValuesVariable)
			{
				translator.PropertyAlias = null;
			}
			commandText.AppendLine();
			if (useGeneratedValuesVariable)
			{
				commandText.AppendKeyword("from ");
				commandText.Append("@generated_keys");
				commandText.AppendKeyword(" as ");
				commandText.Append("g");
				commandText.AppendKeyword(" join ");
				tree.Target.Expression.Accept(translator);
				commandText.AppendKeyword(" as ");
				commandText.Append("t");
				commandText.AppendKeyword(" on ");
				string keyword = string.Empty;
				foreach (EdmMember member in tableType.KeyMembers)
				{
					commandText.AppendKeyword(keyword);
					keyword = " and ";
					commandText.Append("g.");
					string s = DmlSqlGenerator.GenerateMemberTSql(member);
					commandText.Append(s);
					commandText.Append(" = t.");
					commandText.Append(s);
				}
				commandText.AppendLine();
				commandText.AppendKeyword("where @@ROWCOUNT > 0");
				return;
			}
			commandText.AppendKeyword("from ");
			tree.Target.Expression.Accept(translator);
			commandText.AppendLine();
			commandText.AppendKeyword("where @@ROWCOUNT > 0");
			EntitySetBase target = ((DbScanExpression)tree.Target.Expression).Target;
			bool flag = false;
			foreach (EdmMember edmMember in target.ElementType.KeyMembers)
			{
				commandText.AppendKeyword(" and ");
				commandText.Append(DmlSqlGenerator.GenerateMemberTSql(edmMember));
				commandText.Append(" = ");
				SqlParameter sqlParameter;
				if (translator.MemberValues.TryGetValue(edmMember, out sqlParameter))
				{
					commandText.Append(sqlParameter.ParameterName);
				}
				else
				{
					if (flag)
					{
						throw new NotSupportedException(Strings.Update_NotSupportedServerGenKey(target.Name));
					}
					if (!DmlSqlGenerator.IsValidScopeIdentityColumnType(edmMember.TypeUsage))
					{
						throw new InvalidOperationException(Strings.Update_NotSupportedIdentityType(edmMember.Name, edmMember.TypeUsage.ToString()));
					}
					commandText.Append("scope_identity()");
					flag = true;
				}
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000074EC File Offset: 0x000056EC
		private static bool IsValidScopeIdentityColumnType(TypeUsage typeUsage)
		{
			if (typeUsage.EdmType.BuiltInTypeKind != BuiltInTypeKind.PrimitiveType)
			{
				return false;
			}
			string name = typeUsage.EdmType.Name;
			Facet facet;
			return name == "tinyint" || name == "smallint" || name == "int" || name == "bigint" || ((name == "decimal" || name == "numeric") && typeUsage.Facets.TryGetValue("Scale", false, out facet) && Convert.ToInt32(facet.Value, CultureInfo.InvariantCulture) == 0);
		}

		// Token: 0x04000063 RID: 99
		private const int CommandTextBuilderInitialCapacity = 256;

		// Token: 0x04000064 RID: 100
		private const string GeneratedValuesVariableName = "@generated_keys";

		// Token: 0x02000020 RID: 32
		internal class ExpressionTranslator : BasicExpressionVisitor
		{
			// Token: 0x060001CB RID: 459 RVA: 0x00007594 File Offset: 0x00005794
			internal ExpressionTranslator(SqlStringBuilder commandText, DbModificationCommandTree commandTree, bool preserveMemberValues, SqlGenerator sqlGenerator, ICollection<EdmProperty> localVariableBindings = null, bool createParameters = true)
			{
				this._commandText = commandText;
				this._commandTree = commandTree;
				this._sqlGenerator = sqlGenerator;
				this._localVariableBindings = localVariableBindings;
				this._parameters = new List<SqlParameter>();
				this._memberValues = (preserveMemberValues ? new Dictionary<EdmMember, SqlParameter>() : null);
				this._createParameters = createParameters;
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060001CC RID: 460 RVA: 0x000075E9 File Offset: 0x000057E9
			internal List<SqlParameter> Parameters
			{
				get
				{
					return this._parameters;
				}
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060001CD RID: 461 RVA: 0x000075F1 File Offset: 0x000057F1
			internal Dictionary<EdmMember, SqlParameter> MemberValues
			{
				get
				{
					return this._memberValues;
				}
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060001CE RID: 462 RVA: 0x000075F9 File Offset: 0x000057F9
			// (set) Token: 0x060001CF RID: 463 RVA: 0x00007601 File Offset: 0x00005801
			internal string PropertyAlias { get; set; }

			// Token: 0x060001D0 RID: 464 RVA: 0x0000760C File Offset: 0x0000580C
			internal SqlParameter CreateParameter(object value, TypeUsage type, string name = null)
			{
				SqlParameter sqlParameter = SqlProviderServices.CreateSqlParameter(name ?? DmlSqlGenerator.ExpressionTranslator.GetParameterName(this._parameters.Count), type, ParameterMode.In, value, true, this._sqlGenerator.SqlVersion);
				this._parameters.Add(sqlParameter);
				return sqlParameter;
			}

			// Token: 0x060001D1 RID: 465 RVA: 0x00007650 File Offset: 0x00005850
			internal static string GetParameterName(int index)
			{
				return "@" + index.ToString(CultureInfo.InvariantCulture);
			}

			// Token: 0x060001D2 RID: 466 RVA: 0x00007668 File Offset: 0x00005868
			public override void Visit(DbAndExpression expression)
			{
				Check.NotNull<DbAndExpression>(expression, "expression");
				this.VisitBinary(expression, " and ");
			}

			// Token: 0x060001D3 RID: 467 RVA: 0x00007682 File Offset: 0x00005882
			public override void Visit(DbOrExpression expression)
			{
				Check.NotNull<DbOrExpression>(expression, "expression");
				this.VisitBinary(expression, " or ");
			}

			// Token: 0x060001D4 RID: 468 RVA: 0x0000769C File Offset: 0x0000589C
			public override void Visit(DbComparisonExpression expression)
			{
				Check.NotNull<DbComparisonExpression>(expression, "expression");
				this.VisitBinary(expression, " = ");
				this.RegisterMemberValue(expression.Left, expression.Right);
			}

			// Token: 0x060001D5 RID: 469 RVA: 0x000076C8 File Offset: 0x000058C8
			internal void RegisterMemberValue(DbExpression propertyExpression, DbExpression value)
			{
				if (this._memberValues != null)
				{
					EdmMember property = ((DbPropertyExpression)propertyExpression).Property;
					if (value.ExpressionKind != DbExpressionKind.Null)
					{
						this._memberValues[property] = this._parameters[this._parameters.Count - 1];
					}
				}
			}

			// Token: 0x060001D6 RID: 470 RVA: 0x00007717 File Offset: 0x00005917
			public override void Visit(DbIsNullExpression expression)
			{
				Check.NotNull<DbIsNullExpression>(expression, "expression");
				expression.Argument.Accept(this);
				this._commandText.AppendKeyword(" is null");
			}

			// Token: 0x060001D7 RID: 471 RVA: 0x00007742 File Offset: 0x00005942
			public override void Visit(DbNotExpression expression)
			{
				Check.NotNull<DbNotExpression>(expression, "expression");
				this._commandText.AppendKeyword("not (");
				expression.Accept(this);
				this._commandText.Append(")");
			}

			// Token: 0x060001D8 RID: 472 RVA: 0x0000777C File Offset: 0x0000597C
			public override void Visit(DbConstantExpression expression)
			{
				Check.NotNull<DbConstantExpression>(expression, "expression");
				SqlParameter sqlParameter = this.CreateParameter(expression.Value, expression.ResultType, null);
				if (this._createParameters)
				{
					this._commandText.Append(sqlParameter.ParameterName);
					return;
				}
				using (SqlWriter sqlWriter = new SqlWriter(this._commandText.InnerBuilder))
				{
					this._sqlGenerator.WriteSql(sqlWriter, expression.Accept<ISqlFragment>(this._sqlGenerator));
				}
			}

			// Token: 0x060001D9 RID: 473 RVA: 0x0000780C File Offset: 0x00005A0C
			public override void Visit(DbParameterReferenceExpression expression)
			{
				Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
				SqlParameter sqlParameter = this.CreateParameter(DBNull.Value, expression.ResultType, "@" + expression.ParameterName);
				this._commandText.Append(sqlParameter.ParameterName);
			}

			// Token: 0x060001DA RID: 474 RVA: 0x0000785C File Offset: 0x00005A5C
			public override void Visit(DbScanExpression expression)
			{
				Check.NotNull<DbScanExpression>(expression, "expression");
				if (expression.Target.GetMetadataPropertyValue("DefiningQuery") != null)
				{
					string p;
					if (this._commandTree is DbDeleteCommandTree)
					{
						p = "DeleteFunction";
					}
					else if (this._commandTree is DbInsertCommandTree)
					{
						p = "InsertFunction";
					}
					else
					{
						p = "UpdateFunction";
					}
					throw new UpdateException(Strings.Update_SqlEntitySetWithoutDmlFunctions(expression.Target.Name, p, "ModificationFunctionMapping"));
				}
				this._commandText.Append(SqlGenerator.GetTargetTSql(expression.Target));
			}

			// Token: 0x060001DB RID: 475 RVA: 0x000078EC File Offset: 0x00005AEC
			public override void Visit(DbPropertyExpression expression)
			{
				Check.NotNull<DbPropertyExpression>(expression, "expression");
				if (!string.IsNullOrEmpty(this.PropertyAlias))
				{
					this._commandText.Append(this.PropertyAlias);
					this._commandText.Append(".");
				}
				this._commandText.Append(DmlSqlGenerator.GenerateMemberTSql(expression.Property));
			}

			// Token: 0x060001DC RID: 476 RVA: 0x0000794C File Offset: 0x00005B4C
			public override void Visit(DbNullExpression expression)
			{
				Check.NotNull<DbNullExpression>(expression, "expression");
				this._commandText.AppendKeyword("null");
			}

			// Token: 0x060001DD RID: 477 RVA: 0x0000796C File Offset: 0x00005B6C
			public override void Visit(DbNewInstanceExpression expression)
			{
				Check.NotNull<DbNewInstanceExpression>(expression, "expression");
				bool flag = true;
				foreach (DbExpression dbExpression in expression.Arguments)
				{
					EdmMember property = ((DbPropertyExpression)dbExpression).Property;
					string text = (this._localVariableBindings != null) ? (this._localVariableBindings.Contains(property) ? ("@" + property.Name + " = ") : null) : string.Empty;
					if (text != null)
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							this._commandText.Append(", ");
						}
						this._commandText.Append(text);
						dbExpression.Accept(this);
					}
				}
			}

			// Token: 0x060001DE RID: 478 RVA: 0x00007A3C File Offset: 0x00005C3C
			private void VisitBinary(DbBinaryExpression expression, string separator)
			{
				this._commandText.Append("(");
				expression.Left.Accept(this);
				this._commandText.AppendKeyword(separator);
				expression.Right.Accept(this);
				this._commandText.Append(")");
			}

			// Token: 0x04000066 RID: 102
			private readonly SqlStringBuilder _commandText;

			// Token: 0x04000067 RID: 103
			private readonly DbModificationCommandTree _commandTree;

			// Token: 0x04000068 RID: 104
			private readonly List<SqlParameter> _parameters;

			// Token: 0x04000069 RID: 105
			private readonly Dictionary<EdmMember, SqlParameter> _memberValues;

			// Token: 0x0400006A RID: 106
			private readonly SqlGenerator _sqlGenerator;

			// Token: 0x0400006B RID: 107
			private readonly ICollection<EdmProperty> _localVariableBindings;

			// Token: 0x0400006C RID: 108
			private readonly bool _createParameters;
		}
	}
}
