using System;

namespace Telerik.Web.UI
{
	// Token: 0x020010CF RID: 4303
	public class GridEditManager
	{
		// Token: 0x0600AFB5 RID: 44981 RVA: 0x00261343 File Offset: 0x0025F543
		public GridEditManager(GridEditableItem editItem)
		{
			this._editItem = editItem;
		}

		// Token: 0x0600AFB6 RID: 44982 RVA: 0x00261354 File Offset: 0x0025F554
		public IGridColumnEditor GetColumnEditor(IGridEditableColumn column)
		{
			GridEditableColumn column2 = column.Column;
			IGridColumnEditor currentColumnEditor = column2.CurrentColumnEditor;
			if (currentColumnEditor == null)
			{
				throw new GridColumnEditorException("Editor not present for column: " + column2.UniqueName);
			}
			this._editItem.InitializeEditorInCell(column);
			if (!currentColumnEditor.IsInitialized && this._editItem.OwnerTableView.ItemTemplate == null)
			{
				throw new GridColumnEditorException("Editor cannot be initialized for column: " + column2.UniqueName);
			}
			return currentColumnEditor;
		}

		// Token: 0x0600AFB7 RID: 44983 RVA: 0x002613C5 File Offset: 0x0025F5C5
		public IGridColumnEditor GetColumnEditor(string columnUniqueName)
		{
			return this.GetColumnEditor(this._editItem.OwnerTableView.GetColumn(columnUniqueName) as IGridEditableColumn);
		}

		// Token: 0x04002E52 RID: 11858
		private GridEditableItem _editItem;
	}
}
