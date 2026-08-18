using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000A1D RID: 2589
	public class ButtonClickEventArgs : EventArgs
	{
		// Token: 0x17002012 RID: 8210
		// (get) Token: 0x060061F9 RID: 25081 RVA: 0x00171EA5 File Offset: 0x001700A5
		// (set) Token: 0x060061FA RID: 25082 RVA: 0x00171EAD File Offset: 0x001700AD
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

		// Token: 0x060061FB RID: 25083 RVA: 0x00171EB6 File Offset: 0x001700B6
		public ButtonClickEventArgs(bool isSplitButtonClick)
		{
			this._isSplitButtonClick = isSplitButtonClick;
		}

		// Token: 0x060061FC RID: 25084 RVA: 0x00171EC5 File Offset: 0x001700C5
		public ButtonClickEventArgs(ButtonClickEventArgs e) : this(e.IsSplitButtonClick)
		{
		}

		// Token: 0x04001805 RID: 6149
		private bool _isSplitButtonClick;
	}
}
