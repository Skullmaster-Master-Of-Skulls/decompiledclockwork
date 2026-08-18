using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200035E RID: 862
	[ParseChildren(true, "Cells")]
	public class HtmlTableRow : HtmlContainerControl
	{
		// Token: 0x060027DD RID: 10205 RVA: 0x00081456 File Offset: 0x0007F656
		public HtmlTableRow() : base("tr")
		{
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x00081464 File Offset: 0x0007F664
		// (set) Token: 0x060027DF RID: 10207 RVA: 0x0007EEAC File Offset: 0x0007D0AC
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

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x0008148C File Offset: 0x0007F68C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual HtmlTableCellCollection Cells
		{
			get
			{
				if (this.cells == null)
				{
					this.cells = new HtmlTableCellCollection(this);
				}
				return this.cells;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060027E1 RID: 10209 RVA: 0x000814A8 File Offset: 0x0007F6A8
		// (set) Token: 0x060027E2 RID: 10210 RVA: 0x00080F58 File Offset: 0x0007F158
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

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060027E3 RID: 10211 RVA: 0x000814D0 File Offset: 0x0007F6D0
		// (set) Token: 0x060027E4 RID: 10212 RVA: 0x00080FC8 File Offset: 0x0007F1C8
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

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060027E5 RID: 10213 RVA: 0x000814F8 File Offset: 0x0007F6F8
		// (set) Token: 0x060027E6 RID: 10214 RVA: 0x00081098 File Offset: 0x0007F298
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

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060027E7 RID: 10215 RVA: 0x0008027F File Offset: 0x0007E47F
		// (set) Token: 0x060027E8 RID: 10216 RVA: 0x0008027F File Offset: 0x0007E47F
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

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x000802A4 File Offset: 0x0007E4A4
		// (set) Token: 0x060027EA RID: 10218 RVA: 0x000802A4 File Offset: 0x0007E4A4
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

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060027EB RID: 10219 RVA: 0x00081520 File Offset: 0x0007F720
		// (set) Token: 0x060027EC RID: 10220 RVA: 0x00081328 File Offset: 0x0007F528
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

		// Token: 0x060027ED RID: 10221 RVA: 0x00081548 File Offset: 0x0007F748
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			writer.WriteLine();
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			base.RenderChildren(writer);
			indent = writer.Indent;
			writer.Indent = indent - 1;
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x00081146 File Offset: 0x0007F346
		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			writer.WriteLine();
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x00081582 File Offset: 0x0007F782
		protected override ControlCollection CreateControlCollection()
		{
			return new HtmlTableRow.HtmlTableCellControlCollection(this);
		}

		// Token: 0x04001DDF RID: 7647
		private HtmlTableCellCollection cells;

		// Token: 0x02000990 RID: 2448
		protected class HtmlTableCellControlCollection : ControlCollection
		{
			// Token: 0x06006A77 RID: 27255 RVA: 0x00061D30 File Offset: 0x0005FF30
			internal HtmlTableCellControlCollection(Control owner) : base(owner)
			{
			}

			// Token: 0x06006A78 RID: 27256 RVA: 0x0017C14C File Offset: 0x0017A34C
			public override void Add(Control child)
			{
				if (child is HtmlTableCell)
				{
					base.Add(child);
					return;
				}
				throw new ArgumentException(SR.GetString("Cannot_Have_Children_Of_Type", new object[]
				{
					"HtmlTableRow",
					child.GetType().Name.ToString(CultureInfo.InvariantCulture)
				}));
			}

			// Token: 0x06006A79 RID: 27257 RVA: 0x0017C1A0 File Offset: 0x0017A3A0
			public override void AddAt(int index, Control child)
			{
				if (child is HtmlTableCell)
				{
					base.AddAt(index, child);
					return;
				}
				throw new ArgumentException(SR.GetString("Cannot_Have_Children_Of_Type", new object[]
				{
					"HtmlTableRow",
					child.GetType().Name.ToString(CultureInfo.InvariantCulture)
				}));
			}
		}
	}
}
