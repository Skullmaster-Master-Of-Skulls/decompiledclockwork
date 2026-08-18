using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005A8 RID: 1448
	internal sealed class WebPartMinimizeVerb : WebPartActionVerb
	{
		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x06004955 RID: 18773 RVA: 0x000F3EF1 File Offset: 0x000F20F1
		private string DefaultDescription
		{
			get
			{
				if (this._defaultDescription == null)
				{
					this._defaultDescription = SR.GetString("WebPartMinimizeVerb_Description");
				}
				return this._defaultDescription;
			}
		}

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x000F3F11 File Offset: 0x000F2111
		private string DefaultText
		{
			get
			{
				if (this._defaultText == null)
				{
					this._defaultText = SR.GetString("WebPartMinimizeVerb_Text");
				}
				return this._defaultText;
			}
		}

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x06004957 RID: 18775 RVA: 0x000F3F34 File Offset: 0x000F2134
		// (set) Token: 0x06004958 RID: 18776 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartMinimizeVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.DefaultDescription;
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x06004959 RID: 18777 RVA: 0x000F3F64 File Offset: 0x000F2164
		// (set) Token: 0x0600495A RID: 18778 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartMinimizeVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.DefaultText;
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x0400279B RID: 10139
		private string _defaultDescription;

		// Token: 0x0400279C RID: 10140
		private string _defaultText;
	}
}
