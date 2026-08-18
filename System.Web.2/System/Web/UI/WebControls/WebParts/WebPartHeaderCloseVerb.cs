using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005A1 RID: 1441
	internal sealed class WebPartHeaderCloseVerb : WebPartActionVerb
	{
		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x0600484C RID: 18508 RVA: 0x000ED1B8 File Offset: 0x000EB3B8
		// (set) Token: 0x0600484D RID: 18509 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartHeaderCloseVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartHeaderCloseVerb_Description");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x0600484E RID: 18510 RVA: 0x000ED1EC File Offset: 0x000EB3EC
		// (set) Token: 0x0600484F RID: 18511 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartHeaderCloseVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartHeaderCloseVerb_Text");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
