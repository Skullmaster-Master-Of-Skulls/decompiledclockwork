using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x020004AA RID: 1194
	[ParseChildren(true, "Rows")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTable : HtmlContainerControl
	{
		// Token: 0x060037DF RID: 14303 RVA: 0x000EF41E File Offset: 0x000EE41E
		public HtmlTable() : base("table")
		{
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x060037E0 RID: 14304 RVA: 0x000EF42C File Offset: 0x000EE42C
		// (set) Token: 0x060037E1 RID: 14305 RVA: 0x000EF454 File Offset: 0x000EE454
		[WebCategory("Layout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
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

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x060037E2 RID: 14306 RVA: 0x000EF46C File Offset: 0x000EE46C
		// (set) Token: 0x060037E3 RID: 14307 RVA: 0x000EF494 File Offset: 0x000EE494
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[WebCategory("Appearance")]
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

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x060037E4 RID: 14308 RVA: 0x000EF4AC File Offset: 0x000EE4AC
		// (set) Token: 0x060037E5 RID: 14309 RVA: 0x000EF4DA File Offset: 0x000EE4DA
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

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x060037E6 RID: 14310 RVA: 0x000EF4F4 File Offset: 0x000EE4F4
		// (set) Token: 0x060037E7 RID: 14311 RVA: 0x000EF51C File Offset: 0x000EE51C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[WebCategory("Appearance")]
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

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x060037E8 RID: 14312 RVA: 0x000EF534 File Offset: 0x000EE534
		// (set) Token: 0x060037E9 RID: 14313 RVA: 0x000EF562 File Offset: 0x000EE562
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
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

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x060037EA RID: 14314 RVA: 0x000EF57C File Offset: 0x000EE57C
		// (set) Token: 0x060037EB RID: 14315 RVA: 0x000EF5AA File Offset: 0x000EE5AA
		[DefaultValue("")]
		[WebCategory("Appearance")]
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

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x060037EC RID: 14316 RVA: 0x000EF5C4 File Offset: 0x000EE5C4
		// (set) Token: 0x060037ED RID: 14317 RVA: 0x000EF5F8 File Offset: 0x000EE5F8
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

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x060037EE RID: 14318 RVA: 0x000EF62C File Offset: 0x000EE62C
		// (set) Token: 0x060037EF RID: 14319 RVA: 0x000EF660 File Offset: 0x000EE660
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

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x060037F0 RID: 14320 RVA: 0x000EF694 File Offset: 0x000EE694
		// (set) Token: 0x060037F1 RID: 14321 RVA: 0x000EF6BC File Offset: 0x000EE6BC
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x060037F2 RID: 14322 RVA: 0x000EF6D4 File Offset: 0x000EE6D4
		// (set) Token: 0x060037F3 RID: 14323 RVA: 0x000EF6FC File Offset: 0x000EE6FC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Layout")]
		[DefaultValue("")]
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

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060037F4 RID: 14324 RVA: 0x000EF714 File Offset: 0x000EE714
		[IgnoreUnknownContent]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x060037F5 RID: 14325 RVA: 0x000EF730 File Offset: 0x000EE730
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			writer.WriteLine();
			writer.Indent++;
			base.RenderChildren(writer);
			writer.Indent--;
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x000EF75B File Offset: 0x000EE75B
		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			writer.WriteLine();
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x000EF76A File Offset: 0x000EE76A
		protected override ControlCollection CreateControlCollection()
		{
			return new HtmlTable.HtmlTableRowControlCollection(this);
		}

		// Token: 0x040025D8 RID: 9688
		private HtmlTableRowCollection rows;

		// Token: 0x020004AB RID: 1195
		protected class HtmlTableRowControlCollection : ControlCollection
		{
			// Token: 0x060037F8 RID: 14328 RVA: 0x000EF772 File Offset: 0x000EE772
			internal HtmlTableRowControlCollection(Control owner) : base(owner)
			{
			}

			// Token: 0x060037F9 RID: 14329 RVA: 0x000EF77C File Offset: 0x000EE77C
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

			// Token: 0x060037FA RID: 14330 RVA: 0x000EF7D0 File Offset: 0x000EE7D0
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
