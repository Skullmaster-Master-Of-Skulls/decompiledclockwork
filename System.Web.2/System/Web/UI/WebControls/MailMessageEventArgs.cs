using System;
using System.Net.Mail;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000469 RID: 1129
	public class MailMessageEventArgs : LoginCancelEventArgs
	{
		// Token: 0x060036E8 RID: 14056 RVA: 0x000B1AD8 File Offset: 0x000AFCD8
		public MailMessageEventArgs(MailMessage message)
		{
			this._message = message;
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x060036E9 RID: 14057 RVA: 0x000B1AE7 File Offset: 0x000AFCE7
		public MailMessage Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x0400222A RID: 8746
		private MailMessage _message;
	}
}
