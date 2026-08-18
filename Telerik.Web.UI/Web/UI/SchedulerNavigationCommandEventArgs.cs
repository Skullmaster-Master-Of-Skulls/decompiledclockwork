using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020012D6 RID: 4822
	public class SchedulerNavigationCommandEventArgs : CancelEventArgs
	{
		// Token: 0x0600CA72 RID: 51826 RVA: 0x002D2B88 File Offset: 0x002D0D88
		public SchedulerNavigationCommandEventArgs(SchedulerNavigationCommand command)
		{
			this._command = command;
			this._selectedDate = DateTime.MinValue;
		}

		// Token: 0x0600CA73 RID: 51827 RVA: 0x002D2BA2 File Offset: 0x002D0DA2
		public SchedulerNavigationCommandEventArgs(SchedulerNavigationCommand command, DateTime selectedDate)
		{
			this._command = command;
			this._selectedDate = selectedDate;
		}

		// Token: 0x17004175 RID: 16757
		// (get) Token: 0x0600CA74 RID: 51828 RVA: 0x002D2BB8 File Offset: 0x002D0DB8
		public SchedulerNavigationCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x17004176 RID: 16758
		// (get) Token: 0x0600CA75 RID: 51829 RVA: 0x002D2BC0 File Offset: 0x002D0DC0
		public DateTime SelectedDate
		{
			get
			{
				return this._selectedDate;
			}
		}

		// Token: 0x0400352A RID: 13610
		private readonly SchedulerNavigationCommand _command;

		// Token: 0x0400352B RID: 13611
		private readonly DateTime _selectedDate;
	}
}
