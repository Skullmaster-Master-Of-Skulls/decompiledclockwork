using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200057F RID: 1407
	internal sealed class WebPartCatalogAddVerb : WebPartActionVerb
	{
		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x06004750 RID: 18256 RVA: 0x000EA858 File Offset: 0x000E8A58
		// (set) Token: 0x06004751 RID: 18257 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartCatalogAddVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartCatalogAddVerb_Description");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x06004752 RID: 18258 RVA: 0x000EA8A0 File Offset: 0x000E8AA0
		// (set) Token: 0x06004753 RID: 18259 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartCatalogAddVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartCatalogAddVerb_Text");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
