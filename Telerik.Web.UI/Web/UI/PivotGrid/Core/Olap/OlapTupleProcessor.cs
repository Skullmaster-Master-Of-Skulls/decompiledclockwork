using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D16 RID: 3350
	internal class OlapTupleProcessor
	{
		// Token: 0x06007CE8 RID: 31976 RVA: 0x001CA7F4 File Offset: 0x001C89F4
		public OlapTupleProcessor(OlapTupleProcessorInput tupleInfo)
		{
			this.tupleInfo = tupleInfo;
			this.groupingInfo = new OlapTupleProcessorOutput();
			this.typeInspector = new TupleTypeInspector(this.tupleInfo.GroupDescriptions);
			this.tupleTraversal = new TupleTraversalState(this.tupleInfo.GroupDescriptions);
			this.InitializeAggregatesDisctionary();
		}

		// Token: 0x06007CE9 RID: 31977 RVA: 0x001CA858 File Offset: 0x001C8A58
		private void InitializeAggregatesDisctionary()
		{
			this.aggregateIndicesDictionary = new Dictionary<string, int>();
			int num = 0;
			foreach (OlapAggregateDescription olapAggregateDescription in this.tupleInfo.AggregateDescriptions)
			{
				this.aggregateIndicesDictionary[olapAggregateDescription.MemberName] = num;
				num++;
			}
		}

		// Token: 0x06007CEA RID: 31978 RVA: 0x001CA8C8 File Offset: 0x001C8AC8
		public OlapTupleProcessorOutput Process()
		{
			IList<IOlapTuple> tuples = this.tupleInfo.Tuples;
			this.AssignRaggedBottomLevels(tuples);
			for (int i = 0; i <= tuples.Count - 1; i++)
			{
				IOlapTuple tupleItem = tuples[i];
				this.ProcessTuple(tupleItem, i);
			}
			return this.groupingInfo;
		}

		// Token: 0x06007CEB RID: 31979 RVA: 0x001CA914 File Offset: 0x001C8B14
		private void AssignRaggedBottomLevels(IList<IOlapTuple> tuples)
		{
			List<int> list = this.HierarchyGroupDescriptionIndexes();
			foreach (int num in list)
			{
				int num2 = 0;
				string levelUniqueName = string.Empty;
				for (int i = 0; i <= tuples.Count - 1; i++)
				{
					IOlapTuple olapTuple = tuples[i];
					List<IOlapMember> list2 = olapTuple.Members.OfType<IOlapMember>().ToList<IOlapMember>();
					LevelTraversalState levelTraversalState = this.tupleTraversal.CreateLevelState(list2[num], num);
					if (num2 == list2[num].LevelNumber && num2 != levelTraversalState.Hierarchy.TotalLevels - 1)
					{
						this.AddToRaggedBottomLevels(levelUniqueName);
					}
					else if (num2 > list2[num].LevelNumber)
					{
						if (num2 != levelTraversalState.Hierarchy.TotalLevels - 1)
						{
							this.AddToRaggedBottomLevels(levelUniqueName);
						}
					}
					else
					{
						this.RemoveFromRaggedBottomLevels(levelUniqueName);
					}
					levelUniqueName = levelTraversalState.UniqueName;
					num2 = levelTraversalState.LevelNumber;
				}
			}
		}

		// Token: 0x06007CEC RID: 31980 RVA: 0x001CAA2C File Offset: 0x001C8C2C
		private void AddToRaggedBottomLevels(string levelUniqueName)
		{
			if (!string.IsNullOrEmpty(levelUniqueName) && !this.raggedBottomLevels.Contains(levelUniqueName))
			{
				this.raggedBottomLevels.Add(levelUniqueName);
			}
		}

		// Token: 0x06007CED RID: 31981 RVA: 0x001CAA50 File Offset: 0x001C8C50
		private void RemoveFromRaggedBottomLevels(string levelUniqueName)
		{
			if (!string.IsNullOrEmpty(levelUniqueName) && this.raggedBottomLevels.Contains(levelUniqueName))
			{
				this.raggedBottomLevels.Remove(levelUniqueName);
			}
		}

		// Token: 0x06007CEE RID: 31982 RVA: 0x001CAA78 File Offset: 0x001C8C78
		private List<int> HierarchyGroupDescriptionIndexes()
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this.tupleInfo.GroupDescriptions.Count; i++)
			{
				if (this.tupleInfo.GroupDescriptions[i].Levels.Count > 1)
				{
					list.Add(i);
				}
			}
			return list;
		}

		// Token: 0x06007CEF RID: 31983 RVA: 0x001CAACC File Offset: 0x001C8CCC
		private void ProcessTuple(IOlapTuple tupleItem, int tupleIndex)
		{
			List<IOlapMember> tupleMembers = tupleItem.Members.OfType<IOlapMember>().ToList<IOlapMember>();
			switch (this.typeInspector.DetermineTupleType(tupleMembers))
			{
			case TupleType.GrandTotal:
				this.ProcessGrandTotalTuple(tupleIndex);
				return;
			case TupleType.Invalid:
				return;
			}
			this.ProcessNonGrandTotalTuple(tupleMembers, tupleIndex);
		}

		// Token: 0x06007CF0 RID: 31984 RVA: 0x001CAB1D File Offset: 0x001C8D1D
		private void ProcessGrandTotalTuple(int tupleIndex)
		{
			this.AddTupleForLaterProcessing(this.groupingInfo.RootGroup, tupleIndex);
		}

		// Token: 0x06007CF1 RID: 31985 RVA: 0x001CAB34 File Offset: 0x001C8D34
		private void ProcessNonGrandTotalTuple(IList<IOlapMember> tupleMembers, int tupleIndex)
		{
			int count = this.tupleInfo.GroupDescriptions.Count;
			this.tupleRepresentsATotal = false;
			int num = 0;
			while (num < count && !this.tupleRepresentsATotal)
			{
				this.ProcessMember(tupleMembers[num], num);
				num++;
			}
			this.MarkTupleForLaterProcessingIfNeeded(tupleIndex);
		}

		// Token: 0x06007CF2 RID: 31986 RVA: 0x001CAB84 File Offset: 0x001C8D84
		private void ProcessMember(IOlapMember memberItem, int tupleIndex)
		{
			LevelTraversalState levelTraversalState = this.tupleTraversal.CreateLevelState(memberItem, tupleIndex);
			if (levelTraversalState == null)
			{
				return;
			}
			HierarchyTraversalState hierarchy = levelTraversalState.Hierarchy;
			if (this.rootGroup == null)
			{
				this.PrepareRoot(levelTraversalState);
				return;
			}
			bool flag = !hierarchy.HasLevels;
			if (flag)
			{
				this.ProcessNextHierarchy(levelTraversalState);
				return;
			}
			this.ProcessSameHierarchy(levelTraversalState);
		}

		// Token: 0x06007CF3 RID: 31987 RVA: 0x001CABD8 File Offset: 0x001C8DD8
		private void ProcessNextHierarchy(LevelTraversalState newLevelInfo)
		{
			LevelTraversalState currentLevelState = this.tupleTraversal.GetCurrentLevelState();
			int totalLevels = currentLevelState.Hierarchy.TotalLevels;
			bool flag = currentLevelState.LevelNumber >= totalLevels - 1 || this.raggedBottomLevels.Contains(currentLevelState.UniqueName);
			if (flag)
			{
				this.AddToGroupHierarchy(newLevelInfo);
				this.tupleTraversal.Push(newLevelInfo);
				return;
			}
			if (newLevelInfo.IsTotal)
			{
				this.tupleRepresentsATotal = true;
			}
		}

		// Token: 0x06007CF4 RID: 31988 RVA: 0x001CAC44 File Offset: 0x001C8E44
		private void ProcessSameHierarchy(LevelTraversalState newLevelInfo)
		{
			HierarchyTraversalState hierarchy = newLevelInfo.Hierarchy;
			LevelTraversalState currentLevel = hierarchy.GetCurrentLevel();
			bool flag = newLevelInfo.LevelNumber > currentLevel.LevelNumber;
			if (newLevelInfo.UniqueName == currentLevel.UniqueName)
			{
				return;
			}
			if (flag)
			{
				this.AddToGroupHierarchy(newLevelInfo);
				this.tupleTraversal.Push(newLevelInfo);
				return;
			}
			this.tupleTraversal.TraverseToParent(newLevelInfo);
			this.AddToGroupHierarchy(newLevelInfo);
			this.tupleTraversal.Push(newLevelInfo);
		}

		// Token: 0x06007CF5 RID: 31989 RVA: 0x001CACB8 File Offset: 0x001C8EB8
		private void MarkTupleForLaterProcessingIfNeeded(int tupleIndex)
		{
			HierarchyTraversalState lastHierarchyState = this.tupleTraversal.GetLastHierarchyState();
			LevelTraversalState currentLevelState = this.tupleTraversal.GetCurrentLevelState();
			if (lastHierarchyState.HasLevels || this.tupleRepresentsATotal)
			{
				this.AddTupleForLaterProcessing(currentLevelState.Group, tupleIndex);
			}
		}

		// Token: 0x06007CF6 RID: 31990 RVA: 0x001CACFC File Offset: 0x001C8EFC
		private void AddTupleForLaterProcessing(Group tupleGroup, int tupleIndex)
		{
			int aggregateIndex = this.FindAggregateIndexForTuple(tupleIndex);
			ProcessedTuple pair = new ProcessedTuple
			{
				Group = tupleGroup,
				SourceTupleIndex = tupleIndex,
				AggregateIndex = aggregateIndex
			};
			this.groupingInfo.AddTuple(pair);
		}

		// Token: 0x06007CF7 RID: 31991 RVA: 0x001CAD3C File Offset: 0x001C8F3C
		private int FindAggregateIndexForTuple(int tupleIndex)
		{
			IOlapTuple olapTuple = this.tupleInfo.Tuples[tupleIndex];
			IOlapMember olapMember = null;
			foreach (object obj in olapTuple.Members)
			{
				olapMember = (obj as IOlapMember);
			}
			if (olapMember == null)
			{
				return -1;
			}
			if (this.aggregateIndicesDictionary.ContainsKey(olapMember.UniqueName))
			{
				return this.aggregateIndicesDictionary[olapMember.UniqueName];
			}
			return -1;
		}

		// Token: 0x06007CF8 RID: 31992 RVA: 0x001CADD4 File Offset: 0x001C8FD4
		private void AddToGroupHierarchy(LevelTraversalState info)
		{
			if (this.tupleTraversal.GetCurrentLevelState() == null)
			{
				return;
			}
			if (info.IsTotal)
			{
				info.Group = this.tupleTraversal.GetCurrentLevelState().Group;
				return;
			}
			this.tupleTraversal.GetCurrentLevelState().Group.AddGroup(info.Group);
		}

		// Token: 0x06007CF9 RID: 31993 RVA: 0x001CAE2B File Offset: 0x001C902B
		private void PrepareRoot(LevelTraversalState info)
		{
			if (info.IsTotal)
			{
				this.PrepraRootForTotal(info);
			}
			else
			{
				this.PrepareRootForNonTotal(info);
			}
			this.tupleTraversal.Push(info);
		}

		// Token: 0x06007CFA RID: 31994 RVA: 0x001CAE54 File Offset: 0x001C9054
		private void PrepareRootForNonTotal(LevelTraversalState info)
		{
			Group group = this.groupingInfo.RootGroup;
			this.rootGroup = group;
			group.AddGroup(info.Group);
			HierarchyTraversalState hierarchyState = this.tupleTraversal.GetHierarchyState(0);
			LevelTraversalState info2 = new LevelTraversalState(hierarchyState)
			{
				Group = group,
				LevelName = "Root",
				LevelNumber = info.LevelNumber - 1
			};
			this.tupleTraversal.Push(info2);
			this.groupingInfo.RootGroup = group;
		}

		// Token: 0x06007CFB RID: 31995 RVA: 0x001CAED0 File Offset: 0x001C90D0
		private void PrepraRootForTotal(LevelTraversalState info)
		{
			Group group = this.groupingInfo.RootGroup;
			info.Group = group;
			this.rootGroup = info.Group;
		}

		// Token: 0x04002235 RID: 8757
		private TupleTypeInspector typeInspector;

		// Token: 0x04002236 RID: 8758
		private OlapTupleProcessorOutput groupingInfo;

		// Token: 0x04002237 RID: 8759
		private OlapTupleProcessorInput tupleInfo;

		// Token: 0x04002238 RID: 8760
		private TupleTraversalState tupleTraversal;

		// Token: 0x04002239 RID: 8761
		private bool tupleRepresentsATotal;

		// Token: 0x0400223A RID: 8762
		private Group rootGroup;

		// Token: 0x0400223B RID: 8763
		private Dictionary<string, int> aggregateIndicesDictionary;

		// Token: 0x0400223C RID: 8764
		private List<string> raggedBottomLevels = new List<string>();
	}
}
