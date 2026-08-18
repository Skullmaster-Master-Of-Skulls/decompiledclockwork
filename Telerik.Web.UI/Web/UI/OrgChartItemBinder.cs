using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000BFA RID: 3066
	internal class OrgChartItemBinder : DataBoundControl, IItemContainer
	{
		// Token: 0x060074A4 RID: 29860 RVA: 0x001B2D10 File Offset: 0x001B0F10
		private OrgChartItemBinder()
		{
		}

		// Token: 0x060074A5 RID: 29861 RVA: 0x001B2D18 File Offset: 0x001B0F18
		public OrgChartItemBinder(RadOrgChart orgChart, OrgChartGroupItemBindingSettings settings)
		{
			this._orgChart = orgChart;
			this._settings = settings;
			this.DataSource = settings.DataSource;
			this.DataSourceID = settings.DataSourceID;
		}

		// Token: 0x060074A6 RID: 29862 RVA: 0x001B2D48 File Offset: 0x001B0F48
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data == null && this.DataSource == null)
			{
				foreach (object obj in this.Children)
				{
					ControlItem controlItem = (ControlItem)obj;
					controlItem.DataBind();
				}
				return;
			}
			this.PrepareForDataBinding();
			ControlDataBinder controlDataBinder = new ControlDataBinder(this);
			controlDataBinder.BindToEnumerableData(data);
		}

		// Token: 0x060074A7 RID: 29863 RVA: 0x001B2DC0 File Offset: 0x001B0FC0
		protected void PrepareForDataBinding()
		{
			this.Children.Clear();
		}

		// Token: 0x060074A8 RID: 29864 RVA: 0x001B2DCD File Offset: 0x001B0FCD
		public IItem CreateItem()
		{
			return new OrgChartGroupItem(this._orgChart);
		}

		// Token: 0x060074A9 RID: 29865 RVA: 0x001B2DDA File Offset: 0x001B0FDA
		public void RaiseItemDataBound(IItem item)
		{
		}

		// Token: 0x17002600 RID: 9728
		// (get) Token: 0x060074AA RID: 29866 RVA: 0x001B2DDC File Offset: 0x001B0FDC
		public IList Children
		{
			get
			{
				if (this._items == null)
				{
					this._items = new OrgChartGroupItemCollection(this._orgChart);
				}
				return this._items;
			}
		}

		// Token: 0x04001FBE RID: 8126
		private RadOrgChart _orgChart;

		// Token: 0x04001FBF RID: 8127
		private OrgChartGroupItemBindingSettings _settings;

		// Token: 0x04001FC0 RID: 8128
		private OrgChartGroupItemCollection _items;
	}
}
