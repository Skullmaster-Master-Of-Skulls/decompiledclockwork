using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Text;
using OracleInternal.EntityFramework;

namespace Oracle.ManagedDataAccess.Client.SqlGen
{
	// Token: 0x020000EC RID: 236
	internal static class DmlSqlGenerator
	{
		// Token: 0x0600097D RID: 2429 RVA: 0x0006D588 File Offset: 0x0006B788
		internal static string GenerateUpdateSql(DbUpdateCommandTree tree, EFOracleProviderManifest providerManifest, EFOracleVersion sqlVersion, out List<OracleParameter> parameters)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			DmlSqlGenerator.ExpressionTranslator expressionTranslator = new DmlSqlGenerator.ExpressionTranslator(stringBuilder, tree, null != tree.Returning, sqlVersion);
			int count = tree.SetClauses.Count;
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
				stringBuilder.Append("[place_holder] ");
			}
			stringBuilder.AppendLine();
			stringBuilder.Append("where ");
			tree.Predicate.Accept(expressionTranslator);
			if (flag)
			{
				string text = stringBuilder.ToString();
				int num = text.IndexOf("where ");
				text = text.Substring(num + "where ".Length);
				text = text.Replace("(", "");
				text = text.Replace(")", "");
				text = text.Replace(" and ", " ,");
				stringBuilder.Replace("[place_holder]", text);
			}
			stringBuilder.AppendLine();
			DmlSqlGenerator.GenerateReturningSql(stringBuilder, tree, expressionTranslator, tree.Returning, providerManifest, sqlVersion, true);
			parameters = expressionTranslator.Parameters;
			return stringBuilder.ToString();
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0006D738 File Offset: 0x0006B938
		internal static string GenerateDeleteSql(DbDeleteCommandTree tree, EFOracleProviderManifest providerManifest, EFOracleVersion sqlVersion, out List<OracleParameter> parameters)
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

		// Token: 0x0600097F RID: 2431 RVA: 0x0006D7A4 File Offset: 0x0006B9A4
		internal static string GenerateInsertSql(DbInsertCommandTree tree, EFOracleProviderManifest providerManifest, EFOracleVersion sqlVersion, out List<OracleParameter> parameters)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			DmlSqlGenerator.ExpressionTranslator expressionTranslator = new DmlSqlGenerator.ExpressionTranslator(stringBuilder, tree, null != tree.Returning, sqlVersion);
			stringBuilder.Append("insert into ");
			tree.Target.Expression.Accept(expressionTranslator);
			if (0 < tree.SetClauses.Count)
			{
				stringBuilder.Append("(");
				bool flag = true;
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
				}
				stringBuilder.AppendLine(")");
				flag = true;
				stringBuilder.Append("values (");
				foreach (DbModificationClause dbModificationClause2 in tree.SetClauses)
				{
					DbSetClause dbSetClause2 = (DbSetClause)dbModificationClause2;
					if (flag)
					{
						flag = false;
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
				stringBuilder.AppendLine().AppendLine(" values (default)");
			}
			DmlSqlGenerator.GenerateReturningSql(stringBuilder, tree, expressionTranslator, tree.Returning, providerManifest, sqlVersion, false);
			parameters = expressionTranslator.Parameters;
			return stringBuilder.ToString();
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0006D93C File Offset: 0x0006BB3C
		private static string GenerateMemberTSql(EdmMember member)
		{
			return SqlGenerator.QuoteIdentifier(member.Name);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0006D94C File Offset: 0x0006BB4C
		private static void GenerateReturningSql(StringBuilder commandText, DbModificationCommandTree tree, DmlSqlGenerator.ExpressionTranslator translator, DbExpression returning, EFOracleProviderManifest providerManifest, EFOracleVersion sqlVersion, bool isUpdate)
		{
			if (returning == null)
			{
				return;
			}
			EntitySetBase target = ((DbScanExpression)tree.Target.Expression).Target;
			StringBuilder stringBuilder = new StringBuilder(50);
			stringBuilder.Append("declare\n");
			Dictionary<EdmMember, string> dictionary = new Dictionary<EdmMember, string>();
			foreach (EdmMember edmMember in target.ElementType.Members)
			{
				ReadOnlyMetadataCollection<Facet> facets = ((TypeUsage)edmMember.MetadataProperties["TypeUsage"].Value).Facets;
				string text = string.Empty;
				if (facets.Contains("StoreGeneratedPattern"))
				{
					text = facets["StoreGeneratedPattern"].Value.ToString();
					if (!string.IsNullOrEmpty(text))
					{
						if (isUpdate && text.ToUpperInvariant() == "COMPUTED")
						{
							dictionary[edmMember] = text;
						}
						else if (!isUpdate && (text.ToUpperInvariant() == "COMPUTED" || text.ToUpperInvariant() == "IDENTITY"))
						{
							dictionary[edmMember] = text;
						}
					}
				}
				if (dictionary.ContainsKey(edmMember))
				{
					stringBuilder.Append(DmlSqlGenerator.GenerateMemberTSql(edmMember));
					stringBuilder.Append(" ");
					stringBuilder.Append(SqlGenerator.GetSqlPrimitiveType(providerManifest, sqlVersion, edmMember.TypeUsage));
					stringBuilder.Append(";\n");
				}
			}
			stringBuilder.Append("begin\n");
			commandText.Insert(0, stringBuilder.ToString());
			OracleParameter oracleParameter = translator.CreateParameter(OracleDbType.RefCursor, ParameterDirection.Output);
			commandText.Append("returning\n");
			string value = string.Empty;
			foreach (EdmMember edmMember2 in target.ElementType.Members)
			{
				if (dictionary.ContainsKey(edmMember2))
				{
					commandText.Append(value);
					commandText.Append(DmlSqlGenerator.GenerateMemberTSql(edmMember2));
					value = ", ";
				}
			}
			commandText.Append(" into\n");
			value = string.Empty;
			foreach (EdmMember edmMember3 in target.ElementType.Members)
			{
				if (dictionary.ContainsKey(edmMember3))
				{
					commandText.Append(value);
					commandText.Append(DmlSqlGenerator.GenerateMemberTSql(edmMember3));
					value = ", ";
				}
			}
			commandText.Append(";\n");
			commandText.Append("open ");
			commandText.Append(oracleParameter.ParameterName);
			commandText.Append(" for select\n");
			value = string.Empty;
			foreach (EdmMember edmMember4 in target.ElementType.Members)
			{
				if (dictionary.ContainsKey(edmMember4))
				{
					commandText.Append(value);
					commandText.Append(DmlSqlGenerator.GenerateMemberTSql(edmMember4));
					commandText.Append(" as ");
					commandText.Append(DmlSqlGenerator.GenerateMemberTSql(edmMember4));
					value = ", ";
				}
			}
			commandText.Append(" from dual;\n");
			commandText.Append("end;");
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0006DCEC File Offset: 0x0006BEEC
		private static bool IsValidIdentityColumnType(TypeUsage typeUsage)
		{
			if (typeUsage.EdmType.BuiltInTypeKind != BuiltInTypeKind.PrimitiveType)
			{
				return false;
			}
			string name = typeUsage.EdmType.Name;
			Facet facet;
			return name == "tinyint" || name == "smallint" || name == "int" || name == "bigint" || ((name == "decimal" || name == "numeric") && typeUsage.Facets.TryGetValue("Scale", false, out facet) && Convert.ToInt32(facet.Value, CultureInfo.InvariantCulture) == 0);
		}

		// Token: 0x04000C4E RID: 3150
		private const int s_commandTextBuilderInitialCapacity = 256;

		// Token: 0x020000ED RID: 237
		private class ExpressionTranslator : BasicExpressionVisitor
		{
			// Token: 0x06000983 RID: 2435 RVA: 0x0006DD94 File Offset: 0x0006BF94
			internal ExpressionTranslator(StringBuilder commandText, DbModificationCommandTree commandTree, bool preserveMemberValues, EFOracleVersion version)
			{
				this._commandText = commandText;
				this._commandTree = commandTree;
				this._version = version;
				this._parameters = new List<OracleParameter>();
				this._memberValues = (preserveMemberValues ? new Dictionary<EdmMember, OracleParameter>() : null);
			}

			// Token: 0x17000215 RID: 533
			// (get) Token: 0x06000984 RID: 2436 RVA: 0x0006DDD0 File Offset: 0x0006BFD0
			internal List<OracleParameter> Parameters
			{
				get
				{
					return this._parameters;
				}
			}

			// Token: 0x17000216 RID: 534
			// (get) Token: 0x06000985 RID: 2437 RVA: 0x0006DDD8 File Offset: 0x0006BFD8
			internal Dictionary<EdmMember, OracleParameter> MemberValues
			{
				get
				{
					return this._memberValues;
				}
			}

			// Token: 0x06000986 RID: 2438 RVA: 0x0006DDE0 File Offset: 0x0006BFE0
			internal OracleParameter CreateParameter(OracleDbType oracleType, ParameterDirection direction)
			{
				OracleParameter oracleParameter = new OracleParameter();
				oracleParameter.ParameterName = this.NextName();
				oracleParameter.OracleDbType = oracleType;
				oracleParameter.Direction = direction;
				this._parameters.Add(oracleParameter);
				return oracleParameter;
			}

			// Token: 0x06000987 RID: 2439 RVA: 0x0006DE1C File Offset: 0x0006C01C
			internal OracleParameter CreateParameter(object value, TypeUsage type)
			{
				OracleParameter oracleParameter = EFOracleProviderServices.CreateOracleParameter(this.NextName(), type, ParameterMode.In, value, this._version);
				this._parameters.Add(oracleParameter);
				return oracleParameter;
			}

			// Token: 0x06000988 RID: 2440 RVA: 0x0006DE4C File Offset: 0x0006C04C
			private string NextName()
			{
				string result = ":p" + this.parameterNameCount.ToString(CultureInfo.InvariantCulture);
				this.parameterNameCount++;
				return result;
			}

			// Token: 0x06000989 RID: 2441 RVA: 0x0006DE84 File Offset: 0x0006C084
			public override void Visit(DbAndExpression expression)
			{
				this.VisitBinary(expression, " and ");
			}

			// Token: 0x0600098A RID: 2442 RVA: 0x0006DE94 File Offset: 0x0006C094
			public override void Visit(DbOrExpression expression)
			{
				this.VisitBinary(expression, " or ");
			}

			// Token: 0x0600098B RID: 2443 RVA: 0x0006DEA4 File Offset: 0x0006C0A4
			public override void Visit(DbComparisonExpression expression)
			{
				this.VisitBinary(expression, " = ");
				this.RegisterMemberValue(expression.Left, expression.Right);
			}

			// Token: 0x0600098C RID: 2444 RVA: 0x0006DEC4 File Offset: 0x0006C0C4
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

			// Token: 0x0600098D RID: 2445 RVA: 0x0006DF14 File Offset: 0x0006C114
			public override void Visit(DbIsNullExpression expression)
			{
				expression.Argument.Accept(this);
				this._commandText.Append(" is null");
			}

			// Token: 0x0600098E RID: 2446 RVA: 0x0006DF34 File Offset: 0x0006C134
			public override void Visit(DbNotExpression expression)
			{
				this._commandText.Append("not (");
				expression.Accept(this);
				this._commandText.Append(")");
			}

			// Token: 0x0600098F RID: 2447 RVA: 0x0006DF60 File Offset: 0x0006C160
			public override void Visit(DbConstantExpression expression)
			{
				OracleParameter oracleParameter = this.CreateParameter(expression.Value, expression.ResultType);
				this._commandText.Append(oracleParameter.ParameterName);
			}

			// Token: 0x06000990 RID: 2448 RVA: 0x0006DF94 File Offset: 0x0006C194
			public override void Visit(DbScanExpression expression)
			{
				string metadataProperty = MetadataHelpers.GetMetadataProperty<string>(expression.Target, "DefiningQuery");
				if (metadataProperty != null)
				{
					if (!(this._commandTree is DbDeleteCommandTree))
					{
						DbInsertCommandTree dbInsertCommandTree = this._commandTree as DbInsertCommandTree;
					}
					throw new InvalidOperationException(EFProviderSettings.Instance.GetErrorMessage(-5001, new string[0]));
				}
				this._commandText.Append(SqlGenerator.GetTargetTSql(expression.Target));
			}

			// Token: 0x06000991 RID: 2449 RVA: 0x0006E000 File Offset: 0x0006C200
			public override void Visit(DbPropertyExpression expression)
			{
				this._commandText.Append(DmlSqlGenerator.GenerateMemberTSql(expression.Property));
			}

			// Token: 0x06000992 RID: 2450 RVA: 0x0006E01C File Offset: 0x0006C21C
			public override void Visit(DbNullExpression expression)
			{
				this._commandText.Append("null");
			}

			// Token: 0x06000993 RID: 2451 RVA: 0x0006E030 File Offset: 0x0006C230
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

			// Token: 0x06000994 RID: 2452 RVA: 0x0006E098 File Offset: 0x0006C298
			private void VisitBinary(DbBinaryExpression expression, string separator)
			{
				this._commandText.Append("(");
				expression.Left.Accept(this);
				this._commandText.Append(separator);
				expression.Right.Accept(this);
				this._commandText.Append(")");
			}

			// Token: 0x04000C4F RID: 3151
			private readonly StringBuilder _commandText;

			// Token: 0x04000C50 RID: 3152
			private readonly DbModificationCommandTree _commandTree;

			// Token: 0x04000C51 RID: 3153
			private readonly List<OracleParameter> _parameters;

			// Token: 0x04000C52 RID: 3154
			private readonly Dictionary<EdmMember, OracleParameter> _memberValues;

			// Token: 0x04000C53 RID: 3155
			private readonly EFOracleVersion _version;

			// Token: 0x04000C54 RID: 3156
			private int parameterNameCount;
		}
	}
}
