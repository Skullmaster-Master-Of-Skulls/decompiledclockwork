using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000437 RID: 1079
	public class HyperLinkColumn : DataGridColumn
	{
		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06003443 RID: 13379 RVA: 0x000AA114 File Offset: 0x000A8314
		// (set) Token: 0x06003444 RID: 13380 RVA: 0x000AA141 File Offset: 0x000A8341
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("HyperLinkColumn_DataNavigateUrlField")]
		public virtual string DataNavigateUrlField
		{
			get
			{
				object obj = base.ViewState["DataNavigateUrlField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataNavigateUrlField"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06003445 RID: 13381 RVA: 0x000AA15C File Offset: 0x000A835C
		// (set) Token: 0x06003446 RID: 13382 RVA: 0x000AA189 File Offset: 0x000A8389
		[WebCategory("Data")]
		[DefaultValue("")]
		[Description("The formatting applied to the value bound to the NavigateUrl property.")]
		public virtual string DataNavigateUrlFormatString
		{
			get
			{
				object obj = base.ViewState["DataNavigateUrlFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataNavigateUrlFormatString"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06003447 RID: 13383 RVA: 0x000AA1A4 File Offset: 0x000A83A4
		// (set) Token: 0x06003448 RID: 13384 RVA: 0x00088381 File Offset: 0x00086581
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("HyperLinkColumn_DataTextField")]
		public virtual string DataTextField
		{
			get
			{
				object obj = base.ViewState["DataTextField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataTextField"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06003449 RID: 13385 RVA: 0x000AA1D4 File Offset: 0x000A83D4
		// (set) Token: 0x0600344A RID: 13386 RVA: 0x000AA201 File Offset: 0x000A8401
		[WebCategory("Data")]
		[DefaultValue("")]
		[Description("The formatting applied to the value bound to the Text property.")]
		public virtual string DataTextFormatString
		{
			get
			{
				object obj = base.ViewState["DataTextFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataTextFormatString"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x0600344B RID: 13387 RVA: 0x000AA21C File Offset: 0x000A841C
		// (set) Token: 0x0600344C RID: 13388 RVA: 0x000AA249 File Offset: 0x000A8449
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[UrlProperty]
		[WebSysDescription("HyperLinkColumn_NavigateUrl")]
		public virtual string NavigateUrl
		{
			get
			{
				object obj = base.ViewState["NavigateUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["NavigateUrl"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x0600344D RID: 13389 RVA: 0x000AA264 File Offset: 0x000A8464
		// (set) Token: 0x0600344E RID: 13390 RVA: 0x000AA291 File Offset: 0x000A8491
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[TypeConverter(typeof(TargetConverter))]
		[WebSysDescription("HyperLink_Target")]
		public virtual string Target
		{
			get
			{
				object obj = base.ViewState["Target"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["Target"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x0600344F RID: 13391 RVA: 0x000AA2AC File Offset: 0x000A84AC
		// (set) Token: 0x06003450 RID: 13392 RVA: 0x00088411 File Offset: 0x00086611
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("HyperLinkColumn_Text")]
		public virtual string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["Text"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000AA2DC File Offset: 0x000A84DC
		protected virtual string FormatDataNavigateUrlValue(object dataUrlValue)
		{
			string result = string.Empty;
			if (!DataBinder.IsNull(dataUrlValue))
			{
				string dataNavigateUrlFormatString = this.DataNavigateUrlFormatString;
				if (dataNavigateUrlFormatString.Length == 0)
				{
					result = dataUrlValue.ToString();
				}
				else
				{
					result = string.Format(CultureInfo.CurrentCulture, dataNavigateUrlFormatString, new object[]
					{
						dataUrlValue
					});
				}
			}
			return result;
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000AA328 File Offset: 0x000A8528
		protected virtual string FormatDataTextValue(object dataTextValue)
		{
			string result = string.Empty;
			if (!DataBinder.IsNull(dataTextValue))
			{
				string dataTextFormatString = this.DataTextFormatString;
				if (dataTextFormatString.Length == 0)
				{
					result = dataTextValue.ToString();
				}
				else
				{
					result = string.Format(CultureInfo.CurrentCulture, dataTextFormatString, new object[]
					{
						dataTextValue
					});
				}
			}
			return result;
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000AA372 File Offset: 0x000A8572
		public override void Initialize()
		{
			base.Initialize();
			this.textFieldDesc = null;
			this.urlFieldDesc = null;
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000AA388 File Offset: 0x000A8588
		public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			base.InitializeCell(cell, columnIndex, itemType);
			if (itemType != ListItemType.Header && itemType != ListItemType.Footer)
			{
				HyperLink hyperLink = new HyperLink();
				hyperLink.Text = this.Text;
				hyperLink.NavigateUrl = this.NavigateUrl;
				hyperLink.Target = this.Target;
				if (this.DataNavigateUrlField.Length != 0 || this.DataTextField.Length != 0)
				{
					hyperLink.DataBinding += this.OnDataBindColumn;
				}
				cell.Controls.Add(hyperLink);
			}
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000AA408 File Offset: 0x000A8608
		private void OnDataBindColumn(object sender, EventArgs e)
		{
			HyperLink hyperLink = (HyperLink)sender;
			DataGridItem dataGridItem = (DataGridItem)hyperLink.NamingContainer;
			object dataItem = dataGridItem.DataItem;
			if (this.textFieldDesc == null && this.urlFieldDesc == null)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItem);
				string text = this.DataTextField;
				if (text.Length != 0)
				{
					this.textFieldDesc = properties.Find(text, true);
					if (this.textFieldDesc == null && !base.DesignMode)
					{
						throw new HttpException(SR.GetString("Field_Not_Found", new object[]
						{
							text
						}));
					}
				}
				text = this.DataNavigateUrlField;
				if (text.Length != 0)
				{
					this.urlFieldDesc = properties.Find(text, true);
					if (this.urlFieldDesc == null && !base.DesignMode)
					{
						throw new HttpException(SR.GetString("Field_Not_Found", new object[]
						{
							text
						}));
					}
				}
			}
			if (this.textFieldDesc != null)
			{
				object value = this.textFieldDesc.GetValue(dataItem);
				string text2 = this.FormatDataTextValue(value);
				hyperLink.Text = text2;
			}
			else if (base.DesignMode && this.DataTextField.Length != 0)
			{
				hyperLink.Text = SR.GetString("Sample_Databound_Text");
			}
			if (this.urlFieldDesc != null)
			{
				object value2 = this.urlFieldDesc.GetValue(dataItem);
				string navigateUrl = this.FormatDataNavigateUrlValue(value2);
				hyperLink.NavigateUrl = navigateUrl;
				return;
			}
			if (base.DesignMode && this.DataNavigateUrlField.Length != 0)
			{
				hyperLink.NavigateUrl = "url";
			}
		}

		// Token: 0x04002199 RID: 8601
		private PropertyDescriptor textFieldDesc;

		// Token: 0x0400219A RID: 8602
		private PropertyDescriptor urlFieldDesc;
	}
}
