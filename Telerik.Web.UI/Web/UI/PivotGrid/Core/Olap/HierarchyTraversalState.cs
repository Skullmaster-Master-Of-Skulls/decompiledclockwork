using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D12 RID: 3346
	internal class HierarchyTraversalState
	{
		// Token: 0x06007CAE RID: 31918 RVA: 0x001C9FC5 File Offset: 0x001C81C5
		public HierarchyTraversalState()
		{
			this.levelsStack = new Stack<LevelTraversalState>();
		}

		// Token: 0x170027C6 RID: 10182
		// (get) Token: 0x06007CAF RID: 31919 RVA: 0x001C9FD8 File Offset: 0x001C81D8
		public bool HasLevels
		{
			get
			{
				return this.levelsStack.Count > 0;
			}
		}

		// Token: 0x170027C7 RID: 10183
		// (get) Token: 0x06007CB0 RID: 31920 RVA: 0x001C9FE8 File Offset: 0x001C81E8
		// (set) Token: 0x06007CB1 RID: 31921 RVA: 0x001C9FF0 File Offset: 0x001C81F0
		public int HieararchyIndex { get; set; }

		// Token: 0x170027C8 RID: 10184
		// (get) Token: 0x06007CB2 RID: 31922 RVA: 0x001C9FF9 File Offset: 0x001C81F9
		// (set) Token: 0x06007CB3 RID: 31923 RVA: 0x001CA001 File Offset: 0x001C8201
		public string HieararchyName { get; set; }

		// Token: 0x170027C9 RID: 10185
		// (get) Token: 0x06007CB4 RID: 31924 RVA: 0x001CA00A File Offset: 0x001C820A
		// (set) Token: 0x06007CB5 RID: 31925 RVA: 0x001CA012 File Offset: 0x001C8212
		public int TotalLevels { get; set; }

		// Token: 0x170027CA RID: 10186
		// (get) Token: 0x06007CB6 RID: 31926 RVA: 0x001CA01B File Offset: 0x001C821B
		// (set) Token: 0x06007CB7 RID: 31927 RVA: 0x001CA023 File Offset: 0x001C8223
		public string AllMemberName { get; set; }

		// Token: 0x170027CB RID: 10187
		// (get) Token: 0x06007CB8 RID: 31928 RVA: 0x001CA02C File Offset: 0x001C822C
		// (set) Token: 0x06007CB9 RID: 31929 RVA: 0x001CA034 File Offset: 0x001C8234
		public bool ShouleIgnoreHierarchicalStructure { get; set; }

		// Token: 0x06007CBA RID: 31930 RVA: 0x001CA03D File Offset: 0x001C823D
		public LevelTraversalState GetCurrentLevel()
		{
			if (this.HasLevels)
			{
				return this.levelsStack.Peek();
			}
			return null;
		}

		// Token: 0x06007CBB RID: 31931 RVA: 0x001CA054 File Offset: 0x001C8254
		public void PushLevel(LevelTraversalState level)
		{
			this.levelsStack.Push(level);
		}

		// Token: 0x06007CBC RID: 31932 RVA: 0x001CA062 File Offset: 0x001C8262
		public LevelTraversalState PopLevel()
		{
			if (this.HasLevels)
			{
				return this.levelsStack.Pop();
			}
			return null;
		}

		// Token: 0x06007CBD RID: 31933 RVA: 0x001CA079 File Offset: 0x001C8279
		public void ClearLevels()
		{
			this.levelsStack.Clear();
		}

		// Token: 0x06007CBE RID: 31934 RVA: 0x001CA088 File Offset: 0x001C8288
		public void TraverseToLevel(int levelNumber)
		{
			while (this.HasLevels)
			{
				LevelTraversalState currentLevel = this.GetCurrentLevel();
				if (currentLevel.LevelNumber <= levelNumber)
				{
					break;
				}
				this.PopLevel();
			}
		}

		// Token: 0x04002226 RID: 8742
		private Stack<LevelTraversalState> levelsStack;
	}
}
