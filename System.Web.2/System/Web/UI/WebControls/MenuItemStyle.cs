using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000473 RID: 1139
	public sealed class MenuItemStyle : Style
	{
		// Token: 0x06003846 RID: 14406 RVA: 0x000B75ED File Offset: 0x000B57ED
		public MenuItemStyle()
		{
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x000B75F5 File Offset: 0x000B57F5
		public MenuItemStyle(StateBag bag) : base(bag)
		{
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x000B75FE File Offset: 0x000B57FE
		// (set) Token: 0x06003849 RID: 14409 RVA: 0x000B7628 File Offset: 0x000B5828
		[DefaultValue(typeof(Unit), "")]
		[WebCategory("Layout")]
		[NotifyParentProperty(true)]
		[WebSysDescription("MenuItemStyle_HorizontalPadding")]
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

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x0600384A RID: 14410 RVA: 0x000B767D File Offset: 0x000B587D
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

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x0600384B RID: 14411 RVA: 0x000B7699 File Offset: 0x000B5899
		// (set) Token: 0x0600384C RID: 14412 RVA: 0x000B76C4 File Offset: 0x000B58C4
		[DefaultValue(typeof(Unit), "")]
		[WebCategory("Layout")]
		[NotifyParentProperty(true)]
		[WebSysDescription("MenuItemStyle_ItemSpacing")]
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

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000B7719 File Offset: 0x000B5919
		// (set) Token: 0x0600384E RID: 14414 RVA: 0x000B7744 File Offset: 0x000B5944
		[DefaultValue(typeof(Unit), "")]
		[WebCategory("Layout")]
		[NotifyParentProperty(true)]
		[WebSysDescription("MenuItemStyle_VerticalPadding")]
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

		// Token: 0x0600384F RID: 14415 RVA: 0x000B779C File Offset: 0x000B599C
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

		// Token: 0x06003850 RID: 14416 RVA: 0x000B7878 File Offset: 0x000B5A78
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

		// Token: 0x06003851 RID: 14417 RVA: 0x000B7A74 File Offset: 0x000B5C74
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

		// Token: 0x06003852 RID: 14418 RVA: 0x000B7B34 File Offset: 0x000B5D34
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

		// Token: 0x06003853 RID: 14419 RVA: 0x000B7BA4 File Offset: 0x000B5DA4
		internal void ResetCachedStyles()
		{
			this._hyperLinkStyle = null;
		}

		// Token: 0x0400227B RID: 8827
		private const int PROP_VPADDING = 65536;

		// Token: 0x0400227C RID: 8828
		private const int PROP_HPADDING = 131072;

		// Token: 0x0400227D RID: 8829
		private const int PROP_ITEMSPACING = 262144;

		// Token: 0x0400227E RID: 8830
		private HyperLinkStyle _hyperLinkStyle;
	}
}
