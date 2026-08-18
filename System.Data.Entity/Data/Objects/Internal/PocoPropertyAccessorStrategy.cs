using System;
using System.Collections.Generic;
using System.Data.Common.Internal.Materialization;
using System.Data.Entity;
using System.Data.Mapping;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Objects.Internal
{
	// Token: 0x0200016E RID: 366
	internal sealed class PocoPropertyAccessorStrategy : IPropertyAccessorStrategy
	{
		// Token: 0x06001AF3 RID: 6899 RVA: 0x0005BE83 File Offset: 0x0005A083
		public PocoPropertyAccessorStrategy(object entity)
		{
			this._entity = entity;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0005BE94 File Offset: 0x0005A094
		public object GetNavigationPropertyValue(RelatedEnd relatedEnd)
		{
			object result = null;
			if (relatedEnd != null)
			{
				if (relatedEnd.TargetAccessor.ValueGetter == null)
				{
					Type declaringType = PocoPropertyAccessorStrategy.GetDeclaringType(relatedEnd);
					PropertyInfo topProperty = EntityUtil.GetTopProperty(ref declaringType, relatedEnd.TargetAccessor.PropertyName);
					if (topProperty == null)
					{
						throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, declaringType.FullName));
					}
					EntityProxyFactory entityProxyFactory = new EntityProxyFactory();
					relatedEnd.TargetAccessor.ValueGetter = entityProxyFactory.CreateBaseGetter(declaringType, topProperty);
				}
				try
				{
					result = relatedEnd.TargetAccessor.ValueGetter(this._entity);
				}
				catch (Exception innerException)
				{
					throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, this._entity.GetType().FullName), innerException);
				}
			}
			return result;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0005BF64 File Offset: 0x0005A164
		public void SetNavigationPropertyValue(RelatedEnd relatedEnd, object value)
		{
			if (relatedEnd != null)
			{
				if (relatedEnd.TargetAccessor.ValueSetter == null)
				{
					Type declaringType = PocoPropertyAccessorStrategy.GetDeclaringType(relatedEnd);
					PropertyInfo topProperty = EntityUtil.GetTopProperty(ref declaringType, relatedEnd.TargetAccessor.PropertyName);
					if (topProperty == null)
					{
						throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, declaringType.FullName));
					}
					EntityProxyFactory entityProxyFactory = new EntityProxyFactory();
					relatedEnd.TargetAccessor.ValueSetter = entityProxyFactory.CreateBaseSetter(declaringType, topProperty);
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

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0005C030 File Offset: 0x0005A230
		private static Type GetDeclaringType(RelatedEnd relatedEnd)
		{
			if (relatedEnd.NavigationProperty != null)
			{
				EntityType type = (EntityType)relatedEnd.NavigationProperty.DeclaringType;
				ObjectTypeMapping objectMapping = System.Data.Common.Internal.Materialization.Util.GetObjectMapping(type, relatedEnd.WrappedOwner.Context.MetadataWorkspace);
				return objectMapping.ClrType.ClrType;
			}
			return relatedEnd.WrappedOwner.IdentityType;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0005C084 File Offset: 0x0005A284
		private static Type GetNavigationPropertyType(Type entityType, string propertyName)
		{
			PropertyInfo topProperty = EntityUtil.GetTopProperty(entityType, propertyName);
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

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0005C0D8 File Offset: 0x0005A2D8
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
				if (obj != relatedEnd)
				{
					if (relatedEnd.TargetAccessor.CollectionAdd == null)
					{
						relatedEnd.TargetAccessor.CollectionAdd = PocoPropertyAccessorStrategy.CreateCollectionAddFunction(entity.GetType(), relatedEnd.TargetAccessor.PropertyName);
					}
					relatedEnd.TargetAccessor.CollectionAdd(obj, value);
				}
			}
			catch (Exception innerException)
			{
				throw new EntityException(Strings.PocoEntityWrapper_UnableToSetFieldOrProperty(relatedEnd.TargetAccessor.PropertyName, entity.GetType().FullName), innerException);
			}
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0005C184 File Offset: 0x0005A384
		private static Action<object, object> CreateCollectionAddFunction(Type type, string propertyName)
		{
			Type navigationPropertyType = PocoPropertyAccessorStrategy.GetNavigationPropertyType(type, propertyName);
			Type collectionElementType = EntityUtil.GetCollectionElementType(navigationPropertyType);
			Type type2 = typeof(ICollection<>).MakeGenericType(new Type[]
			{
				collectionElementType
			});
			MethodInfo methodInfo = PocoPropertyAccessorStrategy.s_AddToCollectionGeneric.MakeGenericMethod(new Type[]
			{
				collectionElementType
			});
			return (Action<object, object>)methodInfo.Invoke(null, null);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0005C1DC File Offset: 0x0005A3DC
		private static Action<object, object> AddToCollection<T>()
		{
			return delegate(object collectionArg, object item)
			{
				ICollection<T> collection = (ICollection<T>)collectionArg;
				Array array = collection as Array;
				if (array != null && array.IsFixedSize)
				{
					throw EntityUtil.CannotAddToFixedSizeArray(array);
				}
				collection.Add((T)((object)item));
			};
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0005C200 File Offset: 0x0005A400
		public bool CollectionRemove(RelatedEnd relatedEnd, object value)
		{
			object entity = this._entity;
			try
			{
				object navigationPropertyValue = this.GetNavigationPropertyValue(relatedEnd);
				if (navigationPropertyValue != null)
				{
					if (navigationPropertyValue == relatedEnd)
					{
						return true;
					}
					if (relatedEnd.TargetAccessor.CollectionRemove == null)
					{
						relatedEnd.TargetAccessor.CollectionRemove = PocoPropertyAccessorStrategy.CreateCollectionRemoveFunction(entity.GetType(), relatedEnd.TargetAccessor.PropertyName);
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

		// Token: 0x06001AFC RID: 6908 RVA: 0x0005C2A4 File Offset: 0x0005A4A4
		private static Func<object, object, bool> CreateCollectionRemoveFunction(Type type, string propertyName)
		{
			Type navigationPropertyType = PocoPropertyAccessorStrategy.GetNavigationPropertyType(type, propertyName);
			Type collectionElementType = EntityUtil.GetCollectionElementType(navigationPropertyType);
			Type type2 = typeof(ICollection<>).MakeGenericType(new Type[]
			{
				collectionElementType
			});
			MethodInfo methodInfo = PocoPropertyAccessorStrategy.s_RemoveFromCollectionGeneric.MakeGenericMethod(new Type[]
			{
				collectionElementType
			});
			return (Func<object, object, bool>)methodInfo.Invoke(null, null);
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x0005C2FC File Offset: 0x0005A4FC
		private static Func<object, object, bool> RemoveFromCollection<T>()
		{
			return delegate(object collectionArg, object item)
			{
				ICollection<T> collection = (ICollection<T>)collectionArg;
				Array array = collection as Array;
				if (array != null && array.IsFixedSize)
				{
					throw EntityUtil.CannotRemoveFromFixedSizeArray(array);
				}
				return collection.Remove((T)((object)item));
			};
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0005C320 File Offset: 0x0005A520
		public object CollectionCreate(RelatedEnd relatedEnd)
		{
			if (this._entity is IEntityWithRelationships)
			{
				return relatedEnd;
			}
			if (relatedEnd.TargetAccessor.CollectionCreate == null)
			{
				Type type = this._entity.GetType();
				string propertyName = relatedEnd.TargetAccessor.PropertyName;
				Type navigationPropertyType = PocoPropertyAccessorStrategy.GetNavigationPropertyType(type, propertyName);
				relatedEnd.TargetAccessor.CollectionCreate = PocoPropertyAccessorStrategy.CreateCollectionCreateDelegate(type, navigationPropertyType, propertyName);
			}
			return relatedEnd.TargetAccessor.CollectionCreate();
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0005C38C File Offset: 0x0005A58C
		private static Func<object> CreateCollectionCreateDelegate(Type entityType, Type navigationPropertyType, string propName)
		{
			Type type = EntityUtil.DetermineCollectionType(navigationPropertyType);
			if (type == null)
			{
				throw new EntityException(Strings.PocoEntityWrapper_UnableToMaterializeArbitaryNavPropType(propName, navigationPropertyType));
			}
			return Expression.Lambda<Func<object>>(Expression.New(type), new ParameterExpression[0]).Compile();
		}

		// Token: 0x04000B34 RID: 2868
		private static readonly MethodInfo s_AddToCollectionGeneric = typeof(PocoPropertyAccessorStrategy).GetMethod("AddToCollection", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04000B35 RID: 2869
		private static readonly MethodInfo s_RemoveFromCollectionGeneric = typeof(PocoPropertyAccessorStrategy).GetMethod("RemoveFromCollection", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04000B36 RID: 2870
		private object _entity;
	}
}
