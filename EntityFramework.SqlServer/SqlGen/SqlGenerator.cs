using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Spatial;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000039 RID: 57
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal class SqlGenerator : DbExpressionVisitor<ISqlFragment>
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000F63C File Offset: 0x0000D83C
		private SqlSelectStatement CurrentSelectStatement
		{
			get
			{
				return this.selectStatementStack.Peek();
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000F649 File Offset: 0x0000D849
		private bool IsParentAJoin
		{
			get
			{
				return this.isParentAJoinStack.Count != 0 && this.isParentAJoinStack.Peek();
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000F665 File Offset: 0x0000D865
		internal Dictionary<string, int> AllExtentNames
		{
			get
			{
				return this.allExtentNames;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000F66D File Offset: 0x0000D86D
		internal Dictionary<string, int> AllColumnNames
		{
			get
			{
				return this.allColumnNames;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000F675 File Offset: 0x0000D875
		public List<string> Targets
		{
			get
			{
				return this._targets;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000F67D File Offset: 0x0000D87D
		internal SqlVersion SqlVersion
		{
			get
			{
				return this._sqlVersion;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000F685 File Offset: 0x0000D885
		internal bool IsPreKatmai
		{
			get
			{
				return SqlVersionUtils.IsPreKatmai(this.SqlVersion);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
		internal TypeUsage IntegerType
		{
			get
			{
				TypeUsage result;
				if ((result = this._integerType) == null)
				{
					result = (this._integerType = TypeUsage.CreateDefaultTypeUsage(this.StoreItemCollection.GetPrimitiveTypes().First((PrimitiveType t) => t.PrimitiveTypeKind == PrimitiveTypeKind.Int64)));
				}
				return result;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000F6F2 File Offset: 0x0000D8F2
		internal virtual StoreItemCollection StoreItemCollection
		{
			get
			{
				return this._storeItemCollection;
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000F6FA File Offset: 0x0000D8FA
		internal SqlGenerator()
		{
			this._sqlVersion = SqlVersion.Sql11;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000F72B File Offset: 0x0000D92B
		internal SqlGenerator(SqlVersion sqlVersion)
		{
			this._sqlVersion = sqlVersion;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000F75C File Offset: 0x0000D95C
		internal static string GenerateSql(DbCommandTree tree, SqlVersion sqlVersion, out List<SqlParameter> parameters, out CommandType commandType, out HashSet<string> paramsToForceNonUnicode)
		{
			commandType = CommandType.Text;
			parameters = null;
			paramsToForceNonUnicode = null;
			SqlGenerator sqlGenerator = new SqlGenerator(sqlVersion);
			switch (tree.CommandTreeKind)
			{
			case DbCommandTreeKind.Query:
				return sqlGenerator.GenerateSql((DbQueryCommandTree)tree, out paramsToForceNonUnicode);
			case DbCommandTreeKind.Update:
				return DmlSqlGenerator.GenerateUpdateSql((DbUpdateCommandTree)tree, sqlGenerator, out parameters, true, true);
			case DbCommandTreeKind.Insert:
				return DmlSqlGenerator.GenerateInsertSql((DbInsertCommandTree)tree, sqlGenerator, out parameters, true, true, true);
			case DbCommandTreeKind.Delete:
				return DmlSqlGenerator.GenerateDeleteSql((DbDeleteCommandTree)tree, sqlGenerator, out parameters, true, true);
			case DbCommandTreeKind.Function:
				return SqlGenerator.GenerateFunctionSql((DbFunctionCommandTree)tree, out commandType);
			default:
				return null;
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000F7EC File Offset: 0x0000D9EC
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

		// Token: 0x06000399 RID: 921 RVA: 0x0000F884 File Offset: 0x0000DA84
		internal string GenerateSql(DbQueryCommandTree tree, out HashSet<string> paramsToForceNonUnicode)
		{
			this._targets = new List<string>();
			DbQueryCommandTree dbQueryCommandTree = tree;
			if (this.SqlVersion == SqlVersion.Sql8 && Sql8ConformanceChecker.NeedsRewrite(tree.Query))
			{
				dbQueryCommandTree = Sql8ExpressionRewriter.Rewrite(tree);
			}
			this._storeItemCollection = (StoreItemCollection)dbQueryCommandTree.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
			this.selectStatementStack = new Stack<SqlSelectStatement>();
			this.isParentAJoinStack = new Stack<bool>();
			this.allExtentNames = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			this.allColumnNames = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			ISqlFragment sqlStatement;
			if (BuiltInTypeKind.CollectionType == dbQueryCommandTree.Query.ResultType.EdmType.BuiltInTypeKind)
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
				throw new NotSupportedException();
			}
			paramsToForceNonUnicode = new HashSet<string>((from p in this._candidateParametersToForceNonUnicode
			where p.Value
			select p into q
			select q.Key).ToList<string>());
			StringBuilder stringBuilder = new StringBuilder(1024);
			using (SqlWriter sqlWriter = new SqlWriter(stringBuilder))
			{
				this.WriteSql(sqlWriter, sqlStatement);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000FA08 File Offset: 0x0000DC08
		internal SqlWriter WriteSql(SqlWriter writer, ISqlFragment sqlStatement)
		{
			sqlStatement.WriteSql(writer, this);
			return writer;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000FA13 File Offset: 0x0000DC13
		public override ISqlFragment Visit(DbAndExpression e)
		{
			Check.NotNull<DbAndExpression>(e, "e");
			return this.VisitBinaryExpression(" AND ", DbExpressionKind.And, e.Left, e.Right);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		public override ISqlFragment Visit(DbApplyExpression e)
		{
			Check.NotNull<DbApplyExpression>(e, "e");
			List<DbExpressionBinding> list = new List<DbExpressionBinding>();
			list.Add(e.Input);
			list.Add(e.Apply);
			DbExpressionKind expressionKind = e.ExpressionKind;
			string joinString;
			if (expressionKind != DbExpressionKind.CrossApply)
			{
				if (expressionKind != DbExpressionKind.OuterApply)
				{
					throw new InvalidOperationException(string.Empty);
				}
				joinString = "OUTER APPLY";
			}
			else
			{
				joinString = "CROSS APPLY";
			}
			return this.VisitJoinExpression(list, DbExpressionKind.CrossJoin, joinString, null);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000FAAC File Offset: 0x0000DCAC
		public override ISqlFragment Visit(DbArithmeticExpression e)
		{
			Check.NotNull<DbArithmeticExpression>(e, "e");
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
			throw new InvalidOperationException(string.Empty);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		public override ISqlFragment Visit(DbCaseExpression e)
		{
			Check.NotNull<DbCaseExpression>(e, "e");
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

		// Token: 0x0600039F RID: 927 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public override ISqlFragment Visit(DbCastExpression e)
		{
			Check.NotNull<DbCastExpression>(e, "e");
			if (e.ResultType.IsSpatialType())
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

		// Token: 0x060003A0 RID: 928 RVA: 0x0000FD70 File Offset: 0x0000DF70
		public override ISqlFragment Visit(DbComparisonExpression e)
		{
			Check.NotNull<DbComparisonExpression>(e, "e");
			if (e.Left.ResultType.IsPrimitiveType(PrimitiveTypeKind.String))
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
					goto IL_11C;
				}
				switch (expressionKind)
				{
				case DbExpressionKind.GreaterThan:
					result = this.VisitComparisonExpression(" > ", e.Left, e.Right);
					goto IL_11C;
				case DbExpressionKind.GreaterThanOrEquals:
					result = this.VisitComparisonExpression(" >= ", e.Left, e.Right);
					goto IL_11C;
				}
			}
			else
			{
				switch (expressionKind)
				{
				case DbExpressionKind.LessThan:
					result = this.VisitComparisonExpression(" < ", e.Left, e.Right);
					goto IL_11C;
				case DbExpressionKind.LessThanOrEquals:
					result = this.VisitComparisonExpression(" <= ", e.Left, e.Right);
					goto IL_11C;
				default:
					if (expressionKind == DbExpressionKind.NotEquals)
					{
						result = this.VisitComparisonExpression(" <> ", e.Left, e.Right);
						goto IL_11C;
					}
					break;
				}
			}
			throw new InvalidOperationException(string.Empty);
			IL_11C:
			this._forceNonUnicode = false;
			return result;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000FEA1 File Offset: 0x0000E0A1
		private bool CheckIfForceNonUnicodeRequired(DbExpression e)
		{
			if (this._forceNonUnicode)
			{
				throw new NotSupportedException();
			}
			return this.MatchPatternForForcingNonUnicode(e);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
		private bool MatchPatternForForcingNonUnicode(DbExpression e)
		{
			DbExpressionKind expressionKind = e.ExpressionKind;
			if (expressionKind == DbExpressionKind.Like)
			{
				DbLikeExpression dbLikeExpression = (DbLikeExpression)e;
				return SqlGenerator.MatchSourcePatternForForcingNonUnicode(dbLikeExpression.Argument) && this.MatchTargetPatternForForcingNonUnicode(dbLikeExpression.Pattern) && this.MatchTargetPatternForForcingNonUnicode(dbLikeExpression.Escape);
			}
			if (expressionKind != DbExpressionKind.In)
			{
				DbComparisonExpression dbComparisonExpression = (DbComparisonExpression)e;
				DbExpression left = dbComparisonExpression.Left;
				DbExpression right = dbComparisonExpression.Right;
				return (SqlGenerator.MatchSourcePatternForForcingNonUnicode(left) && this.MatchTargetPatternForForcingNonUnicode(right)) || (SqlGenerator.MatchSourcePatternForForcingNonUnicode(right) && this.MatchTargetPatternForForcingNonUnicode(left));
			}
			DbInExpression dbInExpression = (DbInExpression)e;
			return SqlGenerator.MatchSourcePatternForForcingNonUnicode(dbInExpression.Item);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000FF5C File Offset: 0x0000E15C
		internal bool MatchTargetPatternForForcingNonUnicode(DbExpression expr)
		{
			if (SqlGenerator.IsConstParamOrNullExpressionUnicodeNotSpecified(expr))
			{
				return true;
			}
			if (expr.ExpressionKind == DbExpressionKind.Function)
			{
				DbFunctionExpression dbFunctionExpression = (DbFunctionExpression)expr;
				EdmFunction function = dbFunctionExpression.Function;
				if (!function.IsCanonicalFunction() && !SqlFunctionCallHandler.IsStoreFunction(function))
				{
					return false;
				}
				string fullName = function.FullName;
				if (SqlGenerator._canonicalAndStoreStringFunctionsOneArg.Contains(fullName))
				{
					return this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[0]);
				}
				if ("Edm.Concat".Equals(fullName, StringComparison.Ordinal))
				{
					return this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[0]) && this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[1]);
				}
				if ("Edm.Replace".Equals(fullName, StringComparison.Ordinal) || "SqlServer.REPLACE".Equals(fullName, StringComparison.Ordinal))
				{
					return this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[0]) && this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[1]) && this.MatchTargetPatternForForcingNonUnicode(dbFunctionExpression.Arguments[2]);
				}
			}
			return false;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00010058 File Offset: 0x0000E258
		private static bool MatchSourcePatternForForcingNonUnicode(DbExpression argument)
		{
			bool flag;
			return argument.ExpressionKind == DbExpressionKind.Property && argument.ResultType.TryGetIsUnicode(out flag) && !flag;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00010084 File Offset: 0x0000E284
		internal static bool IsConstParamOrNullExpressionUnicodeNotSpecified(DbExpression argument)
		{
			DbExpressionKind expressionKind = argument.ExpressionKind;
			TypeUsage resultType = argument.ResultType;
			bool flag;
			return resultType.IsPrimitiveType(PrimitiveTypeKind.String) && (expressionKind == DbExpressionKind.Constant || expressionKind == DbExpressionKind.ParameterReference || expressionKind == DbExpressionKind.Null) && !resultType.TryGetFacetValue("Unicode", out flag);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000100CC File Offset: 0x0000E2CC
		private ISqlFragment VisitConstant(DbConstantExpression e, bool isCastOptional)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			TypeUsage resultType = e.ResultType;
			if (resultType.IsPrimitiveType())
			{
				PrimitiveTypeKind primitiveTypeKind = resultType.GetPrimitiveTypeKind();
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
					if (!e.ResultType.TryGetIsUnicode(out isUnicode))
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
				throw new NotSupportedException(Strings.NoStoreTypeForEdmType(resultType.EdmType.Name, ((PrimitiveType)resultType.EdmType).PrimitiveTypeKind));
			}
			throw new NotSupportedException();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001050C File Offset: 0x0000E70C
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

		// Token: 0x060003A8 RID: 936 RVA: 0x00010638 File Offset: 0x0000E838
		private static void AssertValidDouble(double value)
		{
			if (double.IsNaN(value))
			{
				throw new NotSupportedException(Strings.SqlGen_TypedNaNNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Double)));
			}
			if (double.IsPositiveInfinity(value))
			{
				throw new NotSupportedException(Strings.SqlGen_TypedPositiveInfinityNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Double), typeof(double).Name));
			}
			if (double.IsNegativeInfinity(value))
			{
				throw new NotSupportedException(Strings.SqlGen_TypedNegativeInfinityNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Double), typeof(double).Name));
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000106DC File Offset: 0x0000E8DC
		private static void AssertValidSingle(float value)
		{
			if (float.IsNaN(value))
			{
				throw new NotSupportedException(Strings.SqlGen_TypedNaNNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Single)));
			}
			if (float.IsPositiveInfinity(value))
			{
				throw new NotSupportedException(Strings.SqlGen_TypedPositiveInfinityNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Single), typeof(float).Name));
			}
			if (float.IsNegativeInfinity(value))
			{
				throw new NotSupportedException(Strings.SqlGen_TypedNegativeInfinityNotSupported(Enum.GetName(typeof(PrimitiveTypeKind), PrimitiveTypeKind.Single), typeof(float).Name));
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001077F File Offset: 0x0000E97F
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

		// Token: 0x060003AB RID: 939 RVA: 0x000107BB File Offset: 0x0000E9BB
		public override ISqlFragment Visit(DbConstantExpression e)
		{
			Check.NotNull<DbConstantExpression>(e, "e");
			return this.VisitConstant(e, false);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000107D1 File Offset: 0x0000E9D1
		public override ISqlFragment Visit(DbDerefExpression e)
		{
			Check.NotNull<DbDerefExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000107E4 File Offset: 0x0000E9E4
		public override ISqlFragment Visit(DbDistinctExpression e)
		{
			Check.NotNull<DbDistinctExpression>(e, "e");
			SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(e.Argument);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				TypeUsage elementTypeUsage = e.Argument.ResultType.GetElementTypeUsage();
				Symbol fromSymbol;
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, "distinct", elementTypeUsage, out fromSymbol);
				this.AddFromSymbol(sqlSelectStatement, "distinct", fromSymbol, false);
			}
			sqlSelectStatement.Select.IsDistinct = true;
			return sqlSelectStatement;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00010854 File Offset: 0x0000EA54
		public override ISqlFragment Visit(DbElementExpression e)
		{
			Check.NotNull<DbElementExpression>(e, "e");
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("(");
			sqlBuilder.Append(this.VisitExpressionEnsureSqlStatement(e.Argument));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001089C File Offset: 0x0000EA9C
		public override ISqlFragment Visit(DbExceptExpression e)
		{
			Check.NotNull<DbExceptExpression>(e, "e");
			return this.VisitSetOpExpression(e, "EXCEPT");
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000108B6 File Offset: 0x0000EAB6
		public override ISqlFragment Visit(DbExpression e)
		{
			Check.NotNull<DbExpression>(e, "e");
			throw new InvalidOperationException(string.Empty);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000108D0 File Offset: 0x0000EAD0
		public override ISqlFragment Visit(DbScanExpression e)
		{
			Check.NotNull<DbScanExpression>(e, "e");
			EntitySetBase target = e.Target;
			string targetTSql = SqlGenerator.GetTargetTSql(target);
			if (this._targets != null)
			{
				this._targets.Add(targetTSql);
			}
			if (this.IsParentAJoin)
			{
				SqlBuilder sqlBuilder = new SqlBuilder();
				sqlBuilder.Append(targetTSql);
				return sqlBuilder;
			}
			SqlSelectStatement sqlSelectStatement = new SqlSelectStatement();
			sqlSelectStatement.From.Append(targetTSql);
			return sqlSelectStatement;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00010938 File Offset: 0x0000EB38
		internal static string GetTargetTSql(EntitySetBase entitySetBase)
		{
			string metadataPropertyValue = entitySetBase.GetMetadataPropertyValue("DefiningQuery");
			if (metadataPropertyValue != null)
			{
				return "(" + metadataPropertyValue + ")";
			}
			StringBuilder stringBuilder = new StringBuilder(50);
			string metadataPropertyValue2 = entitySetBase.GetMetadataPropertyValue("Schema");
			if (!string.IsNullOrEmpty(metadataPropertyValue2))
			{
				stringBuilder.Append(SqlGenerator.QuoteIdentifier(metadataPropertyValue2));
				stringBuilder.Append(".");
			}
			else
			{
				stringBuilder.Append(SqlGenerator.QuoteIdentifier(entitySetBase.EntityContainer.Name));
				stringBuilder.Append(".");
			}
			string metadataPropertyValue3 = entitySetBase.GetMetadataPropertyValue("Table");
			stringBuilder.Append(string.IsNullOrEmpty(metadataPropertyValue3) ? SqlGenerator.QuoteIdentifier(entitySetBase.Name) : SqlGenerator.QuoteIdentifier(metadataPropertyValue3));
			return stringBuilder.ToString();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000109F3 File Offset: 0x0000EBF3
		public override ISqlFragment Visit(DbFilterExpression e)
		{
			Check.NotNull<DbFilterExpression>(e, "e");
			return this.VisitFilterExpression(e.Input, e.Predicate, false);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00010A14 File Offset: 0x0000EC14
		public override ISqlFragment Visit(DbFunctionExpression e)
		{
			Check.NotNull<DbFunctionExpression>(e, "e");
			return SqlFunctionCallHandler.GenerateFunctionCallSql(this, e);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00010A29 File Offset: 0x0000EC29
		public override ISqlFragment Visit(DbLambdaExpression expression)
		{
			Check.NotNull<DbLambdaExpression>(expression, "expression");
			throw new NotSupportedException();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00010A3C File Offset: 0x0000EC3C
		public override ISqlFragment Visit(DbEntityRefExpression e)
		{
			Check.NotNull<DbEntityRefExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00010A4F File Offset: 0x0000EC4F
		public override ISqlFragment Visit(DbRefKeyExpression e)
		{
			Check.NotNull<DbRefKeyExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00010A64 File Offset: 0x0000EC64
		public override ISqlFragment Visit(DbGroupByExpression e)
		{
			Check.NotNull<DbGroupByExpression>(e, "e");
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
			RowType rowType = (RowType)((CollectionType)e.ResultType.EdmType).TypeUsage.EdmType;
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
			using (IEnumerator<EdmProperty> enumerator = rowType.Properties.GetEnumerator())
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

		// Token: 0x060003B9 RID: 953 RVA: 0x00010EF8 File Offset: 0x0000F0F8
		public override ISqlFragment Visit(DbIntersectExpression e)
		{
			Check.NotNull<DbIntersectExpression>(e, "e");
			return this.VisitSetOpExpression(e, "INTERSECT");
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00010F12 File Offset: 0x0000F112
		public override ISqlFragment Visit(DbIsEmptyExpression e)
		{
			Check.NotNull<DbIsEmptyExpression>(e, "e");
			return this.VisitIsEmptyExpression(e, false);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00010F28 File Offset: 0x0000F128
		public override ISqlFragment Visit(DbIsNullExpression e)
		{
			Check.NotNull<DbIsNullExpression>(e, "e");
			return this.VisitIsNullExpression(e, false);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00010F3E File Offset: 0x0000F13E
		public override ISqlFragment Visit(DbIsOfExpression e)
		{
			Check.NotNull<DbIsOfExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00010F51 File Offset: 0x0000F151
		public override ISqlFragment Visit(DbCrossJoinExpression e)
		{
			Check.NotNull<DbCrossJoinExpression>(e, "e");
			return this.VisitJoinExpression(e.Inputs, e.ExpressionKind, "CROSS JOIN", null);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00010F78 File Offset: 0x0000F178
		public override ISqlFragment Visit(DbJoinExpression e)
		{
			Check.NotNull<DbJoinExpression>(e, "e");
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

		// Token: 0x060003BF RID: 959 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		public override ISqlFragment Visit(DbLikeExpression e)
		{
			Check.NotNull<DbLikeExpression>(e, "e");
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

		// Token: 0x060003C0 RID: 960 RVA: 0x00011088 File Offset: 0x0000F288
		public override ISqlFragment Visit(DbLimitExpression e)
		{
			Check.NotNull<DbLimitExpression>(e, "e");
			SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(e.Argument, false, false);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				TypeUsage elementTypeUsage = e.Argument.ResultType.GetElementTypeUsage();
				Symbol fromSymbol;
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, "top", elementTypeUsage, out fromSymbol);
				this.AddFromSymbol(sqlSelectStatement, "top", fromSymbol, false);
			}
			ISqlFragment topCount = this.HandleCountExpression(e.Limit);
			sqlSelectStatement.Select.Top = new TopClause(topCount, e.WithTies);
			return sqlSelectStatement;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00011112 File Offset: 0x0000F312
		public override ISqlFragment Visit(DbNewInstanceExpression e)
		{
			Check.NotNull<DbNewInstanceExpression>(e, "e");
			if (BuiltInTypeKind.CollectionType == e.ResultType.EdmType.BuiltInTypeKind)
			{
				return this.VisitCollectionConstructor(e);
			}
			throw new NotSupportedException();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00011140 File Offset: 0x0000F340
		public override ISqlFragment Visit(DbNotExpression e)
		{
			Check.NotNull<DbNotExpression>(e, "e");
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
				if (dbComparisonExpression.Left.ResultType.IsPrimitiveType(PrimitiveTypeKind.String))
				{
					this._forceNonUnicode = this.CheckIfForceNonUnicodeRequired(dbComparisonExpression);
				}
				SqlBuilder result = this.VisitComparisonExpression(" <> ", dbComparisonExpression.Left, dbComparisonExpression.Right);
				this._forceNonUnicode = forceNonUnicode;
				return result;
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(" NOT (");
			sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00011240 File Offset: 0x0000F440
		public override ISqlFragment Visit(DbNullExpression e)
		{
			Check.NotNull<DbNullExpression>(e, "e");
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

		// Token: 0x060003C4 RID: 964 RVA: 0x000112C0 File Offset: 0x0000F4C0
		public override ISqlFragment Visit(DbOfTypeExpression e)
		{
			Check.NotNull<DbOfTypeExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000112D4 File Offset: 0x0000F4D4
		public override ISqlFragment Visit(DbOrExpression e)
		{
			Check.NotNull<DbOrExpression>(e, "e");
			ISqlFragment result = null;
			if (this.TryTranslateIntoIn(e, out result))
			{
				return result;
			}
			return this.VisitBinaryExpression(" OR ", e.ExpressionKind, e.Left, e.Right);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001131C File Offset: 0x0000F51C
		public override ISqlFragment Visit(DbInExpression e)
		{
			Check.NotNull<DbInExpression>(e, "e");
			if (e.List.Count == 0)
			{
				return this.Visit(DbExpressionBuilder.False);
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (e.Item.ResultType.IsPrimitiveType(PrimitiveTypeKind.String))
			{
				this._forceNonUnicode = this.CheckIfForceNonUnicodeRequired(e);
			}
			sqlBuilder.Append(e.Item.Accept<ISqlFragment>(this));
			sqlBuilder.Append(" IN (");
			bool flag = true;
			foreach (DbExpression dbExpression in e.List)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					sqlBuilder.Append(", ");
				}
				sqlBuilder.Append(dbExpression.Accept<ISqlFragment>(this));
			}
			sqlBuilder.Append(")");
			this._forceNonUnicode = false;
			return sqlBuilder;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00011400 File Offset: 0x0000F600
		internal static IDictionary<DbExpression, IList<DbExpression>> HasBuiltMapForIn(DbOrExpression expression)
		{
			Dictionary<DbExpression, IList<DbExpression>> dictionary = new Dictionary<DbExpression, IList<DbExpression>>(new SqlGenerator.KeyFieldExpressionComparer());
			if (!SqlGenerator.HasBuiltMapForIn(expression, dictionary))
			{
				return null;
			}
			return dictionary;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00011440 File Offset: 0x0000F640
		private bool TryTranslateIntoIn(DbOrExpression e, out ISqlFragment sqlFragment)
		{
			IDictionary<DbExpression, IList<DbExpression>> dictionary = SqlGenerator.HasBuiltMapForIn(e);
			if (dictionary == null)
			{
				sqlFragment = null;
				return false;
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			bool flag = true;
			foreach (DbExpression dbExpression in dictionary.Keys)
			{
				IList<DbExpression> source = dictionary[dbExpression];
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
				if (dbExpression.ResultType.IsPrimitiveType(PrimitiveTypeKind.String))
				{
					flag2 = SqlGenerator.MatchSourcePatternForForcingNonUnicode(dbExpression);
					forceNonUnicodeOnKey = (!flag2 && this.MatchTargetPatternForForcingNonUnicode(dbExpression) && enumerable.All(new Func<DbExpression, bool>(SqlGenerator.MatchSourcePatternForForcingNonUnicode)));
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

		// Token: 0x060003C9 RID: 969 RVA: 0x000116A8 File Offset: 0x0000F8A8
		private void HandleInValue(SqlBuilder sqlBuilder, DbExpression value, bool isSameEdmType, bool forceNonUnicodeOnQualifyingValues)
		{
			this.ForcingNonUnicode(delegate
			{
				this.ParenthesizeExpressionWithoutRedundantConstantCasts(value, sqlBuilder, isSameEdmType);
			}, forceNonUnicodeOnQualifyingValues && this.MatchTargetPatternForForcingNonUnicode(value));
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00011720 File Offset: 0x0000F920
		private void HandleInKey(SqlBuilder sqlBuilder, DbExpression key, bool forceNonUnicodeOnKey)
		{
			this.ForcingNonUnicode(delegate
			{
				this.ParenthesizeExpressionIfNeeded(key, sqlBuilder);
			}, forceNonUnicodeOnKey);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001175C File Offset: 0x0000F95C
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

		// Token: 0x060003CC RID: 972 RVA: 0x00011790 File Offset: 0x0000F990
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

		// Token: 0x060003CD RID: 973 RVA: 0x000117C4 File Offset: 0x0000F9C4
		internal static bool IsKeyForIn(DbExpression e)
		{
			return e.ExpressionKind == DbExpressionKind.Property || e.ExpressionKind == DbExpressionKind.VariableReference || e.ExpressionKind == DbExpressionKind.ParameterReference;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000117E8 File Offset: 0x0000F9E8
		internal static bool TryAddExpressionForIn(DbBinaryExpression e, IDictionary<DbExpression, IList<DbExpression>> values)
		{
			if (SqlGenerator.IsKeyForIn(e.Left))
			{
				values.Add(e.Left, e.Right);
				return true;
			}
			if (SqlGenerator.IsKeyForIn(e.Right))
			{
				values.Add(e.Right, e.Left);
				return true;
			}
			return false;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00011838 File Offset: 0x0000FA38
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		internal static bool HasBuiltMapForIn(DbExpression e, IDictionary<DbExpression, IList<DbExpression>> values)
		{
			DbExpressionKind expressionKind = e.ExpressionKind;
			if (expressionKind == DbExpressionKind.Equals)
			{
				return SqlGenerator.TryAddExpressionForIn((DbBinaryExpression)e, values);
			}
			if (expressionKind != DbExpressionKind.IsNull)
			{
				if (expressionKind != DbExpressionKind.Or)
				{
					return false;
				}
				DbBinaryExpression dbBinaryExpression = (DbBinaryExpression)e;
				return SqlGenerator.HasBuiltMapForIn(dbBinaryExpression.Left, values) && SqlGenerator.HasBuiltMapForIn(dbBinaryExpression.Right, values);
			}
			else
			{
				DbExpression argument = ((DbIsNullExpression)e).Argument;
				if (SqlGenerator.IsKeyForIn(argument))
				{
					values.Add(argument, e);
					return true;
				}
				return false;
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000118B0 File Offset: 0x0000FAB0
		public override ISqlFragment Visit(DbParameterReferenceExpression e)
		{
			Check.NotNull<DbParameterReferenceExpression>(e, "e");
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

		// Token: 0x060003D1 RID: 977 RVA: 0x00011930 File Offset: 0x0000FB30
		public override ISqlFragment Visit(DbProjectExpression e)
		{
			Check.NotNull<DbProjectExpression>(e, "e");
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

		// Token: 0x060003D2 RID: 978 RVA: 0x00011A50 File Offset: 0x0000FC50
		public override ISqlFragment Visit(DbPropertyExpression e)
		{
			Check.NotNull<DbPropertyExpression>(e, "e");
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

		// Token: 0x060003D3 RID: 979 RVA: 0x00011C14 File Offset: 0x0000FE14
		public override ISqlFragment Visit(DbQuantifierExpression e)
		{
			Check.NotNull<DbQuantifierExpression>(e, "e");
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

		// Token: 0x060003D4 RID: 980 RVA: 0x00011C9A File Offset: 0x0000FE9A
		public override ISqlFragment Visit(DbRefExpression e)
		{
			Check.NotNull<DbRefExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00011CAD File Offset: 0x0000FEAD
		public override ISqlFragment Visit(DbRelationshipNavigationExpression e)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00011CDC File Offset: 0x0000FEDC
		public override ISqlFragment Visit(DbSkipExpression e)
		{
			Check.NotNull<DbSkipExpression>(e, "e");
			Symbol fromSymbol;
			SqlSelectStatement sqlSelectStatement = this.VisitInputExpression(e.Input.Expression, e.Input.VariableName, e.Input.VariableType, out fromSymbol);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName, e.Input.VariableType, out fromSymbol);
			}
			this.selectStatementStack.Push(sqlSelectStatement);
			this.symbolTable.EnterScope();
			this.AddFromSymbol(sqlSelectStatement, e.Input.VariableName, fromSymbol);
			if (this.SqlVersion >= SqlVersion.Sql11)
			{
				sqlSelectStatement.Select.Skip = new SkipClause(this.HandleCountExpression(e.Count));
				this.AddSortKeys(sqlSelectStatement.OrderBy, e.SortOrder);
				this.symbolTable.ExitScope();
				this.selectStatementStack.Pop();
				return sqlSelectStatement;
			}
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

		// Token: 0x060003D7 RID: 983 RVA: 0x00011FF0 File Offset: 0x000101F0
		public override ISqlFragment Visit(DbSortExpression e)
		{
			Check.NotNull<DbSortExpression>(e, "e");
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

		// Token: 0x060003D8 RID: 984 RVA: 0x000120B5 File Offset: 0x000102B5
		public override ISqlFragment Visit(DbTreatExpression e)
		{
			Check.NotNull<DbTreatExpression>(e, "e");
			throw new NotSupportedException();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000120C8 File Offset: 0x000102C8
		public override ISqlFragment Visit(DbUnionAllExpression e)
		{
			Check.NotNull<DbUnionAllExpression>(e, "e");
			return this.VisitSetOpExpression(e, "UNION ALL");
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000120E4 File Offset: 0x000102E4
		public override ISqlFragment Visit(DbVariableReferenceExpression e)
		{
			Check.NotNull<DbVariableReferenceExpression>(e, "e");
			if (this.isVarRefSingle)
			{
				throw new NotSupportedException();
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

		// Token: 0x060003DB RID: 987 RVA: 0x00012158 File Offset: 0x00010358
		private static SqlBuilder VisitAggregate(DbAggregate aggregate, object aggregateArgument)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			DbFunctionAggregate dbFunctionAggregate = aggregate as DbFunctionAggregate;
			if (dbFunctionAggregate == null)
			{
				throw new NotSupportedException();
			}
			if (dbFunctionAggregate.Function.IsCanonicalFunction() && string.Equals(dbFunctionAggregate.Function.Name, "BigCount", StringComparison.Ordinal))
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

		// Token: 0x060003DC RID: 988 RVA: 0x000121EF File Offset: 0x000103EF
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

		// Token: 0x060003DD RID: 989 RVA: 0x0001222C File Offset: 0x0001042C
		private SqlBuilder VisitBinaryExpression(string op, DbExpressionKind expressionKind, DbExpression left, DbExpression right)
		{
			SqlGenerator.RemoveUnnecessaryCasts(ref left, ref right);
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

		// Token: 0x060003DE RID: 990 RVA: 0x000122A0 File Offset: 0x000104A0
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

		// Token: 0x060003DF RID: 991 RVA: 0x00012328 File Offset: 0x00010528
		private static void ExtractAssociativeArguments(DbExpressionKind expressionKind, List<DbExpression> argumentList, DbExpression expression)
		{
			IEnumerable<DbExpression> leafNodes = expression.GetLeafNodes(expressionKind, delegate(DbExpression exp)
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

		// Token: 0x060003E0 RID: 992 RVA: 0x00012364 File Offset: 0x00010564
		private SqlBuilder VisitComparisonExpression(string op, DbExpression left, DbExpression right)
		{
			SqlGenerator.RemoveUnnecessaryCasts(ref left, ref right);
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

		// Token: 0x060003E1 RID: 993 RVA: 0x000123F0 File Offset: 0x000105F0
		private static void RemoveUnnecessaryCasts(ref DbExpression left, ref DbExpression right)
		{
			if (left.ResultType.EdmType != right.ResultType.EdmType)
			{
				return;
			}
			DbCastExpression dbCastExpression = left as DbCastExpression;
			if (dbCastExpression != null && dbCastExpression.Argument.ResultType.EdmType == left.ResultType.EdmType)
			{
				left = dbCastExpression.Argument;
			}
			DbCastExpression dbCastExpression2 = right as DbCastExpression;
			if (dbCastExpression2 != null && dbCastExpression2.Argument.ResultType.EdmType == left.ResultType.EdmType)
			{
				right = dbCastExpression2.Argument;
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001247C File Offset: 0x0001067C
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

		// Token: 0x060003E3 RID: 995 RVA: 0x00012520 File Offset: 0x00010720
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

		// Token: 0x060003E4 RID: 996 RVA: 0x00012570 File Offset: 0x00010770
		private ISqlFragment VisitCollectionConstructor(DbNewInstanceExpression e)
		{
			if (e.Arguments.Count == 1 && e.Arguments[0].ExpressionKind == DbExpressionKind.Element)
			{
				DbElementExpression dbElementExpression = e.Arguments[0] as DbElementExpression;
				SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(dbElementExpression.Argument);
				if (!SqlGenerator.IsCompatible(sqlSelectStatement, DbExpressionKind.Element))
				{
					TypeUsage elementTypeUsage = dbElementExpression.Argument.ResultType.GetElementTypeUsage();
					Symbol fromSymbol;
					sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, "element", elementTypeUsage, out fromSymbol);
					this.AddFromSymbol(sqlSelectStatement, "element", fromSymbol, false);
				}
				sqlSelectStatement.Select.Top = new TopClause(1, false);
				return sqlSelectStatement;
			}
			CollectionType collectionType = (CollectionType)e.ResultType.EdmType;
			bool flag = BuiltInTypeKind.PrimitiveType == collectionType.TypeUsage.EdmType.BuiltInTypeKind;
			SqlBuilder sqlBuilder = new SqlBuilder();
			string s = "";
			if (e.Arguments.Count == 0)
			{
				sqlBuilder.Append(" SELECT CAST(null as ");
				sqlBuilder.Append(this.GetSqlPrimitiveType(collectionType.TypeUsage));
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

		// Token: 0x060003E5 RID: 997 RVA: 0x000126FC File Offset: 0x000108FC
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

		// Token: 0x060003E6 RID: 998 RVA: 0x0001275C File Offset: 0x0001095C
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

		// Token: 0x060003E7 RID: 999 RVA: 0x000128F0 File Offset: 0x00010AF0
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

		// Token: 0x060003E8 RID: 1000 RVA: 0x00012B18 File Offset: 0x00010D18
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
					if (BuiltInTypeKind.RowType == dbExpression.ResultType.EdmType.BuiltInTypeKind)
					{
						throw new NotSupportedException();
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
			throw new NotSupportedException();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00012C50 File Offset: 0x00010E50
		private ISqlFragment VisitSetOpExpression(DbBinaryExpression setOpExpression, string separator)
		{
			List<SqlSelectStatement> list = new List<SqlSelectStatement>();
			this.VisitAndGatherSetOpLeafExpressions(setOpExpression.ExpressionKind, setOpExpression.Left, list);
			this.VisitAndGatherSetOpLeafExpressions(setOpExpression.ExpressionKind, setOpExpression.Right, list);
			SqlBuilder sqlBuilder = new SqlBuilder();
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					sqlBuilder.AppendLine();
					sqlBuilder.Append(separator);
					sqlBuilder.AppendLine();
				}
				sqlBuilder.Append(list[i]);
			}
			if (!list[0].OutputColumnsRenamed)
			{
				return sqlBuilder;
			}
			SqlSelectStatement sqlSelectStatement = new SqlSelectStatement();
			sqlSelectStatement.From.Append("( ");
			sqlSelectStatement.From.Append(sqlBuilder);
			sqlSelectStatement.From.AppendLine();
			sqlSelectStatement.From.Append(") ");
			Symbol fromSymbol = new Symbol("X", setOpExpression.Left.ResultType.GetElementTypeUsage(), list[0].OutputColumns, true);
			this.AddFromSymbol(sqlSelectStatement, null, fromSymbol, false);
			return sqlSelectStatement;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00012D48 File Offset: 0x00010F48
		private void VisitAndGatherSetOpLeafExpressions(DbExpressionKind kind, DbExpression expression, List<SqlSelectStatement> leafSelectStatements)
		{
			if (this.SqlVersion > SqlVersion.Sql8 && (kind == DbExpressionKind.UnionAll || kind == DbExpressionKind.Intersect) && expression.ExpressionKind == kind)
			{
				DbBinaryExpression dbBinaryExpression = (DbBinaryExpression)expression;
				this.VisitAndGatherSetOpLeafExpressions(kind, dbBinaryExpression.Left, leafSelectStatements);
				this.VisitAndGatherSetOpLeafExpressions(kind, dbBinaryExpression.Right, leafSelectStatements);
				return;
			}
			leafSelectStatements.Add(this.VisitExpressionEnsureSqlStatement(expression, true, true));
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00012DA8 File Offset: 0x00010FA8
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
							if (symbol2.Type != null && BuiltInTypeKind.PrimitiveType != symbol2.Type.EdmType.BuiltInTypeKind)
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
			if (symbol.Type == null || BuiltInTypeKind.PrimitiveType == symbol.Type.EdmType.BuiltInTypeKind)
			{
				this.AddColumn(selectStatement, symbol, columnList, columnDictionary, "X");
				return;
			}
			foreach (EdmProperty edmProperty in symbol.Type.GetProperties())
			{
				this.AddColumn(selectStatement, symbol, columnList, columnDictionary, edmProperty.Name);
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00012F84 File Offset: 0x00011184
		private OptionalColumn CreateOptionalColumn(Symbol inputColumnSymbol, Symbol column)
		{
			if (!this.optionalColumnUsageManager.ContainsKey(column))
			{
				this.optionalColumnUsageManager.Add(inputColumnSymbol, column);
			}
			return new OptionalColumn(this.optionalColumnUsageManager, column);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00012FB0 File Offset: 0x000111B0
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

		// Token: 0x060003EE RID: 1006 RVA: 0x000130C8 File Offset: 0x000112C8
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

		// Token: 0x060003EF RID: 1007 RVA: 0x00013130 File Offset: 0x00011330
		private void AddFromSymbol(SqlSelectStatement selectStatement, string inputVarName, Symbol fromSymbol)
		{
			this.AddFromSymbol(selectStatement, inputVarName, fromSymbol, true);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001313C File Offset: 0x0001133C
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

		// Token: 0x060003F1 RID: 1009 RVA: 0x000131B0 File Offset: 0x000113B0
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

		// Token: 0x060003F2 RID: 1010 RVA: 0x00013254 File Offset: 0x00011454
		private SqlSelectStatement CreateNewSelectStatement(SqlSelectStatement oldStatement, string inputVarName, TypeUsage inputVarType, out Symbol fromSymbol)
		{
			return this.CreateNewSelectStatement(oldStatement, inputVarName, inputVarType, true, out fromSymbol);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00013264 File Offset: 0x00011464
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

		// Token: 0x060003F4 RID: 1012 RVA: 0x00013328 File Offset: 0x00011528
		private static string EscapeSingleQuote(string s, bool isUnicode)
		{
			return (isUnicode ? "N'" : "'") + s.Replace("'", "''") + "'";
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00013354 File Offset: 0x00011554
		private string GetSqlPrimitiveType(TypeUsage type)
		{
			TypeUsage storeType = this._storeItemCollection.ProviderManifest.GetStoreType(type);
			return SqlGenerator.GenerateSqlForStoreType(this._sqlVersion, storeType);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00013380 File Offset: 0x00011580
		internal static string GenerateSqlForStoreType(SqlVersion sqlVersion, TypeUsage storeTypeUsage)
		{
			string text = storeTypeUsage.EdmType.Name;
			int num = 0;
			byte b = 0;
			byte b2 = 0;
			PrimitiveTypeKind primitiveTypeKind = ((PrimitiveType)storeTypeUsage.EdmType).PrimitiveTypeKind;
			PrimitiveTypeKind primitiveTypeKind2 = primitiveTypeKind;
			switch (primitiveTypeKind2)
			{
			case PrimitiveTypeKind.Binary:
				if (!storeTypeUsage.MustFacetBeConstant("MaxLength"))
				{
					storeTypeUsage.TryGetMaxLength(out num);
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
				if (!storeTypeUsage.MustFacetBeConstant("Precision"))
				{
					storeTypeUsage.TryGetPrecision(out b);
					storeTypeUsage.TryGetScale(out b2);
					text = string.Concat(new object[]
					{
						text,
						"(",
						b,
						",",
						b2,
						")"
					});
				}
				break;
			default:
				switch (primitiveTypeKind2)
				{
				case PrimitiveTypeKind.String:
					if (!storeTypeUsage.MustFacetBeConstant("MaxLength"))
					{
						storeTypeUsage.TryGetMaxLength(out num);
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

		// Token: 0x060003F7 RID: 1015 RVA: 0x00013500 File Offset: 0x00011700
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

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001353F File Offset: 0x0001173F
		private static bool IsApplyExpression(DbExpression e)
		{
			return DbExpressionKind.CrossApply == e.ExpressionKind || DbExpressionKind.OuterApply == e.ExpressionKind;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00013556 File Offset: 0x00011756
		private static bool IsJoinExpression(DbExpression e)
		{
			return DbExpressionKind.CrossJoin == e.ExpressionKind || DbExpressionKind.FullOuterJoin == e.ExpressionKind || DbExpressionKind.InnerJoin == e.ExpressionKind || DbExpressionKind.LeftOuterJoin == e.ExpressionKind;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00013584 File Offset: 0x00011784
		private static bool IsComplexExpression(DbExpression e)
		{
			DbExpressionKind expressionKind = e.ExpressionKind;
			switch (expressionKind)
			{
			case DbExpressionKind.Cast:
			case DbExpressionKind.Constant:
				break;
			default:
				if (expressionKind != DbExpressionKind.ParameterReference && expressionKind != DbExpressionKind.Property)
				{
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000135B8 File Offset: 0x000117B8
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private static bool IsCompatible(SqlSelectStatement result, DbExpressionKind expressionKind)
		{
			if (expressionKind <= DbExpressionKind.GroupBy)
			{
				switch (expressionKind)
				{
				case DbExpressionKind.Distinct:
					return result.Select.Top == null && result.Select.Skip == null && result.OrderBy.IsEmpty;
				case DbExpressionKind.Divide:
					goto IL_1CA;
				case DbExpressionKind.Element:
					break;
				default:
					if (expressionKind == DbExpressionKind.Filter)
					{
						return result.Select.IsEmpty && result.Where.IsEmpty && result.GroupBy.IsEmpty && result.Select.Top == null && result.Select.Skip == null;
					}
					if (expressionKind != DbExpressionKind.GroupBy)
					{
						goto IL_1CA;
					}
					return result.Select.IsEmpty && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty && result.Select.Top == null && result.Select.Skip == null && !result.Select.IsDistinct;
				}
			}
			else if (expressionKind != DbExpressionKind.Limit)
			{
				if (expressionKind == DbExpressionKind.Project)
				{
					return result.Select.IsEmpty && result.GroupBy.IsEmpty && !result.Select.IsDistinct;
				}
				switch (expressionKind)
				{
				case DbExpressionKind.Skip:
					return result.Select.IsEmpty && result.Select.Skip == null && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty && !result.Select.IsDistinct;
				case DbExpressionKind.Sort:
					return result.Select.IsEmpty && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty && !result.Select.IsDistinct;
				default:
					goto IL_1CA;
				}
			}
			return result.Select.Top == null;
			IL_1CA:
			throw new InvalidOperationException(string.Empty);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00013799 File Offset: 0x00011999
		internal static string QuoteIdentifier(string name)
		{
			return "[" + name.Replace("]", "]]") + "]";
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000137BA File Offset: 0x000119BA
		private SqlSelectStatement VisitExpressionEnsureSqlStatement(DbExpression e)
		{
			return this.VisitExpressionEnsureSqlStatement(e, true, false);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000137C8 File Offset: 0x000119C8
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
			goto IL_D0;
			IL_3A:
			string inputVarName = "c";
			this.symbolTable.EnterScope();
			DbExpressionKind expressionKind2 = e.ExpressionKind;
			if (expressionKind2 <= DbExpressionKind.InnerJoin)
			{
				switch (expressionKind2)
				{
				case DbExpressionKind.CrossApply:
				case DbExpressionKind.CrossJoin:
					break;
				default:
					if (expressionKind2 != DbExpressionKind.FullOuterJoin && expressionKind2 != DbExpressionKind.InnerJoin)
					{
						goto IL_9A;
					}
					break;
				}
			}
			else if (expressionKind2 != DbExpressionKind.LeftOuterJoin && expressionKind2 != DbExpressionKind.OuterApply && expressionKind2 != DbExpressionKind.Scan)
			{
				goto IL_9A;
			}
			TypeUsage inputVarType = e.ResultType.GetElementTypeUsage();
			goto IL_B0;
			IL_9A:
			inputVarType = ((CollectionType)e.ResultType.EdmType).TypeUsage;
			IL_B0:
			Symbol fromSymbol;
			sqlSelectStatement = this.VisitInputExpression(e, inputVarName, inputVarType, out fromSymbol);
			this.AddFromSymbol(sqlSelectStatement, inputVarName, fromSymbol);
			this.symbolTable.ExitScope();
			IL_D0:
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

		// Token: 0x060003FF RID: 1023 RVA: 0x0001390C File Offset: 0x00011B0C
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

		// Token: 0x06000400 RID: 1024 RVA: 0x000139CC File Offset: 0x00011BCC
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

		// Token: 0x06000401 RID: 1025 RVA: 0x00013A1C File Offset: 0x00011C1C
		private static string ByteArrayToBinaryString(byte[] binaryArray)
		{
			StringBuilder stringBuilder = new StringBuilder(binaryArray.Length * 2);
			for (int i = 0; i < binaryArray.Length; i++)
			{
				stringBuilder.Append(SqlGenerator._hexDigits[(binaryArray[i] & 240) >> 4]).Append(SqlGenerator._hexDigits[(int)(binaryArray[i] & 15)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00013A74 File Offset: 0x00011C74
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

		// Token: 0x06000403 RID: 1027 RVA: 0x00013AD0 File Offset: 0x00011CD0
		private static bool GroupByAggregateNeedsInnerQuery(DbExpression expression, string inputVarRefName)
		{
			return SqlGenerator.GroupByExpressionNeedsInnerQuery(expression, inputVarRefName, true);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00013ADC File Offset: 0x00011CDC
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

		// Token: 0x06000405 RID: 1029 RVA: 0x00013B30 File Offset: 0x00011D30
		private static bool GroupByKeyNeedsInnerQuery(DbExpression expression, string inputVarRefName)
		{
			return SqlGenerator.GroupByExpressionNeedsInnerQuery(expression, inputVarRefName, false);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00013B3C File Offset: 0x00011D3C
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

		// Token: 0x06000407 RID: 1031 RVA: 0x00013BB6 File Offset: 0x00011DB6
		private void AssertKatmaiOrNewer(PrimitiveTypeKind primitiveTypeKind)
		{
			SqlGenerator.AssertKatmaiOrNewer(this._sqlVersion, primitiveTypeKind);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00013BC4 File Offset: 0x00011DC4
		private static void AssertKatmaiOrNewer(SqlVersion sqlVersion, PrimitiveTypeKind primitiveTypeKind)
		{
			if (SqlVersionUtils.IsPreKatmai(sqlVersion))
			{
				throw new NotSupportedException(Strings.SqlGen_PrimitiveTypeNotSupportedPriorSql10(primitiveTypeKind));
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00013BDF File Offset: 0x00011DDF
		internal void AssertKatmaiOrNewer(DbFunctionExpression e)
		{
			if (this.IsPreKatmai)
			{
				throw new NotSupportedException(Strings.SqlGen_CanonicalFunctionNotSupportedPriorSql10(e.Function.Name));
			}
		}

		// Token: 0x040000CC RID: 204
		private const byte DefaultDecimalPrecision = 18;

		// Token: 0x040000CD RID: 205
		private Stack<SqlSelectStatement> selectStatementStack;

		// Token: 0x040000CE RID: 206
		private Stack<bool> isParentAJoinStack;

		// Token: 0x040000CF RID: 207
		private Dictionary<string, int> allExtentNames;

		// Token: 0x040000D0 RID: 208
		private Dictionary<string, int> allColumnNames;

		// Token: 0x040000D1 RID: 209
		private readonly SymbolTable symbolTable = new SymbolTable();

		// Token: 0x040000D2 RID: 210
		private bool isVarRefSingle;

		// Token: 0x040000D3 RID: 211
		private readonly SymbolUsageManager optionalColumnUsageManager = new SymbolUsageManager();

		// Token: 0x040000D4 RID: 212
		private readonly Dictionary<string, bool> _candidateParametersToForceNonUnicode = new Dictionary<string, bool>();

		// Token: 0x040000D5 RID: 213
		private bool _forceNonUnicode;

		// Token: 0x040000D6 RID: 214
		private bool _ignoreForceNonUnicodeFlag;

		// Token: 0x040000D7 RID: 215
		private static readonly char[] _hexDigits = new char[]
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

		// Token: 0x040000D8 RID: 216
		private List<string> _targets;

		// Token: 0x040000D9 RID: 217
		private static readonly ISet<string> _canonicalAndStoreStringFunctionsOneArg = new HashSet<string>(StringComparer.Ordinal)
		{
			"Edm.Trim",
			"Edm.RTrim",
			"Edm.LTrim",
			"Edm.Left",
			"Edm.Right",
			"Edm.Substring",
			"Edm.ToLower",
			"Edm.ToUpper",
			"Edm.Reverse",
			"SqlServer.RTRIM",
			"SqlServer.LTRIM",
			"SqlServer.LEFT",
			"SqlServer.RIGHT",
			"SqlServer.SUBSTRING",
			"SqlServer.LOWER",
			"SqlServer.UPPER",
			"SqlServer.REVERSE"
		};

		// Token: 0x040000DA RID: 218
		private readonly SqlVersion _sqlVersion;

		// Token: 0x040000DB RID: 219
		private TypeUsage _integerType;

		// Token: 0x040000DC RID: 220
		private StoreItemCollection _storeItemCollection;

		// Token: 0x0200003A RID: 58
		internal class KeyFieldExpressionComparer : IEqualityComparer<DbExpression>
		{
			// Token: 0x06000411 RID: 1041 RVA: 0x00013D24 File Offset: 0x00011F24
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
						return ((DbParameterReferenceExpression)x).ParameterName == ((DbParameterReferenceExpression)y).ParameterName;
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
						return object.ReferenceEquals(x, y);
					}
				}
				return false;
			}

			// Token: 0x06000412 RID: 1042 RVA: 0x00013DF8 File Offset: 0x00011FF8
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
		}
	}
}
