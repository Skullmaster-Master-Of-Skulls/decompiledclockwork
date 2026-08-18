using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000334 RID: 820
	public class ColumnCreatingEventArgs : EventArgs, IColumnEvent
	{
		// Token: 0x06001C37 RID: 7223 RVA: 0x0005A304 File Offset: 0x00058504
		public ColumnCreatingEventArgs(IBoundColumn column)
		{
			this._column = column;
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0005A313 File Offset: 0x00058513
		public IBoundColumn Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x0005A31B File Offset: 0x0005851B
		public DataType ColumnType
		{
			get
			{
				return this.Column.DataType;
			}
		}

		// Token: 0x04000733 RID: 1843
		private readonly IBoundColumn _column;
	}
}
