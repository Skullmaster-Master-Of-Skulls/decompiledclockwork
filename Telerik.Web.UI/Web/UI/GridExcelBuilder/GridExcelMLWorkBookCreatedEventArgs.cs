using System;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B24 RID: 6948
	public class GridExcelMLWorkBookCreatedEventArgs : EventArgs
	{
		// Token: 0x170051D8 RID: 20952
		// (get) Token: 0x06010CCC RID: 68812 RVA: 0x003BA5CA File Offset: 0x003B87CA
		// (set) Token: 0x06010CCD RID: 68813 RVA: 0x003BA5D2 File Offset: 0x003B87D2
		public WorkBook WorkBook { get; internal set; }

		// Token: 0x06010CCE RID: 68814 RVA: 0x003BA5DB File Offset: 0x003B87DB
		public GridExcelMLWorkBookCreatedEventArgs(WorkBook workBook)
		{
			this.WorkBook = workBook;
		}
	}
}
