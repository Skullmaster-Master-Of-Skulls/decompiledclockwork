using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder.Spatial;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Spatial;
using System.Data.SqlClient.Internal;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x02000032 RID: 50
	internal sealed class SqlGenerator : DbExpressionVisitor<ISqlFragment>
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x000129B4 File Offset: 0x00010BB4
		private SqlSelectStatement CurrentSelectStatement
		{
			get
			{
				return this.selectStatementStack.Peek();
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x000129C1 File Offset: 0x00010BC1
		private bool IsParentAJoin
		{
			get
			{
				return this.isParentAJoinStack.Count != 0 && this.isParentAJoinStack.Peek();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x000129DD File Offset: 0x00010BDD
		internal Dictionary<string, int> AllExtentNames
		{
			get
			{
				return this.allExtentNames;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x000129E5 File Offset: 0x00010BE5
		internal Dictionary<string, int> AllColumnNames
		{
			get
			{
				return this.allColumnNames;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x000129ED File Offset: 0x00010BED
		internal SqlVersion SqlVersion
		{
			get
			{
				return this.sqlVersion;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x000129F5 File Offset: 0x00010BF5
		internal bool IsPreKatmai
		{
			get
			{
				return SqlVersionUtils.IsPreKatmai(this.SqlVersion);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00012A02 File Offset: 0x00010C02
		internal MetadataWorkspace Workspace
		{
			get
			{
				return this.metadataWorkspace;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00012A0A File Offset: 0x00010C0A
		internal TypeUsage IntegerType
		{
			get
			{
				if (this.integerType == null)
				{
					this.integerType = this.GetPrimitiveType(PrimitiveTypeKind.Int64);
				}
				return this.integerType;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00012A28 File Offset: 0x00010C28
		internal string DefaultStringTypeName
		{
			get
			{
				if (this.defaultStringTypeName == null)
				{
					this.defaultStringTypeName = this.GetSqlPrimitiveType(TypeUsage.CreateStringTypeUsage(this.metadataWorkspace.GetModelPrimitiveType(PrimitiveTypeKind.String), true, false));
				}
				return this.defaultStringTypeName;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00012A58 File Offset: 0x00010C58
		internal StoreItemCollection StoreItemCollection
		{
			get
			{
				return this._storeItemCollection;
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00012A60 File Offset: 0x00010C60
		private SqlGenerator(SqlVersion sqlVersion)
		{
			this.sqlVersion = sqlVersion;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00012A90 File Offset: 0x00010C90
		internal static string GenerateSql(DbCommandTree tree, SqlVersion sqlVersion, out List<SqlParameter> parameters, out CommandType commandType, out HashSet<string> paramsToForceNonUnicode)
		{
			commandType = CommandType.Text;
			parameters = null;
			paramsToForceNonUnicode = null;
			switch (tree.CommandTreeKind)
			{
			case DbCommandTreeKind.Query:
			{
				SqlGenerator sqlGenerator = new SqlGenerator(sqlVersion);
				return sqlGenerator.GenerateSql((DbQueryCommandTree)tree, out paramsToForceNonUnicode);
			}
			case DbCommandTreeKind.Update:
				return DmlSqlGenerator.GenerateUpdateSql((DbUpdateCommandTree)tree, sqlVersion, out parameters);
			case DbCommandTreeKind.Insert:
				return DmlSqlGenerator.GenerateInsertSql((DbInsertCommandTree)tree, sqlVersion, out parameters);
			case DbCommandTreeKind.Delete:
				return DmlSqlGenerator.GenerateDeleteSql((DbDeleteCommandTree)tree, sqlVersion, out parameters);
			case DbCommandTreeKind.Function:
			{
				SqlGenerator sqlGenerator = new SqlGenerator(sqlVersion);
				return SqlGenerator.GenerateFunctionSql((DbFunctionCommandTree)tree, out commandType);
			}
			default:
				parameters = null;
				return null;
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00012B24 File Offset: 0x00010D24
		private static string GenerateFunctionSql(DbFunctionCommandTree tree, out CommandType commandType)
		{
			EdmFunction edmFunction = tree.EdmFunction;
			if (string.IsNullOrEmpty(edmFunction.CommandTextAttribute))
			{
				commandType = CommandType.StoredProcedure;
				string name = string.IsNullOrEmpty(edmFunction.Schema) ? edmFunction.NamespaceName : edmFunction.Schema;
				string name2 = string.IsNullOrEmpty(edmFunction.StoreFunctionNameAttribute) ? edmFunction.Name : edmFunction.StoreFunctionNameAttribute;
				string str = SqlGenerator.QuoteIdentifier(name);
				string str2 = SqlGenerator.QuoteIdentifier(name2);
				return str + "." + str2;
			}
			commandType = CommandType.Text;
			return edmFunction.CommandTextAttribute;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00012BAC File Offset: 0x00010DAC
		private string GenerateSql(DbQueryCommandTree tree, out HashSet<string> paramsToForceNonUnicode)
		{
			DbQueryCommandTree dbQueryCommandTree = tree;
			if (this.SqlVersion == SqlVersion.Sql8 && Sql8ConformanceChecker.NeedsRewrite(tree.Query))
			{
				dbQueryCommandTree = Sql8ExpressionRewriter.Rewrite(tree);
			}
			this.metadataWorkspace = dbQueryCommandTree.MetadataWorkspace;
			this._storeItemCollection = (StoreItemCollection)this.Workspace.GetItemCollection(DataSpace.SSpace);
			this.selectStatementStack = new Stack<SqlSelectStatement>();
			this.isParentAJoinStack = new Stack<bool>();
			this.allExtentNames = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			this.allColumnNames = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			ISqlFragment sqlStatement;
			if (TypeSemantics.IsCollectionType(dbQueryCommandTree.Query.ResultType))
			{
				SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(dbQueryCommandTree.Query);
				sqlSelectStatement.IsTopMost = true;
				sqlStatement = sqlSelectStatement;
			}
			else
			{
				SqlBuilder sqlBuilder = new SqlBuilder();
				sqlBuilder.Append("SELECT ");
				sqlBuilder.Append(dbQueryCommandTree.Query.Accept<ISqlFragment>(this));
				sqlStatement = sqlBuilder;
			}
			if (this.isVarRefSingle)
			{
				throw EntityUtil.NotSupported();
			}
			paramsToForceNonUnicode = new HashSet<string>((from p in this._candidateParametersToForceNonUnicode
			where p.Value
			select p into q
			select q.Key).ToList<string>());
			return this.WriteSql(sqlStatement);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00012CF0 File Offset: 0x00010EF0
		private string WriteSql(ISqlFragment sqlStatement)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			using (SqlWriter sqlWriter = new SqlWriter(stringBuilder))
			{
				sqlStatement.WriteSql(sqlWriter, this);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00012D3C File Offset: 0x00010F3C
		public override ISqlFragment Visit(DbAndExpression e)
		{
			return this.VisitBinaryExpression(" AND ", DbExpressionKind.And, e.Left, e.Right);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00012D58 File Offset: 0x00010F58
		public override ISqlFragment Visit(DbApplyExpression e)
		{
			List<DbExpressionBinding> list = new List<DbExpressionBinding>();
			list.Add(e.Input);
			list.Add(e.Apply);
			DbExpressionKind expressionKind = e.ExpressionKind;
			string joinString;
			if (expressionKind != DbExpressionKind.CrossApply)
			{
				if (expressionKind != DbExpressionKind.OuterApply)
				{
					throw EntityUtil.InvalidOperation(string.Empty);
				}
				joinString = "OUTER APPLY";
			}
			else
			{
				joinString = "CROSS APPLY";
			}
			return this.VisitJoinExpression(list, DbExpressionKind.CrossJoin, joinString, null);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00012DBC File Offset: 0x00010FBC
		public override ISqlFragment Visit(DbArithmeticExpression e)
		{
			DbExpressionKind expressionKind = e.ExpressionKind;
			if (expressionKind <= DbExpressionKind.Multiply)
			{
				if (expressionKind == DbExpressionKind.Divide)
				{
					return this.VisitBinaryExpression(" / ", e.ExpressionKind, e.Arguments[0], e.Arguments[1]);
				}
				switch (expressionKind)
				{
				case DbExpressionKind.Minus:
					return this.VisitBinaryExpression(" - ", e.ExpressionKind, e.Arguments[0], e.Arguments[1]);
				case DbExpressionKind.Modulo:
					return this.VisitBinaryExpression(" % ", e.ExpressionKind, e.Arguments[0], e.Arguments[1]);
				case DbExpressionKind.Multiply:
					return this.VisitBinaryExpression(" * ", e.ExpressionKind, e.Arguments[0], e.Arguments[1]);
				}
			}
			else
			{
				if (expressionKind == DbExpressionKind.Plus)
				{
					return this.VisitBinaryExpression(" + ", e.ExpressionKind, e.Arguments[0], e.Arguments[1]);
				}
				if (expressionKind == DbExpressionKind.UnaryMinus)
				{
					SqlBuilder sqlBuilder = new SqlBuilder();
					sqlBuilder.Append(" -(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(this));
					sqlBuilder.Append(")");
					return sqlBuilder;
				}
			}
			throw EntityUtil.InvalidOperation(string.Empty);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00012F30 File Offset: 0x00011130
		public override ISqlFragment Visit(DbCaseExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("CASE");
			for (int i = 0; i < e.When.Count; i++)
			{
				sqlBuilder.Append(" WHEN (");
				sqlBuilder.Append(e.When[i].Accept<ISqlFragment>(this));
				sqlBuilder.Append(") THEN ");
				sqlBuilder.Append(e.Then[i].Accept<ISqlFragment>(this));
			}
			if (e.Else != null && !(e.Else is DbNullExpression))
			{
				sqlBuilder.Append(" ELSE ");
				sqlBuilder.Append(e.Else.Accept<ISqlFragment>(this));
			}
			sqlBuilder.Append(" END");
			return sqlBuilder;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00012FE8 File Offset: 0x000111E8
		public override ISqlFragment Visit(DbCastExpression e)
		{
			if (Helper.IsSpatialType(e.ResultType))
			{
				return e.Argument.Accept<ISqlFragment>(this);
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(" CAST( ");
			sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
			sqlBuilder.Append(" AS ");
			sqlBuilder.Append(this.GetSqlPrimitiveType(e.ResultType));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001305C File Offset: 0x0001125C
		public override ISqlFragment Visit(DbComparisonExpression e)
		{
			if (TypeSemantics.IsPrimitiveType(e.Left.ResultType, PrimitiveTypeKind.String))
			{
				this._forceNonUnicode = this.CheckIfForceNonUnicodeRequired(e);
			}
			DbExpressionKind expressionKind = e.ExpressionKind;
			SqlBuilder result;
			if (expressionKind <= DbExpressionKind.GreaterThanOrEquals)
			{
				if (expressionKind == DbExpressionKind.Equals)
				{
					result = this.VisitComparisonExpression(" = ", e.Left, e.Right);
					goto IL_105;
				}
				if (expressionKind == DbExpressionKind.GreaterThan)
				{
					result = this.VisitComparisonExpression(" > ", e.Left, e.Right);
					goto IL_105;
				}
				if (expressionKind == DbExpressionKind.GreaterThanOrEquals)
				{
					result = this.VisitComparisonExpression(" >= ", e.Left, e.Right);
					goto IL_105;
				}
			}
			else
			{
				if (expressionKind == DbExpressionKind.LessThan)
				{
					result = this.VisitComparisonExpression(" < ", e.Left, e.Right);
					goto IL_105;
				}
				if (expressionKind == DbExpressionKind.LessThanOrEquals)
				{
					result = this.VisitComparisonExpression(" <= ", e.Left, e.Right);
					goto IL_105;
				}
				if (expressionKind == DbExpressionKind.NotEquals)
				{
					result = this.VisitComparisonExpression(" <> ", e.Left, e.Right);
					goto IL_105;
				}
			}
			throw EntityUtil.InvalidOperation(string.Empty);
			IL_105:
			this._forceNonUnicode = false;
			return result;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00013176 File Offset: 0x00011376
		private bool CheckIfForceNonUnicodeRequired(DbExpression e)
		{
			if (this._forceNonUnicode)
			{
				throw EntityUtil.NotSupported();
			}
			return this.MatchPatternForForcingNonUnicode(e);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00013190 File Offset: 0x00011390
		private bool MatchPatternForForcingNonUnicode(DbExpression e)
		{
			if (e.ExpressionKind == DbExpressionKind.Like)
			{
				DbLikeExpression dbLikeExpression = (DbLikeExpression)e;
				return this.MatchSourcePatternForForcingNonUnicode(dbLikeExpression.Argument) && this.MatchTargetPatternForForcingNonUnicode(dbLikeExpression.Pattern) && this.MatchTargetPatternForForcingNonUnicode(dbLikeExpression.Escape);
			}
			DbComparisonExpression dbComparisonExpression = (DbComparisonExpression)e;
			DbExpression left = dbComparisonExpression.Left;
			DbExpression right = dbComparisonExpression.Right;
			return (this.MatchSourcePatternForForcingNonUnicode(left) && this.MatchTargetPatternForForcingNonUnicode(right)) || (this.MatchSourcePatternForForcingNonUnicode(right) && this.MatchTargetPatternForForcingNonUnicode(left));
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00013214 File Offset: 0x00011414
		private bool MatchTargetPatternForForcingNonUnicode(DbExpression expr)
		{
			if (this.IsConstParamOrNullExpressionUnicodeNotSpecified(expr))
			{
				return true;
			}
			if (expr.ExpressionKind != DbExpressionKind.Function)
			{
				return false;
			}
			DbFunctionExpression dbFunctionExpression = (DbFunctionExpression)expr;
			EdmFunction function = dbFunctionExpression.Function;
			if (!TypeHelpers.IsCanonicalFunction(function) && !SqlFunctionCallHandler.IsStoreFunction(function))
			{
				return false;
			}
			string fullName = function.FullName;
			bool result = false;
			if (SqlGenerator._canonicalStringFunctionsOneArg.Contains(fullName) || SqlGenerator._storeStringFunctionsOneArg.Contains(fullName))
			{
				result = this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[0]);
			}
			else if (SqlGenerator._canonicalStringFunctionsTwoArgs.Contains(fullName))
			{
				result = (this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[0]) && this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[1]));
			}
			else if (SqlGenerator._canonicalStringFunctionsThreeArgs.Contains(fullName) || SqlGenerator._storeStringFunctionsThreeArgs.Contains(fullName))
			{
				result = (this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[0]) && this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[1]) && this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[2]));
			}
			return result;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00013328 File Offset: 0x00011528
		private bool MatchSourcePatternForForcingNonUnicode(DbExpression argument)
		{
			bool flag;
			return argument.ExpressionKind == DbExpressionKind.Property && TypeHelpers.TryGetIsUnicode(argument.ResultType, out flag) && !flag;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00013354 File Offset: 0x00011554
		private bool IsConstParamOrNullExpressionUnicodeNotSpecified(DbExpression argument)
		{
			DbExpressionKind expressionKind = argument.ExpressionKind;
			TypeUsage resultType = argument.ResultType;
			bool flag;
			return TypeSemantics.IsPrimitiveType(resultType, PrimitiveTypeKind.String) && (expressionKind == DbExpressionKind.Constant || expressionKind == DbExpressionKind.ParameterReference || expressionKind == DbExpressionKind.Null) && !TypeHelpers.TryGetBooleanFacetValue(resultType, "Unicode", out flag);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001339C File Offset: 0x0001159C
		private ISqlFragment VisitConstant(DbConstantExpression e, bool isCastOptional)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			TypeUsage resultType = e.ResultType;
			PrimitiveTypeKind primitiveTypeKind;
			if (TypeHelpers.TryGetPrimitiveTypeKind(resultType, out primitiveTypeKind))
			{
				switch (primitiveTypeKind)
				{
				case PrimitiveTypeKind.Binary:
					sqlBuilder.Append(" 0x");
					sqlBuilder.Append(SqlGenerator.ByteArrayToBinaryString((byte[])e.Value));
					sqlBuilder.Append(" ");
					return sqlBuilder;
				case PrimitiveTypeKind.Boolean:
					SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, ((bool)e.Value) ? "1" : "0", "bit", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.Byte:
					SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, e.Value.ToString(), "tinyint", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.DateTime:
					sqlBuilder.Append("convert(");
					sqlBuilder.Append(this.IsPreKatmai ? "datetime" : "datetime2");
					sqlBuilder.Append(", ");
					sqlBuilder.Append(SqlGenerator.EscapeSingleQuote(((DateTime)e.Value).ToString(this.IsPreKatmai ? "yyyy-MM-dd HH:mm:ss.fff" : "yyyy-MM-dd HH:mm:ss.fffffff", CultureInfo.InvariantCulture), false));
					sqlBuilder.Append(", 121)");
					return sqlBuilder;
				case PrimitiveTypeKind.Decimal:
				{
					string text = ((decimal)e.Value).ToString(CultureInfo.InvariantCulture);
					bool cast = -1 == text.IndexOf('.') && text.TrimStart(new char[]
					{
						'-'
					}).Length < 20;
					string typeName = "decimal(" + Math.Max((byte)text.Length, 18).ToString(CultureInfo.InvariantCulture) + ")";
					SqlGenerator.WrapWithCastIfNeeded(cast, text, typeName, sqlBuilder);
					return sqlBuilder;
				}
				case PrimitiveTypeKind.Double:
				{
					double value = (double)e.Value;
					SqlGenerator.AssertValidDouble(value);
					SqlGenerator.WrapWithCastIfNeeded(true, value.ToString("R", CultureInfo.InvariantCulture), "float(53)", sqlBuilder);
					return sqlBuilder;
				}
				case PrimitiveTypeKind.Guid:
					SqlGenerator.WrapWithCastIfNeeded(true, SqlGenerator.EscapeSingleQuote(e.Value.ToString(), false), "uniqueidentifier", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.Single:
				{
					float value2 = (float)e.Value;
					SqlGenerator.AssertValidSingle(value2);
					SqlGenerator.WrapWithCastIfNeeded(true, value2.ToString("R", CultureInfo.InvariantCulture), "real", sqlBuilder);
					return sqlBuilder;
				}
				case PrimitiveTypeKind.Int16:
					SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, e.Value.ToString(), "smallint", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.Int32:
					sqlBuilder.Append(e.Value.ToString());
					return sqlBuilder;
				case PrimitiveTypeKind.Int64:
					SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, e.Value.ToString(), "bigint", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.String:
				{
					bool isUnicode;
					if (!TypeHelpers.TryGetIsUnicode(e.ResultType, out isUnicode))
					{
						isUnicode = !this._forceNonUnicode;
					}
					sqlBuilder.Append(SqlGenerator.EscapeSingleQuote(e.Value as string, isUnicode));
					return sqlBuilder;
				}
				case PrimitiveTypeKind.Time:
					this.AssertKatmaiOrNewer(primitiveTypeKind);
					sqlBuilder.Append("convert(");
					sqlBuilder.Append(e.ResultType.EdmType.Name);
					sqlBuilder.Append(", ");
					sqlBuilder.Append(SqlGenerator.EscapeSingleQuote(e.Value.ToString(), false));
					sqlBuilder.Append(", 121)");
					return sqlBuilder;
				case PrimitiveTypeKind.DateTimeOffset:
					this.AssertKatmaiOrNewer(primitiveTypeKind);
					sqlBuilder.Append("convert(");
					sqlBuilder.Append(e.ResultType.EdmType.Name);
					sqlBuilder.Append(", ");
					sqlBuilder.Append(SqlGenerator.EscapeSingleQuote(((DateTimeOffset)e.Value).ToString("yyyy-MM-dd HH:mm:ss.fffffff zzz", CultureInfo.InvariantCulture), false));
					sqlBuilder.Append(", 121)");
					return sqlBuilder;
				case PrimitiveTypeKind.Geometry:
					this.AppendSpatialConstant(sqlBuilder, ((DbGeometry)e.Value).AsSpatialValue());
					return sqlBuilder;
				case PrimitiveTypeKind.Geography:
					this.AppendSpatialConstant(sqlBuilder, ((DbGeography)e.Value).AsSpatialValue());
					return sqlBuilder;
				}
				throw EntityUtil.NotSupported(Strings.NoStoreTypeForEdmType(resultType.Identity, ((PrimitiveType)resultType.EdmType).PrimitiveTypeKind));
			}
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000137C8 File Offset: 0x000119C8
		private void AppendSpatialConstant(SqlBuilder result, IDbSpatialValue spatialValue)
		{
			DbFunctionExpression dbFunctionExpression = null;
			int? coordinateSystemId = spatialValue.CoordinateSystemId;
			if (coordinateSystemId != null)
			{
				string wellKnownText = spatialValue.WellKnownText;
				if (wellKnownText != null)
				{
					dbFunctionExpression = (spatialValue.IsGeography ? SpatialEdmFunctions.GeographyFromText(wellKnownText, new int?(coordinateSystemId.Value)) : SpatialEdmFunctions.GeometryFromText(wellKnownText, new int?(coordinateSystemId.Value)));
				}
				else
				{
					byte[] wellKnownBinary = spatialValue.WellKnownBinary;
					if (wellKnownBinary != null)
					{
						dbFunctionExpression = (spatialValue.IsGeography ? SpatialEdmFunctions.GeographyFromBinary(wellKnownBinary, new int?(coordinateSystemId.Value)) : SpatialEdmFunctions.GeometryFromBinary(wellKnownBinary, new int?(coordinateSystemId.Value)));
					}
					else
					{
						string gmlString = spatialValue.GmlString;
						if (gmlString != null)
						{
							dbFunctionExpression = (spatialValue.IsGeography ? SpatialEdmFunctions.GeographyFromGml(gmlString, new int?(coordinateSystemId.Value)) : SpatialEdmFunctions.GeometryFromGml(gmlString, new int?(coordinateSystemId.Value)));
						}
					}
				}
			}
			if (dbFunctionExpression != null)
			{
				result.Append(SqlFunctionCallHandler.GenerateFunctionCallSql(this, dbFunctionExpression));
				return;
			}
			throw spatialValue.NotSqlCompatible();
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000138F4 File Offset: 0x00011AF4
		private static void AssertValidDouble(double value)
		{
			if (double.IsNaN(value))
			{
				throw EntityUtil.NotSupported(Strings.SqlGen_TypedNaNNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Double)));
			}
			if (double.IsPositiveInfinity(value))
			{
				throw EntityUtil.NotSupported(Strings.SqlGen_TypedPositiveInfinityNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Double), typeof(double).Name));
			}
			if (double.IsNegativeInfinity(value))
			{
				throw EntityUtil.NotSupported(Strings.SqlGen_TypedNegativeInfinityNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Double), typeof(double).Name));
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00013998 File Offset: 0x00011B98
		private static void AssertValidSingle(float value)
		{
			if (float.IsNaN(value))
			{
				throw EntityUtil.NotSupported(Strings.SqlGen_TypedNaNNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Single)));
			}
			if (float.IsPositiveInfinity(value))
			{
				throw EntityUtil.NotSupported(Strings.SqlGen_TypedPositiveInfinityNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Single), typeof(float).Name));
			}
			if (float.IsNegativeInfinity(value))
			{
				throw EntityUtil.NotSupported(Strings.SqlGen_TypedNegativeInfinityNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Single), typeof(float).Name));
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00013A3B File Offset: 0x00011C3B
		private static void WrapWithCastIfNeeded(bool cast, string value, string typeName, SqlBuilder result)
		{
			if (!cast)
			{
				result.Append(value);
				return;
			}
			result.Append("cast(");
			result.Append(value);
			result.Append(" as ");
			result.Append(typeName);
			result.Append(")");
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00013A77 File Offset: 0x00011C77
		public override ISqlFragment Visit(DbConstantExpression e)
		{
			return this.VisitConstant(e, false);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00013A81 File Offset: 0x00011C81
		public override ISqlFragment Visit(DbDerefExpression e)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00013A88 File Offset: 0x00011C88
		public override ISqlFragment Visit(DbDistinctExpression e)
		{
			SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(e.Argument);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				TypeUsage elementTypeUsage = TypeHelpers.GetElementTypeUsage(e.Argument.ResultType);
				Symbol fromSymbol;
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, "distinct", elementTypeUsage, out fromSymbol);
				this.AddFromSymbol(sqlSelectStatement, "distinct", fromSymbol, false);
			}
			sqlSelectStatement.Select.IsDistinct = true;
			return sqlSelectStatement;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00013AEC File Offset: 0x00011CEC
		public override ISqlFragment Visit(DbElementExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("(");
			sqlBuilder.Append(this.VisitExpressionEnsureSqlStatement(e.Argument));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00013B28 File Offset: 0x00011D28
		public override ISqlFragment Visit(DbExceptExpression e)
		{
			return this.VisitSetOpExpression(e.Left, e.Right, "EXCEPT");
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00013B41 File Offset: 0x00011D41
		public override ISqlFragment Visit(DbExpression e)
		{
			throw EntityUtil.InvalidOperation(string.Empty);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00013B50 File Offset: 0x00011D50
		public override ISqlFragment Visit(DbScanExpression e)
		{
			EntitySetBase target = e.Target;
			if (this.IsParentAJoin)
			{
				SqlBuilder sqlBuilder = new SqlBuilder();
				sqlBuilder.Append(SqlGenerator.GetTargetTSql(target));
				return sqlBuilder;
			}
			SqlSelectStatement sqlSelectStatement = new SqlSelectStatement();
			sqlSelectStatement.From.Append(SqlGenerator.GetTargetTSql(target));
			return sqlSelectStatement;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00013B98 File Offset: 0x00011D98
		internal static string GetTargetTSql(EntitySetBase entitySetBase)
		{
			if (entitySetBase.CachedProviderSql == null)
			{
				if (entitySetBase.DefiningQuery == null)
				{
					StringBuilder stringBuilder = new StringBuilder(50);
					if (!string.IsNullOrEmpty(entitySetBase.Schema))
					{
						stringBuilder.Append(SqlGenerator.QuoteIdentifier(entitySetBase.Schema));
						stringBuilder.Append(".");
					}
					else
					{
						stringBuilder.Append(SqlGenerator.QuoteIdentifier(entitySetBase.EntityContainer.Name));
						stringBuilder.Append(".");
					}
					if (!string.IsNullOrEmpty(entitySetBase.Table))
					{
						stringBuilder.Append(SqlGenerator.QuoteIdentifier(entitySetBase.Table));
					}
					else
					{
						stringBuilder.Append(SqlGenerator.QuoteIdentifier(entitySetBase.Name));
					}
					entitySetBase.CachedProviderSql = stringBuilder.ToString();
				}
				else
				{
					entitySetBase.CachedProviderSql = "(" + entitySetBase.DefiningQuery + ")";
				}
			}
			return entitySetBase.CachedProviderSql;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00013C75 File Offset: 0x00011E75
		public override ISqlFragment Visit(DbFilterExpression e)
		{
			return this.VisitFilterExpression(e.Input, e.Predicate, false);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00013C8A File Offset: 0x00011E8A
		public override ISqlFragment Visit(DbFunctionExpression e)
		{
			return SqlFunctionCallHandler.GenerateFunctionCallSql(this, e);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00013A81 File Offset: 0x00011C81
		public override ISqlFragment Visit(DbLambdaExpression expression)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00013A81 File Offset: 0x00011C81
		public override ISqlFragment Visit(DbEntityRefExpression e)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00013A81 File Offset: 0x00011C81
		public override ISqlFragment Visit(DbRefKeyExpression e)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00013C94 File Offset: 0x00011E94
		public override ISqlFragment Visit(DbGroupByExpression e)
		{
			Symbol symbol;
			SqlSelectStatement sqlSelectStatement = this.VisitInputExpression(e.Input.Expression, e.Input.VariableName, e.Input.VariableType, out symbol);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName, e.Input.VariableType, out symbol);
			}
			this.selectStatementStack.Push(sqlSelectStatement);
			this.symbolTable.EnterScope();
			this.AddFromSymbol(sqlSelectStatement, e.Input.VariableName, symbol);
			this.symbolTable.Add(e.Input.GroupVariableName, symbol);
			RowType edmType = TypeHelpers.GetEdmType<RowType>(TypeHelpers.GetEdmType<CollectionType>(e.ResultType).TypeUsage);
			bool flag = SqlGenerator.GroupByAggregatesNeedInnerQuery(e.Aggregates, e.Input.GroupVariableName) || SqlGenerator.GroupByKeysNeedInnerQuery(e.Keys, e.Input.VariableName);
			SqlSelectStatement sqlSelectStatement2;
			if (flag)
			{
				sqlSelectStatement2 = this.CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName, e.Input.VariableType, false, out symbol);
				this.AddFromSymbol(sqlSelectStatement2, e.Input.VariableName, symbol, false);
			}
			else
			{
				sqlSelectStatement2 = sqlSelectStatement;
			}
			using (IEnumerator<EdmProperty> enumerator = edmType.Properties.GetEnumerator())
			{
				enumerator.MoveNext();
				string s = "";
				foreach (DbExpression dbExpression in e.Keys)
				{
					EdmProperty edmProperty = enumerator.Current;
					string s2 = SqlGenerator.QuoteIdentifier(edmProperty.Name);
					sqlSelectStatement2.GroupBy.Append(s);
					ISqlFragment s3 = dbExpression.Accept<ISqlFragment>(this);
					if (!flag)
					{
						sqlSelectStatement2.Select.Append(s);
						sqlSelectStatement2.Select.AppendLine();
						sqlSelectStatement2.Select.Append(s3);
						sqlSelectStatement2.Select.Append(" AS ");
						sqlSelectStatement2.Select.Append(s2);
						sqlSelectStatement2.GroupBy.Append(s3);
					}
					else
					{
						sqlSelectStatement.Select.Append(s);
						sqlSelectStatement.Select.AppendLine();
						sqlSelectStatement.Select.Append(s3);
						sqlSelectStatement.Select.Append(" AS ");
						sqlSelectStatement.Select.Append(s2);
						sqlSelectStatement2.Select.Append(s);
						sqlSelectStatement2.Select.AppendLine();
						sqlSelectStatement2.Select.Append(symbol);
						sqlSelectStatement2.Select.Append(".");
						sqlSelectStatement2.Select.Append(s2);
						sqlSelectStatement2.Select.Append(" AS ");
						sqlSelectStatement2.Select.Append(s2);
						sqlSelectStatement2.GroupBy.Append(s2);
					}
					s = ", ";
					enumerator.MoveNext();
				}
				foreach (DbAggregate dbAggregate in e.Aggregates)
				{
					EdmProperty edmProperty2 = enumerator.Current;
					string s4 = SqlGenerator.QuoteIdentifier(edmProperty2.Name);
					ISqlFragment sqlFragment = dbAggregate.Arguments[0].Accept<ISqlFragment>(this);
					object aggregateArgument;
					if (flag)
					{
						SqlBuilder sqlBuilder = new SqlBuilder();
						sqlBuilder.Append(symbol);
						sqlBuilder.Append(".");
						sqlBuilder.Append(s4);
						aggregateArgument = sqlBuilder;
						sqlSelectStatement.Select.Append(s);
						sqlSelectStatement.Select.AppendLine();
						sqlSelectStatement.Select.Append(sqlFragment);
						sqlSelectStatement.Select.Append(" AS ");
						sqlSelectStatement.Select.Append(s4);
					}
					else
					{
						aggregateArgument = sqlFragment;
					}
					ISqlFragment s5 = SqlGenerator.VisitAggregate(dbAggregate, aggregateArgument);
					sqlSelectStatement2.Select.Append(s);
					sqlSelectStatement2.Select.AppendLine();
					sqlSelectStatement2.Select.Append(s5);
					sqlSelectStatement2.Select.Append(" AS ");
					sqlSelectStatement2.Select.Append(s4);
					s = ", ";
					enumerator.MoveNext();
				}
			}
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			return sqlSelectStatement2;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00014110 File Offset: 0x00012310
		public override ISqlFragment Visit(DbIntersectExpression e)
		{
			return this.VisitSetOpExpression(e.Left, e.Right, "INTERSECT");
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00014129 File Offset: 0x00012329
		public override ISqlFragment Visit(DbIsEmptyExpression e)
		{
			return this.VisitIsEmptyExpression(e, false);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00014133 File Offset: 0x00012333
		public override ISqlFragment Visit(DbIsNullExpression e)
		{
			return this.VisitIsNullExpression(e, false);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00013A81 File Offset: 0x00011C81
		public override ISqlFragment Visit(DbIsOfExpression e)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001413D File Offset: 0x0001233D
		public override ISqlFragment Visit(DbCrossJoinExpression e)
		{
			return this.VisitJoinExpression(e.Inputs, e.ExpressionKind, "CROSS JOIN", null);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00014158 File Offset: 0x00012358
		public override ISqlFragment Visit(DbJoinExpression e)
		{
			DbExpressionKind expressionKind = e.ExpressionKind;
			string joinString;
			if (expressionKind != DbExpressionKind.FullOuterJoin)
			{
				if (expressionKind != DbExpressionKind.InnerJoin)
				{
					if (expressionKind != DbExpressionKind.LeftOuterJoin)
					{
						joinString = null;
					}
					else
					{
						joinString = "LEFT OUTER JOIN";
					}
				}
				else
				{
					joinString = "INNER JOIN";
				}
			}
			else
			{
				joinString = "FULL OUTER JOIN";
			}
			return this.VisitJoinExpression(new List<DbExpressionBinding>(2)
			{
				e.Left,
				e.Right
			}, e.ExpressionKind, joinString, e.JoinCondition);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000141CC File Offset: 0x000123CC
		public override ISqlFragment Visit(DbLikeExpression e)
		{
			this._forceNonUnicode = this.CheckIfForceNonUnicodeRequired(e);
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
			sqlBuilder.Append(" LIKE ");
			sqlBuilder.Append(e.Pattern.Accept<ISqlFragment>(this));
			if (e.Escape.ExpressionKind != DbExpressionKind.Null)
			{
				sqlBuilder.Append(" ESCAPE ");
				sqlBuilder.Append(e.Escape.Accept<ISqlFragment>(this));
			}
			this._forceNonUnicode = false;
			return sqlBuilder;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00014250 File Offset: 0x00012450
		public override ISqlFragment Visit(DbLimitExpression e)
		{
			SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(e.Argument, false, false);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				TypeUsage elementTypeUsage = TypeHelpers.GetElementTypeUsage(e.Argument.ResultType);
				Symbol fromSymbol;
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, "top", elementTypeUsage, out fromSymbol);
				this.AddFromSymbol(sqlSelectStatement, "top", fromSymbol, false);
			}
			ISqlFragment topCount = this.HandleCountExpression(e.Limit);
			sqlSelectStatement.Select.Top = new TopClause(topCount, e.WithTies);
			return sqlSelectStatement;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000142CE File Offset: 0x000124CE
		public override ISqlFragment Visit(DbNewInstanceExpression e)
		{
			if (TypeSemantics.IsCollectionType(e.ResultType))
			{
				return this.VisitCollectionConstructor(e);
			}
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000142EC File Offset: 0x000124EC
		public override ISqlFragment Visit(DbNotExpression e)
		{
			DbNotExpression dbNotExpression = e.Argument as DbNotExpression;
			if (dbNotExpression != null)
			{
				return dbNotExpression.Argument.Accept<ISqlFragment>(this);
			}
			DbIsEmptyExpression dbIsEmptyExpression = e.Argument as DbIsEmptyExpression;
			if (dbIsEmptyExpression != null)
			{
				return this.VisitIsEmptyExpression(dbIsEmptyExpression, true);
			}
			DbIsNullExpression dbIsNullExpression = e.Argument as DbIsNullExpression;
			if (dbIsNullExpression != null)
			{
				return this.VisitIsNullExpression(dbIsNullExpression, true);
			}
			DbComparisonExpression dbComparisonExpression = e.Argument as DbComparisonExpression;
			if (dbComparisonExpression != null && dbComparisonExpression.ExpressionKind == DbExpressionKind.Equals)
			{
				bool forceNonUnicode = this._forceNonUnicode;
				if (TypeSemantics.IsPrimitiveType(dbComparisonExpression.Left.ResultType, PrimitiveTypeKind.String))
				{
					this._forceNonUnicode = this.CheckIfForceNonUnicodeRequired(dbComparisonExpression);
				}
				SqlBuilder result = this.VisitBinaryExpression(" <> ", DbExpressionKind.NotEquals, dbComparisonExpression.Left, dbComparisonExpression.Right);
				this._forceNonUnicode = forceNonUnicode;
				return result;
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(" NOT (");
			sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000143E4 File Offset: 0x000125E4
		public override ISqlFragment Visit(DbNullExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("CAST(NULL AS ");
			TypeUsage resultType = e.ResultType;
			PrimitiveType primitiveType = resultType.EdmType as PrimitiveType;
			PrimitiveTypeKind primitiveTypeKind = primitiveType.PrimitiveTypeKind;
			if (primitiveTypeKind != PrimitiveTypeKind.Binary)
			{
				if (primitiveTypeKind == PrimitiveTypeKind.String)
				{
					sqlBuilder.Append("varchar(1)");
				}
				else
				{
					sqlBuilder.Append(this.GetSqlPrimitiveType(resultType));
				}
			}
			else
			{
				sqlBuilder.Append("varbinary(1)");
			}
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00013A81 File Offset: 0x00011C81
		public override ISqlFragment Visit(DbOfTypeExpression e)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00014458 File Offset: 0x00012658
		public override ISqlFragment Visit(DbOrExpression e)
		{
			ISqlFragment result = null;
			if (this.TryTranslateIntoIn(e, out result))
			{
				return result;
			}
			return this.VisitBinaryExpression(" OR ", e.ExpressionKind, e.Left, e.Right);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00014494 File Offset: 0x00012694
		private bool TryTranslateIntoIn(DbOrExpression e, out ISqlFragment sqlFragment)
		{
			KeyToListMap<DbExpression, DbExpression> keyToListMap = new KeyToListMap<DbExpression, DbExpression>(SqlGenerator.KeyFieldExpressionComparer.Singleton);
			if (!this.HasBuiltMapForIn(e, keyToListMap) || keyToListMap.Keys.Count<DbExpression>() <= 0)
			{
				sqlFragment = null;
				return false;
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			bool flag = true;
			foreach (DbExpression dbExpression in keyToListMap.Keys)
			{
				ReadOnlyCollection<DbExpression> source = keyToListMap.ListForKey(dbExpression);
				if (!flag)
				{
					sqlBuilder.Append(" OR ");
				}
				else
				{
					flag = false;
				}
				IEnumerable<DbExpression> enumerable = from v in source
				where v.ExpressionKind != DbExpressionKind.IsNull
				select v;
				int num = enumerable.Count<DbExpression>();
				bool flag2 = false;
				bool forceNonUnicodeOnKey = false;
				if (TypeSemantics.IsPrimitiveType(dbExpression.ResultType, PrimitiveTypeKind.String))
				{
					flag2 = this.MatchSourcePatternForForcingNonUnicode(dbExpression);
					forceNonUnicodeOnKey = (!flag2 && this.MatchTargetPatternForForcingNonUnicode(dbExpression) && enumerable.All((DbExpression v) => this.MatchSourcePatternForForcingNonUnicode(v)));
				}
				if (num == 1)
				{
					this.HandleInKey(sqlBuilder, dbExpression, forceNonUnicodeOnKey);
					sqlBuilder.Append(" = ");
					DbExpression dbExpression2 = enumerable.First<DbExpression>();
					this.HandleInValue(sqlBuilder, dbExpression2, dbExpression.ResultType.EdmType == dbExpression2.ResultType.EdmType, flag2);
				}
				if (num > 1)
				{
					this.HandleInKey(sqlBuilder, dbExpression, forceNonUnicodeOnKey);
					sqlBuilder.Append(" IN (");
					bool flag3 = true;
					foreach (DbExpression dbExpression3 in enumerable)
					{
						if (!flag3)
						{
							sqlBuilder.Append(",");
						}
						else
						{
							flag3 = false;
						}
						this.HandleInValue(sqlBuilder, dbExpression3, dbExpression.ResultType.EdmType == dbExpression3.ResultType.EdmType, flag2);
					}
					sqlBuilder.Append(")");
				}
				DbIsNullExpression dbIsNullExpression = source.FirstOrDefault((DbExpression v) => v.ExpressionKind == DbExpressionKind.IsNull) as DbIsNullExpression;
				if (dbIsNullExpression != null)
				{
					if (num > 0)
					{
						sqlBuilder.Append(" OR ");
					}
					sqlBuilder.Append(this.VisitIsNullExpression(dbIsNullExpression, false));
				}
			}
			sqlFragment = sqlBuilder;
			return true;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00014704 File Offset: 0x00012904
		private void HandleInValue(SqlBuilder sqlBuilder, DbExpression value, bool isSameEdmType, bool forceNonUnicodeOnQualifyingValues)
		{
			this.ForcingNonUnicode(delegate
			{
				this.ParenthesizeExpressionWithoutRedundantConstantCasts(value, sqlBuilder, isSameEdmType);
			}, forceNonUnicodeOnQualifyingValues && this.MatchTargetPatternForForcingNonUnicode(value));
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00014758 File Offset: 0x00012958
		private void HandleInKey(SqlBuilder sqlBuilder, DbExpression key, bool forceNonUnicodeOnKey)
		{
			this.ForcingNonUnicode(delegate
			{
				this.ParenthesizeExpressionIfNeeded(key, sqlBuilder);
			}, forceNonUnicodeOnKey);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00014794 File Offset: 0x00012994
		private void ForcingNonUnicode(Action action, bool forceNonUnicode)
		{
			bool flag = false;
			if (forceNonUnicode && !this._forceNonUnicode)
			{
				this._forceNonUnicode = true;
				flag = true;
			}
			action();
			if (flag)
			{
				this._forceNonUnicode = false;
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000147C8 File Offset: 0x000129C8
		private void ParenthesizeExpressionWithoutRedundantConstantCasts(DbExpression value, SqlBuilder sqlBuilder, bool isSameEdmType)
		{
			DbExpressionKind expressionKind = value.ExpressionKind;
			if (expressionKind == DbExpressionKind.Constant)
			{
				sqlBuilder.Append(this.VisitConstant((DbConstantExpression)value, isSameEdmType));
				return;
			}
			this.ParenthesizeExpressionIfNeeded(value, sqlBuilder);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000147FC File Offset: 0x000129FC
		private bool IsKeyForIn(DbExpression e)
		{
			return e.ExpressionKind == DbExpressionKind.Property || e.ExpressionKind == DbExpressionKind.VariableReference || e.ExpressionKind == DbExpressionKind.ParameterReference;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00014820 File Offset: 0x00012A20
		private bool TryAddExpressionForIn(DbBinaryExpression e, KeyToListMap<DbExpression, DbExpression> values)
		{
			if (this.IsKeyForIn(e.Left))
			{
				values.Add(e.Left, e.Right);
				return true;
			}
			if (this.IsKeyForIn(e.Right))
			{
				values.Add(e.Right, e.Left);
				return true;
			}
			return false;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00014874 File Offset: 0x00012A74
		private bool HasBuiltMapForIn(DbExpression e, KeyToListMap<DbExpression, DbExpression> values)
		{
			DbExpressionKind expressionKind = e.ExpressionKind;
			if (expressionKind == DbExpressionKind.Equals)
			{
				return this.TryAddExpressionForIn((DbBinaryExpression)e, values);
			}
			if (expressionKind != DbExpressionKind.IsNull)
			{
				if (expressionKind != DbExpressionKind.Or)
				{
					return false;
				}
				DbBinaryExpression dbBinaryExpression = e as DbBinaryExpression;
				return this.HasBuiltMapForIn(dbBinaryExpression.Left, values) && this.HasBuiltMapForIn(dbBinaryExpression.Right, values);
			}
			else
			{
				DbExpression argument = ((DbIsNullExpression)e).Argument;
				if (this.IsKeyForIn(argument))
				{
					values.Add(argument, e);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000148F0 File Offset: 0x00012AF0
		public override ISqlFragment Visit(DbParameterReferenceExpression e)
		{
			if (!this._ignoreForceNonUnicodeFlag)
			{
				if (!this._forceNonUnicode)
				{
					this._candidateParametersToForceNonUnicode[e.ParameterName] = false;
				}
				else if (!this._candidateParametersToForceNonUnicode.ContainsKey(e.ParameterName))
				{
					this._candidateParametersToForceNonUnicode[e.ParameterName] = true;
				}
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("@" + e.ParameterName);
			return sqlBuilder;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00014964 File Offset: 0x00012B64
		public override ISqlFragment Visit(DbProjectExpression e)
		{
			Symbol fromSymbol;
			SqlSelectStatement sqlSelectStatement = this.VisitInputExpression(e.Input.Expression, e.Input.VariableName, e.Input.VariableType, out fromSymbol);
			bool flag = false;
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName, e.Input.VariableType, out fromSymbol);
			}
			else if (this.SqlVersion == SqlVersion.Sql8 && !sqlSelectStatement.OrderBy.IsEmpty)
			{
				flag = true;
			}
			this.selectStatementStack.Push(sqlSelectStatement);
			this.symbolTable.EnterScope();
			this.AddFromSymbol(sqlSelectStatement, e.Input.VariableName, fromSymbol);
			DbNewInstanceExpression dbNewInstanceExpression = e.Projection as DbNewInstanceExpression;
			if (dbNewInstanceExpression != null)
			{
				Dictionary<string, Symbol> outputColumns;
				sqlSelectStatement.Select.Append(this.VisitNewInstanceExpression(dbNewInstanceExpression, flag, out outputColumns));
				if (flag)
				{
					sqlSelectStatement.OutputColumnsRenamed = true;
				}
				sqlSelectStatement.OutputColumns = outputColumns;
			}
			else
			{
				sqlSelectStatement.Select.Append(e.Projection.Accept<ISqlFragment>(this));
			}
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			return sqlSelectStatement;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00014A78 File Offset: 0x00012C78
		public override ISqlFragment Visit(DbPropertyExpression e)
		{
			ISqlFragment sqlFragment = e.Instance.Accept<ISqlFragment>(this);
			DbVariableReferenceExpression dbVariableReferenceExpression = e.Instance as DbVariableReferenceExpression;
			if (dbVariableReferenceExpression != null)
			{
				this.isVarRefSingle = false;
			}
			JoinSymbol joinSymbol = sqlFragment as JoinSymbol;
			if (joinSymbol == null)
			{
				SymbolPair symbolPair = sqlFragment as SymbolPair;
				SqlBuilder sqlBuilder;
				if (symbolPair != null)
				{
					JoinSymbol joinSymbol2 = symbolPair.Column as JoinSymbol;
					if (joinSymbol2 != null)
					{
						symbolPair.Column = joinSymbol2.NameToExtent[e.Property.Name];
						return symbolPair;
					}
					if (symbolPair.Column.Columns.ContainsKey(e.Property.Name))
					{
						sqlBuilder = new SqlBuilder();
						sqlBuilder.Append(symbolPair.Source);
						sqlBuilder.Append(".");
						Symbol symbol = symbolPair.Column.Columns[e.Property.Name];
						this.optionalColumnUsageManager.MarkAsUsed(symbol);
						sqlBuilder.Append(symbol);
						return sqlBuilder;
					}
				}
				sqlBuilder = new SqlBuilder();
				sqlBuilder.Append(sqlFragment);
				sqlBuilder.Append(".");
				Symbol symbol2 = sqlFragment as Symbol;
				Symbol symbol3;
				if (symbol2 != null && symbol2.OutputColumns.TryGetValue(e.Property.Name, out symbol3))
				{
					this.optionalColumnUsageManager.MarkAsUsed(symbol3);
					if (symbol2.OutputColumnsRenamed)
					{
						sqlBuilder.Append(symbol3);
					}
					else
					{
						sqlBuilder.Append(SqlGenerator.QuoteIdentifier(e.Property.Name));
					}
				}
				else
				{
					sqlBuilder.Append(SqlGenerator.QuoteIdentifier(e.Property.Name));
				}
				return sqlBuilder;
			}
			if (joinSymbol.IsNestedJoin)
			{
				return new SymbolPair(joinSymbol, joinSymbol.NameToExtent[e.Property.Name]);
			}
			return joinSymbol.NameToExtent[e.Property.Name];
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00014C30 File Offset: 0x00012E30
		public override ISqlFragment Visit(DbQuantifierExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			bool negatePredicate = e.ExpressionKind == DbExpressionKind.All;
			if (e.ExpressionKind == DbExpressionKind.Any)
			{
				sqlBuilder.Append("EXISTS (");
			}
			else
			{
				sqlBuilder.Append("NOT EXISTS (");
			}
			SqlSelectStatement sqlSelectStatement = this.VisitFilterExpression(e.Input, e.Predicate, negatePredicate);
			if (sqlSelectStatement.Select.IsEmpty)
			{
				this.AddDefaultColumns(sqlSelectStatement);
			}
			sqlBuilder.Append(sqlSelectStatement);
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00013A81 File Offset: 0x00011C81
		public override ISqlFragment Visit(DbRefExpression e)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00013A81 File Offset: 0x00011C81
		public override ISqlFragment Visit(DbRelationshipNavigationExpression e)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00014CAC File Offset: 0x00012EAC
		public override ISqlFragment Visit(DbSkipExpression e)
		{
			Symbol fromSymbol;
			SqlSelectStatement sqlSelectStatement = this.VisitInputExpression(e.Input.Expression, e.Input.VariableName, e.Input.VariableType, out fromSymbol);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName, e.Input.VariableType, out fromSymbol);
			}
			this.selectStatementStack.Push(sqlSelectStatement);
			this.symbolTable.EnterScope();
			this.AddFromSymbol(sqlSelectStatement, e.Input.VariableName, fromSymbol);
			List<Symbol> list = this.AddDefaultColumns(sqlSelectStatement);
			sqlSelectStatement.Select.Append("row_number() OVER (ORDER BY ");
			this.AddSortKeys(sqlSelectStatement.Select, e.SortOrder);
			sqlSelectStatement.Select.Append(") AS ");
			string row_numberName = "row_number";
			Symbol symbol = new Symbol(row_numberName, this.IntegerType);
			if (list.Any((Symbol c) => string.Equals(c.Name, row_numberName, StringComparison.OrdinalIgnoreCase)))
			{
				symbol.NeedsRenaming = true;
			}
			sqlSelectStatement.Select.Append(symbol);
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			SqlSelectStatement sqlSelectStatement2 = new SqlSelectStatement();
			sqlSelectStatement2.From.Append("( ");
			sqlSelectStatement2.From.Append(sqlSelectStatement);
			sqlSelectStatement2.From.AppendLine();
			sqlSelectStatement2.From.Append(") ");
			Symbol symbol2 = null;
			if (sqlSelectStatement.FromExtents.Count == 1)
			{
				JoinSymbol joinSymbol = sqlSelectStatement.FromExtents[0] as JoinSymbol;
				if (joinSymbol != null)
				{
					symbol2 = new JoinSymbol(e.Input.VariableName, e.Input.VariableType, joinSymbol.ExtentList)
					{
						IsNestedJoin = true,
						ColumnList = list,
						FlattenedExtentList = joinSymbol.FlattenedExtentList
					};
				}
			}
			if (symbol2 == null)
			{
				symbol2 = new Symbol(e.Input.VariableName, e.Input.VariableType, sqlSelectStatement.OutputColumns, false);
			}
			this.selectStatementStack.Push(sqlSelectStatement2);
			this.symbolTable.EnterScope();
			this.AddFromSymbol(sqlSelectStatement2, e.Input.VariableName, symbol2);
			sqlSelectStatement2.Where.Append(symbol2);
			sqlSelectStatement2.Where.Append(".");
			sqlSelectStatement2.Where.Append(symbol);
			sqlSelectStatement2.Where.Append(" > ");
			sqlSelectStatement2.Where.Append(this.HandleCountExpression(e.Count));
			this.AddSortKeys(sqlSelectStatement2.OrderBy, e.SortOrder);
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			return sqlSelectStatement2;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00014F60 File Offset: 0x00013160
		public override ISqlFragment Visit(DbSortExpression e)
		{
			Symbol fromSymbol;
			SqlSelectStatement sqlSelectStatement = this.VisitInputExpression(e.Input.Expression, e.Input.VariableName, e.Input.VariableType, out fromSymbol);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName, e.Input.VariableType, out fromSymbol);
			}
			this.selectStatementStack.Push(sqlSelectStatement);
			this.symbolTable.EnterScope();
			this.AddFromSymbol(sqlSelectStatement, e.Input.VariableName, fromSymbol);
			this.AddSortKeys(sqlSelectStatement.OrderBy, e.SortOrder);
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			return sqlSelectStatement;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00013A81 File Offset: 0x00011C81
		public override ISqlFragment Visit(DbTreatExpression e)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00015019 File Offset: 0x00013219
		public override ISqlFragment Visit(DbUnionAllExpression e)
		{
			return this.VisitSetOpExpression(e.Left, e.Right, "UNION ALL");
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00015034 File Offset: 0x00013234
		public override ISqlFragment Visit(DbVariableReferenceExpression e)
		{
			if (this.isVarRefSingle)
			{
				throw EntityUtil.NotSupported();
			}
			this.isVarRefSingle = true;
			Symbol symbol = this.symbolTable.Lookup(e.VariableName);
			this.optionalColumnUsageManager.MarkAsUsed(symbol);
			if (!this.CurrentSelectStatement.FromExtents.Contains(symbol))
			{
				this.CurrentSelectStatement.OuterExtents[symbol] = true;
			}
			return symbol;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001509C File Offset: 0x0001329C
		private static SqlBuilder VisitAggregate(DbAggregate aggregate, object aggregateArgument)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			DbFunctionAggregate dbFunctionAggregate = aggregate as DbFunctionAggregate;
			if (dbFunctionAggregate == null)
			{
				throw EntityUtil.NotSupported();
			}
			if (TypeHelpers.IsCanonicalFunction(dbFunctionAggregate.Function) && string.Equals(dbFunctionAggregate.Function.Name, "BigCount", StringComparison.Ordinal))
			{
				sqlBuilder.Append("COUNT_BIG");
			}
			else
			{
				SqlFunctionCallHandler.WriteFunctionName(sqlBuilder, dbFunctionAggregate.Function);
			}
			sqlBuilder.Append("(");
			DbFunctionAggregate dbFunctionAggregate2 = dbFunctionAggregate;
			if (dbFunctionAggregate2 != null && dbFunctionAggregate2.Distinct)
			{
				sqlBuilder.Append("DISTINCT ");
			}
			sqlBuilder.Append(aggregateArgument);
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00015133 File Offset: 0x00013333
		internal void ParenthesizeExpressionIfNeeded(DbExpression e, SqlBuilder result)
		{
			if (SqlGenerator.IsComplexExpression(e))
			{
				result.Append("(");
				result.Append(e.Accept<ISqlFragment>(this));
				result.Append(")");
				return;
			}
			result.Append(e.Accept<ISqlFragment>(this));
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00015170 File Offset: 0x00013370
		private SqlBuilder VisitBinaryExpression(string op, DbExpressionKind expressionKind, DbExpression left, DbExpression right)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			bool flag = true;
			foreach (DbExpression e in SqlGenerator.FlattenAssociativeExpression(expressionKind, left, right))
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					sqlBuilder.Append(op);
				}
				this.ParenthesizeExpressionIfNeeded(e, sqlBuilder);
			}
			return sqlBuilder;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000151D8 File Offset: 0x000133D8
		private static IEnumerable<DbExpression> FlattenAssociativeExpression(DbExpressionKind kind, DbExpression left, DbExpression right)
		{
			if (kind != DbExpressionKind.Or && kind != DbExpressionKind.And && kind != DbExpressionKind.Plus && kind != DbExpressionKind.Multiply)
			{
				return new DbExpression[]
				{
					left,
					right
				};
			}
			List<DbExpression> list = new List<DbExpression>();
			SqlGenerator.ExtractAssociativeArguments(kind, list, left);
			SqlGenerator.ExtractAssociativeArguments(kind, list, right);
			return list;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00015220 File Offset: 0x00013420
		private static void ExtractAssociativeArguments(DbExpressionKind expressionKind, List<DbExpression> argumentList, DbExpression expression)
		{
			IEnumerable<DbExpression> leafNodes = Helpers.GetLeafNodes<DbExpression>(expression, (DbExpression exp) => exp.ExpressionKind != expressionKind, delegate(DbExpression exp)
			{
				DbBinaryExpression dbBinaryExpression = exp as DbBinaryExpression;
				if (dbBinaryExpression != null)
				{
					return new DbExpression[]
					{
						dbBinaryExpression.Left,
						dbBinaryExpression.Right
					};
				}
				DbArithmeticExpression dbArithmeticExpression = (DbArithmeticExpression)exp;
				return dbArithmeticExpression.Arguments;
			});
			argumentList.AddRange(leafNodes);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00015274 File Offset: 0x00013474
		private SqlBuilder VisitComparisonExpression(string op, DbExpression left, DbExpression right)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			bool isCastOptional = left.ResultType.EdmType == right.ResultType.EdmType;
			if (left.ExpressionKind == DbExpressionKind.Constant)
			{
				sqlBuilder.Append(this.VisitConstant((DbConstantExpression)left, isCastOptional));
			}
			else
			{
				this.ParenthesizeExpressionIfNeeded(left, sqlBuilder);
			}
			sqlBuilder.Append(op);
			if (right.ExpressionKind == DbExpressionKind.Constant)
			{
				sqlBuilder.Append(this.VisitConstant((DbConstantExpression)right, isCastOptional));
			}
			else
			{
				this.ParenthesizeExpressionIfNeeded(right, sqlBuilder);
			}
			return sqlBuilder;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000152F4 File Offset: 0x000134F4
		private SqlSelectStatement VisitInputExpression(DbExpression inputExpression, string inputVarName, TypeUsage inputVarType, out Symbol fromSymbol)
		{
			ISqlFragment sqlFragment = inputExpression.Accept<ISqlFragment>(this);
			SqlSelectStatement sqlSelectStatement = sqlFragment as SqlSelectStatement;
			if (sqlSelectStatement == null)
			{
				sqlSelectStatement = new SqlSelectStatement();
				SqlGenerator.WrapNonQueryExtent(sqlSelectStatement, sqlFragment, inputExpression.ExpressionKind);
			}
			if (sqlSelectStatement.FromExtents.Count == 0)
			{
				fromSymbol = new Symbol(inputVarName, inputVarType);
			}
			else if (sqlSelectStatement.FromExtents.Count == 1)
			{
				fromSymbol = sqlSelectStatement.FromExtents[0];
			}
			else
			{
				fromSymbol = new JoinSymbol(inputVarName, inputVarType, sqlSelectStatement.FromExtents)
				{
					FlattenedExtentList = sqlSelectStatement.AllJoinExtents
				};
				sqlSelectStatement.FromExtents.Clear();
				sqlSelectStatement.FromExtents.Add(fromSymbol);
			}
			return sqlSelectStatement;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00015398 File Offset: 0x00013598
		private SqlBuilder VisitIsEmptyExpression(DbIsEmptyExpression e, bool negate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (!negate)
			{
				sqlBuilder.Append(" NOT");
			}
			sqlBuilder.Append(" EXISTS (");
			sqlBuilder.Append(this.VisitExpressionEnsureSqlStatement(e.Argument));
			sqlBuilder.AppendLine();
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000153E8 File Offset: 0x000135E8
		private ISqlFragment VisitCollectionConstructor(DbNewInstanceExpression e)
		{
			if (e.Arguments.Count == 1 && e.Arguments[0].ExpressionKind == DbExpressionKind.Element)
			{
				DbElementExpression dbElementExpression = e.Arguments[0] as DbElementExpression;
				SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(dbElementExpression.Argument);
				if (!SqlGenerator.IsCompatible(sqlSelectStatement, DbExpressionKind.Element))
				{
					TypeUsage elementTypeUsage = TypeHelpers.GetElementTypeUsage(dbElementExpression.Argument.ResultType);
					Symbol fromSymbol;
					sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, "element", elementTypeUsage, out fromSymbol);
					this.AddFromSymbol(sqlSelectStatement, "element", fromSymbol, false);
				}
				sqlSelectStatement.Select.Top = new TopClause(1, false);
				return sqlSelectStatement;
			}
			CollectionType edmType = TypeHelpers.GetEdmType<CollectionType>(e.ResultType);
			bool flag = TypeSemantics.IsPrimitiveType(edmType.TypeUsage);
			SqlBuilder sqlBuilder = new SqlBuilder();
			string s = "";
			if (e.Arguments.Count == 0)
			{
				sqlBuilder.Append(" SELECT CAST(null as ");
				sqlBuilder.Append(this.GetSqlPrimitiveType(edmType.TypeUsage));
				sqlBuilder.Append(") AS X FROM (SELECT 1) AS Y WHERE 1=0");
			}
			foreach (DbExpression dbExpression in e.Arguments)
			{
				sqlBuilder.Append(s);
				sqlBuilder.Append(" SELECT ");
				sqlBuilder.Append(dbExpression.Accept<ISqlFragment>(this));
				if (flag)
				{
					sqlBuilder.Append(" AS X ");
				}
				s = " UNION ALL ";
			}
			return sqlBuilder;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00015564 File Offset: 0x00013764
		private SqlBuilder VisitIsNullExpression(DbIsNullExpression e, bool negate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (e.Argument.ExpressionKind == DbExpressionKind.ParameterReference)
			{
				this._ignoreForceNonUnicodeFlag = true;
			}
			sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
			this._ignoreForceNonUnicodeFlag = false;
			if (!negate)
			{
				sqlBuilder.Append(" IS NULL");
			}
			else
			{
				sqlBuilder.Append(" IS NOT NULL");
			}
			return sqlBuilder;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000155C4 File Offset: 0x000137C4
		private ISqlFragment VisitJoinExpression(IList<DbExpressionBinding> inputs, DbExpressionKind joinKind, string joinString, DbExpression joinCondition)
		{
			SqlSelectStatement sqlSelectStatement;
			if (!this.IsParentAJoin)
			{
				sqlSelectStatement = new SqlSelectStatement();
				sqlSelectStatement.AllJoinExtents = new List<Symbol>();
				this.selectStatementStack.Push(sqlSelectStatement);
			}
			else
			{
				sqlSelectStatement = this.CurrentSelectStatement;
			}
			this.symbolTable.EnterScope();
			string text = "";
			bool flag = true;
			int count = inputs.Count;
			for (int i = 0; i < count; i++)
			{
				DbExpressionBinding dbExpressionBinding = inputs[i];
				if (text.Length != 0)
				{
					sqlSelectStatement.From.AppendLine();
				}
				sqlSelectStatement.From.Append(text + " ");
				bool item = dbExpressionBinding.Expression.ExpressionKind == DbExpressionKind.Scan || (flag && (SqlGenerator.IsJoinExpression(dbExpressionBinding.Expression) || SqlGenerator.IsApplyExpression(dbExpressionBinding.Expression)));
				this.isParentAJoinStack.Push(item);
				int count2 = sqlSelectStatement.FromExtents.Count;
				ISqlFragment fromExtentFragment = dbExpressionBinding.Expression.Accept<ISqlFragment>(this);
				this.isParentAJoinStack.Pop();
				this.ProcessJoinInputResult(fromExtentFragment, sqlSelectStatement, dbExpressionBinding, count2);
				text = joinString;
				flag = false;
			}
			if (joinKind == DbExpressionKind.FullOuterJoin || joinKind == DbExpressionKind.InnerJoin || joinKind == DbExpressionKind.LeftOuterJoin)
			{
				sqlSelectStatement.From.Append(" ON ");
				this.isParentAJoinStack.Push(false);
				sqlSelectStatement.From.Append(joinCondition.Accept<ISqlFragment>(this));
				this.isParentAJoinStack.Pop();
			}
			this.symbolTable.ExitScope();
			if (!this.IsParentAJoin)
			{
				this.selectStatementStack.Pop();
			}
			return sqlSelectStatement;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00015750 File Offset: 0x00013950
		private void ProcessJoinInputResult(ISqlFragment fromExtentFragment, SqlSelectStatement result, DbExpressionBinding input, int fromSymbolStart)
		{
			Symbol symbol = null;
			if (result != fromExtentFragment)
			{
				SqlSelectStatement sqlSelectStatement = fromExtentFragment as SqlSelectStatement;
				if (sqlSelectStatement != null)
				{
					if (sqlSelectStatement.Select.IsEmpty)
					{
						List<Symbol> columnList = this.AddDefaultColumns(sqlSelectStatement);
						if (SqlGenerator.IsJoinExpression(input.Expression) || SqlGenerator.IsApplyExpression(input.Expression))
						{
							List<Symbol> fromExtents = sqlSelectStatement.FromExtents;
							symbol = new JoinSymbol(input.VariableName, input.VariableType, fromExtents)
							{
								IsNestedJoin = true,
								ColumnList = columnList
							};
						}
						else
						{
							JoinSymbol joinSymbol = sqlSelectStatement.FromExtents[0] as JoinSymbol;
							if (joinSymbol != null)
							{
								symbol = new JoinSymbol(input.VariableName, input.VariableType, joinSymbol.ExtentList)
								{
									IsNestedJoin = true,
									ColumnList = columnList,
									FlattenedExtentList = joinSymbol.FlattenedExtentList
								};
							}
							else
							{
								symbol = new Symbol(input.VariableName, input.VariableType, sqlSelectStatement.OutputColumns, sqlSelectStatement.OutputColumnsRenamed);
							}
						}
					}
					else
					{
						symbol = new Symbol(input.VariableName, input.VariableType, sqlSelectStatement.OutputColumns, sqlSelectStatement.OutputColumnsRenamed);
					}
					result.From.Append(" (");
					result.From.Append(sqlSelectStatement);
					result.From.Append(" )");
				}
				else if (input.Expression is DbScanExpression)
				{
					result.From.Append(fromExtentFragment);
				}
				else
				{
					SqlGenerator.WrapNonQueryExtent(result, fromExtentFragment, input.Expression.ExpressionKind);
				}
				if (symbol == null)
				{
					symbol = new Symbol(input.VariableName, input.VariableType);
				}
				this.AddFromSymbol(result, input.VariableName, symbol);
				result.AllJoinExtents.Add(symbol);
				return;
			}
			List<Symbol> list = new List<Symbol>();
			for (int i = fromSymbolStart; i < result.FromExtents.Count; i++)
			{
				list.Add(result.FromExtents[i]);
			}
			result.FromExtents.RemoveRange(fromSymbolStart, result.FromExtents.Count - fromSymbolStart);
			symbol = new JoinSymbol(input.VariableName, input.VariableType, list);
			result.FromExtents.Add(symbol);
			this.symbolTable.Add(input.VariableName, symbol);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00015978 File Offset: 0x00013B78
		private ISqlFragment VisitNewInstanceExpression(DbNewInstanceExpression e, bool aliasesNeedRenaming, out Dictionary<string, Symbol> newColumns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			RowType rowType = e.ResultType.EdmType as RowType;
			if (rowType != null)
			{
				newColumns = new Dictionary<string, Symbol>(e.Arguments.Count);
				ReadOnlyMetadataCollection<EdmProperty> properties = rowType.Properties;
				string s = "";
				for (int i = 0; i < e.Arguments.Count; i++)
				{
					DbExpression dbExpression = e.Arguments[i];
					if (TypeSemantics.IsRowType(dbExpression.ResultType))
					{
						throw EntityUtil.NotSupported();
					}
					EdmProperty edmProperty = properties[i];
					sqlBuilder.Append(s);
					sqlBuilder.AppendLine();
					sqlBuilder.Append(dbExpression.Accept<ISqlFragment>(this));
					sqlBuilder.Append(" AS ");
					if (aliasesNeedRenaming)
					{
						Symbol symbol = new Symbol(edmProperty.Name, edmProperty.TypeUsage);
						symbol.NeedsRenaming = true;
						symbol.NewName = "Internal_" + edmProperty.Name;
						sqlBuilder.Append(symbol);
						newColumns.Add(edmProperty.Name, symbol);
					}
					else
					{
						sqlBuilder.Append(SqlGenerator.QuoteIdentifier(edmProperty.Name));
					}
					s = ", ";
				}
				return sqlBuilder;
			}
			throw EntityUtil.NotSupported();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00015AA8 File Offset: 0x00013CA8
		private ISqlFragment VisitSetOpExpression(DbExpression left, DbExpression right, string separator)
		{
			SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(left, true, true);
			SqlSelectStatement s = this.VisitExpressionEnsureSqlStatement(right, true, true);
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(sqlSelectStatement);
			sqlBuilder.AppendLine();
			sqlBuilder.Append(separator);
			sqlBuilder.AppendLine();
			sqlBuilder.Append(s);
			if (!sqlSelectStatement.OutputColumnsRenamed)
			{
				return sqlBuilder;
			}
			SqlSelectStatement sqlSelectStatement2 = new SqlSelectStatement();
			sqlSelectStatement2.From.Append("( ");
			sqlSelectStatement2.From.Append(sqlBuilder);
			sqlSelectStatement2.From.AppendLine();
			sqlSelectStatement2.From.Append(") ");
			Symbol fromSymbol = new Symbol("X", TypeHelpers.GetElementTypeUsage(left.ResultType), sqlSelectStatement.OutputColumns, true);
			this.AddFromSymbol(sqlSelectStatement2, null, fromSymbol, false);
			return sqlSelectStatement2;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00015B64 File Offset: 0x00013D64
		private void AddColumns(SqlSelectStatement selectStatement, Symbol symbol, List<Symbol> columnList, Dictionary<string, Symbol> columnDictionary)
		{
			JoinSymbol joinSymbol = symbol as JoinSymbol;
			if (joinSymbol != null)
			{
				if (!joinSymbol.IsNestedJoin)
				{
					using (List<Symbol>.Enumerator enumerator = joinSymbol.ExtentList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Symbol symbol2 = enumerator.Current;
							if (symbol2.Type != null && !TypeSemantics.IsPrimitiveType(symbol2.Type))
							{
								this.AddColumns(selectStatement, symbol2, columnList, columnDictionary);
							}
						}
						return;
					}
				}
				using (List<Symbol>.Enumerator enumerator2 = joinSymbol.ColumnList.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Symbol symbol3 = enumerator2.Current;
						OptionalColumn optionalColumn = this.CreateOptionalColumn(null, symbol3);
						optionalColumn.Append(symbol);
						optionalColumn.Append(".");
						optionalColumn.Append(symbol3);
						selectStatement.Select.AddOptionalColumn(optionalColumn);
						if (columnDictionary.ContainsKey(symbol3.Name))
						{
							columnDictionary[symbol3.Name].NeedsRenaming = true;
							symbol3.NeedsRenaming = true;
						}
						else
						{
							columnDictionary[symbol3.Name] = symbol3;
						}
						columnList.Add(symbol3);
					}
					return;
				}
			}
			if (symbol.OutputColumnsRenamed)
			{
				selectStatement.OutputColumnsRenamed = true;
			}
			if (selectStatement.OutputColumns == null)
			{
				selectStatement.OutputColumns = new Dictionary<string, Symbol>();
			}
			if (symbol.Type == null || TypeSemantics.IsPrimitiveType(symbol.Type))
			{
				this.AddColumn(selectStatement, symbol, columnList, columnDictionary, "X");
				return;
			}
			foreach (EdmProperty edmProperty in TypeHelpers.GetProperties(symbol.Type))
			{
				this.AddColumn(selectStatement, symbol, columnList, columnDictionary, edmProperty.Name);
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00015D44 File Offset: 0x00013F44
		private OptionalColumn CreateOptionalColumn(Symbol inputColumnSymbol, Symbol column)
		{
			if (!this.optionalColumnUsageManager.ContainsKey(column))
			{
				this.optionalColumnUsageManager.Add(inputColumnSymbol, column);
			}
			return new OptionalColumn(this.optionalColumnUsageManager, column);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00015D70 File Offset: 0x00013F70
		private void AddColumn(SqlSelectStatement selectStatement, Symbol symbol, List<Symbol> columnList, Dictionary<string, Symbol> columnDictionary, string columnName)
		{
			this.allColumnNames[columnName] = 0;
			Symbol symbol2 = null;
			symbol.OutputColumns.TryGetValue(columnName, out symbol2);
			Symbol symbol3;
			if (!symbol.Columns.TryGetValue(columnName, out symbol3))
			{
				symbol3 = ((symbol2 != null && symbol.OutputColumnsRenamed) ? symbol2 : new Symbol(columnName, null));
				symbol.Columns.Add(columnName, symbol3);
			}
			OptionalColumn optionalColumn = this.CreateOptionalColumn(symbol2, symbol3);
			optionalColumn.Append(symbol);
			optionalColumn.Append(".");
			if (symbol.OutputColumnsRenamed)
			{
				optionalColumn.Append(symbol2);
			}
			else
			{
				optionalColumn.Append(SqlGenerator.QuoteIdentifier(columnName));
			}
			optionalColumn.Append(" AS ");
			optionalColumn.Append(symbol3);
			selectStatement.Select.AddOptionalColumn(optionalColumn);
			if (!selectStatement.OutputColumns.ContainsKey(columnName))
			{
				selectStatement.OutputColumns.Add(columnName, symbol3);
			}
			if (columnDictionary.ContainsKey(columnName))
			{
				columnDictionary[columnName].NeedsRenaming = true;
				symbol3.NeedsRenaming = true;
			}
			else
			{
				columnDictionary[columnName] = symbol.Columns[columnName];
			}
			columnList.Add(symbol3);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00015E88 File Offset: 0x00014088
		private List<Symbol> AddDefaultColumns(SqlSelectStatement selectStatement)
		{
			List<Symbol> list = new List<Symbol>();
			Dictionary<string, Symbol> columnDictionary = new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase);
			foreach (Symbol symbol in selectStatement.FromExtents)
			{
				this.AddColumns(selectStatement, symbol, list, columnDictionary);
			}
			return list;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00015EF0 File Offset: 0x000140F0
		private void AddFromSymbol(SqlSelectStatement selectStatement, string inputVarName, Symbol fromSymbol)
		{
			this.AddFromSymbol(selectStatement, inputVarName, fromSymbol, true);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00015EFC File Offset: 0x000140FC
		private void AddFromSymbol(SqlSelectStatement selectStatement, string inputVarName, Symbol fromSymbol, bool addToSymbolTable)
		{
			if (selectStatement.FromExtents.Count == 0 || fromSymbol != selectStatement.FromExtents[0])
			{
				selectStatement.FromExtents.Add(fromSymbol);
				selectStatement.From.Append(" AS ");
				selectStatement.From.Append(fromSymbol);
				this.allExtentNames[fromSymbol.Name] = 0;
			}
			if (addToSymbolTable)
			{
				this.symbolTable.Add(inputVarName, fromSymbol);
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00015F70 File Offset: 0x00014170
		private void AddSortKeys(SqlBuilder orderByClause, IList<DbSortClause> sortKeys)
		{
			string s = "";
			foreach (DbSortClause dbSortClause in sortKeys)
			{
				orderByClause.Append(s);
				orderByClause.Append(dbSortClause.Expression.Accept<ISqlFragment>(this));
				if (!string.IsNullOrEmpty(dbSortClause.Collation))
				{
					orderByClause.Append(" COLLATE ");
					orderByClause.Append(dbSortClause.Collation);
				}
				orderByClause.Append(dbSortClause.Ascending ? " ASC" : " DESC");
				s = ", ";
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00016014 File Offset: 0x00014214
		private SqlSelectStatement CreateNewSelectStatement(SqlSelectStatement oldStatement, string inputVarName, TypeUsage inputVarType, out Symbol fromSymbol)
		{
			return this.CreateNewSelectStatement(oldStatement, inputVarName, inputVarType, true, out fromSymbol);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00016024 File Offset: 0x00014224
		private SqlSelectStatement CreateNewSelectStatement(SqlSelectStatement oldStatement, string inputVarName, TypeUsage inputVarType, bool finalizeOldStatement, out Symbol fromSymbol)
		{
			fromSymbol = null;
			if (finalizeOldStatement && oldStatement.Select.IsEmpty)
			{
				List<Symbol> columnList = this.AddDefaultColumns(oldStatement);
				JoinSymbol joinSymbol = oldStatement.FromExtents[0] as JoinSymbol;
				if (joinSymbol != null)
				{
					fromSymbol = new JoinSymbol(inputVarName, inputVarType, joinSymbol.ExtentList)
					{
						IsNestedJoin = true,
						ColumnList = columnList,
						FlattenedExtentList = joinSymbol.FlattenedExtentList
					};
				}
			}
			if (fromSymbol == null)
			{
				fromSymbol = new Symbol(inputVarName, inputVarType, oldStatement.OutputColumns, oldStatement.OutputColumnsRenamed);
			}
			SqlSelectStatement sqlSelectStatement = new SqlSelectStatement();
			sqlSelectStatement.From.Append("( ");
			sqlSelectStatement.From.Append(oldStatement);
			sqlSelectStatement.From.AppendLine();
			sqlSelectStatement.From.Append(") ");
			return sqlSelectStatement;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000160E8 File Offset: 0x000142E8
		private static string EscapeSingleQuote(string s, bool isUnicode)
		{
			return (isUnicode ? "N'" : "'") + s.Replace("'", "''") + "'";
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00016114 File Offset: 0x00014314
		private string GetSqlPrimitiveType(TypeUsage type)
		{
			TypeUsage storeType = this._storeItemCollection.StoreProviderManifest.GetStoreType(type);
			return SqlGenerator.GenerateSqlForStoreType(this.sqlVersion, storeType);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00016140 File Offset: 0x00014340
		internal static string GenerateSqlForStoreType(SqlVersion sqlVersion, TypeUsage storeTypeUsage)
		{
			string text = storeTypeUsage.EdmType.Name;
			int num = 0;
			byte b = 0;
			byte b2 = 0;
			PrimitiveTypeKind primitiveTypeKind = ((PrimitiveType)storeTypeUsage.EdmType).PrimitiveTypeKind;
			switch (primitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				if (!TypeHelpers.IsFacetValueConstant(storeTypeUsage, "MaxLength"))
				{
					bool flag = TypeHelpers.TryGetMaxLength(storeTypeUsage, out num);
					text = text + "(" + num.ToString(CultureInfo.InvariantCulture) + ")";
				}
				break;
			case PrimitiveTypeKind.Boolean:
			case PrimitiveTypeKind.Byte:
				break;
			case PrimitiveTypeKind.DateTime:
				text = (SqlVersionUtils.IsPreKatmai(sqlVersion) ? "datetime" : "datetime2");
				break;
			case PrimitiveTypeKind.Decimal:
				if (!TypeHelpers.IsFacetValueConstant(storeTypeUsage, "Precision"))
				{
					bool flag = TypeHelpers.TryGetPrecision(storeTypeUsage, out b);
					flag = TypeHelpers.TryGetScale(storeTypeUsage, out b2);
					text = string.Concat(new string[]
					{
						text,
						"(",
						b.ToString(),
						",",
						b2.ToString(),
						")"
					});
				}
				break;
			default:
				switch (primitiveTypeKind)
				{
				case PrimitiveTypeKind.String:
					if (!TypeHelpers.IsFacetValueConstant(storeTypeUsage, "MaxLength"))
					{
						bool flag = TypeHelpers.TryGetMaxLength(storeTypeUsage, out num);
						text = text + "(" + num.ToString(CultureInfo.InvariantCulture) + ")";
					}
					break;
				case PrimitiveTypeKind.Time:
					SqlGenerator.AssertKatmaiOrNewer(sqlVersion, primitiveTypeKind);
					text = "time";
					break;
				case PrimitiveTypeKind.DateTimeOffset:
					SqlGenerator.AssertKatmaiOrNewer(sqlVersion, primitiveTypeKind);
					text = "datetimeoffset";
					break;
				}
				break;
			}
			return text;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000162B4 File Offset: 0x000144B4
		private ISqlFragment HandleCountExpression(DbExpression e)
		{
			ISqlFragment result;
			if (e.ExpressionKind == DbExpressionKind.Constant)
			{
				SqlBuilder sqlBuilder = new SqlBuilder();
				sqlBuilder.Append(((DbConstantExpression)e).Value.ToString());
				result = sqlBuilder;
			}
			else
			{
				result = e.Accept<ISqlFragment>(this);
			}
			return result;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000162F3 File Offset: 0x000144F3
		private static bool IsApplyExpression(DbExpression e)
		{
			return DbExpressionKind.CrossApply == e.ExpressionKind || DbExpressionKind.OuterApply == e.ExpressionKind;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001630A File Offset: 0x0001450A
		private static bool IsJoinExpression(DbExpression e)
		{
			return DbExpressionKind.CrossJoin == e.ExpressionKind || DbExpressionKind.FullOuterJoin == e.ExpressionKind || DbExpressionKind.InnerJoin == e.ExpressionKind || DbExpressionKind.LeftOuterJoin == e.ExpressionKind;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00016338 File Offset: 0x00014538
		private static bool IsComplexExpression(DbExpression e)
		{
			DbExpressionKind expressionKind = e.ExpressionKind;
			return expressionKind - DbExpressionKind.Cast > 1 && expressionKind != DbExpressionKind.ParameterReference && expressionKind != DbExpressionKind.Property;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00016360 File Offset: 0x00014560
		private static bool IsCompatible(SqlSelectStatement result, DbExpressionKind expressionKind)
		{
			if (expressionKind <= DbExpressionKind.GroupBy)
			{
				if (expressionKind <= DbExpressionKind.Element)
				{
					if (expressionKind == DbExpressionKind.Distinct)
					{
						return result.Select.Top == null && result.OrderBy.IsEmpty;
					}
					if (expressionKind != DbExpressionKind.Element)
					{
						goto IL_19F;
					}
				}
				else
				{
					if (expressionKind == DbExpressionKind.Filter)
					{
						return result.Select.IsEmpty && result.Where.IsEmpty && result.GroupBy.IsEmpty && result.Select.Top == null;
					}
					if (expressionKind != DbExpressionKind.GroupBy)
					{
						goto IL_19F;
					}
					return result.Select.IsEmpty && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty && result.Select.Top == null && !result.Select.IsDistinct;
				}
			}
			else if (expressionKind <= DbExpressionKind.Project)
			{
				if (expressionKind != DbExpressionKind.Limit)
				{
					if (expressionKind != DbExpressionKind.Project)
					{
						goto IL_19F;
					}
					return result.Select.IsEmpty && result.GroupBy.IsEmpty && !result.Select.IsDistinct;
				}
			}
			else
			{
				if (expressionKind == DbExpressionKind.Skip)
				{
					return result.Select.IsEmpty && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty && !result.Select.IsDistinct;
				}
				if (expressionKind != DbExpressionKind.Sort)
				{
					goto IL_19F;
				}
				return result.Select.IsEmpty && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty && !result.Select.IsDistinct;
			}
			return result.Select.Top == null;
			IL_19F:
			throw EntityUtil.InvalidOperation(string.Empty);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000033BE File Offset: 0x000015BE
		internal static string QuoteIdentifier(string name)
		{
			return "[" + name.Replace("]", "]]") + "]";
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00016516 File Offset: 0x00014716
		private SqlSelectStatement VisitExpressionEnsureSqlStatement(DbExpression e)
		{
			return this.VisitExpressionEnsureSqlStatement(e, true, false);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00016524 File Offset: 0x00014724
		private SqlSelectStatement VisitExpressionEnsureSqlStatement(DbExpression e, bool addDefaultColumns, bool markAllDefaultColumnsAsUsed)
		{
			DbExpressionKind expressionKind = e.ExpressionKind;
			if (expressionKind <= DbExpressionKind.GroupBy)
			{
				if (expressionKind != DbExpressionKind.Filter && expressionKind != DbExpressionKind.GroupBy)
				{
					goto IL_3A;
				}
			}
			else if (expressionKind != DbExpressionKind.Project && expressionKind != DbExpressionKind.Sort)
			{
				goto IL_3A;
			}
			SqlSelectStatement sqlSelectStatement = e.Accept<ISqlFragment>(this) as SqlSelectStatement;
			goto IL_C1;
			IL_3A:
			string inputVarName = "c";
			this.symbolTable.EnterScope();
			DbExpressionKind expressionKind2 = e.ExpressionKind;
			if (expressionKind2 <= DbExpressionKind.InnerJoin)
			{
				if (expressionKind2 - DbExpressionKind.CrossApply > 1 && expressionKind2 != DbExpressionKind.FullOuterJoin && expressionKind2 != DbExpressionKind.InnerJoin)
				{
					goto IL_90;
				}
			}
			else if (expressionKind2 != DbExpressionKind.LeftOuterJoin && expressionKind2 != DbExpressionKind.OuterApply && expressionKind2 != DbExpressionKind.Scan)
			{
				goto IL_90;
			}
			TypeUsage inputVarType = TypeHelpers.GetElementTypeUsage(e.ResultType);
			goto IL_A1;
			IL_90:
			inputVarType = TypeHelpers.GetEdmType<CollectionType>(e.ResultType).TypeUsage;
			IL_A1:
			Symbol fromSymbol;
			sqlSelectStatement = this.VisitInputExpression(e, inputVarName, inputVarType, out fromSymbol);
			this.AddFromSymbol(sqlSelectStatement, inputVarName, fromSymbol);
			this.symbolTable.ExitScope();
			IL_C1:
			if (addDefaultColumns && sqlSelectStatement.Select.IsEmpty)
			{
				List<Symbol> list = this.AddDefaultColumns(sqlSelectStatement);
				if (markAllDefaultColumnsAsUsed)
				{
					foreach (Symbol key in list)
					{
						this.optionalColumnUsageManager.MarkAsUsed(key);
					}
				}
			}
			return sqlSelectStatement;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001665C File Offset: 0x0001485C
		private SqlSelectStatement VisitFilterExpression(DbExpressionBinding input, DbExpression predicate, bool negatePredicate)
		{
			Symbol fromSymbol;
			SqlSelectStatement sqlSelectStatement = this.VisitInputExpression(input.Expression, input.VariableName, input.VariableType, out fromSymbol);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, DbExpressionKind.Filter))
			{
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, input.VariableName, input.VariableType, out fromSymbol);
			}
			this.selectStatementStack.Push(sqlSelectStatement);
			this.symbolTable.EnterScope();
			this.AddFromSymbol(sqlSelectStatement, input.VariableName, fromSymbol);
			if (negatePredicate)
			{
				sqlSelectStatement.Where.Append("NOT (");
			}
			sqlSelectStatement.Where.Append(predicate.Accept<ISqlFragment>(this));
			if (negatePredicate)
			{
				sqlSelectStatement.Where.Append(")");
			}
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			return sqlSelectStatement;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00016719 File Offset: 0x00014919
		private static void WrapNonQueryExtent(SqlSelectStatement result, ISqlFragment sqlFragment, DbExpressionKind expressionKind)
		{
			if (expressionKind == DbExpressionKind.Function)
			{
				result.From.Append(sqlFragment);
				return;
			}
			result.From.Append(" (");
			result.From.Append(sqlFragment);
			result.From.Append(")");
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001675C File Offset: 0x0001495C
		private static string ByteArrayToBinaryString(byte[] binaryArray)
		{
			StringBuilder stringBuilder = new StringBuilder(binaryArray.Length * 2);
			for (int i = 0; i < binaryArray.Length; i++)
			{
				stringBuilder.Append(SqlGenerator.hexDigits[(binaryArray[i] & 240) >> 4]).Append(SqlGenerator.hexDigits[(int)(binaryArray[i] & 15)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000167B4 File Offset: 0x000149B4
		private TypeUsage GetPrimitiveType(PrimitiveTypeKind modelType)
		{
			PrimitiveType mappedPrimitiveType = this._storeItemCollection.GetMappedPrimitiveType(modelType);
			return TypeUsage.CreateDefaultTypeUsage(mappedPrimitiveType);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000167D8 File Offset: 0x000149D8
		private static bool GroupByAggregatesNeedInnerQuery(IList<DbAggregate> aggregates, string inputVarRefName)
		{
			foreach (DbAggregate dbAggregate in aggregates)
			{
				if (SqlGenerator.GroupByAggregateNeedsInnerQuery(dbAggregate.Arguments[0], inputVarRefName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00016834 File Offset: 0x00014A34
		private static bool GroupByAggregateNeedsInnerQuery(DbExpression expression, string inputVarRefName)
		{
			return SqlGenerator.GroupByExpressionNeedsInnerQuery(expression, inputVarRefName, true);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00016840 File Offset: 0x00014A40
		private static bool GroupByKeysNeedInnerQuery(IList<DbExpression> keys, string inputVarRefName)
		{
			foreach (DbExpression expression in keys)
			{
				if (SqlGenerator.GroupByKeyNeedsInnerQuery(expression, inputVarRefName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00016894 File Offset: 0x00014A94
		private static bool GroupByKeyNeedsInnerQuery(DbExpression expression, string inputVarRefName)
		{
			return SqlGenerator.GroupByExpressionNeedsInnerQuery(expression, inputVarRefName, false);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000168A0 File Offset: 0x00014AA0
		private static bool GroupByExpressionNeedsInnerQuery(DbExpression expression, string inputVarRefName, bool allowConstants)
		{
			if (allowConstants && expression.ExpressionKind == DbExpressionKind.Constant)
			{
				return false;
			}
			if (expression.ExpressionKind == DbExpressionKind.Cast)
			{
				DbCastExpression dbCastExpression = (DbCastExpression)expression;
				return SqlGenerator.GroupByExpressionNeedsInnerQuery(dbCastExpression.Argument, inputVarRefName, allowConstants);
			}
			if (expression.ExpressionKind == DbExpressionKind.Property)
			{
				DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)expression;
				return SqlGenerator.GroupByExpressionNeedsInnerQuery(dbPropertyExpression.Instance, inputVarRefName, allowConstants);
			}
			if (expression.ExpressionKind == DbExpressionKind.VariableReference)
			{
				DbVariableReferenceExpression dbVariableReferenceExpression = expression as DbVariableReferenceExpression;
				return !dbVariableReferenceExpression.VariableName.Equals(inputVarRefName);
			}
			return true;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001691A File Offset: 0x00014B1A
		private void AssertKatmaiOrNewer(PrimitiveTypeKind primitiveTypeKind)
		{
			SqlGenerator.AssertKatmaiOrNewer(this.sqlVersion, primitiveTypeKind);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00016928 File Offset: 0x00014B28
		private static void AssertKatmaiOrNewer(SqlVersion sqlVersion, PrimitiveTypeKind primitiveTypeKind)
		{
			if (SqlVersionUtils.IsPreKatmai(sqlVersion))
			{
				throw EntityUtil.NotSupported(Strings.SqlGen_PrimitiveTypeNotSupportedPriorSql10(primitiveTypeKind));
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00016943 File Offset: 0x00014B43
		internal void AssertKatmaiOrNewer(DbFunctionExpression e)
		{
			if (this.IsPreKatmai)
			{
				throw EntityUtil.NotSupported(Strings.SqlGen_CanonicalFunctionNotSupportedPriorSql10(e.Function.Name));
			}
		}

		// Token: 0x0400070D RID: 1805
		private Stack<SqlSelectStatement> selectStatementStack;

		// Token: 0x0400070E RID: 1806
		private Stack<bool> isParentAJoinStack;

		// Token: 0x0400070F RID: 1807
		private Dictionary<string, int> allExtentNames;

		// Token: 0x04000710 RID: 1808
		private Dictionary<string, int> allColumnNames;

		// Token: 0x04000711 RID: 1809
		private readonly SymbolTable symbolTable = new SymbolTable();

		// Token: 0x04000712 RID: 1810
		private bool isVarRefSingle;

		// Token: 0x04000713 RID: 1811
		private readonly SymbolUsageManager optionalColumnUsageManager = new SymbolUsageManager();

		// Token: 0x04000714 RID: 1812
		private Dictionary<string, bool> _candidateParametersToForceNonUnicode = new Dictionary<string, bool>();

		// Token: 0x04000715 RID: 1813
		private bool _forceNonUnicode;

		// Token: 0x04000716 RID: 1814
		private bool _ignoreForceNonUnicodeFlag;

		// Token: 0x04000717 RID: 1815
		private const byte defaultDecimalPrecision = 18;

		// Token: 0x04000718 RID: 1816
		private static readonly char[] hexDigits = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F'
		};

		// Token: 0x04000719 RID: 1817
		private static readonly Set<string> _canonicalStringFunctionsOneArg = new Set<string>(new string[]
		{
			"Edm.Trim",
			"Edm.RTrim",
			"Edm.LTrim",
			"Edm.Left",
			"Edm.Right",
			"Edm.Substring",
			"Edm.ToLower",
			"Edm.ToUpper",
			"Edm.Reverse"
		}, StringComparer.Ordinal).MakeReadOnly();

		// Token: 0x0400071A RID: 1818
		private static readonly Set<string> _canonicalStringFunctionsTwoArgs = new Set<string>(new string[]
		{
			"Edm.Concat"
		}, StringComparer.Ordinal).MakeReadOnly();

		// Token: 0x0400071B RID: 1819
		private static readonly Set<string> _canonicalStringFunctionsThreeArgs = new Set<string>(new string[]
		{
			"Edm.Replace"
		}, StringComparer.Ordinal).MakeReadOnly();

		// Token: 0x0400071C RID: 1820
		private static readonly Set<string> _storeStringFunctionsOneArg = new Set<string>(new string[]
		{
			"SqlServer.RTRIM",
			"SqlServer.LTRIM",
			"SqlServer.LEFT",
			"SqlServer.RIGHT",
			"SqlServer.SUBSTRING",
			"SqlServer.LOWER",
			"SqlServer.UPPER",
			"SqlServer.REVERSE"
		}, StringComparer.Ordinal).MakeReadOnly();

		// Token: 0x0400071D RID: 1821
		private static readonly Set<string> _storeStringFunctionsThreeArgs = new Set<string>(new string[]
		{
			"SqlServer.REPLACE"
		}, StringComparer.Ordinal).MakeReadOnly();

		// Token: 0x0400071E RID: 1822
		private SqlVersion sqlVersion;

		// Token: 0x0400071F RID: 1823
		private MetadataWorkspace metadataWorkspace;

		// Token: 0x04000720 RID: 1824
		private TypeUsage integerType;

		// Token: 0x04000721 RID: 1825
		private string defaultStringTypeName;

		// Token: 0x04000722 RID: 1826
		private StoreItemCollection _storeItemCollection;

		// Token: 0x02000456 RID: 1110
		private class KeyFieldExpressionComparer : IEqualityComparer<DbExpression>
		{
			// Token: 0x06003AAF RID: 15023 RVA: 0x00002050 File Offset: 0x00000250
			private KeyFieldExpressionComparer()
			{
			}

			// Token: 0x06003AB0 RID: 15024 RVA: 0x000DEA2C File Offset: 0x000DCC2C
			public bool Equals(DbExpression x, DbExpression y)
			{
				if (x.ExpressionKind != y.ExpressionKind)
				{
					return false;
				}
				DbExpressionKind expressionKind = x.ExpressionKind;
				if (expressionKind <= DbExpressionKind.ParameterReference)
				{
					if (expressionKind == DbExpressionKind.Cast)
					{
						DbCastExpression dbCastExpression = (DbCastExpression)x;
						DbCastExpression dbCastExpression2 = (DbCastExpression)y;
						return dbCastExpression.ResultType == dbCastExpression2.ResultType && this.Equals(dbCastExpression.Argument, dbCastExpression2.Argument);
					}
					if (expressionKind == DbExpressionKind.ParameterReference)
					{
						DbParameterReferenceExpression dbParameterReferenceExpression = (DbParameterReferenceExpression)x;
						DbParameterReferenceExpression dbParameterReferenceExpression2 = (DbParameterReferenceExpression)y;
						return dbParameterReferenceExpression.ParameterName == dbParameterReferenceExpression2.ParameterName;
					}
				}
				else
				{
					if (expressionKind == DbExpressionKind.Property)
					{
						DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)x;
						DbPropertyExpression dbPropertyExpression2 = (DbPropertyExpression)y;
						return dbPropertyExpression.Property == dbPropertyExpression2.Property && this.Equals(dbPropertyExpression.Instance, dbPropertyExpression2.Instance);
					}
					if (expressionKind == DbExpressionKind.VariableReference)
					{
						return x == y;
					}
				}
				return false;
			}

			// Token: 0x06003AB1 RID: 15025 RVA: 0x000DEB04 File Offset: 0x000DCD04
			public int GetHashCode(DbExpression obj)
			{
				DbExpressionKind expressionKind = obj.ExpressionKind;
				if (expressionKind <= DbExpressionKind.ParameterReference)
				{
					if (expressionKind == DbExpressionKind.Cast)
					{
						return this.GetHashCode(((DbCastExpression)obj).Argument);
					}
					if (expressionKind == DbExpressionKind.ParameterReference)
					{
						return ((DbParameterReferenceExpression)obj).ParameterName.GetHashCode() ^ int.MaxValue;
					}
				}
				else
				{
					if (expressionKind == DbExpressionKind.Property)
					{
						return ((DbPropertyExpression)obj).Property.GetHashCode();
					}
					if (expressionKind == DbExpressionKind.VariableReference)
					{
						return ((DbVariableReferenceExpression)obj).VariableName.GetHashCode();
					}
				}
				return obj.GetHashCode();
			}

			// Token: 0x04001924 RID: 6436
			internal static readonly SqlGenerator.KeyFieldExpressionComparer Singleton = new SqlGenerator.KeyFieldExpressionComparer();
		}
	}
}
