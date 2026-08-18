using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200034B RID: 843
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	public class HtmlImage : HtmlControl
	{
		// Token: 0x060026BE RID: 9918 RVA: 0x0007EE11 File Offset: 0x0007D011
		public HtmlImage() : base("img")
		{
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x0007EE20 File Offset: 0x0007D020
		// (set) Token: 0x060026C0 RID: 9920 RVA: 0x0007EE48 File Offset: 0x0007D048
		[WebCategory("Appearance")]
		[Localizable(true)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Alt
		{
			get
			{
				string text = base.Attributes["alt"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (this.RenderingCompatibility >= VersionUtil.Framework45)
				{
					base.Attributes["alt"] = value;
					return;
				}
				base.Attributes["alt"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x0007EE84 File Offset: 0x0007D084
		// (set) Token: 0x060026C2 RID: 9922 RVA: 0x0007EEAC File Offset: 0x0007D0AC
		[WebCategory("Appearance")]
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

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x0007EEC4 File Offset: 0x0007D0C4
		// (set) Token: 0x060026C4 RID: 9924 RVA: 0x0007EEF2 File Offset: 0x0007D0F2
		[WebCategory("Appearance")]
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Border
		{
			get
			{
				string text = base.Attributes["border"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["border"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x0007EF0C File Offset: 0x0007D10C
		// (set) Token: 0x060026C6 RID: 9926 RVA: 0x0007EF3A File Offset: 0x0007D13A
		[WebCategory("Layout")]
		[DefaultValue(100)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Height
		{
			get
			{
				string text = base.Attributes["height"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["height"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060026C7 RID: 9927 RVA: 0x0007EF54 File Offset: 0x0007D154
		// (set) Token: 0x060026C8 RID: 9928 RVA: 0x0007DF48 File Offset: 0x0007C148
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[UrlProperty]
		public string Src
		{
			get
			{
				string text = base.Attributes["src"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["src"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x0007EF7C File Offset: 0x0007D17C
		// (set) Token: 0x060026CA RID: 9930 RVA: 0x0007EFAA File Offset: 0x0007D1AA
		[WebCategory("Layout")]
		[DefaultValue(100)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Width
		{
			get
			{
				string text = base.Attributes["width"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["width"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x0007DFE4 File Offset: 0x0007C1E4
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.PreProcessRelativeReferenceAttribute(writer, "src");
			base.RenderAttributes(writer);
			writer.Write(" /");
		}
	}
}
