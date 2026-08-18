using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000355 RID: 853
	[DefaultEvent("ServerClick")]
	[SupportsEventValidation]
	public class HtmlInputSubmit : HtmlInputButton, IPostBackEventHandler
	{
		// Token: 0x06002741 RID: 10049 RVA: 0x0007FD19 File Offset: 0x0007DF19
		public HtmlInputSubmit() : base("submit")
		{
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x0007FCDC File Offset: 0x0007DEDC
		public HtmlInputSubmit(string type) : base(type)
		{
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x0007FD26 File Offset: 0x0007DF26
		internal override void RenderAttributesInternal(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				Util.WriteOnClickAttribute(writer, this, true, false, this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0, this.ValidationGroup);
			}
		}
	}
}
