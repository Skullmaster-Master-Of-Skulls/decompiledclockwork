using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200058F RID: 1423
	internal sealed class WebPartConnectVerb : WebPartActionVerb
	{
		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x060047E6 RID: 18406 RVA: 0x000EC8E4 File Offset: 0x000EAAE4
		private string DefaultDescription
		{
			get
			{
				if (this._defaultDescription == null)
				{
					this._defaultDescription = SR.GetString("WebPartConnectVerb_Description");
				}
				return this._defaultDescription;
			}
		}

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x060047E7 RID: 18407 RVA: 0x000EC904 File Offset: 0x000EAB04
		private string DefaultText
		{
			get
			{
				if (this._defaultText == null)
				{
					this._defaultText = SR.GetString("WebPartConnectVerb_Text");
				}
				return this._defaultText;
			}
		}

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x060047E8 RID: 18408 RVA: 0x000EC924 File Offset: 0x000EAB24
		// (set) Token: 0x060047E9 RID: 18409 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartConnectVerb_Description")]
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

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x060047EA RID: 18410 RVA: 0x000EC954 File Offset: 0x000EAB54
		// (set) Token: 0x060047EB RID: 18411 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartConnectVerb_Text")]
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

		// Token: 0x04002714 RID: 10004
		private string _defaultDescription;

		// Token: 0x04002715 RID: 10005
		private string _defaultText;
	}
}
