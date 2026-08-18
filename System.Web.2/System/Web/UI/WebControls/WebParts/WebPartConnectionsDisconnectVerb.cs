using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200058C RID: 1420
	internal sealed class WebPartConnectionsDisconnectVerb : WebPartActionVerb
	{
		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x060047D6 RID: 18390 RVA: 0x000EC81C File Offset: 0x000EAA1C
		// (set) Token: 0x060047D7 RID: 18391 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartConnectionsDisconnectVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartConnectionsDisconnectVerb_Description");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x060047D8 RID: 18392 RVA: 0x000EC850 File Offset: 0x000EAA50
		// (set) Token: 0x060047D9 RID: 18393 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartConnectionsDisconnectVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartConnectionsDisconnectVerb_Text");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
