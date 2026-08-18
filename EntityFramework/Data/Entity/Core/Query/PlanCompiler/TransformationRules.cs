using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x020006A4 RID: 1700
	internal static class TransformationRules
	{
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600435E RID: 17246 RVA: 0x0013FA8C File Offset: 0x0013DC8C
		private static List<Rule> AllRules
		{
			get
			{
				if (TransformationRules.allRules == null)
				{
					TransformationRules.allRules = new List<Rule>();
					TransformationRules.allRules.AddRange(ScalarOpRules.Rules);
					TransformationRules.allRules.AddRange(FilterOpRules.Rules);
					TransformationRules.allRules.AddRange(ProjectOpRules.Rules);
					TransformationRules.allRules.AddRange(ApplyOpRules.Rules);
					TransformationRules.allRules.AddRange(JoinOpRules.Rules);
					TransformationRules.allRules.AddRange(SingleRowOpRules.Rules);
					TransformationRules.allRules.AddRange(SetOpRules.Rules);
					TransformationRules.allRules.AddRange(GroupByOpRules.Rules);
					TransformationRules.allRules.AddRange(SortOpRules.Rules);
					TransformationRules.allRules.AddRange(ConstrainedSortOpRules.Rules);
					TransformationRules.allRules.AddRange(DistinctOpRules.Rules);
				}
				return TransformationRules.allRules;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600435F RID: 17247 RVA: 0x0013FB58 File Offset: 0x0013DD58
		private static List<Rule> PostJoinEliminationRules
		{
			get
			{
				if (TransformationRules.postJoinEliminationRules == null)
				{
					TransformationRules.postJoinEliminationRules = new List<Rule>();
					TransformationRules.postJoinEliminationRules.AddRange(ProjectOpRules.Rules);
					TransformationRules.postJoinEliminationRules.AddRange(DistinctOpRules.Rules);
					TransformationRules.postJoinEliminationRules.AddRange(FilterOpRules.Rules);
					TransformationRules.postJoinEliminationRules.AddRange(ApplyOpRules.Rules);
					TransformationRules.postJoinEliminationRules.AddRange(JoinOpRules.Rules);
					TransformationRules.postJoinEliminationRules.AddRange(TransformationRules.NullabilityRules);
				}
				return TransformationRules.postJoinEliminationRules;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06004360 RID: 17248 RVA: 0x0013FBD8 File Offset: 0x0013DDD8
		private static List<Rule> NullabilityRules
		{
			get
			{
				if (TransformationRules.nullabilityRules == null)
				{
					TransformationRules.nullabilityRules = new List<Rule>();
					TransformationRules.nullabilityRules.Add(ScalarOpRules.Rule_IsNullOverVarRef);
					TransformationRules.nullabilityRules.Add(ScalarOpRules.Rule_AndOverConstantPred1);
					TransformationRules.nullabilityRules.Add(ScalarOpRules.Rule_AndOverConstantPred2);
					TransformationRules.nullabilityRules.Add(ScalarOpRules.Rule_SimplifyCase);
					TransformationRules.nullabilityRules.Add(ScalarOpRules.Rule_NotOverConstantPred);
				}
				return TransformationRules.nullabilityRules;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06004361 RID: 17249 RVA: 0x0013FC48 File Offset: 0x0013DE48
		private static List<Rule> NullSemanticsRules
		{
			get
			{
				if (TransformationRules.nullSemanticsRules == null)
				{
					TransformationRules.nullSemanticsRules = new List<Rule>();
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_IsNullOverAnything);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_NullCast);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_EqualsOverConstant);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_AndOverConstantPred1);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_AndOverConstantPred2);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_OrOverConstantPred1);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_OrOverConstantPred2);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_NotOverConstantPred);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_LikeOverConstants);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_SimplifyCase);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_FlattenCase);
				}
				return TransformationRules.nullSemanticsRules;
			}
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x0013FD14 File Offset: 0x0013DF14
		private static ReadOnlyCollection<ReadOnlyCollection<Rule>> BuildLookupTableForRules(IEnumerable<Rule> rules)
		{
			ReadOnlyCollection<Rule> readOnlyCollection = new ReadOnlyCollection<Rule>(new Rule[0]);
			List<Rule>[] array = new List<Rule>[73];
			foreach (Rule rule in rules)
			{
				List<Rule> list = array[(int)rule.RuleOpType];
				if (list == null)
				{
					list = new List<Rule>();
					array[(int)rule.RuleOpType] = list;
				}
				list.Add(rule);
			}
			ReadOnlyCollection<Rule>[] array2 = new ReadOnlyCollection<Rule>[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					array2[i] = new ReadOnlyCollection<Rule>(array[i].ToArray());
				}
				else
				{
					array2[i] = readOnlyCollection;
				}
			}
			return new ReadOnlyCollection<ReadOnlyCollection<Rule>>(array2);
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x0013FDD4 File Offset: 0x0013DFD4
		private static HashSet<Rule> InitializeRulesRequiringProjectionPruning()
		{
			return new HashSet<Rule>
			{
				ApplyOpRules.Rule_OuterApplyOverProject,
				JoinOpRules.Rule_CrossJoinOverProject1,
				JoinOpRules.Rule_CrossJoinOverProject2,
				JoinOpRules.Rule_InnerJoinOverProject1,
				JoinOpRules.Rule_InnerJoinOverProject2,
				JoinOpRules.Rule_OuterJoinOverProject2,
				ProjectOpRules.Rule_ProjectWithNoLocalDefs,
				FilterOpRules.Rule_FilterOverProject,
				FilterOpRules.Rule_FilterWithConstantPredicate,
				GroupByOpRules.Rule_GroupByOverProject,
				GroupByOpRules.Rule_GroupByOpWithSimpleVarRedefinitions
			};
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x0013FE6C File Offset: 0x0013E06C
		private static HashSet<Rule> InitializeRulesRequiringNullabilityRulesToBeReapplied()
		{
			return new HashSet<Rule>
			{
				FilterOpRules.Rule_FilterOverLeftOuterJoin
			};
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x0013FE8C File Offset: 0x0013E08C
		internal static bool Process(PlanCompiler compilerState, TransformationRulesGroup rulesGroup)
		{
			ReadOnlyCollection<ReadOnlyCollection<Rule>> rulesTable = null;
			switch (rulesGroup)
			{
			case TransformationRulesGroup.All:
				rulesTable = TransformationRules.AllRulesTable;
				break;
			case TransformationRulesGroup.Project:
				rulesTable = TransformationRules.ProjectRulesTable;
				break;
			case TransformationRulesGroup.PostJoinElimination:
				rulesTable = TransformationRules.PostJoinEliminationRulesTable;
				break;
			case TransformationRulesGroup.NullSemantics:
				rulesTable = TransformationRules.NullSemanticsRulesTable;
				break;
			}
			bool flag;
			if (TransformationRules.Process(compilerState, rulesTable, out flag))
			{
				bool flag2;
				TransformationRules.Process(compilerState, TransformationRules.NullabilityRulesTable, out flag2);
				flag = (flag || flag2);
			}
			return flag;
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x0013FEF8 File Offset: 0x0013E0F8
		private static bool Process(PlanCompiler compilerState, ReadOnlyCollection<ReadOnlyCollection<Rule>> rulesTable, out bool projectionPruningRequired)
		{
			RuleProcessor ruleProcessor = new RuleProcessor();
			TransformationRulesContext transformationRulesContext = new TransformationRulesContext(compilerState);
			compilerState.Command.Root = ruleProcessor.ApplyRulesToSubtree(transformationRulesContext, rulesTable, compilerState.Command.Root);
			projectionPruningRequired = transformationRulesContext.ProjectionPrunningRequired;
			return transformationRulesContext.ReapplyNullabilityRules;
		}

		// Token: 0x040018F1 RID: 6385
		internal static readonly ReadOnlyCollection<ReadOnlyCollection<Rule>> AllRulesTable = TransformationRules.BuildLookupTableForRules(TransformationRules.AllRules);

		// Token: 0x040018F2 RID: 6386
		internal static readonly ReadOnlyCollection<ReadOnlyCollection<Rule>> ProjectRulesTable = TransformationRules.BuildLookupTableForRules(ProjectOpRules.Rules);

		// Token: 0x040018F3 RID: 6387
		internal static readonly ReadOnlyCollection<ReadOnlyCollection<Rule>> PostJoinEliminationRulesTable = TransformationRules.BuildLookupTableForRules(TransformationRules.PostJoinEliminationRules);

		// Token: 0x040018F4 RID: 6388
		internal static readonly ReadOnlyCollection<ReadOnlyCollection<Rule>> NullabilityRulesTable = TransformationRules.BuildLookupTableForRules(TransformationRules.NullabilityRules);

		// Token: 0x040018F5 RID: 6389
		internal static readonly HashSet<Rule> RulesRequiringProjectionPruning = TransformationRules.InitializeRulesRequiringProjectionPruning();

		// Token: 0x040018F6 RID: 6390
		internal static readonly HashSet<Rule> RulesRequiringNullabilityRulesToBeReapplied = TransformationRules.InitializeRulesRequiringNullabilityRulesToBeReapplied();

		// Token: 0x040018F7 RID: 6391
		internal static readonly ReadOnlyCollection<ReadOnlyCollection<Rule>> NullSemanticsRulesTable = TransformationRules.BuildLookupTableForRules(TransformationRules.NullSemanticsRules);

		// Token: 0x040018F8 RID: 6392
		private static List<Rule> allRules;

		// Token: 0x040018F9 RID: 6393
		private static List<Rule> postJoinEliminationRules;

		// Token: 0x040018FA RID: 6394
		private static List<Rule> nullabilityRules;

		// Token: 0x040018FB RID: 6395
		private static List<Rule> nullSemanticsRules;
	}
}
