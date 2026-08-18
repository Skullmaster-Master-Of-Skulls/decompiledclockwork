using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001206 RID: 4614
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class TreeListHyperLinkColumn : TreeListColumn
	{
		// Token: 0x17003D8E RID: 15758
		// (get) Token: 0x0600BEC1 RID: 48833 RVA: 0x002A41D8 File Offset: 0x002A23D8
		// (set) Token: 0x0600BEC2 RID: 48834 RVA: 0x002A4206 File Offset: 0x002A2406
		[Description("HyperLinkColumn_DataNavigateUrlFields")]
		[NotifyParentProperty(true)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[TypeConverter(typeof(GridStringArrayConverter))]
		[Category("Data")]
		[DefaultValue("")]
		public virtual string[] DataNavigateUrlFields
		{
			get
			{
				object obj = base.ViewState["DataNavigateUrlFields"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
			set
			{
				base.ViewState["DataNavigateUrlFields"] = value;
			}
		}

		// Token: 0x17003D8F RID: 15759
		// (get) Token: 0x0600BEC3 RID: 48835 RVA: 0x002A421C File Offset: 0x002A241C
		// (set) Token: 0x0600BEC4 RID: 48836 RVA: 0x002A4249 File Offset: 0x002A2449
		[Localizable(true)]
		[Category("Data")]
		[NotifyParentProperty(true)]
		[Description("The formatting applied to the value bound to the NavigateUrl property.")]
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
				base.ViewState["DataNavigateUrlFormatString"] = value;
			}
		}

		// Token: 0x17003D90 RID: 15760
		// (get) Token: 0x0600BEC5 RID: 48837 RVA: 0x002A425C File Offset: 0x002A245C
		// (set) Token: 0x0600BEC6 RID: 48838 RVA: 0x002A4289 File Offset: 0x002A2489
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Data")]
		[Description("HyperLinkColumn_DataTextField")]
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
				base.UpdateUniqueNameIfDefault(value);
			}
		}

		// Token: 0x17003D91 RID: 15761
		// (get) Token: 0x0600BEC7 RID: 48839 RVA: 0x002A42A4 File Offset: 0x002A24A4
		// (set) Token: 0x0600BEC8 RID: 48840 RVA: 0x002A42D1 File Offset: 0x002A24D1
		[Category("Data")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("The formatting applied to the value bound to the Text property.")]
		[Localizable(true)]
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
			}
		}

		// Token: 0x17003D92 RID: 15762
		// (get) Token: 0x0600BEC9 RID: 48841 RVA: 0x002A42E4 File Offset: 0x002A24E4
		// (set) Token: 0x0600BECA RID: 48842 RVA: 0x002A4311 File Offset: 0x002A2511
		[Description("HyperLinkColumn_NavigateUrl")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Behavior")]
		[Localizable(true)]
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
			}
		}

		// Token: 0x17003D93 RID: 15763
		// (get) Token: 0x0600BECB RID: 48843 RVA: 0x002A4324 File Offset: 0x002A2524
		// (set) Token: 0x0600BECC RID: 48844 RVA: 0x002A4351 File Offset: 0x002A2551
		[NotifyParentProperty(true)]
		[Description("HyperLinkColumn_Target")]
		[Category("Behavior")]
		[Localizable(true)]
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
			}
		}

		// Token: 0x17003D94 RID: 15764
		// (get) Token: 0x0600BECD RID: 48845 RVA: 0x002A4364 File Offset: 0x002A2564
		// (set) Token: 0x0600BECE RID: 48846 RVA: 0x002A4391 File Offset: 0x002A2591
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("HyperLinkColumn_Text")]
		[DefaultValue("")]
		[Category("Appearance")]
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
			}
		}

		// Token: 0x17003D95 RID: 15765
		// (get) Token: 0x0600BECF RID: 48847 RVA: 0x002A43A4 File Offset: 0x002A25A4
		// (set) Token: 0x0600BED0 RID: 48848 RVA: 0x002A43CD File Offset: 0x002A25CD
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
			}
		}

		// Token: 0x17003D96 RID: 15766
		// (get) Token: 0x0600BED1 RID: 48849 RVA: 0x002A43E5 File Offset: 0x002A25E5
		protected override bool Sortable
		{
			get
			{
				return this.AllowSorting;
			}
		}

		// Token: 0x0600BED2 RID: 48850 RVA: 0x002A43F0 File Offset: 0x002A25F0
		protected virtual string FormatDataNavigateUrlValue(object[] dataUrlValues)
		{
			for (int i = 0; i < dataUrlValues.Length; i++)
			{
				if (dataUrlValues[i] == null || dataUrlValues[i] == DBNull.Value)
				{
					dataUrlValues[i] = string.Empty;
				}
			}
			string dataNavigateUrlFormatString = this.DataNavigateUrlFormatString;
			if (dataNavigateUrlFormatString.Length == 0)
			{
				return dataUrlValues[0].ToString();
			}
			string result = string.Empty;
			try
			{
				result = string.Format(dataNavigateUrlFormatString, dataUrlValues);
			}
			catch (Exception)
			{
				throw new FormatException("Illegal DataNavigateUrlFormatString for column: " + this.UniqueName);
			}
			return result;
		}

		// Token: 0x0600BED3 RID: 48851 RVA: 0x002A4474 File Offset: 0x002A2674
		protected virtual string FormatDataTextValue(object dataTextValue)
		{
			string empty = string.Empty;
			if (dataTextValue == null || dataTextValue == DBNull.Value)
			{
				return empty;
			}
			string dataTextFormatString = this.DataTextFormatString;
			if (dataTextFormatString.Length == 0)
			{
				return dataTextValue.ToString();
			}
			return string.Format(dataTextFormatString, dataTextValue);
		}

		// Token: 0x0600BED4 RID: 48852 RVA: 0x002A44B4 File Offset: 0x002A26B4
		protected override void InitializeDataCells(TableCell cell, int columnIndex, TreeListDataItem inItem)
		{
			this.link = new HyperLink();
			this.link.Text = this.Text;
			this.link.NavigateUrl = this.NavigateUrl;
			this.link.Target = this.Target;
			if (this.DataNavigateUrlFields.Length != 0 || this.DataTextField.Length != 0)
			{
				cell.DataBinding += this.OnColumnDataCellBinding;
			}
			cell.Controls.Add(this.link);
		}

		// Token: 0x0600BED5 RID: 48853 RVA: 0x002A453C File Offset: 0x002A273C
		protected void OnColumnDataCellBinding(object sender, EventArgs e)
		{
			TableCell control = (TableCell)sender;
			TreeListDataItem treeListDataItem = (TreeListDataItem)TreeListColumn.GetBindingParentItem(control);
			object dataItem = treeListDataItem.DataItem;
			object obj = null;
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			if (this.textFieldDesc == null || this.urlFieldsDesc == null)
			{
				propertyDescriptorCollection = TypeDescriptor.GetProperties(dataItem);
			}
			if (this.textFieldDesc == null)
			{
				string dataTextField = this.DataTextField;
				if (dataTextField.Length != 0)
				{
					if (propertyDescriptorCollection != null)
					{
						this.textFieldDesc = propertyDescriptorCollection.Find(dataTextField, true);
					}
					if (this.textFieldDesc == null && !base.Owner.IsDesignMode)
					{
						obj = this.ExtractPropertyValue(dataItem, dataTextField);
					}
				}
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (this.urlFieldsDesc != null && this.DataNavigateUrlFields.Length > 0)
			{
				ArrayList arrayList = new ArrayList(this.DataNavigateUrlFields);
				foreach (object obj2 in arrayList)
				{
					string text = (string)obj2;
					if (text.Length != 0)
					{
						PropertyDescriptor propertyDescriptor = this.urlFieldsDesc.Find(text, true);
						if (propertyDescriptor == null)
						{
							if (propertyDescriptorCollection != null)
							{
								propertyDescriptor = propertyDescriptorCollection.Find(text, true);
								if (propertyDescriptor != null)
								{
									this.urlFieldsDesc.Add(propertyDescriptor);
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
			if (this.textFieldDesc != null)
			{
				object dataTextValue = null;
				try
				{
					dataTextValue = this.textFieldDesc.GetValue(dataItem);
				}
				catch
				{
					dataTextValue = this.ExtractPropertyValue(dataItem, this.DataTextField);
				}
				this.link.Text = this.FormatDataTextValue(dataTextValue);
				this.link.ToolTip = this.link.Text;
			}
			else if (this.textFieldDesc == null && !base.Owner.IsDesignMode && obj != null)
			{
				this.link.Text = this.FormatDataTextValue(obj);
				this.link.ToolTip = this.link.Text;
			}
			else if (base.Owner.IsDesignMode && this.DataTextField.Length != 0)
			{
				this.link.Text = "HyperLinkColumn";
			}
			if (dictionary.Count > 0)
			{
				object[] array = new object[dictionary.Count];
				int num = 0;
				foreach (string text2 in this.DataNavigateUrlFields)
				{
					if (dictionary.ContainsKey(text2) && dataItem != null)
					{
						if (dictionary[text2] is PropertyDescriptor)
						{
							try
							{
								array[num] = ((PropertyDescriptor)dictionary[text2]).GetValue(dataItem);
								goto IL_2C4;
							}
							catch
							{
								array[num] = this.ExtractPropertyValue(dataItem, text2);
								goto IL_2C4;
							}
						}
						array[num] = dictionary[text2];
					}
					IL_2C4:
					num++;
				}
				this.link.NavigateUrl = this.FormatDataNavigateUrlValue(array);
				return;
			}
			if (base.Owner.IsDesignMode && this.DataNavigateUrlFields.Length != 0)
			{
				this.link.NavigateUrl = "url";
			}
		}

		// Token: 0x0600BED6 RID: 48854 RVA: 0x002A4884 File Offset: 0x002A2A84
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

		// Token: 0x0400321C RID: 12828
		private HyperLink link;

		// Token: 0x0400321D RID: 12829
		private PropertyDescriptor textFieldDesc;

		// Token: 0x0400321E RID: 12830
		private PropertyDescriptorCollection urlFieldsDesc = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
	}
}
