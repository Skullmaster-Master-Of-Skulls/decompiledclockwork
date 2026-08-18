using System;
using Telerik.Web.UI.Calendar.Collections;

namespace Telerik.Web.UI.Calendar
{
	// Token: 0x02001002 RID: 4098
	public sealed class SelectedDatesEventArgs : EventArgs
	{
		// Token: 0x0600A01D RID: 40989 RVA: 0x0023A3A7 File Offset: 0x002385A7
		public SelectedDatesEventArgs(DateTimeCollection selectedDates)
		{
			this._SelectedDates = selectedDates;
		}

		// Token: 0x17003298 RID: 12952
		// (get) Token: 0x0600A01E RID: 40990 RVA: 0x0023A3B6 File Offset: 0x002385B6
		public DateTimeCollection SelectedDates
		{
			get
			{
				return this._SelectedDates;
			}
		}

		// Token: 0x04002CD8 RID: 11480
		private DateTimeCollection _SelectedDates;
	}
}
