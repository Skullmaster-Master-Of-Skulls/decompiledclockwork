using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200040A RID: 1034
	public class FormViewModeEventArgs : CancelEventArgs
	{
		// Token: 0x06003228 RID: 12840 RVA: 0x000A38AB File Offset: 0x000A1AAB
		public FormViewModeEventArgs(FormViewMode mode, bool cancelingEdit) : base(false)
		{
			this._mode = mode;
			this._cancelingEdit = cancelingEdit;
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x000A38C2 File Offset: 0x000A1AC2
		public bool CancelingEdit
		{
			get
			{
				return this._cancelingEdit;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x000A38CA File Offset: 0x000A1ACA
		// (set) Token: 0x0600322B RID: 12843 RVA: 0x000A38D2 File Offset: 0x000A1AD2
		public FormViewMode NewMode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
			}
		}

		// Token: 0x04002105 RID: 8453
		private FormViewMode _mode;

		// Token: 0x04002106 RID: 8454
		private bool _cancelingEdit;
	}
}
