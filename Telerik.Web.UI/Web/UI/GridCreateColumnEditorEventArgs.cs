using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200109F RID: 4255
	public class GridCreateColumnEditorEventArgs : EventArgs
	{
		// Token: 0x170037D6 RID: 14294
		// (get) Token: 0x0600ACD1 RID: 44241 RVA: 0x002521B6 File Offset: 0x002503B6
		// (set) Token: 0x0600ACD2 RID: 44242 RVA: 0x002521BE File Offset: 0x002503BE
		public IGridColumnEditor ColumnEditor
		{
			get
			{
				return this._columnEditor;
			}
			set
			{
				this._columnEditor = value;
			}
		}

		// Token: 0x170037D7 RID: 14295
		// (get) Token: 0x0600ACD3 RID: 44243 RVA: 0x002521C7 File Offset: 0x002503C7
		// (set) Token: 0x0600ACD4 RID: 44244 RVA: 0x002521CF File Offset: 0x002503CF
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

		// Token: 0x04002DCE RID: 11726
		private GridColumn _column;

		// Token: 0x04002DCF RID: 11727
		private IGridColumnEditor _columnEditor;
	}
}
