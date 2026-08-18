using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000580 RID: 1408
	internal sealed class WebPartCatalogCloseVerb : WebPartActionVerb
	{
		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x06004755 RID: 18261 RVA: 0x000EA8F0 File Offset: 0x000E8AF0
		// (set) Token: 0x06004756 RID: 18262 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartCatalogCloseVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartCatalogCloseVerb_Description");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x06004757 RID: 18263 RVA: 0x000EA924 File Offset: 0x000E8B24
		// (set) Token: 0x06004758 RID: 18264 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartCatalogCloseVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartCatalogCloseVerb_Text");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
