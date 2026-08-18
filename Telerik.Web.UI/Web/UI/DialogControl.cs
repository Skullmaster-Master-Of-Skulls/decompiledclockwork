using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200102F RID: 4143
	public abstract class DialogControl : RadWebControl
	{
		// Token: 0x17003382 RID: 13186
		// (get) Token: 0x0600A337 RID: 41783 RVA: 0x0024535C File Offset: 0x0024355C
		protected DialogParameters DialogParameters
		{
			get
			{
				if (this._dialogParameters == null)
				{
					this._dialogParameters = DialogHandlerNoSession.GetDialogParameters(this);
				}
				return this._dialogParameters;
			}
		}

		// Token: 0x17003383 RID: 13187
		// (get) Token: 0x0600A338 RID: 41784 RVA: 0x00245378 File Offset: 0x00243578
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17003384 RID: 13188
		// (get) Token: 0x0600A339 RID: 41785 RVA: 0x0024537C File Offset: 0x0024357C
		internal override bool ShouldRegisterCssReferences
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04002D5E RID: 11614
		private DialogParameters _dialogParameters;
	}
}
