using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D1A RID: 3354
	internal class TupleTraversalState
	{
		// Token: 0x06007D14 RID: 32020 RVA: 0x001CB0E9 File Offset: 0x001C92E9
		public TupleTraversalState(IList<OlapGroupDescription> groupDescriptions)
		{
			this.hierarchyUniqueNameToIndexMapping = new Dictionary<string, int>();
			this.hierarchyTraversalInfos = new List<HierarchyTraversalState>();
			this.InitializeHierarchies(groupDescriptions);
		}

		// Token: 0x06007D15 RID: 32021 RVA: 0x001CB110 File Offset: 0x001C9310
		private void InitializeHierarchies(IList<OlapGroupDescription> groupDescriptions)
		{
			for (int i = 0; i < groupDescriptions.Count; i++)
			{
				OlapGroupDescription olapGroupDescription = groupDescriptions[i];
				HierarchyTraversalState item = TupleTraversalState.CreateHierarchyState(i, olapGroupDescription);
				this.hierarchyTraversalInfos.Add(item);
				this.hierarchyUniqueNameToIndexMapping[olapGroupDescription.FieldInfo.Name] = i;
			}
		}

		// Token: 0x06007D16 RID: 32022 RVA: 0x001CB164 File Offset: 0x001C9364
		private static HierarchyTraversalState CreateHierarchyState(int i, OlapGroupDescription groupDescriptionItem)
		{
			return new HierarchyTraversalState
			{
				HieararchyIndex = i,
				HieararchyName = groupDescriptionItem.FieldInfo.Name,
				AllMemberName = groupDescriptionItem.FieldInfo.AllMemberName,
				TotalLevels = groupDescriptionItem.FieldInfo.Levels.Count,
				ShouleIgnoreHierarchicalStructure = groupDescriptionItem.FieldInfo.ShouldIgnoreHierarchicalStructure
			};
		}

		// Token: 0x06007D17 RID: 32023 RVA: 0x001CB1C8 File Offset: 0x001C93C8
		public HierarchyTraversalState GetHierarchyState(int index)
		{
			if (index >= this.hierarchyTraversalInfos.Count)
			{
				return null;
			}
			return this.hierarchyTraversalInfos[index];
		}

		// Token: 0x06007D18 RID: 32024 RVA: 0x001CB1E6 File Offset: 0x001C93E6
		public HierarchyTraversalState GetLastHierarchyState()
		{
			if (this.hierarchyTraversalInfos.Count <= 0)
			{
				return null;
			}
			return this.hierarchyTraversalInfos[this.hierarchyTraversalInfos.Count - 1];
		}

		// Token: 0x06007D19 RID: 32025 RVA: 0x001CB210 File Offset: 0x001C9410
		public LevelTraversalState GetCurrentLevelState()
		{
			LevelTraversalState result = null;
			foreach (HierarchyTraversalState hierarchyTraversalState in this.hierarchyTraversalInfos)
			{
				if (hierarchyTraversalState.HasLevels)
				{
					result = hierarchyTraversalState.GetCurrentLevel();
				}
			}
			return result;
		}

		// Token: 0x06007D1A RID: 32026 RVA: 0x001CB270 File Offset: 0x001C9470
		public LevelTraversalState CreateLevelState(IOlapMember member, int hierarchyIndex)
		{
			HierarchyTraversalState hierarchyState = this.GetHierarchyState(hierarchyIndex);
			if (hierarchyState == null)
			{
				return null;
			}
			return TupleTraversalState.CreateLevelState(member, hierarchyState);
		}

		// Token: 0x06007D1B RID: 32027 RVA: 0x001CB294 File Offset: 0x001C9494
		private static LevelTraversalState CreateLevelState(IOlapMember member, HierarchyTraversalState hierarchy)
		{
			OlapGroupName olapGroupName = new OlapGroupName(member.Caption, member.UniqueName);
			if (member.SortKeys != null)
			{
				foreach (string item in member.SortKeys)
				{
					olapGroupName.SortKeys.Add(item);
				}
			}
			Group group = new Group(olapGroupName);
			LevelTraversalState levelTraversalState = new LevelTraversalState(hierarchy)
			{
				Group = group,
				LevelNumber = member.LevelNumber,
				LevelName = member.LevelName,
				UniqueName = member.UniqueName
			};
			if (hierarchy.ShouleIgnoreHierarchicalStructure)
			{
				levelTraversalState.LevelNumber = 0;
			}
			return levelTraversalState;
		}

		// Token: 0x06007D1C RID: 32028 RVA: 0x001CB358 File Offset: 0x001C9558
		public void Push(LevelTraversalState info)
		{
			if (!TupleTraversalState.LevelStateIsValid(info))
			{
				return;
			}
			info.Hierarchy.PushLevel(info);
			this.ClearHierarchyLevels(info.Hierarchy.HieararchyIndex);
		}

		// Token: 0x06007D1D RID: 32029 RVA: 0x001CB380 File Offset: 0x001C9580
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow", Justification = "Design choice.")]
		private void ClearHierarchyLevels(int hierarchyStartIndex)
		{
			for (int i = hierarchyStartIndex + 1; i < this.hierarchyTraversalInfos.Count; i++)
			{
				this.hierarchyTraversalInfos[i].ClearLevels();
			}
		}

		// Token: 0x06007D1E RID: 32030 RVA: 0x001CB3B6 File Offset: 0x001C95B6
		private static bool LevelStateIsValid(LevelTraversalState state)
		{
			return state != null && state.Hierarchy != null;
		}

		// Token: 0x06007D1F RID: 32031 RVA: 0x001CB3C8 File Offset: 0x001C95C8
		public void TraverseToParent(LevelTraversalState info)
		{
			if (!TupleTraversalState.LevelStateIsValid(info))
			{
				return;
			}
			HierarchyTraversalState hierarchy = info.Hierarchy;
			int levelNumber = info.LevelNumber - 1;
			hierarchy.TraverseToLevel(levelNumber);
			this.ClearHierarchyLevels(hierarchy.HieararchyIndex);
		}

		// Token: 0x04002245 RID: 8773
		private List<HierarchyTraversalState> hierarchyTraversalInfos;

		// Token: 0x04002246 RID: 8774
		private Dictionary<string, int> hierarchyUniqueNameToIndexMapping;
	}
}
