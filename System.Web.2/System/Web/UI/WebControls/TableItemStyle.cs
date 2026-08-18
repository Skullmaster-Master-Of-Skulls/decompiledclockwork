using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Configuration;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004EB RID: 1259
	public class TableItemStyle : Style
	{
		// Token: 0x06003EBF RID: 16063 RVA: 0x000B75ED File Offset: 0x000B57ED
		public TableItemStyle()
		{
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x000B75F5 File Offset: 0x000B57F5
		public TableItemStyle(StateBag bag) : base(bag)
		{
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06003EC1 RID: 16065 RVA: 0x000C9E97 File Offset: 0x000C8097
		private bool EnableLegacyRendering
		{
			get
			{
				return RuntimeConfig.GetAppConfig().XhtmlConformance.Mode == XhtmlConformanceMode.Legacy;
			}
		}

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x000C9EAB File Offset: 0x000C80AB
		// (set) Token: 0x06003EC3 RID: 16067 RVA: 0x000C9ED1 File Offset: 0x000C80D1
		[WebCategory("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		[WebSysDescription("TableItem_HorizontalAlign")]
		[NotifyParentProperty(true)]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (base.IsSet(65536))
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
				this.SetBit(65536);
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x000C9F07 File Offset: 0x000C8107
		// (set) Token: 0x06003EC5 RID: 16069 RVA: 0x000C9F2D File Offset: 0x000C812D
		[WebCategory("Layout")]
		[DefaultValue(VerticalAlign.NotSet)]
		[WebSysDescription("TableItem_VerticalAlign")]
		[NotifyParentProperty(true)]
		public virtual VerticalAlign VerticalAlign
		{
			get
			{
				if (base.IsSet(131072))
				{
					return (VerticalAlign)base.ViewState["VerticalAlign"];
				}
				return VerticalAlign.NotSet;
			}
			set
			{
				if (value < VerticalAlign.NotSet || value > VerticalAlign.Bottom)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["VerticalAlign"] = value;
				this.SetBit(131072);
			}
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x000C9F63 File Offset: 0x000C8163
		// (set) Token: 0x06003EC7 RID: 16071 RVA: 0x000C9F89 File Offset: 0x000C8189
		[WebCategory("Layout")]
		[DefaultValue(true)]
		[WebSysDescription("TableItemStyle_Wrap")]
		[NotifyParentProperty(true)]
		public virtual bool Wrap
		{
			get
			{
				return !base.IsSet(262144) || (bool)base.ViewState["Wrap"];
			}
			set
			{
				base.ViewState["Wrap"] = value;
				this.SetBit(262144);
			}
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x000C9FAC File Offset: 0x000C81AC
		public override void AddAttributesToRender(HtmlTextWriter writer, WebControl owner)
		{
			base.AddAttributesToRender(writer, owner);
			if (!this.Wrap)
			{
				if (this.IsControlEnableLegacyRendering(owner))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, "nowrap");
				}
				else
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
				}
			}
			HorizontalAlign horizontalAlign = this.HorizontalAlign;
			if (horizontalAlign != HorizontalAlign.NotSet)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(HorizontalAlign));
				writer.AddAttribute(HtmlTextWriterAttribute.Align, converter.ConvertToString(horizontalAlign).ToLower(CultureInfo.InvariantCulture));
			}
			VerticalAlign verticalAlign = this.VerticalAlign;
			if (verticalAlign != VerticalAlign.NotSet)
			{
				TypeConverter converter2 = TypeDescriptor.GetConverter(typeof(VerticalAlign));
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, converter2.ConvertToString(verticalAlign).ToLower(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x000CA060 File Offset: 0x000C8260
		public override void CopyFrom(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				base.CopyFrom(s);
				if (s is TableItemStyle)
				{
					TableItemStyle tableItemStyle = (TableItemStyle)s;
					if (s.RegisteredCssClass.Length != 0)
					{
						if (tableItemStyle.IsSet(262144))
						{
							base.ViewState.Remove("Wrap");
							base.ClearBit(262144);
						}
					}
					else if (tableItemStyle.IsSet(262144))
					{
						this.Wrap = tableItemStyle.Wrap;
					}
					if (tableItemStyle.IsSet(65536))
					{
						this.HorizontalAlign = tableItemStyle.HorizontalAlign;
					}
					if (tableItemStyle.IsSet(131072))
					{
						this.VerticalAlign = tableItemStyle.VerticalAlign;
					}
				}
			}
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x000CA119 File Offset: 0x000C8319
		private bool IsControlEnableLegacyRendering(Control control)
		{
			if (control != null)
			{
				return control.EnableLegacyRendering;
			}
			return this.EnableLegacyRendering;
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x000CA12C File Offset: 0x000C832C
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
				if (s is TableItemStyle)
				{
					TableItemStyle tableItemStyle = (TableItemStyle)s;
					if (s.RegisteredCssClass.Length == 0 && tableItemStyle.IsSet(262144) && !base.IsSet(262144))
					{
						this.Wrap = tableItemStyle.Wrap;
					}
					if (tableItemStyle.IsSet(65536) && !base.IsSet(65536))
					{
						this.HorizontalAlign = tableItemStyle.HorizontalAlign;
					}
					if (tableItemStyle.IsSet(131072) && !base.IsSet(131072))
					{
						this.VerticalAlign = tableItemStyle.VerticalAlign;
					}
				}
			}
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x000CA1F4 File Offset: 0x000C83F4
		public override void Reset()
		{
			if (base.IsSet(65536))
			{
				base.ViewState.Remove("HorizontalAlign");
			}
			if (base.IsSet(131072))
			{
				base.ViewState.Remove("VerticalAlign");
			}
			if (base.IsSet(262144))
			{
				base.ViewState.Remove("Wrap");
			}
			base.Reset();
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x000CA25E File Offset: 0x000C845E
		private void ResetWrap()
		{
			base.ViewState.Remove("Wrap");
			base.ClearBit(262144);
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x000CA27B File Offset: 0x000C847B
		private bool ShouldSerializeWrap()
		{
			return base.IsSet(262144);
		}

		// Token: 0x04002419 RID: 9241
		internal const int PROP_HORZALIGN = 65536;

		// Token: 0x0400241A RID: 9242
		internal const int PROP_VERTALIGN = 131072;

		// Token: 0x0400241B RID: 9243
		internal const int PROP_WRAP = 262144;
	}
}
