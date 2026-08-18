using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200059A RID: 1434
	internal sealed class WebPartEditorCancelVerb : WebPartActionVerb
	{
		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x0600482E RID: 18478 RVA: 0x000ECF90 File Offset: 0x000EB190
		// (set) Token: 0x0600482F RID: 18479 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartEditorCancelVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartEditorCancelVerb_Description");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x06004830 RID: 18480 RVA: 0x000ECFC4 File Offset: 0x000EB1C4
		// (set) Token: 0x06004831 RID: 18481 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartEditorCancelVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartEditorCancelVerb_Text");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
