using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004E5 RID: 1253
	[Bindable(false)]
	[ControlBuilder(typeof(TableCellControlBuilder))]
	[DefaultProperty("Text")]
	[ParseChildren(false)]
	[ToolboxItem(false)]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class TableCell : WebControl
	{
		// Token: 0x06003E89 RID: 16009 RVA: 0x000C9779 File Offset: 0x000C7979
		public TableCell() : this(HtmlTextWriterTag.Td)
		{
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x000C9783 File Offset: 0x000C7983
		internal TableCell(HtmlTextWriterTag tagKey) : base(tagKey)
		{
			base.PreventAutoID();
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06003E8B RID: 16011 RVA: 0x000C9794 File Offset: 0x000C7994
		// (set) Token: 0x06003E8C RID: 16012 RVA: 0x000C97CC File Offset: 0x000C79CC
		[DefaultValue(null)]
		[TypeConverter(typeof(StringArrayConverter))]
		[WebCategory("Accessibility")]
		[WebSysDescription("TableCell_AssociatedHeaderCellID")]
		public virtual string[] AssociatedHeaderCellID
		{
			get
			{
				object obj = this.ViewState["AssociatedHeaderCellID"];
				if (obj == null)
				{
					return new string[0];
				}
				return (string[])((string[])obj).Clone();
			}
			set
			{
				if (value != null)
				{
					this.ViewState["AssociatedHeaderCellID"] = (string[])value.Clone();
					return;
				}
				this.ViewState["AssociatedHeaderCellID"] = null;
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06003E8D RID: 16013 RVA: 0x000C9800 File Offset: 0x000C7A00
		// (set) Token: 0x06003E8E RID: 16014 RVA: 0x000C9829 File Offset: 0x000C7A29
		[WebCategory("Appearance")]
		[DefaultValue(0)]
		[WebSysDescription("TableCell_ColumnSpan")]
		public virtual int ColumnSpan
		{
			get
			{
				object obj = this.ViewState["ColumnSpan"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["ColumnSpan"] = value;
			}
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06003E8F RID: 16015 RVA: 0x000C9850 File Offset: 0x000C7A50
		// (set) Token: 0x06003E90 RID: 16016 RVA: 0x000C986C File Offset: 0x000C7A6C
		[WebCategory("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		[WebSysDescription("TableItem_HorizontalAlign")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				return ((TableItemStyle)base.ControlStyle).HorizontalAlign;
			}
			set
			{
				((TableItemStyle)base.ControlStyle).HorizontalAlign = value;
			}
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06003E91 RID: 16017 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06003E92 RID: 16018 RVA: 0x000C9880 File Offset: 0x000C7A80
		// (set) Token: 0x06003E93 RID: 16019 RVA: 0x000C98A9 File Offset: 0x000C7AA9
		[WebCategory("Layout")]
		[DefaultValue(0)]
		[WebSysDescription("TableCell_RowSpan")]
		public virtual int RowSpan
		{
			get
			{
				object obj = this.ViewState["RowSpan"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["RowSpan"] = value;
			}
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06003E94 RID: 16020 RVA: 0x000C98D0 File Offset: 0x000C7AD0
		// (set) Token: 0x06003E95 RID: 16021 RVA: 0x000A9ECD File Offset: 0x000A80CD
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[WebSysDescription("TableCell_Text")]
		public virtual string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (this.HasControls())
				{
					this.Controls.Clear();
				}
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06003E96 RID: 16022 RVA: 0x000C98FD File Offset: 0x000C7AFD
		// (set) Token: 0x06003E97 RID: 16023 RVA: 0x000C9919 File Offset: 0x000C7B19
		[WebCategory("Layout")]
		[DefaultValue(VerticalAlign.NotSet)]
		[WebSysDescription("TableItem_VerticalAlign")]
		public virtual VerticalAlign VerticalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return VerticalAlign.NotSet;
				}
				return ((TableItemStyle)base.ControlStyle).VerticalAlign;
			}
			set
			{
				((TableItemStyle)base.ControlStyle).VerticalAlign = value;
			}
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x06003E98 RID: 16024 RVA: 0x000C992C File Offset: 0x000C7B2C
		// (set) Token: 0x06003E99 RID: 16025 RVA: 0x000C9948 File Offset: 0x000C7B48
		[WebCategory("Layout")]
		[DefaultValue(true)]
		[WebSysDescription("TableCell_Wrap")]
		public virtual bool Wrap
		{
			get
			{
				return !base.ControlStyleCreated || ((TableItemStyle)base.ControlStyle).Wrap;
			}
			set
			{
				((TableItemStyle)base.ControlStyle).Wrap = value;
			}
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x000C995C File Offset: 0x000C7B5C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			int num = this.ColumnSpan;
			if (num > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Colspan, num.ToString(NumberFormatInfo.InvariantInfo));
			}
			num = this.RowSpan;
			if (num > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Rowspan, num.ToString(NumberFormatInfo.InvariantInfo));
			}
			string[] associatedHeaderCellID = this.AssociatedHeaderCellID;
			if (associatedHeaderCellID.Length != 0)
			{
				bool flag = true;
				StringBuilder stringBuilder = new StringBuilder();
				Control namingContainer = this.NamingContainer;
				foreach (string text in associatedHeaderCellID)
				{
					TableHeaderCell tableHeaderCell = namingContainer.FindControl(text) as TableHeaderCell;
					if (tableHeaderCell == null)
					{
						throw new HttpException(SR.GetString("TableCell_AssociatedHeaderCellNotFound", new object[]
						{
							text
						}));
					}
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append(tableHeaderCell.ClientID);
				}
				string value = stringBuilder.ToString();
				if (!string.IsNullOrEmpty(value))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Headers, value);
				}
			}
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x000C9A54 File Offset: 0x000C7C54
		protected override void AddParsedSubObject(object obj)
		{
			if (this.HasControls())
			{
				base.AddParsedSubObject(obj);
				return;
			}
			if (obj is LiteralControl)
			{
				if (this._textSetByAddParsedSubObject)
				{
					this.Text += ((LiteralControl)obj).Text;
				}
				else
				{
					this.Text = ((LiteralControl)obj).Text;
				}
				this._textSetByAddParsedSubObject = true;
				return;
			}
			string text = this.Text;
			if (text.Length != 0)
			{
				this.Text = string.Empty;
				base.AddParsedSubObject(new LiteralControl(text));
			}
			base.AddParsedSubObject(obj);
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x000C9AE5 File Offset: 0x000C7CE5
		protected override Style CreateControlStyle()
		{
			return new TableItemStyle(this.ViewState);
		}

		// Token: 0x06003E9D RID: 16029 RVA: 0x000C9AF2 File Offset: 0x000C7CF2
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (base.HasRenderingData())
			{
				base.RenderContents(writer);
				return;
			}
			writer.Write(this.Text);
		}

		// Token: 0x04002413 RID: 9235
		private bool _textSetByAddParsedSubObject;
	}
}
