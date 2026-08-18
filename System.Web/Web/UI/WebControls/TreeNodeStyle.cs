using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000672 RID: 1650
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TreeNodeStyle : Style
	{
		// Token: 0x060050D9 RID: 20697 RVA: 0x00144BE5 File Offset: 0x00143BE5
		public TreeNodeStyle()
		{
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x00144BED File Offset: 0x00143BED
		public TreeNodeStyle(StateBag bag) : base(bag)
		{
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x060050DB RID: 20699 RVA: 0x00144BF6 File Offset: 0x00143BF6
		// (set) Token: 0x060050DC RID: 20700 RVA: 0x00144C20 File Offset: 0x00143C20
		[NotifyParentProperty(true)]
		[WebCategory("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("TreeNodeStyle_ChildNodesPadding")]
		public Unit ChildNodesPadding
		{
			get
			{
				if (base.IsSet(524288))
				{
					return (Unit)base.ViewState["ChildNodesPadding"];
				}
				return Unit.Empty;
			}
			set
			{
				if (value.Type == UnitType.Percentage || value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["ChildNodesPadding"] = value;
				this.SetBit(524288);
			}
		}

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x060050DD RID: 20701 RVA: 0x00144C75 File Offset: 0x00143C75
		// (set) Token: 0x060050DE RID: 20702 RVA: 0x00144CA0 File Offset: 0x00143CA0
		[DefaultValue(typeof(Unit), "")]
		[WebCategory("Layout")]
		[NotifyParentProperty(true)]
		[WebSysDescription("TreeNodeStyle_HorizontalPadding")]
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

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x060050DF RID: 20703 RVA: 0x00144CF5 File Offset: 0x00143CF5
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

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x060050E0 RID: 20704 RVA: 0x00144D11 File Offset: 0x00143D11
		// (set) Token: 0x060050E1 RID: 20705 RVA: 0x00144D3B File Offset: 0x00143D3B
		[WebSysDescription("TreeNodeStyle_ImageUrl")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[UrlProperty]
		[WebCategory("Appearance")]
		public string ImageUrl
		{
			get
			{
				if (base.IsSet(1048576))
				{
					return (string)base.ViewState["ImageUrl"];
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.ViewState["ImageUrl"] = value;
				this.SetBit(1048576);
			}
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x060050E2 RID: 20706 RVA: 0x00144D67 File Offset: 0x00143D67
		// (set) Token: 0x060050E3 RID: 20707 RVA: 0x00144D94 File Offset: 0x00143D94
		[WebSysDescription("TreeNodeStyle_NodeSpacing")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		[WebCategory("Layout")]
		public Unit NodeSpacing
		{
			get
			{
				if (base.IsSet(262144))
				{
					return (Unit)base.ViewState["NodeSpacing"];
				}
				return Unit.Empty;
			}
			set
			{
				if (value.Type == UnitType.Percentage || value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["NodeSpacing"] = value;
				this.SetBit(262144);
			}
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x060050E4 RID: 20708 RVA: 0x00144DE9 File Offset: 0x00143DE9
		// (set) Token: 0x060050E5 RID: 20709 RVA: 0x00144E14 File Offset: 0x00143E14
		[WebSysDescription("TreeNodeStyle_VerticalPadding")]
		[NotifyParentProperty(true)]
		[WebCategory("Layout")]
		[DefaultValue(typeof(Unit), "")]
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

		// Token: 0x060050E6 RID: 20710 RVA: 0x00144E6C File Offset: 0x00143E6C
		public override void CopyFrom(Style s)
		{
			if (s != null)
			{
				base.CopyFrom(s);
				TreeNodeStyle treeNodeStyle = s as TreeNodeStyle;
				if (treeNodeStyle != null && !treeNodeStyle.IsEmpty)
				{
					if (s.RegisteredCssClass.Length != 0)
					{
						if (treeNodeStyle.IsSet(65536))
						{
							base.ViewState.Remove("VerticalPadding");
							base.ClearBit(65536);
						}
						if (treeNodeStyle.IsSet(131072))
						{
							base.ViewState.Remove("HorizontalPadding");
							base.ClearBit(131072);
						}
					}
					else
					{
						if (treeNodeStyle.IsSet(65536))
						{
							this.VerticalPadding = treeNodeStyle.VerticalPadding;
						}
						if (treeNodeStyle.IsSet(131072))
						{
							this.HorizontalPadding = treeNodeStyle.HorizontalPadding;
						}
					}
					if (treeNodeStyle.IsSet(262144))
					{
						this.NodeSpacing = treeNodeStyle.NodeSpacing;
					}
					if (treeNodeStyle.IsSet(524288))
					{
						this.ChildNodesPadding = treeNodeStyle.ChildNodesPadding;
					}
					if (treeNodeStyle.IsSet(1048576))
					{
						this.ImageUrl = treeNodeStyle.ImageUrl;
					}
				}
			}
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x00144F7C File Offset: 0x00143F7C
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
				attributes.Add(HtmlTextWriterStyle.Padding, string.Format(CultureInfo.InvariantCulture, "{0} {1} {0} {1}", new object[]
				{
					this.VerticalPadding.IsEmpty ? Unit.Pixel(0) : this.VerticalPadding,
					this.HorizontalPadding.IsEmpty ? Unit.Pixel(0) : this.HorizontalPadding
				}));
			}
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x0014516C File Offset: 0x0014416C
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
				TreeNodeStyle treeNodeStyle = s as TreeNodeStyle;
				if (treeNodeStyle != null && !treeNodeStyle.IsEmpty)
				{
					if (s.RegisteredCssClass.Length == 0)
					{
						if (treeNodeStyle.IsSet(65536) && !base.IsSet(65536))
						{
							this.VerticalPadding = treeNodeStyle.VerticalPadding;
						}
						if (treeNodeStyle.IsSet(131072) && !base.IsSet(131072))
						{
							this.HorizontalPadding = treeNodeStyle.HorizontalPadding;
						}
					}
					if (treeNodeStyle.IsSet(262144) && !base.IsSet(262144))
					{
						this.NodeSpacing = treeNodeStyle.NodeSpacing;
					}
					if (treeNodeStyle.IsSet(524288) && !base.IsSet(524288))
					{
						this.ChildNodesPadding = treeNodeStyle.ChildNodesPadding;
					}
					if (treeNodeStyle.IsSet(1048576) && !base.IsSet(1048576))
					{
						this.ImageUrl = treeNodeStyle.ImageUrl;
					}
				}
			}
		}

		// Token: 0x060050E9 RID: 20713 RVA: 0x0014527C File Offset: 0x0014427C
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
				base.ViewState.Remove("NodeSpacing");
			}
			if (base.IsSet(524288))
			{
				base.ViewState.Remove("ChildNodesPadding");
			}
			this.ResetCachedStyles();
			base.Reset();
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x00145309 File Offset: 0x00144309
		internal void ResetCachedStyles()
		{
			this._hyperLinkStyle = null;
		}

		// Token: 0x04002D3D RID: 11581
		private const int PROP_VPADDING = 65536;

		// Token: 0x04002D3E RID: 11582
		private const int PROP_HPADDING = 131072;

		// Token: 0x04002D3F RID: 11583
		private const int PROP_NODESPACING = 262144;

		// Token: 0x04002D40 RID: 11584
		private const int PROP_CHILDNODESPADDING = 524288;

		// Token: 0x04002D41 RID: 11585
		private const int PROP_IMAGEURL = 1048576;

		// Token: 0x04002D42 RID: 11586
		private HyperLinkStyle _hyperLinkStyle;
	}
}
