using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020011F8 RID: 4600
	public class TreeListImageColumn : TreeListColumn, IDisposable
	{
		// Token: 0x17003D3B RID: 15675
		// (get) Token: 0x0600BDD8 RID: 48600 RVA: 0x002A0E04 File Offset: 0x0029F004
		// (set) Token: 0x0600BDD9 RID: 48601 RVA: 0x002A0E38 File Offset: 0x0029F038
		[DefaultValue(typeof(string))]
		[TypeConverter(typeof(GridDataTypeConverter))]
		[NotifyParentProperty(true)]
		public Type DataType
		{
			get
			{
				object obj = base.ViewState["DataType"];
				if (obj == null)
				{
					obj = typeof(string);
				}
				return (Type)obj;
			}
			set
			{
				value = TreeListTypeHelper.GetNonNullableType(value);
				if (!GridDataTypeConverter.SupportedTypes.Contains(value) && !value.IsEnum)
				{
					throw new NotSupportedException("Specified column DataType is not supported " + value.ToString());
				}
				base.ViewState["DataType"] = value;
			}
		}

		// Token: 0x17003D3C RID: 15676
		// (get) Token: 0x0600BDDA RID: 48602 RVA: 0x002A0E8C File Offset: 0x0029F08C
		// (set) Token: 0x0600BDDB RID: 48603 RVA: 0x002A0EBA File Offset: 0x0029F0BA
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[TypeConverter(typeof(GridStringArrayConverter))]
		[Description("ImageColumn_DataImageUrlFields")]
		public virtual string[] DataImageUrlFields
		{
			get
			{
				object obj = base.ViewState["DataImageUrlFields"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
			set
			{
				base.ViewState["DataImageUrlFields"] = value;
			}
		}

		// Token: 0x17003D3D RID: 15677
		// (get) Token: 0x0600BDDC RID: 48604 RVA: 0x002A0ED0 File Offset: 0x0029F0D0
		// (set) Token: 0x0600BDDD RID: 48605 RVA: 0x002A0EFD File Offset: 0x0029F0FD
		[NotifyParentProperty(true)]
		[Description("The formatting applied to the value bound to the NavigateUrl property.")]
		[DefaultValue("")]
		[Category("Data")]
		[Localizable(true)]
		public virtual string DataImageUrlFormatString
		{
			get
			{
				object obj = base.ViewState["DataImageUrlFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataImageUrlFormatString"] = value;
			}
		}

		// Token: 0x17003D3E RID: 15678
		// (get) Token: 0x0600BDDE RID: 48606 RVA: 0x002A0F10 File Offset: 0x0029F110
		// (set) Token: 0x0600BDDF RID: 48607 RVA: 0x002A0F3D File Offset: 0x0029F13D
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("ImageColumn_ImageUrl")]
		[DefaultValue("")]
		[UrlProperty("*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png")]
		[Localizable(true)]
		public virtual string ImageUrl
		{
			get
			{
				object obj = base.ViewState["ImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17003D3F RID: 15679
		// (get) Token: 0x0600BDE0 RID: 48608 RVA: 0x002A0F50 File Offset: 0x0029F150
		// (set) Token: 0x0600BDE1 RID: 48609 RVA: 0x002A0F79 File Offset: 0x0029F179
		[Description("AllowSorting")]
		[DefaultValue(true)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual bool AllowSorting
		{
			get
			{
				object obj = base.ViewState["_as"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["_as"] = value;
			}
		}

		// Token: 0x17003D40 RID: 15680
		// (get) Token: 0x0600BDE2 RID: 48610 RVA: 0x002A0F91 File Offset: 0x0029F191
		protected override bool Sortable
		{
			get
			{
				return this.AllowSorting;
			}
		}

		// Token: 0x17003D41 RID: 15681
		// (get) Token: 0x0600BDE3 RID: 48611 RVA: 0x002A0F9C File Offset: 0x0029F19C
		// (set) Token: 0x0600BDE4 RID: 48612 RVA: 0x002A0FC9 File Offset: 0x0029F1C9
		[NotifyParentProperty(true)]
		[Description("ImageColumn_AlternateText")]
		[DefaultValue("")]
		[Category("Behavior")]
		[Localizable(true)]
		public virtual string AlternateText
		{
			get
			{
				object obj = base.ViewState["AlternateText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["AlternateText"] = value;
			}
		}

		// Token: 0x17003D42 RID: 15682
		// (get) Token: 0x0600BDE5 RID: 48613 RVA: 0x002A0FDC File Offset: 0x0029F1DC
		// (set) Token: 0x0600BDE6 RID: 48614 RVA: 0x002A1005 File Offset: 0x0029F205
		[Description("ImageColumn_ImageAlign")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(ImageAlign), "NotSet")]
		[Category("Behavior")]
		public virtual ImageAlign ImageAlign
		{
			get
			{
				object obj = base.ViewState["ImageAlign"];
				if (obj != null)
				{
					return (ImageAlign)obj;
				}
				return ImageAlign.NotSet;
			}
			set
			{
				base.ViewState["ImageAlign"] = value;
			}
		}

		// Token: 0x17003D43 RID: 15683
		// (get) Token: 0x0600BDE7 RID: 48615 RVA: 0x002A1020 File Offset: 0x0029F220
		// (set) Token: 0x0600BDE8 RID: 48616 RVA: 0x002A1052 File Offset: 0x0029F252
		[Category("Behavior")]
		[Description("ImageColumn's image width")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("")]
		public Unit ImageWidth
		{
			get
			{
				object obj = base.ViewState["ImageWidth"];
				if (obj == null)
				{
					obj = Unit.Empty;
				}
				return (Unit)obj;
			}
			set
			{
				base.ViewState["ImageWidth"] = value;
			}
		}

		// Token: 0x17003D44 RID: 15684
		// (get) Token: 0x0600BDE9 RID: 48617 RVA: 0x002A106C File Offset: 0x0029F26C
		// (set) Token: 0x0600BDEA RID: 48618 RVA: 0x002A109E File Offset: 0x0029F29E
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("ImageColumn's image height")]
		[DefaultValue("")]
		[Localizable(true)]
		public Unit ImageHeight
		{
			get
			{
				object obj = base.ViewState["ImageHeight"];
				if (obj == null)
				{
					obj = Unit.Empty;
				}
				return (Unit)obj;
			}
			set
			{
				base.ViewState["ImageHeight"] = value;
			}
		}

		// Token: 0x17003D45 RID: 15685
		// (get) Token: 0x0600BDEB RID: 48619 RVA: 0x002A10B8 File Offset: 0x0029F2B8
		// (set) Token: 0x0600BDEC RID: 48620 RVA: 0x002A10E5 File Offset: 0x0029F2E5
		[NotifyParentProperty(true)]
		[Description("ImageColumn_DataAlternateTextField")]
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataAlternateTextField
		{
			get
			{
				object obj = base.ViewState["DataAlternateTextField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataAlternateTextField"] = value;
				base.UpdateUniqueNameIfDefault(value);
			}
		}

		// Token: 0x17003D46 RID: 15686
		// (get) Token: 0x0600BDED RID: 48621 RVA: 0x002A1100 File Offset: 0x0029F300
		// (set) Token: 0x0600BDEE RID: 48622 RVA: 0x002A112D File Offset: 0x0029F32D
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[Description("The formatting applied to the value bound to the AlternateText property.")]
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataAlternateTextFormatString
		{
			get
			{
				object obj = base.ViewState["DataAlternateTextFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataAlternateTextFormatString"] = value;
			}
		}

		// Token: 0x0600BDEF RID: 48623 RVA: 0x002A1140 File Offset: 0x0029F340
		protected virtual string FormatDataImageUrlValue(object[] dataUrlValues)
		{
			for (int i = 0; i < dataUrlValues.Length; i++)
			{
				if (dataUrlValues[i] == null || dataUrlValues[i] == DBNull.Value)
				{
					dataUrlValues[i] = string.Empty;
				}
			}
			string dataImageUrlFormatString = this.DataImageUrlFormatString;
			if (dataImageUrlFormatString.Length == 0)
			{
				return dataUrlValues[0].ToString();
			}
			string result = string.Empty;
			try
			{
				result = string.Format(dataImageUrlFormatString, dataUrlValues);
			}
			catch (Exception)
			{
				throw new FormatException("Illegal DataNavigateUrlFormatString for column: " + this.UniqueName);
			}
			return result;
		}

		// Token: 0x0600BDF0 RID: 48624 RVA: 0x002A11C4 File Offset: 0x0029F3C4
		protected virtual string FormatDataAlternativeTextValue(object dataTextValue)
		{
			string empty = string.Empty;
			if (dataTextValue == null || dataTextValue == DBNull.Value)
			{
				return empty;
			}
			string dataAlternateTextFormatString = this.DataAlternateTextFormatString;
			if (dataAlternateTextFormatString.Length == 0)
			{
				return dataTextValue.ToString();
			}
			return string.Format(dataAlternateTextFormatString, dataTextValue);
		}

		// Token: 0x0600BDF1 RID: 48625 RVA: 0x002A1204 File Offset: 0x0029F404
		protected override void InitializeDataCells(TableCell cell, int columnIndex, TreeListDataItem inItem)
		{
			this.image = new Image();
			this.image.ID = string.Format("img{0}", this.UniqueName);
			this.image.ImageUrl = this.ImageUrl;
			this.image.AlternateText = this.AlternateText;
			this.image.ToolTip = this.AlternateText;
			this.image.ImageAlign = this.ImageAlign;
			this.image.Width = this.ImageWidth;
			this.image.Height = this.ImageHeight;
			if (this.DataImageUrlFields.Length != 0 || this.DataAlternateTextField.Length != 0)
			{
				cell.DataBinding += this.OnColumnDataCellBinding;
			}
			cell.Controls.Add(this.image);
		}

		// Token: 0x0600BDF2 RID: 48626 RVA: 0x002A12D8 File Offset: 0x0029F4D8
		protected void OnColumnDataCellBinding(object sender, EventArgs e)
		{
			TableCell control = (TableCell)sender;
			TreeListDataItem treeListDataItem = (TreeListDataItem)TreeListColumn.GetBindingParentItem(control);
			object dataItem = treeListDataItem.DataItem;
			object obj = null;
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			if (this._textFieldDesc == null || this._urlFieldsDesc == null)
			{
				propertyDescriptorCollection = TypeDescriptor.GetProperties(dataItem);
			}
			if (this._textFieldDesc == null)
			{
				string dataAlternateTextField = this.DataAlternateTextField;
				if (dataAlternateTextField.Length != 0)
				{
					if (propertyDescriptorCollection != null)
					{
						this._textFieldDesc = propertyDescriptorCollection.Find(dataAlternateTextField, true);
					}
					if (this._textFieldDesc == null && !base.Owner.IsDesignMode)
					{
						obj = this.ExtractPropertyValue(dataItem, dataAlternateTextField);
					}
				}
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (this._urlFieldsDesc != null && this.DataImageUrlFields.Length > 0)
			{
				ArrayList arrayList = new ArrayList(this.DataImageUrlFields);
				foreach (object obj2 in arrayList)
				{
					string text = (string)obj2;
					if (text.Length != 0)
					{
						PropertyDescriptor propertyDescriptor = this._urlFieldsDesc.Find(text, true);
						if (propertyDescriptor == null)
						{
							if (propertyDescriptorCollection != null)
							{
								propertyDescriptor = propertyDescriptorCollection.Find(text, true);
								if (propertyDescriptor != null)
								{
									this._urlFieldsDesc.Add(propertyDescriptor);
									dictionary.Add(text, propertyDescriptor);
								}
							}
						}
						else
						{
							dictionary.Add(text, propertyDescriptor);
						}
						if (propertyDescriptor == null && !base.Owner.IsDesignMode && !string.IsNullOrEmpty(text))
						{
							object value = this.ExtractPropertyValue(dataItem, text);
							dictionary.Add(text, value);
						}
					}
				}
			}
			if (this._textFieldDesc != null)
			{
				object value2 = this._textFieldDesc.GetValue(dataItem);
				this.image.AlternateText = this.FormatDataAlternativeTextValue(value2);
				this.image.ToolTip = this.image.AlternateText;
			}
			else if (this._textFieldDesc == null && !base.Owner.IsDesignMode && obj != null)
			{
				this.image.AlternateText = this.FormatDataAlternativeTextValue(obj);
				this.image.ToolTip = this.image.AlternateText;
			}
			else if (base.Owner.IsDesignMode && this.DataAlternateTextField.Length != 0)
			{
				this.image.AlternateText = "TreeListImageColumn";
			}
			if (dictionary.Count > 0)
			{
				object[] array = new object[dictionary.Count];
				int num = 0;
				foreach (string text2 in this.DataImageUrlFields)
				{
					if (dictionary.ContainsKey(text2) && dataItem != null)
					{
						if (dictionary[text2] is PropertyDescriptor)
						{
							try
							{
								array[num] = ((PropertyDescriptor)dictionary[text2]).GetValue(dataItem);
								goto IL_2AD;
							}
							catch
							{
								array[num] = this.ExtractPropertyValue(dataItem, text2);
								goto IL_2AD;
							}
						}
						array[num] = dictionary[text2];
					}
					IL_2AD:
					num++;
				}
				this.image.ImageUrl = this.FormatDataImageUrlValue(array);
				return;
			}
			if (base.Owner.IsDesignMode && this.DataImageUrlFields.Length != 0)
			{
				this.image.ImageUrl = "url";
			}
		}

		// Token: 0x0600BDF3 RID: 48627 RVA: 0x002A1600 File Offset: 0x0029F800
		protected override string GetSortExpression()
		{
			if (string.IsNullOrEmpty(this.SortExpression) && !string.IsNullOrEmpty(this.DataAlternateTextField) && this.AllowSorting)
			{
				return this.DataAlternateTextField;
			}
			return base.GetSortExpression();
		}

		// Token: 0x0600BDF4 RID: 48628 RVA: 0x002A1634 File Offset: 0x0029F834
		private object ExtractPropertyValue(object obj1, string dataFieldName)
		{
			object result = null;
			if (!string.IsNullOrEmpty(dataFieldName))
			{
				if (dataFieldName.IndexOf(".") > -1)
				{
					try
					{
						return DataBinder.Eval(obj1, dataFieldName);
					}
					catch
					{
						if (!TreeListTypeHelper.IsBindableType(obj1.GetType()))
						{
							result = null;
						}
						return result;
					}
				}
				try
				{
					result = DataBinder.GetPropertyValue(obj1, dataFieldName);
				}
				catch
				{
					try
					{
						result = DataBinder.Eval(obj1, dataFieldName);
					}
					catch
					{
						if (!TreeListTypeHelper.IsBindableType(obj1.GetType()))
						{
							result = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600BDF5 RID: 48629 RVA: 0x002A16C8 File Offset: 0x0029F8C8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600BDF6 RID: 48630 RVA: 0x002A16D1 File Offset: 0x0029F8D1
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.image != null)
			{
				this.image.Dispose();
			}
		}

		// Token: 0x040031F3 RID: 12787
		protected const string SuportedExtensions = "*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png";

		// Token: 0x040031F4 RID: 12788
		private Image image;

		// Token: 0x040031F5 RID: 12789
		private PropertyDescriptor _textFieldDesc;

		// Token: 0x040031F6 RID: 12790
		private PropertyDescriptorCollection _urlFieldsDesc = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
	}
}
