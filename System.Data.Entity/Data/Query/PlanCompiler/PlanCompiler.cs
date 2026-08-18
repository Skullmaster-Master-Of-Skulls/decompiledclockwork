using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;
using System.Diagnostics;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200005B RID: 91
	internal class PlanCompiler
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x000286CB File Offset: 0x000268CB
		private PlanCompiler(DbCommandTree ctree)
		{
			this.m_ctree = ctree;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000286DA File Offset: 0x000268DA
		internal static void Assert(bool condition, string message)
		{
			if (!condition)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.AssertionFailed, 0, message);
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000286EC File Offset: 0x000268EC
		internal static void Compile(DbCommandTree ctree, out List<ProviderCommandInfo> providerCommands, out ColumnMap resultColumnMap, out int columnCount, out Set<EntitySet> entitySets)
		{
			PlanCompiler.Assert(ctree != null, "Expected a valid, non-null Command Tree input");
			PlanCompiler planCompiler = new PlanCompiler(ctree);
			planCompiler.Compile(out providerCommands, out resultColumnMap, out columnCount, out entitySets);
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00028719 File Offset: 0x00026919
		internal Command Command
		{
			get
			{
				return this.m_command;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00028721 File Offset: 0x00026921
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x00028729 File Offset: 0x00026929
		internal bool HasSortingOnNullSentinels
		{
			get
			{
				return this.m_hasSortingOnNullSentinels;
			}
			set
			{
				this.m_hasSortingOnNullSentinels = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00028732 File Offset: 0x00026932
		internal ConstraintManager ConstraintManager
		{
			get
			{
				if (this.m_constraintManager == null)
				{
					this.m_constraintManager = new ConstraintManager();
				}
				return this.m_constraintManager;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0002874D File Offset: 0x0002694D
		internal MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this.m_ctree.MetadataWorkspace;
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0002875A File Offset: 0x0002695A
		internal bool IsPhaseNeeded(PlanCompilerPhase phase)
		{
			return (this.m_neededPhases & 1 << (int)phase) != 0;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0002876C File Offset: 0x0002696C
		internal void MarkPhaseAsNeeded(PlanCompilerPhase phase)
		{
			this.m_neededPhases |= 1 << (int)phase;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00028784 File Offset: 0x00026984
		private void Compile(out List<ProviderCommandInfo> providerCommands, out ColumnMap resultColumnMap, out int columnCount, out Set<EntitySet> entitySets)
		{
			this.Initialize();
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			string text6 = string.Empty;
			string text7 = string.Empty;
			string empty = string.Empty;
			string text8 = string.Empty;
			string empty2 = string.Empty;
			string text9 = string.Empty;
			string empty3 = string.Empty;
			string text10 = string.Empty;
			string empty4 = string.Empty;
			string text11 = string.Empty;
			this.m_neededPhases = 337;
			text = this.SwitchToPhase(PlanCompilerPhase.PreProcessor);
			StructuredTypeInfo structuredTypeInfo;
			Dictionary<EdmFunction, EdmProperty[]> tvfResultKeys;
			PreProcessor.Process(this, out structuredTypeInfo, out tvfResultKeys);
			entitySets = structuredTypeInfo.GetEntitySets();
			if (this.IsPhaseNeeded(PlanCompilerPhase.AggregatePushdown))
			{
				text2 = this.SwitchToPhase(PlanCompilerPhase.AggregatePushdown);
				AggregatePushdown.Process(this);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.Normalization))
			{
				text3 = this.SwitchToPhase(PlanCompilerPhase.Normalization);
				Normalizer.Process(this);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.NTE))
			{
				text4 = this.SwitchToPhase(PlanCompilerPhase.NTE);
				NominalTypeEliminator.Process(this, structuredTypeInfo, tvfResultKeys);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.ProjectionPruning))
			{
				text5 = this.SwitchToPhase(PlanCompilerPhase.ProjectionPruning);
				ProjectionPruner.Process(this);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.NestPullup))
			{
				text6 = this.SwitchToPhase(PlanCompilerPhase.NestPullup);
				NestPullup.Process(this);
				text7 = this.SwitchToPhase(PlanCompilerPhase.ProjectionPruning);
				ProjectionPruner.Process(this);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.Transformations))
			{
				bool flag = this.ApplyTransformations(ref empty, TransformationRulesGroup.All);
				if (flag)
				{
					text8 = this.SwitchToPhase(PlanCompilerPhase.ProjectionPruning);
					ProjectionPruner.Process(this);
					this.ApplyTransformations(ref empty2, TransformationRulesGroup.Project);
				}
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.JoinElimination))
			{
				text9 = this.SwitchToPhase(PlanCompilerPhase.JoinElimination);
				bool flag2 = JoinElimination.Process(this);
				if (flag2)
				{
					this.ApplyTransformations(ref empty3, TransformationRulesGroup.PostJoinElimination);
					text10 = this.SwitchToPhase(PlanCompilerPhase.JoinElimination);
					flag2 = JoinElimination.Process(this);
					if (flag2)
					{
						this.ApplyTransformations(ref empty4, TransformationRulesGroup.PostJoinElimination);
					}
				}
			}
			text11 = this.SwitchToPhase(PlanCompilerPhase.CodeGen);
			CodeGen.Process(this, out providerCommands, out resultColumnMap, out columnCount);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00028935 File Offset: 0x00026B35
		private bool ApplyTransformations(ref string dumpString, TransformationRulesGroup rulesGroup)
		{
			if (this.MayApplyTransformationRules)
			{
				dumpString = this.SwitchToPhase(PlanCompilerPhase.Transformations);
				return TransformationRules.Process(this, rulesGroup);
			}
			return false;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00028954 File Offset: 0x00026B54
		private string SwitchToPhase(PlanCompilerPhase newPhase)
		{
			string empty = string.Empty;
			this.m_phase = newPhase;
			return empty;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0002896F File Offset: 0x00026B6F
		private bool MayApplyTransformationRules
		{
			get
			{
				if (this.m_mayApplyTransformationRules == null)
				{
					this.m_mayApplyTransformationRules = new bool?(this.ComputeMayApplyTransformations());
				}
				return this.m_mayApplyTransformationRules.Value;
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0002899C File Offset: 0x00026B9C
		private bool ComputeMayApplyTransformations()
		{
			if (PlanCompiler._applyTransformationsRegardlessOfSize.Enabled || this.m_command.NextNodeId < 100000)
			{
				return true;
			}
			int num = NodeCounter.Count(this.m_command.Root);
			return num < 100000;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000289E4 File Offset: 0x00026BE4
		private void Initialize()
		{
			DbQueryCommandTree dbQueryCommandTree = this.m_ctree as DbQueryCommandTree;
			PlanCompiler.Assert(dbQueryCommandTree != null, "Unexpected command tree kind. Only query command tree is supported.");
			this.m_command = ITreeGenerator.Generate(dbQueryCommandTree);
			PlanCompiler.Assert(this.m_command != null, "Unable to generate internal tree from Command Tree");
		}

		// Token: 0x040007CD RID: 1997
		private static BooleanSwitch _applyTransformationsRegardlessOfSize = new BooleanSwitch("System.Data.EntityClient.IgnoreOptimizationLimit", "The Entity Framework should try to optimize the query regardless of its size");

		// Token: 0x040007CE RID: 1998
		private const int MaxNodeCountForTransformations = 100000;

		// Token: 0x040007CF RID: 1999
		private DbCommandTree m_ctree;

		// Token: 0x040007D0 RID: 2000
		private Command m_command;

		// Token: 0x040007D1 RID: 2001
		private PlanCompilerPhase m_phase;

		// Token: 0x040007D2 RID: 2002
		private int m_neededPhases;

		// Token: 0x040007D3 RID: 2003
		private ConstraintManager m_constraintManager;

		// Token: 0x040007D4 RID: 2004
		private bool? m_mayApplyTransformationRules;

		// Token: 0x040007D5 RID: 2005
		private bool m_hasSortingOnNullSentinels;
	}
}
