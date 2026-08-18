using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200128F RID: 4751
	public class TreeListDisplayIndexGenerator
	{
		// Token: 0x0600C630 RID: 50736 RVA: 0x002C39BC File Offset: 0x002C1BBC
		private TreeListDisplayIndexGenerator()
		{
			this._generatedIndexes = new int[40];
		}

		// Token: 0x0600C631 RID: 50737 RVA: 0x002C39D1 File Offset: 0x002C1BD1
		public static TreeListDisplayIndexGenerator Create()
		{
			return new TreeListDisplayIndexGenerator();
		}

		// Token: 0x0600C632 RID: 50738 RVA: 0x002C39D8 File Offset: 0x002C1BD8
		public void Reset()
		{
			this._generatedIndexes = new int[40];
		}

		// Token: 0x0600C633 RID: 50739 RVA: 0x002C39E7 File Offset: 0x002C1BE7
		public int GetLevelIndex(int nestedLevel)
		{
			if (nestedLevel < this._generatedIndexes.Length)
			{
				return this._generatedIndexes[nestedLevel];
			}
			return 0;
		}

		// Token: 0x0600C634 RID: 50740 RVA: 0x002C3A00 File Offset: 0x002C1C00
		public int GenerateIndex(int nestedLevel)
		{
			if (nestedLevel >= this._generatedIndexes.Length)
			{
				Array.Resize<int>(ref this._generatedIndexes, nestedLevel + 1);
			}
			return ++this._generatedIndexes[nestedLevel] - 1;
		}

		// Token: 0x04003468 RID: 13416
		private int[] _generatedIndexes;
	}
}
