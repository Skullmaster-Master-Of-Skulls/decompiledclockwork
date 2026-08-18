using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200112F RID: 4399
	public class GridDragDropEventArgs : EventArgs
	{
		// Token: 0x170039F8 RID: 14840
		// (get) Token: 0x0600B36E RID: 45934 RVA: 0x002716B7 File Offset: 0x0026F8B7
		public GridDataItem DestDataItem
		{
			get
			{
				return this._destDataItem;
			}
		}

		// Token: 0x170039F9 RID: 14841
		// (get) Token: 0x0600B36F RID: 45935 RVA: 0x002716BF File Offset: 0x0026F8BF
		public IList<GridDataItem> DraggedItems
		{
			get
			{
				return this._draggedItems;
			}
		}

		// Token: 0x170039FA RID: 14842
		// (get) Token: 0x0600B370 RID: 45936 RVA: 0x002716C7 File Offset: 0x0026F8C7
		public string HtmlElement
		{
			get
			{
				return this._htmlElement;
			}
		}

		// Token: 0x170039FB RID: 14843
		// (get) Token: 0x0600B371 RID: 45937 RVA: 0x002716CF File Offset: 0x0026F8CF
		public RadGrid DestinationGrid
		{
			get
			{
				return this._destinationGrid;
			}
		}

		// Token: 0x170039FC RID: 14844
		// (get) Token: 0x0600B372 RID: 45938 RVA: 0x002716D7 File Offset: 0x0026F8D7
		public GridItemDropPosition DropPosition
		{
			get
			{
				return this._dropPosition;
			}
		}

		// Token: 0x170039FD RID: 14845
		// (get) Token: 0x0600B373 RID: 45939 RVA: 0x002716DF File Offset: 0x0026F8DF
		public GridTableView DestinationTableView
		{
			get
			{
				return this._destinationItemTableView;
			}
		}

		// Token: 0x0600B374 RID: 45940 RVA: 0x002716E7 File Offset: 0x0026F8E7
		public GridDragDropEventArgs(IList<GridDataItem> draggedItems, GridDataItem destDataItem, RadGrid destinationGrid, GridItemDropPosition dropPosition) : this(draggedItems, destDataItem, destinationGrid, dropPosition, null)
		{
		}

		// Token: 0x0600B375 RID: 45941 RVA: 0x002716F5 File Offset: 0x0026F8F5
		public GridDragDropEventArgs(IList<GridDataItem> draggedItems, GridDataItem destDataItem, RadGrid destinationGrid, GridItemDropPosition dropPosition, GridTableView destinationTableView)
		{
			this._htmlElement = string.Empty;
			base..ctor();
			this._destDataItem = destDataItem;
			this._dropPosition = dropPosition;
			this._destinationItemTableView = destinationTableView;
			this._draggedItems = draggedItems;
			this._destinationGrid = destinationGrid;
		}

		// Token: 0x0600B376 RID: 45942 RVA: 0x0027172D File Offset: 0x0026F92D
		public GridDragDropEventArgs(IList<GridDataItem> draggedItems, string htmlElement)
		{
			this._htmlElement = string.Empty;
			base..ctor();
			this._htmlElement = htmlElement;
			this._draggedItems = draggedItems;
			this._dropPosition = GridItemDropPosition.Above;
		}

		// Token: 0x04002F3C RID: 12092
		private GridDataItem _destDataItem;

		// Token: 0x04002F3D RID: 12093
		private IList<GridDataItem> _draggedItems;

		// Token: 0x04002F3E RID: 12094
		private string _htmlElement;

		// Token: 0x04002F3F RID: 12095
		private RadGrid _destinationGrid;

		// Token: 0x04002F40 RID: 12096
		private GridItemDropPosition _dropPosition;

		// Token: 0x04002F41 RID: 12097
		private readonly GridTableView _destinationItemTableView;
	}
}
