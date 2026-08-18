using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing.Design;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000308 RID: 776
	[ComplexBindingProperties("DataSource", "DataMember")]
	internal class ListControlBoundActionList : DesignerActionList
	{
		// Token: 0x06001EB4 RID: 7860 RVA: 0x000B7C14 File Offset: 0x000B5E14
		public ListControlBoundActionList(ControlDesigner owner) : base(owner.Component)
		{
			this._owner = owner;
			ListControl listControl = (ListControl)base.Component;
			if (listControl.DataSource != null)
			{
				this._boundMode = true;
			}
			this.uiService = (base.GetService(typeof(DesignerActionUIService)) as DesignerActionUIService);
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x000B7C6A File Offset: 0x000B5E6A
		private void RefreshPanelContent()
		{
			if (this.uiService != null)
			{
				this.uiService.Refresh(this._owner.Component);
			}
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x000B7C8C File Offset: 0x000B5E8C
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			designerActionItemCollection.Add(new DesignerActionPropertyItem("BoundMode", SR.GetString("BoundModeDisplayName"), SR.GetString("DataCategoryName"), SR.GetString("BoundModeDescription")));
			ListControl listControl = base.Component as ListControl;
			if (this._boundMode || (listControl != null && listControl.DataSource != null))
			{
				this._boundMode = true;
				designerActionItemCollection.Add(new DesignerActionHeaderItem(SR.GetString("BoundModeHeader"), SR.GetString("DataCategoryName")));
				designerActionItemCollection.Add(new DesignerActionPropertyItem("DataSource", SR.GetString("DataSourceDisplayName"), SR.GetString("DataCategoryName"), SR.GetString("DataSourceDescription")));
				designerActionItemCollection.Add(new DesignerActionPropertyItem("DisplayMember", SR.GetString("DisplayMemberDisplayName"), SR.GetString("DataCategoryName"), SR.GetString("DisplayMemberDescription")));
				designerActionItemCollection.Add(new DesignerActionPropertyItem("ValueMember", SR.GetString("ValueMemberDisplayName"), SR.GetString("DataCategoryName"), SR.GetString("ValueMemberDescription")));
				designerActionItemCollection.Add(new DesignerActionPropertyItem("BoundSelectedValue", SR.GetString("BoundSelectedValueDisplayName"), SR.GetString("DataCategoryName"), SR.GetString("BoundSelectedValueDescription")));
				return designerActionItemCollection;
			}
			designerActionItemCollection.Add(new DesignerActionHeaderItem(SR.GetString("UnBoundModeHeader"), SR.GetString("DataCategoryName")));
			designerActionItemCollection.Add(new DesignerActionMethodItem(this, "InvokeItemsDialog", SR.GetString("EditItemDisplayName"), SR.GetString("DataCategoryName"), SR.GetString("EditItemDescription"), true));
			return designerActionItemCollection;
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x000B7E2A File Offset: 0x000B602A
		// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x000B7E32 File Offset: 0x000B6032
		public bool BoundMode
		{
			get
			{
				return this._boundMode;
			}
			set
			{
				if (!value)
				{
					this.DataSource = null;
				}
				if (this.DataSource == null)
				{
					this._boundMode = value;
				}
				this.RefreshPanelContent();
			}
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x000B7E53 File Offset: 0x000B6053
		public void InvokeItemsDialog()
		{
			EditorServiceContext.EditValue(this._owner, base.Component, "Items");
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x000B7E6C File Offset: 0x000B606C
		// (set) Token: 0x06001EBB RID: 7867 RVA: 0x000B7E80 File Offset: 0x000B6080
		[AttributeProvider(typeof(IListSource))]
		public object DataSource
		{
			get
			{
				return ((ListControl)base.Component).DataSource;
			}
			set
			{
				ListControl listControl = (ListControl)base.Component;
				IDesignerHost designerHost = base.GetService(typeof(IDesignerHost)) as IDesignerHost;
				IComponentChangeService componentChangeService = base.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				PropertyDescriptor member = TypeDescriptor.GetProperties(listControl)["DataSource"];
				if (designerHost != null && componentChangeService != null)
				{
					using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("DGV DataSource TX Name"))
					{
						componentChangeService.OnComponentChanging(base.Component, member);
						listControl.DataSource = value;
						if (value == null)
						{
							listControl.DisplayMember = "";
							listControl.ValueMember = "";
						}
						componentChangeService.OnComponentChanged(base.Component, member, null, null);
						designerTransaction.Commit();
						this.RefreshPanelContent();
					}
				}
			}
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x000B7F50 File Offset: 0x000B6150
		private Binding GetSelectedValueBinding()
		{
			ListControl listControl = (ListControl)base.Component;
			Binding result = null;
			if (listControl.DataBindings != null)
			{
				foreach (object obj in listControl.DataBindings)
				{
					Binding binding = (Binding)obj;
					if (binding.PropertyName == "SelectedValue")
					{
						result = binding;
					}
				}
			}
			return result;
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x000B7FD0 File Offset: 0x000B61D0
		private void SetSelectedValueBinding(object dataSource, string dataMember)
		{
			ListControl listControl = (ListControl)base.Component;
			IDesignerHost designerHost = base.GetService(typeof(IDesignerHost)) as IDesignerHost;
			IComponentChangeService componentChangeService = base.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			PropertyDescriptor member = TypeDescriptor.GetProperties(listControl)["DataBindings"];
			if (designerHost != null && componentChangeService != null)
			{
				using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("TextBox DataSource RESX"))
				{
					componentChangeService.OnComponentChanging(this._owner.Component, member);
					Binding selectedValueBinding = this.GetSelectedValueBinding();
					if (selectedValueBinding != null)
					{
						listControl.DataBindings.Remove(selectedValueBinding);
					}
					if (listControl.DataBindings != null && dataSource != null && !string.IsNullOrEmpty(dataMember))
					{
						listControl.DataBindings.Add("SelectedValue", dataSource, dataMember);
					}
					componentChangeService.OnComponentChanged(this._owner.Component, member, null, null);
					designerTransaction.Commit();
				}
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x000B80CC File Offset: 0x000B62CC
		// (set) Token: 0x06001EBF RID: 7871 RVA: 0x000B80E0 File Offset: 0x000B62E0
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string DisplayMember
		{
			get
			{
				return ((ListControl)base.Component).DisplayMember;
			}
			set
			{
				ListControl listControl = (ListControl)base.Component;
				IDesignerHost designerHost = base.GetService(typeof(IDesignerHost)) as IDesignerHost;
				IComponentChangeService componentChangeService = base.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				PropertyDescriptor member = TypeDescriptor.GetProperties(listControl)["DisplayMember"];
				if (designerHost != null && componentChangeService != null)
				{
					using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("DGV DataSource TX Name"))
					{
						componentChangeService.OnComponentChanging(base.Component, member);
						listControl.DisplayMember = value;
						componentChangeService.OnComponentChanged(base.Component, member, null, null);
						designerTransaction.Commit();
					}
				}
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x000B8194 File Offset: 0x000B6394
		// (set) Token: 0x06001EC1 RID: 7873 RVA: 0x000B81A8 File Offset: 0x000B63A8
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string ValueMember
		{
			get
			{
				return ((ListControl)base.Component).ValueMember;
			}
			set
			{
				ListControl listControl = (ListControl)this._owner.Component;
				IDesignerHost designerHost = base.GetService(typeof(IDesignerHost)) as IDesignerHost;
				IComponentChangeService componentChangeService = base.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				PropertyDescriptor member = TypeDescriptor.GetProperties(listControl)["ValueMember"];
				if (designerHost != null && componentChangeService != null)
				{
					using (DesignerTransaction designerTransaction = designerHost.CreateTransaction("DGV DataSource TX Name"))
					{
						componentChangeService.OnComponentChanging(base.Component, member);
						listControl.ValueMember = value;
						componentChangeService.OnComponentChanged(base.Component, member, null, null);
						designerTransaction.Commit();
					}
				}
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x000B8260 File Offset: 0x000B6460
		// (set) Token: 0x06001EC3 RID: 7875 RVA: 0x000B8308 File Offset: 0x000B6508
		[TypeConverter("System.Windows.Forms.Design.DesignBindingConverter")]
		[Editor("System.Windows.Forms.Design.DesignBindingEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public object BoundSelectedValue
		{
			get
			{
				Binding selectedValueBinding = this.GetSelectedValueBinding();
				string text;
				object obj;
				if (selectedValueBinding == null)
				{
					text = null;
					obj = null;
				}
				else
				{
					text = selectedValueBinding.BindingMemberInfo.BindingMember;
					obj = selectedValueBinding.DataSource;
				}
				string typeName = string.Format(CultureInfo.InvariantCulture, "System.Windows.Forms.Design.DesignBinding, {0}", new object[]
				{
					typeof(ControlDesigner).Assembly.FullName
				});
				this._boundSelectedValue = TypeDescriptor.CreateInstance(null, Type.GetType(typeName), new Type[]
				{
					typeof(object),
					typeof(string)
				}, new object[]
				{
					obj,
					text
				});
				return this._boundSelectedValue;
			}
			set
			{
				if (value is string)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)["BoundSelectedValue"];
					TypeConverter converter = propertyDescriptor.Converter;
					this._boundSelectedValue = converter.ConvertFrom(new EditorServiceContext(this._owner), CultureInfo.InvariantCulture, value);
					return;
				}
				this._boundSelectedValue = value;
				if (value != null)
				{
					object value2 = TypeDescriptor.GetProperties(this._boundSelectedValue)["DataSource"].GetValue(this._boundSelectedValue);
					string dataMember = (string)TypeDescriptor.GetProperties(this._boundSelectedValue)["DataMember"].GetValue(this._boundSelectedValue);
					this.SetSelectedValueBinding(value2, dataMember);
				}
			}
		}

		// Token: 0x040017D7 RID: 6103
		private ControlDesigner _owner;

		// Token: 0x040017D8 RID: 6104
		private bool _boundMode;

		// Token: 0x040017D9 RID: 6105
		private object _boundSelectedValue;

		// Token: 0x040017DA RID: 6106
		private DesignerActionUIService uiService;
	}
}
