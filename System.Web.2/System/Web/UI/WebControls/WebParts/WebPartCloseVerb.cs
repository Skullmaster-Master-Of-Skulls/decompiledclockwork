using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000582 RID: 1410
	internal sealed class WebPartCloseVerb : WebPartActionVerb
	{
		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x06004776 RID: 18294 RVA: 0x000EBB91 File Offset: 0x000E9D91
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

		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x06004777 RID: 18295 RVA: 0x000EBBB1 File Offset: 0x000E9DB1
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

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x06004778 RID: 18296 RVA: 0x000EBBD4 File Offset: 0x000E9DD4
		// (set) Token: 0x06004779 RID: 18297 RVA: 0x000EA88A File Offset: 0x000E8A8A
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

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x0600477A RID: 18298 RVA: 0x000EBC04 File Offset: 0x000E9E04
		// (set) Token: 0x0600477B RID: 18299 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
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

		// Token: 0x040026FA RID: 9978
		private string _defaultDescription;

		// Token: 0x040026FB RID: 9979
		private string _defaultText;
	}
}
