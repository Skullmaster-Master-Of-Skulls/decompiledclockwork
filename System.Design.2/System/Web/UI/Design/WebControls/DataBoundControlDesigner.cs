using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Design;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000B3 RID: 179
	public class DataBoundControlDesigner : BaseDataBoundControlDesigner, IDataBindingSchemaProvider, IDataSourceProvider
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0001C3A0 File Offset: 0x0001A5A0
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				if (this.UseDataSourcePickerActionList)
				{
					designerActionListCollection.Add(new DataBoundControlActionList(this, this.DataSourceDesigner));
				}
				designerActionListCollection.AddRange(base.ActionLists);
				return designerActionListCollection;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001C3DB File Offset: 0x0001A5DB
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x0001C3ED File Offset: 0x0001A5ED
		public string DataMember
		{
			get
			{
				return ((DataBoundControl)base.Component).DataMember;
			}
			set
			{
				((DataBoundControl)base.Component).DataMember = value;
				this.OnDataSourceChanged(true);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001C407 File Offset: 0x0001A607
		public IDataSourceDesigner DataSourceDesigner
		{
			get
			{
				return this._dataSourceDesigner;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001C410 File Offset: 0x0001A610
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

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0001C46C File Offset: 0x0001A66C
		protected virtual int SampleRowCount
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected virtual bool UseDataSourcePickerActionList
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001C470 File Offset: 0x0001A670
		protected override bool ConnectToDataSource()
		{
			IDataSourceDesigner dataSourceDesigner = this.GetDataSourceDesigner();
			if (this._dataSourceDesigner != dataSourceDesigner)
			{
				if (this._dataSourceDesigner != null)
				{
					this._dataSourceDesigner.DataSourceChanged -= this.OnDataSourceChanged;
					this._dataSourceDesigner.SchemaRefreshed -= this.OnSchemaRefreshed;
				}
				this._dataSourceDesigner = dataSourceDesigner;
				if (this._dataSourceDesigner != null)
				{
					this._dataSourceDesigner.DataSourceChanged += this.OnDataSourceChanged;
					this._dataSourceDesigner.SchemaRefreshed += this.OnSchemaRefreshed;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001C503 File Offset: 0x0001A703
		protected override void CreateDataSource()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.CreateDataSourceCallback), null, SR.GetString("BaseDataBoundControl_CreateDataSourceTransaction"));
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001C528 File Offset: 0x0001A728
		private bool CreateDataSourceCallback(object context)
		{
			string text;
			DialogResult dialogResult = BaseDataBoundControlDesigner.ShowCreateDataSourceDialog(this, typeof(IDataSource), true, out text);
			if (text.Length > 0)
			{
				base.DataSourceID = text;
			}
			return dialogResult == DialogResult.OK;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001C560 File Offset: 0x0001A760
		protected override void DataBind(BaseDataBoundControl dataBoundControl)
		{
			IEnumerable designTimeDataSource = this.GetDesignTimeDataSource();
			string dataSourceID = dataBoundControl.DataSourceID;
			object dataSource = dataBoundControl.DataSource;
			dataBoundControl.DataSource = designTimeDataSource;
			dataBoundControl.DataSourceID = string.Empty;
			try
			{
				if (designTimeDataSource != null)
				{
					dataBoundControl.DataBind();
				}
			}
			finally
			{
				dataBoundControl.DataSource = dataSource;
				dataBoundControl.DataSourceID = dataSourceID;
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001C5C0 File Offset: 0x0001A7C0
		protected override void DisconnectFromDataSource()
		{
			if (this._dataSourceDesigner != null)
			{
				this._dataSourceDesigner.DataSourceChanged -= this.OnDataSourceChanged;
				this._dataSourceDesigner.SchemaRefreshed -= this.OnSchemaRefreshed;
				this._dataSourceDesigner = null;
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001C600 File Offset: 0x0001A800
		protected override void Dispose(bool disposing)
		{
			if (disposing && this._dataSourceDesigner != null)
			{
				this._dataSourceDesigner.DataSourceChanged -= this.OnDataSourceChanged;
				this._dataSourceDesigner.SchemaRefreshed -= this.OnSchemaRefreshed;
				this._dataSourceDesigner = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001C654 File Offset: 0x0001A854
		private IDataSourceDesigner GetDataSourceDesigner()
		{
			IDataSourceDesigner result = null;
			string dataSourceID = base.DataSourceID;
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

		// Token: 0x06000593 RID: 1427 RVA: 0x0001C6C8 File Offset: 0x0001A8C8
		protected virtual IEnumerable GetDesignTimeDataSource()
		{
			IEnumerable enumerable = null;
			DesignerDataSourceView designerView = this.DesignerView;
			bool flag;
			if (designerView != null)
			{
				try
				{
					enumerable = designerView.GetDesignTimeData(this.SampleRowCount, out flag);
					goto IL_A2;
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
					goto IL_A2;
				}
			}
			IEnumerable resolvedSelectedDataSource = ((IDataSourceProvider)this).GetResolvedSelectedDataSource();
			if (resolvedSelectedDataSource != null)
			{
				DataTable dataTable = DesignTimeData.CreateSampleDataTable(resolvedSelectedDataSource);
				enumerable = DesignTimeData.GetDesignTimeDataSource(dataTable, this.SampleRowCount);
				flag = true;
			}
			IL_A2:
			if (enumerable != null)
			{
				ICollection collection = enumerable as ICollection;
				if (collection == null || collection.Count > 0)
				{
					return enumerable;
				}
			}
			flag = true;
			return this.GetSampleDataSource();
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001C7AC File Offset: 0x0001A9AC
		protected virtual IEnumerable GetSampleDataSource()
		{
			DataTable dataTable;
			if (((DataBoundControl)base.Component).DataSourceID.Length > 0)
			{
				dataTable = DesignTimeData.CreateDummyDataBoundDataTable();
			}
			else
			{
				dataTable = DesignTimeData.CreateDummyDataTable();
			}
			return DesignTimeData.GetDesignTimeDataSource(dataTable, this.SampleRowCount);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001C7EF File Offset: 0x0001A9EF
		private void OnDataSourceChanged(object sender, EventArgs e)
		{
			this.OnDataSourceChanged(true);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001C7F8 File Offset: 0x0001A9F8
		private void OnSchemaRefreshed(object sender, EventArgs e)
		{
			this.OnSchemaRefreshed();
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001C800 File Offset: 0x0001AA00
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["DataMember"];
			propertyDescriptor = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, new Attribute[]
			{
				new TypeConverterAttribute(typeof(DataMemberConverter))
			});
			properties["DataMember"] = propertyDescriptor;
			propertyDescriptor = (PropertyDescriptor)properties["DataSource"];
			propertyDescriptor = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, new Attribute[]
			{
				new TypeConverterAttribute(typeof(DataSourceConverter))
			});
			properties["DataSource"] = propertyDescriptor;
			propertyDescriptor = (PropertyDescriptor)properties["DataSourceID"];
			propertyDescriptor = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, new Attribute[]
			{
				new TypeConverterAttribute(typeof(DataSourceIDConverter))
			});
			properties["DataSourceID"] = propertyDescriptor;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001C8DC File Offset: 0x0001AADC
		IEnumerable IDataSourceProvider.GetResolvedSelectedDataSource()
		{
			IEnumerable result = null;
			DataBinding dataBinding = base.DataBindings["DataSource"];
			if (dataBinding != null)
			{
				result = DesignTimeData.GetSelectedDataSource((DataBoundControl)base.Component, dataBinding.Expression, this.DataMember);
			}
			return result;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001C920 File Offset: 0x0001AB20
		object IDataSourceProvider.GetSelectedDataSource()
		{
			object result = null;
			DataBinding dataBinding = base.DataBindings["DataSource"];
			if (dataBinding != null)
			{
				result = DesignTimeData.GetSelectedDataSource((DataBoundControl)base.Component, dataBinding.Expression);
			}
			return result;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0001C95C File Offset: 0x0001AB5C
		bool IDataBindingSchemaProvider.CanRefreshSchema
		{
			get
			{
				IDataSourceDesigner dataSourceDesigner = this.DataSourceDesigner;
				return dataSourceDesigner != null && dataSourceDesigner.CanRefreshSchema;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001C97C File Offset: 0x0001AB7C
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

		// Token: 0x0600059C RID: 1436 RVA: 0x0001C99C File Offset: 0x0001AB9C
		void IDataBindingSchemaProvider.RefreshSchema(bool preferSilent)
		{
			IDataSourceDesigner dataSourceDesigner = this.DataSourceDesigner;
			if (dataSourceDesigner != null)
			{
				dataSourceDesigner.RefreshSchema(preferSilent);
			}
		}

		// Token: 0x040002F0 RID: 752
		private IDataSourceDesigner _dataSourceDesigner;
	}
}
