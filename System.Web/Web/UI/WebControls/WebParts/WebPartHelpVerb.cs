using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200072F RID: 1839
	internal sealed class WebPartHelpVerb : WebPartActionVerb
	{
		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x060058C4 RID: 22724 RVA: 0x0016435D File Offset: 0x0016335D
		private string DefaultDescription
		{
			get
			{
				if (this._defaultDescription == null)
				{
					this._defaultDescription = SR.GetString("WebPartHelpVerb_Description");
				}
				return this._defaultDescription;
			}
		}

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x060058C5 RID: 22725 RVA: 0x0016437D File Offset: 0x0016337D
		private string DefaultText
		{
			get
			{
				if (this._defaultText == null)
				{
					this._defaultText = SR.GetString("WebPartHelpVerb_Text");
				}
				return this._defaultText;
			}
		}

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x060058C6 RID: 22726 RVA: 0x001643A0 File Offset: 0x001633A0
		// (set) Token: 0x060058C7 RID: 22727 RVA: 0x001643CE File Offset: 0x001633CE
		[WebSysDefaultValue("WebPartHelpVerb_Description")]
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

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x060058C8 RID: 22728 RVA: 0x001643E4 File Offset: 0x001633E4
		// (set) Token: 0x060058C9 RID: 22729 RVA: 0x00164412 File Offset: 0x00163412
		[WebSysDefaultValue("WebPartHelpVerb_Text")]
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

		// Token: 0x04002FF9 RID: 12281
		private string _defaultDescription;

		// Token: 0x04002FFA RID: 12282
		private string _defaultText;
	}
}
