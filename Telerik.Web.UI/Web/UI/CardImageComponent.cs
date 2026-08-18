using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200002B RID: 43
	[ToolboxData("<{0}:CardImageComponent runat=\"server\"></{0}:CardImageComponent>")]
	[TelerikToolboxCategory("Layout")]
	[ToolboxItem(true)]
	public class CardImageComponent : CardComponentBase
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00005764 File Offset: 0x00003964
		public override string DefaultCssClass
		{
			get
			{
				return "k-card-image";
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000576B File Offset: 0x0000396B
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00005797 File Offset: 0x00003997
		[DefaultValue(HtmlTextWriterTag.Img)]
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.ViewState["TagKey"] == null)
				{
					return HtmlTextWriterTag.Img;
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
