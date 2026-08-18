using System;
using System.ComponentModel.Design.Data;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004E3 RID: 1251
	internal sealed class SqlDataSourceOrderClause
	{
		// Token: 0x06002CE8 RID: 11496 RVA: 0x000FE1B7 File Offset: 0x000FD1B7
		public SqlDataSourceOrderClause(DesignerDataConnection designerDataConnection, DesignerDataTableBase designerDataTable, DesignerDataColumn designerDataColumn, bool isDescending)
		{
			this._designerDataConnection = designerDataConnection;
			this._designerDataTable = designerDataTable;
			this._designerDataColumn = designerDataColumn;
			this._isDescending = isDescending;
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x000FE1DC File Offset: 0x000FD1DC
		public DesignerDataColumn DesignerDataColumn
		{
			get
			{
				return this._designerDataColumn;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002CEA RID: 11498 RVA: 0x000FE1E4 File Offset: 0x000FD1E4
		public bool IsDescending
		{
			get
			{
				return this._isDescending;
			}
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x000FE1EC File Offset: 0x000FD1EC
		public override string ToString()
		{
			SqlDataSourceColumnData sqlDataSourceColumnData = new SqlDataSourceColumnData(this._designerDataConnection, this._designerDataColumn);
			if (this._isDescending)
			{
				return sqlDataSourceColumnData.EscapedName + " DESC";
			}
			return sqlDataSourceColumnData.EscapedName;
		}

		// Token: 0x04001EA8 RID: 7848
		private DesignerDataColumn _designerDataColumn;

		// Token: 0x04001EA9 RID: 7849
		private DesignerDataTableBase _designerDataTable;

		// Token: 0x04001EAA RID: 7850
		private DesignerDataConnection _designerDataConnection;

		// Token: 0x04001EAB RID: 7851
		private bool _isDescending;
	}
}
