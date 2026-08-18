using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace AjaxControlToolkit
{
	// Token: 0x02000071 RID: 113
	internal class ComboBoxDesignerActionList : DesignerActionList
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0000C15E File Offset: 0x0000A35E
		public ComboBoxDesignerActionList(IComponent component) : base(component)
		{
			this._comboBox = (ComboBox)component;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000C173 File Offset: 0x0000A373
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x0000C180 File Offset: 0x0000A380
		public bool AppendDataBoundItems
		{
			get
			{
				return this._comboBox.AppendDataBoundItems;
			}
			set
			{
				this.SetComponentProperty("AppendDataBoundItems", value);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000C193 File Offset: 0x0000A393
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x0000C1A0 File Offset: 0x0000A3A0
		public bool CaseSensitive
		{
			get
			{
				return this._comboBox.CaseSensitive;
			}
			set
			{
				this.SetComponentProperty("CaseSensitive", value);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000C1B3 File Offset: 0x0000A3B3
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		public ComboBoxStyle DropDownStyle
		{
			get
			{
				return this._comboBox.DropDownStyle;
			}
			set
			{
				this.SetComponentProperty("DropDownStyle", value);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000C1D3 File Offset: 0x0000A3D3
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000C1E0 File Offset: 0x0000A3E0
		public ComboBoxAutoCompleteMode AutoCompleteMode
		{
			get
			{
				return this._comboBox.AutoCompleteMode;
			}
			set
			{
				this.SetComponentProperty("AutoCompleteMode", value);
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000C1F4 File Offset: 0x0000A3F4
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			DesignerActionPropertyItem propertyItem = this.GetPropertyItem("AppendDataBoundItems", "Append DataBound Items");
			if (propertyItem != null)
			{
				designerActionItemCollection.Add(propertyItem);
			}
			propertyItem = this.GetPropertyItem("CaseSensitive", "Case Sensitive");
			if (propertyItem != null)
			{
				designerActionItemCollection.Add(propertyItem);
			}
			propertyItem = this.GetPropertyItem("DropDownStyle", "DropDown Style");
			if (propertyItem != null)
			{
				designerActionItemCollection.Add(propertyItem);
			}
			propertyItem = this.GetPropertyItem("AutoCompleteMode", "AutoComplete Mode");
			if (propertyItem != null)
			{
				designerActionItemCollection.Add(propertyItem);
			}
			return designerActionItemCollection;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000C278 File Offset: 0x0000A478
		protected virtual DesignerActionPropertyItem GetPropertyItem(string propertyName, string displayName)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._comboBox)[propertyName];
			if (propertyDescriptor != null && propertyDescriptor.IsBrowsable)
			{
				return new DesignerActionPropertyItem(propertyName, displayName, propertyDescriptor.Category, propertyDescriptor.Description);
			}
			return null;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
		protected virtual void SetComponentProperty(string propertyName, object value)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._comboBox)[propertyName];
			if (propertyDescriptor == null)
			{
				throw new ArgumentException("Property not found", propertyName);
			}
			propertyDescriptor.SetValue(this._comboBox, value);
		}

		// Token: 0x04000134 RID: 308
		private ComboBox _comboBox;
	}
}
