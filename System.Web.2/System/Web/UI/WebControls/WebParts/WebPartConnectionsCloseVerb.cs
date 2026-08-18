using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000589 RID: 1417
	internal sealed class WebPartConnectionsCloseVerb : WebPartActionVerb
	{
		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x060047C7 RID: 18375 RVA: 0x000EC6E4 File Offset: 0x000EA8E4
		// (set) Token: 0x060047C8 RID: 18376 RVA: 0x000EA88A File Offset: 0x000E8A8A
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

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x060047C9 RID: 18377 RVA: 0x000EC718 File Offset: 0x000EA918
		// (set) Token: 0x060047CA RID: 18378 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
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
