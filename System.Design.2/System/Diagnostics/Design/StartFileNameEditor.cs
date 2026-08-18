using System;
using System.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Diagnostics.Design
{
	// Token: 0x02000215 RID: 533
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal class StartFileNameEditor : FileNameEditor
	{
		// Token: 0x06001396 RID: 5014 RVA: 0x0006FD68 File Offset: 0x0006DF68
		protected override void InitializeDialog(OpenFileDialog openFile)
		{
			openFile.Filter = SR.GetString("StartFileNameEditorAllFiles");
			openFile.Title = SR.GetString("StartFileNameEditorTitle");
		}
	}
}
