using System;
using System.ComponentModel.Design.Data;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200011D RID: 285
	internal sealed class SqlDataSourceOrderClause
	{
		// Token: 0x06000A6A RID: 2666 RVA: 0x00042C43 File Offset: 0x00040E43
		public SqlDataSourceOrderClause(DesignerDataConnection designerDataConnection, DesignerDataTableBase designerDataTable, DesignerDataColumn designerDataColumn, bool isDescending)
		{
			this._designerDataConnection = designerDataConnection;
			this._designerDataTable = designerDataTable;
			this._designerDataColumn = designerDataColumn;
			this._isDescending = isDescending;
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00042C68 File Offset: 0x00040E68
		public DesignerDataColumn DesignerDataColumn
		{
			get
			{
				return this._designerDataColumn;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00042C70 File Offset: 0x00040E70
		public bool IsDescending
		{
			get
			{
				return this._isDescending;
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00042C78 File Offset: 0x00040E78
		public override string ToString()
		{
			SqlDataSourceColumnData sqlDataSourceColumnData = new SqlDataSourceColumnData(this._designerDataConnection, this._designerDataColumn);
			if (this._isDescending)
			{
				return sqlDataSourceColumnData.EscapedName + " DESC";
			}
			return sqlDataSourceColumnData.EscapedName;
		}

		// Token: 0x04000646 RID: 1606
		private DesignerDataColumn _designerDataColumn;

		// Token: 0x04000647 RID: 1607
		private DesignerDataTableBase _designerDataTable;

		// Token: 0x04000648 RID: 1608
		private DesignerDataConnection _designerDataConnection;

		// Token: 0x04000649 RID: 1609
		private bool _isDescending;
	}
}
