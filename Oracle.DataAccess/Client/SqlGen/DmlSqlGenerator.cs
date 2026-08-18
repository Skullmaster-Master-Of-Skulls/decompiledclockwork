using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Text;

namespace Oracle.DataAccess.Client.SqlGen
{
	// Token: 0x02000085 RID: 133
	internal static class DmlSqlGenerator
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x0003E8E0 File Offset: 0x0003D8E0
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

		// Token: 0x060005D4 RID: 1492 RVA: 0x0003EA90 File Offset: 0x0003DA90
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

		// Token: 0x060005D5 RID: 1493 RVA: 0x0003EAFC File Offset: 0x0003DAFC
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

		// Token: 0x060005D6 RID: 1494 RVA: 0x0003EC94 File Offset: 0x0003DC94
		private static string GenerateMemberTSql(EdmMember member)
		{
			return SqlGenerator.QuoteIdentifier(member.Name);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0003ECA4 File Offset: 0x0003DCA4
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

		// Token: 0x060005D8 RID: 1496 RVA: 0x0003F044 File Offset: 0x0003E044
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

		// Token: 0x040003C1 RID: 961
		private const int s_commandTextBuilderInitialCapacity = 256;

		// Token: 0x02000086 RID: 134
		private class ExpressionTranslator : BasicExpressionVisitor
		{
			// Token: 0x060005D9 RID: 1497 RVA: 0x0003F0EA File Offset: 0x0003E0EA
			internal ExpressionTranslator(StringBuilder commandText, DbModificationCommandTree commandTree, bool preserveMemberValues, EFOracleVersion version)
			{
				this._commandText = commandText;
				this._commandTree = commandTree;
				this._version = version;
				this._parameters = new List<OracleParameter>();
				this._memberValues = (preserveMemberValues ? new Dictionary<EdmMember, OracleParameter>() : null);
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x060005DA RID: 1498 RVA: 0x0003F124 File Offset: 0x0003E124
			internal List<OracleParameter> Parameters
			{
				get
				{
					return this._parameters;
				}
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x060005DB RID: 1499 RVA: 0x0003F12C File Offset: 0x0003E12C
			internal Dictionary<EdmMember, OracleParameter> MemberValues
			{
				get
				{
					return this._memberValues;
				}
			}

			// Token: 0x060005DC RID: 1500 RVA: 0x0003F134 File Offset: 0x0003E134
			internal OracleParameter CreateParameter(OracleDbType oracleType, ParameterDirection direction)
			{
				OracleParameter oracleParameter = new OracleParameter();
				oracleParameter.ParameterName = this.NextName();
				oracleParameter.OracleDbType = oracleType;
				oracleParameter.Direction = direction;
				this._parameters.Add(oracleParameter);
				return oracleParameter;
			}

			// Token: 0x060005DD RID: 1501 RVA: 0x0003F170 File Offset: 0x0003E170
			internal OracleParameter CreateParameter(object value, TypeUsage type)
			{
				OracleParameter oracleParameter = EFOracleProviderServices.CreateOracleParameter(this.NextName(), type, ParameterMode.In, value, this._version);
				this._parameters.Add(oracleParameter);
				return oracleParameter;
			}

			// Token: 0x060005DE RID: 1502 RVA: 0x0003F1A0 File Offset: 0x0003E1A0
			private string NextName()
			{
				string result = ":p" + this.parameterNameCount.ToString(CultureInfo.InvariantCulture);
				this.parameterNameCount++;
				return result;
			}

			// Token: 0x060005DF RID: 1503 RVA: 0x0003F1D7 File Offset: 0x0003E1D7
			public override void Visit(DbAndExpression expression)
			{
				this.VisitBinary(expression, " and ");
			}

			// Token: 0x060005E0 RID: 1504 RVA: 0x0003F1E5 File Offset: 0x0003E1E5
			public override void Visit(DbOrExpression expression)
			{
				this.VisitBinary(expression, " or ");
			}

			// Token: 0x060005E1 RID: 1505 RVA: 0x0003F1F3 File Offset: 0x0003E1F3
			public override void Visit(DbComparisonExpression expression)
			{
				this.VisitBinary(expression, " = ");
				this.RegisterMemberValue(expression.Left, expression.Right);
			}

			// Token: 0x060005E2 RID: 1506 RVA: 0x0003F214 File Offset: 0x0003E214
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

			// Token: 0x060005E3 RID: 1507 RVA: 0x0003F263 File Offset: 0x0003E263
			public override void Visit(DbIsNullExpression expression)
			{
				expression.Argument.Accept(this);
				this._commandText.Append(" is null");
			}

			// Token: 0x060005E4 RID: 1508 RVA: 0x0003F282 File Offset: 0x0003E282
			public override void Visit(DbNotExpression expression)
			{
				this._commandText.Append("not (");
				expression.Accept(this);
				this._commandText.Append(")");
			}

			// Token: 0x060005E5 RID: 1509 RVA: 0x0003F2B0 File Offset: 0x0003E2B0
			public override void Visit(DbConstantExpression expression)
			{
				OracleParameter oracleParameter = this.CreateParameter(expression.Value, expression.ResultType);
				this._commandText.Append(oracleParameter.ParameterName);
			}

			// Token: 0x060005E6 RID: 1510 RVA: 0x0003F2E4 File Offset: 0x0003E2E4
			public override void Visit(DbScanExpression expression)
			{
				string metadataProperty = MetadataHelpers.GetMetadataProperty<string>(expression.Target, "DefiningQuery");
				if (metadataProperty != null)
				{
					if (!(this._commandTree is DbDeleteCommandTree))
					{
						DbInsertCommandTree dbInsertCommandTree = this._commandTree as DbInsertCommandTree;
					}
					throw new InvalidOperationException();
				}
				this._commandText.Append(SqlGenerator.GetTargetTSql(expression.Target));
			}

			// Token: 0x060005E7 RID: 1511 RVA: 0x0003F33B File Offset: 0x0003E33B
			public override void Visit(DbPropertyExpression expression)
			{
				this._commandText.Append(DmlSqlGenerator.GenerateMemberTSql(expression.Property));
			}

			// Token: 0x060005E8 RID: 1512 RVA: 0x0003F354 File Offset: 0x0003E354
			public override void Visit(DbNullExpression expression)
			{
				this._commandText.Append("null");
			}

			// Token: 0x060005E9 RID: 1513 RVA: 0x0003F368 File Offset: 0x0003E368
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

			// Token: 0x060005EA RID: 1514 RVA: 0x0003F3D0 File Offset: 0x0003E3D0
			private void VisitBinary(DbBinaryExpression expression, string separator)
			{
				this._commandText.Append("(");
				expression.Left.Accept(this);
				this._commandText.Append(separator);
				expression.Right.Accept(this);
				this._commandText.Append(")");
			}

			// Token: 0x040003C2 RID: 962
			private readonly StringBuilder _commandText;

			// Token: 0x040003C3 RID: 963
			private readonly DbModificationCommandTree _commandTree;

			// Token: 0x040003C4 RID: 964
			private readonly List<OracleParameter> _parameters;

			// Token: 0x040003C5 RID: 965
			private readonly Dictionary<EdmMember, OracleParameter> _memberValues;

			// Token: 0x040003C6 RID: 966
			private readonly EFOracleVersion _version;

			// Token: 0x040003C7 RID: 967
			private int parameterNameCount;
		}
	}
}
