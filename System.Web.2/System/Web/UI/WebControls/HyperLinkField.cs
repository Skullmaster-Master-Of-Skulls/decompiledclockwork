using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000438 RID: 1080
	public class HyperLinkField : DataControlField
	{
		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06003457 RID: 13399 RVA: 0x000AA57C File Offset: 0x000A877C
		// (set) Token: 0x06003458 RID: 13400 RVA: 0x000AA5B4 File Offset: 0x000A87B4
		[WebCategory("Data")]
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(StringArrayConverter))]
		[WebSysDescription("HyperLinkField_DataNavigateUrlFields")]
		public virtual string[] DataNavigateUrlFields
		{
			get
			{
				object obj = base.ViewState["DataNavigateUrlFields"];
				if (obj != null)
				{
					return (string[])((string[])obj).Clone();
				}
				return new string[0];
			}
			set
			{
				string[] arr = base.ViewState["DataNavigateUrlFields"] as string[];
				if (!this.StringArraysEqual(arr, value))
				{
					if (value != null)
					{
						base.ViewState["DataNavigateUrlFields"] = (string[])value.Clone();
					}
					else
					{
						base.ViewState["DataNavigateUrlFields"] = null;
					}
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06003459 RID: 13401 RVA: 0x000AA618 File Offset: 0x000A8818
		// (set) Token: 0x0600345A RID: 13402 RVA: 0x000AA645 File Offset: 0x000A8845
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("HyperLinkField_DataNavigateUrlFormatString")]
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
				if (!object.Equals(value, base.ViewState["DataNavigateUrlFormatString"]))
				{
					base.ViewState["DataNavigateUrlFormatString"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x0600345B RID: 13403 RVA: 0x000AA678 File Offset: 0x000A8878
		// (set) Token: 0x0600345C RID: 13404 RVA: 0x000886F5 File Offset: 0x000868F5
		[WebCategory("Data")]
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("HyperLinkField_DataTextField")]
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
				if (!object.Equals(value, base.ViewState["DataTextField"]))
				{
					base.ViewState["DataTextField"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x0600345D RID: 13405 RVA: 0x000AA6A8 File Offset: 0x000A88A8
		// (set) Token: 0x0600345E RID: 13406 RVA: 0x000AA6D5 File Offset: 0x000A88D5
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("HyperLinkField_DataTextFormatString")]
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
				if (!object.Equals(value, base.ViewState["DataTextFormatString"]))
				{
					base.ViewState["DataTextFormatString"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x0600345F RID: 13407 RVA: 0x000AA708 File Offset: 0x000A8908
		// (set) Token: 0x06003460 RID: 13408 RVA: 0x000AA735 File Offset: 0x000A8935
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("HyperLinkField_NavigateUrl")]
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
				if (!object.Equals(value, base.ViewState["NavigateUrl"]))
				{
					base.ViewState["NavigateUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06003461 RID: 13409 RVA: 0x000AA768 File Offset: 0x000A8968
		// (set) Token: 0x06003462 RID: 13410 RVA: 0x000AA795 File Offset: 0x000A8995
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
				if (!object.Equals(value, base.ViewState["Target"]))
				{
					base.ViewState["Target"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06003463 RID: 13411 RVA: 0x000AA7C8 File Offset: 0x000A89C8
		// (set) Token: 0x06003464 RID: 13412 RVA: 0x00088815 File Offset: 0x00086A15
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("HyperLinkField_Text")]
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
				if (!object.Equals(value, base.ViewState["Text"]))
				{
					base.ViewState["Text"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x000AA7F8 File Offset: 0x000A89F8
		protected override void CopyProperties(DataControlField newField)
		{
			((HyperLinkField)newField).DataNavigateUrlFields = this.DataNavigateUrlFields;
			((HyperLinkField)newField).DataNavigateUrlFormatString = this.DataNavigateUrlFormatString;
			((HyperLinkField)newField).DataTextField = this.DataTextField;
			((HyperLinkField)newField).DataTextFormatString = this.DataTextFormatString;
			((HyperLinkField)newField).NavigateUrl = this.NavigateUrl;
			((HyperLinkField)newField).Target = this.Target;
			((HyperLinkField)newField).Text = this.Text;
			base.CopyProperties(newField);
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x000AA883 File Offset: 0x000A8A83
		protected override DataControlField CreateField()
		{
			return new HyperLinkField();
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000AA88C File Offset: 0x000A8A8C
		protected virtual string FormatDataNavigateUrlValue(object[] dataUrlValues)
		{
			string result = string.Empty;
			if (dataUrlValues != null)
			{
				string dataNavigateUrlFormatString = this.DataNavigateUrlFormatString;
				if (dataNavigateUrlFormatString.Length == 0)
				{
					if (dataUrlValues.Length != 0 && !DataBinder.IsNull(dataUrlValues[0]))
					{
						result = dataUrlValues[0].ToString();
					}
				}
				else
				{
					result = string.Format(CultureInfo.CurrentCulture, dataNavigateUrlFormatString, dataUrlValues);
				}
			}
			return result;
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x000AA8D8 File Offset: 0x000A8AD8
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

		// Token: 0x06003469 RID: 13417 RVA: 0x000AA922 File Offset: 0x000A8B22
		public override bool Initialize(bool enableSorting, Control control)
		{
			base.Initialize(enableSorting, control);
			this.textFieldDesc = null;
			this.urlFieldDescs = null;
			return false;
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x000AA93C File Offset: 0x000A8B3C
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			if (cellType == DataControlCellType.DataCell)
			{
				HyperLink hyperLink = new HyperLink();
				hyperLink.Text = this.Text;
				hyperLink.NavigateUrl = this.NavigateUrl;
				hyperLink.Target = this.Target;
				if ((rowState & DataControlRowState.Insert) == DataControlRowState.Normal && base.Visible)
				{
					if (this.DataNavigateUrlFields.Length != 0 || this.DataTextField.Length != 0)
					{
						hyperLink.DataBinding += this.OnDataBindField;
					}
					cell.Controls.Add(hyperLink);
				}
			}
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x000AA9C4 File Offset: 0x000A8BC4
		private void OnDataBindField(object sender, EventArgs e)
		{
			HyperLink hyperLink = (HyperLink)sender;
			Control namingContainer = hyperLink.NamingContainer;
			if (namingContainer == null)
			{
				throw new HttpException(SR.GetString("DataControlField_NoContainer"));
			}
			object dataItem = DataBinder.GetDataItem(namingContainer);
			if (dataItem == null && !base.DesignMode)
			{
				throw new HttpException(SR.GetString("DataItem_Not_Found"));
			}
			if (this.textFieldDesc == null && this.urlFieldDescs == null)
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
				string[] dataNavigateUrlFields = this.DataNavigateUrlFields;
				int num = dataNavigateUrlFields.Length;
				this.urlFieldDescs = new PropertyDescriptor[num];
				for (int i = 0; i < num; i++)
				{
					text = dataNavigateUrlFields[i];
					if (text.Length != 0)
					{
						this.urlFieldDescs[i] = properties.Find(text, true);
						if (this.urlFieldDescs[i] == null && !base.DesignMode)
						{
							throw new HttpException(SR.GetString("Field_Not_Found", new object[]
							{
								text
							}));
						}
					}
				}
			}
			string text2 = string.Empty;
			if (this.textFieldDesc != null && dataItem != null)
			{
				object value = this.textFieldDesc.GetValue(dataItem);
				text2 = this.FormatDataTextValue(value);
			}
			if (base.DesignMode && this.DataTextField.Length != 0 && text2.Length == 0)
			{
				text2 = SR.GetString("Sample_Databound_Text");
			}
			if (text2.Length > 0)
			{
				hyperLink.Text = text2;
			}
			int num2 = this.urlFieldDescs.Length;
			string text3 = string.Empty;
			if (this.urlFieldDescs != null && num2 > 0 && dataItem != null)
			{
				object[] array = new object[num2];
				for (int j = 0; j < num2; j++)
				{
					if (this.urlFieldDescs[j] != null)
					{
						array[j] = this.urlFieldDescs[j].GetValue(dataItem);
					}
				}
				string text4 = this.FormatDataNavigateUrlValue(array);
				if (!CrossSiteScriptingValidation.IsDangerousUrl(text4))
				{
					text3 = text4;
				}
			}
			if (base.DesignMode && this.DataNavigateUrlFields.Length != 0 && text3.Length == 0)
			{
				text3 = "url";
			}
			if (text3.Length > 0)
			{
				hyperLink.NavigateUrl = text3;
			}
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x000AAC04 File Offset: 0x000A8E04
		private bool StringArraysEqual(string[] arr1, string[] arr2)
		{
			if (arr1 == null && arr2 == null)
			{
				return true;
			}
			if (arr1 == null || arr2 == null)
			{
				return false;
			}
			if (arr1.Length != arr2.Length)
			{
				return false;
			}
			for (int i = 0; i < arr1.Length; i++)
			{
				if (!string.Equals(arr1[i], arr2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x00006164 File Offset: 0x00004364
		public override void ValidateSupportsCallback()
		{
		}

		// Token: 0x0400219B RID: 8603
		private PropertyDescriptor textFieldDesc;

		// Token: 0x0400219C RID: 8604
		private PropertyDescriptor[] urlFieldDescs;
	}
}
