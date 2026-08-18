using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.EntitySql.AST;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000334 RID: 820
	internal sealed class SemanticAnalyzer
	{
		// Token: 0x060030A0 RID: 12448 RVA: 0x000BC383 File Offset: 0x000BA583
		internal SemanticAnalyzer(SemanticResolver sr)
		{
			this._sr = sr;
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000BC394 File Offset: 0x000BA594
		internal ParseResult AnalyzeCommand(Node astExpr)
		{
			Command command = this.ValidateQueryCommandAst(astExpr);
			SemanticAnalyzer.ConvertAndRegisterNamespaceImports(command.NamespaceImportList, command.ErrCtx, this._sr);
			return SemanticAnalyzer.ConvertStatement(command.Statement, this._sr);
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000BC3D4 File Offset: 0x000BA5D4
		internal DbLambda AnalyzeQueryCommand(Node astExpr)
		{
			Command command = this.ValidateQueryCommandAst(astExpr);
			SemanticAnalyzer.ConvertAndRegisterNamespaceImports(command.NamespaceImportList, command.ErrCtx, this._sr);
			List<FunctionDefinition> list;
			DbExpression body = SemanticAnalyzer.ConvertQueryStatementToDbExpression(command.Statement, this._sr, out list);
			return DbExpressionBuilder.Lambda(body, this._sr.Variables.Values);
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000BC42C File Offset: 0x000BA62C
		private Command ValidateQueryCommandAst(Node astExpr)
		{
			Command command = astExpr as Command;
			if (command == null)
			{
				throw EntityUtil.Argument(Strings.UnknownAstCommandExpression);
			}
			if (!(command.Statement is QueryStatement))
			{
				throw EntityUtil.Argument(Strings.UnknownAstExpressionType);
			}
			return command;
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000BC468 File Offset: 0x000BA668
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
						throw EntityUtil.EntitySqlError(namespaceImport.NamespaceName.ErrCtx, Strings.InvalidMetadataMemberName);
					}
					string text = (namespaceImport.Alias != null) ? namespaceImport.Alias.Name : null;
					MetadataMember metadataMember = sr.ResolveMetadataMemberName(array, namespaceImport.NamespaceName.ErrCtx);
					if (metadataMember.MetadataMemberClass != MetadataMemberClass.Namespace)
					{
						throw EntityUtil.EntitySqlError(namespaceImport.NamespaceName.ErrCtx, Strings.InvalidMetadataMemberClassResolution(metadataMember.Name, metadataMember.MetadataMemberClassName, MetadataNamespace.NamespaceClassName));
					}
					if (text != null)
					{
						list.Add(Tuple.Create<string, MetadataNamespace, ErrorContext>(text, (MetadataNamespace)metadataMember, namespaceImport.ErrCtx));
					}
					else
					{
						list2.Add(Tuple.Create<MetadataNamespace, ErrorContext>((MetadataNamespace)metadataMember, namespaceImport.ErrCtx));
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

		// Token: 0x060030A5 RID: 12453 RVA: 0x000BC6A4 File Offset: 0x000BA8A4
		private static ParseResult ConvertStatement(Statement astStatement, SemanticResolver sr)
		{
			if (astStatement is QueryStatement)
			{
				SemanticAnalyzer.StatementConverter statementConverter = new SemanticAnalyzer.StatementConverter(SemanticAnalyzer.ConvertQueryStatementToDbCommandTree);
				return statementConverter(astStatement, sr);
			}
			throw EntityUtil.Argument(Strings.UnknownAstExpressionType);
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000BC6E0 File Offset: 0x000BA8E0
		private static ParseResult ConvertQueryStatementToDbCommandTree(Statement astStatement, SemanticResolver sr)
		{
			List<FunctionDefinition> functionDefs;
			DbExpression query = SemanticAnalyzer.ConvertQueryStatementToDbExpression(astStatement, sr, out functionDefs);
			return new ParseResult(DbQueryCommandTree.FromValidExpression(sr.TypeResolver.Perspective.MetadataWorkspace, sr.TypeResolver.Perspective.TargetDataspace, query), functionDefs);
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000BC724 File Offset: 0x000BA924
		private static DbExpression ConvertQueryStatementToDbExpression(Statement astStatement, SemanticResolver sr, out List<FunctionDefinition> functionDefs)
		{
			QueryStatement queryStatement = astStatement as QueryStatement;
			if (queryStatement == null)
			{
				throw EntityUtil.Argument(Strings.UnknownAstExpressionType);
			}
			functionDefs = SemanticAnalyzer.ConvertInlineFunctionDefinitions(queryStatement.FunctionDefList, sr);
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(queryStatement.Expr, sr);
			if (dbExpression == null)
			{
				throw EntityUtil.EntitySqlError(queryStatement.Expr.ErrCtx, Strings.ResultingExpressionTypeCannotBeNull);
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

		// Token: 0x060030A8 RID: 12456 RVA: 0x000BC7C8 File Offset: 0x000BA9C8
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
				throw EntityUtil.EntitySqlError(errCtx, Strings.InvalidQueryResultType(resultType.EdmType.FullName));
			}
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000BC880 File Offset: 0x000BAA80
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

		// Token: 0x060030AA RID: 12458 RVA: 0x000BC97C File Offset: 0x000BAB7C
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
						throw EntityUtil.EntitySqlError(propDefinition.ErrCtx, Strings.MultipleDefinitionsOfParameter(name));
					}
					TypeUsage type = SemanticAnalyzer.ConvertTypeDefinition(propDefinition.Type, sr);
					DbVariableReferenceExpression item = new DbVariableReferenceExpression(type, name);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000BCA5C File Offset: 0x000BAC5C
		private static DbLambda ConvertInlineFunctionDefinition(InlineFunctionInfo functionInfo, SemanticResolver sr)
		{
			sr.EnterScope();
			functionInfo.Parameters.ForEach(delegate(DbVariableReferenceExpression p)
			{
				sr.CurrentScope.Add(p.VariableName, new FreeVariableScopeEntry(p));
			});
			DbExpression body = SemanticAnalyzer.ConvertValueExpression(functionInfo.FunctionDefAst.Body, sr);
			sr.LeaveScope();
			return DbExpressionBuilder.Lambda(body, functionInfo.Parameters);
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000BCAC8 File Offset: 0x000BACC8
		private static ExpressionResolution Convert(Node astExpr, SemanticResolver sr)
		{
			SemanticAnalyzer.AstExprConverter astExprConverter = SemanticAnalyzer._astExprConverters[astExpr.GetType()];
			if (astExprConverter == null)
			{
				throw EntityUtil.EntitySqlError(Strings.UnknownAstExpressionType);
			}
			return astExprConverter(astExpr, sr);
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000BCAFC File Offset: 0x000BACFC
		private static DbExpression ConvertValueExpression(Node astExpr, SemanticResolver sr)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(astExpr, sr);
			if (dbExpression == null)
			{
				throw EntityUtil.EntitySqlError(astExpr.ErrCtx, Strings.ExpressionCannotBeNull);
			}
			return dbExpression;
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000BCB28 File Offset: 0x000BAD28
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
			string message = Strings.InvalidExpressionResolutionClass(expressionResolution.ExpressionClassName, ValueExpression.ValueClassName);
			Identifier identifier = astExpr as Identifier;
			if (identifier != null)
			{
				message = Strings.CouldNotResolveIdentifier(identifier.Name);
			}
			DotExpr dotExpr = astExpr as DotExpr;
			string[] names;
			if (dotExpr != null && dotExpr.IsMultipartIdentifier(out names))
			{
				message = Strings.CouldNotResolveIdentifier(TypeResolver.GetFullName(names));
			}
			throw EntityUtil.EntitySqlError(astExpr.ErrCtx, message);
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000BCBE4 File Offset: 0x000BADE4
		private static Pair<DbExpression, DbExpression> ConvertValueExpressionsWithUntypedNulls(Node leftAst, Node rightAst, ErrorContext errCtx, Func<string> formatMessage, SemanticResolver sr)
		{
			DbExpression dbExpression = (leftAst != null) ? SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(leftAst, sr) : null;
			DbExpression dbExpression2 = (rightAst != null) ? SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(rightAst, sr) : null;
			if (dbExpression == null)
			{
				if (dbExpression2 == null)
				{
					throw EntityUtil.EntitySqlError(errCtx, formatMessage());
				}
				dbExpression = dbExpression2.ResultType.Null();
			}
			else if (dbExpression2 == null)
			{
				dbExpression2 = dbExpression.ResultType.Null();
			}
			return new Pair<DbExpression, DbExpression>(dbExpression, dbExpression2);
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000BCC48 File Offset: 0x000BAE48
		private static ExpressionResolution ConvertLiteral(Node expr, SemanticResolver sr)
		{
			Literal literal = (Literal)expr;
			if (literal.IsNullLiteral)
			{
				return new ValueExpression(null);
			}
			return new ValueExpression(SemanticAnalyzer.GetLiteralTypeUsage(literal).Constant(literal.Value));
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000BCC84 File Offset: 0x000BAE84
		private static TypeUsage GetLiteralTypeUsage(Literal literal)
		{
			PrimitiveType primitiveType = null;
			if (!ClrProviderManifest.Instance.TryGetPrimitiveType(literal.Type, out primitiveType))
			{
				throw EntityUtil.EntitySqlError(literal.ErrCtx, Strings.LiteralTypeNotFoundInMetadata(literal.OriginalValue));
			}
			return TypeHelpers.GetLiteralTypeUsage(primitiveType.PrimitiveTypeKind, literal.IsUnicodeString);
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000BCCD1 File Offset: 0x000BAED1
		private static ExpressionResolution ConvertIdentifier(Node expr, SemanticResolver sr)
		{
			return SemanticAnalyzer.ConvertIdentifier((Identifier)expr, false, sr);
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000BCCE0 File Offset: 0x000BAEE0
		private static ExpressionResolution ConvertIdentifier(Identifier identifier, bool leftHandSideOfMemberAccess, SemanticResolver sr)
		{
			return sr.ResolveSimpleName(identifier.Name, leftHandSideOfMemberAccess, identifier.ErrCtx);
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000BCCF8 File Offset: 0x000BAEF8
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
				throw EntityUtil.EntitySqlError(dotExpr.Left.ErrCtx, Strings.UnknownExpressionResolutionClass(expressionResolution.ExpressionClass));
			}
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000BCDF0 File Offset: 0x000BAFF0
		private static ExpressionResolution ConvertParenExpr(Node astExpr, SemanticResolver sr)
		{
			Node expr = ((ParenExpr)astExpr).Expr;
			DbExpression value = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(expr, sr);
			return new ValueExpression(value);
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000BCE18 File Offset: 0x000BB018
		private static ExpressionResolution ConvertGroupPartitionExpr(Node astExpr, SemanticResolver sr)
		{
			GroupPartitionExpr groupPartitionExpr = (GroupPartitionExpr)astExpr;
			DbExpression value = null;
			if (!SemanticAnalyzer.TryConvertAsResolvedGroupAggregate(groupPartitionExpr, sr, out value))
			{
				if (!sr.IsInAnyGroupScope())
				{
					throw EntityUtil.EntitySqlError(astExpr.ErrCtx, Strings.GroupPartitionOutOfContext);
				}
				GroupPartitionInfo groupPartitionInfo;
				DbExpression dbExpression;
				using (sr.EnterGroupPartition(groupPartitionExpr, groupPartitionExpr.ErrCtx, out groupPartitionInfo))
				{
					dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(groupPartitionExpr.ArgExpr, sr);
				}
				if (dbExpression == null)
				{
					throw EntityUtil.EntitySqlError(groupPartitionExpr.ArgExpr.ErrCtx, Strings.ResultingExpressionTypeCannotBeNull);
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

		// Token: 0x060030B7 RID: 12471 RVA: 0x000BCF18 File Offset: 0x000BB118
		private static ExpressionResolution ConvertMethodExpr(Node expr, SemanticResolver sr)
		{
			return SemanticAnalyzer.ConvertMethodExpr((MethodExpr)expr, true, sr);
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000BCF28 File Offset: 0x000BB128
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
				throw EntityUtil.EntitySqlError(methodExpr.ErrCtx, Strings.MethodInvocationNotSupported);
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
				MetadataMemberClass metadataMemberClass = metadataMember.MetadataMemberClass;
				if (metadataMemberClass == MetadataMemberClass.Type)
				{
					methodExpr.ErrCtx.ErrorContextInfo = Strings.CtxTypeCtor(metadataMember.Name);
					methodExpr.ErrCtx.UseContextInfoAsResourceIdentifier = false;
					return SemanticAnalyzer.ConvertTypeConstructorCall((MetadataType)metadataMember, methodExpr, sr);
				}
				if (metadataMemberClass != MetadataMemberClass.FunctionGroup)
				{
					throw EntityUtil.EntitySqlError(methodExpr.Expr.ErrCtx, Strings.CannotResolveNameToTypeOrFunction(metadataMember.Name));
				}
				methodExpr.ErrCtx.ErrorContextInfo = Strings.CtxFunction(metadataMember.Name);
				methodExpr.ErrCtx.UseContextInfoAsResourceIdentifier = false;
				return SemanticAnalyzer.ConvertModelFunctionCall((MetadataFunctionGroup)metadataMember, methodExpr, sr);
			}
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000BD0E4 File Offset: 0x000BB2E4
		private static IDisposable ConvertMethodExpr_TryEnterIgnoreEntityContainerNameResolution(DotExpr leftExpr, SemanticResolver sr)
		{
			if (leftExpr == null || !(leftExpr.Left is Identifier))
			{
				return null;
			}
			return sr.EnterIgnoreEntityContainerNameResolution();
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000BD100 File Offset: 0x000BB300
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

		// Token: 0x060030BB RID: 12475 RVA: 0x000BD174 File Offset: 0x000BB374
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
				throw EntityUtil.EntitySqlError(methodExpr.ErrCtx, Strings.AmbiguousFunctionArguments);
			}
			if (inlineFunctionInfo == null)
			{
				return false;
			}
			SemanticAnalyzer.ConvertUntypedNullsInArguments<DbVariableReferenceExpression>(list, inlineFunctionInfo.Parameters, (DbVariableReferenceExpression formal) => formal.ResultType);
			inlineFunctionCall = new ValueExpression(inlineFunctionInfo.GetLambda(sr).Invoke(list));
			return true;
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000BD268 File Offset: 0x000BB468
		private static ValueExpression ConvertTypeConstructorCall(MetadataType metadataType, MethodExpr methodExpr, SemanticResolver sr)
		{
			if (!TypeSemantics.IsComplexType(metadataType.TypeUsage) && !TypeSemantics.IsEntityType(metadataType.TypeUsage) && !TypeSemantics.IsRelationshipType(metadataType.TypeUsage))
			{
				throw EntityUtil.EntitySqlError(methodExpr.ErrCtx, Strings.InvalidCtorUseOnType(metadataType.TypeUsage.EdmType.FullName));
			}
			if (metadataType.TypeUsage.EdmType.Abstract)
			{
				throw EntityUtil.EntitySqlError(methodExpr.ErrCtx, Strings.CannotInstantiateAbstractType(metadataType.TypeUsage.EdmType.FullName));
			}
			if (methodExpr.DistinctKind != DistinctKind.None)
			{
				throw EntityUtil.EntitySqlError(methodExpr.ErrCtx, Strings.InvalidDistinctArgumentInCtor);
			}
			List<DbRelatedEntityRef> list = null;
			if (methodExpr.HasRelationships)
			{
				if (sr.ParserOptions.ParserCompilationMode != ParserOptions.CompilationMode.RestrictedViewGenerationMode && sr.ParserOptions.ParserCompilationMode != ParserOptions.CompilationMode.UserViewGenerationMode)
				{
					throw EntityUtil.EntitySqlError(methodExpr.Relationships.ErrCtx, Strings.InvalidModeForWithRelationshipClause);
				}
				EntityType entityType = metadataType.TypeUsage.EdmType as EntityType;
				if (entityType == null)
				{
					throw EntityUtil.EntitySqlError(methodExpr.Relationships.ErrCtx, Strings.InvalidTypeForWithRelationshipClause);
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
						throw EntityUtil.EntitySqlError(relshipNavigationExpr.ErrCtx, Strings.RelationshipTargetMustBeUnique(text));
					}
					hashSet.Add(text);
					list.Add(dbRelatedEntityRef);
				}
			}
			List<TypeUsage> list2;
			return new ValueExpression(SemanticAnalyzer.CreateConstructorCallExpression(methodExpr, metadataType.TypeUsage, SemanticAnalyzer.ConvertFunctionArguments(methodExpr.Args, sr, out list2), list, sr));
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000BD448 File Offset: 0x000BB648
		private static ValueExpression ConvertModelFunctionCall(MetadataFunctionGroup metadataFunctionGroup, MethodExpr methodExpr, SemanticResolver sr)
		{
			if (metadataFunctionGroup.FunctionMetadata.Any((EdmFunction f) => !f.IsComposableAttribute))
			{
				throw EntityUtil.EntitySqlError(methodExpr.ErrCtx, Strings.CannotCallNoncomposableFunction(metadataFunctionGroup.Name));
			}
			if (TypeSemantics.IsAggregateFunction(metadataFunctionGroup.FunctionMetadata[0]) && sr.IsInAnyGroupScope())
			{
				return new ValueExpression(SemanticAnalyzer.ConvertAggregateFunctionInGroupScope(methodExpr, metadataFunctionGroup, sr));
			}
			return new ValueExpression(SemanticAnalyzer.CreateModelFunctionCallExpression(methodExpr, metadataFunctionGroup, sr));
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000BD4D0 File Offset: 0x000BB6D0
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
			throw EntityUtil.EntitySqlError(methodExpr.ErrCtx, Strings.FailedToResolveAggregateFunction(metadataFunctionGroup.Name));
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000BD54C File Offset: 0x000BB74C
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

		// Token: 0x060030C0 RID: 12480 RVA: 0x000BD5A4 File Offset: 0x000BB7A4
		private static bool TryConvertAsCollectionFunction(MethodExpr methodExpr, MetadataFunctionGroup metadataFunctionGroup, SemanticResolver sr, out List<TypeUsage> argTypes, out DbExpression converted)
		{
			List<DbExpression> list = SemanticAnalyzer.ConvertFunctionArguments(methodExpr.Args, sr, out argTypes);
			bool flag = false;
			EdmFunction edmFunction = SemanticResolver.ResolveFunctionOverloads(metadataFunctionGroup.FunctionMetadata, argTypes, false, out flag);
			if (flag)
			{
				throw EntityUtil.EntitySqlError(methodExpr.ErrCtx, Strings.AmbiguousFunctionArguments);
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

		// Token: 0x060030C1 RID: 12481 RVA: 0x000BD628 File Offset: 0x000BB828
		private static bool TryConvertAsFunctionAggregate(MethodExpr methodExpr, MetadataFunctionGroup metadataFunctionGroup, List<TypeUsage> argTypes, SemanticResolver sr, out DbExpression converted)
		{
			converted = null;
			bool flag = false;
			EdmFunction edmFunction = SemanticResolver.ResolveFunctionOverloads(metadataFunctionGroup.FunctionMetadata, argTypes, true, out flag);
			if (flag)
			{
				throw EntityUtil.EntitySqlError(methodExpr.ErrCtx, Strings.AmbiguousFunctionArguments);
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

		// Token: 0x060030C2 RID: 12482 RVA: 0x000BD740 File Offset: 0x000BB940
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
					throw EntityUtil.EntitySqlError(methodExpr.ErrCtx, Strings.NumberOfTypeCtorIsLessThenFormalSpec(edmMember.Name));
				}
				if (args[num] == null)
				{
					EdmProperty edmProperty = edmMember as EdmProperty;
					if (edmProperty != null && !edmProperty.Nullable)
					{
						throw EntityUtil.EntitySqlError(methodExpr.Args[num].ErrCtx, Strings.InvalidNullLiteralForNonNullableMember(edmMember.Name, structuralType.FullName));
					}
					args[num] = modelTypeUsage.Null();
				}
				bool flag = TypeSemantics.IsPromotableTo(args[num].ResultType, modelTypeUsage);
				if (ParserOptions.CompilationMode.RestrictedViewGenerationMode == sr.ParserOptions.ParserCompilationMode || ParserOptions.CompilationMode.UserViewGenerationMode == sr.ParserOptions.ParserCompilationMode)
				{
					if (!flag && !TypeSemantics.IsPromotableTo(modelTypeUsage, args[num].ResultType))
					{
						throw EntityUtil.EntitySqlError(methodExpr.Args[num].ErrCtx, Strings.InvalidCtorArgumentType(args[num].ResultType.EdmType.FullName, edmMember.Name, modelTypeUsage.EdmType.FullName));
					}
					if (Helper.IsPrimitiveType(modelTypeUsage.EdmType) && !TypeSemantics.IsSubTypeOf(args[num].ResultType, modelTypeUsage))
					{
						args[num] = args[num].CastTo(modelTypeUsage);
					}
				}
				else if (!flag)
				{
					throw EntityUtil.EntitySqlError(methodExpr.Args[num].ErrCtx, Strings.InvalidCtorArgumentType(args[num].ResultType.EdmType.FullName, edmMember.Name, modelTypeUsage.EdmType.FullName));
				}
				num++;
			}
			if (num != count)
			{
				throw EntityUtil.EntitySqlError(methodExpr.ErrCtx, Strings.NumberOfTypeCtorIsMoreThenFormalSpec(structuralType.FullName));
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

		// Token: 0x060030C3 RID: 12483 RVA: 0x000BD9A8 File Offset: 0x000BBBA8
		private static DbFunctionExpression CreateModelFunctionCallExpression(MethodExpr methodExpr, MetadataFunctionGroup metadataFunctionGroup, SemanticResolver sr)
		{
			bool flag = false;
			if (methodExpr.DistinctKind != DistinctKind.None)
			{
				throw EntityUtil.EntitySqlError(methodExpr.ErrCtx, Strings.InvalidDistinctArgumentInNonAggFunction);
			}
			List<TypeUsage> argTypes;
			List<DbExpression> list = SemanticAnalyzer.ConvertFunctionArguments(methodExpr.Args, sr, out argTypes);
			EdmFunction edmFunction = SemanticResolver.ResolveFunctionOverloads(metadataFunctionGroup.FunctionMetadata, argTypes, false, out flag);
			if (flag)
			{
				throw EntityUtil.EntitySqlError(methodExpr.ErrCtx, Strings.AmbiguousFunctionArguments);
			}
			if (edmFunction == null)
			{
				CqlErrorHelper.ReportFunctionOverloadError(methodExpr, metadataFunctionGroup.FunctionMetadata[0], argTypes);
			}
			SemanticAnalyzer.ConvertUntypedNullsInArguments<FunctionParameter>(list, edmFunction.Parameters, (FunctionParameter parameter) => parameter.TypeUsage);
			return edmFunction.Invoke(list);
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000BDA54 File Offset: 0x000BBC54
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

		// Token: 0x060030C5 RID: 12485 RVA: 0x000BDABC File Offset: 0x000BBCBC
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

		// Token: 0x060030C6 RID: 12486 RVA: 0x000BDAFC File Offset: 0x000BBCFC
		private static ExpressionResolution ConvertParameter(Node expr, SemanticResolver sr)
		{
			QueryParameter queryParameter = (QueryParameter)expr;
			DbParameterReferenceExpression value;
			if (sr.Parameters == null || !sr.Parameters.TryGetValue(queryParameter.Name, out value))
			{
				throw EntityUtil.EntitySqlError(queryParameter.ErrCtx, Strings.ParameterWasNotDefined(queryParameter.Name));
			}
			return new ValueExpression(value);
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000BDB4C File Offset: 0x000BBD4C
		private static DbRelatedEntityRef ConvertRelatedEntityRef(RelshipNavigationExpr relshipExpr, EntityType driverEntityType, SemanticResolver sr)
		{
			EdmType edmType = SemanticAnalyzer.ConvertTypeName(relshipExpr.TypeName, sr).EdmType;
			RelationshipType relationshipType = edmType as RelationshipType;
			if (relationshipType == null)
			{
				throw EntityUtil.EntitySqlError(relshipExpr.TypeName.ErrCtx, Strings.RelationshipTypeExpected(edmType.FullName));
			}
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(relshipExpr.RefExpr, sr);
			RefType refType = dbExpression.ResultType.EdmType as RefType;
			if (refType == null)
			{
				throw EntityUtil.EntitySqlError(relshipExpr.RefExpr.ErrCtx, Strings.RelatedEndExprTypeMustBeReference);
			}
			RelationshipEndMember toEnd;
			if (relshipExpr.ToEndIdentifier != null)
			{
				toEnd = (RelationshipEndMember)relationshipType.Members.FirstOrDefault((EdmMember m) => m.Name.Equals(relshipExpr.ToEndIdentifier.Name, StringComparison.OrdinalIgnoreCase));
				if (toEnd == null)
				{
					throw EntityUtil.EntitySqlError(relshipExpr.ToEndIdentifier.ErrCtx, Strings.InvalidRelationshipMember(relshipExpr.ToEndIdentifier.Name, relationshipType.FullName));
				}
				if (toEnd.RelationshipMultiplicity != RelationshipMultiplicity.One && toEnd.RelationshipMultiplicity != RelationshipMultiplicity.ZeroOrOne)
				{
					throw EntityUtil.EntitySqlError(relshipExpr.ToEndIdentifier.ErrCtx, Strings.InvalidWithRelationshipTargetEndMultiplicity(toEnd.Name, toEnd.RelationshipMultiplicity.ToString()));
				}
				if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(refType, toEnd.TypeUsage.EdmType))
				{
					throw EntityUtil.EntitySqlError(relshipExpr.RefExpr.ErrCtx, Strings.RelatedEndExprTypeMustBePromotoableToToEnd(refType.FullName, toEnd.TypeUsage.EdmType.FullName));
				}
			}
			else
			{
				RelationshipEndMember[] array = (from m in relationshipType.Members
				select (RelationshipEndMember)m into e
				where TypeSemantics.IsStructurallyEqualOrPromotableTo(refType, e.TypeUsage.EdmType) && (e.RelationshipMultiplicity == RelationshipMultiplicity.One || e.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
				select e).ToArray<RelationshipEndMember>();
				int num = array.Length;
				if (num == 0)
				{
					throw EntityUtil.EntitySqlError(relshipExpr.ErrCtx, Strings.InvalidImplicitRelationshipToEnd(relationshipType.FullName));
				}
				if (num != 1)
				{
					throw EntityUtil.EntitySqlError(relshipExpr.ErrCtx, Strings.RelationshipToEndIsAmbiguos);
				}
				toEnd = array[0];
			}
			RelationshipEndMember relationshipEndMember;
			if (relshipExpr.FromEndIdentifier != null)
			{
				relationshipEndMember = (RelationshipEndMember)relationshipType.Members.FirstOrDefault((EdmMember m) => m.Name.Equals(relshipExpr.FromEndIdentifier.Name, StringComparison.OrdinalIgnoreCase));
				if (relationshipEndMember == null)
				{
					throw EntityUtil.EntitySqlError(relshipExpr.FromEndIdentifier.ErrCtx, Strings.InvalidRelationshipMember(relshipExpr.FromEndIdentifier.Name, relationshipType.FullName));
				}
				if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(driverEntityType.GetReferenceType(), relationshipEndMember.TypeUsage.EdmType))
				{
					throw EntityUtil.EntitySqlError(relshipExpr.FromEndIdentifier.ErrCtx, Strings.SourceTypeMustBePromotoableToFromEndRelationType(driverEntityType.FullName, relationshipEndMember.TypeUsage.EdmType.FullName));
				}
				if (relationshipEndMember.EdmEquals(toEnd))
				{
					throw EntityUtil.EntitySqlError(relshipExpr.ErrCtx, Strings.RelationshipFromEndIsAmbiguos);
				}
			}
			else
			{
				RelationshipEndMember[] array2 = (from m in relationshipType.Members
				select (RelationshipEndMember)m into e
				where TypeSemantics.IsStructurallyEqualOrPromotableTo(driverEntityType.GetReferenceType(), e.TypeUsage.EdmType) && !e.EdmEquals(toEnd)
				select e).ToArray<RelationshipEndMember>();
				int num2 = array2.Length;
				if (num2 == 0)
				{
					throw EntityUtil.EntitySqlError(relshipExpr.ErrCtx, Strings.InvalidImplicitRelationshipFromEnd(relationshipType.FullName));
				}
				if (num2 != 1)
				{
					throw EntityUtil.EntitySqlError(relshipExpr.ErrCtx, Strings.RelationshipFromEndIsAmbiguos);
				}
				relationshipEndMember = array2[0];
			}
			return DbExpressionBuilder.CreateRelatedEntityRef(relationshipEndMember, toEnd, dbExpression);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000BDF24 File Offset: 0x000BC124
		private static ExpressionResolution ConvertRelshipNavigationExpr(Node astExpr, SemanticResolver sr)
		{
			RelshipNavigationExpr relshipExpr = (RelshipNavigationExpr)astExpr;
			EdmType edmType = SemanticAnalyzer.ConvertTypeName(relshipExpr.TypeName, sr).EdmType;
			RelationshipType relationshipType = edmType as RelationshipType;
			if (relationshipType == null)
			{
				throw EntityUtil.EntitySqlError(relshipExpr.TypeName.ErrCtx, Strings.RelationshipTypeExpected(edmType.FullName));
			}
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(relshipExpr.RefExpr, sr);
			RefType sourceRefType = dbExpression.ResultType.EdmType as RefType;
			if (sourceRefType == null)
			{
				EntityType entityType = dbExpression.ResultType.EdmType as EntityType;
				if (entityType == null)
				{
					throw EntityUtil.EntitySqlError(relshipExpr.RefExpr.ErrCtx, Strings.RelatedEndExprTypeMustBeReference);
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
					throw EntityUtil.EntitySqlError(relshipExpr.ToEndIdentifier.ErrCtx, Strings.InvalidRelationshipMember(relshipExpr.ToEndIdentifier.Name, relationshipType.FullName));
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
					throw EntityUtil.EntitySqlError(relshipExpr.FromEndIdentifier.ErrCtx, Strings.InvalidRelationshipMember(relshipExpr.FromEndIdentifier.Name, relationshipType.FullName));
				}
				if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(sourceRefType, fromEnd.TypeUsage.EdmType))
				{
					throw EntityUtil.EntitySqlError(relshipExpr.FromEndIdentifier.ErrCtx, Strings.SourceTypeMustBePromotoableToFromEndRelationType(sourceRefType.FullName, fromEnd.TypeUsage.EdmType.FullName));
				}
				if (toEnd != null && fromEnd.EdmEquals(toEnd))
				{
					throw EntityUtil.EntitySqlError(relshipExpr.ErrCtx, Strings.RelationshipFromEndIsAmbiguos);
				}
			}
			else
			{
				RelationshipEndMember[] array = (from m in relationshipType.Members
				select (RelationshipEndMember)m into e
				where TypeSemantics.IsStructurallyEqualOrPromotableTo(sourceRefType, e.TypeUsage.EdmType) && (toEnd == null || !e.EdmEquals(toEnd))
				select e).ToArray<RelationshipEndMember>();
				int num = array.Length;
				if (num == 0)
				{
					throw EntityUtil.EntitySqlError(relshipExpr.ErrCtx, Strings.InvalidImplicitRelationshipFromEnd(relationshipType.FullName));
				}
				if (num != 1)
				{
					throw EntityUtil.EntitySqlError(relshipExpr.ErrCtx, Strings.RelationshipFromEndIsAmbiguos);
				}
				fromEnd = array[0];
			}
			if (toEnd == null)
			{
				RelationshipEndMember[] array2 = (from m in relationshipType.Members
				select (RelationshipEndMember)m into e
				where !e.EdmEquals(fromEnd)
				select e).ToArray<RelationshipEndMember>();
				int num2 = array2.Length;
				if (num2 == 0)
				{
					throw EntityUtil.EntitySqlError(relshipExpr.ErrCtx, Strings.InvalidImplicitRelationshipToEnd(relationshipType.FullName));
				}
				if (num2 != 1)
				{
					throw EntityUtil.EntitySqlError(relshipExpr.ErrCtx, Strings.RelationshipToEndIsAmbiguos);
				}
				toEnd = array2[0];
			}
			DbExpression value = dbExpression.Navigate(fromEnd, toEnd);
			return new ValueExpression(value);
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000BE2B4 File Offset: 0x000BC4B4
		private static ExpressionResolution ConvertRefExpr(Node astExpr, SemanticResolver sr)
		{
			RefExpr refExpr = (RefExpr)astExpr;
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(refExpr.ArgExpr, sr);
			if (!TypeSemantics.IsEntityType(dbExpression.ResultType))
			{
				throw EntityUtil.EntitySqlError(refExpr.ArgExpr.ErrCtx, Strings.RefArgIsNotOfEntityType(dbExpression.ResultType.EdmType.FullName));
			}
			dbExpression = dbExpression.GetEntityRef();
			return new ValueExpression(dbExpression);
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000BE318 File Offset: 0x000BC518
		private static ExpressionResolution ConvertDeRefExpr(Node astExpr, SemanticResolver sr)
		{
			DerefExpr derefExpr = (DerefExpr)astExpr;
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(derefExpr.ArgExpr, sr);
			if (!TypeSemantics.IsReferenceType(dbExpression.ResultType))
			{
				throw EntityUtil.EntitySqlError(derefExpr.ArgExpr.ErrCtx, Strings.DeRefArgIsNotOfRefType(dbExpression.ResultType.EdmType.FullName));
			}
			dbExpression = dbExpression.Deref();
			return new ValueExpression(dbExpression);
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000BE37C File Offset: 0x000BC57C
		private static ExpressionResolution ConvertCreateRefExpr(Node astExpr, SemanticResolver sr)
		{
			CreateRefExpr createRefExpr = (CreateRefExpr)astExpr;
			DbScanExpression dbScanExpression = SemanticAnalyzer.ConvertValueExpression(createRefExpr.EntitySet, sr) as DbScanExpression;
			if (dbScanExpression == null)
			{
				throw EntityUtil.EntitySqlError(createRefExpr.EntitySet.ErrCtx, Strings.ExprIsNotValidEntitySetForCreateRef);
			}
			EntitySet entitySet = dbScanExpression.Target as EntitySet;
			if (entitySet == null)
			{
				throw EntityUtil.EntitySqlError(createRefExpr.EntitySet.ErrCtx, Strings.ExprIsNotValidEntitySetForCreateRef);
			}
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(createRefExpr.Keys, sr);
			RowType rowType = dbExpression.ResultType.EdmType as RowType;
			if (rowType == null)
			{
				throw EntityUtil.EntitySqlError(createRefExpr.Keys.ErrCtx, Strings.InvalidCreateRefKeyType);
			}
			RowType rowType2 = TypeHelpers.CreateKeyRowType(entitySet.ElementType);
			if (rowType2.Members.Count != rowType.Members.Count)
			{
				throw EntityUtil.EntitySqlError(createRefExpr.Keys.ErrCtx, Strings.ImcompatibleCreateRefKeyType);
			}
			if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(dbExpression.ResultType, TypeUsage.Create(rowType2)))
			{
				throw EntityUtil.EntitySqlError(createRefExpr.Keys.ErrCtx, Strings.ImcompatibleCreateRefKeyElementType);
			}
			DbExpression value;
			if (createRefExpr.TypeIdentifier != null)
			{
				TypeUsage typeUsage = SemanticAnalyzer.ConvertTypeName(createRefExpr.TypeIdentifier, sr);
				if (!TypeSemantics.IsEntityType(typeUsage))
				{
					throw EntityUtil.EntitySqlError(createRefExpr.TypeIdentifier.ErrCtx, Strings.CreateRefTypeIdentifierMustSpecifyAnEntityType(typeUsage.EdmType.FullName, typeUsage.EdmType.BuiltInTypeKind.ToString()));
				}
				if (!TypeSemantics.IsValidPolymorphicCast(entitySet.ElementType, typeUsage.EdmType))
				{
					throw EntityUtil.EntitySqlError(createRefExpr.TypeIdentifier.ErrCtx, Strings.CreateRefTypeIdentifierMustBeASubOrSuperType(entitySet.ElementType.FullName, typeUsage.EdmType.FullName));
				}
				value = entitySet.RefFromKey(dbExpression, (EntityType)typeUsage.EdmType);
			}
			else
			{
				value = entitySet.RefFromKey(dbExpression);
			}
			return new ValueExpression(value);
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000BE54C File Offset: 0x000BC74C
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
				throw EntityUtil.EntitySqlError(keyExpr.ArgExpr.ErrCtx, Strings.InvalidKeyArgument(dbExpression.ResultType.EdmType.FullName));
			}
			dbExpression = dbExpression.GetRefKey();
			return new ValueExpression(dbExpression);
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000BE5C4 File Offset: 0x000BC7C4
		private static ExpressionResolution ConvertBuiltIn(Node astExpr, SemanticResolver sr)
		{
			BuiltInExpr builtInExpr = (BuiltInExpr)astExpr;
			SemanticAnalyzer.BuiltInExprConverter builtInExprConverter = SemanticAnalyzer._builtInExprConverter[builtInExpr.Kind];
			if (builtInExprConverter == null)
			{
				throw EntityUtil.EntitySqlError(Strings.UnknownBuiltInAstExpressionType);
			}
			return new ValueExpression(builtInExprConverter(builtInExpr, sr));
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000BE604 File Offset: 0x000BC804
		private static Pair<DbExpression, DbExpression> ConvertArithmeticArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(astBuiltInExpr.Arg1, astBuiltInExpr.Arg2, astBuiltInExpr.ErrCtx, () => Strings.InvalidNullArithmetic, sr);
			if (!TypeSemantics.IsNumericType(pair.Left.ResultType))
			{
				throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg1.ErrCtx, Strings.ExpressionMustBeNumericType);
			}
			if (pair.Right != null)
			{
				if (!TypeSemantics.IsNumericType(pair.Right.ResultType))
				{
					throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg2.ErrCtx, Strings.ExpressionMustBeNumericType);
				}
				if (TypeHelpers.GetCommonTypeUsage(pair.Left.ResultType, pair.Right.ResultType) == null)
				{
					throw EntityUtil.EntitySqlError(astBuiltInExpr.ErrCtx, Strings.ArgumentTypesAreIncompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName));
				}
			}
			return pair;
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x000BE700 File Offset: 0x000BC900
		private static Pair<DbExpression, DbExpression> ConvertPlusOperands(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(astBuiltInExpr.Arg1, astBuiltInExpr.Arg2, astBuiltInExpr.ErrCtx, () => Strings.InvalidNullArithmetic, sr);
			if (!TypeSemantics.IsNumericType(pair.Left.ResultType) && !TypeSemantics.IsPrimitiveType(pair.Left.ResultType, PrimitiveTypeKind.String))
			{
				throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg1.ErrCtx, Strings.PlusLeftExpressionInvalidType);
			}
			if (!TypeSemantics.IsNumericType(pair.Right.ResultType) && !TypeSemantics.IsPrimitiveType(pair.Right.ResultType, PrimitiveTypeKind.String))
			{
				throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg2.ErrCtx, Strings.PlusRightExpressionInvalidType);
			}
			if (TypeHelpers.GetCommonTypeUsage(pair.Left.ResultType, pair.Right.ResultType) == null)
			{
				throw EntityUtil.EntitySqlError(astBuiltInExpr.ErrCtx, Strings.ArgumentTypesAreIncompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName));
			}
			return pair;
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000BE818 File Offset: 0x000BCA18
		private static Pair<DbExpression, DbExpression> ConvertLogicalArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(astBuiltInExpr.Arg1, sr);
			if (dbExpression == null)
			{
				dbExpression = sr.TypeResolver.BooleanType.Null();
			}
			DbExpression dbExpression2 = null;
			if (astBuiltInExpr.Arg2 != null)
			{
				dbExpression2 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(astBuiltInExpr.Arg2, sr);
				if (dbExpression2 == null)
				{
					dbExpression2 = sr.TypeResolver.BooleanType.Null();
				}
			}
			if (!SemanticAnalyzer.IsBooleanType(dbExpression.ResultType))
			{
				throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg1.ErrCtx, Strings.ExpressionTypeMustBeBoolean);
			}
			if (dbExpression2 != null && !SemanticAnalyzer.IsBooleanType(dbExpression2.ResultType))
			{
				throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg2.ErrCtx, Strings.ExpressionTypeMustBeBoolean);
			}
			return new Pair<DbExpression, DbExpression>(dbExpression, dbExpression2);
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x000BE8C4 File Offset: 0x000BCAC4
		private static Pair<DbExpression, DbExpression> ConvertEqualCompArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(astBuiltInExpr.Arg1, astBuiltInExpr.Arg2, astBuiltInExpr.ErrCtx, () => Strings.InvalidNullComparison, sr);
			if (!TypeSemantics.IsEqualComparableTo(pair.Left.ResultType, pair.Right.ResultType))
			{
				throw EntityUtil.EntitySqlError(astBuiltInExpr.ErrCtx, Strings.ArgumentTypesAreIncompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName));
			}
			return pair;
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000BE964 File Offset: 0x000BCB64
		private static Pair<DbExpression, DbExpression> ConvertOrderCompArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(astBuiltInExpr.Arg1, astBuiltInExpr.Arg2, astBuiltInExpr.ErrCtx, () => Strings.InvalidNullComparison, sr);
			if (!TypeSemantics.IsOrderComparableTo(pair.Left.ResultType, pair.Right.ResultType))
			{
				throw EntityUtil.EntitySqlError(astBuiltInExpr.ErrCtx, Strings.ArgumentTypesAreIncompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName));
			}
			return pair;
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x000BEA04 File Offset: 0x000BCC04
		private static Pair<DbExpression, DbExpression> ConvertSetArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(astBuiltInExpr.Arg1, sr);
			DbExpression dbExpression2 = null;
			if (astBuiltInExpr.Arg2 != null)
			{
				if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
				{
					throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg1.ErrCtx, Strings.LeftSetExpressionArgsMustBeCollection);
				}
				dbExpression2 = SemanticAnalyzer.ConvertValueExpression(astBuiltInExpr.Arg2, sr);
				if (!TypeSemantics.IsCollectionType(dbExpression2.ResultType))
				{
					throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg2.ErrCtx, Strings.RightSetExpressionArgsMustBeCollection);
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
						throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg1.ErrCtx, Strings.PlaceholderSetArgTypeIsNotEqualComparable(Strings.LocalizedLeft, astBuiltInExpr.Kind.ToString().ToUpperInvariant(), TypeHelpers.GetElementTypeUsage(dbExpression.ResultType).EdmType.FullName));
					}
					if (!TypeHelpers.IsSetComparableOpType(TypeHelpers.GetElementTypeUsage(dbExpression2.ResultType)))
					{
						throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg2.ErrCtx, Strings.PlaceholderSetArgTypeIsNotEqualComparable(Strings.LocalizedRight, astBuiltInExpr.Kind.ToString().ToUpperInvariant(), TypeHelpers.GetElementTypeUsage(dbExpression2.ResultType).EdmType.FullName));
					}
				}
				else
				{
					if (Helper.IsAssociationType(elementTypeUsage.EdmType))
					{
						throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg1.ErrCtx, Strings.InvalidAssociationTypeForUnion(elementTypeUsage.EdmType.FullName));
					}
					if (Helper.IsAssociationType(elementTypeUsage2.EdmType))
					{
						throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg2.ErrCtx, Strings.InvalidAssociationTypeForUnion(elementTypeUsage2.EdmType.FullName));
					}
				}
			}
			else
			{
				if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
				{
					throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg1.ErrCtx, Strings.InvalidUnarySetOpArgument(astBuiltInExpr.Name));
				}
				if (astBuiltInExpr.Kind == BuiltInKind.Distinct && !TypeHelpers.IsValidDistinctOpType(TypeHelpers.GetElementTypeUsage(dbExpression.ResultType)))
				{
					throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg1.ErrCtx, Strings.ExpressionTypeMustBeEqualComparable);
				}
			}
			return new Pair<DbExpression, DbExpression>(dbExpression, dbExpression2);
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000BEC38 File Offset: 0x000BCE38
		private static Pair<DbExpression, DbExpression> ConvertInExprArgs(BuiltInExpr astBuiltInExpr, SemanticResolver sr)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(astBuiltInExpr.Arg2, sr);
			if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
			{
				throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg2.ErrCtx, Strings.RightSetExpressionArgsMustBeCollection);
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
				throw EntityUtil.EntitySqlError(astBuiltInExpr.Arg1.ErrCtx, Strings.ExpressionTypeMustNotBeCollection);
			}
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(dbExpression2.ResultType, TypeHelpers.GetElementTypeUsage(dbExpression.ResultType));
			if (commonTypeUsage == null || !TypeHelpers.IsValidInOpType(commonTypeUsage))
			{
				throw EntityUtil.EntitySqlError(astBuiltInExpr.ErrCtx, Strings.InvalidInExprArgs(dbExpression2.ResultType.EdmType.FullName, dbExpression.ResultType.EdmType.FullName));
			}
			return new Pair<DbExpression, DbExpression>(dbExpression2, dbExpression);
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000BED26 File Offset: 0x000BCF26
		private static void ValidateTypeForNullExpression(TypeUsage type, ErrorContext errCtx)
		{
			if (TypeSemantics.IsCollectionType(type))
			{
				throw EntityUtil.EntitySqlError(errCtx, Strings.NullLiteralCannotBePromotedToCollectionOfNulls);
			}
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000BED3C File Offset: 0x000BCF3C
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
				throw EntityUtil.EntitySqlError(typeName.ErrCtx, Strings.InvalidMetadataMemberName);
			}
			MetadataMember metadataMember = sr.ResolveMetadataMemberName(array, typeName.ErrCtx);
			MetadataMemberClass metadataMemberClass = metadataMember.MetadataMemberClass;
			if (metadataMemberClass == MetadataMemberClass.Type)
			{
				TypeUsage typeUsage = ((MetadataType)metadataMember).TypeUsage;
				if (nodeList != null)
				{
					typeUsage = SemanticAnalyzer.ConvertTypeSpecArgs(typeUsage, nodeList, typeName.ErrCtx, sr);
				}
				return typeUsage;
			}
			if (metadataMemberClass != MetadataMemberClass.Namespace)
			{
				throw EntityUtil.EntitySqlError(typeName.ErrCtx, Strings.InvalidMetadataMemberClassResolution(metadataMember.Name, metadataMember.MetadataMemberClassName, MetadataType.TypeClassName));
			}
			throw EntityUtil.EntitySqlError(typeName.ErrCtx, Strings.TypeNameNotFound(metadataMember.Name));
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000BEE5C File Offset: 0x000BD05C
		private static TypeUsage ConvertTypeSpecArgs(TypeUsage parameterizedType, NodeList<Node> typeSpecArgs, ErrorContext errCtx, SemanticResolver sr)
		{
			foreach (Node node in ((IEnumerable<Node>)typeSpecArgs))
			{
				if (!(node is Literal))
				{
					throw EntityUtil.EntitySqlError(node.ErrCtx, Strings.TypeArgumentMustBeLiteral);
				}
			}
			PrimitiveType primitiveType = parameterizedType.EdmType as PrimitiveType;
			if (primitiveType == null || primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Decimal)
			{
				throw EntityUtil.EntitySqlError(errCtx, Strings.TypeDoesNotSupportSpec(primitiveType.FullName));
			}
			if (typeSpecArgs.Count > 2)
			{
				throw EntityUtil.EntitySqlError(errCtx, Strings.TypeArgumentCountMismatch(primitiveType.FullName, 2));
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
				throw EntityUtil.EntitySqlError(typeSpecArgs[0].ErrCtx, Strings.PrecisionMustBeGreaterThanScale(b, b2));
			}
			return TypeUsage.CreateDecimalTypeUsage(primitiveType, b, b2);
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000BEF70 File Offset: 0x000BD170
		private static void ConvertTypeFacetValue(PrimitiveType type, Literal value, string facetName, out byte byteValue)
		{
			FacetDescription facet = Helper.GetFacet(type.ProviderManifest.GetFacetDescriptions(type), facetName);
			if (facet == null)
			{
				throw EntityUtil.EntitySqlError(value.ErrCtx, Strings.TypeDoesNotSupportFacet(type.FullName, facetName));
			}
			if (!value.IsNumber || !byte.TryParse(value.OriginalValue, out byteValue))
			{
				throw EntityUtil.EntitySqlError(value.ErrCtx, Strings.TypeArgumentIsNotValid);
			}
			if (facet.MaxValue != null && (int)byteValue > facet.MaxValue.Value)
			{
				throw EntityUtil.EntitySqlError(value.ErrCtx, Strings.TypeArgumentExceedsMax(facetName));
			}
			if (facet.MinValue != null && (int)byteValue < facet.MinValue.Value)
			{
				throw EntityUtil.EntitySqlError(value.ErrCtx, Strings.TypeArgumentBelowMin(facetName));
			}
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000BF03C File Offset: 0x000BD23C
		private static TypeUsage ConvertTypeDefinition(Node typeDefinitionExpr, SemanticResolver sr)
		{
			CollectionTypeDefinition collectionTypeDefinition = typeDefinitionExpr as CollectionTypeDefinition;
			RefTypeDefinition refTypeDefinition = typeDefinitionExpr as RefTypeDefinition;
			RowTypeDefinition rowTypeDefinition = typeDefinitionExpr as RowTypeDefinition;
			TypeUsage result;
			if (collectionTypeDefinition != null)
			{
				TypeUsage elementType = SemanticAnalyzer.ConvertTypeDefinition(collectionTypeDefinition.ElementTypeDef, sr);
				result = TypeHelpers.CreateCollectionTypeUsage(elementType, true);
			}
			else if (refTypeDefinition != null)
			{
				TypeUsage typeUsage = SemanticAnalyzer.ConvertTypeName(refTypeDefinition.RefTypeIdentifier, sr);
				if (!TypeSemantics.IsEntityType(typeUsage))
				{
					throw EntityUtil.EntitySqlError(refTypeDefinition.RefTypeIdentifier.ErrCtx, Strings.RefTypeIdentifierMustSpecifyAnEntityType(typeUsage.EdmType.FullName, typeUsage.EdmType.BuiltInTypeKind.ToString()));
				}
				result = TypeHelpers.CreateReferenceTypeUsage((EntityType)typeUsage.EdmType);
			}
			else if (rowTypeDefinition != null)
			{
				result = TypeHelpers.CreateRowTypeUsage(from p in rowTypeDefinition.Properties
				select new KeyValuePair<string, TypeUsage>(p.Name.Name, SemanticAnalyzer.ConvertTypeDefinition(p.Type, sr)), true);
			}
			else
			{
				result = SemanticAnalyzer.ConvertTypeName(typeDefinitionExpr, sr);
			}
			return result;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000BF138 File Offset: 0x000BD338
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
					throw EntityUtil.EntitySqlError(aliasedExpr.Expr.ErrCtx, Strings.RowCtorElementCannotBeNull);
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
			return new ValueExpression(TypeHelpers.CreateRowTypeUsage(dictionary, true).New(list));
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000BF22C File Offset: 0x000BD42C
		private static ExpressionResolution ConvertMultisetConstructor(Node expr, SemanticResolver sr)
		{
			MultisetConstructorExpr multisetConstructorExpr = (MultisetConstructorExpr)expr;
			if (multisetConstructorExpr.ExprList == null)
			{
				throw EntityUtil.EntitySqlError(expr.ErrCtx, Strings.CannotCreateEmptyMultiset);
			}
			DbExpression[] array = (from e in multisetConstructorExpr.ExprList
			select SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(e, sr)).ToArray<DbExpression>();
			TypeUsage[] array2 = (from e in array
			where e != null
			select e.ResultType).ToArray<TypeUsage>();
			if (array2.Length == 0)
			{
				throw EntityUtil.EntitySqlError(expr.ErrCtx, Strings.CannotCreateMultisetofNulls);
			}
			TypeUsage typeUsage = TypeHelpers.GetCommonTypeUsage(array2);
			if (typeUsage == null)
			{
				throw EntityUtil.EntitySqlError(expr.ErrCtx, Strings.MultisetElemsAreNotTypeCompatible);
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
			return new ValueExpression(TypeHelpers.CreateCollectionTypeUsage(typeUsage, true).New(array));
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000BF35C File Offset: 0x000BD55C
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
					throw EntityUtil.EntitySqlError(whenThenExpr.WhenExpr.ErrCtx, Strings.ExpressionTypeMustBeBoolean);
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
				throw EntityUtil.EntitySqlError(caseExpr.ElseExpr.ErrCtx, Strings.InvalidCaseWhenThenNullType);
			}
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(list3);
			if (commonTypeUsage == null)
			{
				throw EntityUtil.EntitySqlError(caseExpr.WhenThenExprList[0].ThenExpr.ErrCtx, Strings.InvalidCaseResultTypes);
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

		// Token: 0x060030DD RID: 12509 RVA: 0x000BF56C File Offset: 0x000BD76C
		private static ExpressionResolution ConvertQueryExpr(Node expr, SemanticResolver sr)
		{
			QueryExpr queryExpr = (QueryExpr)expr;
			DbExpression value = null;
			bool flag = ParserOptions.CompilationMode.RestrictedViewGenerationMode == sr.ParserOptions.ParserCompilationMode;
			if (queryExpr.HavingClause != null && queryExpr.GroupByClause == null)
			{
				throw EntityUtil.EntitySqlError(queryExpr.ErrCtx, Strings.HavingRequiresGroupClause);
			}
			if (queryExpr.SelectClause.TopExpr != null)
			{
				if (queryExpr.OrderByClause != null && queryExpr.OrderByClause.LimitSubClause != null)
				{
					throw EntityUtil.EntitySqlError(queryExpr.SelectClause.TopExpr.ErrCtx, Strings.TopAndLimitCannotCoexist);
				}
				if (queryExpr.OrderByClause != null && queryExpr.OrderByClause.SkipSubClause != null)
				{
					throw EntityUtil.EntitySqlError(queryExpr.SelectClause.TopExpr.ErrCtx, Strings.TopAndSkipCannotCoexist);
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

		// Token: 0x060030DE RID: 12510 RVA: 0x000BF6A0 File Offset: 0x000BD8A0
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
				SemanticAnalyzer.ValidateExpressionIsCommandParamOrNonNegativeIntegerConstant(dbExpression2, node.ErrCtx, exprName, sr);
				dbExpression = dbExpression.Limit(dbExpression2);
			}
			return dbExpression;
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000BF73C File Offset: 0x000BD93C
		private static List<KeyValuePair<string, DbExpression>> ConvertSelectClauseItems(QueryExpr queryExpr, SemanticResolver sr)
		{
			SelectClause selectClause = queryExpr.SelectClause;
			if (selectClause.SelectKind == SelectKind.Value)
			{
				if (selectClause.Items.Count != 1)
				{
					throw EntityUtil.EntitySqlError(selectClause.ErrCtx, Strings.InvalidSelectValueList);
				}
				if (selectClause.Items[0].Alias != null && queryExpr.OrderByClause == null)
				{
					throw EntityUtil.EntitySqlError(selectClause.Items[0].ErrCtx, Strings.InvalidSelectValueAliasedExpression);
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

		// Token: 0x060030E0 RID: 12512 RVA: 0x000BF860 File Offset: 0x000BDA60
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

		// Token: 0x060030E1 RID: 12513 RVA: 0x000BF8B8 File Offset: 0x000BDAB8
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

		// Token: 0x060030E2 RID: 12514 RVA: 0x000BF91C File Offset: 0x000BDB1C
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
				throw EntityUtil.EntitySqlError(errCtx, Strings.SelectDistinctMustBeEqualComparable);
			}
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000BF998 File Offset: 0x000BDB98
		private static void ValidateExpressionIsCommandParamOrNonNegativeIntegerConstant(DbExpression expr, ErrorContext errCtx, string exprName, SemanticResolver sr)
		{
			if (expr.ExpressionKind != DbExpressionKind.Constant && expr.ExpressionKind != DbExpressionKind.ParameterReference)
			{
				throw EntityUtil.EntitySqlError(errCtx, Strings.PlaceholderExpressionMustBeConstant(exprName));
			}
			if (!TypeSemantics.IsPromotableTo(expr.ResultType, sr.TypeResolver.Int64Type))
			{
				throw EntityUtil.EntitySqlError(errCtx, Strings.PlaceholderExpressionMustBeCompatibleWithEdm64(exprName, expr.ResultType.EdmType.FullName));
			}
			DbConstantExpression dbConstantExpression = expr as DbConstantExpression;
			if (dbConstantExpression != null && System.Convert.ToInt64(dbConstantExpression.Value, CultureInfo.InvariantCulture) < 0L)
			{
				throw EntityUtil.EntitySqlError(errCtx, Strings.PlaceholderExpressionMustBeGreaterThanOrEqualToZero(exprName));
			}
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000BFA28 File Offset: 0x000BDC28
		private static DbExpressionBinding ProcessFromClause(FromClause fromClause, SemanticResolver sr)
		{
			DbExpressionBinding fromBinding = null;
			List<SourceScopeEntry> list = new List<SourceScopeEntry>();
			Action<SourceScopeEntry> <>9__0;
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
					List<SourceScopeEntry> list2 = list;
					Action<SourceScopeEntry> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(SourceScopeEntry scopeEntry)
						{
							scopeEntry.AddParentVar(fromBinding.Variable);
						});
					}
					list2.ForEach(action);
				}
			}
			return fromBinding;
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000BFAE0 File Offset: 0x000BDCE0
		private static DbExpressionBinding ProcessFromClauseItem(FromClauseItem fromClauseItem, SemanticResolver sr, out List<SourceScopeEntry> scopeEntries)
		{
			FromClauseItemKind fromClauseItemKind = fromClauseItem.FromClauseItemKind;
			DbExpressionBinding result;
			if (fromClauseItemKind != FromClauseItemKind.AliasedFromClause)
			{
				if (fromClauseItemKind != FromClauseItemKind.JoinFromClause)
				{
					result = SemanticAnalyzer.ProcessApplyClauseItem((ApplyClauseItem)fromClauseItem.FromExpr, sr, out scopeEntries);
				}
				else
				{
					result = SemanticAnalyzer.ProcessJoinClauseItem((JoinClauseItem)fromClauseItem.FromExpr, sr, out scopeEntries);
				}
			}
			else
			{
				result = SemanticAnalyzer.ProcessAliasedFromClauseItem((AliasedExpr)fromClauseItem.FromExpr, sr, out scopeEntries);
			}
			return result;
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000BFB40 File Offset: 0x000BDD40
		private static DbExpressionBinding ProcessAliasedFromClauseItem(AliasedExpr aliasedExpr, SemanticResolver sr, out List<SourceScopeEntry> scopeEntries)
		{
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(aliasedExpr.Expr, sr);
			if (!TypeSemantics.IsCollectionType(dbExpression.ResultType))
			{
				throw EntityUtil.EntitySqlError(aliasedExpr.Expr.ErrCtx, Strings.ExpressionMustBeCollection);
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

		// Token: 0x060030E7 RID: 12519 RVA: 0x000BFC00 File Offset: 0x000BDE00
		private static DbExpressionBinding ProcessJoinClauseItem(JoinClauseItem joinClause, SemanticResolver sr, out List<SourceScopeEntry> scopeEntries)
		{
			DbExpressionBinding joinBinding = null;
			if (joinClause.OnExpr == null)
			{
				if (JoinKind.Inner == joinClause.JoinKind)
				{
					throw EntityUtil.EntitySqlError(joinClause.ErrCtx, Strings.InnerJoinMustHaveOnPredicate);
				}
			}
			else if (joinClause.JoinKind == JoinKind.Cross)
			{
				throw EntityUtil.EntitySqlError(joinClause.OnExpr.ErrCtx, Strings.InvalidPredicateForCrossJoin);
			}
			List<SourceScopeEntry> list;
			DbExpressionBinding dbExpressionBinding = SemanticAnalyzer.ProcessFromClauseItem(joinClause.LeftExpr, sr, out list);
			list.ForEach(delegate(SourceScopeEntry scopeEntry)
			{
				scopeEntry.IsJoinClauseLeftExpr = true;
			});
			List<SourceScopeEntry> collection;
			DbExpressionBinding dbExpressionBinding2 = SemanticAnalyzer.ProcessFromClauseItem(joinClause.RightExpr, sr, out collection);
			list.ForEach(delegate(SourceScopeEntry scopeEntry)
			{
				scopeEntry.IsJoinClauseLeftExpr = false;
			});
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
			scopeEntries.ForEach(delegate(SourceScopeEntry scopeEntry)
			{
				scopeEntry.AddParentVar(joinBinding.Variable);
			});
			return joinBinding;
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000BFD57 File Offset: 0x000BDF57
		private static DbExpressionKind MapJoinKind(JoinKind joinKind)
		{
			return SemanticAnalyzer.joinMap[(int)joinKind];
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000BFD60 File Offset: 0x000BDF60
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
			scopeEntries.ForEach(delegate(SourceScopeEntry scopeEntry)
			{
				scopeEntry.AddParentVar(applyBinding.Variable);
			});
			return applyBinding;
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000BFDE6 File Offset: 0x000BDFE6
		private static DbExpressionKind MapApplyKind(ApplyKind applyKind)
		{
			return SemanticAnalyzer.applyMap[(int)applyKind];
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000BFDEF File Offset: 0x000BDFEF
		private static DbExpressionBinding ProcessWhereClause(DbExpressionBinding source, Node whereClause, SemanticResolver sr)
		{
			if (whereClause == null)
			{
				return source;
			}
			return SemanticAnalyzer.ProcessWhereHavingClausePredicate(source, whereClause, whereClause.ErrCtx, "where", sr);
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000BFE09 File Offset: 0x000BE009
		private static DbExpressionBinding ProcessHavingClause(DbExpressionBinding source, HavingClause havingClause, SemanticResolver sr)
		{
			if (havingClause == null)
			{
				return source;
			}
			return SemanticAnalyzer.ProcessWhereHavingClausePredicate(source, havingClause.HavingPredicate, havingClause.ErrCtx, "having", sr);
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000BFE28 File Offset: 0x000BE028
		private static DbExpressionBinding ProcessWhereHavingClausePredicate(DbExpressionBinding source, Node predicate, ErrorContext errCtx, string bindingNameTemplate, SemanticResolver sr)
		{
			DbExpressionBinding whereBinding = null;
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpression(predicate, sr);
			if (!SemanticAnalyzer.IsBooleanType(dbExpression.ResultType))
			{
				throw EntityUtil.EntitySqlError(errCtx, Strings.ExpressionTypeMustBeBoolean);
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

		// Token: 0x060030EE RID: 12526 RVA: 0x000BFE9C File Offset: 0x000BE09C
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
						throw EntityUtil.EntitySqlError(aliasedExpr.Expr.ErrCtx, Strings.KeyMustBeCorrelated("GROUP BY"));
					}
					if (!TypeHelpers.IsValidGroupKeyType(dbExpression.ResultType))
					{
						throw EntityUtil.EntitySqlError(aliasedExpr.Expr.ErrCtx, Strings.GroupingKeysMustBeEqualComparable);
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
				DbExpression dbExpression2 = SemanticAnalyzer.ConvertValueExpression(queryExpr.HavingClause.HavingPredicate, sr);
			}
			Dictionary<string, DbExpression> dictionary = null;
			if (queryExpr.OrderByClause != null || queryExpr.SelectClause.HasMethodCall)
			{
				dictionary = new Dictionary<string, DbExpression>(queryExpr.SelectClause.Items.Count, sr.NameComparer);
				for (int j = 0; j < queryExpr.SelectClause.Items.Count; j++)
				{
					AliasedExpr aliasedExpr2 = queryExpr.SelectClause.Items[j];
					DbExpression dbExpression3 = SemanticAnalyzer.ConvertValueExpression(aliasedExpr2.Expr, sr);
					dbExpression3 = ((dbExpression3.ExpressionKind == DbExpressionKind.Null) ? dbExpression3 : dbExpression3.ResultType.Null());
					string text2 = sr.InferAliasName(aliasedExpr2, dbExpression3);
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
					dictionary.Add(text2, dbExpression3);
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
					DbExpression dbExpression4 = SemanticAnalyzer.ConvertValueExpression(orderByClauseItem.OrderExpr, sr);
					if (!sr.CurrentScopeRegion.WasResolutionCorrelated)
					{
						throw EntityUtil.EntitySqlError(orderByClauseItem.ErrCtx, Strings.KeyMustBeCorrelated("ORDER BY"));
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
				GroupAggregateKind aggregateKind = groupAggregateInfo3.AggregateKind;
				if (aggregateKind != GroupAggregateKind.Function)
				{
					if (aggregateKind == GroupAggregateKind.Partition)
					{
						flag2 = true;
					}
				}
				else
				{
					list2.Add(new KeyValuePair<string, DbAggregate>(groupAggregateInfo3.AggregateName, ((FunctionAggregateInfo)groupAggregateInfo3).AggregateDefinition));
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

		// Token: 0x060030EF RID: 12527 RVA: 0x000C07FC File Offset: 0x000BE9FC
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
					DbExpression aggregateDefinition = ((GroupPartitionInfo)groupAggregateInfo).AggregateDefinition;
					if (SemanticAnalyzer.IsTrivialInputProjection(groupAggregateVarRef, aggregateDefinition))
					{
						groupAggregateInfo.AggregateName = groupAggregateVarRef.VariableName;
						flag = true;
					}
					else
					{
						DbLambda lambda = new DbLambda(variables, ((GroupPartitionInfo)groupAggregateInfo).AggregateDefinition);
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

		// Token: 0x060030F0 RID: 12528 RVA: 0x000C0910 File Offset: 0x000BEB10
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

		// Token: 0x060030F1 RID: 12529 RVA: 0x000C0A48 File Offset: 0x000BEC48
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
				SemanticAnalyzer.ValidateExpressionIsCommandParamOrNonNegativeIntegerConstant(dbExpression, orderByClause.SkipSubClause.ErrCtx, "SKIP", sr);
			}
			List<KeyValuePair<string, DbExpression>> list = SemanticAnalyzer.ConvertSelectClauseItems(queryExpr, sr);
			if (selectClause.DistinctKind == DistinctKind.Distinct)
			{
				sr.CurrentScopeRegion.RollbackAllScopes();
			}
			int currentScopeIndex = sr.CurrentScopeIndex;
			sr.EnterScope();
			list.ForEach(delegate(KeyValuePair<string, DbExpression> projectionItem)
			{
				sr.CurrentScope.Add(projectionItem.Key, new ProjectionItemDefinitionScopeEntry(projectionItem.Value));
			});
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
					throw EntityUtil.EntitySqlError(orderByClauseItem.ErrCtx, Strings.KeyMustBeCorrelated("ORDER BY"));
				}
				if (!TypeHelpers.IsValidSortOpKeyType(dbExpression2.ResultType))
				{
					throw EntityUtil.EntitySqlError(orderByClauseItem.OrderExpr.ErrCtx, Strings.OrderByKeyIsNotOrderComparable);
				}
				bool flag = orderByClauseItem.OrderKind == OrderKind.None || orderByClauseItem.OrderKind == OrderKind.Asc;
				string text = null;
				if (orderByClauseItem.Collation != null)
				{
					if (!SemanticAnalyzer.IsStringType(dbExpression2.ResultType))
					{
						throw EntityUtil.EntitySqlError(orderByClauseItem.OrderExpr.ErrCtx, Strings.InvalidKeyTypeForCollation(dbExpression2.ResultType.EdmType.FullName));
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

		// Token: 0x060030F2 RID: 12530 RVA: 0x000C0DC8 File Offset: 0x000BEFC8
		private static DbExpression ConvertSimpleInExpression(SemanticResolver sr, DbExpression left, DbExpression right)
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

		// Token: 0x060030F3 RID: 12531 RVA: 0x000C0E44 File Offset: 0x000BF044
		private static bool IsStringType(TypeUsage type)
		{
			return TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.String);
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x0007B760 File Offset: 0x00079960
		private static bool IsBooleanType(TypeUsage type)
		{
			return TypeSemantics.IsPrimitiveType(type, PrimitiveTypeKind.Boolean);
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000C0E4E File Offset: 0x000BF04E
		private static bool IsSubOrSuperType(TypeUsage type1, TypeUsage type2)
		{
			return TypeSemantics.IsStructurallyEqual(type1, type2) || type1.IsSubtypeOf(type2) || type2.IsSubtypeOf(type1);
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x000C0E6C File Offset: 0x000BF06C
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

		// Token: 0x060030F7 RID: 12535 RVA: 0x000C1060 File Offset: 0x000BF260
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
					throw EntityUtil.EntitySqlError(bltInExpr.ErrCtx, Strings.ConcatBuiltinNotSupported);
				}
				List<TypeUsage> list = new List<TypeUsage>(2);
				list.Add(pair.Left.ResultType);
				list.Add(pair.Right.ResultType);
				bool flag = false;
				EdmFunction edmFunction = SemanticResolver.ResolveFunctionOverloads(metadataFunctionGroup.FunctionMetadata, list, false, out flag);
				if (edmFunction == null || flag)
				{
					throw EntityUtil.EntitySqlError(bltInExpr.ErrCtx, Strings.ConcatBuiltinNotSupported);
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
						throw EntityUtil.EntitySqlError(Strings.InvalidUnsignedTypeForUnaryMinusOperation(left.ResultType.EdmType.FullName));
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
			dictionary.Add(BuiltInKind.Element, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				throw EntityUtil.NotSupported(Strings.ElementOperatorIsNotSupported);
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
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.InvalidFlattenArgument);
				}
				if (!TypeSemantics.IsCollectionType(TypeHelpers.GetElementTypeUsage(dbExpression.ResultType)))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.InvalidFlattenArgument);
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
					return SemanticAnalyzer.ConvertSimpleInExpression(sr, pair.Left, pair.Right);
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
					sr.TypeResolver.BooleanType.Null()
				}, DbExpressionBuilder.False);
				return left2.Or(right);
			});
			dictionary.Add(BuiltInKind.NotIn, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertInExprArgs(bltInExpr, sr);
				if (pair.Right.ExpressionKind == DbExpressionKind.NewInstance)
				{
					return SemanticAnalyzer.ConvertSimpleInExpression(sr, pair.Left, pair.Right).Not();
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
					sr.TypeResolver.BooleanType.Null()
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
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.IsNullInvalidType);
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
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.IsNullInvalidType);
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
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.ExpressionTypeMustBeEntityType(Strings.CtxIsOf, dbExpression.ResultType.EdmType.BuiltInTypeKind.ToString(), dbExpression.ResultType.EdmType.FullName));
				}
				if (flag3 && !TypeSemantics.IsNominalType(dbExpression.ResultType))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.ExpressionTypeMustBeNominalType(Strings.CtxIsOf, dbExpression.ResultType.EdmType.BuiltInTypeKind.ToString(), dbExpression.ResultType.EdmType.FullName));
				}
				if (!flag3 && !TypeSemantics.IsEntityType(typeUsage))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg2.ErrCtx, Strings.TypeMustBeEntityType(Strings.CtxIsOf, typeUsage.EdmType.BuiltInTypeKind.ToString(), typeUsage.EdmType.FullName));
				}
				if (flag3 && !TypeSemantics.IsNominalType(typeUsage))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg2.ErrCtx, Strings.TypeMustBeNominalType(Strings.CtxIsOf, typeUsage.EdmType.BuiltInTypeKind.ToString(), typeUsage.EdmType.FullName));
				}
				if (!TypeSemantics.IsPolymorphicType(dbExpression.ResultType))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.TypeMustBeInheritableType);
				}
				if (!TypeSemantics.IsPolymorphicType(typeUsage))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg2.ErrCtx, Strings.TypeMustBeInheritableType);
				}
				if (!SemanticAnalyzer.IsSubOrSuperType(dbExpression.ResultType, typeUsage))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.ErrCtx, Strings.NotASuperOrSubType(dbExpression.ResultType.EdmType.FullName, typeUsage.EdmType.FullName));
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
					throw EntityUtil.EntitySqlError(bltInExpr.Arg2.ErrCtx, Strings.TypeMustBeEntityType(Strings.CtxTreat, typeUsage.EdmType.BuiltInTypeKind.ToString(), typeUsage.EdmType.FullName));
				}
				if (flag && !TypeSemantics.IsNominalType(typeUsage))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg2.ErrCtx, Strings.TypeMustBeNominalType(Strings.CtxTreat, typeUsage.EdmType.BuiltInTypeKind.ToString(), typeUsage.EdmType.FullName));
				}
				if (dbExpression == null)
				{
					dbExpression = typeUsage.Null();
				}
				else
				{
					if (!flag && !TypeSemantics.IsEntityType(dbExpression.ResultType))
					{
						throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.ExpressionTypeMustBeEntityType(Strings.CtxTreat, dbExpression.ResultType.EdmType.BuiltInTypeKind.ToString(), dbExpression.ResultType.EdmType.FullName));
					}
					if (flag && !TypeSemantics.IsNominalType(dbExpression.ResultType))
					{
						throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.ExpressionTypeMustBeNominalType(Strings.CtxTreat, dbExpression.ResultType.EdmType.BuiltInTypeKind.ToString(), dbExpression.ResultType.EdmType.FullName));
					}
				}
				if (!TypeSemantics.IsPolymorphicType(dbExpression.ResultType))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.TypeMustBeInheritableType);
				}
				if (!TypeSemantics.IsPolymorphicType(typeUsage))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg2.ErrCtx, Strings.TypeMustBeInheritableType);
				}
				if (!SemanticAnalyzer.IsSubOrSuperType(dbExpression.ResultType, typeUsage))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.NotASuperOrSubType(dbExpression.ResultType.EdmType.FullName, typeUsage.EdmType.FullName));
				}
				return dbExpression.TreatAs(TypeHelpers.GetReadOnlyType(typeUsage));
			});
			dictionary.Add(BuiltInKind.Cast, delegate(BuiltInExpr bltInExpr, SemanticResolver sr)
			{
				DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg1, sr);
				TypeUsage typeUsage = SemanticAnalyzer.ConvertTypeName(bltInExpr.Arg2, sr);
				if (!TypeSemantics.IsScalarType(typeUsage))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg2.ErrCtx, Strings.InvalidCastType);
				}
				if (dbExpression == null)
				{
					return typeUsage.Null();
				}
				if (!TypeSemantics.IsScalarType(dbExpression.ResultType))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.InvalidCastExpressionType);
				}
				if (!TypeSemantics.IsCastAllowed(dbExpression.ResultType, typeUsage))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.InvalidCast(dbExpression.ResultType.EdmType.FullName, typeUsage.EdmType.FullName));
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
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.ExpressionMustBeCollection);
				}
				TypeUsage elementTypeUsage = TypeHelpers.GetElementTypeUsage(dbExpression.ResultType);
				if (!flag2 && !TypeSemantics.IsEntityType(elementTypeUsage))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.OfTypeExpressionElementTypeMustBeEntityType(elementTypeUsage.EdmType.BuiltInTypeKind.ToString(), elementTypeUsage));
				}
				if (flag2 && !TypeSemantics.IsNominalType(elementTypeUsage))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.OfTypeExpressionElementTypeMustBeNominalType(elementTypeUsage.EdmType.BuiltInTypeKind.ToString(), elementTypeUsage));
				}
				if (!flag2 && !TypeSemantics.IsEntityType(typeUsage))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg2.ErrCtx, Strings.TypeMustBeEntityType(Strings.CtxOfType, typeUsage.EdmType.BuiltInTypeKind.ToString(), typeUsage.EdmType.FullName));
				}
				if (flag2 && !TypeSemantics.IsNominalType(typeUsage))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg2.ErrCtx, Strings.TypeMustBeNominalType(Strings.CtxOfType, typeUsage.EdmType.BuiltInTypeKind.ToString(), typeUsage.EdmType.FullName));
				}
				if (flag && typeUsage.EdmType.Abstract)
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg2.ErrCtx, Strings.OfTypeOnlyTypeArgumentCannotBeAbstract(typeUsage.EdmType.FullName));
				}
				if (!SemanticAnalyzer.IsSubOrSuperType(elementTypeUsage, typeUsage))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.NotASuperOrSubType(elementTypeUsage.EdmType.FullName, typeUsage.EdmType.FullName));
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
					dbExpression = sr.TypeResolver.StringType.Null();
				}
				else if (!SemanticAnalyzer.IsStringType(dbExpression.ResultType))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.LikeArgMustBeStringType);
				}
				DbExpression dbExpression2 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg2, sr);
				if (dbExpression2 == null)
				{
					dbExpression2 = sr.TypeResolver.StringType.Null();
				}
				else if (!SemanticAnalyzer.IsStringType(dbExpression2.ResultType))
				{
					throw EntityUtil.EntitySqlError(bltInExpr.Arg2.ErrCtx, Strings.LikeArgMustBeStringType);
				}
				DbExpression result;
				if (3 == bltInExpr.ArgCount)
				{
					DbExpression dbExpression3 = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg3, sr);
					if (dbExpression3 == null)
					{
						dbExpression3 = sr.TypeResolver.StringType.Null();
					}
					else if (!SemanticAnalyzer.IsStringType(dbExpression3.ResultType))
					{
						throw EntityUtil.EntitySqlError(bltInExpr.Arg3.ErrCtx, Strings.LikeArgMustBeStringType);
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

		// Token: 0x060030F8 RID: 12536 RVA: 0x000C15FC File Offset: 0x000BF7FC
		private static DbExpression ConvertBetweenExpr(BuiltInExpr bltInExpr, SemanticResolver sr)
		{
			Pair<DbExpression, DbExpression> pair = SemanticAnalyzer.ConvertValueExpressionsWithUntypedNulls(bltInExpr.Arg2, bltInExpr.Arg3, bltInExpr.Arg1.ErrCtx, () => Strings.BetweenLimitsCannotBeUntypedNulls, sr);
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(pair.Left.ResultType, pair.Right.ResultType);
			if (commonTypeUsage == null)
			{
				throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.BetweenLimitsTypesAreNotCompatible(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName));
			}
			if (!TypeSemantics.IsOrderComparableTo(pair.Left.ResultType, pair.Right.ResultType))
			{
				throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.BetweenLimitsTypesAreNotOrderComparable(pair.Left.ResultType.EdmType.FullName, pair.Right.ResultType.EdmType.FullName));
			}
			DbExpression dbExpression = SemanticAnalyzer.ConvertValueExpressionAllowUntypedNulls(bltInExpr.Arg1, sr);
			if (dbExpression == null)
			{
				dbExpression = commonTypeUsage.Null();
			}
			if (!TypeSemantics.IsOrderComparableTo(dbExpression.ResultType, commonTypeUsage))
			{
				throw EntityUtil.EntitySqlError(bltInExpr.Arg1.ErrCtx, Strings.BetweenValueIsNotOrderComparable(dbExpression.ResultType.EdmType.FullName, commonTypeUsage.EdmType.FullName));
			}
			return dbExpression.GreaterThanOrEqual(pair.Left).And(dbExpression.LessThanOrEqual(pair.Right));
		}

		// Token: 0x04001540 RID: 5440
		private SemanticResolver _sr;

		// Token: 0x04001541 RID: 5441
		private static readonly DbExpressionKind[] joinMap = new DbExpressionKind[]
		{
			DbExpressionKind.CrossJoin,
			DbExpressionKind.InnerJoin,
			DbExpressionKind.LeftOuterJoin,
			DbExpressionKind.FullOuterJoin
		};

		// Token: 0x04001542 RID: 5442
		private static readonly DbExpressionKind[] applyMap = new DbExpressionKind[]
		{
			DbExpressionKind.CrossApply,
			DbExpressionKind.OuterApply
		};

		// Token: 0x04001543 RID: 5443
		private static readonly Dictionary<Type, SemanticAnalyzer.AstExprConverter> _astExprConverters = SemanticAnalyzer.CreateAstExprConverters();

		// Token: 0x04001544 RID: 5444
		private static readonly Dictionary<BuiltInKind, SemanticAnalyzer.BuiltInExprConverter> _builtInExprConverter = SemanticAnalyzer.CreateBuiltInExprConverter();

		// Token: 0x0200064E RID: 1614
		// (Invoke) Token: 0x060043D3 RID: 17363
		private delegate ParseResult StatementConverter(Statement astExpr, SemanticResolver sr);

		// Token: 0x0200064F RID: 1615
		private sealed class InlineFunctionInfoImpl : InlineFunctionInfo
		{
			// Token: 0x060043D6 RID: 17366 RVA: 0x000F6352 File Offset: 0x000F4552
			internal InlineFunctionInfoImpl(FunctionDefinition functionDef, List<DbVariableReferenceExpression> parameters) : base(functionDef, parameters)
			{
			}

			// Token: 0x060043D7 RID: 17367 RVA: 0x000F635C File Offset: 0x000F455C
			internal override DbLambda GetLambda(SemanticResolver sr)
			{
				if (this._convertedDefinition == null)
				{
					if (this._convertingDefinition)
					{
						throw EntityUtil.EntitySqlError(this.FunctionDefAst.ErrCtx, Strings.Cqt_UDF_FunctionDefinitionWithCircularReference(this.FunctionDefAst.Name));
					}
					SemanticResolver sr2 = sr.CloneForInlineFunctionConversion();
					this._convertingDefinition = true;
					this._convertedDefinition = SemanticAnalyzer.ConvertInlineFunctionDefinition(this, sr2);
					this._convertingDefinition = false;
				}
				return this._convertedDefinition;
			}

			// Token: 0x04001EEF RID: 7919
			private DbLambda _convertedDefinition;

			// Token: 0x04001EF0 RID: 7920
			private bool _convertingDefinition;
		}

		// Token: 0x02000650 RID: 1616
		private sealed class GroupKeyInfo
		{
			// Token: 0x060043D8 RID: 17368 RVA: 0x000F63C2 File Offset: 0x000F45C2
			internal GroupKeyInfo(string name, DbExpression varBasedKeyExpr, DbExpression groupVarBasedKeyExpr, DbExpression groupAggBasedKeyExpr)
			{
				this.Name = name;
				this.VarRef = varBasedKeyExpr.ResultType.Variable(name);
				this.VarBasedKeyExpr = varBasedKeyExpr;
				this.GroupVarBasedKeyExpr = groupVarBasedKeyExpr;
				this.GroupAggBasedKeyExpr = groupAggBasedKeyExpr;
			}

			// Token: 0x17000BA7 RID: 2983
			// (get) Token: 0x060043D9 RID: 17369 RVA: 0x000F63F9 File Offset: 0x000F45F9
			// (set) Token: 0x060043DA RID: 17370 RVA: 0x000F6401 File Offset: 0x000F4601
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

			// Token: 0x04001EF1 RID: 7921
			internal readonly string Name;

			// Token: 0x04001EF2 RID: 7922
			private string[] _alternativeName;

			// Token: 0x04001EF3 RID: 7923
			internal readonly DbVariableReferenceExpression VarRef;

			// Token: 0x04001EF4 RID: 7924
			internal readonly DbExpression VarBasedKeyExpr;

			// Token: 0x04001EF5 RID: 7925
			internal readonly DbExpression GroupVarBasedKeyExpr;

			// Token: 0x04001EF6 RID: 7926
			internal readonly DbExpression GroupAggBasedKeyExpr;
		}

		// Token: 0x02000651 RID: 1617
		// (Invoke) Token: 0x060043DC RID: 17372
		private delegate ExpressionResolution AstExprConverter(Node astExpr, SemanticResolver sr);

		// Token: 0x02000652 RID: 1618
		// (Invoke) Token: 0x060043E0 RID: 17376
		private delegate DbExpression BuiltInExprConverter(BuiltInExpr astBltInExpr, SemanticResolver sr);
	}
}
