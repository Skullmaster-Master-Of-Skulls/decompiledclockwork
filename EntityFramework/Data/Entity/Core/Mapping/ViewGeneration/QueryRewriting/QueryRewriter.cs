using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x0200043D RID: 1085
	internal class QueryRewriter
	{
		// Token: 0x060027BD RID: 10173 RVA: 0x000C07C0 File Offset: 0x000BE9C0
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

		// Token: 0x060027BE RID: 10174 RVA: 0x000C0924 File Offset: 0x000BEB24
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

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060027BF RID: 10175 RVA: 0x000C0A50 File Offset: 0x000BEC50
		internal ViewgenContext ViewgenContext
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060027C0 RID: 10176 RVA: 0x000C0A58 File Offset: 0x000BEC58
		internal Dictionary<MemberPath, CaseStatement> CaseStatements
		{
			get
			{
				return this._caseStatements;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x000C0A60 File Offset: 0x000BEC60
		internal BoolExpression TopLevelWhereClause
		{
			get
			{
				return this._topLevelWhereClause;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060027C2 RID: 10178 RVA: 0x000C0A68 File Offset: 0x000BEC68
		internal CellTreeNode BasicView
		{
			get
			{
				return this._basicView.MakeCopy();
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060027C3 RID: 10179 RVA: 0x000C0A75 File Offset: 0x000BEC75
		internal List<LeftCellWrapper> UsedCells
		{
			get
			{
				return this._usedCells;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x000C0A7D File Offset: 0x000BEC7D
		private IEnumerable<FragmentQuery> FragmentQueries
		{
			get
			{
				return this._fragmentQueries;
			}
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000C0A88 File Offset: 0x000BEC88
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

		// Token: 0x060027C6 RID: 10182 RVA: 0x000C0B24 File Offset: 0x000BED24
		private void AdjustMemberDomainsForUpdateViews()
		{
			ViewTarget viewTarget = this._context.ViewTarget;
			if (viewTarget != ViewTarget.UpdateView)
			{
				return;
			}
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

		// Token: 0x060027C7 RID: 10183 RVA: 0x000C0C3C File Offset: 0x000BEE3C
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

		// Token: 0x060027C8 RID: 10184 RVA: 0x000C0D14 File Offset: 0x000BEF14
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

		// Token: 0x060027C9 RID: 10185 RVA: 0x000C0DF8 File Offset: 0x000BEFF8
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
				value = new ConstantProjectedSlot(domainValue);
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

		// Token: 0x060027CA RID: 10186 RVA: 0x000C0E84 File Offset: 0x000BF084
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
							ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.AmbiguousMultiConstants, stringBuilder.ToString(), this._context.AllWrappersForExtent, string.Empty);
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

		// Token: 0x060027CB RID: 10187 RVA: 0x000C113C File Offset: 0x000BF33C
		private static List<string> GetTypeBasedMemberPathList(IEnumerable<MemberPath> nonConditionalScalarAttributes)
		{
			List<string> list = new List<string>();
			foreach (MemberPath memberPath in nonConditionalScalarAttributes)
			{
				EdmMember leafEdmMember = memberPath.LeafEdmMember;
				list.Add(leafEdmMember.DeclaringType.Name + "." + leafEdmMember);
			}
			return list;
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x000C11A8 File Offset: 0x000BF3A8
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
			ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.AttributesUnrecoverable, stringBuilder.ToString(), this._context.AllWrappersForExtent, string.Empty);
			errorLog.AddEntry(record);
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x000C1250 File Offset: 0x000BF450
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		private void GenerateCaseStatements(IEnumerable<MemberPath> members, HashSet<FragmentQuery> outputUsedViews)
		{
			IEnumerable<LeftCellWrapper> source = from w in this._context.AllWrappersForExtent
			where this._usedViews.Contains(w.FragmentQuery)
			select w;
			CellTreeNode rightDomainQuery = new OpCellTreeNode(this._context, CellTreeOpType.Union, (from wrapper in source
			select new LeafCellTreeNode(this._context, wrapper)).ToArray<LeafCellTreeNode>());
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
						caseStatement.AddWhenThen(BoolExpression.False, new ConstantProjectedSlot(Constant.Undefined));
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
							ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.AmbiguousMultiConstants, stringBuilder.ToString(), this._context.AllWrappersForExtent, string.Empty);
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

		// Token: 0x060027CE RID: 10190 RVA: 0x000C154C File Offset: 0x000BF74C
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
						caseStatement.AddWhenThen(BoolExpression.True, new ConstantProjectedSlot(constant));
						return;
					}
					fragmentQuery.Condition.ExpensiveSimplify();
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine(Strings.ViewGen_No_Default_Value_For_Configuration(currentPath.PathToString(new bool?(false))));
					this._errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.NoDefaultValue, stringBuilder.ToString(), this._context.AllWrappersForExtent, string.Empty));
				}
			}
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000C161C File Offset: 0x000BF81C
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

		// Token: 0x060027D0 RID: 10192 RVA: 0x000C1698 File Offset: 0x000BF898
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
							ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.ImpopssibleCondition, Strings.Viewgen_QV_RewritingNotFound(leftCellWrapper2.RightExtent.ToString()), leftCellWrapper2.Cells, string.Empty);
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

		// Token: 0x060027D1 RID: 10193 RVA: 0x000C1984 File Offset: 0x000BFB84
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

		// Token: 0x060027D2 RID: 10194 RVA: 0x000C1B24 File Offset: 0x000BFD24
		internal void AddTrivialCaseStatementsForConditionMembers()
		{
			for (int i = 0; i < this._context.MemberMaps.ProjectedSlotMap.Count; i++)
			{
				MemberPath memberPath = this._context.MemberMaps.ProjectedSlotMap[i];
				if (!memberPath.IsScalarType() && !this._caseStatements.ContainsKey(memberPath))
				{
					Constant value = new TypeConstant(memberPath.EdmType);
					CaseStatement caseStatement = new CaseStatement(memberPath);
					caseStatement.AddWhenThen(BoolExpression.True, new ConstantProjectedSlot(value));
					this._caseStatements[memberPath] = caseStatement;
				}
			}
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000C1BB0 File Offset: 0x000BFDB0
		private bool FindRewritingAndUsedViews(IEnumerable<MemberPath> attributes, BoolExpression whereClause, HashSet<FragmentQuery> outputUsedViews, out Tile<FragmentQuery> rewriting)
		{
			IEnumerable<MemberPath> enumerable;
			return this.FindRewritingAndUsedViews(attributes, whereClause, outputUsedViews, out rewriting, out enumerable);
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000C1BCA File Offset: 0x000BFDCA
		private bool FindRewritingAndUsedViews(IEnumerable<MemberPath> attributes, BoolExpression whereClause, HashSet<FragmentQuery> outputUsedViews, out Tile<FragmentQuery> rewriting, out IEnumerable<MemberPath> notCoveredAttributes)
		{
			if (this.FindRewriting(attributes, whereClause, out rewriting, out notCoveredAttributes))
			{
				outputUsedViews.UnionWith(rewriting.GetNamedQueries());
				return true;
			}
			return false;
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000C1BEC File Offset: 0x000BFDEC
		private bool FindRewriting(IEnumerable<MemberPath> attributes, BoolExpression whereClause, out Tile<FragmentQuery> rewriting, out IEnumerable<MemberPath> notCoveredAttributes)
		{
			Tile<FragmentQuery> toFill = QueryRewriter.CreateTile(FragmentQuery.Create(attributes, whereClause));
			Tile<FragmentQuery> toAvoid = QueryRewriter.CreateTile(FragmentQuery.Create(this._keyAttributes, BoolExpression.CreateNot(whereClause)));
			bool isRelaxed = this._context.ViewTarget == ViewTarget.UpdateView;
			return this.RewriteQuery(toFill, toAvoid, out rewriting, out notCoveredAttributes, isRelaxed);
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x000C1C3C File Offset: 0x000BFE3C
		private bool RewriteQuery(Tile<FragmentQuery> toFill, Tile<FragmentQuery> toAvoid, out Tile<FragmentQuery> rewriting, out IEnumerable<MemberPath> notCoveredAttributes, bool isRelaxed)
		{
			notCoveredAttributes = new List<MemberPath>();
			FragmentQuery fragmentQuery = toFill.Query;
			if (this._context.TryGetCachedRewriting(fragmentQuery, out rewriting))
			{
				return true;
			}
			IEnumerable<Tile<FragmentQuery>> relevantViews = this.GetRelevantViews(fragmentQuery);
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
			foreach (MemberPath key in QueryRewriter.NonKeys(fragmentQuery.Attributes))
			{
				dictionary[key] = fragmentQuery;
			}
			if (dictionary.Count == 0 || this.CoverAttributes(ref rewriting, dictionary))
			{
				this.GetUsedViewsAndRemoveTrueSurrogate(ref rewriting);
				this._context.SetCachedRewriting(query, rewriting);
				return true;
			}
			if (isRelaxed)
			{
				foreach (MemberPath key2 in QueryRewriter.NonKeys(fragmentQuery.Attributes))
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
				if (this.CoverAttributes(ref rewriting, dictionary))
				{
					this.GetUsedViewsAndRemoveTrueSurrogate(ref rewriting);
					this._context.SetCachedRewriting(query, rewriting);
					return true;
				}
			}
			notCoveredAttributes = dictionary.Keys;
			return false;
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000C1E1C File Offset: 0x000C001C
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

		// Token: 0x060027D8 RID: 10200 RVA: 0x000C1E6C File Offset: 0x000C006C
		private bool CoverAttributes(ref Tile<FragmentQuery> rewriting, Dictionary<MemberPath, FragmentQuery> attributeConditions)
		{
			HashSet<FragmentQuery> hashSet = new HashSet<FragmentQuery>(rewriting.GetNamedQueries());
			foreach (FragmentQuery fragmentQuery in hashSet)
			{
				foreach (MemberPath projectedAttribute in QueryRewriter.NonKeys(fragmentQuery.Attributes))
				{
					this.CoverAttribute(projectedAttribute, fragmentQuery, attributeConditions);
				}
				if (attributeConditions.Count == 0)
				{
					return true;
				}
			}
			Tile<FragmentQuery> tile = null;
			foreach (FragmentQuery fragmentQuery2 in this._fragmentQueries)
			{
				foreach (MemberPath projectedAttribute2 in QueryRewriter.NonKeys(fragmentQuery2.Attributes))
				{
					if (this.CoverAttribute(projectedAttribute2, fragmentQuery2, attributeConditions))
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

		// Token: 0x060027D9 RID: 10201 RVA: 0x000C1FF0 File Offset: 0x000C01F0
		private bool CoverAttribute(MemberPath projectedAttribute, FragmentQuery view, Dictionary<MemberPath, FragmentQuery> attributeConditions)
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

		// Token: 0x060027DA RID: 10202 RVA: 0x000C2048 File Offset: 0x000C0248
		private IEnumerable<Tile<FragmentQuery>> GetRelevantViews(FragmentQuery query)
		{
			Set<MemberPath> variables = QueryRewriter.GetVariables(query);
			Tile<FragmentQuery> tile = null;
			List<Tile<FragmentQuery>> list = new List<Tile<FragmentQuery>>();
			Tile<FragmentQuery> tile2 = null;
			foreach (Tile<FragmentQuery> tile3 in this._views)
			{
				if (QueryRewriter.GetVariables(tile3.Query).Overlaps(variables))
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
						tile2 = QueryRewriter._trueViewSurrogate;
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

		// Token: 0x060027DB RID: 10203 RVA: 0x000C2194 File Offset: 0x000C0394
		private HashSet<FragmentQuery> GetUsedViewsAndRemoveTrueSurrogate(ref Tile<FragmentQuery> rewriting)
		{
			HashSet<FragmentQuery> hashSet = new HashSet<FragmentQuery>(rewriting.GetNamedQueries());
			if (!hashSet.Contains(QueryRewriter._trueViewSurrogate.Query))
			{
				return hashSet;
			}
			hashSet.Remove(QueryRewriter._trueViewSurrogate.Query);
			Tile<FragmentQuery> tile = null;
			IEnumerable<FragmentQuery> enumerable = hashSet.Concat(this._fragmentQueries);
			foreach (FragmentQuery fragmentQuery in enumerable)
			{
				tile = ((tile == null) ? QueryRewriter.CreateTile(fragmentQuery) : this._qp.Union(tile, QueryRewriter.CreateTile(fragmentQuery)));
				hashSet.Add(fragmentQuery);
				if (this.IsTrue(tile.Query))
				{
					rewriting = rewriting.Replace(QueryRewriter._trueViewSurrogate, tile);
					return hashSet;
				}
			}
			return hashSet;
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x000C2268 File Offset: 0x000C0468
		private BoolExpression CreateMemberCondition(MemberPath path, Constant domainValue)
		{
			return FragmentQuery.CreateMemberCondition(path, domainValue, this._domainMap);
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x000C2277 File Offset: 0x000C0477
		private FragmentQuery CreateMemberConditionQuery(MemberPath currentPath, Constant domainValue)
		{
			return QueryRewriter.CreateMemberConditionQuery(currentPath, domainValue, this._keyAttributes, this._domainMap);
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x000C228C File Offset: 0x000C048C
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

		// Token: 0x060027DF RID: 10207 RVA: 0x000C22C6 File Offset: 0x000C04C6
		private static TileNamed<FragmentQuery> CreateTile(FragmentQuery query)
		{
			return new TileNamed<FragmentQuery>(query);
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x000C2458 File Offset: 0x000C0658
		private static IEnumerable<Constant> GetTypeConstants(IEnumerable<EdmType> types)
		{
			foreach (EdmType type in types)
			{
				yield return new TypeConstant(type);
			}
			yield break;
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000C2478 File Offset: 0x000C0678
		private static IEnumerable<MemberPath> GetNonConditionalScalarMembers(EdmType edmType, MemberPath currentPath, MemberDomainMap domainMap)
		{
			return currentPath.GetMembers(edmType, new bool?(true), new bool?(false), null, domainMap);
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000C24A4 File Offset: 0x000C06A4
		private static IEnumerable<MemberPath> GetConditionalComplexMembers(EdmType edmType, MemberPath currentPath, MemberDomainMap domainMap)
		{
			return currentPath.GetMembers(edmType, new bool?(false), new bool?(true), null, domainMap);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000C24D0 File Offset: 0x000C06D0
		private static IEnumerable<MemberPath> GetNonConditionalComplexMembers(EdmType edmType, MemberPath currentPath, MemberDomainMap domainMap)
		{
			return currentPath.GetMembers(edmType, new bool?(false), new bool?(false), null, domainMap);
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000C24FC File Offset: 0x000C06FC
		private static IEnumerable<MemberPath> GetConditionalScalarMembers(EdmType edmType, MemberPath currentPath, MemberDomainMap domainMap)
		{
			return currentPath.GetMembers(edmType, new bool?(true), new bool?(true), null, domainMap);
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000C2531 File Offset: 0x000C0731
		private static IEnumerable<MemberPath> NonKeys(IEnumerable<MemberPath> attributes)
		{
			return from attr in attributes
			where !attr.IsPartOfKey
			select attr;
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000C2570 File Offset: 0x000C0770
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

		// Token: 0x060027E7 RID: 10215 RVA: 0x000C2618 File Offset: 0x000C0818
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

		// Token: 0x060027E8 RID: 10216 RVA: 0x000C26F8 File Offset: 0x000C08F8
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

		// Token: 0x060027E9 RID: 10217 RVA: 0x000C27C8 File Offset: 0x000C09C8
		private static Set<MemberPath> GetVariables(FragmentQuery query)
		{
			IEnumerable<MemberPath> elements = from domainConstraint in query.Condition.VariableConstraints
			where domainConstraint.Variable.Identifier is MemberRestriction && !domainConstraint.Variable.Domain.All((Constant constant) => domainConstraint.Range.Contains(constant))
			select ((MemberRestriction)domainConstraint.Variable.Identifier).RestrictedMemberSlot.MemberPath;
			return new Set<MemberPath>(elements, MemberPath.EqualityComparer);
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000C2830 File Offset: 0x000C0A30
		private bool IsTrue(FragmentQuery query)
		{
			return !this._context.LeftFragmentQP.IsSatisfiable(FragmentQuery.Create(BoolExpression.CreateNot(query.Condition)));
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000C2858 File Offset: 0x000C0A58
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
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

		// Token: 0x060027EC RID: 10220 RVA: 0x000C2875 File Offset: 0x000C0A75
		[Conditional("DEBUG")]
		internal void TraceVerbose(string msg, params object[] parameters)
		{
			if (this._config.IsVerboseTracing)
			{
				Helpers.FormatTraceLine(msg, parameters);
			}
		}

		// Token: 0x04000EF9 RID: 3833
		private readonly MemberPath _extentPath;

		// Token: 0x04000EFA RID: 3834
		private readonly MemberDomainMap _domainMap;

		// Token: 0x04000EFB RID: 3835
		private readonly ConfigViewGenerator _config;

		// Token: 0x04000EFC RID: 3836
		private readonly CqlIdentifiers _identifiers;

		// Token: 0x04000EFD RID: 3837
		private readonly ViewgenContext _context;

		// Token: 0x04000EFE RID: 3838
		private readonly RewritingProcessor<Tile<FragmentQuery>> _qp;

		// Token: 0x04000EFF RID: 3839
		private readonly List<MemberPath> _keyAttributes;

		// Token: 0x04000F00 RID: 3840
		private readonly List<FragmentQuery> _fragmentQueries = new List<FragmentQuery>();

		// Token: 0x04000F01 RID: 3841
		private readonly List<Tile<FragmentQuery>> _views = new List<Tile<FragmentQuery>>();

		// Token: 0x04000F02 RID: 3842
		private readonly FragmentQuery _domainQuery;

		// Token: 0x04000F03 RID: 3843
		private readonly EdmType _generatedType;

		// Token: 0x04000F04 RID: 3844
		private readonly HashSet<FragmentQuery> _usedViews = new HashSet<FragmentQuery>();

		// Token: 0x04000F05 RID: 3845
		private List<LeftCellWrapper> _usedCells = new List<LeftCellWrapper>();

		// Token: 0x04000F06 RID: 3846
		private BoolExpression _topLevelWhereClause;

		// Token: 0x04000F07 RID: 3847
		private CellTreeNode _basicView;

		// Token: 0x04000F08 RID: 3848
		private Dictionary<MemberPath, CaseStatement> _caseStatements = new Dictionary<MemberPath, CaseStatement>();

		// Token: 0x04000F09 RID: 3849
		private readonly ErrorLog _errorLog = new ErrorLog();

		// Token: 0x04000F0A RID: 3850
		private readonly ViewGenMode _typesGenerationMode;

		// Token: 0x04000F0B RID: 3851
		private static readonly Tile<FragmentQuery> _trueViewSurrogate = QueryRewriter.CreateTile(FragmentQuery.Create(BoolExpression.True));
	}
}
