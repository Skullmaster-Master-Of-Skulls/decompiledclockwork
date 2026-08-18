using System;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x02000096 RID: 150
	public class ResolveControlEventArgs : EventArgs
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x0000CDD1 File Offset: 0x0000AFD1
		public ResolveControlEventArgs(string controlId)
		{
			this._controlID = controlId;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		public string ControlID
		{
			get
			{
				return this._controlID;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x0000CDF0 File Offset: 0x0000AFF0
		public Control Control
		{
			get
			{
				return this._control;
			}
			set
			{
				this._control = value;
			}
		}

		// Token: 0x040002A6 RID: 678
		private string _controlID;

		// Token: 0x040002A7 RID: 679
		private Control _control;
	}
}
