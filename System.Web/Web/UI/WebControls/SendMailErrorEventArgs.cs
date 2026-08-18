using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000639 RID: 1593
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SendMailErrorEventArgs : EventArgs
	{
		// Token: 0x06004EB2 RID: 20146 RVA: 0x0013E1D3 File Offset: 0x0013D1D3
		public SendMailErrorEventArgs(Exception e)
		{
			this._exception = e;
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x06004EB3 RID: 20147 RVA: 0x0013E1E2 File Offset: 0x0013D1E2
		// (set) Token: 0x06004EB4 RID: 20148 RVA: 0x0013E1EA File Offset: 0x0013D1EA
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
			set
			{
				this._exception = value;
			}
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x06004EB5 RID: 20149 RVA: 0x0013E1F3 File Offset: 0x0013D1F3
		// (set) Token: 0x06004EB6 RID: 20150 RVA: 0x0013E1FB File Offset: 0x0013D1FB
		public bool Handled
		{
			get
			{
				return this._handled;
			}
			set
			{
				this._handled = value;
			}
		}

		// Token: 0x04002CAE RID: 11438
		private Exception _exception;

		// Token: 0x04002CAF RID: 11439
		private bool _handled;
	}
}
