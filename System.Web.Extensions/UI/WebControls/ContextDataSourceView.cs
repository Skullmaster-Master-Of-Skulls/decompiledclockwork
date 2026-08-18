using System;
using System.Collections;
using System.Reflection;
using System.Web.Compilation;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200008E RID: 142
	public abstract class ContextDataSourceView : QueryableDataSourceView
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x0001B094 File Offset: 0x00019294
		protected ContextDataSourceView(DataSourceControl owner, string viewName, HttpContext context) : base(owner, viewName, context)
		{
			this._owner = owner;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001B0A6 File Offset: 0x000192A6
		internal ContextDataSourceView(DataSourceControl owner, string viewName, HttpContext context, IDynamicQueryable queryable) : base(owner, viewName, context, queryable)
		{
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001B0B3 File Offset: 0x000192B3
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x0001B0C4 File Offset: 0x000192C4
		public string EntitySetName
		{
			get
			{
				return this._entitySetName ?? string.Empty;
			}
			set
			{
				if (this._entitySetName != value)
				{
					this._entitySetName = value;
					this._entitySetType = null;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0001B0ED File Offset: 0x000192ED
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x0001B0FE File Offset: 0x000192FE
		public string EntityTypeName
		{
			get
			{
				return this._entityTypeName ?? string.Empty;
			}
			set
			{
				if (this._entityTypeName != value)
				{
					this._entityTypeName = value;
					this._entityType = null;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0001B128 File Offset: 0x00019328
		protected override Type EntityType
		{
			get
			{
				string entityTypeName = this.EntityTypeName;
				if (this._entityType == null)
				{
					this._entityType = (ContextDataSourceView.GetDataObjectTypeByName(entityTypeName) ?? this.GetDataObjectType(this.EntitySetType));
				}
				return this._entityType;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001B16C File Offset: 0x0001936C
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0001B17D File Offset: 0x0001937D
		public virtual string ContextTypeName
		{
			get
			{
				return this._contextTypeName ?? string.Empty;
			}
			set
			{
				if (this._contextTypeName != value)
				{
					this._contextTypeName = value;
					this._contextType = null;
					this.OnDataSourceViewChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001B1A6 File Offset: 0x000193A6
		public virtual Type ContextType
		{
			get
			{
				if (this._contextType == null && !string.IsNullOrEmpty(this.ContextTypeName))
				{
					this._contextType = DataSourceHelper.GetType(this.ContextTypeName);
				}
				return this._contextType;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0001B1DA File Offset: 0x000193DA
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x0001B1E2 File Offset: 0x000193E2
		protected object Context { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001B1EB File Offset: 0x000193EB
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x0001B1F3 File Offset: 0x000193F3
		private protected object EntitySet { protected get; private set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0001B1FC File Offset: 0x000193FC
		protected Type EntitySetType
		{
			get
			{
				if (this._entitySetType == null)
				{
					this._entitySetType = this.GetEntitySetType();
				}
				return this._entitySetType;
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001B220 File Offset: 0x00019420
		protected virtual Type GetEntitySetType()
		{
			MemberInfo entitySetMember = this.GetEntitySetMember(this.ContextType);
			if (entitySetMember.MemberType == MemberTypes.Property)
			{
				return ((PropertyInfo)entitySetMember).PropertyType;
			}
			if (entitySetMember.MemberType == MemberTypes.Field)
			{
				return ((FieldInfo)entitySetMember).FieldType;
			}
			throw new InvalidOperationException("EntitySet Type must be a field or property");
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001B270 File Offset: 0x00019470
		private MemberInfo GetEntitySetMember(Type contextType)
		{
			string entitySetName = this.EntitySetName;
			if (string.IsNullOrEmpty(entitySetName))
			{
				return null;
			}
			MemberInfo[] array = contextType.FindMembers(MemberTypes.Field | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null);
			for (int i = 0; i < array.Length; i++)
			{
				if (string.Equals(array[i].Name, entitySetName, StringComparison.OrdinalIgnoreCase))
				{
					return array[i];
				}
			}
			return null;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001B2C0 File Offset: 0x000194C0
		private static Type GetDataObjectTypeByName(string typeName)
		{
			Type result = null;
			if (!string.IsNullOrEmpty(typeName))
			{
				result = BuildManager.GetType(typeName, false, true);
			}
			return result;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001B2E4 File Offset: 0x000194E4
		protected virtual Type GetDataObjectType(Type type)
		{
			if (type.IsGenericType)
			{
				Type[] genericArguments = type.GetGenericArguments();
				if (genericArguments.Length == 1)
				{
					return genericArguments[0];
				}
			}
			return typeof(object);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001B314 File Offset: 0x00019514
		protected virtual ContextDataSourceContextData CreateContext(DataSourceOperation operation)
		{
			return null;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001B318 File Offset: 0x00019518
		protected override object GetSource(QueryContext context)
		{
			ContextDataSourceContextData contextDataSourceContextData = this.CreateContext(DataSourceOperation.Select);
			if (contextDataSourceContextData != null)
			{
				this.Context = contextDataSourceContextData.Context;
				this.EntitySet = contextDataSourceContextData.EntitySet;
				return this.EntitySet;
			}
			return null;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001B350 File Offset: 0x00019550
		protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			try
			{
				ContextDataSourceContextData contextDataSourceContextData = this.CreateContext(DataSourceOperation.Update);
				if (contextDataSourceContextData != null)
				{
					this.Context = contextDataSourceContextData.Context;
					this.EntitySet = contextDataSourceContextData.EntitySet;
					return base.ExecuteUpdate(keys, values, oldValues);
				}
			}
			finally
			{
				this.DisposeContext();
			}
			return -1;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001B3AC File Offset: 0x000195AC
		protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
		{
			try
			{
				ContextDataSourceContextData contextDataSourceContextData = this.CreateContext(DataSourceOperation.Delete);
				if (contextDataSourceContextData != null)
				{
					this.Context = contextDataSourceContextData.Context;
					this.EntitySet = contextDataSourceContextData.EntitySet;
					return base.ExecuteDelete(keys, oldValues);
				}
			}
			finally
			{
				this.DisposeContext();
			}
			return -1;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001B408 File Offset: 0x00019608
		protected override int ExecuteInsert(IDictionary values)
		{
			try
			{
				ContextDataSourceContextData contextDataSourceContextData = this.CreateContext(DataSourceOperation.Insert);
				if (contextDataSourceContextData != null)
				{
					this.Context = contextDataSourceContextData.Context;
					this.EntitySet = contextDataSourceContextData.EntitySet;
					return base.ExecuteInsert(values);
				}
			}
			finally
			{
				this.DisposeContext();
			}
			return -1;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001B460 File Offset: 0x00019660
		protected virtual void DisposeContext(object dataContext)
		{
			if (dataContext != null)
			{
				IDisposable disposable = dataContext as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				dataContext = null;
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001B483 File Offset: 0x00019683
		protected void DisposeContext()
		{
			this.DisposeContext(this.Context);
		}

		// Token: 0x0400022C RID: 556
		private string _entitySetName;

		// Token: 0x0400022D RID: 557
		private string _contextTypeName;

		// Token: 0x0400022E RID: 558
		private Type _contextType;

		// Token: 0x0400022F RID: 559
		private string _entityTypeName;

		// Token: 0x04000230 RID: 560
		private Type _entityType;

		// Token: 0x04000231 RID: 561
		private Type _entitySetType;

		// Token: 0x04000232 RID: 562
		private Control _owner;

		// Token: 0x04000233 RID: 563
		protected static readonly object EventContextCreating = new object();

		// Token: 0x04000234 RID: 564
		protected static readonly object EventContextCreated = new object();

		// Token: 0x04000235 RID: 565
		protected static readonly object EventContextDisposing = new object();
	}
}
