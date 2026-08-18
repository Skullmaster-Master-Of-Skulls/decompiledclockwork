using System;

namespace System.Data.Design
{
	// Token: 0x0200021B RID: 539
	internal class DataAccessor : DataSourceComponent
	{
		// Token: 0x060013FD RID: 5117 RVA: 0x00070D3C File Offset: 0x0006EF3C
		public DataAccessor(DesignTable designTable)
		{
			if (designTable == null)
			{
				throw new ArgumentNullException("DesignTable");
			}
			this.designTable = designTable;
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x00070D59 File Offset: 0x0006EF59
		internal DesignTable DesignTable
		{
			get
			{
				return this.designTable;
			}
		}

		// Token: 0x04000AAF RID: 2735
		private DesignTable designTable;

		// Token: 0x04000AB0 RID: 2736
		internal const string DEFAULT_BASE_CLASS = "System.ComponentModel.Component";

		// Token: 0x04000AB1 RID: 2737
		internal const string DEFAULT_NAME_POSTFIX = "TableAdapter";
	}
}
