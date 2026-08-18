using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200009F RID: 159
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public abstract class BaseDataListDesigner : TemplatedControlDesigner, IDataBindingSchemaProvider, IDataSourceProvider
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00016064 File Offset: 0x00014264
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new BaseDataListActionList(this, this.DataSourceDesigner));
				return designerActionListCollection;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00016097 File Offset: 0x00014297
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x000160A4 File Offset: 0x000142A4
		public string DataKeyField
		{
			get
			{
				return this.bdl.DataKeyField;
			}
			set
			{
				this.bdl.DataKeyField = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x000160B2 File Offset: 0x000142B2
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x000160BF File Offset: 0x000142BF
		public string DataMember
		{
			get
			{
				return this.bdl.DataMember;
			}
			set
			{
				this.bdl.DataMember = value;
				this.OnDataSourceChanged();
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x000160D4 File Offset: 0x000142D4
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x00016104 File Offset: 0x00014304
		public string DataSource
		{
			get
			{
				DataBinding dataBinding = base.DataBindings["DataSource"];
				if (dataBinding != null)
				{
					return dataBinding.Expression;
				}
				return string.Empty;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					base.DataBindings.Remove("DataSource");
				}
				else
				{
					DataBinding dataBinding = base.DataBindings["DataSource"];
					if (dataBinding == null)
					{
						dataBinding = new DataBinding("DataSource", typeof(IEnumerable), value);
					}
					else
					{
						dataBinding.Expression = value;
					}
					base.DataBindings.Add(dataBinding);
				}
				this.OnDataSourceChanged();
				base.OnBindingsCollectionChangedInternal("DataSource");
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0001617E File Offset: 0x0001437E
		public IDataSourceDesigner DataSourceDesigner
		{
			get
			{
				return this._dataSourceDesigner;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00016186 File Offset: 0x00014386
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00016194 File Offset: 0x00014394
		public string DataSourceID
		{
			get
			{
				return this.bdl.DataSourceID;
			}
			set
			{
				if (value == this.DataSourceID)
				{
					return;
				}
				if (value == SR.GetString("DataSourceIDChromeConverter_NewDataSource"))
				{
					this.CreateDataSource();
					return;
				}
				if (value == SR.GetString("DataSourceIDChromeConverter_NoDataSource"))
				{
					value = string.Empty;
				}
				this.bdl.DataSourceID = value;
				this.OnDataSourceChanged();
				this.OnSchemaRefreshed();
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x000161FC File Offset: 0x000143FC
		public DesignerDataSourceView DesignerView
		{
			get
			{
				DesignerDataSourceView designerDataSourceView = null;
				if (this.DataSourceDesigner != null)
				{
					designerDataSourceView = this.DataSourceDesigner.GetView(this.DataMember);
					if (designerDataSourceView == null && string.IsNullOrEmpty(this.DataMember))
					{
						string[] viewNames = this.DataSourceDesigner.GetViewNames();
						if (viewNames != null && viewNames.Length != 0)
						{
							designerDataSourceView = this.DataSourceDesigner.GetView(viewNames[0]);
						}
					}
				}
				return designerDataSourceView;
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00016258 File Offset: 0x00014458
		private bool ConnectToDataSource()
		{
			IDataSourceDesigner dataSourceDesigner = this.GetDataSourceDesigner();
			if (this._dataSourceDesigner != dataSourceDesigner)
			{
				if (this._dataSourceDesigner != null)
				{
					this._dataSourceDesigner.DataSourceChanged -= this.DataSourceChanged;
					this._dataSourceDesigner.SchemaRefreshed -= this.SchemaRefreshed;
				}
				this._dataSourceDesigner = dataSourceDesigner;
				if (this._dataSourceDesigner != null)
				{
					this._dataSourceDesigner.DataSourceChanged += this.DataSourceChanged;
					this._dataSourceDesigner.SchemaRefreshed += this.SchemaRefreshed;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000162EB File Offset: 0x000144EB
		private void CreateDataSource()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.CreateDataSourceCallback), null, SR.GetString("BaseDataBoundControl_CreateDataSourceTransaction"));
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00016310 File Offset: 0x00014510
		private bool CreateDataSourceCallback(object context)
		{
			CreateDataSourceDialog createDataSourceDialog = new CreateDataSourceDialog(this, typeof(IDataSource), true);
			DialogResult dialogResult = UIServiceHelper.ShowDialog(base.Component.Site, createDataSourceDialog);
			string dataSourceID = createDataSourceDialog.DataSourceID;
			if (dataSourceID.Length > 0)
			{
				this.DataSourceID = dataSourceID;
			}
			return dialogResult == DialogResult.OK;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001635C File Offset: 0x0001455C
		private void DataSourceChanged(object sender, EventArgs e)
		{
			this.designTimeDataTable = null;
			this.UpdateDesignTimeHtml();
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001636C File Offset: 0x0001456C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (base.Component != null && base.Component.Site != null)
				{
					if (base.RootDesigner != null)
					{
						base.RootDesigner.LoadComplete -= this.OnDesignerLoadComplete;
					}
					IComponentChangeService componentChangeService = (IComponentChangeService)base.Component.Site.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						componentChangeService.ComponentAdded -= this.OnComponentAdded;
						componentChangeService.ComponentRemoving -= this.OnComponentRemoving;
						componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
						componentChangeService.ComponentChanged -= this.OnAnyComponentChanged;
					}
				}
				this.bdl = null;
				if (this._dataSourceDesigner != null)
				{
					this._dataSourceDesigner.DataSourceChanged -= this.DataSourceChanged;
					this._dataSourceDesigner.SchemaRefreshed -= this.SchemaRefreshed;
					this._dataSourceDesigner = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00016470 File Offset: 0x00014670
		private IDataSourceDesigner GetDataSourceDesigner()
		{
			IDataSourceDesigner result = null;
			string dataSourceID = this.DataSourceID;
			if (!string.IsNullOrEmpty(dataSourceID))
			{
				Control control = ControlHelper.FindControl(base.Component.Site, (Control)base.Component, dataSourceID);
				if (control != null && control.Site != null)
				{
					IDesignerHost designerHost = (IDesignerHost)control.Site.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						result = (designerHost.GetDesigner(control) as IDataSourceDesigner);
					}
				}
			}
			return result;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000164E4 File Offset: 0x000146E4
		protected IEnumerable GetDesignTimeDataSource(int minimumRows, out bool dummyDataSource)
		{
			IEnumerable resolvedSelectedDataSource = this.GetResolvedSelectedDataSource();
			return this.GetDesignTimeDataSource(resolvedSelectedDataSource, minimumRows, out dummyDataSource);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00016504 File Offset: 0x00014704
		protected IEnumerable GetDesignTimeDataSource(IEnumerable selectedDataSource, int minimumRows, out bool dummyDataSource)
		{
			DataTable dataTable = this.designTimeDataTable;
			dummyDataSource = false;
			if (dataTable == null)
			{
				if (selectedDataSource != null)
				{
					this.designTimeDataTable = DesignTimeData.CreateSampleDataTable(selectedDataSource);
					dataTable = this.designTimeDataTable;
				}
				if (dataTable == null)
				{
					if (this.dummyDataTable == null)
					{
						this.dummyDataTable = DesignTimeData.CreateDummyDataTable();
					}
					dataTable = this.dummyDataTable;
					dummyDataSource = true;
				}
			}
			return DesignTimeData.GetDesignTimeDataSource(dataTable, minimumRows);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00016560 File Offset: 0x00014760
		public IEnumerable GetResolvedSelectedDataSource()
		{
			IEnumerable result = null;
			DataBinding dataBinding = base.DataBindings["DataSource"];
			if (dataBinding != null)
			{
				result = DesignTimeData.GetSelectedDataSource(this.bdl, dataBinding.Expression, this.DataMember);
			}
			return result;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001659C File Offset: 0x0001479C
		public object GetSelectedDataSource()
		{
			object result = null;
			DataBinding dataBinding = base.DataBindings["DataSource"];
			if (dataBinding != null)
			{
				result = DesignTimeData.GetSelectedDataSource(this.bdl, dataBinding.Expression);
			}
			return result;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000165D2 File Offset: 0x000147D2
		[Obsolete("Use of this method is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
		public override IEnumerable GetTemplateContainerDataSource(string templateName)
		{
			return this.GetResolvedSelectedDataSource();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000165DC File Offset: 0x000147DC
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(BaseDataList));
			this.bdl = (BaseDataList)component;
			base.Initialize(component);
			base.SetViewFlags(ViewFlags.DesignTimeHtmlRequiresLoadComplete, true);
			if (base.RootDesigner != null)
			{
				if (base.RootDesigner.IsLoading)
				{
					base.RootDesigner.LoadComplete += this.OnDesignerLoadComplete;
				}
				else
				{
					this.OnDesignerLoadComplete(null, EventArgs.Empty);
				}
			}
			IComponentChangeService componentChangeService = (IComponentChangeService)component.Site.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentAdded += this.OnComponentAdded;
				componentChangeService.ComponentRemoving += this.OnComponentRemoving;
				componentChangeService.ComponentRemoved += this.OnComponentRemoved;
				componentChangeService.ComponentChanged += this.OnAnyComponentChanged;
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000166B4 File Offset: 0x000148B4
		protected internal void InvokePropertyBuilder(int initialPage)
		{
			ComponentEditor componentEditor;
			if (this.bdl is System.Web.UI.WebControls.DataGrid)
			{
				componentEditor = new DataGridComponentEditor(initialPage);
			}
			else
			{
				componentEditor = new DataListComponentEditor(initialPage);
			}
			componentEditor.EditComponent(this.bdl);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000166EC File Offset: 0x000148EC
		private void OnAnyComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (e.Member != null)
			{
				object component = e.Component;
				IDataSource dataSource = component as IDataSource;
				if (dataSource != null && dataSource is Control && e.Member.Name == "ID" && base.Component != null && ((string)e.OldValue == this.DataSourceID || (string)e.NewValue == this.DataSourceID))
				{
					this.ConnectToDataSource();
					this.UpdateDesignTimeHtml();
				}
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00003937 File Offset: 0x00001B37
		[Obsolete("Use of this method is not recommended because the AutoFormat dialog is launched by the designer host. The list of available AutoFormats is exposed on the ControlDesigner in the AutoFormats property. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected void OnAutoFormat(object sender, EventArgs e)
		{
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00016776 File Offset: 0x00014976
		public override void OnAutoFormatApplied(DesignerAutoFormat appliedAutoFormat)
		{
			this.OnStylesChanged();
			base.OnAutoFormatApplied(appliedAutoFormat);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00016788 File Offset: 0x00014988
		private void OnComponentAdded(object sender, ComponentEventArgs e)
		{
			IComponent component = e.Component;
			IDataSource dataSource = component as IDataSource;
			if (dataSource != null && component is Control && ((Control)dataSource).ID == this.DataSourceID)
			{
				this.ConnectToDataSource();
				this.UpdateDesignTimeHtml();
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000167D4 File Offset: 0x000149D4
		public override void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (e.Member != null)
			{
				string name = e.Member.Name;
				if (name.Equals("DataSource") || name.Equals("DataMember"))
				{
					this.OnDataSourceChanged();
				}
				else if (name.Equals("ItemStyle") || name.Equals("AlternatingItemStyle") || name.Equals("SelectedItemStyle") || name.Equals("EditItemStyle") || name.Equals("HeaderStyle") || name.Equals("FooterStyle") || name.Equals("SeparatorStyle") || name.Equals("Font") || name.Equals("ForeColor") || name.Equals("BackColor"))
				{
					this.OnStylesChanged();
				}
			}
			base.OnComponentChanged(sender, e);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000168B0 File Offset: 0x00014AB0
		private void OnComponentRemoving(object sender, ComponentEventArgs e)
		{
			IComponent component = e.Component;
			IDataSource dataSource = component as IDataSource;
			if (dataSource != null && dataSource is Control && base.Component != null && ((Control)dataSource).ID == this.DataSourceID && this._dataSourceDesigner != null)
			{
				this._dataSourceDesigner.DataSourceChanged -= this.DataSourceChanged;
				this._dataSourceDesigner.SchemaRefreshed -= this.SchemaRefreshed;
				this._dataSourceDesigner = null;
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00016934 File Offset: 0x00014B34
		private void OnComponentRemoved(object sender, ComponentEventArgs e)
		{
			IComponent component = e.Component;
			IDataSource dataSource = component as IDataSource;
			if (dataSource != null && dataSource is Control && base.Component != null && ((Control)dataSource).ID == this.DataSourceID)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null && !designerHost.Loading)
				{
					this.ConnectToDataSource();
					this.UpdateDesignTimeHtml();
				}
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000169A8 File Offset: 0x00014BA8
		protected internal virtual void OnDataSourceChanged()
		{
			this.ConnectToDataSource();
			this.designTimeDataTable = null;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000169B8 File Offset: 0x00014BB8
		private void OnDesignerLoadComplete(object sender, EventArgs e)
		{
			this.ConnectToDataSource();
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000169C1 File Offset: 0x00014BC1
		protected void OnPropertyBuilder(object sender, EventArgs e)
		{
			this.InvokePropertyBuilder(0);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001317F File Offset: 0x0001137F
		protected virtual void OnSchemaRefreshed()
		{
			this.UpdateDesignTimeHtml();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000169CA File Offset: 0x00014BCA
		protected internal void OnStylesChanged()
		{
			this.OnTemplateEditingVerbsChanged();
		}

		// Token: 0x060004E6 RID: 1254
		protected abstract void OnTemplateEditingVerbsChanged();

		// Token: 0x060004E7 RID: 1255 RVA: 0x000169D4 File Offset: 0x00014BD4
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["DataSource"];
			AttributeCollection attributes = propertyDescriptor.Attributes;
			int num = -1;
			int count = attributes.Count;
			string dataSource = this.DataSource;
			if (dataSource.Length > 0)
			{
				this._keepDataSourceBrowsable = true;
			}
			for (int i = 0; i < attributes.Count; i++)
			{
				if (attributes[i] is BrowsableAttribute)
				{
					num = i;
					break;
				}
			}
			int num2;
			if (num == -1 && dataSource.Length == 0 && !this._keepDataSourceBrowsable)
			{
				num2 = count + 2;
			}
			else
			{
				num2 = count + 1;
			}
			Attribute[] array = new Attribute[num2];
			attributes.CopyTo(array, 0);
			array[count] = new TypeConverterAttribute(typeof(DataSourceConverter));
			if (dataSource.Length == 0 && !this._keepDataSourceBrowsable)
			{
				if (num == -1)
				{
					array[count + 1] = BrowsableAttribute.No;
				}
				else
				{
					array[num] = BrowsableAttribute.No;
				}
			}
			propertyDescriptor = TypeDescriptor.CreateProperty(base.GetType(), "DataSource", typeof(string), array);
			properties["DataSource"] = propertyDescriptor;
			propertyDescriptor = (PropertyDescriptor)properties["DataMember"];
			propertyDescriptor = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, new Attribute[]
			{
				new TypeConverterAttribute(typeof(DataMemberConverter))
			});
			properties["DataMember"] = propertyDescriptor;
			propertyDescriptor = (PropertyDescriptor)properties["DataKeyField"];
			propertyDescriptor = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, new Attribute[]
			{
				new TypeConverterAttribute(typeof(DataFieldConverter))
			});
			properties["DataKeyField"] = propertyDescriptor;
			propertyDescriptor = (PropertyDescriptor)properties["DataSourceID"];
			propertyDescriptor = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, new Attribute[]
			{
				new TypeConverterAttribute(typeof(DataSourceIDConverter))
			});
			properties["DataSourceID"] = propertyDescriptor;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00016BAD File Offset: 0x00014DAD
		private void SchemaRefreshed(object sender, EventArgs e)
		{
			this.OnSchemaRefreshed();
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00016BB8 File Offset: 0x00014DB8
		bool IDataBindingSchemaProvider.CanRefreshSchema
		{
			get
			{
				IDataSourceDesigner dataSourceDesigner = this.DataSourceDesigner;
				return dataSourceDesigner != null && dataSourceDesigner.CanRefreshSchema;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00016BD8 File Offset: 0x00014DD8
		IDataSourceViewSchema IDataBindingSchemaProvider.Schema
		{
			get
			{
				DesignerDataSourceView designerView = this.DesignerView;
				if (designerView != null)
				{
					return designerView.Schema;
				}
				return null;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00016BF8 File Offset: 0x00014DF8
		void IDataBindingSchemaProvider.RefreshSchema(bool preferSilent)
		{
			IDataSourceDesigner dataSourceDesigner = this.DataSourceDesigner;
			if (dataSourceDesigner != null)
			{
				dataSourceDesigner.RefreshSchema(preferSilent);
			}
		}

		// Token: 0x04000216 RID: 534
		private BaseDataList bdl;

		// Token: 0x04000217 RID: 535
		private DataTable dummyDataTable;

		// Token: 0x04000218 RID: 536
		private DataTable designTimeDataTable;

		// Token: 0x04000219 RID: 537
		private IDataSourceDesigner _dataSourceDesigner;

		// Token: 0x0400021A RID: 538
		private bool _keepDataSourceBrowsable;
	}
}
