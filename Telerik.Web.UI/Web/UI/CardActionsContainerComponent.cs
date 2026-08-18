using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000021 RID: 33
	[TelerikToolboxCategory("Layout")]
	[ToolboxData("<{0}:CardActionsContainerComponent runat=\"server\"></{0}:CardActionsContainerComponent>")]
	[ToolboxItem(true)]
	public class CardActionsContainerComponent : CardComponentBase
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00005454 File Offset: 0x00003654
		public override string DefaultCssClass
		{
			get
			{
				string text = "k-card-actions ";
				switch (this.CardActionsAlignment)
				{
				case CardActionsAlignmentType.Stretched:
					text += "k-card-actions-stretched";
					break;
				case CardActionsAlignmentType.Centered:
					text += "k-card-actions-center";
					break;
				case CardActionsAlignmentType.Start:
					text += "k-card-actions-start";
					break;
				case CardActionsAlignmentType.End:
					text += "k-card-actions-end";
					break;
				}
				switch (this.Orientation)
				{
				case CardComponentOrientation.Horizontal:
					text += " k-card-actions-horizontal";
					break;
				case CardComponentOrientation.Vertical:
					text += " k-card-actions-vertical";
					break;
				}
				return text;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000054F6 File Offset: 0x000036F6
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00005521 File Offset: 0x00003721
		[DefaultValue(CardActionsAlignmentType.None)]
		public virtual CardActionsAlignmentType CardActionsAlignment
		{
			get
			{
				if (this.ViewState["CardActionsAlignment"] == null)
				{
					return CardActionsAlignmentType.None;
				}
				return (CardActionsAlignmentType)this.ViewState["CardActionsAlignment"];
			}
			set
			{
				this.ViewState["CardActionsAlignment"] = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00005539 File Offset: 0x00003739
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00005564 File Offset: 0x00003764
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

		// Token: 0x060001D6 RID: 470 RVA: 0x0000557C File Offset: 0x0000377C
		public override bool ShouldRenderAttribute(string key)
		{
			List<string> list = new List<string>
			{
				"orientation",
				"cardactionsalignment"
			};
			return !list.Contains(key.ToLower()) && base.ShouldRenderAttribute(key);
		}
	}
}
