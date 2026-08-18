using System;
using System.Design;
using System.Security.Permissions;
using System.Windows.Forms.Design;

namespace System.Diagnostics.Design
{
	// Token: 0x02000210 RID: 528
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal class FSWPathEditor : FolderNameEditor
	{
		// Token: 0x06001389 RID: 5001 RVA: 0x0006FB13 File Offset: 0x0006DD13
		protected override void InitializeDialog(FolderNameEditor.FolderBrowser folderBrowser)
		{
			folderBrowser.Description = SR.GetString("FSWPathEditorLabel");
		}
	}
}
