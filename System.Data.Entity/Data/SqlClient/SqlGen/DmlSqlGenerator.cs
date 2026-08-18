using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x0200002E RID: 46
	internal static class DmlSqlGenerator
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x000119DC File Offset: 0x0000FBDC
		internal static string GenerateUpdateSql(DbUpdateCommandTree tree, SqlVersion sqlVersion, out List<SqlParameter> parameters)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			DmlSqlGenerator.ExpressionTranslator expressionTranslator = new DmlSqlGenerator.ExpressionTranslator(stringBuilder, tree, tree.Returning != null, sqlVersion);
			if (tree.SetClauses.Count == 0)
			{
				stringBuilder.AppendLine("declare @p int");
			}
			stringBuilder.Append("update ");
			tree.Target.Expression.Accept(expressionTranslator);
			stringBuilder.AppendLine();
			bool flag = true;
			stringBuilder.Append("set ");
			foreach (DbModificationClause dbModificationClause in tree.SetClauses)
			{
				DbSetClause dbSetClause = (DbSetClause)dbModificationClause;
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(", ");
				}
				dbSetClause.Property.Accept(expressionTranslator);
				stringBuilder.Append(" = ");
				dbSetClause.Value.Accept(expressionTranslator);
			}
			if (flag)
			{
				stringBuilder.Append("@p = 0");
			}
			stringBuilder.AppendLine();
			stringBuilder.Append("where ");
			tree.Predicate.Accept(expressionTranslator);
			stringBuilder.AppendLine();
			DmlSqlGenerator.GenerateReturningSql(stringBuilder, tree, null, expressionTranslator, tree.Returning, false);
			parameters = expressionTranslator.Parameters;
			return stringBuilder.ToString();
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00011B1C File Offset: 0x0000FD1C
		internal static string GenerateDeleteSql(DbDeleteCommandTree tree, SqlVersion sqlVersion, out List<SqlParameter> parameters)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			DmlSqlGenerator.ExpressionTranslator expressionTranslator = new DmlSqlGenerator.ExpressionTranslator(stringBuilder, tree, false, sqlVersion);
			stringBuilder.Append("delete ");
			tree.Target.Expression.Accept(expressionTranslator);
			stringBuilder.AppendLine();
			stringBuilder.Append("where ");
			tree.Predicate.Accept(expressionTranslator);
			parameters = expressionTranslator.Parameters;
			return stringBuilder.ToString();
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00011B88 File Offset: 0x0000FD88
		internal static string GenerateInsertSql(DbInsertCommandTree tree, SqlVersion sqlVersion, out List<SqlParameter> parameters)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			DmlSqlGenerator.ExpressionTranslator expressionTranslator = new DmlSqlGenerator.ExpressionTranslator(stringBuilder, tree, tree.Returning != null, sqlVersion);
			bool flag = DmlSqlGenerator.UseGeneratedValuesVariable(tree, sqlVersion, expressionTranslator);
			EntityType entityType = (EntityType)((DbScanExpression)tree.Target.Expression).Target.ElementType;
			if (flag)
			{
				stringBuilder.Append("declare ").Append("@generated_keys").Append(" table(");
				bool flag2 = true;
				foreach (EdmMember edmMember in entityType.KeyMembers)
				{
					if (flag2)
					{
						flag2 = false;
					}
					else
					{
						stringBuilder.Append(", ");
					}
					string text = SqlGenerator.GenerateSqlForStoreType(sqlVersion, edmMember.TypeUsage);
					if (text == "rowversion" || text == "timestamp")
					{
						text = "binary(8)";
					}
					stringBuilder.Append(DmlSqlGenerator.GenerateMemberTSql(edmMember)).Append(" ").Append(text);
					Facet facet;
					if (edmMember.TypeUsage.Facets.TryGetValue("Collation", false, out facet))
					{
						string value = facet.Value as string;
						if (!string.IsNullOrEmpty(value))
						{
							stringBuilder.Append(" collate ").Append(value);
						}
					}
				}
				stringBuilder.AppendLine(")");
			}
			stringBuilder.Append("insert ");
			tree.Target.Expression.Accept(expressionTranslator);
			if (0 < tree.SetClauses.Count)
			{
				stringBuilder.Append("(");
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
						stringBuilder.Append(", ");
					}
					dbSetClause.Property.Accept(expressionTranslator);
				}
				stringBuilder.AppendLine(")");
			}
			else
			{
				stringBuilder.AppendLine();
			}
			if (flag)
			{
				stringBuilder.Append("output ");
				bool flag4 = true;
				foreach (EdmMember member in entityType.KeyMembers)
				{
					if (flag4)
					{
						flag4 = false;
					}
					else
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("inserted.");
					stringBuilder.Append(DmlSqlGenerator.GenerateMemberTSql(member));
				}
				stringBuilder.Append(" into ").AppendLine("@generated_keys");
			}
			if (0 < tree.SetClauses.Count)
			{
				bool flag5 = true;
				stringBuilder.Append("values (");
				foreach (DbModificationClause dbModificationClause2 in tree.SetClauses)
				{
					DbSetClause dbSetClause2 = (DbSetClause)dbModificationClause2;
					if (flag5)
					{
						flag5 = false;
					}
					else
					{
						stringBuilder.Append(", ");
					}
					dbSetClause2.Value.Accept(expressionTranslator);
					expressionTranslator.RegisterMemberValue(dbSetClause2.Property, dbSetClause2.Value);
				}
				stringBuilder.AppendLine(")");
			}
			else
			{
				stringBuilder.AppendLine("default values");
			}
			DmlSqlGenerator.GenerateReturningSql(stringBuilder, tree, entityType, expressionTranslator, tree.Returning, flag);
			parameters = expressionTranslator.Parameters;
			return stringBuilder.ToString();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00011F18 File Offset: 0x00010118
		private static bool UseGeneratedValuesVariable(DbInsertCommandTree tree, SqlVersion sqlVersion, DmlSqlGenerator.ExpressionTranslator translator)
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

		// Token: 0x0600043E RID: 1086 RVA: 0x00011FF0 File Offset: 0x000101F0
		private static string GenerateMemberTSql(EdmMember member)
		{
			EntityType entityType = (EntityType)member.DeclaringType;
			string text;
			if (!entityType.TryGetMemberSql(member, out text))
			{
				text = SqlGenerator.QuoteIdentifier(member.Name);
				entityType.SetMemberSql(member, text);
			}
			return text;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001202C File Offset: 0x0001022C
		private static void GenerateReturningSql(StringBuilder commandText, DbModificationCommandTree tree, EntityType tableType, DmlSqlGenerator.ExpressionTranslator translator, DbExpression returning, bool useGeneratedValuesVariable)
		{
			if (returning == null)
			{
				return;
			}
			commandText.Append("select ");
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
				commandText.Append("from ");
				commandText.Append("@generated_keys");
				commandText.Append(" as g join ");
				tree.Target.Expression.Accept(translator);
				commandText.Append(" as t on ");
				string value = string.Empty;
				foreach (EdmMember member in tableType.KeyMembers)
				{
					commandText.Append(value);
					value = " and ";
					commandText.Append("g.");
					string value2 = DmlSqlGenerator.GenerateMemberTSql(member);
					commandText.Append(value2);
					commandText.Append(" = t.");
					commandText.Append(value2);
				}
				commandText.AppendLine();
				commandText.Append("where @@ROWCOUNT > 0");
				return;
			}
			commandText.Append("from ");
			tree.Target.Expression.Accept(translator);
			commandText.AppendLine();
			commandText.Append("where @@ROWCOUNT > 0");
			EntitySetBase target = ((DbScanExpression)tree.Target.Expression).Target;
			bool flag = false;
			foreach (EdmMember edmMember in target.ElementType.KeyMembers)
			{
				commandText.Append(" and ");
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
						throw EntityUtil.NotSupported(Strings.Update_NotSupportedServerGenKey(target.Name));
					}
					if (!DmlSqlGenerator.IsValidScopeIdentityColumnType(edmMember.TypeUsage))
					{
						throw EntityUtil.InvalidOperation(Strings.Update_NotSupportedIdentityType(edmMember.Name, edmMember.TypeUsage.ToString()));
					}
					commandText.Append("scope_identity()");
					flag = true;
				}
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001227C File Offset: 0x0001047C
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

		// Token: 0x0400070A RID: 1802
		private const int s_commandTextBuilderInitialCapacity = 256;

		// Token: 0x0400070B RID: 1803
		private const string s_generatedValuesVariableName = "@generated_keys";

		// Token: 0x02000454 RID: 1108
		private class ExpressionTranslator : BasicExpressionVisitor
		{
			// Token: 0x06003A99 RID: 15001 RVA: 0x000DE6A2 File Offset: 0x000DC8A2
			internal ExpressionTranslator(StringBuilder commandText, DbModificationCommandTree commandTree, bool preserveMemberValues, SqlVersion version)
			{
				this._commandText = commandText;
				this._commandTree = commandTree;
				this._version = version;
				this._parameters = new List<SqlParameter>();
				this._memberValues = (preserveMemberValues ? new Dictionary<EdmMember, SqlParameter>() : null);
			}

			// Token: 0x17000AB5 RID: 2741
			// (get) Token: 0x06003A9A RID: 15002 RVA: 0x000DE6DC File Offset: 0x000DC8DC
			internal List<SqlParameter> Parameters
			{
				get
				{
					return this._parameters;
				}
			}

			// Token: 0x17000AB6 RID: 2742
			// (get) Token: 0x06003A9B RID: 15003 RVA: 0x000DE6E4 File Offset: 0x000DC8E4
			internal Dictionary<EdmMember, SqlParameter> MemberValues
			{
				get
				{
					return this._memberValues;
				}
			}

			// Token: 0x17000AB7 RID: 2743
			// (get) Token: 0x06003A9C RID: 15004 RVA: 0x000DE6EC File Offset: 0x000DC8EC
			// (set) Token: 0x06003A9D RID: 15005 RVA: 0x000DE6F4 File Offset: 0x000DC8F4
			internal string PropertyAlias { get; set; }

			// Token: 0x06003A9E RID: 15006 RVA: 0x000DE700 File Offset: 0x000DC900
			internal SqlParameter CreateParameter(object value, TypeUsage type)
			{
				SqlParameter sqlParameter = SqlProviderServices.CreateSqlParameter(DmlSqlGenerator.ExpressionTranslator.s_parameterNames.GetName(this._parameters.Count), type, ParameterMode.In, value, true, this._version);
				this._parameters.Add(sqlParameter);
				return sqlParameter;
			}

			// Token: 0x06003A9F RID: 15007 RVA: 0x000DE73F File Offset: 0x000DC93F
			public override void Visit(DbAndExpression expression)
			{
				this.VisitBinary(expression, " and ");
			}

			// Token: 0x06003AA0 RID: 15008 RVA: 0x000DE74D File Offset: 0x000DC94D
			public override void Visit(DbOrExpression expression)
			{
				this.VisitBinary(expression, " or ");
			}

			// Token: 0x06003AA1 RID: 15009 RVA: 0x000DE75B File Offset: 0x000DC95B
			public override void Visit(DbComparisonExpression expression)
			{
				this.VisitBinary(expression, " = ");
				this.RegisterMemberValue(expression.Left, expression.Right);
			}

			// Token: 0x06003AA2 RID: 15010 RVA: 0x000DE77C File Offset: 0x000DC97C
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

			// Token: 0x06003AA3 RID: 15011 RVA: 0x000DE7CB File Offset: 0x000DC9CB
			public override void Visit(DbIsNullExpression expression)
			{
				expression.Argument.Accept(this);
				this._commandText.Append(" is null");
			}

			// Token: 0x06003AA4 RID: 15012 RVA: 0x000DE7EA File Offset: 0x000DC9EA
			public override void Visit(DbNotExpression expression)
			{
				this._commandText.Append("not (");
				expression.Accept(this);
				this._commandText.Append(")");
			}

			// Token: 0x06003AA5 RID: 15013 RVA: 0x000DE818 File Offset: 0x000DCA18
			public override void Visit(DbConstantExpression expression)
			{
				SqlParameter sqlParameter = this.CreateParameter(expression.Value, expression.ResultType);
				this._commandText.Append(sqlParameter.ParameterName);
			}

			// Token: 0x06003AA6 RID: 15014 RVA: 0x000DE84C File Offset: 0x000DCA4C
			public override void Visit(DbScanExpression expression)
			{
				if (expression.Target.DefiningQuery != null)
				{
					string p;
					if (this._commandTree.CommandTreeKind == DbCommandTreeKind.Delete)
					{
						p = "DeleteFunction";
					}
					else if (this._commandTree.CommandTreeKind == DbCommandTreeKind.Insert)
					{
						p = "InsertFunction";
					}
					else
					{
						p = "UpdateFunction";
					}
					throw EntityUtil.Update(Strings.Update_SqlEntitySetWithoutDmlFunctions(expression.Target.Name, p, "ModificationFunctionMapping"), null, new IEntityStateEntry[0]);
				}
				this._commandText.Append(SqlGenerator.GetTargetTSql(expression.Target));
			}

			// Token: 0x06003AA7 RID: 15015 RVA: 0x000DE8D4 File Offset: 0x000DCAD4
			public override void Visit(DbPropertyExpression expression)
			{
				if (!string.IsNullOrEmpty(this.PropertyAlias))
				{
					this._commandText.Append(this.PropertyAlias);
					this._commandText.Append(".");
				}
				this._commandText.Append(DmlSqlGenerator.GenerateMemberTSql(expression.Property));
			}

			// Token: 0x06003AA8 RID: 15016 RVA: 0x000DE928 File Offset: 0x000DCB28
			public override void Visit(DbNullExpression expression)
			{
				this._commandText.Append("null");
			}

			// Token: 0x06003AA9 RID: 15017 RVA: 0x000DE93C File Offset: 0x000DCB3C
			public override void Visit(DbNewInstanceExpression expression)
			{
				bool flag = true;
				foreach (DbExpression dbExpression in expression.Arguments)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						this._commandText.Append(", ");
					}
					dbExpression.Accept(this);
				}
			}

			// Token: 0x06003AAA RID: 15018 RVA: 0x000DE9A4 File Offset: 0x000DCBA4
			private void VisitBinary(DbBinaryExpression expression, string separator)
			{
				this._commandText.Append("(");
				expression.Left.Accept(this);
				this._commandText.Append(separator);
				expression.Right.Accept(this);
				this._commandText.Append(")");
			}

			// Token: 0x0400191B RID: 6427
			private readonly StringBuilder _commandText;

			// Token: 0x0400191C RID: 6428
			private readonly DbModificationCommandTree _commandTree;

			// Token: 0x0400191D RID: 6429
			private readonly List<SqlParameter> _parameters;

			// Token: 0x0400191E RID: 6430
			private readonly Dictionary<EdmMember, SqlParameter> _memberValues;

			// Token: 0x0400191F RID: 6431
			private static readonly AliasGenerator s_parameterNames = new AliasGenerator("@", 1000);

			// Token: 0x04001920 RID: 6432
			private readonly SqlVersion _version;
		}
	}
}
