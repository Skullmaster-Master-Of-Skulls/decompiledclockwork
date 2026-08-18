using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000A29 RID: 2601
	public class ButtonToggleStateChangedEventArgs : CommandEventArgs
	{
		// Token: 0x0600629E RID: 25246 RVA: 0x00173811 File Offset: 0x00171A11
		public ButtonToggleStateChangedEventArgs(string commandName, object commandArgument, int selectedToggleStateIndex, RadButtonToggleState selectedToggleState) : base(commandName, commandArgument)
		{
			this._selectedToggleStateIndex = selectedToggleStateIndex;
			this._selectedToggleState = selectedToggleState;
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x0017382A File Offset: 0x00171A2A
		public ButtonToggleStateChangedEventArgs(ButtonToggleStateChangedEventArgs e) : this(e.CommandName, e.CommandArgument, e.SelectedToggleStateIndex, e.SelectedToggleState)
		{
		}

		// Token: 0x17002057 RID: 8279
		// (get) Token: 0x060062A0 RID: 25248 RVA: 0x0017384A File Offset: 0x00171A4A
		// (set) Token: 0x060062A1 RID: 25249 RVA: 0x00173852 File Offset: 0x00171A52
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

		// Token: 0x17002058 RID: 8280
		// (get) Token: 0x060062A2 RID: 25250 RVA: 0x0017385B File Offset: 0x00171A5B
		// (set) Token: 0x060062A3 RID: 25251 RVA: 0x00173863 File Offset: 0x00171A63
		public RadButtonToggleState SelectedToggleState
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

		// Token: 0x0400180E RID: 6158
		private int _selectedToggleStateIndex;

		// Token: 0x0400180F RID: 6159
		private RadButtonToggleState _selectedToggleState;
	}
}
