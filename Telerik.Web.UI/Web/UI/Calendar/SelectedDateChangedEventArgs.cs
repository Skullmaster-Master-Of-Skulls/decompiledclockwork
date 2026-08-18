using System;

namespace Telerik.Web.UI.Calendar
{
	// Token: 0x02001000 RID: 4096
	public class SelectedDateChangedEventArgs : EventArgs
	{
		// Token: 0x0600A016 RID: 40982 RVA: 0x0023A381 File Offset: 0x00238581
		public SelectedDateChangedEventArgs(DateTime? oldDate, DateTime? newDate)
		{
			this._oldDate = oldDate;
			this._newDate = newDate;
		}

		// Token: 0x17003296 RID: 12950
		// (get) Token: 0x0600A017 RID: 40983 RVA: 0x0023A397 File Offset: 0x00238597
		public DateTime? OldDate
		{
			get
			{
				return this._oldDate;
			}
		}

		// Token: 0x17003297 RID: 12951
		// (get) Token: 0x0600A018 RID: 40984 RVA: 0x0023A39F File Offset: 0x0023859F
		public DateTime? NewDate
		{
			get
			{
				return this._newDate;
			}
		}

		// Token: 0x04002CD6 RID: 11478
		private DateTime? _oldDate;

		// Token: 0x04002CD7 RID: 11479
		private DateTime? _newDate;
	}
}
