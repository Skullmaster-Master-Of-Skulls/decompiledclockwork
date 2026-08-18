using System;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x020000CC RID: 204
	internal class ColumnTypeManager
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x0003E28C File Offset: 0x0003D28C
		public static ColumnTypeManager getInstance()
		{
			return ColumnTypeManager.instance;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0003E2A3 File Offset: 0x0003D2A3
		private ColumnTypeManager()
		{
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0003E2B0 File Offset: 0x0003D2B0
		public int getColumnID(ColumnDefinition colDef)
		{
			return (int)ColumnTypeDefUtil.enumOf(colDef.ColumnType.GetType());
		}

		// Token: 0x040005F2 RID: 1522
		private static ColumnTypeManager instance = new ColumnTypeManager();
	}
}
