using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200002C RID: 44
	[TelerikToolboxCategory("Layout")]
	[ToolboxItem(true)]
	[ToolboxData("<{0}:CardActionComponent runat=\"server\"></{0}:CardActionComponent>")]
	public class CardActionComponent : CardComponentBase
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000057B7 File Offset: 0x000039B7
		public override string DefaultCssClass
		{
			get
			{
				return "k-card-action";
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000057BE File Offset: 0x000039BE
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x000057EA File Offset: 0x000039EA
		[DefaultValue(HtmlTextWriterTag.Span)]
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.ViewState["TagKey"] == null)
				{
					return HtmlTextWriterTag.Span;
				}
				return (HtmlTextWriterTag)this.ViewState["TagKey"];
			}
			set
			{
				this.ViewState["TagKey"] = value;
			}
		}
	}
}
