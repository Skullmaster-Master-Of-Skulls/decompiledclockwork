using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.EntitySql.AST;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000247 RID: 583
	internal static class CqlQuery
	{
		// Token: 0x06001499 RID: 5273 RVA: 0x00062384 File Offset: 0x00060584
		internal static ParseResult Compile(string commandText, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters)
		{
			return CqlQuery.CompileCommon<ParseResult>(commandText, parserOptions, (Node astCommand, ParserOptions validatedParserOptions) => CqlQuery.AnalyzeCommandSemantics(astCommand, perspective, validatedParserOptions, parameters));
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x000623EC File Offset: 0x000605EC
		internal static DbLambda CompileQueryCommandLambda(string queryCommandText, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters, IEnumerable<DbVariableReferenceExpression> variables)
		{
			return CqlQuery.CompileCommon<DbLambda>(queryCommandText, parserOptions, (Node astCommand, ParserOptions validatedParserOptions) => CqlQuery.AnalyzeQueryExpressionSemantics(astCommand, perspective, validatedParserOptions, parameters, variables));
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00062428 File Offset: 0x00060628
		private static Node Parse(string commandText, ParserOptions parserOptions)
		{
			Check.NotEmpty(commandText, "commandText");
			CqlParser cqlParser = new CqlParser(parserOptions, true);
			Node node = cqlParser.Parse(commandText);
			if (node == null)
			{
				throw EntitySqlException.Create(commandText, Strings.InvalidEmptyQuery, 0, null, false, null);
			}
			return node;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x00062465 File Offset: 0x00060665
		private static TResult CompileCommon<TResult>(string commandText, ParserOptions parserOptions, Func<Node, ParserOptions, TResult> compilationFunction) where TResult : class
		{
			parserOptions = (parserOptions ?? new ParserOptions());
			return compilationFunction(CqlQuery.Parse(commandText, parserOptions), parserOptions);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0006249C File Offset: 0x0006069C
		private static ParseResult AnalyzeCommandSemantics(Node astExpr, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters)
		{
			return CqlQuery.AnalyzeSemanticsCommon<ParseResult>(astExpr, perspective, parserOptions, parameters, null, (SemanticAnalyzer analyzer, Node astExpression) => analyzer.AnalyzeCommand(astExpression));
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x000624EA File Offset: 0x000606EA
		private static DbLambda AnalyzeQueryExpressionSemantics(Node astQueryCommand, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters, IEnumerable<DbVariableReferenceExpression> variables)
		{
			return CqlQuery.AnalyzeSemanticsCommon<DbLambda>(astQueryCommand, perspective, parserOptions, parameters, variables, (SemanticAnalyzer analyzer, Node astExpr) => analyzer.AnalyzeQueryCommand(astExpr));
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00062514 File Offset: 0x00060714
		private static TResult AnalyzeSemanticsCommon<TResult>(Node astExpr, Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters, IEnumerable<DbVariableReferenceExpression> variables, Func<SemanticAnalyzer, Node, TResult> analysisFunction) where TResult : class
		{
			TResult result = default(TResult);
			try
			{
				SemanticAnalyzer arg = new SemanticAnalyzer(SemanticResolver.Create(perspective, parserOptions, parameters, variables));
				result = analysisFunction(arg, astExpr);
			}
			catch (MetadataException innerException)
			{
				string message = Strings.GeneralExceptionAsQueryInnerException("Metadata");
				throw new EntitySqlException(message, innerException);
			}
			catch (MappingException innerException2)
			{
				string message2 = Strings.GeneralExceptionAsQueryInnerException("Mapping");
				throw new EntitySqlException(message2, innerException2);
			}
			return result;
		}
	}
}
