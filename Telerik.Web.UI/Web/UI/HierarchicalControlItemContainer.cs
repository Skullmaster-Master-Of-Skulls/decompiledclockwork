using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020005D3 RID: 1491
	public abstract class HierarchicalControlItemContainer : ControlItemContainer, IHierarchicalItemContainer, IItemContainer
	{
		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x060035A6 RID: 13734 RVA: 0x000B2044 File Offset: 0x000B0244
		// (set) Token: 0x060035A7 RID: 13735 RVA: 0x000B204C File Offset: 0x000B024C
		public override object DataSource
		{
			get
			{
				return this._dataSource;
			}
			set
			{
				this._dataSource = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x060035A8 RID: 13736 RVA: 0x000B205B File Offset: 0x000B025B
		// (set) Token: 0x060035A9 RID: 13737 RVA: 0x000B2063 File Offset: 0x000B0263
		internal string CurrentSiteMapUrl { get; set; }

		// Token: 0x060035AA RID: 13738
		protected abstract NavigationItemBindingCollection CreateDataBindings();

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x060035AB RID: 13739 RVA: 0x000B206C File Offset: 0x000B026C
		internal NavigationItemBindingCollection NavigationItemBindings
		{
			get
			{
				if (this._dataBindings == null)
				{
					this._dataBindings = this.CreateDataBindings();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._dataBindings).TrackViewState();
					}
				}
				return this._dataBindings;
			}
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x000B209B File Offset: 0x000B029B
		// (set) Token: 0x060035AD RID: 13741 RVA: 0x000B20BC File Offset: 0x000B02BC
		[Category("Data")]
		[Description("Maximum levels to populate from the datasource")]
		[DefaultValue(-1)]
		public virtual int MaxDataBindDepth
		{
			get
			{
				return (int)(this.ViewState["MaxDataBindDepth"] ?? -1);
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["MaxDataBindDepth"] = value;
			}
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x060035AE RID: 13742 RVA: 0x000B20E3 File Offset: 0x000B02E3
		// (set) Token: 0x060035AF RID: 13743 RVA: 0x000B2103 File Offset: 0x000B0303
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataNavigateUrlField
		{
			get
			{
				return (string)(this.ViewState["DataNavigateUrlField"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataNavigateUrlField"] = value;
			}
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x060035B0 RID: 13744 RVA: 0x000B2116 File Offset: 0x000B0316
		// (set) Token: 0x060035B1 RID: 13745 RVA: 0x000B2136 File Offset: 0x000B0336
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataFieldID
		{
			get
			{
				return (string)(this.ViewState["DataFieldID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataFieldID"] = value;
			}
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x060035B2 RID: 13746 RVA: 0x000B2149 File Offset: 0x000B0349
		// (set) Token: 0x060035B3 RID: 13747 RVA: 0x000B2169 File Offset: 0x000B0369
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataFieldParentID
		{
			get
			{
				return (string)(this.ViewState["DataFieldParentID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataFieldParentID"] = value;
			}
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x000B217C File Offset: 0x000B037C
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			((IStateManager)this.NavigationItemBindings).LoadViewState(array[1]);
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x000B21A8 File Offset: 0x000B03A8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.NavigationItemBindings).SaveViewState()
			};
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x000B21D4 File Offset: 0x000B03D4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.NavigationItemBindings).TrackViewState();
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000B21E8 File Offset: 0x000B03E8
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

		// Token: 0x060035B8 RID: 13752 RVA: 0x000B2220 File Offset: 0x000B0420
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

		// Token: 0x060035B9 RID: 13753 RVA: 0x000B2280 File Offset: 0x000B0480
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

		// Token: 0x060035BA RID: 13754 RVA: 0x000B22F4 File Offset: 0x000B04F4
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data == null && this.DataSource == null)
			{
				foreach (object obj in base.Children)
				{
					ControlItem controlItem = (ControlItem)obj;
					controlItem.DataBind();
				}
				return;
			}
			base.PrepareForDataBinding();
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

		// Token: 0x060035BB RID: 13755 RVA: 0x000B2410 File Offset: 0x000B0610
		protected internal TControlItem FindChildByUrl<TControlItem>(string url) where TControlItem : NavigationItem
		{
			return base.FindChild<TControlItem>((TControlItem item) => string.Compare(item.ResolveUrl(item.NavigateUrl), HttpUtility.UrlDecode(url), true) == 0);
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000B243C File Offset: 0x000B063C
		protected internal override ControlItem FindItemByHierarchicalIndex(string hierarchicalIndex)
		{
			if (string.IsNullOrEmpty(hierarchicalIndex))
			{
				return null;
			}
			NavigationItem navigationItem = null;
			IControlItemContainer controlItemContainer = this;
			foreach (string value in hierarchicalIndex.Split(new char[]
			{
				':'
			}))
			{
				int num = Convert.ToInt32(value);
				if (num >= controlItemContainer.Items.VisibleItems.Count)
				{
					return null;
				}
				navigationItem = (NavigationItem)controlItemContainer.Items.VisibleItems[num];
				controlItemContainer = navigationItem;
			}
			return navigationItem;
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000B24C4 File Offset: 0x000B06C4
		public virtual void LoadContentFile(string xmlFileName)
		{
			string xml = File.ReadAllText(this.Context.Server.MapPath(xmlFileName));
			base.LoadXml(xml);
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x060035BE RID: 13758 RVA: 0x000B24EF File Offset: 0x000B06EF
		// (set) Token: 0x060035BF RID: 13759 RVA: 0x000B24F7 File Offset: 0x000B06F7
		int IHierarchicalItemContainer.MaxDataBindDepth
		{
			get
			{
				return this.MaxDataBindDepth;
			}
			set
			{
				this.MaxDataBindDepth = value;
			}
		}

		// Token: 0x04000E82 RID: 3714
		private NavigationItemBindingCollection _dataBindings;

		// Token: 0x04000E83 RID: 3715
		private object _dataSource;
	}
}
