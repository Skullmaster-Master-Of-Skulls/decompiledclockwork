using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200002E RID: 46
	[PersistChildren(true)]
	[EmbeddedSkin("Card", typeof(RadCard))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadCard))]
	[ParseChildren(false)]
	[TelerikToolboxCategory("Layout")]
	[ToolboxBitmap(typeof(RadCard), "Telerik.Web.UI.Card.png")]
	[ToolboxData("<{0}:RadCard runat=\"server\"></{0}:RadCard>")]
	[LightweightRendering]
	[EmbeddedSkin("Card", "Default", typeof(RadCard))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Classic, typeof(RadCard))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Lightweight, typeof(RadCard))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Mobile, typeof(RadCard))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadCard))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadCard))]
	public class RadCard : RadWebControl
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00005865 File Offset: 0x00003A65
		public override void RenderClientStateField(HtmlTextWriter writer)
		{
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00005867 File Offset: 0x00003A67
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000586B File Offset: 0x00003A6B
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00005896 File Offset: 0x00003A96
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

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000058AE File Offset: 0x00003AAE
		// (set) Token: 0x060001FD RID: 509 RVA: 0x000058D9 File Offset: 0x00003AD9
		[DefaultValue(CardStateType.Default)]
		public virtual CardStateType CardState
		{
			get
			{
				if (this.ViewState["CardState"] == null)
				{
					return CardStateType.Default;
				}
				return (CardStateType)this.ViewState["CardState"];
			}
			set
			{
				this.ViewState["CardState"] = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000058F4 File Offset: 0x00003AF4
		protected override string CssClassFormatString
		{
			get
			{
				string text = "RadCard RadCard_{0} k-card";
				switch (this.Orientation)
				{
				case CardComponentOrientation.Horizontal:
					text += " k-card-horizontal";
					break;
				case CardComponentOrientation.Vertical:
					text += " k-card-vertical";
					break;
				}
				if (this.CardState != CardStateType.Default)
				{
					text = text + " k-state-" + this.CardState.ToString().ToLower();
				}
				if (base.Attributes["dir"] == "rtl")
				{
					text += " k-rtl";
				}
				return text;
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000598E File Offset: 0x00003B8E
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00005997 File Offset: 0x00003B97
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000059A0 File Offset: 0x00003BA0
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			return new List<ScriptDescriptor>();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000059A7 File Offset: 0x00003BA7
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			return new List<ScriptReference>();
		}
	}
}
