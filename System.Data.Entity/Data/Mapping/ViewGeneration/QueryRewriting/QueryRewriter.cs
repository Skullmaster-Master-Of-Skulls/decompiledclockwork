using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Common.Utils.Boolean;
using System.Data.Entity;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Mapping.ViewGeneration.Utils;
using System.Data.Mapping.ViewGeneration.Validation;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200028B RID: 651
	internal class QueryRewriter
	{
		// Token: 0x060026E9 RID: 9961 RVA: 0x00096B00 File Offset: 0x00094D00
		internal QueryRewriter(EdmType generatedType, ViewgenContext context, ViewGenMode typesGenerationMode)
		{
			this._typesGenerationMode = typesGenerationMode;
			this._context = context;
			this._generatedType = generatedType;
			this._domainMap = context.MemberMaps.LeftDomainMap;
			this._config = context.Config;
			this._identifiers = context.CqlIdentifiers;
			this._qp = new RewritingProcessor<Tile<FragmentQuery>>(new DefaultTileProcessor<FragmentQuery>(context.LeftFragmentQP));
			this._extentPath = new MemberPath(context.Extent);
			this._keyAttributes = new List<MemberPath>(MemberPath.GetKeyMembers(context.Extent, this._domainMap));
			foreach (LeftCellWrapper leftCellWrapper in this._context.AllWrappersForExtent)
			{
				FragmentQuery fragmentQuery = leftCellWrapper.FragmentQuery;
				Tile<FragmentQuery> item = QueryRewriter.CreateTile(fragmentQuery);
				this._fragmentQueries.Add(fragmentQuery);
				this._views.Add(item);
			}
			this.AdjustMemberDomainsForUpdateViews();
			this._domainQuery = this.GetDomainQuery(this.FragmentQueries, generatedType);
			this._usedViews = new HashSet<FragmentQuery>();
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x00096C64 File Offset: 0x00094E64
		internal void GenerateViewComponents()
		{
			this.EnsureExtentIsFullyMapped(this._usedViews);
			this.GenerateCaseStatements(this._domainMap.ConditionMembers(this._extentPath.Extent), this._usedViews);
			this.AddTrivialCaseStatementsForConditionMembers();
			if (this._usedViews.Count == 0 || this._errorLog.Count > 0)
			{
				ExceptionHelpers.ThrowMappingException(this._errorLog, this._config);
			}
			this._topLevelWhereClause = this.GetTopLevelWhereClause(this._usedViews);
			ViewTarget viewTarget = this._context.ViewTarget;
			this._usedCells = this.RemapFromVariables();
			BasicViewGenerator basicViewGenerator = new BasicViewGenerator(this._context.MemberMaps.ProjectedSlotMap, this._usedCells, this._domainQuery, this._context, this._domainMap, this._errorLog, this._config);
			this._basicView = basicViewGenerator.CreateViewExpression();
			bool flag = this._context.LeftFragmentQP.IsContainedIn(this._basicView.LeftFragmentQuery, this._domainQuery);
			if (flag)
			{
				this._topLevelWhereClause = BoolExpression.True;
			}
			if (this._errorLog.Count > 0)
			{
				ExceptionHelpers.ThrowMappingException(this._errorLog, this._config);
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060026EB RID: 9963 RVA: 0x00096D90 File Offset: 0x00094F90
		internal ViewgenContext ViewgenContext
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x00096D98 File Offset: 0x00094F98
		internal Dictionary<MemberPath, CaseStatement> CaseStatements
		{
			get
			{
				return this._caseStatements;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060026ED RID: 9965 RVA: 0x00096DA0 File Offset: 0x00094FA0
		internal BoolExpression TopLevelWhereClause
		{
			get
			{
				return this._topLevelWhereClause;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060026EE RID: 9966 RVA: 0x00096DA8 File Offset: 0x00094FA8
		internal CellTreeNode BasicView
		{
			get
			{
				return this._basicView.MakeCopy();
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060026EF RID: 9967 RVA: 0x00096DB5 File Offset: 0x00094FB5
		internal List<LeftCellWrapper> UsedCells
		{
			get
			{
				return this._usedCells;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060026F0 RID: 9968 RVA: 0x00096DBD File Offset: 0x00094FBD
		private IEnumerable<FragmentQuery> FragmentQueries
		{
			get
			{
				return this._fragmentQueries;
			}
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x00096DC8 File Offset: 0x00094FC8
		private IEnumerable<Constant> GetDomain(MemberPath currentPath)
		{
			if (this._context.ViewTarget == ViewTarget.QueryView && MemberPath.EqualityComparer.Equals(currentPath, this._extentPath))
			{
				IEnumerable<EdmType> types;
				if (this._typesGenerationMode == ViewGenMode.OfTypeOnlyViews)
				{
					types = new HashSet<EdmType>
					{
						this._generatedType
					};
				}
				else
				{
					types = MetadataHelper.GetTypeAndSubtypesOf(this._generatedType, this._context.EdmItemCollection, false);
				}
				return QueryRewriter.GetTypeConstants(types);
			}
			return this._domainMap.GetDomain(currentPath);
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x00096E40 File Offset: 0x00095040
		private void AdjustMemberDomainsForUpdateViews()
		{
			ViewTarget viewTarget = this._context.ViewTarget;
			if (viewTarget == ViewTarget.UpdateView)
			{
				List<MemberPath> list = new List<MemberPath>(this._domainMap.ConditionMembers(this._extentPath.Extent));
				using (List<MemberPath>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MemberPath currentPath = enumerator.Current;
						IEnumerable<Constant> domain = this._domainMap.GetDomain(currentPath);
						Constant constant = domain.FirstOrDefault((Constant domainValue) => QueryRewriter.IsDefaultValue(domainValue, currentPath));
						if (constant != null)
						{
							this.RemoveUnusedValueFromStoreDomain(constant, currentPath);
						}
						domain = this._domainMap.GetDomain(currentPath);
						Constant constant2 = domain.FirstOrDefault((Constant domainValue) => domainValue is NegatedConstant);
						if (constant2 != null)
						{
							this.RemoveUnusedValueFromStoreDomain(constant2, currentPath);
						}
					}
				}
			}
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x00096F50 File Offset: 0x00095150
		private void RemoveUnusedValueFromStoreDomain(Constant domainValue, MemberPath currentPath)
		{
			BoolExpression whereClause = this.CreateMemberCondition(currentPath, domainValue);
			HashSet<FragmentQuery> outputUsedViews = new HashSet<FragmentQuery>();
			bool flag = false;
			Tile<FragmentQuery> tile;
			if (this.FindRewritingAndUsedViews(this._keyAttributes, whereClause, outputUsedViews, out tile))
			{
				CellTreeNode cellTreeNode = QueryRewriter.TileToCellTree(tile, this._context);
				flag = !cellTreeNode.IsEmptyRightFragmentQuery;
			}
			if (!flag)
			{
				Set<Constant> set = new Set<Constant>(this._domainMap.GetDomain(currentPath), Constant.EqualityComparer);
				set.Remove(domainValue);
				this._domainMap.UpdateConditionMemberDomain(currentPath, set);
				foreach (FragmentQuery fragmentQuery in this._fragmentQueries)
				{
					fragmentQuery.Condition.FixDomainMap(this._domainMap);
				}
			}
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x00097020 File Offset: 0x00095220
		internal FragmentQuery GetDomainQuery(IEnumerable<FragmentQuery> fragmentQueries, EdmType generatedType)
		{
			if (this._context.ViewTarget == ViewTarget.QueryView)
			{
				BoolExpression whereClause;
				if (generatedType == null)
				{
					whereClause = BoolExpression.True;
				}
				else
				{
					IEnumerable<EdmType> types;
					if (this._typesGenerationMode == ViewGenMode.OfTypeOnlyViews)
					{
						types = new HashSet<EdmType>
						{
							this._generatedType
						};
					}
					else
					{
						types = MetadataHelper.GetTypeAndSubtypesOf(generatedType, this._context.EdmItemCollection, false);
					}
					Domain domain = new Domain(QueryRewriter.GetTypeConstants(types), this._domainMap.GetDomain(this._extentPath));
					whereClause = BoolExpression.CreateLiteral(new TypeRestriction(new MemberProjectedSlot(this._extentPath), domain), this._domainMap);
				}
				return FragmentQuery.Create(this._keyAttributes, whereClause);
			}
			IEnumerable<BoolExpression> source = from fragmentQuery in fragmentQueries
			select fragmentQuery.Condition;
			BoolExpression whereClause2 = BoolExpression.CreateOr(source.ToArray<BoolExpression>());
			return FragmentQuery.Create(this._keyAttributes, whereClause2);
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x00097108 File Offset: 0x00095308
		private bool AddRewritingToCaseStatement(Tile<FragmentQuery> rewriting, CaseStatement caseStatement, MemberPath currentPath, Constant domainValue)
		{
			BoolExpression condition = BoolExpression.True;
			bool flag = this._qp.IsContainedIn(QueryRewriter.CreateTile(this._domainQuery), rewriting);
			bool flag2 = this._qp.IsDisjointFrom(QueryRewriter.CreateTile(this._domainQuery), rewriting);
			if (flag2)
			{
				return false;
			}
			ProjectedSlot value;
			if (domainValue.HasNotNull())
			{
				value = new MemberProjectedSlot(currentPath);
			}
			else
			{
				value = new ConstantProjectedSlot(domainValue, currentPath);
			}
			if (!flag)
			{
				condition = QueryRewriter.TileToBoolExpr(rewriting);
			}
			else
			{
				condition = BoolExpression.True;
			}
			caseStatement.AddWhenThen(condition, value);
			return flag;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x00097188 File Offset: 0x00095388
		private void EnsureConfigurationIsFullyMapped(MemberPath currentPath, BoolExpression currentWhereClause, HashSet<FragmentQuery> outputUsedViews, ErrorLog errorLog)
		{
			foreach (Constant constant in this.GetDomain(currentPath))
			{
				if (constant != Constant.Undefined)
				{
					BoolExpression boolExpression = this.CreateMemberCondition(currentPath, constant);
					BoolExpression boolExpression2 = BoolExpression.CreateAnd(new BoolExpression[]
					{
						currentWhereClause,
						boolExpression
					});
					Tile<FragmentQuery> tile;
					if (!this.FindRewritingAndUsedViews(this._keyAttributes, boolExpression2, outputUsedViews, out tile))
					{
						if (!ErrorPatternMatcher.FindMappingErrors(this._context, this._domainMap, this._errorLog))
						{
							StringBuilder stringBuilder = new StringBuilder();
							string p = StringUtil.FormatInvariant("{0}", new object[]
							{
								this._extentPath
							});
							BoolExpression condition = tile.Query.Condition;
							condition.ExpensiveSimplify();
							if (condition.RepresentsAllTypeConditions)
							{
								string viewGen_Extent = Strings.ViewGen_Extent;
								stringBuilder.AppendLine(Strings.ViewGen_Cannot_Recover_Types(viewGen_Extent, p));
							}
							else
							{
								string viewGen_Entities = Strings.ViewGen_Entities;
								stringBuilder.AppendLine(Strings.ViewGen_Cannot_Disambiguate_MultiConstant(viewGen_Entities, p));
							}
							RewritingValidator.EntityConfigurationToUserString(condition, stringBuilder);
							ErrorLog.Record record = new ErrorLog.Record(true, ViewGenErrorCode.AmbiguousMultiConstants, stringBuilder.ToString(), this._context.AllWrappersForExtent, string.Empty);
							errorLog.AddEntry(record);
						}
					}
					else
					{
						TypeConstant typeConstant = constant as TypeConstant;
						if (typeConstant != null)
						{
							EdmType edmType = typeConstant.EdmType;
							List<MemberPath> list = QueryRewriter.GetNonConditionalScalarMembers(edmType, currentPath, this._domainMap).Union(QueryRewriter.GetNonConditionalComplexMembers(edmType, currentPath, this._domainMap)).ToList<MemberPath>();
							IEnumerable<MemberPath> attributes;
							if (list.Count > 0 && !this.FindRewritingAndUsedViews(list, boolExpression2, outputUsedViews, out tile, out attributes))
							{
								list = new List<MemberPath>(from a in list
								where !a.IsPartOfKey
								select a);
								this.AddUnrecoverableAttributesError(attributes, boolExpression, errorLog);
							}
							else
							{
								foreach (MemberPath currentPath2 in QueryRewriter.GetConditionalComplexMembers(edmType, currentPath, this._domainMap))
								{
									this.EnsureConfigurationIsFullyMapped(currentPath2, boolExpression2, outputUsedViews, errorLog);
								}
								foreach (MemberPath currentPath3 in QueryRewriter.GetConditionalScalarMembers(edmType, currentPath, this._domainMap))
								{
									this.EnsureConfigurationIsFullyMapped(currentPath3, boolExpression2, outputUsedViews, errorLog);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x00097434 File Offset: 0x00095634
		private static List<string> GetTypeBasedMemberPathList(IEnumerable<MemberPath> nonConditionalScalarAttributes)
		{
			List<string> list = new List<string>();
			foreach (MemberPath memberPath in nonConditionalScalarAttributes)
			{
				EdmMember leafEdmMember = memberPath.LeafEdmMember;
				List<string> list2 = list;
				string name = leafEdmMember.DeclaringType.Name;
				string str = ".";
				EdmMember edmMember = leafEdmMember;
				list2.Add(name + str + ((edmMember != null) ? edmMember.ToString() : null));
			}
			return list;
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000974AC File Offset: 0x000956AC
		private void AddUnrecoverableAttributesError(IEnumerable<MemberPath> attributes, BoolExpression domainAddedWhereClause, ErrorLog errorLog)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string p = StringUtil.FormatInvariant("{0}", new object[]
			{
				this._extentPath
			});
			string viewGen_Extent = Strings.ViewGen_Extent;
			string p2 = StringUtil.ToCommaSeparatedString(QueryRewriter.GetTypeBasedMemberPathList(attributes));
			stringBuilder.AppendLine(Strings.ViewGen_Cannot_Recover_Attributes(p2, viewGen_Extent, p));
			RewritingValidator.EntityConfigurationToUserString(domainAddedWhereClause, stringBuilder);
			ErrorLog.Record record = new ErrorLog.Record(true, ViewGenErrorCode.AttributesUnrecoverable, stringBuilder.ToString(), this._context.AllWrappersForExtent, string.Empty);
			errorLog.AddEntry(record);
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x0009752C File Offset: 0x0009572C
		private void GenerateCaseStatements(IEnumerable<MemberPath> members, HashSet<FragmentQuery> outputUsedViews)
		{
			IEnumerable<LeftCellWrapper> source = from w in this._context.AllWrappersForExtent
			where this._usedViews.Contains(w.FragmentQuery)
			select w;
			ViewgenContext context = this._context;
			CellTreeOpType opType = CellTreeOpType.Union;
			CellTreeNode[] children = (from wrapper in source
			select new LeafCellTreeNode(this._context, wrapper)).ToArray<LeafCellTreeNode>();
			CellTreeNode rightDomainQuery = new OpCellTreeNode(context, opType, children);
			foreach (MemberPath memberPath in members)
			{
				List<Constant> list = this.GetDomain(memberPath).ToList<Constant>();
				CaseStatement caseStatement = new CaseStatement(memberPath);
				Tile<FragmentQuery> tile = null;
				bool flag = list.Count != 2 || !list.Contains(Constant.Null, Constant.EqualityComparer) || !list.Contains(Constant.NotNull, Constant.EqualityComparer);
				foreach (Constant constant in list)
				{
					if (constant == Constant.Undefined && this._context.ViewTarget == ViewTarget.QueryView)
					{
						caseStatement.AddWhenThen(BoolExpression.False, new ConstantProjectedSlot(Constant.Undefined, memberPath));
					}
					else
					{
						FragmentQuery fragmentQuery = this.CreateMemberConditionQuery(memberPath, constant);
						Tile<FragmentQuery> tile2;
						if (this.FindRewritingAndUsedViews(fragmentQuery.Attributes, fragmentQuery.Condition, outputUsedViews, out tile2))
						{
							if (this._context.ViewTarget == ViewTarget.UpdateView)
							{
								tile = ((tile != null) ? this._qp.Union(tile, tile2) : tile2);
							}
							if (flag)
							{
								bool flag2 = this.AddRewritingToCaseStatement(tile2, caseStatement, memberPath, constant);
								if (flag2)
								{
									break;
								}
							}
						}
						else if (!QueryRewriter.IsDefaultValue(constant, memberPath) && !ErrorPatternMatcher.FindMappingErrors(this._context, this._domainMap, this._errorLog))
						{
							StringBuilder stringBuilder = new StringBuilder();
							string text = StringUtil.FormatInvariant("{0}", new object[]
							{
								this._extentPath
							});
							string p = (this._context.ViewTarget == ViewTarget.QueryView) ? Strings.ViewGen_Entities : Strings.ViewGen_Tuples;
							if (this._context.ViewTarget == ViewTarget.QueryView)
							{
								stringBuilder.AppendLine(Strings.Viewgen_CannotGenerateQueryViewUnderNoValidation(text));
							}
							else
							{
								stringBuilder.AppendLine(Strings.ViewGen_Cannot_Disambiguate_MultiConstant(p, text));
							}
							RewritingValidator.EntityConfigurationToUserString(fragmentQuery.Condition, stringBuilder, this._context.ViewTarget == ViewTarget.UpdateView);
							ErrorLog.Record record = new ErrorLog.Record(true, ViewGenErrorCode.AmbiguousMultiConstants, stringBuilder.ToString(), this._context.AllWrappersForExtent, string.Empty);
							this._errorLog.AddEntry(record);
						}
					}
				}
				if (this._errorLog.Count == 0)
				{
					if (this._context.ViewTarget == ViewTarget.UpdateView && flag)
					{
						this.AddElseDefaultToCaseStatement(memberPath, caseStatement, list, rightDomainQuery, tile);
					}
					if (caseStatement.Clauses.Count > 0)
					{
						this._caseStatements[memberPath] = caseStatement;
					}
				}
			}
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x00097834 File Offset: 0x00095A34
		private void AddElseDefaultToCaseStatement(MemberPath currentPath, CaseStatement caseStatement, List<Constant> domain, CellTreeNode rightDomainQuery, Tile<FragmentQuery> unionCaseRewriting)
		{
			Constant constant;
			bool flag = Domain.TryGetDefaultValueForMemberPath(currentPath, out constant);
			if (!flag || !domain.Contains(constant))
			{
				CellTreeNode cellTreeNode = QueryRewriter.TileToCellTree(unionCaseRewriting, this._context);
				FragmentQuery fragmentQuery = this._context.RightFragmentQP.Difference(rightDomainQuery.RightFragmentQuery, cellTreeNode.RightFragmentQuery);
				if (this._context.RightFragmentQP.IsSatisfiable(fragmentQuery))
				{
					if (flag)
					{
						caseStatement.AddWhenThen(BoolExpression.True, new ConstantProjectedSlot(constant, currentPath));
						return;
					}
					fragmentQuery.Condition.ExpensiveSimplify();
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine(Strings.ViewGen_No_Default_Value_For_Configuration(currentPath.PathToString(new bool?(false))));
					RewritingValidator.EntityConfigurationToUserString(fragmentQuery.Condition, stringBuilder);
					this._errorLog.AddEntry(new ErrorLog.Record(true, ViewGenErrorCode.NoDefaultValue, stringBuilder.ToString(), this._context.AllWrappersForExtent, string.Empty));
				}
			}
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x00097914 File Offset: 0x00095B14
		private BoolExpression GetTopLevelWhereClause(HashSet<FragmentQuery> outputUsedViews)
		{
			BoolExpression boolExpression = BoolExpression.True;
			Tile<FragmentQuery> tile;
			if (this._context.ViewTarget == ViewTarget.QueryView && !this._domainQuery.Condition.IsTrue && this.FindRewritingAndUsedViews(this._keyAttributes, this._domainQuery.Condition, outputUsedViews, out tile))
			{
				boolExpression = QueryRewriter.TileToBoolExpr(tile);
				boolExpression.ExpensiveSimplify();
			}
			return boolExpression;
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x00097970 File Offset: 0x00095B70
		internal void EnsureExtentIsFullyMapped(HashSet<FragmentQuery> outputUsedViews)
		{
			if (this._context.ViewTarget == ViewTarget.QueryView && this._config.IsValidationEnabled)
			{
				this.EnsureConfigurationIsFullyMapped(this._extentPath, BoolExpression.True, outputUsedViews, this._errorLog);
				if (this._errorLog.Count > 0)
				{
					ExceptionHelpers.ThrowMappingException(this._errorLog, this._config);
					return;
				}
			}
			else
			{
				if (this._config.IsValidationEnabled)
				{
					foreach (MemberPath memberPath in this._context.MemberMaps.ProjectedSlotMap.Members)
					{
						Constant constant;
						if (memberPath.IsScalarType() && !memberPath.IsPartOfKey && !this._domainMap.IsConditionMember(memberPath) && !Domain.TryGetDefaultValueForMemberPath(memberPath, out constant))
						{
							HashSet<MemberPath> hashSet = new HashSet<MemberPath>(this._keyAttributes);
							hashSet.Add(memberPath);
							foreach (LeftCellWrapper leftCellWrapper in this._context.AllWrappersForExtent)
							{
								FragmentQuery fragmentQuery = leftCellWrapper.FragmentQuery;
								FragmentQuery query = new FragmentQuery(fragmentQuery.Description, fragmentQuery.FromVariable, hashSet, fragmentQuery.Condition);
								Tile<FragmentQuery> toAvoid = QueryRewriter.CreateTile(FragmentQuery.Create(this._keyAttributes, BoolExpression.CreateNot(fragmentQuery.Condition)));
								Tile<FragmentQuery> tile;
								IEnumerable<MemberPath> enumerable;
								if (!this.RewriteQuery(QueryRewriter.CreateTile(query), toAvoid, out tile, out enumerable, false))
								{
									Domain.GetDefaultValueForMemberPath(memberPath, new LeftCellWrapper[]
									{
										leftCellWrapper
									}, this._config);
								}
							}
						}
					}
				}
				using (List<Tile<FragmentQuery>>.Enumerator enumerator3 = this._views.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Tile<FragmentQuery> toFill = enumerator3.Current;
						Tile<FragmentQuery> toAvoid2 = QueryRewriter.CreateTile(FragmentQuery.Create(this._keyAttributes, BoolExpression.CreateNot(toFill.Query.Condition)));
						Tile<FragmentQuery> tile2;
						IEnumerable<MemberPath> enumerable2;
						if (!this.RewriteQuery(toFill, toAvoid2, out tile2, out enumerable2, true))
						{
							LeftCellWrapper leftCellWrapper2 = this._context.AllWrappersForExtent.First((LeftCellWrapper lcr) => lcr.FragmentQuery.Equals(toFill.Query));
							ErrorLog.Record record = new ErrorLog.Record(true, ViewGenErrorCode.ImpopssibleCondition, Strings.Viewgen_QV_RewritingNotFound(leftCellWrapper2.RightExtent.ToString()), leftCellWrapper2.Cells, string.Empty);
							this._errorLog.AddEntry(record);
						}
						else
						{
							outputUsedViews.UnionWith(tile2.GetNamedQueries());
						}
					}
				}
			}
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x00097C4C File Offset: 0x00095E4C
		private List<LeftCellWrapper> RemapFromVariables()
		{
			List<LeftCellWrapper> list = new List<LeftCellWrapper>();
			int num = 0;
			Dictionary<BoolLiteral, BoolLiteral> dictionary = new Dictionary<BoolLiteral, BoolLiteral>(BoolLiteral.EqualityIdentifierComparer);
			foreach (LeftCellWrapper leftCellWrapper in this._context.AllWrappersForExtent)
			{
				if (this._usedViews.Contains(leftCellWrapper.FragmentQuery))
				{
					list.Add(leftCellWrapper);
					int cellNumber = leftCellWrapper.OnlyInputCell.CellNumber;
					if (num != cellNumber)
					{
						dictionary[new CellIdBoolean(this._identifiers, cellNumber)] = new CellIdBoolean(this._identifiers, num);
					}
					num++;
				}
			}
			if (dictionary.Count > 0)
			{
				this._topLevelWhereClause = this._topLevelWhereClause.RemapLiterals(dictionary);
				Dictionary<MemberPath, CaseStatement> dictionary2 = new Dictionary<MemberPath, CaseStatement>();
				foreach (KeyValuePair<MemberPath, CaseStatement> keyValuePair in this._caseStatements)
				{
					CaseStatement caseStatement = new CaseStatement(keyValuePair.Key);
					foreach (CaseStatement.WhenThen whenThen in keyValuePair.Value.Clauses)
					{
						caseStatement.AddWhenThen(whenThen.Condition.RemapLiterals(dictionary), whenThen.Value);
					}
					dictionary2[keyValuePair.Key] = caseStatement;
				}
				this._caseStatements = dictionary2;
			}
			return list;
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x00097DF0 File Offset: 0x00095FF0
		internal void AddTrivialCaseStatementsForConditionMembers()
		{
			for (int i = 0; i < this._context.MemberMaps.ProjectedSlotMap.Count; i++)
			{
				MemberPath memberPath = this._context.MemberMaps.ProjectedSlotMap[i];
				if (!memberPath.IsScalarType() && !this._caseStatements.ContainsKey(memberPath))
				{
					Constant value = new TypeConstant(memberPath.EdmType);
					CaseStatement caseStatement = new CaseStatement(memberPath);
					caseStatement.AddWhenThen(BoolExpression.True, new ConstantProjectedSlot(value, memberPath));
					this._caseStatements[memberPath] = caseStatement;
				}
			}
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x00097E7C File Offset: 0x0009607C
		private bool FindRewritingAndUsedViews(IEnumerable<MemberPath> attributes, BoolExpression whereClause, HashSet<FragmentQuery> outputUsedViews, out Tile<FragmentQuery> rewriting)
		{
			IEnumerable<MemberPath> enumerable;
			return this.FindRewritingAndUsedViews(attributes, whereClause, outputUsedViews, out rewriting, out enumerable);
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x00097E96 File Offset: 0x00096096
		private bool FindRewritingAndUsedViews(IEnumerable<MemberPath> attributes, BoolExpression whereClause, HashSet<FragmentQuery> outputUsedViews, out Tile<FragmentQuery> rewriting, out IEnumerable<MemberPath> notCoveredAttributes)
		{
			if (this.FindRewriting(attributes, whereClause, out rewriting, out notCoveredAttributes))
			{
				outputUsedViews.UnionWith(rewriting.GetNamedQueries());
				return true;
			}
			return false;
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x00097EB8 File Offset: 0x000960B8
		private bool FindRewriting(IEnumerable<MemberPath> attributes, BoolExpression whereClause, out Tile<FragmentQuery> rewriting, out IEnumerable<MemberPath> notCoveredAttributes)
		{
			Tile<FragmentQuery> toFill = QueryRewriter.CreateTile(FragmentQuery.Create(attributes, whereClause));
			Tile<FragmentQuery> toAvoid = QueryRewriter.CreateTile(FragmentQuery.Create(this._keyAttributes, BoolExpression.CreateNot(whereClause)));
			bool isRelaxed = this._context.ViewTarget == ViewTarget.UpdateView;
			return this.RewriteQuery(toFill, toAvoid, out rewriting, out notCoveredAttributes, isRelaxed);
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x00097F08 File Offset: 0x00096108
		private bool RewriteQuery(Tile<FragmentQuery> toFill, Tile<FragmentQuery> toAvoid, out Tile<FragmentQuery> rewriting, out IEnumerable<MemberPath> notCoveredAttributes, bool isRelaxed)
		{
			notCoveredAttributes = new List<MemberPath>();
			FragmentQuery fragmentQuery = toFill.Query;
			if (this._context.TryGetCachedRewriting(fragmentQuery, out rewriting))
			{
				return true;
			}
			IEnumerable<Tile<FragmentQuery>> relevantViews = this.GetRelevantViews(fragmentQuery, isRelaxed);
			FragmentQuery query = fragmentQuery;
			if (!this.RewriteQueryCached(QueryRewriter.CreateTile(FragmentQuery.Create(fragmentQuery.Condition)), toAvoid, relevantViews, out rewriting))
			{
				if (!isRelaxed)
				{
					return false;
				}
				fragmentQuery = FragmentQuery.Create(fragmentQuery.Attributes, BoolExpression.CreateAndNot(fragmentQuery.Condition, rewriting.Query.Condition));
				if (this._qp.IsEmpty(QueryRewriter.CreateTile(fragmentQuery)) || !this.RewriteQueryCached(QueryRewriter.CreateTile(FragmentQuery.Create(fragmentQuery.Condition)), toAvoid, relevantViews, out rewriting))
				{
					return false;
				}
			}
			if (fragmentQuery.Attributes.Count == 0)
			{
				return true;
			}
			Dictionary<MemberPath, FragmentQuery> dictionary = new Dictionary<MemberPath, FragmentQuery>();
			foreach (MemberPath key in this.NonKeys(fragmentQuery.Attributes))
			{
				dictionary[key] = fragmentQuery;
			}
			if (dictionary.Count == 0 || this.CoverAttributes(ref rewriting, fragmentQuery, dictionary))
			{
				this.GetUsedViewsAndRemoveTrueSurrogate(ref rewriting);
				this._context.SetCachedRewriting(query, rewriting);
				return true;
			}
			if (isRelaxed)
			{
				foreach (MemberPath key2 in this.NonKeys(fragmentQuery.Attributes))
				{
					FragmentQuery fragmentQuery2;
					if (dictionary.TryGetValue(key2, out fragmentQuery2))
					{
						dictionary[key2] = FragmentQuery.Create(BoolExpression.CreateAndNot(fragmentQuery.Condition, fragmentQuery2.Condition));
					}
					else
					{
						dictionary[key2] = fragmentQuery;
					}
				}
				if (this.CoverAttributes(ref rewriting, fragmentQuery, dictionary))
				{
					this.GetUsedViewsAndRemoveTrueSurrogate(ref rewriting);
					this._context.SetCachedRewriting(query, rewriting);
					return true;
				}
			}
			notCoveredAttributes = dictionary.Keys;
			return false;
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x000980F0 File Offset: 0x000962F0
		private bool RewriteQueryCached(Tile<FragmentQuery> toFill, Tile<FragmentQuery> toAvoid, IEnumerable<Tile<FragmentQuery>> views, out Tile<FragmentQuery> rewriting)
		{
			if (!this._context.TryGetCachedRewriting(toFill.Query, out rewriting))
			{
				bool flag = this._qp.RewriteQuery(toFill, toAvoid, views, out rewriting);
				if (flag)
				{
					this._context.SetCachedRewriting(toFill.Query, rewriting);
				}
				return flag;
			}
			return true;
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x00098140 File Offset: 0x00096340
		private bool CoverAttributes(ref Tile<FragmentQuery> rewriting, FragmentQuery toFillQuery, Dictionary<MemberPath, FragmentQuery> attributeConditions)
		{
			HashSet<FragmentQuery> hashSet = new HashSet<FragmentQuery>(rewriting.GetNamedQueries());
			foreach (FragmentQuery fragmentQuery in hashSet)
			{
				foreach (MemberPath projectedAttribute in this.NonKeys(fragmentQuery.Attributes))
				{
					this.CoverAttribute(projectedAttribute, fragmentQuery, attributeConditions, toFillQuery);
				}
				if (attributeConditions.Count == 0)
				{
					return true;
				}
			}
			Tile<FragmentQuery> tile = null;
			foreach (FragmentQuery fragmentQuery2 in this._fragmentQueries)
			{
				foreach (MemberPath projectedAttribute2 in this.NonKeys(fragmentQuery2.Attributes))
				{
					if (this.CoverAttribute(projectedAttribute2, fragmentQuery2, attributeConditions, toFillQuery))
					{
						tile = ((tile == null) ? QueryRewriter.CreateTile(fragmentQuery2) : this._qp.Union(tile, QueryRewriter.CreateTile(fragmentQuery2)));
					}
				}
				if (attributeConditions.Count == 0)
				{
					break;
				}
			}
			if (attributeConditions.Count == 0)
			{
				rewriting = this._qp.Join(rewriting, tile);
				return true;
			}
			return false;
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x000982C8 File Offset: 0x000964C8
		private bool CoverAttribute(MemberPath projectedAttribute, FragmentQuery view, Dictionary<MemberPath, FragmentQuery> attributeConditions, FragmentQuery toFillQuery)
		{
			FragmentQuery fragmentQuery;
			if (attributeConditions.TryGetValue(projectedAttribute, out fragmentQuery))
			{
				fragmentQuery = FragmentQuery.Create(BoolExpression.CreateAndNot(fragmentQuery.Condition, view.Condition));
				if (this._qp.IsEmpty(QueryRewriter.CreateTile(fragmentQuery)))
				{
					attributeConditions.Remove(projectedAttribute);
				}
				else
				{
					attributeConditions[projectedAttribute] = fragmentQuery;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x00098320 File Offset: 0x00096520
		private IEnumerable<Tile<FragmentQuery>> GetRelevantViews(FragmentQuery query, bool isRelaxed)
		{
			Set<MemberPath> variables = this.GetVariables(query);
			Tile<FragmentQuery> tile = null;
			List<Tile<FragmentQuery>> list = new List<Tile<FragmentQuery>>();
			Tile<FragmentQuery> tile2 = null;
			foreach (Tile<FragmentQuery> tile3 in this._views)
			{
				if (this.GetVariables(tile3.Query).Overlaps(variables))
				{
					tile = ((tile == null) ? tile3 : this._qp.Union(tile, tile3));
					list.Add(tile3);
				}
				else if (this.IsTrue(tile3.Query) && tile2 == null)
				{
					tile2 = tile3;
				}
			}
			if (tile != null && this.IsTrue(tile.Query))
			{
				return list;
			}
			if (tile2 == null)
			{
				Tile<FragmentQuery> tile4 = null;
				foreach (FragmentQuery query2 in this._fragmentQueries)
				{
					tile4 = ((tile4 == null) ? QueryRewriter.CreateTile(query2) : this._qp.Union(tile4, QueryRewriter.CreateTile(query2)));
					if (this.IsTrue(tile4.Query))
					{
						tile2 = QueryRewriter.TrueViewSurrogate;
						break;
					}
				}
			}
			if (tile2 != null)
			{
				list.Add(tile2);
				return list;
			}
			return this._views;
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x0009846C File Offset: 0x0009666C
		private HashSet<FragmentQuery> GetUsedViewsAndRemoveTrueSurrogate(ref Tile<FragmentQuery> rewriting)
		{
			HashSet<FragmentQuery> hashSet = new HashSet<FragmentQuery>(rewriting.GetNamedQueries());
			if (!hashSet.Contains(QueryRewriter.TrueViewSurrogate.Query))
			{
				return hashSet;
			}
			hashSet.Remove(QueryRewriter.TrueViewSurrogate.Query);
			Tile<FragmentQuery> tile = null;
			IEnumerable<FragmentQuery> enumerable = hashSet.Concat(this._fragmentQueries);
			foreach (FragmentQuery fragmentQuery in enumerable)
			{
				tile = ((tile == null) ? QueryRewriter.CreateTile(fragmentQuery) : this._qp.Union(tile, QueryRewriter.CreateTile(fragmentQuery)));
				hashSet.Add(fragmentQuery);
				if (this.IsTrue(tile.Query))
				{
					rewriting = rewriting.Replace(QueryRewriter.TrueViewSurrogate, tile);
					return hashSet;
				}
			}
			return hashSet;
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x00098540 File Offset: 0x00096740
		private BoolExpression CreateMemberCondition(MemberPath path, Constant domainValue)
		{
			return FragmentQuery.CreateMemberCondition(path, domainValue, this._domainMap);
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x0009854F File Offset: 0x0009674F
		private FragmentQuery CreateMemberConditionQuery(MemberPath currentPath, Constant domainValue)
		{
			return QueryRewriter.CreateMemberConditionQuery(currentPath, domainValue, this._keyAttributes, this._domainMap);
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x00098564 File Offset: 0x00096764
		internal static FragmentQuery CreateMemberConditionQuery(MemberPath currentPath, Constant domainValue, IEnumerable<MemberPath> keyAttributes, MemberDomainMap domainMap)
		{
			BoolExpression whereClause = FragmentQuery.CreateMemberCondition(currentPath, domainValue, domainMap);
			IEnumerable<MemberPath> attrs = keyAttributes;
			if (domainValue is NegatedConstant)
			{
				attrs = keyAttributes.Concat(new MemberPath[]
				{
					currentPath
				});
			}
			return FragmentQuery.Create(attrs, whereClause);
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x0009859C File Offset: 0x0009679C
		private static TileNamed<FragmentQuery> CreateTile(FragmentQuery query)
		{
			return new TileNamed<FragmentQuery>(query);
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000985A4 File Offset: 0x000967A4
		private static IEnumerable<Constant> GetTypeConstants(IEnumerable<EdmType> types)
		{
			foreach (EdmType type in types)
			{
				yield return new TypeConstant(type);
			}
			IEnumerator<EdmType> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x000985B4 File Offset: 0x000967B4
		private static IEnumerable<MemberPath> GetNonConditionalScalarMembers(EdmType edmType, MemberPath currentPath, MemberDomainMap domainMap)
		{
			return currentPath.GetMembers(edmType, new bool?(true), new bool?(false), null, domainMap);
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000985E0 File Offset: 0x000967E0
		private static IEnumerable<MemberPath> GetConditionalComplexMembers(EdmType edmType, MemberPath currentPath, MemberDomainMap domainMap)
		{
			return currentPath.GetMembers(edmType, new bool?(false), new bool?(true), null, domainMap);
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x0009860C File Offset: 0x0009680C
		private static IEnumerable<MemberPath> GetNonConditionalComplexMembers(EdmType edmType, MemberPath currentPath, MemberDomainMap domainMap)
		{
			return currentPath.GetMembers(edmType, new bool?(false), new bool?(false), null, domainMap);
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x00098638 File Offset: 0x00096838
		private static IEnumerable<MemberPath> GetConditionalScalarMembers(EdmType edmType, MemberPath currentPath, MemberDomainMap domainMap)
		{
			return currentPath.GetMembers(edmType, new bool?(true), new bool?(true), null, domainMap);
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x00098662 File Offset: 0x00096862
		private IEnumerable<MemberPath> NonKeys(IEnumerable<MemberPath> attributes)
		{
			return from attr in attributes
			where !attr.IsPartOfKey
			select attr;
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x0009868C File Offset: 0x0009688C
		internal static CellTreeNode TileToCellTree(Tile<FragmentQuery> tile, ViewgenContext context)
		{
			if (tile.OpKind == TileOpKind.Named)
			{
				FragmentQuery view = ((TileNamed<FragmentQuery>)tile).NamedQuery;
				LeftCellWrapper cellWrapper = context.AllWrappersForExtent.First((LeftCellWrapper w) => w.FragmentQuery == view);
				return new LeafCellTreeNode(context, cellWrapper);
			}
			CellTreeOpType opType;
			switch (tile.OpKind)
			{
			case TileOpKind.Union:
				opType = CellTreeOpType.Union;
				break;
			case TileOpKind.Join:
				opType = CellTreeOpType.IJ;
				break;
			case TileOpKind.AntiSemiJoin:
				opType = CellTreeOpType.LASJ;
				break;
			default:
				return null;
			}
			return new OpCellTreeNode(context, opType, new CellTreeNode[]
			{
				QueryRewriter.TileToCellTree(tile.Arg1, context),
				QueryRewriter.TileToCellTree(tile.Arg2, context)
			});
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x00098730 File Offset: 0x00096930
		private static BoolExpression TileToBoolExpr(Tile<FragmentQuery> tile)
		{
			switch (tile.OpKind)
			{
			case TileOpKind.Union:
				return BoolExpression.CreateOr(new BoolExpression[]
				{
					QueryRewriter.TileToBoolExpr(tile.Arg1),
					QueryRewriter.TileToBoolExpr(tile.Arg2)
				});
			case TileOpKind.Join:
				return BoolExpression.CreateAnd(new BoolExpression[]
				{
					QueryRewriter.TileToBoolExpr(tile.Arg1),
					QueryRewriter.TileToBoolExpr(tile.Arg2)
				});
			case TileOpKind.AntiSemiJoin:
				return BoolExpression.CreateAnd(new BoolExpression[]
				{
					QueryRewriter.TileToBoolExpr(tile.Arg1),
					BoolExpression.CreateNot(QueryRewriter.TileToBoolExpr(tile.Arg2))
				});
			case TileOpKind.Named:
			{
				FragmentQuery namedQuery = ((TileNamed<FragmentQuery>)tile).NamedQuery;
				if (namedQuery.Condition.IsAlwaysTrue())
				{
					return BoolExpression.True;
				}
				return namedQuery.FromVariable;
			}
			default:
				return null;
			}
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x00098804 File Offset: 0x00096A04
		private static bool IsDefaultValue(Constant domainValue, MemberPath path)
		{
			if (domainValue.IsNull() && path.IsNullable)
			{
				return true;
			}
			if (path.DefaultValue != null)
			{
				ScalarConstant scalarConstant = domainValue as ScalarConstant;
				return scalarConstant.Value == path.DefaultValue;
			}
			return false;
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x00098844 File Offset: 0x00096A44
		private Set<MemberPath> GetVariables(FragmentQuery query)
		{
			IEnumerable<MemberPath> elements = from domainConstraint in query.Condition.VariableConstraints
			where domainConstraint.Variable.Identifier is MemberRestriction && !domainConstraint.Variable.Domain.All((Constant constant) => domainConstraint.Range.Contains(constant))
			select ((MemberRestriction)domainConstraint.Variable.Identifier).RestrictedMemberSlot.MemberPath;
			return new Set<MemberPath>(elements, MemberPath.EqualityComparer);
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x000988B0 File Offset: 0x00096AB0
		private bool IsTrue(FragmentQuery query)
		{
			return !this._context.LeftFragmentQP.IsSatisfiable(FragmentQuery.Create(BoolExpression.CreateNot(query.Condition)));
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x000988D8 File Offset: 0x00096AD8
		[Conditional("DEBUG")]
		private void PrintStatistics(RewritingProcessor<Tile<FragmentQuery>> qp)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			qp.GetStatistics(out num, out num2, out num3, out num4, out num5);
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x000988F5 File Offset: 0x00096AF5
		[Conditional("DEBUG")]
		internal void TraceVerbose(string msg, params object[] parameters)
		{
			if (this._config.IsVerboseTracing)
			{
				Helpers.FormatTraceLine(msg, parameters);
			}
		}

		// Token: 0x040011EA RID: 4586
		private MemberPath _extentPath;

		// Token: 0x040011EB RID: 4587
		private MemberDomainMap _domainMap;

		// Token: 0x040011EC RID: 4588
		private ConfigViewGenerator _config;

		// Token: 0x040011ED RID: 4589
		private CqlIdentifiers _identifiers;

		// Token: 0x040011EE RID: 4590
		private ViewgenContext _context;

		// Token: 0x040011EF RID: 4591
		private RewritingProcessor<Tile<FragmentQuery>> _qp;

		// Token: 0x040011F0 RID: 4592
		private List<MemberPath> _keyAttributes;

		// Token: 0x040011F1 RID: 4593
		private List<FragmentQuery> _fragmentQueries = new List<FragmentQuery>();

		// Token: 0x040011F2 RID: 4594
		private List<Tile<FragmentQuery>> _views = new List<Tile<FragmentQuery>>();

		// Token: 0x040011F3 RID: 4595
		private FragmentQuery _domainQuery;

		// Token: 0x040011F4 RID: 4596
		private EdmType _generatedType;

		// Token: 0x040011F5 RID: 4597
		private HashSet<FragmentQuery> _usedViews = new HashSet<FragmentQuery>();

		// Token: 0x040011F6 RID: 4598
		private List<LeftCellWrapper> _usedCells = new List<LeftCellWrapper>();

		// Token: 0x040011F7 RID: 4599
		private BoolExpression _topLevelWhereClause;

		// Token: 0x040011F8 RID: 4600
		private CellTreeNode _basicView;

		// Token: 0x040011F9 RID: 4601
		private Dictionary<MemberPath, CaseStatement> _caseStatements = new Dictionary<MemberPath, CaseStatement>();

		// Token: 0x040011FA RID: 4602
		private ErrorLog _errorLog = new ErrorLog();

		// Token: 0x040011FB RID: 4603
		private ViewGenMode _typesGenerationMode;

		// Token: 0x040011FC RID: 4604
		private static Tile<FragmentQuery> TrueViewSurrogate = QueryRewriter.CreateTile(FragmentQuery.Create(BoolExpression.True));
	}
}
