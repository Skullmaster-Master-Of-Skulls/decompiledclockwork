using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004C2 RID: 1218
	public class SendMailErrorEventArgs : EventArgs
	{
		// Token: 0x06003CAB RID: 15531 RVA: 0x000C483A File Offset: 0x000C2A3A
		public SendMailErrorEventArgs(Exception e)
		{
			this._exception = e;
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06003CAC RID: 15532 RVA: 0x000C4849 File Offset: 0x000C2A49
		// (set) Token: 0x06003CAD RID: 15533 RVA: 0x000C4851 File Offset: 0x000C2A51
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

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06003CAE RID: 15534 RVA: 0x000C485A File Offset: 0x000C2A5A
		// (set) Token: 0x06003CAF RID: 15535 RVA: 0x000C4862 File Offset: 0x000C2A62
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

		// Token: 0x04002392 RID: 9106
		private Exception _exception;

		// Token: 0x04002393 RID: 9107
		private bool _handled;
	}
}
