using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200045F RID: 1119
	public class LoginCancelEventArgs : EventArgs
	{
		// Token: 0x06003677 RID: 13943 RVA: 0x000B05D9 File Offset: 0x000AE7D9
		public LoginCancelEventArgs() : this(false)
		{
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x000B05E2 File Offset: 0x000AE7E2
		public LoginCancelEventArgs(bool cancel)
		{
			this._cancel = cancel;
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x000B05F1 File Offset: 0x000AE7F1
		// (set) Token: 0x0600367A RID: 13946 RVA: 0x000B05F9 File Offset: 0x000AE7F9
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = value;
			}
		}

		// Token: 0x04002207 RID: 8711
		private bool _cancel;
	}
}
