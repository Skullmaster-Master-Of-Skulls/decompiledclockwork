using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000369 RID: 873
	internal class TreeViewActionList : DesignerActionList
	{
		// Token: 0x060023D9 RID: 9177 RVA: 0x000E0034 File Offset: 0x000DE234
		public TreeViewActionList(TreeViewDesigner designer) : base(designer.Component)
		{
			this._designer = designer;
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000E0049 File Offset: 0x000DE249
		public void InvokeNodesDialog()
		{
			EditorServiceContext.EditValue(this._designer, base.Component, "Nodes");
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060023DB RID: 9179 RVA: 0x000E0062 File Offset: 0x000DE262
		// (set) Token: 0x060023DC RID: 9180 RVA: 0x000E0074 File Offset: 0x000DE274
		public ImageList ImageList
		{
			get
			{
				return ((TreeView)base.Component).ImageList;
			}
			set
			{
				TypeDescriptor.GetProperties(base.Component)["ImageList"].SetValue(base.Component, value);
			}
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000E0098 File Offset: 0x000DE298
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			return new DesignerActionItemCollection
			{
				new DesignerActionMethodItem(this, "InvokeNodesDialog", SR.GetString("InvokeNodesDialogDisplayName"), SR.GetString("PropertiesCategoryName"), SR.GetString("InvokeNodesDialogDescription"), true),
				new DesignerActionPropertyItem("ImageList", SR.GetString("ImageListDisplayName"), SR.GetString("PropertiesCategoryName"), SR.GetString("ImageListDescription"))
			};
		}

		// Token: 0x04001A43 RID: 6723
		private TreeViewDesigner _designer;
	}
}
