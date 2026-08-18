using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200059C RID: 1436
	internal sealed class WebPartEditVerb : WebPartActionVerb
	{
		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x06004838 RID: 18488 RVA: 0x000ED05E File Offset: 0x000EB25E
		private string DefaultDescription
		{
			get
			{
				if (this._defaultDescription == null)
				{
					this._defaultDescription = SR.GetString("WebPartEditVerb_Description");
				}
				return this._defaultDescription;
			}
		}

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x06004839 RID: 18489 RVA: 0x000ED07E File Offset: 0x000EB27E
		private string DefaultText
		{
			get
			{
				if (this._defaultText == null)
				{
					this._defaultText = SR.GetString("WebPartEditVerb_Text");
				}
				return this._defaultText;
			}
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x0600483A RID: 18490 RVA: 0x000ED0A0 File Offset: 0x000EB2A0
		// (set) Token: 0x0600483B RID: 18491 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartEditVerb_Description")]
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

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x0600483C RID: 18492 RVA: 0x000ED0D0 File Offset: 0x000EB2D0
		// (set) Token: 0x0600483D RID: 18493 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartEditVerb_Text")]
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

		// Token: 0x04002723 RID: 10019
		private string _defaultDescription;

		// Token: 0x04002724 RID: 10020
		private string _defaultText;
	}
}
