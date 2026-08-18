using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004F0 RID: 1264
	public class TableStyle : Style
	{
		// Token: 0x06003EF4 RID: 16116 RVA: 0x000B75ED File Offset: 0x000B57ED
		public TableStyle()
		{
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x000B75F5 File Offset: 0x000B57F5
		public TableStyle(StateBag bag) : base(bag)
		{
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06003EF6 RID: 16118 RVA: 0x000BE1C0 File Offset: 0x000BC3C0
		// (set) Token: 0x06003EF7 RID: 16119 RVA: 0x000BE1EA File Offset: 0x000BC3EA
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[UrlProperty]
		[WebSysDescription("TableStyle_BackImageUrl")]
		[NotifyParentProperty(true)]
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

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06003EF8 RID: 16120 RVA: 0x000CA581 File Offset: 0x000C8781
		// (set) Token: 0x06003EF9 RID: 16121 RVA: 0x000CA5A7 File Offset: 0x000C87A7
		[WebCategory("Appearance")]
		[DefaultValue(-1)]
		[WebSysDescription("TableStyle_CellPadding")]
		[NotifyParentProperty(true)]
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

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06003EFA RID: 16122 RVA: 0x000CA5E3 File Offset: 0x000C87E3
		// (set) Token: 0x06003EFB RID: 16123 RVA: 0x000CA609 File Offset: 0x000C8809
		[WebCategory("Appearance")]
		[DefaultValue(-1)]
		[WebSysDescription("TableStyle_CellSpacing")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06003EFC RID: 16124 RVA: 0x000CA645 File Offset: 0x000C8845
		// (set) Token: 0x06003EFD RID: 16125 RVA: 0x000CA66B File Offset: 0x000C886B
		[WebCategory("Appearance")]
		[DefaultValue(GridLines.None)]
		[WebSysDescription("TableStyle_GridLines")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06003EFE RID: 16126 RVA: 0x000CA6A1 File Offset: 0x000C88A1
		// (set) Token: 0x06003EFF RID: 16127 RVA: 0x000CA6C7 File Offset: 0x000C88C7
		[WebCategory("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		[WebSysDescription("TableStyle_HorizontalAlign")]
		[NotifyParentProperty(true)]
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

		// Token: 0x06003F00 RID: 16128 RVA: 0x000CA700 File Offset: 0x000C8900
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

		// Token: 0x06003F01 RID: 16129 RVA: 0x000CA804 File Offset: 0x000C8A04
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

		// Token: 0x06003F02 RID: 16130 RVA: 0x000CA8EC File Offset: 0x000C8AEC
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

		// Token: 0x06003F03 RID: 16131 RVA: 0x000CA924 File Offset: 0x000C8B24
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

		// Token: 0x06003F04 RID: 16132 RVA: 0x000CAA34 File Offset: 0x000C8C34
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

		// Token: 0x04002422 RID: 9250
		internal const int PROP_BACKIMAGEURL = 65536;

		// Token: 0x04002423 RID: 9251
		internal const int PROP_CELLPADDING = 131072;

		// Token: 0x04002424 RID: 9252
		internal const int PROP_CELLSPACING = 262144;

		// Token: 0x04002425 RID: 9253
		internal const int PROP_GRIDLINES = 524288;

		// Token: 0x04002426 RID: 9254
		internal const int PROP_HORZALIGN = 1048576;
	}
}
