using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200058A RID: 1418
	internal sealed class WebPartConnectionsConfigureVerb : WebPartActionVerb
	{
		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x060047CC RID: 18380 RVA: 0x000EC74C File Offset: 0x000EA94C
		// (set) Token: 0x060047CD RID: 18381 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartConnectionsConfigureVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartConnectionsConfigureVerb_Description");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x060047CE RID: 18382 RVA: 0x000EC780 File Offset: 0x000EA980
		// (set) Token: 0x060047CF RID: 18383 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartConnectionsConfigureVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartConnectionsConfigureVerb_Text");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
