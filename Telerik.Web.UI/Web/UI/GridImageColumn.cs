using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020018FB RID: 6395
	public class GridImageColumn : GridColumn, IGridDataColumn
	{
		// Token: 0x17004A3D RID: 19005
		// (get) Token: 0x0600F6A3 RID: 63139 RVA: 0x0037F774 File Offset: 0x0037D974
		// (set) Token: 0x0600F6A4 RID: 63140 RVA: 0x0037F7A2 File Offset: 0x0037D9A2
		[Category("Data")]
		[NotifyParentProperty(true)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[TypeConverter(typeof(GridStringArrayConverter))]
		[DefaultValue("")]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A3E RID: 19006
		// (get) Token: 0x0600F6A5 RID: 63141 RVA: 0x0037F7BC File Offset: 0x0037D9BC
		// (set) Token: 0x0600F6A6 RID: 63142 RVA: 0x0037F7E9 File Offset: 0x0037D9E9
		[Category("Data")]
		[Description("The formatting applied to the value bound to the NavigateUrl property.")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A3F RID: 19007
		// (get) Token: 0x0600F6A7 RID: 63143 RVA: 0x0037F804 File Offset: 0x0037DA04
		// (set) Token: 0x0600F6A8 RID: 63144 RVA: 0x0037F831 File Offset: 0x0037DA31
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("ImageColumn_ImageUrl")]
		[Editor("Telerik.Web.Design.GridUrlImageColumnEditorForm, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[UrlProperty("*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png")]
		[Category("Behavior")]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x0600F6A9 RID: 63145 RVA: 0x0037F84C File Offset: 0x0037DA4C
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

		// Token: 0x0600F6AA RID: 63146 RVA: 0x0037F8D0 File Offset: 0x0037DAD0
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

		// Token: 0x0600F6AB RID: 63147 RVA: 0x0037F90D File Offset: 0x0037DB0D
		public override void Initialize()
		{
			base.Initialize();
			this._urlFieldsDesc = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
		}

		// Token: 0x0600F6AC RID: 63148 RVA: 0x0037F928 File Offset: 0x0037DB28
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			base.InitializeCell(cell, columnIndex, inItem);
			if (inItem.IsDataBound)
			{
				Image image = new Image();
				image.ID = string.Format("img{0}", this.UniqueName);
				image.ImageUrl = this.ImageUrl;
				image.AlternateText = this.AlternateText;
				image.ToolTip = this.AlternateText;
				image.ImageAlign = this.ImageAlign;
				image.Width = this.ImageWidth;
				image.Height = this.ImageHeight;
				if (this.DataImageUrlFields.Length != 0 || this.DataAlternateTextField.Length != 0)
				{
					image.DataBinding += this.OnDataBindColumn;
				}
				cell.Controls.Add(image);
			}
		}

		// Token: 0x0600F6AD RID: 63149 RVA: 0x0037F9E4 File Offset: 0x0037DBE4
		protected virtual void OnDataBindColumn(object sender, EventArgs e)
		{
			Image image = (Image)sender;
			GridItem bindingParentItem = GridColumn.GetBindingParentItem(image);
			object dataItem = bindingParentItem.DataItem;
			object obj = null;
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			bool flag = this._textFieldDesc != null && this._textFieldDesc.ComponentType.FullName != dataItem.GetType().FullName;
			if (this._textFieldDesc == null || flag || this._urlFieldsDesc == null)
			{
				propertyDescriptorCollection = TypeDescriptor.GetProperties(dataItem);
			}
			if (this._textFieldDesc == null || flag)
			{
				string dataAlternateTextField = this.DataAlternateTextField;
				if (dataAlternateTextField.Length != 0)
				{
					if (propertyDescriptorCollection != null)
					{
						this._textFieldDesc = propertyDescriptorCollection.Find(dataAlternateTextField, true);
					}
					if (this._textFieldDesc == null && !base.DesignMode)
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
						if (propertyDescriptor != null && dataItem != null && propertyDescriptor.ComponentType.FullName != dataItem.GetType().FullName)
						{
							propertyDescriptor = TypeDescriptor.GetProperties(dataItem).Find(text, true);
						}
						if (propertyDescriptor == null || flag)
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
						if (propertyDescriptor == null && !base.DesignMode && !string.IsNullOrEmpty(text))
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
				image.AlternateText = this.FormatDataAlternativeTextValue(value2);
				image.ToolTip = image.AlternateText;
			}
			else if (this._textFieldDesc == null && !base.DesignMode && obj != null)
			{
				image.AlternateText = this.FormatDataAlternativeTextValue(obj);
				image.ToolTip = image.AlternateText;
			}
			else if (base.DesignMode && this.DataAlternateTextField.Length != 0)
			{
				image.AlternateText = "GridImageColumn";
			}
			if (dictionary.Count > 0)
			{
				object[] array = new object[dictionary.Count];
				int num = 0;
				foreach (string key in this.DataImageUrlFields)
				{
					if (dictionary.ContainsKey(key) && dataItem != null)
					{
						if (dictionary[key] is PropertyDescriptor)
						{
							array[num] = ((PropertyDescriptor)dictionary[key]).GetValue(dataItem);
						}
						else
						{
							array[num] = dictionary[key];
						}
					}
					num++;
				}
				image.ImageUrl = this.FormatDataImageUrlValue(array);
				return;
			}
			if (base.DesignMode && this.DataImageUrlFields.Length != 0)
			{
				image.ImageUrl = "url";
			}
		}

		// Token: 0x0600F6AE RID: 63150 RVA: 0x0037FD14 File Offset: 0x0037DF14
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
						if (!GridBaseDataList.IsBindableType(obj1.GetType()))
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
						if (!GridBaseDataList.IsBindableType(obj1.GetType()))
						{
							result = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600F6AF RID: 63151 RVA: 0x0037FDA8 File Offset: 0x0037DFA8
		public override void PrepareCell(TableCell cell, GridItem item)
		{
			base.PrepareCell(cell, item);
			if (item is GridDataItem && cell.Controls.Count > 0 && cell.Controls[0] is Image && string.IsNullOrEmpty((cell.Controls[0] as Image).ImageUrl))
			{
				cell.Controls.Add(new LiteralControl("&nbsp;"));
			}
		}

		// Token: 0x0600F6B0 RID: 63152 RVA: 0x0037FE18 File Offset: 0x0037E018
		public override bool SupportsFiltering()
		{
			return this.AllowFiltering;
		}

		// Token: 0x0600F6B1 RID: 63153 RVA: 0x0037FE20 File Offset: 0x0037E020
		protected override string GetFilterDataField()
		{
			return this.DataAlternateTextField;
		}

		// Token: 0x0600F6B2 RID: 63154 RVA: 0x0037FE28 File Offset: 0x0037E028
		public override string GetDefaultGroupByExpression()
		{
			return this.DataAlternateTextField + " Group By " + this.DataAlternateTextField;
		}

		// Token: 0x0600F6B3 RID: 63155 RVA: 0x0037FE40 File Offset: 0x0037E040
		public override bool IsBoundToFieldName(string name)
		{
			if (string.IsNullOrEmpty(this.DataAlternateTextField))
			{
				return this.IsBoundToFieldName(this.DataImageUrlFields, name);
			}
			return string.Compare(this.DataAlternateTextField, name, true) == 0;
		}

		// Token: 0x0600F6B4 RID: 63156 RVA: 0x0037FE70 File Offset: 0x0037E070
		public bool IsBoundToFieldName(string[] urlFields, string name)
		{
			bool result = false;
			for (int i = 0; i < urlFields.Length; i++)
			{
				if (string.Compare(urlFields[i], name, true) == 0)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600F6B5 RID: 63157 RVA: 0x0037FE9E File Offset: 0x0037E09E
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase(this.DataAlternateTextField);
		}

		// Token: 0x0600F6B6 RID: 63158 RVA: 0x0037FEAC File Offset: 0x0037E0AC
		public override IDictionary GetCustomPropertyDataFields(object dataItemInstance)
		{
			Hashtable hashtable = new Hashtable();
			foreach (string dataField in this.DataImageUrlFields)
			{
				GridColumn.AddSubPropertyFieldInfo(hashtable, dataField, dataItemInstance);
			}
			if (!hashtable.ContainsKey(this.DataAlternateTextField))
			{
				GridColumn.AddSubPropertyFieldInfo(hashtable, this.DataAlternateTextField, dataItemInstance);
			}
			return hashtable;
		}

		// Token: 0x17004A40 RID: 19008
		// (get) Token: 0x0600F6B7 RID: 63159 RVA: 0x0037FEFC File Offset: 0x0037E0FC
		// (set) Token: 0x0600F6B8 RID: 63160 RVA: 0x0037FF25 File Offset: 0x0037E125
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("AllowSorting")]
		[Category("Behavior")]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A41 RID: 19009
		// (get) Token: 0x0600F6B9 RID: 63161 RVA: 0x0037FF43 File Offset: 0x0037E143
		protected override bool Sortable
		{
			get
			{
				return this.AllowSorting;
			}
		}

		// Token: 0x0600F6BA RID: 63162 RVA: 0x0037FF4B File Offset: 0x0037E14B
		internal override string GetSortExpression()
		{
			if (string.IsNullOrEmpty(this.SortExpression) && !string.IsNullOrEmpty(this.DataAlternateTextField) && this.AllowSorting)
			{
				return this.DataAlternateTextField;
			}
			return base.GetSortExpression();
		}

		// Token: 0x0600F6BB RID: 63163 RVA: 0x0037FF7C File Offset: 0x0037E17C
		public override GridColumn Clone()
		{
			GridImageColumn gridImageColumn = new GridImageColumn();
			gridImageColumn.CopyBaseProperties(this);
			return gridImageColumn;
		}

		// Token: 0x0600F6BC RID: 63164 RVA: 0x0037FF98 File Offset: 0x0037E198
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridImageColumn gridImageColumn = (GridImageColumn)fromColumn;
			this.DataImageUrlFields = gridImageColumn.DataImageUrlFields;
			this.DataImageUrlFormatString = gridImageColumn.DataImageUrlFormatString;
			this.ImageUrl = gridImageColumn.ImageUrl;
			this.AllowFiltering = gridImageColumn.AllowFiltering;
			this.AllowSorting = gridImageColumn.AllowSorting;
			this.AlternateText = gridImageColumn.AlternateText;
			this.ImageWidth = gridImageColumn.ImageWidth;
			this.ImageHeight = gridImageColumn.ImageHeight;
			this.ImageAlign = gridImageColumn.ImageAlign;
			this.DataAlternateTextField = gridImageColumn.DataAlternateTextField;
			this.DataAlternateTextFormatString = gridImageColumn.DataAlternateTextFormatString;
		}

		// Token: 0x17004A42 RID: 19010
		// (get) Token: 0x0600F6BD RID: 63165 RVA: 0x00380038 File Offset: 0x0037E238
		// (set) Token: 0x0600F6BE RID: 63166 RVA: 0x00380065 File Offset: 0x0037E265
		[DefaultValue("")]
		[Category("Behavior")]
		[Localizable(true)]
		[Description("ImageColumn_AlternateText")]
		[NotifyParentProperty(true)]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A43 RID: 19011
		// (get) Token: 0x0600F6BF RID: 63167 RVA: 0x00380080 File Offset: 0x0037E280
		// (set) Token: 0x0600F6C0 RID: 63168 RVA: 0x003800A9 File Offset: 0x0037E2A9
		[Description("Gets or sets the image alignment of the column Image control.")]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A44 RID: 19012
		// (get) Token: 0x0600F6C1 RID: 63169 RVA: 0x003800C8 File Offset: 0x0037E2C8
		// (set) Token: 0x0600F6C2 RID: 63170 RVA: 0x003800FA File Offset: 0x0037E2FA
		[Description("ImageColumn's image width")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Behavior")]
		[Localizable(true)]
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

		// Token: 0x17004A45 RID: 19013
		// (get) Token: 0x0600F6C3 RID: 63171 RVA: 0x00380114 File Offset: 0x0037E314
		// (set) Token: 0x0600F6C4 RID: 63172 RVA: 0x00380146 File Offset: 0x0037E346
		[NotifyParentProperty(true)]
		[Description("ImageColumn's image height")]
		[DefaultValue("")]
		[Category("Behavior")]
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

		// Token: 0x17004A46 RID: 19014
		// (get) Token: 0x0600F6C5 RID: 63173 RVA: 0x00380160 File Offset: 0x0037E360
		// (set) Token: 0x0600F6C6 RID: 63174 RVA: 0x0038018D File Offset: 0x0037E38D
		[Description("ImageColumn_DataAlternateTextField")]
		[DefaultValue("")]
		[Category("Data")]
		[NotifyParentProperty(true)]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A47 RID: 19015
		// (get) Token: 0x0600F6C7 RID: 63175 RVA: 0x003801B0 File Offset: 0x0037E3B0
		// (set) Token: 0x0600F6C8 RID: 63176 RVA: 0x003801DD File Offset: 0x0037E3DD
		[DefaultValue("")]
		[Localizable(true)]
		[Category("Data")]
		[NotifyParentProperty(true)]
		[Description("The formatting applied to the value bound to the AlternateText property.")]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A48 RID: 19016
		// (get) Token: 0x0600F6C9 RID: 63177 RVA: 0x003801F8 File Offset: 0x0037E3F8
		// (set) Token: 0x0600F6CA RID: 63178 RVA: 0x00380221 File Offset: 0x0037E421
		[DefaultValue(true)]
		[Description("AllowFiltering")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual bool AllowFiltering
		{
			get
			{
				object obj = base.ViewState["_af"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["_af"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x0600F6CB RID: 63179 RVA: 0x0038023F File Offset: 0x0037E43F
		public string GetActiveDataField()
		{
			return this.GetFilterDataField();
		}

		// Token: 0x0400467F RID: 18047
		protected const string SuportedExtensions = "*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png";

		// Token: 0x04004680 RID: 18048
		private PropertyDescriptor _textFieldDesc;

		// Token: 0x04004681 RID: 18049
		private PropertyDescriptorCollection _urlFieldsDesc = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
	}
}
