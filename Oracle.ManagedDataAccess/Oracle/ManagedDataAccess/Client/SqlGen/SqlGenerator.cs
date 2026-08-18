using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Text;
using OracleInternal.EntityFramework;

namespace Oracle.ManagedDataAccess.Client.SqlGen
{
	// Token: 0x020000F2 RID: 242
	internal sealed class SqlGenerator : DbExpressionVisitor<ISqlFragment>
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0006E438 File Offset: 0x0006C638
		private SqlSelectStatement CurrentSelectStatement
		{
			get
			{
				return this.selectStatementStack.Peek();
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0006E448 File Offset: 0x0006C648
		private bool IsParentAJoin
		{
			get
			{
				return this.isParentAJoinStack.Count != 0 && this.isParentAJoinStack.Peek();
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0006E464 File Offset: 0x0006C664
		internal Dictionary<string, int> AllExtentNames
		{
			get
			{
				return this.allExtentNames;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0006E46C File Offset: 0x0006C66C
		internal Dictionary<string, int> AllColumnNames
		{
			get
			{
				return this.allColumnNames;
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0006E474 File Offset: 0x0006C674
		private static Dictionary<string, SqlGenerator.FunctionHandler> InitializeCanonicalFunctionHandlers()
		{
			return new Dictionary<string, SqlGenerator.FunctionHandler>(53, StringComparer.Ordinal)
			{
				{
					"Left",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionLeft)
				},
				{
					"Right",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionRight)
				},
				{
					"IndexOf",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionIndexOf2)
				},
				{
					"Substring",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionSubstring)
				},
				{
					"Length",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionLength)
				},
				{
					"NewGuid",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionNewGuid)
				},
				{
					"Round",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionRound)
				},
				{
					"ToLower",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionToLower)
				},
				{
					"ToUpper",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionToUpper)
				},
				{
					"Ceiling",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionCeiling)
				},
				{
					"Trim",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionTrim)
				},
				{
					"Year",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepart)
				},
				{
					"Month",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepart)
				},
				{
					"Day",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepart)
				},
				{
					"Hour",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepart)
				},
				{
					"Minute",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepart)
				},
				{
					"Second",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepart)
				},
				{
					"Millisecond",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepart)
				},
				{
					"CurrentDateTime",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionCurrentDateTime)
				},
				{
					"CurrentUtcDateTime",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionCurrentDateTime)
				},
				{
					"CurrentDateTimeOffset",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionCurrentDateTime)
				},
				{
					"GetTotalOffsetMinutes",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionGetTotalOffsetMinutes)
				},
				{
					"Concat",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleConcatFunction)
				},
				{
					"BitwiseAnd",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionBitwise)
				},
				{
					"BitwiseNot",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionBitwise)
				},
				{
					"BitwiseOr",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionBitwise)
				},
				{
					"BitwiseXor",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionBitwise)
				},
				{
					"Truncate",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionTruncate)
				},
				{
					"TruncateTime",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionTruncate)
				},
				{
					"DayOfYear",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepart)
				},
				{
					"AddNanoseconds",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartAdd)
				},
				{
					"AddMicroseconds",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartAdd)
				},
				{
					"AddMilliseconds",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartAdd)
				},
				{
					"AddSeconds",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartAdd)
				},
				{
					"AddMinutes",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartAdd)
				},
				{
					"AddHours",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartAdd)
				},
				{
					"AddDays",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartAdd)
				},
				{
					"AddMonths",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartAdd)
				},
				{
					"AddYears",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartAdd)
				},
				{
					"CreateDateTime",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionCurrentDateTime)
				},
				{
					"CreateDateTimeOffset",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionCurrentDateTime)
				},
				{
					"DiffNanoseconds",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartDiff)
				},
				{
					"DiffMilliseconds",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartDiff)
				},
				{
					"DiffMicroseconds",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartDiff)
				},
				{
					"DiffSeconds",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartDiff)
				},
				{
					"DiffMinutes",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartDiff)
				},
				{
					"DiffHours",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartDiff)
				},
				{
					"DiffDays",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartDiff)
				},
				{
					"DiffMonths",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartDiff)
				},
				{
					"DiffYears",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionDatepartDiff)
				},
				{
					"Contains",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionIndexOf2)
				},
				{
					"EndsWith",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionIndexOf2)
				},
				{
					"StartsWith",
					new SqlGenerator.FunctionHandler(SqlGenerator.HandleCanonicalFunctionIndexOf2)
				}
			};
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0006E954 File Offset: 0x0006CB54
		internal bool IsPre10g
		{
			get
			{
				return this._sqlVersion < EFOracleVersion.Oracle10gR1;
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0006E960 File Offset: 0x0006CB60
		private SqlGenerator(EFOracleProviderManifest providerManifest, EFOracleVersion sqlVersion)
		{
			this._providerManifest = providerManifest;
			this._sqlVersion = sqlVersion;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0006E98C File Offset: 0x0006CB8C
		internal static string GenerateSql(DbCommandTree tree, EFOracleProviderManifest providerManifest, EFOracleVersion sqlVersion, out List<OracleParameter> parameters, out CommandType commandType, out HashSet<string> ListOfParamsToMakeUnicodeFalse)
		{
			commandType = CommandType.Text;
			parameters = null;
			ListOfParamsToMakeUnicodeFalse = null;
			if (tree is DbQueryCommandTree)
			{
				SqlGenerator sqlGenerator = new SqlGenerator(providerManifest, sqlVersion);
				return sqlGenerator.GenerateSql((DbQueryCommandTree)tree, out ListOfParamsToMakeUnicodeFalse);
			}
			if (tree is DbInsertCommandTree)
			{
				return DmlSqlGenerator.GenerateInsertSql((DbInsertCommandTree)tree, providerManifest, sqlVersion, out parameters);
			}
			if (tree is DbDeleteCommandTree)
			{
				return DmlSqlGenerator.GenerateDeleteSql((DbDeleteCommandTree)tree, providerManifest, sqlVersion, out parameters);
			}
			if (tree is DbUpdateCommandTree)
			{
				return DmlSqlGenerator.GenerateUpdateSql((DbUpdateCommandTree)tree, providerManifest, sqlVersion, out parameters);
			}
			if (tree is DbFunctionCommandTree)
			{
				SqlGenerator sqlGenerator = new SqlGenerator(providerManifest, sqlVersion);
				return SqlGenerator.GenerateFunctionSql((DbFunctionCommandTree)tree, out commandType, out parameters);
			}
			parameters = null;
			return null;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0006EA30 File Offset: 0x0006CC30
		private static string GenerateFunctionSql(DbFunctionCommandTree tree, out CommandType commandType, out List<OracleParameter> parameters)
		{
			EdmFunction edmFunction = tree.EdmFunction;
			parameters = null;
			string metadataProperty = MetadataHelpers.GetMetadataProperty<string>(edmFunction, "CommandTextAttribute");
			string metadataProperty2 = MetadataHelpers.GetMetadataProperty<string>(edmFunction, "Schema");
			string metadataProperty3 = MetadataHelpers.GetMetadataProperty<string>(edmFunction, "StoreFunctionNameAttribute");
			string metadataProperty4 = MetadataHelpers.GetMetadataProperty<string>(edmFunction, "EFOracleProviderExtensions:CursorParameterName");
			if (!string.IsNullOrEmpty(metadataProperty4))
			{
				parameters = new List<OracleParameter>();
				OracleParameter oracleParameter = new OracleParameter();
				oracleParameter.OracleDbType = OracleDbType.RefCursor;
				oracleParameter.ParameterName = metadataProperty4;
				oracleParameter.Direction = ParameterDirection.Output;
				parameters.Add(oracleParameter);
			}
			if (!string.IsNullOrEmpty(metadataProperty))
			{
				commandType = CommandType.Text;
				return metadataProperty;
			}
			commandType = CommandType.StoredProcedure;
			string name = string.IsNullOrEmpty(metadataProperty3) ? edmFunction.Name : metadataProperty3;
			string text = SqlGenerator.QuoteIdentifier_storeFunctionName(name);
			if (!string.IsNullOrEmpty(metadataProperty2))
			{
				string str = SqlGenerator.QuoteIdentifier(metadataProperty2);
				return str + "." + text;
			}
			return text;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0006EB00 File Offset: 0x0006CD00
		private string GenerateSql(DbQueryCommandTree tree, out HashSet<string> ListOfParamsToMakeUnicodeFalse)
		{
			this.selectStatementStack = new Stack<SqlSelectStatement>();
			this.isParentAJoinStack = new Stack<bool>();
			this.allExtentNames = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			this.allColumnNames = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			ISqlFragment sqlStatement;
			if (MetadataHelpers.IsCollectionType(tree.Query.ResultType.EdmType))
			{
				SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(tree.Query);
				sqlSelectStatement.IsTopMost = true;
				sqlStatement = sqlSelectStatement;
			}
			else
			{
				SqlBuilder sqlBuilder = new SqlBuilder();
				sqlBuilder.Append("SELECT ");
				sqlBuilder.Append(tree.Query.Accept<ISqlFragment>(this));
				sqlStatement = sqlBuilder;
			}
			if (this.isVarRefSingle)
			{
				throw new NotSupportedException();
			}
			ListOfParamsToMakeUnicodeFalse = new HashSet<string>((from p in this.ListOfParamsForNonUnicode
			where p.Value
			select p into q
			select q.Key).ToList<string>());
			return this.WriteSql(sqlStatement);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0006EC04 File Offset: 0x0006CE04
		private string WriteSql(ISqlFragment sqlStatement)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			using (SqlWriter sqlWriter = new SqlWriter(stringBuilder))
			{
				sqlStatement.WriteSql(sqlWriter, this);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0006EC50 File Offset: 0x0006CE50
		public override ISqlFragment Visit(DbAndExpression e)
		{
			return this.VisitBinaryExpression(" AND ", DbExpressionKind.And, e.Left, e.Right);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0006EC6C File Offset: 0x0006CE6C
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
					throw new InvalidOperationException();
				}
				joinString = "OUTER APPLY";
			}
			else
			{
				joinString = "CROSS APPLY";
			}
			return this.VisitJoinExpression(list, DbExpressionKind.CrossJoin, joinString, null);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0006ECCC File Offset: 0x0006CECC
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
				{
					SqlBuilder sqlBuilder = new SqlBuilder();
					sqlBuilder.Append("MOD(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(this));
					sqlBuilder.Append(",");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(this));
					sqlBuilder.Append(")");
					return sqlBuilder;
				}
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

		// Token: 0x060009C0 RID: 2496 RVA: 0x0006EE70 File Offset: 0x0006D070
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

		// Token: 0x060009C1 RID: 2497 RVA: 0x0006EF28 File Offset: 0x0006D128
		public override ISqlFragment Visit(DbCastExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			string sqlPrimitiveType = this.GetSqlPrimitiveType(e.ResultType);
			string a;
			if ((a = sqlPrimitiveType.ToLowerInvariant()) != null)
			{
				if (a == "nclob")
				{
					sqlBuilder.Append("TO_NCLOB(");
					sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
					sqlBuilder.Append(")");
					return sqlBuilder;
				}
				if (a == "clob")
				{
					sqlBuilder.Append("TO_CLOB(");
					sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
					sqlBuilder.Append(")");
					return sqlBuilder;
				}
				if (a == "blob")
				{
					sqlBuilder.Append("TO_BLOB(");
					sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
					sqlBuilder.Append(")");
					return sqlBuilder;
				}
			}
			sqlBuilder.Append(" CAST( ");
			sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
			sqlBuilder.Append(" AS ");
			sqlBuilder.Append(sqlPrimitiveType);
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0006F040 File Offset: 0x0006D240
		public override ISqlFragment Visit(DbComparisonExpression e)
		{
			if (MetadataHelpers.IsPrimitiveType(e.Left.ResultType, PrimitiveTypeKind.String))
			{
				this._bNeedToMakeUnicodeFalse = this.CheckIfNeedToMakeUnicodeFalse(e);
			}
			DbExpressionKind expressionKind = e.ExpressionKind;
			SqlBuilder result;
			if (expressionKind <= DbExpressionKind.GreaterThanOrEquals)
			{
				if (expressionKind == DbExpressionKind.Equals)
				{
					result = this.VisitComparisonExpression(" = ", e.Left, e.Right);
					goto IL_110;
				}
				switch (expressionKind)
				{
				case DbExpressionKind.GreaterThan:
					result = this.VisitComparisonExpression(" > ", e.Left, e.Right);
					goto IL_110;
				case DbExpressionKind.GreaterThanOrEquals:
					result = this.VisitComparisonExpression(" >= ", e.Left, e.Right);
					goto IL_110;
				}
			}
			else
			{
				switch (expressionKind)
				{
				case DbExpressionKind.LessThan:
					result = this.VisitComparisonExpression(" < ", e.Left, e.Right);
					goto IL_110;
				case DbExpressionKind.LessThanOrEquals:
					result = this.VisitComparisonExpression(" <= ", e.Left, e.Right);
					goto IL_110;
				default:
					if (expressionKind == DbExpressionKind.NotEquals)
					{
						result = this.VisitComparisonExpression(" <> ", e.Left, e.Right);
						goto IL_110;
					}
					break;
				}
			}
			throw new InvalidOperationException(string.Empty);
			IL_110:
			this._bNeedToMakeUnicodeFalse = false;
			return result;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0006F168 File Offset: 0x0006D368
		private bool CheckIfNeedToMakeUnicodeFalse(DbExpression e)
		{
			if (this._bNeedToMakeUnicodeFalse)
			{
				throw new NotSupportedException();
			}
			DbExpressionKind expressionKind = e.ExpressionKind;
			if (expressionKind == DbExpressionKind.Like)
			{
				DbLikeExpression dbLikeExpression = (DbLikeExpression)e;
				return SqlGenerator.IsSourceUnicodeFalse(dbLikeExpression.Argument) && this.IsTargetUnicodeNull(dbLikeExpression.Pattern) && this.IsTargetUnicodeNull(dbLikeExpression.Escape);
			}
			DbComparisonExpression dbComparisonExpression = (DbComparisonExpression)e;
			DbExpression left = dbComparisonExpression.Left;
			DbExpression right = dbComparisonExpression.Right;
			return (SqlGenerator.IsSourceUnicodeFalse(left) && this.IsTargetUnicodeNull(right)) || (SqlGenerator.IsSourceUnicodeFalse(right) && this.IsTargetUnicodeNull(left));
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0006F1FC File Offset: 0x0006D3FC
		internal bool IsTargetUnicodeNull(DbExpression expr)
		{
			if (SqlGenerator.IsConstParamOrNullExpressionAndUnicodeIsNull(expr))
			{
				return true;
			}
			if (expr.ExpressionKind == DbExpressionKind.Function)
			{
				DbFunctionExpression dbFunctionExpression = (DbFunctionExpression)expr;
				EdmFunction function = dbFunctionExpression.Function;
				if (!MetadataHelpers.IsCanonicalFunction(function) && !SqlGenerator.IsBuiltInStoreFunction(function))
				{
					return false;
				}
				if ("Edm.Left".Equals(function.FullName, StringComparison.Ordinal) || "Edm.LTrim".Equals(function.FullName, StringComparison.Ordinal) || "Edm.Reverse".Equals(function.FullName, StringComparison.Ordinal) || "Edm.Right".Equals(function.FullName, StringComparison.Ordinal) || "Edm.RTrim".Equals(function.FullName, StringComparison.Ordinal) || "Edm.Substring".Equals(function.FullName, StringComparison.Ordinal) || "Edm.ToLower".Equals(function.FullName, StringComparison.Ordinal) || "Edm.ToUpper".Equals(function.FullName, StringComparison.Ordinal) || "Edm.Trim".Equals(function.FullName, StringComparison.Ordinal))
				{
					return this.IsTargetUnicodeNull(dbFunctionExpression.Arguments[0]);
				}
				if ("Edm.Concat".Equals(function.FullName, StringComparison.Ordinal))
				{
					return (this.IsTargetUnicodeNull(dbFunctionExpression.Arguments[0]) || SqlGenerator.IsConstParamOrNullExpressionAndUnicodeIsNullOrFalse(dbFunctionExpression.Arguments[0])) && (this.IsTargetUnicodeNull(dbFunctionExpression.Arguments[1]) || SqlGenerator.IsConstParamOrNullExpressionAndUnicodeIsNullOrFalse(dbFunctionExpression.Arguments[1]));
				}
				if ("Edm.Replace".Equals(function.FullName, StringComparison.Ordinal))
				{
					return (this.IsTargetUnicodeNull(dbFunctionExpression.Arguments[0]) || SqlGenerator.IsConstParamOrNullExpressionAndUnicodeIsNullOrFalse(dbFunctionExpression.Arguments[0])) && (this.IsTargetUnicodeNull(dbFunctionExpression.Arguments[1]) || SqlGenerator.IsConstParamOrNullExpressionAndUnicodeIsNullOrFalse(dbFunctionExpression.Arguments[1])) && (this.IsTargetUnicodeNull(dbFunctionExpression.Arguments[2]) || SqlGenerator.IsConstParamOrNullExpressionAndUnicodeIsNullOrFalse(dbFunctionExpression.Arguments[2]));
				}
			}
			return false;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0006F3F4 File Offset: 0x0006D5F4
		private static bool IsSourceUnicodeFalse(DbExpression argument)
		{
			bool flag;
			return argument.ExpressionKind == DbExpressionKind.Property && MetadataHelpers.TryGetIsUnicode(argument.ResultType, out flag) && !flag;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0006F420 File Offset: 0x0006D620
		internal static bool IsConstParamOrNullExpressionAndUnicodeIsNull(DbExpression argument)
		{
			DbExpressionKind expressionKind = argument.ExpressionKind;
			TypeUsage resultType = argument.ResultType;
			bool flag;
			return MetadataHelpers.IsPrimitiveType(resultType, PrimitiveTypeKind.String) && (expressionKind == DbExpressionKind.Constant || expressionKind == DbExpressionKind.ParameterReference || expressionKind == DbExpressionKind.Null) && !MetadataHelpers.TryGetBooleanFacetValue(resultType, "Unicode", out flag);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0006F468 File Offset: 0x0006D668
		internal static bool IsConstParamOrNullExpressionAndUnicodeIsNullOrFalse(DbExpression argument)
		{
			DbExpressionKind expressionKind = argument.ExpressionKind;
			TypeUsage resultType = argument.ResultType;
			bool flag;
			return MetadataHelpers.IsPrimitiveType(resultType, PrimitiveTypeKind.String) && (expressionKind == DbExpressionKind.Constant || expressionKind == DbExpressionKind.ParameterReference || expressionKind == DbExpressionKind.Null) && (!MetadataHelpers.TryGetBooleanFacetValue(resultType, "Unicode", out flag) || !flag);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0006F4B4 File Offset: 0x0006D6B4
		private ISqlFragment VisitConstant(DbConstantExpression e, bool isCastOptional)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			PrimitiveTypeKind primitiveTypeKind;
			if (MetadataHelpers.TryGetPrimitiveTypeKind(e.ResultType, out primitiveTypeKind))
			{
				switch (primitiveTypeKind)
				{
				case PrimitiveTypeKind.Binary:
					sqlBuilder.Append(" CAST('");
					sqlBuilder.Append(SqlGenerator.ByteArrayToBinaryString((byte[])e.Value));
					sqlBuilder.Append("' AS RAW(");
					sqlBuilder.Append(((byte[])e.Value).Length.ToString());
					sqlBuilder.Append("))");
					return sqlBuilder;
				case PrimitiveTypeKind.Boolean:
					sqlBuilder.Append(((bool)e.Value) ? "1" : "0");
					return sqlBuilder;
				case PrimitiveTypeKind.Byte:
					SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, e.Value.ToString(), "number(2)", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.DateTime:
					sqlBuilder.Append("TO_TIMESTAMP(");
					sqlBuilder.Append(SqlGenerator.EscapeSingleQuote(((DateTime)e.Value).ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture), false));
					sqlBuilder.Append(", 'YYYY-MM-DD HH24:MI:SS.FF')");
					return sqlBuilder;
				case PrimitiveTypeKind.Decimal:
				{
					string text = ((decimal)e.Value).ToString(CultureInfo.InvariantCulture);
					if (-1 == text.IndexOf('.'))
					{
						bool flag = text.TrimStart(new char[]
						{
							'-'
						}).Length < 20;
					}
					string typeName = "decimal(" + Math.Max((byte)text.Length, 38).ToString(CultureInfo.InvariantCulture) + ")";
					bool cast = false;
					SqlGenerator.WrapWithCastIfNeeded(cast, text, typeName, sqlBuilder);
					return sqlBuilder;
				}
				case PrimitiveTypeKind.Double:
					isCastOptional = true;
					if (this.IsPre10g)
					{
						SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, ((double)e.Value).ToString("R", CultureInfo.InvariantCulture), "number", sqlBuilder);
						return sqlBuilder;
					}
					SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, ((double)e.Value).ToString("R", CultureInfo.InvariantCulture), "binary_double", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.Guid:
					sqlBuilder.Append(" CAST('");
					if (e.Value is Guid)
					{
						sqlBuilder.Append(SqlGenerator.ByteArrayToBinaryString(((Guid)e.Value).ToByteArray()));
					}
					else
					{
						sqlBuilder.Append(SqlGenerator.ByteArrayToBinaryString((byte[])e.Value));
					}
					sqlBuilder.Append("' AS RAW(16))");
					return sqlBuilder;
				case PrimitiveTypeKind.Single:
					isCastOptional = true;
					if (this.IsPre10g)
					{
						SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, ((float)e.Value).ToString("R", CultureInfo.InvariantCulture), "number", sqlBuilder);
						return sqlBuilder;
					}
					SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, ((float)e.Value).ToString("R", CultureInfo.InvariantCulture), "binary_float", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.Int16:
					isCastOptional = true;
					SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, e.Value.ToString(), "number(4)", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.Int32:
					sqlBuilder.Append(e.Value.ToString());
					return sqlBuilder;
				case PrimitiveTypeKind.Int64:
					isCastOptional = true;
					SqlGenerator.WrapWithCastIfNeeded(!isCastOptional, e.Value.ToString(), "number(18)", sqlBuilder);
					return sqlBuilder;
				case PrimitiveTypeKind.String:
				{
					bool isUnicode;
					if (!MetadataHelpers.TryGetIsUnicode(e.ResultType, out isUnicode))
					{
						isUnicode = false;
					}
					sqlBuilder.Append(SqlGenerator.EscapeSingleQuote(e.Value as string, isUnicode));
					return sqlBuilder;
				}
				case PrimitiveTypeKind.Time:
					throw new NotSupportedException(EFProviderSettings.Instance.GetErrorMessage(-1703, new string[]
					{
						"Oracle Data Provider for .NET",
						primitiveTypeKind.ToString()
					}));
				case PrimitiveTypeKind.DateTimeOffset:
					sqlBuilder.Append("TO_TIMESTAMP_TZ(");
					sqlBuilder.Append(SqlGenerator.EscapeSingleQuote(((DateTimeOffset)e.Value).ToString("yyyy-MM-dd HH:mm:ss.fff zzz", CultureInfo.InvariantCulture), false));
					sqlBuilder.Append(", 'yyyy-mm-dd HH24:MI:SS.FF3 TZH:TZM')");
					return sqlBuilder;
				}
				throw new NotSupportedException();
			}
			throw new NotSupportedException();
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0006F8D0 File Offset: 0x0006DAD0
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

		// Token: 0x060009CA RID: 2506 RVA: 0x0006F90C File Offset: 0x0006DB0C
		public override ISqlFragment Visit(DbConstantExpression e)
		{
			return this.VisitConstant(e, false);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0006F918 File Offset: 0x0006DB18
		public override ISqlFragment Visit(DbDerefExpression e)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0006F920 File Offset: 0x0006DB20
		public override ISqlFragment Visit(DbDistinctExpression e)
		{
			SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(e.Argument);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				TypeUsage elementTypeUsage = MetadataHelpers.GetElementTypeUsage(e.Argument.ResultType);
				Symbol fromSymbol;
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, "distinct", elementTypeUsage, out fromSymbol);
				this.AddFromSymbol(sqlSelectStatement, "distinct", fromSymbol, false);
			}
			sqlSelectStatement.IsDistinct = true;
			return sqlSelectStatement;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0006F980 File Offset: 0x0006DB80
		public override ISqlFragment Visit(DbElementExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("(");
			sqlBuilder.Append(this.VisitExpressionEnsureSqlStatement(e.Argument));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0006F9BC File Offset: 0x0006DBBC
		public override ISqlFragment Visit(DbExceptExpression e)
		{
			return this.VisitSetOpExpression(e.Left, e.Right, "MINUS");
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0006F9D8 File Offset: 0x0006DBD8
		public override ISqlFragment Visit(DbExpression e)
		{
			throw new InvalidOperationException(string.Empty);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0006F9E4 File Offset: 0x0006DBE4
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

		// Token: 0x060009D1 RID: 2513 RVA: 0x0006FA2C File Offset: 0x0006DC2C
		internal static string GetTargetTSql(EntitySetBase entitySetBase)
		{
			string metadataProperty = MetadataHelpers.GetMetadataProperty<string>(entitySetBase, "DefiningQuery");
			if (metadataProperty == null)
			{
				string metadataProperty2 = MetadataHelpers.GetMetadataProperty<string>(entitySetBase, "Schema");
				StringBuilder stringBuilder = new StringBuilder(50);
				if (!string.IsNullOrEmpty(metadataProperty2))
				{
					stringBuilder.Append(SqlGenerator.QuoteIdentifier(metadataProperty2));
					stringBuilder.Append(".");
				}
				string metadataProperty3 = MetadataHelpers.GetMetadataProperty<string>(entitySetBase, "Table");
				if (!string.IsNullOrEmpty(metadataProperty3))
				{
					stringBuilder.Append(SqlGenerator.QuoteIdentifier(metadataProperty3));
				}
				else
				{
					stringBuilder.Append(SqlGenerator.QuoteIdentifier(entitySetBase.Name));
				}
				return stringBuilder.ToString();
			}
			return "(" + metadataProperty + ")";
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0006FACC File Offset: 0x0006DCCC
		public override ISqlFragment Visit(DbFilterExpression e)
		{
			return this.VisitFilterExpression(e.Input, e.Predicate, false);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0006FAE4 File Offset: 0x0006DCE4
		public override ISqlFragment Visit(DbFunctionExpression e)
		{
			if (SqlGenerator.IsSpecialCanonicalFunction(e))
			{
				return this.HandleSpecialCanonicalFunction(e);
			}
			return this.HandleFunctionDefault(e);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0006FB00 File Offset: 0x0006DD00
		public override ISqlFragment Visit(DbLambdaExpression expression)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0006FB08 File Offset: 0x0006DD08
		public override ISqlFragment Visit(DbEntityRefExpression e)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0006FB10 File Offset: 0x0006DD10
		public override ISqlFragment Visit(DbRefKeyExpression e)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0006FB18 File Offset: 0x0006DD18
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
			RowType edmType = MetadataHelpers.GetEdmType<RowType>(MetadataHelpers.GetEdmType<CollectionType>(e.ResultType).TypeUsage);
			bool flag = SqlGenerator.GroupByAggregatesNeedInnerQuery(e.Aggregates) || SqlGenerator.GroupByKeysNeedInnerQuery(e.Keys, e.Input.VariableName);
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

		// Token: 0x060009D8 RID: 2520 RVA: 0x0006FF88 File Offset: 0x0006E188
		public override ISqlFragment Visit(DbIntersectExpression e)
		{
			return this.VisitSetOpExpression(e.Left, e.Right, "INTERSECT");
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0006FFA4 File Offset: 0x0006E1A4
		public override ISqlFragment Visit(DbIsEmptyExpression e)
		{
			return this.VisitIsEmptyExpression(e, false);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0006FFB0 File Offset: 0x0006E1B0
		public override ISqlFragment Visit(DbIsNullExpression e)
		{
			return this.VisitIsNullExpression(e, false);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0006FFBC File Offset: 0x0006E1BC
		public override ISqlFragment Visit(DbIsOfExpression e)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0006FFC4 File Offset: 0x0006E1C4
		public override ISqlFragment Visit(DbCrossJoinExpression e)
		{
			return this.VisitJoinExpression(e.Inputs, e.ExpressionKind, "CROSS JOIN", null);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0006FFE0 File Offset: 0x0006E1E0
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

		// Token: 0x060009DE RID: 2526 RVA: 0x00070054 File Offset: 0x0006E254
		public override ISqlFragment Visit(DbLikeExpression e)
		{
			this._bNeedToMakeUnicodeFalse = this.CheckIfNeedToMakeUnicodeFalse(e);
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
			sqlBuilder.Append(" LIKE ");
			sqlBuilder.Append(e.Pattern.Accept<ISqlFragment>(this));
			if (e.Escape.ExpressionKind != DbExpressionKind.Null)
			{
				sqlBuilder.Append(" ESCAPE ");
				SqlBuilder sqlBuilder2 = (SqlBuilder)e.Escape.Accept<ISqlFragment>(this);
				if (!sqlBuilder2.IsEmpty && ((string)sqlBuilder2.sqlFragments[0]).StartsWith("N'"))
				{
					sqlBuilder2.sqlFragments[0] = ((string)sqlBuilder2.sqlFragments[0]).Remove(0, 1);
				}
				sqlBuilder.Append(sqlBuilder2);
			}
			this._bNeedToMakeUnicodeFalse = false;
			return sqlBuilder;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00070128 File Offset: 0x0006E328
		public override ISqlFragment Visit(DbLimitExpression e)
		{
			SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(e.Argument, false);
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				TypeUsage elementTypeUsage = MetadataHelpers.GetElementTypeUsage(e.Argument.ResultType);
				Symbol fromSymbol;
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, "top", elementTypeUsage, out fromSymbol);
				this.AddFromSymbol(sqlSelectStatement, "top", fromSymbol, false);
			}
			ISqlFragment topCount = this.HandleCountExpression(e.Limit);
			sqlSelectStatement.Top = new TopClause(topCount, e.WithTies);
			return sqlSelectStatement;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000701A0 File Offset: 0x0006E3A0
		public override ISqlFragment Visit(DbNewInstanceExpression e)
		{
			if (MetadataHelpers.IsCollectionType(e.ResultType.EdmType))
			{
				return this.VisitCollectionConstructor(e);
			}
			throw new NotSupportedException();
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x000701C4 File Offset: 0x0006E3C4
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
				return this.VisitBinaryExpression(" <> ", DbExpressionKind.NotEquals, dbComparisonExpression.Left, dbComparisonExpression.Right);
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(" NOT (");
			sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00070284 File Offset: 0x0006E484
		public override ISqlFragment Visit(DbNullExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("NULL");
			return sqlBuilder;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x000702A4 File Offset: 0x0006E4A4
		public override ISqlFragment Visit(DbOfTypeExpression e)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x000702AC File Offset: 0x0006E4AC
		public override ISqlFragment Visit(DbOrExpression e)
		{
			return this.VisitBinaryExpression(" OR ", e.ExpressionKind, e.Left, e.Right);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000702CC File Offset: 0x0006E4CC
		public override ISqlFragment Visit(DbParameterReferenceExpression e)
		{
			if (!this._bIgnoreMakingUnicodeFalse)
			{
				if (!this._bNeedToMakeUnicodeFalse)
				{
					this.ListOfParamsForNonUnicode[e.ParameterName] = false;
				}
				else if (!this.ListOfParamsForNonUnicode.ContainsKey(e.ParameterName))
				{
					this.ListOfParamsForNonUnicode[e.ParameterName] = true;
				}
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(":" + e.ParameterName);
			return sqlBuilder;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00070340 File Offset: 0x0006E540
		public override ISqlFragment Visit(DbProjectExpression e)
		{
			Symbol fromSymbol;
			SqlSelectStatement sqlSelectStatement = this.VisitInputExpression(e.Input.Expression, e.Input.VariableName, e.Input.VariableType, out fromSymbol);
			bool flag = false;
			if (!SqlGenerator.IsCompatible(sqlSelectStatement, e.ExpressionKind))
			{
				sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, e.Input.VariableName, e.Input.VariableType, out fromSymbol);
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
					sqlSelectStatement.OutputColumns = outputColumns;
				}
			}
			else
			{
				sqlSelectStatement.Select.Append(e.Projection.Accept<ISqlFragment>(this));
			}
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			return sqlSelectStatement;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00070438 File Offset: 0x0006E638
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
						sqlBuilder.Append(symbolPair.Column.Columns[e.Property.Name]);
						return sqlBuilder;
					}
				}
				sqlBuilder = new SqlBuilder();
				sqlBuilder.Append(sqlFragment);
				sqlBuilder.Append(".");
				Symbol symbol = sqlFragment as Symbol;
				if (symbol != null && symbol.OutputColumnsRenamed)
				{
					sqlBuilder.Append(symbol.Columns[e.Property.Name]);
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

		// Token: 0x060009E8 RID: 2536 RVA: 0x000705B4 File Offset: 0x0006E7B4
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

		// Token: 0x060009E9 RID: 2537 RVA: 0x00070630 File Offset: 0x0006E830
		public override ISqlFragment Visit(DbRefExpression e)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00070638 File Offset: 0x0006E838
		public override ISqlFragment Visit(DbRelationshipNavigationExpression e)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00070640 File Offset: 0x0006E840
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
			List<Symbol> columnList = this.AddDefaultColumns(sqlSelectStatement);
			sqlSelectStatement.Select.Append(", row_number() OVER (ORDER BY ");
			this.AddSortKeys(sqlSelectStatement.Select, e.SortOrder);
			sqlSelectStatement.Select.Append(") AS ");
			Symbol s = new Symbol("row_number", null);
			sqlSelectStatement.Select.Append(s);
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			SqlSelectStatement sqlSelectStatement2 = new SqlSelectStatement();
			sqlSelectStatement2.From.Append("( ");
			sqlSelectStatement2.From.Append(sqlSelectStatement);
			sqlSelectStatement2.From.AppendLine();
			sqlSelectStatement2.From.Append(") ");
			Symbol symbol = null;
			if (sqlSelectStatement.FromExtents.Count == 1)
			{
				JoinSymbol joinSymbol = sqlSelectStatement.FromExtents[0] as JoinSymbol;
				if (joinSymbol != null)
				{
					symbol = new JoinSymbol(e.Input.VariableName, e.Input.VariableType, joinSymbol.ExtentList)
					{
						IsNestedJoin = true,
						ColumnList = columnList,
						FlattenedExtentList = joinSymbol.FlattenedExtentList
					};
				}
			}
			if (symbol == null)
			{
				symbol = new Symbol(e.Input.VariableName, e.Input.VariableType);
			}
			this.selectStatementStack.Push(sqlSelectStatement2);
			this.symbolTable.EnterScope();
			this.AddFromSymbol(sqlSelectStatement2, e.Input.VariableName, symbol);
			sqlSelectStatement2.Where.Append(symbol);
			sqlSelectStatement2.Where.Append(".");
			sqlSelectStatement2.Where.Append(s);
			sqlSelectStatement2.Where.Append(" > ");
			sqlSelectStatement2.Where.Append(this.HandleCountExpression(e.Count));
			this.AddSortKeys(sqlSelectStatement2.OrderBy, e.SortOrder);
			this.symbolTable.ExitScope();
			this.selectStatementStack.Pop();
			return sqlSelectStatement2;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000708B8 File Offset: 0x0006EAB8
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

		// Token: 0x060009ED RID: 2541 RVA: 0x00070974 File Offset: 0x0006EB74
		public override ISqlFragment Visit(DbTreatExpression e)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0007097C File Offset: 0x0006EB7C
		public override ISqlFragment Visit(DbUnionAllExpression e)
		{
			return this.VisitSetOpExpression(e.Left, e.Right, "UNION ALL");
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00070998 File Offset: 0x0006EB98
		public override ISqlFragment Visit(DbVariableReferenceExpression e)
		{
			if (this.isVarRefSingle)
			{
				throw new NotSupportedException();
			}
			this.isVarRefSingle = true;
			Symbol symbol = this.symbolTable.Lookup(e.VariableName);
			if (!this.CurrentSelectStatement.FromExtents.Contains(symbol))
			{
				this.CurrentSelectStatement.OuterExtents[symbol] = true;
			}
			return symbol;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000709F4 File Offset: 0x0006EBF4
		private static SqlBuilder VisitAggregate(DbAggregate aggregate, object aggregateArgument)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			DbFunctionAggregate dbFunctionAggregate = aggregate as DbFunctionAggregate;
			if (dbFunctionAggregate == null)
			{
				throw new NotSupportedException();
			}
			SqlGenerator.WriteFunctionName(sqlBuilder, dbFunctionAggregate.Function);
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

		// Token: 0x060009F1 RID: 2545 RVA: 0x00070A5C File Offset: 0x0006EC5C
		private void ParanthesizeExpressionIfNeeded(DbExpression e, SqlBuilder result)
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

		// Token: 0x060009F2 RID: 2546 RVA: 0x00070A98 File Offset: 0x0006EC98
		private SqlBuilder VisitBinaryExpression(string op, DbExpressionKind expressionKind, DbExpression left, DbExpression right)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			bool flag = true;
			foreach (DbExpression e in CommandTreeUtils.FlattenAssociativeExpression(expressionKind, new DbExpression[]
			{
				left,
				right
			}))
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					sqlBuilder.Append(op);
				}
				this.ParanthesizeExpressionIfNeeded(e, sqlBuilder);
			}
			return sqlBuilder;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00070B14 File Offset: 0x0006ED14
		private SqlBuilder VisitComparisonExpression(string op, DbExpression left, DbExpression right)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (left.ExpressionKind == DbExpressionKind.Function)
			{
				DbFunctionExpression dbFunctionExpression = (DbFunctionExpression)left;
				if (dbFunctionExpression.Function.Name.Equals("regexp_LIKE", StringComparison.InvariantCultureIgnoreCase) && MetadataHelpers.IsProviderSpecificFunction(dbFunctionExpression.Function))
				{
					flag = true;
				}
			}
			else if (left.ExpressionKind == DbExpressionKind.Constant)
			{
				flag3 = true;
			}
			if (right.ExpressionKind == DbExpressionKind.Function)
			{
				DbFunctionExpression dbFunctionExpression = (DbFunctionExpression)right;
				if (dbFunctionExpression.Function.Name.Equals("regexp_LIKE", StringComparison.InvariantCultureIgnoreCase) && MetadataHelpers.IsProviderSpecificFunction(dbFunctionExpression.Function))
				{
					flag2 = true;
				}
			}
			else if (right.ExpressionKind == DbExpressionKind.Constant)
			{
				flag4 = true;
			}
			bool isCastOptional = left.ResultType.EdmType == right.ResultType.EdmType;
			if (flag && flag4)
			{
				this.ParanthesizeExpressionIfNeeded(left, sqlBuilder);
				return sqlBuilder;
			}
			if (flag2 && flag3)
			{
				this.ParanthesizeExpressionIfNeeded(right, sqlBuilder);
				return sqlBuilder;
			}
			if (flag3)
			{
				sqlBuilder.Append(this.VisitConstant((DbConstantExpression)left, isCastOptional));
			}
			else
			{
				this.ParanthesizeExpressionIfNeeded(left, sqlBuilder);
			}
			sqlBuilder.Append(op);
			if (flag4)
			{
				sqlBuilder.Append(this.VisitConstant((DbConstantExpression)right, isCastOptional));
			}
			else
			{
				this.ParanthesizeExpressionIfNeeded(right, sqlBuilder);
			}
			return sqlBuilder;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00070C50 File Offset: 0x0006EE50
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

		// Token: 0x060009F5 RID: 2549 RVA: 0x00070CF4 File Offset: 0x0006EEF4
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

		// Token: 0x060009F6 RID: 2550 RVA: 0x00070D44 File Offset: 0x0006EF44
		private ISqlFragment VisitCollectionConstructor(DbNewInstanceExpression e)
		{
			if (e.Arguments.Count == 1 && e.Arguments[0].ExpressionKind == DbExpressionKind.Element)
			{
				DbElementExpression dbElementExpression = e.Arguments[0] as DbElementExpression;
				SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(dbElementExpression.Argument);
				if (!SqlGenerator.IsCompatible(sqlSelectStatement, DbExpressionKind.Element))
				{
					TypeUsage elementTypeUsage = MetadataHelpers.GetElementTypeUsage(dbElementExpression.Argument.ResultType);
					Symbol fromSymbol;
					sqlSelectStatement = this.CreateNewSelectStatement(sqlSelectStatement, "element", elementTypeUsage, out fromSymbol);
					this.AddFromSymbol(sqlSelectStatement, "element", fromSymbol, false);
				}
				sqlSelectStatement.Top = new TopClause(1, false);
				return sqlSelectStatement;
			}
			CollectionType edmType = MetadataHelpers.GetEdmType<CollectionType>(e.ResultType);
			bool flag = MetadataHelpers.IsPrimitiveType(edmType.TypeUsage.EdmType);
			SqlBuilder sqlBuilder = new SqlBuilder();
			string s = "";
			if (e.Arguments.Count == 0)
			{
				sqlBuilder.Append(" SELECT CAST(null as ");
				sqlBuilder.Append(this.GetSqlPrimitiveType(edmType.TypeUsage));
				sqlBuilder.Append(") AS X FROM DUAL Y WHERE 1=0");
			}
			foreach (DbExpression dbExpression in e.Arguments)
			{
				sqlBuilder.Append(s);
				sqlBuilder.Append(" SELECT ");
				sqlBuilder.Append(dbExpression.Accept<ISqlFragment>(this));
				if (flag)
				{
					sqlBuilder.Append(" FROM DUAL ");
				}
				s = " UNION ALL ";
			}
			return sqlBuilder;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00070EC0 File Offset: 0x0006F0C0
		private SqlBuilder VisitIsNullExpression(DbIsNullExpression e, bool negate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (e.Argument.ExpressionKind == DbExpressionKind.ParameterReference)
			{
				this._bIgnoreMakingUnicodeFalse = true;
			}
			sqlBuilder.Append(e.Argument.Accept<ISqlFragment>(this));
			this._bIgnoreMakingUnicodeFalse = false;
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

		// Token: 0x060009F8 RID: 2552 RVA: 0x00070F20 File Offset: 0x0006F120
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

		// Token: 0x060009F9 RID: 2553 RVA: 0x000710B4 File Offset: 0x0006F2B4
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
							else if (sqlSelectStatement.FromExtents[0].OutputColumnsRenamed)
							{
								symbol = new Symbol(input.VariableName, input.VariableType, sqlSelectStatement.FromExtents[0].Columns);
							}
						}
					}
					else if (sqlSelectStatement.OutputColumnsRenamed)
					{
						symbol = new Symbol(input.VariableName, input.VariableType, sqlSelectStatement.OutputColumns);
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

		// Token: 0x060009FA RID: 2554 RVA: 0x000712F8 File Offset: 0x0006F4F8
		private ISqlFragment VisitNewInstanceExpression(DbNewInstanceExpression e, bool aliasesNeedRenaming, out Dictionary<string, Symbol> newColumns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			RowType rowType = e.ResultType.EdmType as RowType;
			if (rowType != null)
			{
				if (aliasesNeedRenaming)
				{
					newColumns = new Dictionary<string, Symbol>(e.Arguments.Count);
				}
				else
				{
					newColumns = null;
				}
				ReadOnlyMetadataCollection<EdmProperty> properties = rowType.Properties;
				string s = "";
				for (int i = 0; i < e.Arguments.Count; i++)
				{
					DbExpression dbExpression = e.Arguments[i];
					if (MetadataHelpers.IsRowType(dbExpression.ResultType.EdmType))
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

		// Token: 0x060009FB RID: 2555 RVA: 0x00071434 File Offset: 0x0006F634
		private ISqlFragment VisitSetOpExpression(DbExpression left, DbExpression right, string separator)
		{
			SqlSelectStatement sqlSelectStatement = this.VisitExpressionEnsureSqlStatement(left);
			SqlSelectStatement s = this.VisitExpressionEnsureSqlStatement(right);
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
			Symbol fromSymbol = new Symbol("X", left.ResultType, sqlSelectStatement.OutputColumns);
			this.AddFromSymbol(sqlSelectStatement2, null, fromSymbol, false);
			return sqlSelectStatement2;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000714E4 File Offset: 0x0006F6E4
		private static bool IsSpecialCanonicalFunction(DbFunctionExpression e)
		{
			return MetadataHelpers.IsCanonicalFunction(e.Function) && SqlGenerator._canonicalFunctionHandlers.ContainsKey(e.Function.Name);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0007150C File Offset: 0x0006F70C
		private ISqlFragment HandleFunctionDefault(DbFunctionExpression e)
		{
			SqlBuilder result = new SqlBuilder();
			SqlGenerator.WriteFunctionName(result, e.Function);
			this.HandleFunctionArgumentsDefault(e, result);
			return result;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00071534 File Offset: 0x0006F734
		private ISqlFragment HandleFunctionDefaultGivenName(DbFunctionExpression e, string functionName)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append(functionName);
			this.HandleFunctionArgumentsDefault(e, sqlBuilder);
			return sqlBuilder;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00071558 File Offset: 0x0006F758
		private void HandleFunctionArgumentsDefault(DbFunctionExpression e, SqlBuilder result)
		{
			bool metadataProperty = MetadataHelpers.GetMetadataProperty<bool>(e.Function, "NiladicFunctionAttribute");
			if (metadataProperty && e.Arguments.Count > 0)
			{
				throw new MetadataException(EFProviderSettings.Instance.GetErrorMessage(-5000, new string[0]));
			}
			if (!metadataProperty)
			{
				result.Append("(");
				string s = "";
				foreach (DbExpression dbExpression in e.Arguments)
				{
					result.Append(s);
					result.Append(dbExpression.Accept<ISqlFragment>(this));
					s = ", ";
				}
				result.Append(")");
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00071614 File Offset: 0x0006F814
		private ISqlFragment HandleSpecialCanonicalFunction(DbFunctionExpression e)
		{
			return this.HandleSpecialFunction(SqlGenerator._canonicalFunctionHandlers, e);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00071624 File Offset: 0x0006F824
		private ISqlFragment HandleSpecialFunction(Dictionary<string, SqlGenerator.FunctionHandler> handlers, DbFunctionExpression e)
		{
			return handlers[e.Function.Name](this, e);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00071640 File Offset: 0x0006F840
		private static ISqlFragment HandleCanonicalFunctionSubstring(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return sqlgen.HandleFunctionDefaultGivenName(e, "SUBSTR");
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00071650 File Offset: 0x0006F850
		private static ISqlFragment HandleCanonicalFunctionLeft(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("SUBSTR (");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(",1,");
			sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000716B8 File Offset: 0x0006F8B8
		private static ISqlFragment HandleCanonicalFunctionRight(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("(CASE WHEN LENGTH(");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(") >= (");
			sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(") THEN ");
			sqlBuilder.Append("SUBSTR (");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(",-(");
			sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append("),");
			sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(")");
			sqlBuilder.Append(" ELSE ");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(" END)");
			return sqlBuilder;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000717C0 File Offset: 0x0006F9C0
		private static ISqlFragment HandleConcatFunction(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("((");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(")||(");
			sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append("))");
			return sqlBuilder;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00071828 File Offset: 0x0006FA28
		private static ISqlFragment HandleCanonicalFunctionBitwise(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			string a;
			if ((a = e.Function.Name.ToUpperInvariant()) != null)
			{
				if (!(a == "BITWISEAND"))
				{
					if (!(a == "BITWISEOR"))
					{
						if (!(a == "BITWISEXOR"))
						{
							if (a == "BITWISENOT")
							{
								sqlBuilder.Append("((0 - ");
								sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
								sqlBuilder.Append(") - 1)");
							}
						}
						else
						{
							sqlBuilder.Append("((");
							sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
							sqlBuilder.Append(")+(");
							sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
							sqlBuilder.Append(")-2*BITAND(");
							sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
							sqlBuilder.Append(",");
							sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
							sqlBuilder.Append("))");
						}
					}
					else
					{
						sqlBuilder.Append("((");
						sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
						sqlBuilder.Append(")+(");
						sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
						sqlBuilder.Append(")-BITAND(");
						sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
						sqlBuilder.Append(",");
						sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
						sqlBuilder.Append("))");
					}
				}
				else
				{
					sqlBuilder.Append("BITAND(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(",");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(")");
				}
			}
			return sqlBuilder;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00071A4C File Offset: 0x0006FC4C
		private static ISqlFragment HandleCanonicalFunctionDatepart(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			if (e.Function.Name.ToUpperInvariant() == "MILLISECOND")
			{
				sqlBuilder.Append(" NVL(TO_NUMBER(SUBSTR(TO_CHAR(CAST(");
				sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
				sqlBuilder.Append(" AS TIMESTAMP(3))");
				sqlBuilder.Append(" , 'DD-MON-RR HH24:MI:SSXFF'), 20, 3)), 0) ");
				return sqlBuilder;
			}
			if (e.Function.Name.ToUpperInvariant() == "DAYOFYEAR")
			{
				sqlBuilder.Append(" TO_NUMBER(TO_CHAR(");
				sqlBuilder.Append("CAST(");
				sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
				sqlBuilder.Append(" AS TIMESTAMP)");
				sqlBuilder.Append(", 'DDD')) ");
				return sqlBuilder;
			}
			sqlBuilder.Append("EXTRACT (");
			sqlBuilder.Append(e.Function.Name.ToUpperInvariant());
			sqlBuilder.Append(" FROM (");
			sqlBuilder.Append(" CAST(");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(" AS TIMESTAMP)");
			sqlBuilder.Append("))");
			return sqlBuilder;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00071B80 File Offset: 0x0006FD80
		private static ISqlFragment HandleCanonicalFunctionDatepartAdd(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			PrimitiveTypeKind primitiveTypeKind;
			MetadataHelpers.TryGetPrimitiveTypeKind(e.Arguments[0].ResultType, out primitiveTypeKind);
			if (primitiveTypeKind != PrimitiveTypeKind.DateTimeOffset)
			{
				sqlBuilder.Append(" CAST(");
			}
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			if (primitiveTypeKind != PrimitiveTypeKind.DateTimeOffset)
			{
				sqlBuilder.Append(" AS TIMESTAMP(9))");
			}
			sqlBuilder.Append(" + ");
			string key;
			switch (key = e.Function.Name.ToUpperInvariant())
			{
			case "ADDYEARS":
				sqlBuilder.Append(" INTERVAL '");
				sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
				sqlBuilder.Append("' YEAR(9) ");
				break;
			case "ADDMONTHS":
				sqlBuilder.Append(" INTERVAL '");
				sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
				sqlBuilder.Append("' MONTH(9) ");
				break;
			case "ADDDAYS":
				sqlBuilder.Append(" INTERVAL '");
				sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
				sqlBuilder.Append("' DAY(9) ");
				break;
			case "ADDHOURS":
				sqlBuilder.Append(" INTERVAL '");
				sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
				sqlBuilder.Append("' HOUR(9) ");
				break;
			case "ADDMINUTES":
				sqlBuilder.Append(" INTERVAL '");
				sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
				sqlBuilder.Append("' MINUTE(9) ");
				break;
			case "ADDSECONDS":
				sqlBuilder.Append(" INTERVAL '");
				sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
				sqlBuilder.Append("' SECOND(9) ");
				break;
			case "ADDMILLISECONDS":
				sqlBuilder.Append(" (INTERVAL '");
				sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
				sqlBuilder.Append("' SECOND(9) / 1000) ");
				break;
			case "ADDMICROSECONDS":
				sqlBuilder.Append(" (INTERVAL '");
				sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
				sqlBuilder.Append("' SECOND(9) / (1000 * 1000)) ");
				break;
			case "ADDNANOSECONDS":
				sqlBuilder.Append(" (INTERVAL '");
				sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
				sqlBuilder.Append("' SECOND(9) / (1000 * 1000 * 1000)) ");
				break;
			}
			return sqlBuilder;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00071E90 File Offset: 0x00070090
		private static ISqlFragment HandleCanonicalFunctionDatepartDiff(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			PrimitiveTypeKind primitiveTypeKind;
			MetadataHelpers.TryGetPrimitiveTypeKind(e.Arguments[1].ResultType, out primitiveTypeKind);
			SqlBuilder sqlBuilder = new SqlBuilder();
			string key;
			switch (key = e.Function.Name.ToUpperInvariant())
			{
			case "DIFFYEARS":
				if (primitiveTypeKind == PrimitiveTypeKind.DateTimeOffset)
				{
					sqlBuilder.Append(" TRUNC(EXTRACT(");
					sqlBuilder.Append(" DAY FROM (");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(")) / 365) ");
				}
				else
				{
					sqlBuilder.Append(" TRUNC(EXTRACT(");
					sqlBuilder.Append(" DAY FROM (");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(")) / 365) ");
				}
				break;
			case "DIFFMONTHS":
				if (primitiveTypeKind == PrimitiveTypeKind.DateTimeOffset)
				{
					sqlBuilder.Append(" TRUNC(EXTRACT(");
					sqlBuilder.Append(" DAY FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(")) / 31) ");
				}
				else
				{
					sqlBuilder.Append(" TRUNC(EXTRACT(");
					sqlBuilder.Append(" DAY FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(")) / 31) ");
				}
				break;
			case "DIFFDAYS":
				if (primitiveTypeKind == PrimitiveTypeKind.DateTimeOffset)
				{
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" DAY FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(")) ");
				}
				else
				{
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" DAY FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(")) ");
				}
				break;
			case "DIFFHOURS":
				if (primitiveTypeKind == PrimitiveTypeKind.DateTimeOffset)
				{
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" DAY FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*24 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" HOUR FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(")) ");
				}
				else
				{
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" DAY FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*24 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" HOUR FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(")) ");
				}
				break;
			case "DIFFMINUTES":
				if (primitiveTypeKind == PrimitiveTypeKind.DateTimeOffset)
				{
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" DAY FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*24*60 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" HOUR FROM (");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*60 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" MINUTE FROM (");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(")) ");
				}
				else
				{
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" DAY FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*24*60 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" HOUR FROM (");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*60 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" MINUTE FROM (");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(")) ");
				}
				break;
			case "DIFFSECONDS":
				if (primitiveTypeKind == PrimitiveTypeKind.DateTimeOffset)
				{
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" DAY FROM (");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*24*60*60 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" HOUR FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*60*60 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" MINUTE FROM (");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*60 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" SECOND FROM (");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(")) ");
				}
				else
				{
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" DAY FROM (");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*24*60*60 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" HOUR FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*60*60 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" MINUTE FROM (");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*60 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" SECOND FROM (");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(")) ");
				}
				break;
			case "DIFFMILLISECONDS":
				if (primitiveTypeKind == PrimitiveTypeKind.DateTimeOffset)
				{
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" DAY FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*24*60*60*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" HOUR FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*60*60*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" MINUTE FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*60*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" SECOND FROM (");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*1000 ");
				}
				else
				{
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" DAY FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*24*60*60*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" HOUR FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9)) ");
					sqlBuilder.Append("))*60*60*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" MINUTE FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9)) ");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9)) ");
					sqlBuilder.Append("))*60*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" SECOND FROM (");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*1000 ");
				}
				break;
			case "DIFFMICROSECONDS":
				if (primitiveTypeKind == PrimitiveTypeKind.DateTimeOffset)
				{
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" DAY FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*24*60*60*1000*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" HOUR FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*60*60*1000*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" MINUTE FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*60*1000*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" SECOND FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*1000*1000 ");
				}
				else
				{
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" DAY FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*24*60*60*1000*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" HOUR FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*60*60*1000*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" MINUTE FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*60*1000*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" SECOND FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*1000*1000 ");
				}
				break;
			case "DIFFNANOSECONDS":
				if (primitiveTypeKind == PrimitiveTypeKind.DateTimeOffset)
				{
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" DAY FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*24*60*60*1000*1000*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" HOUR FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*60*60*1000*1000*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" MINUTE FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*60*1000*1000*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" SECOND FROM(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("))*1000*1000*1000 ");
				}
				else
				{
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" DAY FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9)) ");
					sqlBuilder.Append("))*24*60*60*1000*1000*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" HOUR FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*60*60*1000*1000*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" MINUTE FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*60*1000*1000*1000 + ");
					sqlBuilder.Append(" EXTRACT(");
					sqlBuilder.Append(" SECOND FROM(");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append(" - ");
					sqlBuilder.Append(" CAST(");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(" AS TIMESTAMP(9))");
					sqlBuilder.Append("))*1000*1000*1000 ");
				}
				break;
			}
			return sqlBuilder;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00073590 File Offset: 0x00071790
		private static ISqlFragment HandleCanonicalFunctionGetTotalOffsetMinutes(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("(EXTRACT (TIMEZONE_HOUR FROM (");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(")) * 60 + EXTRACT (TIMEZONE_MINUTE FROM (");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(")))");
			return sqlBuilder;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000735F8 File Offset: 0x000717F8
		private static ISqlFragment HandleCanonicalFunctionCurrentDateTime(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			string a;
			if ((a = e.Function.Name.ToUpperInvariant()) != null)
			{
				if (!(a == "CURRENTDATETIME"))
				{
					if (!(a == "CURRENTUTCDATETIME"))
					{
						if (!(a == "CURRENTDATETIMEOFFSET"))
						{
							if (!(a == "CREATETIME"))
							{
								if (!(a == "CREATEDATETIME"))
								{
									if (a == "CREATEDATETIMEOFFSET")
									{
										sqlBuilder.Append("TO_TIMESTAMP_TZ(");
										sqlBuilder.Append("'");
										sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
										sqlBuilder.Append("-");
										sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
										sqlBuilder.Append("-");
										sqlBuilder.Append(e.Arguments[2].Accept<ISqlFragment>(sqlgen));
										sqlBuilder.Append(" ");
										sqlBuilder.Append(e.Arguments[3].Accept<ISqlFragment>(sqlgen));
										sqlBuilder.Append(":");
										sqlBuilder.Append(e.Arguments[4].Accept<ISqlFragment>(sqlgen));
										sqlBuilder.Append(":");
										sqlBuilder.Append(string.Format("{0:00.000000000}", ((DbConstantExpression)e.Arguments[5]).Value));
										sqlBuilder.Append(" ");
										sqlBuilder.Append(e.Arguments[6].Accept<ISqlFragment>(sqlgen));
										sqlBuilder.Append("'");
										sqlBuilder.Append(", 'yyyy-mm-dd HH24:MI:SS.FF TZH:TZM')");
									}
								}
								else
								{
									sqlBuilder.Append("TO_TIMESTAMP(");
									sqlBuilder.Append("'");
									sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
									sqlBuilder.Append("-");
									sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
									sqlBuilder.Append("-");
									sqlBuilder.Append(e.Arguments[2].Accept<ISqlFragment>(sqlgen));
									sqlBuilder.Append(" ");
									sqlBuilder.Append(e.Arguments[3].Accept<ISqlFragment>(sqlgen));
									sqlBuilder.Append(":");
									sqlBuilder.Append(e.Arguments[4].Accept<ISqlFragment>(sqlgen));
									sqlBuilder.Append(":");
									sqlBuilder.Append(((DbConstantExpression)e.Arguments[5]).Value.ToString());
									sqlBuilder.Append("'");
									sqlBuilder.Append(", 'YYYY-MM-DD HH24:MI:SS.FF')");
								}
							}
						}
						else
						{
							sqlBuilder.Append("SYSTIMESTAMP");
						}
					}
					else
					{
						sqlBuilder.Append("SYS_EXTRACT_UTC(LOCALTIMESTAMP)");
					}
				}
				else
				{
					sqlBuilder.Append("LOCALTIMESTAMP");
				}
			}
			return sqlBuilder;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000738D4 File Offset: 0x00071AD4
		private static ISqlFragment HandleCanonicalFunctionIndexOf(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return sqlgen.HandleFunctionDefaultGivenName(e, "INSTR");
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x000738E4 File Offset: 0x00071AE4
		private static ISqlFragment HandleCanonicalFunctionIndexOf2(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			string a;
			if ((a = e.Function.Name.ToUpperInvariant()) != null)
			{
				if (!(a == "INDEXOF"))
				{
					if (!(a == "CONTAINS"))
					{
						if (!(a == "STARTSWITH"))
						{
							if (a == "ENDSWITH")
							{
								sqlBuilder.Append(" CASE WHEN NVL(INSTR(");
								sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
								sqlBuilder.Append(", ");
								sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
								sqlBuilder.Append(", LENGTH(");
								sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
								sqlBuilder.Append(") - LENGTH(");
								sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
								sqlBuilder.Append(") + 1, 1 ), 0) > 0 THEN 1 ELSE 0 END ");
							}
						}
						else
						{
							sqlBuilder.Append(" CASE WHEN ");
							sqlBuilder.Append(" NVL(INSTR(");
							sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
							sqlBuilder.Append(", ");
							sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
							sqlBuilder.Append("), 0) ");
							sqlBuilder.Append(" = 1 THEN 1 ELSE 0 END ");
						}
					}
					else
					{
						sqlBuilder.Append(" CASE WHEN ");
						sqlBuilder.Append(" NVL(INSTR(");
						sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
						sqlBuilder.Append(", ");
						sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
						sqlBuilder.Append("), 0) ");
						sqlBuilder.Append(" != 0 THEN 1 ELSE 0 END ");
					}
				}
				else
				{
					sqlBuilder.Append(" NVL(INSTR(");
					sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append(", ");
					sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
					sqlBuilder.Append("), 0) ");
				}
			}
			return sqlBuilder;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00073B14 File Offset: 0x00071D14
		private static ISqlFragment HandleCanonicalFunctionNewGuid(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return sqlgen.HandleFunctionDefaultGivenName(e, "SYS_GUID");
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00073B24 File Offset: 0x00071D24
		private static ISqlFragment HandleCanonicalFunctionLength(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("LENGTH(");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append(")");
			return sqlBuilder;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00073B68 File Offset: 0x00071D68
		private static ISqlFragment HandleCanonicalFunctionRound(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("ROUND(");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			if (e.Arguments.Count == 1)
			{
				sqlBuilder.Append(", 0)");
			}
			else
			{
				sqlBuilder.Append(", ");
				sqlBuilder.Append(e.Arguments[1].Accept<ISqlFragment>(sqlgen));
				sqlBuilder.Append(")");
			}
			return sqlBuilder;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00073BE8 File Offset: 0x00071DE8
		private static ISqlFragment HandleCanonicalFunctionTrim(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.Append("LTRIM(RTRIM(");
			sqlBuilder.Append(e.Arguments[0].Accept<ISqlFragment>(sqlgen));
			sqlBuilder.Append("))");
			return sqlBuilder;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00073C2C File Offset: 0x00071E2C
		private static ISqlFragment HandleCanonicalFunctionToLower(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return sqlgen.HandleFunctionDefaultGivenName(e, "LOWER");
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00073C3C File Offset: 0x00071E3C
		private static ISqlFragment HandleCanonicalFunctionToUpper(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return sqlgen.HandleFunctionDefaultGivenName(e, "UPPER");
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00073C4C File Offset: 0x00071E4C
		private static ISqlFragment HandleCanonicalFunctionCeiling(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return sqlgen.HandleFunctionDefaultGivenName(e, "CEIL");
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00073C5C File Offset: 0x00071E5C
		private static ISqlFragment HandleCanonicalFunctionTruncate(SqlGenerator sqlgen, DbFunctionExpression e)
		{
			return sqlgen.HandleFunctionDefaultGivenName(e, "TRUNC");
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00073C6C File Offset: 0x00071E6C
		private static void WriteFunctionName(SqlBuilder result, EdmFunction function)
		{
			string metadataProperty = MetadataHelpers.GetMetadataProperty<string>(function, "StoreFunctionNameAttribute");
			string text;
			if (metadataProperty != null)
			{
				text = metadataProperty;
			}
			else
			{
				text = function.Name;
			}
			if (MetadataHelpers.IsCanonicalFunction(function))
			{
				if (text.ToUpperInvariant() == "STDEV")
				{
					result.Append("STDDEV");
					return;
				}
				if (text.ToUpperInvariant() == "STDEVP")
				{
					result.Append("STDDEV_POP");
					return;
				}
				if (text.ToUpperInvariant() == "VAR")
				{
					result.Append("VARIANCE");
					return;
				}
				if (text.ToUpperInvariant() == "VARP")
				{
					result.Append("VAR_POP");
					return;
				}
				if (text.ToUpperInvariant() == "BIGCOUNT")
				{
					result.Append("COUNT");
					return;
				}
				result.Append(text.ToUpperInvariant());
				return;
			}
			else
			{
				if (SqlGenerator.IsBuiltInStoreFunction(function))
				{
					result.Append(text);
					return;
				}
				string metadataProperty2 = MetadataHelpers.GetMetadataProperty<string>(function, "Schema");
				if (string.IsNullOrEmpty(metadataProperty2))
				{
					result.Append(SqlGenerator.QuoteIdentifier(function.NamespaceName));
				}
				else
				{
					result.Append(SqlGenerator.QuoteIdentifier(metadataProperty2));
				}
				result.Append(".");
				result.Append(SqlGenerator.QuoteIdentifier_storeFunctionName(text));
				return;
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00073D9C File Offset: 0x00071F9C
		private void AddColumns(SqlSelectStatement selectStatement, Symbol symbol, List<Symbol> columnList, Dictionary<string, Symbol> columnDictionary, ref string separator)
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
							if (symbol2.Type != null && !MetadataHelpers.IsPrimitiveType(symbol2.Type.EdmType))
							{
								this.AddColumns(selectStatement, symbol2, columnList, columnDictionary, ref separator);
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
						selectStatement.Select.Append(separator);
						selectStatement.Select.Append(symbol);
						selectStatement.Select.Append(".");
						selectStatement.Select.Append(symbol3);
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
						separator = ", ";
					}
					return;
				}
			}
			if (symbol.OutputColumnsRenamed)
			{
				selectStatement.OutputColumnsRenamed = true;
				selectStatement.OutputColumns = new Dictionary<string, Symbol>();
			}
			if (symbol.Type == null || MetadataHelpers.IsPrimitiveType(symbol.Type.EdmType))
			{
				this.AddColumn(selectStatement, symbol, columnList, columnDictionary, ref separator, "X");
				return;
			}
			foreach (EdmProperty edmProperty in MetadataHelpers.GetProperties(symbol.Type))
			{
				this.AddColumn(selectStatement, symbol, columnList, columnDictionary, ref separator, edmProperty.Name);
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00073F84 File Offset: 0x00072184
		private void AddColumn(SqlSelectStatement selectStatement, Symbol symbol, List<Symbol> columnList, Dictionary<string, Symbol> columnDictionary, ref string separator, string columnName)
		{
			this.allColumnNames[columnName] = 0;
			Symbol symbol2;
			if (!symbol.Columns.TryGetValue(columnName, out symbol2))
			{
				symbol2 = new Symbol(columnName, null);
				symbol.Columns.Add(columnName, symbol2);
			}
			selectStatement.Select.Append(separator);
			selectStatement.Select.Append(symbol);
			selectStatement.Select.Append(".");
			if (symbol.OutputColumnsRenamed)
			{
				selectStatement.Select.Append(symbol2);
				selectStatement.OutputColumns.Add(symbol2.Name, symbol2);
			}
			else
			{
				selectStatement.Select.Append(SqlGenerator.QuoteIdentifier(columnName));
			}
			selectStatement.Select.Append(" AS ");
			selectStatement.Select.Append(symbol2);
			if (columnDictionary.ContainsKey(columnName))
			{
				columnDictionary[columnName].NeedsRenaming = true;
				symbol2.NeedsRenaming = true;
			}
			else
			{
				columnDictionary[columnName] = symbol.Columns[columnName];
			}
			columnList.Add(symbol2);
			separator = ", ";
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00074090 File Offset: 0x00072290
		private List<Symbol> AddDefaultColumns(SqlSelectStatement selectStatement)
		{
			List<Symbol> list = new List<Symbol>();
			Dictionary<string, Symbol> columnDictionary = new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase);
			string text = "";
			if (!selectStatement.Select.IsEmpty)
			{
				text = ", ";
			}
			foreach (Symbol symbol in selectStatement.FromExtents)
			{
				this.AddColumns(selectStatement, symbol, list, columnDictionary, ref text);
			}
			return list;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00074114 File Offset: 0x00072314
		private void AddFromSymbol(SqlSelectStatement selectStatement, string inputVarName, Symbol fromSymbol)
		{
			this.AddFromSymbol(selectStatement, inputVarName, fromSymbol, true);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00074120 File Offset: 0x00072320
		private void AddFromSymbol(SqlSelectStatement selectStatement, string inputVarName, Symbol fromSymbol, bool addToSymbolTable)
		{
			if (selectStatement.FromExtents.Count == 0 || fromSymbol != selectStatement.FromExtents[0])
			{
				selectStatement.FromExtents.Add(fromSymbol);
				selectStatement.From.Append(" ");
				selectStatement.From.Append(fromSymbol);
				this.allExtentNames[fromSymbol.Name] = 0;
			}
			if (addToSymbolTable)
			{
				this.symbolTable.Add(inputVarName, fromSymbol);
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00074194 File Offset: 0x00072394
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

		// Token: 0x06000A1D RID: 2589 RVA: 0x00074238 File Offset: 0x00072438
		private SqlSelectStatement CreateNewSelectStatement(SqlSelectStatement oldStatement, string inputVarName, TypeUsage inputVarType, out Symbol fromSymbol)
		{
			return this.CreateNewSelectStatement(oldStatement, inputVarName, inputVarType, true, out fromSymbol);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00074248 File Offset: 0x00072448
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
				if (oldStatement.OutputColumnsRenamed)
				{
					fromSymbol = new Symbol(inputVarName, inputVarType, oldStatement.OutputColumns);
				}
				else
				{
					fromSymbol = new Symbol(inputVarName, inputVarType);
				}
			}
			SqlSelectStatement sqlSelectStatement = new SqlSelectStatement();
			sqlSelectStatement.From.Append("( ");
			sqlSelectStatement.From.Append(oldStatement);
			sqlSelectStatement.From.AppendLine();
			sqlSelectStatement.From.Append(") ");
			return sqlSelectStatement;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0007431C File Offset: 0x0007251C
		private static string EscapeSingleQuote(string s, bool isUnicode)
		{
			return (isUnicode ? "N'" : "'") + s.Replace("'", "''") + "'";
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00074348 File Offset: 0x00072548
		internal string GetSqlPrimitiveType(TypeUsage type)
		{
			return SqlGenerator.GetSqlPrimitiveType(this._providerManifest, this._sqlVersion, type);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0007435C File Offset: 0x0007255C
		internal static string GetSqlPrimitiveType(DbProviderManifest providerManifest, EFOracleVersion sqlVersion, TypeUsage type)
		{
			TypeUsage storeType = providerManifest.GetStoreType(type);
			string text = storeType.EdmType.Name;
			int num = 0;
			byte b = 0;
			byte b2 = 0;
			switch (((PrimitiveType)storeType.EdmType).PrimitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				if (!MetadataHelpers.IsFacetValueConstant(storeType, "MaxLength"))
				{
					MetadataHelpers.TryGetMaxLength(storeType, out num);
					text = text + "(" + num.ToString(CultureInfo.InvariantCulture) + ")";
				}
				break;
			case PrimitiveTypeKind.Boolean:
				text = "number(1,0)";
				break;
			case PrimitiveTypeKind.DateTime:
			case PrimitiveTypeKind.Time:
			{
				byte b3;
				if (MetadataHelpers.TryGetByteFacetValue(type, "Precision", out b3))
				{
					if (b3 > 9)
					{
						text = "timestamp with local time zone";
					}
					else
					{
						text = "timestamp";
					}
				}
				else
				{
					text = "date";
				}
				break;
			}
			case PrimitiveTypeKind.Decimal:
				if (!MetadataHelpers.IsFacetValueConstant(storeType, "Precision"))
				{
					MetadataHelpers.TryGetPrecision(storeType, out b);
					MetadataHelpers.TryGetScale(storeType, out b2);
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
			case PrimitiveTypeKind.Double:
				if (sqlVersion < EFOracleVersion.Oracle10gR1)
				{
					text = "number";
				}
				else
				{
					text = "binary_double";
				}
				break;
			case PrimitiveTypeKind.Guid:
				text = "raw(16)";
				break;
			case PrimitiveTypeKind.Single:
				if (sqlVersion < EFOracleVersion.Oracle10gR1)
				{
					text = "number";
				}
				else
				{
					text = "binary_float";
				}
				break;
			case PrimitiveTypeKind.String:
				if (!MetadataHelpers.IsFacetValueConstant(storeType, "MaxLength"))
				{
					MetadataHelpers.TryGetMaxLength(storeType, out num);
					text = text + "(" + num.ToString(CultureInfo.InvariantCulture) + ")";
				}
				break;
			case PrimitiveTypeKind.DateTimeOffset:
				text = "timestamp with time zone";
				break;
			}
			return text;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0007453C File Offset: 0x0007273C
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

		// Token: 0x06000A23 RID: 2595 RVA: 0x0007457C File Offset: 0x0007277C
		private static bool IsApplyExpression(DbExpression e)
		{
			return DbExpressionKind.CrossApply == e.ExpressionKind || DbExpressionKind.OuterApply == e.ExpressionKind;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00074594 File Offset: 0x00072794
		private static bool IsJoinExpression(DbExpression e)
		{
			return DbExpressionKind.CrossJoin == e.ExpressionKind || DbExpressionKind.FullOuterJoin == e.ExpressionKind || DbExpressionKind.InnerJoin == e.ExpressionKind || DbExpressionKind.LeftOuterJoin == e.ExpressionKind;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x000745C0 File Offset: 0x000727C0
		private static bool IsComplexExpression(DbExpression e)
		{
			DbExpressionKind expressionKind = e.ExpressionKind;
			return expressionKind != DbExpressionKind.Constant && expressionKind != DbExpressionKind.ParameterReference && expressionKind != DbExpressionKind.Property;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000745E8 File Offset: 0x000727E8
		private static bool IsCompatible(SqlSelectStatement result, DbExpressionKind expressionKind)
		{
			if (expressionKind <= DbExpressionKind.GroupBy)
			{
				switch (expressionKind)
				{
				case DbExpressionKind.Distinct:
					return result.Top == null && result.OrderBy.IsEmpty;
				case DbExpressionKind.Divide:
					goto IL_163;
				case DbExpressionKind.Element:
					break;
				default:
					if (expressionKind == DbExpressionKind.Filter)
					{
						return result.Select.IsEmpty && result.Where.IsEmpty && result.GroupBy.IsEmpty && result.Top == null;
					}
					if (expressionKind != DbExpressionKind.GroupBy)
					{
						goto IL_163;
					}
					return result.Select.IsEmpty && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty && result.Top == null;
				}
			}
			else if (expressionKind != DbExpressionKind.Limit)
			{
				if (expressionKind == DbExpressionKind.Project)
				{
					return result.Select.IsEmpty && result.GroupBy.IsEmpty && !result.IsDistinct;
				}
				switch (expressionKind)
				{
				case DbExpressionKind.Skip:
					return result.Select.IsEmpty && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty && !result.IsDistinct;
				case DbExpressionKind.Sort:
					return result.Select.IsEmpty && result.GroupBy.IsEmpty && result.OrderBy.IsEmpty && !result.IsDistinct;
				default:
					goto IL_163;
				}
			}
			return result.Top == null;
			IL_163:
			throw new InvalidOperationException(string.Empty);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00074764 File Offset: 0x00072964
		internal static string QuoteIdentifier(string name)
		{
			return "\"" + name.Replace("\"", "\"\"") + "\"";
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00074788 File Offset: 0x00072988
		internal static string QuoteIdentifier_storeFunctionName(string name)
		{
			if (!name.Contains("."))
			{
				return "\"" + name + "\"";
			}
			int num = 0;
			int num2;
			for (num2 = name.IndexOf("."); num2 != -1; num2 = name.IndexOf(".", num2 + 1))
			{
				num++;
			}
			if (num == 1)
			{
				return "\"" + name.Replace(".", "\".\"") + "\"";
			}
			num2 = name.LastIndexOf(".");
			string text = name.Substring(0, num2);
			return string.Concat(new string[]
			{
				"\"",
				text,
				"\".\"",
				name.Substring(num2 + 1),
				"\""
			});
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0007484C File Offset: 0x00072A4C
		private SqlSelectStatement VisitExpressionEnsureSqlStatement(DbExpression e)
		{
			return this.VisitExpressionEnsureSqlStatement(e, true);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00074858 File Offset: 0x00072A58
		private SqlSelectStatement VisitExpressionEnsureSqlStatement(DbExpression e, bool addDefaultColumns)
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
			goto IL_CB;
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
			TypeUsage inputVarType = MetadataHelpers.GetElementTypeUsage(e.ResultType);
			goto IL_AB;
			IL_9A:
			inputVarType = MetadataHelpers.GetEdmType<CollectionType>(e.ResultType).TypeUsage;
			IL_AB:
			Symbol fromSymbol;
			sqlSelectStatement = this.VisitInputExpression(e, inputVarName, inputVarType, out fromSymbol);
			this.AddFromSymbol(sqlSelectStatement, inputVarName, fromSymbol);
			this.symbolTable.ExitScope();
			IL_CB:
			if (addDefaultColumns && sqlSelectStatement.Select.IsEmpty)
			{
				this.AddDefaultColumns(sqlSelectStatement);
			}
			return sqlSelectStatement;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0007494C File Offset: 0x00072B4C
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

		// Token: 0x06000A2C RID: 2604 RVA: 0x00074A0C File Offset: 0x00072C0C
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

		// Token: 0x06000A2D RID: 2605 RVA: 0x00074A5C File Offset: 0x00072C5C
		private static bool IsBuiltInStoreFunction(EdmFunction function)
		{
			bool metadataProperty = MetadataHelpers.GetMetadataProperty<bool>(function, "BuiltInAttribute");
			return metadataProperty && !MetadataHelpers.IsCanonicalFunction(function);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00074A84 File Offset: 0x00072C84
		private static string ByteArrayToBinaryString(byte[] binaryArray)
		{
			StringBuilder stringBuilder = new StringBuilder(binaryArray.Length * 2);
			for (int i = 0; i < binaryArray.Length; i++)
			{
				stringBuilder.Append(SqlGenerator.hexDigits[(binaryArray[i] & 240) >> 4]).Append(SqlGenerator.hexDigits[(int)(binaryArray[i] & 15)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00074ADC File Offset: 0x00072CDC
		private static bool GroupByAggregatesNeedInnerQuery(IList<DbAggregate> aggregates)
		{
			foreach (DbAggregate dbAggregate in aggregates)
			{
				if (SqlGenerator.GroupByAggregateNeedsInnerQuery(dbAggregate.Arguments[0]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00074B38 File Offset: 0x00072D38
		private static bool GroupByAggregateNeedsInnerQuery(DbExpression expression)
		{
			if (expression.ExpressionKind == DbExpressionKind.Constant)
			{
				return false;
			}
			if (expression.ExpressionKind == DbExpressionKind.Cast)
			{
				DbCastExpression dbCastExpression = (DbCastExpression)expression;
				return SqlGenerator.GroupByAggregateNeedsInnerQuery(dbCastExpression.Argument);
			}
			if (expression.ExpressionKind == DbExpressionKind.Property)
			{
				DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)expression;
				return SqlGenerator.GroupByAggregateNeedsInnerQuery(dbPropertyExpression.Instance);
			}
			return expression.ExpressionKind != DbExpressionKind.VariableReference;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00074B98 File Offset: 0x00072D98
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

		// Token: 0x06000A32 RID: 2610 RVA: 0x00074BEC File Offset: 0x00072DEC
		private static bool GroupByKeyNeedsInnerQuery(DbExpression expression, string inputVarRefName)
		{
			if (expression.ExpressionKind == DbExpressionKind.Cast)
			{
				DbCastExpression dbCastExpression = (DbCastExpression)expression;
				return SqlGenerator.GroupByKeyNeedsInnerQuery(dbCastExpression.Argument, inputVarRefName);
			}
			if (expression.ExpressionKind == DbExpressionKind.Property)
			{
				DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)expression;
				return SqlGenerator.GroupByKeyNeedsInnerQuery(dbPropertyExpression.Instance, inputVarRefName);
			}
			if (expression.ExpressionKind == DbExpressionKind.VariableReference)
			{
				DbVariableReferenceExpression dbVariableReferenceExpression = expression as DbVariableReferenceExpression;
				return !dbVariableReferenceExpression.VariableName.Equals(inputVarRefName);
			}
			return true;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00074C58 File Offset: 0x00072E58
		private void Assert10gOrNewer(PrimitiveTypeKind primitiveTypeKind)
		{
			SqlGenerator.Assert10gOrNewer(this._sqlVersion, primitiveTypeKind);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00074C68 File Offset: 0x00072E68
		private static void Assert10gOrNewer(EFOracleVersion _sqlVersion, PrimitiveTypeKind primitiveTypeKind)
		{
			if (_sqlVersion < EFOracleVersion.Oracle10gR1)
			{
				throw new NotSupportedException(EFProviderSettings.Instance.GetErrorMessage(-1202, new string[]
				{
					primitiveTypeKind.ToString()
				}));
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00074CA8 File Offset: 0x00072EA8
		private void Assert10gOrNewer(DbFunctionExpression e)
		{
			if (this.IsPre10g)
			{
				throw new NotSupportedException(EFProviderSettings.Instance.GetErrorMessage(-1202, new string[]
				{
					e.Function.Name
				}));
			}
		}

		// Token: 0x04000C61 RID: 3169
		private const byte defaultDecimalPrecision = 38;

		// Token: 0x04000C62 RID: 3170
		private Stack<SqlSelectStatement> selectStatementStack;

		// Token: 0x04000C63 RID: 3171
		private Stack<bool> isParentAJoinStack;

		// Token: 0x04000C64 RID: 3172
		private Dictionary<string, int> allExtentNames;

		// Token: 0x04000C65 RID: 3173
		private Dictionary<string, int> allColumnNames;

		// Token: 0x04000C66 RID: 3174
		private readonly SymbolTable symbolTable = new SymbolTable();

		// Token: 0x04000C67 RID: 3175
		private bool isVarRefSingle;

		// Token: 0x04000C68 RID: 3176
		private readonly Dictionary<string, bool> ListOfParamsForNonUnicode = new Dictionary<string, bool>();

		// Token: 0x04000C69 RID: 3177
		private bool _bNeedToMakeUnicodeFalse;

		// Token: 0x04000C6A RID: 3178
		private bool _bIgnoreMakingUnicodeFalse;

		// Token: 0x04000C6B RID: 3179
		private static readonly Dictionary<string, SqlGenerator.FunctionHandler> _canonicalFunctionHandlers = SqlGenerator.InitializeCanonicalFunctionHandlers();

		// Token: 0x04000C6C RID: 3180
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

		// Token: 0x04000C6D RID: 3181
		private EFOracleVersion _sqlVersion;

		// Token: 0x04000C6E RID: 3182
		private EFOracleProviderManifest _providerManifest;

		// Token: 0x020000F3 RID: 243
		// (Invoke) Token: 0x06000A3A RID: 2618
		private delegate ISqlFragment FunctionHandler(SqlGenerator sqlgen, DbFunctionExpression functionExpr);
	}
}
