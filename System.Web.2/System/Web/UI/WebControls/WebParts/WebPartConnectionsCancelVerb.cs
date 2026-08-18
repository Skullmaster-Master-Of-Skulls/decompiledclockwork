using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000588 RID: 1416
	internal sealed class WebPartConnectionsCancelVerb : WebPartActionVerb
	{
		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x060047C2 RID: 18370 RVA: 0x000EC67C File Offset: 0x000EA87C
		// (set) Token: 0x060047C3 RID: 18371 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartConnectionsCancelVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartConnectionsCancelVerb_Description");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x060047C4 RID: 18372 RVA: 0x000EC6B0 File Offset: 0x000EA8B0
		// (set) Token: 0x060047C5 RID: 18373 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartConnectionsCancelVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartConnectionsCancelVerb_Text");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
