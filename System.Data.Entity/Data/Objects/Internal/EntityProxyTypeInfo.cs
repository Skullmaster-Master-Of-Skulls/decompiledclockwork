using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000176 RID: 374
	internal sealed class EntityProxyTypeInfo
	{
		// Token: 0x06001B48 RID: 6984 RVA: 0x0005E948 File Offset: 0x0005CB48
		internal EntityProxyTypeInfo(Type proxyType, ClrEntityType ospaceEntityType, DynamicMethod initializeCollections, List<PropertyInfo> baseGetters, List<PropertyInfo> baseSetters)
		{
			this._proxyType = proxyType;
			this._entityType = ospaceEntityType;
			this._initializeCollections = initializeCollections;
			this._navigationPropertyAssociationTypes = new Dictionary<Tuple<string, string>, AssociationType>();
			foreach (NavigationProperty navigationProperty in ospaceEntityType.NavigationProperties)
			{
				this._navigationPropertyAssociationTypes.Add(new Tuple<string, string>(navigationProperty.RelationshipType.FullName, navigationProperty.ToEndMember.Name), (AssociationType)navigationProperty.RelationshipType);
				if (navigationProperty.RelationshipType.Name != navigationProperty.RelationshipType.FullName)
				{
					this._navigationPropertyAssociationTypes.Add(new Tuple<string, string>(navigationProperty.RelationshipType.Name, navigationProperty.ToEndMember.Name), (AssociationType)navigationProperty.RelationshipType);
				}
			}
			FieldInfo field = proxyType.GetField("_entityWrapper", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "proxy");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "value");
			Expression<Func<object, object>> expression = Expression.Lambda<Func<object, object>>(Expression.Field(Expression.Convert(parameterExpression, field.DeclaringType), field), new ParameterExpression[]
			{
				parameterExpression
			});
			Func<object, object> getEntityWrapperDelegate = expression.Compile();
			this.Proxy_GetEntityWrapper = delegate(object proxy)
			{
				IEntityWrapper entityWrapper = (IEntityWrapper)getEntityWrapperDelegate(proxy);
				if (entityWrapper != null && entityWrapper.Entity != proxy)
				{
					throw new InvalidOperationException(Strings.EntityProxyTypeInfo_ProxyHasWrongWrapper);
				}
				return entityWrapper;
			};
			this.Proxy_SetEntityWrapper = Expression.Lambda<Func<object, object, object>>(Expression.Assign(Expression.Field(Expression.Convert(parameterExpression, field.DeclaringType), field), parameterExpression2), new ParameterExpression[]
			{
				parameterExpression,
				parameterExpression2
			}).Compile();
			ParameterExpression parameterExpression3 = Expression.Parameter(typeof(string), "propertyName");
			MethodInfo method = proxyType.GetMethod("GetBasePropertyValue", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
			{
				typeof(string)
			}, null);
			if (method != null)
			{
				this._baseGetter = Expression.Lambda<Func<object, string, object>>(Expression.Call(Expression.Convert(parameterExpression, proxyType), method, new Expression[]
				{
					parameterExpression3
				}), new ParameterExpression[]
				{
					parameterExpression,
					parameterExpression3
				}).Compile();
			}
			ParameterExpression parameterExpression4 = Expression.Parameter(typeof(object), "propertyName");
			MethodInfo method2 = proxyType.GetMethod("SetBasePropertyValue", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
			{
				typeof(string),
				typeof(object)
			}, null);
			if (method2 != null)
			{
				this._baseSetter = Expression.Lambda<Action<object, string, object>>(Expression.Call(Expression.Convert(parameterExpression, proxyType), method2, parameterExpression3, parameterExpression4), new ParameterExpression[]
				{
					parameterExpression,
					parameterExpression3,
					parameterExpression4
				}).Compile();
			}
			this._propertiesWithBaseGetter = new HashSet<string>(from p in baseGetters
			select p.Name);
			this._propertiesWithBaseSetter = new HashSet<string>(from p in baseSetters
			select p.Name);
			this._createObject = (LightweightCodeGenerator.CreateConstructor(proxyType) as Func<object>);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0005EC78 File Offset: 0x0005CE78
		internal object CreateProxyObject()
		{
			return this._createObject();
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x0005EC85 File Offset: 0x0005CE85
		internal Type ProxyType
		{
			get
			{
				return this._proxyType;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x0005EC8D File Offset: 0x0005CE8D
		internal DynamicMethod InitializeEntityCollections
		{
			get
			{
				return this._initializeCollections;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0005EC95 File Offset: 0x0005CE95
		public Func<object, string, object> BaseGetter
		{
			get
			{
				return this._baseGetter;
			}
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0005EC9D File Offset: 0x0005CE9D
		public bool ContainsBaseGetter(string propertyName)
		{
			return this.BaseGetter != null && this._propertiesWithBaseGetter.Contains(propertyName);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0005ECB5 File Offset: 0x0005CEB5
		public bool ContainsBaseSetter(string propertyName)
		{
			return this.BaseSetter != null && this._propertiesWithBaseSetter.Contains(propertyName);
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x0005ECCD File Offset: 0x0005CECD
		public Action<object, string, object> BaseSetter
		{
			get
			{
				return this._baseSetter;
			}
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0005ECD5 File Offset: 0x0005CED5
		public bool TryGetNavigationPropertyAssociationType(string relationshipName, string targetRoleName, out AssociationType associationType)
		{
			return this._navigationPropertyAssociationTypes.TryGetValue(new Tuple<string, string>(relationshipName, targetRoleName), out associationType);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0005ECEA File Offset: 0x0005CEEA
		public void ValidateType(ClrEntityType ospaceEntityType)
		{
			if (ospaceEntityType != this._entityType && ospaceEntityType.HashedDescription != this._entityType.HashedDescription)
			{
				throw EntityUtil.DuplicateTypeForProxyType(ospaceEntityType.ClrType);
			}
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0005ED19 File Offset: 0x0005CF19
		internal IEntityWrapper SetEntityWrapper(IEntityWrapper wrapper)
		{
			return this.Proxy_SetEntityWrapper(wrapper.Entity, wrapper) as IEntityWrapper;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0005ED32 File Offset: 0x0005CF32
		internal IEntityWrapper GetEntityWrapper(object entity)
		{
			return this.Proxy_GetEntityWrapper(entity) as IEntityWrapper;
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x0005ED45 File Offset: 0x0005CF45
		internal Func<object, object> EntityWrapperDelegate
		{
			get
			{
				return this.Proxy_GetEntityWrapper;
			}
		}

		// Token: 0x04000B6A RID: 2922
		private readonly Type _proxyType;

		// Token: 0x04000B6B RID: 2923
		private readonly ClrEntityType _entityType;

		// Token: 0x04000B6C RID: 2924
		internal const string EntityWrapperFieldName = "_entityWrapper";

		// Token: 0x04000B6D RID: 2925
		private const string InitializeEntityCollectionsName = "InitializeEntityCollections";

		// Token: 0x04000B6E RID: 2926
		private readonly DynamicMethod _initializeCollections;

		// Token: 0x04000B6F RID: 2927
		private readonly Func<object, string, object> _baseGetter;

		// Token: 0x04000B70 RID: 2928
		private readonly HashSet<string> _propertiesWithBaseGetter;

		// Token: 0x04000B71 RID: 2929
		private readonly Action<object, string, object> _baseSetter;

		// Token: 0x04000B72 RID: 2930
		private readonly HashSet<string> _propertiesWithBaseSetter;

		// Token: 0x04000B73 RID: 2931
		private readonly Func<object, object> Proxy_GetEntityWrapper;

		// Token: 0x04000B74 RID: 2932
		private readonly Func<object, object, object> Proxy_SetEntityWrapper;

		// Token: 0x04000B75 RID: 2933
		private readonly Func<object> _createObject;

		// Token: 0x04000B76 RID: 2934
		private readonly Dictionary<Tuple<string, string>, AssociationType> _navigationPropertyAssociationTypes;
	}
}
