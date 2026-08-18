using System;
using System.Security.Permissions;
using System.Web.UI.Design.WebControls.ListControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000BB RID: 187
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataGridComponentEditor : BaseDataListComponentEditor
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x0001F81B File Offset: 0x0001DA1B
		public DataGridComponentEditor() : base(DataGridComponentEditor.IDX_GENERAL)
		{
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001F828 File Offset: 0x0001DA28
		public DataGridComponentEditor(int initialPage) : base(initialPage)
		{
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001F831 File Offset: 0x0001DA31
		protected override Type[] GetComponentEditorPages()
		{
			return DataGridComponentEditor.editorPages;
		}

		// Token: 0x04000348 RID: 840
		private static Type[] editorPages = new Type[]
		{
			typeof(DataGridGeneralPage),
			typeof(DataGridColumnsPage),
			typeof(DataGridPagingPage),
			typeof(FormatPage),
			typeof(BordersPage)
		};

		// Token: 0x04000349 RID: 841
		internal static int IDX_GENERAL = 0;

		// Token: 0x0400034A RID: 842
		internal static int IDX_COLUMNS = 1;

		// Token: 0x0400034B RID: 843
		internal static int IDX_PAGING = 2;

		// Token: 0x0400034C RID: 844
		internal static int IDX_FORMAT = 3;

		// Token: 0x0400034D RID: 845
		internal static int IDX_BORDERS = 4;
	}
}
