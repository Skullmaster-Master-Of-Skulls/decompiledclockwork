using System;
using System.Drawing;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000439 RID: 1081
	internal sealed class HyperLinkStyle : Style
	{
		// Token: 0x0600346E RID: 13422 RVA: 0x000AAC49 File Offset: 0x000A8E49
		public HyperLinkStyle(Style owner)
		{
			this._owner = owner;
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000AAC58 File Offset: 0x000A8E58
		// (set) Token: 0x06003470 RID: 13424 RVA: 0x000AAC60 File Offset: 0x000A8E60
		public bool DoNotRenderDefaults
		{
			get
			{
				return this._doNotRenderDefaults;
			}
			set
			{
				this._doNotRenderDefaults = value;
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000AAC6C File Offset: 0x000A8E6C
		public sealed override bool IsEmpty
		{
			get
			{
				return base.RegisteredCssClass.Length == 0 && (!this._owner.IsSet(2) && !this._owner.IsSet(4) && !this._owner.IsSet(512) && !this._owner.IsSet(1024) && !this._owner.IsSet(2048) && !this._owner.IsSet(4096) && !this._owner.IsSet(8192) && !this._owner.IsSet(16384)) && !this._owner.IsSet(32768);
			}
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x000AAD30 File Offset: 0x000A8F30
		public sealed override void AddAttributesToRender(HtmlTextWriter writer, WebControl owner)
		{
			string text = string.Empty;
			bool flag = true;
			if (this._owner.IsSet(2))
			{
				text = this._owner.CssClass;
			}
			if (base.RegisteredCssClass.Length != 0)
			{
				flag = false;
				if (text.Length != 0)
				{
					text = text + " " + base.RegisteredCssClass;
				}
				else
				{
					text = base.RegisteredCssClass;
				}
			}
			if (text.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			}
			if (flag)
			{
				CssStyleCollection styleAttributes = base.GetStyleAttributes(owner);
				styleAttributes.Render(writer);
			}
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000AADB8 File Offset: 0x000A8FB8
		protected sealed override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
		{
			StateBag viewState = base.ViewState;
			if (this._owner.IsSet(4))
			{
				Color foreColor = this._owner.ForeColor;
				if (!foreColor.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(foreColor));
				}
			}
			FontInfo font = this._owner.Font;
			string[] names = font.Names;
			if (names.Length != 0)
			{
				attributes.Add(HtmlTextWriterStyle.FontFamily, string.Join(",", names));
			}
			FontUnit size = font.Size;
			if (!size.IsEmpty)
			{
				attributes.Add(HtmlTextWriterStyle.FontSize, size.ToString(CultureInfo.InvariantCulture));
			}
			if (this._owner.IsSet(2048))
			{
				if (font.Bold)
				{
					attributes.Add(HtmlTextWriterStyle.FontWeight, "bold");
				}
				else
				{
					attributes.Add(HtmlTextWriterStyle.FontWeight, "normal");
				}
			}
			if (this._owner.IsSet(4096))
			{
				if (font.Italic)
				{
					attributes.Add(HtmlTextWriterStyle.FontStyle, "italic");
				}
				else
				{
					attributes.Add(HtmlTextWriterStyle.FontStyle, "normal");
				}
			}
			string text = string.Empty;
			if (font.Underline)
			{
				text = "underline";
			}
			if (font.Overline)
			{
				text += " overline";
			}
			if (font.Strikeout)
			{
				text += " line-through";
			}
			if (text.Length > 0)
			{
				attributes.Add(HtmlTextWriterStyle.TextDecoration, text);
			}
			else if (!this.DoNotRenderDefaults)
			{
				attributes.Add(HtmlTextWriterStyle.TextDecoration, "none");
			}
			if (this._owner.IsSet(2))
			{
				attributes.Add(HtmlTextWriterStyle.BorderStyle, "none");
			}
		}

		// Token: 0x0400219D RID: 8605
		private bool _doNotRenderDefaults;

		// Token: 0x0400219E RID: 8606
		private Style _owner;
	}
}
