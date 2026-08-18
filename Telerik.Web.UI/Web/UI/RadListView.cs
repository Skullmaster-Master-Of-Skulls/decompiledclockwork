using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x02000BB7 RID: 2999
	[ClientScriptResource("Telerik.Web.UI.RadListView", "Telerik.Web.UI.ListView.RadListViewScripts.js")]
	[ControlValueProperty("SelectedValue")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadListView))]
	[ToolboxBitmap(typeof(RadListView), "Telerik.Web.UI.ListView.png")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[Designer("Telerik.Web.Design.RadListViewDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[EmbeddedSkin("ListView", typeof(RadListView))]
	[EmbeddedSkin("ListView", "Default", typeof(RadListView))]
	[Description("Telerik RadListView")]
	[ToolboxData("<{0}:RadListView runat=server></{0}:RadListView>")]
	[LightweightRendering]
	[TelerikToolboxCategory("Data")]
	[RequiredScript(typeof(jQuery))]
	[DefaultProperty("")]
	[DefaultEvent("NeedDataSource")]
	public class RadListView : RadCompositeDataBoundControl, IRadPageableItemContainer, IPostBackEventHandler, IRadFilterableContainer, IPageableItemContainer
	{
		// Token: 0x060071AE RID: 29102 RVA: 0x001A9FF4 File Offset: 0x001A81F4
		static RadListView()
		{
			RadListView.EventItemCreated = new object();
			RadListView.EventItemDataBound = new object();
			RadListView.EventItemCommand = new object();
			RadListView.EventNeedDataSource = new object();
			RadListView.EventPageIndexChanged = new object();
			RadListView.EventPageSizeChanged = new object();
			RadListView.EventSorting = new object();
			RadListView.EventTotalRowCountAvailable = new object();
			RadListView.EventTotalRowCountAvailableAsp = new object();
			RadListView.EventSelectedIndexChanged = new object();
			RadListView.EventItemEditing = new object();
			RadListView.EventItemUpdating = new object();
			RadListView.EventItemCanceling = new object();
			RadListView.EventItemUpdated = new object();
			RadListView.EventItemDeleting = new object();
			RadListView.EventItemDeleted = new object();
			RadListView.EventItemInserting = new object();
			RadListView.EventItemInserted = new object();
			RadListView.EventItemDrop = new object();
			RadListView.EventFieldDescriptorsReady = new object();
			RadListView.EventCustomAggregate = new object();
		}

		// Token: 0x17002518 RID: 9496
		// (get) Token: 0x060071B0 RID: 29104 RVA: 0x001AA15C File Offset: 0x001A835C
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002519 RID: 9497
		// (get) Token: 0x060071B1 RID: 29105 RVA: 0x001AA15F File Offset: 0x001A835F
		internal bool IsUsingModelBinding
		{
			get
			{
				return base.IsUsingModelBinders;
			}
		}

		// Token: 0x1700251A RID: 9498
		// (get) Token: 0x060071B2 RID: 29106 RVA: 0x001AA167 File Offset: 0x001A8367
		// (set) Token: 0x060071B3 RID: 29107 RVA: 0x001AA16F File Offset: 0x001A836F
		protected internal bool IsModelValid
		{
			get
			{
				return this._isModelValid;
			}
			set
			{
				this._isModelValid = value;
			}
		}

		// Token: 0x1700251B RID: 9499
		// (get) Token: 0x060071B4 RID: 29108 RVA: 0x001AA178 File Offset: 0x001A8378
		// (set) Token: 0x060071B5 RID: 29109 RVA: 0x001AA180 File Offset: 0x001A8380
		protected Control LayoutTemplateWrapper { get; set; }

		// Token: 0x1700251C RID: 9500
		// (get) Token: 0x060071B6 RID: 29110 RVA: 0x001AA189 File Offset: 0x001A8389
		internal ListViewControlLocator ControlLocator
		{
			get
			{
				if (this._controlLocator == null)
				{
					this._controlLocator = new ListViewControlLocator();
				}
				return this._controlLocator;
			}
		}

		// Token: 0x1700251D RID: 9501
		// (get) Token: 0x060071B7 RID: 29111 RVA: 0x001AA1A4 File Offset: 0x001A83A4
		// (set) Token: 0x060071B8 RID: 29112 RVA: 0x001AA1AC File Offset: 0x001A83AC
		protected bool IsDataBinding { get; set; }

		// Token: 0x1700251E RID: 9502
		// (get) Token: 0x060071B9 RID: 29113 RVA: 0x001AA1B5 File Offset: 0x001A83B5
		private ListViewControlStateManager ControlState
		{
			get
			{
				if (this._controlStateManager == null)
				{
					this._controlStateManager = new ListViewControlStateManager();
				}
				return this._controlStateManager;
			}
		}

		// Token: 0x1700251F RID: 9503
		// (get) Token: 0x060071BA RID: 29114 RVA: 0x001AA1D0 File Offset: 0x001A83D0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal bool IsDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x17002520 RID: 9504
		// (get) Token: 0x060071BB RID: 29115 RVA: 0x001AA1D8 File Offset: 0x001A83D8
		private string[] DataKeyNamesInternal
		{
			get
			{
				object obj = this.ViewState["DataKeyNames"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
		}

		// Token: 0x17002521 RID: 9505
		// (get) Token: 0x060071BC RID: 29116 RVA: 0x001AA208 File Offset: 0x001A8408
		private string[] ClientDataKeyNamesInternal
		{
			get
			{
				object obj = this.ViewState["ClientDataKeyNames"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
		}

		// Token: 0x17002522 RID: 9506
		// (get) Token: 0x060071BD RID: 29117 RVA: 0x001AA238 File Offset: 0x001A8438
		// (set) Token: 0x060071BE RID: 29118 RVA: 0x001AA265 File Offset: 0x001A8465
		internal List<ListViewDataSourceGroup> DataSourceGroups
		{
			get
			{
				object obj = this.ViewState["DataSourceGroups"];
				if (obj != null)
				{
					return (List<ListViewDataSourceGroup>)obj;
				}
				return new List<ListViewDataSourceGroup>();
			}
			set
			{
				this.ViewState["DataSourceGroups"] = value;
			}
		}

		// Token: 0x17002523 RID: 9507
		// (get) Token: 0x060071BF RID: 29119 RVA: 0x001AA278 File Offset: 0x001A8478
		private List<DataKey> DataKeysArrayList
		{
			get
			{
				if (this._dataKeysArrayList == null)
				{
					this._dataKeysArrayList = new List<DataKey>();
				}
				return this._dataKeysArrayList;
			}
		}

		// Token: 0x17002524 RID: 9508
		// (get) Token: 0x060071C0 RID: 29120 RVA: 0x001AA293 File Offset: 0x001A8493
		// (set) Token: 0x060071C1 RID: 29121 RVA: 0x001AA29B File Offset: 0x001A849B
		protected IEnumerable CurrentDataSource { get; set; }

		// Token: 0x17002525 RID: 9509
		// (get) Token: 0x060071C2 RID: 29122 RVA: 0x001AA2A4 File Offset: 0x001A84A4
		protected virtual ListViewEnumerableBase ResolvedDataSource
		{
			get
			{
				if (this._resolvedDataSource == null)
				{
					if (this.IsDataBinding)
					{
						this._resolvedDataSource = this.DataSourceHelper.GetResolvedDataSource(this, this.CurrentDataSource, this.DataMember);
					}
					else
					{
						this._resolvedDataSource = new ListViewEnumerableFromViewState(this.ControlState);
					}
					if (this._resolvedDataSource == null)
					{
						throw new InvalidOperationException(string.Format("Cannot resolve data source. DataMember: '{0}'", this.DataMember));
					}
					this.PrepareDataSource();
				}
				return this._resolvedDataSource;
			}
		}

		// Token: 0x060071C3 RID: 29123 RVA: 0x001AA31C File Offset: 0x001A851C
		protected virtual void PrepareDataSource()
		{
			ListViewEnumerableBase resolvedDataSource = this._resolvedDataSource;
			resolvedDataSource.IsBoundUsingDataSourceID = base.IsBoundUsingDataSourceID;
			if (resolvedDataSource.SupportsSorting && !this.AllowCustomSorting)
			{
				resolvedDataSource.SetSortExpressions(this.SortExpressions);
			}
			resolvedDataSource.AllowCustomSorting = this.AllowCustomSorting;
			if (resolvedDataSource.SupportsFiltering)
			{
				resolvedDataSource.SetFilteringExpressions(this.FilterExpressions);
				resolvedDataSource.ShouldApplyFiltering = !this._isDataSourceViewFilter;
			}
			if (resolvedDataSource.SupportsPaging)
			{
				RadListViewPagingManager pagingManager = resolvedDataSource.PagingManager;
				pagingManager.CurrentPageIndex = this.CurrentPageIndex;
				pagingManager.PageSize = this.PageSize;
				pagingManager.AllowPaging = this.AllowPaging;
				pagingManager.AllowCustomPaging = this.AllowCustomPaging;
				pagingManager.VirtualItemCount = this.VirtualItemCount;
			}
		}

		// Token: 0x17002526 RID: 9510
		// (get) Token: 0x060071C4 RID: 29124 RVA: 0x001AA3D2 File Offset: 0x001A85D2
		internal virtual ListViewDataSourceHelper DataSourceHelper
		{
			get
			{
				if (this._dataSourceHelper == null)
				{
					this._dataSourceHelper = new ListViewDataSourceHelper();
				}
				return this._dataSourceHelper;
			}
		}

		// Token: 0x17002527 RID: 9511
		// (get) Token: 0x060071C5 RID: 29125 RVA: 0x001AA3ED File Offset: 0x001A85ED
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool DataSourceIsAssigned
		{
			get
			{
				return this.DataSource != null || base.IsBoundUsingDataSourceID;
			}
		}

		// Token: 0x17002528 RID: 9512
		// (get) Token: 0x060071C6 RID: 29126 RVA: 0x001AA400 File Offset: 0x001A8600
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal Dictionary<string, Type> ItemPropertyTypes
		{
			get
			{
				if (this._itemPropertyTypes == null)
				{
					this._itemPropertyTypes = new Dictionary<string, Type>();
					if (this.CurrentDataSource != null)
					{
						ItemPropertiesDescriptor itemPropertiesDescriptor = new ItemPropertiesDescriptor(this.CurrentDataSource);
						foreach (object obj in itemPropertiesDescriptor.Process())
						{
							PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
							this._itemPropertyTypes.Add(propertyDescriptor.Name, propertyDescriptor.PropertyType);
						}
					}
				}
				return this._itemPropertyTypes;
			}
		}

		// Token: 0x060071C7 RID: 29127 RVA: 0x001AA498 File Offset: 0x001A8698
		protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			this.EnsureLayoutTemplate();
			this.RemoveItems();
			this.IsDataBinding = dataBinding;
			this.CurrentDataSource = dataSource;
			if (this.ResolvedDataSource == ListViewEnumerableBase.Null && dataSource != null)
			{
				this.ClearResolvedDataSource();
			}
			if (this.ResolvedDataSource == ListViewEnumerableBase.Null && dataSource == null)
			{
				this.CreateEmptyDataItem(0);
				return 0;
			}
			if (dataBinding)
			{
				DataSourceView data = this.GetData();
				bool flag = this.AllowPaging && data.CanPage;
				DataSourceSelectArguments selectArguments = base.SelectArguments;
				bool flag2 = false;
				if (data.CanSort && !this.OverrideDataSourceControlSorting)
				{
					this.AllowCustomSorting = true;
					flag2 = true;
				}
				if (flag && data.CanRetrieveTotalRowCount)
				{
					this.AllowCustomPaging = true;
					if (((this.SortExpressions.Count > 0 && this.OverrideDataSourceControlSorting) || (this.FilterExpressions.Count > 0 && !this._isDataSourceViewFilter)) && this.CanRetrieveAllData)
					{
						this.AllowCustomPaging = false;
					}
					if (this.FilterExpressions.Count <= 0 || this._isDataSourceViewFilter || !this.CanRetrieveAllData)
					{
						this.VirtualItemCount = selectArguments.TotalRowCount;
					}
					flag2 = true;
				}
				if (flag2)
				{
					this.PrepareDataSource();
				}
			}
			dataSource = this.ResolvedDataSource.RawEnumerable();
			if (dataBinding && this.LayoutTemplateWrapper != null)
			{
				this.LayoutTemplateWrapper.DataBind();
			}
			int dataItemsCount = 0;
			if (this.GroupTemplate != null)
			{
				dataItemsCount = this.CreateItemsWithGroups(this.LayoutTemplateWrapper, dataSource, dataBinding);
			}
			else if (this.DataGroups != null && this.DataGroups.Count > 0 && dataSource != null && this.CurrentDataSource.GetType().GetInterface("IDataReader") == null)
			{
				dataItemsCount = this.CreateItemsWithDataGroups(this.LayoutTemplateWrapper, dataSource, dataBinding);
			}
			else if (this.ItemTemplate != null)
			{
				dataItemsCount = this.CreateDataItems(this.LayoutTemplateWrapper, dataSource, dataBinding);
			}
			this.SavePagingData(dataBinding, this.ResolvedDataSource.PagingManager);
			this.DataSourceCount = this.ResolvedDataSource.DataSourceCount;
			this.CurrentDataSource = null;
			this.ClearResolvedDataSource();
			this.CreateEmptyDataItem(dataItemsCount);
			if (this.ControlState["_!ItemCount"] == null)
			{
				return 0;
			}
			return (int)this.ControlState["_!ItemCount"];
		}

		// Token: 0x060071C8 RID: 29128 RVA: 0x001AA6B4 File Offset: 0x001A88B4
		private void AutoIDControl(Control control)
		{
			control.ID = string.Format("ctrl{0}", this._autoIDIndex++.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060071C9 RID: 29129 RVA: 0x001AA6F0 File Offset: 0x001A88F0
		protected virtual void RemoveItems()
		{
			if (this.GroupTemplate != null)
			{
				if (this._groupsItemCreatedinContainerCount > 0)
				{
					for (int i = 0; i < this._groupsItemCreatedinContainerCount; i++)
					{
						this._groupItemWrapperContainer.Controls.RemoveAt(this._groupPlaceholderControlIndex);
					}
					this._groupsItemCreatedinContainerCount = 0;
				}
			}
			else if (this.DataGroups != null && this.DataGroups.Count > 0)
			{
				if (this._firstLevelDataGroupsCount > 0)
				{
					for (int j = 0; j < this._firstLevelDataGroupsCount; j++)
					{
						this._firstDataGroupWrapperContainer.Controls.RemoveAt(this._firstLevelDataGroupControlIndex);
					}
					this._firstLevelDataGroupsCount = 0;
					string[] array = new string[this._dataGroupWrapperContainers.Keys.Count];
					this._dataGroupWrapperContainers.Keys.CopyTo(array, 0);
					foreach (string text in array)
					{
						if (text != "__0level")
						{
							this._dataGroupWrapperContainers.Remove(text);
						}
					}
				}
			}
			else if (this._itemsCreatedInContainerCount > 0 && this._itemsWrapperContainer != null)
			{
				for (int l = 0; l < this._itemsCreatedInContainerCount; l++)
				{
					this._itemsWrapperContainer.Controls.RemoveAt(this._placeholderControlIndex);
				}
				this._itemsCreatedInContainerCount = 0;
			}
			this._autoIDIndex = 0;
		}

		// Token: 0x060071CA RID: 29130 RVA: 0x001AA844 File Offset: 0x001A8A44
		protected override void CreateChildControls()
		{
			object obj = this.ViewState["_!ItemCount"];
			if (obj == null && base.RequiresDataBinding)
			{
				this.EnsureDataBound();
			}
			if (obj != null && (int)obj != -1)
			{
				object[] dataSource = new object[(int)obj];
				this.CreateChildControls(dataSource, false);
				base.ClearChildViewState();
				return;
			}
			if (!this._instantiatedEmptyDataTemplate)
			{
				this.EnsureLayoutTemplate();
			}
		}

		// Token: 0x060071CB RID: 29131 RVA: 0x001AA8AC File Offset: 0x001A8AAC
		private void SavePagingData(bool useDataSource, RadListViewPagingManager pagingManager)
		{
			if (useDataSource)
			{
				this.ControlState["_!DSIC"] = pagingManager.DataSourceCount;
				this.ControlState["_!ItemCount"] = this.Items.Count;
				if (pagingManager.IsPagingEnabled)
				{
					this.ControlState["_!PCount"] = pagingManager.PageCount;
					this.ControlState["_!DSIC"] = pagingManager.DataSourceCount;
				}
				else
				{
					this.ControlState["_!PCount"] = null;
				}
				this.UpdateFilterControl();
			}
			this.UpdateDataPager(pagingManager);
		}

		// Token: 0x060071CC RID: 29132 RVA: 0x001AA958 File Offset: 0x001A8B58
		private void UpdateDataPager(RadListViewPagingManager pagingManager)
		{
			bool flag = this.FilterExpressions.Count > 0 && this.CanRetrieveAllData && !this.AllowCustomPaging;
			int totalRowCount = (this.VirtualItemCount > 0 && !flag) ? this.VirtualItemCount : pagingManager.DataSourceCount;
			RadDataPagerPageEventArgs e = new RadDataPagerPageEventArgs(this.StartRowIndex, this.PageSize, totalRowCount);
			this.OnTotalRowCountAvailable(e);
		}

		// Token: 0x060071CD RID: 29133 RVA: 0x001AA9BD File Offset: 0x001A8BBD
		private void UpdateFilterControl()
		{
			if (this.CurrentDataSource != null && this.HasFieldDescriptorsReadyAttachedHandlers)
			{
				this.OnFieldDescriptorsReady(new RadFilterFildDesciptorsEventArgs(this.BuildFilterView()));
			}
		}

		// Token: 0x060071CE RID: 29134 RVA: 0x001AA9E0 File Offset: 0x001A8BE0
		protected virtual RadFilterFilterableView BuildFilterView()
		{
			RadFilterFilterableView radFilterFilterableView = new RadFilterFilterableView
			{
				SupportedGroupTypes = 
				{
					RadFilterGroupOperation.And,
					RadFilterGroupOperation.Or
				},
				SupportedFilterFunctions = 
				{
					RadFilterFunction.Contains,
					RadFilterFunction.EqualTo,
					RadFilterFunction.GreaterThan,
					RadFilterFunction.GreaterThanOrEqualTo,
					RadFilterFunction.Group,
					RadFilterFunction.IsEmpty,
					RadFilterFunction.IsNull,
					RadFilterFunction.LessThan,
					RadFilterFunction.LessThanOrEqualTo,
					RadFilterFunction.NotEqualTo,
					RadFilterFunction.NotIsEmpty,
					RadFilterFunction.NotIsNull,
					RadFilterFunction.StartsWith,
					RadFilterFunction.EndsWith
				}
			};
			ItemPropertiesDescriptor itemPropertiesDescriptor = new ItemPropertiesDescriptor(this.CurrentDataSource);
			foreach (object obj in itemPropertiesDescriptor.Process())
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				radFilterFilterableView.DataFields.Add(new RadFilterFieldDescriptor(propertyDescriptor.Name, propertyDescriptor.PropertyType));
			}
			return radFilterFilterableView;
		}

		// Token: 0x060071CF RID: 29135 RVA: 0x001AAB30 File Offset: 0x001A8D30
		public static bool IsBindableType(Type type)
		{
			return type.IsPrimitive || !(type != typeof(string)) || !(type != typeof(DateTime)) || !(type != typeof(TimeSpan)) || !(type != typeof(decimal)) || !(type != typeof(Guid)) || type.IsEnum || (type.IsValueType && type.IsGenericType && type.GetGenericArguments().Length == 1 && RadListView.IsBindableType(type.GetGenericArguments()[0]));
		}

		// Token: 0x060071D0 RID: 29136 RVA: 0x001AABD6 File Offset: 0x001A8DD6
		private void ClearResolvedDataSource()
		{
			this._resolvedDataSource = null;
		}

		// Token: 0x060071D1 RID: 29137 RVA: 0x001AABE0 File Offset: 0x001A8DE0
		protected override DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			DataSourceSelectArguments dataSourceSelectArguments = new DataSourceSelectArguments();
			DataSourceView data = this.GetData();
			string a = data.GetType().ToString();
			if (a == "System.Web.UI.WebControls.EntityDataSourceView" || a == "Microsoft.AspNet.EntityDataSource.EntityDataSourceView" || a == "Telerik.OpenAccess.RT.DataSource.OpenAccessDataSourceView" || a == "Telerik.OpenAccess.Web.OpenAccessLinqDataSourceView" || a == "System.Web.UI.WebControls.LinqDataSourceView")
			{
				if (!data.CanSort || (this.FilterExpressions.Count > 0 && !this._isDataSourceViewFilter) || this.DataGroups.Count > 0 || (this.OverrideDataSourceControlSorting && this.SortExpressions.Count > 0))
				{
					this.CanRetrieveAllData = true;
				}
				else
				{
					this.CanRetrieveAllData = false;
				}
			}
			if (this.IsDataSourceViewWithFiltering() && this.FilterExpressions.Count > 0)
			{
				this.ApplyFilterExpressionToDataSourceView(data);
				this._isDataSourceViewFilter = true;
			}
			bool flag = data.CanSort && this.SortExpressions.Count > 0;
			if (flag && !this.OverrideDataSourceControlSorting)
			{
				if (this.IsUsingModelBinding)
				{
					dataSourceSelectArguments.SortExpression = this.SortExpressions.GetSortString().Replace(" ASC", string.Empty);
				}
				else
				{
					dataSourceSelectArguments.SortExpression = this.SortExpressions.GetSortString();
				}
			}
			if (this.AllowPaging && data.CanPage)
			{
				if (data.CanRetrieveTotalRowCount)
				{
					if (this.CanRetrieveAllData && ((this.FilterExpressions.Count > 0 && !this._isDataSourceViewFilter) || this.DataGroups.Count > 0 || (this.SortExpressions.Count > 0 && ((data.CanSort && this.OverrideDataSourceControlSorting) || !data.CanSort))))
					{
						dataSourceSelectArguments.MaximumRows = int.MaxValue;
						dataSourceSelectArguments.StartRowIndex = 0;
					}
					else
					{
						dataSourceSelectArguments.MaximumRows = this.PageSize;
						dataSourceSelectArguments.StartRowIndex = this.PageSize * this.CurrentPageIndex;
					}
					dataSourceSelectArguments.RetrieveTotalRowCount = true;
				}
				else
				{
					dataSourceSelectArguments.MaximumRows = -1;
					dataSourceSelectArguments.StartRowIndex = this.PageSize * this.CurrentPageIndex;
				}
			}
			return dataSourceSelectArguments;
		}

		// Token: 0x060071D2 RID: 29138 RVA: 0x001AAE78 File Offset: 0x001A9078
		private void ApplyFilterExpressionToDataSourceView(DataSourceView dataView)
		{
			string expression = string.Empty;
			Action<object> action = delegate(object control)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control)["Where"];
				object value = propertyDescriptor.GetValue(control);
				string text = (value != null) ? value.ToString() : string.Empty;
				if (string.IsNullOrEmpty(text))
				{
					propertyDescriptor.SetValue(control, expression);
					return;
				}
				string arg = string.IsNullOrEmpty(expression) ? string.Empty : string.Format(" AND {0}", expression);
				propertyDescriptor.SetValue(control, string.Format("{0}{1}", text, arg));
			};
			if (this.IsEnityDataSourceView)
			{
				expression = this.FilterExpressions.ToEntitySQL();
				DataSourceControl obj = (DataSourceControl)DataSourceControlHelper.FindControl(this, this.DataSourceID);
				action(obj);
				return;
			}
			if (this.IsOpenAccessDataSourceView)
			{
				expression = this.FilterExpressions.ToOql();
				action(dataView);
				return;
			}
			expression = this.FilterExpressions.ToDynamicLinq();
			action(dataView);
		}

		// Token: 0x060071D3 RID: 29139 RVA: 0x001AAF0F File Offset: 0x001A910F
		protected override void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			if (!this._ignoreDataSourceViewChanged)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x060071D4 RID: 29140 RVA: 0x001AAF20 File Offset: 0x001A9120
		protected bool IsDataSourceViewWithFiltering()
		{
			return this.IsLinqDataSourceView || this.IsEnityDataSourceView || this.IsOpenAccessDataSourceView;
		}

		// Token: 0x17002529 RID: 9513
		// (get) Token: 0x060071D5 RID: 29141 RVA: 0x001AAF3A File Offset: 0x001A913A
		protected bool IsLinqDataSourceView
		{
			get
			{
				return this.GetData().GetType().ToString() == "System.Web.UI.WebControls.LinqDataSourceView";
			}
		}

		// Token: 0x1700252A RID: 9514
		// (get) Token: 0x060071D6 RID: 29142 RVA: 0x001AAF56 File Offset: 0x001A9156
		protected bool IsEnityDataSourceView
		{
			get
			{
				return this.GetData().GetType().ToString() == "System.Web.UI.WebControls.EntityDataSourceView" || this.GetData().GetType().ToString() == "Microsoft.AspNet.EntityDataSource.EntityDataSourceView";
			}
		}

		// Token: 0x1700252B RID: 9515
		// (get) Token: 0x060071D7 RID: 29143 RVA: 0x001AAF90 File Offset: 0x001A9190
		protected bool IsOpenAccessDataSourceView
		{
			get
			{
				return this.GetData().GetType().ToString().IndexOf("OpenAccess.RT.DataSource.OpenAccessDataSourceView") > -1;
			}
		}

		// Token: 0x060071D8 RID: 29144 RVA: 0x001AAFB0 File Offset: 0x001A91B0
		protected override void PerformDataBinding(IEnumerable data)
		{
			this.DataKeysArrayList.Clear();
			this.TrackViewState();
			int num = this.CreateChildControls(data, true);
			base.ChildControlsCreated = true;
			this.ViewState["_!ItemCount"] = num;
		}

		// Token: 0x060071D9 RID: 29145 RVA: 0x001AAFF4 File Offset: 0x001A91F4
		protected virtual void EnsureLayoutTemplate()
		{
			if (this.Controls.Count == 0 || this._instantiatedEmptyDataTemplate)
			{
				this.Controls.Clear();
				this.InitializeLayoutTemplate();
			}
		}

		// Token: 0x060071DA RID: 29146 RVA: 0x001AB01D File Offset: 0x001A921D
		private int CalculateItemDataIndex(int currentDisplayIndex)
		{
			return currentDisplayIndex + this.CurrentPageIndex * this.PageSize;
		}

		// Token: 0x060071DB RID: 29147 RVA: 0x001AB05C File Offset: 0x001A925C
		protected virtual int CreateItemsWithGroups(Control container, IEnumerable dataSource, bool dataBinding)
		{
			int num = 0;
			if (dataSource == null)
			{
				return num;
			}
			if (this._groupItemWrapperContainer == null)
			{
				this._groupPlaceholderControlIndex = this.PrepareItemContainer(container, true, delegate(Control control)
				{
					this._groupItemWrapperContainer = control;
				}, string.Empty);
			}
			Control tempGroupContainer = this._groupItemWrapperContainer;
			this._groupsItemCreatedinContainerCount = 0;
			int num2 = this._groupPlaceholderControlIndex;
			int num3 = 0;
			int num4 = 0;
			this.Items.Clear();
			if (this.IsItemInserted && this.InsertItemPosition == RadListViewInsertItemPosition.FirstItem)
			{
				RadListViewGroupItem container2 = this.BuildGroupItem(this._groupItemWrapperContainer, num2);
				num2++;
				num4 = this.PrepareItemContainer(container2, false, delegate(Control control)
				{
					tempGroupContainer = control;
				}, string.Empty);
				this.BuildInsertItem(tempGroupContainer, num4, dataBinding);
				num4++;
				num3++;
			}
			foreach (object dataItem in dataSource)
			{
				if (num3 % this.GroupItemCount == 0)
				{
					num2 = this.BuildGroupItemSeparator(this._groupItemWrapperContainer, num2, num3);
					RadListViewGroupItem container3 = this.BuildGroupItem(this._groupItemWrapperContainer, num2);
					num2++;
					num4 = this.PrepareItemContainer(container3, false, delegate(Control control)
					{
						tempGroupContainer = control;
					}, string.Empty);
				}
				num4 = this.BuildItemSeparator(tempGroupContainer, num3 % this.GroupItemCount, num4);
				num3++;
				num = this.BuildDataItem(num, tempGroupContainer, dataBinding, dataItem, ref num4);
			}
			if (this.IsItemInserted && this.InsertItemPosition == RadListViewInsertItemPosition.LastItem)
			{
				if (num3 % this.GroupItemCount == 0)
				{
					num2 = this.BuildGroupItemSeparator(this._groupItemWrapperContainer, num2, num3);
					RadListViewGroupItem container4 = this.BuildGroupItem(this._groupItemWrapperContainer, num2);
					num2++;
					num4 = this.PrepareItemContainer(container4, false, delegate(Control control)
					{
						tempGroupContainer = control;
					}, string.Empty);
				}
				num4 = this.BuildItemSeparator(tempGroupContainer, num3 % this.GroupItemCount, num4);
				this.BuildInsertItem(tempGroupContainer, num4, dataBinding);
				num4++;
				num3++;
			}
			if (this.EmptyItemTemplate != null)
			{
				while (num3 % this.GroupItemCount != 0)
				{
					num4 = this.BuildItemSeparator(tempGroupContainer, num3 % this.GroupItemCount, num4);
					this.BuildEmptyItem(tempGroupContainer, num4);
					num4++;
					num3++;
				}
			}
			this._groupsItemCreatedinContainerCount = num2 - this._groupPlaceholderControlIndex;
			return num;
		}

		// Token: 0x060071DC RID: 29148 RVA: 0x001AB304 File Offset: 0x001A9504
		protected virtual int CreateItemsWithDataGroups(Control container, IEnumerable dataSource, bool dataBinding)
		{
			int result = 0;
			Control _tempContainer = null;
			if (dataSource == null)
			{
				return result;
			}
			this.Items.Clear();
			List<ListViewDataSourceGroup> dataSourceGroups = new List<ListViewDataSourceGroup>();
			int controlIndexInGroup = this.BuildDataGroupsRecursive(container, dataSource, 0, dataBinding, ref result, ref dataSourceGroups, true, "__0level");
			this.DataSourceGroups = dataSourceGroups;
			if (this.EmptyItemTemplate != null)
			{
				controlIndexInGroup = this.PrepareItemContainer(container, false, delegate(Control control)
				{
					_tempContainer = control;
				}, string.Empty);
				this.BuildEmptyItem(_tempContainer, controlIndexInGroup);
			}
			return result;
		}

		// Token: 0x060071DD RID: 29149 RVA: 0x001AB404 File Offset: 0x001A9604
		private int BuildDataGroupsRecursive(Control container, IEnumerable dataSource, int level, bool dataBinding, ref int itemsCreatedCount, ref List<ListViewDataSourceGroup> dsGroups, bool isLastGroup, string aKey)
		{
			IEnumerable<ListViewDataSourceGroup> enumerable = from g in this.DataSourceGroups
			where g.Level == level && (!this.AllowPaging || g.IsOnCurrentPage)
			select g;
			string text = aKey;
			if (level > 0)
			{
				RadListViewDataGroupItem currentDataGroupItem = container as RadListViewDataGroupItem;
				if (currentDataGroupItem != null)
				{
					enumerable = from g in enumerable
					where g.ParentGroup.Key == currentDataGroupItem.DataGroupKey
					select g;
				}
				text = text + level + currentDataGroupItem.DataGroupKey.ToString();
			}
			dsGroups.AddRange(enumerable);
			Control _dataGroupWrapperContainer = null;
			int num = 0;
			int num2 = 0;
			ListViewDataGroup listViewDataGroup = this.DataGroups[level];
			ListDictionary listDictionary = null;
			bool flag = listViewDataGroup.GroupAggregates.Count > 0;
			string dataGroupPlaceholderID = listViewDataGroup.DataGroupPlaceholderID;
			if (!this._dataGroupWrapperContainers.ContainsKey(text))
			{
				num = this.PrepareItemContainer(container, true, delegate(Control control)
				{
					_dataGroupWrapperContainer = control;
				}, dataGroupPlaceholderID);
				this._dataGroupWrapperContainers[text] = _dataGroupWrapperContainer;
			}
			else
			{
				_dataGroupWrapperContainer = this._dataGroupWrapperContainers[text];
				if (_dataGroupWrapperContainer.Parent == null)
				{
					num = this.PrepareItemContainer(container, true, delegate(Control control)
					{
						_dataGroupWrapperContainer = control;
					}, dataGroupPlaceholderID);
					this._dataGroupWrapperContainers[text] = _dataGroupWrapperContainer;
				}
			}
			if (level == 0)
			{
				if (this._firstDataGroupWrapperContainer != null)
				{
					num = this._firstLevelDataGroupControlIndex;
				}
				if (this._firstDataGroupWrapperContainer == null)
				{
					this._firstLevelDataGroupsCount = 0;
					this._firstDataGroupWrapperContainer = _dataGroupWrapperContainer;
					this._firstLevelDataGroupControlIndex = num;
				}
				if (this.IsItemInserted && this.InsertItemPosition == RadListViewInsertItemPosition.BeforeDataGroups)
				{
					this.BuildInsertItem(_dataGroupWrapperContainer, num, dataBinding);
					num2++;
					this._firstLevelDataGroupsCount++;
					num++;
				}
			}
			foreach (ListViewDataSourceGroup listViewDataSourceGroup in enumerable)
			{
				num = this.BuildGroupItemSeparator(_dataGroupWrapperContainer, num, num2);
				if (dataBinding && flag)
				{
					listDictionary = new ListDictionary();
					foreach (ListViewDataGroupAggregate listViewDataGroupAggregate in listViewDataGroup.GroupAggregates)
					{
						string dataField = listViewDataGroupAggregate.DataField;
						if (this.ItemPropertyTypes.ContainsKey(dataField))
						{
							IEnumerable aggregateItems = listViewDataSourceGroup.AggregateItems;
							object value;
							if (listViewDataGroupAggregate.Aggregate == ListViewAggregateFunction.Custom)
							{
								ListViewCustomAggregateEventArgs listViewCustomAggregateEventArgs = new ListViewCustomAggregateEventArgs(listViewDataGroup, aggregateItems, dataField);
								this.FireCustomAggregate(listViewCustomAggregateEventArgs);
								value = listViewCustomAggregateEventArgs.Result;
							}
							else
							{
								value = ListViewLinqGroupingHelper.GetAggregate(aggregateItems, dataField, this.ItemPropertyTypes[dataField], listViewDataGroupAggregate.Aggregate);
							}
							listDictionary.Add(dataField, value);
						}
					}
				}
				RadListViewDataGroupItem container2 = this.BuildDataGroupItem(listViewDataGroup.DataGroupTemplate, _dataGroupWrapperContainer, num, dataBinding, listDictionary, listViewDataSourceGroup);
				if (level == 0)
				{
					this._firstLevelDataGroupsCount++;
					if (this.GroupSeparatorTemplate != null && num2 > 0)
					{
						this._firstLevelDataGroupsCount++;
					}
				}
				bool flag2 = this._firstLevelDataGroupsCount == 1 || (this.GroupSeparatorTemplate != null && num2 > 0 && this._firstLevelDataGroupsCount == 2);
				num++;
				num2++;
				if (level == this.DataGroups.Count - 1)
				{
					Control itemContainer = null;
					int num3 = this.PrepareItemContainer(container2, false, delegate(Control control)
					{
						itemContainer = control;
					}, string.Empty);
					if (flag2 && this.IsItemInserted && this.InsertItemPosition == RadListViewInsertItemPosition.FirstItem)
					{
						this.BuildInsertItem(itemContainer, num3, dataBinding);
						itemsCreatedCount++;
						num3++;
					}
					if (dataBinding)
					{
						IEnumerable dataItems = listViewDataSourceGroup.DataItems;
						using (IEnumerator enumerator3 = dataItems.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								object dataItem = enumerator3.Current;
								num3 = this.BuildItemSeparator(itemContainer, itemsCreatedCount, num3);
								itemsCreatedCount = this.BuildDataItem(itemsCreatedCount, itemContainer, dataBinding, dataItem, ref num3);
							}
							goto IL_4A3;
						}
						goto IL_45D;
					}
					goto IL_45D;
					IL_4A3:
					if (isLastGroup && num2 == enumerable.Count<ListViewDataSourceGroup>() && this.IsItemInserted && this.InsertItemPosition == RadListViewInsertItemPosition.LastItem)
					{
						num3 = this.BuildItemSeparator(itemContainer, itemsCreatedCount, num3);
						this.BuildInsertItem(itemContainer, num3, dataBinding);
						itemsCreatedCount++;
						num3++;
						continue;
					}
					continue;
					IL_45D:
					int dataItemsCount = listViewDataSourceGroup.DataItemsCount;
					for (int i = 0; i < dataItemsCount; i++)
					{
						num3 = this.BuildItemSeparator(itemContainer, itemsCreatedCount, num3);
						itemsCreatedCount = this.BuildDataItem(itemsCreatedCount, itemContainer, dataBinding, null, ref num3);
					}
					goto IL_4A3;
				}
				bool isLastGroup2 = isLastGroup && num2 == enumerable.Count<ListViewDataSourceGroup>();
				this.BuildDataGroupsRecursive(container2, dataSource, level + 1, dataBinding, ref itemsCreatedCount, ref dsGroups, isLastGroup2, text);
			}
			if (level == 0 && this.IsItemInserted && this.InsertItemPosition == RadListViewInsertItemPosition.AfterDataGroups)
			{
				int num4 = num2;
				num = this.BuildGroupItemSeparator(_dataGroupWrapperContainer, num, num2);
				this.BuildInsertItem(_dataGroupWrapperContainer, num, dataBinding);
				num2++;
				this._firstLevelDataGroupsCount++;
				if (this.GroupSeparatorTemplate != null && num2 != num4)
				{
					this._firstLevelDataGroupsCount++;
				}
				num++;
			}
			return num + this._firstLevelDataGroupsCount;
		}

		// Token: 0x060071DE RID: 29150 RVA: 0x001ABA20 File Offset: 0x001A9C20
		private void BuildEmptyItem(Control tempGroupContainer, int controlIndexInGroup)
		{
			RadListViewItem radListViewItem = this.CreateEmptyItem();
			this.InstantiateEmptyItemTemplate(radListViewItem);
			this.AddItemToContainer(tempGroupContainer, radListViewItem, controlIndexInGroup);
			this.OnItemCreated(new RadListViewItemEventArgs(radListViewItem));
		}

		// Token: 0x060071DF RID: 29151 RVA: 0x001ABA50 File Offset: 0x001A9C50
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private int BuildDataItem(int itemsCreatedCount, Control itemContainer, bool dataBinding, object dataItem, ref int controlIndex)
		{
			RadListViewDataItem radListViewDataItem = this.CreateDataItem(itemsCreatedCount);
			radListViewDataItem.DataItemIndex = this.CalculateItemDataIndex(itemsCreatedCount);
			itemsCreatedCount++;
			this.AutoIDControl(radListViewDataItem);
			this.InstantiateDataItemTemplate(itemsCreatedCount, radListViewDataItem);
			if (dataBinding)
			{
				this.PopulateDataKeys(dataItem);
			}
			this.AddItemToContainer(itemContainer, radListViewDataItem, controlIndex);
			controlIndex++;
			this.OnItemCreated(new RadListViewItemEventArgs(radListViewDataItem));
			this.Items.Add(radListViewDataItem);
			if (dataBinding)
			{
				radListViewDataItem.DataItem = dataItem;
				radListViewDataItem.DataBind();
				this.OnItemDataBound(new RadListViewItemEventArgs(radListViewDataItem));
				radListViewDataItem.ExtractValues(radListViewDataItem.SavedOldValues);
			}
			return itemsCreatedCount;
		}

		// Token: 0x060071E0 RID: 29152 RVA: 0x001ABAE8 File Offset: 0x001A9CE8
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private int BuildGroupItemSeparator(Control groupItemContainer, int controlIndex, int itemsInGroup)
		{
			if (itemsInGroup != 0 && this.GroupSeparatorTemplate != null)
			{
				Control control = new Control();
				this.AutoIDControl(control);
				this.InstantiateGroupSeparatorTemplate(control);
				if (!(groupItemContainer is Table) || control is TableRow)
				{
					this.AddItemSeparatorToContainer(groupItemContainer, control, controlIndex);
					controlIndex++;
				}
			}
			return controlIndex;
		}

		// Token: 0x060071E1 RID: 29153 RVA: 0x001ABB34 File Offset: 0x001A9D34
		private RadListViewGroupItem BuildGroupItem(Control groupItemContainer, int controlIndex)
		{
			RadListViewGroupItem radListViewGroupItem = this.CreateGroupItem();
			this.AutoIDControl(radListViewGroupItem);
			this.InstantiateGroupTemplate(radListViewGroupItem);
			this.AddItemToContainer(groupItemContainer, radListViewGroupItem, controlIndex);
			this.OnItemCreated(new RadListViewItemEventArgs(radListViewGroupItem));
			return radListViewGroupItem;
		}

		// Token: 0x060071E2 RID: 29154 RVA: 0x001ABB6C File Offset: 0x001A9D6C
		private RadListViewDataGroupItem BuildDataGroupItem(ITemplate dataGroupTemplate, Control dataGroupItemContainer, int controlIndex, bool dataBinding, ListDictionary aggregatesValues, ListViewDataSourceGroup dataSourceGroup)
		{
			RadListViewDataGroupItem radListViewDataGroupItem = this.CreateDataGroupItem();
			radListViewDataGroupItem.AggregatesValues = aggregatesValues;
			radListViewDataGroupItem.FieldName = dataSourceGroup.FieldName;
			radListViewDataGroupItem.DataGroupKey = dataSourceGroup.Key;
			this.AutoIDControl(radListViewDataGroupItem);
			this.InstantiateDataGroupTemplate(dataGroupTemplate, radListViewDataGroupItem);
			this.AddItemToContainer(dataGroupItemContainer, radListViewDataGroupItem, controlIndex);
			this.OnItemCreated(new RadListViewItemEventArgs(radListViewDataGroupItem));
			if (dataBinding)
			{
				radListViewDataGroupItem.DataBind();
				this.OnItemDataBound(new RadListViewItemEventArgs(radListViewDataGroupItem));
			}
			return radListViewDataGroupItem;
		}

		// Token: 0x060071E3 RID: 29155 RVA: 0x001ABBDD File Offset: 0x001A9DDD
		protected virtual RadListViewGroupItem CreateGroupItem()
		{
			return new RadListViewGroupItem(this);
		}

		// Token: 0x060071E4 RID: 29156 RVA: 0x001ABBE5 File Offset: 0x001A9DE5
		protected virtual RadListViewDataGroupItem CreateDataGroupItem()
		{
			return new RadListViewDataGroupItem(this);
		}

		// Token: 0x060071E5 RID: 29157 RVA: 0x001ABBED File Offset: 0x001A9DED
		protected virtual RadListViewEmptyItem CreateEmptyItem()
		{
			return new RadListViewEmptyItem(this);
		}

		// Token: 0x060071E6 RID: 29158 RVA: 0x001ABBF5 File Offset: 0x001A9DF5
		protected virtual void InstantiateEmptyItemTemplate(Control container)
		{
			if (this.EmptyItemTemplate != null)
			{
				this.EmptyItemTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x060071E7 RID: 29159 RVA: 0x001ABC0B File Offset: 0x001A9E0B
		protected virtual void InstantiateGroupTemplate(Control container)
		{
			if (this.GroupTemplate != null)
			{
				this.GroupTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x060071E8 RID: 29160 RVA: 0x001ABC21 File Offset: 0x001A9E21
		protected virtual void InstantiateDataGroupTemplate(ITemplate dataGroupTemplate, Control container)
		{
			if (dataGroupTemplate != null)
			{
				dataGroupTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x060071E9 RID: 29161 RVA: 0x001ABC38 File Offset: 0x001A9E38
		protected virtual int CreateDataItems(Control container, IEnumerable dataSource, bool dataBinding)
		{
			int num = 0;
			if (dataSource == null)
			{
				return num;
			}
			if (this._itemsWrapperContainer == null)
			{
				this._placeholderControlIndex = this.PrepareItemContainer(container, false, delegate(Control control)
				{
					this._itemsWrapperContainer = control;
				}, string.Empty);
			}
			int num2 = this._placeholderControlIndex;
			bool flag = false;
			this.Items.Clear();
			if (this.IsItemInserted && this.InsertItemPosition == RadListViewInsertItemPosition.FirstItem)
			{
				this.BuildInsertItem(this._itemsWrapperContainer, num2, dataBinding);
				num2++;
				flag = true;
			}
			foreach (object dataItem in dataSource)
			{
				if (flag)
				{
					num2 = this.BuildItemSeparator(this._itemsWrapperContainer, num2);
				}
				else
				{
					num2 = this.BuildItemSeparator(this._itemsWrapperContainer, num, num2);
				}
				num = this.BuildDataItem(num, this._itemsWrapperContainer, dataBinding, dataItem, ref num2);
			}
			if (this.IsItemInserted && this.InsertItemPosition == RadListViewInsertItemPosition.LastItem)
			{
				num2 = this.BuildItemSeparator(this._itemsWrapperContainer, num, num2);
				this.BuildInsertItem(this._itemsWrapperContainer, num2, dataBinding);
				num2++;
			}
			this._itemsCreatedInContainerCount = num2 - this._placeholderControlIndex;
			return num;
		}

		// Token: 0x060071EA RID: 29162 RVA: 0x001ABD70 File Offset: 0x001A9F70
		protected void BuildInsertItem(Control itemsContainer, int controlIndex, bool dataBinding)
		{
			RadListViewItem radListViewItem = this.CreateInsertItem();
			this.AddItemToContainer(itemsContainer, radListViewItem, controlIndex);
			this.OnItemCreated(new RadListViewItemEventArgs(radListViewItem));
			if (dataBinding)
			{
				((RadListViewInsertItem)radListViewItem).DataItem = this.GetDefaultInsertionObject();
				radListViewItem.DataBind();
			}
		}

		// Token: 0x060071EB RID: 29163 RVA: 0x001ABDB4 File Offset: 0x001A9FB4
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		protected int BuildItemSeparator(Control itemsContainer, int itemsCreatedCount, int controlIndex)
		{
			if (itemsCreatedCount > 0 && this.ItemSeparatorTemplate != null)
			{
				Control control = new Control();
				this.AutoIDControl(control);
				this.InstantiateItemSeparatorTemplate(control);
				if ((!(itemsContainer is Table) || control is TableRow) && (!(itemsContainer is TableRow) || control is TableCell))
				{
					this.AddItemSeparatorToContainer(itemsContainer, control, controlIndex);
					controlIndex++;
				}
			}
			return controlIndex;
		}

		// Token: 0x060071EC RID: 29164 RVA: 0x001ABE10 File Offset: 0x001AA010
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		protected int BuildItemSeparator(Control itemsContainer, int controlIndex)
		{
			Control control = new Control();
			this.AutoIDControl(control);
			this.InstantiateItemSeparatorTemplate(control);
			if ((!(itemsContainer is Table) || control is TableRow) && (!(itemsContainer is TableRow) || control is TableCell))
			{
				this.AddItemSeparatorToContainer(itemsContainer, control, controlIndex);
				controlIndex++;
			}
			return controlIndex;
		}

		// Token: 0x060071ED RID: 29165 RVA: 0x001ABE60 File Offset: 0x001AA060
		protected virtual RadListViewItem CreateInsertItem()
		{
			if (this.InsertItemTemplate == null)
			{
				throw new InvalidOperationException("The RadListView control does not have an InsertItemTemplate template specified.");
			}
			RadListViewInsertItem radListViewInsertItem = new RadListViewInsertItem(this, -1);
			this.InsertItem = radListViewInsertItem;
			this.AutoIDControl(radListViewInsertItem);
			this.InstantiateInsertItemTemplate(radListViewInsertItem);
			return radListViewInsertItem;
		}

		// Token: 0x060071EE RID: 29166 RVA: 0x001ABE9E File Offset: 0x001AA09E
		protected virtual void InstantiateInsertItemTemplate(Control container)
		{
			if (this.InsertItemTemplate != null)
			{
				this.InsertItemTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x060071EF RID: 29167 RVA: 0x001ABEB4 File Offset: 0x001AA0B4
		protected internal void PopulateDataKeys(object dataItem)
		{
			if (this.IsDesignMode || (this.DataKeyNamesInternal.Length == 0 && this.ClientDataKeyNames.Length == 0))
			{
				return;
			}
			DataKey dataKey = new DataKey(base.IsTrackingViewState);
			this.DataKeysArrayList.Add(dataKey);
			try
			{
				foreach (string text in this.DataKeyNamesInternal)
				{
					dataKey[text] = this.ExtractDataKeyValue(dataItem, text);
				}
				foreach (string text2 in this.ClientDataKeyNamesInternal)
				{
					if (dataKey[text2] == null)
					{
						dataKey[text2] = this.ExtractDataKeyValue(dataItem, text2);
					}
				}
			}
			catch (ArgumentNullException innerException)
			{
				throw new ArgumentException("There was a problem extracting DataKeyValues from the DataSource. Please ensure that DataKeyNames are specified correctly and all fields specified exist in the DataSource.", innerException);
			}
			catch (HttpException innerException2)
			{
				throw new ArgumentException("There was a problem extracting DataKeyValues from the DataSource. Please ensure that DataKeyNames are specified correctly and all fields specified exist in the DataSource.", innerException2);
			}
		}

		// Token: 0x060071F0 RID: 29168 RVA: 0x001ABF9C File Offset: 0x001AA19C
		protected virtual void AddItemSeparatorToContainer(Control container, Control itemSeparatorContainer, int index)
		{
			container.Controls.AddAt(index, itemSeparatorContainer);
		}

		// Token: 0x060071F1 RID: 29169 RVA: 0x001ABFAB File Offset: 0x001AA1AB
		protected virtual void InstantiateItemSeparatorTemplate(Control container)
		{
			if (this.ItemSeparatorTemplate != null)
			{
				this.ItemSeparatorTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x060071F2 RID: 29170 RVA: 0x001ABFC1 File Offset: 0x001AA1C1
		protected virtual void InstantiateGroupSeparatorTemplate(Control container)
		{
			if (this.GroupSeparatorTemplate != null)
			{
				this.GroupSeparatorTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x060071F3 RID: 29171 RVA: 0x001ABFD8 File Offset: 0x001AA1D8
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void InstantiateDataItemTemplate(int itemsCreatedCount, RadListViewDataItem listViewItem)
		{
			ITemplate template = this.ItemTemplate;
			if (this.ItemTemplate == null)
			{
				throw new InvalidOperationException("The RadListView control does not have an ItemTemplate template specified.");
			}
			if (this.EditItemTemplate != null && this.EditIndexes.Contains(itemsCreatedCount - 1))
			{
				template = this.EditItemTemplate;
			}
			else if (this.SelectedItemTemplate != null && this.SelectedIndexes.Contains(itemsCreatedCount - 1))
			{
				template = this.SelectedItemTemplate;
			}
			else if (itemsCreatedCount % 2 == 0 && this.AlternatingItemTemplate != null)
			{
				template = this.AlternatingItemTemplate;
			}
			template.InstantiateIn(listViewItem);
		}

		// Token: 0x060071F4 RID: 29172 RVA: 0x001AC05C File Offset: 0x001AA25C
		protected virtual int PrepareItemContainer(Control container, bool isGroupContainer, Action<Control> retrieveItemPlaceHolderParentControl, string groupPlaceholderID)
		{
			Control control;
			if (isGroupContainer)
			{
				if (this.DataGroups != null && this.DataGroups.Count > 0)
				{
					control = this.RetrivePlaceHolderControl(container, groupPlaceholderID);
				}
				else
				{
					control = this.RetrivePlaceHolderControl(container, this.GroupPlaceholderID);
				}
			}
			else
			{
				control = this.RetrivePlaceHolderControl(container, this.ItemPlaceholderID);
			}
			Control parent = control.Parent;
			retrieveItemPlaceHolderParentControl(parent);
			int result = parent.Controls.IndexOf(control);
			parent.Controls.Remove(control);
			return result;
		}

		// Token: 0x060071F5 RID: 29173 RVA: 0x001AC0D4 File Offset: 0x001AA2D4
		protected virtual Control RetrivePlaceHolderControl(Control container, string placeholderId)
		{
			Control control = this.ControlLocator.RetriveFromContainer(container, placeholderId);
			if (control == null)
			{
				throw new InvalidOperationException("The RadListView control does not have an item placeholder specified.");
			}
			return control;
		}

		// Token: 0x060071F6 RID: 29174 RVA: 0x001AC100 File Offset: 0x001AA300
		protected virtual RadListViewDataItem CreateDataItem(int displayIndex)
		{
			if (this.EditItemTemplate != null && this.EditIndexes.Contains(displayIndex))
			{
				return new RadListViewEditableItem(this, displayIndex);
			}
			if (this.SelectedItemTemplate != null && this.SelectedIndexes.Contains(displayIndex))
			{
				return new RadListViewDataItem(this, displayIndex, RadListViewItemType.SelectedItem);
			}
			if (displayIndex % 2 != 0)
			{
				return new RadListViewDataItem(this, displayIndex, RadListViewItemType.AlternatingItem);
			}
			return new RadListViewDataItem(this, displayIndex);
		}

		// Token: 0x060071F7 RID: 29175 RVA: 0x001AC160 File Offset: 0x001AA360
		protected virtual void CreateEmptyDataItem(int dataItemsCount)
		{
			if (dataItemsCount == 0 && this.EmptyDataTemplate != null && !this.IsItemInserted)
			{
				this._instantiatedEmptyDataTemplate = true;
				this.Controls.Clear();
				RadListViewEmptyDataItem radListViewEmptyDataItem = new RadListViewEmptyDataItem(this);
				this.EmptyDataTemplate.InstantiateIn(radListViewEmptyDataItem);
				this.OnItemCreated(new RadListViewItemEventArgs(radListViewEmptyDataItem));
				this.Controls.Add(radListViewEmptyDataItem);
			}
		}

		// Token: 0x060071F8 RID: 29176 RVA: 0x001AC1C0 File Offset: 0x001AA3C0
		protected virtual void AutoDataBind(RadListViewRebindReason rebindReason)
		{
			if (!this.Visible && (rebindReason & RadListViewRebindReason.ExplicitRebind) != RadListViewRebindReason.ExplicitRebind)
			{
				return;
			}
			this.ObtainDataSource(rebindReason, base.IsBoundUsingDataSourceID);
			if ((this.DataSource != null && !base.IsBoundUsingDataSourceID) || (base.IsBoundUsingDataSourceID && rebindReason == RadListViewRebindReason.ExplicitRebind) || (this.DataSource != null && rebindReason == RadListViewRebindReason.ExplicitRebind) || (this.IsUsingModelBinding && rebindReason == RadListViewRebindReason.ExplicitRebind))
			{
				this.DataBind();
			}
		}

		// Token: 0x060071F9 RID: 29177 RVA: 0x001AC223 File Offset: 0x001AA423
		protected override void PerformSelect()
		{
			this._ignoreDataSourceViewChanged = true;
			this._currentDataSource = null;
			base.PerformSelect();
			this._ignoreDataSourceViewChanged = false;
		}

		// Token: 0x060071FA RID: 29178 RVA: 0x001AC240 File Offset: 0x001AA440
		protected override DataSourceView GetData()
		{
			if (this._currentDataSource == null)
			{
				if (this.IsBoundToIQueryableCollection)
				{
					this._currentDataSource = ((IDataSource)new RadListView.DummyDataSource((IEnumerable)this.DataSource)).GetView(this.DataMember);
				}
				else
				{
					this._currentDataSource = base.GetData();
				}
			}
			return this._currentDataSource;
		}

		// Token: 0x1700252C RID: 9516
		// (get) Token: 0x060071FB RID: 29179 RVA: 0x001AC292 File Offset: 0x001AA492
		protected bool IsBoundToIQueryableCollection
		{
			get
			{
				return !base.IsBoundUsingDataSourceID && this.DataSource is IQueryable;
			}
		}

		// Token: 0x1700252D RID: 9517
		// (get) Token: 0x060071FC RID: 29180 RVA: 0x001AC2AC File Offset: 0x001AA4AC
		protected virtual bool IsBoundUsingOData
		{
			get
			{
				return !string.IsNullOrEmpty(this.ODataDataSourceID);
			}
		}

		// Token: 0x060071FD RID: 29181 RVA: 0x001AC2BC File Offset: 0x001AA4BC
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			bool flag = false;
			RadListViewCommandEventArgs radListViewCommandEventArgs = args as RadListViewCommandEventArgs;
			if (radListViewCommandEventArgs != null)
			{
				this.OnItemCommand(radListViewCommandEventArgs);
				flag = true;
			}
			IRadListViewCommandEvent radListViewCommandEvent = args as IRadListViewCommandEvent;
			if (radListViewCommandEvent != null)
			{
				if (!radListViewCommandEvent.Canceled)
				{
					radListViewCommandEvent.ExecuteCommand(source);
				}
				flag = true;
			}
			CommandEventArgs commandEventArgs = args as CommandEventArgs;
			if (!flag && commandEventArgs != null)
			{
				RadListViewCommandEventArgs radListViewCommandEventArgs2 = RadListViewCommandEventArgsFactory.CreateCommandEventArgs(new RadListViewItem(RadListViewItemType.EditItem, this), source, commandEventArgs);
				this.OnItemCommand(radListViewCommandEventArgs2);
				flag = (radListViewCommandEventArgs2.Canceled || RadListViewCommandEventArgsFactory.HandleCommand(this, source, commandEventArgs));
			}
			return flag;
		}

		// Token: 0x060071FE RID: 29182 RVA: 0x001AC338 File Offset: 0x001AA538
		private Control RetriveDataItemsContainer(Control container, string itemPlaceholderId)
		{
			Control control = container.FindControl(itemPlaceholderId);
			if (control == null)
			{
				throw new InvalidOperationException("The RadListView control does not have an item placeholder specified.");
			}
			return control;
		}

		// Token: 0x060071FF RID: 29183 RVA: 0x001AC35C File Offset: 0x001AA55C
		protected virtual int InitializeLayoutTemplate()
		{
			this._itemsWrapperContainer = null;
			this._itemsCreatedInContainerCount = 0;
			this._groupItemWrapperContainer = null;
			this.LayoutTemplateWrapper = new Control();
			if (this.LayoutTemplate == null)
			{
				this.LayoutTemplate = new RadListViewDefaultLayoutTemplate(this.ItemPlaceholderID);
			}
			this.LayoutTemplate.InstantiateIn(this.LayoutTemplateWrapper);
			this.Controls.Add(this.LayoutTemplateWrapper);
			if (!base.DesignMode && string.IsNullOrEmpty(this.ClientSettings.DataBinding.ItemPlaceHolderID))
			{
				this.ClientSettings.DataBinding.ItemPlaceHolderID = this.GetItemPlaceHolderClientID();
			}
			this.OnLayoutCreated(new EventArgs());
			return 1;
		}

		// Token: 0x06007200 RID: 29184 RVA: 0x001AC408 File Offset: 0x001AA608
		private string GetItemPlaceHolderClientID()
		{
			if (this.LayoutTemplateWrapper != null && !string.IsNullOrEmpty(this.ItemPlaceholderID))
			{
				Control control = this.LayoutTemplateWrapper.FindControl(this.ItemPlaceholderID);
				if (control != null)
				{
					return control.ClientID;
				}
			}
			return string.Empty;
		}

		// Token: 0x06007201 RID: 29185 RVA: 0x001AC44B File Offset: 0x001AA64B
		private void SetRequiresDataBindingIfInitialized()
		{
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x06007202 RID: 29186 RVA: 0x001AC45C File Offset: 0x001AA65C
		private void AddItemToContainer(Control container, RadListViewItem listViewItem, int controlIndex)
		{
			if (container is HtmlTableRow)
			{
				RadListViewHtmlTableCell radListViewHtmlTableCell = new RadListViewHtmlTableCell();
				radListViewHtmlTableCell.Controls.Add(listViewItem);
				container.Controls.AddAt(controlIndex, radListViewHtmlTableCell);
				return;
			}
			if (container is HtmlTable)
			{
				RadListViewHtmlTableRow radListViewHtmlTableRow = new RadListViewHtmlTableRow();
				radListViewHtmlTableRow.Controls.Add(listViewItem);
				container.Controls.AddAt(controlIndex, radListViewHtmlTableRow);
				return;
			}
			if (container is TableRow)
			{
				RadListViewTableCell radListViewTableCell = new RadListViewTableCell();
				radListViewTableCell.Controls.Add(listViewItem);
				container.Controls.AddAt(controlIndex, radListViewTableCell);
				return;
			}
			if (container is Table)
			{
				RadListViewTableRow radListViewTableRow = new RadListViewTableRow();
				radListViewTableRow.Controls.Add(listViewItem);
				container.Controls.AddAt(controlIndex, radListViewTableRow);
				return;
			}
			container.Controls.AddAt(controlIndex, listViewItem);
		}

		// Token: 0x06007203 RID: 29187 RVA: 0x001AC516 File Offset: 0x001AA716
		internal void ObtainDataSource(RadListViewRebindReason rebindReason, bool isBoundUsingDataSourceId)
		{
			if (!this.DataSourceIsAssigned && !isBoundUsingDataSourceId)
			{
				this.OnNeedDataSource(new RadListViewNeedDataSourceEventArgs(rebindReason));
			}
		}

		// Token: 0x06007204 RID: 29188 RVA: 0x001AC52F File Offset: 0x001AA72F
		internal void ObtainDataSource(RadListViewRebindReason rebindReason)
		{
			this.ObtainDataSource(rebindReason, base.IsBoundUsingDataSourceID);
		}

		// Token: 0x1700252E RID: 9518
		// (get) Token: 0x06007205 RID: 29189 RVA: 0x001AC53E File Offset: 0x001AA73E
		internal bool IsBoundUsingDataSourceIDInternal
		{
			get
			{
				return base.IsBoundUsingDataSourceID;
			}
		}

		// Token: 0x1700252F RID: 9519
		// (get) Token: 0x06007206 RID: 29190 RVA: 0x001AC546 File Offset: 0x001AA746
		internal bool UsesControlState
		{
			get
			{
				return !base.IsViewStateEnabled;
			}
		}

		// Token: 0x06007207 RID: 29191 RVA: 0x001AC554 File Offset: 0x001AA754
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int current = 0;
				base.LoadViewState(array[current++]);
				((IStateManager)this.ClientSettings).LoadViewState(array[current++]);
				if (!this.UsesControlState)
				{
					this.LoadControlStateObject(array, current);
				}
				this.EnsureChildControls();
			}
		}

		// Token: 0x06007208 RID: 29192 RVA: 0x001AC5A4 File Offset: 0x001AA7A4
		protected virtual void LoadControlStateObject(object[] objArray1, int current)
		{
			((IStateManager)this.ControlState).LoadViewState(objArray1[current++]);
			((IStateManager)this.DataKeyValues).LoadViewState(objArray1[current++]);
			((IStateManager)this.SortExpressions).LoadViewState(objArray1[current++]);
			((IStateManager)this.FilterExpressions).LoadViewState(objArray1[current]);
		}

		// Token: 0x06007209 RID: 29193 RVA: 0x001AC5F8 File Offset: 0x001AA7F8
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			object value = base.SaveViewState();
			arrayList.Add(value);
			arrayList.Add(((IStateManager)this.ClientSettings).SaveViewState());
			if (!this.UsesControlState)
			{
				this.SaveControlStateObject(arrayList);
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600720A RID: 29194 RVA: 0x001AC64C File Offset: 0x001AA84C
		protected virtual void SaveControlStateObject(IList state)
		{
			state.Add(((IStateManager)this.ControlState).SaveViewState());
			state.Add(((IStateManager)this.DataKeyValues).SaveViewState());
			state.Add(((IStateManager)this.SortExpressions).SaveViewState());
			state.Add(((IStateManager)this.FilterExpressions).SaveViewState());
		}

		// Token: 0x0600720B RID: 29195 RVA: 0x001AC6A4 File Offset: 0x001AA8A4
		protected override void TrackViewState()
		{
			if (base.IsTrackingViewState)
			{
				base.TrackViewState();
				return;
			}
			base.TrackViewState();
			((IStateManager)this.DataKeyValues).TrackViewState();
			((IStateManager)this.ControlState).TrackViewState();
			((IStateManager)this.SortExpressions).TrackViewState();
			((IStateManager)this.FilterExpressions).TrackViewState();
		}

		// Token: 0x0600720C RID: 29196 RVA: 0x001AC6F4 File Offset: 0x001AA8F4
		protected override object SaveControlState()
		{
			object value = base.SaveControlState();
			ArrayList arrayList = new ArrayList();
			arrayList.Add(value);
			this.SaveControlStateObject(arrayList);
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600720D RID: 29197 RVA: 0x001AC730 File Offset: 0x001AA930
		protected override void LoadControlState(object savedState)
		{
			object[] array = savedState as object[];
			if (array != null)
			{
				base.LoadControlState(array);
				this.LoadControlStateObject(array, 1);
			}
			else
			{
				base.LoadControlState(savedState);
			}
			if (!base.IsViewStateEnabled)
			{
				int totalRowCount = (this.VirtualItemCount > 0) ? this.VirtualItemCount : this.GetDataItemCountFromState();
				this.OnTotalRowCountAvailable(new RadDataPagerPageEventArgs(this.StartRowIndex, this.PageSize, totalRowCount));
			}
		}

		// Token: 0x0600720E RID: 29198 RVA: 0x001AC797 File Offset: 0x001AA997
		private int GetDataItemCountFromState()
		{
			return (int)(this.ControlState.ContainsKey("_!DSIC") ? this.ControlState["_!DSIC"] : 0);
		}

		// Token: 0x0600720F RID: 29199 RVA: 0x001AC7C8 File Offset: 0x001AA9C8
		protected virtual void SetPageProperties(int startRowIndex, int maximumRows, bool databind)
		{
			if (maximumRows < 1)
			{
				throw new ArgumentOutOfRangeException("maximumRows");
			}
			if (startRowIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startRowIndex");
			}
			bool flag = false;
			if (startRowIndex != this.StartRowIndex)
			{
				flag = true;
			}
			if (maximumRows != this.PageSize)
			{
				this.PageSize = maximumRows;
			}
			if (databind)
			{
				if (flag)
				{
					int num = Math.Max(1, this.PageSize);
					int num2 = startRowIndex / num;
					RadListViewPageChangedEventArgs radListViewPageChangedEventArgs = new RadListViewPageChangedEventArgs(null, null, num2)
					{
						NewPageIndex = num2
					};
					this.FirePageIndexChanged(radListViewPageChangedEventArgs);
					if (radListViewPageChangedEventArgs.Canceled)
					{
						return;
					}
					this.CurrentPageIndex = num2;
				}
				base.RequiresDataBinding = true;
				this.ClearEditItems();
				this.ClearSelectedIndexes();
			}
		}

		// Token: 0x06007210 RID: 29200 RVA: 0x001AC86B File Offset: 0x001AAA6B
		void IRadPageableItemContainer.SetPageProperties(int startRowIndex, int maximumRows, bool databind)
		{
			this.SetPageProperties(startRowIndex, maximumRows, databind);
		}

		// Token: 0x17002530 RID: 9520
		// (get) Token: 0x06007211 RID: 29201 RVA: 0x001AC876 File Offset: 0x001AAA76
		int IRadPageableItemContainer.MaximumRows
		{
			get
			{
				return this.PageSize;
			}
		}

		// Token: 0x17002531 RID: 9521
		// (get) Token: 0x06007212 RID: 29202 RVA: 0x001AC87E File Offset: 0x001AAA7E
		protected virtual int StartRowIndex
		{
			get
			{
				return this.CurrentPageIndex * this.PageSize;
			}
		}

		// Token: 0x17002532 RID: 9522
		// (get) Token: 0x06007213 RID: 29203 RVA: 0x001AC88D File Offset: 0x001AAA8D
		int IRadPageableItemContainer.StartRowIndex
		{
			get
			{
				return this.StartRowIndex;
			}
		}

		// Token: 0x06007214 RID: 29204 RVA: 0x001AC895 File Offset: 0x001AAA95
		internal virtual void AddEditIndex(int itemDisplayIndex)
		{
			if (!this.AllowMultiItemEdit)
			{
				this.EditIndexes.Clear();
			}
			this.EditIndexes.Add(itemDisplayIndex);
		}

		// Token: 0x06007215 RID: 29205 RVA: 0x001AC8B6 File Offset: 0x001AAAB6
		internal virtual void RemoveEditIndex(int itemDisplayIndex)
		{
			this.EditIndexes.Remove(itemDisplayIndex);
		}

		// Token: 0x06007216 RID: 29206 RVA: 0x001AC8C5 File Offset: 0x001AAAC5
		internal virtual void AddSelectedIndex(int itemDisplayIndex)
		{
			if (!this.AllowMultiItemSelection)
			{
				this.SelectedIndexes.Clear();
			}
			this.SelectedIndexes.Add(itemDisplayIndex);
		}

		// Token: 0x06007217 RID: 29207 RVA: 0x001AC8E6 File Offset: 0x001AAAE6
		internal virtual void RemoveSelectedIndex(int itemDisplayIndex)
		{
			this.SelectedIndexes.Remove(itemDisplayIndex);
		}

		// Token: 0x06007218 RID: 29208 RVA: 0x001AC8F5 File Offset: 0x001AAAF5
		internal void ClearSelectedIndexes()
		{
			this.SelectedIndexes.Clear();
		}

		// Token: 0x06007219 RID: 29209 RVA: 0x001AC904 File Offset: 0x001AAB04
		private void FillDataKeys(IDictionary keys, RadListViewDataItem item)
		{
			if (item.DisplayIndex < 0)
			{
				return;
			}
			foreach (string key in this.DataKeyNames)
			{
				keys[key] = this.DataKeyValues[item.DisplayIndex][key];
			}
		}

		// Token: 0x0600721A RID: 29210 RVA: 0x001AC954 File Offset: 0x001AAB54
		protected override bool LoadClientState(Dictionary<string, object> clientState)
		{
			this.LoadPagingState(clientState);
			if (clientState.ContainsKey("selectedIndexes"))
			{
				this.LoadSelectedState(clientState["selectedIndexes"] as object[]);
			}
			if (clientState.ContainsKey("sortExpressions"))
			{
				this.LoadSortingState(clientState["sortExpressions"] as string);
			}
			if (clientState.ContainsKey("filterExpressions"))
			{
				this.LoadFilteringState(clientState["filterExpressions"] as string);
			}
			return base.LoadClientState(clientState);
		}

		// Token: 0x0600721B RID: 29211 RVA: 0x001AC9D8 File Offset: 0x001AABD8
		private void LoadPagingState(Dictionary<string, object> clientState)
		{
			if (this.AllowPaging)
			{
				if (clientState.ContainsKey("currentPageIndex"))
				{
					this.CurrentPageIndex = (int)clientState["currentPageIndex"];
				}
				if (clientState.ContainsKey("pageSize"))
				{
					this.PageSize = (int)clientState["pageSize"];
				}
				if (this.VirtualItemCount > 0 && clientState.ContainsKey("virtualItemCount"))
				{
					this.VirtualItemCount = (int)clientState["virtualItemCount"];
				}
			}
		}

		// Token: 0x0600721C RID: 29212 RVA: 0x001ACA60 File Offset: 0x001AAC60
		private void LoadSelectedState(object[] selectedState)
		{
			if (selectedState != null)
			{
				bool flag = false;
				if (selectedState.Length != this.SelectedIndexes.Count)
				{
					flag = true;
				}
				else
				{
					for (int i = 0; i < selectedState.Length; i++)
					{
						if (!this.SelectedIndexes.Contains((int)selectedState[i]))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					this.SelectedIndexes.Clear();
					for (int j = 0; j < selectedState.Length; j++)
					{
						this.SelectedIndexes.Add((int)selectedState[j]);
					}
				}
			}
		}

		// Token: 0x0600721D RID: 29213 RVA: 0x001ACADC File Offset: 0x001AACDC
		private void LoadSortingState(string sortString)
		{
			if (!string.IsNullOrEmpty(sortString))
			{
				bool flag = false;
				string[] array = sortString.Split(new string[]
				{
					",",
					", "
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length != this.SortExpressions.Count)
				{
					flag = true;
				}
				else
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (!this.SortExpressions.ContainsExpression(array[i]))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					this.SortExpressions.Clear();
					foreach (string expression in array)
					{
						this.SortExpressions.AddSortExpression(expression);
					}
				}
			}
		}

		// Token: 0x0600721E RID: 29214 RVA: 0x001ACB86 File Offset: 0x001AAD86
		private void LoadFilteringState(string filterState)
		{
			if (!string.IsNullOrEmpty(filterState))
			{
				this._clientFilterExpression = filterState;
			}
		}

		// Token: 0x0600721F RID: 29215 RVA: 0x001ACB97 File Offset: 0x001AAD97
		public virtual void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.Contains("FireCommand:"))
			{
				this.HandleClientFireCommand(RadListView.parseFireCommandEventName(eventArgument), RadListView.parseFireCommandArgs(eventArgument));
			}
		}

		// Token: 0x06007220 RID: 29216 RVA: 0x001ACCB4 File Offset: 0x001AAEB4
		protected virtual void HandleClientFireCommand(string eventName, string eventArgs)
		{
			Action<Action<int>> action2 = delegate(Action<int> action)
			{
				int obj;
				if (int.TryParse(eventArgs, out obj))
				{
					action(obj);
				}
			};
			switch (eventName)
			{
			case "Edit":
				action2(delegate(int itemIndex)
				{
					this.Items[itemIndex].FireCommandEvent("Edit", string.Empty);
				});
				return;
			case "Cancel":
				action2(delegate(int itemIndex)
				{
					this.Items[itemIndex].FireCommandEvent("Cancel", string.Empty);
				});
				return;
			case "Update":
				action2(delegate(int itemIndex)
				{
					this.Items[itemIndex].FireCommandEvent("Update", string.Empty);
				});
				return;
			case "Delete":
				action2(delegate(int itemIndex)
				{
					this.Items[itemIndex].FireCommandEvent("Delete", string.Empty);
				});
				return;
			case "Select":
				action2(delegate(int itemIndex)
				{
					this.Items[itemIndex].FireCommandEvent("Select", string.Empty);
				});
				return;
			case "Deselect":
				action2(delegate(int itemIndex)
				{
					this.Items[itemIndex].FireCommandEvent("Deselect", string.Empty);
				});
				return;
			case "InitInsert":
			{
				RadListViewInsertItemPosition itemPosition = RadListViewInsertItemPosition.LastItem;
				if (!string.IsNullOrEmpty(eventArgs) && Enum.Parse(typeof(RadListViewInsertItemPosition), eventArgs) != null)
				{
					itemPosition = (RadListViewInsertItemPosition)Enum.Parse(typeof(RadListViewInsertItemPosition), eventArgs);
				}
				this.ShowInsertItem(itemPosition);
				return;
			}
			case "CancelInsert":
			{
				RadListViewInsertItem insertItem = this.InsertItem;
				if (insertItem != null)
				{
					insertItem.FireCommandEvent("Cancel", string.Empty);
					return;
				}
				this.IsItemInserted = false;
				this.Rebind();
				return;
			}
			case "PerformInsert":
			{
				RadListViewInsertItem insertItem2 = this.InsertItem;
				if (insertItem2 != null)
				{
					insertItem2.FireCommandEvent("PerformInsert", string.Empty);
					return;
				}
				return;
			}
			case "ChangePageSize":
				action2(delegate(int pageSize)
				{
					this.CurrentPageIndex = 0;
					this.PageSize = pageSize;
					this.Rebind();
				});
				return;
			case "Page":
				RadListViewPageChangedEventArgs.HandlePaging(this, this, eventArgs);
				return;
			case "RebindListView":
				this.Rebind();
				return;
			case "ItemDrop":
			{
				string[] array = eventArgs.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				int index;
				if (array.Length > 0 && int.TryParse(array[0], out index))
				{
					RadListViewDataItem draggedItem = this.Items[index];
					string destinationHtmlElement = (array.Length > 1) ? array[1].Trim() : string.Empty;
					RadListViewItemDragDropEventArgs e = new RadListViewItemDragDropEventArgs(draggedItem, destinationHtmlElement);
					this.OnItemDrop(e);
					return;
				}
				return;
			}
			}
			this.OnBubbleEvent(this, new CommandEventArgs(eventName, eventArgs));
		}

		// Token: 0x14000104 RID: 260
		// (add) Token: 0x06007221 RID: 29217 RVA: 0x001ACFFD File Offset: 0x001AB1FD
		// (remove) Token: 0x06007222 RID: 29218 RVA: 0x001AD010 File Offset: 0x001AB210
		event EventHandler<RadFilterFildDesciptorsEventArgs> IRadFilterableContainer.FieldDescriptorsReady
		{
			add
			{
				base.Events.AddHandler(RadListView.EventFieldDescriptorsReady, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventFieldDescriptorsReady, value);
			}
		}

		// Token: 0x06007223 RID: 29219 RVA: 0x001AD024 File Offset: 0x001AB224
		void IRadFilterableContainer.ApplyFilterExpressions(RadFilterGroupExpression expressionRoot, bool shouldBind)
		{
			if (shouldBind)
			{
				this.FilterExpressions.Clear();
				if (!expressionRoot.IsEmpty)
				{
					RadFilterListViewQueryProvider radFilterListViewQueryProvider = new RadFilterListViewQueryProvider(new List<RadFilterGroupOperation>
					{
						RadFilterGroupOperation.And,
						RadFilterGroupOperation.Or
					});
					radFilterListViewQueryProvider.ProcessGroup(expressionRoot);
					RadListViewFilterExpressionCollection listViewExpressions = radFilterListViewQueryProvider.ListViewExpressions;
					if (listViewExpressions.Count > 0)
					{
						this.FilterExpressions.Add(listViewExpressions[0]);
					}
				}
				this.ClearEditItems();
				this.ClearSelectedIndexes();
				this.CurrentPageIndex = 0;
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x06007224 RID: 29220 RVA: 0x001AD0A5 File Offset: 0x001AB2A5
		void IPageableItemContainer.SetPageProperties(int startRowIndex, int maximumRows, bool databind)
		{
			this.SetPageProperties(startRowIndex, maximumRows, databind);
		}

		// Token: 0x17002533 RID: 9523
		// (get) Token: 0x06007225 RID: 29221 RVA: 0x001AD0B0 File Offset: 0x001AB2B0
		int IPageableItemContainer.StartRowIndex
		{
			get
			{
				return this.StartRowIndex;
			}
		}

		// Token: 0x17002534 RID: 9524
		// (get) Token: 0x06007226 RID: 29222 RVA: 0x001AD0B8 File Offset: 0x001AB2B8
		int IPageableItemContainer.MaximumRows
		{
			get
			{
				return this.PageSize;
			}
		}

		// Token: 0x14000105 RID: 261
		// (add) Token: 0x06007227 RID: 29223 RVA: 0x001AD0C0 File Offset: 0x001AB2C0
		// (remove) Token: 0x06007228 RID: 29224 RVA: 0x001AD0D3 File Offset: 0x001AB2D3
		event EventHandler<PageEventArgs> IPageableItemContainer.TotalRowCountAvailable
		{
			add
			{
				base.Events.AddHandler(RadListView.EventTotalRowCountAvailableAsp, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventTotalRowCountAvailableAsp, value);
			}
		}

		// Token: 0x17002535 RID: 9525
		// (get) Token: 0x06007229 RID: 29225 RVA: 0x001AD0E6 File Offset: 0x001AB2E6
		// (set) Token: 0x0600722A RID: 29226 RVA: 0x001AD0EE File Offset: 0x001AB2EE
		protected bool IsNeedDataSourceInProgress { get; set; }

		// Token: 0x17002536 RID: 9526
		// (get) Token: 0x0600722B RID: 29227 RVA: 0x001AD0F7 File Offset: 0x001AB2F7
		// (set) Token: 0x0600722C RID: 29228 RVA: 0x001AD117 File Offset: 0x001AB317
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Data")]
		[Description("Gets or sets the ODataDataSource used for data binding.")]
		public virtual string ODataDataSourceID
		{
			get
			{
				return ((string)this.ViewState["ODataDataSourceID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ODataDataSourceID"] = value;
			}
		}

		// Token: 0x17002537 RID: 9527
		// (get) Token: 0x0600722D RID: 29229 RVA: 0x001AD12A File Offset: 0x001AB32A
		// (set) Token: 0x0600722E RID: 29230 RVA: 0x001AD14A File Offset: 0x001AB34A
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataModelID
		{
			get
			{
				return (string)(this.ViewState["DataModelID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataModelID"] = value;
			}
		}

		// Token: 0x17002538 RID: 9528
		// (get) Token: 0x0600722F RID: 29231 RVA: 0x001AD15D File Offset: 0x001AB35D
		// (set) Token: 0x06007230 RID: 29232 RVA: 0x001AD165 File Offset: 0x001AB365
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[Description("Gets or sets the custom content for the root container in a RadListView control.")]
		[TemplateContainer(typeof(RadListView))]
		[DefaultValue(null)]
		public virtual ITemplate LayoutTemplate { get; set; }

		// Token: 0x17002539 RID: 9529
		// (get) Token: 0x06007231 RID: 29233 RVA: 0x001AD16E File Offset: 0x001AB36E
		// (set) Token: 0x06007232 RID: 29234 RVA: 0x001AD176 File Offset: 0x001AB376
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[Description("Gets or sets the custom content for the data item in a RadListView control")]
		[TemplateContainer(typeof(RadListViewDataItem), BindingDirection.TwoWay)]
		[DefaultValue(null)]
		public virtual ITemplate ItemTemplate { get; set; }

		// Token: 0x1700253A RID: 9530
		// (get) Token: 0x06007233 RID: 29235 RVA: 0x001AD17F File Offset: 0x001AB37F
		// (set) Token: 0x06007234 RID: 29236 RVA: 0x001AD187 File Offset: 0x001AB387
		[TemplateContainer(typeof(RadListViewDataItem), BindingDirection.TwoWay)]
		[Browsable(false)]
		[Description("Gets or sets the custom content for the alternating data item in a RadListView control.")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate AlternatingItemTemplate { get; set; }

		// Token: 0x1700253B RID: 9531
		// (get) Token: 0x06007235 RID: 29237 RVA: 0x001AD190 File Offset: 0x001AB390
		// (set) Token: 0x06007236 RID: 29238 RVA: 0x001AD198 File Offset: 0x001AB398
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RadListViewDataItem), BindingDirection.TwoWay)]
		[Browsable(false)]
		[Description("Gets or sets the custom content for the item in edit mode.")]
		[DefaultValue(null)]
		public virtual ITemplate EditItemTemplate { get; set; }

		// Token: 0x1700253C RID: 9532
		// (get) Token: 0x06007237 RID: 29239 RVA: 0x001AD1A1 File Offset: 0x001AB3A1
		// (set) Token: 0x06007238 RID: 29240 RVA: 0x001AD1A9 File Offset: 0x001AB3A9
		[Browsable(false)]
		[TemplateContainer(typeof(RadListViewDataItem), BindingDirection.TwoWay)]
		[Description("Gets or sets the custom content for an insert item in the RadListView control.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public virtual ITemplate InsertItemTemplate { get; set; }

		// Token: 0x1700253D RID: 9533
		// (get) Token: 0x06007239 RID: 29241 RVA: 0x001AD1B2 File Offset: 0x001AB3B2
		// (set) Token: 0x0600723A RID: 29242 RVA: 0x001AD1BA File Offset: 0x001AB3BA
		[DefaultValue(null)]
		[Description("Gets or sets the custom content for group container in the RadListView control.")]
		[TemplateContainer(typeof(RadListViewItem), BindingDirection.TwoWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate GroupTemplate { get; set; }

		// Token: 0x1700253E RID: 9534
		// (get) Token: 0x0600723B RID: 29243 RVA: 0x001AD1C3 File Offset: 0x001AB3C3
		// (set) Token: 0x0600723C RID: 29244 RVA: 0x001AD1CB File Offset: 0x001AB3CB
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets or sets the user-defined content for the separator between groups in a RadListView control.")]
		[Browsable(false)]
		[TemplateContainer(typeof(RadListViewItem))]
		public virtual ITemplate GroupSeparatorTemplate { get; set; }

		// Token: 0x1700253F RID: 9535
		// (get) Token: 0x0600723D RID: 29245 RVA: 0x001AD1D4 File Offset: 0x001AB3D4
		// (set) Token: 0x0600723E RID: 29246 RVA: 0x001AD1DC File Offset: 0x001AB3DC
		[TemplateContainer(typeof(RadListViewItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets or sets the user-defined content for the empty item that is rendered in a RadListView control when there are no more data items to display in the last row of the current data page.")]
		[DefaultValue(null)]
		[Browsable(false)]
		public virtual ITemplate EmptyItemTemplate { get; set; }

		// Token: 0x17002540 RID: 9536
		// (get) Token: 0x0600723F RID: 29247 RVA: 0x001AD1E8 File Offset: 0x001AB3E8
		// (set) Token: 0x06007240 RID: 29248 RVA: 0x001AD222 File Offset: 0x001AB422
		[Description("Gets or sets the ID for the item placeholder in a RadListView control. ")]
		[DefaultValue("itemPlaceholder")]
		[Category("Behavior")]
		public virtual string ItemPlaceholderID
		{
			get
			{
				object obj = this.ViewState["ItemPlaceholderID"];
				if (obj == null || string.IsNullOrEmpty((string)obj))
				{
					obj = "itemPlaceholder";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ItemPlaceholderID"] = value;
			}
		}

		// Token: 0x17002541 RID: 9537
		// (get) Token: 0x06007241 RID: 29249 RVA: 0x001AD238 File Offset: 0x001AB438
		// (set) Token: 0x06007242 RID: 29250 RVA: 0x001AD272 File Offset: 0x001AB472
		[Description("Gets or sets the ID for the group placeholder in a RadListView control. ")]
		[Category("Behavior")]
		[DefaultValue("groupPlaceholder")]
		public virtual string GroupPlaceholderID
		{
			get
			{
				object obj = this.ViewState["GroupPlaceholderID"];
				if (obj == null || string.IsNullOrEmpty((string)obj))
				{
					obj = "groupPlaceholder";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["GroupPlaceholderID"] = value;
			}
		}

		// Token: 0x17002542 RID: 9538
		// (get) Token: 0x06007243 RID: 29251 RVA: 0x001AD285 File Offset: 0x001AB485
		[Description("Gets a collection of RadListViewDataItem objects that represent the data items of the current page of data in a ListView control.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual RadListViewDataItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new RadListViewDataItemCollection();
				}
				return this._items;
			}
		}

		// Token: 0x17002543 RID: 9539
		// (get) Token: 0x06007244 RID: 29252 RVA: 0x001AD2A0 File Offset: 0x001AB4A0
		// (set) Token: 0x06007245 RID: 29253 RVA: 0x001AD2A8 File Offset: 0x001AB4A8
		[Description("Gets or sets the custom content for the separator between the items in a RadListView control")]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RadListViewItem))]
		[DefaultValue(null)]
		public virtual ITemplate ItemSeparatorTemplate { get; set; }

		// Token: 0x17002544 RID: 9540
		// (get) Token: 0x06007246 RID: 29254 RVA: 0x001AD2B1 File Offset: 0x001AB4B1
		// (set) Token: 0x06007247 RID: 29255 RVA: 0x001AD2B9 File Offset: 0x001AB4B9
		[Description("Template that will be displayed if there are no records in the DataSource assigned")]
		[TemplateContainer(typeof(RadListView))]
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate EmptyDataTemplate { get; set; }

		// Token: 0x17002545 RID: 9541
		// (get) Token: 0x06007248 RID: 29256 RVA: 0x001AD2C4 File Offset: 0x001AB4C4
		// (set) Token: 0x06007249 RID: 29257 RVA: 0x001AD2F2 File Offset: 0x001AB4F2
		[Category("Default")]
		[Description("Gets or sets the number of items to display per group in a RadListView control. ")]
		[DefaultValue(1)]
		public virtual int GroupItemCount
		{
			get
			{
				object obj = this.ViewState["GroupItemCount"] ?? 1;
				return (int)obj;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value", "The GroupItemCount property is set to a value less than 1.");
				}
				this.ViewState["GroupItemCount"] = value;
				this.SetRequiresDataBindingIfInitialized();
			}
		}

		// Token: 0x17002546 RID: 9542
		// (get) Token: 0x0600724A RID: 29258 RVA: 0x001AD324 File Offset: 0x001AB524
		// (set) Token: 0x0600724B RID: 29259 RVA: 0x001AD35C File Offset: 0x001AB55C
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(RadListViewStringArrayConverter))]
		[DefaultValue(null)]
		[Category("Data")]
		[Description("Comma delimited list of data-field Names")]
		public virtual string[] DataKeyNames
		{
			get
			{
				object obj = this.ViewState["DataKeyNames"] ?? new string[0];
				return (string[])((string[])obj).Clone();
			}
			set
			{
				if (!ListViewArrayComparerHelper.CompareStringArrays(value, this.DataKeyNamesInternal))
				{
					this.ViewState["DataKeyNames"] = ((value != null) ? value.Clone() : null);
					this.DataKeysArrayList.Clear();
					this.SetRequiresDataBindingIfInitialized();
				}
			}
		}

		// Token: 0x17002547 RID: 9543
		// (get) Token: 0x0600724C RID: 29260 RVA: 0x001AD39C File Offset: 0x001AB59C
		// (set) Token: 0x0600724D RID: 29261 RVA: 0x001AD3D4 File Offset: 0x001AB5D4
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(RadListViewStringArrayConverter))]
		[Description("Comma delimited list of data-field Names")]
		[NotifyParentProperty(true)]
		[Category("Data")]
		public virtual string[] ClientDataKeyNames
		{
			get
			{
				object obj = this.ViewState["ClientDataKeyNames"] ?? new string[0];
				return (string[])((string[])obj).Clone();
			}
			set
			{
				if (!ListViewArrayComparerHelper.CompareStringArrays(value, this.ClientDataKeyNamesInternal))
				{
					this.ViewState["ClientDataKeyNames"] = ((value != null) ? value.Clone() : null);
					this.DataKeysArrayList.Clear();
					this.SetRequiresDataBindingIfInitialized();
				}
			}
		}

		// Token: 0x17002548 RID: 9544
		// (get) Token: 0x0600724E RID: 29262 RVA: 0x001AD414 File Offset: 0x001AB614
		// (set) Token: 0x0600724F RID: 29263 RVA: 0x001AD442 File Offset: 0x001AB642
		[Category("Default")]
		[DefaultValue(ListViewGroupAggregatesScope.AllItems)]
		public virtual ListViewGroupAggregatesScope GroupAggregatesScope
		{
			get
			{
				object obj = this.ViewState["GroupAggregatesScope"] ?? ListViewGroupAggregatesScope.AllItems;
				return (ListViewGroupAggregatesScope)obj;
			}
			set
			{
				this.ViewState["GroupAggregatesScope"] = value;
			}
		}

		// Token: 0x17002549 RID: 9545
		// (get) Token: 0x06007250 RID: 29264 RVA: 0x001AD45A File Offset: 0x001AB65A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual RadListViewDataKeyArray DataKeyValues
		{
			get
			{
				if (this._dataKeyValues == null)
				{
					this._dataKeyValues = new RadListViewDataKeyArray(this.DataKeysArrayList);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._dataKeyValues).TrackViewState();
					}
				}
				return this._dataKeyValues;
			}
		}

		// Token: 0x1700254A RID: 9546
		// (get) Token: 0x06007251 RID: 29265 RVA: 0x001AD48E File Offset: 0x001AB68E
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadListViewClientSettings ClientSettings
		{
			get
			{
				if (this._clientSettings == null)
				{
					this._clientSettings = new RadListViewClientSettings();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._clientSettings).TrackViewState();
					}
				}
				return this._clientSettings;
			}
		}

		// Token: 0x1700254B RID: 9547
		// (get) Token: 0x06007252 RID: 29266 RVA: 0x001AD4BC File Offset: 0x001AB6BC
		// (set) Token: 0x06007253 RID: 29267 RVA: 0x001AD4EA File Offset: 0x001AB6EA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[SimplePersistenceSetting]
		public virtual RadListViewSortExpressionCollection SortExpressions
		{
			get
			{
				if (this._sortExpressions == null)
				{
					this._sortExpressions = new RadListViewSortExpressionCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._sortExpressions).TrackViewState();
					}
				}
				return this._sortExpressions;
			}
			internal set
			{
				this._sortExpressions = value;
			}
		}

		// Token: 0x1700254C RID: 9548
		// (get) Token: 0x06007254 RID: 29268 RVA: 0x001AD4F3 File Offset: 0x001AB6F3
		// (set) Token: 0x06007255 RID: 29269 RVA: 0x001AD515 File Offset: 0x001AB715
		[SimplePersistenceSetting]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ListViewDataGroupCollection DataGroups
		{
			get
			{
				if (this._dataGroups == null)
				{
					this._dataGroups = new ListViewDataGroupCollection();
					bool isTrackingViewState = base.IsTrackingViewState;
				}
				return this._dataGroups;
			}
			internal set
			{
				this._dataGroups = value;
			}
		}

		// Token: 0x1700254D RID: 9549
		// (get) Token: 0x06007256 RID: 29270 RVA: 0x001AD51E File Offset: 0x001AB71E
		// (set) Token: 0x06007257 RID: 29271 RVA: 0x001AD54C File Offset: 0x001AB74C
		[Browsable(false)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SimplePersistenceSetting]
		public virtual RadListViewFilterExpressionCollection FilterExpressions
		{
			get
			{
				if (this._filterExpressions == null)
				{
					this._filterExpressions = new RadListViewFilterExpressionCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._filterExpressions).TrackViewState();
					}
				}
				return this._filterExpressions;
			}
			internal set
			{
				this._filterExpressions = value;
			}
		}

		// Token: 0x1700254E RID: 9550
		// (get) Token: 0x06007258 RID: 29272 RVA: 0x001AD555 File Offset: 0x001AB755
		// (set) Token: 0x06007259 RID: 29273 RVA: 0x001AD562 File Offset: 0x001AB762
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Gets or sets the value indicating if more than one datafield can be sorted.")]
		[DefaultValue(false)]
		public virtual bool AllowMultiFieldSorting
		{
			get
			{
				return this.SortExpressions.AllowMultiFieldSorting;
			}
			set
			{
				this.SortExpressions.AllowMultiFieldSorting = value;
			}
		}

		// Token: 0x1700254F RID: 9551
		// (get) Token: 0x0600725A RID: 29274 RVA: 0x001AD570 File Offset: 0x001AB770
		// (set) Token: 0x0600725B RID: 29275 RVA: 0x001AD57D File Offset: 0x001AB77D
		[Category("Sorting")]
		[NotifyParentProperty(true)]
		[Description("Allow the no-sort state when changing sort order.")]
		[DefaultValue(false)]
		public virtual bool AllowNaturalSort
		{
			get
			{
				return this.SortExpressions.AllowNaturalSort;
			}
			set
			{
				this.SortExpressions.AllowNaturalSort = value;
			}
		}

		// Token: 0x17002550 RID: 9552
		// (get) Token: 0x0600725C RID: 29276 RVA: 0x001AD58C File Offset: 0x001AB78C
		// (set) Token: 0x0600725D RID: 29277 RVA: 0x001AD5B5 File Offset: 0x001AB7B5
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Allow RadListView equal items not to be reordered when sorting.Enables sorting result consistancy between 3.5, 4.0, 4.5 Framework")]
		public bool AllowStableSort
		{
			get
			{
				object obj = this.ViewState["AllowStableSort"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowStableSort"] = value;
			}
		}

		// Token: 0x17002551 RID: 9553
		// (get) Token: 0x0600725E RID: 29278 RVA: 0x001AD5D0 File Offset: 0x001AB7D0
		// (set) Token: 0x0600725F RID: 29279 RVA: 0x001AD5F9 File Offset: 0x001AB7F9
		[Bindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SimplePersistenceSetting]
		[Description("Gets or sets a value indicating the index of the currently active page in case paging is enabled")]
		[Browsable(false)]
		public int CurrentPageIndex
		{
			get
			{
				object obj = this.ControlState["CurrentPageIndex"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ControlState["CurrentPageIndex"] = value;
			}
		}

		// Token: 0x17002552 RID: 9554
		// (get) Token: 0x06007260 RID: 29280 RVA: 0x001AD620 File Offset: 0x001AB820
		// (set) Token: 0x06007261 RID: 29281 RVA: 0x001AD64E File Offset: 0x001AB84E
		[SimplePersistenceSetting]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsItemInserted
		{
			get
			{
				object obj = this.ControlState["_!iii"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				this.ControlState["_!iii"] = value;
			}
		}

		// Token: 0x17002553 RID: 9555
		// (get) Token: 0x06007262 RID: 29282 RVA: 0x001AD668 File Offset: 0x001AB868
		// (set) Token: 0x06007263 RID: 29283 RVA: 0x001AD696 File Offset: 0x001AB896
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Sorting")]
		public virtual bool OverrideDataSourceControlSorting
		{
			get
			{
				object obj = this.ControlState["OverrideDataSourceControlSorting"] ?? false;
				return (bool)obj;
			}
			set
			{
				this.ControlState["OverrideDataSourceControlSorting"] = value;
			}
		}

		// Token: 0x17002554 RID: 9556
		// (get) Token: 0x06007264 RID: 29284 RVA: 0x001AD6B0 File Offset: 0x001AB8B0
		// (set) Token: 0x06007265 RID: 29285 RVA: 0x001AD6D9 File Offset: 0x001AB8D9
		[DefaultValue(false)]
		[Description("Gets or sets if the custom sorting feature is enabled.")]
		[Category("Sorting")]
		[NotifyParentProperty(true)]
		public virtual bool AllowCustomSorting
		{
			get
			{
				object obj = this.ViewState["AllowCustomSorting"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowCustomSorting"] = value;
			}
		}

		// Token: 0x17002555 RID: 9557
		// (get) Token: 0x06007266 RID: 29286 RVA: 0x001AD6F1 File Offset: 0x001AB8F1
		// (set) Token: 0x06007267 RID: 29287 RVA: 0x001AD6F9 File Offset: 0x001AB8F9
		public int DataSourceCount { get; private set; }

		// Token: 0x17002556 RID: 9558
		// (get) Token: 0x06007268 RID: 29288 RVA: 0x001AD704 File Offset: 0x001AB904
		// (set) Token: 0x06007269 RID: 29289 RVA: 0x001AD730 File Offset: 0x001AB930
		[NotifyParentProperty(true)]
		[Category("Paging")]
		[DefaultValue(10)]
		[SimplePersistenceSetting]
		[Description("Specify the maximum number of items that would appear in a page,when paging is enabled by AllowPaging property.")]
		public virtual int PageSize
		{
			get
			{
				object obj = this.ControlState["PageSize"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 10;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				object obj = this.PageSize;
				if ((int)obj != value && this.AllowPaging)
				{
					RadListViewPageSizeChangedEventArgs radListViewPageSizeChangedEventArgs = new RadListViewPageSizeChangedEventArgs(null, null, value);
					this.FirePageSizeChanged(radListViewPageSizeChangedEventArgs);
					if (radListViewPageSizeChangedEventArgs.Canceled)
					{
						return;
					}
				}
				this.ControlState["PageSize"] = value;
			}
		}

		// Token: 0x17002557 RID: 9559
		// (get) Token: 0x0600726A RID: 29290 RVA: 0x001AD7A0 File Offset: 0x001AB9A0
		[Category("Paging")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Gets the number of pages required to display the records of the data source in a RadListView control.")]
		public virtual int PageCount
		{
			get
			{
				if (this._resolvedDataSource != null)
				{
					return this._resolvedDataSource.PagingManager.PageCount;
				}
				object obj = this.ControlState["_!PCount"];
				if (obj == null)
				{
					return 1;
				}
				return (int)obj;
			}
		}

		// Token: 0x17002558 RID: 9560
		// (get) Token: 0x0600726B RID: 29291 RVA: 0x001AD7E4 File Offset: 0x001AB9E4
		// (set) Token: 0x0600726C RID: 29292 RVA: 0x001AD80D File Offset: 0x001ABA0D
		[Browsable(true)]
		[Category("Paging")]
		[Bindable(true)]
		[Description("VisibleItemCount")]
		[NotifyParentProperty(true)]
		[DefaultValue(0)]
		public virtual int VirtualItemCount
		{
			get
			{
				object obj = this.ControlState["VirtualItemCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ControlState["VirtualItemCount"] = value;
			}
		}

		// Token: 0x17002559 RID: 9561
		// (get) Token: 0x0600726D RID: 29293 RVA: 0x001AD834 File Offset: 0x001ABA34
		// (set) Token: 0x0600726E RID: 29294 RVA: 0x001AD85D File Offset: 0x001ABA5D
		[DefaultValue(false)]
		[Description("Gets or sets if the custom paging feature is enabled.")]
		[Category("Paging")]
		[NotifyParentProperty(true)]
		public virtual bool AllowCustomPaging
		{
			get
			{
				object obj = this.ControlState["AllowCustomPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ControlState["AllowCustomPaging"] = value;
			}
		}

		// Token: 0x14000106 RID: 262
		// (add) Token: 0x0600726F RID: 29295 RVA: 0x001AD875 File Offset: 0x001ABA75
		// (remove) Token: 0x06007270 RID: 29296 RVA: 0x001AD888 File Offset: 0x001ABA88
		[Description("Raised when LayoutTemplate is created")]
		[Category("Action")]
		public event EventHandler<EventArgs> LayoutCreated
		{
			add
			{
				base.Events.AddHandler(RadListView.EventLayoutCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventLayoutCreated, value);
			}
		}

		// Token: 0x06007271 RID: 29297 RVA: 0x001AD89C File Offset: 0x001ABA9C
		protected virtual void OnLayoutCreated(EventArgs e)
		{
			EventHandler<EventArgs> eventHandler = base.Events[RadListView.EventLayoutCreated] as EventHandler<EventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000107 RID: 263
		// (add) Token: 0x06007272 RID: 29298 RVA: 0x001AD8CA File Offset: 0x001ABACA
		// (remove) Token: 0x06007273 RID: 29299 RVA: 0x001AD8DD File Offset: 0x001ABADD
		[Description("Raised when RadListViewItem is created")]
		[Category("Action")]
		public event EventHandler<RadListViewItemEventArgs> ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadListView.EventItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventItemCreated, value);
			}
		}

		// Token: 0x06007274 RID: 29300 RVA: 0x001AD8F0 File Offset: 0x001ABAF0
		protected virtual void OnItemCreated(RadListViewItemEventArgs e)
		{
			EventHandler<RadListViewItemEventArgs> eventHandler = base.Events[RadListView.EventItemCreated] as EventHandler<RadListViewItemEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000108 RID: 264
		// (add) Token: 0x06007275 RID: 29301 RVA: 0x001AD91E File Offset: 0x001ABB1E
		// (remove) Token: 0x06007276 RID: 29302 RVA: 0x001AD931 File Offset: 0x001ABB31
		[Category("Action")]
		[Description("Raised when RadListViewItem is data bound")]
		public event EventHandler<RadListViewItemEventArgs> ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadListView.EventItemDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventItemDataBound, value);
			}
		}

		// Token: 0x06007277 RID: 29303 RVA: 0x001AD944 File Offset: 0x001ABB44
		protected virtual void OnItemDataBound(RadListViewItemEventArgs e)
		{
			EventHandler<RadListViewItemEventArgs> eventHandler = base.Events[RadListView.EventItemDataBound] as EventHandler<RadListViewItemEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000109 RID: 265
		// (add) Token: 0x06007278 RID: 29304 RVA: 0x001AD972 File Offset: 0x001ABB72
		// (remove) Token: 0x06007279 RID: 29305 RVA: 0x001AD985 File Offset: 0x001ABB85
		[Category("Action")]
		[Description("Raised when a button in a RadListView control is clicked.")]
		public event EventHandler<RadListViewCommandEventArgs> ItemCommand
		{
			add
			{
				base.Events.AddHandler(RadListView.EventItemCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventItemCommand, value);
			}
		}

		// Token: 0x0600727A RID: 29306 RVA: 0x001AD998 File Offset: 0x001ABB98
		protected virtual void OnItemCommand(RadListViewCommandEventArgs e)
		{
			EventHandler<RadListViewCommandEventArgs> eventHandler = base.Events[RadListView.EventItemCommand] as EventHandler<RadListViewCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400010A RID: 266
		// (add) Token: 0x0600727B RID: 29307 RVA: 0x001AD9C6 File Offset: 0x001ABBC6
		// (remove) Token: 0x0600727C RID: 29308 RVA: 0x001AD9D9 File Offset: 0x001ABBD9
		[Description("Fires when \"Page\" command bubbles")]
		[Category("Action")]
		public event EventHandler<RadListViewPageChangedEventArgs> PageIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadListView.EventPageIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventPageIndexChanged, value);
			}
		}

		// Token: 0x0600727D RID: 29309 RVA: 0x001AD9EC File Offset: 0x001ABBEC
		protected virtual void OnPageIndexChanged(RadListViewPageChangedEventArgs e)
		{
			EventHandler<RadListViewPageChangedEventArgs> eventHandler = base.Events[RadListView.EventPageIndexChanged] as EventHandler<RadListViewPageChangedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600727E RID: 29310 RVA: 0x001ADA1A File Offset: 0x001ABC1A
		internal void FirePageIndexChanged(RadListViewPageChangedEventArgs e)
		{
			this.OnPageIndexChanged(e);
		}

		// Token: 0x1400010B RID: 267
		// (add) Token: 0x0600727F RID: 29311 RVA: 0x001ADA23 File Offset: 0x001ABC23
		// (remove) Token: 0x06007280 RID: 29312 RVA: 0x001ADA36 File Offset: 0x001ABC36
		[Description("Fires when PageSize has been changed.")]
		[Category("Action")]
		public event EventHandler<RadListViewPageSizeChangedEventArgs> PageSizeChanged
		{
			add
			{
				base.Events.AddHandler(RadListView.EventPageSizeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventPageSizeChanged, value);
			}
		}

		// Token: 0x06007281 RID: 29313 RVA: 0x001ADA4C File Offset: 0x001ABC4C
		protected virtual void OnPageSizeChanged(RadListViewPageSizeChangedEventArgs e)
		{
			EventHandler<RadListViewPageSizeChangedEventArgs> eventHandler = base.Events[RadListView.EventPageSizeChanged] as EventHandler<RadListViewPageSizeChangedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06007282 RID: 29314 RVA: 0x001ADA7A File Offset: 0x001ABC7A
		internal void FirePageSizeChanged(RadListViewPageSizeChangedEventArgs e)
		{
			this.OnPageSizeChanged(e);
		}

		// Token: 0x1400010C RID: 268
		// (add) Token: 0x06007283 RID: 29315 RVA: 0x001ADA83 File Offset: 0x001ABC83
		// (remove) Token: 0x06007284 RID: 29316 RVA: 0x001ADA96 File Offset: 0x001ABC96
		[Description("Raises the SelectedIndexChanged event.")]
		[Category("Action")]
		public event EventHandler<EventArgs> SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadListView.EventSelectedIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventSelectedIndexChanged, value);
			}
		}

		// Token: 0x06007285 RID: 29317 RVA: 0x001ADAAC File Offset: 0x001ABCAC
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler<EventArgs> eventHandler = base.Events[RadListView.EventSelectedIndexChanged] as EventHandler<EventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06007286 RID: 29318 RVA: 0x001ADADA File Offset: 0x001ABCDA
		internal void FireOnSelectedIndexChanged(EventArgs e)
		{
			this.OnSelectedIndexChanged(e);
		}

		// Token: 0x1400010D RID: 269
		// (add) Token: 0x06007287 RID: 29319 RVA: 0x001ADAE3 File Offset: 0x001ABCE3
		// (remove) Token: 0x06007288 RID: 29320 RVA: 0x001ADAF6 File Offset: 0x001ABCF6
		[Description("Fires when Sort has been changed.")]
		[Category("Action")]
		public event EventHandler<RadListViewSortEventArgs> Sorting
		{
			add
			{
				base.Events.AddHandler(RadListView.EventSorting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventSorting, value);
			}
		}

		// Token: 0x06007289 RID: 29321 RVA: 0x001ADB0C File Offset: 0x001ABD0C
		protected virtual void OnSorting(RadListViewSortEventArgs e)
		{
			EventHandler<RadListViewSortEventArgs> eventHandler = base.Events[RadListView.EventSorting] as EventHandler<RadListViewSortEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600728A RID: 29322 RVA: 0x001ADB3A File Offset: 0x001ABD3A
		internal void FireSorting(RadListViewSortEventArgs e)
		{
			this.OnSorting(e);
		}

		// Token: 0x1400010E RID: 270
		// (add) Token: 0x0600728B RID: 29323 RVA: 0x001ADB43 File Offset: 0x001ABD43
		// (remove) Token: 0x0600728C RID: 29324 RVA: 0x001ADB56 File Offset: 0x001ABD56
		[Description("Occurs when an insert operation is requested, but before the RadListView control performs the insert.")]
		[Category("Action")]
		public event EventHandler<RadListViewCommandEventArgs> ItemInserting
		{
			add
			{
				base.Events.AddHandler(RadListView.EventItemInserting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventItemInserting, value);
			}
		}

		// Token: 0x0600728D RID: 29325 RVA: 0x001ADB6C File Offset: 0x001ABD6C
		protected virtual void OnItemInserting(RadListViewCommandEventArgs e)
		{
			EventHandler<RadListViewCommandEventArgs> eventHandler = base.Events[RadListView.EventItemInserting] as EventHandler<RadListViewCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600728E RID: 29326 RVA: 0x001ADB9A File Offset: 0x001ABD9A
		internal void FireItemInserting(RadListViewCommandEventArgs e)
		{
			this.OnItemInserting(e);
		}

		// Token: 0x1400010F RID: 271
		// (add) Token: 0x0600728F RID: 29327 RVA: 0x001ADBA3 File Offset: 0x001ABDA3
		// (remove) Token: 0x06007290 RID: 29328 RVA: 0x001ADBB6 File Offset: 0x001ABDB6
		[Description("Occurs when an insert operation is requested, after the RadListView control has inserted the item in the data source.")]
		[Category("Action")]
		public event EventHandler<RadListViewInsertedEventArgs> ItemInserted
		{
			add
			{
				base.Events.AddHandler(RadListView.EventItemInserted, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventItemInserted, value);
			}
		}

		// Token: 0x06007291 RID: 29329 RVA: 0x001ADBCC File Offset: 0x001ABDCC
		protected virtual void OnItemInserted(RadListViewInsertedEventArgs e)
		{
			EventHandler<RadListViewInsertedEventArgs> eventHandler = base.Events[RadListView.EventItemInserted] as EventHandler<RadListViewInsertedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000110 RID: 272
		// (add) Token: 0x06007292 RID: 29330 RVA: 0x001ADBFA File Offset: 0x001ABDFA
		// (remove) Token: 0x06007293 RID: 29331 RVA: 0x001ADC0D File Offset: 0x001ABE0D
		[Description("Occurs when an edit operation is requested, but before the RadListView item is put in edit mode")]
		[Category("Action")]
		public event EventHandler<RadListViewCommandEventArgs> ItemEditing
		{
			add
			{
				base.Events.AddHandler(RadListView.EventItemEditing, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventItemEditing, value);
			}
		}

		// Token: 0x06007294 RID: 29332 RVA: 0x001ADC20 File Offset: 0x001ABE20
		protected virtual void OnItemEditing(RadListViewCommandEventArgs e)
		{
			EventHandler<RadListViewCommandEventArgs> eventHandler = base.Events[RadListView.EventItemEditing] as EventHandler<RadListViewCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06007295 RID: 29333 RVA: 0x001ADC4E File Offset: 0x001ABE4E
		internal void FireItemEditing(RadListViewCommandEventArgs e)
		{
			this.OnItemEditing(e);
		}

		// Token: 0x14000111 RID: 273
		// (add) Token: 0x06007296 RID: 29334 RVA: 0x001ADC57 File Offset: 0x001ABE57
		// (remove) Token: 0x06007297 RID: 29335 RVA: 0x001ADC6A File Offset: 0x001ABE6A
		[Description("Occurs when a delete operation is requested, but before the RadListView control deletes the item.")]
		[Category("Action")]
		public event EventHandler<RadListViewCommandEventArgs> ItemDeleting
		{
			add
			{
				base.Events.AddHandler(RadListView.EventItemDeleting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventItemDeleting, value);
			}
		}

		// Token: 0x06007298 RID: 29336 RVA: 0x001ADC80 File Offset: 0x001ABE80
		protected virtual void OnItemDeleting(RadListViewCommandEventArgs e)
		{
			EventHandler<RadListViewCommandEventArgs> eventHandler = base.Events[RadListView.EventItemDeleting] as EventHandler<RadListViewCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06007299 RID: 29337 RVA: 0x001ADCAE File Offset: 0x001ABEAE
		internal void FireItemDeleting(RadListViewCommandEventArgs e)
		{
			this.OnItemDeleting(e);
		}

		// Token: 0x14000112 RID: 274
		// (add) Token: 0x0600729A RID: 29338 RVA: 0x001ADCB7 File Offset: 0x001ABEB7
		// (remove) Token: 0x0600729B RID: 29339 RVA: 0x001ADCCA File Offset: 0x001ABECA
		[Description("Occurs when a delete operation is requested, after the RadListView control deletes the item.")]
		[Category("Action")]
		public event EventHandler<RadListViewDeletedEventArgs> ItemDeleted
		{
			add
			{
				base.Events.AddHandler(RadListView.EventItemDeleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventItemDeleted, value);
			}
		}

		// Token: 0x0600729C RID: 29340 RVA: 0x001ADCE0 File Offset: 0x001ABEE0
		protected virtual void OnItemDeleted(RadListViewDeletedEventArgs e)
		{
			EventHandler<RadListViewDeletedEventArgs> eventHandler = base.Events[RadListView.EventItemDeleted] as EventHandler<RadListViewDeletedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000113 RID: 275
		// (add) Token: 0x0600729D RID: 29341 RVA: 0x001ADD0E File Offset: 0x001ABF0E
		// (remove) Token: 0x0600729E RID: 29342 RVA: 0x001ADD21 File Offset: 0x001ABF21
		[Description("Occurs when an edit operation is requested, but before the RadListView item is put in edit mode")]
		[Category("Action")]
		public event EventHandler<RadListViewCommandEventArgs> ItemUpdating
		{
			add
			{
				base.Events.AddHandler(RadListView.EventItemUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventItemUpdating, value);
			}
		}

		// Token: 0x0600729F RID: 29343 RVA: 0x001ADD34 File Offset: 0x001ABF34
		protected virtual void OnItemUpdating(RadListViewCommandEventArgs e)
		{
			EventHandler<RadListViewCommandEventArgs> eventHandler = base.Events[RadListView.EventItemUpdating] as EventHandler<RadListViewCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060072A0 RID: 29344 RVA: 0x001ADD62 File Offset: 0x001ABF62
		internal void FireItemUpdating(RadListViewCommandEventArgs e)
		{
			this.OnItemUpdating(e);
		}

		// Token: 0x14000114 RID: 276
		// (add) Token: 0x060072A1 RID: 29345 RVA: 0x001ADD6B File Offset: 0x001ABF6B
		// (remove) Token: 0x060072A2 RID: 29346 RVA: 0x001ADD7E File Offset: 0x001ABF7E
		[Category("Action")]
		[Description("Occurs when an update operation is requested, after the RadListView control updates the item.")]
		public event EventHandler<RadListViewUpdatedEventArgs> ItemUpdated
		{
			add
			{
				base.Events.AddHandler(RadListView.EventItemUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventItemUpdated, value);
			}
		}

		// Token: 0x060072A3 RID: 29347 RVA: 0x001ADD94 File Offset: 0x001ABF94
		protected virtual void OnItemUpdated(RadListViewUpdatedEventArgs e)
		{
			EventHandler<RadListViewUpdatedEventArgs> eventHandler = base.Events[RadListView.EventItemUpdated] as EventHandler<RadListViewUpdatedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060072A4 RID: 29348 RVA: 0x001ADDC2 File Offset: 0x001ABFC2
		internal void FireItemUpdated(RadListViewUpdatedEventArgs e)
		{
			this.OnItemUpdated(e);
		}

		// Token: 0x14000115 RID: 277
		// (add) Token: 0x060072A5 RID: 29349 RVA: 0x001ADDCB File Offset: 0x001ABFCB
		// (remove) Token: 0x060072A6 RID: 29350 RVA: 0x001ADDDE File Offset: 0x001ABFDE
		[Category("Action")]
		[Description("Occurs when a cancel operation is requested, but before the RadListView control cancels the insert or edit operation. ")]
		public event EventHandler<RadListViewCommandEventArgs> ItemCanceling
		{
			add
			{
				base.Events.AddHandler(RadListView.EventItemCanceling, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventItemCanceling, value);
			}
		}

		// Token: 0x060072A7 RID: 29351 RVA: 0x001ADDF4 File Offset: 0x001ABFF4
		protected virtual void OnItemCanceling(RadListViewCommandEventArgs e)
		{
			EventHandler<RadListViewCommandEventArgs> eventHandler = base.Events[RadListView.EventItemCanceling] as EventHandler<RadListViewCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060072A8 RID: 29352 RVA: 0x001ADE22 File Offset: 0x001AC022
		internal void FireItemCanceling(RadListViewCommandEventArgs e)
		{
			this.OnItemCanceling(e);
		}

		// Token: 0x14000116 RID: 278
		// (add) Token: 0x060072A9 RID: 29353 RVA: 0x001ADE2B File Offset: 0x001AC02B
		// (remove) Token: 0x060072AA RID: 29354 RVA: 0x001ADE3E File Offset: 0x001AC03E
		[Category("Action")]
		[Description("Raised when the listView is about to be bound and the data source must be assigned")]
		public event EventHandler<RadListViewNeedDataSourceEventArgs> NeedDataSource
		{
			add
			{
				base.Events.AddHandler(RadListView.EventNeedDataSource, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventNeedDataSource, value);
			}
		}

		// Token: 0x060072AB RID: 29355 RVA: 0x001ADE54 File Offset: 0x001AC054
		protected virtual void OnNeedDataSource(RadListViewNeedDataSourceEventArgs e)
		{
			this.IsNeedDataSourceInProgress = true;
			try
			{
				EventHandler<RadListViewNeedDataSourceEventArgs> eventHandler = base.Events[RadListView.EventNeedDataSource] as EventHandler<RadListViewNeedDataSourceEventArgs>;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
			finally
			{
				this.IsNeedDataSourceInProgress = false;
			}
		}

		// Token: 0x1700255A RID: 9562
		// (get) Token: 0x060072AC RID: 29356 RVA: 0x001ADEA4 File Offset: 0x001AC0A4
		private bool HasFieldDescriptorsReadyAttachedHandlers
		{
			get
			{
				return base.Events[RadListView.EventFieldDescriptorsReady] is EventHandler<RadFilterFildDesciptorsEventArgs>;
			}
		}

		// Token: 0x060072AD RID: 29357 RVA: 0x001ADED0 File Offset: 0x001AC0D0
		protected virtual void OnFieldDescriptorsReady(RadFilterFildDesciptorsEventArgs e)
		{
			EventHandler<RadFilterFildDesciptorsEventArgs> eventHandler = base.Events[RadListView.EventFieldDescriptorsReady] as EventHandler<RadFilterFildDesciptorsEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000117 RID: 279
		// (add) Token: 0x060072AE RID: 29358 RVA: 0x001ADEFE File Offset: 0x001AC0FE
		// (remove) Token: 0x060072AF RID: 29359 RVA: 0x001ADF11 File Offset: 0x001AC111
		event EventHandler<RadDataPagerPageEventArgs> IRadPageableItemContainer.TotalRowCountAvailable
		{
			add
			{
				base.Events.AddHandler(RadListView.EventTotalRowCountAvailable, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventTotalRowCountAvailable, value);
			}
		}

		// Token: 0x060072B0 RID: 29360 RVA: 0x001ADF24 File Offset: 0x001AC124
		protected virtual void OnTotalRowCountAvailable(RadDataPagerPageEventArgs e)
		{
			EventHandler<RadDataPagerPageEventArgs> eventHandler = base.Events[RadListView.EventTotalRowCountAvailable] as EventHandler<RadDataPagerPageEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			PageEventArgs e2 = new PageEventArgs(e.StartRowIndex, e.MaximumRows, e.TotalRowCount);
			EventHandler<PageEventArgs> eventHandler2 = base.Events[RadListView.EventTotalRowCountAvailableAsp] as EventHandler<PageEventArgs>;
			if (eventHandler2 != null)
			{
				eventHandler2(this, e2);
			}
		}

		// Token: 0x14000118 RID: 280
		// (add) Token: 0x060072B1 RID: 29361 RVA: 0x001ADF8B File Offset: 0x001AC18B
		// (remove) Token: 0x060072B2 RID: 29362 RVA: 0x001ADF9E File Offset: 0x001AC19E
		[Description("Occurs when a ListView item is dragged and dropped on an HTML element")]
		[Category("Action")]
		public event EventHandler<RadListViewItemDragDropEventArgs> ItemDrop
		{
			add
			{
				base.Events.AddHandler(RadListView.EventItemDrop, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventItemDrop, value);
			}
		}

		// Token: 0x060072B3 RID: 29363 RVA: 0x001ADFB4 File Offset: 0x001AC1B4
		protected virtual void OnItemDrop(RadListViewItemDragDropEventArgs e)
		{
			EventHandler<RadListViewItemDragDropEventArgs> eventHandler = base.Events[RadListView.EventItemDrop] as EventHandler<RadListViewItemDragDropEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000119 RID: 281
		// (add) Token: 0x060072B4 RID: 29364 RVA: 0x001ADFE2 File Offset: 0x001AC1E2
		// (remove) Token: 0x060072B5 RID: 29365 RVA: 0x001ADFF5 File Offset: 0x001AC1F5
		[Category("Action")]
		[Description("Occurs when a ListView grouped by custom aggregate")]
		public event EventHandler<ListViewCustomAggregateEventArgs> CustomAggregate
		{
			add
			{
				base.Events.AddHandler(RadListView.EventCustomAggregate, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadListView.EventCustomAggregate, value);
			}
		}

		// Token: 0x060072B6 RID: 29366 RVA: 0x001AE008 File Offset: 0x001AC208
		protected virtual void OnCustomAggregate(ListViewCustomAggregateEventArgs e)
		{
			EventHandler<ListViewCustomAggregateEventArgs> eventHandler = base.Events[RadListView.EventCustomAggregate] as EventHandler<ListViewCustomAggregateEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060072B7 RID: 29367 RVA: 0x001AE036 File Offset: 0x001AC236
		internal void FireCustomAggregate(ListViewCustomAggregateEventArgs e)
		{
			this.OnCustomAggregate(e);
		}

		// Token: 0x1700255B RID: 9563
		// (get) Token: 0x060072B8 RID: 29368 RVA: 0x001AE03F File Offset: 0x001AC23F
		internal bool ShouldBeBound
		{
			get
			{
				return this.ControlState["_!DSIC"] == null;
			}
		}

		// Token: 0x1700255C RID: 9564
		// (get) Token: 0x060072B9 RID: 29369 RVA: 0x001AE054 File Offset: 0x001AC254
		internal bool AlwaysAutoBindOnPostBack
		{
			get
			{
				return !base.IsViewStateEnabled;
			}
		}

		// Token: 0x1700255D RID: 9565
		// (get) Token: 0x060072BA RID: 29370 RVA: 0x001AE05F File Offset: 0x001AC25F
		// (set) Token: 0x060072BB RID: 29371 RVA: 0x001AE067 File Offset: 0x001AC267
		[DefaultValue("")]
		[Description("Gets or sets the name of the method to call in order to update data")]
		[NotifyParentProperty(true)]
		[Category("Data")]
		public new string UpdateMethod
		{
			get
			{
				return base.UpdateMethod;
			}
			set
			{
				base.UpdateMethod = value;
			}
		}

		// Token: 0x1700255E RID: 9566
		// (get) Token: 0x060072BC RID: 29372 RVA: 0x001AE070 File Offset: 0x001AC270
		// (set) Token: 0x060072BD RID: 29373 RVA: 0x001AE078 File Offset: 0x001AC278
		[NotifyParentProperty(true)]
		[Description("Gets or sets the name of the method to call in order to insert data")]
		[DefaultValue("")]
		[Category("Data")]
		public new string InsertMethod
		{
			get
			{
				return base.InsertMethod;
			}
			set
			{
				base.InsertMethod = value;
			}
		}

		// Token: 0x1700255F RID: 9567
		// (get) Token: 0x060072BE RID: 29374 RVA: 0x001AE081 File Offset: 0x001AC281
		// (set) Token: 0x060072BF RID: 29375 RVA: 0x001AE089 File Offset: 0x001AC289
		[Description("Gets or sets the name of the method to call in order to delete data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Data")]
		public new string DeleteMethod
		{
			get
			{
				return base.DeleteMethod;
			}
			set
			{
				base.DeleteMethod = value;
			}
		}

		// Token: 0x17002560 RID: 9568
		// (get) Token: 0x060072C0 RID: 29376 RVA: 0x001AE094 File Offset: 0x001AC294
		// (set) Token: 0x060072C1 RID: 29377 RVA: 0x001AE0BD File Offset: 0x001AC2BD
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Paging")]
		[Description("Gets or sets a value indicating whether the automatic paging feature is enabled.")]
		public virtual bool AllowPaging
		{
			get
			{
				object obj = this.ViewState["AllowPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowPaging"] = value;
			}
		}

		// Token: 0x17002561 RID: 9569
		// (get) Token: 0x060072C2 RID: 29378 RVA: 0x001AE0D8 File Offset: 0x001AC2D8
		// (set) Token: 0x060072C3 RID: 29379 RVA: 0x001AE101 File Offset: 0x001AC301
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating whether Telerik RadListView should retrieve all data and ignore server paging in case of sorting.")]
		[DefaultValue(true)]
		public virtual bool CanRetrieveAllData
		{
			get
			{
				object obj = this.ControlState["CanRetrieveAllData"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ControlState["CanRetrieveAllData"] = value;
			}
		}

		// Token: 0x17002562 RID: 9570
		// (get) Token: 0x060072C4 RID: 29380 RVA: 0x001AE119 File Offset: 0x001AC319
		[Category("Validation")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Validation settings")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public RadListViewValidationSettings ValidationSettings
		{
			get
			{
				if (this._validationSettings == null)
				{
					this._validationSettings = new RadListViewValidationSettings(this.ViewState, this);
				}
				return this._validationSettings;
			}
		}

		// Token: 0x17002563 RID: 9571
		// (get) Token: 0x060072C5 RID: 29381 RVA: 0x001AE13B File Offset: 0x001AC33B
		// (set) Token: 0x060072C6 RID: 29382 RVA: 0x001AE143 File Offset: 0x001AC343
		[Browsable(false)]
		[TemplateContainer(typeof(RadListViewDataItem), BindingDirection.TwoWay)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate SelectedItemTemplate { get; set; }

		// Token: 0x17002564 RID: 9572
		// (get) Token: 0x060072C7 RID: 29383 RVA: 0x001AE14C File Offset: 0x001AC34C
		// (set) Token: 0x060072C8 RID: 29384 RVA: 0x001AE1AB File Offset: 0x001AC3AB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[SimplePersistenceSetting]
		public RadListViewIndexesCollection SelectedIndexes
		{
			get
			{
				if (this._selectedIndexes == null)
				{
					this._selectedIndexes = (RadListViewIndexesCollection)this.ControlState["SelectedIndexes"];
					if (this._selectedIndexes == null)
					{
						this._selectedIndexes = new RadListViewIndexesCollection();
						this.ControlState["SelectedIndexes"] = this._selectedIndexes;
					}
				}
				return this._selectedIndexes;
			}
			internal set
			{
				this._selectedIndexes = value;
			}
		}

		// Token: 0x17002565 RID: 9573
		// (get) Token: 0x060072C9 RID: 29385 RVA: 0x001AE1B4 File Offset: 0x001AC3B4
		// (set) Token: 0x060072CA RID: 29386 RVA: 0x001AE1DD File Offset: 0x001AC3DD
		[Category("Behavior")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool AllowMultiItemSelection
		{
			get
			{
				object obj = this.ControlState["AllowMultiItemSelection"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ControlState["AllowMultiItemSelection"] = value;
			}
		}

		// Token: 0x17002566 RID: 9574
		// (get) Token: 0x060072CB RID: 29387 RVA: 0x001AE1F8 File Offset: 0x001AC3F8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadListViewDataItemCollection SelectedItems
		{
			get
			{
				RadListViewDataItemCollection radListViewDataItemCollection = new RadListViewDataItemCollection();
				foreach (int num in this.SelectedIndexes)
				{
					if (num < this.Items.Count)
					{
						radListViewDataItemCollection.Add(this.Items[num]);
					}
				}
				return radListViewDataItemCollection;
			}
		}

		// Token: 0x17002567 RID: 9575
		// (get) Token: 0x060072CC RID: 29388 RVA: 0x001AE26C File Offset: 0x001AC46C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SelectedValue
		{
			get
			{
				object result = null;
				if (this.Items.Count == 0)
				{
					return result;
				}
				if (this.SelectedItems.Count > 0)
				{
					RadListViewDataItem radListViewDataItem = this.SelectedItems[this.SelectedItems.Count - 1];
					if (radListViewDataItem != null && this.DataKeyNames.Length > 0)
					{
						result = this.DataKeyValues[radListViewDataItem.DisplayIndex][this.DataKeyNames[0]];
					}
				}
				return result;
			}
		}

		// Token: 0x17002568 RID: 9576
		// (get) Token: 0x060072CD RID: 29389 RVA: 0x001AE2E0 File Offset: 0x001AC4E0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DataKey SelectedValues
		{
			get
			{
				DataKey result = null;
				if (this.Items.Count == 0)
				{
					return result;
				}
				if (this.SelectedItems.Count > 0)
				{
					RadListViewDataItem radListViewDataItem = this.SelectedItems[this.SelectedItems.Count - 1];
					if (radListViewDataItem != null && this.DataKeyNames.Length > 0)
					{
						result = this.DataKeyValues[radListViewDataItem.DisplayIndex];
					}
				}
				return result;
			}
		}

		// Token: 0x17002569 RID: 9577
		// (get) Token: 0x060072CE RID: 29390 RVA: 0x001AE348 File Offset: 0x001AC548
		// (set) Token: 0x060072CF RID: 29391 RVA: 0x001AE3A7 File Offset: 0x001AC5A7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[SimplePersistenceSetting]
		public RadListViewIndexesCollection EditIndexes
		{
			get
			{
				if (this._editIndexes == null)
				{
					this._editIndexes = (RadListViewIndexesCollection)this.ControlState["EditIndexes"];
					if (this._editIndexes == null)
					{
						this._editIndexes = new RadListViewIndexesCollection();
						this.ControlState["EditIndexes"] = this._editIndexes;
					}
				}
				return this._editIndexes;
			}
			internal set
			{
				this._editIndexes = new RadListViewIndexesCollection();
				this._editIndexes.AddRange(value);
				this.ControlState["EditIndexes"] = this._editIndexes;
			}
		}

		// Token: 0x1700256A RID: 9578
		// (get) Token: 0x060072D0 RID: 29392 RVA: 0x001AE3D8 File Offset: 0x001AC5D8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadListViewDataItemCollection EditItems
		{
			get
			{
				RadListViewDataItemCollection radListViewDataItemCollection = new RadListViewDataItemCollection();
				foreach (int index in this.EditIndexes)
				{
					radListViewDataItemCollection.Add(this.Items[index]);
				}
				return radListViewDataItemCollection;
			}
		}

		// Token: 0x1700256B RID: 9579
		// (get) Token: 0x060072D1 RID: 29393 RVA: 0x001AE440 File Offset: 0x001AC640
		// (set) Token: 0x060072D2 RID: 29394 RVA: 0x001AE469 File Offset: 0x001AC669
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Category("Behavior")]
		public virtual bool AllowMultiItemEdit
		{
			get
			{
				object obj = this.ControlState["AllowMultiItemEdit"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ControlState["AllowMultiItemEdit"] = value;
			}
		}

		// Token: 0x1700256C RID: 9580
		// (get) Token: 0x060072D3 RID: 29395 RVA: 0x001AE484 File Offset: 0x001AC684
		// (set) Token: 0x060072D4 RID: 29396 RVA: 0x001AE4AD File Offset: 0x001AC6AD
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual bool ConvertEmptyStringToNull
		{
			get
			{
				object obj = this.ControlState["ConvertEmptyStringToNull"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ControlState["ConvertEmptyStringToNull"] = value;
			}
		}

		// Token: 0x1700256D RID: 9581
		// (get) Token: 0x060072D5 RID: 29397 RVA: 0x001AE4C8 File Offset: 0x001AC6C8
		// (set) Token: 0x060072D6 RID: 29398 RVA: 0x001AE4F1 File Offset: 0x001AC6F1
		[Category("Default")]
		[DefaultValue(RadListViewInsertItemPosition.None)]
		[Description("Gets or sets the location of the InsertItemTemplate template when it is rendered as part of the RadListView control.")]
		public virtual RadListViewInsertItemPosition InsertItemPosition
		{
			get
			{
				object obj = this.ViewState["InsertItemPosition"];
				if (obj != null)
				{
					return (RadListViewInsertItemPosition)obj;
				}
				return RadListViewInsertItemPosition.None;
			}
			set
			{
				if (this.InsertItemPosition != value)
				{
					this.ViewState["InsertItemPosition"] = value;
					this.SetRequiresDataBindingIfInitialized();
				}
			}
		}

		// Token: 0x1700256E RID: 9582
		// (get) Token: 0x060072D7 RID: 29399 RVA: 0x001AE518 File Offset: 0x001AC718
		// (set) Token: 0x060072D8 RID: 29400 RVA: 0x001AE520 File Offset: 0x001AC720
		[Description("Gets the insert item of a RadListView control.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadListViewInsertItem InsertItem { get; protected set; }

		// Token: 0x060072D9 RID: 29401 RVA: 0x001AE52C File Offset: 0x001AC72C
		public void ClearSelectedItems()
		{
			foreach (RadListViewDataItem radListViewDataItem in this.Items)
			{
				radListViewDataItem.Selected = false;
			}
		}

		// Token: 0x060072DA RID: 29402 RVA: 0x001AE580 File Offset: 0x001AC780
		public void ClearEditItems()
		{
			foreach (RadListViewDataItem radListViewDataItem in this.Items)
			{
				radListViewDataItem.Edit = false;
			}
		}

		// Token: 0x060072DB RID: 29403 RVA: 0x001AE5D4 File Offset: 0x001AC7D4
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.UsesControlState)
			{
				this.Page.RegisterRequiresControlState(this);
			}
			if (this.AlwaysAutoBindOnPostBack && this.Page.IsPostBack)
			{
				this._shouldCallDataBindOnLoad = false;
				base.RequiresDataBinding = true;
				this.AutoDataBind(RadListViewRebindReason.PostbackViewStateNotPersisted);
			}
		}

		// Token: 0x060072DC RID: 29404 RVA: 0x001AE626 File Offset: 0x001AC826
		protected override void OnPagePreLoad(object sender, EventArgs e)
		{
			this._pagePreLoadFired = true;
			base.OnPagePreLoad(sender, e);
		}

		// Token: 0x1700256F RID: 9583
		// (get) Token: 0x060072DD RID: 29405 RVA: 0x001AE637 File Offset: 0x001AC837
		// (set) Token: 0x060072DE RID: 29406 RVA: 0x001AE657 File Offset: 0x001AC857
		[Description("Gets or sets ID of RadClientDataSource control that to be used for client side binding")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Data")]
		public virtual string ClientDataSourceID
		{
			get
			{
				return ((string)this.ViewState["ClientDataSourceID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ClientDataSourceID"] = value;
			}
		}

		// Token: 0x060072DF RID: 29407 RVA: 0x001AE66C File Offset: 0x001AC86C
		protected override void OnLoad(EventArgs e)
		{
			if (!base.IsBoundUsingDataSourceID)
			{
				base.ConfirmInitState();
				if (this.Page != null && !this._pagePreLoadFired && this.ViewState["_!DataBound"] == null)
				{
					if (!this.Page.IsPostBack)
					{
						base.RequiresDataBinding = true;
					}
					else if (base.IsViewStateEnabled)
					{
						base.RequiresDataBinding = true;
					}
				}
			}
			else
			{
				base.OnLoad(e);
			}
			if (this.ShouldBeBound)
			{
				this.AutoDataBind(RadListViewRebindReason.InitialLoad);
				return;
			}
			if (this.AlwaysAutoBindOnPostBack && this._shouldCallDataBindOnLoad)
			{
				this.AutoDataBind(RadListViewRebindReason.PostbackViewStateNotPersisted);
			}
		}

		// Token: 0x060072E0 RID: 29408 RVA: 0x001AE6FE File Offset: 0x001AC8FE
		protected override void Render(HtmlTextWriter writer)
		{
			this.RenderContents(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}

		// Token: 0x060072E1 RID: 29409 RVA: 0x001AE730 File Offset: 0x001AC930
		protected override void OnPreRender(EventArgs e)
		{
			if (base.RequiresDataBinding)
			{
				this.Rebind();
			}
			base.OnPreRender(e);
		}

		// Token: 0x060072E2 RID: 29410 RVA: 0x001AE747 File Offset: 0x001AC947
		protected override void RenderContents(HtmlTextWriter writer)
		{
			BaseClass.RenderVersionStamp(writer);
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			base.RenderContents(writer);
		}

		// Token: 0x17002570 RID: 9584
		// (get) Token: 0x060072E3 RID: 29411 RVA: 0x001AE76A File Offset: 0x001AC96A
		// (set) Token: 0x060072E4 RID: 29412 RVA: 0x001AE772 File Offset: 0x001AC972
		internal bool SkipDataBinding { get; set; }

		// Token: 0x060072E5 RID: 29413 RVA: 0x001AE77B File Offset: 0x001AC97B
		public override void DataBind()
		{
			if (this.SkipDataBinding)
			{
				return;
			}
			if (this.IsNeedDataSourceInProgress)
			{
				throw new InvalidOperationException("You should not call DataBind in NeedDataSource event handler. DataBind would take place automatically right after NeedDataSource handler finishes execution.");
			}
			base.DataBind();
		}

		// Token: 0x060072E6 RID: 29414 RVA: 0x001AE79F File Offset: 0x001AC99F
		public virtual void Rebind()
		{
			this.AutoDataBind(RadListViewRebindReason.ExplicitRebind);
		}

		// Token: 0x060072E7 RID: 29415 RVA: 0x001AE7A8 File Offset: 0x001AC9A8
		public virtual void ExtractValuesFromItem(IDictionary newValues, RadListViewDataItem dataItem, bool includePrimaryKey)
		{
			if (newValues == null)
			{
				throw new ArgumentNullException("newValues");
			}
			if (dataItem == null)
			{
				throw new ArgumentNullException("dataItem");
			}
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			RadListView.ExtractItemFromBindableControl(orderedDictionary, dataItem);
			this.FillValues(newValues, includePrimaryKey, orderedDictionary);
			IBindableTemplate bindableTemplate;
			if (dataItem.IsInEditMode)
			{
				if (dataItem is IRadListViewInsertItem)
				{
					bindableTemplate = (this.InsertItemTemplate as IBindableTemplate);
				}
				else
				{
					bindableTemplate = (this.EditItemTemplate as IBindableTemplate);
				}
			}
			else if (dataItem.ItemType == RadListViewItemType.SelectedItem)
			{
				bindableTemplate = (this.SelectedItemTemplate as IBindableTemplate);
			}
			else if (dataItem.ItemType == RadListViewItemType.AlternatingItem && this.AlternatingItemTemplate != null)
			{
				bindableTemplate = (this.AlternatingItemTemplate as IBindableTemplate);
			}
			else
			{
				bindableTemplate = (this.ItemTemplate as IBindableTemplate);
			}
			if (bindableTemplate != null)
			{
				this.FillValues(newValues, includePrimaryKey, bindableTemplate.ExtractValues(dataItem));
			}
		}

		// Token: 0x060072E8 RID: 29416 RVA: 0x001AE868 File Offset: 0x001ACA68
		[SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", MessageId = "System.Web.UI.IBindableControl")]
		private static void ExtractItemFromBindableControl(IOrderedDictionary values, Control container)
		{
			IBindableControl bindableControl = container as IBindableControl;
			if (bindableControl != null)
			{
				bindableControl.ExtractValues(values);
			}
			if (container is DataBoundControl || container is CompositeControl)
			{
				return;
			}
			foreach (object obj in container.Controls)
			{
				Control container2 = (Control)obj;
				RadListView.ExtractItemFromBindableControl(values, container2);
			}
		}

		// Token: 0x060072E9 RID: 29417 RVA: 0x001AE8E4 File Offset: 0x001ACAE4
		private void FillValues(IDictionary newValues, bool includePrimaryKey, IOrderedDictionary extractValues)
		{
			foreach (object obj in extractValues)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (includePrimaryKey || Array.IndexOf<object>(this.DataKeyNamesInternal, dictionaryEntry.Key) == -1)
				{
					object obj2 = dictionaryEntry.Value;
					string text = obj2 as string;
					if (this.ConvertEmptyStringToNull && text != null && text.Length == 0)
					{
						obj2 = null;
					}
					newValues.Add(dictionaryEntry.Key, obj2);
				}
			}
		}

		// Token: 0x060072EA RID: 29418 RVA: 0x001AE980 File Offset: 0x001ACB80
		public void PerformUpdate(RadListViewDataItem editedItem)
		{
			this.PerformUpdate(editedItem, false);
		}

		// Token: 0x060072EB RID: 29419 RVA: 0x001AEA2C File Offset: 0x001ACC2C
		public virtual void PerformUpdate(RadListViewDataItem editedItem, bool suppressRebind)
		{
			if (editedItem == null)
			{
				throw new ArgumentNullException("editedItem");
			}
			DataSourceView data = base.GetData();
			ModelDataSourceView modelDataSourceView = data as ModelDataSourceView;
			if (modelDataSourceView != null)
			{
				if (string.IsNullOrWhiteSpace(this.UpdateMethod))
				{
					throw new Exception("Updating is not supported unless the UpdateMethod is specified.");
				}
				this.ModelBindingUpdateProperties(modelDataSourceView);
			}
			if (data.CanUpdate)
			{
				Hashtable keys = new Hashtable();
				this.FillDataKeys(keys, editedItem);
				Hashtable hashtable = new Hashtable();
				this.ExtractValuesFromItem(hashtable, editedItem, false);
				data.Update(keys, hashtable, editedItem.SavedOldValues, delegate(int affectedRows, Exception exception)
				{
					RadListViewUpdatedEventArgs radListViewUpdatedEventArgs = new RadListViewUpdatedEventArgs(affectedRows, exception, editedItem)
					{
						KeepInEditMode = (exception != null)
					};
					if (this.IsUsingModelBinding)
					{
						if (this.Page.ModelState.IsValid)
						{
							this.FireItemUpdatedEvent(editedItem, suppressRebind, exception, radListViewUpdatedEventArgs);
						}
						else
						{
							editedItem.Edit = true;
						}
					}
					else
					{
						this.FireItemUpdatedEvent(editedItem, suppressRebind, exception, radListViewUpdatedEventArgs);
					}
					return radListViewUpdatedEventArgs.ExceptionHandled;
				});
			}
		}

		// Token: 0x060072EC RID: 29420 RVA: 0x001AEAF9 File Offset: 0x001ACCF9
		private void FireItemUpdatedEvent(RadListViewDataItem editedItem, bool suppressRebind, Exception exception, RadListViewUpdatedEventArgs args)
		{
			this.FireItemUpdated(args);
			this.ValidateModel("Update", args);
			if (!args.KeepInEditMode)
			{
				editedItem.Edit = false;
			}
			if (exception == null && !suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x060072ED RID: 29421 RVA: 0x001AEB2C File Offset: 0x001ACD2C
		private void ValidateModel(string commandName, RadListViewDataChangeEventArgs args)
		{
			RadListViewValidationSettings validationSettings = this.ValidationSettings;
			RadListView ownerListView = args.Item.OwnerListView;
			if (args.Exception != null && !args.ExceptionHandled && validationSettings.EnableModelValidation && !validationSettings.ValidateCommand(commandName))
			{
				ownerListView.IsModelValid = false;
				args.ExceptionHandled = true;
			}
		}

		// Token: 0x060072EE RID: 29422 RVA: 0x001AEB7B File Offset: 0x001ACD7B
		public virtual void PerformDelete(RadListViewDataItem editedItem)
		{
			this.PerformDelete(editedItem, false);
		}

		// Token: 0x060072EF RID: 29423 RVA: 0x001AEBE4 File Offset: 0x001ACDE4
		public virtual void PerformDelete(RadListViewDataItem editedItem, bool suppressRebind)
		{
			DataSourceView data = base.GetData();
			ModelDataSourceView modelDataSourceView = data as ModelDataSourceView;
			if (modelDataSourceView != null)
			{
				if (string.IsNullOrWhiteSpace(this.DeleteMethod))
				{
					throw new Exception("Deleting is not supported unless the DeleteMethod is specified.");
				}
				this.ModelBindingUpdateProperties(modelDataSourceView);
			}
			if (data.CanDelete)
			{
				editedItem.Edit = false;
				Hashtable keys = new Hashtable();
				this.FillDataKeys(keys, editedItem);
				Hashtable hashtable = new Hashtable();
				this.ExtractValuesFromItem(hashtable, editedItem, false);
				data.Delete(keys, hashtable, delegate(int affectedRows, Exception exception)
				{
					RadListViewDeletedEventArgs radListViewDeletedEventArgs = new RadListViewDeletedEventArgs(affectedRows, exception, editedItem);
					this.OnItemDeleted(radListViewDeletedEventArgs);
					this.ValidateModel("Delete", radListViewDeletedEventArgs);
					if (exception == null && !suppressRebind)
					{
						this.Rebind();
					}
					return radListViewDeletedEventArgs.ExceptionHandled;
				});
			}
		}

		// Token: 0x060072F0 RID: 29424 RVA: 0x001AEC9E File Offset: 0x001ACE9E
		public virtual void ShowInsertItem()
		{
			if (this.InsertItemPosition != RadListViewInsertItemPosition.None)
			{
				this.ShowInsertItem(this.InsertItemPosition);
				return;
			}
			this.ShowInsertItem(RadListViewInsertItemPosition.LastItem);
		}

		// Token: 0x060072F1 RID: 29425 RVA: 0x001AECBC File Offset: 0x001ACEBC
		public virtual void ShowInsertItem(RadListViewInsertItemPosition itemPosition)
		{
			this.ShowInsertItem(itemPosition, null);
		}

		// Token: 0x060072F2 RID: 29426 RVA: 0x001AECC6 File Offset: 0x001ACEC6
		public virtual void ShowInsertItem(IDictionary defaultValues)
		{
			this.ShowInsertItem(RadListViewInsertItemPosition.LastItem, defaultValues);
		}

		// Token: 0x060072F3 RID: 29427 RVA: 0x001AECD0 File Offset: 0x001ACED0
		public virtual void ShowInsertItem(RadListViewInsertItemPosition itemPosition, IDictionary defaultValues)
		{
			this.InitializeInsertObjectDefaultValues(defaultValues);
			this.InsertItemPosition = itemPosition;
			this.IsItemInserted = true;
			this.Rebind();
		}

		// Token: 0x060072F4 RID: 29428 RVA: 0x001AECED File Offset: 0x001ACEED
		public virtual void ShowInsertItem(object dataItem)
		{
			this._insertObject = dataItem;
			this.ShowInsertItem(RadListViewInsertItemPosition.LastItem, null);
		}

		// Token: 0x060072F5 RID: 29429 RVA: 0x001AECFE File Offset: 0x001ACEFE
		public virtual void ShowInsertItem(RadListViewInsertItemPosition itemPosition, object dataItem)
		{
			this._insertObject = dataItem;
			this.ShowInsertItem(itemPosition, null);
		}

		// Token: 0x060072F6 RID: 29430 RVA: 0x001AED0F File Offset: 0x001ACF0F
		private void InitializeInsertObjectDefaultValues(IDictionary defaultValues)
		{
			this._insertObjectDefaultValues = defaultValues;
		}

		// Token: 0x060072F7 RID: 29431 RVA: 0x001AED18 File Offset: 0x001ACF18
		protected virtual object GetDefaultInsertionObject()
		{
			if (this._insertObject != null)
			{
				return this._insertObject;
			}
			if (this._insertObjectDefaultValues != null)
			{
				return this.ResolvedDataSource.GetInsertionObject(this._insertObjectDefaultValues);
			}
			return null;
		}

		// Token: 0x060072F8 RID: 29432 RVA: 0x001AED44 File Offset: 0x001ACF44
		public virtual void PerformInsert()
		{
			if (this.InsertItem == null)
			{
				throw new InvalidOperationException("Insert item is available only when RadListView is in insert mode.");
			}
			this.PerformInsert(this.InsertItem, false);
		}

		// Token: 0x060072F9 RID: 29433 RVA: 0x001AEE20 File Offset: 0x001AD020
		public virtual void PerformInsert(RadListViewInsertItem insertItem, bool suppressRebind)
		{
			if (insertItem == null)
			{
				throw new ArgumentNullException("insertItem");
			}
			DataSourceView data = base.GetData();
			ModelDataSourceView modelDataSourceView = data as ModelDataSourceView;
			if (modelDataSourceView != null)
			{
				if (string.IsNullOrWhiteSpace(this.InsertMethod))
				{
					throw new Exception("Inserting is not supported unless the InsertMethod is specified.");
				}
				this.ModelBindingUpdateProperties(modelDataSourceView);
				suppressRebind = true;
			}
			if (data.CanInsert)
			{
				Hashtable hashtable = new Hashtable();
				this.ExtractValuesFromItem(hashtable, insertItem, true);
				data.Insert(hashtable, delegate(int affectedRows, Exception exception)
				{
					RadListViewInsertedEventArgs radListViewInsertedEventArgs = new RadListViewInsertedEventArgs(affectedRows, exception, insertItem)
					{
						KeepInInsertMode = (exception != null)
					};
					if (this.IsUsingModelBinding)
					{
						if (this.Page.ModelState.IsValid)
						{
							this.FireItemInsertedEvent(insertItem, suppressRebind, exception, radListViewInsertedEventArgs);
						}
						else
						{
							this.IsItemInserted = true;
							this.RequiresDataBinding = false;
							insertItem.Edit = true;
						}
					}
					else
					{
						this.FireItemInsertedEvent(insertItem, suppressRebind, exception, radListViewInsertedEventArgs);
					}
					return radListViewInsertedEventArgs.ExceptionHandled;
				});
			}
		}

		// Token: 0x060072FA RID: 29434 RVA: 0x001AEED0 File Offset: 0x001AD0D0
		private void FireItemInsertedEvent(RadListViewInsertItem insertItem, bool suppressRebind, Exception exception, RadListViewInsertedEventArgs args)
		{
			this.OnItemInserted(args);
			this.ValidateModel("PerformInsert", args);
			if (!args.KeepInInsertMode)
			{
				this.IsItemInserted = false;
				insertItem.Edit = false;
			}
			if (exception == null && !suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x060072FB RID: 29435 RVA: 0x001AEF0C File Offset: 0x001AD10C
		private void ModelBindingUpdateProperties(ModelDataSourceView modelView)
		{
			string dataKeyName = string.Empty;
			if (this.DataKeyNames.Length > 0)
			{
				dataKeyName = this.DataKeyNames[0];
			}
			modelView.UpdateProperties(this.ItemType, this.SelectMethod, this.UpdateMethod, base.InsertMethod, this.DeleteMethod, dataKeyName);
		}

		// Token: 0x060072FC RID: 29436 RVA: 0x001AEF58 File Offset: 0x001AD158
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			IEnumerable<ScriptReference> scriptReferences = base.GetScriptReferences();
			List<ScriptReference> list = new List<ScriptReference>(scriptReferences);
			if (this.EnableEmbeddedScripts)
			{
				this.AddFeatureSpecificScriptReferences(list);
				bool flag = true;
				RadScriptManager radScriptManager = ScriptManager.GetCurrent(this.Page) as RadScriptManager;
				if (radScriptManager != null)
				{
					flag = radScriptManager.EnableEmbeddedjQuery;
				}
				if (flag)
				{
					list.Add(new ScriptReference("Telerik.Web.UI.Common.jQuery.js", RadListView.CurrentAssemblyName));
				}
			}
			return list;
		}

		// Token: 0x060072FD RID: 29437 RVA: 0x001AEFE0 File Offset: 0x001AD1E0
		private void AddFeatureSpecificScriptReferences(List<ScriptReference> baseReferences)
		{
			string resourceNameSuffix = "Script";
			TFunc<string, ScriptReference> tfunc = (string resourceName) => new ScriptReference(string.Format("{0}{1}.js", resourceName, resourceNameSuffix), RadListView.CurrentAssemblyName);
			if (this.ClientSettings.AllowItemsDragDrop)
			{
				baseReferences.Add(tfunc("Telerik.Web.UI.ListView.RadListViewItemDrag"));
			}
		}

		// Token: 0x060072FE RID: 29438 RVA: 0x001AF040 File Offset: 0x001AD240
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			this.RegisterClientSideEvents(delegate(string eventName, string eventValue)
			{
				RadCompositeDataBoundControl.DescribeEvent(descriptor, eventName, eventValue);
			});
			this.DescribeProperties(descriptor);
		}

		// Token: 0x060072FF RID: 29439 RVA: 0x001AF084 File Offset: 0x001AD284
		private void DescribeProperties(IScriptDescriptor descriptor)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new RadListViewJavaScriptConverter()
			});
			if (this.IsBoundUsingOData)
			{
				descriptor.AddScriptProperty("odataClientSettings", javaScriptSerializer.Serialize(ODataClientSettings.FromRadListViewControl(this)));
			}
			descriptor.AddProperty("UniqueID", this.UniqueID);
			if (base.RuntimeSkin != "Default")
			{
				descriptor.AddProperty("Skin", base.RuntimeSkin);
			}
			if (this.AllowPaging)
			{
				descriptor.AddProperty("_allowPaging", this.AllowPaging);
			}
			if (this.PageSize != 10)
			{
				descriptor.AddProperty("_pageSize", this.PageSize);
			}
			if (this.CurrentPageIndex != 0)
			{
				descriptor.AddProperty("_currentPageIndex", this.CurrentPageIndex);
			}
			int num = (this.VirtualItemCount > 0) ? this.VirtualItemCount : this.GetDataItemCountFromState();
			if (num > 0)
			{
				descriptor.AddProperty("_virtualItemCount", num);
			}
			if (this.IsItemInserted)
			{
				descriptor.AddProperty("_isItemInserted", this.IsItemInserted);
			}
			if (this.SelectedIndexes.Count > 0)
			{
				descriptor.AddProperty("selectedIndexes", this.SelectedIndexes);
			}
			if (this.AllowMultiItemSelection)
			{
				descriptor.AddProperty("_allowMultiItemSelection", this.AllowMultiItemSelection);
			}
			if (this.SortExpressions.Count > 0)
			{
				descriptor.AddScriptProperty("_sortExpressions", javaScriptSerializer.Serialize(this.SortExpressions.GetSortString()));
			}
			if (this.AllowNaturalSort)
			{
				descriptor.AddProperty("_allowNaturalSort", this.AllowNaturalSort);
			}
			if (this.AllowMultiFieldSorting)
			{
				descriptor.AddProperty("_allowMultiFieldSorting", this.AllowMultiFieldSorting);
			}
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				try
				{
					Control control = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					descriptor.AddProperty("_clientDataSourceID", control.ClientID);
				}
				catch (GridException)
				{
					descriptor.AddProperty("_clientDataSourceID", this.ClientDataSourceID);
				}
			}
			if (!string.IsNullOrEmpty(this._clientFilterExpression))
			{
				descriptor.AddScriptProperty("_filterExpressions", javaScriptSerializer.Serialize(this._clientFilterExpression));
			}
			this.AddClientDataKeyValues(javaScriptSerializer, descriptor);
			descriptor.AddScriptProperty("_clientSettings", javaScriptSerializer.Serialize(this.ClientSettings));
		}

		// Token: 0x06007300 RID: 29440 RVA: 0x001AF2E0 File Offset: 0x001AD4E0
		private void AddClientDataKeyValues(JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			if (this.ClientDataKeyNames.Length > 0)
			{
				Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>();
				foreach (RadListViewDataItem radListViewDataItem in this.Items)
				{
					Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
					for (int i = 0; i < this.ClientDataKeyNames.Length; i++)
					{
						string text = this.ClientDataKeyNames[i];
						string value = (radListViewDataItem.GetDataKeyValue(text) != null) ? radListViewDataItem.GetDataKeyValue(text).ToString() : null;
						dictionary2[text] = value;
					}
					dictionary.Add(radListViewDataItem.DisplayIndex.ToString(), dictionary2);
				}
				if (dictionary.Count > 0)
				{
					descriptor.AddScriptProperty("_clientKeyValues", serializer.Serialize(dictionary));
				}
			}
		}

		// Token: 0x06007301 RID: 29441 RVA: 0x001AF3D0 File Offset: 0x001AD5D0
		private void RegisterClientSideEvents(TAction<string, string> eventData)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.ClientSettings.ClientEvents);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!(propertyDescriptor.DisplayName == "ViewState"))
				{
					string text = propertyDescriptor.DisplayName.Replace("On", string.Empty);
					text = Regex.Replace(text, "^[A-Z]", (Match match) => match.ToString().ToLower(CultureInfo.InvariantCulture));
					string text2 = propertyDescriptor.GetValue(this.ClientSettings.ClientEvents).ToString();
					if (!string.IsNullOrEmpty(text2))
					{
						eventData(text, text2);
					}
				}
			}
		}

		// Token: 0x06007302 RID: 29442 RVA: 0x001AF4B4 File Offset: 0x001AD6B4
		private object ExtractDataKeyValue(object dataItem, string name)
		{
			DataRowView dataRowView = dataItem as DataRowView;
			DataRow dataRow = dataItem as DataRow;
			object result;
			if (dataRowView != null)
			{
				result = dataRowView[name];
			}
			else if (dataRow != null)
			{
				result = dataRow[name];
			}
			else
			{
				if (name.Contains("."))
				{
					try
					{
						return DataBinder.GetPropertyValue(dataItem, name);
					}
					catch
					{
						return DataBinder.Eval(dataItem, name);
					}
				}
				result = DataBinder.GetPropertyValue(dataItem, name);
			}
			return result;
		}

		// Token: 0x04001EC4 RID: 7876
		internal const string DataSourceItemCountControlStateKey = "_!DSIC";

		// Token: 0x04001EC5 RID: 7877
		internal const string ItemCountControlStateKey = "_!ItemCount";

		// Token: 0x04001EC6 RID: 7878
		internal const string PageCountViewStateKey = "_!PCount";

		// Token: 0x04001EC7 RID: 7879
		internal const string ClientPostbackFunctionFormat = "FireCommand:{0}|;{1}|;";

		// Token: 0x04001EC8 RID: 7880
		internal const string FirstLevelDataGroupKey = "__0level";

		// Token: 0x04001EC9 RID: 7881
		private const int _defaultPageSize = 10;

		// Token: 0x04001ECA RID: 7882
		public const string PageCommandName = "Page";

		// Token: 0x04001ECB RID: 7883
		public const string ChangePageSizeCommandName = "ChangePageSize";

		// Token: 0x04001ECC RID: 7884
		public const string SortCommandName = "Sort";

		// Token: 0x04001ECD RID: 7885
		public const string SelectCommandName = "Select";

		// Token: 0x04001ECE RID: 7886
		public const string DeselectCommandName = "Deselect";

		// Token: 0x04001ECF RID: 7887
		public const string EditCommandName = "Edit";

		// Token: 0x04001ED0 RID: 7888
		public const string UpdateCommandName = "Update";

		// Token: 0x04001ED1 RID: 7889
		public const string CancelCommandName = "Cancel";

		// Token: 0x04001ED2 RID: 7890
		public const string DeleteCommandName = "Delete";

		// Token: 0x04001ED3 RID: 7891
		public const string PerformInsertCommandName = "PerformInsert";

		// Token: 0x04001ED4 RID: 7892
		public const string InitInsertCommandName = "InitInsert";

		// Token: 0x04001ED5 RID: 7893
		public const string RebindListViewCommandName = "RebindListView";

		// Token: 0x04001ED6 RID: 7894
		internal static readonly string CurrentAssemblyName = Assembly.GetExecutingAssembly().FullName;

		// Token: 0x04001ED7 RID: 7895
		private static TFunc<string, string> parseFireCommandArgs = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[2];
		};

		// Token: 0x04001ED8 RID: 7896
		private static TFunc<string, string> parseFireCommandEventName = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[0];
		};

		// Token: 0x04001ED9 RID: 7897
		private static readonly object EventItemCommand;

		// Token: 0x04001EDA RID: 7898
		private static readonly object EventItemCreated;

		// Token: 0x04001EDB RID: 7899
		private static readonly object EventItemDataBound;

		// Token: 0x04001EDC RID: 7900
		private static readonly object EventLayoutCreated = new object();

		// Token: 0x04001EDD RID: 7901
		private static readonly object EventNeedDataSource;

		// Token: 0x04001EDE RID: 7902
		private static readonly object EventPageIndexChanged;

		// Token: 0x04001EDF RID: 7903
		private static readonly object EventPageSizeChanged;

		// Token: 0x04001EE0 RID: 7904
		private static readonly object EventSorting;

		// Token: 0x04001EE1 RID: 7905
		private static readonly object EventTotalRowCountAvailable;

		// Token: 0x04001EE2 RID: 7906
		private static readonly object EventTotalRowCountAvailableAsp;

		// Token: 0x04001EE3 RID: 7907
		private static readonly object EventSelectedIndexChanged;

		// Token: 0x04001EE4 RID: 7908
		private static readonly object EventItemEditing;

		// Token: 0x04001EE5 RID: 7909
		private static readonly object EventItemUpdating;

		// Token: 0x04001EE6 RID: 7910
		private static readonly object EventItemCanceling;

		// Token: 0x04001EE7 RID: 7911
		private static readonly object EventItemUpdated;

		// Token: 0x04001EE8 RID: 7912
		private static readonly object EventItemDeleting;

		// Token: 0x04001EE9 RID: 7913
		private static readonly object EventItemDeleted;

		// Token: 0x04001EEA RID: 7914
		private static readonly object EventItemInserting;

		// Token: 0x04001EEB RID: 7915
		private static readonly object EventItemInserted;

		// Token: 0x04001EEC RID: 7916
		private static readonly object EventItemDrop;

		// Token: 0x04001EED RID: 7917
		private static readonly object EventFieldDescriptorsReady;

		// Token: 0x04001EEE RID: 7918
		private static readonly object EventCustomAggregate;

		// Token: 0x04001EEF RID: 7919
		private ListViewControlLocator _controlLocator;

		// Token: 0x04001EF0 RID: 7920
		private ListViewControlStateManager _controlStateManager;

		// Token: 0x04001EF1 RID: 7921
		private List<DataKey> _dataKeysArrayList;

		// Token: 0x04001EF2 RID: 7922
		private ListViewDataSourceHelper _dataSourceHelper;

		// Token: 0x04001EF3 RID: 7923
		private RadListViewDataItemCollection _items;

		// Token: 0x04001EF4 RID: 7924
		private ListViewEnumerableBase _resolvedDataSource;

		// Token: 0x04001EF5 RID: 7925
		private Control _itemsWrapperContainer;

		// Token: 0x04001EF6 RID: 7926
		private bool _instantiatedEmptyDataTemplate;

		// Token: 0x04001EF7 RID: 7927
		private int _placeholderControlIndex;

		// Token: 0x04001EF8 RID: 7928
		private int _itemsCreatedInContainerCount;

		// Token: 0x04001EF9 RID: 7929
		private int _autoIDIndex;

		// Token: 0x04001EFA RID: 7930
		private RadListViewIndexesCollection _selectedIndexes;

		// Token: 0x04001EFB RID: 7931
		private RadListViewIndexesCollection _editIndexes;

		// Token: 0x04001EFC RID: 7932
		private RadListViewDataKeyArray _dataKeyValues;

		// Token: 0x04001EFD RID: 7933
		private RadListViewSortExpressionCollection _sortExpressions;

		// Token: 0x04001EFE RID: 7934
		private ListViewDataGroupCollection _dataGroups;

		// Token: 0x04001EFF RID: 7935
		private RadListViewValidationSettings _validationSettings;

		// Token: 0x04001F00 RID: 7936
		private int _groupPlaceholderControlIndex;

		// Token: 0x04001F01 RID: 7937
		private Control _groupItemWrapperContainer;

		// Token: 0x04001F02 RID: 7938
		private Control _firstDataGroupWrapperContainer;

		// Token: 0x04001F03 RID: 7939
		private Dictionary<string, Control> _dataGroupWrapperContainers = new Dictionary<string, Control>();

		// Token: 0x04001F04 RID: 7940
		private Dictionary<string, Type> _itemPropertyTypes;

		// Token: 0x04001F05 RID: 7941
		private int _groupsItemCreatedinContainerCount;

		// Token: 0x04001F06 RID: 7942
		private int _firstLevelDataGroupsCount;

		// Token: 0x04001F07 RID: 7943
		private int _firstLevelDataGroupControlIndex;

		// Token: 0x04001F08 RID: 7944
		private RadListViewClientSettings _clientSettings;

		// Token: 0x04001F09 RID: 7945
		private RadListViewFilterExpressionCollection _filterExpressions;

		// Token: 0x04001F0A RID: 7946
		private bool _shouldCallDataBindOnLoad = true;

		// Token: 0x04001F0B RID: 7947
		private string _clientFilterExpression = string.Empty;

		// Token: 0x04001F0C RID: 7948
		private bool _isModelValid = true;

		// Token: 0x04001F0D RID: 7949
		private bool _isDataSourceViewFilter;

		// Token: 0x04001F0E RID: 7950
		private bool _ignoreDataSourceViewChanged;

		// Token: 0x04001F0F RID: 7951
		private DataSourceView _currentDataSource;

		// Token: 0x04001F10 RID: 7952
		private bool _pagePreLoadFired;

		// Token: 0x04001F11 RID: 7953
		private IDictionary _insertObjectDefaultValues;

		// Token: 0x04001F12 RID: 7954
		private object _insertObject;

		// Token: 0x02000BB8 RID: 3000
		private class DummyDataSource : DataSourceControl
		{
			// Token: 0x0600730F RID: 29455 RVA: 0x001AF524 File Offset: 0x001AD724
			public DummyDataSource(IEnumerable source)
			{
				this._source = source;
			}

			// Token: 0x06007310 RID: 29456 RVA: 0x001AF533 File Offset: 0x001AD733
			protected override DataSourceView GetView(string viewName)
			{
				return new RadListView.DummyDataSource.DummyDataView(this, viewName, this._source);
			}

			// Token: 0x04001F28 RID: 7976
			private IEnumerable _source;

			// Token: 0x02000BB9 RID: 3001
			private class DummyDataView : DataSourceView
			{
				// Token: 0x06007311 RID: 29457 RVA: 0x001AF542 File Offset: 0x001AD742
				public DummyDataView(IDataSource owner, string viewName, IEnumerable source) : base(owner, viewName)
				{
					this._source = source;
				}

				// Token: 0x06007312 RID: 29458 RVA: 0x001AF553 File Offset: 0x001AD753
				protected override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
				{
					return this._source;
				}

				// Token: 0x04001F29 RID: 7977
				private IEnumerable _source;
			}
		}

		// Token: 0x02000BBA RID: 3002
		private class Messages
		{
			// Token: 0x04001F2A RID: 7978
			public const string GroupItemCountOutOfRange = "The GroupItemCount property is set to a value less than 1.";

			// Token: 0x04001F2B RID: 7979
			public const string NotInInsertMode = "Insert item is available only when RadListView is in insert mode.";

			// Token: 0x04001F2C RID: 7980
			public const string BindCalledDuringNeedDataSourceException = "You should not call DataBind in NeedDataSource event handler. DataBind would take place automatically right after NeedDataSource handler finishes execution.";

			// Token: 0x04001F2D RID: 7981
			public const string ItemTemplateIsRequired = "The RadListView control does not have an ItemTemplate template specified.";

			// Token: 0x04001F2E RID: 7982
			public const string NoItemPlaceholderIdError = "The RadListView control does not have an item placeholder specified.";

			// Token: 0x04001F2F RID: 7983
			public const string ThereWasAProblemExtractingDataKeyValuesFromTheDataSource = "There was a problem extracting DataKeyValues from the DataSource. Please ensure that DataKeyNames are specified correctly and all fields specified exist in the DataSource.";

			// Token: 0x04001F30 RID: 7984
			public const string InsertItemTemplateRequired = "The RadListView control does not have an InsertItemTemplate template specified.";
		}
	}
}
