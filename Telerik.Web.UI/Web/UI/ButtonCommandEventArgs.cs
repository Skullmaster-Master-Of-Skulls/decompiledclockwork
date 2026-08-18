using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000A1F RID: 2591
	public class ButtonCommandEventArgs : CommandEventArgs
	{
		// Token: 0x17002013 RID: 8211
		// (get) Token: 0x060061FD RID: 25085 RVA: 0x00171ED3 File Offset: 0x001700D3
		// (set) Token: 0x060061FE RID: 25086 RVA: 0x00171EDB File Offset: 0x001700DB
		public bool IsSplitButtonClick
		{
			get
			{
				return this._isSplitButtonClick;
			}
			set
			{
				this._isSplitButtonClick = value;
			}
		}

		// Token: 0x060061FF RID: 25087 RVA: 0x00171EE4 File Offset: 0x001700E4
		public ButtonCommandEventArgs(string commandName, object commandArgument, bool isSplitButtonClick) : base(commandName, commandArgument)
		{
			this._isSplitButtonClick = isSplitButtonClick;
		}

		// Token: 0x06006200 RID: 25088 RVA: 0x00171EF5 File Offset: 0x001700F5
		public ButtonCommandEventArgs(ButtonCommandEventArgs e) : this(e.CommandName, e.CommandArgument, e.IsSplitButtonClick)
		{
		}

		// Token: 0x04001809 RID: 6153
		private bool _isSplitButtonClick;
	}
}
