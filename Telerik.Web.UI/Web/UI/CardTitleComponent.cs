using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200002D RID: 45
	[TelerikToolboxCategory("Layout")]
	[ToolboxItem(true)]
	[ToolboxData("<{0}:CardTitleComponent runat=\"server\"></{0}:CardTitleComponent>")]
	public class CardTitleComponent : CardComponentBase
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000580A File Offset: 0x00003A0A
		public override string DefaultCssClass
		{
			get
			{
				return "k-card-title";
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00005811 File Offset: 0x00003A11
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000583D File Offset: 0x00003A3D
		[DefaultValue(HtmlTextWriterTag.H5)]
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.ViewState["TagKey"] == null)
				{
					return HtmlTextWriterTag.H5;
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
