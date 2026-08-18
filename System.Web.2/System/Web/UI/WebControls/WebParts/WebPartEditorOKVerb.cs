using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200059B RID: 1435
	internal sealed class WebPartEditorOKVerb : WebPartActionVerb
	{
		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x06004833 RID: 18483 RVA: 0x000ECFF8 File Offset: 0x000EB1F8
		// (set) Token: 0x06004834 RID: 18484 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartEditorOKVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartEditorOKVerb_Description");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x06004835 RID: 18485 RVA: 0x000ED02C File Offset: 0x000EB22C
		// (set) Token: 0x06004836 RID: 18486 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartEditorOKVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartEditorOKVerb_Text");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
