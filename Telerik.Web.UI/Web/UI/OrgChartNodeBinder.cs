using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000BFB RID: 3067
	internal class OrgChartNodeBinder : DataBoundControl, IHierarchicalItemContainer, IItemContainer
	{
		// Token: 0x060074AB RID: 29867 RVA: 0x001B2DFD File Offset: 0x001B0FFD
		public OrgChartNodeBinder(RadOrgChart orgChart, OrgChartGroupEnabledBinding bindingSettings)
		{
			this._orgChart = orgChart;
			this._bindingSettings = bindingSettings;
			this.InitializeBindingSettings();
		}

		// Token: 0x060074AC RID: 29868 RVA: 0x001B2E1C File Offset: 0x001B101C
		private void InitializeBindingSettings()
		{
			this.DataSource = this._orgChart.DataSource;
			this.DataSourceID = this._orgChart.DataSourceID;
			this.DataFieldID = this._orgChart.DataFieldID;
			this.DataFieldParentID = this._orgChart.DataFieldParentID;
			this.DataTextField = this._orgChart.DataTextField;
			this.DataImageUrlField = this._orgChart.DataImageUrlField;
			this.DataImageAltTextField = this._orgChart.DataImageAltTextField;
			this.DataCollapsedField = this._orgChart.DataCollapsedField;
			if (this.IsGroupEnabledBinding)
			{
				this.DataSource = this._bindingSettings.NodeBindingSettings.DataSource;
				this.DataSourceID = this._bindingSettings.NodeBindingSettings.DataSourceID;
				this.DataFieldID = this._bindingSettings.NodeBindingSettings.DataFieldID;
				this.DataFieldParentID = this._bindingSettings.NodeBindingSettings.DataFieldParentID;
				this.DataCollapsedField = this._bindingSettings.NodeBindingSettings.DataCollapsedField;
				this.DataGroupCollapsedField = this._bindingSettings.NodeBindingSettings.DataGroupCollapsedField;
			}
		}

		// Token: 0x17002601 RID: 9729
		// (get) Token: 0x060074AD RID: 29869 RVA: 0x001B2F40 File Offset: 0x001B1140
		// (set) Token: 0x060074AE RID: 29870 RVA: 0x001B2F8C File Offset: 0x001B118C
		public int MaxDataBindDepth
		{
			get
			{
				if (this._orgChart.EnableDrillDown && this._orgChart.DrillDownLevel != 0)
				{
					return this._orgChart.MaxDataBindDepth + this._orgChart.DrillDownLevel - 1;
				}
				return this._orgChart.MaxDataBindDepth;
			}
			set
			{
				this._orgChart.MaxDataBindDepth = value;
			}
		}

		// Token: 0x060074AF RID: 29871 RVA: 0x001B2F9A File Offset: 0x001B119A
		public IItem CreateItem()
		{
			return new OrgChartNode(this._orgChart);
		}

		// Token: 0x060074B0 RID: 29872 RVA: 0x001B2FA7 File Offset: 0x001B11A7
		public void RaiseItemDataBound(IItem item)
		{
			this._orgChart.RaiseNodeDataBound((OrgChartNode)item);
		}

		// Token: 0x17002602 RID: 9730
		// (get) Token: 0x060074B1 RID: 29873 RVA: 0x001B2FBA File Offset: 0x001B11BA
		public IList Children
		{
			get
			{
				return this._orgChart.Nodes;
			}
		}

		// Token: 0x17002603 RID: 9731
		// (get) Token: 0x060074B2 RID: 29874 RVA: 0x001B2FC7 File Offset: 0x001B11C7
		public OrgChartNodeCollection Nodes
		{
			get
			{
				return this._orgChart.Nodes;
			}
		}

		// Token: 0x17002604 RID: 9732
		// (get) Token: 0x060074B3 RID: 29875 RVA: 0x001B2FD4 File Offset: 0x001B11D4
		// (set) Token: 0x060074B4 RID: 29876 RVA: 0x001B2FDC File Offset: 0x001B11DC
		public string DataFieldID { get; set; }

		// Token: 0x17002605 RID: 9733
		// (get) Token: 0x060074B5 RID: 29877 RVA: 0x001B2FE5 File Offset: 0x001B11E5
		// (set) Token: 0x060074B6 RID: 29878 RVA: 0x001B2FED File Offset: 0x001B11ED
		public string DataFieldParentID { get; set; }

		// Token: 0x17002606 RID: 9734
		// (get) Token: 0x060074B7 RID: 29879 RVA: 0x001B2FF6 File Offset: 0x001B11F6
		// (set) Token: 0x060074B8 RID: 29880 RVA: 0x001B2FFE File Offset: 0x001B11FE
		public string DataImageUrlField { get; set; }

		// Token: 0x17002607 RID: 9735
		// (get) Token: 0x060074B9 RID: 29881 RVA: 0x001B3007 File Offset: 0x001B1207
		// (set) Token: 0x060074BA RID: 29882 RVA: 0x001B300F File Offset: 0x001B120F
		public string DataImageAltTextField { get; set; }

		// Token: 0x17002608 RID: 9736
		// (get) Token: 0x060074BB RID: 29883 RVA: 0x001B3018 File Offset: 0x001B1218
		// (set) Token: 0x060074BC RID: 29884 RVA: 0x001B3020 File Offset: 0x001B1220
		public string DataTextField { get; set; }

		// Token: 0x17002609 RID: 9737
		// (get) Token: 0x060074BD RID: 29885 RVA: 0x001B3029 File Offset: 0x001B1229
		// (set) Token: 0x060074BE RID: 29886 RVA: 0x001B3031 File Offset: 0x001B1231
		public string DataCollapsedField { get; set; }

		// Token: 0x1700260A RID: 9738
		// (get) Token: 0x060074BF RID: 29887 RVA: 0x001B303A File Offset: 0x001B123A
		// (set) Token: 0x060074C0 RID: 29888 RVA: 0x001B3042 File Offset: 0x001B1242
		public string DataGroupCollapsedField { get; set; }

		// Token: 0x1700260B RID: 9739
		// (get) Token: 0x060074C1 RID: 29889 RVA: 0x001B304B File Offset: 0x001B124B
		private bool IsGroupEnabledBinding
		{
			get
			{
				return this._orgChart.IsGroupEnabledBinding;
			}
		}

		// Token: 0x1700260C RID: 9740
		// (get) Token: 0x060074C2 RID: 29890 RVA: 0x001B3058 File Offset: 0x001B1258
		// (set) Token: 0x060074C3 RID: 29891 RVA: 0x001B3060 File Offset: 0x001B1260
		internal string CurrentSiteMapUrl { get; set; }

		// Token: 0x060074C4 RID: 29892 RVA: 0x001B306C File Offset: 0x001B126C
		public override void DataBind()
		{
			if (this.IsGroupEnabledBinding)
			{
				this.PreBindingProcedure();
				this.GroupEnabledBindingProcedure();
				return;
			}
			if (this.DataSourceID.Length != 0 || this.DataSource != null)
			{
				this.PreBindingProcedure();
				this.SimpleBindingProcedure();
				return;
			}
			foreach (OrgChartNode orgChartNode in this.Nodes)
			{
				orgChartNode.DataBind();
			}
			this.SimpleBindingProcedure();
		}

		// Token: 0x060074C5 RID: 29893 RVA: 0x001B30FC File Offset: 0x001B12FC
		private void PreBindingProcedure()
		{
			this._orgChart.Nodes.Renderer.Controls.Clear();
			this._orgChart.Nodes.Clear();
		}

		// Token: 0x060074C6 RID: 29894 RVA: 0x001B3128 File Offset: 0x001B1328
		private void GroupEnabledBindingProcedure()
		{
			if (this._bindingSettings.GroupItemBindingSettings != null)
			{
				OrgChartItemBinder orgChartItemBinder = new OrgChartItemBinder(this._orgChart, this._bindingSettings.GroupItemBindingSettings);
				this.Controls.Add(orgChartItemBinder);
				orgChartItemBinder.DataBind();
				this.Controls.Remove(orgChartItemBinder);
			}
			this.SimpleBindingProcedure();
			this.CleanItemsHash();
		}

		// Token: 0x060074C7 RID: 29895 RVA: 0x001B3183 File Offset: 0x001B1383
		private void SimpleBindingProcedure()
		{
			base.DataBind();
			this.Nodes.Renderer.DataBind();
		}

		// Token: 0x060074C8 RID: 29896 RVA: 0x001B319C File Offset: 0x001B139C
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data == null && this.DataSource == null)
			{
				return;
			}
			ControlDataBinder controlDataBinder = new ControlDataBinder(this);
			IHierarchicalEnumerable hierarchyData = this.GetHierarchyData(data);
			if (hierarchyData != null)
			{
				controlDataBinder.BindToHierarchicalData(hierarchyData);
				return;
			}
			DataView dataView = data as DataView;
			if (dataView != null && !base.DesignMode && !string.IsNullOrEmpty(this.DataFieldID) && !string.IsNullOrEmpty(this.DataFieldParentID))
			{
				controlDataBinder.BindToDataTable(dataView.ToTable(), this.DataFieldID, this.DataFieldParentID);
				return;
			}
			controlDataBinder.BindToEnumerableData(data, this.DataFieldID, this.DataFieldParentID);
		}

		// Token: 0x060074C9 RID: 29897 RVA: 0x001B3228 File Offset: 0x001B1428
		protected override IDataSource GetDataSource()
		{
			if (!base.IsBoundUsingDataSourceID)
			{
				return base.GetDataSource();
			}
			Control control = this.FindDataSourceControl();
			IHierarchicalDataSource hierarchicalDataSource = control as IHierarchicalDataSource;
			if (hierarchicalDataSource != null)
			{
				SiteMapDataSource siteMapDataSource = control as SiteMapDataSource;
				if (siteMapDataSource != null)
				{
					IHierarchyData currentNode = siteMapDataSource.Provider.CurrentNode;
					if (currentNode != null)
					{
						this.CurrentSiteMapUrl = currentNode.Path;
					}
				}
				return new DecoratingDataSource(hierarchicalDataSource);
			}
			return base.GetDataSource();
		}

		// Token: 0x060074CA RID: 29898 RVA: 0x001B3288 File Offset: 0x001B1488
		private Control FindDataSourceControl()
		{
			Control control = this;
			Control control2 = null;
			while (control2 == null && control != this.Page)
			{
				control = control.NamingContainer;
				if (control == null)
				{
					break;
				}
				control2 = control.FindControl(this.DataSourceID);
			}
			return control2;
		}

		// Token: 0x060074CB RID: 29899 RVA: 0x001B32C0 File Offset: 0x001B14C0
		private IHierarchicalEnumerable GetHierarchyData(IEnumerable data)
		{
			IHierarchicalEnumerable result = null;
			IHierarchicalEnumerable hierarchicalEnumerable = data as IHierarchicalEnumerable;
			if (this.GetDataSource() is IHierarchicalDataSource)
			{
				IHierarchicalDataSource hierarchicalDataSource = (IHierarchicalDataSource)this.GetDataSource();
				result = hierarchicalDataSource.GetHierarchicalView("").Select();
			}
			else if (this.DataSource is IHierarchicalDataSource)
			{
				IHierarchicalDataSource hierarchicalDataSource2 = (IHierarchicalDataSource)this.DataSource;
				result = hierarchicalDataSource2.GetHierarchicalView("").Select();
			}
			else if (hierarchicalEnumerable != null)
			{
				result = hierarchicalEnumerable;
			}
			return result;
		}

		// Token: 0x060074CC RID: 29900 RVA: 0x001B3334 File Offset: 0x001B1534
		private void CleanItemsHash()
		{
			this._orgChart.CleanItemsHash();
		}

		// Token: 0x04001FC1 RID: 8129
		private RadOrgChart _orgChart;

		// Token: 0x04001FC2 RID: 8130
		private OrgChartGroupEnabledBinding _bindingSettings;
	}
}
