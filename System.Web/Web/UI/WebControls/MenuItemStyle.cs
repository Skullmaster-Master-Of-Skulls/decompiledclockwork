using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005EA RID: 1514
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class MenuItemStyle : Style
	{
		// Token: 0x06004AF3 RID: 19187 RVA: 0x001320E2 File Offset: 0x001310E2
		public MenuItemStyle()
		{
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x001320EA File Offset: 0x001310EA
		public MenuItemStyle(StateBag bag) : base(bag)
		{
		}

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06004AF5 RID: 19189 RVA: 0x001320F3 File Offset: 0x001310F3
		// (set) Token: 0x06004AF6 RID: 19190 RVA: 0x00132120 File Offset: 0x00131120
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("MenuItemStyle_HorizontalPadding")]
		[WebCategory("Layout")]
		[NotifyParentProperty(true)]
		public Unit HorizontalPadding
		{
			get
			{
				if (base.IsSet(131072))
				{
					return (Unit)base.ViewState["HorizontalPadding"];
				}
				return Unit.Empty;
			}
			set
			{
				if (value.Type == UnitType.Percentage || value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["HorizontalPadding"] = value;
				this.SetBit(131072);
			}
		}

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06004AF7 RID: 19191 RVA: 0x00132175 File Offset: 0x00131175
		internal HyperLinkStyle HyperLinkStyle
		{
			get
			{
				if (this._hyperLinkStyle == null)
				{
					this._hyperLinkStyle = new HyperLinkStyle(this);
				}
				return this._hyperLinkStyle;
			}
		}

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06004AF8 RID: 19192 RVA: 0x00132191 File Offset: 0x00131191
		// (set) Token: 0x06004AF9 RID: 19193 RVA: 0x001321BC File Offset: 0x001311BC
		[WebCategory("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("MenuItemStyle_ItemSpacing")]
		[NotifyParentProperty(true)]
		public Unit ItemSpacing
		{
			get
			{
				if (base.IsSet(262144))
				{
					return (Unit)base.ViewState["ItemSpacing"];
				}
				return Unit.Empty;
			}
			set
			{
				if (value.Type == UnitType.Percentage || value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["ItemSpacing"] = value;
				this.SetBit(262144);
			}
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06004AFA RID: 19194 RVA: 0x00132211 File Offset: 0x00131211
		// (set) Token: 0x06004AFB RID: 19195 RVA: 0x0013223C File Offset: 0x0013123C
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("MenuItemStyle_VerticalPadding")]
		[WebCategory("Layout")]
		[NotifyParentProperty(true)]
		public Unit VerticalPadding
		{
			get
			{
				if (base.IsSet(65536))
				{
					return (Unit)base.ViewState["VerticalPadding"];
				}
				return Unit.Empty;
			}
			set
			{
				if (value.Type == UnitType.Percentage || value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["VerticalPadding"] = value;
				this.SetBit(65536);
			}
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x00132294 File Offset: 0x00131294
		public override void CopyFrom(Style s)
		{
			if (s != null)
			{
				base.CopyFrom(s);
				MenuItemStyle menuItemStyle = s as MenuItemStyle;
				if (menuItemStyle != null && !menuItemStyle.IsEmpty)
				{
					if (s.RegisteredCssClass.Length != 0)
					{
						if (menuItemStyle.IsSet(65536))
						{
							base.ViewState.Remove("VerticalPadding");
							base.ClearBit(65536);
						}
						if (menuItemStyle.IsSet(131072))
						{
							base.ViewState.Remove("HorizontalPadding");
							base.ClearBit(131072);
						}
					}
					else
					{
						if (menuItemStyle.IsSet(65536))
						{
							this.VerticalPadding = menuItemStyle.VerticalPadding;
						}
						if (menuItemStyle.IsSet(131072))
						{
							this.HorizontalPadding = menuItemStyle.HorizontalPadding;
						}
					}
					if (menuItemStyle.IsSet(262144))
					{
						this.ItemSpacing = menuItemStyle.ItemSpacing;
					}
				}
			}
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x00132370 File Offset: 0x00131370
		protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
		{
			StateBag viewState = base.ViewState;
			if (base.IsSet(8))
			{
				Color c = (Color)viewState["BackColor"];
				if (!c.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(c));
				}
			}
			if (base.IsSet(16))
			{
				Color c = (Color)viewState["BorderColor"];
				if (!c.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.BorderColor, ColorTranslator.ToHtml(c));
				}
			}
			BorderStyle borderStyle = base.BorderStyle;
			Unit borderWidth = base.BorderWidth;
			if (!borderWidth.IsEmpty)
			{
				attributes.Add(HtmlTextWriterStyle.BorderWidth, borderWidth.ToString(CultureInfo.InvariantCulture));
				if (borderStyle == BorderStyle.NotSet)
				{
					if (borderWidth.Value != 0.0)
					{
						attributes.Add(HtmlTextWriterStyle.BorderStyle, "solid");
					}
				}
				else
				{
					attributes.Add(HtmlTextWriterStyle.BorderStyle, Style.borderStyles[(int)borderStyle]);
				}
			}
			else if (borderStyle != BorderStyle.NotSet)
			{
				attributes.Add(HtmlTextWriterStyle.BorderStyle, Style.borderStyles[(int)borderStyle]);
			}
			if (base.IsSet(128))
			{
				Unit unit = (Unit)viewState["Height"];
				if (!unit.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.Height, unit.ToString(CultureInfo.InvariantCulture));
				}
			}
			if (base.IsSet(256))
			{
				Unit unit = (Unit)viewState["Width"];
				if (!unit.IsEmpty)
				{
					attributes.Add(HtmlTextWriterStyle.Width, unit.ToString(CultureInfo.InvariantCulture));
				}
			}
			if (!this.HorizontalPadding.IsEmpty || !this.VerticalPadding.IsEmpty)
			{
				Unit unit2 = this.VerticalPadding.IsEmpty ? Unit.Pixel(0) : this.VerticalPadding;
				Unit unit3 = this.HorizontalPadding.IsEmpty ? Unit.Pixel(0) : this.HorizontalPadding;
				attributes.Add(HtmlTextWriterStyle.Padding, string.Format(CultureInfo.InvariantCulture, "{0} {1} {0} {1}", new object[]
				{
					unit2.ToString(CultureInfo.InvariantCulture),
					unit3.ToString(CultureInfo.InvariantCulture)
				}));
			}
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x00132574 File Offset: 0x00131574
		public override void MergeWith(Style s)
		{
			if (s != null)
			{
				if (this.IsEmpty)
				{
					this.CopyFrom(s);
					return;
				}
				base.MergeWith(s);
				MenuItemStyle menuItemStyle = s as MenuItemStyle;
				if (menuItemStyle != null && !menuItemStyle.IsEmpty)
				{
					if (s.RegisteredCssClass.Length == 0)
					{
						if (menuItemStyle.IsSet(65536) && !base.IsSet(65536))
						{
							this.VerticalPadding = menuItemStyle.VerticalPadding;
						}
						if (menuItemStyle.IsSet(131072) && !base.IsSet(131072))
						{
							this.HorizontalPadding = menuItemStyle.HorizontalPadding;
						}
					}
					if (menuItemStyle.IsSet(262144) && !base.IsSet(262144))
					{
						this.ItemSpacing = menuItemStyle.ItemSpacing;
					}
				}
			}
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x00132634 File Offset: 0x00131634
		public override void Reset()
		{
			if (base.IsSet(65536))
			{
				base.ViewState.Remove("VerticalPadding");
			}
			if (base.IsSet(131072))
			{
				base.ViewState.Remove("HorizontalPadding");
			}
			if (base.IsSet(262144))
			{
				base.ViewState.Remove("ItemSpacing");
			}
			this.ResetCachedStyles();
			base.Reset();
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x001326A4 File Offset: 0x001316A4
		internal void ResetCachedStyles()
		{
			this._hyperLinkStyle = null;
		}

		// Token: 0x04002B93 RID: 11155
		private const int PROP_VPADDING = 65536;

		// Token: 0x04002B94 RID: 11156
		private const int PROP_HPADDING = 131072;

		// Token: 0x04002B95 RID: 11157
		private const int PROP_ITEMSPACING = 262144;

		// Token: 0x04002B96 RID: 11158
		private HyperLinkStyle _hyperLinkStyle;
	}
}
