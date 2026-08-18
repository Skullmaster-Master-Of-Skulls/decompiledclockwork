using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000599 RID: 1433
	internal sealed class WebPartEditorApplyVerb : WebPartActionVerb
	{
		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06004829 RID: 18473 RVA: 0x000ECF28 File Offset: 0x000EB128
		// (set) Token: 0x0600482A RID: 18474 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartEditorApplyVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartEditorApplyVerb_Description");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x0600482B RID: 18475 RVA: 0x000ECF5C File Offset: 0x000EB15C
		// (set) Token: 0x0600482C RID: 18476 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartEditorApplyVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartEditorApplyVerb_Text");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
