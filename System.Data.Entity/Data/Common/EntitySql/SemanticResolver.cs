using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.EntitySql.AST;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000339 RID: 825
	internal sealed class SemanticResolver
	{
		// Token: 0x06003102 RID: 12546 RVA: 0x000C1800 File Offset: 0x000BFA00
		internal static SemanticResolver Create(Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters, IEnumerable<DbVariableReferenceExpression> variables)
		{
			EntityUtil.CheckArgumentNull<Perspective>(perspective, "perspective");
			EntityUtil.CheckArgumentNull<ParserOptions>(parserOptions, "parserOptions");
			return new SemanticResolver(parserOptions, SemanticResolver.ProcessParameters(parameters, parserOptions), SemanticResolver.ProcessVariables(variables, parserOptions), new TypeResolver(perspective, parserOptions));
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x000C1835 File Offset: 0x000BFA35
		internal SemanticResolver CloneForInlineFunctionConversion()
		{
			return new SemanticResolver(this._parserOptions, this._parameters, this._variables, this._typeResolver);
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x000C1854 File Offset: 0x000BFA54
		private SemanticResolver(ParserOptions parserOptions, Dictionary<string, DbParameterReferenceExpression> parameters, Dictionary<string, DbVariableReferenceExpression> variables, TypeResolver typeResolver)
		{
			this._parserOptions = parserOptions;
			this._parameters = parameters;
			this._variables = variables;
			this._typeResolver = typeResolver;
			this._scopeManager = new ScopeManager(this.NameComparer);
			this.EnterScopeRegion();
			foreach (DbVariableReferenceExpression dbVariableReferenceExpression in this._variables.Values)
			{
				this.CurrentScope.Add(dbVariableReferenceExpression.VariableName, new FreeVariableScopeEntry(dbVariableReferenceExpression));
			}
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000C1904 File Offset: 0x000BFB04
		private static Dictionary<string, DbParameterReferenceExpression> ProcessParameters(IEnumerable<DbParameterReferenceExpression> paramDefs, ParserOptions parserOptions)
		{
			Dictionary<string, DbParameterReferenceExpression> dictionary = new Dictionary<string, DbParameterReferenceExpression>(parserOptions.NameComparer);
			if (paramDefs != null)
			{
				foreach (DbParameterReferenceExpression dbParameterReferenceExpression in paramDefs)
				{
					if (dictionary.ContainsKey(dbParameterReferenceExpression.ParameterName))
					{
						throw EntityUtil.EntitySqlError(Strings.MultipleDefinitionsOfParameter(dbParameterReferenceExpression.ParameterName));
					}
					dictionary.Add(dbParameterReferenceExpression.ParameterName, dbParameterReferenceExpression);
				}
			}
			return dictionary;
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x000C1984 File Offset: 0x000BFB84
		private static Dictionary<string, DbVariableReferenceExpression> ProcessVariables(IEnumerable<DbVariableReferenceExpression> varDefs, ParserOptions parserOptions)
		{
			Dictionary<string, DbVariableReferenceExpression> dictionary = new Dictionary<string, DbVariableReferenceExpression>(parserOptions.NameComparer);
			if (varDefs != null)
			{
				foreach (DbVariableReferenceExpression dbVariableReferenceExpression in varDefs)
				{
					if (dictionary.ContainsKey(dbVariableReferenceExpression.VariableName))
					{
						throw EntityUtil.EntitySqlError(Strings.MultipleDefinitionsOfVariable(dbVariableReferenceExpression.VariableName));
					}
					dictionary.Add(dbVariableReferenceExpression.VariableName, dbVariableReferenceExpression);
				}
			}
			return dictionary;
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06003107 RID: 12551 RVA: 0x000C1A04 File Offset: 0x000BFC04
		internal Dictionary<string, DbParameterReferenceExpression> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x000C1A0C File Offset: 0x000BFC0C
		internal Dictionary<string, DbVariableReferenceExpression> Variables
		{
			get
			{
				return this._variables;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x000C1A14 File Offset: 0x000BFC14
		internal TypeResolver TypeResolver
		{
			get
			{
				return this._typeResolver;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x000C1A1C File Offset: 0x000BFC1C
		internal ParserOptions ParserOptions
		{
			get
			{
				return this._parserOptions;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x000C1A24 File Offset: 0x000BFC24
		internal StringComparer NameComparer
		{
			get
			{
				return this._parserOptions.NameComparer;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x000C1A31 File Offset: 0x000BFC31
		internal IEnumerable<ScopeRegion> ScopeRegions
		{
			get
			{
				return this._scopeRegions;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600310D RID: 12557 RVA: 0x000C1A39 File Offset: 0x000BFC39
		internal ScopeRegion CurrentScopeRegion
		{
			get
			{
				return this._scopeRegions[this._scopeRegions.Count - 1];
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x000C1A53 File Offset: 0x000BFC53
		internal Scope CurrentScope
		{
			get
			{
				return this._scopeManager.CurrentScope;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x000C1A60 File Offset: 0x000BFC60
		internal int CurrentScopeIndex
		{
			get
			{
				return this._scopeManager.CurrentScopeIndex;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x000C1A6D File Offset: 0x000BFC6D
		internal GroupAggregateInfo CurrentGroupAggregateInfo
		{
			get
			{
				return this._currentGroupAggregateInfo;
			}
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000C1A78 File Offset: 0x000BFC78
		private DbExpression GetExpressionFromScopeEntry(ScopeEntry scopeEntry, int scopeIndex, string varName, ErrorContext errCtx)
		{
			DbExpression result = scopeEntry.GetExpression(varName, errCtx);
			if (this._currentGroupAggregateInfo != null)
			{
				ScopeRegion definingScopeRegion = this.GetDefiningScopeRegion(scopeIndex);
				if (definingScopeRegion.ScopeRegionIndex <= this._currentGroupAggregateInfo.DefiningScopeRegion.ScopeRegionIndex)
				{
					this._currentGroupAggregateInfo.UpdateScopeIndex(scopeIndex, this);
					IGroupExpressionExtendedInfo groupExpressionExtendedInfo = scopeEntry as IGroupExpressionExtendedInfo;
					if (groupExpressionExtendedInfo != null)
					{
						GroupAggregateInfo groupAggregateInfo = this._currentGroupAggregateInfo;
						while (groupAggregateInfo != null && groupAggregateInfo.DefiningScopeRegion.ScopeRegionIndex >= definingScopeRegion.ScopeRegionIndex && groupAggregateInfo.DefiningScopeRegion.ScopeRegionIndex != definingScopeRegion.ScopeRegionIndex)
						{
							groupAggregateInfo = groupAggregateInfo.ContainingAggregate;
						}
						if (groupAggregateInfo == null || groupAggregateInfo.DefiningScopeRegion.ScopeRegionIndex < definingScopeRegion.ScopeRegionIndex)
						{
							groupAggregateInfo = this._currentGroupAggregateInfo;
						}
						switch (groupAggregateInfo.AggregateKind)
						{
						case GroupAggregateKind.Function:
							if (groupExpressionExtendedInfo.GroupVarBasedExpression != null)
							{
								result = groupExpressionExtendedInfo.GroupVarBasedExpression;
							}
							break;
						case GroupAggregateKind.Partition:
							if (groupExpressionExtendedInfo.GroupAggBasedExpression != null)
							{
								result = groupExpressionExtendedInfo.GroupAggBasedExpression;
							}
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000C1B6D File Offset: 0x000BFD6D
		internal IDisposable EnterIgnoreEntityContainerNameResolution()
		{
			this._ignoreEntityContainerNameResolution = true;
			return new Disposer(delegate()
			{
				this._ignoreEntityContainerNameResolution = false;
			});
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000C1B88 File Offset: 0x000BFD88
		internal ExpressionResolution ResolveSimpleName(string name, bool leftHandSideOfMemberAccess, ErrorContext errCtx)
		{
			ScopeEntry scopeEntry;
			int num;
			if (this.TryScopeLookup(name, out scopeEntry, out num))
			{
				if (scopeEntry.EntryKind == ScopeEntryKind.SourceVar && ((SourceScopeEntry)scopeEntry).IsJoinClauseLeftExpr)
				{
					throw EntityUtil.EntitySqlError(errCtx, Strings.InvalidJoinLeftCorrelation);
				}
				this.SetScopeRegionCorrelationFlag(num);
				return new ValueExpression(this.GetExpressionFromScopeEntry(scopeEntry, num, name, errCtx));
			}
			else
			{
				EntityContainer defaultContainer = this.TypeResolver.Perspective.GetDefaultContainer();
				ExpressionResolution result;
				if (defaultContainer != null && this.TryResolveEntityContainerMemberAccess(defaultContainer, name, errCtx, out result))
				{
					return result;
				}
				EntityContainer entityContainer;
				if (!this._ignoreEntityContainerNameResolution && this.TypeResolver.Perspective.TryGetEntityContainer(name, this._parserOptions.NameComparisonCaseInsensitive, out entityContainer))
				{
					return new EntityContainerExpression(entityContainer);
				}
				return this.TypeResolver.ResolveUnqualifiedName(name, leftHandSideOfMemberAccess, errCtx);
			}
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000C1C3C File Offset: 0x000BFE3C
		internal MetadataMember ResolveSimpleFunctionName(string name, ErrorContext errCtx)
		{
			MetadataMember metadataMember = this.TypeResolver.ResolveUnqualifiedName(name, false, errCtx);
			if (metadataMember.MetadataMemberClass == MetadataMemberClass.Namespace)
			{
				EntityContainer defaultContainer = this.TypeResolver.Perspective.GetDefaultContainer();
				ExpressionResolution expressionResolution;
				if (defaultContainer != null && this.TryResolveEntityContainerMemberAccess(defaultContainer, name, errCtx, out expressionResolution) && expressionResolution.ExpressionClass == ExpressionResolutionClass.MetadataMember)
				{
					metadataMember = (MetadataMember)expressionResolution;
				}
			}
			return metadataMember;
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000C1C94 File Offset: 0x000BFE94
		private bool TryScopeLookup(string key, out ScopeEntry scopeEntry, out int scopeIndex)
		{
			scopeEntry = null;
			scopeIndex = -1;
			for (int i = this.CurrentScopeIndex; i >= 0; i--)
			{
				if (this._scopeManager.GetScopeByIndex(i).TryLookup(key, out scopeEntry))
				{
					scopeIndex = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000C1CD3 File Offset: 0x000BFED3
		internal MetadataMember ResolveMetadataMemberName(string[] name, ErrorContext errCtx)
		{
			return this.TypeResolver.ResolveMetadataMemberName(name, errCtx);
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000C1CE4 File Offset: 0x000BFEE4
		internal ValueExpression ResolvePropertyAccess(DbExpression valueExpr, string name, ErrorContext errCtx)
		{
			DbExpression value;
			if (this.TryResolveAsPropertyAccess(valueExpr, name, errCtx, out value))
			{
				return new ValueExpression(value);
			}
			if (this.TryResolveAsRefPropertyAccess(valueExpr, name, errCtx, out value))
			{
				return new ValueExpression(value);
			}
			if (TypeSemantics.IsCollectionType(valueExpr.ResultType))
			{
				throw EntityUtil.EntitySqlError(errCtx, Strings.NotAMemberOfCollection(name, valueExpr.ResultType.EdmType.FullName));
			}
			throw EntityUtil.EntitySqlError(errCtx, Strings.NotAMemberOfType(name, valueExpr.ResultType.EdmType.FullName));
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000C1D60 File Offset: 0x000BFF60
		private bool TryResolveAsPropertyAccess(DbExpression valueExpr, string name, ErrorContext errCtx, out DbExpression propertyExpr)
		{
			propertyExpr = null;
			EdmMember member;
			if (Helper.IsStructuralType(valueExpr.ResultType.EdmType) && this.TypeResolver.Perspective.TryGetMember((StructuralType)valueExpr.ResultType.EdmType, name, this._parserOptions.NameComparisonCaseInsensitive, out member))
			{
				propertyExpr = DbExpressionBuilder.CreatePropertyExpressionFromMember(valueExpr, member);
				return true;
			}
			return false;
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000C1DC0 File Offset: 0x000BFFC0
		private bool TryResolveAsRefPropertyAccess(DbExpression valueExpr, string name, ErrorContext errCtx, out DbExpression propertyExpr)
		{
			propertyExpr = null;
			if (!TypeSemantics.IsReferenceType(valueExpr.ResultType))
			{
				return false;
			}
			DbExpression dbExpression = valueExpr.Deref();
			TypeUsage resultType = dbExpression.ResultType;
			if (this.TryResolveAsPropertyAccess(dbExpression, name, errCtx, out propertyExpr))
			{
				return true;
			}
			throw EntityUtil.EntitySqlError(errCtx, Strings.InvalidDeRefProperty(name, resultType.EdmType.FullName, valueExpr.ResultType.EdmType.FullName));
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000C1E24 File Offset: 0x000C0024
		internal ExpressionResolution ResolveEntityContainerMemberAccess(EntityContainer entityContainer, string name, ErrorContext errCtx)
		{
			ExpressionResolution result;
			if (this.TryResolveEntityContainerMemberAccess(entityContainer, name, errCtx, out result))
			{
				return result;
			}
			throw EntityUtil.EntitySqlError(errCtx, Strings.MemberDoesNotBelongToEntityContainer(name, entityContainer.Name));
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000C1E54 File Offset: 0x000C0054
		private bool TryResolveEntityContainerMemberAccess(EntityContainer entityContainer, string name, ErrorContext errCtx, out ExpressionResolution resolution)
		{
			EntitySetBase targetSet;
			if (this.TypeResolver.Perspective.TryGetExtent(entityContainer, name, this._parserOptions.NameComparisonCaseInsensitive, out targetSet))
			{
				resolution = new ValueExpression(targetSet.Scan());
				return true;
			}
			EdmFunction edmFunction;
			if (this.TypeResolver.Perspective.TryGetFunctionImport(entityContainer, name, this._parserOptions.NameComparisonCaseInsensitive, out edmFunction))
			{
				resolution = new MetadataFunctionGroup(edmFunction.FullName, new EdmFunction[]
				{
					edmFunction
				});
				return true;
			}
			resolution = null;
			return false;
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000C1ED2 File Offset: 0x000C00D2
		internal MetadataMember ResolveMetadataMemberAccess(MetadataMember metadataMember, string name, ErrorContext errCtx)
		{
			return this.TypeResolver.ResolveMetadataMemberAccess(metadataMember, name, errCtx);
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000C1EE4 File Offset: 0x000C00E4
		internal bool TryResolveInternalAggregateName(string name, ErrorContext errCtx, out DbExpression dbExpression)
		{
			ScopeEntry scopeEntry;
			int scopeRegionCorrelationFlag;
			if (this.TryScopeLookup(name, out scopeEntry, out scopeRegionCorrelationFlag))
			{
				this.SetScopeRegionCorrelationFlag(scopeRegionCorrelationFlag);
				dbExpression = scopeEntry.GetExpression(name, errCtx);
				return true;
			}
			dbExpression = null;
			return false;
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000C1F18 File Offset: 0x000C0118
		internal bool TryResolveDotExprAsGroupKeyAlternativeName(DotExpr dotExpr, out ValueExpression groupKeyResolution)
		{
			groupKeyResolution = null;
			string[] array;
			ScopeEntry scopeEntry;
			int num;
			if (this.IsInAnyGroupScope() && dotExpr.IsMultipartIdentifier(out array) && this.TryScopeLookup(TypeResolver.GetFullName(array), out scopeEntry, out num))
			{
				IGetAlternativeName getAlternativeName = scopeEntry as IGetAlternativeName;
				if (getAlternativeName != null && getAlternativeName.AlternativeName != null && array.SequenceEqual(getAlternativeName.AlternativeName, this.NameComparer))
				{
					this.SetScopeRegionCorrelationFlag(num);
					groupKeyResolution = new ValueExpression(this.GetExpressionFromScopeEntry(scopeEntry, num, TypeResolver.GetFullName(array), dotExpr.ErrCtx));
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000C1F98 File Offset: 0x000C0198
		internal string GenerateInternalName(string hint)
		{
			string str = "_##";
			uint namegenCounter = this._namegenCounter;
			this._namegenCounter = namegenCounter + 1U;
			return str + hint + namegenCounter.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000C1FCC File Offset: 0x000C01CC
		private string CreateNewAlias(DbExpression expr)
		{
			DbScanExpression dbScanExpression = expr as DbScanExpression;
			if (dbScanExpression != null)
			{
				return dbScanExpression.Target.Name;
			}
			DbPropertyExpression dbPropertyExpression = expr as DbPropertyExpression;
			if (dbPropertyExpression != null)
			{
				return dbPropertyExpression.Property.Name;
			}
			DbVariableReferenceExpression dbVariableReferenceExpression = expr as DbVariableReferenceExpression;
			if (dbVariableReferenceExpression != null)
			{
				return dbVariableReferenceExpression.VariableName;
			}
			return this.GenerateInternalName(string.Empty);
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000C2024 File Offset: 0x000C0224
		internal string InferAliasName(AliasedExpr aliasedExpr, DbExpression convertedExpression)
		{
			if (aliasedExpr.Alias != null)
			{
				return aliasedExpr.Alias.Name;
			}
			Identifier identifier = aliasedExpr.Expr as Identifier;
			if (identifier != null)
			{
				return identifier.Name;
			}
			DotExpr dotExpr = aliasedExpr.Expr as DotExpr;
			string[] array;
			if (dotExpr != null && dotExpr.IsMultipartIdentifier(out array))
			{
				return array[array.Length - 1];
			}
			return this.CreateNewAlias(convertedExpression);
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x000C2084 File Offset: 0x000C0284
		internal IDisposable EnterScopeRegion()
		{
			this._scopeManager.EnterScope();
			ScopeRegion item = new ScopeRegion(this._scopeManager, this.CurrentScopeIndex, this._scopeRegions.Count);
			this._scopeRegions.Add(item);
			return new Disposer(delegate()
			{
				this.CurrentScopeRegion.GroupAggregateInfos.ForEach(delegate(GroupAggregateInfo groupAggregateInfo)
				{
					groupAggregateInfo.DetachFromAstNode();
				});
				this.CurrentScopeRegion.RollbackAllScopes();
				this._scopeRegions.Remove(this.CurrentScopeRegion);
			});
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000C20D6 File Offset: 0x000C02D6
		internal void RollbackToScope(int scopeIndex)
		{
			this._scopeManager.RollbackToScope(scopeIndex);
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000C20E4 File Offset: 0x000C02E4
		internal void EnterScope()
		{
			this._scopeManager.EnterScope();
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000C20F1 File Offset: 0x000C02F1
		internal void LeaveScope()
		{
			this._scopeManager.LeaveScope();
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000C2100 File Offset: 0x000C0300
		internal bool IsInAnyGroupScope()
		{
			for (int i = 0; i < this._scopeRegions.Count; i++)
			{
				if (this._scopeRegions[i].IsAggregating)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000C213C File Offset: 0x000C033C
		internal ScopeRegion GetDefiningScopeRegion(int scopeIndex)
		{
			for (int i = this._scopeRegions.Count - 1; i >= 0; i--)
			{
				if (this._scopeRegions[i].ContainsScope(scopeIndex))
				{
					return this._scopeRegions[i];
				}
			}
			return null;
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000C2183 File Offset: 0x000C0383
		private void SetScopeRegionCorrelationFlag(int scopeIndex)
		{
			this.GetDefiningScopeRegion(scopeIndex).WasResolutionCorrelated = true;
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x000C2192 File Offset: 0x000C0392
		internal IDisposable EnterFunctionAggregate(MethodExpr methodExpr, ErrorContext errCtx, out FunctionAggregateInfo aggregateInfo)
		{
			aggregateInfo = new FunctionAggregateInfo(methodExpr, errCtx, this._currentGroupAggregateInfo, this.CurrentScopeRegion);
			return this.EnterGroupAggregate(aggregateInfo);
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x000C21B1 File Offset: 0x000C03B1
		internal IDisposable EnterGroupPartition(GroupPartitionExpr groupPartitionExpr, ErrorContext errCtx, out GroupPartitionInfo aggregateInfo)
		{
			aggregateInfo = new GroupPartitionInfo(groupPartitionExpr, errCtx, this._currentGroupAggregateInfo, this.CurrentScopeRegion);
			return this.EnterGroupAggregate(aggregateInfo);
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x000C21D0 File Offset: 0x000C03D0
		internal IDisposable EnterGroupKeyDefinition(GroupAggregateKind aggregateKind, ErrorContext errCtx, out GroupKeyAggregateInfo aggregateInfo)
		{
			aggregateInfo = new GroupKeyAggregateInfo(aggregateKind, errCtx, this._currentGroupAggregateInfo, this.CurrentScopeRegion);
			return this.EnterGroupAggregate(aggregateInfo);
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x000C21F0 File Offset: 0x000C03F0
		private IDisposable EnterGroupAggregate(GroupAggregateInfo aggregateInfo)
		{
			this._currentGroupAggregateInfo = aggregateInfo;
			return new Disposer(delegate()
			{
				this._currentGroupAggregateInfo = aggregateInfo.ContainingAggregate;
				aggregateInfo.ValidateAndComputeEvaluatingScopeRegion(this);
			});
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000C222E File Offset: 0x000C042E
		internal static EdmFunction ResolveFunctionOverloads(IList<EdmFunction> functionsMetadata, IList<TypeUsage> argTypes, bool isGroupAggregateFunction, out bool isAmbiguous)
		{
			return FunctionOverloadResolver.ResolveFunctionOverloads(functionsMetadata, argTypes, new Func<TypeUsage, IEnumerable<TypeUsage>>(SemanticResolver.UntypedNullAwareFlattenArgumentType), new Func<TypeUsage, TypeUsage, IEnumerable<TypeUsage>>(SemanticResolver.UntypedNullAwareFlattenParameterType), new Func<TypeUsage, TypeUsage, bool>(SemanticResolver.UntypedNullAwareIsPromotableTo), new Func<TypeUsage, TypeUsage, bool>(SemanticResolver.UntypedNullAwareIsStructurallyEqual), isGroupAggregateFunction, out isAmbiguous);
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x000C226C File Offset: 0x000C046C
		internal static TFunctionMetadata ResolveFunctionOverloads<TFunctionMetadata, TFunctionParameterMetadata>(IList<TFunctionMetadata> functionsMetadata, IList<TypeUsage> argTypes, Func<TFunctionMetadata, IList<TFunctionParameterMetadata>> getSignatureParams, Func<TFunctionParameterMetadata, TypeUsage> getParameterTypeUsage, Func<TFunctionParameterMetadata, ParameterMode> getParameterMode, bool isGroupAggregateFunction, out bool isAmbiguous) where TFunctionMetadata : class
		{
			return FunctionOverloadResolver.ResolveFunctionOverloads<TFunctionMetadata, TFunctionParameterMetadata>(functionsMetadata, argTypes, getSignatureParams, getParameterTypeUsage, getParameterMode, new Func<TypeUsage, IEnumerable<TypeUsage>>(SemanticResolver.UntypedNullAwareFlattenArgumentType), new Func<TypeUsage, TypeUsage, IEnumerable<TypeUsage>>(SemanticResolver.UntypedNullAwareFlattenParameterType), new Func<TypeUsage, TypeUsage, bool>(SemanticResolver.UntypedNullAwareIsPromotableTo), new Func<TypeUsage, TypeUsage, bool>(SemanticResolver.UntypedNullAwareIsStructurallyEqual), isGroupAggregateFunction, out isAmbiguous);
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x000C22B8 File Offset: 0x000C04B8
		private static IEnumerable<TypeUsage> UntypedNullAwareFlattenArgumentType(TypeUsage argType)
		{
			if (argType == null)
			{
				return new TypeUsage[1];
			}
			return TypeSemantics.FlattenType(argType);
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000C22D8 File Offset: 0x000C04D8
		private static IEnumerable<TypeUsage> UntypedNullAwareFlattenParameterType(TypeUsage paramType, TypeUsage argType)
		{
			if (argType == null)
			{
				return new TypeUsage[]
				{
					paramType
				};
			}
			return TypeSemantics.FlattenType(paramType);
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000C22FB File Offset: 0x000C04FB
		private static bool UntypedNullAwareIsPromotableTo(TypeUsage fromType, TypeUsage toType)
		{
			if (fromType == null)
			{
				return !Helper.IsCollectionType(toType.EdmType);
			}
			return TypeSemantics.IsPromotableTo(fromType, toType);
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000C2316 File Offset: 0x000C0516
		private static bool UntypedNullAwareIsStructurallyEqual(TypeUsage fromType, TypeUsage toType)
		{
			if (fromType == null)
			{
				return SemanticResolver.UntypedNullAwareIsPromotableTo(fromType, toType);
			}
			return TypeSemantics.IsStructurallyEqual(fromType, toType);
		}

		// Token: 0x0400154C RID: 5452
		private readonly ParserOptions _parserOptions;

		// Token: 0x0400154D RID: 5453
		private readonly Dictionary<string, DbParameterReferenceExpression> _parameters;

		// Token: 0x0400154E RID: 5454
		private readonly Dictionary<string, DbVariableReferenceExpression> _variables;

		// Token: 0x0400154F RID: 5455
		private readonly TypeResolver _typeResolver;

		// Token: 0x04001550 RID: 5456
		private readonly ScopeManager _scopeManager;

		// Token: 0x04001551 RID: 5457
		private readonly List<ScopeRegion> _scopeRegions = new List<ScopeRegion>();

		// Token: 0x04001552 RID: 5458
		private bool _ignoreEntityContainerNameResolution;

		// Token: 0x04001553 RID: 5459
		private GroupAggregateInfo _currentGroupAggregateInfo;

		// Token: 0x04001554 RID: 5460
		private uint _namegenCounter;
	}
}
