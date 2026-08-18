using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000726 RID: 1830
	internal sealed class WebPartEditorCancelVerb : WebPartActionVerb
	{
		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x060058A1 RID: 22689 RVA: 0x00163FE4 File Offset: 0x00162FE4
		// (set) Token: 0x060058A2 RID: 22690 RVA: 0x00164016 File Offset: 0x00163016
		[WebSysDefaultValue("WebPartEditorCancelVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartEditorCancelVerb_Description");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x060058A3 RID: 22691 RVA: 0x0016402C File Offset: 0x0016302C
		// (set) Token: 0x060058A4 RID: 22692 RVA: 0x0016405E File Offset: 0x0016305E
		[WebSysDefaultValue("WebPartEditorCancelVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartEditorCancelVerb_Text");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
