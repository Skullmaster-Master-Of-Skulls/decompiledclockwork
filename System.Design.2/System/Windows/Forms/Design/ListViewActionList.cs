using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200030B RID: 779
	internal class ListViewActionList : DesignerActionList
	{
		// Token: 0x06001ED1 RID: 7889 RVA: 0x000B878C File Offset: 0x000B698C
		public ListViewActionList(ComponentDesigner designer) : base(designer.Component)
		{
			this._designer = designer;
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x000B87A1 File Offset: 0x000B69A1
		public void InvokeItemsDialog()
		{
			EditorServiceContext.EditValue(this._designer, base.Component, "Items");
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x000B87BA File Offset: 0x000B69BA
		public void InvokeColumnsDialog()
		{
			EditorServiceContext.EditValue(this._designer, base.Component, "Columns");
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x000B87D3 File Offset: 0x000B69D3
		public void InvokeGroupsDialog()
		{
			EditorServiceContext.EditValue(this._designer, base.Component, "Groups");
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x000B87EC File Offset: 0x000B69EC
		// (set) Token: 0x06001ED6 RID: 7894 RVA: 0x000B87FE File Offset: 0x000B69FE
		public View View
		{
			get
			{
				return ((ListView)base.Component).View;
			}
			set
			{
				TypeDescriptor.GetProperties(base.Component)["View"].SetValue(base.Component, value);
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x000B8826 File Offset: 0x000B6A26
		// (set) Token: 0x06001ED8 RID: 7896 RVA: 0x000B8838 File Offset: 0x000B6A38
		public ImageList LargeImageList
		{
			get
			{
				return ((ListView)base.Component).LargeImageList;
			}
			set
			{
				TypeDescriptor.GetProperties(base.Component)["LargeImageList"].SetValue(base.Component, value);
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x000B885B File Offset: 0x000B6A5B
		// (set) Token: 0x06001EDA RID: 7898 RVA: 0x000B886D File Offset: 0x000B6A6D
		public ImageList SmallImageList
		{
			get
			{
				return ((ListView)base.Component).SmallImageList;
			}
			set
			{
				TypeDescriptor.GetProperties(base.Component)["SmallImageList"].SetValue(base.Component, value);
			}
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x000B8890 File Offset: 0x000B6A90
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			return new DesignerActionItemCollection
			{
				new DesignerActionMethodItem(this, "InvokeItemsDialog", SR.GetString("ListViewActionListEditItemsDisplayName"), SR.GetString("PropertiesCategoryName"), SR.GetString("ListViewActionListEditItemsDescription"), true),
				new DesignerActionMethodItem(this, "InvokeColumnsDialog", SR.GetString("ListViewActionListEditColumnsDisplayName"), SR.GetString("PropertiesCategoryName"), SR.GetString("ListViewActionListEditColumnsDescription"), true),
				new DesignerActionMethodItem(this, "InvokeGroupsDialog", SR.GetString("ListViewActionListEditGroupsDisplayName"), SR.GetString("PropertiesCategoryName"), SR.GetString("ListViewActionListEditGroupsDescription"), true),
				new DesignerActionPropertyItem("View", SR.GetString("ListViewActionListViewDisplayName"), SR.GetString("PropertiesCategoryName"), SR.GetString("ListViewActionListViewDescription")),
				new DesignerActionPropertyItem("SmallImageList", SR.GetString("ListViewActionListSmallImagesDisplayName"), SR.GetString("PropertiesCategoryName"), SR.GetString("ListViewActionListSmallImagesDescription")),
				new DesignerActionPropertyItem("LargeImageList", SR.GetString("ListViewActionListLargeImagesDisplayName"), SR.GetString("PropertiesCategoryName"), SR.GetString("ListViewActionListLargeImagesDescription"))
			};
		}

		// Token: 0x040017DE RID: 6110
		private ComponentDesigner _designer;
	}
}
