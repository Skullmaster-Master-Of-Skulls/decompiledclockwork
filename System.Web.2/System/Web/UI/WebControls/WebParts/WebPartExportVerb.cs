using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005A0 RID: 1440
	internal sealed class WebPartExportVerb : WebPartActionVerb
	{
		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x06004845 RID: 18501 RVA: 0x000ED115 File Offset: 0x000EB315
		private string DefaultDescription
		{
			get
			{
				if (this._defaultDescription == null)
				{
					this._defaultDescription = SR.GetString("WebPartExportVerb_Description");
				}
				return this._defaultDescription;
			}
		}

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x06004846 RID: 18502 RVA: 0x000ED135 File Offset: 0x000EB335
		private string DefaultText
		{
			get
			{
				if (this._defaultText == null)
				{
					this._defaultText = SR.GetString("WebPartExportVerb_Text");
				}
				return this._defaultText;
			}
		}

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x06004847 RID: 18503 RVA: 0x000ED158 File Offset: 0x000EB358
		// (set) Token: 0x06004848 RID: 18504 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartExportVerb_Description")]
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

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x06004849 RID: 18505 RVA: 0x000ED188 File Offset: 0x000EB388
		// (set) Token: 0x0600484A RID: 18506 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartExportVerb_Text")]
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

		// Token: 0x0400272A RID: 10026
		private string _defaultDescription;

		// Token: 0x0400272B RID: 10027
		private string _defaultText;
	}
}
