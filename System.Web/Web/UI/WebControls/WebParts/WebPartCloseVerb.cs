using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200070E RID: 1806
	internal sealed class WebPartCloseVerb : WebPartActionVerb
	{
		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x060057E9 RID: 22505 RVA: 0x0016287D File Offset: 0x0016187D
		private string DefaultDescription
		{
			get
			{
				if (this._defaultDescription == null)
				{
					this._defaultDescription = SR.GetString("WebPartCloseVerb_Description");
				}
				return this._defaultDescription;
			}
		}

		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x060057EA RID: 22506 RVA: 0x0016289D File Offset: 0x0016189D
		private string DefaultText
		{
			get
			{
				if (this._defaultText == null)
				{
					this._defaultText = SR.GetString("WebPartCloseVerb_Text");
				}
				return this._defaultText;
			}
		}

		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x060057EB RID: 22507 RVA: 0x001628C0 File Offset: 0x001618C0
		// (set) Token: 0x060057EC RID: 22508 RVA: 0x001628EE File Offset: 0x001618EE
		[WebSysDefaultValue("WebPartCloseVerb_Description")]
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

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x060057ED RID: 22509 RVA: 0x00162904 File Offset: 0x00161904
		// (set) Token: 0x060057EE RID: 22510 RVA: 0x00162932 File Offset: 0x00161932
		[WebSysDefaultValue("WebPartCloseVerb_Text")]
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

		// Token: 0x04002FC3 RID: 12227
		private string _defaultDescription;

		// Token: 0x04002FC4 RID: 12228
		private string _defaultText;
	}
}
