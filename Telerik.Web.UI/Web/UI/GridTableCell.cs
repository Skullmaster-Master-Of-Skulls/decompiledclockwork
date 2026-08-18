using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200117D RID: 4477
	public class GridTableCell : TableCell
	{
		// Token: 0x0600B683 RID: 46723 RVA: 0x002838BF File Offset: 0x00281ABF
		public GridTableCell()
		{
		}

		// Token: 0x0600B684 RID: 46724 RVA: 0x002838C7 File Offset: 0x00281AC7
		public GridTableCell(bool useNbsp)
		{
			this.Text = (useNbsp ? "&nbsp;" : "");
		}

		// Token: 0x17003AF7 RID: 15095
		// (get) Token: 0x0600B685 RID: 46725 RVA: 0x002838E4 File Offset: 0x00281AE4
		// (set) Token: 0x0600B686 RID: 46726 RVA: 0x002838EC File Offset: 0x00281AEC
		internal bool WrapElements
		{
			get
			{
				return this.wrapElements;
			}
			set
			{
				this.wrapElements = value;
			}
		}

		// Token: 0x17003AF8 RID: 15096
		// (get) Token: 0x0600B687 RID: 46727 RVA: 0x002838F5 File Offset: 0x00281AF5
		// (set) Token: 0x0600B688 RID: 46728 RVA: 0x002838FD File Offset: 0x00281AFD
		public GridItem Item
		{
			get
			{
				return this._parentItem;
			}
			set
			{
				this._parentItem = value;
			}
		}

		// Token: 0x17003AF9 RID: 15097
		// (get) Token: 0x0600B689 RID: 46729 RVA: 0x00283908 File Offset: 0x00281B08
		public string ParentItemIndexHierarchical
		{
			get
			{
				string result = string.Empty;
				if (this.Item != null)
				{
					result = this.Item.ItemIndexHierarchical;
				}
				return result;
			}
		}

		// Token: 0x17003AFA RID: 15098
		// (get) Token: 0x0600B68A RID: 46730 RVA: 0x00283930 File Offset: 0x00281B30
		// (set) Token: 0x0600B68B RID: 46731 RVA: 0x00283938 File Offset: 0x00281B38
		public GridColumn Column
		{
			get
			{
				return this._column;
			}
			set
			{
				this._column = value;
			}
		}

		// Token: 0x17003AFB RID: 15099
		// (get) Token: 0x0600B68C RID: 46732 RVA: 0x00283944 File Offset: 0x00281B44
		public string CellIndexHierarchical
		{
			get
			{
				string result = string.Empty;
				if (this.Item != null && this.Column != null)
				{
					result = this.Item.ItemIndexHierarchical + "&" + this.Column.UniqueName;
				}
				return result;
			}
		}

		// Token: 0x17003AFC RID: 15100
		// (get) Token: 0x0600B68D RID: 46733 RVA: 0x0028398C File Offset: 0x00281B8C
		// (set) Token: 0x0600B68E RID: 46734 RVA: 0x002839D0 File Offset: 0x00281BD0
		public bool Selected
		{
			get
			{
				bool result = false;
				if (this.Item != null && this.Column != null)
				{
					result = this.Item.OwnerTableView.OwnerGrid.SelectedCellIndexes.Contains(this.CellIndexHierarchical);
				}
				return result;
			}
			set
			{
				if (this.Item != null && this.Column != null)
				{
					if (value)
					{
						this.Item.OwnerTableView.OwnerGrid.SelectedCellIndexes.Add(this.CellIndexHierarchical);
						return;
					}
					this.Item.OwnerTableView.OwnerGrid.SelectedCellIndexes.Remove(this.CellIndexHierarchical);
				}
			}
		}

		// Token: 0x0600B68F RID: 46735 RVA: 0x00283A31 File Offset: 0x00281C31
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this.WrapElements)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				base.RenderContents(writer);
				writer.RenderEndTag();
				return;
			}
			base.RenderContents(writer);
		}

		// Token: 0x04003019 RID: 12313
		private GridItem _parentItem;

		// Token: 0x0400301A RID: 12314
		private GridColumn _column;

		// Token: 0x0400301B RID: 12315
		private bool wrapElements;
	}
}
