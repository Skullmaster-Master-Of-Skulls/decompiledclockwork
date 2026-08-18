using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005A3 RID: 1443
	internal sealed class WebPartHelpVerb : WebPartActionVerb
	{
		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x06004851 RID: 18513 RVA: 0x000ED21E File Offset: 0x000EB41E
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

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06004852 RID: 18514 RVA: 0x000ED23E File Offset: 0x000EB43E
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

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x06004853 RID: 18515 RVA: 0x000ED260 File Offset: 0x000EB460
		// (set) Token: 0x06004854 RID: 18516 RVA: 0x000EA88A File Offset: 0x000E8A8A
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

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06004855 RID: 18517 RVA: 0x000ED290 File Offset: 0x000EB490
		// (set) Token: 0x06004856 RID: 18518 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
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

		// Token: 0x04002730 RID: 10032
		private string _defaultDescription;

		// Token: 0x04002731 RID: 10033
		private string _defaultText;
	}
}
