using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000597 RID: 1431
	internal sealed class PocoPropertyAccessorStrategy : IPropertyAccessorStrategy
	{
		// Token: 0x060037E2 RID: 14306 RVA: 0x00109308 File Offset: 0x00107508
		public PocoPropertyAccessorStrategy(object entity)
		{
			this._entity = entity;
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x00109318 File Offset: 0x00107518
		public object GetNavigationPropertyValue(RelatedEnd relatedEnd)
		{
			object result = null;
			if (relatedEnd != null)
			{
				if (relatedEnd.TargetAccessor.ValueGetter == null)
				{
					Type declaringType = PocoPropertyAccessorStrategy.GetDeclaringType(relatedEnd);
					PropertyInfo topProperty = declaringType.GetTopProperty(relatedEnd.TargetAccessor.PropertyName);
					if (topProperty == null)
					{
						throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, declaringType.FullName));
					}
					EntityProxyFactory entityProxyFactory = new EntityProxyFactory();
					relatedEnd.TargetAccessor.ValueGetter = entityProxyFactory.CreateBaseGetter(topProperty.DeclaringType, topProperty);
				}
				bool state = relatedEnd.DisableLazyLoading();
				try
				{
					result = relatedEnd.TargetAccessor.ValueGetter(this._entity);
				}
				catch (Exception innerException)
				{
					throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, this._entity.GetType().FullName), innerException);
				}
				finally
				{
					relatedEnd.ResetLazyLoading(state);
				}
			}
			return result;
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x0010940C File Offset: 0x0010760C
		public void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
			if (relatedEnd != null)
			{
				if (relatedEnd.TargetAccessor.ValueSetter == null)
				{
					Type declaringType = PocoPropertyAccessorStrategy.GetDeclaringType(relatedEnd);
					PropertyInfo topProperty = declaringType.GetTopProperty(relatedEnd.TargetAccessor.PropertyName);
					if (topProperty == null)
					{
						throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, declaringType.FullName));
					}
					EntityProxyFactory entityProxyFactory = new EntityProxyFactory();
					relatedEnd.TargetAccessor.ValueSetter = entityProxyFactory.CreateBaseSetter(topProperty.DeclaringType, topProperty);
				}
				try
				{
					relatedEnd.TargetAccessor.ValueSetter(this._entity, value);
				}
				catch (Exception innerException)
				{
					throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, this._entity.GetType().FullName), innerException);
				}
			}
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x001094DC File Offset: 0x001076DC
		private static Type GetDeclaringType(RelatedEnd relatedEnd)
		{
			if (relatedEnd.NavigationProperty != null)
			{
				EntityType type = (EntityType)relatedEnd.NavigationProperty.DeclaringType;
				ObjectTypeMapping objectMapping = Util.GetObjectMapping(type, relatedEnd.WrappedOwner.Context.MetadataWorkspace);
				return objectMapping.ClrType.ClrType;
			}
			return relatedEnd.WrappedOwner.IdentityType;
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x00109530 File Offset: 0x00107730
		private static Type GetNavigationPropertyType(Type entityType, string propertyName)
		{
			PropertyInfo topProperty = entityType.GetTopProperty(propertyName);
			Type result;
			if (topProperty != null)
			{
				result = topProperty.PropertyType;
			}
			else
			{
				FieldInfo field = entityType.GetField(propertyName);
				if (!(field != null))
				{
					throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(propertyName, entityType.FullName));
				}
				result = field.FieldType;
			}
			return result;
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x00109584 File Offset: 0x00107784
		public void CollectionAdd(RelatedEnd relatedEnd, object value)
		{
			object entity = this._entity;
			try
			{
				object obj = this.GetNavigationPropertyValue(relatedEnd);
				if (obj == null)
				{
					obj = this.CollectionCreate(relatedEnd);
					this.SetNavigationPropertyValue(relatedEnd, obj);
				}
				if (!object.ReferenceEquals(obj, relatedEnd))
				{
					if (relatedEnd.TargetAccessor.CollectionAdd == null)
					{
						relatedEnd.TargetAccessor.CollectionAdd = PocoPropertyAccessorStrategy.CreateCollectionAddFunction(PocoPropertyAccessorStrategy.GetDeclaringType(relatedEnd), relatedEnd.TargetAccessor.PropertyName);
					}
					relatedEnd.TargetAccessor.CollectionAdd(obj, value);
				}
			}
			catch (Exception innerException)
			{
				throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, entity.GetType().FullName), innerException);
			}
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x00109634 File Offset: 0x00107834
		private static Action<object, object> CreateCollectionAddFunction(Type type, string propertyName)
		{
			Type navigationPropertyType = PocoPropertyAccessorStrategy.GetNavigationPropertyType(type, propertyName);
			Type collectionElementType = EntityUtil.GetCollectionElementType(navigationPropertyType);
			MethodInfo methodInfo = PocoPropertyAccessorStrategy.AddToCollectionGeneric.MakeGenericMethod(new Type[]
			{
				collectionElementType
			});
			return (Action<object, object>)methodInfo.Invoke(null, null);
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x001096B7 File Offset: 0x001078B7
		private static Action<object, object> AddToCollection<T>()
		{
			return delegate(object collectionArg, object item)
			{
				ICollection<T> collection = (ICollection<T>)collectionArg;
				Array array = collection as Array;
				if (array != null && array.IsFixedSize)
				{
					throw new InvalidOperationException(Strings.RelatedEnd_CannotAddToFixedSizeArray(array.GetType()));
				}
				collection.Add((T)((object)item));
			};
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x001096C8 File Offset: 0x001078C8
		public bool CollectionRemove(RelatedEnd relatedEnd, object value)
		{
			object entity = this._entity;
			try
			{
				object navigationPropertyValue = this.GetNavigationPropertyValue(relatedEnd);
				if (navigationPropertyValue != null)
				{
					if (object.ReferenceEquals(navigationPropertyValue, relatedEnd))
					{
						return true;
					}
					if (relatedEnd.TargetAccessor.CollectionRemove == null)
					{
						relatedEnd.TargetAccessor.CollectionRemove = PocoPropertyAccessorStrategy.CreateCollectionRemoveFunction(PocoPropertyAccessorStrategy.GetDeclaringType(relatedEnd), relatedEnd.TargetAccessor.PropertyName);
					}
					return relatedEnd.TargetAccessor.CollectionRemove(navigationPropertyValue, value);
				}
			}
			catch (Exception innerException)
			{
				throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, entity.GetType().FullName), innerException);
			}
			return false;
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x00109770 File Offset: 0x00107970
		private static Func<object, object, bool> CreateCollectionRemoveFunction(Type type, string propertyName)
		{
			Type navigationPropertyType = PocoPropertyAccessorStrategy.GetNavigationPropertyType(type, propertyName);
			Type collectionElementType = EntityUtil.GetCollectionElementType(navigationPropertyType);
			MethodInfo methodInfo = PocoPropertyAccessorStrategy.RemoveFromCollectionGeneric.MakeGenericMethod(new Type[]
			{
				collectionElementType
			});
			return (Func<object, object, bool>)methodInfo.Invoke(null, null);
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x001097F3 File Offset: 0x001079F3
		private static Func<object, object, bool> RemoveFromCollection<T>()
		{
			return delegate(object collectionArg, object item)
			{
				ICollection<T> collection = (ICollection<T>)collectionArg;
				Array array = collection as Array;
				if (array != null && array.IsFixedSize)
				{
					throw new InvalidOperationException(Strings.RelatedEnd_CannotRemoveFromFixedSizeArray(array.GetType()));
				}
				return collection.Remove((T)((object)item));
			};
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x00109804 File Offset: 0x00107A04
		public object CollectionCreate(RelatedEnd relatedEnd)
		{
			if (this._entity is IEntityWithRelationships)
			{
				return relatedEnd;
			}
			if (relatedEnd.TargetAccessor.CollectionCreate == null)
			{
				Type declaringType = PocoPropertyAccessorStrategy.GetDeclaringType(relatedEnd);
				string propertyName = relatedEnd.TargetAccessor.PropertyName;
				Type navigationPropertyType = PocoPropertyAccessorStrategy.GetNavigationPropertyType(declaringType, propertyName);
				relatedEnd.TargetAccessor.CollectionCreate = PocoPropertyAccessorStrategy.CreateCollectionCreateDelegate(navigationPropertyType, propertyName);
			}
			return relatedEnd.TargetAccessor.CollectionCreate();
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x0010986C File Offset: 0x00107A6C
		private static Func<object> CreateCollectionCreateDelegate(Type navigationPropertyType, string propName)
		{
			Type type = EntityUtil.DetermineCollectionType(navigationPropertyType);
			if (type == null)
			{
				throw new EntityException(Strings.PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType(propName, navigationPropertyType));
			}
			return Expression.Lambda<Func<object>>(DelegateFactory.GetNewExpressionForCollectionType(type), new ParameterExpression[0]).Compile();
		}

		// Token: 0x0400157C RID: 5500
		internal static readonly MethodInfo AddToCollectionGeneric = typeof(PocoPropertyAccessorStrategy).GetOnlyDeclaredMethod("AddToCollection");

		// Token: 0x0400157D RID: 5501
		internal static readonly MethodInfo RemoveFromCollectionGeneric = typeof(PocoPropertyAccessorStrategy).GetOnlyDeclaredMethod("RemoveFromCollection");

		// Token: 0x0400157E RID: 5502
		private readonly object _entity;
	}
}
