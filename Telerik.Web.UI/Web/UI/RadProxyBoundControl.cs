using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020000C9 RID: 201
	internal class RadProxyBoundControl : CompositeDataBoundControl
	{
		// Token: 0x060007AC RID: 1964 RVA: 0x0001CEB3 File Offset: 0x0001B0B3
		public RadProxyBoundControl(RadClientDataSource owner)
		{
			this.Owner = owner;
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0001CECD File Offset: 0x0001B0CD
		internal bool IsUsingModelBinding
		{
			get
			{
				return base.IsUsingModelBinders;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0001CED5 File Offset: 0x0001B0D5
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x0001CEDD File Offset: 0x0001B0DD
		protected bool IsDataBinding { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0001CEE6 File Offset: 0x0001B0E6
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x0001CEEE File Offset: 0x0001B0EE
		public bool AllowPaging { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0001CEF7 File Offset: 0x0001B0F7
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0001CEFF File Offset: 0x0001B0FF
		public bool AllowCustomPaging { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0001CF08 File Offset: 0x0001B108
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x0001CF10 File Offset: 0x0001B110
		protected IEnumerable CurrentDataSource { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0001CF19 File Offset: 0x0001B119
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x0001CF21 File Offset: 0x0001B121
		public int DataSourceCount { get; private set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001CF2A File Offset: 0x0001B12A
		internal virtual ProxyBoundControlDataSourceHelper DataSourceHelper
		{
			get
			{
				if (this._dataSourceHelper == null)
				{
					this._dataSourceHelper = new ProxyBoundControlDataSourceHelper();
				}
				return this._dataSourceHelper;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001CF48 File Offset: 0x0001B148
		protected virtual ProxyBoundControlEnumerableBase ResolvedDataSource
		{
			get
			{
				if (this._resolvedDataSource == null)
				{
					if (this.IsDataBinding)
					{
						this._resolvedDataSource = this.DataSourceHelper.GetResolvedDataSource(this, this.CurrentDataSource, this.DataMember);
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

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0001CFAD File Offset: 0x0001B1AD
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x0001CFB5 File Offset: 0x0001B1B5
		internal virtual bool CanRetrieveAllData { get; set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001CFBE File Offset: 0x0001B1BE
		internal virtual RadListViewSortExpressionCollection SortExpressions
		{
			get
			{
				if (this._sortExpressions == null)
				{
					this._sortExpressions = new RadListViewSortExpressionCollection();
				}
				return this._sortExpressions;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001CFD9 File Offset: 0x0001B1D9
		internal virtual RadListViewFilterExpressionCollection FilterExpressions
		{
			get
			{
				if (this._filterExpressions == null)
				{
					this._filterExpressions = new RadListViewFilterExpressionCollection();
				}
				return this._filterExpressions;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x0001D008 File Offset: 0x0001B208
		public virtual int PageSize
		{
			get
			{
				if (this._pageSize >= 1)
				{
					return this._pageSize;
				}
				return 10;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._pageSize = value;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0001D020 File Offset: 0x0001B220
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x0001D032 File Offset: 0x0001B232
		public int CurrentPageIndex
		{
			get
			{
				if (this._currentPageIndex != 0)
				{
					return this._currentPageIndex;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._currentPageIndex = value;
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001D04C File Offset: 0x0001B24C
		protected override DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			DataSourceSelectArguments dataSourceSelectArguments = new DataSourceSelectArguments();
			DataSourceView data = this.GetData();
			string a = data.GetType().ToString();
			if (a == "System.Web.UI.WebControls.EntityDataSourceView" || a == "Microsoft.AspNet.EntityDataSource.EntityDataSourceView" || a == "Telerik.OpenAccess.RT.DataSource.OpenAccessDataSourceView" || a == "Telerik.OpenAccess.Web.OpenAccessLinqDataSourceView" || a == "System.Web.UI.WebControls.LinqDataSourceView")
			{
				if (!data.CanSort || (this.FilterExpressions.Count > 0 && !this._isDataSourceViewFilter))
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
			if (flag)
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
					if (this.CanRetrieveAllData && ((this.FilterExpressions.Count > 0 && !this._isDataSourceViewFilter) || this.SortExpressions.Count > 0))
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

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001D290 File Offset: 0x0001B490
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

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001D327 File Offset: 0x0001B527
		protected bool IsDataSourceViewWithFiltering()
		{
			return this.IsLinqDataSourceView || this.IsEnityDataSourceView || this.IsOpenAccessDataSourceView;
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0001D341 File Offset: 0x0001B541
		protected bool IsLinqDataSourceView
		{
			get
			{
				return this.GetData().GetType().ToString() == "System.Web.UI.WebControls.LinqDataSourceView";
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0001D35D File Offset: 0x0001B55D
		protected bool IsEnityDataSourceView
		{
			get
			{
				return this.GetData().GetType().ToString() == "System.Web.UI.WebControls.EntityDataSourceView" || this.GetData().GetType().ToString() == "Microsoft.AspNet.EntityDataSource.EntityDataSourceView";
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0001D397 File Offset: 0x0001B597
		protected bool IsOpenAccessDataSourceView
		{
			get
			{
				return this.GetData().GetType().ToString().IndexOf("OpenAccess.RT.DataSource.OpenAccessDataSourceView") > -1;
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001D3B6 File Offset: 0x0001B5B6
		private void ClearResolvedDataSource()
		{
			this._resolvedDataSource = null;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001D3C0 File Offset: 0x0001B5C0
		protected virtual void PrepareDataSource()
		{
			ProxyBoundControlEnumerableBase resolvedDataSource = this._resolvedDataSource;
			resolvedDataSource.IsBoundUsingDataSourceID = base.IsBoundUsingDataSourceID;
			if (resolvedDataSource.SupportsSorting)
			{
				resolvedDataSource.SetSortExpressions(this.SortExpressions);
			}
			if (resolvedDataSource.SupportsFiltering)
			{
				resolvedDataSource.SetFilteringExpressions(this.FilterExpressions);
				resolvedDataSource.ShouldApplyFiltering = !this._isDataSourceViewFilter;
			}
			if (resolvedDataSource.SupportsPaging)
			{
				RadProxyBoundControlPagingManager pagingManager = resolvedDataSource.PagingManager;
				pagingManager.CurrentPageIndex = this.CurrentPageIndex;
				pagingManager.PageSize = this.PageSize;
				pagingManager.AllowPaging = this.AllowPaging;
				pagingManager.AllowCustomPaging = this.AllowCustomPaging;
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001D45C File Offset: 0x0001B65C
		public virtual void PerformInsert(Hashtable newValues)
		{
			if (this.Owner.DataSource.DataSourceControlSettings == null || !this.Owner.DataSource.DataSourceControlSettings.AllowAutomaticInserts)
			{
				return;
			}
			DataSourceView data = base.GetData();
			if (data.CanInsert)
			{
				data.Insert(newValues, (int affectedRows, Exception exception) => false);
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001D4CC File Offset: 0x0001B6CC
		public virtual void PerformUpdate(Hashtable keys, Hashtable newValues, Hashtable oldValues)
		{
			if (this.Owner.DataSource.DataSourceControlSettings == null || !this.Owner.DataSource.DataSourceControlSettings.AllowAutomaticUpdates)
			{
				return;
			}
			DataSourceView data = base.GetData();
			if (data.CanUpdate)
			{
				data.Update(keys, newValues, oldValues, (int affectedRows, Exception exception) => false);
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001D53C File Offset: 0x0001B73C
		public virtual void PerformDelete(Hashtable keys, Hashtable oldValues)
		{
			if (this.Owner.DataSource.DataSourceControlSettings == null || !this.Owner.DataSource.DataSourceControlSettings.AllowAutomaticDeletes)
			{
				return;
			}
			DataSourceView data = base.GetData();
			if (data.CanDelete)
			{
				data.Delete(keys, oldValues, (int affectedRows, Exception exception) => false);
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001D6C4 File Offset: 0x0001B8C4
		protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			this.IsDataBinding = dataBinding;
			this.CurrentDataSource = dataSource;
			if (this.ResolvedDataSource == ProxyBoundControlEnumerableBase.Null && dataSource != null)
			{
				this.ClearResolvedDataSource();
			}
			if (this.ResolvedDataSource == ProxyBoundControlEnumerableBase.Null && dataSource == null)
			{
				return 0;
			}
			int count = 0;
			bool flag = false;
			bool flag2 = false;
			if (dataBinding)
			{
				DataSourceView data = this.GetData();
				flag2 = (data is ObjectDataSourceView);
				bool flag3 = this.AllowPaging && data.CanPage;
				DataSourceSelectArguments selectArguments = base.SelectArguments;
				bool flag4 = false;
				flag = data.CanRetrieveTotalRowCount;
				if (flag3 && data.CanRetrieveTotalRowCount)
				{
					this.AllowCustomPaging = true;
					if ((this.SortExpressions.Count > 0 || (this.FilterExpressions.Count > 0 && !this._isDataSourceViewFilter)) && this.CanRetrieveAllData)
					{
						this.AllowCustomPaging = false;
					}
					flag4 = true;
				}
				if (flag4)
				{
					this.PrepareDataSource();
				}
			}
			dataSource = this.ResolvedDataSource.RawEnumerable();
			count = this.ResolvedDataSource.Count;
			count = ((flag && !flag2) ? base.SelectArguments.TotalRowCount : this.ResolvedDataSource.DataSourceCount);
			this.DataSourceCount = this.ResolvedDataSource.DataSourceCount;
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			foreach (object dataItem in dataSource)
			{
				list.Add(this.DataItemToDictionary(dataItem));
			}
			string text = javaScriptSerializer.Serialize(new
			{
				count = count,
				data = list
			});
			this.CurrentDataSource = null;
			this.ClearResolvedDataSource();
			this.returnJson = text;
			return 0;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001D86C File Offset: 0x0001BA6C
		internal Dictionary<string, object> DataItemToDictionary(object dataItem)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(dataItem);
			if (this.Owner.Schema != null && this.Owner.Schema.Model != null && this.Owner.Schema.Model.Fields != null && this.Owner.Schema.Model.Fields.Count > 0)
			{
				propertyDescriptorCollection = this.FilterModelProperties(propertyDescriptorCollection);
			}
			foreach (object obj in propertyDescriptorCollection)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				dictionary[propertyDescriptor.Name] = propertyDescriptor.GetValue(dataItem);
			}
			return dictionary;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001D938 File Offset: 0x0001BB38
		private PropertyDescriptorCollection FilterModelProperties(PropertyDescriptorCollection properties)
		{
			ClientDataSourceModelFieldCollection fields = this.Owner.Schema.Model.Fields;
			List<string> list = new List<string>(fields.Count);
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			foreach (object obj in fields)
			{
				ClientDataSourceModelField clientDataSourceModelField = (ClientDataSourceModelField)obj;
				list.Add(string.IsNullOrEmpty(clientDataSourceModelField.OriginalFieldName) ? clientDataSourceModelField.FieldName : clientDataSourceModelField.OriginalFieldName);
			}
			foreach (object obj2 in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
				if (list.Contains(propertyDescriptor.Name))
				{
					propertyDescriptorCollection.Add(propertyDescriptor);
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0001DA38 File Offset: 0x0001BC38
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x0001DA40 File Offset: 0x0001BC40
		public RadClientDataSource Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001DA49 File Offset: 0x0001BC49
		public string GetJson()
		{
			return this.returnJson;
		}

		// Token: 0x040001CB RID: 459
		private string returnJson = string.Empty;

		// Token: 0x040001CC RID: 460
		private RadClientDataSource _owner;

		// Token: 0x040001CD RID: 461
		private ProxyBoundControlDataSourceHelper _dataSourceHelper;

		// Token: 0x040001CE RID: 462
		private ProxyBoundControlEnumerableBase _resolvedDataSource;

		// Token: 0x040001CF RID: 463
		private RadListViewSortExpressionCollection _sortExpressions;

		// Token: 0x040001D0 RID: 464
		private RadListViewFilterExpressionCollection _filterExpressions;

		// Token: 0x040001D1 RID: 465
		private int _pageSize;

		// Token: 0x040001D2 RID: 466
		private int _currentPageIndex;

		// Token: 0x040001D3 RID: 467
		private bool _isDataSourceViewFilter;
	}
}
