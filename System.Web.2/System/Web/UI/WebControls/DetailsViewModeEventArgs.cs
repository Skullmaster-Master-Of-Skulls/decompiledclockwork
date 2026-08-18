using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003E2 RID: 994
	public class DetailsViewModeEventArgs : CancelEventArgs
	{
		// Token: 0x0600304E RID: 12366 RVA: 0x0009E57B File Offset: 0x0009C77B
		public DetailsViewModeEventArgs(DetailsViewMode mode, bool cancelingEdit) : base(false)
		{
			this._mode = mode;
			this._cancelingEdit = cancelingEdit;
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x0009E592 File Offset: 0x0009C792
		public bool CancelingEdit
		{
			get
			{
				return this._cancelingEdit;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x0009E59A File Offset: 0x0009C79A
		// (set) Token: 0x06003051 RID: 12369 RVA: 0x0009E5A2 File Offset: 0x0009C7A2
		public DetailsViewMode NewMode
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

		// Token: 0x04002083 RID: 8323
		private DetailsViewMode _mode;

		// Token: 0x04002084 RID: 8324
		private bool _cancelingEdit;
	}
}
