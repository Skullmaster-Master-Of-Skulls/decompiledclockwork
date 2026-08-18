using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.EntitySql.AST;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000332 RID: 818
	internal static class CqlQuery
	{
		// Token: 0x06003096 RID: 12438 RVA: 0x000BC174 File Offset: 0x000BA374
		internal static ParseResult Compile(string commandText, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters)
		{
			return CqlQuery.CompileCommon<ParseResult>(commandText, perspective, parserOptions, (Node astCommand, ParserOptions validatedParserOptions) => CqlQuery.AnalyzeCommandSemantics(astCommand, perspective, validatedParserOptions, parameters));
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000BC1B0 File Offset: 0x000BA3B0
		internal static DbLambda CompileQueryCommandLambda(string queryCommandText, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters, IEnumerable<DbVariableReferenceExpression> variables)
		{
			return CqlQuery.CompileCommon<DbLambda>(queryCommandText, perspective, parserOptions, (Node astCommand, ParserOptions validatedParserOptions) => CqlQuery.AnalyzeQueryExpressionSemantics(astCommand, perspective, validatedParserOptions, parameters, variables));
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000BC1F4 File Offset: 0x000BA3F4
		private static Node Parse(string commandText, ParserOptions parserOptions)
		{
			CqlParser cqlParser = new CqlParser(parserOptions, true);
			Node node = cqlParser.Parse(commandText);
			if (node == null)
			{
				throw EntityUtil.EntitySqlError(commandText, Strings.InvalidEmptyQuery, 0);
			}
			return node;
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000BC224 File Offset: 0x000BA424
		private static TResult CompileCommon<TResult>(string commandText, Perspective perspective, ParserOptions parserOptions, Func<Node, ParserOptions, TResult> compilationFunction) where TResult : class
		{
			TResult tresult = default(TResult);
			EntityUtil.CheckArgumentNull<Perspective>(perspective, "commandText");
			EntityUtil.CheckArgumentNull<Perspective>(perspective, "perspective");
			parserOptions = (parserOptions ?? new ParserOptions());
			Node arg = CqlQuery.Parse(commandText, parserOptions);
			return compilationFunction(arg, parserOptions);
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x000BC270 File Offset: 0x000BA470
		private static ParseResult AnalyzeCommandSemantics(Node astExpr, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters)
		{
			return CqlQuery.AnalyzeSemanticsCommon<ParseResult>(astExpr, perspective, parserOptions, parameters, null, (SemanticAnalyzer analyzer, Node astExpression) => analyzer.AnalyzeCommand(astExpression));
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x000BC2A8 File Offset: 0x000BA4A8
		private static DbLambda AnalyzeQueryExpressionSemantics(Node astQueryCommand, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters, IEnumerable<DbVariableReferenceExpression> variables)
		{
			return CqlQuery.AnalyzeSemanticsCommon<DbLambda>(astQueryCommand, perspective, parserOptions, parameters, variables, (SemanticAnalyzer analyzer, Node astExpr) => analyzer.AnalyzeQueryCommand(astExpr));
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x000BC2D4 File Offset: 0x000BA4D4
		private static TResult AnalyzeSemanticsCommon<TResult>(Node astExpr, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters, IEnumerable<DbVariableReferenceExpression> variables, Func<SemanticAnalyzer, Node, TResult> analysisFunction) where TResult : class
		{
			TResult result = default(TResult);
			try
			{
				EntityUtil.CheckArgumentNull<Node>(astExpr, "astExpr");
				EntityUtil.CheckArgumentNull<Perspective>(perspective, "perspective");
				SemanticAnalyzer arg = new SemanticAnalyzer(SemanticResolver.Create(perspective, parserOptions, parameters, variables));
				result = analysisFunction(arg, astExpr);
			}
			catch (MetadataException innerException)
			{
				throw EntityUtil.EntitySqlError(Strings.GeneralExceptionAsQueryInnerException("Metadata"), innerException);
			}
			catch (MappingException innerException2)
			{
				throw EntityUtil.EntitySqlError(Strings.GeneralExceptionAsQueryInnerException("Mapping"), innerException2);
			}
			return result;
		}
	}
}
