using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap.NodeBuilders
{
	// Token: 0x02000D06 RID: 3334
	internal class OlapDisplayFolderParser
	{
		// Token: 0x06007C4B RID: 31819 RVA: 0x001C9529 File Offset: 0x001C7729
		public OlapDisplayFolderParser(string olapFolder)
		{
			this.olapFolder = olapFolder;
			this.FolderLevels = new List<string>();
			this.ParseFolder();
		}

		// Token: 0x170027A9 RID: 10153
		// (get) Token: 0x06007C4C RID: 31820 RVA: 0x001C9549 File Offset: 0x001C7749
		public bool HasFolder
		{
			get
			{
				return !string.IsNullOrEmpty(this.olapFolder);
			}
		}

		// Token: 0x170027AA RID: 10154
		// (get) Token: 0x06007C4D RID: 31821 RVA: 0x001C9559 File Offset: 0x001C7759
		// (set) Token: 0x06007C4E RID: 31822 RVA: 0x001C9561 File Offset: 0x001C7761
		public IList<string> FolderLevels { get; private set; }

		// Token: 0x06007C4F RID: 31823 RVA: 0x001C956C File Offset: 0x001C776C
		private void ParseFolder()
		{
			if (!this.HasFolder)
			{
				return;
			}
			string[] array = this.olapFolder.Split(new string[]
			{
				";"
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length > 0)
			{
				this.CreateFoldersFromString(array[0]);
			}
		}

		// Token: 0x06007C50 RID: 31824 RVA: 0x001C95B0 File Offset: 0x001C77B0
		private void CreateFoldersFromString(string folderString)
		{
			string[] array = folderString.Split(new string[]
			{
				"\\"
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string item in array)
			{
				this.FolderLevels.Add(item);
			}
		}

		// Token: 0x0400220F RID: 8719
		private string olapFolder;
	}
}
