using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004E2 RID: 1250
	[DefaultProperty("Rows")]
	[ParseChildren(true, "Rows")]
	[Designer("System.Web.UI.Design.WebControls.TableDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class Table : WebControl, IPostBackEventHandler
	{
		// Token: 0x06003E6D RID: 15981 RVA: 0x00088D32 File Offset: 0x00086F32
		public Table() : base(HtmlTextWriterTag.Table)
		{
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06003E6E RID: 15982 RVA: 0x000963E9 File Offset: 0x000945E9
		// (set) Token: 0x06003E6F RID: 15983 RVA: 0x00096409 File Offset: 0x00094609
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("Table_BackImageUrl")]
		public virtual string BackImageUrl
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return string.Empty;
				}
				return ((TableStyle)base.ControlStyle).BackImageUrl;
			}
			set
			{
				((TableStyle)base.ControlStyle).BackImageUrl = value;
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06003E70 RID: 15984 RVA: 0x000C9414 File Offset: 0x000C7614
		// (set) Token: 0x06003E71 RID: 15985 RVA: 0x00085605 File Offset: 0x00083805
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Accessibility")]
		[WebSysDescription("Table_Caption")]
		public virtual string Caption
		{
			get
			{
				string text = (string)this.ViewState["Caption"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["Caption"] = value;
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06003E72 RID: 15986 RVA: 0x000C9444 File Offset: 0x000C7644
		// (set) Token: 0x06003E73 RID: 15987 RVA: 0x00085641 File Offset: 0x00083841
		[DefaultValue(TableCaptionAlign.NotSet)]
		[WebCategory("Accessibility")]
		[WebSysDescription("WebControl_CaptionAlign")]
		public virtual TableCaptionAlign CaptionAlign
		{
			get
			{
				object obj = this.ViewState["CaptionAlign"];
				if (obj == null)
				{
					return TableCaptionAlign.NotSet;
				}
				return (TableCaptionAlign)obj;
			}
			set
			{
				if (value < TableCaptionAlign.NotSet || value > TableCaptionAlign.Right)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["CaptionAlign"] = value;
			}
		}

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06003E74 RID: 15988 RVA: 0x0008566C File Offset: 0x0008386C
		// (set) Token: 0x06003E75 RID: 15989 RVA: 0x00085688 File Offset: 0x00083888
		[WebCategory("Appearance")]
		[DefaultValue(-1)]
		[WebSysDescription("Table_CellPadding")]
		public virtual int CellPadding
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return ((TableStyle)base.ControlStyle).CellPadding;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellPadding = value;
			}
		}

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06003E76 RID: 15990 RVA: 0x0008E6AC File Offset: 0x0008C8AC
		// (set) Token: 0x06003E77 RID: 15991 RVA: 0x000856B7 File Offset: 0x000838B7
		[WebCategory("Appearance")]
		[DefaultValue(-1)]
		[WebSysDescription("Table_CellSpacing")]
		public virtual int CellSpacing
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return ((TableStyle)base.ControlStyle).CellSpacing;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellSpacing = value;
			}
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06003E78 RID: 15992 RVA: 0x00098EF0 File Offset: 0x000970F0
		// (set) Token: 0x06003E79 RID: 15993 RVA: 0x0008587A File Offset: 0x00083A7A
		[WebCategory("Appearance")]
		[DefaultValue(GridLines.None)]
		[WebSysDescription("Table_GridLines")]
		public virtual GridLines GridLines
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return GridLines.None;
				}
				return ((TableStyle)base.ControlStyle).GridLines;
			}
			set
			{
				((TableStyle)base.ControlStyle).GridLines = value;
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06003E7A RID: 15994 RVA: 0x000C946D File Offset: 0x000C766D
		// (set) Token: 0x06003E7B RID: 15995 RVA: 0x000C9475 File Offset: 0x000C7675
		internal bool HasRowSections
		{
			get
			{
				return this._hasRowSections;
			}
			set
			{
				this._hasRowSections = value;
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06003E7C RID: 15996 RVA: 0x0008588D File Offset: 0x00083A8D
		// (set) Token: 0x06003E7D RID: 15997 RVA: 0x000858A9 File Offset: 0x00083AA9
		[WebCategory("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		[WebSysDescription("Table_HorizontalAlign")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				return ((TableStyle)base.ControlStyle).HorizontalAlign;
			}
			set
			{
				((TableStyle)base.ControlStyle).HorizontalAlign = value;
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06003E7E RID: 15998 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06003E7F RID: 15999 RVA: 0x000C947E File Offset: 0x000C767E
		[MergableProperty(false)]
		[WebSysDescription("Table_Rows")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public virtual TableRowCollection Rows
		{
			get
			{
				if (this._rows == null)
				{
					this._rows = new TableRowCollection(this);
				}
				return this._rows;
			}
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x000C949C File Offset: 0x000C769C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.ControlStyleCreated && (base.EnableLegacyRendering || writer is Html32TextWriter))
			{
				Color borderColor = this.BorderColor;
				if (!borderColor.IsEmpty)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Bordercolor, ColorTranslator.ToHtml(borderColor));
				}
			}
			string value = "0";
			bool flag = false;
			if (base.ControlStyleCreated)
			{
				Unit borderWidth = this.BorderWidth;
				GridLines gridLines = this.GridLines;
				if (gridLines != GridLines.None)
				{
					if (borderWidth.IsEmpty || borderWidth.Type != UnitType.Pixel)
					{
						value = "1";
						flag = true;
					}
					else
					{
						value = ((int)borderWidth.Value).ToString(NumberFormatInfo.InvariantInfo);
					}
				}
			}
			if (this.RenderingCompatibility < VersionUtil.Framework40 || flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Border, value);
			}
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x000C9558 File Offset: 0x000C7758
		protected override ControlCollection CreateControlCollection()
		{
			return new Table.RowControlCollection(this);
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x0008E809 File Offset: 0x0008CA09
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState);
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x000C9560 File Offset: 0x000C7760
		protected virtual void RaisePostBackEvent(string argument)
		{
			base.ValidateEvent(this.UniqueID, argument);
			if (base.AdapterInternal != null)
			{
				IPostBackEventHandler postBackEventHandler = base.AdapterInternal as IPostBackEventHandler;
				if (postBackEventHandler != null)
				{
					postBackEventHandler.RaisePostBackEvent(argument);
				}
			}
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x000C9598 File Offset: 0x000C7798
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
			string caption = this.Caption;
			if (caption.Length != 0)
			{
				TableCaptionAlign captionAlign = this.CaptionAlign;
				if (captionAlign != TableCaptionAlign.NotSet)
				{
					string value = "Right";
					switch (captionAlign)
					{
					case TableCaptionAlign.Top:
						value = "Top";
						break;
					case TableCaptionAlign.Bottom:
						value = "Bottom";
						break;
					case TableCaptionAlign.Left:
						value = "Left";
						break;
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Align, value);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Caption);
				writer.Write(caption);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x000C9614 File Offset: 0x000C7814
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			TableRowCollection rows = this.Rows;
			int count = rows.Count;
			if (count > 0)
			{
				if (this.HasRowSections)
				{
					TableRowSection tableRowSection = TableRowSection.TableHeader;
					bool flag = false;
					foreach (object obj in rows)
					{
						TableRow tableRow = (TableRow)obj;
						if (tableRow.TableSection < tableRowSection)
						{
							throw new HttpException(SR.GetString("Table_SectionsMustBeInOrder", new object[]
							{
								this.ID
							}));
						}
						if (tableRowSection < tableRow.TableSection || (tableRow.TableSection == TableRowSection.TableHeader && !flag))
						{
							if (flag)
							{
								writer.RenderEndTag();
							}
							tableRowSection = tableRow.TableSection;
							flag = true;
							switch (tableRowSection)
							{
							case TableRowSection.TableHeader:
								writer.RenderBeginTag(HtmlTextWriterTag.Thead);
								break;
							case TableRowSection.TableBody:
								writer.RenderBeginTag(HtmlTextWriterTag.Tbody);
								break;
							case TableRowSection.TableFooter:
								writer.RenderBeginTag(HtmlTextWriterTag.Tfoot);
								break;
							}
						}
						tableRow.RenderControl(writer);
					}
					writer.RenderEndTag();
					return;
				}
				foreach (object obj2 in rows)
				{
					TableRow tableRow2 = (TableRow)obj2;
					tableRow2.RenderControl(writer);
				}
			}
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x000C9770 File Offset: 0x000C7970
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x0400240B RID: 9227
		private TableRowCollection _rows;

		// Token: 0x0400240C RID: 9228
		private bool _hasRowSections;

		// Token: 0x020009C7 RID: 2503
		protected class RowControlCollection : ControlCollection
		{
			// Token: 0x06006C60 RID: 27744 RVA: 0x00061D30 File Offset: 0x0005FF30
			internal RowControlCollection(Control owner) : base(owner)
			{
			}

			// Token: 0x06006C61 RID: 27745 RVA: 0x00183B4C File Offset: 0x00181D4C
			public override void Add(Control child)
			{
				if (child is TableRow)
				{
					base.Add(child);
					return;
				}
				throw new ArgumentException(SR.GetString("Cannot_Have_Children_Of_Type", new object[]
				{
					"Table",
					child.GetType().Name.ToString(CultureInfo.InvariantCulture)
				}));
			}

			// Token: 0x06006C62 RID: 27746 RVA: 0x00183BA0 File Offset: 0x00181DA0
			public override void AddAt(int index, Control child)
			{
				if (child is TableRow)
				{
					base.AddAt(index, child);
					return;
				}
				throw new ArgumentException(SR.GetString("Cannot_Have_Children_Of_Type", new object[]
				{
					"Table",
					child.GetType().Name.ToString(CultureInfo.InvariantCulture)
				}));
			}
		}
	}
}
