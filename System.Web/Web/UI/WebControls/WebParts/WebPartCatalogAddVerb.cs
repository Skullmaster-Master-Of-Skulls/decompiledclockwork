using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200070B RID: 1803
	internal sealed class WebPartCatalogAddVerb : WebPartActionVerb
	{
		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x060057C3 RID: 22467 RVA: 0x0016150C File Offset: 0x0016050C
		// (set) Token: 0x060057C4 RID: 22468 RVA: 0x0016153E File Offset: 0x0016053E
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

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x060057C5 RID: 22469 RVA: 0x00161554 File Offset: 0x00160554
		// (set) Token: 0x060057C6 RID: 22470 RVA: 0x00161586 File Offset: 0x00160586
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
