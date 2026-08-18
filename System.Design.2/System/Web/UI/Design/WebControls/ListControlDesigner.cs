using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000D9 RID: 217
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ListControlDesigner : DataBoundControlDesigner
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x000288A8 File Offset: 0x00026AA8
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new ListControlActionList(this, base.DataSourceDesigner));
				return designerActionListCollection;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x000288DB File Offset: 0x00026ADB
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x000288ED File Offset: 0x00026AED
		public string DataValueField
		{
			get
			{
				return ((System.Web.UI.WebControls.ListControl)base.Component).DataValueField;
			}
			set
			{
				((System.Web.UI.WebControls.ListControl)base.Component).DataValueField = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00028900 File Offset: 0x00026B00
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x00028912 File Offset: 0x00026B12
		public string DataTextField
		{
			get
			{
				return ((System.Web.UI.WebControls.ListControl)base.Component).DataTextField;
			}
			set
			{
				((System.Web.UI.WebControls.ListControl)base.Component).DataTextField = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool UseDataSourcePickerActionList
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00028925 File Offset: 0x00026B25
		internal void ConnectToDataSourceAction()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ConnectToDataSourceCallback), null, SR.GetString("ListControlDesigner_ConnectToDataSource"));
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0002894C File Offset: 0x00026B4C
		private bool ConnectToDataSourceCallback(object context)
		{
			ListControlConnectToDataSourceDialog form = new ListControlConnectToDataSourceDialog(this);
			DialogResult dialogResult = UIServiceHelper.ShowDialog(base.Component.Site, form);
			return dialogResult == DialogResult.OK;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00003937 File Offset: 0x00001B37
		protected override void DataBind(BaseDataBoundControl dataBoundControl)
		{
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00028978 File Offset: 0x00026B78
		internal void EditItems()
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Items"];
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.EditItemsCallback), propertyDescriptor, SR.GetString("ListControlDesigner_EditItems"), propertyDescriptor);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x000289C0 File Offset: 0x00026BC0
		private bool EditItemsCallback(object context)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)context;
			ListItemsCollectionEditor listItemsCollectionEditor = new ListItemsCollectionEditor(typeof(ListItemCollection));
			listItemsCollectionEditor.EditValue(new TypeDescriptorContext(designerHost, propertyDescriptor, base.Component), new WindowsFormsEditorServiceHelper(this), propertyDescriptor.GetValue(base.Component));
			return true;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00028A24 File Offset: 0x00026C24
		public override string GetDesignTimeHtml()
		{
			string result;
			try
			{
				System.Web.UI.WebControls.ListControl listControl = (System.Web.UI.WebControls.ListControl)base.ViewControl;
				ListItemCollection items = listControl.Items;
				bool flag = this.IsDataBound(listControl);
				if (items.Count == 0 || flag)
				{
					if (flag)
					{
						items.Clear();
						items.Add(SR.GetString("Sample_Databound_Text"));
					}
					else
					{
						items.Add(SR.GetString("Sample_Unbound_Text"));
					}
				}
				result = base.GetDesignTimeHtml();
			}
			catch (Exception e)
			{
				result = this.GetErrorDesignTimeHtml(e);
			}
			return result;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00028AAC File Offset: 0x00026CAC
		public IEnumerable GetResolvedSelectedDataSource()
		{
			return ((IDataSourceProvider)this).GetResolvedSelectedDataSource();
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00028AB4 File Offset: 0x00026CB4
		public object GetSelectedDataSource()
		{
			return ((IDataSourceProvider)this).GetSelectedDataSource();
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00028ABC File Offset: 0x00026CBC
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(System.Web.UI.WebControls.ListControl));
			base.Initialize(component);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00028AD8 File Offset: 0x00026CD8
		private bool IsDataBound(System.Web.UI.WebControls.ListControl listControl)
		{
			return base.DataBindings["DataSource"] != null || base.DataSourceID.Length > 0 || listControl.IsDataBindingAutomatic;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00028B0F File Offset: 0x00026D0F
		public virtual void OnDataSourceChanged()
		{
			base.OnDataSourceChanged(true);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00028B18 File Offset: 0x00026D18
		protected override void OnDataSourceChanged(bool forceUpdateView)
		{
			this.OnDataSourceChanged();
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00028B20 File Offset: 0x00026D20
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			Attribute[] attributes = new Attribute[]
			{
				new TypeConverterAttribute(typeof(DataFieldConverter))
			};
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["DataTextField"];
			propertyDescriptor = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, attributes);
			properties["DataTextField"] = propertyDescriptor;
			propertyDescriptor = (PropertyDescriptor)properties["DataValueField"];
			propertyDescriptor = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, attributes);
			properties["DataValueField"] = propertyDescriptor;
		}
	}
}
