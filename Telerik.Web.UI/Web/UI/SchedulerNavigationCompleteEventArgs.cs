using System;

namespace Telerik.Web.UI
{
	// Token: 0x020012D7 RID: 4823
	public class SchedulerNavigationCompleteEventArgs : EventArgs
	{
		// Token: 0x0600CA76 RID: 51830 RVA: 0x002D2BC8 File Offset: 0x002D0DC8
		public SchedulerNavigationCompleteEventArgs(SchedulerNavigationCommand command)
		{
			this._command = command;
		}

		// Token: 0x17004177 RID: 16759
		// (get) Token: 0x0600CA77 RID: 51831 RVA: 0x002D2BD7 File Offset: 0x002D0DD7
		public SchedulerNavigationCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x0400352C RID: 13612
		private readonly SchedulerNavigationCommand _command;
	}
}
