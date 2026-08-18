using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Calendar
{
	// Token: 0x02001003 RID: 4099
	[Browsable(false)]
	public class TimePickerEventArgs : EventArgs
	{
		// Token: 0x17003299 RID: 12953
		// (get) Token: 0x0600A01F RID: 40991 RVA: 0x0023A3BE File Offset: 0x002385BE
		public DataListItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x0600A020 RID: 40992 RVA: 0x0023A3C6 File Offset: 0x002385C6
		public TimePickerEventArgs(DataListItem item)
		{
			this.item = item;
		}

		// Token: 0x04002CD9 RID: 11481
		private DataListItem item;
	}
}
