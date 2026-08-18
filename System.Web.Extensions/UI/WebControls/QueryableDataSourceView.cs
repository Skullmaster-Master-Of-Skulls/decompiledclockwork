using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Resources;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000C4 RID: 196
	public abstract class QueryableDataSourceView : DataSourceView, IStateManager
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x00024AD0 File Offset: 0x00022CD0
		protected QueryableDataSourceView(DataSourceControl owner, string viewName, HttpContext context) : this(owner, viewName, context, new DynamicQueryableWrapper())
		{
			this._context = context;
			this._owner = owner;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00024AEE File Offset: 0x00022CEE
		internal QueryableDataSourceView(DataSourceControl owner, string viewName, HttpContext context, IDynamicQueryable queryable) : base(owner, viewName)
		{
			this._context = context;
			this._queryable = queryable;
			this._owner = owner;
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00024B1C File Offset: 0x00022D1C
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00024B24 File Offset: 0x00022D24
		public bool AutoGenerateOrderByClause
		{
			get
			{
				return this._autoGenerateOrderByClause;
			}
			set
			{
				if (this._autoGenerateOrderByClause != value)
				{
					this._autoGenerateOrderByClause = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00024B41 File Offset: 0x00022D41
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x00024B49 File Offset: 0x00022D49
		public bool AutoGenerateWhereClause
		{
			get
			{
				return this._autoGenerateWhereClause;
			}
			set
			{
				if (this._autoGenerateWhereClause != value)
				{
					this._autoGenerateWhereClause = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00024B66 File Offset: 0x00022D66
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x00024B6E File Offset: 0x00022D6E
		public virtual bool AutoPage
		{
			get
			{
				return this._autoPage;
			}
			set
			{
				if (this._autoPage != value)
				{
					this._autoPage = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00024B8B File Offset: 0x00022D8B
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x00024B93 File Offset: 0x00022D93
		public virtual bool AutoSort
		{
			get
			{
				return this._autoSort;
			}
			set
			{
				if (this._autoSort != value)
				{
					this._autoSort = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0001359B File Offset: 0x0001179B
		public override bool CanDelete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0001359B File Offset: 0x0001179B
		public override bool CanInsert
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0001D1CA File Offset: 0x0001B3CA
		public override bool CanPage
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0001D1CA File Offset: 0x0001B3CA
		public override bool CanRetrieveTotalRowCount
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0001D1CA File Offset: 0x0001B3CA
		public override bool CanSort
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0001359B File Offset: 0x0001179B
		public override bool CanUpdate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00024BB0 File Offset: 0x00022DB0
		public virtual ParameterCollection DeleteParameters
		{
			get
			{
				if (this._deleteParameters == null)
				{
					this._deleteParameters = new ParameterCollection();
				}
				return this._deleteParameters;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600099A RID: 2458
		protected abstract Type EntityType { get; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00024BCC File Offset: 0x00022DCC
		public virtual ParameterCollection GroupByParameters
		{
			get
			{
				if (this._groupByParameters == null)
				{
					this._groupByParameters = new ParameterCollection();
					this._groupByParameters.ParametersChanged += this.OnQueryParametersChanged;
					if (this._isTracking)
					{
						DataSourceHelper.TrackViewState(this._groupByParameters);
					}
				}
				return this._groupByParameters;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00024C1C File Offset: 0x00022E1C
		protected bool IsTrackingViewState
		{
			get
			{
				return this._isTracking;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00024C24 File Offset: 0x00022E24
		public virtual ParameterCollection InsertParameters
		{
			get
			{
				if (this._insertParameters == null)
				{
					this._insertParameters = new ParameterCollection();
				}
				return this._insertParameters;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00024C40 File Offset: 0x00022E40
		public virtual ParameterCollection OrderByParameters
		{
			get
			{
				if (this._orderByParameters == null)
				{
					this._orderByParameters = new ParameterCollection();
					this._orderByParameters.ParametersChanged += this.OnQueryParametersChanged;
					if (this._isTracking)
					{
						DataSourceHelper.TrackViewState(this._orderByParameters);
					}
				}
				return this._orderByParameters;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00024C90 File Offset: 0x00022E90
		public virtual ParameterCollection OrderGroupsByParameters
		{
			get
			{
				if (this._orderGroupsByParameters == null)
				{
					this._orderGroupsByParameters = new ParameterCollection();
					this._orderGroupsByParameters.ParametersChanged += this.OnQueryParametersChanged;
					if (this._isTracking)
					{
						DataSourceHelper.TrackViewState(this._orderGroupsByParameters);
					}
				}
				return this._orderGroupsByParameters;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00024CE0 File Offset: 0x00022EE0
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00024CF1 File Offset: 0x00022EF1
		public virtual string OrderBy
		{
			get
			{
				return this._orderBy ?? string.Empty;
			}
			set
			{
				if (this._orderBy != value)
				{
					this._orderBy = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00024D13 File Offset: 0x00022F13
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x00024D24 File Offset: 0x00022F24
		public virtual string OrderGroupsBy
		{
			get
			{
				return this._orderGroupsBy ?? string.Empty;
			}
			set
			{
				if (this._orderGroupsBy != value)
				{
					this._orderGroupsBy = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00024D46 File Offset: 0x00022F46
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x00024D57 File Offset: 0x00022F57
		public virtual string GroupBy
		{
			get
			{
				return this._groupBy ?? string.Empty;
			}
			set
			{
				if (this._groupBy != value)
				{
					this._groupBy = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00024D7C File Offset: 0x00022F7C
		public virtual ParameterCollection SelectNewParameters
		{
			get
			{
				if (this._selectNewParameters == null)
				{
					this._selectNewParameters = new ParameterCollection();
					this._selectNewParameters.ParametersChanged += this.OnQueryParametersChanged;
					if (this._isTracking)
					{
						DataSourceHelper.TrackViewState(this._selectNewParameters);
					}
				}
				return this._selectNewParameters;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00024DCC File Offset: 0x00022FCC
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x00024DDD File Offset: 0x00022FDD
		public virtual string SelectNew
		{
			get
			{
				return this._selectNew ?? string.Empty;
			}
			set
			{
				if (this._selectNew != value)
				{
					this._selectNew = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00024E00 File Offset: 0x00023000
		public virtual ParameterCollection WhereParameters
		{
			get
			{
				if (this._whereParameters == null)
				{
					this._whereParameters = new ParameterCollection();
					this._whereParameters.ParametersChanged += this.OnQueryParametersChanged;
					if (this._isTracking)
					{
						DataSourceHelper.TrackViewState(this._whereParameters);
					}
				}
				return this._whereParameters;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00024E50 File Offset: 0x00023050
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x00024E61 File Offset: 0x00023061
		public virtual string Where
		{
			get
			{
				return this._where ?? string.Empty;
			}
			set
			{
				if (this._where != value)
				{
					this._where = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00024E83 File Offset: 0x00023083
		public virtual ParameterCollection UpdateParameters
		{
			get
			{
				if (this._updateParameters == null)
				{
					this._updateParameters = new ParameterCollection();
				}
				return this._updateParameters;
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00024E9E File Offset: 0x0002309E
		protected void OnQueryParametersChanged(object sender, EventArgs e)
		{
			this.RaiseViewChanged();
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00024EA6 File Offset: 0x000230A6
		public void RaiseViewChanged()
		{
			this.OnDataSourceViewChanged(EventArgs.Empty);
		}

		// Token: 0x060009AF RID: 2479
		protected abstract object GetSource(QueryContext context);

		// Token: 0x060009B0 RID: 2480 RVA: 0x00024EB4 File Offset: 0x000230B4
		protected QueryContext CreateQueryContext(DataSourceSelectArguments arguments)
		{
			IDictionary<string, object> whereParameters = this.WhereParameters.ToDictionary(this._context, this._owner);
			IOrderedDictionary orderByParameters = this.OrderByParameters.GetValues(this._context, this._owner).ToCaseInsensitiveDictionary();
			IDictionary<string, object> orderGroupsByParameters = this.OrderGroupsByParameters.ToDictionary(this._context, this._owner);
			IDictionary<string, object> selectParameters = this.SelectNewParameters.ToDictionary(this._context, this._owner);
			IDictionary<string, object> groupByParameters = this.GroupByParameters.ToDictionary(this._context, this._owner);
			return new QueryContext(whereParameters, orderGroupsByParameters, orderByParameters, groupByParameters, selectParameters, arguments);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00024F4C File Offset: 0x0002314C
		protected virtual IQueryable BuildQuery(DataSourceSelectArguments arguments)
		{
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			QueryContext context = this.CreateQueryContext(arguments);
			this._originalValues = null;
			object source = this.GetSource(context);
			if (source != null)
			{
				IQueryable source2 = QueryableDataSourceHelper.AsQueryable(source);
				return this.ExecuteQuery(source2, context);
			}
			return null;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00024F92 File Offset: 0x00023192
		protected virtual IQueryable ExecuteQuery(IQueryable source, QueryContext context)
		{
			source = this.ExecuteQueryExpressions(source, context);
			source = this.ExecuteSorting(source, context);
			source = this.ExecutePaging(source, context);
			return source;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00024FB4 File Offset: 0x000231B4
		protected IQueryable ExecuteQueryExpressions(IQueryable source, QueryContext context)
		{
			if (source != null)
			{
				QueryCreatedEventArgs queryCreatedEventArgs = new QueryCreatedEventArgs(source);
				this.OnQueryCreated(queryCreatedEventArgs);
				source = (queryCreatedEventArgs.Query ?? source);
				if (this.AutoGenerateWhereClause)
				{
					if (!string.IsNullOrEmpty(this.Where))
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_WhereAlreadySpecified, new object[]
						{
							this._owner.ID
						}));
					}
					source = QueryableDataSourceHelper.CreateWhereExpression(context.WhereParameters, source, this._queryable);
				}
				else if (!string.IsNullOrEmpty(this.Where))
				{
					source = this._queryable.Where(source, this.Where, new object[]
					{
						context.WhereParameters.ToEscapedParameterKeys(this._owner)
					});
				}
				if (this.AutoGenerateOrderByClause)
				{
					if (!string.IsNullOrEmpty(this.OrderBy))
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_OrderByAlreadySpecified, new object[]
						{
							this._owner.ID
						}));
					}
					source = QueryableDataSourceHelper.CreateOrderByExpression(context.OrderByParameters, source, this._queryable);
				}
				else if (!string.IsNullOrEmpty(this.OrderBy))
				{
					source = this._queryable.OrderBy(source, this.OrderBy, new object[]
					{
						context.OrderByParameters.ToEscapedParameterKeys(this._owner)
					});
				}
				string groupBy = this.GroupBy;
				if (string.IsNullOrEmpty(groupBy))
				{
					if (!string.IsNullOrEmpty(this.OrderGroupsBy))
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_OrderGroupsByRequiresGroupBy, new object[]
						{
							this._owner.ID
						}));
					}
				}
				else
				{
					source = this._queryable.GroupBy(source, groupBy, "it", new object[]
					{
						context.GroupByParameters.ToEscapedParameterKeys(this._owner)
					});
					if (!string.IsNullOrEmpty(this.OrderGroupsBy))
					{
						source = this._queryable.OrderBy(source, this.OrderGroupsBy, new object[]
						{
							context.OrderGroupsByParameters.ToEscapedParameterKeys(this._owner)
						});
					}
				}
				if (!string.IsNullOrEmpty(this.SelectNew))
				{
					source = this._queryable.Select(source, this.SelectNew, new object[]
					{
						context.SelectParameters.ToEscapedParameterKeys(this._owner)
					});
				}
				return source;
			}
			return source;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000251F0 File Offset: 0x000233F0
		protected IQueryable ExecuteSorting(IQueryable source, QueryContext context)
		{
			string sortExpression = context.Arguments.SortExpression;
			if (this.CanSort && this.AutoSort && !string.IsNullOrEmpty(sortExpression))
			{
				source = this._queryable.OrderBy(source, sortExpression, new object[0]);
			}
			return source;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00025238 File Offset: 0x00023438
		protected IQueryable ExecutePaging(IQueryable source, QueryContext context)
		{
			if (this.CanPage && this.AutoPage)
			{
				if (this.CanRetrieveTotalRowCount && context.Arguments.RetrieveTotalRowCount)
				{
					context.Arguments.TotalRowCount = this._queryable.Count(source);
				}
				if (context.Arguments.MaximumRows > 0 && context.Arguments.StartRowIndex >= 0)
				{
					source = this._queryable.Skip(source, context.Arguments.StartRowIndex);
					source = this._queryable.Take(source, context.Arguments.MaximumRows);
				}
			}
			else if (context.Arguments.RetrieveTotalRowCount && context.Arguments.TotalRowCount == -1)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_PagingNotHandled, new object[]
				{
					this._owner.ID
				}));
			}
			return source;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002531C File Offset: 0x0002351C
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				if (array[0] != null)
				{
					((IStateManager)this.WhereParameters).LoadViewState(array[0]);
				}
				if (array[1] != null)
				{
					((IStateManager)this.OrderByParameters).LoadViewState(array[1]);
				}
				if (array[2] != null)
				{
					((IStateManager)this.GroupByParameters).LoadViewState(array[2]);
				}
				if (array[3] != null)
				{
					((IStateManager)this.OrderGroupsByParameters).LoadViewState(array[3]);
				}
				if (array[4] != null)
				{
					((IStateManager)this.SelectNewParameters).LoadViewState(array[4]);
				}
				if (array[5] != null)
				{
					this._originalValues = (Hashtable)array[5];
				}
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x000253A8 File Offset: 0x000235A8
		protected virtual object SaveViewState()
		{
			object[] array = new object[6];
			array[0] = DataSourceHelper.SaveViewState(this._whereParameters);
			array[1] = DataSourceHelper.SaveViewState(this._orderByParameters);
			array[2] = DataSourceHelper.SaveViewState(this._groupByParameters);
			array[3] = DataSourceHelper.SaveViewState(this._orderGroupsByParameters);
			array[4] = DataSourceHelper.SaveViewState(this._selectNewParameters);
			if (this._originalValues != null && this._originalValues.Count > 0)
			{
				array[5] = this._originalValues;
			}
			return array;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00025422 File Offset: 0x00023622
		protected virtual void TrackViewState()
		{
			this._isTracking = true;
			DataSourceHelper.TrackViewState(this._whereParameters);
			DataSourceHelper.TrackViewState(this._orderByParameters);
			DataSourceHelper.TrackViewState(this._groupByParameters);
			DataSourceHelper.TrackViewState(this._orderGroupsByParameters);
			DataSourceHelper.TrackViewState(this._selectNewParameters);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00025464 File Offset: 0x00023664
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			this.ClearOriginalValues();
			IQueryable queryable = this.BuildQuery(arguments);
			IList list = queryable.ToList(queryable.ElementType);
			this.StoreOriginalValues(list);
			return list;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00025494 File Offset: 0x00023694
		protected void ClearOriginalValues()
		{
			this._originalValues = null;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x000254A0 File Offset: 0x000236A0
		protected virtual IDictionary GetOriginalValues(IDictionary keys)
		{
			if (this._originalValues == null)
			{
				return null;
			}
			List<bool> list = new List<bool>();
			foreach (object obj in keys)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string key = (string)dictionaryEntry.Key;
				if (this._originalValues.ContainsKey(key))
				{
					object value = dictionaryEntry.Value;
					ArrayList arrayList = (ArrayList)this._originalValues[key];
					for (int i = 0; i < arrayList.Count; i++)
					{
						if (list.Count <= i)
						{
							list.Add(this.OriginalValueMatches(arrayList[i], value));
						}
						else if (list[i])
						{
							list[i] = this.OriginalValueMatches(arrayList[i], value);
						}
					}
				}
			}
			int num = list.IndexOf(true);
			if (num < 0 || list.IndexOf(true, num + 1) >= 0)
			{
				throw new InvalidOperationException(AtlasWeb.LinqDataSourceView_OriginalValuesNotFound);
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>(this._originalValues.Count, StringComparer.OrdinalIgnoreCase);
			foreach (object obj2 in this._originalValues)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				ArrayList arrayList2 = (ArrayList)dictionaryEntry2.Value;
				dictionary.Add((string)dictionaryEntry2.Key, arrayList2[num]);
			}
			return dictionary;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x000032F4 File Offset: 0x000014F4
		protected virtual void StoreOriginalValues(IList results)
		{
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002564C File Offset: 0x0002384C
		protected void StoreOriginalValues(IList results, Func<PropertyDescriptor, bool> include)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.EntityType);
			int count = results.Count;
			int count2 = properties.Count;
			this._originalValues = new Hashtable(count2, StringComparer.OrdinalIgnoreCase);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (include(propertyDescriptor) && propertyDescriptor.PropertyType.IsSerializable)
				{
					ArrayList arrayList = new ArrayList(count);
					this._originalValues[propertyDescriptor.Name] = arrayList;
					foreach (object component in results)
					{
						arrayList.Add(propertyDescriptor.GetValue(component));
					}
				}
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00025754 File Offset: 0x00023954
		public int Update(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			return this.ExecuteUpdate(keys, values, oldValues);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002575F File Offset: 0x0002395F
		public int Delete(IDictionary keys, IDictionary oldValues)
		{
			return this.ExecuteDelete(keys, oldValues);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00025769 File Offset: 0x00023969
		public int Insert(IDictionary values)
		{
			return this.ExecuteInsert(values);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00025774 File Offset: 0x00023974
		protected QueryableDataSourceEditData BuildDeleteObject(IDictionary keys, IDictionary oldValues, IDictionary<string, Exception> validationErrors)
		{
			QueryableDataSourceEditData queryableDataSourceEditData = new QueryableDataSourceEditData();
			Type entityType = this.EntityType;
			IDictionary dictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
			IDictionary originalValues = this.GetOriginalValues(keys);
			ParameterCollection deleteParameters = this.DeleteParameters;
			if (!DataSourceHelper.MergeDictionaries(entityType, deleteParameters, keys, dictionary, validationErrors))
			{
				return queryableDataSourceEditData;
			}
			if (!DataSourceHelper.MergeDictionaries(entityType, deleteParameters, oldValues, dictionary, validationErrors))
			{
				return queryableDataSourceEditData;
			}
			if (originalValues != null && !DataSourceHelper.MergeDictionaries(entityType, deleteParameters, originalValues, dictionary, validationErrors))
			{
				return queryableDataSourceEditData;
			}
			queryableDataSourceEditData.OriginalDataObject = DataSourceHelper.BuildDataObject(entityType, dictionary, validationErrors);
			return queryableDataSourceEditData;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x000257E8 File Offset: 0x000239E8
		protected QueryableDataSourceEditData BuildInsertObject(IDictionary values, IDictionary<string, Exception> validationErrors)
		{
			QueryableDataSourceEditData queryableDataSourceEditData = new QueryableDataSourceEditData();
			Type entityType = this.EntityType;
			IDictionary dictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
			if (!DataSourceHelper.MergeDictionaries(entityType, this.InsertParameters, this.InsertParameters.GetValues(this._context, this._owner), dictionary, validationErrors))
			{
				return queryableDataSourceEditData;
			}
			if (!DataSourceHelper.MergeDictionaries(entityType, this.InsertParameters, values, dictionary, validationErrors))
			{
				return queryableDataSourceEditData;
			}
			queryableDataSourceEditData.NewDataObject = DataSourceHelper.BuildDataObject(entityType, dictionary, validationErrors);
			return queryableDataSourceEditData;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00025858 File Offset: 0x00023A58
		protected QueryableDataSourceEditData BuildUpdateObjects(IDictionary keys, IDictionary values, IDictionary oldValues, IDictionary<string, Exception> validationErrors)
		{
			QueryableDataSourceEditData queryableDataSourceEditData = new QueryableDataSourceEditData();
			Type entityType = this.EntityType;
			IDictionary dictionary = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
			IDictionary dictionary2 = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
			IDictionary originalValues = this.GetOriginalValues(keys);
			ParameterCollection updateParameters = this.UpdateParameters;
			if (!DataSourceHelper.MergeDictionaries(entityType, updateParameters, oldValues, dictionary2, dictionary, validationErrors))
			{
				return queryableDataSourceEditData;
			}
			if (!DataSourceHelper.MergeDictionaries(entityType, updateParameters, keys, dictionary2, dictionary, validationErrors))
			{
				return queryableDataSourceEditData;
			}
			if (originalValues != null && !DataSourceHelper.MergeDictionaries(entityType, updateParameters, originalValues, dictionary2, dictionary, validationErrors))
			{
				return queryableDataSourceEditData;
			}
			if (!DataSourceHelper.MergeDictionaries(entityType, updateParameters, values, dictionary, validationErrors))
			{
				return queryableDataSourceEditData;
			}
			queryableDataSourceEditData.NewDataObject = DataSourceHelper.BuildDataObject(entityType, dictionary, validationErrors);
			if (queryableDataSourceEditData.NewDataObject != null)
			{
				queryableDataSourceEditData.OriginalDataObject = DataSourceHelper.BuildDataObject(entityType, dictionary2, validationErrors);
			}
			return queryableDataSourceEditData;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0001359B File Offset: 0x0001179B
		protected virtual int DeleteObject(object oldEntity)
		{
			return 0;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0001359B File Offset: 0x0001179B
		protected virtual int UpdateObject(object oldEntity, object newEntity)
		{
			return 0;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0001359B File Offset: 0x0001179B
		protected virtual int InsertObject(object newEntity)
		{
			return 0;
		}

		// Token: 0x060009C7 RID: 2503
		protected abstract void HandleValidationErrors(IDictionary<string, Exception> errors, DataSourceOperation operation);

		// Token: 0x060009C8 RID: 2504 RVA: 0x00025908 File Offset: 0x00023B08
		protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
		{
			IDictionary<string, Exception> dictionary = new Dictionary<string, Exception>(StringComparer.OrdinalIgnoreCase);
			QueryableDataSourceEditData queryableDataSourceEditData = this.BuildDeleteObject(keys, oldValues, dictionary);
			if (dictionary.Any<KeyValuePair<string, Exception>>())
			{
				this.HandleValidationErrors(dictionary, DataSourceOperation.Delete);
				return -1;
			}
			return this.DeleteObject(queryableDataSourceEditData.OriginalDataObject);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002594C File Offset: 0x00023B4C
		protected override int ExecuteInsert(IDictionary values)
		{
			IDictionary<string, Exception> dictionary = new Dictionary<string, Exception>(StringComparer.OrdinalIgnoreCase);
			QueryableDataSourceEditData queryableDataSourceEditData = this.BuildInsertObject(values, dictionary);
			if (dictionary.Any<KeyValuePair<string, Exception>>())
			{
				this.HandleValidationErrors(dictionary, DataSourceOperation.Insert);
				return -1;
			}
			return this.InsertObject(queryableDataSourceEditData.NewDataObject);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00025990 File Offset: 0x00023B90
		protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			IDictionary<string, Exception> dictionary = new Dictionary<string, Exception>(StringComparer.OrdinalIgnoreCase);
			QueryableDataSourceEditData queryableDataSourceEditData = this.BuildUpdateObjects(keys, values, oldValues, dictionary);
			if (dictionary.Any<KeyValuePair<string, Exception>>())
			{
				this.HandleValidationErrors(dictionary, DataSourceOperation.Update);
				return -1;
			}
			return this.UpdateObject(queryableDataSourceEditData.OriginalDataObject, queryableDataSourceEditData.NewDataObject);
		}

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x060009CB RID: 2507 RVA: 0x000259D9 File Offset: 0x00023BD9
		// (remove) Token: 0x060009CC RID: 2508 RVA: 0x000259EC File Offset: 0x00023BEC
		public event EventHandler<QueryCreatedEventArgs> QueryCreated
		{
			add
			{
				base.Events.AddHandler(QueryableDataSourceView.EventQueryCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(QueryableDataSourceView.EventQueryCreated, value);
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00025A00 File Offset: 0x00023C00
		protected virtual void OnQueryCreated(QueryCreatedEventArgs e)
		{
			EventHandler<QueryCreatedEventArgs> eventHandler = (EventHandler<QueryCreatedEventArgs>)base.Events[QueryableDataSourceView.EventQueryCreated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00025A30 File Offset: 0x00023C30
		private bool OriginalValueMatches(object originalValue, object value)
		{
			IEnumerable enumerable = originalValue as IEnumerable;
			IEnumerable enumerable2 = value as IEnumerable;
			if (enumerable != null && enumerable2 != null)
			{
				return QueryableDataSourceHelper.EnumerableContentEquals(enumerable, enumerable2);
			}
			return originalValue.Equals(value);
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00025A60 File Offset: 0x00023C60
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00025A68 File Offset: 0x00023C68
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00025A71 File Offset: 0x00023C71
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00025A79 File Offset: 0x00023C79
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x04000319 RID: 793
		private ParameterCollection _whereParameters;

		// Token: 0x0400031A RID: 794
		private ParameterCollection _orderByParameters;

		// Token: 0x0400031B RID: 795
		private ParameterCollection _orderGroupsByParameters;

		// Token: 0x0400031C RID: 796
		private ParameterCollection _selectNewParameters;

		// Token: 0x0400031D RID: 797
		private ParameterCollection _groupByParameters;

		// Token: 0x0400031E RID: 798
		private ParameterCollection _deleteParameters;

		// Token: 0x0400031F RID: 799
		private ParameterCollection _updateParameters;

		// Token: 0x04000320 RID: 800
		private ParameterCollection _insertParameters;

		// Token: 0x04000321 RID: 801
		private HttpContext _context;

		// Token: 0x04000322 RID: 802
		private DataSourceControl _owner;

		// Token: 0x04000323 RID: 803
		private IDynamicQueryable _queryable;

		// Token: 0x04000324 RID: 804
		private string _groupBy;

		// Token: 0x04000325 RID: 805
		private string _orderBy;

		// Token: 0x04000326 RID: 806
		private string _orderGroupsBy;

		// Token: 0x04000327 RID: 807
		private string _selectNew;

		// Token: 0x04000328 RID: 808
		private string _where;

		// Token: 0x04000329 RID: 809
		private bool _autoGenerateOrderByClause;

		// Token: 0x0400032A RID: 810
		private bool _autoGenerateWhereClause;

		// Token: 0x0400032B RID: 811
		private bool _autoPage = true;

		// Token: 0x0400032C RID: 812
		private bool _autoSort = true;

		// Token: 0x0400032D RID: 813
		private bool _isTracking;

		// Token: 0x0400032E RID: 814
		protected static readonly object EventSelected = new object();

		// Token: 0x0400032F RID: 815
		protected static readonly object EventSelecting = new object();

		// Token: 0x04000330 RID: 816
		private static readonly object EventQueryCreated = new object();

		// Token: 0x04000331 RID: 817
		private Hashtable _originalValues;
	}
}
