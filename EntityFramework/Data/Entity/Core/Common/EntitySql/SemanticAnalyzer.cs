using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql.AST;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200026A RID: 618
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal sealed class SemanticAnalyzer
	{
		// Token: 0x0600151A RID: 5402 RVA: 0x000633F9 File Offset: 0x000615F9
		internal SemanticAnalyzer(SemanticResolver sr)
		{
			this._sr = sr;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00063408 File Offset: 0x00061608
		internal ParseResult AnalyzeCommand(Node astExpr)
		{
			Command command = SemanticAnalyzer.ValidateQueryCommandAst(astExpr);
			SemanticAnalyzer.ConvertAndRegisterNamespaceImports(command.NamespaceImportList, command.ErrCtx, this._sr);
			return SemanticAnalyzer.ConvertStatement(command.Statement, this._sr);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00063448 File Offset: 0x00061648
		internal DbLambda AnalyzeQueryCommand(Node astExpr)
		{
			Command command = SemanticAnalyzer.ValidateQueryCommandAst(astExpr);
			SemanticAnalyzer.ConvertAndRegisterNamespaceImports(command.NamespaceImportList, command.ErrCtx, this._sr);
			List<FunctionDefinition> list;
			DbExpression body = SemanticAnalyzer.ConvertQueryStatementToDbExpression(command.Statement, this._sr, out list);
			return DbExpressionBuilder.Lambda(body, this._sr.Variables.Values);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x000634A0 File Offset: 0x000616A0
		private static Command ValidateQueryCommandAst(Node astExpr)
		{
			Command command = astExpr as Command;
			if (command == null)
			{
				throw new ArgumentException(Strings.UnknownAstCommandExpression);
			}
			if (!(command.Statement is QueryStatement))
			{
				throw new ArgumentException(Strings.UnknownAstExpressionType);
			}
			return command;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x000634DC File Offset: 0x000616DC
		private static void ConvertAndRegisterNamespaceImports(NodeList<NamespaceImport> nsImportList, ErrorContext cmdErrCtx, SemanticResolver sr)
		{
			List<Tuple<string, MetadataNamespace, ErrorContext>> list = new List<Tuple<string, MetadataNamespace, ErrorContext>>();
			List<Tuple<MetadataNamespace, ErrorContext>> list2 = new List<Tuple<MetadataNamespace, ErrorContext>>();
			if (nsImportList != null)
			{
				foreach (NamespaceImport namespaceImport in ((IEnumerable<NamespaceImport>)nsImportList))
				{
					string[] array = null;
					Identifier identifier = namespaceImport.NamespaceName as Identifier;
					if (identifier != null)
					{
						array = new string[]
						{
							identifier.Name
						};
					}
					DotExpr dotExpr = namespaceImport.NamespaceName as DotExpr;
					if (dotExpr != null)
					{
						dotExpr.IsMultipartIdentifier(out array);
					}
					if (array == null)
					{
						ErrorContext errCtx = namespaceImport.NamespaceName.ErrCtx;
						string invalidMetadataMemberName = Strings.InvalidMetadataMemberName;
						throw EntitySqlException.Create(errCtx, invalidMetadataMemberName, null);
					}
					string text = (namespaceImport.Alias != null) ? namespaceImport.Alias.Name : null;
					MetadataMember metadataMember = sr.ResolveMetadataMemberName(array, namespaceImport.NamespaceName.ErrCtx);
					if (metadataMember.MetadataMemberClass != MetadataMemberClass.Namespace)
					{
						ErrorContext errCtx2 = namespaceImport.NamespaceName.ErrCtx;
						string errorMessage = Strings.InvalidMetadataMemberClassResolution(metadataMember.Name, metadataMember.MetadataMemberClassName, MetadataNamespace.NamespaceClassName);
						throw EntitySqlException.Create(errCtx2, errorMessage, null);
					}
					MetadataNamespace metadataNamespace = (MetadataNamespace)metadataMember;
					if (text != null)
					{
						list.Add(Tuple.Create<string, MetadataNamespace, ErrorContext>(text, metadataNamespace, namespaceImport.ErrCtx));
					}
					else
					{
						list2.Add(Tuple.Create<MetadataNamespace, ErrorContext>(metadataNamespace, namespaceImport.ErrCtx));
					}
				}
			}
			sr.TypeResolver.AddNamespaceImport(new MetadataNamespace("Edm"), (nsImportList != null) ? nsImportList.ErrCtx : cmdErrCtx);
			foreach (Tuple<string, MetadataNamespace, ErrorContext> tuple in list)
			{
				sr.TypeResolver.AddAliasedNamespaceImport(tuple.Item1, tuple.Item2, tuple.Item3);
			}
			foreach (Tuple<MetadataNamespace, ErrorContext> tuple2 in list2)
			{
				sr.TypeResolver.AddNamespaceImport(tuple2.Item1, tuple2.Item2);
			}
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00063730 File Offset: 0x00061930
		private static ParseResult ConvertStatement(Statement astStatement, SemanticResolver sr)
		{
			if (astStatement is QueryStatement)
			{
				SemanticAnalyzer.StatementConverter statementConverter = new SemanticAnalyzer.StatementConverter(SemanticAnalyzer.ConvertQueryStatementToDbCommandTree);
				return statementConverter(astStatement, sr);
			}
			throw new ArgumentException(Strings.UnknownAstExpressionType);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0006376C File Offset: 0x0006196C
		private static ParseResult ConvertQueryStatementToDbCommandTree(Statement astStatement, SemanticResolver sr)
		{
			List<FunctionDefinition> functionDefs;
			DbExpression query = SemanticAnalyzer.ConvertQueryStatementToDbExpression(astStatement, sr, out functionDefs);
			return new ParseResult(DbQueryCommandTree.FromValidExpression(sr.TypeResolver.Perspective.MetadataWorkspace, sr.TypeResolver.Perspective.TargetDataspace, query, true), functionDefs);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x000637B0 File Offset: 0x000619B0
		private static DbExpression ConvertQueryStatementToDbExpression(Statement astStatement, SemanticResolver sr, out List<FunctionDefinition> functionDefs)
		{
			QueryStatement queryStatement = astStatement as QueryStatement;
			if (queryStatement == null)
			{
				throw new ArgumentException(Strings.UnknownAstExpressionType);
			}
			functionDefs = SemanticAnalyzer.ConvertInlineFunctionDefinitions(queryStatement.FunctionDefList, sr);
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(queryStatement.Expr, sr);
			if (dbExpression == null)
			{
				ErrorContext errCtx = queryStatement.Expr.ErrCtx;
				string resultingExpressionTypeCannotBeNull = Strings.ResultingExpressionTypeCannotBeNull;
				throw EntitySqlException.Create(errCtx, resultingExpressionTypeCannotBeNull, null);
			}
			if (dbExpression is DbScanExpression)
			{
				DbExpressionBinding dbExpressionBinding = dbExpression.BindAs(sr.GenerateInternalName("extent"));
				dbExpression = dbExpressionBinding.Project(dbExpressionBinding.Variable);
			}
			if (sr.ParserOptions.ParserCompilationMode == ParserOptions.CompilationMode.NormalMode)
			{
				SemanticAnalyzer.ValidateQueryResultType(dbExpression.ResultType, queryStatement.Expr.ErrCtx);
			}
			return dbExpression;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0006385C File Offset: 0x00061A5C
		private static void ValidateQueryResultType(TypeUsage resultType, ErrorContext errCtx)
		{
			if (Helper.IsCollectionType(resultType.EdmType))
			{
				SemanticAnalyzer.ValidateQueryResultType(((CollectionType)resultType.EdmType).TypeUsage, errCtx);
				return;
			}
			if (Helper.IsRowType(resultType.EdmType))
			{
				using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator = ((RowType)resultType.EdmType).Properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EdmProperty edmProperty = enumerator.Current;
						SemanticAnalyzer.ValidateQueryResultType(edmProperty.TypeUsage, errCtx);
					}
					return;
				}
			}
			if (Helper.IsAssociationType(resultType.EdmType))
			{
				string errorMessage = Strings.InvalidQueryResultType(resultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00063918 File Offset: 0x00061B18
		private static List<FunctionDefinition> ConvertInlineFunctionDefinitions(NodeList<FunctionDefinition> functionDefList, SemanticResolver sr)
		{
			List<FunctionDefinition> list = new List<FunctionDefinition>();
			if (functionDefList != null)
			{
				List<InlineFunctionInfo> list2 = new List<InlineFunctionInfo>();
				foreach (FunctionDefinition functionDefinition in ((IEnumerable<FunctionDefinition>)functionDefList))
				{
					string name = functionDefinition.Name;
					List<DbVariableReferenceExpression> parameters = SemanticAnalyzer.ConvertInlineFunctionParameterDefs(functionDefinition.Parameters, sr);
					InlineFunctionInfo inlineFunctionInfo = new SemanticAnalyzer.InlineFunctionInfoImpl(functionDefinition, parameters);
					list2.Add(inlineFunctionInfo);
					sr.TypeResolver.DeclareInlineFunction(name, inlineFunctionInfo);
				}
				foreach (InlineFunctionInfo inlineFunctionInfo2 in list2)
				{
					list.Add(new FunctionDefinition(inlineFunctionInfo2.FunctionDefAst.Name, inlineFunctionInfo2.GetLambda(sr), inlineFunctionInfo2.FunctionDefAst.StartPosition, inlineFunctionInfo2.FunctionDefAst.EndPosition));
				}
			}
			return list;
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x00063A50 File Offset: 0x00061C50
		private static List<DbVariableReferenceExpression> ConvertInlineFunctionParameterDefs(NodeList<PropDefinition> parameterDefs, SemanticResolver sr)
		{
			List<DbVariableReferenceExpression> list = new List<DbVariableReferenceExpression>();
			if (parameterDefs != null)
			{
				foreach (PropDefinition propDefinition in ((IEnumerable<PropDefinition>)parameterDefs))
				{
					string name = propDefinition.Name.Name;
					if (list.Exists((DbVariableReferenceExpression arg) => sr.NameComparer.Compare(arg.VariableName, name) == 0))
					{
						ErrorContext errCtx = propDefinition.ErrCtx;
						string errorMessage = Strings.MultipleDefinitionsOfParameter(name);
						throw EntitySqlException.Create(errCtx, errorMessage, null);
					}
					TypeUsage type = SemanticAnalyzer.ConvertTypeDefinition(propDefinition.Type, sr);
					DbVariableReferenceExpression item = new DbVariableReferenceExpression(type, name);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x00063B60 File Offset: 0x00061D60
		private static DbLambda ConvertInlineFunctionDefinition(InlineFunctionInfo functionInfo, SemanticResolver sr)
		{
			sr.EnterScope();
			functionInfo.Parameters.Each((DbVariableReferenceExpression p) => sr.CurrentScope.Add(p.VariableName, new FreeVariableScopeEntry(p)));
			DbExpression body = SemanticAnalyzer.ConvertValueExpression(functionInfo.FunctionDefAst.Body, sr);
			sr.LeaveScope();
			return DbExpressionBuilder.Lambda(body, functionInfo.Parameters);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00063BCC File Offset: 0x00061DCC
		private static ExpressionResolution Convert(Node astExpr, SemanticResolver sr)
		{
			SemanticAnalyzer.AstExprConverter astExprConverter = SemanticAnalyzer._astExprConverters[astExpr.GetType()];
			if (astExprConverter == null)
			{
				string unknownAstExpressionType = Strings.UnknownAstExpressionType;
				throw new EntitySqlException(unknownAstExpressionType);
			}
			return astExprConverter(astExpr, sr);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x00063C04 File Offset: 0x00061E04
		private static DbExpression ConvertValueExpression(Node astExpr, SemanticResolver sr)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(astExpr, sr);
			if (dbExpression == null)
			{
				ErrorContext errCtx = astExpr.ErrCtx;
				string expressionCannotBeNull = Strings.ExpressionCannotBeNull;
				throw EntitySqlException.Create(errCtx, expressionCannotBeNull, null);
			}
			return dbExpression;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00063C34 File Offset: 0x00061E34
		private static DbExpression ConvertValueExpressionAllowUntypedNulls(Node astExpr, SemanticResolver sr)
		{
			ExpressionResolution expressionResolution = SemanticAnalyzer.Convert(astExpr, sr);
			if (expressionResolution.ExpressionClass == ExpressionResolutionClass.Value)
			{
				return ((ValueExpression)expressionResolution).Value;
			}
			if (expressionResolution.ExpressionClass == ExpressionResolutionClass.MetadataMember)
			{
				MetadataMember metadataMember = (MetadataMember)expressionResolution;
				if (metadataMember.MetadataMemberClass == MetadataMemberClass.EnumMember)
				{
					MetadataEnumMember metadataEnumMember = (MetadataEnumMember)metadataMember;
					return metadataEnumMember.EnumType.Constant(metadataEnumMember.EnumMember.Value);
				}
			}
			string errorMessage = Strings.InvalidExpressionResolutionClass(expressionResolution.ExpressionClassName, ValueExpression.ValueClassName);
			Identifier identifier = astExpr as Identifier;
			if (identifier != null)
			{
				errorMessage = Strings.CouldNotResolveIdentifier(identifier.Name);
			}
			DotExpr dotExpr = astExpr as DotExpr;
			string[] names;
			if (dotExpr != null && dotExpr.IsMultipartIdentifier(out names))
			{
				errorMessage = Strings.CouldNotResolveIdentifier(TypeResolver.GetFullName(names));
			}
			ErrorContext errCtx = astExpr.ErrCtx;
			throw EntitySqlException.Create(errCtx, errorMessage, null);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00063CF4 File Offset: 0x00061EF4
		private static Pair<DbExpression, DbExpression> ConvertValueExpressionsWithUntypedNulls(Node leftAst, Node rightAst, ErrorContext errCtx, Func<string> formatMessage, SemanticResolver sr)
		{
			DbExpression dbExpression = (leftAst != null) ? SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(leftAst, sr) : null;
			DbExpression dbExpression2 = (rightAst != null) ? SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(rightAst, sr) : null;
			if (dbExpression == null)
			{
				if (dbExpression2 == null)
				{
					string errorMessage = formatMessage();
					throw EntitySqlException.Create(errCtx, errorMessage, null);
				}
				dbExpression = dbExpression2.ResultType.Null();
			}
			else if (dbExpression2 == null)
			{
				dbExpression2 = dbExpression.ResultType.Null();
			}
			return new Pair<DbExpression, DbExpression>(dbExpression, dbExpression2);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00063D5C File Offset: 0x00061F5C
		private static ExpressionResolution ConvertLiteral(Node expr, SemanticResolver sr)
		{
			Literal literal = (Literal)expr;
			if (literal.IsNullLiteral)
			{
				return new ValueExpression(null);
			}
			return new ValueExpression(SemanticAnalyzer.GetLiteralTypeUsage(literal).Constant(literal.Value));
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00063D98 File Offset: 0x00061F98
		private static TypeUsage GetLiteralTypeUsage(Literal literal)
		{
			PrimitiveType primitiveType = null;
			if (!ClrProviderManifest.Instance.TryGetPrimitiveType(literal.Type, out primitiveType))
			{
				ErrorContext errCtx = literal.ErrCtx;
				string errorMessage = Strings.LiteralTypeNotFoundInMetadata(literal.OriginalValue);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			return TypeHelpers.GetLiteralTypeUsage(primitiveType.PrimitiveTypeKind, literal.IsUnicodeString);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00063DEA File Offset: 0x00061FEA
		private static ExpressionResolution ConvertIdentifier(Node expr, SemanticResolver sr)
		{
			return SemanticAnalyzer.ConvertIdentifier((Identifier)expr, false, sr);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x00063DF9 File Offset: 0x00061FF9
		private static ExpressionResolution ConvertIdentifier(Identifier identifier, bool leftHandSideOfMemberAccess, SemanticResolver sr)
		{
			return sr.ResolveSimpleName(identifier.Name, leftHandSideOfMemberAccess, identifier.ErrCtx);
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00063E10 File Offset: 0x00062010
		private static ExpressionResolution ConvertDotExpr(Node expr, SemanticResolver sr)
		{
			DotExpr dotExpr = (DotExpr)expr;
			ValueExpression result;
			if (sr.TryResolveDotExprAsGroupKeyAlternativeName(dotExpr, out result))
			{
				return result;
			}
			Identifier identifier = dotExpr.Left as Identifier;
			ExpressionResolution expressionResolution;
			if (identifier != null)
			{
				expressionResolution = SemanticAnalyzer.ConvertIdentifier(identifier, true, sr);
			}
			else
			{
				expressionResolution = SemanticAnalyzer.Convert(dotExpr.Left, sr);
			}
			switch (expressionResolution.ExpressionClass)
			{
			case ExpressionResolutionClass.Value:
				return sr.ResolvePropertyAccess(((ValueExpression)expressionResolution).Value, dotExpr.Identifier.Name, dotExpr.Identifier.ErrCtx);
			case ExpressionResolutionClass.EntityContainer:
				return sr.ResolveEntityContainerMemberAccess(((EntityContainerExpression)expressionResolution).EntityContainer, dotExpr.Identifier.Name, dotExpr.Identifier.ErrCtx);
			case ExpressionResolutionClass.MetadataMember:
				return sr.ResolveMetadataMemberAccess((MetadataMember)expressionResolution, dotExpr.Identifier.Name, dotExpr.Identifier.ErrCtx);
			default:
			{
				ErrorContext errCtx = dotExpr.Left.ErrCtx;
				string errorMessage = Strings.UnknownExpressionResolutionClass(expressionResolution.ExpressionClass);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			}
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00063F14 File Offset: 0x00062114
		private static ExpressionResolution ConvertParenExpr(Node astExpr, SemanticResolver sr)
		{
			Node expr = ((ParenExpr)astExpr).Expr;
			DbExpression value = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(expr, sr);
			return new ValueExpression(value);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00063F3C File Offset: 0x0006213C
		private static ExpressionResolution ConvertGroupPartitionExpr(Node astExpr, SemanticResolver sr)
		{
			GroupPartitionExpr groupPartitionExpr = (GroupPartitionExpr)astExpr;
			DbExpression value = null;
			if (!SemanticAnalyzer.TryConvertAsResolvedGroupAggregate(groupPartitionExpr, sr, out value))
			{
				if (!sr.IsInAnyGroupScope())
				{
					ErrorContext errCtx = astExpr.ErrCtx;
					string groupPartitionOutOfContext = Strings.GroupPartitionOutOfContext;
					throw EntitySqlException.Create(errCtx, groupPartitionOutOfContext, null);
				}
				GroupPartitionInfo groupPartitionInfo;
				DbExpression dbExpression;
				using (sr.EnterGroupPartition(groupPartitionExpr, groupPartitionExpr.ErrCtx, out groupPartitionInfo))
				{
					dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(groupPartitionExpr.ArgExpr, sr);
				}
				if (dbExpression == null)
				{
					ErrorContext errCtx2 = groupPartitionExpr.ArgExpr.ErrCtx;
					string resultingExpressionTypeCannotBeNull = Strings.ResultingExpressionTypeCannotBeNull;
					throw EntitySqlException.Create(errCtx2, resultingExpressionTypeCannotBeNull, null);
				}
				DbExpression dbExpression2 = groupPartitionInfo.EvaluatingScopeRegion.GroupAggregateBinding.Project(dbExpression);
				if (groupPartitionExpr.DistinctKind == DistinctKind.Distinct)
				{
					SemanticAnalyzer.ValidateDistinctProjection(dbExpression2.ResultType, groupPartitionExpr.ArgExpr.ErrCtx, null);
					dbExpression2 = dbExpression2.Distinct();
				}
				groupPartitionInfo.AttachToAstNode(sr.GenerateInternalName("groupPartition"), dbExpression2);
				groupPartitionInfo.EvaluatingScopeRegion.GroupAggregateInfos.Add(groupPartitionInfo);
				value = groupPartitionInfo.AggregateStubExpression;
			}
			return new ValueExpression(value);
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00064054 File Offset: 0x00062254
		private static ExpressionResolution ConvertMethodExpr(Node expr, SemanticResolver sr)
		{
			return SemanticAnalyzer.ConvertMethodExpr((MethodExpr)expr, true, sr);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x00064064 File Offset: 0x00062264
		private static ExpressionResolution ConvertMethodExpr(MethodExpr methodExpr, bool includeInlineFunctions, SemanticResolver sr)
		{
			ExpressionResolution expressionResolution;
			using (sr.TypeResolver.EnterFunctionNameResolution(includeInlineFunctions))
			{
				Identifier identifier = methodExpr.Expr as Identifier;
				if (identifier != null)
				{
					expressionResolution = sr.ResolveSimpleFunctionName(identifier.Name, identifier.ErrCtx);
				}
				else
				{
					DotExpr leftExpr = methodExpr.Expr as DotExpr;
					using (SemanticAnalyzer.ConvertMethodExpr_TryEnterIgnoreEntityContainerNameResolution(leftExpr, sr))
					{
						using (SemanticAnalyzer.ConvertMethodExpr_TryEnterV1ViewGenBackwardCompatibilityResolution(leftExpr, sr))
						{
							expressionResolution = SemanticAnalyzer.Convert(methodExpr.Expr, sr);
						}
					}
				}
			}
			if (expressionResolution.ExpressionClass != ExpressionResolutionClass.MetadataMember)
			{
				ErrorContext errCtx = methodExpr.ErrCtx;
				string methodInvocationNotSupported = Strings.MethodInvocationNotSupported;
				throw EntitySqlException.Create(errCtx, methodInvocationNotSupported, null);
			}
			MetadataMember metadataMember = (MetadataMember)expressionResolution;
			if (metadataMember.MetadataMemberClass == MetadataMemberClass.InlineFunctionGroup)
			{
				methodExpr.ErrCtx.ErrorContextInfo = Strings.CtxFunction(metadataMember.Name);
				methodExpr.ErrCtx.UseContextInfoAsResourceIdentifier = false;
				ValueExpression result;
				if (SemanticAnalyzer.TryConvertInlineFunctionCall((InlineFunctionGroup)metadataMember, methodExpr, sr, out result))
				{
					return result;
				}
				return SemanticAnalyzer.ConvertMethodExpr(methodExpr, false, sr);
			}
			else
			{
				switch (metadataMember.MetadataMemberClass)
				{
				case MetadataMemberClass.Type:
					methodExpr.ErrCtx.ErrorContextInfo = Strings.CtxTypeCtor(metadataMember.Name);
					methodExpr.ErrCtx.UseContextInfoAsResourceIdentifier = false;
					return SemanticAnalyzer.ConvertTypeConstructorCall((MetadataType)metadataMember, methodExpr, sr);
				case MetadataMemberClass.FunctionGroup:
					methodExpr.ErrCtx.ErrorContextInfo = Strings.CtxFunction(metadataMember.Name);
					methodExpr.ErrCtx.UseContextInfoAsResourceIdentifier = false;
					return SemanticAnalyzer.ConvertModelFunctionCall((MetadataFunctionGroup)metadataMember, methodExpr, sr);
				default:
				{
					ErrorContext errCtx2 = methodExpr.Expr.ErrCtx;
					string errorMessage = Strings.CannotResolveNameToTypeOrFunction(metadataMember.Name);
					throw EntitySqlException.Create(errCtx2, errorMessage, null);
				}
				}
			}
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00064234 File Offset: 0x00062434
		private static IDisposable ConvertMethodExpr_TryEnterIgnoreEntityContainerNameResolution(DotExpr leftExpr, SemanticResolver sr)
		{
			if (leftExpr == null || !(leftExpr.Left is Identifier))
			{
				return null;
			}
			return sr.EnterIgnoreEntityContainerNameResolution();
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00064250 File Offset: 0x00062450
		private static IDisposable ConvertMethodExpr_TryEnterV1ViewGenBackwardCompatibilityResolution(DotExpr leftExpr, SemanticResolver sr)
		{
			if (leftExpr != null && leftExpr.Left is Identifier && (sr.ParserOptions.ParserCompilationMode == ParserOptions.CompilationMode.RestrictedViewGenerationMode || sr.ParserOptions.ParserCompilationMode == ParserOptions.CompilationMode.UserViewGenerationMode))
			{
				StorageMappingItemCollection storageMappingItemCollection = sr.TypeResolver.Perspective.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace) as StorageMappingItemCollection;
				if (storageMappingItemCollection.MappingVersion < 2.0)
				{
					return sr.TypeResolver.EnterBackwardCompatibilityResolution();
				}
			}
			return null;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x000642E0 File Offset: 0x000624E0
		private static bool TryConvertInlineFunctionCall(InlineFunctionGroup inlineFunctionGroup, MethodExpr methodExpr, SemanticResolver sr, out ValueExpression inlineFunctionCall)
		{
			inlineFunctionCall = null;
			if (methodExpr.DistinctKind != DistinctKind.None)
			{
				return false;
			}
			List<TypeUsage> argTypes;
			List<DbExpression> list = SemanticAnalyzer.ConvertFunctionArguments(methodExpr.Args, sr, out argTypes);
			bool flag = false;
			InlineFunctionInfo inlineFunctionInfo = SemanticResolver.ResolveFunctionOverloads<InlineFunctionInfo, DbVariableReferenceExpression>(inlineFunctionGroup.FunctionMetadata, argTypes, (InlineFunctionInfo lambdaOverload) => lambdaOverload.Parameters, (DbVariableReferenceExpression varRef) => varRef.ResultType, (DbVariableReferenceExpression varRef) => ParameterMode.In, false, out flag);
			if (flag)
			{
				ErrorContext errCtx = methodExpr.ErrCtx;
				string ambiguousFunctionArguments = Strings.AmbiguousFunctionArguments;
				throw EntitySqlException.Create(errCtx, ambiguousFunctionArguments, null);
			}
			if (inlineFunctionInfo == null)
			{
				return false;
			}
			SemanticAnalyzer.ConvertUntypedNullsInArguments<DbVariableReferenceExpression>(list, inlineFunctionInfo.Parameters, (DbVariableReferenceExpression formal) => formal.ResultType);
			inlineFunctionCall = new ValueExpression(inlineFunctionInfo.GetLambda(sr).Invoke(list));
			return true;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x000643D4 File Offset: 0x000625D4
		private static ValueExpression ConvertTypeConstructorCall(MetadataType metadataType, MethodExpr methodExpr, SemanticResolver sr)
		{
			if (!TypeSemantics.IsComplexType(metadataType.TypeUsage) && !TypeSemantics.IsEntityType(metadataType.TypeUsage) && !TypeSemantics.IsRelationshipType(metadataType.TypeUsage))
			{
				ErrorContext errCtx = methodExpr.ErrCtx;
				string errorMessage = Strings.InvalidCtorUseOnType(metadataType.TypeUsage.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			if (metadataType.TypeUsage.EdmType.Abstract)
			{
				ErrorContext errCtx2 = methodExpr.ErrCtx;
				string errorMessage2 = Strings.CannotInstantiateAbstractType(metadataType.TypeUsage.EdmType.FullName);
				throw EntitySqlException.Create(errCtx2, errorMessage2, null);
			}
			if (methodExpr.DistinctKind != DistinctKind.None)
			{
				ErrorContext errCtx3 = methodExpr.ErrCtx;
				string invalidDistinctArgumentInCtor = Strings.InvalidDistinctArgumentInCtor;
				throw EntitySqlException.Create(errCtx3, invalidDistinctArgumentInCtor, null);
			}
			List<DbRelatedEntityRef> list = null;
			if (methodExpr.HasRelationships)
			{
				if (sr.ParserOptions.ParserCompilationMode != ParserOptions.CompilationMode.RestrictedViewGenerationMode && sr.ParserOptions.ParserCompilationMode != ParserOptions.CompilationMode.UserViewGenerationMode)
				{
					ErrorContext errCtx4 = methodExpr.Relationships.ErrCtx;
					string invalidModeForWithRelationshipClause = Strings.InvalidModeForWithRelationshipClause;
					throw EntitySqlException.Create(errCtx4, invalidModeForWithRelationshipClause, null);
				}
				EntityType entityType = metadataType.TypeUsage.EdmType as EntityType;
				if (entityType == null)
				{
					ErrorContext errCtx5 = methodExpr.Relationships.ErrCtx;
					string invalidTypeForWithRelationshipClause = Strings.InvalidTypeForWithRelationshipClause;
					throw EntitySqlException.Create(errCtx5, invalidTypeForWithRelationshipClause, null);
				}
				HashSet<string> hashSet = new HashSet<string>();
				list = new List<DbRelatedEntityRef>(methodExpr.Relationships.Count);
				for (int i = 0; i < methodExpr.Relationships.Count; i++)
				{
					RelshipNavigationExpr relshipNavigationExpr = methodExpr.Relationships[i];
					DbRelatedEntityRef dbRelatedEntityRef = SemanticAnalyzer.ConvertRelatedEntityRef(relshipNavigationExpr, entityType, sr);
					string text = string.Join(":", new string[]
					{
						dbRelatedEntityRef.TargetEnd.DeclaringType.Identity,
						dbRelatedEntityRef.TargetEnd.Identity
					});
					if (hashSet.Contains(text))
					{
						ErrorContext errCtx6 = relshipNavigationExpr.ErrCtx;
						string errorMessage3 = Strings.RelationshipTargetMustBeUnique(text);
						throw EntitySqlException.Create(errCtx6, errorMessage3, null);
					}
					hashSet.Add(text);
					list.Add(dbRelatedEntityRef);
				}
			}
			List<TypeUsage> list2;
			return new ValueExpression(SemanticAnalyzer.CreateConstructorCallExpression(methodExpr, metadataType.TypeUsage, SemanticAnalyzer.ConvertFunctionArguments(methodExpr.Args, sr, out list2), list, sr));
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00064600 File Offset: 0x00062800
		private static ValueExpression ConvertModelFunctionCall(MetadataFunctionGroup metadataFunctionGroup, MethodExpr methodExpr, SemanticResolver sr)
		{
			if (metadataFunctionGroup.FunctionMetadata.Any((EdmFunction f) => !f.IsComposableAttribute))
			{
				ErrorContext errCtx = methodExpr.ErrCtx;
				string errorMessage = Strings.CannotCallNoncomposableFunction(metadataFunctionGroup.Name);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			if (TypeSemantics.IsAggregateFunction(metadataFunctionGroup.FunctionMetadata[0]) && sr.IsInAnyGroupScope())
			{
				return new ValueExpression(SemanticAnalyzer.ConvertAggregateFunctionInGroupScope(methodExpr, metadataFunctionGroup, sr));
			}
			return new ValueExpression(SemanticAnalyzer.CreateModelFunctionCallExpression(methodExpr, metadataFunctionGroup, sr));
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0006468C File Offset: 0x0006288C
		private static DbExpression ConvertAggregateFunctionInGroupScope(MethodExpr methodExpr, MetadataFunctionGroup metadataFunctionGroup, SemanticResolver sr)
		{
			DbExpression result = null;
			if (SemanticAnalyzer.TryConvertAsResolvedGroupAggregate(methodExpr, sr, out result))
			{
				return result;
			}
			ScopeRegion innermostReferencedScopeRegion = (sr.CurrentGroupAggregateInfo != null) ? sr.CurrentGroupAggregateInfo.InnermostReferencedScopeRegion : null;
			List<TypeUsage> argTypes;
			if (SemanticAnalyzer.TryConvertAsCollectionFunction(methodExpr, metadataFunctionGroup, sr, out argTypes, out result))
			{
				return result;
			}
			if (sr.CurrentGroupAggregateInfo != null)
			{
				sr.CurrentGroupAggregateInfo.InnermostReferencedScopeRegion = innermostReferencedScopeRegion;
			}
			if (SemanticAnalyzer.TryConvertAsFunctionAggregate(methodExpr, metadataFunctionGroup, argTypes, sr, out result))
			{
				return result;
			}
			ErrorContext errCtx = methodExpr.ErrCtx;
			string errorMessage = Strings.FailedToResolveAggregateFunction(metadataFunctionGroup.Name);
			throw EntitySqlException.Create(errCtx, errorMessage, null);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00064710 File Offset: 0x00062910
		private static bool TryConvertAsResolvedGroupAggregate(GroupAggregateExpr groupAggregateExpr, SemanticResolver sr, out DbExpression converted)
		{
			converted = null;
			if (groupAggregateExpr.AggregateInfo == null)
			{
				return false;
			}
			groupAggregateExpr.AggregateInfo.SetContainingAggregate(sr.CurrentGroupAggregateInfo);
			if (!sr.TryResolveInternalAggregateName(groupAggregateExpr.AggregateInfo.AggregateName, groupAggregateExpr.AggregateInfo.ErrCtx, out converted))
			{
				converted = groupAggregateExpr.AggregateInfo.AggregateStubExpression;
			}
			return true;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00064770 File Offset: 0x00062970
		private static bool TryConvertAsCollectionFunction(MethodExpr methodExpr, MetadataFunctionGroup metadataFunctionGroup, SemanticResolver sr, out List<TypeUsage> argTypes, out DbExpression converted)
		{
			List<DbExpression> list = SemanticAnalyzer.ConvertFunctionArguments(methodExpr.Args, sr, out argTypes);
			bool flag = false;
			EdmFunction edmFunction = SemanticResolver.ResolveFunctionOverloads(metadataFunctionGroup.FunctionMetadata, argTypes, false, out flag);
			if (flag)
			{
				ErrorContext errCtx = methodExpr.ErrCtx;
				string ambiguousFunctionArguments = Strings.AmbiguousFunctionArguments;
				throw EntitySqlException.Create(errCtx, ambiguousFunctionArguments, null);
			}
			if (edmFunction != null)
			{
				SemanticAnalyzer.ConvertUntypedNullsInArguments<FunctionParameter>(list, edmFunction.Parameters, (FunctionParameter parameter) => parameter.TypeUsage);
				converted = edmFunction.Invoke(list);
				return true;
			}
			converted = null;
			return false;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00064804 File Offset: 0x00062A04
		private static bool TryConvertAsFunctionAggregate(MethodExpr methodExpr, MetadataFunctionGroup metadataFunctionGroup, List<TypeUsage> argTypes, SemanticResolver sr, out DbExpression converted)
		{
			converted = null;
			bool flag = false;
			EdmFunction edmFunction = SemanticResolver.ResolveFunctionOverloads(metadataFunctionGroup.FunctionMetadata, argTypes, true, out flag);
			if (flag)
			{
				ErrorContext errCtx = methodExpr.ErrCtx;
				string ambiguousFunctionArguments = Strings.AmbiguousFunctionArguments;
				throw EntitySqlException.Create(errCtx, ambiguousFunctionArguments, null);
			}
			if (edmFunction == null)
			{
				CqlErrorHelper.ReportFunctionOverloadError(methodExpr, metadataFunctionGroup.FunctionMetadata[0], argTypes);
			}
			FunctionAggregateInfo functionAggregateInfo;
			List<DbExpression> list;
			using (sr.EnterFunctionAggregate(methodExpr, methodExpr.ErrCtx, out functionAggregateInfo))
			{
				List<TypeUsage> list2;
				list = SemanticAnalyzer.ConvertFunctionArguments(methodExpr.Args, sr, out list2);
			}
			SemanticAnalyzer.ConvertUntypedNullsInArguments<FunctionParameter>(list, edmFunction.Parameters, (FunctionParameter parameter) => TypeHelpers.GetElementTypeUsage(parameter.TypeUsage));
			DbFunctionAggregate aggregateDefinition;
			if (methodExpr.DistinctKind == DistinctKind.Distinct)
			{
				aggregateDefinition = edmFunction.AggregateDistinct(list[0]);
			}
			else
			{
				aggregateDefinition = edmFunction.Aggregate(list[0]);
			}
			functionAggregateInfo.AttachToAstNode(sr.GenerateInternalName("groupAgg" + edmFunction.Name), aggregateDefinition);
			functionAggregateInfo.EvaluatingScopeRegion.GroupAggregateInfos.Add(functionAggregateInfo);
			converted = functionAggregateInfo.AggregateStubExpression;
			return true;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x00064928 File Offset: 0x00062B28
		private static DbExpression CreateConstructorCallExpression(MethodExpr methodExpr, TypeUsage type, List<DbExpression> args, List<DbRelatedEntityRef> relshipExprList, SemanticResolver sr)
		{
			int num = 0;
			int count = args.Count;
			StructuralType structuralType = (StructuralType)type.EdmType;
			foreach (object obj in TypeHelpers.GetAllStructuralMembers(structuralType))
			{
				EdmMember edmMember = (EdmMember)obj;
				TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(edmMember);
				if (count <= num)
				{
					ErrorContext errCtx = methodExpr.ErrCtx;
					string errorMessage = Strings.NumberOfTypeCtorIsLessThenFormalSpec(edmMember.Name);
					throw EntitySqlException.Create(errCtx, errorMessage, null);
				}
				if (args[num] == null)
				{
					EdmProperty edmProperty = edmMember as EdmProperty;
					if (edmProperty != null && !edmProperty.Nullable)
					{
						ErrorContext errCtx2 = methodExpr.Args[num].ErrCtx;
						string errorMessage2 = Strings.InvalidNullLiteralForNonNullableMember(edmMember.Name, structuralType.FullName);
						throw EntitySqlException.Create(errCtx2, errorMessage2, null);
					}
					args[num] = modelTypeUsage.Null();
				}
				bool flag = TypeSemantics.IsPromotableTo(args[num].ResultType, modelTypeUsage);
				if (ParserOptions.CompilationMode.RestrictedViewGenerationMode == sr.ParserOptions.ParserCompilationMode || ParserOptions.CompilationMode.UserViewGenerationMode == sr.ParserOptions.ParserCompilationMode)
				{
					if (!flag && !TypeSemantics.IsPromotableTo(modelTypeUsage, args[num].ResultType))
					{
						ErrorContext errCtx3 = methodExpr.Args[num].ErrCtx;
						string errorMessage3 = Strings.InvalidCtorArgumentType(args[num].ResultType.EdmType.FullName, edmMember.Name, modelTypeUsage.EdmType.FullName);
						throw EntitySqlException.Create(errCtx3, errorMessage3, null);
					}
					if (Helper.IsPrimitiveType(modelTypeUsage.EdmType) && !TypeSemantics.IsSubTypeOf(args[num].ResultType, modelTypeUsage))
					{
						args[num] = args[num].CastTo(modelTypeUsage);
					}
				}
				else if (!flag)
				{
					ErrorContext errCtx4 = methodExpr.Args[num].ErrCtx;
					string errorMessage4 = Strings.InvalidCtorArgumentType(args[num].ResultType.EdmType.FullName, edmMember.Name, modelTypeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx4, errorMessage4, null);
				}
				num++;
			}
			if (num != count)
			{
				ErrorContext errCtx5 = methodExpr.ErrCtx;
				string errorMessage5 = Strings.NumberOfTypeCtorIsMoreThenFormalSpec(structuralType.FullName);
				throw EntitySqlException.Create(errCtx5, errorMessage5, null);
			}
			DbExpression result;
			if (relshipExprList != null && relshipExprList.Count > 0)
			{
				EntityType entityType = (EntityType)type.EdmType;
				result = DbExpressionBuilder.CreateNewEntityWithRelationshipsExpression(entityType, args, relshipExprList);
			}
			else
			{
				result = TypeHelpers.GetReadOnlyType(type).New(args);
			}
			return result;
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00064BC4 File Offset: 0x00062DC4
		private static DbFunctionExpression CreateModelFunctionCallExpression(MethodExpr methodExpr, MetadataFunctionGroup metadataFunctionGroup, SemanticResolver sr)
		{
			bool flag = false;
			if (methodExpr.DistinctKind != DistinctKind.None)
			{
				ErrorContext errCtx = methodExpr.ErrCtx;
				string invalidDistinctArgumentInNonAggFunction = Strings.InvalidDistinctArgumentInNonAggFunction;
				throw EntitySqlException.Create(errCtx, invalidDistinctArgumentInNonAggFunction, null);
			}
			List<TypeUsage> argTypes;
			List<DbExpression> list = SemanticAnalyzer.ConvertFunctionArguments(methodExpr.Args, sr, out argTypes);
			EdmFunction edmFunction = SemanticResolver.ResolveFunctionOverloads(metadataFunctionGroup.FunctionMetadata, argTypes, false, out flag);
			if (flag)
			{
				ErrorContext errCtx2 = methodExpr.ErrCtx;
				string ambiguousFunctionArguments = Strings.AmbiguousFunctionArguments;
				throw EntitySqlException.Create(errCtx2, ambiguousFunctionArguments, null);
			}
			if (edmFunction == null)
			{
				CqlErrorHelper.ReportFunctionOverloadError(methodExpr, metadataFunctionGroup.FunctionMetadata[0], argTypes);
			}
			SemanticAnalyzer.ConvertUntypedNullsInArguments<FunctionParameter>(list, edmFunction.Parameters, (FunctionParameter parameter) => parameter.TypeUsage);
			return edmFunction.Invoke(list);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x00064C90 File Offset: 0x00062E90
		private static List<DbExpression> ConvertFunctionArguments(NodeList<Node> astExprList, SemanticResolver sr, out List<TypeUsage> argTypes)
		{
			List<DbExpression> list = new List<DbExpression>();
			if (astExprList != null)
			{
				for (int i = 0; i < astExprList.Count; i++)
				{
					list.Add(SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(astExprList[i], sr));
				}
			}
			argTypes = list.Select(delegate(DbExpression a)
			{
				if (a == null)
				{
					return null;
				}
				return a.ResultType;
			}).ToList<TypeUsage>();
			return list;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00064CF8 File Offset: 0x00062EF8
		private static void ConvertUntypedNullsInArguments<TParameterMetadata>(List<DbExpression> args, IList<TParameterMetadata> parametersMetadata, Func<TParameterMetadata, TypeUsage> getParameterTypeUsage)
		{
			for (int i = 0; i < args.Count; i++)
			{
				if (args[i] == null)
				{
					args[i] = getParameterTypeUsage(parametersMetadata[i]).Null();
				}
			}
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00064D38 File Offset: 0x00062F38
		private static ExpressionResolution ConvertParameter(Node expr, SemanticResolver sr)
		{
			QueryParameter queryParameter = (QueryParameter)expr;
			DbParameterReferenceExpression value;
			if (sr.Parameters == null || !sr.Parameters.TryGetValue(queryParameter.Name, out value))
			{
				ErrorContext errCtx = queryParameter.ErrCtx;
				string errorMessage = Strings.ParameterWasNotDefined(queryParameter.Name);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			return new ValueExpression(value);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00064E40 File Offset: 0x00063040
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private static DbRelatedEntityRef ConvertRelatedEntityRef(RelshipNavigationExpr relshipExpr, EntityType driverEntityType, SemanticResolver sr)
		{
			EdmType edmType = SemanticAnalyzer.ConvertTypeName(relshipExpr.TypeName, sr).EdmType;
			RelationshipType relationshipType = edmType as RelationshipType;
			if (relationshipType == null)
			{
				ErrorContext errCtx = relshipExpr.TypeName.ErrCtx;
				string errorMessage = Strings.RelationshipTypeExpected(edmType.FullName);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(relshipExpr.RefExpr, sr);
			RefType refType = dbExpression.ResultType.EdmType as RefType;
			if (refType == null)
			{
				ErrorContext errCtx2 = relshipExpr.RefExpr.ErrCtx;
				string relatedEndExprTypeMustBeReference = Strings.RelatedEndExprTypeMustBeReference;
				throw EntitySqlException.Create(errCtx2, relatedEndExprTypeMustBeReference, null);
			}
			RelationshipEndMember toEnd;
			if (relshipExpr.ToEndIdentifier != null)
			{
				toEnd = (RelationshipEndMember)relationshipType.Members.FirstOrDefault((EdmMember m) => m.Name.Equals(relshipExpr.ToEndIdentifier.Name, StringComparison.OrdinalIgnoreCase));
				if (toEnd == null)
				{
					ErrorContext errCtx3 = relshipExpr.ToEndIdentifier.ErrCtx;
					string errorMessage2 = Strings.InvalidRelationshipMember(relshipExpr.ToEndIdentifier.Name, relationshipType.FullName);
					throw EntitySqlException.Create(errCtx3, errorMessage2, null);
				}
				if (toEnd.RelationshipMultiplicity != RelationshipMultiplicity.One && toEnd.RelationshipMultiplicity != RelationshipMultiplicity.ZeroOrOne)
				{
					ErrorContext errCtx4 = relshipExpr.ToEndIdentifier.ErrCtx;
					string errorMessage3 = Strings.InvalidWithRelationshipTargetEndMultiplicity(toEnd.Name, toEnd.RelationshipMultiplicity.ToString());
					throw EntitySqlException.Create(errCtx4, errorMessage3, null);
				}
				if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(refType, toEnd.TypeUsage.EdmType))
				{
					ErrorContext errCtx5 = relshipExpr.RefExpr.ErrCtx;
					string errorMessage4 = Strings.RelatedEndExprTypeMustBePromotoableToToEnd(refType.FullName, toEnd.TypeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx5, errorMessage4, null);
				}
			}
			else
			{
				RelationshipEndMember[] array = (from m in relationshipType.Members
				select (RelationshipEndMember)m into e
				where TypeSemantics.IsStructurallyEqualOrPromotableTo(refType, e.TypeUsage.EdmType) && (e.RelationshipMultiplicity == RelationshipMultiplicity.One || e.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
				select e).ToArray<RelationshipEndMember>();
				switch (array.Length)
				{
				case 0:
				{
					ErrorContext errCtx6 = relshipExpr.ErrCtx;
					string errorMessage5 = Strings.InvalidImplicitRelationshipToEnd(relationshipType.FullName);
					throw EntitySqlException.Create(errCtx6, errorMessage5, null);
				}
				case 1:
					toEnd = array[0];
					break;
				default:
				{
					ErrorContext errCtx7 = relshipExpr.ErrCtx;
					string relationshipToEndIsAmbiguos = Strings.RelationshipToEndIsAmbiguos;
					throw EntitySqlException.Create(errCtx7, relationshipToEndIsAmbiguos, null);
				}
				}
			}
			RelationshipEndMember relationshipEndMember;
			if (relshipExpr.FromEndIdentifier != null)
			{
				relationshipEndMember = (RelationshipEndMember)relationshipType.Members.FirstOrDefault((EdmMember m) => m.Name.Equals(relshipExpr.FromEndIdentifier.Name, StringComparison.OrdinalIgnoreCase));
				if (relationshipEndMember == null)
				{
					ErrorContext errCtx8 = relshipExpr.FromEndIdentifier.ErrCtx;
					string errorMessage6 = Strings.InvalidRelationshipMember(relshipExpr.FromEndIdentifier.Name, relationshipType.FullName);
					throw EntitySqlException.Create(errCtx8, errorMessage6, null);
				}
				if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(driverEntityType.GetReferenceType(), relationshipEndMember.TypeUsage.EdmType))
				{
					ErrorContext errCtx9 = relshipExpr.FromEndIdentifier.ErrCtx;
					string errorMessage7 = Strings.SourceTypeMustBePromotoableToFromEndRelationType(driverEntityType.FullName, relationshipEndMember.TypeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx9, errorMessage7, null);
				}
				if (relationshipEndMember.EdmEquals(toEnd))
				{
					ErrorContext errCtx10 = relshipExpr.ErrCtx;
					string relationshipFromEndIsAmbiguos = Strings.RelationshipFromEndIsAmbiguos;
					throw EntitySqlException.Create(errCtx10, relationshipFromEndIsAmbiguos, null);
				}
			}
			else
			{
				RelationshipEndMember[] array2 = (from m in relationshipType.Members
				select (RelationshipEndMember)m into e
				where TypeSemantics.IsStructurallyEqualOrPromotableTo(driverEntityType.GetReferenceType(), e.TypeUsage.EdmType) && !e.EdmEquals(toEnd)
				select e).ToArray<RelationshipEndMember>();
				switch (array2.Length)
				{
				case 0:
				{
					ErrorContext errCtx11 = relshipExpr.ErrCtx;
					string errorMessage8 = Strings.InvalidImplicitRelationshipFromEnd(relationshipType.FullName);
					throw EntitySqlException.Create(errCtx11, errorMessage8, null);
				}
				case 1:
					relationshipEndMember = array2[0];
					break;
				default:
				{
					ErrorContext errCtx12 = relshipExpr.ErrCtx;
					string relationshipFromEndIsAmbiguos2 = Strings.RelationshipFromEndIsAmbiguos;
					throw EntitySqlException.Create(errCtx12, relationshipFromEndIsAmbiguos2, null);
				}
				}
			}
			return DbExpressionBuilder.CreateRelatedEntityRef(relationshipEndMember, toEnd, dbExpression);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0006537C File Offset: 0x0006357C
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private static ExpressionResolution ConvertRelshipNavigationExpr(Node astExpr, SemanticResolver sr)
		{
			RelshipNavigationExpr relshipExpr = (RelshipNavigationExpr)astExpr;
			EdmType edmType = SemanticAnalyzer.ConvertTypeName(relshipExpr.TypeName, sr).EdmType;
			RelationshipType relationshipType = edmType as RelationshipType;
			if (relationshipType == null)
			{
				ErrorContext errCtx = relshipExpr.TypeName.ErrCtx;
				string errorMessage = Strings.RelationshipTypeExpected(edmType.FullName);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(relshipExpr.RefExpr, sr);
			RefType sourceRefType = dbExpression.ResultType.EdmType as RefType;
			if (sourceRefType == null)
			{
				EntityType entityType = dbExpression.ResultType.EdmType as EntityType;
				if (entityType == null)
				{
					ErrorContext errCtx2 = relshipExpr.RefExpr.ErrCtx;
					string relatedEndExprTypeMustBeReference = Strings.RelatedEndExprTypeMustBeReference;
					throw EntitySqlException.Create(errCtx2, relatedEndExprTypeMustBeReference, null);
				}
				dbExpression = dbExpression.GetEntityRef();
				sourceRefType = (RefType)dbExpression.ResultType.EdmType;
			}
			RelationshipEndMember toEnd;
			if (relshipExpr.ToEndIdentifier != null)
			{
				toEnd = (RelationshipEndMember)relationshipType.Members.FirstOrDefault((EdmMember m) => m.Name.Equals(relshipExpr.ToEndIdentifier.Name, StringComparison.OrdinalIgnoreCase));
				if (toEnd == null)
				{
					ErrorContext errCtx3 = relshipExpr.ToEndIdentifier.ErrCtx;
					string errorMessage2 = Strings.InvalidRelationshipMember(relshipExpr.ToEndIdentifier.Name, relationshipType.FullName);
					throw EntitySqlException.Create(errCtx3, errorMessage2, null);
				}
			}
			else
			{
				toEnd = null;
			}
			RelationshipEndMember fromEnd;
			if (relshipExpr.FromEndIdentifier != null)
			{
				fromEnd = (RelationshipEndMember)relationshipType.Members.FirstOrDefault((EdmMember m) => m.Name.Equals(relshipExpr.FromEndIdentifier.Name, StringComparison.OrdinalIgnoreCase));
				if (fromEnd == null)
				{
					ErrorContext errCtx4 = relshipExpr.FromEndIdentifier.ErrCtx;
					string errorMessage3 = Strings.InvalidRelationshipMember(relshipExpr.FromEndIdentifier.Name, relationshipType.FullName);
					throw EntitySqlException.Create(errCtx4, errorMessage3, null);
				}
				if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(sourceRefType, fromEnd.TypeUsage.EdmType))
				{
					ErrorContext errCtx5 = relshipExpr.FromEndIdentifier.ErrCtx;
					string errorMessage4 = Strings.SourceTypeMustBePromotoableToFromEndRelationType(sourceRefType.FullName, fromEnd.TypeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx5, errorMessage4, null);
				}
				if (toEnd != null && fromEnd.EdmEquals(toEnd))
				{
					ErrorContext errCtx6 = relshipExpr.ErrCtx;
					string relationshipFromEndIsAmbiguos = Strings.RelationshipFromEndIsAmbiguos;
					throw EntitySqlException.Create(errCtx6, relationshipFromEndIsAmbiguos, null);
				}
			}
			else
			{
				RelationshipEndMember[] array = (from m in relationshipType.Members
				select (RelationshipEndMember)m into e
				where TypeSemantics.IsStructurallyEqualOrPromotableTo(sourceRefType, e.TypeUsage.EdmType) && (toEnd == null || !e.EdmEquals(toEnd))
				select e).ToArray<RelationshipEndMember>();
				switch (array.Length)
				{
				case 0:
				{
					ErrorContext errCtx7 = relshipExpr.ErrCtx;
					string errorMessage5 = Strings.InvalidImplicitRelationshipFromEnd(relationshipType.FullName);
					throw EntitySqlException.Create(errCtx7, errorMessage5, null);
				}
				case 1:
					fromEnd = array[0];
					break;
				default:
				{
					ErrorContext errCtx8 = relshipExpr.ErrCtx;
					string relationshipFromEndIsAmbiguos2 = Strings.RelationshipFromEndIsAmbiguos;
					throw EntitySqlException.Create(errCtx8, relationshipFromEndIsAmbiguos2, null);
				}
				}
			}
			if (toEnd == null)
			{
				RelationshipEndMember[] array2 = (from m in relationshipType.Members
				select (RelationshipEndMember)m into e
				where !e.EdmEquals(fromEnd)
				select e).ToArray<RelationshipEndMember>();
				switch (array2.Length)
				{
				case 0:
				{
					ErrorContext errCtx9 = relshipExpr.ErrCtx;
					string errorMessage6 = Strings.InvalidImplicitRelationshipToEnd(relationshipType.FullName);
					throw EntitySqlException.Create(errCtx9, errorMessage6, null);
				}
				case 1:
					toEnd = array2[0];
					break;
				default:
				{
					ErrorContext errCtx10 = relshipExpr.ErrCtx;
					string relationshipToEndIsAmbiguos = Strings.RelationshipToEndIsAmbiguos;
					throw EntitySqlException.Create(errCtx10, relationshipToEndIsAmbiguos, null);
				}
				}
			}
			DbExpression value = dbExpression.Navigate(fromEnd, toEnd);
			return new ValueExpression(value);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x000657CC File Offset: 0x000639CC
		private static ExpressionResolution ConvertRefExpr(Node astExpr, SemanticResolver sr)
		{
			RefExpr refExpr = (RefExpr)astExpr;
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(refExpr.ArgExpr, sr);
			if (!TypeSemantics.IsEntityType(dbExpression.ResultType))
			{
				ErrorContext errCtx = refExpr.ArgExpr.ErrCtx;
				string errorMessage = Strings.RefArgIsNotOfEntityType(dbExpression.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			dbExpression = dbExpression.GetEntityRef();
			return new ValueExpression(dbExpression);
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00065834 File Offset: 0x00063A34
		private static ExpressionResolution ConvertDeRefExpr(Node astExpr, SemanticResolver sr)
		{
			DerefExpr derefExpr = (DerefExpr)astExpr;
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(derefExpr.ArgExpr, sr);
			if (!TypeSemantics.IsReferenceType(dbExpression.ResultType))
			{
				ErrorContext errCtx = derefExpr.ArgExpr.ErrCtx;
				string errorMessage = Strings.DeRefArgIsNotOfRefType(dbExpression.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			dbExpression = dbExpression.Deref();
			return new ValueExpression(dbExpression);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0006589C File Offset: 0x00063A9C
		private static ExpressionResolution ConvertCreateRefExpr(Node astExpr, SemanticResolver sr)
		{
			CreateRefExpr createRefExpr = (CreateRefExpr)astExpr;
			DbScanExpression dbScanExpression = SemanticAnalyzer.ConvertValueExpression(createRefExpr.EntitySet, sr) as DbScanExpression;
			if (dbScanExpression == null)
			{
				ErrorContext errCtx = createRefExpr.EntitySet.ErrCtx;
				string exprIsNotValidEntitySetForCreateRef = Strings.ExprIsNotValidEntitySetForCreateRef;
				throw EntitySqlException.Create(errCtx, exprIsNotValidEntitySetForCreateRef, null);
			}
			EntitySet entitySet = dbScanExpression.Target as EntitySet;
			if (entitySet == null)
			{
				ErrorContext errCtx2 = createRefExpr.EntitySet.ErrCtx;
				string exprIsNotValidEntitySetForCreateRef2 = Strings.ExprIsNotValidEntitySetForCreateRef;
				throw EntitySqlException.Create(errCtx2, exprIsNotValidEntitySetForCreateRef2, null);
			}
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(createRefExpr.Keys, sr);
			RowType rowType = dbExpression.ResultType.EdmType as RowType;
			if (rowType == null)
			{
				ErrorContext errCtx3 = createRefExpr.Keys.ErrCtx;
				string invalidCreateRefKeyType = Strings.InvalidCreateRefKeyType;
				throw EntitySqlException.Create(errCtx3, invalidCreateRefKeyType, null);
			}
			RowType rowType2 = TypeHelpers.CreateKeyRowType(entitySet.ElementType);
			if (rowType2.Members.Count != rowType.Members.Count)
			{
				ErrorContext errCtx4 = createRefExpr.Keys.ErrCtx;
				string imcompatibleCreateRefKeyType = Strings.ImcompatibleCreateRefKeyType;
				throw EntitySqlException.Create(errCtx4, imcompatibleCreateRefKeyType, null);
			}
			if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(dbExpression.ResultType, TypeUsage.Create(rowType2)))
			{
				ErrorContext errCtx5 = createRefExpr.Keys.ErrCtx;
				string imcompatibleCreateRefKeyElementType = Strings.ImcompatibleCreateRefKeyElementType;
				throw EntitySqlException.Create(errCtx5, imcompatibleCreateRefKeyElementType, null);
			}
			DbExpression value;
			if (createRefExpr.TypeIdentifier != null)
			{
				TypeUsage typeUsage = SemanticAnalyzer.ConvertTypeName(createRefExpr.TypeIdentifier, sr);
				if (!TypeSemantics.IsEntityType(typeUsage))
				{
					ErrorContext errCtx6 = createRefExpr.TypeIdentifier.ErrCtx;
					string errorMessage = Strings.CreateRefTypeIdentifierMustSpecifyAnEntityType(typeUsage.EdmType.FullName, typeUsage.EdmType.BuiltInTypeKind.ToString());
					throw EntitySqlException.Create(errCtx6, errorMessage, null);
				}
				if (!TypeSemantics.IsValidPolymorphicCast(entitySet.ElementType, typeUsage.EdmType))
				{
					ErrorContext errCtx7 = createRefExpr.TypeIdentifier.ErrCtx;
					string errorMessage2 = Strings.CreateRefTypeIdentifierMustBeASubOrSuperType(entitySet.ElementType.FullName, typeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx7, errorMessage2, null);
				}
				value = entitySet.RefFromKey(dbExpression, (EntityType)typeUsage.EdmType);
			}
			else
			{
				value = entitySet.RefFromKey(dbExpression);
			}
			return new ValueExpression(value);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00065AAC File Offset: 0x00063CAC
		private static ExpressionResolution ConvertKeyExpr(Node astExpr, SemanticResolver sr)
		{
			KeyExpr keyExpr = (KeyExpr)astExpr;
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(keyExpr.ArgExpr, sr);
			if (TypeSemantics.IsEntityType(dbExpression.ResultType))
			{
				dbExpression = dbExpression.GetEntityRef();
			}
			else if (!TypeSemantics.IsReferenceType(dbExpression.ResultType))
			{
				ErrorContext errCtx = keyExpr.ArgExpr.ErrCtx;
				string errorMessage = Strings.InvalidKeyArgument(dbExpression.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			dbExpression = dbExpression.GetRefKey();
			return new ValueExpression(dbExpression);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00065B28 File Offset: 0x00063D28
		private static ExpressionResolution ConvertBuiltIn(Node astExpr, SemanticResolver sr)
		{
			BuiltInExpr builtInExpr = (BuiltInExpr)astExpr;
			SemanticAnalyzer.BuiltInExprConverter builtInExprConverter = SemanticAnalyzer._builtInExprConverter[builtInExpr.Kind];
			if (builtInExprConverter == null)
			{
				string unknownBuiltInAstExpressionType = Strings.UnknownBuiltInAstExpressionType;
				throw new EntitySqlException(unknownBuiltInAstExpressionType);
			}
			return new ValueExpression(builtInExprConverter(builtInExpr, sr));
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00065B74 File Offset: 0x00063D74
		private static Pair<DbExpression, DbExpression> ConvertArithmeticArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(astBuiltInExpr.Arg1, astBuiltInExpr.Arg2, astBuiltInExpr.ErrCtx, () => Strings.InvalidNullArithmetic, sr);
			if (!TypeSemantics.IsNumericType(pair.Left.ResultType))
			{
				ErrorContext errCtx = astBuiltInExpr.Arg1.ErrCtx;
				string expressionMustBeNumericType = Strings.ExpressionMustBeNumericType;
				throw EntitySqlException.Create(errCtx, expressionMustBeNumericType, null);
			}
			if (pair.Right != null)
			{
				if (!TypeSemantics.IsNumericType(pair.Right.ResultType))
				{
					ErrorContext errCtx2 = astBuiltInExpr.Arg2.ErrCtx;
					string expressionMustBeNumericType2 = Strings.ExpressionMustBeNumericType;
					throw EntitySqlException.Create(errCtx2, expressionMustBeNumericType2, null);
				}
				if (TypeHelpers.GetCommonTypeUsage(pair.Left.ResultType, pair.Right.ResultType) == null)
				{
					ErrorContext errCtx3 = astBuiltInExpr.ErrCtx;
					string errorMessage = Strings.ArgumentTypesAreIncompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName);
					throw EntitySqlException.Create(errCtx3, errorMessage, null);
				}
			}
			return pair;
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x00065C88 File Offset: 0x00063E88
		private static Pair<DbExpression, DbExpression> ConvertPlusOperands(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(astBuiltInExpr.Arg1, astBuiltInExpr.Arg2, astBuiltInExpr.ErrCtx, () => Strings.InvalidNullArithmetic, sr);
			if (!TypeSemantics.IsNumericType(pair.Left.ResultType) && !TypeSemantics.IsPrimitiveType(pair.Left.ResultType, PrimitiveTypeKind.String))
			{
				ErrorContext errCtx = astBuiltInExpr.Arg1.ErrCtx;
				string plusLeftExpressionInvalidType = Strings.PlusLeftExpressionInvalidType;
				throw EntitySqlException.Create(errCtx, plusLeftExpressionInvalidType, null);
			}
			if (!TypeSemantics.IsNumericType(pair.Right.ResultType) && !TypeSemantics.IsPrimitiveType(pair.Right.ResultType, PrimitiveTypeKind.String))
			{
				ErrorContext errCtx2 = astBuiltInExpr.Arg2.ErrCtx;
				string plusRightExpressionInvalidType = Strings.PlusRightExpressionInvalidType;
				throw EntitySqlException.Create(errCtx2, plusRightExpressionInvalidType, null);
			}
			if (TypeHelpers.GetCommonTypeUsage(pair.Left.ResultType, pair.Right.ResultType) == null)
			{
				ErrorContext errCtx3 = astBuiltInExpr.ErrCtx;
				string errorMessage = Strings.ArgumentTypesAreIncompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx3, errorMessage, null);
			}
			return pair;
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x00065DB4 File Offset: 0x00063FB4
		private static Pair<DbExpression, DbExpression> ConvertLogicalArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(astBuiltInExpr.Arg1, sr);
			if (dbExpression == null)
			{
				dbExpression = TypeResolver.BooleanType.Null();
			}
			DbExpression dbExpression2 = null;
			if (astBuiltInExpr.Arg2 != null)
			{
				dbExpression2 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(astBuiltInExpr.Arg2, sr);
				if (dbExpression2 == null)
				{
					dbExpression2 = TypeResolver.BooleanType.Null();
				}
			}
			if (!SemanticAnalyzer.IsBooleanType(dbExpression.ResultType))
			{
				ErrorContext errCtx = astBuiltInExpr.Arg1.ErrCtx;
				string expressionTypeMustBeBoolean = Strings.ExpressionTypeMustBeBoolean;
				throw EntitySqlException.Create(errCtx, expressionTypeMustBeBoolean, null);
			}
			if (dbExpression2 != null && !SemanticAnalyzer.IsBooleanType(dbExpression2.ResultType))
			{
				ErrorContext errCtx2 = astBuiltInExpr.Arg2.ErrCtx;
				string expressionTypeMustBeBoolean2 = Strings.ExpressionTypeMustBeBoolean;
				throw EntitySqlException.Create(errCtx2, expressionTypeMustBeBoolean2, null);
			}
			return new Pair<DbExpression, DbExpression>(dbExpression, dbExpression2);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00065E68 File Offset: 0x00064068
		private static Pair<DbExpression, DbExpression> ConvertEqualCompArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(astBuiltInExpr.Arg1, astBuiltInExpr.Arg2, astBuiltInExpr.ErrCtx, () => Strings.InvalidNullComparison, sr);
			if (!TypeSemantics.IsEqualComparableTo(pair.Left.ResultType, pair.Right.ResultType))
			{
				ErrorContext errCtx = astBuiltInExpr.ErrCtx;
				string errorMessage = Strings.ArgumentTypesAreIncompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			return pair;
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00065F10 File Offset: 0x00064110
		private static Pair<DbExpression, DbExpression> ConvertOrderCompArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(astBuiltInExpr.Arg1, astBuiltInExpr.Arg2, astBuiltInExpr.ErrCtx, () => Strings.InvalidNullComparison, sr);
			if (!TypeSemantics.IsOrderComparableTo(pair.Left.ResultType, pair.Right.ResultType))
			{
				ErrorContext errCtx = astBuiltInExpr.ErrCtx;
				string errorMessage = Strings.ArgumentTypesAreIncompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			return pair;
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00065FB4 File Offset: 0x000641B4
		private static Pair<DbExpression, DbExpression> ConvertSetArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(astBuiltInExpr.Arg1, sr);
			DbExpression dbExpression2 = null;
			if (astBuiltInExpr.Arg2 != null)
			{
				if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
				{
					ErrorContext errCtx = astBuiltInExpr.Arg1.ErrCtx;
					string leftSetExpressionArgsMustBeCollection = Strings.LeftSetExpressionArgsMustBeCollection;
					throw EntitySqlException.Create(errCtx, leftSetExpressionArgsMustBeCollection, null);
				}
				dbExpression2 = SemanticAnalyzer.ConvertValueExpression(astBuiltInExpr.Arg2, sr);
				if (!TypeSemantics.IsCollectionType(dbExpression2.ResultType))
				{
					ErrorContext errCtx2 = astBuiltInExpr.Arg2.ErrCtx;
					string rightSetExpressionArgsMustBeCollection = Strings.RightSetExpressionArgsMustBeCollection;
					throw EntitySqlException.Create(errCtx2, rightSetExpressionArgsMustBeCollection, null);
				}
				TypeUsage elementTypeUsage = TypeHelpers.GetElementTypeUsage(dbExpression.ResultType);
				TypeUsage elementTypeUsage2 = TypeHelpers.GetElementTypeUsage(dbExpression2.ResultType);
				TypeUsage typeUsage;
				if (!TypeSemantics.TryGetCommonType(elementTypeUsage, elementTypeUsage2, out typeUsage))
				{
					CqlErrorHelper.ReportIncompatibleCommonType(astBuiltInExpr.ErrCtx, elementTypeUsage, elementTypeUsage2);
				}
				if (astBuiltInExpr.Kind != BuiltInKind.UnionAll)
				{
					if (!TypeHelpers.IsSetComparableOpType(TypeHelpers.GetElementTypeUsage(dbExpression.ResultType)))
					{
						ErrorContext errCtx3 = astBuiltInExpr.Arg1.ErrCtx;
						string errorMessage = Strings.PlaceholderSetArgTypeIsNotEqualComparable(Strings.LocalizedLeft, astBuiltInExpr.Kind.ToString().ToUpperInvariant(), TypeHelpers.GetElementTypeUsage(dbExpression.ResultType).EdmType.FullName);
						throw EntitySqlException.Create(errCtx3, errorMessage, null);
					}
					if (!TypeHelpers.IsSetComparableOpType(TypeHelpers.GetElementTypeUsage(dbExpression2.ResultType)))
					{
						ErrorContext errCtx4 = astBuiltInExpr.Arg2.ErrCtx;
						string errorMessage2 = Strings.PlaceholderSetArgTypeIsNotEqualComparable(Strings.LocalizedRight, astBuiltInExpr.Kind.ToString().ToUpperInvariant(), TypeHelpers.GetElementTypeUsage(dbExpression2.ResultType).EdmType.FullName);
						throw EntitySqlException.Create(errCtx4, errorMessage2, null);
					}
				}
				else
				{
					if (Helper.IsAssociationType(elementTypeUsage.EdmType))
					{
						ErrorContext errCtx5 = astBuiltInExpr.Arg1.ErrCtx;
						string errorMessage3 = Strings.InvalidAssociationTypeForUnion(elementTypeUsage.EdmType.FullName);
						throw EntitySqlException.Create(errCtx5, errorMessage3, null);
					}
					if (Helper.IsAssociationType(elementTypeUsage2.EdmType))
					{
						ErrorContext errCtx6 = astBuiltInExpr.Arg2.ErrCtx;
						string errorMessage4 = Strings.InvalidAssociationTypeForUnion(elementTypeUsage2.EdmType.FullName);
						throw EntitySqlException.Create(errCtx6, errorMessage4, null);
					}
				}
			}
			else
			{
				if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
				{
					ErrorContext errCtx7 = astBuiltInExpr.Arg1.ErrCtx;
					string errorMessage5 = Strings.InvalidUnarySetOpArgument(astBuiltInExpr.Name);
					throw EntitySqlException.Create(errCtx7, errorMessage5, null);
				}
				if (astBuiltInExpr.Kind == BuiltInKind.Distinct && !TypeHelpers.IsValidDistinctOpType(TypeHelpers.GetElementTypeUsage(dbExpression.ResultType)))
				{
					ErrorContext errCtx8 = astBuiltInExpr.Arg1.ErrCtx;
					string expressionTypeMustBeEqualComparable = Strings.ExpressionTypeMustBeEqualComparable;
					throw EntitySqlException.Create(errCtx8, expressionTypeMustBeEqualComparable, null);
				}
			}
			return new Pair<DbExpression, DbExpression>(dbExpression, dbExpression2);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0006622C File Offset: 0x0006442C
		private static Pair<DbExpression, DbExpression> ConvertInExprArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(astBuiltInExpr.Arg2, sr);
			if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
			{
				ErrorContext errCtx = astBuiltInExpr.Arg2.ErrCtx;
				string rightSetExpressionArgsMustBeCollection = Strings.RightSetExpressionArgsMustBeCollection;
				throw EntitySqlException.Create(errCtx, rightSetExpressionArgsMustBeCollection, null);
			}
			DbExpression dbExpression2 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(astBuiltInExpr.Arg1, sr);
			if (dbExpression2 == null)
			{
				TypeUsage elementTypeUsage = TypeHelpers.GetElementTypeUsage(dbExpression.ResultType);
				SemanticAnalyzer.ValidateTypeForNullExpression(elementTypeUsage, astBuiltInExpr.Arg1.ErrCtx);
				dbExpression2 = elementTypeUsage.Null();
			}
			if (TypeSemantics.IsCollectionType(dbExpression2.ResultType))
			{
				ErrorContext errCtx2 = astBuiltInExpr.Arg1.ErrCtx;
				string expressionTypeMustNotBeCollection = Strings.ExpressionTypeMustNotBeCollection;
				throw EntitySqlException.Create(errCtx2, expressionTypeMustNotBeCollection, null);
			}
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(dbExpression2.ResultType, TypeHelpers.GetElementTypeUsage(dbExpression.ResultType));
			if (commonTypeUsage == null || !TypeHelpers.IsValidInOpType(commonTypeUsage))
			{
				ErrorContext errCtx3 = astBuiltInExpr.ErrCtx;
				string errorMessage = Strings.InvalidInExprArgs(dbExpression2.ResultType.EdmType.FullName, dbExpression.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx3, errorMessage, null);
			}
			return new Pair<DbExpression, DbExpression>(dbExpression2, dbExpression);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00066338 File Offset: 0x00064538
		private static void ValidateTypeForNullExpression(TypeUsage type, ErrorContext errCtx)
		{
			if (TypeSemantics.IsCollectionType(type))
			{
				string nullLiteralCannotBePromotedToCollectionOfNulls = Strings.NullLiteralCannotBePromotedToCollectionOfNulls;
				throw EntitySqlException.Create(errCtx, nullLiteralCannotBePromotedToCollectionOfNulls, null);
			}
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0006635C File Offset: 0x0006455C
		private static TypeUsage ConvertTypeName(Node typeName, SemanticResolver sr)
		{
			string[] array = null;
			NodeList<Node> nodeList = null;
			MethodExpr methodExpr = typeName as MethodExpr;
			if (methodExpr != null)
			{
				typeName = methodExpr.Expr;
				typeName.ErrCtx.ErrorContextInfo = methodExpr.ErrCtx.ErrorContextInfo;
				typeName.ErrCtx.UseContextInfoAsResourceIdentifier = methodExpr.ErrCtx.UseContextInfoAsResourceIdentifier;
				nodeList = methodExpr.Args;
			}
			Identifier identifier = typeName as Identifier;
			if (identifier != null)
			{
				array = new string[]
				{
					identifier.Name
				};
			}
			DotExpr dotExpr = typeName as DotExpr;
			if (dotExpr != null)
			{
				dotExpr.IsMultipartIdentifier(out array);
			}
			if (array == null)
			{
				ErrorContext errCtx = typeName.ErrCtx;
				string invalidMetadataMemberName = Strings.InvalidMetadataMemberName;
				throw EntitySqlException.Create(errCtx, invalidMetadataMemberName, null);
			}
			MetadataMember metadataMember = sr.ResolveMetadataMemberName(array, typeName.ErrCtx);
			MetadataMemberClass metadataMemberClass = metadataMember.MetadataMemberClass;
			if (metadataMemberClass == MetadataMemberClass.Type)
			{
				TypeUsage typeUsage = ((MetadataType)metadataMember).TypeUsage;
				if (nodeList != null)
				{
					typeUsage = SemanticAnalyzer.ConvertTypeSpecArgs(typeUsage, nodeList, typeName.ErrCtx);
				}
				return typeUsage;
			}
			if (metadataMemberClass != MetadataMemberClass.Namespace)
			{
				ErrorContext errCtx2 = typeName.ErrCtx;
				string errorMessage = Strings.InvalidMetadataMemberClassResolution(metadataMember.Name, metadataMember.MetadataMemberClassName, MetadataType.TypeClassName);
				throw EntitySqlException.Create(errCtx2, errorMessage, null);
			}
			ErrorContext errCtx3 = typeName.ErrCtx;
			string errorMessage2 = Strings.TypeNameNotFound(metadataMember.Name);
			throw EntitySqlException.Create(errCtx3, errorMessage2, null);
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0006649C File Offset: 0x0006469C
		private static TypeUsage ConvertTypeSpecArgs(TypeUsage parameterizedType, NodeList<Node> typeSpecArgs, ErrorContext errCtx)
		{
			foreach (Node node in ((IEnumerable<Node>)typeSpecArgs))
			{
				if (!(node is Literal))
				{
					ErrorContext errCtx2 = node.ErrCtx;
					string typeArgumentMustBeLiteral = Strings.TypeArgumentMustBeLiteral;
					throw EntitySqlException.Create(errCtx2, typeArgumentMustBeLiteral, null);
				}
			}
			PrimitiveType primitiveType = parameterizedType.EdmType as PrimitiveType;
			if (primitiveType == null || primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Decimal)
			{
				string errorMessage = Strings.TypeDoesNotSupportSpec(primitiveType.FullName);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			if (typeSpecArgs.Count > 2)
			{
				string errorMessage2 = Strings.TypeArgumentCountMismatch(primitiveType.FullName, 2);
				throw EntitySqlException.Create(errCtx, errorMessage2, null);
			}
			byte b;
			SemanticAnalyzer.ConvertTypeFacetValue(primitiveType, (Literal)typeSpecArgs[0], "Precision", out b);
			byte b2 = 0;
			if (typeSpecArgs.Count == 2)
			{
				SemanticAnalyzer.ConvertTypeFacetValue(primitiveType, (Literal)typeSpecArgs[1], "Scale", out b2);
			}
			if (b < b2)
			{
				ErrorContext errCtx3 = typeSpecArgs[0].ErrCtx;
				string errorMessage3 = Strings.PrecisionMustBeGreaterThanScale(b, b2);
				throw EntitySqlException.Create(errCtx3, errorMessage3, null);
			}
			return TypeUsage.CreateDecimalTypeUsage(primitiveType, b, b2);
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x000665D4 File Offset: 0x000647D4
		private static void ConvertTypeFacetValue(PrimitiveType type, Literal value, string facetName, out byte byteValue)
		{
			FacetDescription facet = Helper.GetFacet(type.ProviderManifest.GetFacetDescriptions(type), facetName);
			if (facet == null)
			{
				ErrorContext errCtx = value.ErrCtx;
				string errorMessage = Strings.TypeDoesNotSupportFacet(type.FullName, facetName);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			if (!value.IsNumber || !byte.TryParse(value.OriginalValue, out byteValue))
			{
				ErrorContext errCtx2 = value.ErrCtx;
				string typeArgumentIsNotValid = Strings.TypeArgumentIsNotValid;
				throw EntitySqlException.Create(errCtx2, typeArgumentIsNotValid, null);
			}
			if (facet.MaxValue != null && (int)byteValue > facet.MaxValue.Value)
			{
				ErrorContext errCtx3 = value.ErrCtx;
				string errorMessage2 = Strings.TypeArgumentExceedsMax(facetName);
				throw EntitySqlException.Create(errCtx3, errorMessage2, null);
			}
			if (facet.MinValue != null && (int)byteValue < facet.MinValue.Value)
			{
				ErrorContext errCtx4 = value.ErrCtx;
				string errorMessage3 = Strings.TypeArgumentBelowMin(facetName);
				throw EntitySqlException.Create(errCtx4, errorMessage3, null);
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x000666F0 File Offset: 0x000648F0
		private static TypeUsage ConvertTypeDefinition(Node typeDefinitionExpr, SemanticResolver sr)
		{
			CollectionTypeDefinition collectionTypeDefinition = typeDefinitionExpr as CollectionTypeDefinition;
			RefTypeDefinition refTypeDefinition = typeDefinitionExpr as RefTypeDefinition;
			RowTypeDefinition rowTypeDefinition = typeDefinitionExpr as RowTypeDefinition;
			TypeUsage result;
			if (collectionTypeDefinition != null)
			{
				TypeUsage elementType = SemanticAnalyzer.ConvertTypeDefinition(collectionTypeDefinition.ElementTypeDef, sr);
				result = TypeHelpers.CreateCollectionTypeUsage(elementType);
			}
			else if (refTypeDefinition != null)
			{
				TypeUsage typeUsage = SemanticAnalyzer.ConvertTypeName(refTypeDefinition.RefTypeIdentifier, sr);
				if (!TypeSemantics.IsEntityType(typeUsage))
				{
					ErrorContext errCtx = refTypeDefinition.RefTypeIdentifier.ErrCtx;
					string errorMessage = Strings.RefTypeIdentifierMustSpecifyAnEntityType(typeUsage.EdmType.FullName, typeUsage.EdmType.BuiltInTypeKind.ToString());
					throw EntitySqlException.Create(errCtx, errorMessage, null);
				}
				result = TypeHelpers.CreateReferenceTypeUsage((EntityType)typeUsage.EdmType);
			}
			else if (rowTypeDefinition != null)
			{
				result = TypeHelpers.CreateRowTypeUsage(from p in rowTypeDefinition.Properties
				select new KeyValuePair<string, TypeUsage>(p.Name.Name, SemanticAnalyzer.ConvertTypeDefinition(p.Type, sr)));
			}
			else
			{
				result = SemanticAnalyzer.ConvertTypeName(typeDefinitionExpr, sr);
			}
			return result;
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x000667FC File Offset: 0x000649FC
		private static ExpressionResolution ConvertRowConstructor(Node expr, SemanticResolver sr)
		{
			RowConstructorExpr rowConstructorExpr = (RowConstructorExpr)expr;
			Dictionary<string, TypeUsage> dictionary = new Dictionary<string, TypeUsage>(sr.NameComparer);
			List<DbExpression> list = new List<DbExpression>(rowConstructorExpr.AliasedExprList.Count);
			for (int i = 0; i < rowConstructorExpr.AliasedExprList.Count; i++)
			{
				AliasedExpr aliasedExpr = rowConstructorExpr.AliasedExprList[i];
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(aliasedExpr.Expr, sr);
				if (dbExpression == null)
				{
					ErrorContext errCtx = aliasedExpr.Expr.ErrCtx;
					string rowCtorElementCannotBeNull = Strings.RowCtorElementCannotBeNull;
					throw EntitySqlException.Create(errCtx, rowCtorElementCannotBeNull, null);
				}
				string text = sr.InferAliasName(aliasedExpr, dbExpression);
				if (dictionary.ContainsKey(text))
				{
					if (aliasedExpr.Alias != null)
					{
						CqlErrorHelper.ReportAliasAlreadyUsedError(text, aliasedExpr.Alias.ErrCtx, Strings.InRowCtor);
					}
					else
					{
						text = sr.GenerateInternalName("autoRowCol");
					}
				}
				dictionary.Add(text, dbExpression.ResultType);
				list.Add(dbExpression);
			}
			return new ValueExpression(TypeHelpers.CreateRowTypeUsage(dictionary).New(list));
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00066920 File Offset: 0x00064B20
		private static ExpressionResolution ConvertMultisetConstructor(Node expr, SemanticResolver sr)
		{
			MultisetConstructorExpr multisetConstructorExpr = (MultisetConstructorExpr)expr;
			if (multisetConstructorExpr.ExprList == null)
			{
				ErrorContext errCtx = expr.ErrCtx;
				string cannotCreateEmptyMultiset = Strings.CannotCreateEmptyMultiset;
				throw EntitySqlException.Create(errCtx, cannotCreateEmptyMultiset, null);
			}
			DbExpression[] array = (from e in multisetConstructorExpr.ExprList
			select SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(e, sr)).ToArray<DbExpression>();
			TypeUsage[] array2 = (from e in array
			where e != null
			select e.ResultType).ToArray<TypeUsage>();
			if (array2.Length == 0)
			{
				ErrorContext errCtx2 = expr.ErrCtx;
				string cannotCreateMultisetofNulls = Strings.CannotCreateMultisetofNulls;
				throw EntitySqlException.Create(errCtx2, cannotCreateMultisetofNulls, null);
			}
			TypeUsage typeUsage = TypeHelpers.GetCommonTypeUsage(array2);
			if (typeUsage == null)
			{
				ErrorContext errCtx3 = expr.ErrCtx;
				string multisetElemsAreNotTypeCompatible = Strings.MultisetElemsAreNotTypeCompatible;
				throw EntitySqlException.Create(errCtx3, multisetElemsAreNotTypeCompatible, null);
			}
			typeUsage = TypeHelpers.GetReadOnlyType(typeUsage);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					SemanticAnalyzer.ValidateTypeForNullExpression(typeUsage, multisetConstructorExpr.ExprList[i].ErrCtx);
					array[i] = typeUsage.Null();
				}
			}
			return new ValueExpression(TypeHelpers.CreateCollectionTypeUsage(typeUsage).New(array));
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00066A7C File Offset: 0x00064C7C
		private static ExpressionResolution ConvertCaseExpr(Node expr, SemanticResolver sr)
		{
			CaseExpr caseExpr = (CaseExpr)expr;
			List<DbExpression> list = new List<DbExpression>(caseExpr.WhenThenExprList.Count);
			List<DbExpression> list2 = new List<DbExpression>(caseExpr.WhenThenExprList.Count);
			for (int i = 0; i < caseExpr.WhenThenExprList.Count; i++)
			{
				WhenThenExpr whenThenExpr = caseExpr.WhenThenExprList[i];
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(whenThenExpr.WhenExpr, sr);
				if (!SemanticAnalyzer.IsBooleanType(dbExpression.ResultType))
				{
					ErrorContext errCtx = whenThenExpr.WhenExpr.ErrCtx;
					string expressionTypeMustBeBoolean = Strings.ExpressionTypeMustBeBoolean;
					throw EntitySqlException.Create(errCtx, expressionTypeMustBeBoolean, null);
				}
				list.Add(dbExpression);
				DbExpression item = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(whenThenExpr.ThenExpr, sr);
				list2.Add(item);
			}
			DbExpression dbExpression2 = (caseExpr.ElseExpr != null) ? SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(caseExpr.ElseExpr, sr) : null;
			List<TypeUsage> list3 = (from e in list2
			where e != null
			select e.ResultType).ToList<TypeUsage>();
			if (dbExpression2 != null)
			{
				list3.Add(dbExpression2.ResultType);
			}
			if (list3.Count == 0)
			{
				ErrorContext errCtx2 = caseExpr.ElseExpr.ErrCtx;
				string invalidCaseWhenThenNullType = Strings.InvalidCaseWhenThenNullType;
				throw EntitySqlException.Create(errCtx2, invalidCaseWhenThenNullType, null);
			}
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(list3);
			if (commonTypeUsage == null)
			{
				ErrorContext errCtx3 = caseExpr.WhenThenExprList[0].ThenExpr.ErrCtx;
				string invalidCaseResultTypes = Strings.InvalidCaseResultTypes;
				throw EntitySqlException.Create(errCtx3, invalidCaseResultTypes, null);
			}
			for (int j = 0; j < list2.Count; j++)
			{
				if (list2[j] == null)
				{
					SemanticAnalyzer.ValidateTypeForNullExpression(commonTypeUsage, caseExpr.WhenThenExprList[j].ThenExpr.ErrCtx);
					list2[j] = commonTypeUsage.Null();
				}
			}
			if (dbExpression2 == null)
			{
				if (caseExpr.ElseExpr == null && TypeSemantics.IsCollectionType(commonTypeUsage))
				{
					dbExpression2 = commonTypeUsage.NewEmptyCollection();
				}
				else
				{
					SemanticAnalyzer.ValidateTypeForNullExpression(commonTypeUsage, (caseExpr.ElseExpr ?? caseExpr).ErrCtx);
					dbExpression2 = commonTypeUsage.Null();
				}
			}
			return new ValueExpression(DbExpressionBuilder.Case(list, list2, dbExpression2));
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00066CA4 File Offset: 0x00064EA4
		private static ExpressionResolution ConvertQueryExpr(Node expr, SemanticResolver sr)
		{
			QueryExpr queryExpr = (QueryExpr)expr;
			DbExpression value = null;
			bool flag = ParserOptions.CompilationMode.RestrictedViewGenerationMode == sr.ParserOptions.ParserCompilationMode;
			if (queryExpr.HavingClause != null && queryExpr.GroupByClause == null)
			{
				ErrorContext errCtx = queryExpr.ErrCtx;
				string havingRequiresGroupClause = Strings.HavingRequiresGroupClause;
				throw EntitySqlException.Create(errCtx, havingRequiresGroupClause, null);
			}
			if (queryExpr.SelectClause.TopExpr != null)
			{
				if (queryExpr.OrderByClause != null && queryExpr.OrderByClause.LimitSubClause != null)
				{
					ErrorContext errCtx2 = queryExpr.SelectClause.TopExpr.ErrCtx;
					string topAndLimitCannotCoexist = Strings.TopAndLimitCannotCoexist;
					throw EntitySqlException.Create(errCtx2, topAndLimitCannotCoexist, null);
				}
				if (queryExpr.OrderByClause != null && queryExpr.OrderByClause.SkipSubClause != null)
				{
					ErrorContext errCtx3 = queryExpr.SelectClause.TopExpr.ErrCtx;
					string topAndSkipCannotCoexist = Strings.TopAndSkipCannotCoexist;
					throw EntitySqlException.Create(errCtx3, topAndSkipCannotCoexist, null);
				}
			}
			using (sr.EnterScopeRegion())
			{
				DbExpressionBinding source = SemanticAnalyzer.ProcessFromClause(queryExpr.FromClause, sr);
				source = SemanticAnalyzer.ProcessWhereClause(source, queryExpr.WhereClause, sr);
				bool queryProjectionProcessed = false;
				if (!flag)
				{
					source = SemanticAnalyzer.ProcessGroupByClause(source, queryExpr, sr);
					source = SemanticAnalyzer.ProcessHavingClause(source, queryExpr.HavingClause, sr);
					source = SemanticAnalyzer.ProcessOrderByClause(source, queryExpr, out queryProjectionProcessed, sr);
				}
				value = SemanticAnalyzer.ProcessSelectClause(source, queryExpr, queryProjectionProcessed, sr);
			}
			return new ValueExpression(value);
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00066DF4 File Offset: 0x00064FF4
		private static DbExpression ProcessSelectClause(DbExpressionBinding source, QueryExpr queryExpr, bool queryProjectionProcessed, SemanticResolver sr)
		{
			SelectClause selectClause = queryExpr.SelectClause;
			DbExpression dbExpression;
			if (queryProjectionProcessed)
			{
				dbExpression = source.Expression;
			}
			else
			{
				List<KeyValuePair<string, DbExpression>> projectionItems = SemanticAnalyzer.ConvertSelectClauseItems(queryExpr, sr);
				dbExpression = SemanticAnalyzer.CreateProjectExpression(source, selectClause, projectionItems);
			}
			if (selectClause.TopExpr != null || (queryExpr.OrderByClause != null && queryExpr.OrderByClause.LimitSubClause != null))
			{
				Node node;
				string exprName;
				if (selectClause.TopExpr != null)
				{
					node = selectClause.TopExpr;
					exprName = "TOP";
				}
				else
				{
					node = queryExpr.OrderByClause.LimitSubClause;
					exprName = "LIMIT";
				}
				DbExpression dbExpression2 = SemanticAnalyzer.ConvertValueExpression(node, sr);
				SemanticAnalyzer.ValidateExpressionIsCommandParamOrNonNegativeIntegerConstant(dbExpression2, node.ErrCtx, exprName);
				dbExpression = dbExpression.Limit(dbExpression2);
			}
			return dbExpression;
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00066E90 File Offset: 0x00065090
		private static List<KeyValuePair<string, DbExpression>> ConvertSelectClauseItems(QueryExpr queryExpr, SemanticResolver sr)
		{
			SelectClause selectClause = queryExpr.SelectClause;
			if (selectClause.SelectKind == SelectKind.Value)
			{
				if (selectClause.Items.Count != 1)
				{
					ErrorContext errCtx = selectClause.ErrCtx;
					string invalidSelectValueList = Strings.InvalidSelectValueList;
					throw EntitySqlException.Create(errCtx, invalidSelectValueList, null);
				}
				if (selectClause.Items[0].Alias != null && queryExpr.OrderByClause == null)
				{
					ErrorContext errCtx2 = selectClause.Items[0].ErrCtx;
					string invalidSelectValueAliasedExpression = Strings.InvalidSelectValueAliasedExpression;
					throw EntitySqlException.Create(errCtx2, invalidSelectValueAliasedExpression, null);
				}
			}
			HashSet<string> hashSet = new HashSet<string>(sr.NameComparer);
			List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>(selectClause.Items.Count);
			for (int i = 0; i < selectClause.Items.Count; i++)
			{
				AliasedExpr aliasedExpr = selectClause.Items[i];
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(aliasedExpr.Expr, sr);
				string text = sr.InferAliasName(aliasedExpr, dbExpression);
				if (hashSet.Contains(text))
				{
					if (aliasedExpr.Alias != null)
					{
						CqlErrorHelper.ReportAliasAlreadyUsedError(text, aliasedExpr.Alias.ErrCtx, Strings.InSelectProjectionList);
					}
					else
					{
						text = sr.GenerateInternalName("autoProject");
					}
				}
				hashSet.Add(text);
				list.Add(new KeyValuePair<string, DbExpression>(text, dbExpression));
			}
			return list;
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00066FD0 File Offset: 0x000651D0
		private static DbExpression CreateProjectExpression(DbExpressionBinding source, SelectClause selectClause, List<KeyValuePair<string, DbExpression>> projectionItems)
		{
			DbExpression dbExpression;
			if (selectClause.SelectKind == SelectKind.Value)
			{
				dbExpression = source.Project(projectionItems[0].Value);
			}
			else
			{
				dbExpression = source.Project(DbExpressionBuilder.NewRow(projectionItems));
			}
			if (selectClause.DistinctKind == DistinctKind.Distinct)
			{
				SemanticAnalyzer.ValidateDistinctProjection(dbExpression.ResultType, selectClause);
				dbExpression = dbExpression.Distinct();
			}
			return dbExpression;
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00067034 File Offset: 0x00065234
		private static void ValidateDistinctProjection(TypeUsage projectExpressionResultType, SelectClause selectClause)
		{
			ErrorContext errCtx = selectClause.Items[0].Expr.ErrCtx;
			List<ErrorContext> projectionItemErrCtxs;
			if (selectClause.SelectKind != SelectKind.Row)
			{
				projectionItemErrCtxs = null;
			}
			else
			{
				projectionItemErrCtxs = new List<ErrorContext>(from item in selectClause.Items
				select item.Expr.ErrCtx);
			}
			SemanticAnalyzer.ValidateDistinctProjection(projectExpressionResultType, errCtx, projectionItemErrCtxs);
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00067098 File Offset: 0x00065298
		private static void ValidateDistinctProjection(TypeUsage projectExpressionResultType, ErrorContext defaultErrCtx, List<ErrorContext> projectionItemErrCtxs)
		{
			TypeUsage elementTypeUsage = TypeHelpers.GetElementTypeUsage(projectExpressionResultType);
			if (!TypeHelpers.IsValidDistinctOpType(elementTypeUsage))
			{
				ErrorContext errCtx = defaultErrCtx;
				if (projectionItemErrCtxs != null && TypeSemantics.IsRowType(elementTypeUsage))
				{
					RowType rowType = elementTypeUsage.EdmType as RowType;
					for (int i = 0; i < rowType.Members.Count; i++)
					{
						if (!TypeHelpers.IsValidDistinctOpType(rowType.Members[i].TypeUsage))
						{
							errCtx = projectionItemErrCtxs[i];
							break;
						}
					}
				}
				string selectDistinctMustBeEqualComparable = Strings.SelectDistinctMustBeEqualComparable;
				throw EntitySqlException.Create(errCtx, selectDistinctMustBeEqualComparable, null);
			}
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00067118 File Offset: 0x00065318
		private static void ValidateExpressionIsCommandParamOrNonNegativeIntegerConstant(DbExpression expr, ErrorContext errCtx, string exprName)
		{
			if (expr.ExpressionKind != DbExpressionKind.Constant && expr.ExpressionKind != DbExpressionKind.ParameterReference)
			{
				string errorMessage = Strings.PlaceholderExpressionMustBeConstant(exprName);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			if (!TypeSemantics.IsPromotableTo(expr.ResultType, TypeResolver.Int64Type))
			{
				string errorMessage2 = Strings.PlaceholderExpressionMustBeCompatibleWithEdm64(exprName, expr.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, errorMessage2, null);
			}
			DbConstantExpression dbConstantExpression = expr as DbConstantExpression;
			if (dbConstantExpression != null && System.Convert.ToInt64(dbConstantExpression.Value, CultureInfo.InvariantCulture) < 0L)
			{
				string errorMessage3 = Strings.PlaceholderExpressionMustBeGreaterThanOrEqualToZero(exprName);
				throw EntitySqlException.Create(errCtx, errorMessage3, null);
			}
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x000671C4 File Offset: 0x000653C4
		private static DbExpressionBinding ProcessFromClause(FromClause fromClause, SemanticResolver sr)
		{
			DbExpressionBinding fromBinding = null;
			List<SourceScopeEntry> list = new List<SourceScopeEntry>();
			for (int i = 0; i < fromClause.FromClauseItems.Count; i++)
			{
				List<SourceScopeEntry> collection;
				DbExpressionBinding dbExpressionBinding = SemanticAnalyzer.ProcessFromClauseItem(fromClause.FromClauseItems[i], sr, out collection);
				list.AddRange(collection);
				if (fromBinding == null)
				{
					fromBinding = dbExpressionBinding;
				}
				else
				{
					fromBinding = fromBinding.CrossApply(dbExpressionBinding).BindAs(sr.GenerateInternalName("lcapply"));
					list.Each((SourceScopeEntry scopeEntry) => scopeEntry.AddParentVar(fromBinding.Variable));
				}
			}
			return fromBinding;
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00067274 File Offset: 0x00065474
		private static DbExpressionBinding ProcessFromClauseItem(FromClauseItem fromClauseItem, SemanticResolver sr, out List<SourceScopeEntry> scopeEntries)
		{
			DbExpressionBinding result;
			switch (fromClauseItem.FromClauseItemKind)
			{
			case FromClauseItemKind.AliasedFromClause:
				result = SemanticAnalyzer.ProcessAliasedFromClauseItem((AliasedExpr)fromClauseItem.FromExpr, sr, out scopeEntries);
				break;
			case FromClauseItemKind.JoinFromClause:
				result = SemanticAnalyzer.ProcessJoinClauseItem((JoinClauseItem)fromClauseItem.FromExpr, sr, out scopeEntries);
				break;
			default:
				result = SemanticAnalyzer.ProcessApplyClauseItem((ApplyClauseItem)fromClauseItem.FromExpr, sr, out scopeEntries);
				break;
			}
			return result;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x000672D8 File Offset: 0x000654D8
		private static DbExpressionBinding ProcessAliasedFromClauseItem(AliasedExpr aliasedExpr, SemanticResolver sr, out List<SourceScopeEntry> scopeEntries)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(aliasedExpr.Expr, sr);
			if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
			{
				ErrorContext errCtx = aliasedExpr.Expr.ErrCtx;
				string expressionMustBeCollection = Strings.ExpressionMustBeCollection;
				throw EntitySqlException.Create(errCtx, expressionMustBeCollection, null);
			}
			string text = sr.InferAliasName(aliasedExpr, dbExpression);
			if (sr.CurrentScope.Contains(text))
			{
				if (aliasedExpr.Alias != null)
				{
					CqlErrorHelper.ReportAliasAlreadyUsedError(text, aliasedExpr.Alias.ErrCtx, Strings.InFromClause);
				}
				else
				{
					text = sr.GenerateInternalName("autoFrom");
				}
			}
			DbExpressionBinding dbExpressionBinding = dbExpression.BindAs(text);
			SourceScopeEntry sourceScopeEntry = new SourceScopeEntry(dbExpressionBinding.Variable);
			sr.CurrentScope.Add(dbExpressionBinding.Variable.VariableName, sourceScopeEntry);
			scopeEntries = new List<SourceScopeEntry>();
			scopeEntries.Add(sourceScopeEntry);
			return dbExpressionBinding;
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000673F0 File Offset: 0x000655F0
		private static DbExpressionBinding ProcessJoinClauseItem(JoinClauseItem joinClause, SemanticResolver sr, out List<SourceScopeEntry> scopeEntries)
		{
			DbExpressionBinding joinBinding = null;
			if (joinClause.OnExpr == null)
			{
				if (JoinKind.Inner == joinClause.JoinKind)
				{
					ErrorContext errCtx = joinClause.ErrCtx;
					string innerJoinMustHaveOnPredicate = Strings.InnerJoinMustHaveOnPredicate;
					throw EntitySqlException.Create(errCtx, innerJoinMustHaveOnPredicate, null);
				}
			}
			else if (joinClause.JoinKind == JoinKind.Cross)
			{
				ErrorContext errCtx2 = joinClause.OnExpr.ErrCtx;
				string invalidPredicateForCrossJoin = Strings.InvalidPredicateForCrossJoin;
				throw EntitySqlException.Create(errCtx2, invalidPredicateForCrossJoin, null);
			}
			List<SourceScopeEntry> list;
			DbExpressionBinding dbExpressionBinding = SemanticAnalyzer.ProcessFromClauseItem(joinClause.LeftExpr, sr, out list);
			list.Each((SourceScopeEntry scopeEntry) => scopeEntry.IsJoinClauseLeftExpr = true);
			List<SourceScopeEntry> collection;
			DbExpressionBinding dbExpressionBinding2 = SemanticAnalyzer.ProcessFromClauseItem(joinClause.RightExpr, sr, out collection);
			list.Each((SourceScopeEntry scopeEntry) => scopeEntry.IsJoinClauseLeftExpr = false);
			if (joinClause.JoinKind == JoinKind.RightOuter)
			{
				joinClause.JoinKind = JoinKind.LeftOuter;
				DbExpressionBinding dbExpressionBinding3 = dbExpressionBinding;
				dbExpressionBinding = dbExpressionBinding2;
				dbExpressionBinding2 = dbExpressionBinding3;
			}
			DbExpressionKind dbExpressionKind = SemanticAnalyzer.MapJoinKind(joinClause.JoinKind);
			DbExpression joinCondition = null;
			if (joinClause.OnExpr == null)
			{
				if (DbExpressionKind.CrossJoin != dbExpressionKind)
				{
					joinCondition = DbExpressionBuilder.True;
				}
			}
			else
			{
				joinCondition = SemanticAnalyzer.ConvertValueExpression(joinClause.OnExpr, sr);
			}
			joinBinding = DbExpressionBuilder.CreateJoinExpressionByKind(dbExpressionKind, joinCondition, dbExpressionBinding, dbExpressionBinding2).BindAs(sr.GenerateInternalName("join"));
			scopeEntries = list;
			scopeEntries.AddRange(collection);
			scopeEntries.Each((SourceScopeEntry scopeEntry) => scopeEntry.AddParentVar(joinBinding.Variable));
			return joinBinding;
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0006755A File Offset: 0x0006575A
		private static DbExpressionKind MapJoinKind(JoinKind joinKind)
		{
			return SemanticAnalyzer._joinMap[(int)joinKind];
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00067580 File Offset: 0x00065780
		private static DbExpressionBinding ProcessApplyClauseItem(ApplyClauseItem applyClause, SemanticResolver sr, out List<SourceScopeEntry> scopeEntries)
		{
			DbExpressionBinding applyBinding = null;
			List<SourceScopeEntry> list;
			DbExpressionBinding input = SemanticAnalyzer.ProcessFromClauseItem(applyClause.LeftExpr, sr, out list);
			List<SourceScopeEntry> collection;
			DbExpressionBinding apply = SemanticAnalyzer.ProcessFromClauseItem(applyClause.RightExpr, sr, out collection);
			applyBinding = DbExpressionBuilder.CreateApplyExpressionByKind(SemanticAnalyzer.MapApplyKind(applyClause.ApplyKind), input, apply).BindAs(sr.GenerateInternalName("apply"));
			scopeEntries = list;
			scopeEntries.AddRange(collection);
			scopeEntries.Each((SourceScopeEntry scopeEntry) => scopeEntry.AddParentVar(applyBinding.Variable));
			return applyBinding;
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00067609 File Offset: 0x00065809
		private static DbExpressionKind MapApplyKind(ApplyKind applyKind)
		{
			return SemanticAnalyzer._applyMap[(int)applyKind];
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00067612 File Offset: 0x00065812
		private static DbExpressionBinding ProcessWhereClause(DbExpressionBinding source, Node whereClause, SemanticResolver sr)
		{
			if (whereClause == null)
			{
				return source;
			}
			return SemanticAnalyzer.ProcessWhereHavingClausePredicate(source, whereClause, whereClause.ErrCtx, "where", sr);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0006762C File Offset: 0x0006582C
		private static DbExpressionBinding ProcessHavingClause(DbExpressionBinding source, HavingClause havingClause, SemanticResolver sr)
		{
			if (havingClause == null)
			{
				return source;
			}
			return SemanticAnalyzer.ProcessWhereHavingClausePredicate(source, havingClause.HavingPredicate, havingClause.ErrCtx, "having", sr);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00067674 File Offset: 0x00065874
		private static DbExpressionBinding ProcessWhereHavingClausePredicate(DbExpressionBinding source, Node predicate, ErrorContext errCtx, string bindingNameTemplate, SemanticResolver sr)
		{
			DbExpressionBinding whereBinding = null;
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(predicate, sr);
			if (!SemanticAnalyzer.IsBooleanType(dbExpression.ResultType))
			{
				string expressionTypeMustBeBoolean = Strings.ExpressionTypeMustBeBoolean;
				throw EntitySqlException.Create(errCtx, expressionTypeMustBeBoolean, null);
			}
			whereBinding = source.Filter(dbExpression).BindAs(sr.GenerateInternalName(bindingNameTemplate));
			sr.CurrentScopeRegion.ApplyToScopeEntries(delegate(ScopeEntry scopeEntry)
			{
				if (scopeEntry.EntryKind == ScopeEntryKind.SourceVar)
				{
					((SourceScopeEntry)scopeEntry).ReplaceParentVar(whereBinding.Variable);
				}
			});
			return whereBinding;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x000677A4 File Offset: 0x000659A4
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		private static DbExpressionBinding ProcessGroupByClause(DbExpressionBinding source, QueryExpr queryExpr, SemanticResolver sr)
		{
			GroupByClause groupByClause = queryExpr.GroupByClause;
			int num = (groupByClause != null) ? groupByClause.GroupItems.Count : 0;
			bool flag = num == 0;
			if (flag && !queryExpr.HasMethodCall)
			{
				return source;
			}
			DbGroupExpressionBinding groupInputBinding = source.Expression.GroupBindAs(sr.GenerateInternalName("geb"), sr.GenerateInternalName("group"));
			DbGroupAggregate groupAggregate = groupInputBinding.GroupAggregate;
			DbVariableReferenceExpression dbVariableReferenceExpression = groupAggregate.ResultType.Variable(sr.GenerateInternalName("groupAggregate"));
			DbExpressionBinding groupAggregateBinding = dbVariableReferenceExpression.BindAs(sr.GenerateInternalName("groupPartitionItem"));
			sr.CurrentScopeRegion.EnterGroupOperation(groupAggregateBinding);
			sr.CurrentScopeRegion.ApplyToScopeEntries(delegate(ScopeEntry scopeEntry)
			{
				((SourceScopeEntry)scopeEntry).AdjustToGroupVar(groupInputBinding.Variable, groupInputBinding.GroupVariable, groupAggregateBinding.Variable);
			});
			HashSet<string> hashSet = new HashSet<string>(sr.NameComparer);
			List<SemanticAnalyzer.GroupKeyInfo> list = new List<SemanticAnalyzer.GroupKeyInfo>(num);
			if (!flag)
			{
				for (int i = 0; i < num; i++)
				{
					AliasedExpr aliasedExpr = groupByClause.GroupItems[i];
					sr.CurrentScopeRegion.WasResolutionCorrelated = false;
					GroupKeyAggregateInfo groupKeyAggregateInfo;
					DbExpression dbExpression;
					using (sr.EnterGroupKeyDefinition(GroupAggregateKind.GroupKey, aliasedExpr.ErrCtx, out groupKeyAggregateInfo))
					{
						dbExpression = SemanticAnalyzer.ConvertValueExpression(aliasedExpr.Expr, sr);
					}
					if (!sr.CurrentScopeRegion.WasResolutionCorrelated)
					{
						ErrorContext errCtx = aliasedExpr.Expr.ErrCtx;
						string errorMessage = Strings.KeyMustBeCorrelated("GROUP BY");
						throw EntitySqlException.Create(errCtx, errorMessage, null);
					}
					if (!TypeHelpers.IsValidGroupKeyType(dbExpression.ResultType))
					{
						ErrorContext errCtx2 = aliasedExpr.Expr.ErrCtx;
						string groupingKeysMustBeEqualComparable = Strings.GroupingKeysMustBeEqualComparable;
						throw EntitySqlException.Create(errCtx2, groupingKeysMustBeEqualComparable, null);
					}
					GroupKeyAggregateInfo groupKeyAggregateInfo2;
					DbExpression groupVarBasedKeyExpr;
					using (sr.EnterGroupKeyDefinition(GroupAggregateKind.Function, aliasedExpr.ErrCtx, out groupKeyAggregateInfo2))
					{
						groupVarBasedKeyExpr = SemanticAnalyzer.ConvertValueExpression(aliasedExpr.Expr, sr);
					}
					GroupKeyAggregateInfo groupKeyAggregateInfo3;
					DbExpression groupAggBasedKeyExpr;
					using (sr.EnterGroupKeyDefinition(GroupAggregateKind.Partition, aliasedExpr.ErrCtx, out groupKeyAggregateInfo3))
					{
						groupAggBasedKeyExpr = SemanticAnalyzer.ConvertValueExpression(aliasedExpr.Expr, sr);
					}
					string text = sr.InferAliasName(aliasedExpr, dbExpression);
					if (hashSet.Contains(text))
					{
						if (aliasedExpr.Alias != null)
						{
							CqlErrorHelper.ReportAliasAlreadyUsedError(text, aliasedExpr.Alias.ErrCtx, Strings.InGroupClause);
						}
						else
						{
							text = sr.GenerateInternalName("autoGroup");
						}
					}
					hashSet.Add(text);
					SemanticAnalyzer.GroupKeyInfo groupKeyInfo = new SemanticAnalyzer.GroupKeyInfo(text, dbExpression, groupVarBasedKeyExpr, groupAggBasedKeyExpr);
					list.Add(groupKeyInfo);
					if (aliasedExpr.Alias == null)
					{
						DotExpr dotExpr = aliasedExpr.Expr as DotExpr;
						string[] array;
						if (dotExpr != null && dotExpr.IsMultipartIdentifier(out array))
						{
							groupKeyInfo.AlternativeName = array;
							string fullName = TypeResolver.GetFullName(array);
							if (hashSet.Contains(fullName))
							{
								CqlErrorHelper.ReportAliasAlreadyUsedError(fullName, dotExpr.ErrCtx, Strings.InGroupClause);
							}
							hashSet.Add(fullName);
						}
					}
				}
			}
			int currentScopeIndex = sr.CurrentScopeIndex;
			sr.EnterScope();
			foreach (SemanticAnalyzer.GroupKeyInfo groupKeyInfo2 in list)
			{
				sr.CurrentScope.Add(groupKeyInfo2.Name, new GroupKeyDefinitionScopeEntry(groupKeyInfo2.VarBasedKeyExpr, groupKeyInfo2.GroupVarBasedKeyExpr, groupKeyInfo2.GroupAggBasedKeyExpr, null));
				if (groupKeyInfo2.AlternativeName != null)
				{
					string fullName2 = TypeResolver.GetFullName(groupKeyInfo2.AlternativeName);
					sr.CurrentScope.Add(fullName2, new GroupKeyDefinitionScopeEntry(groupKeyInfo2.VarBasedKeyExpr, groupKeyInfo2.GroupVarBasedKeyExpr, groupKeyInfo2.GroupAggBasedKeyExpr, groupKeyInfo2.AlternativeName));
				}
			}
			if (queryExpr.HavingClause != null && queryExpr.HavingClause.HasMethodCall)
			{
				SemanticAnalyzer.ConvertValueExpression(queryExpr.HavingClause.HavingPredicate, sr);
			}
			Dictionary<string, DbExpression> dictionary = null;
			if (queryExpr.OrderByClause != null || queryExpr.SelectClause.HasMethodCall)
			{
				dictionary = new Dictionary<string, DbExpression>(queryExpr.SelectClause.Items.Count, sr.NameComparer);
				for (int j = 0; j < queryExpr.SelectClause.Items.Count; j++)
				{
					AliasedExpr aliasedExpr2 = queryExpr.SelectClause.Items[j];
					DbExpression dbExpression2 = SemanticAnalyzer.ConvertValueExpression(aliasedExpr2.Expr, sr);
					dbExpression2 = ((dbExpression2.ExpressionKind == DbExpressionKind.Null) ? dbExpression2 : dbExpression2.ResultType.Null());
					string text2 = sr.InferAliasName(aliasedExpr2, dbExpression2);
					if (dictionary.ContainsKey(text2))
					{
						if (aliasedExpr2.Alias != null)
						{
							CqlErrorHelper.ReportAliasAlreadyUsedError(text2, aliasedExpr2.Alias.ErrCtx, Strings.InSelectProjectionList);
						}
						else
						{
							text2 = sr.GenerateInternalName("autoProject");
						}
					}
					dictionary.Add(text2, dbExpression2);
				}
			}
			if (queryExpr.OrderByClause != null && queryExpr.OrderByClause.HasMethodCall)
			{
				sr.EnterScope();
				foreach (KeyValuePair<string, DbExpression> keyValuePair in dictionary)
				{
					sr.CurrentScope.Add(keyValuePair.Key, new ProjectionItemDefinitionScopeEntry(keyValuePair.Value));
				}
				for (int k = 0; k < queryExpr.OrderByClause.OrderByClauseItem.Count; k++)
				{
					OrderByClauseItem orderByClauseItem = queryExpr.OrderByClause.OrderByClauseItem[k];
					sr.CurrentScopeRegion.WasResolutionCorrelated = false;
					SemanticAnalyzer.ConvertValueExpression(orderByClauseItem.OrderExpr, sr);
					if (!sr.CurrentScopeRegion.WasResolutionCorrelated)
					{
						ErrorContext errCtx3 = orderByClauseItem.ErrCtx;
						string errorMessage2 = Strings.KeyMustBeCorrelated("ORDER BY");
						throw EntitySqlException.Create(errCtx3, errorMessage2, null);
					}
				}
				sr.LeaveScope();
			}
			if (flag && sr.CurrentScopeRegion.GroupAggregateInfos.Count == 0)
			{
				sr.RollbackToScope(currentScopeIndex);
				sr.CurrentScopeRegion.ApplyToScopeEntries(delegate(ScopeEntry scopeEntry)
				{
					((SourceScopeEntry)scopeEntry).RollbackAdjustmentToGroupVar(source.Variable);
				});
				sr.CurrentScopeRegion.RollbackGroupOperation();
				return source;
			}
			List<KeyValuePair<string, DbAggregate>> list2 = new List<KeyValuePair<string, DbAggregate>>(sr.CurrentScopeRegion.GroupAggregateInfos.Count);
			bool flag2 = false;
			foreach (GroupAggregateInfo groupAggregateInfo3 in sr.CurrentScopeRegion.GroupAggregateInfos)
			{
				switch (groupAggregateInfo3.AggregateKind)
				{
				case GroupAggregateKind.Function:
					list2.Add(new KeyValuePair<string, DbAggregate>(groupAggregateInfo3.AggregateName, ((FunctionAggregateInfo)groupAggregateInfo3).AggregateDefinition));
					break;
				case GroupAggregateKind.Partition:
					flag2 = true;
					break;
				}
			}
			if (flag2)
			{
				list2.Add(new KeyValuePair<string, DbAggregate>(dbVariableReferenceExpression.VariableName, groupAggregate));
			}
			DbGroupByExpression input = groupInputBinding.GroupBy(from keyInfo in list
			select new KeyValuePair<string, DbExpression>(keyInfo.Name, keyInfo.VarBasedKeyExpr), list2);
			DbExpressionBinding groupBinding = input.BindAs(sr.GenerateInternalName("group"));
			if (flag2)
			{
				List<KeyValuePair<string, DbExpression>> list3 = SemanticAnalyzer.ProcessGroupPartitionDefinitions(sr.CurrentScopeRegion.GroupAggregateInfos, dbVariableReferenceExpression, groupBinding);
				if (list3 != null)
				{
					list3.AddRange(from keyInfo in list
					select new KeyValuePair<string, DbExpression>(keyInfo.Name, groupBinding.Variable.Property(keyInfo.Name)));
					list3.AddRange(from groupAggregateInfo in sr.CurrentScopeRegion.GroupAggregateInfos
					where groupAggregateInfo.AggregateKind == GroupAggregateKind.Function
					select new KeyValuePair<string, DbExpression>(groupAggregateInfo.AggregateName, groupBinding.Variable.Property(groupAggregateInfo.AggregateName)));
					DbExpression projection = DbExpressionBuilder.NewRow(list3);
					groupBinding = groupBinding.Project(projection).BindAs(sr.GenerateInternalName("groupPartitionDefs"));
				}
			}
			sr.RollbackToScope(currentScopeIndex);
			sr.CurrentScopeRegion.ApplyToScopeEntries((ScopeEntry scopeEntry) => new InvalidGroupInputRefScopeEntry());
			sr.EnterScope();
			foreach (SemanticAnalyzer.GroupKeyInfo groupKeyInfo3 in list)
			{
				sr.CurrentScope.Add(groupKeyInfo3.VarRef.VariableName, new SourceScopeEntry(groupKeyInfo3.VarRef).AddParentVar(groupBinding.Variable));
				if (groupKeyInfo3.AlternativeName != null)
				{
					string fullName3 = TypeResolver.GetFullName(groupKeyInfo3.AlternativeName);
					sr.CurrentScope.Add(fullName3, new SourceScopeEntry(groupKeyInfo3.VarRef, groupKeyInfo3.AlternativeName).AddParentVar(groupBinding.Variable));
				}
			}
			foreach (GroupAggregateInfo groupAggregateInfo2 in sr.CurrentScopeRegion.GroupAggregateInfos)
			{
				DbVariableReferenceExpression dbVariableReferenceExpression2 = groupAggregateInfo2.AggregateStubExpression.ResultType.Variable(groupAggregateInfo2.AggregateName);
				if (!sr.CurrentScope.Contains(dbVariableReferenceExpression2.VariableName))
				{
					sr.CurrentScope.Add(dbVariableReferenceExpression2.VariableName, new SourceScopeEntry(dbVariableReferenceExpression2).AddParentVar(groupBinding.Variable));
					sr.CurrentScopeRegion.RegisterGroupAggregateName(dbVariableReferenceExpression2.VariableName);
				}
				groupAggregateInfo2.AggregateStubExpression = null;
			}
			return groupBinding;
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00068154 File Offset: 0x00066354
		private static List<KeyValuePair<string, DbExpression>> ProcessGroupPartitionDefinitions(List<GroupAggregateInfo> groupAggregateInfos, DbVariableReferenceExpression groupAggregateVarRef, DbExpressionBinding groupBinding)
		{
			ReadOnlyCollection<DbVariableReferenceExpression> variables = new ReadOnlyCollection<DbVariableReferenceExpression>(new DbVariableReferenceExpression[]
			{
				groupAggregateVarRef
			});
			List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>();
			bool flag = false;
			foreach (GroupAggregateInfo groupAggregateInfo in groupAggregateInfos)
			{
				if (groupAggregateInfo.AggregateKind == GroupAggregateKind.Partition)
				{
					GroupPartitionInfo groupPartitionInfo = (GroupPartitionInfo)groupAggregateInfo;
					DbExpression aggregateDefinition = groupPartitionInfo.AggregateDefinition;
					if (SemanticAnalyzer.IsTrivialInputProjection(groupAggregateVarRef, aggregateDefinition))
					{
						groupAggregateInfo.AggregateName = groupAggregateVarRef.VariableName;
						flag = true;
					}
					else
					{
						DbLambda lambda = new DbLambda(variables, groupPartitionInfo.AggregateDefinition);
						list.Add(new KeyValuePair<string, DbExpression>(groupAggregateInfo.AggregateName, lambda.Invoke(new DbExpression[]
						{
							groupBinding.Variable.Property(groupAggregateVarRef.VariableName)
						})));
					}
				}
			}
			if (flag)
			{
				if (list.Count > 0)
				{
					list.Add(new KeyValuePair<string, DbExpression>(groupAggregateVarRef.VariableName, groupBinding.Variable.Property(groupAggregateVarRef.VariableName)));
				}
				else
				{
					list = null;
				}
			}
			return list;
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0006826C File Offset: 0x0006646C
		private static bool IsTrivialInputProjection(DbVariableReferenceExpression lambdaVariable, DbExpression lambdaBody)
		{
			if (lambdaBody.ExpressionKind != DbExpressionKind.Project)
			{
				return false;
			}
			DbProjectExpression dbProjectExpression = (DbProjectExpression)lambdaBody;
			if (dbProjectExpression.Input.Expression != lambdaVariable)
			{
				return false;
			}
			if (dbProjectExpression.Projection.ExpressionKind == DbExpressionKind.VariableReference)
			{
				DbVariableReferenceExpression dbVariableReferenceExpression = (DbVariableReferenceExpression)dbProjectExpression.Projection;
				return dbVariableReferenceExpression == dbProjectExpression.Input.Variable;
			}
			if (dbProjectExpression.Projection.ExpressionKind != DbExpressionKind.NewInstance || !TypeSemantics.IsRowType(dbProjectExpression.Projection.ResultType))
			{
				return false;
			}
			if (!TypeSemantics.IsEqual(dbProjectExpression.Projection.ResultType, dbProjectExpression.Input.Variable.ResultType))
			{
				return false;
			}
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(dbProjectExpression.Input.Variable.ResultType);
			DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)dbProjectExpression.Projection;
			for (int i = 0; i < dbNewInstanceExpression.Arguments.Count; i++)
			{
				if (dbNewInstanceExpression.Arguments[i].ExpressionKind != DbExpressionKind.Property)
				{
					return false;
				}
				DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)dbNewInstanceExpression.Arguments[i];
				if (dbPropertyExpression.Instance != dbProjectExpression.Input.Variable || dbPropertyExpression.Property != allStructuralMembers[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x000683F0 File Offset: 0x000665F0
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		private static DbExpressionBinding ProcessOrderByClause(DbExpressionBinding source, QueryExpr queryExpr, out bool queryProjectionProcessed, SemanticResolver sr)
		{
			queryProjectionProcessed = false;
			if (queryExpr.OrderByClause == null)
			{
				return source;
			}
			DbExpressionBinding sortBinding = null;
			OrderByClause orderByClause = queryExpr.OrderByClause;
			SelectClause selectClause = queryExpr.SelectClause;
			DbExpression dbExpression = null;
			if (orderByClause.SkipSubClause != null)
			{
				dbExpression = SemanticAnalyzer.ConvertValueExpression(orderByClause.SkipSubClause, sr);
				SemanticAnalyzer.ValidateExpressionIsCommandParamOrNonNegativeIntegerConstant(dbExpression, orderByClause.SkipSubClause.ErrCtx, "SKIP");
			}
			List<KeyValuePair<string, DbExpression>> list = SemanticAnalyzer.ConvertSelectClauseItems(queryExpr, sr);
			if (selectClause.DistinctKind == DistinctKind.Distinct)
			{
				sr.CurrentScopeRegion.RollbackAllScopes();
			}
			int currentScopeIndex = sr.CurrentScopeIndex;
			sr.EnterScope();
			list.Each((KeyValuePair<string, DbExpression> projectionItem) => sr.CurrentScope.Add(projectionItem.Key, new ProjectionItemDefinitionScopeEntry(projectionItem.Value)));
			if (selectClause.DistinctKind == DistinctKind.Distinct)
			{
				DbExpression input = SemanticAnalyzer.CreateProjectExpression(source, selectClause, list);
				source = input.BindAs(sr.GenerateInternalName("distinct"));
				if (selectClause.SelectKind == SelectKind.Value)
				{
					sr.CurrentScope.Replace(list[0].Key, new SourceScopeEntry(source.Variable));
				}
				else
				{
					foreach (KeyValuePair<string, DbExpression> keyValuePair in list)
					{
						DbVariableReferenceExpression dbVariableReferenceExpression = keyValuePair.Value.ResultType.Variable(keyValuePair.Key);
						sr.CurrentScope.Replace(dbVariableReferenceExpression.VariableName, new SourceScopeEntry(dbVariableReferenceExpression).AddParentVar(source.Variable));
					}
				}
				queryProjectionProcessed = true;
			}
			List<DbSortClause> list2 = new List<DbSortClause>(orderByClause.OrderByClauseItem.Count);
			for (int i = 0; i < orderByClause.OrderByClauseItem.Count; i++)
			{
				OrderByClauseItem orderByClauseItem = orderByClause.OrderByClauseItem[i];
				sr.CurrentScopeRegion.WasResolutionCorrelated = false;
				DbExpression dbExpression2 = SemanticAnalyzer.ConvertValueExpression(orderByClauseItem.OrderExpr, sr);
				if (!sr.CurrentScopeRegion.WasResolutionCorrelated)
				{
					ErrorContext errCtx = orderByClauseItem.ErrCtx;
					string errorMessage = Strings.KeyMustBeCorrelated("ORDER BY");
					throw EntitySqlException.Create(errCtx, errorMessage, null);
				}
				if (!TypeHelpers.IsValidSortOpKeyType(dbExpression2.ResultType))
				{
					ErrorContext errCtx2 = orderByClauseItem.OrderExpr.ErrCtx;
					string orderByKeyIsNotOrderComparable = Strings.OrderByKeyIsNotOrderComparable;
					throw EntitySqlException.Create(errCtx2, orderByKeyIsNotOrderComparable, null);
				}
				bool flag = orderByClauseItem.OrderKind == OrderKind.None || orderByClauseItem.OrderKind == OrderKind.Asc;
				string text = null;
				if (orderByClauseItem.Collation != null)
				{
					if (!SemanticAnalyzer.IsStringType(dbExpression2.ResultType))
					{
						ErrorContext errCtx3 = orderByClauseItem.OrderExpr.ErrCtx;
						string errorMessage2 = Strings.InvalidKeyTypeForCollation(dbExpression2.ResultType.EdmType.FullName);
						throw EntitySqlException.Create(errCtx3, errorMessage2, null);
					}
					text = orderByClauseItem.Collation.Name;
				}
				if (string.IsNullOrEmpty(text))
				{
					list2.Add(flag ? dbExpression2.ToSortClause() : dbExpression2.ToSortClauseDescending());
				}
				else
				{
					list2.Add(flag ? dbExpression2.ToSortClause(text) : dbExpression2.ToSortClauseDescending(text));
				}
			}
			sr.RollbackToScope(currentScopeIndex);
			DbExpression input2;
			if (dbExpression != null)
			{
				input2 = source.Skip(list2, dbExpression);
			}
			else
			{
				input2 = source.Sort(list2);
			}
			sortBinding = input2.BindAs(sr.GenerateInternalName("sort"));
			if (!queryProjectionProcessed)
			{
				sr.CurrentScopeRegion.ApplyToScopeEntries(delegate(ScopeEntry scopeEntry)
				{
					if (scopeEntry.EntryKind == ScopeEntryKind.SourceVar)
					{
						((SourceScopeEntry)scopeEntry).ReplaceParentVar(sortBinding.Variable);
					}
				});
			}
			return sortBinding;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x000687C0 File Offset: 0x000669C0
		private static DbExpression ConvertSimpleInExpression(DbExpression left, DbExpression right)
		{
			DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)right;
			if (dbNewInstanceExpression.Arguments.Count == 0)
			{
				return DbExpressionBuilder.False;
			}
			IEnumerable<DbComparisonExpression> collection = from arg in dbNewInstanceExpression.Arguments
			select left.Equal(arg);
			List<DbExpression> nodes = new List<DbExpression>(collection);
			return Helpers.BuildBalancedTreeInPlace<DbExpression>(nodes, (DbExpression prev, DbExpression next) => prev.Or(next));
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0006883B File Offset: 0x00066A3B
		private static bool IsStringType(TypeUsage type)
		{
			return TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.String);
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00068845 File Offset: 0x00066A45
		private static bool IsBooleanType(TypeUsage type)
		{
			return TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Boolean);
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0006884E File Offset: 0x00066A4E
		private static bool IsSubOrSuperType(TypeUsage type1, TypeUsage type2)
		{
			return TypeSemantics.IsStructurallyEqual(type1, type2) || type1.IsSubtypeOf(type2) || type2.IsSubtypeOf(type1);
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0006886C File Offset: 0x00066A6C
		private static Dictionary<Type, SemanticAnalyzer.AstExprConverter> CreateAstExprConverters()
		{
			return new Dictionary<Type, SemanticAnalyzer.AstExprConverter>(17)
			{
				{
					typeof(Literal),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertLiteral)
				},
				{
					typeof(QueryParameter),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertParameter)
				},
				{
					typeof(Identifier),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertIdentifier)
				},
				{
					typeof(DotExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertDotExpr)
				},
				{
					typeof(BuiltInExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertBuiltIn)
				},
				{
					typeof(QueryExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertQueryExpr)
				},
				{
					typeof(ParenExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertParenExpr)
				},
				{
					typeof(RowConstructorExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertRowConstructor)
				},
				{
					typeof(MultisetConstructorExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertMultisetConstructor)
				},
				{
					typeof(CaseExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertCaseExpr)
				},
				{
					typeof(RelshipNavigationExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertRelshipNavigationExpr)
				},
				{
					typeof(RefExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertRefExpr)
				},
				{
					typeof(DerefExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertDeRefExpr)
				},
				{
					typeof(MethodExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertMethodExpr)
				},
				{
					typeof(CreateRefExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertCreateRefExpr)
				},
				{
					typeof(KeyExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertKeyExpr)
				},
				{
					typeof(GroupPartitionExpr),
					new SemanticAnalyzer.AstExprConverter(SemanticAnalyzer.ConvertGroupPartitionExpr)
				}
			};
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00069AEC File Offset: 0x00067CEC
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
		private static Dictionary<BuiltInKind, SemanticAnalyzer.BuiltInExprConverter> CreateBuiltInExprConverter()
		{
			Dictionary<BuiltInKind, SemanticAnalyzer.BuiltInExprConverter> dictionary = new Dictionary<BuiltInKind, SemanticAnalyzer.BuiltInExprConverter>(4);
			dictionary.Add(BuiltInKind.Plus, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertPlusOperands(bltInExpr, sr);
				if (TypeSemantics.IsNumericType(pair.Left.ResultType))
				{
					return pair.Left.Plus(pair.Right);
				}
				MetadataFunctionGroup metadataFunctionGroup;
				if (!sr.TypeResolver.TryGetFunctionFromMetadata("Edm", "Concat", out metadataFunctionGroup))
				{
					ErrorContext errCtx = bltInExpr.ErrCtx;
					string concatBuiltinNotSupported = Strings.ConcatBuiltinNotSupported;
					throw EntitySqlException.Create(errCtx, concatBuiltinNotSupported, null);
				}
				List<TypeUsage> list = new List<TypeUsage>(2);
				list.Add(pair.Left.ResultType);
				list.Add(pair.Right.ResultType);
				bool flag = false;
				EdmFunction edmFunction = SemanticResolver.ResolveFunctionOverloads(metadataFunctionGroup.FunctionMetadata, list, false, out flag);
				if (edmFunction == null || flag)
				{
					ErrorContext errCtx2 = bltInExpr.ErrCtx;
					string concatBuiltinNotSupported2 = Strings.ConcatBuiltinNotSupported;
					throw EntitySqlException.Create(errCtx2, concatBuiltinNotSupported2, null);
				}
				return edmFunction.Invoke(new DbExpression[]
				{
					pair.Left,
					pair.Right
				});
			});
			dictionary.Add(BuiltInKind.Minus, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertArithmeticArgs(bltInExpr, sr);
				return pair.Left.Minus(pair.Right);
			});
			dictionary.Add(BuiltInKind.Multiply, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertArithmeticArgs(bltInExpr, sr);
				return pair.Left.Multiply(pair.Right);
			});
			dictionary.Add(BuiltInKind.Divide, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertArithmeticArgs(bltInExpr, sr);
				return pair.Left.Divide(pair.Right);
			});
			dictionary.Add(BuiltInKind.Modulus, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertArithmeticArgs(bltInExpr, sr);
				return pair.Left.Modulo(pair.Right);
			});
			dictionary.Add(BuiltInKind.UnaryMinus, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression left = SemanticAnalyzer.ConvertArithmeticArgs(bltInExpr, sr).Left;
				if (TypeSemantics.IsUnsignedNumericType(left.ResultType))
				{
					TypeUsage typeUsage = null;
					if (!TypeHelpers.TryGetClosestPromotableType(left.ResultType, out typeUsage))
					{
						string message = Strings.InvalidUnsignedTypeForUnaryMinusOperation(left.ResultType.EdmType.FullName);
						throw new EntitySqlException(message);
					}
				}
				return left.UnaryMinus();
			});
			dictionary.Add(BuiltInKind.UnaryPlus, (BuiltInExpr bltInExpr, SemanticResolver sr) => SemanticAnalyzer.ConvertArithmeticArgs(bltInExpr, sr).Left);
			dictionary.Add(BuiltInKind.And, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertLogicalArgs(bltInExpr, sr);
				return pair.Left.And(pair.Right);
			});
			dictionary.Add(BuiltInKind.Or, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertLogicalArgs(bltInExpr, sr);
				return pair.Left.Or(pair.Right);
			});
			dictionary.Add(BuiltInKind.Not, (BuiltInExpr bltInExpr, SemanticResolver sr) => SemanticAnalyzer.ConvertLogicalArgs(bltInExpr, sr).Left.Not());
			dictionary.Add(BuiltInKind.Equal, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertEqualCompArgs(bltInExpr, sr);
				return pair.Left.Equal(pair.Right);
			});
			dictionary.Add(BuiltInKind.NotEqual, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertEqualCompArgs(bltInExpr, sr);
				return pair.Left.Equal(pair.Right).Not();
			});
			dictionary.Add(BuiltInKind.GreaterEqual, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertOrderCompArgs(bltInExpr, sr);
				return pair.Left.GreaterThanOrEqual(pair.Right);
			});
			dictionary.Add(BuiltInKind.GreaterThan, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertOrderCompArgs(bltInExpr, sr);
				return pair.Left.GreaterThan(pair.Right);
			});
			dictionary.Add(BuiltInKind.LessEqual, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertOrderCompArgs(bltInExpr, sr);
				return pair.Left.LessThanOrEqual(pair.Right);
			});
			dictionary.Add(BuiltInKind.LessThan, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertOrderCompArgs(bltInExpr, sr);
				return pair.Left.LessThan(pair.Right);
			});
			dictionary.Add(BuiltInKind.Union, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr);
				return pair.Left.UnionAll(pair.Right).Distinct();
			});
			dictionary.Add(BuiltInKind.UnionAll, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr);
				return pair.Left.UnionAll(pair.Right);
			});
			dictionary.Add(BuiltInKind.Intersect, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr);
				return pair.Left.Intersect(pair.Right);
			});
			dictionary.Add(BuiltInKind.Overlaps, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr);
				return pair.Left.Intersect(pair.Right).IsEmpty().Not();
			});
			dictionary.Add(BuiltInKind.AnyElement, (BuiltInExpr bltInExpr, SemanticResolver sr) => SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr).Left.Element());
			dictionary.Add(BuiltInKind.Element, delegate(BuiltInExpr param0, SemanticResolver param1)
			{
				throw new NotSupportedException(Strings.ElementOperatorIsNotSupported);
			});
			dictionary.Add(BuiltInKind.Except, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr);
				return pair.Left.Except(pair.Right);
			});
			dictionary.Add(BuiltInKind.Exists, (BuiltInExpr bltInExpr, SemanticResolver sr) => SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr).Left.IsEmpty().Not());
			dictionary.Add(BuiltInKind.Flatten, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(bltInExpr.Arg1, sr);
				if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
				{
					ErrorContext errCtx = bltInExpr.Arg1.ErrCtx;
					string invalidFlattenArgument = Strings.InvalidFlattenArgument;
					throw EntitySqlException.Create(errCtx, invalidFlattenArgument, null);
				}
				if (!TypeSemantics.IsCollectionType(TypeHelpers.GetElementTypeUsage(dbExpression.ResultType)))
				{
					ErrorContext errCtx2 = bltInExpr.Arg1.ErrCtx;
					string invalidFlattenArgument2 = Strings.InvalidFlattenArgument;
					throw EntitySqlException.Create(errCtx2, invalidFlattenArgument2, null);
				}
				DbExpressionBinding dbExpressionBinding = dbExpression.BindAs(sr.GenerateInternalName("l_flatten"));
				DbExpressionBinding dbExpressionBinding2 = dbExpressionBinding.Variable.BindAs(sr.GenerateInternalName("r_flatten"));
				DbExpressionBinding dbExpressionBinding3 = dbExpressionBinding.CrossApply(dbExpressionBinding2).BindAs(sr.GenerateInternalName("flatten"));
				return dbExpressionBinding3.Project(dbExpressionBinding3.Variable.Property(dbExpressionBinding2.VariableName));
			});
			dictionary.Add(BuiltInKind.In, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertInExprArgs(bltInExpr, sr);
				if (pair.Right.ExpressionKind == DbExpressionKind.NewInstance)
				{
					return SemanticAnalyzer.ConvertSimpleInExpression(pair.Left, pair.Right);
				}
				DbExpressionBinding dbExpressionBinding = pair.Right.BindAs(sr.GenerateInternalName("in-filter"));
				DbExpression left = pair.Left;
				DbExpression variable = dbExpressionBinding.Variable;
				DbExpression right = dbExpressionBinding.Filter(left.Equal(variable)).IsEmpty().Not();
				DbExpression left2 = DbExpressionBuilder.Case(new List<DbExpression>(1)
				{
					left.IsNull()
				}, new List<DbExpression>(1)
				{
					TypeResolver.BooleanType.Null()
				}, DbExpressionBuilder.False);
				return left2.Or(right);
			});
			dictionary.Add(BuiltInKind.NotIn, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertInExprArgs(bltInExpr, sr);
				if (pair.Right.ExpressionKind == DbExpressionKind.NewInstance)
				{
					return SemanticAnalyzer.ConvertSimpleInExpression(pair.Left, pair.Right).Not();
				}
				DbExpressionBinding dbExpressionBinding = pair.Right.BindAs(sr.GenerateInternalName("in-filter"));
				DbExpression left = pair.Left;
				DbExpression variable = dbExpressionBinding.Variable;
				DbExpression right = dbExpressionBinding.Filter(left.Equal(variable)).IsEmpty();
				DbExpression left2 = DbExpressionBuilder.Case(new List<DbExpression>(1)
				{
					left.IsNull()
				}, new List<DbExpression>(1)
				{
					TypeResolver.BooleanType.Null()
				}, DbExpressionBuilder.True);
				return left2.And(right);
			});
			dictionary.Add(BuiltInKind.Distinct, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertSetArgs(bltInExpr, sr);
				return pair.Left.Distinct();
			});
			dictionary.Add(BuiltInKind.IsNull, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg1, sr);
				if (dbExpression != null && !TypeHelpers.IsValidIsNullOpType(dbExpression.ResultType))
				{
					ErrorContext errCtx = bltInExpr.Arg1.ErrCtx;
					string isNullInvalidType = Strings.IsNullInvalidType;
					throw EntitySqlException.Create(errCtx, isNullInvalidType, null);
				}
				if (dbExpression == null)
				{
					return DbExpressionBuilder.True;
				}
				return dbExpression.IsNull();
			});
			dictionary.Add(BuiltInKind.IsNotNull, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg1, sr);
				if (dbExpression != null && !TypeHelpers.IsValidIsNullOpType(dbExpression.ResultType))
				{
					ErrorContext errCtx = bltInExpr.Arg1.ErrCtx;
					string isNullInvalidType = Strings.IsNullInvalidType;
					throw EntitySqlException.Create(errCtx, isNullInvalidType, null);
				}
				if (dbExpression == null)
				{
					return DbExpressionBuilder.False;
				}
				return dbExpression.IsNull().Not();
			});
			dictionary.Add(BuiltInKind.IsOf, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(bltInExpr.Arg1, sr);
				TypeUsage typeUsage = SemanticAnalyzer.ConvertTypeName(bltInExpr.Arg2, sr);
				bool flag = (bool)((Literal)bltInExpr.Arg3).Value;
				bool flag2 = (bool)((Literal)bltInExpr.Arg4).Value;
				bool flag3 = sr.ParserOptions.ParserCompilationMode == ParserOptions.CompilationMode.RestrictedViewGenerationMode;
				if (!flag3 && !TypeSemantics.IsEntityType(dbExpression.ResultType))
				{
					ErrorContext errCtx = bltInExpr.Arg1.ErrCtx;
					string errorMessage = Strings.ExpressionTypeMustBeEntityType(Strings.CtxIsOf, dbExpression.ResultType.EdmType.BuiltInTypeKind.ToString(), dbExpression.ResultType.EdmType.FullName);
					throw EntitySqlException.Create(errCtx, errorMessage, null);
				}
				if (flag3 && !TypeSemantics.IsNominalType(dbExpression.ResultType))
				{
					ErrorContext errCtx2 = bltInExpr.Arg1.ErrCtx;
					string errorMessage2 = Strings.ExpressionTypeMustBeNominalType(Strings.CtxIsOf, dbExpression.ResultType.EdmType.BuiltInTypeKind.ToString(), dbExpression.ResultType.EdmType.FullName);
					throw EntitySqlException.Create(errCtx2, errorMessage2, null);
				}
				if (!flag3 && !TypeSemantics.IsEntityType(typeUsage))
				{
					ErrorContext errCtx3 = bltInExpr.Arg2.ErrCtx;
					string errorMessage3 = Strings.TypeMustBeEntityType(Strings.CtxIsOf, typeUsage.EdmType.BuiltInTypeKind.ToString(), typeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx3, errorMessage3, null);
				}
				if (flag3 && !TypeSemantics.IsNominalType(typeUsage))
				{
					ErrorContext errCtx4 = bltInExpr.Arg2.ErrCtx;
					string errorMessage4 = Strings.TypeMustBeNominalType(Strings.CtxIsOf, typeUsage.EdmType.BuiltInTypeKind.ToString(), typeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx4, errorMessage4, null);
				}
				if (!TypeSemantics.IsPolymorphicType(dbExpression.ResultType))
				{
					ErrorContext errCtx5 = bltInExpr.Arg1.ErrCtx;
					string typeMustBeInheritableType = Strings.TypeMustBeInheritableType;
					throw EntitySqlException.Create(errCtx5, typeMustBeInheritableType, null);
				}
				if (!TypeSemantics.IsPolymorphicType(typeUsage))
				{
					ErrorContext errCtx6 = bltInExpr.Arg2.ErrCtx;
					string typeMustBeInheritableType2 = Strings.TypeMustBeInheritableType;
					throw EntitySqlException.Create(errCtx6, typeMustBeInheritableType2, null);
				}
				if (!SemanticAnalyzer.IsSubOrSuperType(dbExpression.ResultType, typeUsage))
				{
					ErrorContext errCtx7 = bltInExpr.ErrCtx;
					string errorMessage5 = Strings.NotASuperOrSubType(dbExpression.ResultType.EdmType.FullName, typeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx7, errorMessage5, null);
				}
				typeUsage = TypeHelpers.GetReadOnlyType(typeUsage);
				DbExpression dbExpression2;
				if (flag)
				{
					dbExpression2 = dbExpression.IsOfOnly(typeUsage);
				}
				else
				{
					dbExpression2 = dbExpression.IsOf(typeUsage);
				}
				if (flag2)
				{
					dbExpression2 = dbExpression2.Not();
				}
				return dbExpression2;
			});
			dictionary.Add(BuiltInKind.Treat, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg1, sr);
				TypeUsage typeUsage = SemanticAnalyzer.ConvertTypeName(bltInExpr.Arg2, sr);
				bool flag = sr.ParserOptions.ParserCompilationMode == ParserOptions.CompilationMode.RestrictedViewGenerationMode;
				if (!flag && !TypeSemantics.IsEntityType(typeUsage))
				{
					ErrorContext errCtx = bltInExpr.Arg2.ErrCtx;
					string errorMessage = Strings.TypeMustBeEntityType(Strings.CtxTreat, typeUsage.EdmType.BuiltInTypeKind.ToString(), typeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx, errorMessage, null);
				}
				if (flag && !TypeSemantics.IsNominalType(typeUsage))
				{
					ErrorContext errCtx2 = bltInExpr.Arg2.ErrCtx;
					string errorMessage2 = Strings.TypeMustBeNominalType(Strings.CtxTreat, typeUsage.EdmType.BuiltInTypeKind.ToString(), typeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx2, errorMessage2, null);
				}
				if (dbExpression == null)
				{
					dbExpression = typeUsage.Null();
				}
				else
				{
					if (!flag && !TypeSemantics.IsEntityType(dbExpression.ResultType))
					{
						ErrorContext errCtx3 = bltInExpr.Arg1.ErrCtx;
						string errorMessage3 = Strings.ExpressionTypeMustBeEntityType(Strings.CtxTreat, dbExpression.ResultType.EdmType.BuiltInTypeKind.ToString(), dbExpression.ResultType.EdmType.FullName);
						throw EntitySqlException.Create(errCtx3, errorMessage3, null);
					}
					if (flag && !TypeSemantics.IsNominalType(dbExpression.ResultType))
					{
						ErrorContext errCtx4 = bltInExpr.Arg1.ErrCtx;
						string errorMessage4 = Strings.ExpressionTypeMustBeNominalType(Strings.CtxTreat, dbExpression.ResultType.EdmType.BuiltInTypeKind.ToString(), dbExpression.ResultType.EdmType.FullName);
						throw EntitySqlException.Create(errCtx4, errorMessage4, null);
					}
				}
				if (!TypeSemantics.IsPolymorphicType(dbExpression.ResultType))
				{
					ErrorContext errCtx5 = bltInExpr.Arg1.ErrCtx;
					string typeMustBeInheritableType = Strings.TypeMustBeInheritableType;
					throw EntitySqlException.Create(errCtx5, typeMustBeInheritableType, null);
				}
				if (!TypeSemantics.IsPolymorphicType(typeUsage))
				{
					ErrorContext errCtx6 = bltInExpr.Arg2.ErrCtx;
					string typeMustBeInheritableType2 = Strings.TypeMustBeInheritableType;
					throw EntitySqlException.Create(errCtx6, typeMustBeInheritableType2, null);
				}
				if (!SemanticAnalyzer.IsSubOrSuperType(dbExpression.ResultType, typeUsage))
				{
					ErrorContext errCtx7 = bltInExpr.Arg1.ErrCtx;
					string errorMessage5 = Strings.NotASuperOrSubType(dbExpression.ResultType.EdmType.FullName, typeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx7, errorMessage5, null);
				}
				return dbExpression.TreatAs(TypeHelpers.GetReadOnlyType(typeUsage));
			});
			dictionary.Add(BuiltInKind.Cast, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg1, sr);
				TypeUsage typeUsage = SemanticAnalyzer.ConvertTypeName(bltInExpr.Arg2, sr);
				if (!TypeSemantics.IsScalarType(typeUsage))
				{
					ErrorContext errCtx = bltInExpr.Arg2.ErrCtx;
					string invalidCastType = Strings.InvalidCastType;
					throw EntitySqlException.Create(errCtx, invalidCastType, null);
				}
				if (dbExpression == null)
				{
					return typeUsage.Null();
				}
				if (!TypeSemantics.IsScalarType(dbExpression.ResultType))
				{
					ErrorContext errCtx2 = bltInExpr.Arg1.ErrCtx;
					string invalidCastExpressionType = Strings.InvalidCastExpressionType;
					throw EntitySqlException.Create(errCtx2, invalidCastExpressionType, null);
				}
				if (!TypeSemantics.IsCastAllowed(dbExpression.ResultType, typeUsage))
				{
					ErrorContext errCtx3 = bltInExpr.Arg1.ErrCtx;
					string errorMessage = Strings.InvalidCast(dbExpression.ResultType.EdmType.FullName, typeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx3, errorMessage, null);
				}
				return dbExpression.CastTo(TypeHelpers.GetReadOnlyType(typeUsage));
			});
			dictionary.Add(BuiltInKind.OfType, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(bltInExpr.Arg1, sr);
				TypeUsage typeUsage = SemanticAnalyzer.ConvertTypeName(bltInExpr.Arg2, sr);
				bool flag = (bool)((Literal)bltInExpr.Arg3).Value;
				bool flag2 = sr.ParserOptions.ParserCompilationMode == ParserOptions.CompilationMode.RestrictedViewGenerationMode;
				if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
				{
					ErrorContext errCtx = bltInExpr.Arg1.ErrCtx;
					string expressionMustBeCollection = Strings.ExpressionMustBeCollection;
					throw EntitySqlException.Create(errCtx, expressionMustBeCollection, null);
				}
				TypeUsage elementTypeUsage = TypeHelpers.GetElementTypeUsage(dbExpression.ResultType);
				if (!flag2 && !TypeSemantics.IsEntityType(elementTypeUsage))
				{
					ErrorContext errCtx2 = bltInExpr.Arg1.ErrCtx;
					string errorMessage = Strings.OfTypeExpressionElementTypeMustBeEntityType(elementTypeUsage.EdmType.BuiltInTypeKind.ToString(), elementTypeUsage);
					throw EntitySqlException.Create(errCtx2, errorMessage, null);
				}
				if (flag2 && !TypeSemantics.IsNominalType(elementTypeUsage))
				{
					ErrorContext errCtx3 = bltInExpr.Arg1.ErrCtx;
					string errorMessage2 = Strings.OfTypeExpressionElementTypeMustBeNominalType(elementTypeUsage.EdmType.BuiltInTypeKind.ToString(), elementTypeUsage);
					throw EntitySqlException.Create(errCtx3, errorMessage2, null);
				}
				if (!flag2 && !TypeSemantics.IsEntityType(typeUsage))
				{
					ErrorContext errCtx4 = bltInExpr.Arg2.ErrCtx;
					string errorMessage3 = Strings.TypeMustBeEntityType(Strings.CtxOfType, typeUsage.EdmType.BuiltInTypeKind.ToString(), typeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx4, errorMessage3, null);
				}
				if (flag2 && !TypeSemantics.IsNominalType(typeUsage))
				{
					ErrorContext errCtx5 = bltInExpr.Arg2.ErrCtx;
					string errorMessage4 = Strings.TypeMustBeNominalType(Strings.CtxOfType, typeUsage.EdmType.BuiltInTypeKind.ToString(), typeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx5, errorMessage4, null);
				}
				if (flag && typeUsage.EdmType.Abstract)
				{
					ErrorContext errCtx6 = bltInExpr.Arg2.ErrCtx;
					string errorMessage5 = Strings.OfTypeOnlyTypeArgumentCannotBeAbstract(typeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx6, errorMessage5, null);
				}
				if (!SemanticAnalyzer.IsSubOrSuperType(elementTypeUsage, typeUsage))
				{
					ErrorContext errCtx7 = bltInExpr.Arg1.ErrCtx;
					string errorMessage6 = Strings.NotASuperOrSubType(elementTypeUsage.EdmType.FullName, typeUsage.EdmType.FullName);
					throw EntitySqlException.Create(errCtx7, errorMessage6, null);
				}
				DbExpression result;
				if (flag)
				{
					result = dbExpression.OfTypeOnly(TypeHelpers.GetReadOnlyType(typeUsage));
				}
				else
				{
					result = dbExpression.OfType(TypeHelpers.GetReadOnlyType(typeUsage));
				}
				return result;
			});
			dictionary.Add(BuiltInKind.Like, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg1, sr);
				if (dbExpression == null)
				{
					dbExpression = TypeResolver.StringType.Null();
				}
				else if (!SemanticAnalyzer.IsStringType(dbExpression.ResultType))
				{
					ErrorContext errCtx = bltInExpr.Arg1.ErrCtx;
					string likeArgMustBeStringType = Strings.LikeArgMustBeStringType;
					throw EntitySqlException.Create(errCtx, likeArgMustBeStringType, null);
				}
				DbExpression dbExpression2 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg2, sr);
				if (dbExpression2 == null)
				{
					dbExpression2 = TypeResolver.StringType.Null();
				}
				else if (!SemanticAnalyzer.IsStringType(dbExpression2.ResultType))
				{
					ErrorContext errCtx2 = bltInExpr.Arg2.ErrCtx;
					string likeArgMustBeStringType2 = Strings.LikeArgMustBeStringType;
					throw EntitySqlException.Create(errCtx2, likeArgMustBeStringType2, null);
				}
				DbExpression result;
				if (3 == bltInExpr.ArgCount)
				{
					DbExpression dbExpression3 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg3, sr);
					if (dbExpression3 == null)
					{
						dbExpression3 = TypeResolver.StringType.Null();
					}
					else if (!SemanticAnalyzer.IsStringType(dbExpression3.ResultType))
					{
						ErrorContext errCtx3 = bltInExpr.Arg3.ErrCtx;
						string likeArgMustBeStringType3 = Strings.LikeArgMustBeStringType;
						throw EntitySqlException.Create(errCtx3, likeArgMustBeStringType3, null);
					}
					result = dbExpression.Like(dbExpression2, dbExpression3);
				}
				else
				{
					result = dbExpression.Like(dbExpression2);
				}
				return result;
			});
			dictionary.Add(BuiltInKind.Between, new SemanticAnalyzer.BuiltInExprConverter(SemanticAnalyzer.ConvertBetweenExpr));
			dictionary.Add(BuiltInKind.NotBetween, (BuiltInExpr bltInExpr, SemanticResolver sr) => SemanticAnalyzer.ConvertBetweenExpr(bltInExpr, sr).Not());
			return dictionary;
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0006A048 File Offset: 0x00068248
		private static DbExpression ConvertBetweenExpr(BuiltInExpr bltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(bltInExpr.Arg2, bltInExpr.Arg3, bltInExpr.Arg1.ErrCtx, () => Strings.BetweenLimitsCannotBeUntypedNulls, sr);
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(pair.Left.ResultType, pair.Right.ResultType);
			if (commonTypeUsage == null)
			{
				ErrorContext errCtx = bltInExpr.Arg1.ErrCtx;
				string errorMessage = Strings.BetweenLimitsTypesAreNotCompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			if (!TypeSemantics.IsOrderComparableTo(pair.Left.ResultType, pair.Right.ResultType))
			{
				ErrorContext errCtx2 = bltInExpr.Arg1.ErrCtx;
				string errorMessage2 = Strings.BetweenLimitsTypesAreNotOrderComparable(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx2, errorMessage2, null);
			}
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg1, sr);
			if (dbExpression == null)
			{
				dbExpression = commonTypeUsage.Null();
			}
			if (!TypeSemantics.IsOrderComparableTo(dbExpression.ResultType, commonTypeUsage))
			{
				ErrorContext errCtx3 = bltInExpr.Arg1.ErrCtx;
				string errorMessage3 = Strings.BetweenValueIsNotOrderComparable(dbExpression.ResultType.EdmType.FullName, commonTypeUsage.EdmType.FullName);
				throw EntitySqlException.Create(errCtx3, errorMessage3, null);
			}
			return dbExpression.GreaterThanOrEqual(pair.Left).And(dbExpression.LessThanOrEqual(pair.Right));
		}

		// Token: 0x0400075B RID: 1883
		private readonly SemanticResolver _sr;

		// Token: 0x0400075C RID: 1884
		private static readonly DbExpressionKind[] _joinMap = new DbExpressionKind[]
		{
			DbExpressionKind.CrossJoin,
			DbExpressionKind.InnerJoin,
			DbExpressionKind.LeftOuterJoin,
			DbExpressionKind.FullOuterJoin
		};

		// Token: 0x0400075D RID: 1885
		private static readonly DbExpressionKind[] _applyMap = new DbExpressionKind[]
		{
			DbExpressionKind.CrossApply,
			DbExpressionKind.OuterApply
		};

		// Token: 0x0400075E RID: 1886
		private static readonly Dictionary<Type, SemanticAnalyzer.AstExprConverter> _astExprConverters = SemanticAnalyzer.CreateAstExprConverters();

		// Token: 0x0400075F RID: 1887
		private static readonly Dictionary<BuiltInKind, SemanticAnalyzer.BuiltInExprConverter> _builtInExprConverter = SemanticAnalyzer.CreateBuiltInExprConverter();

		// Token: 0x0200026B RID: 619
		// (Invoke) Token: 0x060015B6 RID: 5558
		private delegate ParseResult StatementConverter(Statement astExpr, SemanticResolver sr);

		// Token: 0x0200026C RID: 620
		private sealed class InlineFunctionInfoImpl : InlineFunctionInfo
		{
			// Token: 0x060015B9 RID: 5561 RVA: 0x0006A237 File Offset: 0x00068437
			internal InlineFunctionInfoImpl(FunctionDefinition functionDef, List<DbVariableReferenceExpression> parameters) : base(functionDef, parameters)
			{
			}

			// Token: 0x060015BA RID: 5562 RVA: 0x0006A244 File Offset: 0x00068444
			internal override DbLambda GetLambda(SemanticResolver sr)
			{
				if (this._convertedDefinition == null)
				{
					if (this._convertingDefinition)
					{
						ErrorContext errCtx = this.FunctionDefAst.ErrCtx;
						string errorMessage = Strings.Cqt_UDF_FunctionDefinitionWithCircularReference(this.FunctionDefAst.Name);
						throw EntitySqlException.Create(errCtx, errorMessage, null);
					}
					SemanticResolver sr2 = sr.CloneForInlineFunctionConversion();
					this._convertingDefinition = true;
					this._convertedDefinition = SemanticAnalyzer.ConvertInlineFunctionDefinition(this, sr2);
					this._convertingDefinition = false;
				}
				return this._convertedDefinition;
			}

			// Token: 0x040007A1 RID: 1953
			private DbLambda _convertedDefinition;

			// Token: 0x040007A2 RID: 1954
			private bool _convertingDefinition;
		}

		// Token: 0x0200026D RID: 621
		private sealed class GroupKeyInfo
		{
			// Token: 0x060015BB RID: 5563 RVA: 0x0006A2AF File Offset: 0x000684AF
			internal GroupKeyInfo(string name, DbExpression varBasedKeyExpr, DbExpression groupVarBasedKeyExpr, DbExpression groupAggBasedKeyExpr)
			{
				this.Name = name;
				this.VarRef = varBasedKeyExpr.ResultType.Variable(name);
				this.VarBasedKeyExpr = varBasedKeyExpr;
				this.GroupVarBasedKeyExpr = groupVarBasedKeyExpr;
				this.GroupAggBasedKeyExpr = groupAggBasedKeyExpr;
			}

			// Token: 0x17000277 RID: 631
			// (get) Token: 0x060015BC RID: 5564 RVA: 0x0006A2E6 File Offset: 0x000684E6
			// (set) Token: 0x060015BD RID: 5565 RVA: 0x0006A2EE File Offset: 0x000684EE
			internal string[] AlternativeName
			{
				get
				{
					return this._alternativeName;
				}
				set
				{
					this._alternativeName = value;
				}
			}

			// Token: 0x040007A3 RID: 1955
			internal readonly string Name;

			// Token: 0x040007A4 RID: 1956
			private string[] _alternativeName;

			// Token: 0x040007A5 RID: 1957
			internal readonly DbVariableReferenceExpression VarRef;

			// Token: 0x040007A6 RID: 1958
			internal readonly DbExpression VarBasedKeyExpr;

			// Token: 0x040007A7 RID: 1959
			internal readonly DbExpression GroupVarBasedKeyExpr;

			// Token: 0x040007A8 RID: 1960
			internal readonly DbExpression GroupAggBasedKeyExpr;
		}

		// Token: 0x0200026E RID: 622
		// (Invoke) Token: 0x060015BF RID: 5567
		private delegate ExpressionResolution AstExprConverter(Node astExpr, SemanticResolver sr);

		// Token: 0x0200026F RID: 623
		// (Invoke) Token: 0x060015C3 RID: 5571
		private delegate DbExpression BuiltInExprConverter(BuiltInExpr astBltInExpr, SemanticResolver sr);
	}
}
