using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005BA RID: 1466
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HyperLinkColumn : DataGridColumn
	{
		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x060047A1 RID: 18337 RVA: 0x00124AE4 File Offset: 0x00123AE4
		// (set) Token: 0x060047A2 RID: 18338 RVA: 0x00124B11 File Offset: 0x00123B11
		[WebSysDescription("HyperLinkColumn_DataNavigateUrlField")]
		[DefaultValue("")]
		[WebCategory("Data")]
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

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x060047A3 RID: 18339 RVA: 0x00124B2C File Offset: 0x00123B2C
		// (set) Token: 0x060047A4 RID: 18340 RVA: 0x00124B59 File Offset: 0x00123B59
		[DefaultValue("")]
		[WebCategory("Data")]
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

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x060047A5 RID: 18341 RVA: 0x00124B74 File Offset: 0x00123B74
		// (set) Token: 0x060047A6 RID: 18342 RVA: 0x00124BA1 File Offset: 0x00123BA1
		[WebSysDescription("HyperLinkColumn_DataTextField")]
		[WebCategory("Data")]
		[DefaultValue("")]
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

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x060047A7 RID: 18343 RVA: 0x00124BBC File Offset: 0x00123BBC
		// (set) Token: 0x060047A8 RID: 18344 RVA: 0x00124BE9 File Offset: 0x00123BE9
		[DefaultValue("")]
		[Description("The formatting applied to the value bound to the Text property.")]
		[WebCategory("Data")]
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

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x060047A9 RID: 18345 RVA: 0x00124C04 File Offset: 0x00123C04
		// (set) Token: 0x060047AA RID: 18346 RVA: 0x00124C31 File Offset: 0x00123C31
		[WebCategory("Behavior")]
		[WebSysDescription("HyperLinkColumn_NavigateUrl")]
		[DefaultValue("")]
		[UrlProperty]
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

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x060047AB RID: 18347 RVA: 0x00124C4C File Offset: 0x00123C4C
		// (set) Token: 0x060047AC RID: 18348 RVA: 0x00124C79 File Offset: 0x00123C79
		[WebCategory("Behavior")]
		[TypeConverter(typeof(TargetConverter))]
		[WebSysDescription("HyperLink_Target")]
		[DefaultValue("")]
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

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x060047AD RID: 18349 RVA: 0x00124C94 File Offset: 0x00123C94
		// (set) Token: 0x060047AE RID: 18350 RVA: 0x00124CC1 File Offset: 0x00123CC1
		[DefaultValue("")]
		[WebSysDescription("HyperLinkColumn_Text")]
		[WebCategory("Appearance")]
		[Localizable(true)]
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

		// Token: 0x060047AF RID: 18351 RVA: 0x00124CDC File Offset: 0x00123CDC
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

		// Token: 0x060047B0 RID: 18352 RVA: 0x00124D28 File Offset: 0x00123D28
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

		// Token: 0x060047B1 RID: 18353 RVA: 0x00124D74 File Offset: 0x00123D74
		public override void Initialize()
		{
			base.Initialize();
			this.textFieldDesc = null;
			this.urlFieldDesc = null;
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x00124D8C File Offset: 0x00123D8C
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

		// Token: 0x060047B3 RID: 18355 RVA: 0x00124E0C File Offset: 0x00123E0C
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

		// Token: 0x04002AAA RID: 10922
		private PropertyDescriptor textFieldDesc;

		// Token: 0x04002AAB RID: 10923
		private PropertyDescriptor urlFieldDesc;
	}
}
