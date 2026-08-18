using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200057D RID: 1405
	internal sealed class EntityProxyTypeInfo
	{
		// Token: 0x060036DD RID: 14045 RVA: 0x001049D0 File Offset: 0x00102BD0
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		internal EntityProxyTypeInfo(Type proxyType, ClrEntityType ospaceEntityType, DynamicMethod initializeCollections, List<PropertyInfo> baseGetters, List<PropertyInfo> baseSetters, MetadataWorkspace workspace)
		{
			this._proxyType = proxyType;
			this._entityType = ospaceEntityType;
			this._initializeCollections = initializeCollections;
			foreach (AssociationType associationType in EntityProxyTypeInfo.GetAllRelationshipsForType(workspace, proxyType))
			{
				this._navigationPropertyAssociationTypes.Add(associationType.FullName, associationType);
				if (associationType.Name != associationType.FullName)
				{
					this._navigationPropertyAssociationTypes.Add(associationType.Name, associationType);
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
				if (entityWrapper != null && !object.ReferenceEquals(entityWrapper.Entity, proxy))
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
			MethodInfo publicInstanceMethod = proxyType.GetPublicInstanceMethod("GetBasePropertyValue", new Type[]
			{
				typeof(string)
			});
			if (publicInstanceMethod != null)
			{
				this._baseGetter = Expression.Lambda<Func<object, string, object>>(Expression.Call(Expression.Convert(parameterExpression, proxyType), publicInstanceMethod, new Expression[]
				{
					parameterExpression3
				}), new ParameterExpression[]
				{
					parameterExpression,
					parameterExpression3
				}).Compile();
			}
			ParameterExpression parameterExpression4 = Expression.Parameter(typeof(object), "propertyName");
			MethodInfo publicInstanceMethod2 = proxyType.GetPublicInstanceMethod("SetBasePropertyValue", new Type[]
			{
				typeof(string),
				typeof(object)
			});
			if (publicInstanceMethod2 != null)
			{
				this._baseSetter = Expression.Lambda<Action<object, string, object>>(Expression.Call(Expression.Convert(parameterExpression, proxyType), publicInstanceMethod2, parameterExpression3, parameterExpression4), new ParameterExpression[]
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
			this._createObject = DelegateFactory.CreateConstructor(proxyType);
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x00104CFC File Offset: 0x00102EFC
		internal static IEnumerable<AssociationType> GetAllRelationshipsForType(MetadataWorkspace workspace, Type clrType)
		{
			return from a in ((ObjectItemCollection)workspace.GetItemCollection(DataSpace.OSpace)).GetItems<AssociationType>()
			where EntityProxyTypeInfo.IsEndMemberForType(a.AssociationEndMembers[0], clrType) || EntityProxyTypeInfo.IsEndMemberForType(a.AssociationEndMembers[1], clrType)
			select a;
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x00104D38 File Offset: 0x00102F38
		private static bool IsEndMemberForType(AssociationEndMember end, Type clrType)
		{
			RefType refType = end.TypeUsage.EdmType as RefType;
			return refType != null && refType.ElementType.ClrType.IsAssignableFrom(clrType);
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x00104D6C File Offset: 0x00102F6C
		internal object CreateProxyObject()
		{
			return this._createObject();
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060036E1 RID: 14049 RVA: 0x00104D79 File Offset: 0x00102F79
		internal Type ProxyType
		{
			get
			{
				return this._proxyType;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060036E2 RID: 14050 RVA: 0x00104D81 File Offset: 0x00102F81
		internal DynamicMethod InitializeEntityCollections
		{
			get
			{
				return this._initializeCollections;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x00104D89 File Offset: 0x00102F89
		public Func<object, string, object> BaseGetter
		{
			get
			{
				return this._baseGetter;
			}
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x00104D91 File Offset: 0x00102F91
		public bool ContainsBaseGetter(string propertyName)
		{
			return this.BaseGetter != null && this._propertiesWithBaseGetter.Contains(propertyName);
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x00104DA9 File Offset: 0x00102FA9
		public bool ContainsBaseSetter(string propertyName)
		{
			return this.BaseSetter != null && this._propertiesWithBaseSetter.Contains(propertyName);
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x060036E6 RID: 14054 RVA: 0x00104DC1 File Offset: 0x00102FC1
		public Action<object, string, object> BaseSetter
		{
			get
			{
				return this._baseSetter;
			}
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x00104DC9 File Offset: 0x00102FC9
		public bool TryGetNavigationPropertyAssociationType(string relationshipName, out AssociationType associationType)
		{
			return this._navigationPropertyAssociationTypes.TryGetValue(relationshipName, out associationType);
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x00104DD8 File Offset: 0x00102FD8
		public IEnumerable<AssociationType> GetAllAssociationTypes()
		{
			return this._navigationPropertyAssociationTypes.Values.Distinct<AssociationType>();
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x00104DEA File Offset: 0x00102FEA
		public void ValidateType(ClrEntityType ospaceEntityType)
		{
			if (ospaceEntityType != this._entityType && ospaceEntityType.HashedDescription != this._entityType.HashedDescription)
			{
				throw new InvalidOperationException(Strings.EntityProxyTypeInfo_DuplicateOSpaceType(ospaceEntityType.ClrType.FullName));
			}
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x00104E23 File Offset: 0x00103023
		internal IEntityWrapper SetEntityWrapper(IEntityWrapper wrapper)
		{
			return this.Proxy_SetEntityWrapper(wrapper.Entity, wrapper) as IEntityWrapper;
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x00104E3C File Offset: 0x0010303C
		internal IEntityWrapper GetEntityWrapper(object entity)
		{
			return this.Proxy_GetEntityWrapper(entity) as IEntityWrapper;
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x060036EC RID: 14060 RVA: 0x00104E4F File Offset: 0x0010304F
		internal Func<object, object> EntityWrapperDelegate
		{
			get
			{
				return this.Proxy_GetEntityWrapper;
			}
		}

		// Token: 0x04001505 RID: 5381
		internal const string EntityWrapperFieldName = "_entityWrapper";

		// Token: 0x04001506 RID: 5382
		private const string InitializeEntityCollectionsName = "InitializeEntityCollections";

		// Token: 0x04001507 RID: 5383
		private readonly Type _proxyType;

		// Token: 0x04001508 RID: 5384
		private readonly ClrEntityType _entityType;

		// Token: 0x04001509 RID: 5385
		private readonly DynamicMethod _initializeCollections;

		// Token: 0x0400150A RID: 5386
		private readonly Func<object, string, object> _baseGetter;

		// Token: 0x0400150B RID: 5387
		private readonly HashSet<string> _propertiesWithBaseGetter;

		// Token: 0x0400150C RID: 5388
		private readonly Action<object, string, object> _baseSetter;

		// Token: 0x0400150D RID: 5389
		private readonly HashSet<string> _propertiesWithBaseSetter;

		// Token: 0x0400150E RID: 5390
		private readonly Func<object, object> Proxy_GetEntityWrapper;

		// Token: 0x0400150F RID: 5391
		private readonly Func<object, object, object> Proxy_SetEntityWrapper;

		// Token: 0x04001510 RID: 5392
		private readonly Func<object> _createObject;

		// Token: 0x04001511 RID: 5393
		private readonly Dictionary<string, AssociationType> _navigationPropertyAssociationTypes = new Dictionary<string, AssociationType>();
	}
}
