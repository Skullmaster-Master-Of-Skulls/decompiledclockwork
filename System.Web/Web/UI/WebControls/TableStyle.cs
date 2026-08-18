using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200065F RID: 1631
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TableStyle : Style
	{
		// Token: 0x06004FB4 RID: 20404 RVA: 0x0013FF09 File Offset: 0x0013EF09
		public TableStyle()
		{
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x0013FF11 File Offset: 0x0013EF11
		public TableStyle(StateBag bag) : base(bag)
		{
		}

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06004FB6 RID: 20406 RVA: 0x0013FF1A File Offset: 0x0013EF1A
		// (set) Token: 0x06004FB7 RID: 20407 RVA: 0x0013FF44 File Offset: 0x0013EF44
		[WebCategory("Appearance")]
		[WebSysDescription("TableStyle_BackImageUrl")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[UrlProperty]
		public virtual string BackImageUrl
		{
			get
			{
				if (base.IsSet(65536))
				{
					return (string)base.ViewState["BackImageUrl"];
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.ViewState["BackImageUrl"] = value;
				this.SetBit(65536);
			}
		}

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x06004FB8 RID: 20408 RVA: 0x0013FF70 File Offset: 0x0013EF70
		// (set) Token: 0x06004FB9 RID: 20409 RVA: 0x0013FF96 File Offset: 0x0013EF96
		[DefaultValue(-1)]
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		[WebSysDescription("TableStyle_CellPadding")]
		public virtual int CellPadding
		{
			get
			{
				if (base.IsSet(131072))
				{
					return (int)base.ViewState["CellPadding"];
				}
				return -1;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("TableStyle_InvalidCellPadding"));
				}
				base.ViewState["CellPadding"] = value;
				this.SetBit(131072);
			}
		}

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x06004FBA RID: 20410 RVA: 0x0013FFD2 File Offset: 0x0013EFD2
		// (set) Token: 0x06004FBB RID: 20411 RVA: 0x0013FFF8 File Offset: 0x0013EFF8
		[NotifyParentProperty(true)]
		[WebSysDescription("TableStyle_CellSpacing")]
		[WebCategory("Appearance")]
		[DefaultValue(-1)]
		public virtual int CellSpacing
		{
			get
			{
				if (base.IsSet(262144))
				{
					return (int)base.ViewState["CellSpacing"];
				}
				return -1;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("TableStyle_InvalidCellSpacing"));
				}
				base.ViewState["CellSpacing"] = value;
				this.SetBit(262144);
			}
		}

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x06004FBC RID: 20412 RVA: 0x00140034 File Offset: 0x0013F034
		// (set) Token: 0x06004FBD RID: 20413 RVA: 0x0014005A File Offset: 0x0013F05A
		[NotifyParentProperty(true)]
		[WebCategory("Appearance")]
		[DefaultValue(GridLines.None)]
		[WebSysDescription("TableStyle_GridLines")]
		public virtual GridLines GridLines
		{
			get
			{
				if (base.IsSet(524288))
				{
					return (GridLines)base.ViewState["GridLines"];
				}
				return GridLines.None;
			}
			set
			{
				if (value < GridLines.None || value > GridLines.Both)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["GridLines"] = value;
				this.SetBit(524288);
			}
		}

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x06004FBE RID: 20414 RVA: 0x00140090 File Offset: 0x0013F090
		// (set) Token: 0x06004FBF RID: 20415 RVA: 0x001400B6 File Offset: 0x0013F0B6
		[NotifyParentProperty(true)]
		[WebCategory("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		[WebSysDescription("TableStyle_HorizontalAlign")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (base.IsSet(1048576))
				{
					return (HorizontalAlign)base.ViewState["HorizontalAlign"];
				}
				return HorizontalAlign.NotSet;
			}
			set
			{
				if (value < HorizontalAlign.NotSet || value > HorizontalAlign.Justify)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["HorizontalAlign"] = value;
				this.SetBit(1048576);
			}
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x001400EC File Offset: 0x0013F0EC
		public override void AddAttributesToRender(HtmlTextWriter writer, WebControl owner)
		{
			base.AddAttributesToRender(writer, owner);
			int num = this.CellSpacing;
			if (num >= 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, num.ToString(NumberFormatInfo.InvariantInfo));
				if (num == 0)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.BorderCollapse, "collapse");
				}
			}
			num = this.CellPadding;
			if (num >= 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, num.ToString(NumberFormatInfo.InvariantInfo));
			}
			HorizontalAlign horizontalAlign = this.HorizontalAlign;
			if (horizontalAlign != HorizontalAlign.NotSet)
			{
				string value = "Justify";
				switch (horizontalAlign)
				{
				case HorizontalAlign.Left:
					value = "Left";
					break;
				case HorizontalAlign.Center:
					value = "Center";
					break;
				case HorizontalAlign.Right:
					value = "Right";
					break;
				case HorizontalAlign.Justify:
					value = "Justify";
					break;
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Align, value);
			}
			GridLines gridLines = this.GridLines;
			if (gridLines != GridLines.None)
			{
				string value2 = string.Empty;
				switch (this.GridLines)
				{
				case GridLines.Horizontal:
					value2 = "rows";
					break;
				case GridLines.Vertical:
					value2 = "cols";
					break;
				case GridLines.Both:
					value2 = "all";
					break;
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Rules, value2);
			}
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x001401F4 File Offset: 0x0013F1F4
		public override void CopyFrom(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				base.CopyFrom(s);
				TableStyle tableStyle = s as TableStyle;
				if (tableStyle != null)
				{
					if (s.RegisteredCssClass.Length != 0)
					{
						if (tableStyle.IsSet(65536))
						{
							base.ViewState.Remove("BackImageUrl");
							base.ClearBit(65536);
						}
					}
					else if (tableStyle.IsSet(65536))
					{
						this.BackImageUrl = tableStyle.BackImageUrl;
					}
					if (tableStyle.IsSet(131072))
					{
						this.CellPadding = tableStyle.CellPadding;
					}
					if (tableStyle.IsSet(262144))
					{
						this.CellSpacing = tableStyle.CellSpacing;
					}
					if (tableStyle.IsSet(524288))
					{
						this.GridLines = tableStyle.GridLines;
					}
					if (tableStyle.IsSet(1048576))
					{
						this.HorizontalAlign = tableStyle.HorizontalAlign;
					}
				}
			}
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x001402DC File Offset: 0x0013F2DC
		protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
		{
			base.FillStyleAttributes(attributes, urlResolver);
			string text = this.BackImageUrl;
			if (text.Length != 0)
			{
				if (urlResolver != null)
				{
					text = urlResolver.ResolveClientUrl(text);
				}
				attributes.Add(HtmlTextWriterStyle.BackgroundImage, text);
			}
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x00140314 File Offset: 0x0013F314
		public override void MergeWith(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				if (this.IsEmpty)
				{
					this.CopyFrom(s);
					return;
				}
				base.MergeWith(s);
				TableStyle tableStyle = s as TableStyle;
				if (tableStyle != null)
				{
					if (s.RegisteredCssClass.Length == 0 && tableStyle.IsSet(65536) && !base.IsSet(65536))
					{
						this.BackImageUrl = tableStyle.BackImageUrl;
					}
					if (tableStyle.IsSet(131072) && !base.IsSet(131072))
					{
						this.CellPadding = tableStyle.CellPadding;
					}
					if (tableStyle.IsSet(262144) && !base.IsSet(262144))
					{
						this.CellSpacing = tableStyle.CellSpacing;
					}
					if (tableStyle.IsSet(524288) && !base.IsSet(524288))
					{
						this.GridLines = tableStyle.GridLines;
					}
					if (tableStyle.IsSet(1048576) && !base.IsSet(1048576))
					{
						this.HorizontalAlign = tableStyle.HorizontalAlign;
					}
				}
			}
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x00140424 File Offset: 0x0013F424
		public override void Reset()
		{
			if (base.IsSet(65536))
			{
				base.ViewState.Remove("BackImageUrl");
			}
			if (base.IsSet(131072))
			{
				base.ViewState.Remove("CellPadding");
			}
			if (base.IsSet(262144))
			{
				base.ViewState.Remove("CellSpacing");
			}
			if (base.IsSet(524288))
			{
				base.ViewState.Remove("GridLines");
			}
			if (base.IsSet(1048576))
			{
				base.ViewState.Remove("HorizontalAlign");
			}
			base.Reset();
		}

		// Token: 0x04002CF3 RID: 11507
		internal const int PROP_BACKIMAGEURL = 65536;

		// Token: 0x04002CF4 RID: 11508
		internal const int PROP_CELLPADDING = 131072;

		// Token: 0x04002CF5 RID: 11509
		internal const int PROP_CELLSPACING = 262144;

		// Token: 0x04002CF6 RID: 11510
		internal const int PROP_GRIDLINES = 524288;

		// Token: 0x04002CF7 RID: 11511
		internal const int PROP_HORZALIGN = 1048576;
	}
}
