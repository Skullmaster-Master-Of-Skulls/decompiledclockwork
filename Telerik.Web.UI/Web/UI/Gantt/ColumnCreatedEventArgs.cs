using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000335 RID: 821
	public class ColumnCreatedEventArgs : EventArgs, IColumnEvent
	{
		// Token: 0x06001C3A RID: 7226 RVA: 0x0005A328 File Offset: 0x00058528
		public ColumnCreatedEventArgs(IBoundColumn column)
		{
			this._column = column;
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x0005A337 File Offset: 0x00058537
		public IBoundColumn Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0005A33F File Offset: 0x0005853F
		public DataType ColumnType
		{
			get
			{
				return this.Column.DataType;
			}
		}

		// Token: 0x04000734 RID: 1844
		private readonly IBoundColumn _column;
	}
}
