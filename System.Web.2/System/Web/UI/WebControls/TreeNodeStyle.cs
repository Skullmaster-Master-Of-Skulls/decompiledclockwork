using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000501 RID: 1281
	public sealed class TreeNodeStyle : Style
	{
		// Token: 0x0600401F RID: 16415 RVA: 0x000B75ED File Offset: 0x000B57ED
		public TreeNodeStyle()
		{
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x000B75F5 File Offset: 0x000B57F5
		public TreeNodeStyle(StateBag bag) : base(bag)
		{
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06004021 RID: 16417 RVA: 0x000CF1A4 File Offset: 0x000CD3A4
		// (set) Token: 0x06004022 RID: 16418 RVA: 0x000CF1D0 File Offset: 0x000CD3D0
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("TreeNodeStyle_ChildNodesPadding")]
		[WebCategory("Layout")]
		[NotifyParentProperty(true)]
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

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06004023 RID: 16419 RVA: 0x000B75FE File Offset: 0x000B57FE
		// (set) Token: 0x06004024 RID: 16420 RVA: 0x000CF228 File Offset: 0x000CD428
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("TreeNodeStyle_HorizontalPadding")]
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

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06004025 RID: 16421 RVA: 0x000CF27D File Offset: 0x000CD47D
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

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x000CF299 File Offset: 0x000CD499
		// (set) Token: 0x06004027 RID: 16423 RVA: 0x000CF2C3 File Offset: 0x000CD4C3
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		[UrlProperty]
		[WebCategory("Appearance")]
		[WebSysDescription("TreeNodeStyle_ImageUrl")]
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

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x000CF2EF File Offset: 0x000CD4EF
		// (set) Token: 0x06004029 RID: 16425 RVA: 0x000CF31C File Offset: 0x000CD51C
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("TreeNodeStyle_NodeSpacing")]
		[WebCategory("Layout")]
		[NotifyParentProperty(true)]
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

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x0600402A RID: 16426 RVA: 0x000B7719 File Offset: 0x000B5919
		// (set) Token: 0x0600402B RID: 16427 RVA: 0x000CF374 File Offset: 0x000CD574
		[DefaultValue(typeof(Unit), "")]
		[WebSysDescription("TreeNodeStyle_VerticalPadding")]
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

		// Token: 0x0600402C RID: 16428 RVA: 0x000CF3CC File Offset: 0x000CD5CC
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

		// Token: 0x0600402D RID: 16429 RVA: 0x000CF4DC File Offset: 0x000CD6DC
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

		// Token: 0x0600402E RID: 16430 RVA: 0x000CF6C4 File Offset: 0x000CD8C4
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

		// Token: 0x0600402F RID: 16431 RVA: 0x000CF7D4 File Offset: 0x000CD9D4
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

		// Token: 0x06004030 RID: 16432 RVA: 0x000CF861 File Offset: 0x000CDA61
		internal void ResetCachedStyles()
		{
			this._hyperLinkStyle = null;
		}

		// Token: 0x04002472 RID: 9330
		private const int PROP_VPADDING = 65536;

		// Token: 0x04002473 RID: 9331
		private const int PROP_HPADDING = 131072;

		// Token: 0x04002474 RID: 9332
		private const int PROP_NODESPACING = 262144;

		// Token: 0x04002475 RID: 9333
		private const int PROP_CHILDNODESPADDING = 524288;

		// Token: 0x04002476 RID: 9334
		private const int PROP_IMAGEURL = 1048576;

		// Token: 0x04002477 RID: 9335
		private HyperLinkStyle _hyperLinkStyle;
	}
}
