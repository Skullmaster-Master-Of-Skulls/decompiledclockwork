using System;
using System.Design;
using System.Security.Permissions;
using System.Windows.Forms.Design;

namespace System.Diagnostics.Design
{
	// Token: 0x02000216 RID: 534
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal class WorkingDirectoryEditor : FolderNameEditor
	{
		// Token: 0x06001398 RID: 5016 RVA: 0x0006FD92 File Offset: 0x0006DF92
		protected override void InitializeDialog(FolderNameEditor.FolderBrowser folderBrowser)
		{
			folderBrowser.Description = SR.GetString("WorkingDirectoryEditorLabel");
		}
	}
}
