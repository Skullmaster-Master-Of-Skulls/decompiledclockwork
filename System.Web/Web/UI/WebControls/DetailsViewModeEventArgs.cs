using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000563 RID: 1379
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DetailsViewModeEventArgs : CancelEventArgs
	{
		// Token: 0x06004424 RID: 17444 RVA: 0x001198DA File Offset: 0x001188DA
		public DetailsViewModeEventArgs(DetailsViewMode mode, bool cancelingEdit) : base(false)
		{
			this._mode = mode;
			this._cancelingEdit = cancelingEdit;
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06004425 RID: 17445 RVA: 0x001198F1 File Offset: 0x001188F1
		public bool CancelingEdit
		{
			get
			{
				return this._cancelingEdit;
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06004426 RID: 17446 RVA: 0x001198F9 File Offset: 0x001188F9
		// (set) Token: 0x06004427 RID: 17447 RVA: 0x00119901 File Offset: 0x00118901
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

		// Token: 0x040029A0 RID: 10656
		private DetailsViewMode _mode;

		// Token: 0x040029A1 RID: 10657
		private bool _cancelingEdit;
	}
}
