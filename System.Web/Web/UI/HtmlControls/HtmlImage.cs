using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200049A RID: 1178
	[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlImage : HtmlControl
	{
		// Token: 0x060036FB RID: 14075 RVA: 0x000ED05A File Offset: 0x000EC05A
		public HtmlImage() : base("img")
		{
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x000ED068 File Offset: 0x000EC068
		// (set) Token: 0x060036FD RID: 14077 RVA: 0x000ED090 File Offset: 0x000EC090
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
		[Localizable(true)]
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
				base.Attributes["alt"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x000ED0A8 File Offset: 0x000EC0A8
		// (set) Token: 0x060036FF RID: 14079 RVA: 0x000ED0D0 File Offset: 0x000EC0D0
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
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

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06003700 RID: 14080 RVA: 0x000ED0E8 File Offset: 0x000EC0E8
		// (set) Token: 0x06003701 RID: 14081 RVA: 0x000ED116 File Offset: 0x000EC116
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
		[DefaultValue(0)]
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

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06003702 RID: 14082 RVA: 0x000ED130 File Offset: 0x000EC130
		// (set) Token: 0x06003703 RID: 14083 RVA: 0x000ED15E File Offset: 0x000EC15E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(100)]
		[WebCategory("Layout")]
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

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x000ED178 File Offset: 0x000EC178
		// (set) Token: 0x06003705 RID: 14085 RVA: 0x000ED1A0 File Offset: 0x000EC1A0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[UrlProperty]
		[WebCategory("Behavior")]
		[DefaultValue("")]
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

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06003706 RID: 14086 RVA: 0x000ED1B8 File Offset: 0x000EC1B8
		// (set) Token: 0x06003707 RID: 14087 RVA: 0x000ED1E6 File Offset: 0x000EC1E6
		[WebCategory("Layout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(100)]
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

		// Token: 0x06003708 RID: 14088 RVA: 0x000ED1FE File Offset: 0x000EC1FE
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			base.PreProcessRelativeReferenceAttribute(writer, "src");
			base.RenderAttributes(writer);
			writer.Write(" /");
		}
	}
}
