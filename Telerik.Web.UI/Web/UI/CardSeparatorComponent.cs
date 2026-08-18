using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000027 RID: 39
	[ToolboxData("<{0}:CardSeparatorComponent runat=\"server\"></{0}:CardSeparatorComponent>")]
	[ToolboxItem(true)]
	[TelerikToolboxCategory("Layout")]
	public class CardSeparatorComponent : CardComponentBase
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000055E4 File Offset: 0x000037E4
		public override string DefaultCssClass
		{
			get
			{
				string text = "k-card-separator";
				switch (this.Orientation)
				{
				case CardComponentOrientation.Horizontal:
					text += " k-separator-horizontal";
					break;
				case CardComponentOrientation.Vertical:
					text += " k-separator-vertical";
					break;
				}
				return text;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000562D File Offset: 0x0000382D
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00005658 File Offset: 0x00003858
		[DefaultValue(CardComponentOrientation.Default)]
		public virtual CardComponentOrientation Orientation
		{
			get
			{
				if (this.ViewState["Orientation"] == null)
				{
					return CardComponentOrientation.Default;
				}
				return (CardComponentOrientation)this.ViewState["Orientation"];
			}
			set
			{
				this.ViewState["Orientation"] = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00005670 File Offset: 0x00003870
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000569C File Offset: 0x0000389C
		[DefaultValue(HtmlTextWriterTag.Hr)]
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.ViewState["TagKey"] == null)
				{
					return HtmlTextWriterTag.Hr;
				}
				return (HtmlTextWriterTag)this.ViewState["TagKey"];
			}
			set
			{
				this.ViewState["TagKey"] = value;
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000056B4 File Offset: 0x000038B4
		public override bool ShouldRenderAttribute(string key)
		{
			List<string> list = new List<string>
			{
				"orientation"
			};
			return !list.Contains(key.ToLower()) && base.ShouldRenderAttribute(key);
		}
	}
}
