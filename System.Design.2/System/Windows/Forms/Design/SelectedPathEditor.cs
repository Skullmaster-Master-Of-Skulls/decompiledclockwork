using System;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200032A RID: 810
	internal class SelectedPathEditor : FolderNameEditor
	{
		// Token: 0x06001FE8 RID: 8168 RVA: 0x000C13A9 File Offset: 0x000BF5A9
		protected override void InitializeDialog(FolderNameEditor.FolderBrowser folderBrowser)
		{
			folderBrowser.Description = SR.GetString("SelectedPathEditorLabel");
		}
	}
}
