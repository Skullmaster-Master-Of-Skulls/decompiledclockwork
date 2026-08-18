using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020019F4 RID: 6644
	public class GridHTMLEditorColumn : GridBoundColumn
	{
		// Token: 0x06010147 RID: 65863 RVA: 0x0039CC20 File Offset: 0x0039AE20
		public override GridColumn Clone()
		{
			GridHTMLEditorColumn gridHTMLEditorColumn = new GridHTMLEditorColumn();
			gridHTMLEditorColumn.CopyBaseProperties(this);
			return gridHTMLEditorColumn;
		}

		// Token: 0x06010148 RID: 65864 RVA: 0x0039CC3B File Offset: 0x0039AE3B
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridHTMLEditorColumn gridHTMLEditorColumn = (GridHTMLEditorColumn)fromColumn;
		}

		// Token: 0x06010149 RID: 65865 RVA: 0x0039CC4B File Offset: 0x0039AE4B
		protected override IGridColumnEditor CreateDefaultColumnEditor()
		{
			if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile && base.UseNativeEditorsInMobileMode)
			{
				return new GridMobileHTMLEditorColumnEditor(this);
			}
			return new GridHTMLEditorColumnEditor(this);
		}

		// Token: 0x0601014A RID: 65866 RVA: 0x0039CC75 File Offset: 0x0039AE75
		protected override void SetCurrentFilterValueToControl(TableCell cell)
		{
			base.SetCurrentFilterValueToControl(cell);
			if (!string.IsNullOrEmpty(this.CurrentFilterValue))
			{
				((TextBox)cell.Controls[0]).Text = this.CurrentFilterValue;
			}
		}
	}
}
