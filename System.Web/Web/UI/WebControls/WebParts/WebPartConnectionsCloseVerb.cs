using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000715 RID: 1813
	internal sealed class WebPartConnectionsCloseVerb : WebPartActionVerb
	{
		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x0600583A RID: 22586 RVA: 0x00163528 File Offset: 0x00162528
		// (set) Token: 0x0600583B RID: 22587 RVA: 0x0016355A File Offset: 0x0016255A
		[WebSysDefaultValue("WebPartConnectionsCloseVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartConnectionsCloseVerb_Description");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x0600583C RID: 22588 RVA: 0x00163570 File Offset: 0x00162570
		// (set) Token: 0x0600583D RID: 22589 RVA: 0x001635A2 File Offset: 0x001625A2
		[WebSysDefaultValue("WebPartConnectionsCloseVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartConnectionsCloseVerb_Text");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
