using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000D0 RID: 208
	public class HierarchicalDataBoundControlDesigner : BaseDataBoundControlDesigner
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0002754C File Offset: 0x0002574C
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
				if (this.UseDataSourcePickerActionList)
				{
					designerActionListCollection.Add(new HierarchicalDataBoundControlActionList(this, this.DataSourceDesigner));
				}
				designerActionListCollection.AddRange(base.ActionLists);
				return designerActionListCollection;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00027587 File Offset: 0x00025787
		public IHierarchicalDataSourceDesigner DataSourceDesigner
		{
			get
			{
				return this._dataSourceDesigner;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00027590 File Offset: 0x00025790
		public DesignerHierarchicalDataSourceView DesignerView
		{
			get
			{
				DesignerHierarchicalDataSourceView result = null;
				if (this.DataSourceDesigner != null)
				{
					result = this.DataSourceDesigner.GetView(string.Empty);
				}
				return result;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected virtual bool UseDataSourcePickerActionList
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000275BC File Offset: 0x000257BC
		protected override bool ConnectToDataSource()
		{
			IHierarchicalDataSourceDesigner dataSourceDesigner = this.GetDataSourceDesigner();
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

		// Token: 0x06000721 RID: 1825 RVA: 0x0002764F File Offset: 0x0002584F
		protected override void CreateDataSource()
		{
			ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.CreateDataSourceCallback), null, SR.GetString("BaseDataBoundControl_CreateDataSourceTransaction"));
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00027674 File Offset: 0x00025874
		private bool CreateDataSourceCallback(object context)
		{
			string text;
			DialogResult dialogResult = BaseDataBoundControlDesigner.ShowCreateDataSourceDialog(this, typeof(IHierarchicalDataSource), true, out text);
			if (text.Length > 0)
			{
				base.DataSourceID = text;
			}
			return dialogResult == DialogResult.OK;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000276AC File Offset: 0x000258AC
		protected override void DataBind(BaseDataBoundControl dataBoundControl)
		{
			IHierarchicalEnumerable designTimeDataSource = this.GetDesignTimeDataSource();
			string dataSourceID = dataBoundControl.DataSourceID;
			object dataSource = dataBoundControl.DataSource;
			HierarchicalDataBoundControl hierarchicalDataBoundControl = (HierarchicalDataBoundControl)dataBoundControl;
			hierarchicalDataBoundControl.DataSource = designTimeDataSource;
			hierarchicalDataBoundControl.DataSourceID = string.Empty;
			try
			{
				if (designTimeDataSource != null)
				{
					hierarchicalDataBoundControl.DataBind();
				}
			}
			finally
			{
				hierarchicalDataBoundControl.DataSource = dataSource;
				hierarchicalDataBoundControl.DataSourceID = dataSourceID;
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00027714 File Offset: 0x00025914
		protected override void DisconnectFromDataSource()
		{
			if (this._dataSourceDesigner != null)
			{
				this._dataSourceDesigner.DataSourceChanged -= this.OnDataSourceChanged;
				this._dataSourceDesigner.SchemaRefreshed -= this.OnSchemaRefreshed;
				this._dataSourceDesigner = null;
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00027754 File Offset: 0x00025954
		private IHierarchicalDataSourceDesigner GetDataSourceDesigner()
		{
			IHierarchicalDataSourceDesigner result = null;
			string dataSourceID = base.DataSourceID;
			if (!string.IsNullOrEmpty(dataSourceID))
			{
				Control control = ControlHelper.FindControl(base.Component.Site, (Control)base.Component, dataSourceID);
				if (control != null && control.Site != null)
				{
					IDesignerHost designerHost = (IDesignerHost)control.Site.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						result = (designerHost.GetDesigner(control) as IHierarchicalDataSourceDesigner);
					}
				}
			}
			return result;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x000277C8 File Offset: 0x000259C8
		protected virtual IHierarchicalEnumerable GetDesignTimeDataSource()
		{
			IHierarchicalEnumerable hierarchicalEnumerable = null;
			DesignerHierarchicalDataSourceView designerView = this.DesignerView;
			bool flag;
			if (designerView != null)
			{
				try
				{
					hierarchicalEnumerable = designerView.GetDesignTimeData(out flag);
					goto IL_A6;
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
								"DesignerHierarchicalDataSourceView.GetDesignTimeData",
								ex.Message
							}));
						}
					}
					goto IL_A6;
				}
			}
			DataBinding dataBinding = base.DataBindings["DataSource"];
			if (dataBinding != null)
			{
				hierarchicalEnumerable = (DesignTimeData.GetSelectedDataSource(base.Component, dataBinding.Expression, null) as IHierarchicalEnumerable);
			}
			IL_A6:
			if (hierarchicalEnumerable != null)
			{
				ICollection collection = hierarchicalEnumerable as ICollection;
				if (collection == null || collection.Count > 0)
				{
					return hierarchicalEnumerable;
				}
			}
			flag = true;
			return this.GetSampleDataSource();
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000278B0 File Offset: 0x00025AB0
		protected virtual IHierarchicalEnumerable GetSampleDataSource()
		{
			return new HierarchicalDataBoundControlDesigner.HierarchicalSampleData(0, string.Empty);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001C7EF File Offset: 0x0001A9EF
		private void OnDataSourceChanged(object sender, EventArgs e)
		{
			this.OnDataSourceChanged(true);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001C7F8 File Offset: 0x0001A9F8
		private void OnSchemaRefreshed(object sender, EventArgs e)
		{
			this.OnSchemaRefreshed();
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000278C0 File Offset: 0x00025AC0
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["DataSource"];
			propertyDescriptor = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, new Attribute[]
			{
				new TypeConverterAttribute(typeof(HierarchicalDataSourceConverter))
			});
			properties["DataSource"] = propertyDescriptor;
			propertyDescriptor = (PropertyDescriptor)properties["DataSourceID"];
			propertyDescriptor = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, new Attribute[]
			{
				new TypeConverterAttribute(typeof(HierarchicalDataSourceIDConverter))
			});
			properties["DataSourceID"] = propertyDescriptor;
		}

		// Token: 0x04000459 RID: 1113
		private IHierarchicalDataSourceDesigner _dataSourceDesigner;

		// Token: 0x02000402 RID: 1026
		private class HierarchicalSampleData : IHierarchicalEnumerable, IEnumerable
		{
			// Token: 0x060027A6 RID: 10150 RVA: 0x000F3E90 File Offset: 0x000F2090
			public HierarchicalSampleData(int depth, string path)
			{
				this._list = new ArrayList();
				if (depth == 0)
				{
					this._list.Add(new HierarchicalDataBoundControlDesigner.HierarchicalSampleDataNode(SR.GetString("HierarchicalDataBoundControlDesigner_SampleRoot"), depth, path));
					return;
				}
				if (depth == 2)
				{
					this._list.Add(new HierarchicalDataBoundControlDesigner.HierarchicalSampleDataNode(SR.GetString("HierarchicalDataBoundControlDesigner_SampleLeaf", new object[]
					{
						1
					}), depth, path));
					this._list.Add(new HierarchicalDataBoundControlDesigner.HierarchicalSampleDataNode(SR.GetString("HierarchicalDataBoundControlDesigner_SampleLeaf", new object[]
					{
						2
					}), depth, path));
					return;
				}
				this._list.Add(new HierarchicalDataBoundControlDesigner.HierarchicalSampleDataNode(SR.GetString("HierarchicalDataBoundControlDesigner_SampleParent", new object[]
				{
					1
				}), depth, path));
				this._list.Add(new HierarchicalDataBoundControlDesigner.HierarchicalSampleDataNode(SR.GetString("HierarchicalDataBoundControlDesigner_SampleParent", new object[]
				{
					2
				}), depth, path));
			}

			// Token: 0x060027A7 RID: 10151 RVA: 0x000F3F84 File Offset: 0x000F2184
			public IEnumerator GetEnumerator()
			{
				return this._list.GetEnumerator();
			}

			// Token: 0x060027A8 RID: 10152 RVA: 0x000F3F91 File Offset: 0x000F2191
			public IHierarchyData GetHierarchyData(object enumeratedItem)
			{
				return (IHierarchyData)enumeratedItem;
			}

			// Token: 0x04001C67 RID: 7271
			private ArrayList _list;
		}

		// Token: 0x02000403 RID: 1027
		private class HierarchicalSampleDataNode : IHierarchyData
		{
			// Token: 0x060027A9 RID: 10153 RVA: 0x000F3F99 File Offset: 0x000F2199
			public HierarchicalSampleDataNode(string text, int depth, string path)
			{
				this._text = text;
				this._depth = depth;
				this._path = path + "\\" + text;
			}

			// Token: 0x17000844 RID: 2116
			// (get) Token: 0x060027AA RID: 10154 RVA: 0x000F3FC1 File Offset: 0x000F21C1
			public bool HasChildren
			{
				get
				{
					return this._depth < 2;
				}
			}

			// Token: 0x17000845 RID: 2117
			// (get) Token: 0x060027AB RID: 10155 RVA: 0x000F3FCF File Offset: 0x000F21CF
			public string Path
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x17000846 RID: 2118
			// (get) Token: 0x060027AC RID: 10156 RVA: 0x0000CA50 File Offset: 0x0000AC50
			public object Item
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17000847 RID: 2119
			// (get) Token: 0x060027AD RID: 10157 RVA: 0x000F3FD7 File Offset: 0x000F21D7
			public string Type
			{
				get
				{
					return "SampleData";
				}
			}

			// Token: 0x060027AE RID: 10158 RVA: 0x000F3FDE File Offset: 0x000F21DE
			public override string ToString()
			{
				return this._text;
			}

			// Token: 0x060027AF RID: 10159 RVA: 0x000F3FE6 File Offset: 0x000F21E6
			public IHierarchicalEnumerable GetChildren()
			{
				return new HierarchicalDataBoundControlDesigner.HierarchicalSampleData(this._depth + 1, this._path);
			}

			// Token: 0x060027B0 RID: 10160 RVA: 0x00003598 File Offset: 0x00001798
			public IHierarchyData GetParent()
			{
				return null;
			}

			// Token: 0x04001C68 RID: 7272
			private string _text;

			// Token: 0x04001C69 RID: 7273
			private int _depth;

			// Token: 0x04001C6A RID: 7274
			private string _path;
		}
	}
}
