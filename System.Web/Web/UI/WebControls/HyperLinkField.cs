using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005BB RID: 1467
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HyperLinkField : DataControlField
	{
		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x060047B5 RID: 18357 RVA: 0x00124F90 File Offset: 0x00123F90
		// (set) Token: 0x060047B6 RID: 18358 RVA: 0x00124FC8 File Offset: 0x00123FC8
		[WebSysDescription("HyperLinkField_DataNavigateUrlFields")]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(StringArrayConverter))]
		[WebCategory("Data")]
		[DefaultValue(null)]
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

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x060047B7 RID: 18359 RVA: 0x0012502C File Offset: 0x0012402C
		// (set) Token: 0x060047B8 RID: 18360 RVA: 0x00125059 File Offset: 0x00124059
		[WebCategory("Data")]
		[WebSysDescription("HyperLinkField_DataNavigateUrlFormatString")]
		[DefaultValue("")]
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

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x060047B9 RID: 18361 RVA: 0x0012508C File Offset: 0x0012408C
		// (set) Token: 0x060047BA RID: 18362 RVA: 0x001250B9 File Offset: 0x001240B9
		[WebSysDescription("HyperLinkField_DataTextField")]
		[WebCategory("Data")]
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x060047BB RID: 18363 RVA: 0x001250EC File Offset: 0x001240EC
		// (set) Token: 0x060047BC RID: 18364 RVA: 0x00125119 File Offset: 0x00124119
		[WebCategory("Data")]
		[WebSysDescription("HyperLinkField_DataTextFormatString")]
		[DefaultValue("")]
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

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x060047BD RID: 18365 RVA: 0x0012514C File Offset: 0x0012414C
		// (set) Token: 0x060047BE RID: 18366 RVA: 0x00125179 File Offset: 0x00124179
		[UrlProperty]
		[WebSysDescription("HyperLinkField_NavigateUrl")]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
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

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x060047BF RID: 18367 RVA: 0x001251AC File Offset: 0x001241AC
		// (set) Token: 0x060047C0 RID: 18368 RVA: 0x001251D9 File Offset: 0x001241D9
		[TypeConverter(typeof(TargetConverter))]
		[WebSysDescription("HyperLink_Target")]
		[WebCategory("Behavior")]
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
				if (!object.Equals(value, base.ViewState["Target"]))
				{
					base.ViewState["Target"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x060047C1 RID: 18369 RVA: 0x0012520C File Offset: 0x0012420C
		// (set) Token: 0x060047C2 RID: 18370 RVA: 0x00125239 File Offset: 0x00124239
		[WebSysDescription("HyperLinkField_Text")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
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

		// Token: 0x060047C3 RID: 18371 RVA: 0x0012526C File Offset: 0x0012426C
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

		// Token: 0x060047C4 RID: 18372 RVA: 0x001252F7 File Offset: 0x001242F7
		protected override DataControlField CreateField()
		{
			return new HyperLinkField();
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x00125300 File Offset: 0x00124300
		protected virtual string FormatDataNavigateUrlValue(object[] dataUrlValues)
		{
			string result = string.Empty;
			if (dataUrlValues != null)
			{
				string dataNavigateUrlFormatString = this.DataNavigateUrlFormatString;
				if (dataNavigateUrlFormatString.Length == 0)
				{
					if (dataUrlValues.Length > 0 && !DataBinder.IsNull(dataUrlValues[0]))
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

		// Token: 0x060047C6 RID: 18374 RVA: 0x00125350 File Offset: 0x00124350
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

		// Token: 0x060047C7 RID: 18375 RVA: 0x0012539C File Offset: 0x0012439C
		public override bool Initialize(bool enableSorting, Control control)
		{
			base.Initialize(enableSorting, control);
			this.textFieldDesc = null;
			this.urlFieldDescs = null;
			return false;
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x001253B8 File Offset: 0x001243B8
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

		// Token: 0x060047C9 RID: 18377 RVA: 0x00125440 File Offset: 0x00124440
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

		// Token: 0x060047CA RID: 18378 RVA: 0x0012568C File Offset: 0x0012468C
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

		// Token: 0x060047CB RID: 18379 RVA: 0x001256D1 File Offset: 0x001246D1
		public override void ValidateSupportsCallback()
		{
		}

		// Token: 0x04002AAC RID: 10924
		private PropertyDescriptor textFieldDesc;

		// Token: 0x04002AAD RID: 10925
		private PropertyDescriptor[] urlFieldDescs;
	}
}
