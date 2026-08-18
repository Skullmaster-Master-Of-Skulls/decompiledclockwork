using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020000D4 RID: 212
	public class ToggleButtonStateChangedEventArgs : CommandEventArgs
	{
		// Token: 0x0600080B RID: 2059 RVA: 0x0001E3E2 File Offset: 0x0001C5E2
		public ToggleButtonStateChangedEventArgs(string commandName, object commandArgument, int selectedToggleStateIndex, ButtonToggleState selectedToggleState) : base(commandName, commandArgument)
		{
			this._selectedToggleStateIndex = selectedToggleStateIndex;
			this._selectedToggleState = selectedToggleState;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001E3FB File Offset: 0x0001C5FB
		public ToggleButtonStateChangedEventArgs(ToggleButtonStateChangedEventArgs e) : this(e.CommandName, e.CommandArgument, e.SelectedToggleStateIndex, e.SelectedToggleState)
		{
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0001E41B File Offset: 0x0001C61B
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x0001E423 File Offset: 0x0001C623
		public int SelectedToggleStateIndex
		{
			get
			{
				return this._selectedToggleStateIndex;
			}
			set
			{
				this._selectedToggleStateIndex = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0001E42C File Offset: 0x0001C62C
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x0001E434 File Offset: 0x0001C634
		public ButtonToggleState SelectedToggleState
		{
			get
			{
				return this._selectedToggleState;
			}
			set
			{
				this._selectedToggleState = value;
			}
		}

		// Token: 0x040001EA RID: 490
		private int _selectedToggleStateIndex;

		// Token: 0x040001EB RID: 491
		private ButtonToggleState _selectedToggleState;
	}
}
