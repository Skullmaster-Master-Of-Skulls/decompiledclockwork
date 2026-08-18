using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000590 RID: 1424
	internal sealed class WebPartDeleteVerb : WebPartActionVerb
	{
		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x060047ED RID: 18413 RVA: 0x000EC982 File Offset: 0x000EAB82
		private string DefaultDescription
		{
			get
			{
				if (this._defaultDescription == null)
				{
					this._defaultDescription = SR.GetString("WebPartDeleteVerb_Description");
				}
				return this._defaultDescription;
			}
		}

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x060047EE RID: 18414 RVA: 0x000EC9A2 File Offset: 0x000EABA2
		private string DefaultText
		{
			get
			{
				if (this._defaultText == null)
				{
					this._defaultText = SR.GetString("WebPartDeleteVerb_Text");
				}
				return this._defaultText;
			}
		}

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x060047EF RID: 18415 RVA: 0x000EC9C4 File Offset: 0x000EABC4
		// (set) Token: 0x060047F0 RID: 18416 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartDeleteVerb_Description")]
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

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x060047F1 RID: 18417 RVA: 0x000EC9F4 File Offset: 0x000EABF4
		// (set) Token: 0x060047F2 RID: 18418 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartDeleteVerb_Text")]
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

		// Token: 0x04002716 RID: 10006
		private string _defaultDescription;

		// Token: 0x04002717 RID: 10007
		private string _defaultText;
	}
}
