using System;
using System.Security.Permissions;
using System.Web.UI.Design.WebControls.ListControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000BE RID: 190
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataListComponentEditor : BaseDataListComponentEditor
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x00020457 File Offset: 0x0001E657
		public DataListComponentEditor() : base(DataListComponentEditor.IDX_GENERAL)
		{
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001F828 File Offset: 0x0001DA28
		public DataListComponentEditor(int initialPage) : base(initialPage)
		{
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00020464 File Offset: 0x0001E664
		protected override Type[] GetComponentEditorPages()
		{
			return DataListComponentEditor.editorPages;
		}

		// Token: 0x04000370 RID: 880
		private static Type[] editorPages = new Type[]
		{
			typeof(DataListGeneralPage),
			typeof(FormatPage),
			typeof(BordersPage)
		};

		// Token: 0x04000371 RID: 881
		internal static int IDX_GENERAL = 0;

		// Token: 0x04000372 RID: 882
		internal static int IDX_FORMAT = 1;

		// Token: 0x04000373 RID: 883
		internal static int IDX_BORDERS = 2;
	}
}
