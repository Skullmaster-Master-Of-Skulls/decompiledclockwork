using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200068B RID: 1675
	internal class PlanCompiler
	{
		// Token: 0x06004203 RID: 16899 RVA: 0x0013760B File Offset: 0x0013580B
		private PlanCompiler(DbCommandTree ctree)
		{
			this.m_ctree = ctree;
		}

		// Token: 0x06004204 RID: 16900 RVA: 0x0013761A File Offset: 0x0013581A
		internal static void Assert(bool condition, string message)
		{
			if (!condition)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.AssertionFailed, 0, message);
			}
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x0013762C File Offset: 0x0013582C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal static void Compile(DbCommandTree ctree, out List<ProviderCommandInfo> providerCommands, out ColumnMap resultColumnMap, out int columnCount, out Set<EntitySet> entitySets)
		{
			PlanCompiler.Assert(ctree != null, "Expected a valid, non-null Command Tree input");
			PlanCompiler planCompiler = new PlanCompiler(ctree);
			planCompiler.Compile(out providerCommands, out resultColumnMap, out columnCount, out entitySets);
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06004206 RID: 16902 RVA: 0x0013765C File Offset: 0x0013585C
		internal Command Command
		{
			get
			{
				return this.m_command;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06004207 RID: 16903 RVA: 0x00137664 File Offset: 0x00135864
		// (set) Token: 0x06004208 RID: 16904 RVA: 0x0013766C File Offset: 0x0013586C
		internal bool HasSortingOnNullSentinels { get; set; }

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06004209 RID: 16905 RVA: 0x00137675 File Offset: 0x00135875
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

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x0600420A RID: 16906 RVA: 0x00137690 File Offset: 0x00135890
		internal MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this.m_ctree.MetadataWorkspace;
			}
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x0013769D File Offset: 0x0013589D
		internal bool IsPhaseNeeded(PlanCompilerPhase phase)
		{
			return (this.m_neededPhases & 1 << (int)phase) != 0;
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x001376B2 File Offset: 0x001358B2
		internal void MarkPhaseAsNeeded(PlanCompilerPhase phase)
		{
			this.m_neededPhases |= 1 << (int)phase;
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x001376C7 File Offset: 0x001358C7
		internal bool IsAfterPhase(PlanCompilerPhase phase)
		{
			return (this._precedingPhases & 1 << (int)phase) != 0;
		}

		// Token: 0x0600420E RID: 16910 RVA: 0x001376DC File Offset: 0x001358DC
		private void Compile(out List<ProviderCommandInfo> providerCommands, out ColumnMap resultColumnMap, out int columnCount, out Set<EntitySet> entitySets)
		{
			this.Initialize();
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			string empty4 = string.Empty;
			string empty5 = string.Empty;
			string empty6 = string.Empty;
			string empty7 = string.Empty;
			string empty8 = string.Empty;
			string empty9 = string.Empty;
			string empty10 = string.Empty;
			string empty11 = string.Empty;
			string empty12 = string.Empty;
			string empty13 = string.Empty;
			string empty14 = string.Empty;
			string empty15 = string.Empty;
			this.m_neededPhases = 593;
			this.SwitchToPhase(PlanCompilerPhase.PreProcessor);
			StructuredTypeInfo structuredTypeInfo;
			Dictionary<EdmFunction, EdmProperty[]> tvfResultKeys;
			PreProcessor.Process(this, out structuredTypeInfo, out tvfResultKeys);
			entitySets = structuredTypeInfo.GetEntitySets();
			if (this.IsPhaseNeeded(PlanCompilerPhase.AggregatePushdown))
			{
				this.SwitchToPhase(PlanCompilerPhase.AggregatePushdown);
				AggregatePushdown.Process(this);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.Normalization))
			{
				this.SwitchToPhase(PlanCompilerPhase.Normalization);
				Normalizer.Process(this);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.NTE))
			{
				this.SwitchToPhase(PlanCompilerPhase.NTE);
				NominalTypeEliminator.Process(this, structuredTypeInfo, tvfResultKeys);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.ProjectionPruning))
			{
				this.SwitchToPhase(PlanCompilerPhase.ProjectionPruning);
				ProjectionPruner.Process(this);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.NestPullup))
			{
				this.SwitchToPhase(PlanCompilerPhase.NestPullup);
				NestPullup.Process(this);
				this.SwitchToPhase(PlanCompilerPhase.ProjectionPruning);
				ProjectionPruner.Process(this);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.Transformations))
			{
				bool flag = this.ApplyTransformations(ref empty8, TransformationRulesGroup.All);
				if (flag)
				{
					this.SwitchToPhase(PlanCompilerPhase.ProjectionPruning);
					ProjectionPruner.Process(this);
					this.ApplyTransformations(ref empty10, TransformationRulesGroup.Project);
				}
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.NullSemantics))
			{
				this.SwitchToPhase(PlanCompilerPhase.NullSemantics);
				if (!this.m_ctree.UseDatabaseNullSemantics && NullSemantics.Process(this.Command))
				{
					this.ApplyTransformations(ref empty12, TransformationRulesGroup.NullSemantics);
				}
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.JoinElimination))
			{
				for (int i = 0; i < 10; i++)
				{
					this.SwitchToPhase(PlanCompilerPhase.JoinElimination);
					if (!JoinElimination.Process(this) && !this.TransformationsDeferred)
					{
						break;
					}
					this.TransformationsDeferred = false;
					this.ApplyTransformations(ref empty14, TransformationRulesGroup.PostJoinElimination);
				}
			}
			this.SwitchToPhase(PlanCompilerPhase.CodeGen);
			CodeGen.Process(this, out providerCommands, out resultColumnMap, out columnCount);
		}

		// Token: 0x0600420F RID: 16911 RVA: 0x001378B3 File Offset: 0x00135AB3
		private bool ApplyTransformations(ref string dumpString, TransformationRulesGroup rulesGroup)
		{
			if (this.MayApplyTransformationRules)
			{
				dumpString = this.SwitchToPhase(PlanCompilerPhase.Transformations);
				return TransformationRules.Process(this, rulesGroup);
			}
			return false;
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x001378D0 File Offset: 0x00135AD0
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "newPhase")]
		private string SwitchToPhase(PlanCompilerPhase newPhase)
		{
			string empty = string.Empty;
			if (newPhase != this.m_phase)
			{
				this._precedingPhases |= 1 << (int)this.m_phase;
			}
			this.m_phase = newPhase;
			return empty;
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06004211 RID: 16913 RVA: 0x0013790C File Offset: 0x00135B0C
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

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06004212 RID: 16914 RVA: 0x00137937 File Offset: 0x00135B37
		// (set) Token: 0x06004213 RID: 16915 RVA: 0x0013793F File Offset: 0x00135B3F
		internal bool TransformationsDeferred { get; set; }

		// Token: 0x06004214 RID: 16916 RVA: 0x00137948 File Offset: 0x00135B48
		private bool ComputeMayApplyTransformations()
		{
			if (PlanCompiler._applyTransformationsRegardlessOfSize.Enabled || this.m_command.NextNodeId < 100000)
			{
				return true;
			}
			int num = NodeCounter.Count(this.m_command.Root);
			return num < 100000;
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x00137990 File Offset: 0x00135B90
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		private void Initialize()
		{
			DbQueryCommandTree dbQueryCommandTree = this.m_ctree as DbQueryCommandTree;
			PlanCompiler.Assert(dbQueryCommandTree != null, "Unexpected command tree kind. Only query command tree is supported.");
			this.m_command = ITreeGenerator.Generate(dbQueryCommandTree);
			PlanCompiler.Assert(this.m_command != null, "Unable to generate internal tree from Command Tree");
		}

		// Token: 0x04001873 RID: 6259
		private const int MaxNodeCountForTransformations = 100000;

		// Token: 0x04001874 RID: 6260
		private static readonly BooleanSwitch _applyTransformationsRegardlessOfSize = new BooleanSwitch("System.Data.Entity.Core.EntityClient.IgnoreOptimizationLimit", "The Entity Framework should try to optimize the query regardless of its size");

		// Token: 0x04001875 RID: 6261
		private readonly DbCommandTree m_ctree;

		// Token: 0x04001876 RID: 6262
		private Command m_command;

		// Token: 0x04001877 RID: 6263
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
		private PlanCompilerPhase m_phase;

		// Token: 0x04001878 RID: 6264
		private int _precedingPhases;

		// Token: 0x04001879 RID: 6265
		private int m_neededPhases;

		// Token: 0x0400187A RID: 6266
		private ConstraintManager m_constraintManager;

		// Token: 0x0400187B RID: 6267
		private bool? m_mayApplyTransformationRules;
	}
}
