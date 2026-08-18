using System;

namespace System.Windows.Forms
{
	// Token: 0x0200018C RID: 396
	public sealed class GridTablesFactory
	{
		// Token: 0x06001840 RID: 6208 RVA: 0x00002843 File Offset: 0x00000A43
		private GridTablesFactory()
		{
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00056FF2 File Offset: 0x000551F2
		public static DataGridTableStyle[] CreateGridTables(DataGridTableStyle gridTable, object dataSource, string dataMember, BindingContext bindingManager)
		{
			return new DataGridTableStyle[]
			{
				gridTable
			};
		}
	}
}
