using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql.AST;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000270 RID: 624
	internal sealed class SemanticResolver
	{
		// Token: 0x060015C6 RID: 5574 RVA: 0x0006A2F7 File Offset: 0x000684F7
		internal static SemanticResolver Create(Perspective perspective, ParserOptions parserOptions, IEnumerable<DbParameterReferenceExpression> parameters, IEnumerable<DbVariableReferenceExpression> variables)
		{
			return new SemanticResolver(parserOptions, SemanticResolver.ProcessParameters(parameters, parserOptions), SemanticResolver.ProcessVariables(variables, parserOptions), new TypeResolver(perspective, parserOptions));
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x0006A314 File Offset: 0x00068514
		internal SemanticResolver CloneForInlineFunctionConversion()
		{
			return new SemanticResolver(this._parserOptions, this._parameters, this._variables, this._typeResolver);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0006A334 File Offset: 0x00068534
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

		// Token: 0x060015C9 RID: 5577 RVA: 0x0006A3E4 File Offset: 0x000685E4
		private static Dictionary<string, DbParameterReferenceExpression> ProcessParameters(IEnumerable<DbParameterReferenceExpression> paramDefs, ParserOptions parserOptions)
		{
			Dictionary<string, DbParameterReferenceExpression> dictionary = new Dictionary<string, DbParameterReferenceExpression>(parserOptions.NameComparer);
			if (paramDefs != null)
			{
				foreach (DbParameterReferenceExpression dbParameterReferenceExpression in paramDefs)
				{
					if (dictionary.ContainsKey(dbParameterReferenceExpression.ParameterName))
					{
						string message = Strings.MultipleDefinitionsOfParameter(dbParameterReferenceExpression.ParameterName);
						throw new EntitySqlException(message);
					}
					dictionary.Add(dbParameterReferenceExpression.ParameterName, dbParameterReferenceExpression);
				}
			}
			return dictionary;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x0006A464 File Offset: 0x00068664
		private static Dictionary<string, DbVariableReferenceExpression> ProcessVariables(IEnumerable<DbVariableReferenceExpression> varDefs, ParserOptions parserOptions)
		{
			Dictionary<string, DbVariableReferenceExpression> dictionary = new Dictionary<string, DbVariableReferenceExpression>(parserOptions.NameComparer);
			if (varDefs != null)
			{
				foreach (DbVariableReferenceExpression dbVariableReferenceExpression in varDefs)
				{
					if (dictionary.ContainsKey(dbVariableReferenceExpression.VariableName))
					{
						string message = Strings.MultipleDefinitionsOfVariable(dbVariableReferenceExpression.VariableName);
						throw new EntitySqlException(message);
					}
					dictionary.Add(dbVariableReferenceExpression.VariableName, dbVariableReferenceExpression);
				}
			}
			return dictionary;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x0006A4E4 File Offset: 0x000686E4
		internal Dictionary<string, DbParameterReferenceExpression> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x0006A4EC File Offset: 0x000686EC
		internal Dictionary<string, DbVariableReferenceExpression> Variables
		{
			get
			{
				return this._variables;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x0006A4F4 File Offset: 0x000686F4
		internal TypeResolver TypeResolver
		{
			get
			{
				return this._typeResolver;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x0006A4FC File Offset: 0x000686FC
		internal ParserOptions ParserOptions
		{
			get
			{
				return this._parserOptions;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x0006A504 File Offset: 0x00068704
		internal StringComparer NameComparer
		{
			get
			{
				return this._parserOptions.NameComparer;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x0006A511 File Offset: 0x00068711
		internal IEnumerable<ScopeRegion> ScopeRegions
		{
			get
			{
				return this._scopeRegions;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x0006A519 File Offset: 0x00068719
		internal ScopeRegion CurrentScopeRegion
		{
			get
			{
				return this._scopeRegions[this._scopeRegions.Count - 1];
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0006A533 File Offset: 0x00068733
		internal Scope CurrentScope
		{
			get
			{
				return this._scopeManager.CurrentScope;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x0006A540 File Offset: 0x00068740
		internal int CurrentScopeIndex
		{
			get
			{
				return this._scopeManager.CurrentScopeIndex;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0006A54D File Offset: 0x0006874D
		internal GroupAggregateInfo CurrentGroupAggregateInfo
		{
			get
			{
				return this._currentGroupAggregateInfo;
			}
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0006A558 File Offset: 0x00068758
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

		// Token: 0x060015D6 RID: 5590 RVA: 0x0006A656 File Offset: 0x00068856
		internal IDisposable EnterIgnoreEntityContainerNameResolution()
		{
			this._ignoreEntityContainerNameResolution = true;
			return new Disposer(delegate()
			{
				this._ignoreEntityContainerNameResolution = false;
			});
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0006A670 File Offset: 0x00068870
		internal ExpressionResolution ResolveSimpleName(string name, bool leftHandSideOfMemberAccess, ErrorContext errCtx)
		{
			ScopeEntry scopeEntry;
			int num;
			if (this.TryScopeLookup(name, out scopeEntry, out num))
			{
				if (scopeEntry.EntryKind == ScopeEntryKind.SourceVar && ((SourceScopeEntry)scopeEntry).IsJoinClauseLeftExpr)
				{
					string invalidJoinLeftCorrelation = Strings.InvalidJoinLeftCorrelation;
					throw EntitySqlException.Create(errCtx, invalidJoinLeftCorrelation, null);
				}
				this.SetScopeRegionCorrelationFlag(num);
				return new ValueExpression(this.GetExpressionFromScopeEntry(scopeEntry, num, name, errCtx));
			}
			else
			{
				EntityContainer defaultContainer = this.TypeResolver.Perspective.GetDefaultContainer();
				ExpressionResolution result;
				if (defaultContainer != null && this.TryResolveEntityContainerMemberAccess(defaultContainer, name, out result))
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

		// Token: 0x060015D8 RID: 5592 RVA: 0x0006A728 File Offset: 0x00068928
		internal MetadataMember ResolveSimpleFunctionName(string name, ErrorContext errCtx)
		{
			MetadataMember metadataMember = this.TypeResolver.ResolveUnqualifiedName(name, false, errCtx);
			if (metadataMember.MetadataMemberClass == MetadataMemberClass.Namespace)
			{
				EntityContainer defaultContainer = this.TypeResolver.Perspective.GetDefaultContainer();
				ExpressionResolution expressionResolution;
				if (defaultContainer != null && this.TryResolveEntityContainerMemberAccess(defaultContainer, name, out expressionResolution) && expressionResolution.ExpressionClass == ExpressionResolutionClass.MetadataMember)
				{
					metadataMember = (MetadataMember)expressionResolution;
				}
			}
			return metadataMember;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0006A780 File Offset: 0x00068980
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

		// Token: 0x060015DA RID: 5594 RVA: 0x0006A7BF File Offset: 0x000689BF
		internal MetadataMember ResolveMetadataMemberName(string[] name, ErrorContext errCtx)
		{
			return this.TypeResolver.ResolveMetadataMemberName(name, errCtx);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0006A7D0 File Offset: 0x000689D0
		internal ValueExpression ResolvePropertyAccess(DbExpression valueExpr, string name, ErrorContext errCtx)
		{
			DbExpression value;
			if (this.TryResolveAsPropertyAccess(valueExpr, name, out value))
			{
				return new ValueExpression(value);
			}
			if (this.TryResolveAsRefPropertyAccess(valueExpr, name, errCtx, out value))
			{
				return new ValueExpression(value);
			}
			if (TypeSemantics.IsCollectionType(valueExpr.ResultType))
			{
				string errorMessage = Strings.NotAMemberOfCollection(name, valueExpr.ResultType.EdmType.FullName);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			string errorMessage2 = Strings.NotAMemberOfType(name, valueExpr.ResultType.EdmType.FullName);
			throw EntitySqlException.Create(errCtx, errorMessage2, null);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0006A850 File Offset: 0x00068A50
		private bool TryResolveAsPropertyAccess(DbExpression valueExpr, string name, out DbExpression propertyExpr)
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

		// Token: 0x060015DD RID: 5597 RVA: 0x0006A8B0 File Offset: 0x00068AB0
		private bool TryResolveAsRefPropertyAccess(DbExpression valueExpr, string name, ErrorContext errCtx, out DbExpression propertyExpr)
		{
			propertyExpr = null;
			if (!TypeSemantics.IsReferenceType(valueExpr.ResultType))
			{
				return false;
			}
			DbExpression dbExpression = valueExpr.Deref();
			TypeUsage resultType = dbExpression.ResultType;
			if (this.TryResolveAsPropertyAccess(dbExpression, name, out propertyExpr))
			{
				return true;
			}
			string errorMessage = Strings.InvalidDeRefProperty(name, resultType.EdmType.FullName, valueExpr.ResultType.EdmType.FullName);
			throw EntitySqlException.Create(errCtx, errorMessage, null);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0006A918 File Offset: 0x00068B18
		internal ExpressionResolution ResolveEntityContainerMemberAccess(EntityContainer entityContainer, string name, ErrorContext errCtx)
		{
			ExpressionResolution result;
			if (this.TryResolveEntityContainerMemberAccess(entityContainer, name, out result))
			{
				return result;
			}
			string errorMessage = Strings.MemberDoesNotBelongToEntityContainer(name, entityContainer.Name);
			throw EntitySqlException.Create(errCtx, errorMessage, null);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0006A948 File Offset: 0x00068B48
		private bool TryResolveEntityContainerMemberAccess(EntityContainer entityContainer, string name, out ExpressionResolution resolution)
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

		// Token: 0x060015E0 RID: 5600 RVA: 0x0006A9C5 File Offset: 0x00068BC5
		internal MetadataMember ResolveMetadataMemberAccess(MetadataMember metadataMember, string name, ErrorContext errCtx)
		{
			return this.TypeResolver.ResolveMetadataMemberAccess(metadataMember, name, errCtx);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0006A9D8 File Offset: 0x00068BD8
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

		// Token: 0x060015E2 RID: 5602 RVA: 0x0006AA0C File Offset: 0x00068C0C
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

		// Token: 0x060015E3 RID: 5603 RVA: 0x0006AA8C File Offset: 0x00068C8C
		internal string GenerateInternalName(string hint)
		{
			return "_##" + hint + this._namegenCounter++.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x0006AAC4 File Offset: 0x00068CC4
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

		// Token: 0x060015E5 RID: 5605 RVA: 0x0006AB1C File Offset: 0x00068D1C
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

		// Token: 0x060015E6 RID: 5606 RVA: 0x0006ABDC File Offset: 0x00068DDC
		internal IDisposable EnterScopeRegion()
		{
			this._scopeManager.EnterScope();
			ScopeRegion item = new ScopeRegion(this._scopeManager, this.CurrentScopeIndex, this._scopeRegions.Count);
			this._scopeRegions.Add(item);
			return new Disposer(delegate()
			{
				this.CurrentScopeRegion.GroupAggregateInfos.Each(delegate(GroupAggregateInfo groupAggregateInfo)
				{
					groupAggregateInfo.DetachFromAstNode();
				});
				this.CurrentScopeRegion.RollbackAllScopes();
				this._scopeRegions.Remove(this.CurrentScopeRegion);
			});
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x0006AC2E File Offset: 0x00068E2E
		internal void RollbackToScope(int scopeIndex)
		{
			this._scopeManager.RollbackToScope(scopeIndex);
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0006AC3C File Offset: 0x00068E3C
		internal void EnterScope()
		{
			this._scopeManager.EnterScope();
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0006AC49 File Offset: 0x00068E49
		internal void LeaveScope()
		{
			this._scopeManager.LeaveScope();
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x0006AC58 File Offset: 0x00068E58
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

		// Token: 0x060015EB RID: 5611 RVA: 0x0006AC94 File Offset: 0x00068E94
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

		// Token: 0x060015EC RID: 5612 RVA: 0x0006ACDB File Offset: 0x00068EDB
		private void SetScopeRegionCorrelationFlag(int scopeIndex)
		{
			this.GetDefiningScopeRegion(scopeIndex).WasResolutionCorrelated = true;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x0006ACEA File Offset: 0x00068EEA
		internal IDisposable EnterFunctionAggregate(MethodExpr methodExpr, ErrorContext errCtx, out FunctionAggregateInfo aggregateInfo)
		{
			aggregateInfo = new FunctionAggregateInfo(methodExpr, errCtx, this._currentGroupAggregateInfo, this.CurrentScopeRegion);
			return this.EnterGroupAggregate(aggregateInfo);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x0006AD09 File Offset: 0x00068F09
		internal IDisposable EnterGroupPartition(GroupPartitionExpr groupPartitionExpr, ErrorContext errCtx, out GroupPartitionInfo aggregateInfo)
		{
			aggregateInfo = new GroupPartitionInfo(groupPartitionExpr, errCtx, this._currentGroupAggregateInfo, this.CurrentScopeRegion);
			return this.EnterGroupAggregate(aggregateInfo);
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0006AD28 File Offset: 0x00068F28
		internal IDisposable EnterGroupKeyDefinition(GroupAggregateKind aggregateKind, ErrorContext errCtx, out GroupKeyAggregateInfo aggregateInfo)
		{
			aggregateInfo = new GroupKeyAggregateInfo(aggregateKind, errCtx, this._currentGroupAggregateInfo, this.CurrentScopeRegion);
			return this.EnterGroupAggregate(aggregateInfo);
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x0006AD78 File Offset: 0x00068F78
		private IDisposable EnterGroupAggregate(GroupAggregateInfo aggregateInfo)
		{
			this._currentGroupAggregateInfo = aggregateInfo;
			return new Disposer(delegate()
			{
				this._currentGroupAggregateInfo = aggregateInfo.ContainingAggregate;
				aggregateInfo.ValidateAndComputeEvaluatingScopeRegion(this);
			});
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x0006ADB6 File Offset: 0x00068FB6
		internal static EdmFunction ResolveFunctionOverloads(IList<EdmFunction> functionsMetadata, IList<TypeUsage> argTypes, bool isGroupAggregateFunction, out bool isAmbiguous)
		{
			return FunctionOverloadResolver.ResolveFunctionOverloads(functionsMetadata, argTypes, new Func<TypeUsage, IEnumerable<TypeUsage>>(SemanticResolver.UntypedNullAwareFlattenArgumentType), new Func<TypeUsage, TypeUsage, IEnumerable<TypeUsage>>(SemanticResolver.UntypedNullAwareFlattenParameterType), new Func<TypeUsage, TypeUsage, bool>(SemanticResolver.UntypedNullAwareIsPromotableTo), new Func<TypeUsage, TypeUsage, bool>(SemanticResolver.UntypedNullAwareIsStructurallyEqual), isGroupAggregateFunction, out isAmbiguous);
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x0006ADF4 File Offset: 0x00068FF4
		internal static TFunctionMetadata ResolveFunctionOverloads<TFunctionMetadata, TFunctionParameterMetadata>(IList<TFunctionMetadata> functionsMetadata, IList<TypeUsage> argTypes, Func<TFunctionMetadata, IList<TFunctionParameterMetadata>> getSignatureParams, Func<TFunctionParameterMetadata, TypeUsage> getParameterTypeUsage, Func<TFunctionParameterMetadata, ParameterMode> getParameterMode, bool isGroupAggregateFunction, out bool isAmbiguous) where TFunctionMetadata : class
		{
			return FunctionOverloadResolver.ResolveFunctionOverloads<TFunctionMetadata, TFunctionParameterMetadata>(functionsMetadata, argTypes, getSignatureParams, getParameterTypeUsage, getParameterMode, new Func<TypeUsage, IEnumerable<TypeUsage>>(SemanticResolver.UntypedNullAwareFlattenArgumentType), new Func<TypeUsage, TypeUsage, IEnumerable<TypeUsage>>(SemanticResolver.UntypedNullAwareFlattenParameterType), new Func<TypeUsage, TypeUsage, bool>(SemanticResolver.UntypedNullAwareIsPromotableTo), new Func<TypeUsage, TypeUsage, bool>(SemanticResolver.UntypedNullAwareIsStructurallyEqual), isGroupAggregateFunction, out isAmbiguous);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0006AE40 File Offset: 0x00069040
		private static IEnumerable<TypeUsage> UntypedNullAwareFlattenArgumentType(TypeUsage argType)
		{
			if (argType == null)
			{
				return new TypeUsage[1];
			}
			return TypeSemantics.FlattenType(argType);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x0006AE60 File Offset: 0x00069060
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

		// Token: 0x060015F5 RID: 5621 RVA: 0x0006AE83 File Offset: 0x00069083
		private static bool UntypedNullAwareIsPromotableTo(TypeUsage fromType, TypeUsage toType)
		{
			if (fromType == null)
			{
				return !Helper.IsCollectionType(toType.EdmType);
			}
			return TypeSemantics.IsPromotableTo(fromType, toType);
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x0006AE9E File Offset: 0x0006909E
		private static bool UntypedNullAwareIsStructurallyEqual(TypeUsage fromType, TypeUsage toType)
		{
			if (fromType == null)
			{
				return SemanticResolver.UntypedNullAwareIsPromotableTo(fromType, toType);
			}
			return TypeSemantics.IsStructurallyEqual(fromType, toType);
		}

		// Token: 0x040007A9 RID: 1961
		private readonly ParserOptions _parserOptions;

		// Token: 0x040007AA RID: 1962
		private readonly Dictionary<string, DbParameterReferenceExpression> _parameters;

		// Token: 0x040007AB RID: 1963
		private readonly Dictionary<string, DbVariableReferenceExpression> _variables;

		// Token: 0x040007AC RID: 1964
		private readonly TypeResolver _typeResolver;

		// Token: 0x040007AD RID: 1965
		private readonly ScopeManager _scopeManager;

		// Token: 0x040007AE RID: 1966
		private readonly List<ScopeRegion> _scopeRegions = new List<ScopeRegion>();

		// Token: 0x040007AF RID: 1967
		private bool _ignoreEntityContainerNameResolution;

		// Token: 0x040007B0 RID: 1968
		private GroupAggregateInfo _currentGroupAggregateInfo;

		// Token: 0x040007B1 RID: 1969
		private uint _namegenCounter;
	}
}
