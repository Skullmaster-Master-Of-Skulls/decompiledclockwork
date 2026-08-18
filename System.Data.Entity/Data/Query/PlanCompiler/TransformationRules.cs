using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200006F RID: 111
	internal static class TransformationRules
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0002E780 File Offset: 0x0002C980
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

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0002E84C File Offset: 0x0002CA4C
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
					TransformationRules.postJoinEliminationRules.AddRange(JoinOpRules.Rules);
					TransformationRules.postJoinEliminationRules.AddRange(TransformationRules.NullabilityRules);
				}
				return TransformationRules.postJoinEliminationRules;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0002E8BC File Offset: 0x0002CABC
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

		// Token: 0x060008E5 RID: 2277 RVA: 0x0002E92C File Offset: 0x0002CB2C
		private static ReadOnlyCollection<ReadOnlyCollection<Rule>> BuildLookupTableForRules(IEnumerable<Rule> rules)
		{
			ReadOnlyCollection<Rule> readOnlyCollection = new ReadOnlyCollection<Rule>(new Rule[0]);
			List<Rule>[] array = new List<Rule>[72];
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

		// Token: 0x060008E6 RID: 2278 RVA: 0x0002E9EC File Offset: 0x0002CBEC
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

		// Token: 0x060008E7 RID: 2279 RVA: 0x0002EA84 File Offset: 0x0002CC84
		private static HashSet<Rule> InitializeRulesRequiringNullabilityRulesToBeReapplied()
		{
			return new HashSet<Rule>
			{
				FilterOpRules.Rule_FilterOverLeftOuterJoin
			};
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0002EAA4 File Offset: 0x0002CCA4
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

		// Token: 0x060008E9 RID: 2281 RVA: 0x0002EAFC File Offset: 0x0002CCFC
		private static bool Process(PlanCompiler compilerState, ReadOnlyCollection<ReadOnlyCollection<Rule>> rulesTable, out bool projectionPruningRequired)
		{
			RuleProcessor ruleProcessor = new RuleProcessor();
			TransformationRulesContext transformationRulesContext = new TransformationRulesContext(compilerState);
			compilerState.Command.Root = ruleProcessor.ApplyRulesToSubtree(transformationRulesContext, rulesTable, compilerState.Command.Root);
			projectionPruningRequired = transformationRulesContext.ProjectionPrunningRequired;
			return transformationRulesContext.ReapplyNullabilityRules;
		}

		// Token: 0x04000813 RID: 2067
		internal static readonly ReadOnlyCollection<ReadOnlyCollection<Rule>> AllRulesTable = TransformationRules.BuildLookupTableForRules(TransformationRules.AllRules);

		// Token: 0x04000814 RID: 2068
		internal static readonly ReadOnlyCollection<ReadOnlyCollection<Rule>> ProjectRulesTable = TransformationRules.BuildLookupTableForRules(ProjectOpRules.Rules);

		// Token: 0x04000815 RID: 2069
		internal static readonly ReadOnlyCollection<ReadOnlyCollection<Rule>> PostJoinEliminationRulesTable = TransformationRules.BuildLookupTableForRules(TransformationRules.PostJoinEliminationRules);

		// Token: 0x04000816 RID: 2070
		internal static readonly ReadOnlyCollection<ReadOnlyCollection<Rule>> NullabilityRulesTable = TransformationRules.BuildLookupTableForRules(TransformationRules.NullabilityRules);

		// Token: 0x04000817 RID: 2071
		internal static readonly HashSet<Rule> RulesRequiringProjectionPruning = TransformationRules.InitializeRulesRequiringProjectionPruning();

		// Token: 0x04000818 RID: 2072
		internal static readonly HashSet<Rule> RulesRequiringNullabilityRulesToBeReapplied = TransformationRules.InitializeRulesRequiringNullabilityRulesToBeReapplied();

		// Token: 0x04000819 RID: 2073
		private static List<Rule> allRules;

		// Token: 0x0400081A RID: 2074
		private static List<Rule> postJoinEliminationRules;

		// Token: 0x0400081B RID: 2075
		private static List<Rule> nullabilityRules;
	}
}
