using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000FAD RID: 4013
	public class DockCommandEventArgs : EventArgs
	{
		// Token: 0x06009A0E RID: 39438 RVA: 0x00225B54 File Offset: 0x00223D54
		internal DockCommandEventArgs(DockCommand command)
		{
			this._command = command;
		}

		// Token: 0x170030BD RID: 12477
		// (get) Token: 0x06009A0F RID: 39439 RVA: 0x00225B63 File Offset: 0x00223D63
		public DockCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x04002BB9 RID: 11193
		private readonly DockCommand _command;
	}
}
