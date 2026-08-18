using System;

namespace Telerik.Web.UI.Widgets
{
	// Token: 0x0200132F RID: 4911
	public class FileBrowserRoot : DirectoryItem
	{
		// Token: 0x0600CD24 RID: 52516 RVA: 0x002DB05B File Offset: 0x002D925B
		public FileBrowserRoot(DirectoryItem[] directories) : base("", "", "", "", PathPermissions.Read, new FileItem[0], directories)
		{
		}
	}
}
