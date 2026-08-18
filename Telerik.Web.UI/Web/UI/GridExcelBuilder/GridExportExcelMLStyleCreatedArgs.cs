using System;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B1F RID: 6943
	public class GridExportExcelMLStyleCreatedArgs : EventArgs
	{
		// Token: 0x06010CBE RID: 68798 RVA: 0x003BA561 File Offset: 0x003B8761
		public GridExportExcelMLStyleCreatedArgs(IStylesCollection styles)
		{
			this._styles = styles;
		}

		// Token: 0x170051D4 RID: 20948
		// (get) Token: 0x06010CBF RID: 68799 RVA: 0x003BA57B File Offset: 0x003B877B
		public IStylesCollection Styles
		{
			get
			{
				return this._styles;
			}
		}

		// Token: 0x04004B23 RID: 19235
		private IStylesCollection _styles = new StylesCollection();
	}
}
