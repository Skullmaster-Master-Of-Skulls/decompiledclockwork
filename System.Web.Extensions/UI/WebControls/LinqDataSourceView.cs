using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Web.Compilation;
using System.Web.DynamicData;
using System.Web.Resources;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000A6 RID: 166
	public class LinqDataSourceView : ContextDataSourceView
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x0001D174 File Offset: 0x0001B374
		public LinqDataSourceView(LinqDataSource owner, string name, HttpContext context) : this(owner, name, context, new DynamicQueryableWrapper(), new LinqToSqlWrapper())
		{
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001D189 File Offset: 0x0001B389
		internal LinqDataSourceView(LinqDataSource owner, string name, HttpContext context, IDynamicQueryable dynamicQueryable, ILinqToSql linqToSql) : base(owner, name, context, dynamicQueryable)
		{
			this._context = context;
			this._owner = owner;
			this._linqToSql = linqToSql;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0001D1BA File Offset: 0x0001B3BA
		public override bool CanDelete
		{
			get
			{
				return this.EnableDelete;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001D1C2 File Offset: 0x0001B3C2
		public override bool CanInsert
		{
			get
			{
				return this.EnableInsert;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0001D1CA File Offset: 0x0001B3CA
		public override bool CanPage
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001D1CA File Offset: 0x0001B3CA
		public override bool CanRetrieveTotalRowCount
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001D1CA File Offset: 0x0001B3CA
		public override bool CanSort
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001D1CD File Offset: 0x0001B3CD
		public override bool CanUpdate
		{
			get
			{
				return this.EnableUpdate;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001D1D8 File Offset: 0x0001B3D8
		public override Type ContextType
		{
			[SecuritySafeCritical]
			get
			{
				if (this._contextType == null)
				{
					string contextTypeName = this.ContextTypeName;
					if (string.IsNullOrEmpty(contextTypeName))
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_ContextTypeNameNotSpecified, new object[]
						{
							this._owner.ID
						}));
					}
					try
					{
						this._contextType = BuildManager.GetType(contextTypeName, true, true);
					}
					catch (Exception innerException)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_ContextTypeNameNotFound, new object[]
						{
							this._owner.ID
						}), innerException);
					}
				}
				return this._contextType;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001D280 File Offset: 0x0001B480
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x0001D294 File Offset: 0x0001B494
		public override string ContextTypeName
		{
			get
			{
				return this._contextTypeName ?? string.Empty;
			}
			set
			{
				if (this._contextTypeName != value)
				{
					if (this._reuseSelectContext)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_ContextTypeNameChanged, new object[]
						{
							this._owner.ID
						}));
					}
					this._contextTypeName = value;
					this._contextType = null;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001D2F9 File Offset: 0x0001B4F9
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x0001D301 File Offset: 0x0001B501
		public bool EnableDelete
		{
			get
			{
				return this._enableDelete;
			}
			set
			{
				if (this._enableDelete != value)
				{
					this._enableDelete = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001D31E File Offset: 0x0001B51E
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0001D326 File Offset: 0x0001B526
		public bool EnableInsert
		{
			get
			{
				return this._enableInsert;
			}
			set
			{
				if (this._enableInsert != value)
				{
					this._enableInsert = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001D343 File Offset: 0x0001B543
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x0001D34C File Offset: 0x0001B54C
		public bool EnableObjectTracking
		{
			get
			{
				return this._enableObjectTracking;
			}
			set
			{
				if (this._enableObjectTracking != value)
				{
					if (this._reuseSelectContext)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_EnableObjectTrackingChanged, new object[]
						{
							this._owner.ID
						}));
					}
					this._enableObjectTracking = value;
				}
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001D39A File Offset: 0x0001B59A
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0001D3A2 File Offset: 0x0001B5A2
		public bool EnableUpdate
		{
			get
			{
				return this._enableUpdate;
			}
			set
			{
				if (this._enableUpdate != value)
				{
					this._enableUpdate = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001D3BF File Offset: 0x0001B5BF
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x0001D3C7 File Offset: 0x0001B5C7
		public bool StoreOriginalValuesInViewState
		{
			get
			{
				return this._storeOriginalValuesInViewState;
			}
			set
			{
				if (this._storeOriginalValuesInViewState != value)
				{
					this._storeOriginalValuesInViewState = value;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001D3E4 File Offset: 0x0001B5E4
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x0001D3EC File Offset: 0x0001B5EC
		public string TableName
		{
			get
			{
				return base.EntitySetName;
			}
			set
			{
				if (base.EntitySetName != value)
				{
					if (this._reuseSelectContext)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_TableNameChanged, new object[]
						{
							this._owner.ID
						}));
					}
					base.EntitySetName = value;
				}
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000756 RID: 1878 RVA: 0x0001D43F File Offset: 0x0001B63F
		// (remove) Token: 0x06000757 RID: 1879 RVA: 0x0001D452 File Offset: 0x0001B652
		public event EventHandler<LinqDataSourceStatusEventArgs> ContextCreated
		{
			add
			{
				base.Events.AddHandler(ContextDataSourceView.EventContextCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(ContextDataSourceView.EventContextCreated, value);
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000758 RID: 1880 RVA: 0x0001D465 File Offset: 0x0001B665
		// (remove) Token: 0x06000759 RID: 1881 RVA: 0x0001D478 File Offset: 0x0001B678
		public event EventHandler<LinqDataSourceContextEventArgs> ContextCreating
		{
			add
			{
				base.Events.AddHandler(ContextDataSourceView.EventContextCreating, value);
			}
			remove
			{
				base.Events.RemoveHandler(ContextDataSourceView.EventContextCreating, value);
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600075A RID: 1882 RVA: 0x0001D48B File Offset: 0x0001B68B
		// (remove) Token: 0x0600075B RID: 1883 RVA: 0x0001D49E File Offset: 0x0001B69E
		public event EventHandler<LinqDataSourceDisposeEventArgs> ContextDisposing
		{
			add
			{
				base.Events.AddHandler(ContextDataSourceView.EventContextDisposing, value);
			}
			remove
			{
				base.Events.RemoveHandler(ContextDataSourceView.EventContextDisposing, value);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600075C RID: 1884 RVA: 0x0001D4B1 File Offset: 0x0001B6B1
		// (remove) Token: 0x0600075D RID: 1885 RVA: 0x0001D4C4 File Offset: 0x0001B6C4
		public event EventHandler<LinqDataSourceStatusEventArgs> Deleted
		{
			add
			{
				base.Events.AddHandler(LinqDataSourceView.EventDeleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinqDataSourceView.EventDeleted, value);
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x0600075E RID: 1886 RVA: 0x0001D4D7 File Offset: 0x0001B6D7
		// (remove) Token: 0x0600075F RID: 1887 RVA: 0x0001D4EA File Offset: 0x0001B6EA
		public event EventHandler<LinqDataSourceDeleteEventArgs> Deleting
		{
			add
			{
				base.Events.AddHandler(LinqDataSourceView.EventDeleting, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinqDataSourceView.EventDeleting, value);
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000760 RID: 1888 RVA: 0x0001D4FD File Offset: 0x0001B6FD
		// (remove) Token: 0x06000761 RID: 1889 RVA: 0x0001D510 File Offset: 0x0001B710
		internal event EventHandler<DynamicValidatorEventArgs> Exception
		{
			add
			{
				base.Events.AddHandler(LinqDataSourceView.EventException, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinqDataSourceView.EventException, value);
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000762 RID: 1890 RVA: 0x0001D523 File Offset: 0x0001B723
		// (remove) Token: 0x06000763 RID: 1891 RVA: 0x0001D536 File Offset: 0x0001B736
		public event EventHandler<LinqDataSourceStatusEventArgs> Inserted
		{
			add
			{
				base.Events.AddHandler(LinqDataSourceView.EventInserted, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinqDataSourceView.EventInserted, value);
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000764 RID: 1892 RVA: 0x0001D549 File Offset: 0x0001B749
		// (remove) Token: 0x06000765 RID: 1893 RVA: 0x0001D55C File Offset: 0x0001B75C
		public event EventHandler<LinqDataSourceInsertEventArgs> Inserting
		{
			add
			{
				base.Events.AddHandler(LinqDataSourceView.EventInserting, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinqDataSourceView.EventInserting, value);
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000766 RID: 1894 RVA: 0x0001D56F File Offset: 0x0001B76F
		// (remove) Token: 0x06000767 RID: 1895 RVA: 0x0001D582 File Offset: 0x0001B782
		public event EventHandler<LinqDataSourceStatusEventArgs> Selected
		{
			add
			{
				base.Events.AddHandler(QueryableDataSourceView.EventSelected, value);
			}
			remove
			{
				base.Events.RemoveHandler(QueryableDataSourceView.EventSelected, value);
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000768 RID: 1896 RVA: 0x0001D595 File Offset: 0x0001B795
		// (remove) Token: 0x06000769 RID: 1897 RVA: 0x0001D5A8 File Offset: 0x0001B7A8
		public event EventHandler<LinqDataSourceSelectEventArgs> Selecting
		{
			add
			{
				base.Events.AddHandler(QueryableDataSourceView.EventSelecting, value);
			}
			remove
			{
				base.Events.RemoveHandler(QueryableDataSourceView.EventSelecting, value);
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600076A RID: 1898 RVA: 0x0001D5BB File Offset: 0x0001B7BB
		// (remove) Token: 0x0600076B RID: 1899 RVA: 0x0001D5CE File Offset: 0x0001B7CE
		public event EventHandler<LinqDataSourceStatusEventArgs> Updated
		{
			add
			{
				base.Events.AddHandler(LinqDataSourceView.EventUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinqDataSourceView.EventUpdated, value);
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600076C RID: 1900 RVA: 0x0001D5E1 File Offset: 0x0001B7E1
		// (remove) Token: 0x0600076D RID: 1901 RVA: 0x0001D5F4 File Offset: 0x0001B7F4
		public event EventHandler<LinqDataSourceUpdateEventArgs> Updating
		{
			add
			{
				base.Events.AddHandler(LinqDataSourceView.EventUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinqDataSourceView.EventUpdating, value);
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001D607 File Offset: 0x0001B807
		protected virtual object CreateContext(Type contextType)
		{
			return DataSourceHelper.CreateObjectInstance(contextType);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001D60F File Offset: 0x0001B80F
		protected override ContextDataSourceContextData CreateContext(DataSourceOperation operation)
		{
			if (operation == DataSourceOperation.Select)
			{
				return this.CreateContextAndTableForSelect();
			}
			return this.CreateContextAndTableForEdit(operation);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001D624 File Offset: 0x0001B824
		private ContextDataSourceContextData CreateContextAndTable(DataSourceOperation operation)
		{
			ContextDataSourceContextData contextDataSourceContextData = null;
			bool flag = false;
			try
			{
				LinqDataSourceContextEventArgs linqDataSourceContextEventArgs = new LinqDataSourceContextEventArgs(operation);
				this.OnContextCreating(linqDataSourceContextEventArgs);
				contextDataSourceContextData = new ContextDataSourceContextData(linqDataSourceContextEventArgs.ObjectInstance);
				Type type = null;
				MemberInfo tableMemberInfo;
				if (contextDataSourceContextData.Context == null)
				{
					type = this.ContextType;
					tableMemberInfo = this.GetTableMemberInfo(type);
					if (tableMemberInfo != null)
					{
						if (LinqDataSourceView.MemberIsStatic(tableMemberInfo))
						{
							if (operation != DataSourceOperation.Select)
							{
								throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_TableCannotBeStatic, new object[]
								{
									this.TableName,
									type.Name,
									this._owner.ID
								}));
							}
						}
						else
						{
							contextDataSourceContextData.Context = this.CreateContext(type);
							this._isNewContext = true;
						}
					}
				}
				else
				{
					tableMemberInfo = this.GetTableMemberInfo(contextDataSourceContextData.Context.GetType());
				}
				if (tableMemberInfo != null)
				{
					FieldInfo fieldInfo = tableMemberInfo as FieldInfo;
					if (fieldInfo != null)
					{
						contextDataSourceContextData.EntitySet = fieldInfo.GetValue(contextDataSourceContextData.Context);
					}
					PropertyInfo propertyInfo = tableMemberInfo as PropertyInfo;
					if (propertyInfo != null)
					{
						contextDataSourceContextData.EntitySet = propertyInfo.GetValue(contextDataSourceContextData.Context, null);
					}
				}
				if (contextDataSourceContextData.EntitySet == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_TableNameNotFound, new object[]
					{
						this.TableName,
						type.Name,
						this._owner.ID
					}));
				}
			}
			catch (Exception exception)
			{
				flag = true;
				LinqDataSourceStatusEventArgs linqDataSourceStatusEventArgs = new LinqDataSourceStatusEventArgs(exception);
				this.OnContextCreated(linqDataSourceStatusEventArgs);
				this.OnException(new DynamicValidatorEventArgs(exception, DynamicDataSourceOperation.ContextCreate));
				if (!linqDataSourceStatusEventArgs.ExceptionHandled)
				{
					throw;
				}
			}
			finally
			{
				if (!flag)
				{
					object result = (contextDataSourceContextData == null) ? null : contextDataSourceContextData.Context;
					LinqDataSourceStatusEventArgs e = new LinqDataSourceStatusEventArgs(result);
					this.OnContextCreated(e);
				}
			}
			return contextDataSourceContextData;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001D80C File Offset: 0x0001BA0C
		private ContextDataSourceContextData CreateContextAndTableForEdit(DataSourceOperation operation)
		{
			ContextDataSourceContextData contextDataSourceContextData = this.CreateContextAndTable(operation);
			if (contextDataSourceContextData != null)
			{
				if (contextDataSourceContextData.Context == null)
				{
					return null;
				}
				if (contextDataSourceContextData.EntitySet == null)
				{
					this.DisposeContext(contextDataSourceContextData.Context);
					return null;
				}
				this.ValidateContextType(contextDataSourceContextData.Context.GetType(), false);
				this.ValidateTableType(contextDataSourceContextData.EntitySet.GetType(), false);
			}
			return contextDataSourceContextData;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001D86C File Offset: 0x0001BA6C
		private ContextDataSourceContextData CreateContextAndTableForSelect()
		{
			this._isNewContext = false;
			if (this._selectContexts == null)
			{
				this._selectContexts = new List<ContextDataSourceContextData>();
			}
			else if (this._reuseSelectContext && this._selectContexts.Count > 0)
			{
				return this._selectContexts[this._selectContexts.Count - 1];
			}
			ContextDataSourceContextData contextDataSourceContextData = this.CreateContextAndTable(DataSourceOperation.Select);
			if (contextDataSourceContextData != null)
			{
				if (contextDataSourceContextData.Context != null)
				{
					this.ValidateContextType(contextDataSourceContextData.Context.GetType(), true);
				}
				if (contextDataSourceContextData.EntitySet != null)
				{
					this.ValidateTableType(contextDataSourceContextData.EntitySet.GetType(), true);
				}
				this._selectContexts.Add(contextDataSourceContextData);
				DataContext dataContext = contextDataSourceContextData.Context as DataContext;
				if (dataContext != null && this._isNewContext)
				{
					dataContext.ObjectTrackingEnabled = this.EnableObjectTracking;
				}
				this._reuseSelectContext = (dataContext == null || !this.EnableObjectTracking);
			}
			return contextDataSourceContextData;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001D948 File Offset: 0x0001BB48
		protected virtual void DeleteDataObject(object dataContext, object table, object oldDataObject)
		{
			this._linqToSql.Attach((ITable)table, oldDataObject);
			this._linqToSql.Remove((ITable)table, oldDataObject);
			this._linqToSql.SubmitChanges((DataContext)dataContext);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001D980 File Offset: 0x0001BB80
		protected override int DeleteObject(object oldEntity)
		{
			LinqDataSourceDeleteEventArgs linqDataSourceDeleteEventArgs = new LinqDataSourceDeleteEventArgs(oldEntity);
			this.OnDeleting(linqDataSourceDeleteEventArgs);
			if (linqDataSourceDeleteEventArgs.Cancel)
			{
				return -1;
			}
			LinqDataSourceStatusEventArgs linqDataSourceStatusEventArgs;
			try
			{
				this.DeleteDataObject(base.Context, base.EntitySet, linqDataSourceDeleteEventArgs.OriginalObject);
			}
			catch (Exception exception)
			{
				linqDataSourceStatusEventArgs = new LinqDataSourceStatusEventArgs(exception);
				this.OnDeleted(linqDataSourceStatusEventArgs);
				this.OnException(new DynamicValidatorEventArgs(exception, DynamicDataSourceOperation.Delete));
				if (linqDataSourceStatusEventArgs.ExceptionHandled)
				{
					return -1;
				}
				throw;
			}
			linqDataSourceStatusEventArgs = new LinqDataSourceStatusEventArgs(linqDataSourceDeleteEventArgs.OriginalObject);
			this.OnDeleted(linqDataSourceStatusEventArgs);
			return 1;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001DA14 File Offset: 0x0001BC14
		protected override void DisposeContext(object dataContext)
		{
			if (dataContext != null)
			{
				LinqDataSourceDisposeEventArgs linqDataSourceDisposeEventArgs = new LinqDataSourceDisposeEventArgs(dataContext);
				this.OnContextDisposing(linqDataSourceDisposeEventArgs);
				if (!linqDataSourceDisposeEventArgs.Cancel)
				{
					base.DisposeContext(dataContext);
				}
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001DA41 File Offset: 0x0001BC41
		protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
		{
			this.ValidateDeleteSupported(keys, oldValues);
			return base.ExecuteDelete(keys, oldValues);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001DA53 File Offset: 0x0001BC53
		protected override int ExecuteInsert(IDictionary values)
		{
			this.ValidateInsertSupported(values);
			return base.ExecuteInsert(values);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001DA63 File Offset: 0x0001BC63
		protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			this.ValidateUpdateSupported(keys, values, oldValues);
			return base.ExecuteUpdate(keys, values, oldValues);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001DA78 File Offset: 0x0001BC78
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			base.ClearOriginalValues();
			QueryContext context = base.CreateQueryContext(arguments);
			object source = this.GetSource(context);
			IList list = null;
			if (this._selectResult != null)
			{
				try
				{
					IQueryable queryable = QueryableDataSourceHelper.AsQueryable(this._selectResult);
					queryable = this.ExecuteQuery(queryable, context);
					Type dataObjectType = this.GetDataObjectType(queryable.GetType());
					list = queryable.ToList(dataObjectType);
					if (this._storeOriginalValues)
					{
						ITable table = source as ITable;
						if (table != null && dataObjectType.IsAssignableFrom(this.EntityType))
						{
							this.StoreOriginalValues(list);
						}
					}
				}
				catch (Exception exception)
				{
					list = null;
					LinqDataSourceStatusEventArgs linqDataSourceStatusEventArgs = new LinqDataSourceStatusEventArgs(exception);
					this.OnSelected(linqDataSourceStatusEventArgs);
					this.OnException(new DynamicValidatorEventArgs(exception, DynamicDataSourceOperation.Select));
					if (!linqDataSourceStatusEventArgs.ExceptionHandled)
					{
						throw;
					}
				}
				finally
				{
					if (list != null)
					{
						int totalRowCount = -1;
						if (arguments.RetrieveTotalRowCount)
						{
							totalRowCount = arguments.TotalRowCount;
						}
						else if (!this.AutoPage)
						{
							totalRowCount = list.Count;
						}
						LinqDataSourceStatusEventArgs e = new LinqDataSourceStatusEventArgs(list, totalRowCount);
						this.OnSelected(e);
					}
				}
				base.Context = null;
			}
			return list;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001DB90 File Offset: 0x0001BD90
		protected override object GetSource(QueryContext context)
		{
			LinqDataSourceSelectEventArgs linqDataSourceSelectEventArgs = new LinqDataSourceSelectEventArgs(context.Arguments, context.WhereParameters, context.OrderByParameters, context.GroupByParameters, context.OrderGroupsByParameters, context.SelectParameters);
			this.OnSelecting(linqDataSourceSelectEventArgs);
			if (linqDataSourceSelectEventArgs.Cancel)
			{
				return null;
			}
			this._selectResult = linqDataSourceSelectEventArgs.Result;
			object obj = this._selectResult;
			this._storeOriginalValues = (this.StoreOriginalValuesInViewState && (this.CanDelete || this.CanUpdate) && string.IsNullOrEmpty(this.GroupBy) && string.IsNullOrEmpty(this.SelectNew));
			if (this._selectResult == null)
			{
				obj = base.GetSource(context);
				this._selectResult = obj;
			}
			else if (!(obj is ITable) && this._storeOriginalValues)
			{
				obj = base.GetSource(context);
			}
			return obj;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001DC58 File Offset: 0x0001BE58
		protected virtual MemberInfo GetTableMemberInfo(Type contextType)
		{
			string tableName = this.TableName;
			if (string.IsNullOrEmpty(tableName))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_TableNameNotSpecified, new object[]
				{
					this._owner.ID
				}));
			}
			MemberInfo[] array = contextType.FindMembers(MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null);
			for (int i = 0; i < array.Length; i++)
			{
				if (string.Equals(array[i].Name, tableName, StringComparison.OrdinalIgnoreCase))
				{
					return array[i];
				}
			}
			return null;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001DCD0 File Offset: 0x0001BED0
		private ReadOnlyCollection<MetaDataMember> GetTableMetaDataMembers(ITable table, Type dataObjectType)
		{
			DataContext context = table.Context;
			MetaModel mapping = context.Mapping;
			MetaTable table2 = mapping.GetTable(dataObjectType);
			MetaType metaType = table2.Model.GetMetaType(dataObjectType);
			return metaType.DataMembers;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001DD08 File Offset: 0x0001BF08
		protected override void HandleValidationErrors(IDictionary<string, Exception> errors, DataSourceOperation operation)
		{
			LinqDataSourceValidationException ex = new LinqDataSourceValidationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_ValidationFailed, new object[]
			{
				this.EntityType,
				errors.Values.First<Exception>().Message
			}), errors);
			bool flag = false;
			switch (operation)
			{
			case DataSourceOperation.Delete:
			{
				LinqDataSourceDeleteEventArgs linqDataSourceDeleteEventArgs = new LinqDataSourceDeleteEventArgs(ex);
				this.OnDeleting(linqDataSourceDeleteEventArgs);
				this.OnException(new DynamicValidatorEventArgs(ex, DynamicDataSourceOperation.Delete));
				flag = linqDataSourceDeleteEventArgs.ExceptionHandled;
				break;
			}
			case DataSourceOperation.Insert:
			{
				LinqDataSourceInsertEventArgs linqDataSourceInsertEventArgs = new LinqDataSourceInsertEventArgs(ex);
				this.OnInserting(linqDataSourceInsertEventArgs);
				this.OnException(new DynamicValidatorEventArgs(ex, DynamicDataSourceOperation.Insert));
				flag = linqDataSourceInsertEventArgs.ExceptionHandled;
				break;
			}
			case DataSourceOperation.Update:
			{
				LinqDataSourceUpdateEventArgs linqDataSourceUpdateEventArgs = new LinqDataSourceUpdateEventArgs(ex);
				this.OnUpdating(linqDataSourceUpdateEventArgs);
				this.OnException(new DynamicValidatorEventArgs(ex, DynamicDataSourceOperation.Update));
				flag = linqDataSourceUpdateEventArgs.ExceptionHandled;
				break;
			}
			}
			if (!flag)
			{
				throw ex;
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001DDD9 File Offset: 0x0001BFD9
		protected virtual void InsertDataObject(object dataContext, object table, object newDataObject)
		{
			this._linqToSql.Add((ITable)table, newDataObject);
			this._linqToSql.SubmitChanges((DataContext)dataContext);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001DE00 File Offset: 0x0001C000
		protected override int InsertObject(object newEntity)
		{
			LinqDataSourceInsertEventArgs linqDataSourceInsertEventArgs = new LinqDataSourceInsertEventArgs(newEntity);
			this.OnInserting(linqDataSourceInsertEventArgs);
			if (linqDataSourceInsertEventArgs.Cancel)
			{
				return -1;
			}
			LinqDataSourceStatusEventArgs linqDataSourceStatusEventArgs;
			try
			{
				this.InsertDataObject(base.Context, base.EntitySet, linqDataSourceInsertEventArgs.NewObject);
			}
			catch (Exception exception)
			{
				linqDataSourceStatusEventArgs = new LinqDataSourceStatusEventArgs(exception);
				this.OnInserted(linqDataSourceStatusEventArgs);
				this.OnException(new DynamicValidatorEventArgs(exception, DynamicDataSourceOperation.Insert));
				if (linqDataSourceStatusEventArgs.ExceptionHandled)
				{
					return -1;
				}
				throw;
			}
			linqDataSourceStatusEventArgs = new LinqDataSourceStatusEventArgs(linqDataSourceInsertEventArgs.NewObject);
			this.OnInserted(linqDataSourceStatusEventArgs);
			return 1;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001DE94 File Offset: 0x0001C094
		private static bool MemberIsStatic(MemberInfo member)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				return fieldInfo.IsStatic;
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				MethodInfo getMethod = propertyInfo.GetGetMethod();
				return getMethod != null && getMethod.IsStatic;
			}
			return false;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001DEE4 File Offset: 0x0001C0E4
		protected virtual void OnContextCreated(LinqDataSourceStatusEventArgs e)
		{
			EventHandler<LinqDataSourceStatusEventArgs> eventHandler = (EventHandler<LinqDataSourceStatusEventArgs>)base.Events[ContextDataSourceView.EventContextCreated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001DF14 File Offset: 0x0001C114
		protected virtual void OnContextCreating(LinqDataSourceContextEventArgs e)
		{
			EventHandler<LinqDataSourceContextEventArgs> eventHandler = (EventHandler<LinqDataSourceContextEventArgs>)base.Events[ContextDataSourceView.EventContextCreating];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001DF44 File Offset: 0x0001C144
		protected virtual void OnContextDisposing(LinqDataSourceDisposeEventArgs e)
		{
			EventHandler<LinqDataSourceDisposeEventArgs> eventHandler = (EventHandler<LinqDataSourceDisposeEventArgs>)base.Events[ContextDataSourceView.EventContextDisposing];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001DF74 File Offset: 0x0001C174
		protected virtual void OnDeleted(LinqDataSourceStatusEventArgs e)
		{
			EventHandler<LinqDataSourceStatusEventArgs> eventHandler = (EventHandler<LinqDataSourceStatusEventArgs>)base.Events[LinqDataSourceView.EventDeleted];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001DFA4 File Offset: 0x0001C1A4
		protected virtual void OnDeleting(LinqDataSourceDeleteEventArgs e)
		{
			EventHandler<LinqDataSourceDeleteEventArgs> eventHandler = (EventHandler<LinqDataSourceDeleteEventArgs>)base.Events[LinqDataSourceView.EventDeleting];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001DFD4 File Offset: 0x0001C1D4
		protected virtual void OnException(DynamicValidatorEventArgs e)
		{
			EventHandler<DynamicValidatorEventArgs> eventHandler = (EventHandler<DynamicValidatorEventArgs>)base.Events[LinqDataSourceView.EventException];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001E004 File Offset: 0x0001C204
		protected virtual void OnInserted(LinqDataSourceStatusEventArgs e)
		{
			EventHandler<LinqDataSourceStatusEventArgs> eventHandler = (EventHandler<LinqDataSourceStatusEventArgs>)base.Events[LinqDataSourceView.EventInserted];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001E034 File Offset: 0x0001C234
		protected virtual void OnInserting(LinqDataSourceInsertEventArgs e)
		{
			EventHandler<LinqDataSourceInsertEventArgs> eventHandler = (EventHandler<LinqDataSourceInsertEventArgs>)base.Events[LinqDataSourceView.EventInserting];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001E064 File Offset: 0x0001C264
		protected virtual void OnSelected(LinqDataSourceStatusEventArgs e)
		{
			EventHandler<LinqDataSourceStatusEventArgs> eventHandler = (EventHandler<LinqDataSourceStatusEventArgs>)base.Events[QueryableDataSourceView.EventSelected];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001E094 File Offset: 0x0001C294
		protected virtual void OnSelecting(LinqDataSourceSelectEventArgs e)
		{
			EventHandler<LinqDataSourceSelectEventArgs> eventHandler = (EventHandler<LinqDataSourceSelectEventArgs>)base.Events[QueryableDataSourceView.EventSelecting];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001E0C4 File Offset: 0x0001C2C4
		protected virtual void OnUpdated(LinqDataSourceStatusEventArgs e)
		{
			EventHandler<LinqDataSourceStatusEventArgs> eventHandler = (EventHandler<LinqDataSourceStatusEventArgs>)base.Events[LinqDataSourceView.EventUpdated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001E0F4 File Offset: 0x0001C2F4
		protected virtual void OnUpdating(LinqDataSourceUpdateEventArgs e)
		{
			EventHandler<LinqDataSourceUpdateEventArgs> eventHandler = (EventHandler<LinqDataSourceUpdateEventArgs>)base.Events[LinqDataSourceView.EventUpdating];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001E124 File Offset: 0x0001C324
		internal void ReleaseSelectContexts()
		{
			if (this._selectContexts != null)
			{
				foreach (ContextDataSourceContextData contextDataSourceContextData in this._selectContexts)
				{
					this.DisposeContext(contextDataSourceContextData.Context);
				}
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000032F4 File Offset: 0x000014F4
		protected virtual void ResetDataObject(object table, object dataObject)
		{
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001E184 File Offset: 0x0001C384
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.ExecuteSelect(arguments);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001E190 File Offset: 0x0001C390
		private Dictionary<string, Exception> SetDataObjectProperties(object oldDataObject, object newDataObject)
		{
			Dictionary<string, Exception> dictionary = null;
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(oldDataObject);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.PropertyType.IsSerializable && !propertyDescriptor.IsReadOnly)
				{
					object value = propertyDescriptor.GetValue(newDataObject);
					try
					{
						propertyDescriptor.SetValue(oldDataObject, value);
					}
					catch (Exception value2)
					{
						if (dictionary == null)
						{
							dictionary = new Dictionary<string, Exception>(StringComparer.OrdinalIgnoreCase);
						}
						dictionary[propertyDescriptor.Name] = value2;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001E240 File Offset: 0x0001C440
		protected override void StoreOriginalValues(IList results)
		{
			Type entityType = this.EntityType;
			IDictionary<string, MetaDataMember> columns = this.GetTableMetaDataMembers((ITable)base.EntitySet, entityType).ToDictionary((MetaDataMember c) => c.Member.Name);
			base.StoreOriginalValues(results, (PropertyDescriptor p) => columns.ContainsKey(p.Name) && (columns[p.Name].IsPrimaryKey || columns[p.Name].IsVersion || columns[p.Name].UpdateCheck != UpdateCheck.Never));
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001E2AC File Offset: 0x0001C4AC
		protected virtual void UpdateDataObject(object dataContext, object table, object oldDataObject, object newDataObject)
		{
			this._linqToSql.Attach((ITable)table, oldDataObject);
			Dictionary<string, Exception> dictionary = this.SetDataObjectProperties(oldDataObject, newDataObject);
			if (dictionary != null)
			{
				throw new LinqDataSourceValidationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_ValidationFailed, new object[]
				{
					oldDataObject.GetType(),
					dictionary.Values.First<Exception>().Message
				}), dictionary);
			}
			this._linqToSql.SubmitChanges((DataContext)dataContext);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001E324 File Offset: 0x0001C524
		protected override int UpdateObject(object oldEntity, object newEntity)
		{
			LinqDataSourceUpdateEventArgs linqDataSourceUpdateEventArgs = new LinqDataSourceUpdateEventArgs(oldEntity, newEntity);
			this.OnUpdating(linqDataSourceUpdateEventArgs);
			if (linqDataSourceUpdateEventArgs.Cancel)
			{
				return -1;
			}
			LinqDataSourceStatusEventArgs linqDataSourceStatusEventArgs;
			try
			{
				this.UpdateDataObject(base.Context, base.EntitySet, linqDataSourceUpdateEventArgs.OriginalObject, linqDataSourceUpdateEventArgs.NewObject);
			}
			catch (Exception exception)
			{
				this.ResetDataObject(base.EntitySet, linqDataSourceUpdateEventArgs.OriginalObject);
				linqDataSourceStatusEventArgs = new LinqDataSourceStatusEventArgs(exception);
				this.OnUpdated(linqDataSourceStatusEventArgs);
				this.OnException(new DynamicValidatorEventArgs(exception, DynamicDataSourceOperation.Update));
				if (linqDataSourceStatusEventArgs.ExceptionHandled)
				{
					return -1;
				}
				throw;
			}
			linqDataSourceStatusEventArgs = new LinqDataSourceStatusEventArgs(linqDataSourceUpdateEventArgs.NewObject);
			this.OnUpdated(linqDataSourceStatusEventArgs);
			return 1;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001E3D0 File Offset: 0x0001C5D0
		protected virtual void ValidateContextType(Type contextType, bool selecting)
		{
			if (!selecting && !typeof(DataContext).IsAssignableFrom(contextType))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_InvalidContextType, new object[]
				{
					this._owner.ID
				}));
			}
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001E410 File Offset: 0x0001C610
		protected virtual void ValidateDeleteSupported(IDictionary keys, IDictionary oldValues)
		{
			if (!this.CanDelete)
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_DeleteNotSupported, new object[]
				{
					this._owner.ID
				}));
			}
			this.ValidateEditSupported();
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001E44C File Offset: 0x0001C64C
		protected virtual void ValidateEditSupported()
		{
			if (!string.IsNullOrEmpty(this.GroupBy))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_GroupByNotSupportedOnEdit, new object[]
				{
					this._owner.ID
				}));
			}
			if (!string.IsNullOrEmpty(this.SelectNew))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_SelectNewNotSupportedOnEdit, new object[]
				{
					this._owner.ID
				}));
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001E4C8 File Offset: 0x0001C6C8
		protected virtual void ValidateInsertSupported(IDictionary values)
		{
			if (!this.CanInsert)
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_InsertNotSupported, new object[]
				{
					this._owner.ID
				}));
			}
			this.ValidateEditSupported();
			if (values == null || values.Count == 0)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_InsertRequiresValues, new object[]
				{
					this._owner.ID
				}));
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001E540 File Offset: 0x0001C740
		protected virtual void ValidateTableType(Type tableType, bool selecting)
		{
			if (!selecting && (!tableType.IsGenericType || tableType.GetGenericArguments().Length != 1 || !typeof(ITable).IsAssignableFrom(tableType)))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_InvalidTablePropertyType, new object[]
				{
					this._owner.ID
				}));
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001E59E File Offset: 0x0001C79E
		protected virtual void ValidateUpdateSupported(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			if (!this.CanUpdate)
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSourceView_UpdateNotSupported, new object[]
				{
					this._owner.ID
				}));
			}
			this.ValidateEditSupported();
		}

		// Token: 0x04000273 RID: 627
		private static readonly object EventDeleted = new object();

		// Token: 0x04000274 RID: 628
		private static readonly object EventDeleting = new object();

		// Token: 0x04000275 RID: 629
		private static readonly object EventException = new object();

		// Token: 0x04000276 RID: 630
		private static readonly object EventInserted = new object();

		// Token: 0x04000277 RID: 631
		private static readonly object EventInserting = new object();

		// Token: 0x04000278 RID: 632
		private static readonly object EventUpdated = new object();

		// Token: 0x04000279 RID: 633
		private static readonly object EventUpdating = new object();

		// Token: 0x0400027A RID: 634
		private HttpContext _context;

		// Token: 0x0400027B RID: 635
		private Type _contextType;

		// Token: 0x0400027C RID: 636
		private string _contextTypeName;

		// Token: 0x0400027D RID: 637
		private LinqDataSource _owner;

		// Token: 0x0400027E RID: 638
		private List<ContextDataSourceContextData> _selectContexts;

		// Token: 0x0400027F RID: 639
		private bool _enableDelete;

		// Token: 0x04000280 RID: 640
		private bool _enableInsert;

		// Token: 0x04000281 RID: 641
		private bool _enableObjectTracking = true;

		// Token: 0x04000282 RID: 642
		private bool _enableUpdate;

		// Token: 0x04000283 RID: 643
		private bool _isNewContext;

		// Token: 0x04000284 RID: 644
		private ILinqToSql _linqToSql;

		// Token: 0x04000285 RID: 645
		private bool _reuseSelectContext;

		// Token: 0x04000286 RID: 646
		private bool _storeOriginalValuesInViewState = true;

		// Token: 0x04000287 RID: 647
		private bool _storeOriginalValues;

		// Token: 0x04000288 RID: 648
		private object _selectResult;
	}
}
