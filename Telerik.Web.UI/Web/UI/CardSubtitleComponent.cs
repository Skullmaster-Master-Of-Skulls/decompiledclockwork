using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000029 RID: 41
	[ToolboxData("<{0}:CardSubtitleComponent runat=\"server\"></{0}:CardSubtitleComponent>")]
	[ToolboxItem(true)]
	[TelerikToolboxCategory("Layout")]
	public class CardSubtitleComponent : CardComponentBase
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00005702 File Offset: 0x00003902
		public override string DefaultCssClass
		{
			get
			{
				return "k-card-subtitle";
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00005709 File Offset: 0x00003909
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00005735 File Offset: 0x00003935
		[DefaultValue(HtmlTextWriterTag.H6)]
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.ViewState["TagKey"] == null)
				{
					return HtmlTextWriterTag.H6;
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
