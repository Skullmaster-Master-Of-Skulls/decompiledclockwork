using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200035C RID: 860
	[ConstructorNeedsTag(true)]
	public class HtmlTableCell : HtmlContainerControl
	{
		// Token: 0x060027BB RID: 10171 RVA: 0x0008115D File Offset: 0x0007F35D
		public HtmlTableCell() : base("td")
		{
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x0008116A File Offset: 0x0007F36A
		public HtmlTableCell(string tagName) : base(tagName)
		{
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x060027BD RID: 10173 RVA: 0x00081174 File Offset: 0x0007F374
		// (set) Token: 0x060027BE RID: 10174 RVA: 0x0007EEAC File Offset: 0x0007D0AC
		[WebCategory("Layout")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Align
		{
			get
			{
				string text = base.Attributes["align"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["align"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x060027BF RID: 10175 RVA: 0x0008119C File Offset: 0x0007F39C
		// (set) Token: 0x060027C0 RID: 10176 RVA: 0x00080F58 File Offset: 0x0007F158
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string BgColor
		{
			get
			{
				string text = base.Attributes["bgcolor"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["bgcolor"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x000811C4 File Offset: 0x0007F3C4
		// (set) Token: 0x060027C2 RID: 10178 RVA: 0x00080FC8 File Offset: 0x0007F1C8
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string BorderColor
		{
			get
			{
				string text = base.Attributes["bordercolor"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["bordercolor"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x060027C3 RID: 10179 RVA: 0x000811EC File Offset: 0x0007F3EC
		// (set) Token: 0x060027C4 RID: 10180 RVA: 0x0008121A File Offset: 0x0007F41A
		[WebCategory("Layout")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColSpan
		{
			get
			{
				string text = base.Attributes["colspan"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["colspan"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x00081234 File Offset: 0x0007F434
		// (set) Token: 0x060027C6 RID: 10182 RVA: 0x00081098 File Offset: 0x0007F298
		[WebCategory("Layout")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Height
		{
			get
			{
				string text = base.Attributes["height"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["height"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x0008125C File Offset: 0x0007F45C
		// (set) Token: 0x060027C8 RID: 10184 RVA: 0x0008128A File Offset: 0x0007F48A
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[TypeConverter(typeof(MinimizableAttributeTypeConverter))]
		public bool NoWrap
		{
			get
			{
				string text = base.Attributes["nowrap"];
				return text != null && text.Equals("nowrap");
			}
			set
			{
				if (value)
				{
					base.Attributes["nowrap"] = "nowrap";
					return;
				}
				base.Attributes["nowrap"] = null;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x000812B8 File Offset: 0x0007F4B8
		// (set) Token: 0x060027CA RID: 10186 RVA: 0x000812E6 File Offset: 0x0007F4E6
		[WebCategory("Layout")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int RowSpan
		{
			get
			{
				string text = base.Attributes["rowspan"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["rowspan"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x00081300 File Offset: 0x0007F500
		// (set) Token: 0x060027CC RID: 10188 RVA: 0x00081328 File Offset: 0x0007F528
		[WebCategory("Layout")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string VAlign
		{
			get
			{
				string text = base.Attributes["valign"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["valign"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060027CD RID: 10189 RVA: 0x00081340 File Offset: 0x0007F540
		// (set) Token: 0x060027CE RID: 10190 RVA: 0x000810D8 File Offset: 0x0007F2D8
		[WebCategory("Layout")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Width
		{
			get
			{
				string text = base.Attributes["width"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["width"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x00081146 File Offset: 0x0007F346
		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			writer.WriteLine();
		}
	}
}
