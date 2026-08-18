using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200035B RID: 859
	[ParseChildren(true, "Rows")]
	public class HtmlTable : HtmlContainerControl
	{
		// Token: 0x060027A2 RID: 10146 RVA: 0x00080EFA File Offset: 0x0007F0FA
		public HtmlTable() : base("table")
		{
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x060027A3 RID: 10147 RVA: 0x00080F08 File Offset: 0x0007F108
		// (set) Token: 0x060027A4 RID: 10148 RVA: 0x0007EEAC File Offset: 0x0007D0AC
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

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x00080F30 File Offset: 0x0007F130
		// (set) Token: 0x060027A6 RID: 10150 RVA: 0x00080F58 File Offset: 0x0007F158
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

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x00080F70 File Offset: 0x0007F170
		// (set) Token: 0x060027A8 RID: 10152 RVA: 0x0007EEF2 File Offset: 0x0007D0F2
		[WebCategory("Appearance")]
		[DefaultValue(-1)]
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

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060027A9 RID: 10153 RVA: 0x00080FA0 File Offset: 0x0007F1A0
		// (set) Token: 0x060027AA RID: 10154 RVA: 0x00080FC8 File Offset: 0x0007F1C8
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

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x00080FE0 File Offset: 0x0007F1E0
		// (set) Token: 0x060027AC RID: 10156 RVA: 0x0008100E File Offset: 0x0007F20E
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int CellPadding
		{
			get
			{
				string text = base.Attributes["cellpadding"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["cellpadding"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x060027AD RID: 10157 RVA: 0x00081028 File Offset: 0x0007F228
		// (set) Token: 0x060027AE RID: 10158 RVA: 0x00081056 File Offset: 0x0007F256
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int CellSpacing
		{
			get
			{
				string text = base.Attributes["cellspacing"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["cellspacing"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060027AF RID: 10159 RVA: 0x0008027F File Offset: 0x0007E47F
		// (set) Token: 0x060027B0 RID: 10160 RVA: 0x0008027F File Offset: 0x0007E47F
		public override string InnerHtml
		{
			get
			{
				throw new NotSupportedException(SR.GetString("InnerHtml_not_supported", new object[]
				{
					base.GetType().Name
				}));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("InnerHtml_not_supported", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x000802A4 File Offset: 0x0007E4A4
		// (set) Token: 0x060027B2 RID: 10162 RVA: 0x000802A4 File Offset: 0x0007E4A4
		public override string InnerText
		{
			get
			{
				throw new NotSupportedException(SR.GetString("InnerText_not_supported", new object[]
				{
					base.GetType().Name
				}));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("InnerText_not_supported", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x00081070 File Offset: 0x0007F270
		// (set) Token: 0x060027B4 RID: 10164 RVA: 0x00081098 File Offset: 0x0007F298
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

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x000810B0 File Offset: 0x0007F2B0
		// (set) Token: 0x060027B6 RID: 10166 RVA: 0x000810D8 File Offset: 0x0007F2D8
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

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x000810F0 File Offset: 0x0007F2F0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[IgnoreUnknownContent]
		public virtual HtmlTableRowCollection Rows
		{
			get
			{
				if (this.rows == null)
				{
					this.rows = new HtmlTableRowCollection(this);
				}
				return this.rows;
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x0008110C File Offset: 0x0007F30C
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			writer.WriteLine();
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.RenderChildren(writer);
			indent = writer.Indent;
			writer.Indent = indent - 1;
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x00081146 File Offset: 0x0007F346
		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			writer.WriteLine();
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x00081155 File Offset: 0x0007F355
		protected override ControlCollection CreateControlCollection()
		{
			return new HtmlTable.HtmlTableRowControlCollection(this);
		}

		// Token: 0x04001DDD RID: 7645
		private HtmlTableRowCollection rows;

		// Token: 0x0200098F RID: 2447
		protected class HtmlTableRowControlCollection : ControlCollection
		{
			// Token: 0x06006A74 RID: 27252 RVA: 0x00061D30 File Offset: 0x0005FF30
			internal HtmlTableRowControlCollection(Control owner) : base(owner)
			{
			}

			// Token: 0x06006A75 RID: 27253 RVA: 0x0017C0A4 File Offset: 0x0017A2A4
			public override void Add(Control child)
			{
				if (child is HtmlTableRow)
				{
					base.Add(child);
					return;
				}
				throw new ArgumentException(SR.GetString("Cannot_Have_Children_Of_Type", new object[]
				{
					"HtmlTable",
					child.GetType().Name.ToString(CultureInfo.InvariantCulture)
				}));
			}

			// Token: 0x06006A76 RID: 27254 RVA: 0x0017C0F8 File Offset: 0x0017A2F8
			public override void AddAt(int index, Control child)
			{
				if (child is HtmlTableRow)
				{
					base.AddAt(index, child);
					return;
				}
				throw new ArgumentException(SR.GetString("Cannot_Have_Children_Of_Type", new object[]
				{
					"HtmlTable",
					child.GetType().Name.ToString(CultureInfo.InvariantCulture)
				}));
			}
		}
	}
}
