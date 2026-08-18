using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Design;
using System.Diagnostics;
using System.Security.Permissions;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000FF RID: 255
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class RepeaterDesigner : ControlDesigner, IDataSourceProvider
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0003409C File Offset: 0x0003229C
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				designerActionListCollection.AddRange(base.ActionLists);
				designerActionListCollection.Add(new DataBoundControlActionList(this, this.DataSourceDesigner));
				return designerActionListCollection;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x000340CF File Offset: 0x000322CF
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x000340E1 File Offset: 0x000322E1
		public string DataMember
		{
			get
			{
				return ((Repeater)base.Component).DataMember;
			}
			set
			{
				((Repeater)base.Component).DataMember = value;
				this.OnDataSourceChanged();
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x000340FC File Offset: 0x000322FC
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0003412C File Offset: 0x0003232C
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

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x000341A6 File Offset: 0x000323A6
		public IDataSourceDesigner DataSourceDesigner
		{
			get
			{
				return this._dataSourceDesigner;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x000341AE File Offset: 0x000323AE
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x000341C0 File Offset: 0x000323C0
		public string DataSourceID
		{
			get
			{
				return ((Repeater)base.Component).DataSourceID;
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
				((Repeater)base.Component).DataSourceID = value;
				this.OnDataSourceChanged();
				this.ExecuteChooseDataSourcePostSteps();
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0003422C File Offset: 0x0003242C
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

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00034288 File Offset: 0x00032488
		protected bool TemplatesExist
		{
			get
			{
				Repeater repeater = (Repeater)base.ViewControl;
				return repeater.ItemTemplate != null || repeater.HeaderTemplate != null || repeater.FooterTemplate != null || repeater.AlternatingItemTemplate != null;
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000342C4 File Offset: 0x000324C4
		private bool ConnectToDataSource()
		{
			IDataSourceDesigner dataSourceDesigner = this.GetDataSourceDesigner();
			if (this._dataSourceDesigner != dataSourceDesigner)
			{
				if (this._dataSourceDesigner != null)
				{
					this._dataSourceDesigner.DataSourceChanged -= this.DataSourceChanged;
				}
				this._dataSourceDesigner = dataSourceDesigner;
				if (this._dataSourceDesigner != null)
				{
					this._dataSourceDesigner.DataSourceChanged += this.DataSourceChanged;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00034329 File Offset: 0x00032529
		private void CreateDataSource()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.CreateDataSourceCallback), null, SR.GetString("BaseDataBoundControl_CreateDataSourceTransaction"));
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00034350 File Offset: 0x00032550
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

		// Token: 0x060008F9 RID: 2297 RVA: 0x0003439C File Offset: 0x0003259C
		private void DataSourceChanged(object sender, EventArgs e)
		{
			this.designTimeDataTable = null;
			this.UpdateDesignTimeHtml();
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000343AC File Offset: 0x000325AC
		protected override void Dispose(bool disposing)
		{
			if (disposing && base.Component != null && base.Component.Site != null)
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
			base.Dispose(disposing);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void ExecuteChooseDataSourcePostSteps()
		{
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0003446C File Offset: 0x0003266C
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

		// Token: 0x060008FD RID: 2301 RVA: 0x000344E0 File Offset: 0x000326E0
		protected IEnumerable GetDesignTimeDataSource(int minimumRows)
		{
			IEnumerable resolvedSelectedDataSource = this.GetResolvedSelectedDataSource();
			return this.GetDesignTimeDataSource(resolvedSelectedDataSource, minimumRows);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x000344FC File Offset: 0x000326FC
		protected IEnumerable GetDesignTimeDataSource(IEnumerable selectedDataSource, int minimumRows)
		{
			DataTable dataTable = this.designTimeDataTable;
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
				}
			}
			return DesignTimeData.GetDesignTimeDataSource(dataTable, minimumRows);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00034550 File Offset: 0x00032750
		public override string GetDesignTimeHtml()
		{
			bool templatesExist = this.TemplatesExist;
			Repeater repeater = (Repeater)base.ViewControl;
			if (templatesExist)
			{
				DesignerDataSourceView designerView = this.DesignerView;
				IEnumerable dataSource = null;
				bool flag = false;
				string dataSourceID = string.Empty;
				if (designerView == null)
				{
					IEnumerable resolvedSelectedDataSource = this.GetResolvedSelectedDataSource();
					dataSource = this.GetDesignTimeDataSource(resolvedSelectedDataSource, 5);
				}
				else
				{
					try
					{
						bool flag2;
						dataSource = designerView.GetDesignTimeData(5, out flag2);
					}
					catch (Exception ex)
					{
						if (base.Component.Site != null)
						{
							IComponentDesignerDebugService componentDesignerDebugService = (IComponentDesignerDebugService)base.Component.Site.GetService(typeof(IComponentDesignerDebugService));
							if (componentDesignerDebugService != null)
							{
								componentDesignerDebugService.Fail(SR.GetString("DataSource_DebugService_FailedCall", new object[]
								{
									"DesignerDataSourceView.GetDesignTimeData",
									ex.Message
								}));
							}
						}
					}
				}
				try
				{
					repeater.DataSource = dataSource;
					dataSourceID = repeater.DataSourceID;
					repeater.DataSourceID = string.Empty;
					flag = true;
					repeater.DataBind();
					return base.GetDesignTimeHtml();
				}
				catch (Exception e)
				{
					return this.GetErrorDesignTimeHtml(e);
				}
				finally
				{
					repeater.DataSource = null;
					if (flag)
					{
						repeater.DataSourceID = dataSourceID;
					}
				}
			}
			return this.GetEmptyDesignTimeHtml();
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00034694 File Offset: 0x00032894
		protected override string GetEmptyDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("Repeater_NoTemplatesInst"));
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001FC84 File Offset: 0x0001DE84
		protected override string GetErrorDesignTimeHtml(Exception e)
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("Control_ErrorRendering"));
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x000346A8 File Offset: 0x000328A8
		public IEnumerable GetResolvedSelectedDataSource()
		{
			IEnumerable result = null;
			DataBinding dataBinding = base.DataBindings["DataSource"];
			if (dataBinding != null)
			{
				result = DesignTimeData.GetSelectedDataSource(base.Component, dataBinding.Expression, this.DataMember);
			}
			return result;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x000346E4 File Offset: 0x000328E4
		public object GetSelectedDataSource()
		{
			object result = null;
			DataBinding dataBinding = base.DataBindings["DataSource"];
			if (dataBinding != null)
			{
				result = DesignTimeData.GetSelectedDataSource(base.Component, dataBinding.Expression);
			}
			return result;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0003471C File Offset: 0x0003291C
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(Repeater));
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

		// Token: 0x06000905 RID: 2309 RVA: 0x000347E8 File Offset: 0x000329E8
		private void OnAnyComponentChanged(object source, ComponentChangedEventArgs ce)
		{
			if (ce.Member != null)
			{
				object component = ce.Component;
				Control control = component as Control;
				if (control != null && ce.Member.Name == "ID" && base.Component != null && ((string)ce.OldValue == this.DataSourceID || (string)ce.NewValue == this.DataSourceID))
				{
					this.ConnectToDataSource();
					this.UpdateDesignTimeHtml();
				}
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0003486C File Offset: 0x00032A6C
		private void OnComponentAdded(object sender, ComponentEventArgs e)
		{
			IComponent component = e.Component;
			Control control = component as Control;
			if (control != null && control.ID == this.DataSourceID)
			{
				this.ConnectToDataSource();
				this.UpdateDesignTimeHtml();
			}
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000348AC File Offset: 0x00032AAC
		public override void OnComponentChanged(object source, ComponentChangedEventArgs ce)
		{
			if (ce.Member != null)
			{
				string name = ce.Member.Name;
				if (name.Equals("DataSource") || name.Equals("DataMember"))
				{
					this.OnDataSourceChanged();
				}
			}
			base.OnComponentChanged(source, ce);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x000348F8 File Offset: 0x00032AF8
		private void OnComponentRemoving(object sender, ComponentEventArgs e)
		{
			IComponent component = e.Component;
			Control control = component as Control;
			if (control != null && control.ID == this.DataSourceID && base.Component != null && this._dataSourceDesigner != null)
			{
				this._dataSourceDesigner.DataSourceChanged -= this.DataSourceChanged;
				this._dataSourceDesigner = null;
			}
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00034958 File Offset: 0x00032B58
		private void OnComponentRemoved(object sender, ComponentEventArgs e)
		{
			IComponent component = e.Component;
			Control control = component as Control;
			if (control != null && base.Component != null && control.ID == this.DataSourceID)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null && !designerHost.Loading)
				{
					this.ConnectToDataSource();
					this.UpdateDesignTimeHtml();
				}
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x000349BF File Offset: 0x00032BBF
		public virtual void OnDataSourceChanged()
		{
			this.ConnectToDataSource();
			this.designTimeDataTable = null;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000349CF File Offset: 0x00032BCF
		private void OnDesignerLoadComplete(object sender, EventArgs e)
		{
			this.ConnectToDataSource();
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x000349D8 File Offset: 0x00032BD8
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["DataSource"];
			AttributeCollection attributes = propertyDescriptor.Attributes;
			int num = -1;
			int count = attributes.Count;
			string dataSource = this.DataSource;
			for (int i = 0; i < attributes.Count; i++)
			{
				if (attributes[i] is BrowsableAttribute)
				{
					num = i;
				}
			}
			int num2;
			if (num == -1 && dataSource.Length == 0)
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
			if (dataSource.Length == 0)
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
			propertyDescriptor = (PropertyDescriptor)properties["DataSourceID"];
			propertyDescriptor = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, new Attribute[]
			{
				new TypeConverterAttribute(typeof(DataSourceIDConverter))
			});
			properties["DataSourceID"] = propertyDescriptor;
		}

		// Token: 0x04000551 RID: 1361
		internal static TraceSwitch RepeaterDesignerSwitch = new TraceSwitch("RepeaterDesigner", "Enable Repeater designer general purpose traces.");

		// Token: 0x04000552 RID: 1362
		private DataTable dummyDataTable;

		// Token: 0x04000553 RID: 1363
		private DataTable designTimeDataTable;

		// Token: 0x04000554 RID: 1364
		private IDataSourceDesigner _dataSourceDesigner;
	}
}
