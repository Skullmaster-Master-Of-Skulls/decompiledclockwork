using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000716 RID: 1814
	internal sealed class WebPartConnectionsConfigureVerb : WebPartActionVerb
	{
		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x0600583F RID: 22591 RVA: 0x001635C0 File Offset: 0x001625C0
		// (set) Token: 0x06005840 RID: 22592 RVA: 0x001635F2 File Offset: 0x001625F2
		[WebSysDefaultValue("WebPartConnectionsConfigureVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartConnectionsConfigureVerb_Description");
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x06005841 RID: 22593 RVA: 0x00163608 File Offset: 0x00162608
		// (set) Token: 0x06005842 RID: 22594 RVA: 0x0016363A File Offset: 0x0016263A
		[WebSysDefaultValue("WebPartConnectionsConfigureVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("WebPartConnectionsConfigureVerb_Text");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
