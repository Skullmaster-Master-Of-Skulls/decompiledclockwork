using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x020004AE RID: 1198
	[ParseChildren(true, "Cells")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTableRow : HtmlContainerControl
	{
		// Token: 0x0600381D RID: 14365 RVA: 0x000EFBAF File Offset: 0x000EEBAF
		public HtmlTableRow() : base("tr")
		{
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x0600381E RID: 14366 RVA: 0x000EFBBC File Offset: 0x000EEBBC
		// (set) Token: 0x0600381F RID: 14367 RVA: 0x000EFBE4 File Offset: 0x000EEBE4
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Layout")]
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

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06003820 RID: 14368 RVA: 0x000EFBFC File Offset: 0x000EEBFC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06003821 RID: 14369 RVA: 0x000EFC18 File Offset: 0x000EEC18
		// (set) Token: 0x06003822 RID: 14370 RVA: 0x000EFC40 File Offset: 0x000EEC40
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
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

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06003823 RID: 14371 RVA: 0x000EFC58 File Offset: 0x000EEC58
		// (set) Token: 0x06003824 RID: 14372 RVA: 0x000EFC80 File Offset: 0x000EEC80
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

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06003825 RID: 14373 RVA: 0x000EFC98 File Offset: 0x000EEC98
		// (set) Token: 0x06003826 RID: 14374 RVA: 0x000EFCC0 File Offset: 0x000EECC0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[WebCategory("Layout")]
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

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06003827 RID: 14375 RVA: 0x000EFCD8 File Offset: 0x000EECD8
		// (set) Token: 0x06003828 RID: 14376 RVA: 0x000EFD0C File Offset: 0x000EED0C
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

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06003829 RID: 14377 RVA: 0x000EFD40 File Offset: 0x000EED40
		// (set) Token: 0x0600382A RID: 14378 RVA: 0x000EFD74 File Offset: 0x000EED74
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

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x0600382B RID: 14379 RVA: 0x000EFDA8 File Offset: 0x000EEDA8
		// (set) Token: 0x0600382C RID: 14380 RVA: 0x000EFDD0 File Offset: 0x000EEDD0
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Layout")]
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

		// Token: 0x0600382D RID: 14381 RVA: 0x000EFDE8 File Offset: 0x000EEDE8
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			writer.WriteLine();
			writer.Indent++;
			base.RenderChildren(writer);
			writer.Indent--;
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x000EFE13 File Offset: 0x000EEE13
		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			writer.WriteLine();
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000EFE22 File Offset: 0x000EEE22
		protected override ControlCollection CreateControlCollection()
		{
			return new HtmlTableRow.HtmlTableCellControlCollection(this);
		}

		// Token: 0x040025DA RID: 9690
		private HtmlTableCellCollection cells;

		// Token: 0x020004AF RID: 1199
		protected class HtmlTableCellControlCollection : ControlCollection
		{
			// Token: 0x06003830 RID: 14384 RVA: 0x000EFE2A File Offset: 0x000EEE2A
			internal HtmlTableCellControlCollection(Control owner) : base(owner)
			{
			}

			// Token: 0x06003831 RID: 14385 RVA: 0x000EFE34 File Offset: 0x000EEE34
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

			// Token: 0x06003832 RID: 14386 RVA: 0x000EFE88 File Offset: 0x000EEE88
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
