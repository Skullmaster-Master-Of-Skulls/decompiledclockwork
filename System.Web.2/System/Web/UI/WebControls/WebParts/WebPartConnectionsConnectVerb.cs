using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200058B RID: 1419
	internal sealed class WebPartConnectionsConnectVerb : WebPartActionVerb
	{
		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x060047D1 RID: 18385 RVA: 0x000EC7B4 File Offset: 0x000EA9B4
		// (set) Token: 0x060047D2 RID: 18386 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartConnectionsConnectVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartConnectionsConnectVerb_Description");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x060047D3 RID: 18387 RVA: 0x000EC7E8 File Offset: 0x000EA9E8
		// (set) Token: 0x060047D4 RID: 18388 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartConnectionsConnectVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartConnectionsConnectVerb_Text");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
